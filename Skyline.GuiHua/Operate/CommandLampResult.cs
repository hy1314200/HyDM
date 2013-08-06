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
    public class CommandLampResult:Common.Operate.CommandDockable
    {
        public CommandLampResult()
        {
            this.m_Caption = "信号灯自动化结果";

            this.m_Message = "信号灯自动化结果";
            this.m_Tooltip = "显示或关闭信号灯结果窗口";
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
        
        private UcLampAnalysisResult m_UcLampResult;       

        protected override Control CreateControl()
        {
            if (m_UcLampResult == null)
            {
                m_UcLampResult = new UcLampAnalysisResult();
                m_UcLampResult.Hook = m_SkylineHook.SGWorld;
                LampAnalysisResult.Instance.LampAnalysised += delegate
                {
                    m_UcLampResult.Hook = Program.sgworld;
                    m_UcLampResult.AnalysisResult = LampAnalysisResult.Instance;
                };
            }
            return m_UcLampResult;
        }

        protected override global::Define.enumDockPosition DockPosition
        {
            get { return global::Define.enumDockPosition.Right; }
        }

        protected override void Init()
        {
            m_UcLampResult.AnalysisResult = LampAnalysisResult.Instance;
        }
    }
}
