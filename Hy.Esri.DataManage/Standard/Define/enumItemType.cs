using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Esri.DataManage.Standard
{
    public enum enumItemType
    {
        /// <summary>
        /// 标准
        /// </summary>
        Standard=0,

        /// <summary>
        /// FeatureDataset
        /// </summary>
        FeatureDataset=1,

        /// <summary>
        /// FeatureClass(Layer)
        /// </summary>
        FeatureClass=11,

        RasterCatalog=2,

        Rasterset=21,

        RasterMosaic=22,

        Table=3
    }
}
