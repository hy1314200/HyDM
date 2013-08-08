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

namespace Skyline.UrbanConstruction.Bussiness
{
    public partial class FrmTrafficRoute : DevExpress.XtraEditors.XtraForm
    {
        private TerraExplorerClass m_TerraExplorer;
        private ISGWorld61 m_SGWorld;
        public FrmTrafficRoute(TerraExplorerClass te,TerraExplorerX.ISGWorld61 sgWorld)
        {
            InitializeComponent();

            m_TerraExplorer = te;
            m_SGWorld = sgWorld;
            lblStatus.Text = "";
        }

        int m_Flag = 0;
        double[] m_FromPoint;
        double[] m_ToPoint;
        ITerrainImageLabel61 m_From, m_To;
        ITerrainPolyline61 m_Route;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            m_Flag = 1;

            m_TerraExplorer.OnLButtonDown += new _ITerraExplorerEvents5_OnLButtonDownEventHandler(m_TerraExplorer_OnLButtonDown);
        }

        void m_TerraExplorer_OnLButtonDown(int Flags, int X, int Y, ref object pbHandled)
        {
            object x,y,z,oid;
            m_TerraExplorer.ScreenToWorld(X, Y, ref pbHandled, out x, out z, out y, out oid);

            double[] array=new double[] {(double) x,(double) y,2};
            IPosition61 p=m_SGWorld.Creator.CreatePosition((double) x,(double) y,2);//, AltitudeTypeCode.ATC_TERRAIN_RELATIVE,0,0,0,0);
            ITerrainImageLabel61 lbl= m_SGWorld.Creator.CreateImageLabel(p, ConfigurationManager.AppSettings["LabelModelFile"]);
            if (m_Flag == 1)
            {
                m_FromPoint = array;
                lblFromPoint.Text = "已选择";
                lblFromPoint.ForeColor = Color.Green;
                m_From = lbl;
            }
            else
            {
                m_ToPoint = array;
                lblToPoint.Text = "已选择";
                lblToPoint.ForeColor = Color.Green;
                m_To = lbl;
            }

           m_TerraExplorer.OnLButtonDown -= new _ITerraExplorerEvents5_OnLButtonDownEventHandler(m_TerraExplorer_OnLButtonDown);

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            m_Flag = 2;
            m_TerraExplorer.OnLButtonDown += new _ITerraExplorerEvents5_OnLButtonDownEventHandler(m_TerraExplorer_OnLButtonDown);

        }

        private void DisplayEvents(string strEvent, int sleep)
        {
            lblStatus.Text = strEvent;
            Application.DoEvents();

            System.Threading.Thread.Sleep(sleep);
        }
        private ITerrainPolyline61 m_LineLest;
        private ITerrainPolyline61 CreateLine(string strPoints,int color)
        {
            char[] cSplit = { ',' };
            string[] array = strPoints.Split(cSplit, StringSplitOptions.RemoveEmptyEntries);
            double[] pArray = new double[array.Length + 6];
            pArray[0] = m_FromPoint[0];
            pArray[1] = m_FromPoint[1];
            pArray[2] = m_FromPoint[2];

            for (int i = 0; i < array.Length; i++)
            {
                pArray[3 + i] = Convert.ToDouble(array[i]);
            }

            pArray[array.Length + 3] = m_ToPoint[0];
            pArray[array.Length + 4] = m_ToPoint[1];
            pArray[array.Length + 5] = m_ToPoint[2];

            return m_SGWorld.Creator.CreatePolylineFromArray(pArray, color,AltitudeTypeCode.ATC_TERRAIN_RELATIVE);

        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DisplayEvents("正在加载参数设置...", 1000);
            DisplayEvents("正在计算最短路径...", 2000);
            m_LineLest = CreateLine(ConfigurationManager.AppSettings["LestPoints"],0x0000ff);

            DisplayEvents("正在计算规避点重新规划...", 2000);
            m_Route = CreateLine(ConfigurationManager.AppSettings["RoutePoints"],0x00ff00);
            
            lblStatus.Text = "计算完成。";

        }

        private void FrmTrafficRoute_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_From != null)
            {
                m_TerraExplorer.DeleteItem(m_From.TreeItem.ItemID);
            }
            if (m_To != null)
            {
                m_TerraExplorer.DeleteItem(m_To.TreeItem.ItemID);
            }
            if (m_LineLest != null)
            {
                m_TerraExplorer.DeleteItem(m_LineLest.TreeItem.ItemID);
            }
            if (m_Route != null)
            {
                m_TerraExplorer.DeleteItem(m_Route.TreeItem.ItemID);
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}