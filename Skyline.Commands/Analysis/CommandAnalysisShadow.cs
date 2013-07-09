using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;
using TerraExplorerX;

namespace Skyline.Commands
{
    public class CommandAnalysisShadow:SkylineBaseCommand
    {
        public CommandAnalysisShadow()
        {
            this.m_Category = "三维分析";
            this.m_Caption = "日照分析";

            this.m_Message = "日照分析";
            this.m_Tooltip = "日照分析";
        }

        public override void OnClick()
        {
            (this.m_SkylineHook.TerraExplorer as TerraExplorerX.IRender5).SetMouseInputMode(MouseInputMode.MI_COM_CLIENT);
            //  labelControl1.Text = "单击模型创建阴影,按ESC结束";
            //labelControl1.Visible = true;
            //   Program.sgworld.Command.Execute(1149,14);
            Frmshadow pFrmshaow = new Frmshadow(base.m_Hook.UIHook.MainForm);

            try
            {
                if (!pFrmshaow.IsDisposed)
                {
                    base.m_Hook.UIHook.MainForm.AddOwnedForm(pFrmshaow);
                    pFrmshaow.Show();
                }
            }
            catch
            {

                MessageBox.Show("发生错误!");

            }

        }
    }
}
