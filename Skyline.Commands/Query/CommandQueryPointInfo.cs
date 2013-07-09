using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;
using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandQueryPointInfo:SkylineBaseCommand
    {
        public CommandQueryPointInfo()
        {
            this.m_Category = "三维查询";
            this.m_Caption = "点信息";

            this.m_Message = "点信息查询";
            this.m_Tooltip = "在场景中查看鼠标所在点的坐标信息";
        }

        public override void OnClick()
        {
            FrmQueryCoordinate fqc = new FrmQueryCoordinate(base.m_Hook.UIHook.MainForm);
            try
            {
                if (!fqc.IsDisposed)
                {
                    base.m_Hook.UIHook.MainForm.AddOwnedForm(fqc);
                    fqc.Show();
                }
            }
            catch
            {

                MessageBox.Show("发生错误!");

            }
        }
    }
}
