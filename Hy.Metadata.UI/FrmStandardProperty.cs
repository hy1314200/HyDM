using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hy.Metadata.UI
{
    public partial class FrmStandardProperty : DevExpress.XtraEditors.XtraForm
    {
        public FrmStandardProperty()
        {
            InitializeComponent();
        }

        public enum enumPropertyViewMode
        {
            View=0,
            Edit=1,
            New=2
        }

        public enumPropertyViewMode ViewMode
        {
            set
            {
                if (value == enumPropertyViewMode.View)
                {
                    btnOK.Visible = false;
                    ucStandardProperty1.EditAble = false;
                    return;
                }

                if (value == enumPropertyViewMode.Edit || value == enumPropertyViewMode.New)
                {
                    btnOK.Visible = true;
                    btnOK.Text = "保存";
                    ucStandardProperty1.EditAble = true;
                }
            }
        }

        public MetaStandard CurrentStandard
        {
            get
            {
                return ucStandardProperty1.CurrentStandard;
            }

            set
            {
                ucStandardProperty1.CurrentStandard = value;
            }
        }
    }
}