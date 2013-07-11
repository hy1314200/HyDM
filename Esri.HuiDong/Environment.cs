using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Define;

namespace Esri.HuiDong
{
    public class Environment:IPlugin
    {

        public static INhibernateHelper NhibernateHelper { get; set; }

        public static IAdodbHelper AdodbHelper {  get; set; }

        public static ILogWriter Logger {  get; set; }

        public static IApplication Application {  get; set; }


        IAdodbHelper IPlugin.AdodbHelper
        {
            set
            {
                Environment.AdodbHelper = value;
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
            get { return "惠东据库连接环境"; }
        }


        IApplication IPlugin.Application
        {
            set { Environment.Application = value; }
        }
    }
}
