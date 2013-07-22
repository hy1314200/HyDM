using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hy.Esri.DataManage.Standard;

namespace Hy.Esri.DataManage.UI
{
    public partial class FrmFeatureDatasetInfo : DevExpress.XtraEditors.XtraForm
    {
        public FrmFeatureDatasetInfo()
        {
            InitializeComponent();
        }

        public bool EditAble
        {
            set
            {
                ucFeatureDataset1.EditAble = value;
            }
        }

        public StandardItem StandardItem
        {
            get
            {
                return ucFeatureDataset1.StandardItem;
            }
            set
            {
                ucFeatureDataset1.StandardItem = value;
            }
        }
    }
}