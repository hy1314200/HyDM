using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Common.Utility.Data;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList;
using Hy.Check.Define;
using Hy.Check.Engine;
using Hy.Check.Utility;

namespace Hy.Check.UI.UC
{

    /// <summary>
    /// 规则错误显示类型
    /// </summary>
    public enum RuleShowType
    {
        /// <summary>
        /// 按照检查类型分类
        /// </summary>
        DefualtType=0,
        /// <summary>
        /// 按照图层类型分类
        /// </summary>
        LayerType
    }

    /// <summary>
    /// 规则树显示分类
    /// </summary>
    public enum RuleTreeShowType
    {
        /// <summary>
        /// 只显示规则列表
        /// </summary>
        ViewRules=0,
        /// <summary>
        /// 显示规则列表并显示各个规则错误数
        /// </summary>
        ViewRuleErrors,
        /// <summary>
        /// 显示带复选框的规则列表，用于抽检
        /// </summary>
        ViewRulesAndSelection
    }

    public partial class UCRulesTree : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 用户选择某个树节点后的委托，用于和主窗口通讯
        /// </summary>
        public delegate void TreeNodeSelected(string strRuleName,TreeRulesEventArgs e);

        public delegate void TreeNodeCheckStateHandle(bool isAllCheck);
        /// <summary>
        /// 树节点选择事件
        /// </summary>
        public event TreeNodeSelected TreeListNodeIsSelected;

        public event TreeNodeCheckStateHandle TreeNodeCheckStateChanged;

        /// <summary>
        /// Gets or sets the type of the rule.
        /// </summary>
        /// <value>The type of the rule.</value>
        public RuleShowType RuleType
        {
            set { m_RuleType = value; }
            get { return m_RuleType; }
        }

        private RuleTreeShowType TreeShowType = RuleTreeShowType.ViewRules;

        /// <summary>
        /// Gets or sets the rules selection.
        /// </summary>
        /// <value>The rules selection.</value>
        public List<SchemaRuleEx> RulesSelection
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets the current schema id.
        /// </summary>
        /// <value>The current schema id.</value>
        public string CurrentSchemaId
        {
            set;
            get;
        }
        
        /// <summary>
        /// Gets or sets the name of the current task.
        /// </summary>
        /// <value>The name of the current task.</value>
        public string CurrentTaskName
        {
            set;
            get;
        }

        private bool m_ShowRulesCount = true;

        /// <summary>
        /// 是否显示规则数目
        /// </summary>
        /// <value><c>true</c> if [show rules count]; otherwise, <c>false</c>.</value>
        public bool ShowRulesCount
        {
            set
            {
            	 m_ShowRulesCount= value;
            }
            get
            {
                return m_ShowRulesCount;
            }
        }

        /// <summary>
        /// 返回所有的规则数据表
        /// </summary>
        /// <value>The rules dt.</value>
        public DataTable RulesDt
        {
            private set;
            get;
        }

        private TemplateRules m_CurrTemplateRules = null;

        /// <summary>
        /// Gets or sets the current template rules.
        /// </summary>
        /// <value>The current template rules.</value>
        public TemplateRules CurrentTemplateRules
        {
            get { return m_CurrTemplateRules; }
            set { m_CurrTemplateRules = value; }
        }

        /// <summary>
        /// 获取只包含错误数的检查结果分类表.
        /// </summary>
        /// <value>The results dt.</value>
        public DataTable ResultsDt
        {
            set;
            private get;
        }

        /// <summary>
        /// 设置按图层分类的只包含错误数的检查结果分类表.
        /// </summary>
        /// <value>The results dt.</value>
        public DataTable LayersResultsDt
        {
            set;
            private get;
        }
        
        /// <summary>
        /// Gets or sets the Hy.Check results count.
        /// </summary>
        /// <value>The Hy.Check results count.</value>
        public int CheckResultsCount
        {
            set;
            private get;
        }

        /// <summary>
        /// 设置或者获取空间要显示的类型
        /// </summary>
        /// <value>The type of the show.</value>
        public RuleTreeShowType ShowType
        {
            get { return TreeShowType; }
            set 
            { 
                TreeShowType = value;
                this.treeListRule.Nodes.Clear();
                this.treeListRule.Columns.Clear();
                Init();
            }
        }

        /// <summary>
        /// 当前的tree控件
        /// </summary>
        /// <value>The current tree list.</value>
        public TreeList CurrentTreeList
        {
            get;
            private set;
        }

        /// <summary>
        /// 设置当前的节点全选或者不全选
        /// </summary>
        /// <value><c>true</c> if [all node checked]; otherwise, <c>false</c>.</value>
        public bool AllNodeChecked
        {
            set
            {
                if (TreeShowType != RuleTreeShowType.ViewRulesAndSelection) return;
                bool bolChecked=value;
                if (this.treeListRule.Nodes.Count > 0)
                {
                    TreeListNode node;
                    for (int i = 0; i < treeListRule.Nodes.Count; i++)
                    {
                        node = treeListRule.Nodes[i];
                        node.CheckState = bolChecked==true? CheckState.Checked :CheckState.Unchecked;
                        //SetRulesHistoryState(node["RuleName"].ToString(), node.Checked);
                        SetCheckedChildNodes(node, node.CheckState);
                        SetCheckedParentNodes(node, node.CheckState);
                        SetRules(node, node.CheckState);
                    }
                }
                if (!bolChecked)
                {
                    this.RulesSelection.RemoveRange(0, RulesSelection.Count);
                }
            }
           private get{return true;}

        }

        //public Dictionary<string, bool> RulesSelectionHistoryState
        //{
        //    set
        //    {
        //        m_RulesSelectionHistoryState = value;
        //    }
        //    get
        //    {
        //        return m_RulesSelectionHistoryState;
        //    }
        //}

        private RuleShowType m_RuleType = RuleShowType.DefualtType;

        private List<SchemaRuleEx> m_TempScheamRules = null;

        private Dictionary<string, bool> m_RulesSelectionHistoryState; //= new Dictionary<string, bool>();
        /// <summary>
        /// 默认构造函数 ，只显示规则列表<see cref="UCRulesTree"/> class.
        /// </summary>
        public UCRulesTree()
        {
            InitializeComponent();
            this.treeListRule.OptionsMenu.EnableColumnMenu = false;
            this.treeListRule.OptionsMenu.EnableFooterMenu = false;
            this.treeListRule.OptionsBehavior.AutoChangeParent = false;
            this.treeListRule.OptionsBehavior.AutoSelectAllInEditor = false;
            this.treeListRule.OptionsBehavior.Editable = false;
            this.treeListRule.OptionsBehavior.MoveOnEdit = false;
            CurrentTreeList = this.treeListRule;
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        private void Init()
        {
            string columnCaption = "检查类型";
            if (TreeShowType == RuleTreeShowType.ViewRulesAndSelection)
            {
                this.treeListRule.OptionsView.ShowCheckBoxes = true;
                this.radioType.Visible = false;
            }
            else if (TreeShowType == RuleTreeShowType.ViewRuleErrors)
            {
                this.treeListRule.OptionsView.ShowCheckBoxes = true;
                columnCaption = "检查类型(错误数目)";
                this.radioType.Visible = true;
            }
            else
            {
                this.treeListRule.OptionsView.ShowCheckBoxes = false;
                this.radioType.Visible = false;
                columnCaption = "检查类型(规则数目)";
            }

            TreeListColumn col = treeListRule.Columns.Add();
            col.Caption = columnCaption;
            col.Name = "TypeName";
            col.FieldName = "TypeName";
            col.Visible = true;
            col.OptionsColumn.AllowEdit = false;


            col = treeListRule.Columns.Add();
            col.Caption = "规则名称";
            col.Name = "RuleName";
            col.FieldName = "RuleName";
            col.Visible = false;
            col.OptionsColumn.AllowEdit = false;


            col = treeListRule.Columns.Add();
            col.Caption = "规则类型编码";
            col.Name = "ruleBM";
            col.FieldName = "ruleBM";
            col.Visible = false;
            col.OptionsColumn.AllowEdit = false;

            col = treeListRule.Columns.Add();
            col.Caption = "规则数";
            col.Name = "rulesCount";
            col.FieldName = "rulesCount";
            col.Visible = false;
            col.OptionsColumn.AllowEdit = false;

            if (!this.radioType.Visible)
            {
                this.treeListRule.Top = this.radioType.Top;
                this.treeListRule.Height += this.radioType.Height - 3;
            }
            else
            {
                this.treeListRule.Top =this.radioType.Height;
                this.treeListRule.Height -= this.radioType.Height - 3;
            }
        }

        /// <summary>
        /// Loads the rules tree.
        /// </summary>
        public void LoadRulesTree()
        {
            this.treeListRule.BeginUnboundLoad();
            this.treeListRule.BeginUpdate();
            this.treeListRule.ClearNodes();
            try
            {
                if (m_CurrTemplateRules == null)
                {
                    m_CurrTemplateRules = new TemplateRules(CurrentSchemaId);
                }
                RulesDt = m_CurrTemplateRules.CurrentSchemaRulesDt;
                m_TempScheamRules = m_CurrTemplateRules.CurrentSchemaRules;
                bool NodeChecked = false;

                //创建一个副本
                if (TreeShowType == RuleTreeShowType.ViewRulesAndSelection)
                {
                    //创建一个副本
                    if (RulesSelection == null)
                    {
                        SchemaRuleEx[] ex = new SchemaRuleEx[m_TempScheamRules.Count];
                        m_CurrTemplateRules.CurrentSchemaRules.CopyTo(ex, 0);
                        RulesSelection = new List<SchemaRuleEx>();
                        RulesSelection.AddRange(ex);
                    }
                }
                //增加根节点
                int count = m_CurrTemplateRules.CalculateRulesCount(m_CurrTemplateRules.ClassifyRules);
                TreeListNode rootNode = treeListRule.AppendNode(new object[] { string.Format("{0} ({1})", CurrentTaskName, count), 0, "0", count }, null);
                //rootNode.StateImageIndex = 0;

                if (RulesSelection != null)
                {
                    SetRulesHistoryState(Convert.ToInt32(rootNode["ruleBM"]), RulesSelection);
                }
                //GetRulesHistoryState(rootNode["RuleName"].ToString(), ref NodeChecked);

                rootNode.Checked = RulesSelection == null ? treeListRule.OptionsView.ShowCheckBoxes : NodeChecked;
              
                //SetRulesHistoryState(rootNode["RuleName"].ToString(), rootNode.Checked);
                
                //增加一级、二级节点
                AppendTreeNodes(m_CurrTemplateRules.ClassifyRules, rootNode);
            }
            catch
            {
            }
            finally
            {
                treeListRule.Columns[0].Width = 280;
                //treeListRule.BestFitColumns();
                this.treeListRule.EndUpdate();
                this.treeListRule.EndUnboundLoad();
                this.treeListRule.ExpandAll();
            }
        }

        /// <summary>
        /// Loads the results tree.
        /// </summary>
        public void LoadResultsTree()
        {
            this.treeListRule.BeginUnboundLoad();
            this.treeListRule.BeginUpdate();
            this.treeListRule.ClearNodes();
            try
            {
                if (m_CurrTemplateRules == null)
                {
                    m_CurrTemplateRules = new TemplateRules(CurrentSchemaId);
                    RulesDt = m_CurrTemplateRules.CurrentSchemaRulesDt;
                }

                //增加根节点
                TreeListNode rootNode = treeListRule.AppendNode(new object[] { string.Format("{0} ({1})", CurrentTaskName, this.CheckResultsCount), 0, "0", this.CheckResultsCount }, null);
                //rootNode.StateImageIndex = 0;
                rootNode.Checked =treeListRule.OptionsView.ShowCheckBoxes;
                if (m_RuleType == RuleShowType.DefualtType)
                {
                    //增加一级、二级节点
                    AppendResultsNodes(m_CurrTemplateRules.ClassifyRules, rootNode);
                }
                else
                {
                    AppendLayersResultNodes(rootNode);
                }
            }
            catch
            {
            }
            finally
            {
                this.treeListRule.EndUpdate();
                this.treeListRule.EndUnboundLoad();
                this.treeListRule.ExpandAll();
                treeListRule.BestFitColumns();
            }
        }

        private void AppendTreeNodes(List<ClassifyRule> rules,TreeListNode parentNode)
        {
            foreach (ClassifyRule rule in rules)
            {
                string str = string.Format("{0} ({1})", rule.ruleName, rule.SubRulesCount);
                TreeListNode node = treeListRule.AppendNode(new object[] { str, rule.ruleName, rule.ruleBm, rule.SubRulesCount }, parentNode);

                bool NodeChecked = SetRulesHistoryState(Convert.ToInt32(node["ruleBM"]), RulesSelection);

                //GetRulesHistoryState(node["RuleName"].ToString(), ref NodeChecked);
                node.Checked = RulesSelection == null ? treeListRule.OptionsView.ShowCheckBoxes :NodeChecked;
                node.ParentNode.Checked = node.Checked;
                //SetRulesHistoryState(node["RuleName"].ToString(), node.Checked);
                AppendTreeNodes(rule.SubRules, node);
            }
        }

        private void AppendTreeNodes(TreeList treeList,List<ClassifyRule> rules, TreeListNode parentNode,string ruleName)
        {
            foreach (ClassifyRule rule in rules)
            {
                if (!string.IsNullOrEmpty(ruleName)&&rule.ruleName == ruleName)
                {
                    TreeListNode node = treeList.AppendNode(new object[] { rule.ruleName, rule.SubRulesCount }, parentNode);
                    AppendTreeNodes(treeList, rule.SubRules, node);
                }
            }
        }

        private void AppendTreeNodes(TreeList treeList, List<ClassifyRule> rules, TreeListNode parentNode)
        {
            if (rules.Count == 0) return;

            foreach (ClassifyRule rule in rules)
            {
                TreeListNode node = treeList.AppendNode(new object[] { rule.ruleName, rule.SubRulesCount }, parentNode);
                node.Checked = treeList.OptionsView.ShowCheckBoxes;
                //if (rule.SubRules.Count==0)
                //{
                //    AddTemplateRuleToTree(treeList, rule.ruleName, node);
                //}
                AppendTreeNodes(treeList,rule.SubRules, node);
            }
        }

        private object  GetSubRulesSource(TreeListNode node)
        {
            TreeList tempList = null;
            if (node.Level==0)
            {
                tempList=new TreeList();
                TreeListColumn col = tempList.Columns.Add();
                col.Caption = "规则分类";
                col.Name = "TypeName";
                col.FieldName = "TypeName";
                col.Visible = true;
                col.OptionsColumn.AllowEdit = false;

                col = tempList.Columns.Add();
                col.Caption = "规则个数";
                col.Name = "RuleCount";
                col.FieldName = "RuleCount";
                col.Visible = true;
                col.OptionsColumn.AllowEdit = false;

                TreeListNode rootNode = tempList.AppendNode(new object[] { "root", "0"}, null);
                //rootNode.StateImageIndex = 0;
                AppendTreeNodes(tempList, m_CurrTemplateRules.ClassifyRules, rootNode);
                return rootNode;
            }
            else if (node.HasChildren)
            {
                tempList = new TreeList();
                TreeListColumn col = tempList.Columns.Add();
                col.Caption = "规则分类";
                col.Name = "TypeName";
                col.FieldName = "TypeName";
                col.Visible = true;
                col.OptionsColumn.AllowEdit = false;

                col = tempList.Columns.Add();
                col.Caption = "规则个数";
                col.Name = "RuleCount";
                col.FieldName = "RuleCount";
                col.Visible = true;
                col.OptionsColumn.AllowEdit = false;
                //增加一级、二级节点
                AppendTreeNodes(tempList, m_CurrTemplateRules.ClassifyRules, null, node["RuleName"].ToString());
                return tempList.Nodes.FirstNode;
            }
            else
            {
                if (RulesDt == null || RulesDt.Rows.Count == 0) return null;

                DataRow[] drs = RulesDt.Select(string.Format("ChkTypeName='{0}'", node["RuleName"]));
                if (drs == null || drs.Length == 0)
                {
                    return null;
                }

                DataTable tempTable = new DataTable();
                tempTable = drs[0].Table.Clone();
                foreach (DataRow dr in drs)
                {
                    tempTable.ImportRow(dr);
                }
                return tempTable;
            }
        }

        private void AddTemplateRuleToTree(TreeList treList, string ruleName, TreeListNode parentNode)
        {
            if (RulesDt == null || RulesDt.Rows.Count == 0) return;

            DataRow[] drs = RulesDt.Select(string.Format("ChkTypeName='{0}'", ruleName));
            if (drs == null || drs.Length == 0)
            {
                return;
            }
            foreach (DataRow dr in drs)
            {
                treList.AppendNode(new object[] { dr["ChkTypeName"], 1 }, parentNode);
            }
            return;
        }

        private void treeListRule_Click(object sender, EventArgs e)
        {
            if (TreeShowType == RuleTreeShowType.ViewRulesAndSelection) return;

            if (this.treeListRule.Selection.Count > 0)
            {
                TreeListNode node = treeListRule.Selection[0];
                TreeRulesEventArgs ev = new TreeRulesEventArgs();
                if (TreeListNodeIsSelected != null)
                {
                    if (TreeShowType == RuleTreeShowType.ViewRules)
                    {
                        ev.RulesCount = int.Parse(node["rulesCount"].ToString());
                        ev.RuleTypeName = node["TypeName"].ToString();
                        ev.SubRules = GetSubRulesSource(node);
                        if (node.HasChildren)
                        {
                            ev.BolHaveChildNode = true;
                        }
                    }
                    else
                    {
                        ev = new TreeRulesEventArgs();
                        ev.RulesCount = int.Parse(node["rulesCount"].ToString());
                        ev.RuleTypeName = node["TypeName"].ToString();
                        ev.SubRules = this.GetSubResultsSource(node);
                        if (node.HasChildren)
                        {
                            ev.BolHaveChildNode = true;
                        }
                    }
                    TreeListNodeIsSelected.Invoke(node["RuleName"].ToString(), ev);
                }
            }
        }

        private void radioType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioType.SelectedIndex == 0)
            {
                m_RuleType=RuleShowType.DefualtType;
            }
            else
            {
                m_RuleType = RuleShowType.LayerType;
            }
            LoadResultsTree();
        }

        private void AppendResultsNodes(List<ClassifyRule> rules, TreeListNode parentNode)
        {
            try
            {
                foreach (ClassifyRule rule in rules)
                {
                    int count = 0;
                    if (rule.ruleBm.ToString().Length <= 2)
                    {
                        count = GetResultCount(rule.ruleBm.ToString().Length, rule.ruleBm); 
                    }
                    else
                    {
                        count=GetResultCount(rule.ruleName);
                    }
                    string str = string.Format("{0} ({1})", rule.ruleName, count);
                    TreeListNode node = treeListRule.AppendNode(new object[] { str, rule.ruleName, rule.ruleBm, count }, parentNode);
                    node.Checked = treeListRule.OptionsView.ShowCheckBoxes;
                    AppendResultsNodes(rule.SubRules, node);
                }
            }
            catch
            {

            }
        }

        private void AppendLayersResultNodes(TreeListNode parentNode)
        {
            string temp="";
            foreach (DataRow dr in LayersResultsDt.Rows)
            {
                if (temp.Equals(dr["TaretFeatClass1"].ToString()))
                {
                    string str = string.Format("{0} ({1})", dr["TaretFeatClass1"].ToString(), GetLayersResultCount(dr["TaretFeatClass1"].ToString()));
                    TreeListNode node = treeListRule.AppendNode(new object[] { str, dr["CheckType"].ToString(), dr["TaretFeatClass1"].ToString(), GetLayersResultCount(dr["TaretFeatClass1"].ToString()) }, parentNode);
                    node.Checked = treeListRule.OptionsView.ShowCheckBoxes;
                    AppendSubResultNode(dr["TaretFeatClass1"].ToString(), node);
                    temp = dr["TaretFeatClass1"].ToString();
                }
            }
        }

        private void AppendSubResultNode(string layerName, TreeListNode parentNode)
        {
            DataRow[] drs = LayersResultsDt.Select(string.Format("TaretFeatClass1='{0}'", layerName));

            if (drs == null || drs.Length == 0) return;

            foreach (DataRow dr in drs)
            {
                string str = string.Format("{0} ({1})", dr["CheckType"].ToString(),dr["ErrCount"].ToString());
                TreeListNode node = treeListRule.AppendNode(new object[] 
                           {str,dr["CheckType"].ToString(), dr["ErrCount"].ToString(), 
                       dr["RuleId"].ToString()}, parentNode);
                node.Checked = treeListRule.OptionsView.ShowCheckBoxes;
            }
        }
        

        private int GetResultCount(int length,int bm)
        {
            int count=0;
            DataRow[] drs = ResultsDt.Select(string.Format("substring(GZBM,1,{0})='{1}'", length, bm.ToString()));
            if (drs == null || drs.Length == 0) return count;
            foreach (DataRow dr in drs)
            {
                count += int.Parse(dr["ErrorCount"].ToString());
            }
            return count;
        }

        private int GetResultCount(string RuleName)
        {
            int count = 0;
            DataRow[] drs = ResultsDt.Select(string.Format("CheckType='{0}'", RuleName));
            if (drs == null || drs.Length == 0) return count;
            foreach (DataRow dr in drs)
            {
                count += int.Parse(dr["ErrorCount"].ToString());
            }
            return count;
        }


         private int GetLayersResultCount(string layerName)
        {
            int count = 0;
            DataRow[] drs = LayersResultsDt.Select(string.Format("TaretFeatClass1='{0}'", layerName));
            if (drs == null || drs.Length == 0) return count;
            foreach (DataRow dr in drs)
            {
                count += int.Parse(dr["ErrCount"].ToString());
            }
            return count;
        }

        private object GetSubResultsSource(TreeListNode node)
        {
            TreeList tempList = null;
            if (node.Level == 0)
            {
                tempList = new TreeList();
                TreeListColumn col = tempList.Columns.Add();
                col.Caption = "规则分类";
                col.Name = "TypeName";
                col.FieldName = "TypeName";
                col.Visible = true;
                col.OptionsColumn.AllowEdit = false;

                col = tempList.Columns.Add();
                col.Caption = "错误个数";
                col.Name = "ErrCount";
                col.FieldName = "ErrCount";
                col.Visible = true;
                col.OptionsColumn.AllowEdit = false;

                TreeListNode rootNode = tempList.AppendNode(new object[] { "root", "0" }, null);
                //rootNode.StateImageIndex = 0;
                AddNode(tempList,node.Nodes, rootNode);
                return rootNode;
            }
            else if (node.HasChildren)
            {
                tempList = new TreeList();
                TreeListColumn col = tempList.Columns.Add();
                col.Caption = "规则分类";
                col.Name = "TypeName";
                col.FieldName = "TypeName";
                col.Visible = true;
                col.OptionsColumn.AllowEdit = false;

                col = tempList.Columns.Add();
                col.Caption = "错误个数";
                col.Name = "ErrCount";
                col.FieldName = "ErrCount";
                col.Visible = true;
                col.OptionsColumn.AllowEdit = false;
                //增加一级、二级节点
                TreeListNode rootNode = tempList.AppendNode(new object[] { node["RuleName"], node["rulesCount"] }, null);
                //rootNode.StateImageIndex = 0;
                AddNode(tempList, node.Nodes, rootNode);
                return tempList.Nodes.FirstNode;
            }
            else
            {
                if (m_RuleType == RuleShowType.DefualtType)
                {
                    if (ResultsDt == null || ResultsDt.Rows.Count == 0) return null;

                    DataTable tempTable = new DataTable();

                    DataRow[] drs = ResultsDt.Select(string.Format("CheckType='{0}'", node["RuleName"]));
                    if (drs == null || drs.Length == 0)
                    {
                        tempTable = ResultsDt.Clone();
                        return tempTable;
                    }


                    tempTable = drs[0].Table.Clone();
                    foreach (DataRow dr in drs)
                    {
                        tempTable.ImportRow(dr);
                    }
                    return tempTable;
                }
                else
                {
                    if (LayersResultsDt == null || LayersResultsDt.Rows.Count == 0) return null;
                    DataRow[] drs = LayersResultsDt.Select(string.Format("targetfeatclass1='{0}'", node["CheckType"]));
                    if (drs == null || drs.Length == 0)
                    {
                        return null;
                    }

                    DataTable tempTable = new DataTable();
                    tempTable = drs[0].Table.Clone();
                    foreach (DataRow dr in drs)
                    {
                        tempTable.ImportRow(dr);
                    }
                    return tempTable;
                }
            }
        }

        private void AddNode(TreeList tree, TreeListNodes nodeSource, TreeListNode nodeParent)
        {
            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode nodeSub in nodeSource)
            {
                object[] objValues = new object[2];
                objValues[0] = nodeSub.GetValue("RuleName");
                objValues[1] = nodeSub.GetValue("rulesCount");
                TreeListNode nodeCurrent = tree.AppendNode(objValues, nodeParent);
                if (nodeSub.HasChildren)
                {
                    AddNode(tree, nodeSub.Nodes, nodeCurrent);
                }
            }
        }

        public DataTable GetError(int pageIndex)
        {
            return null;
        }

        private void UCRulesTree_Resize(object sender, EventArgs e)
        {
            if (this.treeListRule == null) return;
            if (this.treeListRule.Columns.Count > 0)
            {
                this.treeListRule.Columns[0].Width = this.Width;
            }
        }

        private void treeListRule_BeforeCheckNode(object sender, CheckNodeEventArgs e)
        {
            if (TreeShowType != RuleTreeShowType.ViewRulesAndSelection) return;

            e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
        }

        private void treeListRule_AfterCheckNode(object sender, NodeEventArgs e)
        {
            if (TreeShowType != RuleTreeShowType.ViewRulesAndSelection) return;

            if (TreeShowType == RuleTreeShowType.ViewRuleErrors)
            {
                if (e.Node.CheckState == CheckState.Checked)
                {
                    e.Node.CheckState = CheckState.Unchecked;
                }
                else
                {
                    e.Node.CheckState = CheckState.Checked;
                }
                return;
            }
            SetCheckedChildNodes(e.Node, e.Node.CheckState);

            SetCheckedParentNodes(e.Node, e.Node.CheckState);

            SetRules(e.Node, e.Node.CheckState);
            //SetRulesHistoryState(e.Node["RuleName"].ToString(), e.Node.Checked);

            if (!e.Node.Checked)
            {
                if (TreeNodeCheckStateChanged != null)
                {
                    TreeNodeCheckStateChanged(false);
                }
            }
            else
            {
                bool bol = IsAllChecked(e.Node.TreeList.Nodes);
                if (TreeNodeCheckStateChanged != null)
                {
                    TreeNodeCheckStateChanged(bol);
                }
            }
        }

        private bool IsAllChecked(TreeListNodes node)
        {
            bool bol = true;

            foreach (TreeListNode subNode in node)
            {
                if (!subNode.Checked)
                {
                    bol = false;
                    return bol;
                }
                else
                {
                    bol = IsAllChecked(subNode.Nodes);
                    if (!bol)
                    {
                        return bol;
                    }
                }
            }
            return bol;
        }

        private void SetCheckedChildNodes(TreeListNode node, CheckState checkState)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState =checkState;
                //SetRulesHistoryState(node.Nodes[i]["RuleName"].ToString(), node.Nodes[i].Checked);
                SetRules(node.Nodes[i], node.Nodes[i].CheckState);
                SetCheckedChildNodes(node.Nodes[i], checkState);
            }
        }

        private void SetCheckedParentNodes(TreeListNode node, CheckState checkState)
        {
            if (node.ParentNode != null)
            {
                bool b = false;
                CheckState state;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    state = (CheckState)node.ParentNode.Nodes[i].CheckState;
                    if (!checkState.Equals(state))
                    {
                        b = !b;
                        break;
                    }
                }
                node.ParentNode.CheckState = b ? CheckState.Indeterminate : checkState;
                //SetRulesHistoryState(node.ParentNode["RuleName"].ToString(), node.ParentNode.Checked);

                SetCheckedParentNodes(node.ParentNode, checkState);

            }
        }

        /// <summary>
        /// 设置哪些规则被选择
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="Hy.Check">The Hy.Check.</param>
        private void SetRules(TreeListNode node,CheckState  checkState)
        {
            int nodeBm=Convert.ToInt32(node["ruleBM"]);
            if (checkState == CheckState.Checked)
            {
                if (GetRulesByBM(RulesSelection, nodeBm).Count ==0)
                {
                    RulesSelection.AddRange(GetRulesByBM(m_TempScheamRules, nodeBm));
                }
            }
            else if (checkState == CheckState.Unchecked)
            {
                if (GetRulesByBM(RulesSelection, nodeBm).Count >0)
                {
                    foreach (SchemaRuleEx rule in GetRulesByBM(RulesSelection, nodeBm))
                    {
                        RulesSelection.Remove(rule);
                    }
                }
            }
        }

        private List<SchemaRuleEx> GetRulesByBM(List<SchemaRuleEx> levels, int bm)
        {
            List<SchemaRuleEx> rules = new List<SchemaRuleEx>();
            if (bm.ToString().Length == 1)
            {
                rules = levels.FindAll(delegate(SchemaRuleEx T) { return T.FirstClassificationBM== bm;});
            }
            else if (bm.ToString().Length == 2)
            {
                rules = levels.FindAll(delegate(SchemaRuleEx T) { return T.SecondeClassificationBM== bm;});
            }
            else
            {
                rules = levels.FindAll(delegate(SchemaRuleEx T) { return T.ThridClassificationBM == bm; });
            }
            return rules;
        }

        private bool SetRulesHistoryState(int bm, List<SchemaRuleEx> levels)
        {
            if (levels == null || levels.Count == 0) return true;
            bool bolState = false;

            int iLength=bm.ToString().Length;
            int temp;
            switch(iLength)
            {
                case 1:
                        temp=Convert.ToInt32(bm.ToString().Substring(0,1));
                        bolState = levels.Exists(delegate(SchemaRuleEx T) { return T.FirstClassificationBM == temp; });
                    break;
                case 2:
                        temp=Convert.ToInt32(bm.ToString().Substring(0,2));
                        bolState = levels.Exists(delegate(SchemaRuleEx T) { return T.SecondeClassificationBM == temp; });
                    break;
                case 3:
                        temp=Convert.ToInt32(bm.ToString().Substring(0,3));
                        bolState = levels.Exists(delegate(SchemaRuleEx T) { return T.ThridClassificationBM == temp; });
                    break;
            }
            return bolState;
        }

        //private bool GetRulesHistoryState(string name,ref bool nodeChecked)
        //{
        //    if (m_RulesSelectionHistoryState == null || m_RulesSelectionHistoryState.Count == 0) return false;
        //    if (m_RulesSelectionHistoryState.ContainsKey(name))
        //    {
        //        nodeChecked = m_RulesSelectionHistoryState[name];
        //    }
        //    return true;
        //}

        private bool SetRulesHistoryState(string name, bool nodeChecked)
        {
            if (m_RulesSelectionHistoryState == null )
            {
                m_RulesSelectionHistoryState=new Dictionary<string,bool>();
            }
            if (m_RulesSelectionHistoryState.ContainsKey(name))
            {
                m_RulesSelectionHistoryState[name] = nodeChecked;
            }
            else
            {
                m_RulesSelectionHistoryState.Add(name, nodeChecked);
            }
            return true;
        }
    }

    /// <summary>
    /// EventArgs
    /// </summary>
    public class TreeRulesEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the name of the rule.
        /// </summary>
        /// <value>The name of the rule.</value>
        public string RuleTypeName { get; set; }

        /// <summary>
        /// Gets or sets the rules count.
        /// </summary>
        /// <value>The rules count.</value>
        public  int RulesCount{get;set;}

        /// <summary>
        /// Gets or sets the sub rules.
        /// </summary>
        /// <value>The sub rules.</value>
        public object SubRules { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [bol have child node].
        /// </summary>
        /// <value><c>true</c> if [bol have child node]; otherwise, <c>false</c>.</value>
        public bool BolHaveChildNode { get; set; }

    }
}