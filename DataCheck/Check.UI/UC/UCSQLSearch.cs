using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System.Collections;
using ESRI.ArcGIS.SystemUI;


namespace Check.UI.UC
{
    public partial class UCSQLSearch : DevExpress.XtraEditors.XtraUserControl
    {
        private AxMapControl m_MapControl;
        private IMap m_Map;
        private IActiveView m_activeView;
        private IFeatureLayer m_FeatureLayer = null;
        private int m_intSelectionResultEnum = 0;

        private Hashtable m_FieldTable;

        public UCSQLSearch(AxMapControl mapControl)
        {
            InitializeComponent();
            
            m_MapControl = mapControl;
            m_Map = m_MapControl.Map;
            m_activeView = m_MapControl.ActiveView;
            //获取图层列表
            GetLayers(m_Map, combLayers);
            if (combLayers.Properties.Items.Count > 0)
                combLayers.SelectedIndex = 0;

            if (combMethods.Properties.Items.Count > 0)
                combMethods.SelectedIndex = 0;
            //获取图层对应的字段
            GetFields(m_Map, (string)combLayers.SelectedItem, ref m_FeatureLayer);
            listboxValues.Items.Clear();
            listboxValues.Enabled = false;
            btnUniqueValue.Enabled = true;
        }

        //private void InitTreeList()
        //{
        //    //初始化属性视图结构
        //    TreeListColumn col = this.treeListFields.Columns.Add();
        //    col.Caption = "字段名";
        //    col.Name = "FieldName";
        //    col.FieldName = "FieldName";
        //    col.Width = 80;
        //    col.Visible = false;
        //    col.BestFit();
        //    col.OptionsColumn.AllowEdit = false;

        //    col = treeListFields.Columns.Add();
        //    col.Caption = "字段别名";
        //    col.Name = "FieldAlias";
        //    col.FieldName = "FieldAlias";
        //    col.Visible = true;
        //    col.Width = 254;
        //    col.BestFit();
        //    col.OptionsColumn.AllowEdit = false;
        //}

        #region Methods
        private void GetLayers(IMap pMap, DevExpress.XtraEditors.ComboBoxEdit combLayers)
        {
            try
            {
                if (pMap == null)
                    return;
                int lyrCount = pMap.LayerCount;                

                for (int i = 0; i < lyrCount; i++)
                {
                    ILayer pLayer = pMap.get_Layer(i);
                    if (pLayer is ICompositeLayer)
                    {
                        GetLayerFromCompositeLayer(pLayer,combLayers);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 获取复合图层（ICompositeLayer）中的图层，并添加到combox中
        /// </summary>
        /// <param name="pLayer">复合图层</param>
        /// <param name="combLayers">combox</param>
        private void GetLayerFromCompositeLayer(ILayer pLayer, DevExpress.XtraEditors.ComboBoxEdit combLayers)
        {
            ICompositeLayer pComLayer = (ICompositeLayer)pLayer;
            ILayer tempLyr = null;
            for (int i = 0; i < pComLayer.Count; i++)
            {
                tempLyr = pComLayer.get_Layer(i);
                if (tempLyr is ICompositeLayer)
                {
                    GetLayerFromCompositeLayer(tempLyr, combLayers);
                }
                else
                {
                    combLayers.Properties.Items.Add(tempLyr.Name);
                }
            }
        }

        private void GetFields(IMap pMap, string sLayerName, ref IFeatureLayer pFeatureLayer)
        {
            try
            {
                IFeatureLayer _featureLayer;
                IFeatureClass _featureClass = null;
                IFields _fields;
                IDataset _dataSet;

                _featureLayer = (IFeatureLayer)Common.Utility.Esri.MapOperAPI.GetLayerFromMapByName(pMap, sLayerName);
                pFeatureLayer = _featureLayer;

                if (_featureLayer != null)
                {
                    _featureClass = _featureLayer.FeatureClass;
                   
                }
                this.listboxFields.Items.Clear();
                m_FieldTable = new Hashtable();
                if (_featureClass !=null)
                {
                    _dataSet = _featureClass as IDataset;
                    _fields = _featureClass.Fields;
                    if (_dataSet.Workspace.Type == esriWorkspaceType.esriFileSystemWorkspace ||
                        _dataSet.Workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                    {
                        for (int i = 0; i < _fields.FieldCount; i++)
                        {
                            IField pField = _fields.get_Field(i);
                            if (pField.Type != esriFieldType.esriFieldTypeGeometry &&
                                pField.Type != esriFieldType.esriFieldTypeBlob &&
                                pField.Type != esriFieldType.esriFieldTypeRaster)
                            {
                                listboxFields.Items.Add('[' + pField.AliasName + ']');
                                m_FieldTable.Add(pField.AliasName,pField.Name);
                                //TreeListNode node = this.treeListFields.AppendNode(new object[] { '[' + _fields.get_Field(i).Name + ']', '[' + _fields.get_Field(i).AliasName + ']' }, null);
               
                            }
                        }
                    }
                    else if (_dataSet.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                    {
                        for (int i = 0; i < _fields.FieldCount; i++)
                        {
                            IField pField = _fields.get_Field(i);
                            if (pField.Type != esriFieldType.esriFieldTypeGeometry &&
                                pField.Type != esriFieldType.esriFieldTypeBlob &&
                                pField.Type != esriFieldType.esriFieldTypeRaster)
                            {

                                listboxFields.Items.Add('[' + pField.AliasName + ']');
                                m_FieldTable.Add(pField.AliasName, pField.Name);
                                //TreeListNode node = this.treeListFields.AppendNode(new object[] { '[' + _fields.get_Field(i).Name + ']', '[' + _fields.get_Field(i).AliasName + ']' }, null);
               
                            }
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="sFieldName">字段别名</param>
        /// <param name="listboxValues"></param>
        private void GetUniqueValue(ITable table, string sFieldName, DevExpress.XtraEditors.ListBoxControl listboxValues)
        {

            try
            {
                IDataStatistics _dataStatistics = new DataStatisticsClass();
                ICursor _cursor;

                string strFieldName = m_FieldTable[sFieldName].ToString();
                int _fieldIndex = table.Fields.FindField(strFieldName);

                _cursor = table.Search(null, false);
                listboxValues.Items.Clear();

                _dataStatistics.Field = strFieldName;
                _dataStatistics.Cursor = _cursor;
                IEnumerator _enumerator = _dataStatistics.UniqueValues;
                _enumerator.Reset();
                while (_enumerator.MoveNext())
                {
                    if (table.Fields.get_Field(_fieldIndex).Type == esriFieldType.esriFieldTypeString)
                        listboxValues.Items.Add("'" + _enumerator.Current.ToString() + "'");
                    else
                        listboxValues.Items.Add(_enumerator.Current.ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SelectFeatures()
        {
            try
            {
                string strTemp = txtExpression.Text;
                
                string _whereCause = txtExpression.Text;
                if (null == _whereCause)
                    return;
                if (null == m_FeatureLayer)
                    return;
                IQueryFilter _queryFilter;
                IFeatureSelection _featureSelection;

                _featureSelection = m_FeatureLayer as IFeatureSelection;
                _queryFilter = new QueryFilterClass();
                _queryFilter.SubFields = "*";
                _queryFilter.WhereClause = _whereCause;

                m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                _featureSelection.SelectFeatures(_queryFilter, (esriSelectionResultEnum)m_intSelectionResultEnum, false);
                m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ZoomToSelectedFeatures()
        {
            try
            {
                ICommand pZoomToSelectedFeaturesCommand = new ControlsZoomToSelectedCommandClass();
                pZoomToSelectedFeaturesCommand.OnCreate(m_MapControl.Object);
                pZoomToSelectedFeaturesCommand.OnClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 运算符
        private void btn1_Click(object sender, EventArgs e)
        {
            int _selStart = txtExpression.SelectionStart;
            txtExpression.Text = txtExpression.Text.Insert(_selStart, " " + btn1.Text);
            txtExpression.SelectionStart = _selStart + btn1.Text.Length + 1;
            txtExpression.Focus();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            int _selStart = txtExpression.SelectionStart;
            txtExpression.Text = txtExpression.Text.Insert(_selStart, " " + btn2.Text);
            txtExpression.SelectionStart = _selStart + btn2.Text.Length + 1;
            txtExpression.Focus();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            int _selStart = txtExpression.SelectionStart;
            txtExpression.Text = txtExpression.Text.Insert(_selStart, " " + btn3.Text);
            txtExpression.SelectionStart = _selStart + btn3.Text.Length + 1;
            txtExpression.Focus();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            int _selStart = txtExpression.SelectionStart;
            txtExpression.Text = txtExpression.Text.Insert(_selStart, " " + btn4.Text);
            txtExpression.SelectionStart = _selStart + btn4.Text.Length + 1;
            txtExpression.Focus();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            int _selStart = txtExpression.SelectionStart;
            txtExpression.Text = txtExpression.Text.Insert(_selStart, " " + btn5.Text);
            txtExpression.SelectionStart = _selStart + btn5.Text.Length + 1;
            txtExpression.Focus();
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            int _selStart = txtExpression.SelectionStart;
            txtExpression.Text = txtExpression.Text.Insert(_selStart, " " + btn6.Text);
            txtExpression.SelectionStart = _selStart + btn6.Text.Length + 1;
            txtExpression.Focus();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            int _selStart = txtExpression.SelectionStart;
            txtExpression.Text = txtExpression.Text.Insert(_selStart, " " + btn7.Text);
            txtExpression.SelectionStart = _selStart + btn7.Text.Length + 1;
            txtExpression.Focus();
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            int _selStart = txtExpression.SelectionStart;
            txtExpression.Text = txtExpression.Text.Insert(_selStart, " " + btn8.Text);
            txtExpression.SelectionStart = _selStart + btn8.Text.Length + 1;
            txtExpression.Focus();
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            int _selStart = txtExpression.SelectionStart;
            txtExpression.Text = txtExpression.Text.Insert(_selStart, " " + btn9.Text);
            txtExpression.SelectionStart = _selStart + btn9.Text.Length + 1;
            txtExpression.Focus();
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            int _selStart = txtExpression.SelectionStart;
            if (m_FeatureLayer.FeatureClass != null)
            {
                IDataset _dataSet = (IDataset)m_FeatureLayer.FeatureClass;
                if (_dataSet.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace) //personal DB
                {
                    txtExpression.Text = txtExpression.Text.Insert(_selStart, "?");

                }
                else
                    txtExpression.Text = txtExpression.Text.Insert(_selStart, "_");
                txtExpression.SelectionStart = _selStart + 1;
            }
            txtExpression.Focus();
        }

        private void btn11_Click(object sender, EventArgs e)
        {
            int _selStart = txtExpression.SelectionStart;
            if (m_FeatureLayer.FeatureClass != null)
            {
                IDataset _dataSet = (IDataset)m_FeatureLayer.FeatureClass;
                if (_dataSet.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace) //personal DB
                {
                    txtExpression.Text = txtExpression.Text.Insert(_selStart, "*");
                }
                else
                    txtExpression.Text = txtExpression.Text.Insert(_selStart, "%");

                txtExpression.SelectionStart = _selStart + 1;
            }
            txtExpression.Focus();
        }

        private void btn12_Click(object sender, EventArgs e)
        {
            int _selStart = txtExpression.SelectionStart;
            txtExpression.Text = txtExpression.Text.Insert(_selStart, " " + btn12.Text);
            txtExpression.SelectionStart = _selStart + btn12.Text.Length + 1;
            txtExpression.Focus();
        }

        private void btn13_Click(object sender, EventArgs e)
        {
            int _selStart = txtExpression.SelectionStart;
            txtExpression.Text = txtExpression.Text.Insert(_selStart, " " + btn13.Text);
            txtExpression.SelectionStart = _selStart + btn13.Text.Length + 1;
            txtExpression.Focus();
        }
        #endregion

        private void combLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFields(m_Map, (string)combLayers.SelectedItem, ref m_FeatureLayer);
            listboxValues.Items.Clear();
            listboxValues.Enabled = false;
            btnUniqueValue.Enabled = true;
            txtExpression.Text = null;
            //if(m_MapControl.Map.FeatureSelection.CanClear())
            m_MapControl.Map.ClearSelection();
        }

        private void btnUniqueValue_Click(object sender, EventArgs e)
        {
            ITable _table;
            if (null != m_FeatureLayer)
            {
                _table = m_FeatureLayer as ITable;
                this.Cursor = Cursors.WaitCursor;
                GetUniqueValue(_table, listboxFields.Text.Substring(1, listboxFields.Text.Length - 2), listboxValues);
                btnUniqueValue.Enabled = false;
                listboxValues.Enabled = true;
                this.Cursor = Cursors.Arrow;
            }
        }

        private void listboxFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            listboxValues.Items.Clear();
            listboxValues.Enabled = false;
            btnUniqueValue.Enabled = true;
        }

        private void listboxFields_DoubleClick(object sender, EventArgs e)
        {
            int _selStart = txtExpression.SelectionStart;
            string strFieldAliasName = listboxFields.Text.Substring(1, listboxFields.Text.Length - 2);
            string strFieldName = m_FieldTable[strFieldAliasName].ToString();
            txtExpression.Text = txtExpression.Text.Insert(_selStart, " [" + strFieldName + "]");
            txtExpression.SelectionStart = _selStart + listboxFields.Text.Length + 1;
            txtExpression.Focus();
        }

        private void listboxValues_DoubleClick(object sender, EventArgs e)
        {
            int _selStart = txtExpression.SelectionStart;
            txtExpression.Text = txtExpression.Text.Insert(_selStart, " " + listboxValues.Text);
            txtExpression.SelectionStart = _selStart + listboxValues.Text.Length + 1;
            txtExpression.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtExpression.Text = null;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtExpression.Text))
                {
                    MessageBox.Show("查询条件不能为空");
                    return;
                }

                SelectFeatures();
                ZoomToSelectedFeatures();

                //lblNums.Text = "当前选择的要素数目为" + m_MapControl.Map.SelectionCount.ToString() + "个";
            }
            catch
            {
                MessageBox.Show("语法错误,请重新检查");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Parent.Hide();
        }

        private void combMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_intSelectionResultEnum = combMethods.SelectedIndex;
        }

    }
}
