using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Esri.Catalog.Define
{
    /// <summary>
    /// Catalog类型
    /// </summary>
    public enum enumCatalogType
    {
        /// <summary>
        /// 未定义
        /// </summary>
        Undefine=-1,
        /// <summary>
        /// Workspace
        /// </summary>
        Workpace = 0,
        /// <summary>
        /// FeatureDataset
        /// </summary>
        FeatureDataset = 1,
        /// <summary>
        /// FeatureClass点
        /// </summary>
        FeatureClassPoint = 12,
        /// <summary>
        /// 线
        /// </summary>
        FeatureClassLine=13,
        /// <summary>
        /// 面
        /// </summary>
        FeatureClassArea=14,
        /// <summary>
        /// 注记
        /// </summary>
        FeatureClassAnnotation=15,
        /// <summary>
        /// 空，指FeatureClass中的其它非3维类型
        /// </summary>
        FeatureClassEmpty=16,
        /// <summary>
        /// 3维FeatureClass
        /// </summary>
        FeatureClass3D = 11,
        /// <summary>
        /// 栅格数据目录
        /// </summary>
        RasterCatalog = 21,
        /// <summary>
        /// 栅格数据集
        /// </summary>
        RasterSet = 22,
        /// <summary>
        /// 栅格色带
        /// </summary>
        RasterBand = 23,
        /// <summary>
        /// 镶嵌
        /// </summary>
        RasterMosaic=24,
        /// <summary>
        /// 拓扑
        /// </summary>
        Topology = 3,

        // 3维
        /// <summary>
        /// Tin
        /// </summary>
        Tin = 4,
        /// <summary>
        /// Terrain
        /// </summary>
        Terrain = 5,

        /// <summary>
        /// 纯属性表
        /// </summary>
        Table = 6,

        /// <summary>
        /// 3D模型
        /// </summary>
        ThreeDimenModel=7
    }
}
