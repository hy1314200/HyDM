using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using Hy.Esri.Catalog.Define;

namespace Hy.Esri.Catalog.Utility
{
    /// <summary>
    /// Workspace服务类
    /// 如打开Workspace等
    /// </summary>
    public class WorkspaceHelper
    {
        /// <summary>
        /// 打开Workspace
        /// </summary>
        /// <param name="wsType"></param>
        /// <param name="objWorkspace">当为SDE时使用IPropertySet，其余情况使用路径（string）</param>
        /// <returns></returns>
        public static IWorkspace OpenWorkspace(enumWorkspaceType wsType, object objWorkspace)
        {
           IWorkspaceFactory wsf = null;
            try
            {
                switch (wsType)
                {
                    case enumWorkspaceType.FileGDB:
                        wsf = new FileGDBWorkspaceFactoryClass();

                        return wsf.OpenFromFile(objWorkspace as string, 0);

                    case enumWorkspaceType.PGDB:
                        wsf = new AccessWorkspaceFactoryClass();
                        return wsf.OpenFromFile(objWorkspace as string, 0);

                    case enumWorkspaceType.File:
                        wsf = new ShapefileWorkspaceFactoryClass();
                        return wsf.OpenFromFile(objWorkspace as string, 0);

                    case enumWorkspaceType.SDE:
                        IPropertySet pSet = objWorkspace as IPropertySet;
                        if (pSet == null)
                        {
                            string strArgs = objWorkspace as string;
                            pSet= new PropertySetClass();
                            string[] argList = strArgs.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string strArg in argList)
                            {
                                string[] argPair = strArg.Split(new char[] { '=' });
                                pSet.SetProperty(argPair[0], argPair[1]);
                            }
                        }

                        return wsf.Open(objWorkspace as IPropertySet, 0);
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                if (wsf != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wsf);
            }

            return null;
        }

        /// <summary>
        /// PropertySet生成显示字符串
        /// </summary>
        /// <param name="propertySet"></param>
        /// <returns></returns>
        public static string PropertySetToString(IPropertySet propertySet)
        {
            if (propertySet == null)
                return null;

            string strPropertySet = string.Format("DbClient={0};", propertySet.GetProperty("dbclient"));
            strPropertySet += string.Format("Server={0};", propertySet.GetProperty("Server"));
            strPropertySet += string.Format("Instance={0};", propertySet.GetProperty("Instance"));
            strPropertySet += string.Format("User={0};", propertySet.GetProperty("user"));
            strPropertySet += string.Format("Password={0}", propertySet.GetProperty("password"));

            return strPropertySet;
        }

        /// <summary>
        /// 创建FileGDB和PGDB Workspace
        /// @remark 
        /// 1.Shp创建Workspace没有意义
        /// 2.不支持SDE创建
        /// </summary>
        /// <param name="wsType"></param>
        /// <param name="strPath"></param>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static IWorkspace CreateWorkspace(enumWorkspaceType wsType, string strPath, string strName)
        {
            try
            {
                IWorkspaceFactory wsf = null;
                switch (wsType)
                {
                    case enumWorkspaceType.SDE:
                        throw new Exception("CreateWorkspace方法被设计为不支持SDE方式创建");

                    case enumWorkspaceType.FileGDB:
                        wsf = new ESRI.ArcGIS.DataSourcesGDB.FileGDBWorkspaceFactoryClass();
                        break;

                    case enumWorkspaceType.PGDB:
                        wsf = new AccessWorkspaceFactoryClass();
                        break;

                    case enumWorkspaceType.File:
                        wsf = new ShapefileWorkspaceFactoryClass();
                        break;

                }
                IWorkspaceName wsName = wsf.Create(strPath, strName, null, 0);
                return (wsName as IName).Open() as IWorkspace;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 给指定的Workspace生成GP字符串
        /// </summary>
        /// <param name="wsTarget"></param>
        /// <returns></returns>
        public static string GetGpString(IWorkspace wsTarget)
        {
            if (wsTarget == null)
                return null;

            return GetGpString((wsTarget as IDataset).FullName as IWorkspaceName);
            //if (wsTarget.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
            //{

            //    try
            //    {
            //        string strTempPath = System.IO.Path.GetPathRoot(Environment.SystemDirectory); // System.IO.Path.Combine(System.IO.Path.GetPathRoot(Environment.SystemDirectory), "SDETemp");
            //        string strTempName = "Temp";
            //        string strGpString = System.IO.Path.Combine(strTempPath, strTempName + ".SDE");
            //        if (System.IO.File.Exists(strGpString))
            //        {
            //            System.IO.File.Delete(strGpString);
            //        }

            //        IPropertySet propertySet = (wsTarget.ConnectionProperties as IClone).Clone() as IPropertySet;
            //        IWorkspaceFactory wsfSDE = new SdeWorkspaceFactoryClass();
            //        wsfSDE.Create(strTempPath, strTempName, propertySet, 0);
            //        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(propertySet);
            //        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(wsfSDE);
            //        return strGpString;
            //    }
            //    catch
            //    {
            //        return null;
            //    }
            //}
            //else
            //{
            //    return wsTarget.PathName;
            //}
        }

        /// <summary>
        /// 给指定的WorkspaceName生成GP字符串
        /// </summary>
        /// <param name="wsNameTarget"></param>
        /// <returns></returns>
        public static string GetGpString(IWorkspaceName wsNameTarget)
        {
            if (wsNameTarget == null)
                return null;

            if (wsNameTarget.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
            {
                try
                {
                    string strTempPath = System.IO.Path.GetPathRoot(System.Environment.SystemDirectory); // System.IO.Path.Combine(System.IO.Path.GetPathRoot(Environment.SystemDirectory), "SDETemp");
                    string strTempName = string.Format("Temp_{0}", wsNameTarget.GetHashCode());
                    string strGpString = System.IO.Path.Combine(strTempPath, strTempName + ".SDE");

                    IPropertySet propertySet = wsNameTarget.ConnectionProperties;
                    IWorkspaceFactory wsfSDE = new SdeWorkspaceFactoryClass();
                    if (System.IO.File.Exists(strGpString))
                    {
                        System.IO.File.Delete(strGpString);
                    }
                    wsfSDE.Create(strTempPath, strTempName, propertySet, 0);
                    return strGpString;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return wsNameTarget.PathName;
            }
        }

        /// <summary>
        /// 指定Workspace，FeatureDataset名 和 FeatureClass名生成Gp字符串
        /// </summary>
        /// <param name="wsTarget"></param>
        /// <param name="featureDatasetName">可以为空</param>
        /// <param name="featureClassName">可以为空</param>
        /// <returns></returns>
        public static string GetGpString(IWorkspace wsTarget, string featureDatasetName, string featureClassName)
        {
            string strGpString = GetGpString(wsTarget);
            if (strGpString == null)
                return null;

            if (!string.IsNullOrEmpty(featureDatasetName))
            {
                strGpString = string.Format("{0}\\{1}", strGpString, featureDatasetName);
            }
            if (!string.IsNullOrWhiteSpace(featureClassName))
            {
                strGpString = string.Format("{0}\\{1}", strGpString, featureClassName);
            }

            return strGpString;
        }

        /// <summary>
        /// 指定WorkspaceName，FeatureDataset名 和 FeatureClass名生成Gp字符串
        /// </summary>
        /// <param name="wsNameTarget"></param>
        /// <param name="featureDatasetName">可以为空</param>
        /// <param name="featureClassName">可以为空</param>
        /// <returns></returns>
        public static string GetGpString(IWorkspaceName wsNameTarget, string featureDatasetName, string featureClassName)
        {
            string strGpString = GetGpString(wsNameTarget);
            if (strGpString == null)
                return null;

            if (!string.IsNullOrEmpty(featureDatasetName))
            {
                strGpString = string.Format("{0}\\{1}", strGpString, featureDatasetName);
            }
            if (!string.IsNullOrWhiteSpace(featureClassName))
            {
                strGpString = string.Format("{0}\\{1}", strGpString, featureClassName);
            }

            return strGpString;
        }

        /// <summary>
        /// 生成TerrainLayer的格式化字符串
        /// </summary>
        /// <param name="wsTarget"></param>
        /// <param name="strName">若为空，则生成null</param>
        /// <returns></returns>
        public static string GetTELayerString(IWorkspace wsTarget, string strName)
        {
            if (wsTarget == null)
                return null;

            if (string.IsNullOrWhiteSpace(strName))
                return null;

            if (wsTarget.Type == esriWorkspaceType.esriFileSystemWorkspace)
            {
                return string.Format("{0}\\{1}", wsTarget.PathName, strName);
            }
            else if(wsTarget.Type==esriWorkspaceType.esriRemoteDatabaseWorkspace)
            {   
                //htParameters.Add("SERVER", "172.16.1.9");
                //htParameters.Add("USER", "sde");
                //htParameters.Add("PASSWORD", "sde");
                //htParameters.Add("VERSION", "SDE.DEFAULT");
                //htParameters.Add("INSTANCE", "5151/tcp");

                //"Server=" + server + ";User=" + user + ";Password=" + password + ";LayerName=" + layerName + ";Instance=5151/tcp;TEPlugName=arcsde;"

                try
                {
                    IPropertySet propertySet = wsTarget.ConnectionProperties;
                    object[] objParameters = 
                    {
                        propertySet.GetProperty("SERVER"),
                        propertySet.GetProperty("USER"),
                        propertySet.GetProperty("PASSWORD"),
                        strName,
                        propertySet.GetProperty("INSTANCE")
                    };

                    return string.Format("Server={0};User={1};Password={2};LayerName={3};Instance={4};TEPlugName=arcsde;", objParameters);
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        public static string GetTELayerString(object objWorkspacePropertySet, enumWorkspaceType wsType, string strName)
        {
            if (objWorkspacePropertySet==null|| string.IsNullOrWhiteSpace(strName))
                return null;

            if (wsType == enumWorkspaceType.SDE)
            {
                IPropertySet propertySet = objWorkspacePropertySet as IPropertySet;
                if (propertySet == null)
                    return null;

                object[] objParameters = 
                    {
                        propertySet.GetProperty("SERVER"),
                        propertySet.GetProperty("USER"),
                        propertySet.GetProperty("PASSWORD"),
                        strName,
                        propertySet.GetProperty("INSTANCE")
                    };

                return string.Format("Server={0};User={1};Password={2};LayerName={3};Instance={4};TEPlugName=arcsde;", objParameters);
             
            }
            else if(wsType==enumWorkspaceType.File)
            {
                return string.Format("{0}\\{1}{2}",objWorkspacePropertySet,strName,strName.ToLower().Contains(".shp")?null:".shp");
            }
            else if(wsType==enumWorkspaceType.PGDB)
            {
                return string.Format("FileName={0};LayerName={1}", objWorkspacePropertySet, strName);
            }

            return null;
        }
    }
}
