using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandAnalysisBuffer:Skyline.Define.SkylineBaseCommand
    {
        public CommandAnalysisBuffer()
        {
            this.m_Category = "三维分析";
            this.m_Caption = "缓冲区分析";

            this.m_Message = "缓冲区分析";
            this.m_Tooltip = "缓冲区分析";
        }

        public override void OnClick()
        {
            FrmBufferAnaylsis pFrmBuffer = new FrmBufferAnaylsis(base.m_Hook.UIHook.MainForm);
            pFrmBuffer.SgWorld = this.m_SkylineHook.SGWorld;
            pFrmBuffer.TerraExplorer = this.m_SkylineHook.TerraExplorer;
            try
            {
                if (!pFrmBuffer.IsDisposed)
                {
                    base.m_Hook.UIHook.MainForm.AddOwnedForm(pFrmBuffer);
                    pFrmBuffer.Show();
                }
            }
            catch
            {

                MessageBox.Show("发生错误!");

            }
        }
    }
}
