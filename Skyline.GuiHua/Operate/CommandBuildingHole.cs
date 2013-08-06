using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.GuiHua.Bussiness;
using System.Windows.Forms;
using System.IO;
using Skyline.Define;

namespace Skyline.GuiHua.Operate
{
    public class CommandBuildingHole:Common.Operate.CommandDockable
    {
        public CommandBuildingHole()
        {
            this.m_Caption = "地基开挖分析";

            this.m_Message = "地基开挖分析";
            this.m_Tooltip = "地基开挖分析";
        }

        protected ISkylineHook m_SkylineHook;
        public override bool Enabled
        {
            get
            {
              return  (m_SkylineHook != null && m_SkylineHook.SGWorld != null && !string.IsNullOrEmpty(m_SkylineHook.SGWorld.Project.Name));
            }
        }

        public override void OnCreate(object Hook)
        {
            base.OnCreate(Hook);
            this.m_SkylineHook = base.m_Hook.Hook as ISkylineHook;
        }
        
        private UCHoleResult m_UcHole;       

        protected override Control CreateControl()
        {
            if (m_UcHole == null)
            {
                PipeAnalysis.Instance.Hook = m_SkylineHook.SGWorld;
                PipeAnalysis.Instance.TE = m_SkylineHook.TerraExplorer;
                m_UcHole = new UCHoleResult();

                PipeAnalysis.Instance.Analysised += delegate
                {
                    this.Init();
                };
            }
            return m_UcHole;
        }

        protected override global::Define.enumDockPosition DockPosition
        {
            get { return global::Define.enumDockPosition.Right; }
        }

        protected override void Init()
        {
            m_UcHole.AnalysisResult = PipeAnalysis.Instance;
        }

        public override void OnClick()
        {
            base.OnClick();
            if (this.Checked)
            {
                PipeAnalysis.Instance.StartPipeAnalysis();
            }
            else
            {
                PipeAnalysis.Instance.EndPipeAnalysis();
            }
        }
    }
}
