using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;
using Skyline.Core.Helper;
using TerraExplorerX;
using System.Threading;

namespace Skyline.Commands
{
    public class CommandAnalysisContourRationality:SkylineBaseCommand
    {
        public CommandAnalysisContourRationality()
        {
            this.m_Category = "等高线";
            this.m_Caption = "合理性检查";

            this.m_Message = "等高线合理性检查";
            this.m_Tooltip = "检查场景中加载的等高线合理性";
        }

        public override void OnClick()
        {
            try
            {
                List<string> pContourList = new List<string>();
                int GroupID = this.m_SkylineHook.SGWorld.ProjectTree.FindItem("指定等高线");
                if (GroupID == 0)
                {
                    MessageBox.Show("请先指定区域生成等高线！");
                    return;
                }
                int sID = this.m_SkylineHook.TerraExplorer.GetNextItem(GroupID, ItemCode.CHILD);
                while (sID > 0)
                {
                    string ItemName = this.m_SkylineHook.TerraExplorer.GetItemName(sID);
                    pContourList.Add(ItemName);
                    sID = this.m_SkylineHook.TerraExplorer.GetNextItem(sID, ItemCode.NEXT);
                }
                FrmCheckContour pFrmCheckContour = new FrmCheckContour(pContourList);
                pFrmCheckContour.ShowDialog();
                if (pFrmCheckContour.DialogResult == DialogResult.OK)
                {
                    string ShapeWorkspace = pFrmCheckContour.Random;
                    pFrmCheckContour.Dispose();
                    CreateContour pC = new CreateContour();
                    pC.CreateTopology(ShapeWorkspace);
                }
                else
                {
                    pFrmCheckContour.Dispose();
                }
            }
            catch
            {
                MessageBox.Show("发生错误!");
            }
        }
    }
}
