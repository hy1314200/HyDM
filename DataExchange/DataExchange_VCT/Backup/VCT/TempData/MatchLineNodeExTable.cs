using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace DIST.DGP.DataExchange.VCT.TempData
{
    public class MatchLineNodeExTable : BaseTable
    {
        public string FieldName_Index1 = "Index1";
        public string FieldName_Index2 = "Index2";
        public string FieldName_Reverse1 = "Reverse1";
        public string FieldName_Reverse2 = "Reverse2";
        //public string FieldName_EntityID = "EntityID";

        public MatchLineNodeExTable(OleDbConnection pOleDbConnection, bool isCreateTable, bool bIsFirst)
            : base(pOleDbConnection, "MatchLineNodeEx", isCreateTable, bIsFirst)
        {

        }

        public override bool CreateTable()
        {
            if (m_pOleDbConnection != null)
            {
                try
                {
                    base.CreateTable();

                    string strCommand = "Create Table " + TableName_TempTable + "("
                        + FieldName_Index1 + " int,"
                        + FieldName_Index2 + " int,"
                        + FieldName_Reverse1 + " int,"
                        + FieldName_Reverse2 + " int)";
                        //+ FieldName_EntityID + " int primary key)";

                    OleDbCommand oleDbCommand = new OleDbCommand(strCommand, m_pOleDbConnection);
                    oleDbCommand.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    LogAPI.WriteErrorLog(ex);
                }
            }
            return false;
        }

        public void AddRow(int nIndex1, int nIndex2, int nReverse1, int nReverse2/*,int nEntityID*/)
        {
            DataRow dataRow = CreateRow(nIndex1, nIndex2, nReverse1, nReverse2/*, nEntityID*/);
            m_pDataTable.Rows.Add(dataRow);
        }

        protected virtual DataRow CreateRow(int nIndex1, int nIndex2, int nReverse1, int nReverse2/*, int nEntityID*/)
        {
            if (m_pDataTable == null)
            {
                GetTableForAdd();
            }
            if (m_pDataTable != null)
            {
                DataRow dataRow = m_pDataTable.NewRow();

                dataRow[FieldName_Index1] = nIndex1;
                dataRow[FieldName_Index2] = nIndex2;
                dataRow[FieldName_Reverse1] = nReverse1;
                dataRow[FieldName_Reverse2] = nReverse2;
                //dataRow[FieldName_EntityID] = nEntityID;

                return dataRow;
            }
            return null;
        }

        //public virtual void GetEntityNodeByDataRow(DataRow dataRow, ref EntityNode entityNode)
        //{
        //    if (dataRow != null)
        //    {
        //        entityNode.EntityID = dataRow[FieldName_EntityID] == null ? -1 : Convert.ToInt32(dataRow[FieldName_EntityID]);
        //        entityNode.FeatureCode = dataRow[FieldName_FeatureCode] == null ? "" : dataRow[FieldName_FeatureCode].ToString();
        //        entityNode.Representation = dataRow[FieldName_Representation] == null ? "" : dataRow[FieldName_Representation].ToString();
        //    }
        //}
    }
}
