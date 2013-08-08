using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandAnalysisTerrainLine:Skyline.Define.SkylineBaseCommand
    {
        public CommandAnalysisTerrainLine()
        {
            this.m_Category = "三维分析";
            this.m_Caption = "地貌结构线生成";

            this.m_Message = "地貌结构线生成";
            this.m_Tooltip = "从指定的地形生成山谷线和山脊线";
        }

        public override void OnClick()
        {
            try
            {
                FrmGeomorphological pFrmGeomorphological = new FrmGeomorphological(); 
                pFrmGeomorphological.ShowDialog();
            }
            catch
            {
                MessageBox.Show("发生错误!");
            }
        }
    }
}
