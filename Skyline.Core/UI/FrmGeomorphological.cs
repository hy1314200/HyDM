using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geoprocessing;
using System.IO;

namespace Skyline.Core.UI
{
    public partial class FrmGeomorphological : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// GP工具
        /// </summary>
        private Geoprocessor gp;

        public FrmGeomorphological()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            // 2013-04-10 张航宇 
            // 添加验证            
            if (string.IsNullOrWhiteSpace(buttonEdit1.Text))
            {
                MessageBox.Show("请选择原DEM路径！");
                return;
            }
            if (string.IsNullOrWhiteSpace(buttonEdit2.Text))
            {
                MessageBox.Show("请选择山脊线生成路径！");
                return;
            }

            if (string.IsNullOrWhiteSpace(buttonEdit3.Text))
            {
                MessageBox.Show("请选择山谷线生成路径！");
                return;
            }

            //1-定义GeoProcessor对象
            this.gp = new Geoprocessor();
            object sev = null;
            //2-设置参数
            gp.OverwriteOutput = true;
            //3-设置工具箱所在的路径
            gp.AddToolbox(Application.StartupPath + @"\Convert\TerrainTool.tbx");
            //4-设置输入参数
            ESRI.ArcGIS.esriSystem.IVariantArray parameters = new VarArrayClass();
            parameters.Add(buttonEdit1.Text);
            parameters.Add(buttonEdit2.Text+"\\shanji");
            parameters.Add(buttonEdit3.Text+"\\shangu");
            parameters.Add(Convert.ToDouble(spinEdit1.Value));
            IGeoProcessorResult results = null;
            try
            {
                results = (IGeoProcessorResult)gp.Execute("Terrain", parameters, null);
                if (results == null)
                {
                    MessageBox.Show("生成失败！");
                }
                MessageBox.Show("生成成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成失败！");

                //throw;
            }


        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmGeomorphological_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(@"D:\TTT"))
            {
                Directory.CreateDirectory(@"D:\TTT");
            }
            else
            {
                try
                {
                    Directory.Delete(@"D:\TTT",true);
                    Directory.CreateDirectory(@"D:\TTT");
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
        }

        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog()==DialogResult.OK)
            {
                this.buttonEdit1.Text = this.folderBrowserDialog1.SelectedPath;
            }
            
        }

        private void buttonEdit2_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.buttonEdit2.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void buttonEdit3_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.buttonEdit3.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

       
    }
}