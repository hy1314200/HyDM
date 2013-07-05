using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hy.Metadata
{
    public class MetaStandardHelper
    {
        public MetaStandardHelper(MetaStandard metaStandard)
        {
        }

        public static MetaStandard GetStandardByName(string strName)
        {
            IList<MetaStandard> standardList = Environment.NhibernateHelper.GetObjectsByCondition<MetaStandard>(string.Format("from MetaStandard mStandard where mStandard.Name='{0}'", strName));
            if (standardList == null || standardList.Count == 0)
                return null;

            return standardList[0];
        }

        public static MetaStandard GetStandardById(string id)
        {
            return  Environment.NhibernateHelper.GetObjectById<MetaStandard>(id);          
        }

        public static IList<MetaStandard> GetAll()
        {
            return Environment.NhibernateHelper.GetAll<MetaStandard>();
        }

        public static string ErrorMessage { get; private set; }

        /// <summary>
        /// 删除字典项
        /// </summary>
        /// <param name="standard"></param>
        /// <returns></returns>
        public static bool DeleteStandard(MetaStandard standard)
        {
            try
            {
                // 记录
                Environment.NhibernateHelper.DeleteObject(standard);
                Environment.NhibernateHelper.Flush();

                // 删除数据表
                if (Environment.AdodbHelper.TableExists(standard.TableName))
                {
                    if (Environment.AdodbHelper.ExecuteSQL(string.Format("Drop table {0}", standard.TableName))<1)
                    {
                        ErrorMessage = "标准已删除，但未删除数据";
                    }
                }
               
                return true;
            }
            catch (Exception exp)
            {
                Environment.Logger.AppendMessage(Define.enumLogType.Error, string.Format("删除元数据标准时出错：{0}", exp.ToString()));

                return false;
            }
        }

        /// <summary>
        /// 保存字典项
        /// </summary>
        /// <param name="standard"></param>
        /// <returns></returns>
        public static bool SaveStandard(MetaStandard standard)
        {
            try
            {
                // 记录
                Environment.NhibernateHelper.SaveObject(standard);
                Environment.NhibernateHelper.Flush();

                // 创建表
                if (Environment.AdodbHelper.TableExists(standard.TableName))
                {
                    Environment.AdodbHelper.ExecuteSQL(string.Format("Drop table {0}", standard.TableName));

                }

                return true;
            }
            catch (Exception exp)
            {
                Environment.Logger.AppendMessage(Define.enumLogType.Error, string.Format("保存元数据标准时出错：{0}", exp.ToString()));

                return false;
            }
        }

        public static void ReLoadItem(MetaStandard standard)
        {
            Environment.NhibernateHelper.RefreshObject(standard, Define.enumLockMode.UpgradeNoWait);
        }

        public static DataTable GetMetaData(MetaStandard standard)
        {
            Environment.AdodbHelper.ExecuteDataTable(
        }
    }
}
