using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandAnalysisViewshed:SkylineBaseCommand
    {
        public CommandAnalysisViewshed()
        {
            this.m_Category = "三维分析";
            this.m_Caption = "可视域分析";

            this.m_Message = "可视域分析";
            this.m_Tooltip = "对指定点分析选定区域的可视性";
        }

        FrmViewshed m_FrmViewshed = null;
        public override void OnClick()
        {
            try
            {
                if (m_FrmViewshed == null || m_FrmViewshed.IsDisposed)
                {
                    m_FrmViewshed = new FrmViewshed(this.m_SkylineHook.SGWorld, this.m_SkylineHook.TerraExplorer);

                }
                //frmViewshed.ControlHook = uc3DWindow.teTopLeft;
                base.m_Hooker.MainForm.AddOwnedForm(m_FrmViewshed);
                m_FrmViewshed.Show();
            }
            catch
            {
                MessageBox.Show("发生错误!");
            }

        }
    }
}
