using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Define;

namespace Frame
{
    internal class DefaultApplication:IApplication
    {
        public DefaultApplication()
        {
            Enterprise = ConfigManager.Enterprise;
            this.UserID= "Administrator";
            this.UserName = "系统管理员";
        }
        public string Name
        {
            get { return ConfigManager.AppName; }
        }

        public string Enterprise
        {
            get;
            set;
        }

        public object UserID
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string Version
        {
            get { return ConfigManager.Version; }
        }
    }
}
