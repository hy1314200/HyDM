using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using System.Linq;

using Frame.Define;
using DevExpress.XtraTreeList.Nodes;

namespace Frame
{
    public partial class UCCommandCategory : DevExpress.XtraEditors.XtraUserControl
    {
        public UCCommandCategory()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 要求已经排序
        /// </summary>
        public IList<ClassInfo> ClassInfos
        {
            set
            {
                tlCommands.ClearNodes();
                if (value == null)
                    return;

                int count = value.Count;

                TreeListNode nodeCategory=null;
                string curCategory = null;
                for (int i = 0; i < count; i++)
                {
                    ClassInfo cInfo = value[i];
                    if (cInfo == null)
                        continue;

                    if (cInfo.Type == enumResourceType.Command)
                    {
                        if (curCategory != cInfo.Category)
                        {
                            nodeCategory = tlCommands.AppendNode(new object[] { cInfo.Category, null }, null, null);
                            curCategory = cInfo.Category;
                        }
                        tlCommands.AppendNode(new object[] { cInfo.Description, cInfo.ClassName }, nodeCategory, cInfo);
                    }
                }
            }
        }


        private ClassInfo m_SelectedClassInfo;
        public ClassInfo SelectedClassInfo
        {
            get
            {
                return m_SelectedClassInfo;
            }
            set
            {
                if (value == null)
                    return;

                foreach (TreeListNode nodeCategory in tlCommands.Nodes)
                {
                    foreach (TreeListNode nodeClassInfo in nodeCategory.Nodes)
                    {
                        ClassInfo cInfo = nodeClassInfo.Tag as ClassInfo;
                        if (cInfo != null && cInfo.ID == value.ID)
                        {
                            tlCommands.FocusedNode = nodeClassInfo;
                        }
                    }
                }
            }
        }

        public event ClassInfoEventHandler SelectedClassInfoChanged;
        public event ClassInfoEventHandler ClassInfoDoubleClicked;

        private void tlCommands_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null)
                m_SelectedClassInfo = null;
            else
                m_SelectedClassInfo = e.Node.Tag as ClassInfo;

            if (SelectedClassInfoChanged != null)
                SelectedClassInfoChanged.Invoke(m_SelectedClassInfo);

        }

        private void tlCommands_DoubleClick(object sender, EventArgs e)
        {
            if (ClassInfoDoubleClicked != null)
                ClassInfoDoubleClicked.Invoke(m_SelectedClassInfo);
        }
    }

    public delegate void ClassInfoEventHandler(ClassInfo cInfo);
}
