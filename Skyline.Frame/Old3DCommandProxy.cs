using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeDimension.BasicEngine;
using PlatDefine = Define;
using TerraExplorerX;
namespace Skyline.Frame
{
    internal class Old3DCommandProxy :PlatDefine.BaseCommand, PlatDefine.ICommandEx
    {
        private Skyline.Define.ISkylineHook m_SkylineHook;
        ThreeDimension.BasicEngine.ICommand m_OldCommand;

        static Application3D application3D;
        internal Old3DCommandProxy(ThreeDimension.BasicEngine.ICommand oldCommand)
        {
            this.m_OldCommand=oldCommand;
            
        }

        public override string Caption
        {
            get { return m_OldCommand.ToolAndCommandUI.Text; }
        }

        public override string Category
        {
            get { return "旧三维命令"; }
        }

        public override bool Checked
        {
            get { return false; }
        }

        public override bool Enabled
        {
            get
            {
                if (base.Enabled)
                {
                    return (m_SkylineHook != null && m_SkylineHook.SGWorld != null && !string.IsNullOrEmpty(m_SkylineHook.SGWorld.Project.Name));
                }
                return false;
            }
        }

        public override System.Drawing.Image Icon
        {
            get { return m_OldCommand.ToolAndCommandUI.Image; }
        }

        public override string Message
        {
            get { return m_OldCommand.StateMessage; }
        }

        public override string Name
        {
            get { return m_OldCommand.Name; }
        }

        public override void OnClick()
        {
        }

        public override void OnCreate(object Hook)
        {
            base.OnCreate(Hook);
            this.m_SkylineHook = base.m_Hook.Hook as Skyline.Define.ISkylineHook;

            // For Old Command
            if (application3D == null)
            {
                application3D = new Application3D(null);
                application3D.MainForm = this.m_Hook.UIHook.MainForm;
            }
            this.m_OldCommand.Application = application3D;
        }

        public override string Tooltip
        {
            get { return m_OldCommand.ToolAndCommandUI.TipText; }
        }

        public  object ExControl
        {
            get { return m_OldCommand.ToolAndCommandUI.GetUIObject(); }
        }
    }
}
