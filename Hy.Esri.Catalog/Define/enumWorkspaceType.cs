using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Esri.Catalog.Define
{
    /// <summary>
    /// Workspace类型
    /// </summary>
    public enum enumWorkspaceType
    {
        /// <summary>
        /// SDE，指远程型的，SQLServer，Oracle
        /// </summary>
        SDE,
        /// <summary>
        /// FileGDB
        /// </summary>
        FileGDB,
        /// <summary>
        /// PGDB（MDB）
        /// </summary>
        PGDB,
        /// <summary>
        /// 应该叫Folder更合适
        /// 对Feature，指Shp，对Raster，指文件型
        /// </summary>
        File,
        /// <summary>
        /// 未知类型
        /// </summary>
        Unknown
    }
}
