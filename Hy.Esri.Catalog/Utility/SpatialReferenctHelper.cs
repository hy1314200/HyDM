using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace Hy.Esri.Catalog.Utility
{
    /// <summary>
    /// 空间参考服务类
    /// </summary>
    public class SpatialReferenctHelper
    {
        /// <summary>
        /// 从Prj文件创建空间参考
        /// </summary>
        /// <param name="strFile"></param>
        /// <returns></returns>
        public static ISpatialReference CreateSpatialReference(string strFile)
        {
            ISpatialReferenceFactory spatialRefFactory = new SpatialReferenceEnvironment();

            return spatialRefFactory.CreateESRISpatialReferenceFromPRJFile(strFile);
        }

        /// <summary>
        /// GP的空间参考只接受字符串
        /// </summary>
        /// <param name="spatialRef"></param>
        /// <returns></returns>
        public static string ToGpString(ISpatialReference spatialRef)
        {
            if (spatialRef == null)
                return null;

            int strCount = -1;
            string gpString = null;
            (spatialRef as IESRISpatialReferenceGEN).ExportToESRISpatialReference(out gpString, out strCount);

            return gpString;
        }

        public static string ToDisplayString(ISpatialReference spatialRef)
        {
            if (spatialRef == null)
                return null;

            string strDisplay = "名称：";
            strDisplay += spatialRef.Name;


            return strDisplay;

        }
    }
}
