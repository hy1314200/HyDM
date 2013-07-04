using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace Utility
{
    public class ExcelHelper
    {
        public ExcelHelper(string excelFile)
        {
            this.m_FileName = excelFile;
        }

        private string m_FileName;
        private OleDbConnection m_Connection;
        private IList<string> m_SheetNameList;

        /// <summary>
        /// 获取错误信息
        /// </summary>
        public string ErrorMessage { private set; get; }

        /// <summary>
        /// 打开
        /// </summary>
        private void Open()
        {
            if (m_Connection == null)
            {
                m_Connection = new OleDbConnection();
                m_Connection.ConnectionString = string.Format("Provider=Microsoft.Jet.OleDb.4.0;Data Source='{0}';Extended Properties='Excel 8.0;HDR=YES'", m_FileName);
                m_Connection.Open();
            }
            else if (m_Connection.State == ConnectionState.Closed)
            {
                m_Connection.Open();
            }

        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void Refresh()
        {
            Release();
            m_Connection = null;
            m_SheetNameList = null;
        }

        /// <summary>
        /// 获取所有表名
        /// </summary>
        /// <returns></returns>
        IList<string> GetSheetNameList()
        {
            if (m_SheetNameList == null)
            {
                DataTable dtSchema = m_Connection.GetSchema("Tables");
                m_SheetNameList = new List<string>();
                foreach (DataRow rowSheet in dtSchema.Rows)
                {
                    m_SheetNameList.Add(rowSheet["Table_Name"] as string);
                }
            }
            return m_SheetNameList;
        }

        /// <summary>
        /// 将指定Excel表读为Reader
        /// </summary>
        /// <param name="sheetName">null为默认读第一个</param>
        /// <returns></returns>
        public IDataReader ReadToReader(string sheetName)
        {
            try
            {
                sheetName = VerifySheetName(sheetName);
                return GetCommand(sheetName).ExecuteReader();
            }
            catch (Exception exp)
            {
                this.ErrorMessage = exp.Message;
                return null;
            }
        }

        private string VerifySheetName(string sheetName)
        {
            GetSheetNameList();
            if (string.IsNullOrEmpty(sheetName))
            {
                if (this.m_SheetNameList.Count > 0)
                    return this.m_SheetNameList[0];
                else
                    throw new Exception("当前Excel中没有表");
            }
            if (!this.m_SheetNameList.Contains(sheetName))
            {
                throw new Exception(string.Format("当前Excel中不存在表[{0}]", sheetName));
            }

            return sheetName;
        }
        private OleDbCommand GetCommand(string sheetName)
        {
            sheetName = VerifySheetName(sheetName);

            string strSQL = string.Format("select * from [{0}]", sheetName);
            OleDbCommand cmdSelect = m_Connection.CreateCommand();
            cmdSelect.CommandText = strSQL;

            return cmdSelect;
        }

        /// <summary>
        /// 将指定Excel表读为Reader
        /// </summary>
        /// <param name="sheetName">null为默认读第一个</param>
        /// <returns></returns>
        public DataTable ReadToDataTable(string sheetName)
        {
            try
            {
                sheetName = VerifySheetName(sheetName);

                OleDbDataAdapter dbAdapter = new OleDbDataAdapter(GetCommand(sheetName));
                DataTable dtResult = new DataTable();
                dbAdapter.Fill(dtResult);

                dbAdapter.Dispose();
                return dtResult;
            }
            catch (Exception exp)
            {
                this.ErrorMessage = exp.Message;
                return null;
            }
        }

        public void Release()
        {
            if (this.m_Connection != null)
            {
                if (this.m_Connection.State == ConnectionState.Open)
                    this.m_Connection.Close();

                this.m_Connection.Dispose();
            }
            this.m_Connection = null;
            this.m_SheetNameList = null;
        }


    }
}
