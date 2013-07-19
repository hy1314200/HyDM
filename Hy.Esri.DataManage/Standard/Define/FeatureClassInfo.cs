using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hy.Metadata;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;


namespace Hy.Esri.DataManage.Standard
{
    public class FeatureClassInfo:TableInfo
    {
    //    /// <summary>
    //    /// 标识
    //    /// </summary>
    //    public string ID { get; set; }

    //    /// <summary>
    //    /// FeatureClass名
    //    /// </summary>
    //    public string Name { get; set; }

    //    /// <summary>
    //    /// 别名
    //    /// </summary>
    //    public string AliasName { get; set; }

        /// <summary>
        /// ShapeField名称
        /// </summary>
        public string ShapeFieldName { get; set; }

        /// <summary>
        /// FeatureType类型
        /// </summary>
        public esriFeatureType FeatureType{get;set;}

        /// <summary>
        /// 几何类型
        /// </summary>
        public esriGeometryType ShapeType { get; set; }

        ///// <summary>
        ///// 字段信息(对SubItems的引用，换个名)
        ///// </summary>
        //public IList<FieldInfo> FieldsInfo
        //{
        //    get { return this.SubItems; }
        //    set { this.SubItems = value; }
        //}

        /// <summary>
        /// 平均点数量
        /// </summary>
        public int AvgNumPoints { get; set; }

        /// <summary>
        /// 格网数量
        /// </summary>
        public int GridCount { get; set; }

        /// <summary>
        /// 格网大小
        /// </summary>
        public double GridSize { get; set; }

        /// <summary>
        /// 是否具有M值
        /// </summary>
        public bool HasM { get; set; }

        /// <summary>
        /// 是否有Y值
        /// </summary>
        public bool HasZ { get; set; }

        /// <summary>
        /// 空间参考
        /// </summary>
        public string SpatialReferenceString { get; set; }

        private ISpatialReference m_SpatialReference;
        public ISpatialReference SpatialReference
        {
            get
            {
                if (m_SpatialReference == null)
                {
                    if (!string.IsNullOrWhiteSpace(this.SpatialReferenceString))
                        m_SpatialReference = (new SpatialReferenceEnvironment()).CreateESRISpatialReferenceFromPRJ(this.SpatialReferenceString);

                    else
                        m_SpatialReference = (new UnknownCoordinateSystemClass());
                }
                return m_SpatialReference;
            }

            set
            {
                m_SpatialReference = value;
                this.SpatialReferenceString = null;
                if (m_SpatialReference != null)
                {
                    int strCount = -1;
                    string spatialReferenceString = null;
                    (m_SpatialReference as IESRISpatialReferenceGEN).ExportToESRISpatialReference(out spatialReferenceString, out strCount);
                    this.SpatialReferenceString = spatialReferenceString;
                }
            }
        }

        ///// <summary>
        ///// 父引用（可能是库标准、FeatureDataset或其它，作为扩展使用）
        ///// </summary>
        //public string Parent { get; set; }
    }
}
