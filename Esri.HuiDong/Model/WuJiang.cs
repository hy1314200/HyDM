using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.HuiDong.Model
{
    public class WuJiang
    {
       public  int ID { get; set; }
       
       public  string SDECode { get; set; }

       public  string SDELayer { get; set; }

       public  string FeatureName { get; set; }

       public  string Description { get; set; }

       public enumLayerType LayerType { get; set; }
    }

    public enum enumLayerType
    {
        点=1,
        线=2,
        面=3,
        注记=4
    }
}
