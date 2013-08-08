using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyline.GuiHua.Bussiness
{
   public class SchemaInfo
   {
       public string ID { get; set; }

       public string Name { get; set; }

       public string Type { get; set; }
       
       public string Folder { get; set; }

       public string File { get; set; }

       public double BuildingArea { get; set; }

       public double VegetationArea { get; set; }

       public double RoadArea { get; set; }

       public ProjectInfo Project { get; set; }
    }
}
