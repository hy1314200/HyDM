using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Hy.Esri.Catalog.Utility;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using Hy.Esri.Catalog.Define;

namespace Hy.Esri.Catalog.UI
{
    public partial class Frm3DFilesImport : DevExpress.XtraEditors.XtraForm
    {
        public Frm3DFilesImport()
        {
            InitializeComponent();
        }

        private void txt3DFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt3DFile.Text))
                dlg3DFile.FileName = txt3DFile.Text;

            if (dlg3DFile.ShowDialog() != DialogResult.OK)
                return;

            txt3DFile.Text = dlg3DFile.FileName;            
        }

      

        private enumWorkspaceType m_SelectedWorkspaceType;
        private string m_SpatialReferenceString;
        private ISpatialReference m_SpatailReference;
        private IWorkspace m_Workspace;
        private IPropertySet m_SDEPropertySet;

        private void cmbWorkspaceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtWorkspace.Text = "";
            m_Workspace = null;
            m_SelectedWorkspaceType = (enumWorkspaceType)cmbWorkspaceType.SelectedIndex;

            if (m_SelectedWorkspaceType == enumWorkspaceType.File)
            {
                cmbDataset.Enabled = false;
            }
            else
            {
                cmbDataset.Enabled = true;

            }
        }

        private void txtWorkspace_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            m_Workspace = null;
            switch (m_SelectedWorkspaceType)
            {
                case enumWorkspaceType.SDE:
                    FrmSDESetting frmSetting = new FrmSDESetting();
                    if (m_SDEPropertySet != null)
                        frmSetting.SDEPropertySet = m_SDEPropertySet;

                    if (frmSetting.ShowDialog(this) != DialogResult.OK)
                        return;

                    m_SDEPropertySet = frmSetting.SDEPropertySet;
                    txtWorkspace.Text = Utility.WorkspaceHelper.PropertySetToString(m_SDEPropertySet);
                    m_Workspace = Utility.WorkspaceHelper.OpenWorkspace(enumWorkspaceType.SDE, m_SDEPropertySet);

                    break;

                case enumWorkspaceType.FileGDB:
                    if (folderBrowserWorkspace.ShowDialog(this) != DialogResult.OK)
                        return;

                    txtWorkspace.Text = folderBrowserWorkspace.SelectedPath;
                    m_Workspace = Utility.WorkspaceHelper.OpenWorkspace(enumWorkspaceType.FileGDB, folderBrowserWorkspace.SelectedPath);

                    break;

                case enumWorkspaceType.PGDB:
                    dlgWorkspace.Filter = "PGDB |*.mdb";
                    if (dlgWorkspace.ShowDialog(this) != DialogResult.OK)
                        return;

                    txtWorkspace.Text = dlgWorkspace.FileName;
                    m_Workspace = Utility.WorkspaceHelper.OpenWorkspace(enumWorkspaceType.PGDB, dlgWorkspace.FileName);

                    break;

                case enumWorkspaceType.File:
                    if (folderBrowserWorkspace.ShowDialog(this) != DialogResult.OK)
                        return;

                    txtWorkspace.Text = folderBrowserWorkspace.SelectedPath;
                    m_Workspace = Utility.WorkspaceHelper.OpenWorkspace(enumWorkspaceType.File, folderBrowserWorkspace.SelectedPath);

                    break;
            }

            // 判断
            if (m_Workspace == null)
            {
                XtraMessageBox.Show("非正确的数据库！");
                return;
            }

            // 加载Dataset 
            cmbDataset.Properties.Items.Clear();
            cmbDataset.Properties.Items.Add("");
            IEnumDatasetName enDatasetName = m_Workspace.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
            IDatasetName dsName = enDatasetName.Next();
            while (dsName != null)
            {
                cmbDataset.Properties.Items.Add(dsName.Name);

                //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(dsName);
                dsName = enDatasetName.Next();
            }
            //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(dsName);
            //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(enDatasetName);
        }

        private void txtSpatailRef_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            m_SpatailReference = null;
            m_SpatialReferenceString = null;

            dlgWorkspace.Filter = "空间参考文件（*.Prj）|*.prj";
            if (dlgWorkspace.ShowDialog(this) == DialogResult.OK)
            {
                string[] strSpatialRef = System.IO.File.ReadAllLines(dlgWorkspace.FileName);
                if (strSpatialRef.Length > 0)
                    m_SpatialReferenceString = strSpatialRef[0];

                m_SpatailReference = SpatialReferenctHelper.CreateSpatialReference(dlgWorkspace.FileName);
                txtSpatailRef.Text = m_SpatailReference.Name;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string strErrMsg=null;
            if (!ValidateSetting(ref strErrMsg))
            {
                XtraMessageBox.Show(strErrMsg);
                return;
            }

            //string strOutput=txtWorkspace.Text;
            //if (m_SelectedWorkspaceType == enumWorkspaceType.SDE)
            //{
            //    strOutput = Utility.SDEHelper.GetGpString(m_Workspace, cmbDataset.Text, txtFeatureClassName.Text);
            //}
            //else
            //{
            //    if(!string.IsNullOrEmpty(cmbDataset.Text))
            //        strOutput = string.Format("{0}\\{1}", strOutput, cmbDataset.Text);

            //    strOutput = string.Format("{0}\\{1}", strOutput, txtFeatureClassName.Text);

            //}

            //if (!string.IsNullOrEmpty(cmbDataset.Text))
            //{
            //    IFeatureDataset fDsTarget = (m_Workspace as IFeatureWorkspace).OpenFeatureDataset(cmbDataset.Text);
            //    (fDsTarget as ISchemaLock).ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);
            //}
            string strOutput = Utility.WorkspaceHelper.GetGpString(m_Workspace, cmbDataset.Text, txtFeatureClassName.Text);
            bool isSucceed= GpTool.Import3DFile(txt3DFile.Text, strOutput,m_SpatialReferenceString);
            if (isSucceed)
            {
                XtraMessageBox.Show("导入3D数据成功！");
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                XtraMessageBox.Show("抱歉，导入出现错误！");
            }

        }


        private bool ValidateSetting(ref string errMsg)
        {
            if (string.IsNullOrEmpty(txt3DFile.Text))
            {
                errMsg = "请选择需要导入的3D文件！";
                return false;
            }

            if (m_Workspace == null)
            {
                errMsg = "必须填写正确的目标数据库！";
                return false;
            }

            if (string.IsNullOrEmpty(txtFeatureClassName.Text))
            {
                errMsg = "要素类名称必须填写！";
                return false;
            }

            return true;
        }
    }
}