using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandAnalysisTerrainArea:SkylineBaseCommand
    {
        public CommandAnalysisTerrainArea()
        {
            this.m_Category = "三维分析";
            this.m_Caption = "表面积计算";

            this.m_Message = "表面积计算";
            this.m_Tooltip = "计算场景中选定区域的表面积";
        }

        public override void OnClick()
        {
            FrmTerrainSurface pFFrmTerrainSurface = new FrmTerrainSurface(base.m_Hook.MainForm);
            pFFrmTerrainSurface.SgWorld = this.m_SkylineHook.SGWorld;
            pFFrmTerrainSurface.TerraExplorer = this.m_SkylineHook.TerraExplorer;
            try
            {
                if (!pFFrmTerrainSurface.IsDisposed)
                {
                    base.m_Hook.MainForm.AddOwnedForm(pFFrmTerrainSurface);
                    pFFrmTerrainSurface.Show();
                }
            }
            catch
            {

                MessageBox.Show("发生错误!");

            }
        }
    }
}
