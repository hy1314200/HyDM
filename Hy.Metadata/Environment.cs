using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Define;


namespace Frame
{
    public class Environment:IPlugin
    {
        internal static ILogger Logger { get; private set; }

        internal static IDbConnection DbConnection { get; private set; }

        internal static INhibernateHelper NHibernateHelper { get; private set; }


        public string Description
        {
            get { return "元数据管理数据连接环境"; }
        }

        public IDbConnection SysConnection
        {
            set { DbConnection = value; }
        }

        public object GisWorkspace
        {
            set {  }
        }

        public INhibernateHelper NhibernateHelper
        {
            set { Environment.NHibernateHelper = value; }
        }

        ILogger IPlugin.Logger
        {
            set { Logger = value; }
        }
    }
}
