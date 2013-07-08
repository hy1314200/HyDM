using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandAnalysisVolume:SkylineBaseCommand
    {
        public CommandAnalysisVolume()
        {
            this.m_Category = "土方";
            this.m_Caption = "挖方量算";

            this.m_Message = "挖方量算";
            this.m_Tooltip = "在场景中进行探访量算";
        }

        public override void OnClick()
        {            
            FrmTerrainModifier fmTerrModifier = new FrmTerrainModifier(base.m_Hook.MainForm);
            fmTerrModifier.SgWorld=this.m_SkylineHook.SGWorld;
            fmTerrModifier.TerraExplorer=this.m_SkylineHook.TerraExplorer;
            try
            {
                if (!fmTerrModifier.IsDisposed)
                {
                    this.m_Hook.MainForm.AddOwnedForm(fmTerrModifier);
                    fmTerrModifier.Show();
                }
            }
            catch 
            {

                MessageBox.Show("发生错误!");

            }
        }
    }
}
