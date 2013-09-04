using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesGDB;

namespace Hy.Esri.Catalog.Utility
{
    /// <summary>
    /// 处理SDE的相关问题
    /// 如SDE的GP字符串生成
    /// </summary>
    public class SDEHelper
    {
        /// <summary>
        /// 为SDE的Workspace生成GP字符串
        /// @remark  方法是在系统目录的根目录下生成Temp.SDE,并用此文件来生成字符串
        /// </summary>
        /// <param name="wsTarget"></param>
        /// <param name="featureDatasetName"></param>
        /// <param name="featureClassName"></param>
        /// <returns></returns>
        public static string GetGpString(IWorkspace wsTarget, string featureDatasetName, string featureClassName)
        {
            if (wsTarget == null)
                return null;

            try
            {
                string strTempPath = System.IO.Path.GetPathRoot(global::System.Environment.SystemDirectory); // System.IO.Path.Combine(System.IO.Path.GetPathRoot(Environment.SystemDirectory), "SDETemp");
                string strTempName = "Temp";
                string strGpString = System.IO.Path.Combine(strTempPath, strTempName+".SDE");

                IPropertySet propertySet = wsTarget.ConnectionProperties;
                IWorkspaceFactory wsfSDE = new SdeWorkspaceFactoryClass();
                if (System.IO.File.Exists(strGpString))
                {
                    System.IO.File.Delete(strGpString);
                }
                wsfSDE.Create(strTempPath, strTempName, propertySet, 0);

                if (!string.IsNullOrEmpty(featureDatasetName))
                {
                    //strGpString = System.IO.Path.Combine(strGpString, featureDatasetName);
                    return string.Format("{0}\\{1}\\{2}", strGpString, featureDatasetName, featureClassName);
                }
                else
                {
                    return string.Format("{0}.{1}", strGpString, featureClassName);
                }
            }
            catch
            {
                return null;
            }

        }

    }
}
