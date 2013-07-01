using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Common.Utility.Encryption;
using System.IO;
using Common.UI;
using CheckCommand;
using Check.Utility;
using Common.Utility;

namespace Check.Demo.Helper
{
    internal class ConfigManager
    {

        /// <summary>
        /// 获取设置的环境变量
        /// </summary>
        public static void GetEnvironVariable()
        {
            
            //SysDbHelper.SysDbPath=SysDBPath;
           
            ////拷贝mxd到系统指定目录
            //foreach (System.Collections.Generic.KeyValuePair<string,string> entry in MxdMapping)
            //{
            //    if (!string.IsNullOrEmpty(entry.Value))
            //    {
            //        string name=Path.GetFileName(entry.Value);
            //        System.IO.File.Copy(entry.Value, string.Format("{0}{1}", System.Windows.Forms.Application.StartupPath, name),true);
            //    }
            //}
            CheckApplication.BolIgnoreRootFile = IgnoreRootFile;
            CheckApplication.RelationalPath = RelativePath;

            COMMONCONST.RemovePlus = DataPrefix;
            COMMONCONST.m_strDistinctField = IdentityFieldName;
            COMMONCONST.dAreaThread = FragmentAreaTolerance;
            COMMONCONST.dLengthThread = FragmentLineTolerance;
        }

        ///// <summary>
        ///// 系统库路径
        ///// </summary>
        //public static string SysDBPath
        //{
        //    get
        //    {
        //        return string.Format( ConfigReader.GetStringValue("SysDB"),System.Windows.Forms.Application.StartupPath);
        //    }
        //}

        /// <summary>
        /// 获取Mxd文件与标准名的映射关系（保留）
        /// </summary>
        public static Dictionary<string, string> MxdMapping
        {
            get
            {
                XmlNode nodeMapping = ConfigReader.GetNode("MxdMapping");
                if (nodeMapping == null)
                    return null;

                Dictionary<string, string> dicMapping = new Dictionary<string, string>();
                foreach (XmlNode nodeItem in nodeMapping.ChildNodes)
                {
                    dicMapping.Add(nodeItem.Attributes["StandardName"].Value, nodeItem.Attributes["MXDFile"].Value);
                }

                return dicMapping;
            }
        }

        /// <summary>
        /// 数据(图层名)统一前缀，用于导入时去掉
        /// </summary>
        public static string DataPrefix
        {
            get
            {
                return ConfigReader.GetStringValue("DataPrefix");
            }
        }

        /// <summary>
        /// 唯一（标识）字段名
        /// </summary>
        public static string IdentityFieldName
        {
            get
            {
                return ConfigReader.GetStringValue("IdentityFieldName");
            }
        }

        /// <summary>
        /// 碎面容差值，单位：平方米
        /// 默认值为400
        /// </summary>
        public static double FragmentAreaTolerance
        {
            get
            {
                string strValue= ConfigReader.GetStringValue("FragmentAreaTolerance");
                if (string.IsNullOrEmpty(strValue))
                    return COMMONCONST.dAreaThread;

                return Convert.ToDouble(strValue);
            }
        }

        /// <summary>
        /// 碎线容差值，单位：米
        /// 默认值为0.2
        /// </summary>
        public static double FragmentLineTolerance
        {
            get
            {
                string strValue = ConfigReader.GetStringValue("FragmentLineTolerance");
                if (string.IsNullOrEmpty(strValue))
                    return COMMONCONST.dLengthThread;

                return Convert.ToDouble(strValue);
            }
        }

        /// <summary>
        /// 获取数据源存储的相对路径名称
        /// 默认值为空
        /// </summary>
        public static string RelativePath
        {
            get
            {
                string strValue = ConfigReader.GetStringValue(@"MultiTask/RelativePath");
                if (!string.IsNullOrEmpty(strValue))
                {
                    return strValue;
                }
                return "";
            }
        }

        /// <summary>
        ///获取是否忽略目录下的数据源文件
        ///默认值为True;
        /// </summary>
        /// <value>The relative path.</value>
        public static bool IgnoreRootFile
        {
            get
            {
                string strValue = ConfigReader.GetStringValue(@"MultiTask/IgnoreRootFile");
                if (!string.IsNullOrEmpty(strValue))
                {
                    return Convert.ToBoolean(strValue);
                }
                return true;
            }
        }
    }
}
