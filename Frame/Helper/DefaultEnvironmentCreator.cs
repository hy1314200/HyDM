using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame
{
    internal class DefaultEnvironmentCreator:Define.IEnvironmentCreator
    {
        public global::Define.IAdodbHelper AdodbHelper
        {
            get
            {
                return new Utility.AdodbHelper(NhibernateHelper.DbConnection,Utility.DataFactory.GetProviderFactory(ConfigManager.ADOType));
            }
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

        private Helper.DbLogger m_LogWriter;
        public global::Define.ILogWriter LogWriter
        {
            get {
                if (m_LogWriter == null)
                    m_LogWriter = new Helper.DbLogger();

                return m_LogWriter;
            
            }// Logger.Instance; }
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

            if (m_LogWriter != null)
            {
                m_LogWriter.Flush();
            }
            Logger.Instance.Close();

        }
    }
}
