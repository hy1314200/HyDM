using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Data;
using ESRI.ArcGIS.Geodatabase;

using Check.Task.DataImport;
using Check.Define;
using Check.Engine;
using Check.Utility;

namespace Check.Task
{
    public delegate void TaskCheckEventsHandler(Checker curChecker, Task curTask);
    public delegate void TaskCreateEventsHandler(Task curTask);


    /// <summary>
    /// 批量任务创建（和检查）
    /// </summary>
    public class MultiTask
    {
        private List<Task> m_TaskList = new List<Task>();

        public MultiTask()
        {
        }

        public MultiTask(List<Task> tasks)
        {
            this.SetTasks(tasks);
        }

        /// <summary>
        /// 添加任务信息
        /// </summary>
        /// <param name="task"></param>
        public void AddTask(Task task)
        {
            m_TaskList.Add(task);
        }

        /// <summary>
        /// 删除任务信息
        /// </summary>
        /// <param name="task"></param>
        public void DeleteTask(Task task)
        {
            for(int i=0;i<m_TaskList.Count;i++) 
            {
                if (this.m_TaskList[i].Name == task.Name)
                {
                    m_TaskList.RemoveAt(i);
                    return;
                }
            }
        }

        public List<Task> GetTasks()
        {
            return m_TaskList;
        }

        /// <summary>
        /// 任务数目
        /// </summary>
        public int TaskCount
        {
            get { return m_TaskList.Count; }
        }

        /// <summary>
        /// 清空任务信息（列表）
        /// </summary>
        public void Clear()
        {
            m_TaskList.Clear();
        }

        /// <summary>
        /// 设置当前任务信息（列表）
        /// </summary>
        /// <param name="tasks"></param>
        public void SetTasks(List<Task> tasks)
        {
            if (tasks != null)
                this.m_TaskList = tasks;
        }

        private string m_PromptMsg;
        /// <summary>
        /// 获取（执行后的）提示信息
        /// </summary>
        public string PromptMessage
        {
            get
            {
                return m_PromptMsg;
            }
        }

        private int m_SucceedCount;
        /// <summary>
        /// 获取成功执行的任务数
        /// </summary>
        public int SucceedCount
        {
            get
            {
                return m_SucceedCount;
            }
        }

        /// <summary>
        /// 执行
        /// 指创建任务[并执行检查]
        /// </summary>
        /// <returns>返回【至少创建成功了的】任务列表</returns>
        public bool Excute(ref List<Task> availableTasks)
        {
            try
            {
                availableTasks = new List<Task>();

                this.m_SucceedCount = 0;
                this.m_PromptMsg = "";
                if (this.m_TaskList == null || this.m_TaskList.Count == 0)
                {
                    m_PromptMsg="任务列表为空！无法创建质检任务！";
                    return false;
                }


                int count = this.m_TaskList.Count;
                int succeedCount=0;
                int excuteCount=0;
                for (int i = 0; i < count; i++)
                {
                    ExtendTask curTask = this.m_TaskList[i] as ExtendTask;
                    if (curTask == null)
                        continue;

                    // 创建
                    if (this.CreatingTaskChanged != null)
                        this.CreatingTaskChanged.Invoke(curTask);

                    bool isSucceed= curTask.Create();
                    isSucceed = curTask.CreateMXD();

                    if (this.TaskCreated != null)
                        this.TaskCreated.Invoke(curTask);

                    if (!isSucceed)
                    {
                        SendMessage(enumMessageType.Exception,string.Format("任务:{0}创建失败",curTask.Name));
                        continue;
                    }
                    availableTasks.Add(curTask);

                    ////执行质检
                    if (curTask.CheckMode!=enumCheckMode.CreateOnly)
                    {
                        Checker m_TaskChecker = new Checker();

                        if (this.CheckingTaskChanged != null)
                            this.CheckingTaskChanged.Invoke(m_TaskChecker, curTask);

                        curTask.ReadyForCheck();
                        m_TaskChecker.BaseWorkspace = curTask.BaseWorkspace;
                        m_TaskChecker.QueryConnection = curTask.QueryConnection;
                        m_TaskChecker.QueryWorkspace = curTask.QueryWorkspace;
                        m_TaskChecker.ResultPath = curTask.GetResultDBPath();
                        if (curTask.CheckMode == enumCheckMode.CheckAll)
                        {
                            TemplateRules tempRules = new TemplateRules(curTask.SchemaID);
                            m_TaskChecker.RuleInfos = tempRules.CurrentSchemaRules;
                        }
                        else
                        {
                            m_TaskChecker.RuleInfos = curTask.RuleInfos;
                        }
                        m_TaskChecker.SchemaID = curTask.SchemaID;
                        m_TaskChecker.TopoDBPath = curTask.GetResultDBPath();
                        m_TaskChecker.TopoTolerance = curTask.TopoTolerance;
                        COMMONCONST.TOPOTOLORANCE = curTask.TopoTolerance;
                        COMMONCONST.dAreaThread = curTask.MapScale * 0.04;
                        COMMONCONST.dLengthThread = curTask.MapScale * 0.2 / 10000;

                        m_TaskChecker.Check();                       

                        if (this.TaskChecked != null)
                            this.TaskChecked.Invoke(m_TaskChecker, curTask);

                        excuteCount++;
                    }

                    succeedCount++;
                }

                if(succeedCount==count)
                {
                    if(excuteCount==0)
                       m_PromptMsg=string.Format("{0}个任务全部创建完成！",count);
                    else
                        m_PromptMsg=string.Format("{0}个任务全部创建完成，{1}个任务检查完成！",count,excuteCount);
                }
                else
                {
                    if(excuteCount==0)
                       m_PromptMsg=string.Format("{0}质检任务创建完成，其余质检任务执行失败", succeedCount);
                    else
                        m_PromptMsg=string.Format("{0}质检任务创建完成({1}个任务检查完成)，其余质检任务执行失败", succeedCount,excuteCount);
                
                }             
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, "创建任务过程中出现错误，错误原因：" + ex.ToString());
                return false;
            }
            finally
            {
            }

            return true;
        }


        //public event ImportingObjectChangedHandler ImportingDataChanged;
        /// <summary>
        /// （将要）创建的任务变化
        /// </summary>
        public event TaskCreateEventsHandler CreatingTaskChanged;
        /// <summary>
        /// （将要）检查的任务变化
        /// </summary>
        public event TaskCheckEventsHandler CheckingTaskChanged;
        /// <summary>
        /// （某一个）任务创建完成
        /// </summary>
        public event TaskCreateEventsHandler TaskCreated;
        /// <summary>
        /// （某一个）任务检查完成
        /// </summary>
        public event TaskCheckEventsHandler TaskChecked;

        //private Checker m_TaskChecker=new Checker();
        ///// <summary>
        ///// 检查类，不要销毁此对象
        ///// </summary>
        //public Checker TaskChecker
        //{
        //    get
        //    {
        //        return m_TaskChecker;
        //    }
        //}


        private MessageHandler m_Messager;
        public MessageHandler Messager
        {
            set
            {
                m_Messager = value;
            }
        }
        private void SendMessage(enumMessageType msgType, string strMsg)
        {
            if (m_Messager != null)
                m_Messager(msgType, strMsg);
        }

     }
}
