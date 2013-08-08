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
    public class CommandStopTracking:Skyline.Define.SkylineBaseCommand
    {
        public CommandStopTracking()
        {
            this.m_Category = "飞行浏览";
            this.m_Caption = "停止跟踪";

            this.m_Message = "停止跟踪";
            this.m_Tooltip = "停止跟踪";
        }

        public override void OnClick()
        {
            try
            {
                int groupid = Program.TE.FindItem("fly");
                if (groupid >= 0)
                {
                    int childId = Program.sgworld.ProjectTree.GetNextItem(groupid, ItemCode.CHILD);
                    //if (Currentitdo == -1)
                    //{
                    while (childId != 0)
                    {
                        ITerrainDynamicObject61 itdo = (ITerrainDynamicObject61)Program.sgworld.ProjectTree.GetObject(childId);
                        if (itdo.Pause == false)
                        {
                            itdo.Pause = true;
                            //Currentitdo = itdo.TreeItem.ItemID;
                        }
                        childId = Program.sgworld.ProjectTree.GetNextItem(childId, ItemCode.NEXT);
                    }
                    //}
                }
            }
            catch
            {
                MessageBox.Show("发生错误!");
            }
        }
    }
}
