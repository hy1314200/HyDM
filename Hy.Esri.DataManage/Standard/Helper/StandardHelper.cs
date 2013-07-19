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

        public static bool SaveStanard(StandardItem sItem)
        {
            try
            {
                Environment.NhibernateHelper.SaveObject(sItem);
                if (sItem.Details != null)
                    Environment.NhibernateHelper.SaveObject(sItem.Details);

                foreach (StandardItem subItem in sItem.SubItems)
                {
                    SaveStanard(subItem);
                }
                Environment.NhibernateHelper.Flush();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
