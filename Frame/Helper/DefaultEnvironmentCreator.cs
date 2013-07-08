using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame
{
    internal class DefaultEnvironmentCreator:Define.IEnvironmentCreator
    {
        public System.Data.IDbConnection SysConnection
        {
            get { return (this.NhibernateHelper as Utility.NhibernateHelper).DbConnection; }
                //Utility.DataFactory.GetConnection(ConfigManager.ADOType, ConfigManager.ADOConnection); }
        }

        private Utility.NhibernateHelper m_NHibernateHelper;
        public global::Define.INhibernateHelper NhibernateHelper
        {
            get
            {
                if (m_NHibernateHelper == null)
                {
                    m_NHibernateHelper = Utility.DataFactory.GetNhibernateHelper(ConfigManager.ADOConnection, ConfigManager.HibernateAssemblys);
                }
                return m_NHibernateHelper;
            }
        }

        public global::Define.ILogWriter LogWriter
        {
            get { return Logger.Instance; }
        }

        public global::Define.IApplication Application
        {
            get { return new DefaultApplication(); }
        }


        public void Release()
        {
            if (m_NHibernateHelper != null)
            {
                m_NHibernateHelper.Close();
            }
            Logger.Instance.Close();
        }
    }
}
