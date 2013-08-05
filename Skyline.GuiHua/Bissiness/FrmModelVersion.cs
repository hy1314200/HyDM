using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using TerraExplorerX;
using System.IO;
using System.Configuration;

namespace Skyline.GuiHua.Bussiness
{
    public partial class FrmModelVersion : DevExpress.XtraEditors.XtraForm
    {
        public FrmModelVersion(TerraExplorerX.ISGWorld61 hook,string projectID)
        {
            InitializeComponent();

            this.m_Hook = hook;
            this.m_ProjectID = projectID;
        }

        private TerraExplorerX.ISGWorld61 m_Hook;
        private string m_ProjectID;


        private List<VersionInfo> m_VersionList;
        private VersionInfo m_SelectedVersion;

        private void FrmModelVersion_Load(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void Refresh()
        {

            tBar.Properties.Maximum = 0;
            List<VersionInfo> vList = ProjectHelper.GetVersionList(this.m_ProjectID);

            if (vList == null || vList.Count == 0)
            {
                tBar.Enabled = false;
                tBtnDelete.Enabled = false;
                tBtnView.Enabled = false;
            }
            else
            {
                tBar.Enabled = true;
                tBtnDelete.Enabled = true;
                tBtnView.Enabled = true;
                tBar.Properties.Maximum = vList.Count;

                tBar_ValueChanged(null, null);
            }
            this.m_VersionList = vList;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmAddVersion frmRemark = new FrmAddVersion();
            if (frmRemark.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int GroupID = this.m_Hook.ProjectTree.FindItem(strLabel);
                    if (GroupID > 0)
                    {
                        ILayer61 lyrModel = this.m_Hook.ProjectTree.GetLayer(GroupID);
                        // string strFile = Guid.NewGuid().ToString() + ".shp";
                        //Application.StartupPath + "\\GuiHua\\Versions\\" + Guid.NewGuid().ToString() + ".Shp";
                        lyrModel.Save();
                        string strOld = lyrModel.DataSourceInfo.ConnectionString;
                        string[] strs = strOld.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        strOld = strs[0];
                        strOld = strOld.Substring(strOld.IndexOf("=") + 1);

                        string strFolder = System.IO.Path.GetDirectoryName(strOld);
                        string strName = System.IO.Path.GetFileNameWithoutExtension(strOld);
                        string strSource = strFolder + "\\" + strName;
                        string strTarget = Application.StartupPath + "\\GuiHua\\Versions\\" + Guid.NewGuid().ToString();
                        try
                        {
                            File.Copy(strSource + ".shp", strTarget + ".shp");
                            File.Copy(strSource + ".shx", strTarget + ".shx");
                            File.Copy(strSource + ".dbf", strTarget + ".dbf");
                            File.Copy(strSource + ".shp.xml", strTarget + ".shp.xml");
                            File.Copy(strSource + ".prj", strTarget + ".prj");
                            File.Copy(strSource + ".sbx", strTarget + ".sbx");
                            File.Copy(strSource + ".sbn", strTarget + ".sbn");
                        }
                        catch
                        {
                        }

                        if (ProjectHelper.AddVersion(this.m_ProjectID, strTarget + ".shp", frmRemark.Description))
                        {
                            XtraMessageBox.Show("添加成功");
                            this.Refresh();
                        }
                        else
                        {
                            XtraMessageBox.Show("添加失败");
                        }
                    }
                }
                catch
                {
                    XtraMessageBox.Show("对不起，添加操作出现了错误，这通常是因为配置不当引起的。");
                }
            }
        }

        string strLabel = ConfigurationManager.AppSettings["LayerPath"];
        private void tBar_ValueChanged(object sender, EventArgs e)
        {
            if (m_VersionList == null)
            {
                tBtnDelete.Enabled = false;
                tBtnView.Enabled = false;
                return;
            }

            if (tBar.Value==0 || tBar.Value > m_VersionList.Count )
            {
                tBtnDelete.Enabled = false;
                tBtnView.Enabled = false;
                return;
            }

            m_SelectedVersion = this.m_VersionList[tBar.Value-1];
            tBtnView.Enabled= tBtnDelete.Enabled = (m_SelectedVersion != null);

            lblDate.Text = m_SelectedVersion.Date;
            labelControl6.Text = m_SelectedVersion.Description;

            if (cbView.Checked)
            {
                ViewVersion();
            }
        }

        private void ViewVersion()
        {
            try
            {
                int GroupID = this.m_Hook.ProjectTree.FindItem(strLabel);
                ILayer61 lyrModel = this.m_Hook.ProjectTree.GetLayer(GroupID);
                
                lyrModel.DataSourceInfo.ConnectionString = string.Format("FileName={0};TEPlugName=OGR;", m_SelectedVersion.VersionFile);
                lyrModel.Refresh();
            }
            catch
            {
                XtraMessageBox.Show("对不起，预览操作出现了错误，这通常是因为配置不当引起的。");
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("确定要删除当前版本吗？", "删除确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if(ProjectHelper.DeleteVersion(this.m_ProjectID,m_SelectedVersion.Date))
                {
                    XtraMessageBox.Show("删除成功");
                    this.Refresh();
                }
                else
                {
                    XtraMessageBox.Show("删除失败");
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ViewVersion();
        }
    }
}