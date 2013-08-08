using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Skyline.GuiHua.Bussiness
{
    public partial class FrmProjects : DevExpress.XtraEditors.XtraForm
    {
        public FrmProjects()
        {
            InitializeComponent();

            Refresh();
        }

        private void Refresh()
        {
            List<ProjectInfo> pInfoList = null;
            ProjectHelper dbStructor = new ProjectHelper();
            if (dbStructor.TestSysTable(ref pInfoList))
            {
                this.Datasource = pInfoList;
            }
            else
            {
                MessageBox.Show("内部错误：数据库没有初始化，无法加载项目列表");
            }
        }

        private List<ProjectInfo> m_Datasource;
        public List<ProjectInfo> Datasource
        {
            set
            {
                if (value == null)
                    return;

                m_Datasource = value;
                this.ucPorject1.Datasource = value;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string strName = txtClause.Text;
            if (string.IsNullOrWhiteSpace(strName))
            {
                this.ucPorject1.Datasource = m_Datasource;
                return;
            }

            List<ProjectInfo> pInfoList = new List<ProjectInfo>();
            foreach (ProjectInfo pInfo in m_Datasource)
            {
                if (pInfo == null || string.IsNullOrEmpty(pInfo.Name))
                    continue;

                if (pInfo.Name.Contains(strName))
                {
                    pInfoList.Add(pInfo);
                }
            }

            this.ucPorject1.Datasource = pInfoList;

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (this.folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string folderProject = folderBrowser.SelectedPath;
                ProjectFileStructure pStructure = null;
                string errMsg = null;
                if (ProjectHelper.TestProjectFolder(folderProject, ref pStructure, ref errMsg))
                {
                    ProjectInfo projectInfo = null;
                    if (ProjectHelper.ImportProject(pStructure, ref projectInfo))
                    {
                        XtraMessageBox.Show("导入成功!");
                        Refresh();
                    }
                    else
                    {
                        XtraMessageBox.Show("导入失败!");
                        return;
                    }
                }
                else
                {
                    XtraMessageBox.Show(string.Format("当前所选择的文件夹不是正确的项目文件夹：{0}!", errMsg));
                    return;
                }
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("确定要删除当前的项目吗？","删除确认", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            ProjectInfo pInfo=this.ucPorject1.CurrentProject;
            if (pInfo == null)
                return;

            string strID = pInfo.ID;
            if(ProjectHelper.DeleteProjectInfo(pInfo))
            {
                XtraMessageBox.Show("删除成功!");
                Refresh();
            }
            else
            {
                XtraMessageBox.Show("抱歉，删除项目失败!");
            }
        }

        private ProjectInfo m_SelectedProject;
        public ProjectInfo SelectedProject
        {
            get
            {
                return m_SelectedProject;
            }
        }
        private void ucPorject1_FocusedProjectChanged(ProjectInfo projectInfo)
        {
            this.m_SelectedProject = projectInfo;
            btnDelete.Enabled = btnOpen.Enabled = (m_SelectedProject != null);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}