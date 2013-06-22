using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandAnalysisTerrainModify:SkylineBaseCommand
    {
        public CommandAnalysisTerrainModify()
        {
            this.m_Category = "三维分析";
            this.m_Caption = "地形调整";

            this.m_Message = "地形调整";
            this.m_Tooltip = "允许选定局部区域调整地形";
        }

        public override void OnClick()
        {
            // 2013-04-10 张航宇
            // 自定义的产生了一系列问题：一次性允许做多个调整，第一次以后有可能出错；清除目标会将原来保留下来的也清除掉
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld,CommandParam.IModifyTerrain, CommandParam.PModifyTerrain);

            //FrmModifyTerrain fmTerrModifier = new FrmModifyTerrain(this);
            //fmTerrModifier.SgWorld = this.m_SkylineHook.SGWorld;
            //fmTerrModifier.TerraExplorer = this.m_SkylineHook.TerraExplorer;
            //try
            //{
            //    if (!fmTerrModifier.IsDisposed)
            //    {
            //        this.AddOwnedForm(fmTerrModifier);
            //        fmTerrModifier.Show();
            //    }
            //}
            //catch 
            //{

            //    MessageBox.Show("发生错误!");

            //}
        }
    }
}
