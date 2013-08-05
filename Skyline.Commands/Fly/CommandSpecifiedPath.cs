using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;
using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandSpecifiedPath:Skyline.Define.SkylineBaseCommand
    {
        public CommandSpecifiedPath()
        {
            this.m_Category = "飞行浏览";
            this.m_Caption = "规定路径";

            this.m_Message = "规定路径";
            this.m_Tooltip = "规定路径";
        }

        public override void OnClick()
        {
            try
            {
            FrmStipulatePath fsp = new FrmStipulatePath(base.m_Hook.UIHook.MainForm);
                base.m_Hook.UIHook.MainForm.AddOwnedForm(fsp);
                if (!fsp.IsDisposed)
                {
                    fsp.Show();
                }
            }
            catch
            {
                MessageBox.Show("发生错误!");
            }
        }
    }
}
