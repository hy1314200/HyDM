using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Skyline.UrbanConstruction.Bussiness;

namespace Skyline.Commands
{
    public class CommandTrafficRoute : Skyline.Define.SkylineBaseCommand
    {
        public CommandTrafficRoute()
        {
            this.m_Category = "城建";
            this.m_Caption = "交通路线分析";

            this.m_Message = "交通路线分析";
            this.m_Tooltip = "交通路线分析";
        }

        public override void OnClick()
        {
            FrmTrafficRoute frmRoute = new FrmTrafficRoute(m_SkylineHook.TerraExplorer, m_SkylineHook.SGWorld);
            this.m_Hook.UIHook.MainForm.AddOwnedForm(frmRoute);
            frmRoute.Show(this.m_Hook.UIHook.MainForm);
        }
    }
}
