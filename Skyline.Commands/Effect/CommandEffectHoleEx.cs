using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;
using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandEffectHoleEx:Skyline.Define.SkylineBaseCommand
    {
        public CommandEffectHoleEx()
        {
            this.m_Category = "三维特效";
            this.m_Caption = "指定挖开";

            this.m_Message = "预定义区域挖开";
            this.m_Tooltip = "挖开指定Shp文件所确定的地面范围";
        }

        public override void OnClick()
        {
            try
            {
                FrmGetExtentFromFiles pFrmGetExtentFromFiles = new FrmGetExtentFromFiles();
                pFrmGetExtentFromFiles.ShowDialog();
                if (!pFrmGetExtentFromFiles.IsDisposed)
                {
                    pFrmGetExtentFromFiles.Dispose();
                }
            }
            catch
            {
                MessageBox.Show("发生错误!");
            }
        }
    }
}
