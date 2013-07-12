using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.HuiDong.Model
{
    public class WjToSg
    {
        public int ID { get; set; }

        //public int SGID { get; set; }

        //public int WJID { get; set; }

        public SgMapping SG { get; set; }

        public WuJiang WJ { get; set; }


        public string 要素编码 { get { return SG.SDECode; } }

        public string 图层 { get { return SG.SDELayer; } }

        public string 图层类型 { get { return SG.LayerType; } }

        public string 要素名称 { get { return SG.FeatureName; } }

        public string 描述 { get { return SG.Description; } }

        public string Cass代码 { get { return SG.CADCode; } }

        public bool 已映射 { get { return WJ != null; } }
    }
}
