using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace Hy.Common.Utility.Esri
{
    public class DataConverter
    {
        /// <summary>
        /// 拷贝源图层到空间数据集下的目标图层
        /// </summary>
        /// <param name="sourceWorkspace"></param>
        /// <param name="targetWorkspace"></param>
        /// <param name="nameOfSourceFeatureClass"></param>
        /// <param name="nameOfTargetFeatureClass"></param>
        public static void ConvertFeatureClass(IWorkspace sourceWorkspace, IWorkspace targetWorkspace,
                                               string nameOfSourceFeatureClass, string nameOfTargetFeatureClass)
        {
            try
            {
                //create source workspace name   
                IDataset sourceWorkspaceDataset = (IDataset)sourceWorkspace;
                IWorkspaceName sourceWorkspaceName = (IWorkspaceName)sourceWorkspaceDataset.FullName;

                //create source dataset name       
                IFeatureClassName sourceFeatureClassName = new FeatureClassNameClass();

                IDatasetName sourceDatasetName = (IDatasetName)sourceFeatureClassName;
                sourceDatasetName.WorkspaceName = sourceWorkspaceName;
                sourceDatasetName.Name = nameOfSourceFeatureClass;
                //create target workspace name        
                IDataset targetWorkspaceDataset = (IDataset)targetWorkspace;
                IWorkspaceName targetWorkspaceName = (IWorkspaceName)targetWorkspaceDataset.FullName;
                //create target dataset name       
                IFeatureClassName targetFeatureClassName = new FeatureClassNameClass();
                IDatasetName targetDatasetName = (IDatasetName)targetFeatureClassName;
                targetDatasetName.WorkspaceName = targetWorkspaceName;
                targetDatasetName.Name = nameOfTargetFeatureClass;
                //Open input Featureclass to get field definitions.        
                IName sourceName = (IName)sourceFeatureClassName;
                IFeatureClass sourceFeatureClass = (IFeatureClass)sourceName.Open();
                //Validate the field names because you are converting between different workspace types.       
                IFieldChecker fieldChecker = new FieldCheckerClass();
                IFields targetFeatureClassFields;
                IFields sourceFeatureClassFields = sourceFeatureClass.Fields;
                IEnumFieldError enumFieldError;
                // Most importantly set the input and validate workspaces! 
                fieldChecker.InputWorkspace = sourceWorkspace;
                fieldChecker.ValidateWorkspace = targetWorkspace;
                fieldChecker.Validate(sourceFeatureClassFields, out enumFieldError, out targetFeatureClassFields);
                // Loop through the output fields to find the geomerty field        
                IField geometryField;
                for (int i = 0; i < targetFeatureClassFields.FieldCount; i++)
                {
                    geometryField = targetFeatureClassFields.get_Field(i);
                    if (geometryField.Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        // Get the geometry field's geometry defenition             
                        IGeometryDef geometryDef = geometryField.GeometryDef;

                        //Give the geometry definition a spatial index grid count and grid size             
                        IGeometryDefEdit targetFCGeoDefEdit = (IGeometryDefEdit)geometryDef;

                        //targetFCGeoDefEdit.GridCount_2 = 1;
                        //targetFCGeoDefEdit.set_GridSize(0, 1000);
                        //Allow ArcGIS to determine a valid grid size for the data loaded        
                        targetFCGeoDefEdit.SpatialReference_2 = geometryField.GeometryDef.SpatialReference;
                        // we want to convert all of the features                
                        IQueryFilter queryFilter = new QueryFilterClass();
                        queryFilter.WhereClause = "";

                        IEnumDataset pEnumDataset = targetWorkspace.get_Datasets(esriDatasetType.esriDTFeatureDataset);
                        IFeatureDataset ipDataset = (IFeatureDataset)pEnumDataset.Next();
                        IDatasetName pTargetDsName = (IDatasetName)ipDataset.FullName;
                        // Load the feature class                
                        IFeatureDataConverter fctofc = new FeatureDataConverterClass();

                        IEnumInvalidObject enumErrors =
                            fctofc.ConvertFeatureClass(sourceFeatureClassName, queryFilter,
                                                       pTargetDsName as IFeatureDatasetName, targetFeatureClassName,
                                                       geometryDef, targetFeatureClassFields, "", 1000, 0);
                        IInvalidObjectInfo obj = enumErrors.Next();
                        break;
                    }
                }
            }
            catch (Exception exp)
            {
                Hy.Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return;
            }
        }

        /// <summary>
        /// 复制源图层到目标dataset中的目标图层
        /// </summary>
        /// <param name="sourceWorkspaceDataset">源图层集</param>
        /// <param name="targetWorkspaceDataset">目标图层集</param>
        /// <param name="sourceFeatureClass">源图层</param>
        /// <param name="nameOfTargetFeatureClass">待创建的图层名</param>
        public static IFeatureClass ConvertFeatureClass(IDataset sourceWorkspaceDataset, IDataset targetWorkspaceDataset,
                                                        IFeatureClass sourceFeatureClass,
                                                        string nameOfTargetFeatureClass)
        {
            IFeatureClass TragetFeatureClass = null;
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


                //Validate the field names because you are converting between different workspace types.       
                IFieldChecker fieldChecker = new FieldCheckerClass();
                IFields targetFeatureClassFields;
                IFields sourceFeatureClassFields = sourceFeatureClass.Fields;
                IEnumFieldError enumFieldError;
                // Most importantly set the input and validate workspaces!

                fieldChecker.InputWorkspace = sourceWorkspaceDataset.Workspace;
                fieldChecker.ValidateWorkspace = targetWorkspaceDataset.Workspace;
                fieldChecker.Validate(sourceFeatureClassFields, out enumFieldError, out targetFeatureClassFields);
                // Loop through the output fields to find the geomerty field        
                IField geometryField;
                for (int i = 0; i < targetFeatureClassFields.FieldCount; i++)
                {
                    if (targetFeatureClassFields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        geometryField = targetFeatureClassFields.get_Field(i);
                        // Get the geometry field's geometry defenition             
                        IGeometryDef geometryDef = geometryField.GeometryDef;
                        //Give the geometry definition a spatial index grid count and grid size             
                        IGeometryDefEdit targetFCGeoDefEdit = (IGeometryDefEdit)geometryDef;
                        //targetFCGeoDefEdit.GridCount_2 = 1;
                        //targetFCGeoDefEdit.set_GridSize(0, 0);
                        //Allow ArcGIS to determine a valid grid size for the data loaded        
                        targetFCGeoDefEdit.SpatialReference_2 = geometryField.GeometryDef.SpatialReference;
                        // we want to convert all of the features                
                        IQueryFilter queryFilter = new QueryFilterClass();
                        queryFilter.WhereClause = "";
                        // Load the feature class                
                        IFeatureDataConverter fctofc = new FeatureDataConverterClass();

                        IEnumInvalidObject enumErrors =
                            fctofc.ConvertFeatureClass(nameOfSourceFeatureClass, queryFilter,
                                                       pTargetDsName as IFeatureDatasetName, targetFeatureClassName,
                                                       geometryDef, targetFeatureClassFields, "", 1000, 0);
                        break;
                    }
                }

                string strTargetFtCls = ((IDatasetName)targetFeatureClassName).Name;
                TragetFeatureClass =
                    ((IFeatureWorkspace)targetWorkspaceDataset.Workspace).OpenFeatureClass(strTargetFtCls);
            }
            catch (Exception exp)
            {
                Hy.Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return null;
            }
            return TragetFeatureClass;
        }  
        
        /// <summary>
        /// 拷贝源表到目标空间
        /// </summary>
        /// <param name="sourceWorkspace"></param>
        /// <param name="targetWorkspace"></param>
        /// <param name="SourceDataset"></param>
        /// <param name="nameOfTargetDataset"></param>
        public static void ConvertTable(IWorkspace sourceWorkspace, IWorkspace targetWorkspace, IDataset SourceDataset,
                                        string nameOfTargetDataset)
        {
            try
            {
                //create source workspace name   
                ITable pSourceTab = (ITable)SourceDataset;

                IDataset sourceWorkspaceDataset = (IDataset)sourceWorkspace;
                IWorkspaceName sourceWorkspaceName = (IWorkspaceName)sourceWorkspaceDataset.FullName;

                IDatasetName sourceDatasetName = (IDatasetName)SourceDataset.FullName;

                //create target workspace name        
                IDataset targetWorkspaceDataset = (IDataset)targetWorkspace;
                IWorkspaceName targetWorkspaceName = (IWorkspaceName)targetWorkspaceDataset.FullName;
                //create target dataset name       
                ITableName targetTableName = new FeatureClassNameClass();
                IDatasetName targetDatasetName = (IDatasetName)targetTableName;
                targetDatasetName.WorkspaceName = targetWorkspaceName;
                targetDatasetName.Name = nameOfTargetDataset;
                ////Open input Featureclass to get field definitions.        
                //ESRI.ArcGIS.esriSystem.IName sourceName = (ESRI.ArcGIS.esriSystem.IName)sourceDatasetName;
                //ITable sourceTable = (ITable)sourceName.Open();

                // we want to convert all of the features                
                IQueryFilter queryFilter = new QueryFilterClass();
                queryFilter.WhereClause = "";
                //Validate the field names because you are converting between different workspace types.       
                IFieldChecker fieldChecker = new FieldCheckerClass();
                IFields targetFields;
                IFields sourceFields = pSourceTab.Fields;
                IEnumFieldError enumFieldError;
                // Most importantly set the input and validate workspaces! 
                fieldChecker.InputWorkspace = sourceWorkspace;
                fieldChecker.ValidateWorkspace = targetWorkspace;
                fieldChecker.Validate(sourceFields, out enumFieldError, out targetFields);
                if (enumFieldError == null)
                {
                    IFeatureDataConverter fctofc = new FeatureDataConverterClass();

                    IEnumInvalidObject enumErrors =
                        fctofc.ConvertTable(sourceDatasetName, queryFilter, targetDatasetName, pSourceTab.Fields, "",
                                            1000, 0);
                }
            }
            catch (Exception exp)
            {
                Hy.Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return;
            }
        }
    }
}
