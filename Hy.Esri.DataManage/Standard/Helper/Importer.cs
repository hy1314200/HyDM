using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.DataManage.Standard.Helper
{
    internal class Importer:Define.Messager
    {
        public IWorkspace Source { private get; set; }

        public string StandardName { private get; set; }


        public StandardItem Import()
        {
            StandardItem sItem = new StandardItem();
            sItem.Type = enumItemType.Standard;
            sItem.Name = this.StandardName;

            IList<StandardItem> subList = new List<StandardItem>();
            sItem.SubItems = subList;

            IEnumDataset enDSN = this.Source.get_Datasets(esriDatasetType.esriDTAny);
            IDataset dsName = enDSN.Next();
            while (dsName != null)
            {
                switch (dsName.Type)
                {
                    case esriDatasetType.esriDTFeatureDataset:
                        SendMessage(string.Format("正在导入矢量数据集[{0}]及其子图层...", dsName.Name));
                        StandardItem sItemDs = StandardHelper.Import(dsName as IFeatureDataset);
                        sItemDs.Parent = sItem;
                        subList.Add(sItemDs);
                        break;

                    case esriDatasetType.esriDTFeatureClass:
                        SendMessage(string.Format("正在导入矢量图层[{0}]...", dsName.Name));
                        StandardItem sItemClass = StandardHelper.Import(dsName as IFeatureClass);
                        sItemClass.Parent = sItem;
                        subList.Add(sItemClass);
                        break;
                }
                dsName = enDSN.Next();
            }

            return sItem;
            
        }
    }
}
