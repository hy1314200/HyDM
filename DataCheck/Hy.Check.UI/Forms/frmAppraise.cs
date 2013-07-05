using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Hy.Check.Utility;
using Common.Utility.Data.Excel;
namespace Hy.Check.UI.Forms
{
    public partial class FrmAppraise : XtraForm
    {
        private StateAppraise _stateApp;

        public FrmAppraise()
        {
            InitializeComponent();
        }

        public StateAppraise StateApp
        {
            get
            {
                if (_stateApp == null)
                {
                    _stateApp = new StateAppraise();
                }
                return _stateApp;
            }
        }

        /// <summary>
        /// 初始化统计图
        /// </summary>
        /// <param name="useKind"></param>
        private void InitChart(int useKind)
        {
            Series series1 = new Series();

            DataTable dtResult = null;
            if (useKind == 0)
            {
                dtResult = StateApp.BuilderRuleStyleTable();
            }
            else
            {
                dtResult = StateApp.BuilderLayerStyleTable();
            }

            ReportListView.DataSource = null;
            gridViewMain.Columns.Clear();

            DataRow pRow = dtResult.NewRow();
            if (useKind == 0)
                pRow["检查类型"] = "总计";
            else
                pRow["图层"] = "总计";
            pRow["严重缺陷"] = dtResult.Compute("sum(严重缺陷)", "");
            pRow["重缺陷"] = dtResult.Compute("sum(重缺陷)", "");
            pRow["轻缺陷"] = dtResult.Compute("sum(轻缺陷)", "");

            if (pRow["严重缺陷"] is DBNull || pRow["重缺陷"] is DBNull || pRow["轻缺陷"] is DBNull)
            {
                btnExport.Enabled = dtResult.Rows.Count > 0;
                ReportListView.DataSource = dtResult;
                return;
            }
            double mark = StateApp.GetResultMark(dtResult);
            if (mark < 50)
                labelControlMark.Text = "得分低于50分";
            else
                labelControlMark.Text = "得分:" + mark;

            pRow["错误合计"] = Convert.ToInt32(pRow["严重缺陷"]) + Convert.ToInt32(pRow["重缺陷"]) + Convert.ToInt32(pRow["轻缺陷"]);


            int nErrorCount = Convert.ToInt32(pRow["错误合计"]);
            if (useKind == 0)
            {
                foreach (DataRow dr in dtResult.Rows)
                {
                    SeriesPoint pPoint = new SeriesPoint();
                    long lcount = Convert.ToInt32(dr["错误合计"]);

                    if (lcount > 0)
                    {
                        double temp = (lcount*100.0)/nErrorCount;

                        pPoint.Argument = dr["检查类型"] + ":" + temp.ToString("f1") + "%";
                        pPoint.Values = new double[] {lcount};

                        series1.Points.Add(pPoint);
                    }
                }
            }
            else
            {
                foreach (DataRow dr in dtResult.Rows)
                {
                    SeriesPoint pPoint = new SeriesPoint();
                    long lcount = Convert.ToInt32(dr["错误合计"]);
                    if (lcount > 0)
                    {
                        double temp = (lcount*100.0)/nErrorCount;

                        pPoint.Argument = dr["图层"] + ":" + temp.ToString("f1") + "%";
                        pPoint.Values = new double[] {lcount};
                        series1.Points.Add(pPoint);
                    }
                }
            }

            series1.ChangeView(ViewType.Pie);
            series1.PointOptions.ValueNumericOptions.Format = NumericFormat.FixedPoint;

            series1.PointOptions.ValueNumericOptions.Precision = 2;
            series1.PointOptions.PointView = PointView.Argument;


            PieSeriesLabel label = series1.Label as PieSeriesLabel;
            label.Position = PieSeriesLabelPosition.Outside;
            label.TextColor = Color.Empty;


            series1.ShowInLegend = false;

            //OverlappingOptions options = series1.Label.OverlappingOptions;
            //options.ResolveOverlapping = true;

            //chartControl1.Series.Clear();
            //chartControl1.Series.Add(series1);

            dtResult.Rows.Add(pRow);

            ReportListView.DataSource = dtResult;
            btnExport.Enabled = dtResult.Rows.Count > 0;
        }


        /// <summary>
        /// 选择改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGroupOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitChart(radioGroupOption.SelectedIndex);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmAppraise_Load(object sender, EventArgs e)
        {
            //XGifProgress progressbar = new XGifProgress();
            //progressbar.ShowHint("正在统计结果...");
            Cursor = Cursors.WaitCursor;
            InitChart(0);
            xtraTabControl1.SelectedTabPageIndex = 0;
            Cursor = Cursors.Default;
            //progressbar.Hide();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.RestoreDirectory = true;
            Application.DoEvents();
            try
            {
                string strFilter = "Excel文件(*.xls)|*.xls";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                {
                    ExportXls();
                }
                else
                {
                    strFilter = "图片文件(*.jpeg)|*.jpeg";
                    saveFileDialog1.Filter = strFilter;
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        try
                        {
                            chartControl1.ExportToImage(saveFileDialog1.FileName, ImageFormat.Jpeg);
                            XtraMessageBox.Show("导出图片成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show("导出图片失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        saveFileDialog1.FileName = "";
                        saveFileDialog1.Dispose();
                    }
                }
            }
            catch
            {

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                btnExport.Text = "导出为Excel";
            }
            else
            {
                btnExport.Text = "导出为图片(*.jpg)";
            }
        }

        private void ExportXls()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            Application.DoEvents();
            try
            {
                // 2012-08-10 张航宇
                // 修改填写后缀名与选择的类型不一致时保存的类型以后缀名为准的bug
                saveFileDialog1.Title = "保存结果到excel文件......";
                saveFileDialog1.Filter = "Excel97-2003文件(*.xls)|*.xls|Excel2007文件(*.xlsx)|*.xlsx";   
                string[] extensions={"xls","xlsx"};
                saveFileDialog1.FileName = "错误结果";
                saveFileDialog1.ValidateNames = true;
                saveFileDialog1.AddExtension = true;
                saveFileDialog1.OverwritePrompt = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    int filterIndex = saveFileDialog1.FilterIndex - 1;
                    if (!System.IO.Path.GetExtension(saveFileDialog1.FileName).Equals(extensions[filterIndex], StringComparison.OrdinalIgnoreCase))
                    {
                        saveFileDialog1.FileName = saveFileDialog1.FileName + "." + extensions[filterIndex];
                    }
                    this.Cursor = Cursors.WaitCursor;
                    ExcelatorEx excelOper = new ExcelatorEx();
                    excelOper.bAutoPagination = false;
                    excelOper.MyFileName = saveFileDialog1.FileName;
                    //pProgress.ShowHint(string.Format("正在输出'{0}'文件，可能需要较长时间，请稍候......", saveFileDialog1.FileName));
                    //pProgress.ShowGifProgress(null);

                    if (excelOper.ExportExcel(ReportListView.DataSource as DataTable))
                    {
                        //pProgress.Hide();
                        this.Cursor = Cursors.Default;
                        if (XtraMessageBox.Show(string.Format("保存{0}文件成功！是否打开？", saveFileDialog1.FileName), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            try
                            {
                                //string strFileName = ExcelatorEx.IsExcel2007Ver() == true
                                //                         ? saveFileDialog1.FileName + "x"
                                //                         : saveFileDialog1.FileName;
                                System.Diagnostics.Process.Start(saveFileDialog1.FileName);
                            }
                            catch (Exception ex)
                            {
                                this.Cursor = Cursors.Default;
                                XtraMessageBox.Show(string.Format("打开文件失败！错误原因:{0}", ex.Message), "警告",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        //pProgress.Hide();
                        XtraMessageBox.Show(string.Format("保存{0}文件失败！", saveFileDialog1.FileName), "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                //pProgress.Hide();
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(string.Format("保存{0}文件失败！" + ex.Message, saveFileDialog1.FileName), "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //pProgress.Hide();
            saveFileDialog1.Dispose();
        }
    }
}