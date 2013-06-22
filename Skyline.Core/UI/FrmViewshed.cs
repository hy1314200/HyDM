using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using TerraExplorerX;

using System.Configuration;

namespace Skyline.Core.UI
{
    public partial class FrmViewshed : DevExpress.XtraEditors.XtraForm
    {
        public FrmViewshed(ISGWorld61 hook ,ITerraExplorer te)
        {
            InitializeComponent();

            this.m_Hook = hook;
            this.m_EventHook = hook as _ISGWorld61Events_Event;
            this.m_TerraExplorer = te;
            this.m_GroupID = this.m_Hook.ProjectTree.FindItem("视域分析");
            if (this.m_GroupID <= 0)
            {
                this.m_GroupID = this.m_Hook.ProjectTree.CreateGroup("视域分析");
            }

            // 绑定
            Bound();
        }
        private TerraExplorerX.ISGWorld61 m_Hook;
        private ITerraExplorer m_TerraExplorer;
        private TerraExplorerX._ISGWorld61Events_Event m_EventHook;

        //private Control m_ControlHook;
        //public Control ControlHook
        //{
        //    set
        //    {
        //        m_ControlHook = value;
        //        m_ControlHook.MouseMove += new MouseEventHandler(m_ControlHook_MouseMove);
        //        //m_ControlHook.MouseClick += new MouseEventHandler(m_ControlHook_MouseClick);
        //    }
        //}

        //void m_ControlHook_MouseClick(object sender, MouseEventArgs e)
        //{
        //}




        private int m_GroupID = 0;
        private string m_RootName = ConfigurationManager.AppSettings["ViewshedRoot"];// "Root";
        private string m_LeftRayName = ConfigurationManager.AppSettings["ViewshedLeft"];//  "LeftLimit";
        private string m_RightRayName = ConfigurationManager.AppSettings["ViewshedRight"];// "RightLimit";
        private string m_ArcName = ConfigurationManager.AppSettings["ViewshedArc"];//"Arc";
        private string m_VisibleArea = ConfigurationManager.AppSettings["ViewshedVisible"];//Visible Area
        private string m_InvisibleArea = ConfigurationManager.AppSettings["ViewshedInvisible"];//Invisible Area

        private ITerrainPolyline61 m_Root;
        private ITerrainPolyline61 m_LeftRay;
        private ITerrainPolyline61 m_RightRay;
        private ITerrainArc61 m_Arc;

        private double m_RootX, m_RootY;
        bool m_EventHook_OnLButtonUp(int Flags, int X, int Y)
        {
            double longitude, latitude, height;
            (this.m_TerraExplorer as IRender5).ScreenToTerrain(X, Y, out longitude, out latitude, out height);

            double viewerHeight = (double)speViewer.Value;
            if (m_Root == null)
            {
                m_RootX = longitude;
                m_RootY = latitude;

                double[] points ={
                                    longitude,latitude,0,
                                    longitude,latitude,viewerHeight
                                };

                ILineString geoLine = m_Hook.Creator.GeometryCreator.CreateLineStringGeometry(points);

                // 创建Root
                m_Root = m_Hook.Creator.CreatePolyline(geoLine as IGeometry, 0xFF00ffff, AltitudeTypeCode.ATC_TERRAIN_RELATIVE, m_GroupID, m_RootName);
                m_Root.ExtendToGround = true;
                m_Root.SaveInFlyFile = false;
            }
            else
            {
                try
                {
                    // 创建视域分析
                    double dis = (this.m_TerraExplorer as ICoordSys3).GetDistance(m_RootX, m_RootY, longitude, latitude); 
                    //Math.Sqrt((latitude - m_RootY) * (latitude - m_RootY) + (longitude - m_RootX) * (longitude - m_RootX));
                    //if (dis < 0.1) // 防止除以0
                    //    dis = 0.1;


                    if (dis <= viewerHeight)// 操他妈的必须大于视点高
                        dis = viewerHeight + 0.01;

                    double rawArc = Math.Atan2(latitude - m_RootY, longitude - m_RootX);
                    rawArc = 180 * rawArc / Math.PI;
                    //double rawArc = Math.Asin((latitude - m_RootY) / dis);//(longitude - m_RootX));
                    //rawArc = 180 * rawArc / Math.PI;
                    //if (longitude < m_RootX)
                    //    rawArc = 180 - rawArc;

                    // skyline中视域分析角度与数学角度转换
                    rawArc = rawArc - 90;

                    double disViewshed = dis;
                    disViewshed = Math.Sqrt(disViewshed * disViewshed - viewerHeight * viewerHeight);
                    IPosition61 pMouse = m_Hook.Creator.CreatePosition(m_RootX, m_RootY, viewerHeight, AltitudeTypeCode.ATC_TERRAIN_RELATIVE, rawArc, 0, 0, disViewshed);

                    int index = 1;
                    while (true)
                    {
                        if (m_Hook.ProjectTree.FindItem(string.Format("视域分析\\视域分析{0}", index)) > 0)
                        {
                            index++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    string strViewshed = string.Format("视域分析{0}", index);
                    int viewshedID = m_Hook.Analysis.CreateViewshed(pMouse, (double)speField.Value, (double)speSample.Value, (double)speRay.Value, (double)speTarget.Value, null, null, m_GroupID, strViewshed);

                    if (viewshedID <= 0)
                    {
                        MessageBox.Show("分析出现了错误");
                        return false;
                    }

                    // 修改视域分析结果
                    string viewshedFull = string.Format("视域分析\\视域分析{0}", index);
                    // 删掉Root
                    m_Hook.ProjectTree.DeleteItem(m_Root.TreeItem.ItemID);
                    m_Root = null;
                    // 换掉Left，Right，Arc
                    int leftRay = m_Hook.ProjectTree.FindItem(string.Format("{0}\\{1}", viewshedFull,m_LeftRayName));
                    if (leftRay > 0)
                    {
                        m_Hook.ProjectTree.DeleteItem(leftRay);
                    }
                    int rightRay = m_Hook.ProjectTree.FindItem(string.Format("{0}\\{1}", viewshedFull,m_RightRayName));
                    if (rightRay > 0)
                    {
                        m_Hook.ProjectTree.DeleteItem(rightRay);
                    }
                    int arc = m_Hook.ProjectTree.FindItem(string.Format("{0}\\{1}", viewshedFull,m_ArcName));
                    if (arc > 0)
                    {
                        m_Hook.ProjectTree.DeleteItem(arc);
                    }

                    m_Hook.ProjectTree.SetParent(m_LeftRay.TreeItem.ItemID, viewshedID);
                    m_Hook.ProjectTree.SetParent(m_RightRay.TreeItem.ItemID, viewshedID);
                    m_Hook.ProjectTree.SetParent(m_Arc.TreeItem.ItemID, viewshedID);

                    // Visible和InVisible修改高度方式和高度
                    int vID = m_Hook.ProjectTree.FindItem(string.Format("{0}\\{1}", viewshedFull,m_VisibleArea));
                    int invID = m_Hook.ProjectTree.FindItem(string.Format("{0}\\{1}", viewshedFull,m_InvisibleArea));
                    if (vID > 0 && invID > 0)
                    {
                        object[] objAreas = 
                    {
                        m_Hook.ProjectTree.GetObject(vID),
                        m_Hook.ProjectTree.GetObject(invID)
                    };

                        foreach (object objArea in objAreas)
                        {
                            ITerrainPolygon61 polygon = objArea as ITerrainPolygon61;
                            polygon.Position.AltitudeType = AltitudeTypeCode.ATC_TERRAIN_RELATIVE;
                            polygon.Position.Altitude = (double)speTarget.Value;
                        }
                    }

                    m_LeftRay = null;
                    m_RightRay = null;
                    m_Arc = null;

                    this.Close();
                }
                catch
                {
                    MessageBox.Show("很抱歉，分析过程出现错误");
                }
            }

            return false;
        }

        double disInMap = 0;
        void m_ControlHook_MouseMove(object sender, MouseEventArgs e)
        {
            // 若Root还没有建立，则返回
            if (m_Root == null)
                return;

            double viewerHeight = (double)speViewer.Value;
            double longitude, latitude, height;
            (this.m_TerraExplorer as IRender5).ScreenToTerrain(e.X, e.Y, out longitude, out latitude, out height);
            
            
            // Arc
            double rArc = (this.m_TerraExplorer as ICoordSys3).GetDistance(m_RootX, m_RootY, longitude, latitude); 
            //Math.Sqrt((latitude - m_RootY) * (latitude - m_RootY) + (longitude - m_RootX) * (longitude - m_RootX));

            //if (rArc < 0.1) // 防止除以0
            //    rArc = 0.1;

            if (rArc <= viewerHeight) // 操他妈的必须大于视点高
                rArc = viewerHeight + 0.01;
            else
                // 计算左右斜线时要使用图上距离
                disInMap = Math.Sqrt((latitude - m_RootY) * (latitude - m_RootY) + (longitude - m_RootX) * (longitude - m_RootX));
            

            //double degree = Math.Asin((latitude - m_RootY) / rArc);
            //degree = 180 * degree / Math.PI;
            //if (longitude < m_RootX)
            //    degree = 180 - degree;

            double degree = Math.Atan2(latitude - m_RootY,longitude-m_RootX);
            degree = 180 * degree / Math.PI;

            // skyline中角度与数学角度转换
            double rawArc = degree - 90;
            rawArc = 360 - rawArc; // 顺时针方向

            txtDistance.Text = Math.Sqrt(rArc * rArc - viewerHeight * viewerHeight).ToString();

            IPosition61 pMouse = m_Hook.Creator.CreatePosition(longitude, latitude, (double)speTarget.Value, AltitudeTypeCode.ATC_TERRAIN_RELATIVE);//, rawArc);

            double fieldOffset = (double)speField.Value / 2.0;
            double degreeLeft = (degree - fieldOffset) * Math.PI / 180;
            double degreeRight = (degree + fieldOffset) * Math.PI / 180;

            double[] pointsLeft ={
                                    m_RootX,m_RootY,viewerHeight,
                                    m_RootX+disInMap*Math.Cos(degreeLeft) ,m_RootY+disInMap*Math.Sin(degreeLeft),(double)speTarget.Value
                                };
            double[] pointsRight ={
                                    m_RootX,m_RootY,viewerHeight,
                                    m_RootX+disInMap*Math.Cos(degreeRight) ,m_RootY+disInMap*Math.Sin(degreeRight),(double)speTarget.Value
                                };
            IGeometry geoLeft = m_Hook.Creator.GeometryCreator.CreateLineStringGeometry(pointsLeft) as IGeometry;
            IGeometry geoRight = m_Hook.Creator.GeometryCreator.CreateLineStringGeometry(pointsRight) as IGeometry;

            // 若左右射线和Arc没建立，则建立
            if (m_Arc == null)
            {
                // 创建Left，Right，Arc
                // Left
                m_LeftRay = m_Hook.Creator.CreatePolyline(geoLeft as IGeometry, 0xFF00ffff, AltitudeTypeCode.ATC_TERRAIN_RELATIVE, m_GroupID, m_LeftRayName);

                // Right 
                m_RightRay = m_Hook.Creator.CreatePolyline(geoRight as IGeometry, 0xFF00ffff, AltitudeTypeCode.ATC_TERRAIN_RELATIVE, m_GroupID, m_RightRayName);

                // Arc
                m_Arc = m_Hook.Creator.CreateArc(pMouse, rArc, rArc, rawArc - fieldOffset, rawArc + fieldOffset, 0xff00ffff, 0x00000000, 12, m_GroupID, m_ArcName);
                m_Arc.SaveInFlyFile = false;
            }
            // 否则动态改变
            else
            {
                // Left
                m_LeftRay.Geometry = geoLeft;

                // Right
                m_RightRay.Geometry = geoRight;

                // Arc
                m_Arc.Radius = rArc;
                m_Arc.Radius2 = rArc;
                m_Arc.StartAngle = rawArc - fieldOffset;
                m_Arc.EndAngle = rawArc + fieldOffset;

            }
        }


        private void Bound()
        {
            if (this.m_EventHook != null)
            {
                this.m_EventHook.OnFrame += new _ISGWorld61Events_OnFrameEventHandler(m_EventHook_OnFrame);
                this.m_EventHook.OnLButtonUp += new _ISGWorld61Events_OnLButtonUpEventHandler(m_EventHook_OnLButtonUp);
            }

        }

        void m_EventHook_OnFrame()
        {
           
            object x,y,objFlag;
            (this.m_TerraExplorer as IRender5).GetMouseInfo(out objFlag, out x, out y);
            MouseEventArgs e = new MouseEventArgs(System.Windows.Forms.MouseButtons.None, 0, (int)x, (int)y, 0);

            m_ControlHook_MouseMove(null, e);
        }

        private void UnBound()
        {
            if (this.m_EventHook != null)
            {
                this.m_EventHook.OnFrame -= new _ISGWorld61Events_OnFrameEventHandler(m_EventHook_OnFrame);
                this.m_EventHook.OnLButtonUp -= new _ISGWorld61Events_OnLButtonUpEventHandler(m_EventHook_OnLButtonUp);
                
            }

            if (m_Arc != null)
            {
                try
                {
                    m_Hook.ProjectTree.DeleteItem(m_Root.TreeItem.ItemID);
                    m_Hook.ProjectTree.DeleteItem(m_LeftRay.TreeItem.ItemID);
                    m_Hook.ProjectTree.DeleteItem(m_RightRay.TreeItem.ItemID);
                    m_Hook.ProjectTree.DeleteItem(m_Arc.TreeItem.ItemID);
                }
                catch
                {
                }
            }

            //if (this.m_ControlHook != null)
            //{
            //    this.m_ControlHook.MouseMove-=new MouseEventHandler(m_ControlHook_MouseMove);
            //}
        }


        private void speViewer_EditValueChanged(object sender, EventArgs e)
        {
            if (m_Root != null)
            {

                double[] points ={
                                    m_RootX,m_RootY,0,
                                    m_RootX,m_RootY,(double)speViewer.Value
                                };

                ILineString geoLine = m_Hook.Creator.GeometryCreator.CreateLineStringGeometry(points);
                m_Root.Geometry = geoLine;


                if (m_Arc != null)
                {
                    // Left
                    ILineString lineLeft = m_LeftRay.Geometry as ILineString;
                    double[] pointsLeft ={
                                             lineLeft.StartPoint.X,lineLeft.StartPoint.Y,(double)speViewer.Value,
                                             lineLeft.EndPoint.X,lineLeft.EndPoint.Y,lineLeft.EndPoint.Z
                                        };
                    m_LeftRay.Geometry = m_Hook.Creator.GeometryCreator.CreateLineStringGeometry(pointsLeft);

                    // Right
                    ILineString lineRight = m_RightRay.Geometry as ILineString;
                    double[] pointsRight ={
                                             lineRight.StartPoint.X,lineRight.StartPoint.Y,(double)speViewer.Value,
                                             lineRight.EndPoint.X,lineRight.EndPoint.Y,lineRight.EndPoint.Z
                                        };
                    m_RightRay.Geometry = m_Hook.Creator.GeometryCreator.CreateLineStringGeometry(pointsRight);


                }

            }
        }

        private void speField_EditValueChanged(object sender, EventArgs e)
        {
            if (m_Arc != null)
            {
                m_Arc.StartAngle = -(double)speField.Value / 2.0;
                m_Arc.EndAngle = (double)speField.Value / 2.0;
            }
        }

        private void FrmViewshed_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnBound();
        }

        private void speTarget_EditValueChanged(object sender, EventArgs e)
        {
            if (m_Arc != null)
            {

                // Left
                ILineString lineLeft = m_LeftRay.Geometry as ILineString;
                double[] pointsLeft ={
                                             lineLeft.StartPoint.X,lineLeft.StartPoint.Y,lineLeft.EndPoint.Z,
                                             lineLeft.EndPoint.X,lineLeft.EndPoint.Y,(double)speTarget.Value
                                        };
                m_LeftRay.Geometry = m_Hook.Creator.GeometryCreator.CreateLineStringGeometry(pointsLeft);

                // Right
                ILineString lineRight = m_RightRay.Geometry as ILineString;
                double[] pointsRight ={
                                             lineRight.StartPoint.X,lineRight.StartPoint.Y,lineRight.EndPoint.Z,
                                             lineRight.EndPoint.X,lineRight.EndPoint.Y,(double)speTarget.Value
                                        };
                m_RightRay.Geometry = m_Hook.Creator.GeometryCreator.CreateLineStringGeometry(pointsRight);

                // Arc
                m_Arc.Position.Altitude = (double)speTarget.Value;
               
            }
        }

    }
}