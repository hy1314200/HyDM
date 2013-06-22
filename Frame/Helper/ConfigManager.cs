using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Frame
{
    internal class ConfigManager
    {
        /// <summary>
        /// 应用名
        /// </summary>
        public static string AppName
        {
            get
            {
                return ConfigurationManager.AppSettings["AppName"];
            }
        }

        /// <summary>
        /// Logo文件
        /// </summary>
        public static System.Drawing.Icon Logo
        {
            get
            {
                string strLogoFile=string.Format(ConfigurationManager.AppSettings["Logo"],System.Windows.Forms.Application.StartupPath);
                if (System.IO.File.Exists(strLogoFile))
                {
                    return new System.Drawing.Icon(strLogoFile);
                }

                return Properties.Resources.DefaultLogo;
            }
        }

        /// <summary>
        /// ADO数据库类型
        /// </summary>
        public static string ADOType
        {
            get
            {
                return ConfigurationManager.AppSettings["ADOType"];
            }
        }

        /// <summary>
        /// ADO连接字符串
        /// </summary>
        public static string ADOConnection
        {
            get
            {
                return string.Format(ConfigurationManager.AppSettings["ADOConnection"],System.Windows.Forms.Application.StartupPath);
            }
        }

        /// <summary>
        /// ESRI数据库类型
        /// </summary>
        public static string WorkspaceType
        {
            get
            {
                return ConfigurationManager.AppSettings["WorkspaceType"];
            }
        }

        /// <summary>
        /// ESRI连接参数
        /// </summary>
        public static string WorkspaceArgs
        {
            get
            {
                return ConfigurationManager.AppSettings["WorkspaceArgs"];
            }
        }

        /// <summary>
        /// Hibernate的程序集列表
        /// </summary>
        public static List<string> HibernateAssemblys
        {
            get
            {
                string strAssemblys= ConfigurationManager.AppSettings["HibernateAssemblys"];
                return strAssemblys.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
        }

        public static string ResourceManager
        {
            get
            {
                return ConfigurationManager.AppSettings["ResourceManager"];
            }
        }
    }
}
