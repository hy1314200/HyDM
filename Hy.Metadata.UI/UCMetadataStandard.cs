using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hy.Metadata.UI
{
    public partial class UCMetadataStandard : UserControl
    {
        public UCMetadataStandard()
        {
            InitializeComponent();
        }

        public MetaStandard FieldInfo
        {
            get
            {
                if (m_CurrentStandard == null)
                    m_CurrentStandard = new MetaStandard();

                m_CurrentStandard.Name = txtName.Text;
                m_CurrentStandard.TableName = this.txtTableName.Text;
                m_CurrentStandard.MappingDict = this.cmbDictItem.Text;
                m_CurrentStandard.Description = this.txtDescription.Text;
                for (int i = 0; i < gvFields.RowCount; i++)
                {

                }

                return m_CurrentStandard;
            }

            set
            {
                SetEmpty();
                m_CurrentStandard = value;
                if (m_CurrentStandard == null)
                    return;
                
                this.txtName.Text = m_CurrentStandard.Name;
                this.txtTableName.Text = m_CurrentStandard.TableName;
                this.cmbDictItem.Text = m_CurrentStandard.MappingDict;
                this.txtDescription.Text = m_CurrentStandard.Description;
                gcFields.DataSource = m_CurrentStandard.FieldsInfo;
                gvFields.RefreshData();
            }
        }

        private MetaStandard m_CurrentStandard; 
        private void SetEmpty()
        {
            this.txtName.Text = "";
            this.txtTableName.Text = "";
            this.cmbDictItem.Text = "";
            this.txtDescription.Text = "";
            gcFields.DataSource = null;
        }

        private bool m_EditAble = true;
        public bool EditAble
        {
            set
            {
                this.m_EditAble = value;
            }
        }

        private void gvFields_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {

        }
    }
}
