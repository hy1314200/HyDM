using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using Hy.Metadata;
using ESRI.ArcGIS.Geometry;

namespace Hy.Esri.DataManage.Standard.Helper
{
    internal class StandardHelper
    {
        public static IList<StandardItem> GetAllStandard()
        {
            return Environment.NhibernateHelper.GetObjectsByCondition<StandardItem>("from StandardItem sItem where sItem.Type=0");
        }

        public static void InitItemDetial(StandardItem sItem)
        {
            if (sItem == null || sItem.Details != null)
                return;

            switch (sItem.Type)
            {
                case enumItemType.Standard:
                    return;

                case enumItemType.FeatureClass:
                    sItem.Details = Environment.NhibernateHelper.GetObject<FeatureClassInfo>(string.Format("from FeatureClassInfo fcInfo where fcInfo.Parent='{0}'", sItem.ID));
                    return;

                case enumItemType.Table:
                    sItem.Details = Environment.NhibernateHelper.GetObject<TableInfo>(string.Format("from TableInfo tInfo where tInfo.Parent='{0}'", sItem.ID));
                    return;

                default:
                    return;
            }
        }

        public static IField CreateShapeField(FeatureClassInfo fcInfo)
        {
            IFieldEdit shpField = new FieldClass();
            shpField.Name_2=string.IsNullOrWhiteSpace(fcInfo.ShapeFieldName)?"Shape":fcInfo.ShapeFieldName;
            shpField.Type_2 = esriFieldType.esriFieldTypeGeometry;
            IGeometryDefEdit geoDef=new GeometryDefClass();
            geoDef.AvgNumPoints_2 = fcInfo.AvgNumPoints;
            geoDef.GeometryType_2=fcInfo.ShapeType;
            geoDef.GridCount_2=fcInfo.GridCount;            
            geoDef.HasM_2 = fcInfo.HasM;
            geoDef.HasZ_2 = fcInfo.HasZ;
            geoDef.SpatialReference_2 = fcInfo.SpatialReference;
            

            shpField.GeometryDef_2=geoDef;

            return shpField;

        }

        public static IField CreateField(FieldInfo fInfo)
        {
            IFieldEdit normalField = new FieldClass();
            normalField.Name_2 = fInfo.Name;
            normalField.AliasName_2 = fInfo.AliasName;
            normalField.Type_2 = (esriFieldType)fInfo.Type;

            normalField.IsNullable_2 = fInfo.NullAble;
            normalField.Length_2 = fInfo.Length;
            normalField.Precision_2 = fInfo.Precision;

            return normalField;
        }

        public static IFields CreateFields(TableInfo tInfo)
        {
            if (tInfo == null || tInfo.FieldsInfo == null || tInfo.FieldsInfo.Count == 0)
                return null;

            IFieldsEdit fields=new FieldsClass();
            foreach (FieldInfo fInfo in tInfo.FieldsInfo)
            {
                fields.AddField(CreateField(fInfo));
            }
            return fields;
        }

        public static IFields CreateFields(FeatureClassInfo fcInfo)
        {
            if (fcInfo == null )
                return null;

            IFieldsEdit fields = CreateFields(fcInfo as TableInfo) as IFieldsEdit;
            if (fields == null)
                fields = new FieldsClass();

            fields.AddField(CreateShapeField(fcInfo));

            return fields;
        }

        public static IFeatureClass CreateFeatureClass(IWorkspace fWs,FeatureClassInfo fcInfo)
        {
            if (fWs == null || fcInfo == null)
                return null;

            if((fWs as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass,fcInfo.Name))
            {
                ((fWs as IFeatureWorkspace).OpenFeatureClass(fcInfo.Name) as IDataset).Delete();
            }
            return (fWs as IFeatureWorkspace).CreateFeatureClass(fcInfo.Name, CreateFields(fcInfo), null, null, fcInfo.FeatureType, fcInfo.ShapeFieldName, null);
        }

        public static IFeatureClass CreateFeatureClass(IFeatureDataset fds,FeatureClassInfo fcInfo)
        {
            if (fds == null || fcInfo == null)
                return null;
            
            if ((fds.Workspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, fcInfo.Name))
            {
                ((fds.Workspace as IFeatureWorkspace).OpenFeatureClass(fcInfo.Name) as IDataset).Delete();
            }
            return fds.CreateFeatureClass(fcInfo.Name, CreateFields(fcInfo), null, null, fcInfo.FeatureType, fcInfo.ShapeFieldName, null);
        }

        public static bool SaveStandard(StandardItem sItem)
        {
            try
            {
                Environment.NhibernateHelper.SaveObject(sItem);
                if (sItem.Details != null)
                {
                    if (sItem.Details is FeatureClassInfo)
                    {
                        FeatureClassInfo fcInfo = sItem.Details as FeatureClassInfo;
                        fcInfo.Parent = sItem.ID;
                        Environment.NhibernateHelper.SaveObject(fcInfo);
                        
                        if (fcInfo.FieldsInfo != null)
                        {
                            foreach (FieldInfo fInfo in fcInfo.FieldsInfo)
                            {
                                fInfo.Layer = fcInfo.ID;
                                Environment.NhibernateHelper.SaveObject(fInfo);
                            }
                        }
                    }

                }

               
                foreach (StandardItem subItem in sItem.SubItems)
                {
                    SaveStandard(subItem);
                }
                Environment.NhibernateHelper.Flush();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteStandard(StandardItem sItem)
        {
            try
            {
                if (sItem.Details != null)
                {
                    if (sItem.Details is FeatureClassInfo)
                    {
                        FeatureClassInfo fcInfo = sItem.Details as FeatureClassInfo;
                        if (fcInfo.FieldsInfo != null)
                        {
                            foreach (FieldInfo fInfo in fcInfo.FieldsInfo)
                            {
                                Environment.NhibernateHelper.DeleteObject(fInfo);
                            }
                        }
                        Environment.NhibernateHelper.DeleteObject(fcInfo);
                    }

                }


                foreach (StandardItem subItem in sItem.SubItems)
                {
                    DeleteStandard(subItem);
                }
                Environment.NhibernateHelper.DeleteObject(sItem);
                Environment.NhibernateHelper.Flush();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static StandardItem Import(IFeatureDataset fds)
        {
            if (fds == null)
                return null;

            StandardItem sItem = new StandardItem();
            sItem.Type = enumItemType.FeatureDataset;
            sItem.Name = fds.Name;
            sItem.SpatialReference = (fds as IGeoDataset).SpatialReference;
            sItem.ID = Guid.NewGuid().ToString("N");

            IList<StandardItem> subList = new List<StandardItem>();
            sItem.SubItems = subList;
            IFeatureClassContainer fcContianer = fds as IFeatureClassContainer;
            for (int i = 0; i < fcContianer.ClassCount; i++)
            {
                StandardItem sItemClass = Import(fcContianer.get_Class(i));
                sItemClass.Parent = sItem;

                subList.Add(sItemClass);
            }

            return sItem;
          
        }

        public static StandardItem Import(IFeatureClass fClass)
        {
            if (fClass == null)
                return null;

            StandardItem sItem = new StandardItem();
            sItem.Type = enumItemType.FeatureClass;
            sItem.ID = Guid.NewGuid().ToString("N");

            FeatureClassInfo fcInfo = new FeatureClassInfo();
            fcInfo.ID = Guid.NewGuid().ToString("N");
            fcInfo.Name=(fClass as IDataset).Name;
            fcInfo.AliasName=fClass.AliasName;
            fcInfo.SpatialReference=(fClass as IGeoDataset).SpatialReference;
            fcInfo.FeatureType = esriFeatureType.esriFTSimple;
            fcInfo.ShapeFieldName = fClass.ShapeFieldName;
            fcInfo.ShapeType = fClass.ShapeType;

            IList<FieldInfo> fList = new List<FieldInfo>();
            for(int i=0;i<fClass.Fields.FieldCount;i++)
            {
                IField field = fClass.Fields.get_Field(i);
                if (field.Type == esriFieldType.esriFieldTypeOID)
                    continue;

                if (field.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    IGeometryDef geoDef = field.GeometryDef;
                    fcInfo.AvgNumPoints = geoDef.AvgNumPoints;
                    fcInfo.GridCount = geoDef.GridCount;
                    fcInfo.HasM = geoDef.HasM;
                    fcInfo.HasZ = geoDef.HasZ;
                    continue;
                }
                FieldInfo fInfo = FromEsriField(field);
                fInfo.Layer = fcInfo.ID;
                fList.Add(fInfo);
            }
            fcInfo.FieldsInfo = fList;

            sItem.Name = fcInfo.Name;
            sItem.AliasName = fcInfo.AliasName;
            sItem.SpatialReferenceString = fcInfo.SpatialReferenceString;
            sItem.Details = fcInfo;

            return sItem;
        }

        public static FieldInfo FromEsriField(IField field)
        {
            if (field == null)
                return null;

            FieldInfo fInfo = new FieldInfo();
            fInfo.Name = field.Name;
            fInfo.AliasName = field.AliasName;
            fInfo.Length = field.Length;
            fInfo.NullAble = field.IsNullable;
            fInfo.Precision = field.Precision;
            fInfo.Type = (enumFieldType)field.Type;

            return fInfo;
        }


    }
}
