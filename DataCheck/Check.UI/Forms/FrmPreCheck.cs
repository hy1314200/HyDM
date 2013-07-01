using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Check.Define;
using Check.Task;
using Check.Engine;
using Check.UI.UC;
using Check.Utility;

namespace Check.UI.Forms
{
    public partial class FrmPreCheck : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 返回选择的规则列表
        /// </summary>
        /// <value>The schema rules selection.</value>
        public List<SchemaRuleEx> SchemaRulesSelection
        {
            get;
            set;
        }
        public TemplateRules CurrentTemplateRules
        {
            set;
            private get;
        }

        //public Dictionary<string, bool> RulesState
        //{
        //    set;
        //    get;
        //}

        public string TaskName
        {
            set;
            get;
        }

        public FrmPreCheck()
        {
            InitializeComponent();
            this.ucRulesTree.TreeNodeCheckStateChanged += ucRulesTree_TreeNodeCheckStateChanged;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SchemaRulesSelection = this.ucRulesTree.RulesSelection;

            if (SchemaRulesSelection.Count == 0)
            {
                XtraMessageBox.Show("当前没有选择任何规则！", "警告",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            this.DialogResult = DialogResult.Yes;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FrmPreCheck_Load(object sender, EventArgs e)
        {
            this.ucRulesTree.CurrentTemplateRules = CurrentTemplateRules;
            this.ucRulesTree.ShowType = RuleTreeShowType.ViewRulesAndSelection;
            this.ucRulesTree.CurrentTaskName = TaskName;
            this.ucRulesTree.RulesSelection = SchemaRulesSelection;
            this.ucRulesTree.LoadRulesTree();
            this.ucRulesTree.CurrentTreeList.ExpandAll();
            this.ucRulesTree.CurrentTreeList.Refresh();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!bolIscheckEditChecked)
            {
                bolIscheckEditChecked = true;
                return;
            }

            if (checkEdit1.Checked)
            {
                this.ucRulesTree.AllNodeChecked = true;
            }
            else
            {
                this.ucRulesTree.AllNodeChecked = false;
            }
        }

        private bool bolIscheckEditChecked = true;
        private void ucRulesTree_TreeNodeCheckStateChanged(bool bol)
        {
            bolIscheckEditChecked = false;
            this.checkEdit1.Checked= bol;
        }
    }
}