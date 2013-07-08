using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Define;
using Utility;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using Frame.Define;

namespace Frame
{
    public partial class FrmConfig : Form
    {
        public FrmConfig()
        {
            InitializeComponent();

            InitResource();

            InitRibbon();
        }

        INhibernateHelper m_HibernateHelper = Environment.NHibernateHelper;
        List<ClassInfo> m_ResourceList = null;

        /// <summary>
        /// 初始化资源管理
        /// </summary>
        private void InitResource()
        {
            lbPlugins.Items.Clear();

            IList<ClassInfo> listResource = m_HibernateHelper.GetObjectsByCondition<ClassInfo>("from ClassInfo cInfo order by Category,Description asc");
            this.ucCommandCategory1.ClassInfos = listResource;
            this.ucCommandCategory2.ClassInfos = listResource;
            int count = listResource.Count;
            for (int i = 0; i < count; i++)
            {
                ClassInfo cInfo = listResource[i];
                if (cInfo == null)
                    continue;

                if (cInfo.Type == enumResourceType.Plugin)
                {
                    lbPlugins.Items.Add(cInfo);
                }
                //else
                //{
                //    lbCommands.Items.Add(cInfo);
                //    lbPreCommands.Items.Add(cInfo);
                //}
            }

        }


        List<RibbonCommandInfo> m_CommandList = null;
        private void InitRibbon()
        {
            IList<RibbonCommandInfo> listCommand = m_HibernateHelper.GetObjectsByCondition<RibbonCommandInfo>("from RibbonCommandInfo rcInfo order by Order asc");//from RibbonCommandInfo rcInfo order by Page,PageGroup asc");
            int count = listCommand.Count;

            //m_CommandList = new List<RibbonCommandInfo>();
            //for (int i = 0; i < count; i++)
            //{
            //    m_CommandList.Add(listCommand[i] as RibbonCommandInfo);
            //}
            m_CommandList = listCommand as List<RibbonCommandInfo>;

            // 工具栏预览
            this.applicationMenu1.ItemLinks.Clear();
            this.ribbonControl1.Items.Clear();
            this.ribbonControl1.Pages.Clear();
            this.ribbonControl1.StatusBar.ItemLinks.Clear();
            this.ribbonControl1.PageHeaderItemLinks.Clear();
            this.ribbonControl1.Toolbar.ItemLinks.Clear();
            ribbonControl1.Pages.Clear();
            RibbonEngine ribbonEngine = new RibbonEngine();
            ribbonEngine.CommandInfoList = m_CommandList;
            ribbonEngine.Ribbon = this.ribbonControl1;
            ribbonEngine.MainMenu = applicationMenu1;
            ribbonEngine.CommandInfoFinder=delegate(string resourceID)
            {
                return m_CommandList.Find(delegate(RibbonCommandInfo cmdInfo) { return cmdInfo.ID == resourceID; });
            };
            List<ICommand > cmdList=new List<ICommand>();

            ribbonEngine.Load(ref cmdList);
            

            // 树
            tlRibbon.Nodes.Clear();
            string strPage = null;
            string strGroup = null;
            Dictionary<string, TreeListNode> dicNode = new Dictionary<string, TreeListNode>();
            TreeListNode nodePage = null;
            TreeListNode nodeGroup = null;
            TreeListNode nodeParent = null;
            int pageOrder = 1;
            int groupOrder = 1;
            int cmdOrder = 1;
            for(int i=0;i<count;i++)
            {
                RibbonCommandInfo cmdInfo = m_CommandList[i];
                //if (cmdInfo.Type != enumCommandType.Normal)
                //    continue;
                if (cmdInfo.Parent != null)
                {
                    // 必须排好序才能这么来
                    nodeParent = dicNode[cmdInfo.Parent.ID];
                }
                else
                {
                    if (strPage != cmdInfo.Page)
                    {
                        strPage = cmdInfo.Page;
                        nodePage = tlRibbon.AppendNode(new object[] { strPage, null, null, pageOrder++ }, null);
                        pageOrder = 1;
                    }

                    if (strGroup != cmdInfo.PageGroup)
                    {
                        strGroup = cmdInfo.PageGroup;
                        nodeGroup = tlRibbon.AppendNode(new object[] { strGroup, null, null, groupOrder++ }, nodePage);
                        cmdOrder = 1;
                    }
                    nodeParent = nodeGroup;
                }

                TreeListNode nodeCommand = tlRibbon.AppendNode(new object[] { cmdInfo.Caption, cmdInfo.Icon, cmdInfo.CommandClass, cmdOrder++, m_TypeNames[(int)cmdInfo.Type] }, nodeParent);
                nodeCommand.Tag = cmdInfo;

                dicNode.Add(cmdInfo.ID, nodeCommand);
            }
        }
        private List<string> m_TypeNames =new List<string>()
        {
            "工具栏",
            "快速访问栏",
            "应用菜单",
            "状态栏",
            "页头"
        };

        private int m_SelectedLevel = 0;
        private DevExpress.XtraTreeList.Nodes.TreeListNode m_PageNode;
        private DevExpress.XtraTreeList.Nodes.TreeListNode m_GroupNode;
        private DevExpress.XtraTreeList.Nodes.TreeListNode m_SelectedCommandNode;

        private void RefreshRibbonEnabled()
        {
            barBtnAddPage.Enabled = true;
            barBtnDeletePage.Enabled = (m_SelectedLevel == 1);

            barBtnAddGroup.Enabled = (m_SelectedLevel > 0);
            barBtnDeleteGroup.Enabled = (m_SelectedLevel == 2);
            barBtnAddCommand.Enabled = (m_SelectedLevel >1 && m_SelectedClassInfo != null);
            barBtnDeleteCommand.Enabled = (m_SelectedLevel == 3);

            // Up, Down

            TreeListNode selNode = tlRibbon.FocusedNode;
            if (selNode == null)
            {
                barBtnUp.Enabled = false;
                barBtnDown.Enabled = false;
                return;
            }
            int minOrder = 1;
            if (selNode.ParentNode!=null)
                minOrder = (int)selNode.ParentNode.FirstNode.GetValue(tlColOrder);

            int maxOrder = 1;
            if (selNode.ParentNode != null)
                maxOrder = (int)selNode.ParentNode.LastNode.GetValue(tlColOrder);
            else
                maxOrder = tlRibbon.Nodes.Count;

            int curOrder =(int) selNode.GetValue(tlColOrder);
            barBtnUp.Enabled = curOrder > minOrder;
            barBtnDown.Enabled = curOrder < maxOrder;
        }

        private void tlRibbon_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            DevExpress.XtraTreeList.Nodes.TreeListNode curNode=e.Node;
            m_SelectedLevel = 0;

            m_PageNode = null;
            m_GroupNode = null;
            m_SelectedCommandNode = null;

            if (curNode != null)
            {

                m_SelectedLevel = curNode.Level + 1;
                // Page
                if (m_SelectedLevel == 1)
                {
                    m_PageNode = curNode;
                }
                else
                if (m_SelectedLevel == 2)
                {
                    m_PageNode = curNode.ParentNode;
                    m_GroupNode = curNode;
                }
                else
                //if (m_SelectedLevel == 3)
                {
                    m_PageNode = curNode.ParentNode.ParentNode;
                    m_GroupNode = curNode.ParentNode;
                    m_SelectedCommandNode = curNode;

                    this.ucCommandCategory2.SelectedClassInfo = m_SelectedCommandNode.Tag as ClassInfo;
                }
            }
            RefreshRibbonEnabled();
        }

        private void AddCommand(object sender, EventArgs e)
        {
            if (m_SelectedClassInfo != null && m_GroupNode != null)
            {
                ICommand cmd = ResourceFactory.CreateCommand(m_SelectedClassInfo);
                TreeListNode nodeCommand = null;
                if (m_SelectedLevel == 3)
                {
                    nodeCommand = tlRibbon.AppendNode(new object[] { cmd.Caption, cmd.Icon, m_SelectedClassInfo, tlRibbon.FocusedNode.Nodes.Count + 1, m_TypeNames[0] }, tlRibbon.FocusedNode, new RibbonCommandInfo());
                }
                else
                {
                    nodeCommand = tlRibbon.AppendNode(new object[] { cmd.Caption, cmd.Icon, m_SelectedClassInfo, m_GroupNode.Nodes.Count + 1, m_TypeNames[0] }, m_GroupNode, new RibbonCommandInfo());
                }      
                //RibbonCommandInfo cmdInfo=new RibbonCommandInfo();
                //cmdInfo.Type = enumCommandType.Normal;
                //nodeCommand.Tag = cmdInfo;
                nodeCommand.ParentNode.Expanded = true;
            }
        }

        private void DeleteCommand(object sender, EventArgs e)
        {
            if (m_SelectedCommandNode != null)
            {
                //if (m_SelectedCommandNode.Tag is RibbonCommandInfo)
                //{
                //    m_HibernateHelper.DeleteObject(m_SelectedCommandNode.Tag);
                //    m_HibernateHelper.Flush();
                //}

                //tlRibbon.DeleteNode(m_SelectedCommandNode);

                //m_SelectedCommandNode = null;

                DeleteNode(m_SelectedCommandNode);
                m_HibernateHelper.Flush();
                tlRibbon.DeleteNode(m_SelectedCommandNode);
            }
        }

        private void DeleteNode(TreeListNode nodeTarget)
        {
            foreach (TreeListNode nodeSub in nodeTarget.Nodes)
            {
                DeleteNode(nodeSub);
            }
            if (nodeTarget.Tag is RibbonCommandInfo)
            {
                m_HibernateHelper.DeleteObject(nodeTarget.Tag);
            }
        }

        private void barBtnUnRegister_Click(object sender, EventArgs e)
        {
            m_HibernateHelper.DeleteObject(m_SelectedResource);
            m_HibernateHelper.Flush();
            InitResource();
        }

        private void barBtnRegister_Click(object sender, EventArgs e)
        {
            FrmResourceRegister frmRegister = new FrmResourceRegister();
            if (frmRegister.ShowDialog() == DialogResult.OK)
            {
                InitResource();
            }

        }

        private void barBtnAddGroup_Click(object sender, EventArgs e)
        {
            if (m_PageNode != null)
            {
                tlRibbon.AppendNode(new object[] { "新建组", null, null,m_PageNode.Nodes.Count+1 }, m_PageNode);
                m_PageNode.Expanded = true;
            }
        }

        private void barBtnDeletePage_Click(object sender, EventArgs e)
        {
            if (m_PageNode != null)
            {
                foreach (TreeListNode nodeGroup in m_PageNode.Nodes)
                {
                    foreach (TreeListNode nodeCommand in nodeGroup.Nodes)
                    {
                        DeleteNode(nodeCommand);
                        //if (nodeCommand.Tag is RibbonCommandInfo)
                        //    m_HibernateHelper.DeleteObject(nodeCommand.Tag);
                    }
                }
                m_HibernateHelper.Flush();

                tlRibbon.DeleteNode(m_PageNode);
                m_PageNode = null;
            }
        }

        private void DeleteGroup(object sender, EventArgs e)
        {
            if (m_GroupNode != null)
            {
                foreach (TreeListNode nodeCommand in m_GroupNode.Nodes)
                {
                    if (nodeCommand.Tag is RibbonCommandInfo)
                        m_HibernateHelper.DeleteObject(nodeCommand.Tag);
                }

                m_HibernateHelper.Flush();
                tlRibbon.DeleteNode(m_GroupNode);
            }
        }

        private void barBtnAddPage_Click(object sender, EventArgs e)
        {
            tlRibbon.AppendNode(new object[] { "新建页", null, null,tlRibbon.Nodes.Count+1 }, null);
        }


        private OpenFileDialog m_DialogIcon = new OpenFileDialog(){Filter=" *.Jpg|*.jpg| *.bmp|*.bmp| *.Png|*.png| *.Icon|*.icon"};
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (m_DialogIcon.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void barBtnSave_Click(object sender, EventArgs e)
        {
            m_SaveIndex = 0;
            int pageOrder = 0;
            foreach (TreeListNode nodePage in tlRibbon.Nodes)
            {
                pageOrder++;
                int groupOrder = 0;                
                foreach (TreeListNode nodeGroup in nodePage.Nodes)
                {
                    groupOrder++;
                    int cmdOrder = 0;
                    foreach (TreeListNode nodeCommand in nodeGroup.Nodes)
                    {
                        cmdOrder++;
                        SaveCommandNode(nodeCommand, nodePage, nodeGroup, pageOrder, groupOrder, cmdOrder);
                    }
                }
            }

            SendMessage("正在写入数据库...");
            m_HibernateHelper.Flush();
            SendMessage(string.Format("保存成功，共{0}个命令",m_SaveIndex));
        }
        private int m_SaveIndex = 0;
        private void SendMessage(string strMsg)
        {
            this.txtStatus.Text = strMsg;
            Application.DoEvents();
        }
        private void SaveCommandNode(TreeListNode nodeCommand, TreeListNode nodePage, TreeListNode nodeGroup, int pageOrder, int groupOrder, int cmdOrder)
        {
            SendMessage(string.Format("正在保存第{0}个命令...",++m_SaveIndex));
            RibbonCommandInfo rcInfo = null;
            if (nodeCommand.Tag is RibbonCommandInfo)
            {
                rcInfo = nodeCommand.Tag as RibbonCommandInfo;
            }
            else
            {
                rcInfo = new RibbonCommandInfo();
            }

            rcInfo.Page = nodePage.GetValue(tlColCaption) as string;
            rcInfo.PageGroup = nodeGroup.GetValue(tlColCaption) as string;
            rcInfo.Caption = nodeCommand.GetValue(tlColCaption) as string;
            rcInfo.CommandClass = nodeCommand.GetValue(tlColContent) as ClassInfo;
            object objIcon= nodeCommand.GetValue(tlColIcon);
            rcInfo.Icon =objIcon is byte[]?Image.FromStream(new System.IO.MemoryStream(objIcon as byte[])):objIcon as Image;
            rcInfo.Parent = nodeCommand.ParentNode.Tag as RibbonCommandInfo;// nodeCommand.ParentNode.Tag is RibbonCommandInfo ? nodeCommand.ParentNode.Tag as RibbonCommandInfo : null;
            if (rcInfo.Parent != null)
            {
                rcInfo.Order = rcInfo.Parent.Order + cmdOrder.ToString("00");
            }
            else
            {
                rcInfo.Order = pageOrder.ToString("00") + groupOrder.ToString("00") + cmdOrder.ToString("00");
            }
            int typeIndex=m_TypeNames.IndexOf(nodeCommand.GetValue(tlColCommandType) as string);
            rcInfo.Type = (enumCommandType)(typeIndex<0?0:typeIndex);

            //if (nodeCommand.Tag is RibbonCommandInfo)
            //{
            //    m_HibernateHelper.UpdateObject(rcInfo);

            //}
            //else
            //{
            //    m_HibernateHelper.SaveObject(rcInfo);
            //    nodeCommand.Tag = rcInfo;
            //}
            m_HibernateHelper.SaveObject(rcInfo);
            nodeCommand.Tag = rcInfo;
            //m_HibernateHelper.RefreshObject(rcInfo);
            for (int i = 0; i < nodeCommand.Nodes.Count;i++ )
            {
                SaveCommandNode(nodeCommand.Nodes[i], nodePage, nodeGroup, pageOrder, groupOrder, i+1);

            }
            //foreach (TreeListNode nodeSub in nodeCommand.Nodes)
            //{
            //    SaveCommandNode(nodeSub, nodePage, nodeGroup, pageOrder, groupOrder, cmdOrder);
            //}
        }

        private void barBtnRefresh_Click(object sender, EventArgs e)
        {
            InitRibbon();
        }

        private void barBtnUp_Click(object sender, EventArgs e)
        {
            TreeListNode selNode = tlRibbon.FocusedNode;
            if (selNode != null)
            {
                int curOrder = (int)selNode.GetValue(tlColOrder);
                selNode.SetValue(tlColOrder, curOrder - 1);
                selNode.PrevNode.SetValue(tlColOrder, curOrder);

                RefreshRibbonEnabled();
            }
        }

        private void barBtnDown_Click(object sender, EventArgs e)
        {
            TreeListNode selNode = tlRibbon.FocusedNode;
            if (selNode != null)
            {
                int curOrder = (int)selNode.GetValue(tlColOrder);
                selNode.SetValue(tlColOrder, curOrder + 1);
                selNode.NextNode.SetValue(tlColOrder, curOrder);

                RefreshRibbonEnabled();
            }
        }



        private ClassInfo m_SelectedClassInfo = null;
        private void ucCommandCategory2_SelectedClassInfoChanged(ClassInfo cInfo)
        {
            m_SelectedClassInfo = cInfo;
            RefreshRibbonEnabled();
        }

        private void ucCommandCategory2_ClassInfoDoubleClicked(ClassInfo cInfo)
        {
            AddCommand(null, null);
        }

        private ClassInfo m_SelectedResource = null;
        private void SelectedResoureceChanged(object sender, EventArgs e)
        {
            ClassInfo cInfo = (sender as DevExpress.XtraEditors.ListBoxControl).SelectedItem as ClassInfo;
            SelectedResourceChanged(cInfo);
        }
        private void SelectedResourceChanged(ClassInfo cInfo)
        {
            m_SelectedResource = cInfo;
            ucClassInfo1.ClassInfo = cInfo;
            barBtnUnRegister.Enabled = (m_SelectedResource != null); 
        }
    }
}
