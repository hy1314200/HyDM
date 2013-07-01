using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Controls;

namespace Check.Task.CustomClass
{
    public  class VCTHeadInfo
    {
        public string strDataMark;

        /// <summary>
        /// 表示土地利用矢量数据文件的版本号,用2.0表示
        /// </summary>
        public string strVersion;

        /// <summary>
        /// 坐标单位.K表示公里,M表示米,D表示以度为单位的经纬度,S表示以度分秒表示的经纬度(此时坐标格式为DDDMMSS.SSSS, DDD为度, MM为分, SS.SSSS为秒)。本矢量数据交换格式中，平面坐标使用“M”，球面坐标采用“D”。
        /// </summary>
        public string strUnit;

        /// <summary>
        /// 坐标维数。2表示仅有二维坐标,3表示有三维坐标。三维时, 无论Unit如何定义,高程坐标单位均用米。
        /// </summary>
        public int nDim;

        /// <summary>
        /// 是否带结点与线段的拓扑关系。本矢量数据交换格式中，采用Topo:1。
        /// </summary>
        public int nTopo;

        /// <summary>
        /// 坐标系，G表示测量坐标系、M表示数学坐标系。
        /// </summary>
        public string strCoordinate;

        /// <summary>
        /// 投影类型
        /// </summary>
        public string strProjection;

        /// <summary>
        /// 参考椭球体
        /// </summary>
        public string strSpheroid;

        /// <summary>
        /// 投影参数
        /// </summary>
        public List<int> nParameters;

        /// <summary>
        /// 中央子午线经度，以度为最小单位
        /// </summary>
        public double dMeridian;

        /// <summary>
        /// 最小X坐标
        /// </summary>
        public double dMinX;

        /// <summary>
        /// 最小Y坐标
        /// </summary>
        public double dMinY;

        /// <summary>
        /// 最大X坐标
        /// </summary>
        public double dMaxX;

        /// <summary>
        /// 最大Y坐标
        /// </summary>
        public double dMaxY;

        /// <summary>
        /// 原图比例尺分母
        /// </summary>
        public string strScale;

        /// <summary>
        /// 外业调查完成的日期
        /// </summary>
        public DateTime date;

        /// <summary>
        /// 任意单字节非空白字符,用做属性字段分隔符。基本部分，缺省为半角字符逗号“,”。
        /// </summary>
        public char cSeparator;
    }
}
