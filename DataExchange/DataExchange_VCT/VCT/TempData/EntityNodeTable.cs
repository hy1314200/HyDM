using System.Data.OleDb;
using System.Data;
using System;
using DIST.DGP.DataExchange.VCT.FileData;

namespace DIST.DGP.DataExchange.VCT.TempData 
{
    public class EntityNodeTable : BaseTable
    {
        public string FieldName_EntityID = "EntityID";
        public string FieldName_FeatureCode = "FeatureCode";
        public string FieldName_Representation = "Representation";

        public EntityNodeTable(OleDbConnection pOleDbConnection, string strTableName, bool isCreateTable, bool isFirst)
            : base(pOleDbConnection, strTableName, isCreateTable, isFirst)
        {

        }

        public void AddRow(EntityNode entityNode)
        {
            DataRow dataRow = CreateRow(entityNode);
            m_pDataTable.Rows.Add(dataRow);
        }

        protected virtual DataRow CreateRow(EntityNode entityNode)
        {
            if (m_pDataTable == null)
            {
                GetTableForAdd();
            }
            if (m_pDataTable != null)
            {
                DataRow dataRow = m_pDataTable.NewRow();

                dataRow[FieldName_EntityID] = entityNode.EntityID;
                dataRow[FieldName_FeatureCode] = entityNode.FeatureCode;
                dataRow[FieldName_Representation] = entityNode.Representation;
                
                return dataRow;
            }
            return null;
        }

        public virtual void GetEntityNodeByDataRow(DataRow dataRow, ref EntityNode entityNode)
        {
            if (dataRow != null)
            {
                entityNode.EntityID = dataRow[FieldName_EntityID] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow[FieldName_EntityID]);
                entityNode.FeatureCode = dataRow[FieldName_FeatureCode] == System.DBNull.Value ? "" : dataRow[FieldName_FeatureCode].ToString();
                entityNode.Representation = dataRow[FieldName_Representation] == System.DBNull.Value ? "" : dataRow[FieldName_Representation].ToString();
            }
        }
    }
}
