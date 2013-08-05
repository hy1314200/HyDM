using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;
using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandPathDefine:Skyline.Define.SkylineBaseCommand
    {
        public CommandPathDefine()
        {
            this.m_Category = "飞行浏览";
            this.m_Caption = "自定义路径";

            this.m_Message = "自定义飞行路径";
            this.m_Tooltip = "自定义飞行路径";
        }

        public override void OnClick()
        {
            try
            {
                FrmCustomPath fcp = new FrmCustomPath(base.m_Hook.UIHook.MainForm);
                base.m_Hook.UIHook.MainForm.AddOwnedForm(fcp);
                fcp.Show();
            }
            catch
            {
                MessageBox.Show("发生错误!");
            }
        }
    }
}
