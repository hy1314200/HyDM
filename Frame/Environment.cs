using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Utility;

namespace Frame
{
    internal class Environment
    {
        public static Logger Logger
        {
            get { return Logger.Instance; }
        }

        private static IDbConnection m_SysDbConnection;
        internal static IDbConnection SysDbConnection
        {
            get
            {
                if (m_SysDbConnection == null || m_SysDbConnection.State == ConnectionState.Closed)
                {
                    
                    m_SysDbConnection = DataFactory.GetConnection(ConfigManager.ADOType, ConfigManager.ADOConnection);
                }

                return m_SysDbConnection;
            }
        }

        private static NhibernateHelper m_NHibernateHelper;
        internal static NhibernateHelper NHibernateHelper
        {
            get
            {
                if(m_NHibernateHelper==null)
                   m_NHibernateHelper=  Utility.DataFactory.GetNhibernateHelper(ConfigManager.ADOConnection, ConfigManager.HibernateAssemblys);

                return m_NHibernateHelper;
            }
        }

        private static object m_Workspace;
        internal static object Workspace
        {
            get
            {
                if (m_Workspace == null)
                    m_Workspace =ResourceManager.GetWorkspace(ConfigManager.WorkspaceType, ConfigManager.WorkspaceArgs);

                return m_Workspace;
            }
        }

        internal static Define.IResourceManager ResourceManager { get; set; }
    }
}
