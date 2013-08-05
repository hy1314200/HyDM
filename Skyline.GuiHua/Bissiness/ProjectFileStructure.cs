using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyline.GuiHua.Bussiness
{
    public class ProjectFileStructure
    {
        public string RootFolder { get; set; }

        public string ProjectExcel { get; set; }

        public string FlyFile { get; set; }

        public string ImageFile { get; set; }

        public string DemFile { get; set; }

        public List<SchemaFileStructure> SchemaStruactures { get; set; }
    }

    public class SchemaFileStructure
    {
        public string SchemaFolder { get; set; }

        public string SchemaExcel { get; set; }

        public string LocationExcel { get; set; }

        public string ModelFolder { get; set; }
    }


}
