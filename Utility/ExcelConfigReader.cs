using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Utility
{
    public class ExcelConfigReader
    {
        public string FileName { private get; set; }
        
        private string m_CurrentKey;
        private string m_CurrentValue;

        ExcelHelper m_ExcelHelper = null;
        public void Reset()
        {
            m_CurrentKey = null;
            m_CurrentValue = null;
            OpenReader();
        }
        private bool OpenReader()
        {
            if (m_DataReader == null)
            {
                m_ExcelHelper = new ExcelHelper(this.FileName);
                this.m_DataReader= m_ExcelHelper.ReadToReader(null);
                if (this.m_DataReader == null)
                {
                    this.ErrorMessage = m_ExcelHelper.ErrorMessage;
                    return false;
                }
            }
            return true;
        }

        public string ErrorMessage { private set; get; }

        private IList<string> m_ColumnNames;
        public IList<string> GetColumnNames()
        {
            if (m_ColumnNames == null)
            {
                if (!OpenReader())
                    return null;

                m_ColumnNames = new List<string>();
                DataTable dtSchema = this.m_DataReader.GetSchemaTable();
                foreach (DataColumn dCol in dtSchema.Columns)
                {
                    m_ColumnNames.Add(dCol.ColumnName);
                }
            }
            return m_ColumnNames;
        }

        public DataTable ReadToDataTable()
        {
            return ReadToDataTable(null);
        }

        public DataTable ReadToDataTable(string sheetName)
        {
            m_ExcelHelper = new ExcelHelper(this.FileName);
            return m_ExcelHelper.ReadToDataTable(sheetName);
        }

        private IDataReader m_DataReader;
    }
}
