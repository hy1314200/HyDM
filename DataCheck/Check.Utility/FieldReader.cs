using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Check.Define;

namespace Check.Utility
{
    public class FieldReader
    {

        private static DataTable m_TableFields;
        private static DataTable TableFields
        {
            get
            {
                if (m_TableFields == null)
                    m_TableFields = GetAllFields();

                return m_TableFields;
            }
        }

        public static DataTable GetAllFields()
        {
            IDbConnection sysConnection = SysDbHelper.GetSysDbConnection();
            return Common.Utility.Data.AdoDbHelper.GetDataTable(sysConnection, "select * from LR_DicField");
        }


        /// <summary>
        /// 从DataRow生成Field对象
        /// </summary>
        /// <param name="rowField"></param>
        /// <returns></returns>
        public static StandardField GetFieldFromDataRow(DataRow rowField)
        {
            if (rowField == null)
                return null;

            StandardField lyr = new StandardField();
            lyr.LayerID = Convert.ToInt32(rowField["LayerID"]);
            lyr.Name = rowField["FieldCode"] as string;
            lyr.AliasName = rowField["FieldName"] as string;
            lyr.Length =Convert.ToInt32( rowField["Length"]);
            lyr.Description = rowField["FieldDesc"] as string;
            lyr.OrderIndex = Convert.ToInt32(rowField["FldSeqID"]);
            lyr.Type = Convert.ToInt32(rowField["FieldType"]);

            return lyr;
        }

        /// <summary>
        /// 获取指定标准下的图层集合
        /// </summary>
        /// <param name="lyrID"></param>
        /// <returns></returns>
        public static List<StandardField> GetFieldsByLayer(int lyrID)
        {
            DataRow[] rowFields = TableFields.Select(string.Format("LayerID='{0}'", lyrID));
            int count = rowFields.Length;
            List<StandardField> lyrList = new List<StandardField>(count);
            for (int i = 0; i < count; i++)
            {
                lyrList.Add(GetFieldFromDataRow(rowFields[i]));
            }

            return lyrList;
        }

        /// <summary>
        /// 根据图层的名称获取别名
        /// 若无此图层，返回名称本身
        /// </summary>
        /// <param name="strAliasName"></param>
        /// <returns></returns>
        public static string GetAliasName(string strName, int lyrID)
        {
            DataRow[] rowFields = TableFields.Select(string.Format("FieldCode='{0}' and LayerID='{1}'", strName, lyrID));
            if (rowFields.Length > 0)
                return rowFields[0]["FieldName"] as string;

            return strName;

        }

        /// <summary>
        /// 根据图层的别名获取名称
        /// 若无此别名，返回null
        /// </summary>
        /// <param name="strAliasName"></param>
        /// <returns></returns>
        public static string GetNameByAliasName(string strAliasName, int lyrID)
        {
            DataRow[] rowFields = TableFields.Select(string.Format("FieldName='{0}' and LayerID='{1}'", strAliasName, lyrID));
            if (rowFields.Length > 0)
                return rowFields[0]["FieldCode"] as string;

            return null;

        }

        /// <summary>
        /// 获取指定图层名称的图层对象
        /// </summary>
        /// <param name="strAliasName"></param>
        /// <returns></returns>
        public static StandardField GetFieldByName(string strName, int lyrID)
        {
            DataRow[] rowFields = TableFields.Select(string.Format("FieldCode='{0}' and LayerID='{1}'", strName, lyrID));
            if (rowFields.Length > 0)
                return GetFieldFromDataRow(rowFields[0]);

            return null;
        }

        /// <summary>
        /// 获取指定图层名称的图层对象
        /// </summary>
        /// <param name="strAliasName"></param>
        /// <returns></returns>
        public static StandardField GetFieldByAliasName(string strAliasName, int lyrID)
        {
            DataRow[] rowFields = TableFields.Select(string.Format("FieldName='{0}' and LayerID='{1}'", strAliasName, lyrID));
            if (rowFields.Length > 0)
                return GetFieldFromDataRow(rowFields[0]);

            return null;
        }
    }
}
