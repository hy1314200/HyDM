using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.HuiDong.Model
{
    public class SgMapping
    {
       public int ID { get; set; }

       public  string CADCode { get; set; }

       public string CADLayer { get; set; }

        public string SDECode { get; set; }

        public string SDELayer { get; set; }

        public string LayerType { get; set; }

        public string FeatureName { get; set; }

        public string Description { get; set; }
    }
}
