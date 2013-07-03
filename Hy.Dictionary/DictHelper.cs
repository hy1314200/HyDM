using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Define;
using System.Data;

namespace Hy.Dictionary
{
    public class DictHelper:Define.IPlugin
    {
        internal const string DictTableName = "T_Dictionary";

        internal static INhibernateHelper m_NhibernateHelper{get;set;}

        internal static IADODBHelper m_AdodbHelper{get;set;}

        internal static ILogger m_Logger { get; set; }

        /// <summary>
        /// 获取所有“根类型”名称列表
        /// </summary>
        /// <returns></returns>
        public static IList<string> GetRootTypeNameList()
        {
            return m_NhibernateHelper.GetObjectByCondition<string>("select dItem.Name from DictItem dItem where dItem.Parent = null");
        }

        /// <summary>
        /// 获取所有“根类型”列表
        /// </summary>
        /// <returns></returns>
        public static IList<DictItem> GetRootTypeList()
        {
            return m_NhibernateHelper.GetObjectByCondition<DictItem>("from DictItem dItem where dItem.Parent = null");
        }


        /// <summary>
        /// 从类型名获取子项列表
        /// </summary>
        /// <param name="strType"></param>
        /// <returns></returns>
        public static IList<DictItem> GetSubItems(string strType)
        {
            return m_NhibernateHelper.GetObjectByCondition<DictItem>(string.Format("from DictItem dItem where dItem.Parent.Name='{0}'", strType));
        }

        /// <summary>
        /// 取出所有
        /// </summary>
        /// <returns></returns>
        public static IList<DictItem> GetAll()
        {
            return m_NhibernateHelper.GetObjectByCondition<DictItem>("from DictItem dItem");
        }

        /// <summary>
        /// 删除字典项
        /// </summary>
        /// <param name="dItem"></param>
        /// <returns></returns>
        public static bool DeleteItem(DictItem dItem)
        {
            try
            {
                m_NhibernateHelper.DeleteObject(dItem);
                m_NhibernateHelper.Flush();

                return true;
            }
            catch(Exception exp)
            {
                m_Logger.AppendMessage(enumLogType.Error, string.Format("保存字典时出错：{0}", exp.ToString()));
                
                return false;
            }
        }

        /// <summary>
        /// 保存字典项
        /// </summary>
        /// <param name="dItem"></param>
        /// <returns></returns>
        public static bool SaveItem(DictItem dItem)
        {
            try
            {
                m_NhibernateHelper.SaveObject(dItem);
                m_NhibernateHelper.Flush();

                return true;
            }
            catch(Exception exp)
            {
                m_Logger.AppendMessage(enumLogType.Error, string.Format("保存字典时出错：{0}", exp.ToString()));
                
                return false;
            }
        }


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
            set {  }
        }

        public INhibernateHelper NhibernateHelper
        {
            set { 
                DictHelper.m_NhibernateHelper = value;
                try
                {
                    DictHelper.GetAll();
                }
                catch
                {
                    throw new Exception("数据库中无字典表");
                }
            }
        }

        public ILogger Logger
        {
            set { DictHelper.m_Logger = value; }
        }

        public string Description
        {
            get { return "字典数据库连接环境"; }
        }
    }
}
