using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Define;
using Utility;
using DevExpress.XtraTreeList.Nodes;
using Frame.Define;

namespace Frame
{
    public partial class UCResourceRegister : UserControl
    {
        public UCResourceRegister()
        {
            InitializeComponent();

            m_NodeAll = tlResource.AppendNode(new object[] { "全选", null }, null);
        }

        private string m_DllName = null;
        private TreeListNode m_NodeAll = null;

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string strPath = openFileDialog1.FileName;
                m_DllName = System.IO.Path.GetFileName(strPath);

                Dictionary<string,enumResourceType> dicResource= ResourceFactory.GetResources(strPath);

                m_NodeAll.Nodes.Clear();
                int count = dicResource.Count;
                for (int i = 0; i < count; i++)
                {
                    ClassInfo cInfo = new ClassInfo();
                    cInfo.ClassName = dicResource.Keys.ElementAt(i);
                    cInfo.DllName = m_DllName;
                    cInfo.Type = dicResource.Values.ElementAt(i);

                    object objResource= ResourceFactory.CreateInstance(cInfo.DllName,cInfo.ClassName);
                    ICommand cmd = objResource as ICommand;
                    if (cmd != null)
                    {
                        cInfo.Category = cmd.Category;
                        cInfo.Description = cmd.Caption;
                    }
                    else
                    {
                        IPlugin plugin = objResource as IPlugin;
                        if (plugin != null)
                        {
                            cInfo.Category="插件";
                            cInfo.Description = plugin.Description;
                        }
                    }

                    TreeListNode nodeResource =
                        tlResource.AppendNode(new object[] { dicResource.Values.ElementAt(i) == enumResourceType.Command ? "命令" : "插件",string.IsNullOrWhiteSpace(cInfo.Description)? dicResource.Keys.ElementAt(i):cInfo.Description }, m_NodeAll);

                    nodeResource.Tag = cInfo;
                }
                m_NodeAll.ExpandAll();
            }
        }

        private void tlResource_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null)
                return;

            ucClassInfo1.ClassInfo = e.Node.Tag as ClassInfo;
        }

        private void tlResource_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (e.Node == m_NodeAll)
            {
                bool flag = e.Node.Checked;
                foreach (TreeListNode node in m_NodeAll.Nodes)
                {
                    node.Checked = flag;
                }
            }
        }

        public List<ClassInfo> SelectedClasses
        {
            get
            {
                List<ClassInfo> infoList = new List<ClassInfo>();
                foreach (TreeListNode node in m_NodeAll.Nodes)
                {
                    if (node.Checked)
                        infoList.Add(node.Tag as ClassInfo);
                }

                return infoList;
            }
        }

    }
}
