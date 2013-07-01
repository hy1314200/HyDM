using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Check.UI.Forms
{
    public partial class FrmShowFeatureStatistic : DevExpress.XtraEditors.XtraForm
    {
        private DataTable xyDt = null;
        //private DataTable zxcqDt = null;

        public string FrmText
        { 
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmShowFeatureStatistic"/> class.
        /// </summary>
        /// <param name="dt">The dt.</param>
        public FrmShowFeatureStatistic(DataTable dt)//,DataTable dt2)
        {
            InitializeComponent();
            xyDt = dt;
            //zxcqDt = dt2;
        }

        private void frmShowFeatureStatistic_Load(object sender, EventArgs e)
        {
            this.grid1.DataSource = null;
            this.gridView1.Columns.Clear();
            this.grid1.RefreshDataSource();

            this.Text = FrmText+"图层要素个数统计";

            this.grid1.DataSource = xyDt;
            this.gridView1.BestFitColumns();
            this.grid1.Refresh();
            this.gridView1.OptionsView.AllowCellMerge = true;
          
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.ValidateNames = true;
            saveFileDialog1.Filter = "Excel文件|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.gridView1.ExportToExcelOld(saveFileDialog1.FileName);
                    XtraMessageBox.Show("导出Excel成功!", "提示");
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("导出Excel失败!", "系统提示");
                }
            }
            saveFileDialog1.Dispose();
        }
    }
}