using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.SystemUI;
using Hy.Common.Utility.Esri;

namespace Hy.Check.UI.Forms
{
    public partial class FrmSQLSearch : DevExpress.XtraEditors.XtraForm
    {

        private AxMapControl m_MapControl;
        private IMap m_Map;
        private IActiveView m_activeView;
        private IFeatureLayer m_FeatureLayer = null;

        private Hashtable m_FieldTable;
        /// <summary>
        /// cboFieldsValue已经获取了唯一值
        /// </summary>
        private bool bGetUniqueValue=true;

        public FrmSQLSearch(AxMapControl mapControl)
        {
            InitializeComponent();

            m_MapControl = mapControl;
            m_Map = m_MapControl.Map;
            m_activeView = m_MapControl.ActiveView;

            InitcboLayers();

            if (cboLayers.Properties.Items.Count > 0)
                cboLayers.SelectedIndex = 0;
            
        }

        private void InitcboLayers()
        {
            //获取图层列表
            GetLayers(m_Map);
        }
        private void InitcboFields()
        {
            if (cboLayers.Properties.Items.Count > 0)
                cboLayers.SelectedIndex = 0;
           
            //获取图层对应的字段
            GetFields();
            if (this.cboFields.Properties.Items.Contains("标识码"))
            {
                this.cboFields.SelectedItem = "标识码";
            }
            
        }

        /// <summary>
        /// 获取地图控件中的所有错误
        /// </summary>
        /// <param name="pMap"></param>
        private void GetLayers(IMap pMap)
        {
            try
            {
                if (pMap == null)
                    return;
                int lyrCount = pMap.LayerCount;

                for (int i = 0; i < lyrCount; i++)
                {
                    ILayer pLayer = pMap.get_Layer(i);
                    if(pLayer.Name=="拓扑错误")
                    {
                        continue;
                    }

                    if (pLayer is ICompositeLayer)
                    {
                        GetLayerFromCompositeLayer(pLayer);
                    }
                    else
                    {
                        cboLayers.Properties.Items.Add(pLayer.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 获取选中图层的字段
        /// </summary>
        private void GetFields()
        {
            try
            {
                string sLayerName = (string) cboLayers.SelectedItem;
                IFeatureLayer _featureLayer;
                IFeatureClass _featureClass = null;
                IFields _fields;
                IDataset _dataSet;

                _featureLayer = (IFeatureLayer)MapOperAPI.GetLayerFromMapByName(m_Map, sLayerName);
                m_FeatureLayer = _featureLayer;

                if (_featureLayer != null)
                {
                    _featureClass = _featureLayer.FeatureClass;

                }
                this.cboFields.Properties.Items.Clear();
                this.cboFieldsValue.Properties.Items.Clear();
                
                m_FieldTable = new Hashtable();
                if (_featureClass != null)
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
                                cboFields.Properties.Items.Add(pField.AliasName);
                                m_FieldTable.Add(pField.AliasName, pField.Name);
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
                                if (pField.Name.ToUpper().Contains("OBJECTID")|| pField.AliasName.ToLower().Contains("shape"))
                                {
                                    continue;
                                
                                }

                                cboFields.Properties.Items.Add(pField.AliasName);
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
        /// 获取复合图层（ICompositeLayer）中的图层，并添加到combox中
        /// </summary>
        /// <param name="pLayer">复合图层</param>
        private void GetLayerFromCompositeLayer(ILayer pLayer)
        {
            ICompositeLayer pComLayer = (ICompositeLayer)pLayer;
            ILayer tempLyr = null;
            for (int i = 0; i < pComLayer.Count; i++)
            {
                tempLyr = pComLayer.get_Layer(i);
                if (tempLyr.Valid == false)
                {
                    continue;
                
                }
                if (tempLyr.Name == "注记")
                {
                    continue;
                
                }
                if (tempLyr is ICompositeLayer)
                {
                        GetLayerFromCompositeLayer(tempLyr);                    
                }
                else
                {
                   cboLayers.Properties.Items.Add(tempLyr.Name);
                   
                }
            }
        }

        /// <summary>
        /// 获取指定字段的所有值
        /// </summary>
        /// <param name="table"></param>
        /// <param name="sFieldName">字段别名</param>
        private bool GetUniqueValue(ITable table, string sFieldName)
        {

            try
            {
                IDataStatistics _dataStatistics = new DataStatisticsClass();
                ICursor _cursor;
                               
                int _fieldIndex = table.Fields.FindField(sFieldName);

                _cursor = table.Search(null, false);
                this.cboFieldsValue.Properties.Items.Clear();

                _dataStatistics.Field = sFieldName;
                _dataStatistics.Cursor = _cursor;
                IEnumerator _enumerator = _dataStatistics.UniqueValues;
                _enumerator.Reset();
                while (_enumerator.MoveNext())
                {
                    cboFieldsValue.Properties.Items.Add(_enumerator.Current.ToString());

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //MessageBox.Show(ex.Message);
            }
            return true;
        }


        private void cboFieldsValue_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void cboLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFields();
            if (this.cboFields.Properties.Items.Contains("标识码"))
            {
                this.cboFields.SelectedItem = "标识码";
                
            }
            else
            {
                this.cboFields.SelectedIndex = 1;
            }
            bGetUniqueValue = false;
            btnlist.Enabled = true;

            this.cboFieldsValue.Properties.Items.Clear();
            this.cboFieldsValue.SelectedItem = null;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (cboLayers.SelectedItem == null || this.cboFields.SelectedItem == null || this.cboFieldsValue.SelectedItem==null)
            {
                XtraMessageBox.Show("图层名,字段名和字段值均不能为空！", "提示");
                return;
            }

            //if (!IsValid(this.cboFieldsValue.Text))
            //{
            //    XtraMessageBox.Show("没有匹配的查询结果，请重新设置查询条件！", "提示");
            //    return;
            //}

            this.Cursor = Cursors.WaitCursor;

            //删除图上所有的图形要素
            IGraphicsContainer pGraphContainer1 = m_activeView.GraphicsContainer;

            int index = 0;
            //GT_CARTO.Engine_API.DeleteElementExceptionManualCheck(m_Map, pGraphContainer1, ref index);


            if (SelectFeatures())
                ZoomToSelectedFeatures();
           
                

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// 使用正则表达式，限制strvalue的匹配,strvalue不能为特殊字符
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private bool IsValid(string strValue)
        {
            return Regex.IsMatch(strValue, "^[A-Za-z0-9]+$");
        }

        private bool SelectFeatures()
        {
            try
            {               
                string strTemp = (string)this.cboFields.SelectedItem;
                string strFieldAliasName = strTemp;//.Substring(1, strTemp.Length - 2);
                string strFieldName = m_FieldTable[strFieldAliasName].ToString();

                
                IFeatureClass pFtCls = m_FeatureLayer.FeatureClass;
                int nFieldIndex = pFtCls.Fields.FindField(strFieldName);
                string strWhereClause = "";
                if (pFtCls.Fields.get_Field(nFieldIndex).Type == esriFieldType.esriFieldTypeString)
                    strWhereClause =  strFieldName + " = '" + (string)this.cboFieldsValue.SelectedItem+"'";
                else
                    strWhereClause = strFieldName + " =" + (string)this.cboFieldsValue.SelectedItem;
                               
                if (m_FeatureLayer == null)
                    return false;
                IQueryFilter _queryFilter;
                IFeatureSelection _featureSelection;

                _featureSelection = m_FeatureLayer as IFeatureSelection;
                _queryFilter = new QueryFilterClass();
                _queryFilter.SubFields = "*";
                _queryFilter.WhereClause = strWhereClause;
                                
                m_Map.ClearSelection();
                _featureSelection.SelectFeatures(_queryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);

                if (_featureSelection.SelectionSet.Count == 0)
                {
                    XtraMessageBox.Show("没有符合查询条件的要素！","提示",MessageBoxButtons.OK);
                    return false;
                
                }
                if (m_FeatureLayer.Visible == false)
                {
                    m_FeatureLayer.Visible = true;
                    object obj = MapOperAPI.GetLayerParent((IBasicMap)m_Map, (ILayer)m_FeatureLayer);
                    if (obj is ICompositeLayer)
                    {
                        ILayer pParentLayer = (ILayer)obj;
                        pParentLayer.Visible = true;                    

                    }
                
                }
                    
                m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("没有匹配的查询结果，请重新设置查询条件！", "提示");
                return false;
            }
            return true;
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

        private void cboFieldsValue_DragDrop(object sender, DragEventArgs e)
        {
            

        }

        private void cboFieldsValue_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void cboFieldsValue_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            
        }

        private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            bGetUniqueValue = false;
            btnlist.Enabled = true;
            this.cboFieldsValue.Properties.Items.Clear();
            this.cboFieldsValue.SelectedItem = null;
        }

        private void btnlist_Click(object sender, EventArgs e)
        {
            if (bGetUniqueValue == false)
            {
                if(cboLayers.SelectedItem==null || this.cboFields.SelectedItem==null)
                {
                    XtraMessageBox.Show("图层名和字段名不能为空！", "提示");
                    return;
                }
                string strTemp = (string)this.cboFields.SelectedItem;
                string strFieldAliasName = strTemp;//.Substring(1, strTemp.Length - 2);
                string strFieldName = m_FieldTable[strFieldAliasName].ToString();


                ITable _table = m_FeatureLayer as ITable;
                this.Cursor = Cursors.WaitCursor;
                if(GetUniqueValue(_table, strFieldName))
                {
                    if (this.cboFieldsValue.Properties.Items.Count > 0)
                    {
                        cboFieldsValue.SelectedIndex = 0;
                    }

                    bGetUniqueValue = true;
                    btnlist.Enabled = false;
                }
                this.Cursor = Cursors.Default;
               
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}