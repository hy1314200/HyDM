using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Skyline.Core.UI
{
    public partial class FrmAddModelShape : DevExpress.XtraEditors.XtraForm
    {
        private string[] Fileds;
        public bool PathType = true;//相对路径
        public string Filed = "";
        public FrmAddModelShape()
        {
            InitializeComponent();
        }
        public string[] GetFiledName
        {

            get { return Fileds; }
            set { Fileds = value; }
        
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.comboBoxEdit1.SelectedIndex==0) 
            {
                PathType = true;
            }
            else
            {
                PathType = false;
            }
            Filed = comboBoxEdit2.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Hide();
        }

        private void FrmAddModelShape_Load(object sender, EventArgs e)
        {
            foreach (string item in Fileds)
            {
                comboBoxEdit2.Properties.Items.Add(item);
            }
            if (comboBoxEdit2.Properties.Items.Count!=0)
            {
                comboBoxEdit2.SelectedIndex = 0;
            }
           
        }
    }
}