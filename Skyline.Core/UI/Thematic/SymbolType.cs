using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Skyline.Core.UI
{
    public class SymbolType
    {

        private PointSymbol _pCurrentPointSymbol = new PointSymbol { PointType = "Circle", 
                                                                     PointSize = 1000, 
                                                                    // PointSizeClass = "NoClass",
                                                                    // PointSizeClass2 = "NoClass",
                                                                     //NumOfSidesClass = "NoClass",
                                                                     //PointFillcolorClass = "NoClass",
                                                                     //PointFillOpacityClass = "NoClass"
                                                                    };
        private PolylineSymbol _pCurrentPolylineSymbol = new PolylineSymbol { PolylineType = "Solidline", 
                                                                              PolylineWidth = 10, 
                                                                              PolylineBackColor = Color.Black,
                                                                              PolylineBackOpacity = 0,
                                                                              //PolylineWidthClass = "NoClass",
                                                                             // PolylineColorClass = "NoClass",
                                                                             // PolylineOpacityClass = "NoClass",
                                                                             // PolylineBackColorClass = "NoClass",
                                                                              //PolylineBackOpacityClass = "NoClass"
                                                                            };
        private PointSymbol _pPrePointSymbol = new PointSymbol { PointType = "Circle", 
                                                                     PointSize = 1000,
                                                                 //PointSizeClass = "NoClass",
                                                                // PointSizeClass2 = "NoClass",
                                                                 //NumOfSidesClass = "NoClass",
                                                                 //PointFillcolorClass = "NoClass",
                                                                 //PointFillOpacityClass = "NoClass"
                                                               };
        private PolylineSymbol _pPrePolylineSymbol = new PolylineSymbol { PolylineType = "Solidline", 
                                                                              PolylineWidth = 10, 
                                                                              PolylineBackColor = Color.Black,
                                                                              PolylineBackOpacity = 0,
                                                                          //PolylineWidthClass = "NoClass",
                                                                          //PolylineColorClass = "NoClass",
                                                                          //PolylineOpacityClass = "NoClass",
                                                                          //PolylineBackColorClass = "NoClass",
                                                                          //PolylineBackOpacityClass = "NoClass"
                                                                         };
        private PolygonSymbol _pPrePolygonSymbol = new PolygonSymbol { /*PolygonFillcolorClass = "NoClass", PolygonFillOpacityClass = "NoClass"*/ };

        public PointSymbol CurrentPointSymbol
        {
            get { return _pCurrentPointSymbol; }
            set { _pCurrentPointSymbol = value; }
        }
        public PolylineSymbol CurrentPolylineSymbol
        {
            get { return _pCurrentPolylineSymbol; }
            set { _pCurrentPolylineSymbol = value; }
        }
        public PointSymbol PrePointSymbol
        {
            get { return _pPrePointSymbol; }
            set { _pPrePointSymbol = value; }
        }
        public PolylineSymbol PrePolylineSymbol
        {
            get { return _pPrePolylineSymbol; }
            set { _pPrePolylineSymbol = value; }
        }
        public PolygonSymbol PrePolygonSymbol
        {
            get { return _pPrePolygonSymbol; }
            set { _pPrePolygonSymbol = value; }
        }
     
    }
     public struct PointSymbol
    {
        public string PointType;
        public double PointSize;
        public double PointSize2;
        public string PointSizeClass;
        public string PointSizeClass2;
        public int NumOfSides;
        public string NumOfSidesClass;
        public Color PointFillcolor;
        public string PointFillcolorClass;
        public double PointFillOpacity;
        public string PointFillOpacityClass;
        public int AltitMethod;
    }
     public struct PolylineSymbol
     {
         public string PolylineType;
         public double PolylineWidth;
         public string PolylineWidthClass;
         public Color PolylineColor;
         public string PolylineColorClass;
         public double PolylineOpacity;
         public string PolylineOpacityClass;
         public Color PolylineBackColor;
         public string PolylineBackColorClass;
         public int PolylineBackOpacity;
         public string PolylineBackOpacityClass;
         public int AltitMethod;

     }
     public struct PolygonSymbol
     {
         public Color PolygonFillcolor;
         public string PolygonFillcolorClass;
         public double PolygonFillOpacity;
         public string PolygonFillOpacityClass;
         public int AltitMethod;
     }

}

