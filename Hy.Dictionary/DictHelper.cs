using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hy.Dictionary
{
    public class DictHelper
    {

        /// <summary>
        /// 获取所有“根类型”名称列表
        /// </summary>
        /// <returns></returns>
        public static IList<string> GetRootTypeNameList()
        {
            return Environment.NhibernateHelper.GetObjectsByCondition<string>("select dItem.Name from DictItem dItem where dItem.Parent = null");
        }

        /// <summary>
        /// 获取所有“根类型”列表
        /// </summary>
        /// <returns></returns>
        public static IList<DictItem> GetRootTypeList()
        {
            return Environment.NhibernateHelper.GetObjectsByCondition<DictItem>("from DictItem dItem where dItem.Parent = null");
        }


        /// <summary>
        /// 从类型名获取子项列表
        /// </summary>
        /// <param name="strType"></param>
        /// <returns></returns>
        public static IList<DictItem> GetSubItems(string strType)
        {
            return Environment.NhibernateHelper.GetObjectsByCondition<DictItem>(string.Format("from DictItem dItem where dItem.Parent.Name='{0}'", strType));
        }

        public static IList<DictItem> GetItemsByName(string strName)
        {
            return Environment.NhibernateHelper.GetObjectsByCondition<DictItem>(string.Format("from DictItem dItem where dItem.Name='{0}'", strName));
        }

        public static DictItem GetItemById(string id)
        {
            return Environment.NhibernateHelper.GetObjectById<DictItem>(id);
        }

        /// <summary>
        /// 取出所有
        /// </summary>
        /// <returns></returns>
        public static IList<DictItem> GetAll()
        {
            return Environment.NhibernateHelper.GetObjectsByCondition<DictItem>("from DictItem dItem");
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
                //if (dItem.Parent != null)
                //    dItem.Parent.SubItems.Remove(dItem);

                Environment.NhibernateHelper.DeleteObject(dItem);
                Environment.NhibernateHelper.Flush();
                if (dItem.Parent != null)
                    ReLoadItem(dItem.Parent);

                return true;
            }
            catch(Exception exp)
            {
                Environment.Logger.AppendMessage(Define.enumLogType.Error, string.Format("删除字典时出错：{0}", exp.ToString()));
                
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
                Environment.NhibernateHelper.SaveObject(dItem);
                Environment.NhibernateHelper.Flush();

                if (dItem.Parent != null)
                    ReLoadItem(dItem.Parent);

                return true;
            }
            catch(Exception exp)
            {
                Environment.Logger.AppendMessage(Define.enumLogType.Error, string.Format("保存字典时出错：{0}", exp.ToString()));
                
                return false;
            }
        }

        public static void ReLoadItem(DictItem dItem)
        {
            Environment.NhibernateHelper.RefreshObject(dItem, Define.enumLockMode.UpgradeNoWait);
        }



    }
}
