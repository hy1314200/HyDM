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
    public partial class FrmQueryCoordinate :FrmBase
    {
        private Form _frmMain;
        public FrmQueryCoordinate(Form frmMain)
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
        /// 窗体登陆事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmQueryCoordinate_Load(object sender, EventArgs e)
        {
            base.FrmName = "查询坐标";
            Program.TE.OnLButtonDown += new TerraExplorerX._ITerraExplorerEvents5_OnLButtonDownEventHandler(TE_OnLButtonDown);
            Program.pRender.SetMouseInputMode(MouseInputMode.MI_COM_CLIENT);
        }

        /// <summary>
        /// TE左键单击事件
        /// 获取当点鼠标点击坐标 等信息并显示
        /// </summary>
        /// <param name="Flags"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="pbHandled"></param>
        void TE_OnLButtonDown(int Flags, int X, int Y, ref object pbHandled)
        {
            object objType, longitude, height, latitude, objID;
            objType =16;
            try
            {
                Program.TE.ScreenToWorld(X, Y, ref objType, out longitude, out height, out latitude, out objID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;

            }
           
            this.lab_X.Text = X.ToString();
            this.lab_Y.Text = Y.ToString();
            this.lab_longitude.Text = TransformationFormat(longitude.ToString());
            this.lab_latitude.Text = TransformationFormat(latitude.ToString());
            this.lab_height.Text = height.ToString()+"米";
        }
        private string TransformationFormat(string Coor)
        {
            string newStr = "";
          
            try
            {
                double DegreeDouble = Convert.ToDouble(Coor);
                int zh = (int)DegreeDouble;
                DegreeDouble = DegreeDouble - zh;
                DegreeDouble = DegreeDouble * 60;
                int DegreeInt = (int)DegreeDouble;
                double MiniDouble = DegreeDouble - DegreeInt;
                MiniDouble = MiniDouble * 60;
                // int MiniInt = (int)MiniDouble;
                double Secdouble = MiniDouble;

                Secdouble = Math.Round(Secdouble, 0);
                string DegreeStr = zh.ToString();
                string MiniStr = DegreeInt.ToString();
                string SecStr = Secdouble.ToString();
                newStr = DegreeStr + "度" + MiniStr + "分" + SecStr + "秒";
            }
            catch (Exception)
            {
             
            }
           
            return newStr;
        
        }
        /// <summary>
        /// 窗体关闭事件
        /// 注销TE事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmQueryCoordinate_FormClosing(object sender, FormClosingEventArgs e)
        {
            _frmMain.RemoveOwnedForm(this);
            Program.TE.OnLButtonDown -= new TerraExplorerX._ITerraExplorerEvents5_OnLButtonDownEventHandler(TE_OnLButtonDown);
            Program.pRender.SetMouseInputMode(MouseInputMode.MI_FREE_FLIGHT);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            TransformationFormat("114.40905228583");
        }
    }
}

