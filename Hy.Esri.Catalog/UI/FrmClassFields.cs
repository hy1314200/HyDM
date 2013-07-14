using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.Catalog.UI
{
    /// <summary>
    /// 矢量/ArcEngine支持的3D（还是FeatureClass）数据属性字段添加
    /// @remark 将IClass对象设置给本对象实例属性 @see::TargetClass，自动处理完
    /// </summary>
    public partial class FrmClassFields : DevExpress.XtraEditors.XtraForm
    {
        public FrmClassFields()
        {
            InitializeComponent();

        }

        private IClass m_Class;
        /// <summary>
        /// 编辑的目标Class对象
        /// </summary>
        public IClass TargetClass
        {
            set
            {
                m_Class = value;

                this.m_UCClassFields.SourceClass = m_Class;
            }
        }

        /// <summary>
        /// 此属性的赋值必须在TargetClass@see::TargetClass之前
        /// </summary>
        public Dictionary<string, string> FieldMapping
        {
            set
            {
                m_UCClassFields.FieldMapping = value;
            }
        }

        public Dictionary<string, string> AddedFieldMapping
        {
            get
            {
                List<IField> newFields = this.m_UCClassFields.NewFieldList;
                if (newFields == null)
                    return null;

                Dictionary<string, string> dictMapping = new Dictionary<string, string>();
                for (int i = 0; i < newFields.Count; i++)
                {
                    dictMapping.Add(newFields[i].Name,string.IsNullOrWhiteSpace(newFields[i].AliasName) ? newFields[i].Name:newFields[i].AliasName);
                }

                return dictMapping;
            }
        }

        public List<IField> NewFieldList
        {
            get
            {
                return this.m_UCClassFields.NewFieldList;
            }
        }
        //private void btnOK_Click(object sender, EventArgs e)
        //{
            //try
            //{
            //    // 考虑使用GP工具 AddField 在 ESRI.ArcGIS.DataManagementTools下
            //    List<IField> newFields = this.m_UCClassFields.NewFieldList;
            //    //for (int i = 0; i < newFields.Count; i++)
            //    //{
            //    //    if (m_Class.FindField(newFields[i].Name) > -1)
            //    //    {
            //    //        continue;
            //    //    }
            //    //    m_Class.AddField(newFields[i]);
            //    //}
            //    if (Utility.GpTool.AddFields(m_Class as ITable, newFields))
            //    {
            //        XtraMessageBox.Show("添加字段成功");
            //        this.DialogResult = DialogResult.OK;
            //    }
            //    else
            //    {
            //        XtraMessageBox.Show(string.Format("抱歉，添加操作失败了！\n信息：{0}", Utility.GpTool.ErrorMessage));
            //    }
            //}
            //catch (Exception exp)
            //{
            //    XtraMessageBox.Show(string.Format("抱歉，添加操作发生了错误！\n信息：{0}", exp.Message));
            //}
        //}
    }
}