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
    public class CommandAccessibility : Skyline.Define.SkylineBaseCommand
    {
        public CommandAccessibility()
        {
            this.m_Category = "城建";
            this.m_Caption = "通达性分析";

            this.m_Message = "通达性分析";
            this.m_Tooltip = "通达性分析";
        }


        bool m_AccessFlag = false;
        FrmAccessibility m_FrmAccess;
       public override void  OnClick()
       {
            if (m_AccessFlag == false)
            {
                m_SkylineHook.TerraExplorer.OnLButtonDown += new _ITerraExplorerEvents5_OnLButtonDownEventHandler(AccessTE_OnLButtonDown);
                m_AccessFlag = true;
            }
        }

        ITerrainPolyline61 m_Subway, m_Railway, m_Airport;
        void AccessTE_OnLButtonDown(int Flags, int X, int Y, ref object pbHandled)
        {
            m_SkylineHook.TerraExplorer.OnLButtonDown -= new _ITerraExplorerEvents5_OnLButtonDownEventHandler(AccessTE_OnLButtonDown);
            m_AccessFlag = false;

            Form frmProcess = new Form();
            frmProcess.Size = new Size(200, 40);
            frmProcess.FormBorderStyle = FormBorderStyle.None;
            frmProcess.StartPosition = FormStartPosition.CenterScreen;
            Label lblProcess = new Label();
            lblProcess.Size = new Size(100, 21);
            frmProcess.Controls.Add(lblProcess);
            lblProcess.Top = 10;
            lblProcess.Left = 50;
            lblProcess.Text = "正在分析...";
            frmProcess.Show(this.m_Hook.UIHook.MainForm);
            Application.DoEvents();

            Thread.Sleep(4500);
            frmProcess.Close();

            object x, y, z, oid;
            m_SkylineHook.TerraExplorer.ScreenToWorld(X, Y, ref pbHandled, out x, out z, out y, out oid);

            m_SkylineHook.TerraExplorer.SetVisibility(m_SkylineHook.TerraExplorer.FindItem(ConfigurationManager.AppSettings["SWBuffer"]), 1);
            m_SkylineHook.TerraExplorer.SetVisibility(m_SkylineHook.TerraExplorer.FindItem(ConfigurationManager.AppSettings["RWBuffer"]), 1);
            m_SkylineHook.TerraExplorer.SetVisibility(m_SkylineHook.TerraExplorer.FindItem(ConfigurationManager.AppSettings["APBuffer"]), 1);

            m_Subway = CreateLine(ConfigurationManager.AppSettings["Subway"], x, y, 2, 0x00ffff);
            m_Railway = CreateLine(ConfigurationManager.AppSettings["Railway"], x, y, 2, 0x00ff00);
            m_Airport = CreateLine(ConfigurationManager.AppSettings["Airport"], x, y, 2, 0x0000ff);


            if (m_FrmAccess == null || m_FrmAccess.IsDisposed)
            {
                m_FrmAccess = new FrmAccessibility();
                m_FrmAccess.FormClosed += new FormClosedEventHandler(m_FrmAccess_FormClosed);
            }

            if (m_FrmAccess.Visible == false)
                m_FrmAccess.Show(this.m_Hook.UIHook.MainForm);

        }

        ITerrainPolyline61 CreateLine(string strConfig, object x, object y, object z, int color)
        {
            char[] cSplit = { ',' };
            string[] array = strConfig.Split(cSplit, StringSplitOptions.RemoveEmptyEntries);
            double[] pArray = new double[array.Length + 3];
            pArray[0] = Convert.ToDouble(x);
            pArray[1] = Convert.ToDouble(y);
            pArray[2] = Convert.ToDouble(z);

            for (int i = 0; i < array.Length; i++)
            {
                pArray[3 + i] = Convert.ToDouble(array[i]);
            }

            return m_SkylineHook.SGWorld.Creator.CreatePolylineFromArray(pArray, color, AltitudeTypeCode.ATC_TERRAIN_RELATIVE);
        }

        void m_FrmAccess_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_Subway != null)
            {
                m_SkylineHook.TerraExplorer.DeleteItem(m_Subway.TreeItem.ItemID);
            }
            if (m_Railway != null)
            {
                m_SkylineHook.TerraExplorer.DeleteItem(m_Railway.TreeItem.ItemID);
            }
            if (m_Airport != null)
            {
                m_SkylineHook.TerraExplorer.DeleteItem(m_Airport.TreeItem.ItemID);
            }

            int iid = m_SkylineHook.TerraExplorer.FindItem(ConfigurationManager.AppSettings["SWBuffer"]);
            if (iid > 0)
                m_SkylineHook.TerraExplorer.SetVisibility(iid, -1);

            iid = m_SkylineHook.TerraExplorer.FindItem(ConfigurationManager.AppSettings["RWBuffer"]);
            if (iid > 0)
                m_SkylineHook.TerraExplorer.SetVisibility(iid, -1);

            iid = m_SkylineHook.TerraExplorer.FindItem(ConfigurationManager.AppSettings["APBuffer"]);
            if (iid > 0)
                m_SkylineHook.TerraExplorer.SetVisibility(iid, -1);
        }
    }
}
