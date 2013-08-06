using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Skyline.UrbanConstruction.Bussiness;
using TerraExplorerX;
using System.Drawing;
using System.Threading;
using System.Configuration;

namespace Skyline.Commands
{
    public class CommandEnterpriseQuery : Skyline.Define.SkylineBaseCommand
    {
        public CommandEnterpriseQuery()
        {
            this.m_Category = "城建";
            this.m_Caption = "房屋及人口、企业信息查询";

            this.m_Message = "房屋及人口、企业信息查询";
            this.m_Tooltip = "房屋及人口、企业信息查询";
        }

        public override bool Checked
        {
            get
            {
                return m_Flag;
            }
        }

        private bool m_Flag = false;
        FrmFloor m_FrmFloor = null;
        ITerrainModel61 m_ModelFloor;
        public override void OnClick()
        {
            if (m_Flag == false)
            {
                Program.TE.OnLButtonDown += new _ITerraExplorerEvents5_OnLButtonDownEventHandler(TE_OnLButtonDown);
            }
            else
            {
                Program.TE.OnLButtonDown -= new _ITerraExplorerEvents5_OnLButtonDownEventHandler(TE_OnLButtonDown);
            }
            m_Flag = !m_Flag;
        }
        void TE_OnLButtonDown(int Flags, int X, int Y, ref object pbHandled)
        {
            string strModel = ConfigurationManager.AppSettings["FloorPath"];
            ITerraExplorerObject61 objModel = Program.pCreator6.GetObject(Program.IInfoTree.GetTerraObjectID(Program.IInfoTree.FindItem(strModel)));
            ////model.ModelType = ModelTypeCode.MT_ANIMATION;
            m_ModelFloor = objModel as ITerrainModel61;
            //Type[] types= model.GetType().GetInterfaces();
            m_ModelFloor.Visibility.Show = true;


            if (m_FrmFloor == null || m_FrmFloor.IsDisposed)
            {
                m_FrmFloor = new FrmFloor();
                m_FrmFloor.FormClosed += new FormClosedEventHandler(m_FrmFloor_FormClosed);
            }

            if (m_FrmFloor.Visible == false)
                m_FrmFloor.Show(this.m_Hook.UIHook.MainForm);

        }
        void m_FrmFloor_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_ModelFloor != null)
                m_ModelFloor.Visibility.Show = false;
        }
    }
}
