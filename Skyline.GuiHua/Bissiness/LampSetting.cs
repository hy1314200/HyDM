using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyline.GuiHua.Bussiness
{
    /// <summary>
    /// 信号灯设置信息
    /// </summary>
    public class LampSetting
    {
        public LampSetting()
        {
            MostLampHeight = 6;
            MostCarHeight = 4;
            LestSetHeight = 1;
            MostCarLength = 10;
            MustViewDistance = 40;
        }
        /// <summary>
        /// 允许最大灯高
        /// </summary>
        public double MostLampHeight { get; set; }
        /// <summary>
        /// 汽车最大高度
        /// </summary>
        public double MostCarHeight { get; set; }
        /// <summary>
        /// 最低车座高度
        /// </summary>
        public double LestSetHeight { get; set; }
        /// <summary>
        /// 最大车长度
        /// </summary>
        public double MostCarLength { get; set; }

        /// <summary>
        /// 最小必须可见距离（指到路口）
        /// </summary>
        public double MustViewDistance { get; set; }
    }
}
