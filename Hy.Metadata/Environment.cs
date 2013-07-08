using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Define;


namespace Hy.Metadata
{
    public class Environment:IPlugin
    {

        public static INhibernateHelper NhibernateHelper { get; set; }

        public static IADODBHelper AdodbHelper { internal get; set; }

        public static ILogWriter Logger { internal get; set; }

        public static IApplication Application { internal get; set; }

        public System.Data.IDbConnection SysConnection
        {
            set
            {
            }
        }

        public object GisWorkspace
        {
            set { }
        }

        INhibernateHelper IPlugin.NhibernateHelper
        {
            set
            {
                Environment.NhibernateHelper = value;
            }
        }

        ILogWriter IPlugin.Logger
        {
            set { Environment.Logger = value; }
        }

        public string Description
        {
            get { return "元数据管理数据连接环境"; }
        }


        IApplication IPlugin.Application
        {
            set { Environment.Application = value; }
        }
    }
}
