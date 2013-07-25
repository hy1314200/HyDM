using System;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.esriSystem;
using System.Runtime.InteropServices;

namespace Hy.Common.Utility.Esri
{
    public class GPTool
    {

        /// <summary>
        /// Batches the copy feature class.
        /// </summary>
        /// <param name="workspacePath">The workspace path.</param>
        /// <param name="sourceDatasetName">Name of the source dataset.</param>
        /// <param name="Layerfilter">The layerfilter.</param>
        /// <param name="targetPath">The target path.</param>
        /// <param name="targetDatasetName">Name of the target dataset.</param>
        /// <returns></returns>
        public bool BatchCopyFeatureClass(string workspacePath, string sourceDatasetName,List<string> Layerfilter, string targetPath
                           ,string targetDatasetName)
        {
            try
            {
                Geoprocessor pGP = new Geoprocessor();
                List<string> featclsList = new List<string>();
                pGP.OverwriteOutput = true;
                pGP.SetEnvironmentValue("workspace", (object)workspacePath);

                IGpEnumList pFeatDatasetList = pGP.ListDatasets("", "");

                IGpEnumList pFeatClsList = null;

                string strDatasetName = pFeatDatasetList.Next();

                if (strDatasetName != "")
                {
                    while (strDatasetName !="")
                    {
                        pFeatClsList = pGP.ListFeatureClasses("", "", strDatasetName);

                        string strName = pFeatClsList.Next();
                        while (strName != "")
                        {
                            if (Layerfilter.Contains(strName.ToUpper()) && !featclsList.Contains(strName))
                            {
                                featclsList.Add(strName.ToUpper());
                            }
                            strName = pFeatClsList.Next();
                        }
                        strDatasetName = pFeatDatasetList.Next();
                    }
                }
                else
                {
                    pFeatClsList = pGP.ListFeatureClasses("", "", "");
                    string strName = pFeatClsList.Next();
                    while (strName != "")
                    {
                        if (Layerfilter.Contains(strName.ToUpper()) && !featclsList.Contains(strName))
                        {
                            featclsList.Add(strName.ToUpper());
                        }
                        strName = pFeatClsList.Next();
                    }
                }

                if (featclsList.Count == 0) return false;

                CopyFeatures pCopyFeature = new CopyFeatures();

                foreach (string str in featclsList)
                {
                    if (string.IsNullOrEmpty(strDatasetName))
                    {
                        pCopyFeature.in_features = string.Format("{0}\\{1}", workspacePath, str);
                    }
                    else
                    {
                        pCopyFeature.in_features = string.Format("{0}\\{1}\\{2}", workspacePath, strDatasetName, str);
                    }
                    pCopyFeature.out_feature_class = string.Format("{0}\\{1}\\{2}_Standard", targetPath, targetDatasetName, str); 
                    pGP.Execute(pCopyFeature, null);
                    object obj = null;
                    //GT_CONST.LogAPI.CheckLog.AppendErrLogs(pGP.GetMessages(ref obj));
                }
            }
            catch (Exception exp)
            {
                Hy.Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
            return true;
        }

        /// <summary>
        /// Copies the feature class.
        /// </summary>
        /// <param name="inputPaths">The input paths.</param>
        /// <param name="outPutPath">The out put path.</param>
        /// <returns></returns>
        public  IGeoProcessorResult CopyFeatureClass(string inputPaths, string outPutPath)
        {
            IGeoProcessorResult pGeoProcessroResult = null;
            try
            {

                CopyFeatures pCopyFeature = new CopyFeatures(inputPaths, outPutPath);
                Geoprocessor GP = new Geoprocessor();
                GP.OverwriteOutput = true;
                GP.TemporaryMapLayers = false;
                pGeoProcessroResult= GP.Execute(pCopyFeature, null) as IGeoProcessorResult;

                //GT_CONST.LogAPI.CheckLog.AppendErrLogs(pGeoProcessroResult.Status.ToString());
                //GT_CONST.LogAPI.CheckLog.AppendErrLogs(pGeoProcessroResult.GetMessages(0));
                return pGeoProcessroResult;
            }
            catch (Exception exp)
            {
                Hy.Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return null;
            }
        }
        
        /// <summary>
        /// Copies the feature class.
        /// </summary>
        /// <param name="inputPaths">The input paths.</param>
        /// <param name="outPutPath">The out put path.</param>
        /// <param name="outputHasZValues"></param>
        /// <returns></returns>
        public  IGeoProcessorResult CopyFeatureClass(string inputPaths, string outPutPath,bool outputHasZValues)
        {
            try
            {

                CopyFeatures pCopyFeature = new CopyFeatures(inputPaths, outPutPath);
                Geoprocessor GP = new Geoprocessor();
                GP.OverwriteOutput = true;
                GP.TemporaryMapLayers = false;
                if (outputHasZValues)
                {
                    object obj = GP.GetEnvironmentValue("OutputZFlag"); //设置Output has Z Values
                    GP.SetEnvironmentValue("OutputZFlag", "DEFAULT");
                }
                IGeoProcessorResult result = GP.Execute(pCopyFeature, null) as IGeoProcessorResult;
                //GT_CONST.LogAPI.CheckLog.AppendErrLogs(result.Status.ToString());
                //GT_CONST.LogAPI.CheckLog.AppendErrLogs(result.GetMessages(0));
                return result;
            }
            catch(Exception exp)
            {
                Hy.Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return null;
            }
        }

         /// <summary>
        /// Copies the feature class.
        /// </summary>
        /// <param name="inputPaths">The input paths.</param>
        /// <param name="outPutPath">The out put path.</param>
        /// <param name="outputHasMValues"></param>
        /// <param name="outputHasZValues"></param>
        /// <returns></returns>
        public IGeoProcessorResult CopyFeatureClass(string inputPaths, string outPutPath, bool outputHasZValues, bool outputHasMValues)
        {
            try
            {
                CopyFeatures pCopyFeature = new CopyFeatures(inputPaths, outPutPath);
                Geoprocessor GP = new Geoprocessor();
                GP.OverwriteOutput = true;
                GP.TemporaryMapLayers = false;


                if (outputHasZValues)
                {
                    object obj = GP.GetEnvironmentValue("OutputZFlag"); //设置Output has Z Values
                    GP.SetEnvironmentValue("OutputZFlag", "DEFAULT");
                }

                if (outputHasMValues)
                {
                    object obj = GP.GetEnvironmentValue("OutputMFlag");                    //设置Output has M Values
                    GP.SetEnvironmentValue("OutputMFlag", "DEFAULT");
                }

                IGeoProcessorResult result = GP.Execute(pCopyFeature, null) as IGeoProcessorResult;
                //GT_CONST.LogAPI.CheckLog.AppendErrLogs(result.Status.ToString());
                //GT_CONST.LogAPI.CheckLog.AppendErrLogs(result.GetMessages(0));
                return result;
            }
            catch (Exception exp)
            {
                Hy.Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return null;
            }
        }
        /// <summary>
        /// Copies 
        /// </summary>
        /// <param name="inputPaths">The input paths.</param>
        /// <param name="outPutPath">The out put path.</param>
        /// <returns></returns>
        public IGeoProcessorResult Copy(string inputPaths, string outPutPath)
        {
            try
            {
                Copy pCopy = new Copy(inputPaths, outPutPath);
                Geoprocessor GP = new Geoprocessor();
                GP.OverwriteOutput = true;
                GP.TemporaryMapLayers = false;
                IGeoProcessorResult result = GP.Execute(pCopy, null) as IGeoProcessorResult;
                //GT_CONST.LogAPI.CheckLog.AppendErrLogs(result.Status.ToString());
                //GT_CONST.LogAPI.CheckLog.AppendErrLogs(result.GetMessages(0));
                return result;
            }
            catch (Exception exp)
            {
                Hy.Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return null;
            }
        }

        /// <summary>
        /// Deletes the features.
        /// </summary>
        /// <param name="inputPaths">必须设置featureclass的选择集</param>
        /// <returns></returns>
        public IGeoProcessorResult DeleteFeatures(string inputPaths)
        {
            try
            {
                DeleteFeatures pDeleteFeatures = new DeleteFeatures(inputPaths);
                Geoprocessor GP = new Geoprocessor();
                //GP.OverwriteOutput = true;
                GP.TemporaryMapLayers = false;
                IGeoProcessorResult result = GP.Execute(pDeleteFeatures, null) as IGeoProcessorResult;
                //GT_CONST.LogAPI.CheckLog.AppendErrLogs(result.Status.ToString());
                //GT_CONST.LogAPI.CheckLog.AppendErrLogs(result.GetMessages(0));
                return result;
            }
            catch (Exception exp)
            {
                Hy.Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return null;
            }
        }

        //执行GP的数据修复功能
        //田晶添加20080916
        //修改，提高效率
        //靳军杰2011-11-30
        /// <summary>
        /// 修复面图层自相交错误
        /// </summary>
        /// <param name="sPath"></param>
        /// <returns></returns>
        public bool FeatureRepair(string sPath)
        {
            if (string.IsNullOrEmpty(sPath))
            {
                return false;
            }

            IGeoProcessor pGP = new GeoProcessorClass();
            try
            {
                //GT_CARTO.XApplication.ProgressBar.ShowGifProgress(null);
                //GT_CARTO.XApplication.ProgressBar.ShowHint("正在预处理地类图斑数据...");
                //string sPath = pWks.PathName;
                pGP.OverwriteOutput = true;
                pGP.SetEnvironmentValue("workspace", (object)sPath);

                IGpEnumList pfds = pGP.ListFeatureClasses("", "Polygon", "Dataset");

                string sFeatClsName = pfds.Next();

                //IGeoProcessorResult pResult;

                while (!string.IsNullOrEmpty(sFeatClsName))
                {
                    if (!sFeatClsName.Contains("_Standard") &&
                        !sFeatClsName.Equals("TK", StringComparison.OrdinalIgnoreCase))
                    {
                        //要修复的FeatureClass路径
                        string sInFeatureClassPath = string.Format("{0}\\dataset\\{1}", sPath, sFeatClsName);
                        IVariantArray pValues = new VarArrayClass();
                        pValues.Add(sInFeatureClassPath);
                        pGP.Execute("RepairGeometry", pValues, null);
                        object obj = 2;
                        //GT_CONST.LogAPI.CheckLog.AppendErrLogs(pGP.GetMessages(ref obj));
                    }
                    sFeatClsName = pfds.Next();
                }
                return true;
            }

            catch (Exception exp)
            {
                Hy.Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                //GT_CARTO.XApplication.ProgressBar.Hide();
                return false;
            }
            finally
            {
                //GT_CARTO.XApplication.ProgressBar.Hide();
                Marshal.ReleaseComObject(pGP);
            }
        }

    }
}
