using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TerraExplorerX;

namespace Skyline.Core.UI
{
    public partial class FrmStipulatePath : FrmBase
    {
        TreeNode tn;
        /// <summary>
        /// 地表动态对象
        /// </summary>
        ITerrainDynamicObject61 itdo1;
        public FrmStipulatePath(Form frmMain)
        {
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
        /// 数据初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmStipulatePath_Load(object sender, EventArgs e)
        {
            //this.dt = new DataTable();
            //DataColumn dcOID = new DataColumn("KEYID", Type.GetType("System.Int32"));
            //DataColumn dcParentOID = new DataColumn("ParentID", Type.GetType("System.Int32"));
            //DataColumn dcNodeName = new DataColumn("NodeName", Type.GetType("System.String"));

            //dt.Columns.Add(dcOID);
            //dt.Columns.Add(dcParentOID);
            //dt.Columns.Add(dcNodeName);

            try
            {
                base.FrmName = "规定路径";
                //获取飞行浏览信息树文件组
                int groupid = Program.TE.FindItem("fly");

                int childId = Program.sgworld.ProjectTree.GetNextItem(groupid, ItemCode.CHILD);
                while (childId != 0)
                {
                    ITerrainDynamicObject61 itdo = (ITerrainDynamicObject61)Program.sgworld.ProjectTree.GetObject(childId);

                    TreeNode tn = new TreeNode(itdo.TreeItem.Name);
                  
                    tn.Tag = itdo;
                    tn.ImageIndex = 0;
                    this.tree_Stipulate.Nodes[0].Nodes.Add(tn);

                    childId = Program.sgworld.ProjectTree.GetNextItem(childId, ItemCode.NEXT);
                }
                this.tree_Stipulate.ExpandAll();
            }
            catch (Exception ex)
            {
                
               // throw;
            }

           
        }


        /// <summary>
        /// 鼠标左键单击树时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tree_Stipulate_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                tn = tree_Stipulate.GetNodeAt(e.X, e.Y);
                if (tn != null)
                {
                    tree_Stipulate.SelectedNode = tn;
                    tn.ContextMenuStrip = contextMenuStrip1;
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ITerrainDynamicObject61 itdo = (ITerrainDynamicObject61)tn.Tag;
                Program.TE.DeleteItem(itdo.TreeItem.ItemID);
                tree_Stipulate.Nodes.Remove(tn);
            }
            catch (Exception)
            {
           
            }
           
        }

        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tree_Stipulate.LabelEdit = true;
            tn.BeginEdit();
        }

        private void tree_Stipulate_NodeMouseDoubleClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Name =="root")
            {
                return;
            }
            try
            {
                ITerrainDynamicObject61 itdo = (ITerrainDynamicObject61)e.Node.Tag;
                // ITerrainDynamicObject6
                itdo1 = itdo;
                itdo.RestartRoute(0);
                string tempName = Program.TE.GetTerraObjectID(itdo.TreeItem.ItemID);
                if (true)
                {
                    itdo.Action.Code = ActionCode.AC_FLYTO;
                    Program.TE.SelectItem(itdo.TreeItem.ItemID, 0, 0);
                    Program.TE.Invoke((int)InvokeNumber.BehindObject);
                }
                else
                {
                    Program.TE.FlyToObject(tempName, ActionCode.AC_FLYTO);
                }
            }
            catch (Exception)
            {
             
            }
            
        }

        private void tree_Stipulate_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            ITerrainDynamicObject5 itdo = (ITerrainDynamicObject5)tn.Tag;
            //itdo.Text = e.Label;
            itdo.Description = e.Label;
            tree_Stipulate.LabelEdit = false;
        }

        private void FlyParam_Click(object sender, EventArgs e)
        {
            if (this.tree_Stipulate.SelectedNode!=null)
            {
                itdo1 = (ITerrainDynamicObject61)this.tree_Stipulate.SelectedNode.Tag;  
            }
            if (itdo1 == null)
            {
                return;
            }
            FrmSetPlaneParam pSetPlaneParam = new FrmSetPlaneParam();
            pSetPlaneParam.GetDynamicObject = itdo1;
            pSetPlaneParam.ShowDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void 参数设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tree_Stipulate.SelectedNode != null)
            {
                itdo1 = (ITerrainDynamicObject61)this.tree_Stipulate.SelectedNode.Tag;
            }
            if (itdo1 == null)
            {
                return;
            }
            FrmSetPlaneParam pSetPlaneParam = new FrmSetPlaneParam();
            pSetPlaneParam.GetDynamicObject = itdo1;
            pSetPlaneParam.ShowDialog();
        }

    
    }
}



