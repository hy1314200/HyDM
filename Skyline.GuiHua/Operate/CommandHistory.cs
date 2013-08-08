using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.GuiHua.Bussiness;
using System.Windows.Forms;
using System.IO;

namespace Skyline.GuiHua.Operate
{
    public class CommandHistory:GuiHuaBaseCommand
    {
        public CommandHistory()
        {
            this.m_Caption = "历史方案模型";

            this.m_Message = "历史方案模型";
            this.m_Tooltip = "查看历史方案模型";
        }

        public override void OnClick()
        {
            try
            {
                FrmModelVersion frmVersion = new FrmModelVersion(m_SkylineHook.SGWorld,Bussiness.Environment.m_Project.ID);
                frmVersion.Show(this.m_Hook.UIHook.MainForm);
            }
            catch
            {
                MessageBox.Show("发生错误!");
            }
        }
    }
}
