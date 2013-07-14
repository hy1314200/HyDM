using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.Catalog.UI
{
    public partial class UCClassFields : DevExpress.XtraEditors.XtraUserControl
    {
        public UCClassFields()
        {
            InitializeComponent();

            Init();
        }

        private Dictionary<string, string> m_DictFieldMapping;
        public Dictionary<string, string> FieldMapping
        {
            set
            {
                m_DictFieldMapping = value;
            }
        }

        private IClass m_Class;
        public IClass SourceClass
        {
            set
            {
                m_Class = value;
                if (m_Class == null)
                    return;

                IFields fields = m_Class.Fields;
                //IFieldsEdit fieldsEdit = fields as IFieldsEdit;

                int count = fields.FieldCount;
                for (int i = 0; i < count; i++)
                {
                    IField field = fields.get_Field(i);
                    DataRow rowField = m_DataTable.NewRow();
                    m_DataTable.Rows.Add(rowField);

                    string fieldName=field.Name.ToUpper();
                    rowField[0] = fieldName;
                    if (m_DictFieldMapping != null && m_DictFieldMapping.ContainsKey(fieldName))
                    {
                        rowField[1] = m_DictFieldMapping[fieldName];
                    }
                    else
                    {
                        rowField[1] = string.IsNullOrWhiteSpace(field.AliasName)?fieldName:field.AliasName;
                    }
                    rowField[2] = new FieldType(field.Type, false);
                    rowField[3] = field.Length;
                    rowField[4] = field.Precision;
                    rowField[5] = field.IsNullable;
                    rowField[6] = field.DefaultValue;
                    rowField[7] = field.Editable;
                }
            }
        }
        //public void SetSourceClass(IClass sourceClass)
        //{
        //    m_Class = sourceClass;
        //    if (m_Class == null)
        //        return;

        //    IFields fields = m_Class.Fields;
        //    //IFieldsEdit fieldsEdit = fields as IFieldsEdit;

        //    int count = fields.FieldCount;
        //    for (int i = 0; i < count; i++)
        //    {
        //        IField field = fields.get_Field(i);
        //        DataRow rowField = m_DataTable.NewRow();
        //        m_DataTable.Rows.Add(rowField);

        //        rowField[0] = field.Name;
        //        rowField[1] = field.AliasName;
        //        rowField[2] = new FieldType(field.Type, false);
        //        rowField[3] = field.Length;
        //        rowField[4] = field.Precision;
        //        rowField[5] = field.IsNullable;
        //        rowField[6] = field.DefaultValue;
        //        rowField[7] = field.Editable;
        //    }
        //}
        private DataTable m_DataTable;
        private void Init()
        {
            m_DataTable = new DataTable();
            m_DataTable.Columns.Add("名称", typeof(string));
            //DataColumn colName= m_DataTable.Columns.Add("名称", typeof(string));
            //colName.DefaultValue = "NewField";
            DataColumn colAlias= m_DataTable.Columns.Add("别名", typeof(string));
            colAlias.DefaultValue = "";
            DataColumn colDataType=  m_DataTable.Columns.Add("数据类型", typeof(FieldType));
            colDataType.DefaultValue = new FieldType(esriFieldType.esriFieldTypeString, true);
            DataColumn colLength= m_DataTable.Columns.Add("字段长度", typeof(int));
            colLength.DefaultValue = 50;
            DataColumn colPrecision= m_DataTable.Columns.Add("精度", typeof(int));
            colPrecision.DefaultValue = 0;
            DataColumn colNullAble= m_DataTable.Columns.Add("允许空", typeof(bool));
            colNullAble.DefaultValue = true;
            DataColumn colDefaultValue= m_DataTable.Columns.Add("默认值", typeof(object));
            colDefaultValue.DefaultValue = null;
            DataColumn colEditAble= m_DataTable.Columns.Add("可编辑", typeof(bool));
            colEditAble.DefaultValue = true;

            gcFields.DataSource = m_DataTable;

            esriFieldType[] fieldTypes =
                            {
                                esriFieldType.esriFieldTypeDate,
                                esriFieldType.esriFieldTypeDouble,
                                esriFieldType.esriFieldTypeGUID,
                                esriFieldType.esriFieldTypeInteger,
                                esriFieldType.esriFieldTypeSingle,
                                esriFieldType.esriFieldTypeSmallInteger,
                                esriFieldType.esriFieldTypeString
                            };

            repCmbDatatype.Items.Clear();
            for (int i = 0; i < fieldTypes.Length; i++)
            {
                repCmbDatatype.Items.Add(new FieldType(fieldTypes[i], true));
            }
        }



        private void gvFields_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (gcFields.IsLoading || !gcFields.Created || gvFields.IsLoading || gvFields.IsEmpty || e.FocusedColumn == null)
                return;

            e.FocusedColumn.OptionsColumn.AllowEdit = true;
            FieldType fType = gvFields.GetRowCellValue(gvFields.FocusedRowHandle, gColDatatype) as FieldType;
            if (fType == null)
                return;
            
            if (e.FocusedColumn == gColLength)
                e.FocusedColumn.OptionsColumn.AllowEdit = !fType.LengthFixed;

            if (e.FocusedColumn == gColPrecision)
                e.FocusedColumn.OptionsColumn.AllowEdit = fType.PrecisionEnabled;
        }

        private void gvFields_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gcFields.IsLoading || !gcFields.Created || gvFields.IsLoading || gvFields.IsEmpty)
                return;

            gvFields.OptionsBehavior.Editable = true;
            FieldType fType = gvFields.GetRowCellValue(e.FocusedRowHandle, gColDatatype) as FieldType;
            if (fType == null)
                return;

            gvFields.OptionsBehavior.Editable = fType.EditAble;
            btnDelete.Enabled = fType.EditAble;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            gvFields.AddNewRow();
            DataRow rowAdded= gvFields.GetDataRow(gvFields.GetRowHandle(gvFields.RowCount - 1));
            string strNewFieldName = string.Format("Field{0}", m_DataTable.Select("名称 like 'Field%'").Length + 1);
            while (m_DataTable.Select(string.Format("名称 = '{0}'", strNewFieldName)).Length > 0)
            {
                strNewFieldName = strNewFieldName + "_1";
            }
            rowAdded[0] = strNewFieldName;
            gvFields.RefreshData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            gvFields.DeleteRow(gvFields.FocusedRowHandle);
        }

        public List<IField> NewFieldList
        {
            get
            {
                gvFields.UpdateCurrentRow();
                List<IField> newFieldList = new List<IField>();
                for (int i = 0; i < m_DataTable.Rows.Count; i++)
                {
                    FieldType fType = m_DataTable.Rows[i]["数据类型"] as FieldType;
                    if (fType.EditAble)
                    {
                        DataRow rowField = m_DataTable.Rows[i];
                        IFieldEdit newField = new FieldClass();
                        newField.Name_2 = rowField[0] as string;

                        newField.AliasName_2 = rowField[1] as string;
                        newField.Type_2 = fType.Type;

                        if (!fType.LengthFixed)
                            newField.Length_2 = Convert.ToInt32(rowField[3]);
                        if (fType.PrecisionEnabled)
                            newField.Precision_2 = Convert.ToInt32(rowField[4]);
                        newField.IsNullable_2 = Convert.ToBoolean(rowField[5]);
                        newField.DefaultValue_2 = rowField[6];
                        newField.Editable_2 = Convert.ToBoolean(rowField[7]);

                        newFieldList.Add(newField);
                    }
                }

                return newFieldList;
            }
        }


        private class FieldType
        {
            public FieldType(esriFieldType fType, bool editAble)
            {
                this.Type = fType;
                this.EditAble = editAble;
                //if (fType == esriFieldType.esriFieldTypeString)
                //    this.LengthFixed = false;
                //else
                //    this.LengthFixed = true;

                //if (fType == esriFieldType.esriFieldTypeSingle || fType == esriFieldType.esriFieldTypeDouble)
                //{
                //    this.PrecisionEnabled = true;
                //}
                //else
                //{
                //    this.PrecisionEnabled = false;
                //}

                this.DefaultLength = 0;
                this.DefaultPrecision = 0;
                this.LengthFixed = true;
                this.PrecisionEnabled = false;

                switch (fType)
                {
                    case esriFieldType.esriFieldTypeString:
                        this.Name = "字符串";
                        this.LengthFixed = false;
                        this.DefaultLength = 50;
                        this.DefaultPrecision = 0;
                        break;

                    case esriFieldType.esriFieldTypeSingle:
                        this.Name = "浮点型";
                        this.PrecisionEnabled = true;
                        break;

                    case esriFieldType.esriFieldTypeDouble:
                        this.Name = "双精度";
                        this.PrecisionEnabled = true;
                        break;

                    case esriFieldType.esriFieldTypeBlob:
                        this.Name = "二进制";
                        break;

                    case esriFieldType.esriFieldTypeDate:
                        this.Name = "日期";
                        break;

                    case esriFieldType.esriFieldTypeGeometry:
                        this.Name = "几何对象";
                        break;

                    case esriFieldType.esriFieldTypeGUID:
                        this.Name = "唯一标识";
                        break;

                    case esriFieldType.esriFieldTypeInteger:
                        this.Name = "整型";
                        break;

                    case esriFieldType.esriFieldTypeOID:
                        this.Name = "OID";
                        break;

                    case esriFieldType.esriFieldTypeRaster:
                        this.Name = "影像";
                        break;

                    case esriFieldType.esriFieldTypeSmallInteger:
                        this.Name = "短整型";
                        break;

                    case esriFieldType.esriFieldTypeXML:
                        this.Name = "XML";
                        break;

                    default:
                        break;
                }
            }

            public esriFieldType Type { get; private set; }

            public int DefaultLength { get; private set; }

            public int DefaultPrecision { get; private set; }

            public bool EditAble { get; private set; }

            public bool LengthFixed { get; private set; }

            public bool PrecisionEnabled { get; private set; }

            public string Name { get; private set; }

            public override string ToString()
            {
                return this.Name;
            }

            //public static bool operator ==(FieldType fType1, FieldType fType2)
            //{
            //    //if (fType1 == null && fType2 == null)
            //    //    return true;

            //    //if (fType1 == null || fType2 == null)
            //    //    return false;

            //    return fType1.Type == fType2.Type;
            //}
            //public static bool operator !=(FieldType fType1, FieldType fType2)
            //{
            //    //if (fType1 == null && fType2 == null)
            //    //    return false;

            //    //if (fType1 == null || fType2 == null)
            //    //    return true;

            //    return fType1.Type != fType2.Type;
            //}

        }

        private void gvFields_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (gvFields.FocusedColumn == gColName)
            {
                string strNewValue = e.Value as string;
                // 空检查
                if (string.IsNullOrWhiteSpace(strNewValue))
                {
                    e.Valid = false;
                    e.ErrorText = "字段名不能为空";
                    return;
                }

                // 重复判断
                strNewValue = strNewValue.ToUpper();
                DataRow rowFocusd = gvFields.GetFocusedDataRow();
                for (int i = 0; i < m_DataTable.Rows.Count; i++)
                {
                    DataRow rowCurrent = m_DataTable.Rows[i];
                    if (rowCurrent == rowFocusd)
                        continue;

                    if ((rowCurrent[0] as string).ToUpper() == strNewValue)
                    {
                        e.Valid = false;
                        e.ErrorText = "字段名不能与已有字段重复";
                        return;
                    }
                }
            }
        }

        private void gvFields_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == gColDatatype)
            {
                FieldType fType = e.Value as FieldType;
                gvFields.SetRowCellValue(e.RowHandle, gColLength, fType.DefaultLength);
                gvFields.SetRowCellValue(e.RowHandle, gColPrecision, fType.DefaultPrecision);
            }
        }
    }
}
