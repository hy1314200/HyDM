using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.GuiHua.Bussiness;
using System.Windows.Forms;
using System.IO;

namespace Skyline.GuiHua.Operate
{
    public class CommandOpenProject:GuiHuaBaseCommand
    {
        public CommandOpenProject()
        {
            this.m_Caption = "打开项目";

            this.m_Message = "打开规划项目";
            this.m_Tooltip = "打开一个规划项目";
        }

        public override bool Enabled
        {
            get
            {
                return m_Hook != null && m_SkylineHook != null && m_SkylineHook.TerraExplorer != null;
            }
        }

        public override void OnClick()
        {
            FrmProjects frmProjects = new FrmProjects();
            if (frmProjects.ShowDialog() == DialogResult.OK)
            {

                Skyline.GuiHua.Bussiness.Environment.m_Project = frmProjects.SelectedProject;

                if (!File.Exists(Skyline.GuiHua.Bussiness.Environment.m_Project.File))
                {
                    MessageBox.Show("当前项目文件结构已被破坏：项目文件已不存在！");
                    return;
                }

                try
                {
                    Program.TE.Load(Skyline.GuiHua.Bussiness.Environment.m_Project.File);
                }
                catch
                {
                    MessageBox.Show("当前项目文件无法打开！");
                    return;
                }
            }
        }
    }
}
