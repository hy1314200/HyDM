using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyline.GuiHua.Bussiness
{
    /// <summary>
    /// 信号灯信息
    /// </summary>
    public class LampInfo
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路口宽
        /// </summary>
        public double CrossWidth { get; set; }

        /// <summary>
        /// 偏离正轴方向角度（360）
        /// </summary>
        public double RawOffset { get; set; }

        /// <summary>
        /// 路口名
        /// </summary>
        public string CrossName { get; set; }

        /// <summary>
        /// 方向
        /// </summary>
        public string DirectString { get; set; }

        /// <summary>
        /// X坐标
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// 最小理论高度
        /// </summary>
        public double LestHeight { get; set; }

        /// <summary>
        /// 高度是否超标
        /// </summary>
        public bool HeightFlag { get; set; }

        /// <summary>
        /// 视野是否被挡
        /// </summary>
        public bool ViewFlag { get; set; }

        public object TheLamp { get; set; }

        public object AssistantLamp { get; set; }

        /// <summary>
        /// Tag
        /// </summary>
        public object Tag { get; set; }

    }
}
