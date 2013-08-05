using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;
using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandHotPoint:Skyline.Define.SkylineBaseCommand
    {
        public CommandHotPoint()
        {
            this.m_Category = "飞行浏览";
            this.m_Caption = "热点设置";

            this.m_Message = "热点设置";
            this.m_Tooltip = "热点设置";
        }

        public override void OnClick()
        {
            try
            {
                FrmHotDot fhd = new FrmHotDot(base.m_Hook.UIHook.MainForm);
                if (!fhd.IsDisposed)
                {
                    base.m_Hook.UIHook.MainForm.AddOwnedForm(fhd);
                    fhd.Show();
                }
            }
            catch
            {

                MessageBox.Show("发生错误!");

            }
        }
    }
}
