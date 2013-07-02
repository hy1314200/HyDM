using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace Common.Utility.Data.Excel
{
    public class Excelator
    {
        //作者: 日期:2009年2月3日11:15:16 描述:修改变量命名，满足规范
        private string m_sFileName;
        public Microsoft.Office.Interop.Excel.Application m_pExcelApp = null;
        public Workbook m_pWorkBook = null;
        private System.Object Nothing = System.Reflection.Missing.Value;
        public static readonly int MAX_SHEET_ROWS_COUNT = 65535;

        /// <summary>
        /// 描述:增加列标识列表对象
        /// 作者:董兰芳
        /// 创建日期:2009年2月3日13:12:59
        /// </summary>
        private List<string> m_pColFlagList = null;

        private const int C_OFFICE_2003_VER = 11;

        /// <summary>
        /// 检查安装的版本是不是2003或以下版
        /// </summary>
        private bool m_bOffice2003Ver = true;

        /// <summary>
        /// excel文件名.
        /// 2012-08-10 张航宇
        /// 要求FileName必须包括后缀名
        /// </summary>
        /// <value>The name of my file.</value>
        public string MyFileName
        {
            get
            {
                return m_sFileName;
            }
            set
            {
                if (Path.GetExtension(value).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    m_bOffice2003Ver = false;
                }
                else
                {
                    m_bOffice2003Ver = true;
                }
                m_sFileName = value;
            }
        }

        /// <summary>
        /// 描述:修改方法名，由Save改为SaveAs，另存文件
        /// 作者:张怡鹏
        /// 创建日期:2009.11.5
        /// </summary>
        /// <returns></returns>
        public bool SaveAs()
        {
            try
            {
                // 2012-08-10 张航宇
                // 要求FileName包括后缀名，此处不将后缀名去掉

                //string strName =m_sFileName.Substring(0,m_sFileName.LastIndexOf(('.')));

                XlFileFormat xlFileFormat = new XlFileFormat();
                xlFileFormat = m_bOffice2003Ver == true ? XlFileFormat.xlExcel8 : XlFileFormat.xlWorkbookDefault;

                m_pWorkBook.SaveAs(m_sFileName, xlFileFormat, Nothing, Nothing, Nothing, Nothing, XlSaveAsAccessMode.xlExclusive, Nothing, Nothing, Nothing, Nothing, Nothing);
                m_pWorkBook.Close(false, Nothing, Nothing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pWorkBook);
                m_pExcelApp.Quit();
                m_pWorkBook = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pExcelApp);
                m_pExcelApp = null;
                GC.Collect();
                return true;
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                if (m_pWorkBook != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pWorkBook);
                }
                if (m_pExcelApp != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pExcelApp);
                }
                m_pWorkBook = null;
                m_pExcelApp = null;
                return false;
            }
        }

        public bool QuitExcel()
        {
            try
            {
                if (m_pWorkBook != null)
                {
                    m_pWorkBook.Close(true, Nothing, Nothing);
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pWorkBook);
                m_pWorkBook = null;
                m_pExcelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pExcelApp);
                m_pExcelApp = null;
                GC.Collect();
                return true;
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                if (m_pWorkBook != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pWorkBook);
                }
                if (m_pExcelApp != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pExcelApp);
                }
                m_pWorkBook = null;
                m_pExcelApp = null;
                return false;
            }
        }

        /// <summary>
        /// 描述:释放内存，关闭进程
        /// 作者:董兰芳
        /// 创建日期:2009年2月3日14:41:30
        /// </summary>
        /// <returns></returns>
        public bool ReleaseObjects()
        {
            if (m_pWorkBook != null)
            {
                m_pWorkBook.Close(false, Nothing, Nothing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pWorkBook);
                m_pWorkBook = null;
            }
            if (m_pExcelApp != null)
            {
                m_pExcelApp.Quit();
                int generation = System.GC.GetGeneration(m_pExcelApp);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pExcelApp);
                m_pExcelApp = null;
                GC.Collect(generation);
            }
            return true;
        }

        public static void KillExcelPProcess()
        {
            Process[] pProcess= Process.GetProcessesByName("EXCEL"); //关闭excel进程
            if(pProcess==null)
            {
                return;
            }
            for (int i = 0; i < pProcess.Length;i++)
                pProcess[i].Kill();
        }

        /// <summary>
        /// 构造函数，不创建Excel工作薄
        /// </summary>
        public Excelator()
        {
            m_pColFlagList = GetColumnFlags();
            //检查office的版本号
            m_bOffice2003Ver = FindOfficeVer(C_OFFICE_2003_VER);
        }

        /// <summary>
        /// 创建Excel工作薄
        /// </summary>
        public void CreateExcel()
        {
            m_pExcelApp = new Microsoft.Office.Interop.Excel.Application();
            m_pExcelApp.DisplayAlerts = false;
            m_pExcelApp.AlertBeforeOverwriting = false;
            //myWorkBook = myExcel.Application.Workbooks.Add(true);
            m_pWorkBook = m_pExcelApp.Application.Workbooks.Add(true);
            ////查询office的版本，如果为2003 版，禁用后台错误检查
            //if (m_bOffice2003Ver)
            //{
                //设置不允许后台错误检查
                m_pExcelApp.ErrorCheckingOptions.BackgroundChecking = false;
            //}
        }

        /// <summary>
        /// 显示Excel
        /// </summary>
        public void ShowExcel()
        {
            m_pExcelApp.Visible = true;
        }

        /// <summary>
        /// 将数据写入Excel
        /// </summary>
        /// <param name="data">要写入的二维数组数据</param>
        /// <param name="startRow">Excel中的起始行</param>
        /// <param name="startColumn">Excel中的起始列</param>
        public void WriteData(string[,] data, int startRow, int startColumn)
        {
            int rowNumber = data.GetLength(0);
            int columnNumber = data.GetLength(1);

            for (int i = 0; i < rowNumber; i++)
            {
                for (int j = 0; j < columnNumber; j++)
                {
                    //在Excel中，如果某单元格以单引号“'”开头，表示该单元格为纯文本，因此，我们在每个单元格前面加单引号。 
                    m_pExcelApp.Cells[startRow + i, startColumn + j] = "'" + data[i, j];
                }
            }
        }

        /// <summary>
        /// 将数据写入Excel
        /// </summary>
        /// <param name="sData">要写入的字符串</param>
        /// <param name="iRow">写入的行</param>
        /// <param name="iColumn">写入的列</param>
        public void WriteData(string sData, int iRow, int iColumn)
        {
            m_pWorkBook.Save();
            m_pExcelApp.Cells[iRow, iColumn] = sData;
        }

        /// <summary>
        /// 将数据写入Excel
        /// </summary>
        /// <param name="data">要写入的数据表</param>
        /// <param name="startRow">Excel中的起始行</param>
        /// <param name="startColumn">Excel中的起始列</param>
        public void WriteData(System.Data.DataTable data, int startRow, int startColumn)
        {
            for (int i = 0; i <= data.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= data.Columns.Count - 1; j++)
                {
                    //在Excel中，如果某单元格以单引号“'”开头，表示该单元格为纯文本，因此，我们在每个单元格前面加单引号。 
                    m_pExcelApp.Cells[startRow + i, startColumn + j] = "'" + data.Rows[i][j].ToString();
                    //myExcel.Cells[startRow + i, startColumn + j] =  data.Rows[i][j].ToString();
                }
            }
        }

        //作者:董兰芳 日期:2009年2月3日11:05:13 描述:优化导出速度及内存问题
        /// <summary>
        /// 将数据表中的数据导入Excel
        /// </summary>
        /// <param name="pDataTable">要写入的数据表</param>
        /// <param name="iStartRow">Excel中的起始行</param>
        /// <param name="iStartColumn">Excel中的起始列</param>
        /// <param name="iTextColumnIndexes">指示文本类型的数据列索引的数组</param>
        /// <param name="bCaption">是否包含列名</param>
        /// <param name="bAutoPagination">是否自动分页</param>
        public void WriteData(System.Data.DataTable pDataTable, int iStartRow, int iStartColumn, int[] iTextColumnIndexes, 
                                          bool bCaption, bool bAutoPagination)
        {
            //获得当前激活页面
            Worksheet pWorksheet = m_pExcelApp.ActiveSheet as Worksheet;
            if (pWorksheet == null)
            {
                return;
            }
            if (pDataTable == null || pDataTable.Columns.Count == 0 || pDataTable.Rows.Count == 0)
            {
                return;
            }
            int iPagination = bAutoPagination == true ? MAX_SHEET_ROWS_COUNT : 99999999;
            Range pRange = null;
            try
            {
                //将数据表的列名写入Excel
                string sLastColFlag = m_pColFlagList[iStartColumn + pDataTable.Columns.Count - 2];
                string sFirstColFlag = m_pColFlagList[iStartColumn - 1];
                int iColumn = iStartColumn;
                int iRow = 1;
                if (bCaption)
                {
                    foreach (System.Data.DataColumn pColumn in pDataTable.Columns)
                    {
                        pWorksheet.Cells[iRow, iColumn++] = pColumn.Caption;
                    }
                    //iRow++;
                    //iColumn = iStartColumn;
                    //居中对齐
                    //Range pTempRange = pWorksheet.get_Range(sFirstColFlag + iRow.ToString(), sLastColFlag + iRow.ToString());
                    //pTempRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    //pTempRange.VerticalAlignment = XlHAlign.xlHAlignCenter;
                    //Marshal.ReleaseComObject(pTempRange);
                }
                int iRowCount = bAutoPagination == true ? MAX_SHEET_ROWS_COUNT : pDataTable.Rows.Count;
                iRowCount = pDataTable.Rows.Count < MAX_SHEET_ROWS_COUNT ? pDataTable.Rows.Count : MAX_SHEET_ROWS_COUNT;
                int iColCount = pDataTable.Columns.Count;
                //先将每一行数据放入一个数组,然后再放入清单
                string[,] objData = new string[iRowCount, iColCount];
                int tempi = 0;
                //加入分页控制
                for (int i = 0; i < iRowCount; i++)
                {
                    if (tempi < iPagination && iStartRow < pDataTable.Rows.Count)
                    {
                        for (int j = 0; j < iColCount; j++)
                        {
                            if (pDataTable.Rows[iStartRow][j] != null && pDataTable.Rows[iStartRow][j] != DBNull.Value)
                            {
                                if (iTextColumnIndexes != null && Array.IndexOf(iTextColumnIndexes, j + 1) != -1)
                                {
                                    objData[i, j] = "'" + pDataTable.Rows[iStartRow][j].ToString();
                                }
                                else
                                {
                                    objData[i, j] = pDataTable.Rows[iStartRow][j].ToString();
                                }
                            }
                            else
                            {
                                objData[i, j] = "";
                            }
                        }
                        iStartRow++;
                        tempi++;
                    }
                }
                //批量写入数据
                iRow++;
                pRange=pWorksheet.get_Range(sFirstColFlag + iRow.ToString(), sLastColFlag + iRow.ToString());
                pRange = pRange.get_Resize(objData.GetUpperBound(0) + 1, objData.GetUpperBound(1) + 1);
                //居中对齐
                //pRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                //pRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                pRange.Value2 = objData;
                //单元格自适应宽度
                pWorksheet.Columns.EntireColumn.AutoFit();
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

            }
            finally
            {
                if (pRange!=null)
                {
                    Marshal.ReleaseComObject(pRange);
                    pRange = null;
                }
                Marshal.ReleaseComObject(pWorksheet);
                pWorksheet = null;
            }
        }

        /// <summary>
        /// 读取指定单元格数据
        /// </summary>
        /// <param name="row">行序号</param>
        /// <param name="column">列序号</param>
        /// <returns>该格的数据</returns>
        public string ReadData(int row, int column)
        {
            Range range = m_pExcelApp.get_Range(m_pExcelApp.Cells[row, column], m_pExcelApp.Cells[row, column]);
            return range.Text.ToString();
        }

        /// <summary>
        /// 向Excel中插入图片
        /// </summary>
        /// <param name="pictureName">图片的绝对路径加文件名</param>
        public void InsertPictures(string pictureName)
        {
            Worksheet worksheet = (Worksheet)m_pExcelApp.ActiveSheet;
            //后面的数字表示位置，位置默认
            worksheet.Shapes.AddPicture(pictureName, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, 10, 10, 150, 150);
        }

        /// <summary>
        /// 向Excel中插入图片
        /// </summary>
        /// <param name="pictureName">图片的绝对路径加文件名</param>
        /// <param name="left">左边距</param>
        /// <param name="top">右边距</param>
        /// <param name="width">宽</param>
        /// <param name="heigth">高</param>
        public void InsertPictures(string pictureName, int left, int top, int width, int heigth)
        {
            Worksheet worksheet = (Worksheet)m_pExcelApp.ActiveSheet;
            worksheet.Shapes.AddPicture(pictureName, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, top, left, heigth, width);
        }

        /// <summary>
        /// 重命名工作表
        /// </summary>
        /// <param name="sheetNum">工作表序号，从左到右，从1开始</param>
        /// <param name="newSheetName">新的工作表名</param>
        public void ReNameSheet(int sheetNum, string newSheetName)
        {
            Worksheet worksheet = (Worksheet)m_pExcelApp.Worksheets[sheetNum];
            worksheet.Name = newSheetName;
        }

        /// <summary>
        /// 重命名工作表
        /// </summary>
        /// <param name="oldSheetName">原有工作表名</param>
        /// <param name="newSheetName">新的工作表名</param>
        public void ReNameSheet(string oldSheetName, string newSheetName)
        {
            Worksheet worksheet = (Worksheet)m_pExcelApp.Worksheets[oldSheetName];
            worksheet.Name = newSheetName;
        }

        /// <summary>
        /// 新建工作表
        /// </summary>
        /// <param name="sheetName">工作表名</param>
        public void CreateWorkSheet(string sheetName)
        {
            Worksheet newWorksheet = null;
            for (int i = 1; i <=m_pWorkBook.Sheets.Count; i++)
            {
                newWorksheet = m_pWorkBook.Sheets[i] as Worksheet;
                //newWorksheet.Activate();
                if (newWorksheet.Name.Equals(sheetName, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }
            newWorksheet = (Worksheet)m_pWorkBook.Worksheets.Add(Nothing, Nothing, Nothing, Nothing);
            newWorksheet.Name = sheetName;
        }

        /// <summary>
        /// 激活工作表
        /// </summary>
        /// <param name="sheetName">工作表名</param>
        public void ActivateSheet(string sheetName)
        {
            Worksheet worksheet = (Worksheet)m_pExcelApp.Worksheets[sheetName];
            worksheet.Activate();
        }

        public void MoveSheetToAfter(string sheetName)
        {
            Worksheet worksheet = (Worksheet) m_pExcelApp.ActiveSheet;
            object next = m_pExcelApp.Worksheets[sheetName];
            worksheet.Move(Missing.Value, next);
        }

        public void MoveSheetToBefore(string sheetName)
        {
            Worksheet worksheet = (Worksheet) m_pExcelApp.ActiveSheet;

            object before = m_pExcelApp.Worksheets[sheetName];
            worksheet.Move(before,Missing.Value);
        }

        public void SheetSort()
        {
            Worksheet worksheet = null;
            for (int i = 1; i <m_pWorkBook.Sheets.Count; i++)
            {
                worksheet = m_pWorkBook.Sheets[i] as Worksheet;
                if(i==1)
                {
                    worksheet.Move(Missing.Value, m_pWorkBook.Sheets[m_pWorkBook.Sheets.Count]);
                }
                else
                {
                    worksheet.Move(m_pWorkBook.Sheets[m_pWorkBook.Sheets.Count - i + 1], Missing.Value);
                }
            }
        }

        /// <summary>
        /// 激活工作表
        /// </summary>
        /// <param name="sheetNum">工作表序号</param>
        public void ActivateSheet(int sheetNum)
        {
            Worksheet worksheet = (Worksheet)m_pExcelApp.Worksheets[sheetNum];
            worksheet.Activate();
        }

        /// <summary>
        /// 最适合列宽
        /// </summary>
        public void BestFitColumnWidth()
        {
            try
            {
                Worksheet pWorkSheet = m_pExcelApp.ActiveSheet as Worksheet;
                if (pWorkSheet != null)
                {
                    //pWorkSheet.Columns.EntireColumn.AutoFit();
                    //如下设置功能只是针对分析功能出的EXCEL结果值，其列数不超过6列
                    Range pRange = pWorkSheet.get_Range(pWorkSheet.Cells[2, 1], pWorkSheet.Cells[2, 6]);
                    pRange.ColumnWidth = 24;
                }

            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return;
            }
        }

        /// <summary>
        /// 最适合列宽
        /// </summary>
        public void BestFitColumnWidth2()
        {
            try
            {
                Worksheet pWorkSheet = m_pExcelApp.ActiveSheet as Worksheet;
                if (pWorkSheet != null)
                {
                    pWorkSheet.Columns.EntireColumn.AutoFit();
                }
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return;
            }
        }

        /// <summary>
        /// 删除一个工作表
        /// </summary>
        /// <param name="iSheetNum">删除的工作表名</param>
        public void DeleteSheet(int iSheetNum)
        {
            ((Worksheet)m_pWorkBook.Worksheets[iSheetNum]).Delete();
        }

        /// <summary>
        /// 删除一个工作表
        /// </summary>
        /// <param name="iSheetName">删除的工作表序号</param>
        public void DeleteSheet(string iSheetName)
        {
            ((Worksheet)m_pWorkBook.Worksheets[iSheetName]).Delete();
        }

        /// <summary>
        /// 删除一整列
        /// </summary>
        /// <param name="iSheetName"></param>
        /// <param name="colNum"></param>
        public void DeleteColumn(string iSheetName, object colNum)
        {
            Range pRange = ((Worksheet)m_pWorkBook.Worksheets[iSheetName]).Cells[colNum, colNum] as Range;
            pRange.Select();
            pRange.EntireColumn.Delete(XlDirection.xlToLeft);
            ((Worksheet)m_pWorkBook.Worksheets[iSheetName]).UsedRange.Columns.Delete(colNum);
        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        public void CellsUnite(int startRow, int startColumn, int endRow, int endColumn)
        {
            CellsUnite(startRow, startColumn, endRow, endColumn, false);
        }

        /// <summary>
        /// 描述:合并单元格，默认居中对齐
        /// 作者:董兰芳
        /// 创建日期:2009年2月4日10:29:17
        /// 描述：去除合并提示页面，改善合并效果
        /// 作者：张怡鹏
        /// 修改日期：2009.11.17
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="startColumn"></param>
        /// <param name="endRow"></param>
        /// <param name="endColumn"></param>
        /// <param name="bCenterAlign"></param>
        /// <returns></returns>
        public bool CellsUnite(int startRow, int startColumn, int endRow, int endColumn, bool bCenterAlign)
        {
            Range pRange = m_pExcelApp.get_Range(m_pExcelApp.Cells[startRow, startColumn], m_pExcelApp.Cells[endRow, endColumn]);

            m_pExcelApp.DisplayAlerts = false; //关闭提示！
            pRange.MergeCells = true;

            if (bCenterAlign)
            {
                pRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                pRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                pRange.WrapText = false;
                pRange.IndentLevel = false;
            }
            Marshal.ReleaseComObject(pRange);
            return true;
        }

        public bool CellsMargeing(object startCell, object endCell, bool bCenterAlign)
        {
            Range pRange = m_pExcelApp.get_Range(startCell, endCell);
            //pRange.MergeCells = false;
            pRange.Select();
            //pRange.ClearContents();
            pRange.MergeCells = true;
            m_pExcelApp.DisplayAlerts = false; //关闭提示！
            if (bCenterAlign)
            {
                pRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                pRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                pRange.WrapText = false;
                pRange.IndentLevel = 0;
                pRange.Orientation = 0;
                pRange.AddIndent = false;
                pRange.ShrinkToFit = false;
            }
            pRange.Merge(true);
            Marshal.ReleaseComObject(pRange);
            return true;
        }

        /// <summary>
        /// 合并单元格并对合并后的单元格进行赋值
        /// </summary>
        /// <param name="startCell"></param>
        /// <param name="endCell"></param>
        /// <param name="bCenterAlign"></param>
        /// <param name="rangeValue">单元格值</param>
        /// <returns></returns>
        public bool CellsMargeing(object startCell, object endCell, bool bCenterAlign, string rangeValue)
        {
            Range pRange = m_pExcelApp.get_Range(startCell, endCell);
            pRange.MergeCells = false;
            pRange.Select();
            pRange.ClearContents();
            pRange.MergeCells = true;
            m_pExcelApp.DisplayAlerts = false; //关闭提示！
            if (bCenterAlign)
            {
                pRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                pRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                pRange.WrapText = false;
                pRange.IndentLevel = 0;
                pRange.Orientation = 0;
                pRange.AddIndent = false;
                pRange.ShrinkToFit = false;
            }

            //pRange.Merge(true);
            pRange.Value2 = rangeValue;
            Marshal.ReleaseComObject(pRange);
            return true;
        }
        
        /// <summary>
        /// 单元格文字对齐方式
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="hAlign">水平对齐</param>
        /// <param name="vAlign">垂直对齐</param>
        public void CellsAlignment(int startRow, int startColumn, int endRow, int endColumn, Microsoft.Office.Interop.Excel.XlHAlign hAlign, Microsoft.Office.Interop.Excel.XlVAlign vAlign)
        {
            Range range = m_pExcelApp.get_Range(m_pExcelApp.Cells[startRow, startColumn], m_pExcelApp.Cells[endRow, endColumn]);
            range.HorizontalAlignment = hAlign;
            range.VerticalAlignment = vAlign;
            //作者:董兰芳 日期:2009年2月4日17:17:06 描述:
            Marshal.ReleaseComObject(range);
        }

        /// <summary>
        /// 绘制指定单元格的边框
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        public void CellsDrawFrame(int startRow, int startColumn, int endRow, int endColumn)
        {
            //CellsDrawFrame(startRow, startColumn, endRow, endColumn,
            // true, true, true, true, true, true, false, false,
            // LineStyle.连续直线, BorderWeight.细, ColorIndex.自动);

            CellsDrawFrame(startRow, startColumn, endRow, endColumn,
            true, true, true, true, true, true, false, false, XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
        }

        /// <summary>
        /// 绘制指定单元格的边框
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="isDrawTop">是否画上外框</param>
        /// <param name="isDrawBottom">是否画下外框</param>
        /// <param name="isDrawLeft">是否画左外框</param>
        /// <param name="isDrawRight">是否画右外框</param>
        /// <param name="isDrawHInside">是否画水平内框</param>
        /// <param name="isDrawVInside">是否画垂直内框</param>
        /// <param name="isDrawDiagonalDown">是否画斜向下线</param>
        /// <param name="isDrawDiagonalUp">是否画斜向上线</param>
        /// <param name="lineStyle">线类型</param>
        /// <param name="borderWeight">线粗细</param>
        /// <param name="color">线颜色</param>
        public void CellsDrawFrame(int startRow, int startColumn, int endRow, int endColumn, bool isDrawTop, bool isDrawBottom, bool isDrawLeft,
            bool isDrawRight, bool isDrawHInside, bool isDrawVInside, bool isDrawDiagonalDown, bool isDrawDiagonalUp, Microsoft.Office.Interop.Excel.XlLineStyle lineStyle,
            Microsoft.Office.Interop.Excel.XlBorderWeight borderWeight, Microsoft.Office.Interop.Excel.XlColorIndex color)
        {
            //获取画边框的单元格
            Range range = m_pExcelApp.get_Range(m_pExcelApp.Cells[startRow, startColumn], m_pExcelApp.Cells[endRow, endColumn]);

            //清除所有边框

            //range.Borders[XlBordersIndex.xlEdgeTop].LineStyle = LineStyle.无;
            //range.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = LineStyle.无;
            //range.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = LineStyle.无;
            //range.Borders[XlBordersIndex.xlEdgeRight].LineStyle = LineStyle.无;
            //range.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = LineStyle.无;
            //range.Borders[XlBordersIndex.xlInsideVertical].LineStyle = LineStyle.无;
            //range.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = LineStyle.无;
            //range.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = LineStyle.无;

            range.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlLineStyleNone;
            range.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlLineStyleNone;
            range.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlLineStyleNone;
            range.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlLineStyleNone;
            range.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlLineStyleNone;
            range.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlLineStyleNone;
            range.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
            range.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;

            //以下是按参数画边框 
            if (isDrawTop)
            {
                range.Borders[XlBordersIndex.xlEdgeTop].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlEdgeTop].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlEdgeTop].ColorIndex = color;
            }

            if (isDrawBottom)
            {
                range.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlEdgeBottom].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlEdgeBottom].ColorIndex = color;
            }

            if (isDrawLeft)
            {
                range.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlEdgeLeft].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlEdgeLeft].ColorIndex = color;
            }

            if (isDrawRight)
            {
                range.Borders[XlBordersIndex.xlEdgeRight].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlEdgeRight].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlEdgeRight].ColorIndex = color;
            }

            if (isDrawVInside)
            {
                range.Borders[XlBordersIndex.xlInsideVertical].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlInsideVertical].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlInsideVertical].ColorIndex = color;
            }

            if (isDrawHInside)
            {
                range.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlInsideHorizontal].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlInsideHorizontal].ColorIndex = color;
            }

            if (isDrawDiagonalDown)
            {
                range.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlDiagonalDown].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlDiagonalDown].ColorIndex = color;
            }

            if (isDrawDiagonalUp)
            {
                range.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlDiagonalUp].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlDiagonalUp].ColorIndex = color;
            }
            Marshal.ReleaseComObject(range);
        }

        /// <summary>
        /// 描述:用于获得对应列的列标识，如A、B、AN等。
        /// 作者:董兰芳
        /// 创建日期:2009年2月3日11:34:04
        /// </summary>
        /// <returns></returns>
        public List<string> GetColumnFlags()
        {
            //26个英文字母组成的数组
            string[] sLetters = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R",
                "S", "T", "U", "V", "W", "X", "Y", "Z" };
            List<string> pFlagList = new List<string>();
            pFlagList.AddRange(sLetters);
            foreach (string sLetter1 in sLetters)
            {
                foreach (string sLetter2 in sLetters)
                {
                    pFlagList.Add(sLetter1 + sLetter2);
                }
            }
            return pFlagList;
        }

        /// <summary>
        /// 描述：设置单元格字体
        /// 作者：张怡鹏
        /// 创建日期：2009.11.05
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="startColumn"></param>
        /// <param name="endRow"></param>
        /// <param name="endColumn"></param>
        /// <param name="strFontName"></param>
        /// <param name="iSize"></param>
        public void SetCellFont(int startRow, int startColumn, int endRow, int endColumn, string strFontName, int iSize)
        {
            Range pRange = m_pExcelApp.get_Range(m_pExcelApp.Cells[startRow, startColumn], m_pExcelApp.Cells[endRow, endColumn]);
            try
            {
                if (pRange == null)
                    return;
                pRange.Select();
                pRange.Font.Name = strFontName;
                pRange.Font.Size = iSize;
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return;
            }
            return;
        }

        /// <summary>
        /// 描述：拷贝模板文件
        /// 作者：张怡鹏
        /// 创建日期：2009.11.05
        /// </summary>
        /// <param name="strTemplateFilePath">模板路径</param>
        /// <returns></returns>
        public string CopyTemplateFile(string strTemplateFilePath)
        {
            //检查模板文件是否存在
            if (!System.IO.File.Exists(strTemplateFilePath))
            {
                return null;
            }
            //拷贝文件
            else
            {
                System.IO.File.Copy(strTemplateFilePath, m_sFileName);
                return m_sFileName;
            }
        }

        /// <summary>
        /// 描述：打开Excel工作薄
        /// 作者：张怡鹏
        /// 创建日期：2009.11.05
        /// </summary>
        public virtual void OpenExcel()
        {
            m_pExcelApp = new Microsoft.Office.Interop.Excel.Application();
            m_pWorkBook = m_pExcelApp.Application.Workbooks.Open(m_sFileName, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing);
            m_pExcelApp.DisplayAlerts = false;
            m_pExcelApp.AlertBeforeOverwriting = false;
            //查询office的版本，如果为2003 版，禁用后台错误检查
            if (m_bOffice2003Ver)
            {
                //设置不允许后台错误检查
                m_pExcelApp.ErrorCheckingOptions.BackgroundChecking = false;
            }
        }

        /// <summary>
        /// 描述：保存文件
        /// 作者：张怡鹏
        /// 创建日期：2009.11.05
        /// </summary>
        /// <returns></returns>
        public virtual bool Save()
        {
            try
            {
                //m_pExcelApp.SaveWorkspace(m_sFileName);
                m_pWorkBook.Save();
                m_pExcelApp.Save(m_sFileName);
                m_pExcelApp.SaveWorkspace(m_sFileName);
                //m_pWorkBook.Close(true, Nothing, Nothing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pWorkBook);
                m_pExcelApp.DisplayAlerts = false;
                m_pExcelApp.AlertBeforeOverwriting = false;  
                m_pWorkBook = null;
                m_pExcelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pExcelApp);
                m_pExcelApp = null;
                GC.Collect();
                return true;
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                if (m_pWorkBook != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pWorkBook);
                }
                if (m_pExcelApp != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pExcelApp);
                }
                m_pWorkBook = null;
                m_pExcelApp = null;
                GC.Collect();
                return false;
            }
        }

        /// <summary>
        /// 描述：按条件合并所选区域单元格
        /// 作者：张怡鹏
        /// 创建日期：2009.11.05
        /// </summary>
        /// <param name="indexColumn">条件列</param>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        public void CellsMergeBaseIndex(int indexColumn, int startRow, int startColumn, int endRow, int endColumn)
        {
            string preKey = "";
            string nextKey = "";
            bool m_CellsMergeSuccess = false;

            try
            {
                for (int i = startRow; i < endRow; i++)
                {
                    if (m_CellsMergeSuccess == false)
                    {
                        preKey = ReadData(i, indexColumn);
                        nextKey = ReadData(i + 1, indexColumn);
                    }
                    else
                    {
                        nextKey = ReadData(i + 1, indexColumn);
                    }

                    int result = string.Compare(preKey, nextKey);

                    if (result == 0)
                    {
                        for (int j = startColumn; j <= endColumn; j++)
                        {
                            CellsUnite(i, j, i + 1, j);
                            m_CellsMergeSuccess = true;
                        }
                    }
                    else
                    {
                        m_CellsMergeSuccess = false;
                    }

                }
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());
            }
        }


        /// <summary>
        /// 指定位置插入一行
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="sheetName"></param>
        public void InsertRow(int Row, string sheetName)
        {
            Worksheet worksheet = (Worksheet)m_pExcelApp.Worksheets[sheetName];
            worksheet.Activate();
            Range range = (Range)worksheet.Rows[Row, Missing.Value];
            range.Insert(XlInsertShiftDirection.xlShiftDown, Missing.Value);

        }

        /// <summary>
        ///获取sheet页得分页数.
        /// </summary>
        /// <param name="RowsCount">The rows count.</param>
        /// <returns></returns>
        public int GetSheetCount(int RowsCount)
        {
            int sheetCount = 1;
            if (RowsCount < MAX_SHEET_ROWS_COUNT)
            {
                return sheetCount;
            }
            int Remainder = RowsCount % MAX_SHEET_ROWS_COUNT;

            double dou = RowsCount / MAX_SHEET_ROWS_COUNT;
            if (Remainder>0)
            {
                return Convert.ToInt32(Math.Floor(dou)) + 1;
            }
            else
            {
                return Convert.ToInt32(Math.Floor(dou));
            }
        }

        /// <summary>
        ///查找office的版本号
        /// </summary>
        /// <param name="iVer"></param>
        /// <returns></returns>
        public static  bool FindOfficeVer(int iVer)
        {
            try
            {
                string str = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("Excel.Application\\CurVer").GetValue("").ToString();
                str = str.Substring(str.LastIndexOf('.') + 1, str.Length - str.LastIndexOf('.') - 1);
                int i = Convert.ToInt32(str);
                if (i == iVer)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
        }
        public static bool IsExcel2007Ver()
        {
            try
            {
                string str = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("Excel.Application\\CurVer").GetValue("").ToString();
                str = str.Substring(str.LastIndexOf('.') + 1, str.Length - str.LastIndexOf('.') - 1);
                int i = Convert.ToInt32(str);
                if (i == C_OFFICE_2003_VER)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());
                return false;
            }
        }
    }
}
