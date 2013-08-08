using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesGDB;


using Hy.Esri.Catalog.Utility;
using Hy.Esri.Catalog.Define;
using Hy.Esri.Utility;

namespace Hy.Esri.Catalog.UI
{
    public partial class FrmWorkspaceProperty : DevExpress.XtraEditors.XtraForm
    {
        public FrmWorkspaceProperty()
        {
            InitializeComponent();
        }

        public string WorkspaceName
        {
            set
            {
                txtName.Text = value;
            }
        }

        private IPropertySet m_WorkspaceProperty;
        public IPropertySet WorkspaceProperty
        {
            set
            {
                m_WorkspaceProperty = value;
                if (m_WorkspaceProperty == null)
                    return;

                txtServer.Text = m_WorkspaceProperty.GetProperty("Server") as string;
                txtInstance.Text = m_WorkspaceProperty.GetProperty("instance") as string;
                txtVersion.Text = m_WorkspaceProperty.GetProperty("version") as string;
                txtDatabase.Text = m_WorkspaceProperty.GetProperty("database") as string;
                txtUser.Text = m_WorkspaceProperty.GetProperty("user") as string;
                txtPassword.Text = m_WorkspaceProperty.GetProperty("password") as string;
            }
        }
        //private enumWorkspaceType m_WorkspaceType;
        //public enumWorkspaceType WorkspaceType
        //{
        //    set
        //    {
        //        m_WorkspaceType = value;
        //    }
        //}


        private void TestConn_Click(object sender, EventArgs e)
        {
            IWorkspace wsTest = Hy.Esri.Utility.WorkspaceHelper.OpenWorkspace(enumWorkspaceType.SDE, this.m_WorkspaceProperty);
            if (wsTest != null)
            {
                XtraMessageBox.Show("连接成功!");
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wsTest);
            }
            else
            {
                XtraMessageBox.Show("连接失败，此连接可能已经失效!");
            }

        }
    
    }
}