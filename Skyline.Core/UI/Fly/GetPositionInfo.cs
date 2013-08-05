//*****************************************
//类作用：获取当前当前视角位置信息
//制作者：张牧
//日期：2009-5-13
//版本：1.0
//说明：类需要TerraExplorerClass对象  通过Program.TE调用
//*****************************************

using System;
using System.Collections.Generic;
using System.Text;
using TerraExplorerX;


namespace Skyline.Core.UI
{
    /// <summary>
    /// 获取当前视角位置信息
    /// </summary>
    public class GetPositionInfo
    {
        //8个参数
        private double _longitude;// 当前视角经度
        private double _latitude;// 当前视角纬度
        private double _height;// 当前视角高度
        private double _yaw;// 当前视角方位
        private double _pitch;//  当前视角上下倾斜角度
        private double _roll;// 当前视角左右倾斜角度
        private double _cameraDeltaYaw;// 照相机三角架方位
        private double _cameraDeltaPitch;// 照相机三角架上下倾斜角度

        #region -------------------------属性-------------------------

        /// <summary>
        /// 照相机三角架上下倾斜角度
        /// </summary>
        public double CameraDeltaPitch
        {
            get { return _cameraDeltaPitch; }
            set { _cameraDeltaPitch = value; }
        }


        /// <summary>
        /// 照相机三角架方位
        /// </summary>
        public double CameraDeltaYaw
        {
            get { return _cameraDeltaYaw; }
            set { _cameraDeltaYaw = value; }
        }


        /// <summary>
        /// 当前视角左右倾斜角度
        /// </summary>
        public double Roll
        {
            get { return _roll; }
            set { _roll = value; }
        }


        /// <summary>
        ///  当前视角上下倾斜角度
        /// </summary>
        public double Pitch
        {
            get { return _pitch; }
            set { _pitch = value; }
        }


        /// <summary>
        /// 当前视角方位
        /// </summary>
        public double Yaw
        {
            get { return _yaw; }
            set { _yaw = value; }
        }

        /// <summary>
        /// 当前视角高度
        /// </summary>
        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }


        /// <summary>
        /// 当前视角纬度
        /// </summary>
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        /// <summary>
        /// 当前视角经度
        /// </summary>
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        #endregion -------------------------属性-------------------------

        /// <summary>
        /// 构造方法
        /// </summary>
        public GetPositionInfo()
        {
           
        }


        /// <summary>
        /// 获取当前当前视角位置信息
        /// 这个“视角位置”是怎么定义的？
        /// </summary>
        public void GetPosition()
        {
            try
            {
                object longitude, latitude, height, yaw, pitch, roll, careraDeltaYaw, cameraDeltaPitch;
                Program.TE.IPlane5_GetPosition(out longitude, out latitude, out height, out yaw, out pitch, out roll, out careraDeltaYaw, out cameraDeltaPitch);
                this.Longitude = Convert.ToDouble(longitude.ToString());
                this.Latitude = Convert.ToDouble(latitude.ToString());
                this.Height = Convert.ToDouble(height.ToString());
                this.Yaw = Convert.ToDouble(yaw.ToString());
                this.Pitch = Convert.ToDouble(pitch.ToString());
                this.Roll = Convert.ToDouble(roll.ToString());
                this.CameraDeltaYaw = Convert.ToDouble(careraDeltaYaw.ToString());
                this.CameraDeltaPitch = Convert.ToDouble(cameraDeltaPitch.ToString());
            }
            catch (Exception)
            {
                
               
            }
           
        }

    }
}
