using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Core
{
    public class BuilderObject
    {
        //共有8个字段
        private int _builID;  // 物体编号 自增
        private string _builName;  // 物体名称
      //  private string _builItemID;  // 物体信息树ID  使用时注意转型int
        private string _builHeight;  // 物体高度 使用时注意转型double
        private string _builArea;  // 物体面积 使用时注意转型double
        private string _builInfo;  // 物体说明信息
        private string _builLongitude;  // 物体经度 使用时注意转型double
        private string _builLatitude;  // 物体纬度 使用时注意转型double
        private string _builObjectid;
        /// <summary>
        /// 物体纬度 使用时注意转型double
        /// </summary>
        public string BuilLatitude
        {
            get { return _builLatitude; }
            set { _builLatitude = value; }
        }
	

        /// <summary>
        /// 物体经度 使用时注意转型double
        /// </summary>
        public string BuilLongitude
        {
            get { return _builLongitude; }
            set { _builLongitude = value; }
        }
	

        /// <summary>
        /// 物体说明信息
        /// </summary>
        public string BuilInfo
        {
            get { return _builInfo; }
            set { _builInfo = value; }
        }
	


        /// <summary>
        /// 物体面积 使用时注意转型double
        /// </summary>
        public string BuilArea
        {
            get { return _builArea; }
            set { _builArea = value; }
        }
	

        /// <summary>
        /// 物体高度 使用时注意转型double
        /// </summary>
        public string BuilHeight
        {
            get { return _builHeight; }
            set { _builHeight = value; }
        }
	

        /// <summary>
        /// 物体信息树ID  使用时注意转型int
        /// </summary>
        //public string BuilItemID
        //{
        //    get { return _builItemID; }
        //    set { _builItemID = value; }
        //}
	

        /// <summary>
        /// 物体名称
        /// </summary>
        public string BuilName
        {
            get { return _builName; }
            set { _builName = value; }
        }
	

        /// <summary>
        /// 物体编号 自增
        /// </summary>
        public int BuilId
        {
            get { return _builID; }
            set { _builID = value; }
        }
        public string BuidObjectID
        {
            get { return _builObjectid; }
            set { _builObjectid = value; }
        }
    }
}



