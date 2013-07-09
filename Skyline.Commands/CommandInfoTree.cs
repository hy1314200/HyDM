using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandTocControl : Common.Operate.CommandDockable
    {
        public CommandTocControl()
        {
            this.m_Category = "视图控制";
            this.m_Caption = "图层目录";
            this.m_Message = "打开三维图层目录";
            this.m_Tooltip = "点击以打开或关闭三维图层目录";
        }
        private AxTerraExplorerX.AxTEInformationWindow m_InfoTree;
        protected override Control CreateControl()
        {
            return new AxTerraExplorerX.AxTEInformationWindow();
        }

        protected override enumDockPosition DockPosition
        {
            get
            {
                return enumDockPosition.Left;
            }
        }

        protected override void Init()
        {
        }

        public override string Message
        {
            get
            {
                this.m_Message = this.Checked ? "关闭三维图层目录" : "打开三维图层目录";
                return base.Message;
            }
        }
    }
}
