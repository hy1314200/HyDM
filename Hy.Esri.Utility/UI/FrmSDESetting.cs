using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ESRI.ArcGIS.esriSystem;

namespace Hy.Esri.Utility.UI
{
    public partial class FrmSDESetting : DevExpress.XtraEditors.XtraForm
    {
        public FrmSDESetting()
        {
            InitializeComponent();
        }

        public IPropertySet SDEPropertySet
        {
            get
            {
                IPropertySet connectionProps = new PropertySetClass();
                connectionProps.SetProperty("authentication_mode", "DBMS");
                connectionProps.SetProperty("VERSION", "SDE.DEFAULT");
                connectionProps.SetProperty("dbclient", cmbSDEType.Text);
                connectionProps.SetProperty("Server", txtSDEServer.Text);
                connectionProps.SetProperty("Instance", txtSDEDatabase.Text);
                connectionProps.SetProperty("user", txtUserName.Text);
                connectionProps.SetProperty("password", txtPassword.Text);

                return connectionProps;
            }

            set
            {
                IPropertySet propertySet = value;
                if (propertySet == null)
                    return;


            }
        }

        private bool ValidateSetting(ref string errMsg)
        {
            if (cmbSDEType.SelectedIndex < 0)
            {
                errMsg = "请选择服务器类型";
                return false;
            }

            if (string.IsNullOrEmpty(txtSDEServer.Text))
            {
                errMsg = "必须填写服务器路径";
                return false;
            }

            if (string.IsNullOrEmpty(txtSDEDatabase.Text))
            {
                errMsg = "必须填写服务名称";
                return false;
            }

            if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                errMsg = "请填写用户名和密码";
                return false;
            }

            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string strError="";
            if (!ValidateSetting(ref strError))
            {
                XtraMessageBox.Show(strError);
                this.DialogResult = DialogResult.None;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string strError="";
            if (!ValidateSetting(ref strError))
            {
                XtraMessageBox.Show(strError);
                return;
            }

            if (WorkspaceHelper.OpenWorkspace(enumWorkspaceType.SDE, this.SDEPropertySet) == null)
            {
                XtraMessageBox.Show("连接测试失败！");
            }
            else
            {
                XtraMessageBox.Show("连接测试成功！");
            }
        }
    }
}