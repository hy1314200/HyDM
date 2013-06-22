using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;
using Skyline.Core.Helper;

namespace Skyline.Commands
{
    public class CommandAnalysisSlopDirect:SkylineBaseCommand
    {
        public CommandAnalysisSlopDirect()
        {
            this.m_Category = "坡计算";
            this.m_Caption = "坡向计算";

            this.m_Message = "坡向计算";
            this.m_Tooltip = "对场景中选定区域进行坡向分析";
        }

        public override void OnClick()
        {
            try
            {
                MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld,CommandParam.ISlopeDirections, CommandParam.PSlopeDirections);
                CSharpAPIsClass CSHarp = new CSharpAPIsClass();
                CSharpAPIsClass.WindowInfo[] df = CSHarp.GetAllDesktopWindows();

                for (int i = 0; i < df.Length; i++)
                {
                    if (df[i].szWindowName == "Slope Analysis Properties")
                    {
                        CSharpAPIsClass.RECT rc = new CSharpAPIsClass.RECT();

                        rc = CSharpAPIsClass.getRect(df[i].hWnd);

                        CSHarp.ToChange(df[i].hWnd, false);

                        //FrmAutCover fcov = new FrmAutCover(df[i].szWindowName);

                        //Point p = new Point();

                        //p.X = rc.Left;

                        //p.Y = rc.Top;

                        //fcov.Location = p;

                        //fcov.Show();

                        break;
                    }

                }
            }
            catch
            {
                MessageBox.Show("发生错误!");
            }
        }
    }
}
