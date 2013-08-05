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
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;

namespace Skyline.GuiHua.Bussiness
{
    public partial class FrmLampManual : DevExpress.XtraEditors.XtraForm
    {
        public FrmLampManual(TerraExplorerX.TerraExplorerClass te, TerraExplorerX.ISGWorld61 hook)
        {
            InitializeComponent();

            this.m_TerraExplorer = te;
            this.m_Hook = hook;
        }

        private bool m_Flag = false;
        void m_Hook_OnLButtonDown(int Flags, int X, int Y, ref object pbHandled)
        {
            try
            {
                object obj1, obj2, obj3;
                this.m_TerraExplorer.GetMouseInfo(out obj1, out obj2, out obj3);


                double longitude, latitude, height;
                this.m_TerraExplorer.ScreenToTerrain(X, Y, out longitude, out latitude, out height);

                IPosition61 position = m_Hook.Creator.CreatePosition(longitude, latitude, 0,AltitudeTypeCode.ATC_PIVOT_RELATIVE);
                int tempGroup = this.m_Hook.ProjectTree.FindItem(m_TempGroupName);
                if (tempGroup > -1)
                {
                    this.m_Hook.ProjectTree.DeleteItem(tempGroup);
                }
                tempGroup = this.m_Hook.ProjectTree.CreateGroup(m_TempGroupName);
                this.m_Model = this.m_Hook.Creator.CreateModel(position, ConfigurationManager.AppSettings["LampModelFile"], 1, ModelTypeCode.MT_NORMAL, tempGroup, m_TempModelName);

                if (cbAuto.Checked)
                {
                    LampAnalysis();
                }

                pbHandled = true;

                m_Flag = false;
            }
            catch
            {
                MessageBox.Show("您当前的配置不能完成此操作，请修改配置!");
            }
            finally
            {
                this.m_TerraExplorer.OnLButtonDown -= new TerraExplorerX._ITerraExplorerEvents5_OnLButtonDownEventHandler(m_Hook_OnLButtonDown);
            }
        }

        private TerraExplorerX.TerraExplorerClass m_TerraExplorer;
        private TerraExplorerX.ISGWorld61 m_Hook;
        private string m_TempGroupName = "LampAnalysis";
        private string m_TempModelName = "Lamp";
        private string m_TempAnalysisName = "Analysis";

        private ITerrainModel61 m_Model;

        private void LampAnalysis()
        {
            try
            {
                if (m_Model == null)
                {
                    MessageBox.Show("请先安放信号灯!");
                    return;
                }

                this.txtResult.Clear();
                Application.DoEvents();

                SendMessage("正在获取路口信息...");

                string lyr = ConfigurationManager.AppSettings["CrossLayer"];
                int lyrID = m_Hook.ProjectTree.FindItem(lyr);
                if (lyrID < 0)
                {
                    MessageBox.Show("您的配置有问题或者路口图层没有加载");
                    return;
                }

                ILayer61 teLayer = m_Hook.ProjectTree.GetLayer(lyrID);
                if (teLayer == null)
                {
                    return;
                }
                //IFeature61 tefCurrent = teLayer.FeatureGroups.Point[0] as IFeature61;

                // 严格来说要根据所点的位置来确定是哪个路口
                // 取第一个作为路口信息

                // 先直接从数据库中读取，模拟计算
                string strOld = teLayer.DataSourceInfo.ConnectionString;
                string[] strs = strOld.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                strOld = strs[0];
                strOld = strOld.Substring(strOld.IndexOf("=") + 1);
                string strFolder = System.IO.Path.GetDirectoryName(strOld);
                string strName = System.IO.Path.GetFileNameWithoutExtension(strOld);

                IWorkspaceFactory wsf = new ShapefileWorkspaceFactoryClass();
                IWorkspace wsShp= wsf.OpenFromFile(strFolder, 0);
                IFeatureClass fClass = (wsShp as IFeatureWorkspace).OpenFeatureClass(strName);
                
                IPosition61 position = m_Model.Position;
                ESRI.ArcGIS.Geometry.IPoint pModel = new ESRI.ArcGIS.Geometry.PointClass();
                pModel.SpatialReference = (fClass as IGeoDataset).SpatialReference;
                pModel.PutCoords(position.X, position.Y);

                ESRI.ArcGIS.Geometry.ITopologicalOperator topoOperator=pModel as ESRI.ArcGIS.Geometry.ITopologicalOperator;
                ESRI.ArcGIS.Geometry.IGeometry geoModel=topoOperator.Buffer(50);

                ISpatialFilter qFilter=new SpatialFilterClass();
                qFilter.Geometry=geoModel;
                qFilter.SpatialRel=esriSpatialRelEnum.esriSpatialRelIntersects;
                IFeatureCursor fCursor = fClass.Search(qFilter,false);
                IFeature fCross = fCursor.NextFeature();
                if (fCross == null)
                {
                    SendMessage("在当前位置的50米范围内没有找到路口信息。");
                    SendMessage("若您的路口信息是全面的，则您所安放的位置偏离了路口过多，理论上不适合安放信号灯！");
                    SendMessage("分析结束");
                    return;
                }

                double lampHeight = (double)spinEdit1.Value;
                double carHeight = (double)spinEdit2.Value;
                double setHeight = (double)spinEdit3.Value;
                double carLength = (double)spinEdit4.Value;
                double lampMustDistance = (double)spinEdit5.Value;
                double roadWidth = 30;

                roadWidth = Convert.ToDouble(fCross.get_Value(fClass.FindField("NorthWidth")));

                System.Threading.Thread.Sleep(1000);
                SendMessage("正在计算有大车情况下是否能在规定的最小必须可见距离内看到信号灯...");
                System.Threading.Thread.Sleep(1000);
                double lampMustHeight = (carHeight - setHeight) * lampMustDistance / (carLength + roadWidth) + setHeight;
                if (lampHeight < lampMustHeight)
                {
                    SendMessage(string.Format("  信号灯在有大车情况下不能在规定的最小距离内看到信号灯，必须在路对面增加辅助信号灯。"));
                    SendMessage("当前位置不合适安放信号灯或必须添加辅助信号灯!");
                    SendMessage("分析结束。");
                    return;
                }

                SendMessage("正在计算是否会因为建筑物及绿化带等引起信号灯盲区...");

                //int tempGroup = this.m_Hook.ProjectTree.FindItem(m_TempGroup);
                //if (tempGroup < 0)
                //{
                //    tempGroup = this.m_Hook.ProjectTree.CreateGroup(m_TempGroup);
                //}

                //IPosition61 position = m_Model.Position;
                //m_Hook.CoordServices.MoveCoordEx(ref position, 0, 0, lampHeight);
                //int analysisGroup = m_Hook.Analysis.CreateViewshed(position, 360, 1, roadWidth + lampMustDistance, setHeight, null, null, tempGroup, m_TempAnalysisName);
                //int invisibleItem = m_Hook.ProjectTree.FindItem(string.Format(@"{0}\{1}\Invisible Area",m_TempGroup,m_TempAnalysisName));
                //object obj = m_Hook.ProjectTree.GetObject(invisibleItem);

                System.Threading.Thread.Sleep(5000);
                if (Convert.ToInt32(fCross.get_Value(fClass.FindField("Flag"))) > 0)
                {
                    SendMessage("   由于建筑物及绿化带等将引起信号灯盲区，必须在路对面增加辅助信号灯.");
                    SendMessage("当前位置不合适安放信号灯或必须添加辅助信号灯!");
                    SendMessage("分析结束。");
                    return;
                }

                SendMessage("当前位置安放信号灯后不需要添加辅助信号灯!");
                SendMessage("分析结束。");
            }
            catch
            {
                SendMessage("分析过程出错了,分析结束");
            }
        }

        private void SendMessage(string strMsg)
        {
            this.txtResult.AppendText(strMsg + "\r\n");
            this.txtResult.ScrollToCaret();
            Application.DoEvents();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (m_Flag)
                return;

            this.m_TerraExplorer.OnLButtonDown += new TerraExplorerX._ITerraExplorerEvents5_OnLButtonDownEventHandler(m_Hook_OnLButtonDown);
            m_Flag = true;
        }

        private void FrmLampManual_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.m_TerraExplorer.OnLButtonDown -= new TerraExplorerX._ITerraExplorerEvents5_OnLButtonDownEventHandler(m_Hook_OnLButtonDown);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.LampAnalysis();
        }
    }
}