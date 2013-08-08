using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;

namespace Skyline.GuiHua.Bussiness
{

    public partial class UCPorject : UserControl
    {
        public UCPorject()
        {
            InitializeComponent();
        }

        private List<ProjectInfo> m_Datasource;
        public List<ProjectInfo> Datasource
        {
            set
            {
                if (value == null)
                    return;

                m_Datasource = value;
                this.tlProject.ClearNodes();
                foreach (ProjectInfo pInfo in value)
                {
                    if (pInfo == null)
                        continue;
                 
                    TreeListNode nodeNew = this.tlProject.AppendNode(new object[] { pInfo.Name, pInfo.Type, pInfo.Enterprise, pInfo.Address }, null, pInfo);
                }
            }
        }

        public event ProjectEventHandle FocusedProjectChanged;
        private ProjectInfo m_CurrentProject;
        public ProjectInfo CurrentProject
        {
            get
            {
                return m_CurrentProject;
            }
        }
        private void tlProject_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            m_CurrentProject = null;
            if (this.tlProject.FocusedNode != null)
                m_CurrentProject = this.tlProject.FocusedNode.Tag as ProjectInfo;

            if (this.FocusedProjectChanged != null)
                this.FocusedProjectChanged.Invoke(m_CurrentProject);
        }

    }
}
