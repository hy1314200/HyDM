using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

using System.IO;

namespace Hy.Esri.Utility
{
    public class SpatialReferenceHelper
    {
        public static string ToDisplayString(ISpatialReference spatialRef)
        {
            if (spatialRef == null)
                return "";

            if (spatialRef is IUnknownCoordinateSystem)
                return "未知坐标系";

            string strDisplay = "";
            IGeographicCoordinateSystem geoCoord = spatialRef as IGeographicCoordinateSystem;
            IProjectedCoordinateSystem prjCood=spatialRef as IProjectedCoordinateSystem;
            if (prjCood != null)
            {
                geoCoord = prjCood.GeographicCoordinateSystem;

                strDisplay += string.Format("投影：　　　　　{0}\r\n", prjCood.Projection.Name);
                strDisplay += string.Format("东移假定值(X0)：{0}\r\n", prjCood.FalseEasting);
                strDisplay += string.Format("北移假定值(Y0)：{0}\r\n", prjCood.FalseNorthing);
                strDisplay += string.Format("中心轴：　　　　{0}\r\n", prjCood.get_CentralMeridian(true));
                strDisplay += string.Format("比例系数：　　　{0}\r\n", prjCood.ScaleFactor);
                //strDisplay += string.Format("源点经度：　　　{0}\r\n", prjCood.LongitudeOfOrigin);
                strDisplay += string.Format("线性单位：　　　{0}\r\n", prjCood.CoordinateUnit.Name);
                strDisplay += "\r\n";
            }

            if (geoCoord != null)
            {  
                strDisplay += string.Format("地理坐标系：　　{0}\r\n", geoCoord.Name);
                strDisplay += string.Format("角单位：　　　　{0}({1})\r\n", geoCoord.CoordinateUnit.Name, geoCoord.CoordinateUnit.RadiansPerUnit);
                strDisplay += string.Format("本初子午线：　　{0}({1})\r\n", geoCoord.PrimeMeridian.Name, geoCoord.PrimeMeridian.Longitude);
                strDisplay += string.Format("基准面：　　　　{0}\r\n", geoCoord.Datum.Name); 
                strDisplay += string.Format("　　椭球：　　　{0}\r\n", geoCoord.Datum.Spheroid.Name);
                strDisplay += string.Format("　　　　长半轴：{0}\r\n", geoCoord.Datum.Spheroid.SemiMajorAxis);
                strDisplay += string.Format("　　　　短半轴：{0}\r\n", geoCoord.Datum.Spheroid.SemiMinorAxis);
            }

            return strDisplay;
        }

        public static void SaveToFile(ISpatialReference spatialRef, string strFile)
        {
            File.WriteAllText(strFile, ToPrjString(spatialRef));
        }

        public static string ToPrjString(ISpatialReference spatialRef)
        {
            if (spatialRef == null)
                spatialRef = new UnknownCoordinateSystemClass();

            int strCount = -1;
            string prjString = null;
            (spatialRef as IESRISpatialReferenceGEN).ExportToESRISpatialReference(out prjString, out strCount);

            return prjString;
        }

        public static ISpatialReference FromPrjString(string prjString)
        {
            ISpatialReference spatialRef = null;
            int temp = 0;
            try
            {
                (new SpatialReferenceEnvironment()).CreateESRISpatialReference(prjString, out spatialRef, out temp);
            }
            catch
            {
            }
            return spatialRef;
        }

        public static ISpatialReference FromPrjFile(string prjFile)
        {
            try
            {
                return (new SpatialReferenceEnvironment()).CreateESRISpatialReferenceFromPRJFile(prjFile);
            }
            catch
            {
                return null;
            }
        }
    }
}
