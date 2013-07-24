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
    public partial class FrmFeatureClassInfo : DevExpress.XtraEditors.XtraForm
    {
        public FrmFeatureClassInfo()
        {
            InitializeComponent();
            this.EditAble = true;
        }


        public bool EditAble
        {
            set
            {
                ucFeatureClassInfo1.EditAble = value;
            }
        }

        public FeatureClassInfo FeatureClassInfo
        {
            get
            {
                return ucFeatureClassInfo1.FeatrueClassInfo;
            }
            set
            {
                ucFeatureClassInfo1.FeatrueClassInfo = value;
            }
        }
    }
}