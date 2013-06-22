using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TerraExplorerX;

namespace Skyline.Core.UI
{
    public partial class FrmModifyTerrain : FrmBase
    {
        public ISGWorld61 SgWorld { set; private get; }

        public ITerraExplorer TerraExplorer { set; private get; }

        private Form _frmMain;
        private int LClickCount = 0;
        private String pbhander = "";
        private ITerrainPolygon61 pITerrainPolygon; 
        private ITerrainPolyline5 pITerrainPolyline;
        private ITerrainModifier61 pITerrainModifier = null;
        private List<double> ListVerticsArray = new List<double>();
        public FrmModifyTerrain(Form FrmMain)
        {
            _frmMain = FrmMain;

            if (base.BeginForm(FrmMain))
            {
                InitializeComponent();
            }
            else
            {
                this.Close();
            }
        }
        private int GroupID = -1;
        private void FrmTerrainModifier_Load(object sender, EventArgs e)
        {
            this.spinEditAlti.Value = 0;
            this.spinEditFeather.Value = 0;
            this.simpleButtonCancel.Enabled = false;

        }
        //开始
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            try
            {
                GroupID = this.SgWorld.ProjectTree.FindItem("TerrainModify");
                if (GroupID == 0)
                {
                    GroupID = this.SgWorld.ProjectTree.CreateGroup("TerrainModify", 0);
                    this.pITerrainPolyline = (this.TerraExplorer as IObjectManager51).CreatePolyline(null, 0x0000ff, HeightStyleCode.HSC_TERRAIN_RELATIVE, GroupID, "newline");
                }
                else
                    this.pITerrainPolyline = (this.TerraExplorer as IObjectManager51).CreatePolyline(null, 0x0000ff, HeightStyleCode.HSC_TERRAIN_RELATIVE, GroupID, "newline");

                (this.SgWorld as _ISGWorld61Events_Event).OnLButtonDown += new _ISGWorld61Events_OnLButtonDownEventHandler(sgworld_OnLButtonDown);
                (this.SgWorld as _ISGWorld61Events_Event).OnRButtonDown += new _ISGWorld61Events_OnRButtonDownEventHandler(sgworld_OnRButtonDown);

                this.LClickCount = 0;
                ListVerticsArray.Clear();
                (this.TerraExplorer as IRender5).SetMouseInputMode(MouseInputMode.MI_COM_CLIENT);
                pbhander = "modify";
            }
            catch
            {
                MessageBox.Show("开始绘制失败!");
            }


        }
        //左键单击
        bool sgworld_OnLButtonDown(int Flags, int X, int Y)
        {
            try
            {
                if (pbhander == "modify")
                {
                    LClickCount++;
                    //点击三个点以前画线，三个点以后画面
                    double longitude, latitude, height;
                    (this.TerraExplorer as IRender5).ScreenToTerrain(X, Y, out longitude, out latitude, out height);
                    IPosition61 pIPosition = this.SgWorld.Navigate.GetPosition(AltitudeTypeCode.ATC_TERRAIN_RELATIVE);
                    if (LClickCount < 3)
                    {
                        this.pITerrainPolyline.AddVertex(longitude, height, latitude, 0);
                        ListVerticsArray.Add(longitude);
                        ListVerticsArray.Add(latitude);
                        ListVerticsArray.Add(pIPosition.Distance);
                    }
                    else
                    {
                        if (LClickCount == 3)
                        {
                            ListVerticsArray.Add(longitude);
                            ListVerticsArray.Add(latitude);
                            ListVerticsArray.Add(pIPosition.Distance);
                            double[] tripletArray = new double[ListVerticsArray.Count];
                            ListVerticsArray.CopyTo(tripletArray, 0);
                            IColor61 LineColor = this.SgWorld.Creator.CreateColor(Color.Red.R, Color.Red.G, Color.Red.B, Color.Red.A);
                            IColor61 FillColor = this.SgWorld.Creator.CreateColor(Color.Red.R, Color.Red.G, Color.Red.B, 0);
                            AltitudeTypeCode eAltitudeTypeCode = AltitudeTypeCode.ATC_TERRAIN_RELATIVE;
                            pITerrainPolygon = this.SgWorld.Creator.CreatePolygonFromArray(tripletArray, LineColor, FillColor, eAltitudeTypeCode, GroupID, "TempPolygon");
                            this.SgWorld.ProjectTree.DeleteItem(this.pITerrainPolyline.InfoTreeItemID);
                        }
                        else
                        {
                            if (pITerrainPolygon != null)
                            {
                                IPolygon pPolygon = pITerrainPolygon.Geometry as IPolygon;
                                pPolygon.StartEdit();
                                foreach (ILinearRing r in pPolygon.Rings)
                                {
                                    r.Points.AddPoint(longitude, latitude, pIPosition.Distance);
                                }
                                pITerrainPolygon.Geometry = pPolygon.EndEdit();
                            }
                        }
                    }
                }
                
            }
            catch
            {
                MessageBox.Show("地形调整范围绘制失败！");
            }
            return true;
        }

        //右键单击
        bool sgworld_OnRButtonDown(int Flags, int X, int Y)
        {
            try
            {

                if (LClickCount < 3)
                {
                    DialogResult dr = MessageBox.Show("请绘制三个以上点,是否重新绘制?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr == DialogResult.OK)
                    {
                        //重新绘制
                        if (this.pITerrainPolyline.NumOfVertices != 0)
                        {
                            for (int i = 0; i < this.pITerrainPolyline.NumOfVertices; i++)
                                this.pITerrainPolyline.DeleteVertex(i);
                            this.LClickCount = 0;
                            ListVerticsArray.Clear();
                            (this.TerraExplorer as IRender5).SetMouseInputMode(MouseInputMode.MI_COM_CLIENT);
                            pbhander = "modify";
                        }
                    }
                    return true;
                }
                else
                {

                    if (this.pITerrainPolygon != null)
                    {
                        pbhander = "";
                        (this.SgWorld as _ISGWorld61Events_Event).OnLButtonDown -= new _ISGWorld61Events_OnLButtonDownEventHandler(sgworld_OnLButtonDown);
                        (this.SgWorld as _ISGWorld61Events_Event).OnRButtonDown -= new _ISGWorld61Events_OnRButtonDownEventHandler(sgworld_OnRButtonDown);
                        (this.TerraExplorer as IRender5).SetMouseInputMode(MouseInputMode.MI_FREE_FLIGHT);

                        string Volum = "TerrainModifier" + System.Guid.NewGuid().ToString().Substring(0, 6);
                        pITerrainModifier = this.SgWorld.Creator.CreateTerrainModifier(this.pITerrainPolygon.Geometry, ElevationBehaviorMode.EB_REPLACE, true, 0, GroupID, Volum);
                        pITerrainModifier.Position.AltitudeType = AltitudeTypeCode.ATC_TERRAIN_RELATIVE;
                        pITerrainModifier.Position.Altitude = (double)this.spinEditAlti.Value;
                        pITerrainModifier.SetFeather((double)this.spinEditFeather.Value);
                        pITerrainModifier.SaveInFlyFile = true;

                        this.SgWorld.ProjectTree.DeleteItem(pITerrainPolygon.TreeItem.ItemID);
                        this.simpleButtonCancel.Enabled = true;
                    }

                   

                }
            }
            catch
            {
                MessageBox.Show("地形调整失败！");
            }
            return true;
        }

        //窗体关闭
        private void FrmModifyTerrain_FormClosed(object sender, FormClosedEventArgs e)
        {
            GroupID = this.SgWorld.ProjectTree.FindItem("TerrainModify");
            if (GroupID > 0)
            {
                DialogResult dr = MessageBox.Show("是否保存地形调整结果？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.No)
                {
                    this.SgWorld.ProjectTree.DeleteItem(GroupID);
                }
            }
            this.SgWorld.Project.Save();
            _frmMain.RemoveOwnedForm(this);
        }
        //清除
        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            GroupID = this.SgWorld.ProjectTree.FindItem("TerrainModify");
            if (GroupID > 0)
                this.SgWorld.ProjectTree.DeleteItem(GroupID);
        }
        //修正高程调整
        private void spinEditAlti_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.pITerrainModifier != null)
                {
                    pITerrainModifier.Position.Altitude = (double)this.spinEditAlti.Value;
                }
            }
            catch
            {
                MessageBox.Show("高程调整失败！");
            }
        }
        //羽化半径调整,当绘制面面积过小时，羽化半径不能过大
        private void spinEditFeather_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.pITerrainModifier != null)
                {
                    pITerrainModifier.SetFeather((double)this.spinEditFeather.Value);
                }
            }
            catch 
            {
                MessageBox.Show("发生错误，可能原因：修整范围面积过小，羽化半径设置过大！");
            }
        }
    }
}
