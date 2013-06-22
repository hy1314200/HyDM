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
    public class CommandAnalysisContourGenerate:SkylineBaseCommand
    {
        public CommandAnalysisContourGenerate()
        {
            this.m_Category = "等高线";
            this.m_Caption = "等高线生成";

            this.m_Message = "等高线生成";
            this.m_Tooltip = "指定区域生成等高线shp并加载";
        }

        public override void OnClick()
        {
            try
            {
                FrmWriteDataCreatContour frmCreatContour = new FrmWriteDataCreatContour();
                if (frmCreatContour.ShowDialog() == DialogResult.OK)
                {

                    double _interval = frmCreatContour.interval;
                    double[] _extent = frmCreatContour.extent;
                    frmCreatContour.Dispose();
                    CreateContour pCreateContour = new CreateContour();
                    double luz = this.m_SkylineHook.SGWorld.Terrain.GetGroundHeightInfo(_extent[0], _extent[1], AccuracyLevel.ACCURACY_FORCE_BEST_RENDERED, true).Position.Altitude;
                    double rlz = this.m_SkylineHook.SGWorld.Terrain.GetGroundHeightInfo(_extent[2], _extent[3], AccuracyLevel.ACCURACY_FORCE_BEST_RENDERED, true).Position.Altitude;
                    string randomname = pCreateContour.CreateContourShape(this.m_SkylineHook.SGWorld, _extent[0], _extent[1], luz, _extent[2], _extent[3], rlz, _interval);
                    //string randomname = pCreateContour.CreateContourShape(114.403211, 23.318350, 445.2734375, 114.428304, 23.303542, 371.992554);
                    ArcGISDataManager pArcGISDataManager = new ArcGISDataManager();
                    Thread.Sleep(500);
                    int GroupID = this.m_SkylineHook.SGWorld.ProjectTree.FindItem("指定等高线");
                    if (GroupID == 0)
                    {
                        GroupID = this.m_SkylineHook.SGWorld.ProjectTree.CreateGroup("指定等高线", 0);

                    }
                    pArcGISDataManager.LoadShapeFile(this.m_SkylineHook.SGWorld, Application.StartupPath + "\\Convert\\ContourResult\\" + randomname + "\\Contour.shp", "Contour" + randomname, GroupID);
                }
                else
                {
                    frmCreatContour.Dispose();
                }
            }
            catch
            {
                MessageBox.Show("发生错误!");
            }
        }
    }
}
