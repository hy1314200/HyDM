using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;


using Check.Task;
using Check.Engine;
using CheckTask = Check.Task.Task;

namespace Check.UI.Forms
{
    public partial class FrmMultiTaskCheck : FrmTaskCheck
    {
        private void InitUI()
        { 
            this.progressBarControl1.Location = new System.Drawing.Point(2, 111);
            this.progressBarControl1.Size = new System.Drawing.Size(469, 22);
     
            this.panelControl2.Location = new System.Drawing.Point(2, 133);
            this.panelControl2.Size = new System.Drawing.Size(469, 36);
         
            this.btnClose.Location = new System.Drawing.Point(357, 5);
         
            this.labelControl1.Location = new System.Drawing.Point(161, 22);
         
            this.labelControl3.Location = new System.Drawing.Point(326, 22);
           
            this.lblTime.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblTime.Appearance.Options.UseForeColor = true;
            this.lblTime.Location = new System.Drawing.Point(428, 45);
         
            this.labelControl2.Location = new System.Drawing.Point(326, 45);
         
            this.lblRuleCount.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblRuleCount.Appearance.Options.UseForeColor = true;
            this.lblRuleCount.Location = new System.Drawing.Point(249, 23);
           
            this.lblExcutedRuleCount.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblExcutedRuleCount.Appearance.Options.UseForeColor = true;
            this.lblExcutedRuleCount.Location = new System.Drawing.Point(428, 22);
        
            this.lblOperate.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblOperate.Appearance.Options.UseForeColor = true;
            this.lblOperate.Location = new System.Drawing.Point(16, 68);
       
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Size = new System.Drawing.Size(149, 171);
           
            this.lblErrorCount.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblErrorCount.Appearance.Options.UseForeColor = true;
            this.lblErrorCount.Location = new System.Drawing.Point(249, 45);
       
            this.labelControl5.Location = new System.Drawing.Point(161, 45);
      
            this.btnViewLog.Location = new System.Drawing.Point(247, 5);
        }
        private FrmMultiTaskCheck()
        {
            InitializeComponent();

            //InitUI();
        }


        public FrmMultiTaskCheck( MultiTask multiTask)
        {
            InitializeComponent();

            //InitUI();

            this.CurrentMultiTask = multiTask;
        }

        private List<CheckTask> m_TaskList;
        public MultiTask CurrentMultiTask
        {
            set
            {
                m_MultiTask = value;

                if (m_MultiTask == null)
                    return;

                m_TaskList = m_MultiTask.GetTasks();
                for (int i = 0; i < m_MultiTask.TaskCount; i++)
                {
                    clbTask.Items.Add(m_TaskList[i].Name, false);
                }
            }
        }
        private MultiTask m_MultiTask;

        public void ExcuteMultiTask(ref List<CheckTask> availableTasks)
        {
            if (m_MultiTask == null)
                return;

            List<CheckTask> tempTasks=null;
            System.Threading.ThreadStart threadStart = delegate
            {
                m_MultiTask.CheckingTaskChanged += new TaskCheckEventsHandler(m_MultiTask_CheckingTaskChanged);
                m_MultiTask.CreatingTaskChanged += new TaskCreateEventsHandler(m_MultiTask_CreatingTaskChanged);
                m_MultiTask.TaskChecked += new TaskCheckEventsHandler(m_MultiTask_TaskChecked);
                m_MultiTask.TaskCreated += new TaskCreateEventsHandler(m_MultiTask_TaskCreated);

                //AdaptCheckerEvents(m_MultiTask.TaskChecker);
                m_MultiTask.Excute(ref tempTasks);
               // availableTasks = tempTasks;


                ThreadStart invokeFuction = delegate
                {
                    lblOperateType.Text = "所有任务执行完成";
                };
                this.Invoke(invokeFuction);
            };

            System.Threading.Thread thread = new System.Threading.Thread(threadStart);
            thread.Start();

            this.ShowDialog();

            availableTasks = tempTasks;
        }


        void m_MultiTask_CheckingTaskChanged(Checker curChecker, CheckTask curTask)
        {
            this.AdaptCheckerEvents(curChecker);
            this.CurrentTask = curTask;
            if (this.InvokeRequired)
            {
                ThreadStart threadStart = delegate
                {
                    ChangeOperateMode(true);
                    ReadyForCheck();
                    lblOperateType.Text = "正在检查任务……";
                    lblOperate.Text = "正在计算检查环境……";
                };

                this.Invoke(threadStart);
            }
            else
            {
                ChangeOperateMode(true);
                ReadyForCheck();
                lblOperateType.Text = "正在检查任务……";
                lblOperate.Text = "正在计算检查环境……";
            }

            for (int i = 0; i < m_TaskList.Count; i++)
            {
                if (m_TaskList[i].ID == curTask.ID)
                {
                    if (this.InvokeRequired)
                    {
                        ThreadStart threadStart = delegate
                        {
                            clbTask.SelectedIndex = i;
                        };
                        this.Invoke(threadStart);
                    }
                    else
                    {
                        clbTask.SelectedIndex = i;
                    }
                    break;
                }
            }
        }
        void m_MultiTask_TaskChecked(Checker curChecker, CheckTask curTask)
        {
            for (int i = 0; i < m_TaskList.Count; i++)
            {
                if (m_TaskList[i].ID == curTask.ID)
                {
                    if (this.InvokeRequired)
                    {
                        ThreadStart threadStart = delegate
                        {
                            clbTask.Items[i].CheckState = CheckState.Checked;
                        };
                        this.Invoke(threadStart);
                    }
                    else
                    {
                        clbTask.Items[i].CheckState = CheckState.Checked;
                    }
                    break;
                }
            }
        }

        void m_MultiTask_CreatingTaskChanged(CheckTask curTask)
        {
            this.CurrentTask = curTask;
            curTask.DataImporter.ImportingObjectChanged += new Check.Task.DataImport.ImportingObjectChangedHandler(DataImporter_ImportingObjectChanged);

            if (this.InvokeRequired)
            {
                ThreadStart threadStart = delegate
                {
                    lblOperateType.Text = "正在创建任务……";
                    lblOperate.Text = "正在创建任务结构……";
                    ChangeOperateMode(false);
                };

                this.Invoke(threadStart);
            }
            else
            {
                lblOperateType.Text = "正在创建任务……";
                lblOperate.Text = "正在创建任务结构……";
                ChangeOperateMode(false);
            }
        }

        void m_MultiTask_TaskCreated(CheckTask curTask)
        {

        }

        void DataImporter_ImportingObjectChanged(string strOjbectName)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetTextHandler(SetOperateText), new object[] { strOjbectName });
            }
            else
            {
                SetOperateText(strOjbectName);                
            }
        }


        public void ReadyForMultiTask()
        {
            this.ReadyForCheck();

            ChangeOperateMode(false);
        }

        public override void ReadyForCheck()
        {
            base.ReadyForCheck();
            lblOperate.Text = "正在创建任务结构……";
        }

        private void ChangeOperateMode(bool check)
        {
            labelControl1.Visible = check;
            labelControl2.Visible = check;
            labelControl3.Visible = check;
            labelControl5.Visible = check;

            lblErrorCount.Visible = check;
            lblExcutedRuleCount.Visible = check;
            lblRuleCount.Visible = check;
            lblTime.Visible = check;

            progressBarControl1.Visible = check;
            marqueeProgressBarControl1.Visible = !check;


        }     

        /// <summary>
        /// 检查完成时不显示查看和审核
        /// </summary>
        /// <param name="sender"></param>
        protected override void CheckComplete(Check.Engine.Checker sender)
        {
            base.CheckComplete(sender);
            if (this.InvokeRequired)
            {
                NoneArgmentEventHandler uiChanged = delegate
                {
                    this.btnViewLog.Visible = false;
                    this.btnClose.Text = "取消";
                };
                this.Invoke(uiChanged);
            }
            else
            {
                this.btnViewLog.Visible = false;
                    this.btnClose.Text = "取消";
                Application.DoEvents();
            }
        }

    }
}
