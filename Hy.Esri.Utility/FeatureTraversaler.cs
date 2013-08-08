using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.Utility
{
    public class FeatureTraversaler
    {
        public IFeatureClass Source { private get; set; }

        public IQueryFilter QueryFilter { private get; set; }

        public void Traversal()
        {
        }
    }
}
