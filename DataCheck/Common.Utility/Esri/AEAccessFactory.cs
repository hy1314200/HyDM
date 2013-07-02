using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesFile;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;

namespace Common.Utility.Esri
{

    /// <summary>
    /// 数据类型
    /// </summary>
    public enum enumDataType
    {
        PGDB = 0,
        FileGDB = 1,
        VCT = 2,
        SHP = 3
    }

   
    /// <summary>
    /// workspaceFactory工厂类
    /// </summary>
    public class AEAccessFactory
    {
        public static IWorkspaceFactory2 GetWorkspaceFactory(enumDataType databaseType)
        {
            IWorkspaceFactory2 workspacefacory = null;
            switch (databaseType)
            {
                case enumDataType.PGDB:
                    {
                        workspacefacory = new AccessWorkspaceFactoryClass();
                        break;
                    }
                case enumDataType.FileGDB:
                    {
                        workspacefacory = new FileGDBWorkspaceFactoryClass();
                        break;
                    }
                case enumDataType.SHP:
                    {
                        workspacefacory = new ShapefileWorkspaceFactoryClass();
                        break;
                    }
                default:
                    {
                        return null;
                    }
            }
            return workspacefacory;
        }

        public static IWorkspace OpenWorkspace(enumDataType wsType, string strPath)
        {
            IWorkspaceFactory2 wsFactory = GetWorkspaceFactory(wsType);
            if (wsFactory == null)
                throw new Exception("调用错误：打开Workspace时传入了未被支持的数据类型");

            try
            {
                return wsFactory.OpenFromFile(strPath, 0);
            }
            catch(Exception exp)
            {
                throw new Exception("打开Workspace时发生错误，可能的原因是数据路径与数据类型不一致，信息：" + exp.ToString());
            }
        }


        /// <summary>
        /// 打开个人数据库工作空间
        /// </summary>
        /// <param name="ppOutWS">打开的工作空间</param>
        /// <param name="strFullPath">mdb全路径</param>
        /// <returns>打开成功与否</returns>
        public static bool OpenPGDBEx(ref IWorkspace ppOutWS, string strFullPath)
        {
            try
            {
                if (!System.IO.File.Exists(strFullPath)) return false;

                IWorkspaceFactory2 pFactory = GetWorkspaceFactory(enumDataType.PGDB);
                ppOutWS = pFactory.OpenFromFile(strFullPath, 0);
                if (pFactory != null)
                {
                    Marshal.ReleaseComObject(pFactory);
                    pFactory = null;
                }
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
            return true;
        }

        /// <summary>
        /// 打开个人数据库工作空间
        /// </summary>
        /// <param name="ppOutWS">打开的工作空间</param>
        /// <param name="strFullPath">mdb全路径</param>
        /// <returns>打开成功与否</returns>
        public static bool OpenPGDB(ref IWorkspace ppOutWS, string strFullPath)
        {
            try
            {
                if (!System.IO.File.Exists(strFullPath)) return false;

                IWorkspaceFactory pFactory = GetWorkspaceFactory(enumDataType.PGDB);
                IPropertySet Propset = new PropertySetClass();
                Propset.SetProperty("DATABASE", strFullPath);
                ppOutWS = pFactory.Open(Propset, 0);
                if (Propset != null)
                {
                    Marshal.ReleaseComObject(Propset);
                    Propset = null;
                }
                if (pFactory != null)
                {
                    Marshal.ReleaseComObject(pFactory);
                    pFactory = null;
                }
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
            return true;
        }

        /// <summary>
        /// 打开FileGeoDatabase
        /// </summary>
        /// <param name="ppOutWS"></param>
        /// <param name="strFullPath"></param>
        /// <returns></returns>
        public static bool OpenFGDBEx(ref IWorkspace ppOutWS, string strFullPath)
        {
            try
            {
                if (!System.IO.Directory.Exists(strFullPath)) return false;
                Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
                IWorkspaceFactory pFactory = (IWorkspaceFactory2)Activator.CreateInstance(factoryType);
                IPropertySet Propset = new PropertySetClass();
                Propset.SetProperty("DATABASE", strFullPath);
                ppOutWS = pFactory.Open(Propset, 0);
                if (Propset != null)
                {
                    Marshal.ReleaseComObject(Propset);
                    Propset = null;
                }
                if (pFactory != null)
                {
                    Marshal.ReleaseComObject(pFactory);
                    pFactory = null;
                }
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());


            }
            return true;
        }

        /// <summary>
        /// 打开FileGeoDatabase
        /// </summary>
        /// <param name="ppOutWS"></param>
        /// <param name="strFullPath"></param>
        /// <returns></returns>
        public static bool OpenFGDB(ref IWorkspace ppOutWS, string strFullPath)
        {
            try
            {
                string connectionstring = "DATABASE=" + strFullPath;
                IWorkspaceFactory2 pFactory = (IWorkspaceFactory2)new FileGDBWorkspaceFactoryClass();
                ppOutWS = pFactory.OpenFromString(connectionstring, 0);
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
            return true;
        }

        /// <summary>
        /// 创建新的FileGeodatabase
        /// </summary>
        /// <param name="ParentDirectory">创建FGDB路径</param>
        /// <param name="strName">FGDB名称</param>
        public static bool CreateFGDB(string ParentDirectory, string strName)
        {
            IWorkspace ipWks = null;
            try
            {
                IWorkspaceFactory2 workspaceFactory = (IWorkspaceFactory2)new FileGDBWorkspaceFactoryClass();
                string strFilePath = ParentDirectory +"\\"+ strName;
                if (System.IO.Directory.Exists(strFilePath))
                {
                    System.IO.Directory.Delete(strFilePath, true);
                }

                IWorkspaceName workspacename = workspaceFactory.Create(ParentDirectory, strName, null, 0);
                ipWks = ((IName)workspacename).Open() as IWorkspace;
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
            finally
            {
                if(ipWks!=null)
                {
                    Marshal.ReleaseComObject(ipWks);
                    ipWks = null;
               }
            }
            return true;
        }

        /// <summary>
        /// 创建新的FileGeodatabase
        /// </summary>
        /// <param name="ParentDirectory">创建FGDB路径</param>
        /// <param name="strName">FGDB名称</param>
        public static bool CreateFGDB(string ParentDirectory, string strName,ref IWorkspace ws)
        {
            IWorkspace ipWks = null;
            try
            {
                IWorkspaceFactory2 workspaceFactory = (IWorkspaceFactory2)new FileGDBWorkspaceFactoryClass();
                string strFilePath = ParentDirectory + "\\" + strName;
                if (System.IO.Directory.Exists(strFilePath))
                {
                    System.IO.Directory.Delete(strFilePath, true);
                }

                IWorkspaceName workspacename = workspaceFactory.Create(ParentDirectory, strName, null, 0);
                ipWks = ((IName)workspacename).Open() as IWorkspace;
                ws = ipWks;
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
            return true;
        }

        /// <summary>
        /// 创建新的PersonalGeodatabase
        /// </summary>
        /// <param name="ParentDirectory">创建MDB路径</param>
        /// <param name="strName">MDB名称</param>
        public static bool CreatePGDB(string ParentDirectory, string strName)
        {
             IWorkspace Ws =null;
            try
            {
                IWorkspaceFactory2 workspaceFactory = (IWorkspaceFactory2)new AccessWorkspaceFactoryClass();
                string strFilePath = ParentDirectory + strName;
                if (System.IO.File.Exists(strFilePath))
                {
                    System.IO.File.Delete(strFilePath);
                }
                IWorkspaceName workspacename = workspaceFactory.Create(ParentDirectory, strName, null, 0);
                string connectionstring = "DATABASE=" + workspacename.PathName;
                Ws = workspaceFactory.OpenFromString(connectionstring, 0);
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
            finally
            {
                if (Ws != null)
                {
                    Marshal.ReleaseComObject(Ws);
                    Ws = null;
                }
            }
            return true;
        }

        /// <summary>
        /// 创建新的PersonalGeodatabase
        /// </summary>
        /// <param name="ParentDirectory">创建MDB路径</param>
        /// <param name="strName">MDB名称</param>
        public static bool CreatePGDB(string ParentDirectory, string strName,ref IWorkspace work)
        {
            try
            {
                IWorkspaceFactory2 workspaceFactory = (IWorkspaceFactory2)new AccessWorkspaceFactoryClass();
                string strFilePath = ParentDirectory + strName;
                if (System.IO.File.Exists(strFilePath))
                {
                    System.IO.File.Delete(strFilePath);
                }
                IWorkspaceName workspacename = workspaceFactory.Create(ParentDirectory, strName, null, 0);
                string connectionstring = "DATABASE=" + workspacename.PathName;
                IWorkspace Ws = workspaceFactory.OpenFromString(connectionstring, 0);
                work = Ws;
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
            return true;
        }


        /// <summary>
        /// 重置工作空间
        /// </summary>
        /// <param name="pNewWksp"></param>
        /// <param name="pMap"></param>
        /// <returns></returns>
        public static bool ResetMapWorkspace(IWorkspace pNewWksp, IMap pMap)
        {
            UID uid = new UIDClass();
            uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";

            IEnumLayer pEnumLayer = pMap.get_Layers(uid, true);
            pEnumLayer.Reset();
            ILayer pLayer = pEnumLayer.Next();
            while (pLayer != null)
            {
                //if (pLayer.Name.Contains("XZQ_") || pLayer.Name.Contains("Dist_"))
                //{
                //    ResetLayerDatasource2(pLayer as IDataLayer2, pNewWksp);
                //}
                //else
                //{
                    ResetLayerDatasource(pLayer as IDataLayer2, pNewWksp);
                //}
                pLayer = pEnumLayer.Next();
            }
            return true;
        }

        /// <summary>
        /// 重置图层数据源
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="pNewWksp"></param>
        /// <returns></returns>
        public static bool ResetLayerDatasource(IDataLayer2 pLayer, IWorkspace pNewWksp)
        {
            if (pLayer == null || pNewWksp == null) return false;

            //// 2012-06-15 张航宇
            //// 直接获取Layer的数据源名称，从新的Workspace打开FeatureClass进行替换
            //IDatasetName fClassName = pLayer.DataSourceName as IDatasetName;
            //if (fClassName != null)
            //{
            //    string strClassName = fClassName.Name;
            //    try
            //    {
            //        IFeatureClass fClass = (pNewWksp as IFeatureWorkspace).OpenFeatureClass(strClassName);
            //        pLayer.DataSourceName = (fClass as IDataset).FullName;
            //    }
            //    catch
            //    {
            //        return false;
            //    }
            //}

            IEnumDatasetName pEnumDsName = pNewWksp.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
            IDatasetName pFtDsName = pEnumDsName.Next();
            try
            {
                if (!pLayer.InWorkspace(pNewWksp))
                {
                    IFeatureClassName pOldName = pLayer.DataSourceName as IFeatureClassName;
                    // 2012-06-15 张航宇
                    // 修改对没有Dataset的Workspace，直接将Workspace重置
                    if (pOldName != null)
                    {
                        if (pOldName.FeatureDatasetName == null || pFtDsName == null)
                        {
                            (pOldName as IDatasetName).WorkspaceName = (pNewWksp as IDataset).FullName as IWorkspaceName;
                        }
                        else
                        {
                            pOldName.FeatureDatasetName = pFtDsName;
                            pOldName.FeatureDatasetName.WorkspaceName = ((IDataset)pNewWksp).FullName as IWorkspaceName;
                        }
                    }
                    pLayer.Connect(pOldName as IName);
                }
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
            return true;
        }

        /// <summary>
        /// 重置图层数据源
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="pNewWksp"></param>
        /// <returns></returns>
        public static bool ResetLayerDatasource2(IDataLayer2 pLayer, IWorkspace pNewWksp)
        {
            if (pLayer == null || pNewWksp == null) return false;
            IEnumDataset pEnumDsName = pNewWksp.get_Datasets(esriDatasetType.esriDTFeatureDataset);
            IDataset pFtDsName = pEnumDsName.Next();
            IEnumDataset pEnumDs = pFtDsName.Subsets;
            try
            {
                if (!pLayer.InWorkspace(pNewWksp))
                {
                    IFeatureClassName pOldName = pLayer.DataSourceName as IFeatureClassName;

                    if (pOldName != null)
                    {
                        IDatasetName dsName = pOldName as IDatasetName;
                        IDataset pDs = null;
                        while ((pDs = pEnumDs.Next()) != null)
                        {
                            if (dsName.Name.Equals(pDs.Name, StringComparison.OrdinalIgnoreCase) || 
                                pDs.Name.Contains(dsName.Name.ToUpper()))
                            {
                                IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                                if (pFeatureLayer == null) return true;
                                pFeatureLayer.FeatureClass = pDs as IFeatureClass;
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
            return true;
        }

        /// <summary>
        /// 压缩数据库工作空间
        /// </summary>
        public static void CompactWorkspace(IWorkspace workspace)
        {
            try
            {
                IDatabaseCompact databaseCompact = workspace as IDatabaseCompact;
                if (databaseCompact != null)
                {
                    if (databaseCompact.CanCompact())
                        databaseCompact.Compact();
                }
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

            }
            finally
            {
            }
        }
    }
}
