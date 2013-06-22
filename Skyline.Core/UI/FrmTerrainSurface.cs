using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using TerraExplorerX;
using System.Threading;

namespace Skyline.Core.UI
{
    public partial class FrmTerrainSurface : FrmBase
    {
        /// <summary>
        /// 模型对象
        /// </summary>
        private ITerrainPolyline5 modeObj = null;
        /// <summary>
        /// 流程控制 
        /// </summary>
        private bool lock_BtnBeginCreate = false;
        /// <summary>
        /// 控制OnFrame事件创建及修改点
        /// </summary>
 
        private bool lock_OnRButton = true;

        private ITerrainPolygon61 pTerrainPolygon61;
        private IGeometry geo;
        private Form _frmMain;
        private string CurrObjectID = String.Empty;
        /// <summary>
        /// 记录2Dpolygon对象点集合
        /// </summary>
        private IList<double[]> dotList = new List<double[]>();

        public ISGWorld61 SgWorld { set; private get; }

        public ITerraExplorer TerraExplorer { set; private get; }


        public FrmTerrainSurface(Form frmMain)
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

        private void FrmTerrainSurface_Load(object sender, EventArgs e)
        {
            (this.SgWorld as _ISGWorld61Events_Event).OnObjectAction += new _ISGWorld61Events_OnObjectActionEventHandler(sgworld_OnObjectAction);  
            (this.TerraExplorer as _ITerraExplorerEvents5_Event).OnRButtonDown += new TerraExplorerX._ITerraExplorerEvents5_OnRButtonDownEventHandler(TE_OnRButtonDown);
        }

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
            if (!this.lock_OnRButton)
            {
                try
                {
                    ITerraExplorerObject61 pp = this.SgWorld.Creator.GetObject(this.CurrObjectID);
                    this.pTerrainPolygon61 = pp as ITerrainPolygon61;
                    geo = this.pTerrainPolygon61.Geometry;
                    this.timer1.Enabled = true;
                    // double SurfaceArea = Math.Round(this.SgWorld.Analysis.MeasureTerrainSurface(geo, 10), 2);
                    // this.labelControl1.Text = "区域内表面积：" + SurfaceArea.ToString() + "平方米";
                    this.lock_OnRButton = true;
                }
                catch (Exception)
                {
                    
                   
                }
                
               
            }
        }

      

      
      

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.lock_OnRButton = false;
            MenuIDCommand.RunMenuCommand(this.SgWorld,CommandParam.ICreatePolygon, CommandParam.PCreatePolygon);
        }

        private void FrmTerrainSurface_FormClosing(object sender, FormClosingEventArgs e)
        {

            (this.SgWorld as _ISGWorld61Events_Event).OnObjectAction -= new _ISGWorld61Events_OnObjectActionEventHandler(sgworld_OnObjectAction);
            (this.TerraExplorer as _ITerraExplorerEvents5_Event).OnRButtonDown -= new TerraExplorerX._ITerraExplorerEvents5_OnRButtonDownEventHandler(TE_OnRButtonDown);
            _frmMain.RemoveOwnedForm(this);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                this.timer1.Enabled = false;
                double SurfaceArea = Math.Round(this.SgWorld.Analysis.MeasureTerrainSurface(geo, 10), 2);
                this.labelControl1.Text = "区域内表面积：" + SurfaceArea.ToString() + "平方米";

                int GroupID = this.SgWorld.ProjectTree.FindItem("分析结果");
                if (GroupID == 0)
                {
                    GroupID = this.SgWorld.ProjectTree.CreateGroup("分析结果", 0);

                }
                this.SgWorld.ProjectTree.SetParent(pTerrainPolygon61.TreeItem.ItemID, GroupID);
            }
            catch (Exception)
            {
                
               
            }
            
        }
    }
}