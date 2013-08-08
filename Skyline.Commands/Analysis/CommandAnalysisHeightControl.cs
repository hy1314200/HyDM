using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Skyline.Core.UI;

namespace Skyline.Commands
{
    public class CommandAnalysisHeightControl : Skyline.Define.SkylineBaseCommand
    {
        public CommandAnalysisHeightControl()
        {
            this.m_Category = "三维分析";
            this.m_Caption = "控高分析";

            this.m_Message = "控高分析";
            this.m_Tooltip = "查看指定范围内建筑是否超过指定控制高度";
        }
      
        public override void OnClick()
        {
            try
            {
            FrmHeightControl myHeightControl = new FrmHeightControl(this.m_Hook.UIHook.MainForm);
                if (!myHeightControl.IsDisposed)
                {
                    this.m_Hook.UIHook.MainForm.AddOwnedForm(myHeightControl);
                    myHeightControl.Show();
                }
            }
            catch
            {
                MessageBox.Show("发生错误!");
            }
        }
    }
}
