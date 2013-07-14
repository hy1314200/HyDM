using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.Catalog.Define
{
    public class TableCatalogItem:CatalogItem
    {
        //public TableCatalogItem(IDataset dsTable, ICatalogItem parent)
        //    : base(dsTable, parent)
        //{
        //    if(!(dsTable is ITable))
        //        throw new Exception("内部错误：TableCatalogItem构造参数必须为线性属性表");
        //}
        public TableCatalogItem(IDatasetName dsName, ICatalogItem parent)
            : base(dsName, parent)
        {
            if(!(dsName is ITableName))
                throw new Exception("内部错误：TableCatalogItem构造参数必须为线性属性表");
        }

        public override List<ICatalogItem> Childrens
        {
            get { return null; }
        }

        public override bool HasChild
        {
            get
            {
                return false;
            }
        }

        public override enumCatalogType Type
        {
            get
            {
                return enumCatalogType.Table;
            }
        }

        public override string GetGpString()
        {
            return Utility.WorkspaceHelper.GetGpString(m_DatasetName.WorkspaceName,  null , m_DatasetName.Name);
        }
    }
}
