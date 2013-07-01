using System;
using System.Data;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using Check.Task;
using Common.UI;
using Check.Utility;
using CheckTask = Check.Task.Task;

namespace Check.UI.Forms
{
    public partial class FrmOPenTask : XtraForm
    {
        private CheckTask m_SytemTask;
        /// <summary>
        /// 系统任务
        /// </summary>
        public CheckTask SystemTask
        {
            set
            {
                m_SytemTask = value;
            }
        }

        public FrmOPenTask()
        {
            InitializeComponent();

            Init();
        }

        public CheckTask SelectedTask
        {
            get
            {
                return m_SelectedTask;
            }
            set
            {
            }
        }

        private List<CheckTask> m_AllTasks;
        /// <summary>
        /// 加载任务列表
        /// 使用结构化的方法--由CheckTask和TaskHelper提供的方法，而不在此类中直接跟数据库及字段打交道
        /// </summary>
        private void Init()
        {
            // 加载任务列表 
            m_AllTasks = TaskHelper.GetAllTasks();
            if(m_AllTasks==null)
                return;

            DataTable tTask = ConstructTaskTable();
            int count = m_AllTasks.Count;
            for (int i = 0; i < count; i++)
            {
                CheckTask task=m_AllTasks[i];
                DataRow rowTask = tTask.NewRow();
                rowTask[0] = task.Name;
                rowTask[1] = task.Path;
                rowTask[2] = task.SourcePath;
                rowTask[3] = task.DatasourceType.ToString();

                tTask.Rows.Add(rowTask);
            }

            this.gridControlTasks.DataSource = tTask;
            this.gridControlTasks.RefreshDataSource();
        }
        private DataTable ConstructTaskTable()
        {
            DataTable tStruct = new DataTable();
            tStruct.Columns.Add("任务名称");
            tStruct.Columns.Add("任务路径");
            tStruct.Columns.Add("数据源路径");
            tStruct.Columns.Add("数据源数据类型");

            return tStruct;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            if (m_SytemTask!=null && m_SelectedTask.ID == m_SytemTask.ID)
            {
                XtraMessageBox.Show("所选中任务为当前打开的任务，不能删除");
                return;
            }

            if (XtraMessageBox.Show("确实要删除此任务吗", "删除确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                try
                {
                    string strFolder = m_SelectedTask.Path + "\\" + m_SelectedTask.Name;
                    if (Directory.Exists(strFolder))
                    {
                        m_SelectedTask.Release();
                        Directory.Delete(strFolder, true);
                    }
                    if (!TaskHelper.DeleteTask(m_SelectedTask.ID))
                    {
                        XtraMessageBox.Show("删除时发生错误");
                        return;
                    }
                    m_AllTasks.Remove(m_SelectedTask);
                    gridViewTasks.DeleteRow(gridViewTasks.FocusedRowHandle);
                    this.gridViewTasks_FocusedRowChanged(gridViewTasks, null);
                }
                catch
                {
                    XtraMessageBox.Show("删除时发生错误");
                }
            }
        }

        public void DeleteFolder(string dir)
        {
            if (Directory.Exists(dir))
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                        File.Delete(d);    //直接删除其中的文件  
                    else
                        DeleteFolder(d);       //递归删除子文件夹  
                }
                Directory.Delete(dir);    //删除已空文件夹  
            }
        }
    
        private void btnOpenTask_Click(object sender, EventArgs e)
        {
            if (OpenTask())
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool OpenTask()
        {
            try
            {
                if (gridViewTasks.RowCount < 1)
                {
                    XtraMessageBox.Show("当前任务列表为空！", "提示");
                    return false;
                }

                int nRowIndex = gridViewTasks.FocusedRowHandle;
                if (nRowIndex < 0)
                {
                    XtraMessageBox.Show("未选中任务，请选择需要打开的任务！", "提示");
                    return false;
                }


                if (m_SytemTask != null)
                {
                    if (m_SytemTask.Name == m_SelectedTask.Name && m_SytemTask.Path == m_SelectedTask.Path)
                    {
                        XtraMessageBox.Show("任务已打开!", "提示");
                        return false;
                    }
                }

                if (!Directory.Exists(m_SelectedTask.Path+"\\"+m_SelectedTask.Name))
                {
                    XtraMessageBox.Show("任务目录不存在!无法打开");
                    return false;
                }
              
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void btnOpenFromFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.Description = "请选择质检任务";

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string strFolderPath = folderDialog.SelectedPath;
                    string strConfigFile = strFolderPath + "\\" + COMMONCONST.File_Name_SystemConfig;
                    CheckTask task = TaskHelper.FromTaskConfig(strConfigFile);
                    // 修改task的path为上级目录，名称为当前文件夹名称
                    DirectoryInfo dirInfo = new DirectoryInfo(strFolderPath);
                    task.Name = dirInfo.Name;
                    task.Path = dirInfo.Parent.FullName;

                    m_SelectedTask = task;
                    if (m_SytemTask != null && m_SytemTask.Name == m_SelectedTask.Name && m_SytemTask.Path==m_SelectedTask.Path)
                    {
                        XtraMessageBox.Show("任务已打开");
                        return;
                    }
                    this.DialogResult = DialogResult.OK;
                }
                catch
                {
                    XtraMessageBox.Show("从文件打开任务失败！", "提示");
                }
            }
        }

        private CheckTask m_SelectedTask = null;

        private void gridViewTasks_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int[] handleSelected = gridViewTasks.GetSelectedRows();
            if (handleSelected == null || handleSelected.Length == 0)
            {
                btnOpenTask.Enabled = false;
                btnDeleteTask.Enabled = false;
            }
            else
            {
                btnOpenTask.Enabled = true;
                btnDeleteTask.Enabled = true;
            }

           int taskIndex= gridViewTasks.GetDataSourceRowIndex(handleSelected[0]);
           m_SelectedTask = m_AllTasks[taskIndex];
        }

        private void gridControlTasks_DoubleClick(object sender, EventArgs e)
        {
            if (OpenTask())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}