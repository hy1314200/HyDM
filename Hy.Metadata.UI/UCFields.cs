using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hy.Metadata.UI
{
    public partial class UCFields : UserControl
    {
        public UCFields()
        {
            InitializeComponent();

            string[] m_Captions =
            {                 
                "字符串",
                "整数",
                "小数",
                "日期和时间",
                "图片",
                "二进制"
            };
            //ComboBoxEdit cmbFiels = this.repositoryItemComboBox1.OwnerEdit;
            //for(int i=0;i<m_Captions.Length;i++)
            //{
            //    cmbFiels.Properties.Items.Add(new ComboItem((enumFieldType)i, m_Captions[i]));
            //}
            for (int i = 0; i <6; i++)
            {
                this.repositoryItemComboBox1.Items.Add((enumFieldType)i);
            }

            this.FieldsInfo = new List<FieldInfo>();
        }
        
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<FieldInfo> FieldsInfo
        {
            get
            {
                if (m_FieldsInfo == null)
                    m_FieldsInfo = gvFields.DataSource as IList<FieldInfo>;

                return m_FieldsInfo;
            }

            set
            {
                m_FieldsInfo = value;
                gcFields.DataSource = m_FieldsInfo;
                gvFields.RefreshData();

                RefreshEnabled();
            }
        }

        IList<FieldInfo> m_FieldsInfo;
       

        private bool m_EditAble = true;
        public bool EditAble
        {
            set
            {
                this.m_EditAble = value;
                RefreshEnabled();
            }
        }

        private void gvFields_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            gcolLength.OptionsColumn.AllowEdit = (this.m_EditAble && m_SelectedFieldInfo != null && m_SelectedFieldInfo.Type == enumFieldType.String);
            gcolPrecision.OptionsColumn.AllowEdit = (this.m_EditAble && m_SelectedFieldInfo != null && m_SelectedFieldInfo.Type == enumFieldType.Decimal);
        }

        private void RefreshEnabled()
        {
            if (m_EditAble)
            {
                simpleButton1.Enabled = true;
                simpleButton2.Enabled = m_SelectedFieldInfo!=null;
                gvFields.OptionsBehavior.Editable = true;
            }
            else
            {
                simpleButton1.Enabled = false;
                simpleButton2.Enabled = false;
                gvFields.OptionsBehavior.Editable = false;
            }
        }

        private FieldInfo m_SelectedFieldInfo;
        private void gvFields_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            m_SelectedFieldInfo = gvFields.GetFocusedRow() as FieldInfo;
            RefreshEnabled();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (m_FieldsInfo == null)
            {
                this.FieldsInfo = new List<FieldInfo>();
               
            }
            m_FieldsInfo.Add(new FieldInfo());
            gvFields.RefreshData();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (m_SelectedFieldInfo != null)
            {
                m_FieldsInfo.Remove(m_SelectedFieldInfo);
                if (!string.IsNullOrEmpty(m_SelectedFieldInfo.ID))
                {
                    Hy.Metadata.Environment.NhibernateHelper.DeleteObject(m_SelectedFieldInfo);
                    Hy.Metadata.Environment.NhibernateHelper.Flush();
                }
            }

            gvFields.RefreshData();
        }
    }
}
