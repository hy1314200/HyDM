using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hy.Metadata;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;


namespace Hy.Esri.DataManage
{
    public class LayerInfo
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string AliasName { get; set; }

        public esriFeatureType FeatureType{get;set;}

        public esriGeometryType ShapeType { get; set; }

        public IList<FieldInfo> FieldsInfo { get; set; }

        public int AvgNumPoints { get; set; }

        public int GridCount { get; set; }

        public double GridSize { get; set; }

        public bool HasM { get; set; }

        public bool HasZ { get; set; }

        public string SpatialReference { get; set; }
    }
}
