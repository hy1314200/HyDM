using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandAnalysisProfileEx:Skyline.Define.SkylineBaseCommand
    {
        public CommandAnalysisProfileEx()
        {
            this.m_Category = "剖面分析";
            this.m_Caption = "指定分析";

            this.m_Message = "指定剖面分析";
            this.m_Tooltip = "从外部加载剖面路线分析";
        }

        public override void OnClick()
        {

            FrmTerrainProfileArrPoints fTeerArr = new FrmTerrainProfileArrPoints(this.m_SkylineHook.SGWorld,base.m_Hook.UIHook.MainForm);
            try
            {
                if (!fTeerArr.IsDisposed)
                {
                    base.m_Hook.UIHook.MainForm.AddOwnedForm(fTeerArr);
                    fTeerArr.Show();
                }
            }
            catch
            {

                MessageBox.Show("发生错误!");

            }
        }
    }
}
