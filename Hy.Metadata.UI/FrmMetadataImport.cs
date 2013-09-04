using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Hy.Metadata.UI
{
    public partial class FrmMetadataImport : DevExpress.XtraEditors.XtraForm
    {
        public FrmMetadataImport()
        {
            InitializeComponent();
        }

        private MetaStandard m_CurrentStandard;

        public MetaStandard CurrentStandard
        {
            set
            {
                m_CurrentStandard=value;
            }
        }

        private void txtPath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            dlgPath.FilterIndex = rpDataType.SelectedIndex;
            if (!string.IsNullOrEmpty(txtPath.Text))
            {
                dlgPath.FileName = txtPath.Text;
            }
            if (dlgPath.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = dlgPath.FileName;
            }

        }

        private void rpDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rpDataType.SelectedIndex == 0)// Text
            {
                pcTable.Visible = false;
                gpSplit.Visible = true;
            }
            else
            {
                //pcTable.Visible = true;
                gpSplit.Visible = false;
            }
        }

        private void rpSplit_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSplit.Enabled = rpSplit.SelectedIndex == 4;
        }


        string[] m_DataTypeString =
        {
            "文本",
            "Excel",
            "Xml"
        };

        string m_SplitString;
        string[] m_SplitArray =
        {
            " ",
            "\t",
            ",",
            ";",
            ""
        };
        private void wizardControl1_NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            if (e.Page == wpSetting)
            {
                if (string.IsNullOrEmpty(txtPath.Text))
                {
                    XtraMessageBox.Show("请指定数据源路径！");
                    e.Handled = true;
                    return;
                }

                //if (rpDataType.SelectedIndex != 0 && cmbTable.SelectedIndex < 0)
                //{
                //    XtraMessageBox.Show("请选择需要导入的数据表");
                //    e.Handled = true;
                //    return;
                //}

                m_SplitArray[4] = txtSplit.Text;
                if (txtSplit.Enabled && string.IsNullOrEmpty(txtSplit.Text))
                {
                    XtraMessageBox.Show("请输入自定义分隔符");
                    e.Handled = true;
                    return;
                }

                string strInfomation = string.Format("数据源类型: {0};\n", m_DataTypeString[rpDataType.SelectedIndex]);
                strInfomation += string.Format("数据源路径: {0};\n", txtPath.Text);
                if (rpDataType.SelectedIndex == 0)
                {
                    strInfomation += string.Format("字段分隔符: {0};\n", m_SplitArray[rpSplit.SelectedIndex]);
                }
                else
                {
                    strInfomation += string.Format("数据表：    {0}\n", cmbTable.Text);

                }

                strInfomation+=string.Format("仅一条记录：{0}",cbOnlyOne.Checked?"是":"否");
                txtInfomation.Text = strInfomation;
            }
        }


        private void SendMessage(string strMsg)
        {
            lblStatus.Text = strMsg;
            Application.DoEvents();
        }
        private void wizardControl1_FinishClick(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            if (m_CurrentStandard == null)
            {
                XtraMessageBox.Show("请指定元数据标准。");
                return;
            }
            SendMessage("正在解析数据...");
            DataTable dtData = null;
            try
            {
                if (rpDataType.SelectedIndex == 0)
                {
                    Utility.TxtConfigReader txtReader = new Utility.TxtConfigReader();
                    txtReader.FileName = txtPath.Text;
                    txtReader.SplitString = m_SplitArray[rpSplit.SelectedIndex];
                    dtData = txtReader.ReadMultiToDataTable();
                }

                if (rpDataType.SelectedIndex == 1)
                {
                    Utility.ExcelConfigReader excelReader = new Utility.ExcelConfigReader();
                    excelReader.FileName = txtPath.Text;
                    dtData = excelReader.ReadToDataTable();
                }
                if (rpDataType.SelectedIndex == 2)
                {
                    Utility.XmlConfigReader xmlReader = new Utility.XmlConfigReader();
                    xmlReader.FileName = txtPath.Text;
                    dtData = xmlReader.ReadToDataTable();
                }
            }
            catch
            {
            }

            if (dtData == null)
            {
                XtraMessageBox.Show("数据解析失败");
                return;
            }

            // 
            SendMessage("正在匹配字段...");
            Dictionary<string, string> dictMapping = new Dictionary<string, string>();
            Dictionary<string, int> dictFieldIndex = new Dictionary<string, int>();
            if (!string.IsNullOrEmpty(this.m_CurrentStandard.MappingDict))
            {
                IList<Hy.Dictionary.DictItem> dictList = Hy.Dictionary.DictHelper.GetItemsByName(this.m_CurrentStandard.MappingDict);
                IList<Hy.Dictionary.DictItem> mappingList = dictList.Count > 0 ? dictList[0].SubItems : null;

                if (mappingList != null && mappingList.Count > 0)
                {
                    foreach (Hy.Dictionary.DictItem dItem in mappingList)
                    {
                        dictMapping[dItem.Code] = dItem.Name;
                    }
                }
            }
            for (int i = 0; i < dtData.Columns.Count; i++)
            {
                //dtData.Columns[i].ColumnName = dictMapping[dtData.Columns[i].ColumnName];
                dictFieldIndex[dictMapping.ContainsKey(dtData.Columns[i].ColumnName) ? dictMapping[dtData.Columns[i].ColumnName] : dtData.Columns[i].ColumnName]
                = i;
            }

            int countTemp = 0;
            DataTable dtTarget = MetaStandardHelper.GetMetadata(m_CurrentStandard, "1=2", 1, 0, ref countTemp);
            List<int> sourceIndexs = new List<int>();
            List<int> targetIndexs = new List<int>();
            for (int i = 0; i < dtTarget.Columns.Count; i++)
            {
                if (dtTarget.Columns[i].ColumnName == "ID")
                    continue;

                if (dictFieldIndex.ContainsKey(dtTarget.Columns[i].ColumnName))
                {
                    targetIndexs.Add(i);
                    sourceIndexs.Add(dictFieldIndex[dtTarget.Columns[i].ColumnName]);
                }
            }
            int fieldCount = sourceIndexs.Count;
            SendMessage("开始导入...");
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                SendMessage(string.Format("正在导入{0}/{1}...", i + 1, dtData.Rows.Count));
                DataRow rowNew = dtTarget.NewRow();
                for (int j = 0; j < fieldCount; j++)
                {
                    rowNew[targetIndexs[j]] = dtData.Rows[i][sourceIndexs[j]];
                }
                dtTarget.Rows.Add(rowNew);
            }
            SendMessage("正在写入数据库...");
            Environment.AdodbHelper.UpdateTable(m_CurrentStandard.TableName, dtTarget);

            SendMessage("导入成功");

            XtraMessageBox.Show("导入成功");
            e.Cancel = false;

        }
    }
}