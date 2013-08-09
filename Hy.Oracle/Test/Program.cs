using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NHibernate.Cfg;
using Microsoft.Win32;
using System.Data.Common;

namespace Test
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            DbConnectionStringBuilder dcsBuilder = new DbConnectionStringBuilder();
            dcsBuilder.Add("User ID", "wjzgis");
            dcsBuilder.Add("Password", "wjzgis");
            dcsBuilder.Add("Service Name", "sunz");
            dcsBuilder.Add("Host", "172.16.1.9");
            dcsBuilder.Add("Integrated Security", false);
            string licPath = Application.StartupPath + "\\DDTek.lic";
            //dcsBuilder.Add("License Path", licPath);
            //若路径中存在空格，则会在路径名称前加上"\""
            string conStr = dcsBuilder.ConnectionString;
            conStr = conStr.Replace("\"", "");

            Configuration config = new Configuration();
            config.AddDirectory(new System.IO.DirectoryInfo( System.IO.Path.Combine(Application.StartupPath, "DataMapping")));

            config.Properties["connection.connection_string"] = conStr;


            NHibernate.ISessionFactory sFactory = config.BuildSessionFactory();
            NHibernate.ISession session = sFactory.OpenSession();   


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
