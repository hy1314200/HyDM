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
    public partial class FrmHeightControl : FrmBase
    {
      
        private Form _frmMain;
        public FrmHeightControl(Form FrmMain)
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

        private void FrmHeightControl_Load(object sender, EventArgs e)
        {
            //恢复默认设置
            //this.textEditBaseHeight.Text = "0";
            //this.textEditLmtHeight.Text = "";
            this.spinEditBaseEdit.Text = "0";
            this.spinEditLmtHeight.Text = "0";
            this.zoomOpacity.Value = 50;
            this.colorEditLmtHeight.Color = Color.Lime;
        }
        
        bool StartDraw = false;
        private ITerrainPolyline5 polyLine;
        private IList<double> list = new List<double> { };
        //private int clickIndex = 0;
        private int GroupID = -1;
        private ITerrainPolygon61 CurrentControlHeight = null;
        //开始绘制
        private void simpleBtnDraw_Click(object sender, EventArgs e)
        {
            try
            {
                if (!StartDraw)
                {
                    //判断是否填写限高高度
                    //if (this.textEditLmtHeight.Text == "")
                    //    MessageBox.Show("请先填写限高高度");
                    //else
                    //{
                    StartDraw = true;
                    this.simpleBtnClear.Enabled = false;
                    Program.pRender.SetMouseInputMode(MouseInputMode.MI_COM_CLIENT);

                    GroupID = Program.sgworld.ProjectTree.FindItem("Temp");
                    if (GroupID == 0)
                    {
                        GroupID = Program.sgworld.ProjectTree.CreateGroup("Temp");
                        this.polyLine = Program.TE.IObjectManager51_CreatePolyline(null, 0x0000ff, HeightStyleCode.HSC_TERRAIN_RELATIVE, GroupID, "newline");
                    }
                    else
                        this.polyLine = Program.TE.IObjectManager51_CreatePolyline(null, 0x0000ff, HeightStyleCode.HSC_TERRAIN_RELATIVE, GroupID, "newline");

                    Program.TE.OnLButtonDown += new TerraExplorerX._ITerraExplorerEvents5_OnLButtonDownEventHandler(TE_OnLButtonDown);
                    //Program.TE.OnFrame += new TerraExplorerX._ITerraExplorerEvents5_OnFrameEventHandler(TE_OnFrame);
                    Program.TE.OnRButtonDown += new TerraExplorerX._ITerraExplorerEvents5_OnRButtonDownEventHandler(TE_OnRButtonDown);
                    //clickIndex = 0;

                    //}
                }
            }
            catch
            {
                MessageBox.Show("开始绘制失败!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
                
        }

        //左键单击收集坐标点
        void TE_OnLButtonDown(int Flags, int X, int Y, ref object pbHandled)
        {
            if (StartDraw)
            {
                try
                {
                    //this.clickIndex += 1;
                    
                    double longitude, latitude, height;
                    Program.TE.ScreenToTerrain(X, Y, out longitude, out latitude, out height);
                    //height = Convert.ToDouble(this.textEditBaseHeight.Text) + Convert.ToDouble(this.textEditLmtHeight.Text);
                    height = Convert.ToDouble(this.spinEditBaseEdit.Text) + Convert.ToDouble(this.spinEditLmtHeight.Text);

                    this.list.Add(longitude);
                    this.list.Add(latitude);
                    this.list.Add(height);//height可改为设置的限高
                    this.polyLine.AddVertex(longitude, height, latitude, 0);

                }
                catch 
                {
                    MessageBox.Show("无法添加当前点！");
                }
            }

        }
        //刷屏,暂不使用
        void TE_OnFrame()
        {
            //if (StartDraw)
            //{
            //    if (this.lock_onFrame)
            //    {
            //        object longitudeObj, latitudeObj, flagsObj;
            //        double longitude, latitude, height;
            //        Program.TE.GetMouseInfo(out flagsObj, out longitudeObj, out latitudeObj);
            //        Program.TE.ScreenToTerrain(int.Parse(longitudeObj.ToString()), int.Parse(latitudeObj.ToString()), out longitude, out latitude, out height);

            //        if (this.lock_moveForOnframe)
            //        {
            //            this.polyLine.AddVertex(longitude, height, latitude, 0);
            //            this.lock_moveForOnframe = false;

            //        }
            //        else
            //        {
            //            this.polyLine.ModifyVertex(this.clickIndex, longitude, height, latitude, 0);
            //        }
            //    }
            //}
            //throw new NotImplementedException();
        }
        //右键结束画面，同时根据设置生成限高面
        void TE_OnRButtonDown(int Flags, int X, int Y, ref object pbHandled)
        {
            if (StartDraw)
            {
                try
                {
                    StartDraw = false;
                    pbHandled = true;
                    Program.pRender.SetMouseInputMode(MouseInputMode.MI_FREE_FLIGHT);
                    Program.sgworld.ProjectTree.DeleteItem(this.polyLine.InfoTreeItemID);

                    IColor61 LineColor = Program.pCreator6.CreateColor(this.colorEditLmtHeight.Color.R, this.colorEditLmtHeight.Color.G,
                                                                       this.colorEditLmtHeight.Color.B, this.colorEditLmtHeight.Color.A);
                    int Alpha = (int)(255 * (this.zoomOpacity.Value / 100.0));
                    IColor61 FillColor = Program.pCreator6.CreateColor(this.colorEditLmtHeight.Color.R, this.colorEditLmtHeight.Color.G,
                                                     this.colorEditLmtHeight.Color.B, Alpha);
                    if (list != null)
                    {
                        if (list.Count > 2)
                        {
                            double[] tripletArray = new double[list.Count];
                            list.CopyTo(tripletArray, 0);
                            CurrentControlHeight = Program.pCreator6.CreatePolygonFromArray(tripletArray, LineColor, FillColor, AltitudeTypeCode.ATC_TERRAIN_RELATIVE, GroupID, "Controlheight");
                            list.Clear();
                            this.simpleBtnClear.Enabled = true;
                        }
                        else
                            MessageBox.Show("至少绘制三个点！");
                    }
                }
                catch
                {
                    MessageBox.Show("绘制失败!");
                    list.Clear();
                    if(this.polyLine.NumOfVertices > 0)
                    {
                        for (int i = this.polyLine.NumOfVertices; i > 0; i--)
                             this.polyLine.DeleteVertex(i);
                    }
                }
            }
        }
        //窗体关闭
        private void FrmHeightControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (StartDraw)
            {
                StartDraw = false;
            }
            //并清除限高面与Temp文件夹
            if (GroupID >= 0)
            {
                Program.sgworld.ProjectTree.DeleteItem(GroupID);
                GroupID = -1;
            }
            _frmMain.RemoveOwnedForm(this);
        }
        //清除
        private void simpleBtnClear_Click(object sender, EventArgs e)
        {
            if (GroupID >= 0)
            {
                Program.sgworld.ProjectTree.DeleteItem(GroupID);
                GroupID = -1;
            }
        }
        //基准高程调整
        private void spinEditBaseEdit_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.CurrentControlHeight != null)
                {
                    this.CurrentControlHeight.Position.Altitude = Convert.ToDouble(this.spinEditBaseEdit.Text) + Convert.ToDouble(this.spinEditLmtHeight.Text);
                }
            }
            catch
            {
                MessageBox.Show("基准高程调整失败！");
            }
        }
        //限高高程调整
        private void spinEditLmtHeight_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.CurrentControlHeight != null)
                {
                    this.CurrentControlHeight.Position.Altitude = Convert.ToDouble(this.spinEditBaseEdit.Text) + Convert.ToDouble(this.spinEditLmtHeight.Text);
                }
            }
            catch
            {
                MessageBox.Show("限高高程调整失败！");
            }

        }
        //透明度调整
        private void zoomOpacity_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.CurrentControlHeight != null)
                {
                    int Alpha = (int)(255 * (this.zoomOpacity.Value / 100.0));
                    IColor61 FillColor = Program.pCreator6.CreateColor(this.colorEditLmtHeight.Color.R, this.colorEditLmtHeight.Color.G,
                                                     this.colorEditLmtHeight.Color.B, Alpha);
                    this.CurrentControlHeight.FillStyle.Color = FillColor;
                }
            }
            catch
            {
                MessageBox.Show("透明度调整失败！");
            }

        }
        //填充颜色调整
        private void colorEditLmtHeight_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.CurrentControlHeight != null)
                {
                    IColor61 LineColor = Program.pCreator6.CreateColor(this.colorEditLmtHeight.Color.R, this.colorEditLmtHeight.Color.G,
                                                                      this.colorEditLmtHeight.Color.B, this.colorEditLmtHeight.Color.A);
                    int Alpha = (int)(255 * (this.zoomOpacity.Value / 100.0));
                    IColor61 FillColor = Program.pCreator6.CreateColor(this.colorEditLmtHeight.Color.R, this.colorEditLmtHeight.Color.G,
                                                     this.colorEditLmtHeight.Color.B, Alpha);
                    this.CurrentControlHeight.FillStyle.Color = FillColor;
                    this.CurrentControlHeight.LineStyle.Color = LineColor;
                }
            }
            catch
            {
                MessageBox.Show("颜色调整失败！");
            }

        }
    }
}
