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
    public partial class FrmTerrainModifier : FrmBase
    {
        private Form _frmMain;
       // private double ABhigh = 0;
        /// <summary>
        /// 左键单击次数
        /// </summary>
        private int LClickCount = 0;
        private String pbhander = "";
        /// <summary>
        /// 地表海拔高度
        /// </summary>
        private double earth = 0;
        /// <summary>
        /// 地表面
        /// </summary>
        private ITerrainPolygon61 pITerrainPolygon = null;
        /// <summary>
        /// 地表土方量
        /// </summary>
        private ITerrainModifier61 pITerrainModifier = null;

        private List<double> ListVerticsArray = new List<double>();

        public ISGWorld61 SgWorld { set; private get; }

        public ITerraExplorer TerraExplorer { set; private get; }

        public FrmTerrainModifier(Form FrmMain)
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

        private void FrmTerrainModifier_Load(object sender, EventArgs e)
        {
            (this.SgWorld as _ISGWorld61Events_Event).OnLButtonDown += new _ISGWorld61Events_OnLButtonDownEventHandler(sgworld_OnLButtonDown);
            (this.SgWorld as _ISGWorld61Events_Event).OnRButtonDown += new _ISGWorld61Events_OnRButtonDownEventHandler(sgworld_OnRButtonDown);
        }

        bool sgworld_OnRButtonDown(int Flags, int X, int Y)
        {
            if (LClickCount<=2)
            {
                MessageBox.Show("绘制三个以上点构造面！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.SgWorld.Creator.DeleteObject(pITerrainPolygon.ID);
                pITerrainPolygon = null;
                return true;
            }
            if (pbhander == "modify" && pITerrainPolygon != null && this.earth != 0 )
            {
                string Volum = "填挖方分析" + System.Guid.NewGuid().ToString().Substring(0, 6);
                IGeometry pIGeometry = pITerrainPolygon.Geometry;
               // ABhigh = pITerrainPolygon.Position.Altitude;
                pITerrainModifier = this.SgWorld.Creator.CreateTerrainModifier(pIGeometry, ElevationBehaviorMode.EB_REPLACE, true, 0, 0, Volum);
                pITerrainModifier.Position.AltitudeType = AltitudeTypeCode.ATC_TERRAIN_RELATIVE;
                pITerrainModifier.Position.Altitude = this.earth;
                
                this.SgWorld.Creator.DeleteObject(pITerrainPolygon.ID);
                pITerrainPolygon = null;
                String[] cVerticesArray = null;
                int pobjitemid = this.SgWorld.ProjectTree.FindItem(Volum);
                String pObjid = this.SgWorld.ProjectTree.GetTerraObjectID(pobjitemid);

                cVerticesArray = new String[] { pObjid };
                IVolumeAnalysisInfo61 pIVolumeAnalysisInfo = this.SgWorld.Analysis.CalculateVolume(cVerticesArray, 0.5);
                this.label1.Text = "【土方】增加：" + Math.Round(pIVolumeAnalysisInfo.AddedCubicMeters,3).ToString() + "立方米" + "||减少：" + Math.Round(pIVolumeAnalysisInfo.RemovedCubicMeters,3).ToString() + "立方米";
            }
            pbhander = "";
           
            return true;
        }

        bool sgworld_OnLButtonDown(int Flags, int X, int Y)
        {
            if (pbhander == "modify")
            {
                LClickCount++;
                IWorldPointInfo61 pIWPInfo = this.SgWorld.Window.PixelToWorld(X, Y, WorldPointType.WPT_TERRAIN);
                IPosition61 pIPosition = this.SgWorld.Navigate.GetPosition(AltitudeTypeCode.ATC_TERRAIN_RELATIVE);

                if (pITerrainPolygon == null)
                {
                    try
                    {
                        ILinearRing cRing = null;
                        double[] cVerticesArray = null;
                        cVerticesArray = new double[] {
                    pIPosition.X,  pIPosition.Y,  0, 
                    pIWPInfo.Position.X,  pIWPInfo.Position.Y,  0,  
                    pIWPInfo.Position.X,  pIWPInfo.Position.Y,  0,                         
					};
                        cRing = this.SgWorld.Creator.GeometryCreator.CreateLinearRingGeometry(cVerticesArray);

                        uint nLineColor = 0xFF00FF00;
                        uint nFillColor = 0x7FFF0000;
                        AltitudeTypeCode eAltitudeTypeCode = AltitudeTypeCode.ATC_TERRAIN_RELATIVE;
                        pITerrainPolygon = this.SgWorld.Creator.CreatePolygon(cRing, nLineColor, nFillColor, eAltitudeTypeCode, 0, "Polygon");

                        IPolygon polygonGeometry = pITerrainPolygon.Geometry as IPolygon;
                        polygonGeometry.StartEdit();
                        foreach (ILinearRing ring in polygonGeometry.Rings)
                        {
                            double dx = pIWPInfo.Position.X;
                            double dy = pIWPInfo.Position.Y;
                            double dh = pIWPInfo.Position.Distance;
                            ring.Points.AddPoint(dx, dy, dh);
                            ring.Points.DeletePoint(0);
                        }
                        IGeometry editedGeometry = polygonGeometry.EndEdit();
                        pITerrainPolygon.Geometry = editedGeometry;
                    }
                    catch (Exception)
                    {
                        
                     
                    }
                    
                }
                else
                {
                    try
                    {
                        IPolygon polygonGeometry = pITerrainPolygon.Geometry as IPolygon;
                        polygonGeometry.StartEdit();
                        foreach (ILinearRing ring in polygonGeometry.Rings)
                        {
                            double dx = pIWPInfo.Position.X;
                            double dy = pIWPInfo.Position.Y;
                            double dh = pIWPInfo.Position.Distance;
                            ring.Points.AddPoint(dx, dy, dh);
                        }
                        IGeometry editedGeometry = polygonGeometry.EndEdit();
                        pITerrainPolygon.Geometry = editedGeometry;
                    }
                    catch (Exception)
                    {
                      
                    }
                   
                }
            }
            return false;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.LClickCount = 0;
            (this.TerraExplorer as IRender5).SetMouseInputMode(MouseInputMode.MI_COM_CLIENT);
            pbhander = "modify";
            this.earth = Convert.ToDouble(spinEdit1.Value);

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
           
        }

        private void FrmTerrainModifier_FormClosed(object sender, FormClosedEventArgs e)
        {
            _frmMain.RemoveOwnedForm(this);
            (this.SgWorld as _ISGWorld61Events_Event).OnLButtonDown -= new _ISGWorld61Events_OnLButtonDownEventHandler(sgworld_OnLButtonDown);
            (this.SgWorld as _ISGWorld61Events_Event).OnRButtonDown -= new _ISGWorld61Events_OnRButtonDownEventHandler(sgworld_OnRButtonDown);
        }

    }
}
