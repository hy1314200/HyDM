using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.GuiHua.Bussiness;
using System.Windows.Forms;
using System.IO;

namespace Skyline.GuiHua.Operate
{
    public class CommandProjectProperty:GuiHuaBaseCommand
    {
        public CommandProjectProperty()
        {
            this.m_Caption = "项目属性";

            this.m_Message = "查看项目属性";
            this.m_Tooltip = "查看当前项目属性";
        }

        public override void OnClick()
        {
            try
            {
                if (Bussiness.Environment.m_Project != null)
                {
                    FrmProjectProperty frmProperty = new FrmProjectProperty();
                    frmProperty.Project = Bussiness.Environment.m_Project;
                    frmProperty.ShowDialog(this.m_Hook.UIHook.MainForm);
                }
            }
            catch
            {
                MessageBox.Show("发生错误!");
            }
        }
    }
}
