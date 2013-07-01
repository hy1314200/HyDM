using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessing;
using IWorkspaceFactory=ESRI.ArcGIS.Geodatabase.IWorkspaceFactory;

namespace Check.Rule.Helper
{
    /// <summary>
    /// 涉及到Engine操作的一些共用函数。
    /// </summary>
    public class TopoHelper
    {
        /* -------------------------------------------------------------------- */
        /*                            拓扑规则名称定义                          */
        /* -------------------------------------------------------------------- */


        /// <summary>
        /// 点要素拓扑规则
        /// </summary>
        //1
        public const string TopoName_PointCoveredByLineEndpoint = "点要素必须被线的端点覆盖";
        //2
        public const string TopoName_PointCoveredByLine = "点必须被线覆盖";
        //3
        public const string TopoName_PointProperlyInsideArea = "点必须完全位于面内部";
        //4
        public const string TopoName_PointCoveredByAreaBoundary = "点必须被面边界覆盖";

        //---------------线要素拓扑规则-------
        //1
        public const string TopoName_LineNoOverlap = "线层内要素不互相重叠";
        //2
        public const string TopoName_LineNoIntersection = "线层内要素不互相相交";
        //3
        public const string TopoName_LineNoDangles = "线层内要素没有悬挂节点";
        //4
        public const string TopoName_LineNoPseudos = "线层内要素没有伪节点";
        //5-
        public const string TopoName_LineNoIntersectOrInteriorTouch = "线层内要素不能相交或内部相切";
        //6
        public const string TopoName_LineNoOverlapLine = "线层与线层间要素不相互重叠";
        //7
        public const string TopoName_LineCoveredByLineClass = "线层要素必须被另一线层要素覆盖";
        //8
        public const string TopoName_LineCoveredByAreaBoundary = "线层必须被面层边界覆盖";
        //9
        public const string TopoName_LineEndpointCoveredByPoint = "线层要素端点必须被点层覆盖";
        //10
        public const string TopoName_LineNoSelfOverlap = "线层内要素不自重叠";
        //11
        public const string TopoName_LineNoSelfIntersect = "线层内要素不自相交";
        //12-
        public const string TopoName_LineNoMultipart = "线层内要素必须为单部分";

        //---------------面要素拓扑规则---------
        //1
        public const string TopoName_AreaNoOverlap = "面层内要素不相互重叠";
        //2
        public const string TopoName_AreaNoGaps = "面层内要素之间没有缝隙";
        //3
        public const string TopoName_AreaNoOverlapArea = "面层不和另一面层重叠";
        //4
        public const string TopoName_AreaCoveredByAreaClass = "面层内要素被另一个面层要素覆盖";
        //5
        public const string TopoName_AreaAreaCoverEachOther = "面层和另一个面层相互覆盖";
        //6-
        public const string TopoName_AreaCoveredByArea = "面层必须被另一个面层覆盖";
        //7-
        public const string TopoName_AreaBoundaryCoveredByLine = "面层边界被另一个线层覆盖";
        //8
        public const string TopoName_AreaBoundaryCoveredByAreaBoundary = "面层和另一面层边界一致";
        //9
        public const string TopoName_AreaContainPoint = "面层包含点层要素";


        /// <summary>
        /// 根据本系统字段类型编号转换得到esri字段类型
        /// </summary>
        /// <param name="nFldType"></param>
        /// <returns></returns>
        public static esriFieldType en_GetEsriFieldByEnum(int nFldType)
        {
            esriFieldType esriFldType = esriFieldType.esriFieldTypeSmallInteger;

            switch (nFldType)
            {
                case 1:
                    {
                        esriFldType = esriFieldType.esriFieldTypeOID;
                        break;
                    }
                case 2:
                    {
                        esriFldType = esriFieldType.esriFieldTypeInteger;
                        break;
                    }
                case 3:
                    {
                        esriFldType = esriFieldType.esriFieldTypeSingle;
                        break;
                    }
                case 4:
                    {
                        esriFldType = esriFieldType.esriFieldTypeDouble;
                        break;
                    }
                case 5:
                    {
                        esriFldType = esriFieldType.esriFieldTypeString;
                        break;
                    }
                case 6:
                    {
                        esriFldType = esriFieldType.esriFieldTypeDate;
                        break;
                    }
                case 8:
                    {
                        esriFldType = esriFieldType.esriFieldTypeBlob;
                        break;
                    }
            }

            return esriFldType;
        }

        /// <summary>
        /// 根据esri字段类型转换得到字段名称
        /// </summary>
        /// <param name="esriFldType"></param>
        /// <returns></returns>
        public static string en_GetFieldTypebyEsriField(esriFieldType esriFldType)
        {
            string strFldType = "未知类型";

            switch (esriFldType)
            {
                case esriFieldType.esriFieldTypeOID:
                    {
                        strFldType = "唯一标志码类型";
                        break;
                    }
                case esriFieldType.esriFieldTypeInteger:
                    {
                        strFldType = "整形";
                        break;
                    }
                case esriFieldType.esriFieldTypeSingle:
                    {
                        strFldType = "单精度浮点型";
                        break;
                    }
                case esriFieldType.esriFieldTypeDouble:
                    {
                        strFldType = "双精度浮点型";
                        break;
                    }
                case esriFieldType.esriFieldTypeString:
                    {
                        strFldType = "字符型";
                        break;
                    }
                case esriFieldType.esriFieldTypeDate:
                    {
                        strFldType = "日期型";
                        break;
                    }
                case esriFieldType.esriFieldTypeBlob:
                    {
                        strFldType = "大二进制类型";
                        break;
                    }
            }

            return strFldType;
        }

        /// <summary>
        /// 根据esri字段类型，判断sql语句查询是否需要''
        /// </summary>
        /// <param name="esriFldType"></param>
        /// <returns>返回true,则说明是string型，需要''</returns>
        public static bool en_GetTypebyEsriField(esriFieldType esriFldType)
        {
            bool bTest = true;

            switch (esriFldType)
            {
                case esriFieldType.esriFieldTypeOID:
                case esriFieldType.esriFieldTypeInteger:
                case esriFieldType.esriFieldTypeSingle:
                case esriFieldType.esriFieldTypeDouble:
                    {
                        bTest = false;
                        break;
                    }
                case esriFieldType.esriFieldTypeString:
                case esriFieldType.esriFieldTypeDate:
                case esriFieldType.esriFieldTypeBlob:
                    {
                        bTest = true;
                        break;
                    }
            }

            return bTest;
        }

    }
}