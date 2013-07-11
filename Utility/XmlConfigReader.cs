using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;

namespace Utility
{
    public class XmlConfigReader
    {
        public string FileName { private get; set; }

        public void Reset()
        {
        }

        public DataTable ReadToDataTable()
        {
            DataTable dt = new DataTable(System.IO.Path.GetFileNameWithoutExtension(FileName));
            dt.ReadXmlSchema(this.FileName);
            dt.ReadXml(FileName);

            return dt;
        }

    }
}
