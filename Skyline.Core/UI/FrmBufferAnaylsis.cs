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
    public partial class FrmBufferAnaylsis : FrmBase
    {

        private double[] cVerticesArray;

        private ITerrainPolygon61 mTerrainPolygon61;
        private IGeometry geo;
        /// <summary>
        /// 表示当前要做的命令的字符串变量
        /// </summary>
        private string En = "";

        private int GroupID = 0;

        private bool LockRButton = false;

        private Form _frmMain;
        /// <summary>
        /// 当前获取到的ObjectID
        /// </summary>
        private string CurrObjectID = String.Empty;

        public ISGWorld61 SgWorld { set; private get; }

        public ITerraExplorer TerraExplorer { set; private get; }


        public FrmBufferAnaylsis(Form frmMain)
        {
            _frmMain = frmMain;
            if (base.BeginForm(frmMain))
            {
                InitializeComponent();
            }
            else
            {
                this.Close();
            }
     
        }
        /// <summary>
        /// 窗体的数据初始化，并创建分析结果文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBufferAnaylsis_Load(object sender, EventArgs e)
        {
            try
            {
                GroupID = this.SgWorld.ProjectTree.FindItem("分析结果");
                if (GroupID==0)
                {
                    GroupID = this.SgWorld.ProjectTree.CreateGroup("分析结果");
                }
            }
            catch (Exception)
            {
             
            }
            (this.SgWorld as _ISGWorld61Events_Event).OnObjectAction += new _ISGWorld61Events_OnObjectActionEventHandler(sgworld_OnObjectAction); 
            (this.TerraExplorer as _ITerraExplorerEvents5_Event).OnLButtonDown += new TerraExplorerX._ITerraExplorerEvents5_OnLButtonDownEventHandler(TE_OnLButtonDown);
            (this.TerraExplorer as _ITerraExplorerEvents5_Event).OnRButtonDown += new TerraExplorerX._ITerraExplorerEvents5_OnRButtonDownEventHandler(TE_OnRButtonDown);
        }

        /// <summary>
        /// 获取当前激活的Object的主键
        /// </summary>
        /// <param name="objectid"></param>
        /// <param name="actioncode">动作编码</param>
        void sgworld_OnObjectAction(string objectid, IAction61 actioncode)
        {
            if (actioncode.Code == ActionCode.AC_OBJECT_ADDED)
            {
                this.CurrObjectID = objectid;
            }

        }

        /// <summary>
        /// TE 右键单击事件
        /// </summary>
        /// <param name="Flags"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="pbHandled"></param>
        void TE_OnRButtonDown(int Flags, int X, int Y, ref object pbHandled)
        {
            if ( En =="DrawGeo"&&!this.LockRButton)
            {
                try
                {
                    ITerraExplorerObject61 pp = this.SgWorld.Creator.GetObject(this.CurrObjectID);
                    this.mTerrainPolygon61 = pp as ITerrainPolygon61;
                    IGeometry geo = this.mTerrainPolygon61.Geometry;
                    IPolygon pPolygon = geo as IPolygon;
                    ISpatialOperator _SpatialOperator = pPolygon.SpatialOperator;

                    uint nLineColor = 0xFF00FF00; // Abgr value -> solid green
                    uint nFillColor = 0x7FFF0000; // Abgr value -> 50% transparent blue

                    this.geo = _SpatialOperator.buffer((double)this.spinEdit1.Value);
                    this.SgWorld.Creator.CreatePolygon(this.geo, nLineColor, nFillColor, AltitudeTypeCode.ATC_ON_TERRAIN, GroupID, "Buffer" + System.Guid.NewGuid().ToString().Substring(0, 6));
                    this.LockRButton = true;
                    //Program.TE.Save();

                    this.En = null;
                    this.simpleButton2.Checked = false;
                }
                catch (Exception)
                {
                    
                    
                }
               
            }
           
           
        }
        /// <summary>
        /// 鼠标左键按下时发生的事件（建立缓冲区的主要方法）
        /// </summary>
        /// <param name="Flags"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="pbHandled"></param>
        void TE_OnLButtonDown(int Flags, int X, int Y, ref object pbHandled)
        {
            if (En =="DrawGeo")
            {
              
            }
            else if (En =="SelectModle")
            {
                // 2012-9-20 张航宇
                // 若成功的完成了一次缓冲分析，则将状态释放，要求重新点击
                IWorldPointInfo61 pIWorldPointInfo = this.SgWorld.Window.PixelToWorld(X, Y, WorldPointType.WPT_MODEL);
                if (pIWorldPointInfo.ObjectID != "" && pIWorldPointInfo.ObjectID != null)
                {
                    try
                    {
                        ITerraExplorerObject61 pp = this.SgWorld.Creator.GetObject(pIWorldPointInfo.ObjectID);
                        if (pp.ObjectType == ObjectTypeCode.OT_FEATURE)
                        {
                            IFeature61 pIF = pp as IFeature61;
                            this.geo = pIF.GeometryZ.SpatialOperator.buffer((double)this.spinEdit1.Value);
                            ITerrainPolygon61 polygon= this.SgWorld.Creator.CreatePolygon(this.geo, -16711936, -10197916,cbRelative.Checked?AltitudeTypeCode.ATC_TERRAIN_RELATIVE: AltitudeTypeCode.ATC_ON_TERRAIN, GroupID, "Buffer" + System.Guid.NewGuid().ToString().Substring(0, 6));

                            if (cbRelative.Checked)
                            {
                                polygon.Position.Altitude = (double)speHeight.Value;
                            }

                            this.En = null;
                            this.Select.Checked = false;
                        }
                        else
                        {
                            ITerrainModel61 pITerrainModel = this.SgWorld.Creator.GetObject(pIWorldPointInfo.ObjectID) as ITerrainModel61;
                            IBBox3D61 pBBox = pITerrainModel.Terrain.BBox;
                            cVerticesArray = new double[] {
                                    pBBox.MaxX,  pBBox.MinY,  0, 
                                    pBBox.MaxX,  pBBox.MaxY,  0,
                                    pBBox.MinX, pBBox.MaxY,  0,
                                    pBBox.MinX,  pBBox.MinY,  0,
                        };
                            ILinearRing cRing = this.SgWorld.Creator.GeometryCreator.CreateLinearRingGeometry(cVerticesArray);
                            IPolygon cPolygonGeometry = this.SgWorld.Creator.GeometryCreator.CreatePolygonGeometry(cRing, null);
                            //this.geo = cPolygonGeometry as IGeometry;
                            ISpatialOperator _SpatialOperator = cPolygonGeometry.SpatialOperator;

                            this.geo = _SpatialOperator.buffer((double)this.spinEdit1.Value);
                            ITerrainPolygon61 polygon = this.SgWorld.Creator.CreatePolygon(this.geo, -16711936, -10197916, cbRelative.Checked ? AltitudeTypeCode.ATC_TERRAIN_RELATIVE : AltitudeTypeCode.ATC_ON_TERRAIN, GroupID, "Buffer" + System.Guid.NewGuid().ToString().Substring(0, 6));

                            if (cbRelative.Checked)
                            {
                                polygon.Position.Altitude = (double)speHeight.Value;
                            }

                            this.En = null;
                            this.Select.Checked = false;
                        }
                    }
                    catch
                    {

                    }
                }
            }

        }

        /// <summary>
        /// 选择模型，以模型地面图形建立缓冲区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Select_Click(object sender, EventArgs e)
        {
            try
            {
                En = "SelectModle";
                this.Select.Checked = true;
                this.simpleButton2.Checked = false;
            }
            catch (Exception)
            {
                 
            }
           
        }

        /// <summary>
        /// 绘图型形成缓冲区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                En = "DrawGeo";
                MenuIDCommand.RunMenuCommand(this.SgWorld, CommandParam.ICreatePolygon, CommandParam.PCreatePolygon);
                LockRButton = false;

                this.Select.Checked = false;
                this.simpleButton2.Checked = true;
            }
            catch (Exception)
            {
                
              
            }
           
        }


        // 2012-9-20 张航宇
        // 窗体关闭时要取消挂接，而不仅仅是点击“取消”时
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBufferAnaylsis_FormClosed(object sender, FormClosedEventArgs e)
        {
            _frmMain.RemoveOwnedForm(this);
            (this.SgWorld as _ISGWorld61Events_Event).OnObjectAction -= new _ISGWorld61Events_OnObjectActionEventHandler(sgworld_OnObjectAction);
            (this.TerraExplorer as _ITerraExplorerEvents5_Event).OnLButtonDown -= new TerraExplorerX._ITerraExplorerEvents5_OnLButtonDownEventHandler(TE_OnLButtonDown);
            (this.TerraExplorer as _ITerraExplorerEvents5_Event).OnRButtonDown -= new TerraExplorerX._ITerraExplorerEvents5_OnRButtonDownEventHandler(TE_OnRButtonDown);
        }

        private void cbRelative_CheckedChanged(object sender, EventArgs e)
        {
            checkEdit1.Checked = !cbRelative.Checked;
            speHeight.Enabled = cbRelative.Checked;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            cbRelative.Checked = !checkEdit1.Checked;
        }
    }
}
