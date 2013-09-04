using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Esri.Catalog.Define
{
    /// <summary>
    /// 三维模型项
    /// </summary>
    public interface I3DModelCatalogItem:ICatalogItem
    {
        /// <summary>
        /// FeatureClass 名
        /// </summary>
        string ClassName { get; }

        /// <summary>
        /// Feature OID
        /// </summary>
        int FeatureOID { get; }

        /// <summary>
        /// 模型数据在File3DRegister表中的标识
        /// </summary>
        string DataID { get; }

        /// <summary>
        /// 模型路径
        /// </summary>
        string ModelPath { get; }

        /// <summary>
        /// 属性数据OID
        /// </summary>
        int AttributeOID { get; }
    }
}
