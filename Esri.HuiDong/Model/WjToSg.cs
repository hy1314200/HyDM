using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.HuiDong.Model
{
    public  class WjToSg
    {
        public int ID { get; set; }

        //public int SGID { get; set; }

        //public int WJID { get; set; }

        public SgMapping SG { get; set; }

        public WuJiang WJ { get; set; }


       public  string SDECode { get { return WJ.SDECode; } }

       public  string SDELayer { get { return WJ.SDELayer; } }

       public  string FeatureName { get { return WJ.FeatureName; } }

        public string Description { get { return WJ.Description; } }

       public  bool Checked { get { return SG != null; } }
    }
}
