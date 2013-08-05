using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace Skyline.GuiHua.Bussiness
{
    public  class ExcelHelper
    {
        //public string ExcelFile {private get; set; }

        public static DataTable ReadData(string excelFile)
        {
            if (!System.IO.File.Exists(excelFile))
                return null;

            OleDbConnection excelConnection = new OleDbConnection();
            excelConnection.ConnectionString = string.Format("Provider=Microsoft.Jet.OleDb.4.0;Data Source='{0}';Extended Properties='Excel 8.0;HDR=YES'", excelFile);
            excelConnection.Open();
            DataTable dtSchema = excelConnection.GetSchema("Tables");
            if (dtSchema.Rows.Count == 0)
                return null;

            string strTableName = dtSchema.Rows[0]["Table_Name"] as string;
            string strSQL = string.Format("select * from [{0}]", strTableName);
            OleDbCommand cmdSelect = excelConnection.CreateCommand();
            cmdSelect.CommandText = strSQL;
            OleDbDataAdapter dbAdapter = new OleDbDataAdapter(cmdSelect);
            DataTable dtResult=new DataTable();
            dbAdapter.Fill(dtResult);

            
            dbAdapter.Dispose();
            excelConnection.Close();
            excelConnection.Dispose();

            return dtResult;
        }



    }
}
