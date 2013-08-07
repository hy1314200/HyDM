using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.ConversionTools;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Geometry;
using Hy.Esri.Catalog.Define;
using Hy.Esri.Utility;

namespace Hy.Esri.Catalog.Utility
{
    /// <summary>
    /// Gp工具包装类
    /// </summary>
    public class GpTool
    {
        public static string ErrorMessage { get; private set; }

        /// <summary>
        /// 导入3d数据到目标FeatureClass
        /// </summary>
        /// <param name="str3DFile"></param>
        /// <param name="outPut"></param>
        /// <returns></returns>
        public static bool Import3DFile(string str3DFile, IWorkspace wsTarget,string featrueDatasetName,string featureClassName, string strSpatialRef,ref IFeature feature3D)
        {
            try
            {
                string outPut = WorkspaceHelper.GetGpString(wsTarget, featrueDatasetName, featureClassName);
                if (!Import3DFile(str3DFile, outPut, strSpatialRef))
                    return false;

                // 返回Feature
                IFeatureClass fClass = (wsTarget as IFeatureWorkspace).OpenFeatureClass(featureClassName);
                IFeatureCursor fCursor = fClass.Search(null, false);
                feature3D = fCursor.NextFeature();

                return true;
            }
            catch(Exception exp)
            {
                ErrorMessage = exp.Message;
                return false;
            }
        }

        public static bool Import3DFile(string str3DFile,  string outPut, string strSpatialRef)
        {
            try
            {
                ESRI.ArcGIS.Analyst3DTools.Import3DFiles gpImport3DFiles = new ESRI.ArcGIS.Analyst3DTools.Import3DFiles();
                gpImport3DFiles.in_files = str3DFile;
                gpImport3DFiles.out_featureClass = outPut;
                gpImport3DFiles.spatial_reference = strSpatialRef;

                Geoprocessor geoProcessor = new Geoprocessor();
                geoProcessor.OverwriteOutput = true;
                IGeoProcessorResult gpResult = geoProcessor.Execute(gpImport3DFiles, null) as IGeoProcessorResult;
                //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(geoProcessor);

                return gpResult.Status == esriJobStatus.esriJobSucceeded;

            }
            catch (Exception exp)
            {
                ErrorMessage = exp.Message;
                return false;
            }
        }

        public static bool Append3DFile(string str3DFile, IFeatureClass fClassTarget , string strSpatialRef, ref IFeature feature3D)
        {
            try
            {

                string strTempPath=System.IO.Path.GetPathRoot(System.Environment.SystemDirectory);
                string strOutputTemp = System.IO.Path.Combine(strTempPath, "temp.shp");

                if (System.IO.File.Exists(strOutputTemp) && !DeleteDataset(strOutputTemp))
                {
                    ErrorMessage = "无法使用临时文件";
                    return false;
                }

                if (!Import3DFile(str3DFile, strOutputTemp, strSpatialRef))
                {
                    return false;
                }

                IWorkspace wsTemp =Hy.Esri.Utility.WorkspaceHelper.OpenWorkspace(enumWorkspaceType.File, strTempPath);
                IFeatureClass fClassTemp = (wsTemp as IFeatureWorkspace).OpenFeatureClass("temp");
                IFeatureCursor fCursorTemp = fClassTemp.Search(null, false);
                IFeature fTemp = fCursorTemp.NextFeature();
                if (fTemp == null)
                {
                    ErrorMessage = "追加的中转过程出错!";
                    return false;
                }
                IGeometry geo3D = (fTemp.Shape as IClone).Clone() as IGeometry;

                //IFeatureClass fClass = (wsTarget as IFeatureWorkspace).OpenFeatureClass(featureClassName);
                //feature3D = fClass.CreateFeature();
                feature3D = fClassTarget.CreateFeature();
                feature3D.Shape = geo3D;
                feature3D.Store();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(fTemp);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(fCursorTemp);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(fClassTemp);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wsTemp);

                return true;
                
                //string outPut = WorkspaceHelper.GetGpString(wsTarget, featrueDatasetName, featureClassName);
                //IFeature newFeature = null;
                //Append gpAppend = new Append();
                //gpAppend.inputs = strOutputTemp;
                //gpAppend.output = newFeature;
                //gpAppend.target = outPut;

                //Geoprocessor geoProcessor = new Geoprocessor();
                //geoProcessor.OverwriteOutput = true;
                //IGeoProcessorResult gpResult = geoProcessor.Execute(gpAppend, null) as IGeoProcessorResult;

                //return gpResult.Status == esriJobStatus.esriJobSucceeded;
            }
            catch(Exception exp)
            {
                ErrorMessage = exp.Message;
                return false;
            }

        }

        /// <summary>
        /// 要素类复制
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="outPath"></param>
        /// <param name="outName"></param>
        /// <returns></returns>
        public static bool CopyFeatureClass(string strInput, string outPath,string outName)
        {
            try
            {
                FeatureClassToFeatureClass gpCopyFeatureClass = new FeatureClassToFeatureClass();
                gpCopyFeatureClass.in_features = strInput;
                gpCopyFeatureClass.out_path = outPath;
                gpCopyFeatureClass.out_name = outName;

                Geoprocessor geoProcessor = new Geoprocessor();
                geoProcessor.OverwriteOutput = true;
                IGeoProcessorResult gpResult = geoProcessor.Execute(gpCopyFeatureClass, null) as IGeoProcessorResult;
                //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(geoProcessor);

                return gpResult.Status == esriJobStatus.esriJobSucceeded;

            }
            catch(Exception exp)
            {
                ErrorMessage = exp.Message;
                return false;
            }
        }  
        
        /// <summary>
        /// 要素类复制 
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="outPut"></param>
        /// <returns></returns>
        public static bool CopyFeatureClass(string strInput, string outPut)
        {
            try
            {
                string outPath = System.IO.Path.GetDirectoryName(outPut);
                string outName = System.IO.Path.GetFileName(outPut);

                return CopyFeatureClass(strInput, outPath, outName);
            }
            catch(Exception exp)
            {
                ErrorMessage = exp.Message;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="outPut"></param>
        /// <param name="isToFile"></param>
        /// <param name="strFormat">当isToFile为Ture时，可设置此值，默认值为TIFF，可选值：BMP、JPEG、PNG、GIF、TIFF、IMAGINE、GRID</param>
        /// <returns></returns>
        public static bool CopyRaster(string strInput, string outPut,bool isToFile,string strFormat)
        {
            try{
                IGPProcess gpTool = null;
                if (isToFile)
                {
                    RasterToOtherFormat gpToOther = new RasterToOtherFormat();
                    gpToOther.Input_Rasters = strInput;
                    gpToOther.Output_Workspace = outPut;
                    gpToOther.Raster_Format = strFormat;
                    
                    gpTool = gpToOther;
                }
                else
                {
                    RasterToGeodatabase gpToGDB = new RasterToGeodatabase();
                    gpToGDB.Input_Rasters = strInput;
                    gpToGDB.Output_Geodatabase = outPut;

                    gpTool = gpToGDB;
                }


                Geoprocessor geoProcessor = new Geoprocessor();
               
                geoProcessor.OverwriteOutput = true;
                IGeoProcessorResult gpResult = geoProcessor.Execute(gpTool, null) as IGeoProcessorResult;
                //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(geoProcessor);

                return gpResult.Status == esriJobStatus.esriJobSucceeded;

            }
            catch(Exception exp)
            {
                ErrorMessage = exp.Message;
                return false;
            }
        }

        /// <summary>
        /// 删除IDataset 备用方法，没有经过测试
        /// </summary>
        /// <param name="dsGpString"></param>
        /// <returns></returns>
        public static bool DeleteDataset(string dsGpString)
        {
            if (string.IsNullOrWhiteSpace(dsGpString))
                return false;

            try
            {
                ESRI.ArcGIS.DataManagementTools.Delete gpDetele = new ESRI.ArcGIS.DataManagementTools.Delete(dsGpString);
                Geoprocessor geoProcessor = new Geoprocessor();
                IGeoProcessorResult gpResult = geoProcessor.Execute(gpDetele, null) as IGeoProcessorResult;
                //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(geoProcessor);

                return gpResult.Status == esriJobStatus.esriJobSucceeded;
            }
            catch(Exception exp)
            {
                ErrorMessage = exp.Message;
                return false;
            }
        }

        /// <summary>
        /// 删除IDataset 
        /// </summary>
        /// <param name="dsTarget"></param>
        /// <returns></returns>
        public static bool DeleteDataset(IDataset dsTarget)
        {
            if (dsTarget == null)
                return false;

            try
            {
                ESRI.ArcGIS.DataManagementTools.Delete gpDetele = new ESRI.ArcGIS.DataManagementTools.Delete(dsTarget);
                Geoprocessor geoProcessor = new Geoprocessor();
                IGeoProcessorResult gpResult = geoProcessor.Execute(gpDetele, null) as IGeoProcessorResult;
                //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(geoProcessor);

                return gpResult.Status == esriJobStatus.esriJobSucceeded;
            }
            catch(Exception exp)
            {
                ErrorMessage = exp.Message;
                return false;
            }
        }

        /// <summary>
        /// 添加属性字段
        /// </summary>
        /// <param name="dsTarget"></param>
        /// <param name="newFields"></param>
        /// <returns></returns>
        public static bool AddFields(ITable dsTarget, List<IField> newFields)
        {
            if (dsTarget == null || newFields == null || newFields.Count == 0)
                return false;

            try
            {
                Geoprocessor geoProcessor = new Geoprocessor();
                foreach (IField field in newFields)
                {
                    ESRI.ArcGIS.DataManagementTools.AddField gpAddField = new ESRI.ArcGIS.DataManagementTools.AddField();
                    gpAddField.in_table = dsTarget;
                    gpAddField.field_name = field.Name;
                    gpAddField.field_type = GetFieldTypeGpString(field.Type);

                    gpAddField.field_alias = field.AliasName;
                    if (field.Domain != null)
                        gpAddField.field_domain = field.Domain.Name;

                    gpAddField.field_is_nullable = field.IsNullable.ToString();
                    gpAddField.field_is_required = field.Required.ToString();
                    gpAddField.field_length = field.Length;
                    gpAddField.field_precision = field.Precision;
                    gpAddField.field_scale = field.Scale;

                    IGeoProcessorResult gpResult = geoProcessor.Execute(gpAddField, null) as IGeoProcessorResult;
                    if (gpResult.Status != esriJobStatus.esriJobSucceeded)
                        return false;
                }
               //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(geoProcessor);


                return true;
            }
            catch(Exception exp)
            {
                ErrorMessage = exp.Message;
                return false;
            }
        }

        /// <summary>
        /// 鉴于GP错误提示，可取以下值
        /// TEXT | FLOAT | DOUBLE | SHORT | LONG | DATE | BLOB | RASTER | GUID 
        /// 那：
        /// Geometry将取BLOB
        /// OID将取LONG
        /// XML将取TEXT
        /// </summary>
        /// <param name="fType"></param>
        /// <returns></returns>
        public static string GetFieldTypeGpString(esriFieldType fType)
        {
            switch (fType)
            {
                case esriFieldType.esriFieldTypeXML:
                case esriFieldType.esriFieldTypeString:
                    return "TEXT";

                case esriFieldType.esriFieldTypeSingle:
                    return "FLOAT";

                case esriFieldType.esriFieldTypeDouble:
                    return "DOUBLE";

                case esriFieldType.esriFieldTypeBlob:
                    return "BLOB";

                case esriFieldType.esriFieldTypeDate:
                    return "DATE";

                case esriFieldType.esriFieldTypeGeometry:
                    return "BLOB";

                case esriFieldType.esriFieldTypeGUID:
                    return "GUID";

                case esriFieldType.esriFieldTypeRaster:
                    return "Raster";

                case esriFieldType.esriFieldTypeOID:
                case esriFieldType.esriFieldTypeInteger:
                    return "LONG";

                case esriFieldType.esriFieldTypeSmallInteger:
                    return "SHORT";

                default:
                    return "TEXT";
            }
        }


    }
}
