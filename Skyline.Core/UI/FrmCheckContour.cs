using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Skyline.Core.UI
{
    public partial class FrmCheckContour : Form
    {
        private List<string> mContourList;

        public string Random = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContourList"></param>
        public FrmCheckContour(List<string> ContourList)
        {
            InitializeComponent();
            mContourList = new List<string>();
            mContourList = ContourList;
        }

        /// <summary>
        /// 将ShapeFile类型的等高线，加载到下拉列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCheckContour_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < mContourList.Count; i++)
            {
                this.comboBoxEdit1.Properties.Items.Add(mContourList[i].ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.comboBoxEdit1.Text=="")
            {
                MessageBox.Show("请先选择需要检查等高线！","Sunz",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                this.Random = "";
                return;
            }
            this.Random = this.comboBoxEdit1.Text.Substring(7, 6);
            this.Hide();
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }
    }
}
