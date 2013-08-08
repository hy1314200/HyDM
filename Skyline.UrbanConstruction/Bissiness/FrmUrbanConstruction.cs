using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Define;

namespace Skyline.UrbanConstruction.Bussiness
{
    public partial class FrmUrbanConstruction : DevExpress.XtraEditors.XtraForm
    {
        public FrmUrbanConstruction()
        {
            InitializeComponent();
        }
        IAdodbHelper dbHelper = Environment.AdodbHelper;

        DataRow GetFirstRow(string strTable)
        {
            DataSet ds = dbHelper.ExecuteDataset("select * from "+strTable);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0];
                }
            }

            return null;
        }

        DataTable GetAllRows(string strTable)
        {
            DataSet ds = dbHelper.ExecuteDataset("select * from " + strTable);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }

            return null;
        }

        private DataTable m_DtArchs;
        private DataTable m_DtFiles;

        private void Baund(DataTable dt, ComboBoxEdit cmb,string stField)
        {
            cmb.Properties.Items.Clear();

            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmb.Properties.Items.Add(dt.Rows[i][stField]);
                }

                if (cmb.Properties.Items.Count > 0)
                    cmb.SelectedIndex = 0;
            }
        }

        public void SetData()
        {
            DisplayProxy.Display(GetFirstRow("城建项目表"), ucSolution1);
            DisplayProxy.Display(GetFirstRow("工程档案表"), ucProject1);
            DisplayProxy.Display(GetFirstRow("单体工程档案表"), ucSingleProjectBase1);
            DisplayProxy.Display(GetFirstRow("规划房屋建筑档案表"), ucSingleProjectBuilding1);
            //DisplayProxy.Display(GetFirstRow("案卷表"), ucArchives1);
            //DisplayProxy.Display(GetFirstRow("非电子化文件表"), ucFile1);

            Baund(m_DtArchs=GetAllRows("案卷表"), cmbAch, "案卷题名");
            Baund(m_DtFiles=GetAllRows("非电子化文件表"), cmbFileList, "文件题名");
        }

        private void cmbAch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAch.SelectedIndex < 0)
                return;

            DisplayProxy.Display(m_DtArchs.Rows[cmbAch.SelectedIndex], ucArchives1);
        }

        private void cmbFileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFileList.SelectedIndex < 0)
                return;

            DisplayProxy.Display(m_DtFiles.Rows[cmbFileList.SelectedIndex], ucFile1);

        }


    }
}