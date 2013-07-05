using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common.Utility.Esri;
using Hy.Check.Define;


namespace Hy.Check.Task.DataImport
{
    public delegate void ImportingObjectChangedHandler(string strOjbectName);

    /// <summary>
    /// 数据导入接口
    /// </summary>
    public interface IDataImport
    {
        /// <summary>
        /// 消息处理器
        /// </summary>
        MessageHandler Messager { set; }

        /// <summary>
        /// 导入对象发生改变事件
        /// </summary>
        event ImportingObjectChangedHandler ImportingObjectChanged;

        /// <summary>
        /// 数据源路径
        /// </summary>
        string Datasource { set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        enumDataType DataType { set; }

        /// <summary>
        /// 目标（要导入到的）路径
        /// </summary>
        string TargetPath { set; }

        /// <summary>
        /// 是否只进行复制
        /// </summary>
        bool JustCopy { set; }

        ///// <summary>
        ///// 是否直接使用Base库作为Query库（MDB情况下方可为true）
        ///// </summary>
        //bool BaseAsQuery { set; }

        /// <summary>
        /// （目标数据库所使用的）空间参考
        /// </summary>
        ESRI.ArcGIS.Geometry.ISpatialReference SpatialReference { set; }

        /// <summary>
        /// 数据（统一）前缀，即若数据（Table，FeatureClass等）有统一的前缀如“GT_”时应该去掉
        /// </summary>
        string DataPrefix { set; }

        /// <summary>
        /// 方案ID
        /// </summary>
        string SchemaID { set; }

        /// <summary>
        /// 导入数据
        /// @remark 包括Query库的创建
        /// </summary>
        /// <returns></returns>
        bool Import();
    }
}
