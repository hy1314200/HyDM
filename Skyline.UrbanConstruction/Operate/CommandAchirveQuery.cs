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
    public class CommandAchirveQuery : Skyline.Define.SkylineBaseCommand
    {
        public CommandAchirveQuery()
        {
            this.m_Category = "城建";
            this.m_Caption = "城建档案查询";

            this.m_Message = "城建档案查询";
            this.m_Tooltip = "城建档案查询";
        }

        public override bool Checked
        {
            get
            {
                return m_UrbanFlag;
            }
        }

        private bool m_UrbanFlag = false;
        FrmUrbanConstruction m_FrmUrban = null;
        public override void OnClick()
        {
            if (m_UrbanFlag == false)
            {
                Program.TE.OnLButtonDown += new _ITerraExplorerEvents5_OnLButtonDownEventHandler(TE_OnLButtonDown);
            }
            else
            {
                Program.TE.OnLButtonDown -= new _ITerraExplorerEvents5_OnLButtonDownEventHandler(TE_OnLButtonDown);
            }
            m_UrbanFlag = !m_UrbanFlag;
        }
        void TE_OnLButtonDown(int Flags, int X, int Y, ref object pbHandled)
        {
            if (m_FrmUrban == null || m_FrmUrban.IsDisposed)
                m_FrmUrban = new FrmUrbanConstruction();

            m_FrmUrban.SetData();

            if (m_FrmUrban.Visible == false)
                m_FrmUrban.Show(this.m_Hook.UIHook.MainForm);

        }
    }
}
