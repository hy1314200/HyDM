using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Check.Engine;
using Check.Define;
using Check.Utility;
using CheckTask = Check.Task.Task;
 
namespace Check.UI.Forms
{
    public partial class FrmTaskCheck : DevExpress.XtraEditors.XtraForm
    {
        public FrmTaskCheck(CheckTask task)
        {
            InitializeComponent();

            this.m_Task = task;
        }

        private CheckTask m_Task;

        public void CheckTask()
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(Check));
            thread.Start();
            this.ShowDialog();
            //this.Show();
            //this.Check();
        }

        private void Check()
        {
            if (m_Task == null)
                return;

            Checker checker = new Checker();
            checker.CheckingRuleChanged += new DealingRuleChangedHandler(checker_PretreatingRuleChanged);
            checker.PretreatingRuleChanged += new DealingRuleChangedHandler(checker_PretreatingRuleChanged);
            checker.VerifyedComplete += new VerifyedCompleteHandler(checker_VerifyedComplete);
            checker.VerifyingRuleChanged += new DealingRuleChangedHandler(checker_PretreatingRuleChanged);
            checker.PretreatComplete += new CheckEventHandler(checker_PretreatComplete);


            checker.BaseWorkspace = m_Task.BaseWorkspace;
            checker.QueryConnection = m_Task.QueryConnection;
            checker.QueryWorkspace = m_Task.QueryWorkspace;
            checker.ResultPath = m_Task.GetResultDBPath();
            TemplateRules templateRules = new TemplateRules(m_Task.SchemaID);
            checker.RuleInfos = templateRules.CurrentSchemaRules; //Helper.RuleInfoHelper.GetRuleInfos(m_Task.SchemaID);
            checker.SchemaID = m_Task.SchemaID;
            checker.TopoDBPath = m_Task.GetTaskFolder();
            checker.TopoTolerance = m_Task.TopoTolerance;

            Common.Utility.Log.TaskLogManager.SetLogFile("f:\\CheckLog.log");
            checker.Messager = LogDeal;
            if (checker.Check())
            {
                MessageBox.Show("成功");
            }
            else
            {
                MessageBox.Show("失败");
            }
        }

        void LogDeal(enumMessageType msgType, string strMsg)
        {
            Common.Utility.Log.TaskLogManager.AppendMessage(strMsg);
        }

        void checker_PretreatComplete(Checker sender)
        {
            this.Invoke(new SetTextHandler(SetOperateText), new object[] { "预处理完成" });

        }

        private delegate void SetTextHandler(string strText);
        private void SetOperateText(string strText)
        {
            lblOperate.Text = strText;
        }
        private void SetAvaliateText(string strText)
        {
            lblAvaliateCount.Text = strText;
        }

        void checker_VerifyedComplete(int availCount)
        {
            this.Invoke(new SetTextHandler(SetOperateText), new object[] { availCount.ToString() });
        }

        void checker_PretreatingRuleChanged(Check.Define.ICheckRule dealingRule)
        {
            if (dealingRule == null)
                return;

            this.Invoke(new SetTextHandler(SetOperateText), new object[] { dealingRule.Name });
        }



    }
}
