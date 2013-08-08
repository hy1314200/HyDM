using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public  class CommandOpenFly:Skyline.Define.SkylineBaseCommand
    {

        public CommandOpenFly()
        {
            this.m_Category = "三维浏览";
            this.m_Caption = "打开";

            this.m_Message = "打开Fly文件";
            this.m_Tooltip = "点击选择打开一个Fly文件";

            m_DlgOpen.Filter = "*.fly|*.fly";
        }

        public override bool Enabled
        {
            get
            {
                return m_Hook != null && m_SkylineHook != null && m_SkylineHook.TerraExplorer != null;
            }
        }

        private OpenFileDialog m_DlgOpen = new OpenFileDialog();
        public override void OnClick()
        {
            if (m_DlgOpen.ShowDialog() == DialogResult.OK)
            {
                this.m_SkylineHook.TerraExplorer.Load(m_DlgOpen.FileName);
            }
        }
    }
}
