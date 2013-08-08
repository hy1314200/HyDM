using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace Hy.Esri.DataManage.Standard
{
    public class StandardItem : global::Utility.NHibernateSet<StandardItem>
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string AliasName { get; set; }

        public StandardItem Parent { get; set; }

        public string SpatialReferenceString { get; set; }
        
        public enumItemType Type { get; set; }

        //IList<StandardItem> SubItems { get; set; }

        public object Details { get; set; }

        private ISpatialReference m_SpatialReference;
        public ISpatialReference SpatialReference
        {
            get
            {
                if (m_SpatialReference == null)
                {
                    if (!string.IsNullOrWhiteSpace(this.SpatialReferenceString))
                    {
                        int temp = -1;
                        (new SpatialReferenceEnvironment()).CreateESRISpatialReference(this.SpatialReferenceString,out this.m_SpatialReference,out temp);
                    }
                    else
                        m_SpatialReference = (new UnknownCoordinateSystemClass());
                }
                return m_SpatialReference;
            }

            set
            {
                m_SpatialReference = value;
                this.SpatialReferenceString = null;
                if (m_SpatialReference != null)
                {
                    int strCount = -1;
                    string spatialReferenceString = null;
                    (m_SpatialReference as IESRISpatialReferenceGEN).ExportToESRISpatialReference(out spatialReferenceString, out strCount);
                    this.SpatialReferenceString = spatialReferenceString;
                }
            }
        }
    }
}
