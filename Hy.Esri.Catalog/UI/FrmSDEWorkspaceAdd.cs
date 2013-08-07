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
using Hy.Esri.Catalog;
using Hy.Esri.Catalog.Define;
using Hy.Esri.Utility;

namespace Hy.Esri.Catalog.UI
{
    public partial class FrmSDEWorkspaceAdd : DevExpress.XtraEditors.XtraForm
    {
        private bool SAVEorUN = false;
        private bool Success = false;
        public FrmSDEWorkspaceAdd()
        {
            InitializeComponent();
            textEdit6.Properties.ReadOnly = true;
        }

        private void Frm3CreatSdeConn_Load(object sender, EventArgs e)
        {

        }

        private void TestConn_Click(object sender, EventArgs e)
        {
            Success = false;
            try
            {
                IWorkspace Currworkspace = null;
                string IP = textEdit1.Text;
                string Instance = textEdit2.Text;
                string version = textEdit6.Text;
                string database = textEdit3.Text;
                string user = textEdit7.Text;
                string password = textEdit4.Text;
                string ConnCname = textEdit5.Text;
             
                IPropertySet propertyset = new ESRI.ArcGIS.esriSystem.PropertySetClass();
                propertyset.SetProperty("Server", IP);
                propertyset.SetProperty("instance", Instance);
                propertyset.SetProperty("database", database);
                propertyset.SetProperty("user", user);
                propertyset.SetProperty("password", password);
                propertyset.SetProperty("version", version);
                IWorkspaceFactory workspaceFactory = new SdeWorkspaceFactory(); 
                Currworkspace = workspaceFactory.Open(propertyset, 0);
                MessageBox.Show("连接成功!");
                Success = true;
            
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败!");
                Success = false;
                string Error = ex.Message;
            }
           
        }
       
        private void SaveConn_Click(object sender, EventArgs e)
        {          
            if (Success)
            {
                if (textEdit5.Text == "")
                {
                    MessageBox.Show("请填写连接名称！");
                    return;
                }
                m_ConnectionProperty = new WorkspaceInfo();
                m_ConnectionProperty.Name=textEdit5.Text;
                m_ConnectionProperty.Type = enumWorkspaceType.SDE;
                object[] objArgs=
                {
                    textEdit1.Text,textEdit2.Text,textEdit3.Text,textEdit7.Text,textEdit4.Text,textEdit6.Text
                };
                m_ConnectionProperty.Args=string.Format(
                    "Server={0};instance={1};database={2};user={3};password={4};version={5}",objArgs);

                global::Hy.Esri.Catalog.Environment.NhibernateHelper.SaveObject(m_ConnectionProperty);
                global::Hy.Esri.Catalog.Environment.NhibernateHelper.Flush();

                Success = false;
                MessageBox.Show("保存成功！");

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("请先测试连接再保存！");
            }
        }

        private WorkspaceInfo m_ConnectionProperty;
        public WorkspaceInfo ConnectionProperty
        {
            get
            {
                return m_ConnectionProperty;
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}