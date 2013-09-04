using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hy.Esri.DataManage.Standard.Helper;
using Hy.Esri.DataManage.Standard;


namespace Hy.Esri.DataManage.UI
{
    public partial class FrmStandardImport : DevExpress.XtraEditors.XtraForm
    {
        public FrmStandardImport()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (m_WsSource != null && !string.IsNullOrWhiteSpace(txtName.Text))
            {
                Importer importer = new Importer();
                importer.Source = this.m_WsSource;
                importer.StandardName = txtName.Text.Trim();
                importer.OnMessage += ShowMessage;

                StandardItem sItem= importer.Import();
                StandardHelper.SaveStandard(sItem);

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                XtraMessageBox.Show("请正确填写参数!");
            }

        }

        private void ShowMessage(string strMsg)
        {
            lblStatus.Text = strMsg;
            Application.DoEvents();
        }


        ESRI.ArcGIS.Geodatabase.IWorkspace m_WsSource;
        private void ChooseSource(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (dlgSource.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    m_WsSource = (new ESRI.ArcGIS.DataSourcesGDB.AccessWorkspaceFactoryClass()).OpenFromFile(dlgSource.FileName, 0);
                    txtSource.Text = dlgSource.FileName;
                }
                catch
                {
                    XtraMessageBox.Show("不是正确的PGDB数据库，请重选");
                    ChooseSource(sender, e);
                }
            }
        }
    }
}