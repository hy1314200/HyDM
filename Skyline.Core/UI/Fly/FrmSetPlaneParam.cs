using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using TerraExplorerX;

namespace Skyline.Core.UI
{
    public partial class FrmSetPlaneParam : DevExpress.XtraEditors.XtraForm
    {
        private ITerrainDynamicObject61 dynamicObj;
        private IPosition61 _Position61;
        private IRouteWaypoints61 pRouteWaypoints61;
        private bool Model = false;

        public FrmSetPlaneParam()
        {
            InitializeComponent();
           
        }
        public ITerrainDynamicObject61 GetDynamicObject
        {
            set { this.dynamicObj = (ITerrainDynamicObject61)value; }
            get { return this.dynamicObj; }
        
        }
      

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                dynamicObj.Pause = true;

                dynamicObj.Shadow.Show = true;

                dynamicObj.TurnSpeed = Convert.ToDouble(this.spinEdit1.EditValue);
                dynamicObj.SaveInFlyFile = true;
                if (this.Model)
                {
                    DynamicMotionStyle MotionStyle = this.dynamicObj.MotionStyle;
                    switch (MotionStyle)
                    {
                        case DynamicMotionStyle.MOTION_AIRPLANE:
                            //textEdit2.Text = "80";

                            this.dynamicObj.FileName = Application.StartupPath + @"\data\plane1.xpc";
                            this.dynamicObj.ScaleFactor = Convert.ToDouble(this.spinEdit3.EditValue);
                            break;
                        case DynamicMotionStyle.MOTION_GROUND_VEHICLE:
                            this.dynamicObj.ScaleFactor = Convert.ToDouble(this.spinEdit3.EditValue);
                            this.dynamicObj.FileName = Application.StartupPath + @"\data\nissan.xpc";
                            break;
                        case DynamicMotionStyle.MOTION_HELICOPTER:
                            this.dynamicObj.ScaleFactor = Convert.ToDouble(this.spinEdit3.EditValue);
                            this.dynamicObj.FileName = Application.StartupPath + @"\data\hel1.xpc";
                            // textEdit2.Text = "50";
                            break;
                        case DynamicMotionStyle.MOTION_HOVER:
                            this.dynamicObj.FileName = "";
                            break;
                        default:
                            break;
                    }
                    Program.TE.SelectItem(dynamicObj.TreeItem.ItemID, 0, 0);
                    dynamicObj.Pause = false;

                }
                int waypointCount = this.pRouteWaypoints61.Count;
                for (int i = 0; i < waypointCount; i++)
                {
                    IRouteWaypoint61 pRouteWaypoint = this.pRouteWaypoints61[i] as IRouteWaypoint61;
                    pRouteWaypoint.Speed = Convert.ToDouble(this.spinEdit2.EditValue);
                    pRouteWaypoint.Altitude = Convert.ToDouble(this.spinEdit4.Value);
                }
            }
            catch (Exception)
            {
             
            }
       
       
        }

        private void checkButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkButton1.Checked)
            {
                Program.pNavigate6.UndergroundMode = true;
            }
            else
            {
                Program.pNavigate6.UndergroundMode = false;
            }

        }

        private void FrmSetPlaneParam_Load(object sender, EventArgs e)
        {
            //this.comboBoxEdit2.SelectedIndex = 0;
            //this.comboBoxEdit3.SelectedIndex = 0;
            try
            {
                //相对高度

                _Position61 = dynamicObj.Position;
                pRouteWaypoints61 = dynamicObj.Waypoints;//路点
                IRouteWaypoint61 pRouteWaypoint = this.pRouteWaypoints61[0] as IRouteWaypoint61;
                spinEdit2.Value = Convert.ToDecimal(pRouteWaypoint.Speed);
                spinEdit1.Value = Convert.ToDecimal(dynamicObj.TurnSpeed);
                this.spinEdit4.Value = Convert.ToDecimal(pRouteWaypoint.Altitude);
                if (this.dynamicObj.DynamicType == DynamicObjectType.DYNAMIC_VIRTUAL)
                {
                    this.comboBoxEdit2.SelectedIndex = 0;
                    this.Model = false;
                    comboBoxEdit3.Enabled = false;
                }
                else
                {
                    this.comboBoxEdit2.SelectedIndex = 1;
                    this.Model = true;
                    comboBoxEdit3.Enabled = true;
                }

                if (this.dynamicObj.MotionStyle == DynamicMotionStyle.MOTION_AIRPLANE)
                {
                    this.comboBoxEdit3.SelectedIndex = 1;
                }
                else if (this.dynamicObj.MotionStyle == DynamicMotionStyle.MOTION_HELICOPTER)
                {
                    this.comboBoxEdit3.SelectedIndex = 2;
                }
                else if (this.dynamicObj.MotionStyle == DynamicMotionStyle.MOTION_GROUND_VEHICLE)
                {
                    this.comboBoxEdit3.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
          
            }
            


        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            dynamicObj.Pause = true;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            dynamicObj.Pause = false;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            
           // Program.TE.Save();
            this.Close();
            
        }

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            this.simpleButton3.Visible = false;
        }

        private void buttonEdit1_Properties_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Title = "加载模型";
            this.openFileDialog1.Filter = "";

        }

        private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit2.SelectedIndex == 0)
            {
                this.dynamicObj.DynamicType = DynamicObjectType.DYNAMIC_VIRTUAL; 
                this.Model = false;
                comboBoxEdit3.Enabled = false;
            }
            else
            {
                this.dynamicObj.DynamicType = DynamicObjectType.DYNAMIC_3D_MODEL; 
                this.Model = true;
                comboBoxEdit3.Enabled = true;
                this.dynamicObj.MotionStyle = DynamicMotionStyle.MOTION_GROUND_VEHICLE;
            }
        }
        /// <summary>
        /// 不同模型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxEdit3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.comboBoxEdit3.SelectedIndex;
            switch (index)
            {
                case 0:
                    this.dynamicObj.MotionStyle = DynamicMotionStyle.MOTION_GROUND_VEHICLE;
                    this.spinEdit3.Value = Convert.ToDecimal(0.01);
                    this.spinEdit2.Value = Convert.ToDecimal(10);
                    this.spinEdit1.Value = Convert.ToDecimal(10);
                    this.spinEdit4.Value = Convert.ToDecimal(0.1);
                    break;
                case 1:
                    this.dynamicObj.MotionStyle = DynamicMotionStyle.MOTION_AIRPLANE;
                    this.spinEdit3.Value = Convert.ToDecimal(1);
                    this.spinEdit2.Value = Convert.ToDecimal(100);
                    this.spinEdit1.Value = Convert.ToDecimal(40);
                    this.spinEdit4.Value = Convert.ToDecimal(80);
                    break;
                case 2:
                    this.dynamicObj.MotionStyle = DynamicMotionStyle.MOTION_HELICOPTER;
                    this.spinEdit3.Value = Convert.ToDecimal(1);
                    this.spinEdit2.Value = Convert.ToDecimal(50);
                    this.spinEdit1.Value = Convert.ToDecimal(50);
                    this.spinEdit4.Value = Convert.ToDecimal(100);
                    break;
                case 3:
                    this.dynamicObj.MotionStyle = DynamicMotionStyle.MOTION_HOVER;
                    break;
                default:
                    break;
            }
        }

        private void spinEdit3_EditValueChanged(object sender, EventArgs e)
        {

        }
        
    }
}