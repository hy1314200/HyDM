using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hy.Esri.DataManage.Standard;

namespace Hy.Esri.DataManage.UI
{
    public partial class UCStandardManager : DevExpress.XtraEditors.XtraUserControl, Hy.Esri.DataManage.Standard.IStandardManager
    {
        public UCStandardManager()
        {
            InitializeComponent();


            ucStandardList1.SelectedStandardItemChanged += new StandardItemEventHandler(ucStandardList1_SelectedStandardItemChanged);
            this.Refresh();
        }

        void ucStandardList1_SelectedStandardItemChanged(Standard.StandardItem sItem)
        {
            if (sItem == null)
            {
                tabInfo.SelectedTabPage = tpEmpty;
                return;
            }

            Standard.Helper.StandardHelper.InitItemDetial(sItem);
            switch (sItem.Type)
            {
                case Standard.enumItemType.Standard:
                case Standard.enumItemType.FeatureDataset:
                    ucFeatureDataset1.StandardItem = sItem;
                    tabInfo.SelectedTabPage = tpFeatureDataset;
                    return;

                case Standard.enumItemType.FeatureClass:
                    ucFeatureClassInfo1.FeatrueClassInfo = sItem.Details as FeatureClassInfo;
                    tabInfo.SelectedTabPage = tpClassInfo;
                    return;
            }

        }

        public void Refresh()
        {
            ucStandardList1.Init();
        }

        public Standard.StandardItem SelectedItem
        {
            get { return ucStandardList1.SelectedStandardItem; }
        }
    }
}
