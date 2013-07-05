using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Check.Define
{
    /// <summary>
    /// 为拓扑规则开放入口，设置拓扑规则需要的环境
    /// </summary>
    public interface ITopologicalRule:ICheckRule
    {
        /// <summary>
        /// 设置拓扑规则需要的Topology，若需要FeatureDataset，请自行从此属性获取
        /// </summary>
        ESRI.ArcGIS.Geodatabase.ITopology Topology { set; }

        /// <summary>
        /// Rank
        /// </summary>
        Dictionary<string, int> RankDictionary { set; }
    }
}
