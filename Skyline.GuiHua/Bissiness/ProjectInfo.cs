using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyline.GuiHua.Bussiness
{
    public class ProjectInfo
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Enterprise { get; set; }

        public string Address { get; set; }

        public string Folder { get; set; }

        public string File { get; set; }

        public List<SchemaInfo> Schemas{get;set;}

    }

    
    public delegate void ProjectEventHandle(ProjectInfo projectInfo);
}
