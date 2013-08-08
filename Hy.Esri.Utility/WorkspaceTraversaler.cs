using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.Utility
{
    public class WorkspaceTraversaler
    {
        public IWorkspace Source { private get; set; }

        public void Tranversal()
        {
            if (this.Source == null)
                return;

            IEnumDatasetName enDSN= Source.get_DatasetNames(esriDatasetType.esriDTAny);
            IDatasetName dsName = enDSN.Next();
            while (dsName != null)
            {

                dsName = enDSN.Next();
            }
            
        }
    }
}
