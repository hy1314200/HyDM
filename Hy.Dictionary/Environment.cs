using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Define;

namespace Hy.Dictionary
{
    public class Environment:IPlugin
    {

        public static INhibernateHelper NhibernateHelper {internal get; set; }

        public static IADODBHelper AdodbHelper { internal get; set; }

        public static ILogger Logger { internal get; set; }


        public System.Data.IDbConnection SysConnection
        {
            set
            {
                //IDbCommand dbCmd = value.CreateCommand();
                //dbCmd.CommandText = string.Format("select 0 from {0} where 1=2", DictHelper.DictTableName);
                //try
                //{
                //    dbCmd.ExecuteNonQuery();
                //}
                //catch
                //{
                //}
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

        ILogger IPlugin.Logger
        {
            set { Environment.Logger = value; }
        }

        public string Description
        {
            get { return "字典数据库连接环境"; }
        }
    }
}
