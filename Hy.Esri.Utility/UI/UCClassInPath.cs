using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using Hy.Esri.Utility.UI;

namespace Hy.Esri.Utility.UI
{
    public enum enumPathType
    {
        Feature,
        Raster
    }

    public partial class UCClassInPath : DevExpress.XtraEditors.XtraUserControl
    {
        public UCClassInPath()
        {
            InitializeComponent();
        }

        private enumPathType m_PathType = enumPathType.Feature;
        public enumPathType PathType
        {
            set
            {
                this.m_PathType = value;

                cmbWorkspaceType.Properties.Items.Clear();
                cmbWorkspaceType.Properties.Items.Add("SDE");
                cmbWorkspaceType.Properties.Items.Add("FileGDB");
                cmbWorkspaceType.Properties.Items.Add("PGDB");
                if (m_PathType == enumPathType.Feature)
                {
                    cmbWorkspaceType.Properties.Items.Add("Shp");
                    lblFeatureDataset.Text = "要素集：";
                    lblClass.Text = "要素名：";
                }
                else
                {
                    cmbWorkspaceType.Properties.Items.Add("栅格文件");
                    lblFeatureDataset.Text = "栅格目录：";
                    lblClass.Text = "栅格名：";
                }
            }
                
        }

        private enumWorkspaceType m_SelectedWorkspaceType;
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
                cmbClass.Enabled = false;
            }
            else
            {
                cmbDataset.Enabled = true;
                cmbClass.Enabled = true;
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
                    if (this.m_PathType == enumPathType.Feature)
                    {
                        dlgWorkspace.Filter = "Shp |*.shp";                    }
                    else
                    {
                        dlgWorkspace.Filter = "所有类型|*.jpg;*.bmp;*.tif;*.img;*.png;*.ovr;";
                    }
                    if (dlgWorkspace.ShowDialog(this) != DialogResult.OK)
                        return;

                    txtWorkspace.Text = dlgWorkspace.FileName;
                    m_Workspace = Utility.WorkspaceHelper.OpenWorkspace(enumWorkspaceType.File, System.IO.Path.GetDirectoryName(dlgWorkspace.FileName));
                    string strName=System.IO.Path.GetFileName(dlgWorkspace.FileName);
                    cmbClass.Properties.Items.Clear();
                    cmbClass.Properties.Items.Add(strName);
                    cmbClass.Text = strName;

                    // Shp / 文件栅格的话到此为止
                    return;

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
            esriDatasetType dsType = (this.m_PathType == enumPathType.Feature ? esriDatasetType.esriDTFeatureDataset : esriDatasetType.esriDTRasterCatalog);
            IEnumDatasetName enDatasetName = m_Workspace.get_DatasetNames(dsType);
            IDatasetName dsName = enDatasetName.Next();
            while (dsName != null)
            {
                cmbDataset.Properties.Items.Add(dsName.Name);
                dsName = enDatasetName.Next();
            }


        }

        public enumWorkspaceType WorkspaceType
        {
            get
            {
                return m_SelectedWorkspaceType;
            }
        }

        public IWorkspace Workspace
        {
            get { return m_Workspace; }
        }

        public IFeatureDataset FeatureDataset
        {
            get
            {
                if (m_Workspace == null)
                    return null;

                if (this.m_PathType != enumPathType.Feature)
                    return null;

                return (m_Workspace as IFeatureWorkspace).OpenFeatureDataset(cmbDataset.Text);
            }
            //set
            //{
            //    if (this.m_PathType != enumPathType.Feature)
            //        return ;

            //    if (value == null)
            //        return;

            //    this.cmbDataset.Text = value.Name;
            //}
        }

        public string FeatureDatasetName
        {
            get
            {
                return cmbDataset.Text;
            }
        }

        public string FeatureClassName
        {
            get
            {
                return cmbClass.Text;
            }
        }

        public IFeatureClass FeatureClass
        {
            get
            {
                return (m_Workspace as IFeatureWorkspace).OpenFeatureClass(cmbClass.Text);
            }

        }

        public IRasterCatalog RasterCatalog
        {
            get
            {
                if (this.m_PathType != enumPathType.Raster)
                    return null;

                try
                {
                    return (m_Workspace as IRasterWorkspaceEx).OpenRasterCatalog(cmbDataset.Text);
                }
                catch
                {
                    return null;
                }
            }
            //set
            //{
            //    if (value == null)
            //        return;

            //    if (this.m_PathType != enumPathType.Raster)
            //        return ;

            //    cmbDataset.Text = (value as IDataset).Name;
            //}
        }

        public string RasterCatalogName
        {
            get
            {
                return cmbDataset.Text;
            }
        }

        public IRasterDataset RasterDataset
        {
            get
            {
                return (m_Workspace as IRasterWorkspaceEx).OpenRasterDataset(cmbClass.Text);
            }
        }

        public string RasterDatasetName
        {
            get
            {
                return cmbClass.Text;
            }
        }

        private void cmbDataset_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strSetName = cmbDataset.Text;
            if (this.m_PathType == enumPathType.Feature)
            {
                if (string.IsNullOrWhiteSpace(strSetName))
                {
                    IEnumDatasetName enDsName = m_Workspace.get_DatasetNames(esriDatasetType.esriDTFeatureClass);
                    IDatasetName dsName = enDsName.Next();
                    while (dsName != null)
                    {
                        if (dsName.Type == esriDatasetType.esriDTFeatureClass)
                        {
                            cmbClass.Properties.Items.Add(dsName.Name);
                        }

                        dsName = enDsName.Next();
                    }
                }
                else
                {
                    IFeatureDataset fDs = this.FeatureDataset;
                    IEnumDataset enDataset = fDs.Subsets;
                    IDataset ds = enDataset.Next();
                    while (ds != null)
                    {
                        if (ds.Type == esriDatasetType.esriDTFeatureClass)
                        {
                            cmbClass.Properties.Items.Add(ds.Name);
                        }

                        ds = enDataset.Next();
                    }

                }
            }
            else
            {
            }
        }
    }
}
