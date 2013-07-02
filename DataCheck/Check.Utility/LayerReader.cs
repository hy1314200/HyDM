using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Check.Define;
using Common.Utility.Data;

namespace Check.Utility
{
    public class LayerReader
    {
        private  static DataTable m_TableLayers;
        private static DataTable TableLayers
        {
            get
            {
                if (m_TableLayers == null)
                    m_TableLayers = GetAllLayers();

                return m_TableLayers;
            }
        }

        /// <summary>
        /// 获取所有图层（DataTable对象）
        /// 开放给知道图层表结构的开发/应用或者配合GetLayerFromDataRow@see::GetLayerFromDataRow方法使用
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllLayers()
        {
            IDbConnection sysConnection= SysDbHelper.GetSysDbConnection();
            return AdoDbHelper.GetDataTable(sysConnection, "select * from LR_DicLayer");
        }

        /// <summary>
        /// 从DataRow生成Layer对象
        /// </summary>
        /// <param name="rowLayer"></param>
        /// <returns></returns>
        public static StandardLayer GetLayerFromDataRow(DataRow rowLayer)
        {
            if (rowLayer == null)
                return null;

            StandardLayer lyr = new StandardLayer();
            lyr.ID = Convert.ToInt32( rowLayer["LayerID"]);
            lyr.Name = rowLayer["LayerCode"] as string;
            lyr.AliasName = rowLayer["LayerName"]as string;
            lyr.AttributeTableName = rowLayer["AttrTableName"] as string;
            lyr.Description = rowLayer["LayerDesc"] as string;
            lyr.OrderIndex = Convert.ToInt32(rowLayer["SeqID"]);
            lyr.Type = (enumLayerType)Convert.ToInt32(rowLayer["GeometryType"]);

            return lyr;
        }

        /// <summary>
        /// 获取指定标准下的图层集合
        /// </summary>
        /// <param name="standardID"></param>
        /// <returns></returns>
        public static List<StandardLayer> GetLayersByStandard(int standardID)
        {
            DataRow[] rowLayers=  TableLayers.Select(string.Format("StandardID={0}", standardID));
            int count = rowLayers.Length;
            List<StandardLayer> lyrList = new List<StandardLayer>(count);
            for (int i = 0; i < count; i++)
            {
                lyrList.Add(GetLayerFromDataRow(rowLayers[i]));
            }

            return lyrList;
        }

        /// <summary>
        /// 根据图层的名称获取别名
        /// 若无此图层，返回名称本身
        /// </summary>
        /// <param name="strAliasName"></param>
        /// <returns></returns>
        public static string GetAliasName(string strName,int standardID)
        {
            DataRow[] rowLayers = TableLayers.Select(string.Format("LayerCode='{0}' and StandardID={1}", strName,standardID));
            if (rowLayers.Length > 0)
                return rowLayers[0]["LayerName"] as string;

            return strName;

        }

        /// <summary>
        /// 根据图层的别名获取名称
        /// 若无此图层别名，返回null
        /// </summary>
        /// <param name="strAliasName"></param>
        /// <returns></returns>
        public static string GetNameByAliasName(string strAliasName,int standardID)
        {
            DataRow[] rowLayers = TableLayers.Select(string.Format("LayerName='{0}' and StandardID={1}", strAliasName,standardID));
            if (rowLayers.Length > 0)
                return rowLayers[0]["LayerCode"] as string;

            return null; ;

        }

        /// <summary>
        /// 获取指定图层名称的图层对象
        /// </summary>
        /// <param name="strAliasName"></param>
        /// <returns></returns>
        public static StandardLayer GetLayerByName(string strName,int standardID)
        {
            DataRow[] rowLayers = TableLayers.Select(string.Format("LayerCode='{0}' and StandardID={1}", strName, standardID));
            if (rowLayers.Length > 0)
                return GetLayerFromDataRow(rowLayers[0]);

            return null;
        }

        public static StandardLayer GetLayerByAliasName(string strAliasName, int standardID)
        {
            DataRow[] rowLayers = TableLayers.Select(string.Format("LayerName='{0}' and StandardID={1}", strAliasName, standardID));
            if (rowLayers.Length > 0)
                return GetLayerFromDataRow(rowLayers[0]);

            return null;
        }
    }
}
