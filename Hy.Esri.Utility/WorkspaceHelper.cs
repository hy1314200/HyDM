using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;

namespace Hy.Esri.Utility
{
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
                        wsf = new SdeWorkspaceFactoryClass();
                        IPropertySet pSet = objWorkspace as IPropertySet;
                        if (pSet == null)
                        {
                            string strArgs = objWorkspace as string;
                            pSet = new PropertySetClass();
                            string[] argList = strArgs.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string strArg in argList)
                            {
                                string[] argPair = strArg.Split(new char[] { '=' });
                                pSet.SetProperty(argPair[0], argPair[1]);
                            }
                        }

                        return wsf.Open(pSet, 0);
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

    }
}
