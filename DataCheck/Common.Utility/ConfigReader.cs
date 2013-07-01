using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Common.Utility
{
    /// <summary>
    /// Config文件简单读取
    /// </summary>
    public class ConfigReader
    {
        /// <summary>
        /// 配置文件名称
        /// </summary>
        private readonly static string ConfigName = "Setting.config";

        /// <summary>
        /// 配置文件全路径
        /// </summary>
        private static string m_ConfigFile =string.Format("{0}\\{1}", System.Windows.Forms.Application.StartupPath,ConfigName);
        /// <summary>
        /// 获取或设置Config文件路径
        /// </summary>
        public static string ConfigFile
        {
            get
            {
                if (!System.IO.File.Exists(m_ConfigFile))
                {

                    return null;
                }
                return m_ConfigFile;
            }
            //set
            //{
            //    m_ConfigFile = value;
            //    // 重置
            //    m_RootNode = null;
            //}
        }

        private static XmlNode m_RootNode;

        private static XmlNode GetRootNode()
        {
            if (m_RootNode == null)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                m_RootNode = xmlDoc.DocumentElement;
            }

            return m_RootNode;
        }

        /// <summary>
        /// 从Config文件读取指定节点名的节点
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static XmlNode GetNode(string strKey)
        {
            XmlNode nodeRoot = GetRootNode();
            return nodeRoot.SelectSingleNode(strKey);
        }

        /// <summary>
        /// 从Config文件读取指定名节点的值
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string GetStringValue(string strKey)
        {
            XmlNode nodeValue = GetNode(strKey);
            if (nodeValue == null)
                return null;
            return nodeValue.InnerText;
        }

    }
}
