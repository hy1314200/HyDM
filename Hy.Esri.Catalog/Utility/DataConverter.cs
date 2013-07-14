using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace Hy.Esri.Catalog.Utility
{
    public class DataConverter
    {
        /// <summary>
        /// 拷贝源图层到空间数据集下的目标图层
        /// </summary>
        /// <param name="sourceWorkspace">源Workspace</param>
        /// <param name="targetWorkspace">目标Workspace</param>
        /// <param name="sourceClassName">源图层</param>
        /// <param name="destClassName">待创建的图层名</param>
        public static bool ConvertFeatureClass(IWorkspace sourceWorkspace, IWorkspace targetWorkspace,
                                               string sourceClassName, string destClassName)
        {
            try
            {
                IDataset sourceWorkspaceDataset = (IDataset)sourceWorkspace;
                IWorkspaceName sourceWorkspaceName = (IWorkspaceName)sourceWorkspaceDataset.FullName;

                IFeatureClassName sourceFeatureClassName = new FeatureClassNameClass();

                IDatasetName sourceDatasetName = (IDatasetName)sourceFeatureClassName;
                sourceDatasetName.WorkspaceName = sourceWorkspaceName;
                sourceDatasetName.Name = sourceClassName;
   
                IDataset targetWorkspaceDataset = (IDataset)targetWorkspace;
                IWorkspaceName targetWorkspaceName = (IWorkspaceName)targetWorkspaceDataset.FullName;
    
                IFeatureClassName targetFeatureClassName = new FeatureClassNameClass();
                IDatasetName targetDatasetName = (IDatasetName)targetFeatureClassName;
                targetDatasetName.WorkspaceName = targetWorkspaceName;
                targetDatasetName.Name = destClassName;
     
                IName sourceName = (IName)sourceFeatureClassName;
                IFeatureClass sourceFeatureClass = (IFeatureClass)sourceName.Open();
    
                IFields targetFeatureClassFields;
                IFields sourceFeatureClassFields = sourceFeatureClass.Fields;
                IEnumFieldError enumFieldError;

                IFieldChecker fieldChecker = new FieldCheckerClass();
                fieldChecker.InputWorkspace = sourceWorkspace;
                fieldChecker.ValidateWorkspace = targetWorkspace;
                fieldChecker.Validate(sourceFeatureClassFields, out enumFieldError, out targetFeatureClassFields);

                IField geometryField = targetFeatureClassFields.get_Field(targetFeatureClassFields.FindField(sourceFeatureClass.ShapeFieldName));
                
                //for (int i = 0; i < targetFeatureClassFields.FieldCount; i++)
                //{
                //    geometryField = targetFeatureClassFields.get_Field(i);
                //    if (geometryField.Type == esriFieldType.esriFieldTypeGeometry)
                //    {        
                        IGeometryDef geometryDef = geometryField.GeometryDef;
      
                        IGeometryDefEdit targetFCGeoDefEdit = (IGeometryDefEdit)geometryDef;
  
                        targetFCGeoDefEdit.SpatialReference_2 = geometryField.GeometryDef.SpatialReference;
                        IQueryFilter queryFilter = new QueryFilterClass();
                        queryFilter.WhereClause = "";

                        IEnumDataset pEnumDataset = targetWorkspace.get_Datasets(esriDatasetType.esriDTFeatureDataset);

                        IFeatureDataConverter fConverter = new FeatureDataConverterClass();

                        IEnumInvalidObject enumErrors =
                            fConverter.ConvertFeatureClass(sourceFeatureClassName, queryFilter,
                                                       null, targetFeatureClassName,
                                                       geometryDef, targetFeatureClassFields, "", 1000, 0);
                        IInvalidObjectInfo obj = enumErrors.Next();
                //        break;
                //    }
                //}

                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        /// <summary>
        /// 复制源图层到目标dataset中的目标图层
        /// </summary>
        /// <param name="sourceWorkspaceDataset">源图层集</param>
        /// <param name="targetWorkspaceDataset">目标图层集</param>
        /// <param name="sourceFeatureClass">源图层</param>
        /// <param name="nameOfTargetFeatureClass">待创建的图层名</param>
        public static bool ConvertFeatureClass(IDataset sourceWorkspaceDataset, IDataset targetWorkspaceDataset,
                                                        IFeatureClass sourceFeatureClass,
                                                        string nameOfTargetFeatureClass)
        {
            try
            {
                IFeatureClassName nameOfSourceFeatureClass =
                    ((IDataset)sourceFeatureClass).FullName as IFeatureClassName;

                IDatasetName pSourceDsName = (IDatasetName)sourceWorkspaceDataset.FullName;
                IDatasetName pTargetDsName = (IDatasetName)targetWorkspaceDataset.FullName;

                IFeatureClassName targetFeatureClassName = new FeatureClassNameClass();
                targetFeatureClassName.FeatureDatasetName = pTargetDsName;
                IDatasetName targetDatasetName = (IDatasetName)targetFeatureClassName;
                targetDatasetName.Name = nameOfTargetFeatureClass;

    
                IFields targetFeatureClassFields;
                IFields sourceFeatureClassFields = sourceFeatureClass.Fields;
                IEnumFieldError enumFieldError;

                IFieldChecker fieldChecker = new FieldCheckerClass();
                fieldChecker.InputWorkspace = sourceWorkspaceDataset.Workspace;
                fieldChecker.ValidateWorkspace = targetWorkspaceDataset.Workspace;
                fieldChecker.Validate(sourceFeatureClassFields, out enumFieldError, out targetFeatureClassFields);

                IField geometryField = targetFeatureClassFields.get_Field(targetFeatureClassFields.FindField(sourceFeatureClass.ShapeFieldName));
                //for (int i = 0; i < targetFeatureClassFields.FieldCount; i++)
                //{
                //    if (targetFeatureClassFields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                //    {
                        //geometryField = targetFeatureClassFields.get_Field(i);   
                        IGeometryDef geometryDef = geometryField.GeometryDef;        
                        IGeometryDefEdit targetFCGeoDefEdit = (IGeometryDefEdit)geometryDef;  
                        targetFCGeoDefEdit.SpatialReference_2 = geometryField.GeometryDef.SpatialReference;
                        IQueryFilter queryFilter = new QueryFilterClass();
                        queryFilter.WhereClause = "";
                        IFeatureDataConverter fConverter = new FeatureDataConverterClass();

                        IEnumInvalidObject enumErrors =
                            fConverter.ConvertFeatureClass(nameOfSourceFeatureClass, queryFilter,
                                                       pTargetDsName as IFeatureDatasetName, targetFeatureClassName,
                                                       geometryDef, targetFeatureClassFields, "", 1000, 0);
                //        break;
                //    }
                //}
            return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }  
        
        /// <summary>
        /// 拷贝源表到目标空间
        /// </summary>
        /// <param name="sourceWorkspace"></param>
        /// <param name="targetWorkspace"></param>
        /// <param name="SourceDataset"></param>
        /// <param name="nameOfTargetDataset"></param>
        public static bool ConvertTable(IWorkspace sourceWorkspace, IWorkspace targetWorkspace, IDataset SourceDataset,
                                        string nameOfTargetDataset)
        {
            try
            {
                
                ITable pSourceTab = (ITable)SourceDataset;

                IDataset sourceWorkspaceDataset = (IDataset)sourceWorkspace;
                IWorkspaceName sourceWorkspaceName = (IWorkspaceName)sourceWorkspaceDataset.FullName;

                IDatasetName sourceDatasetName = (IDatasetName)SourceDataset.FullName;

                IDataset targetWorkspaceDataset = (IDataset)targetWorkspace;
                IWorkspaceName targetWorkspaceName = (IWorkspaceName)targetWorkspaceDataset.FullName;

                ITableName targetTableName = new FeatureClassNameClass();
                IDatasetName targetDatasetName = (IDatasetName)targetTableName;
                targetDatasetName.WorkspaceName = targetWorkspaceName;
                targetDatasetName.Name = nameOfTargetDataset;
         
                IQueryFilter queryFilter = new QueryFilterClass();
                queryFilter.WhereClause = "";
  
                IFieldChecker fieldChecker = new FieldCheckerClass();
                IFields targetFields;
                IFields sourceFields = pSourceTab.Fields;
                IEnumFieldError enumFieldError;

                fieldChecker.InputWorkspace = sourceWorkspace;
                fieldChecker.ValidateWorkspace = targetWorkspace;
                fieldChecker.Validate(sourceFields, out enumFieldError, out targetFields);
                if (enumFieldError == null)
                {
                    IFeatureDataConverter fConverter = new FeatureDataConverterClass();
                    IEnumInvalidObject enumErrors =
                        fConverter.ConvertTable(sourceDatasetName, queryFilter, targetDatasetName, pSourceTab.Fields, "",
                                            1000, 0);
                }

                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }
    }
}
