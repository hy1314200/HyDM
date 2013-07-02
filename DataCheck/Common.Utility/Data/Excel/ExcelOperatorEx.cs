using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Common.Utility.Data.Excel
{
    /// <summary>
    /// 导出excel操作类支持2007
    /// </summary>
    public class ExcelatorEx:Excelator
    {
        private Dictionary<string,DataTable> m_DataTables = null;

        private DataTable m_DataTable = null;

        public bool ExportExcel(Dictionary<string,DataTable> tables)
        {
            try
            {
                //if()
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
            return true;
        }

        public bool bAutoPagination = true;

        /// <summary>
        /// Exports the excel.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public bool ExportExcel(DataTable table)
        {
            try
            {
                if (table == null || table.Rows.Count == 0)
                {
                    return false;
                }
                //创建要导出的excel文件;
                base.CreateExcel();
                //得到要导出的sheet页数；
                int iSheetCount = base.GetSheetCount(table.Rows.Count);
                int istartRowNum = 0;
                for (int i = 1; i <= iSheetCount; i++)
                {
                    string sheetName = "sheet" + i.ToString();
                    //创建sheet;
                    base.CreateWorkSheet(sheetName);
                    //使用sheet；
                    base.ActivateSheet(sheetName);
                    //写入表内容
                    base.WriteData(table, istartRowNum, 1, null, true, bAutoPagination);
                    istartRowNum += MAX_SHEET_ROWS_COUNT;
                }
                //如果有多个sheet页，在保存时，将sheet1页做为首页
                if (iSheetCount > 1)
                {
                    base.SheetSort();
                    base.ActivateSheet("sheet1");
                }
                base.SaveAs();
                //KillExcelPProcess();
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                base.ReleaseObjects();
                //KillExcelPProcess();
                return false;
            }
            finally
            {
                GC.Collect();
            }
            return true;
        }

        private string[,] GetColumns(DataColumnCollection columnCollection)
        {
            string[,] titles = new string[1, columnCollection.Count];
            int i = 0;
            foreach(DataColumn col in columnCollection)
            {
                titles[0, i] = col.Caption;
                i++;
            }
            return titles;
        }
    }
}
