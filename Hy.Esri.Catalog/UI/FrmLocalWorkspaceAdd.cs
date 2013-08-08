using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Hy.Esri.Catalog.Utility;
using Hy.Esri.Catalog.Define;
using Hy.Esri.Utility;

namespace Hy.Esri.Catalog.UI
{
    public partial class FrmLocalWorkspaceAdd : DevExpress.XtraEditors.XtraForm
    {
        public FrmLocalWorkspaceAdd()
        {
            InitializeComponent();
        }

        public bool Editable
        {
            set
            {
                cmbWorkspaceType.Enabled = value;
                txtWorkspacePath.Enabled = value;
                txtWorkspaceName.Enabled = value;
                txtWorkspaceAlias.Enabled = value;

                cbExistDB.Visible = value;
                btnWorkspaceSelect.Visible = value;
                btnOK.Visible = value;
               
            }
        }

        private void txtWorkspacePath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(!string.IsNullOrEmpty(this.txtWorkspacePath.Text))
                folderWorkspace.SelectedPath=this.txtWorkspacePath.Text;

            if (folderWorkspace.ShowDialog() == DialogResult.OK)
            {
                txtWorkspacePath.Text = folderWorkspace.SelectedPath;
                txtWorkspaceName.Text = "";
            }
        }

        private IWorkspace m_Workspace;
        public IWorkspace Workspace
        {
            get
            {
                return m_Workspace;
            }
        }

        public enumWorkspaceType WorkspaceType
        {
            get { return cmbWorkspaceType.SelectedIndex == 0 ? enumWorkspaceType.PGDB : enumWorkspaceType.FileGDB; }
            set
            {
                cmbWorkspaceType.SelectedIndex = (enumWorkspaceType.PGDB == value ? 0 : 1);
            }
        }
        public string WorkspacePath
        {
            get { return txtWorkspacePath.Text; }
            set
            {
                txtWorkspacePath.Text = value;
            }
        }

        public string WorkspaceName
        {
            get { return txtWorkspaceName.Text+(cmbWorkspaceType.SelectedIndex==0?".mdb":".gdb"); }
            set
            {
                txtWorkspaceName.Text = value;
            }
        }
        public string WorkspaceAlias
        {
         
            get { return string.IsNullOrEmpty(txtWorkspaceAlias.Text) ? txtWorkspaceName.Text : txtWorkspaceAlias.Text; }
            set
            {
                txtWorkspaceAlias.Text = value;
            }
        }

        public bool CreateNew
        {
            get
            {
                return !cbExistDB.Checked;
            }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtWorkspacePath.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请填写数据库存储路径!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtWorkspaceName.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请填写数据库名称!");
                return;
            }

            //enumWorkspaceType wsType = enumWorkspaceType.PGDB;
            //if (cmbWorkspaceType.SelectedIndex == 1)
            //    wsType = enumWorkspaceType.FileGDB;

            //m_Workspace= Hy.Esri.Catalog.Utility.WorkspaceHelper.CreateWorkspace(wsType, txtWorkspacePath.Text, txtWorkspaceName.Text);

            this.DialogResult = DialogResult.OK;
        }


        private void txtWorkspaceName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtWorkspaceName.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("数据库名称不允许为空!");
                e.Cancel = true;
                return;
            }
            string strFullPath = System.IO.Path.Combine(txtWorkspacePath.Text, txtWorkspaceName.Text);
            if ((cmbWorkspaceType.SelectedIndex == 0 && System.IO.File.Exists(strFullPath)) || (cmbWorkspaceType.SelectedIndex == 1 && System.IO.Directory.Exists(strFullPath)))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("指定目录下已存在指定名称的数据库，请更换名称或路径");
                e.Cancel = true;
            }
            else
            {
                if (string.IsNullOrEmpty(txtWorkspaceAlias.Text))
                    txtWorkspaceAlias.Text = txtWorkspaceName.Text;
            }
        }

        private void cmbWorkspaceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtWorkspacePath.Text = "";
            txtWorkspaceName.Text = "";
            txtWorkspaceAlias.Text = "";
        }

        private void cbExistDB_CheckedChanged(object sender, EventArgs e)
        {
            btnWorkspaceSelect.Enabled = cbExistDB.Checked;
            txtWorkspaceName.Enabled= txtWorkspacePath.Enabled = !cbExistDB.Checked;
            
        }

        private void btnWorkspaceSelect_Click(object sender, EventArgs e)
        {
            if (cmbWorkspaceType.SelectedIndex == 0)
            {
                if (dlgWorkspace.ShowDialog() == DialogResult.OK)
                {
                    string strFile = dlgWorkspace.FileName;
                    if (!(new ESRI.ArcGIS.DataSourcesGDB.AccessWorkspaceFactoryClass()).IsWorkspace(strFile))
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("当前所选择的文件不是正确的个人空间数据库文件!");
                        return;
                    }

                    txtWorkspacePath.Text = System.IO.Path.GetDirectoryName(strFile);
                    txtWorkspaceName.Text = System.IO.Path.GetFileNameWithoutExtension(strFile);
                }
            }
            else
            {
                if (folderWorkspace.ShowDialog() == DialogResult.OK)
                {
                    string strFile = folderWorkspace.SelectedPath;
                    if (!(new ESRI.ArcGIS.DataSourcesGDB.FileGDBWorkspaceFactoryClass()).IsWorkspace(strFile))
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("当前所选择的文件不是正确的文件空间数据库文件!");
                        return;
                    }

                    txtWorkspacePath.Text = System.IO.Path.GetDirectoryName(strFile);
                    txtWorkspaceName.Text = System.IO.Path.GetFileNameWithoutExtension(strFile);
                }

            }
        }
    }
}
