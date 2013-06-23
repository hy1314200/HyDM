using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using ESRI.ArcGIS.Carto;
//using ESRI.ArcGIS.Controls;
//using ESRI.ArcGIS.Geodatabase;
using System.Collections;
using System.Configuration;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using Skyline.Core.Helper;
using TerraExplorerX;
//using ESRI.ArcGIS.Geometry;

namespace Skyline.Core.UI
{
    public partial class FrmSearchByAttribute : FrmBase
    {
        private string ModelID = ConfigurationManager.AppSettings["ModelID"];
        private IWorkspace pWorkspace;
        private IFeatureClass tFeatureClass;
        private string ConnString = ADODBHelper.ConfigConnectionString;
        private ADODBHelper m_OracleHelper;
        private int TYPE = 0;
        private string LayerName = "";
        private DataTable dt;
        private Hashtable FeatureList = new Hashtable();
        private SqlHelper m_SqlHelper;
        private DataSet ds;
        private Form _frmMain;
   

        public FrmSearchByAttribute(Form frmMain)
        {
            _frmMain = frmMain;
            if (base.BeginForm(frmMain))
            {
                InitializeComponent();
            }
            else
            {
                this.Close();
            }
        }
       
   

        private void Frm2SearchByAttribute_Load(object sender, EventArgs e)
        {
            try
            {
                labelControl2.Text = "select * from FILE3DATTRIBUTE where";
                IFeatureWorkspace pFeatureWorkspace = Program.pWorkspace as IFeatureWorkspace;
                this.tFeatureClass = pFeatureWorkspace.OpenFeatureClass(ConfigurationManager.AppSettings["ModleTable"]);
                this.m_OracleHelper = new ADODBHelper(this.ConnString, true);
                m_SqlHelper = new SqlHelper();
                getFieldNames(0);
            }
            catch (Exception)
            {
              
            }
            
           
        }

       /// <summary>
        /// 获得字段名列表
       /// </summary>
       /// <param name="layerName"></param>
       /// <param name="pListBox"></param>
        private void getFieldNames(int type)
        {
            ds = new DataSet();
            AttributeCollistBox.Items.Clear();
            if (type ==0)
            {
                ds = this.m_OracleHelper.OpenDS("select t.* from FILE3DATTRIBUTE t");
                //ds = m_SqlHelper.selectTableAll("builderObject");
                // this.gridControl1.DataSource = ds.Tables[0];
                DataTable dt = ds.Tables[0];
                foreach (DataColumn item in dt.Columns)
                {
                    string Caption = "";
                    try
                    {
                        
                        string Asql = "select t.fieldalias from ATTR3DFIELDMAPPING t where t.fieldname ='" + item.ColumnName + "'";
                        Caption = item.ColumnName + ":" + this.m_OracleHelper.ExecSQLScalar(Asql).ToString();
                        AttributeCollistBox.Items.Add(Caption);
                    }
                    catch (Exception ex)
                    {
                        Caption = item.ColumnName + ":";
                        continue;
             
                    }
                   
                    //switch (item.ColumnName)
                    //{
                    //    case "OBJECTID":
                    //        string Alias = "";

                    //        Caption = item.ColumnName + ":模型主键";
                    //        break;
                    //    case "YSDM":
                    //        Caption = item.ColumnName + ":要素代码";
                    //        break;
                    //    case "YSLX":
                    //        Caption = item.ColumnName + ":要素类型";
                    //        break;
                    //    case "MC":
                    //        Caption = item.ColumnName + ":模型名称";
                    //        break;
                    //    case "LXR":
                    //        Caption = item.ColumnName + ":联系人";
                    //        break;
                    //    case "GXSJ":
                    //        Caption = item.ColumnName + ":更新时间";
                    //        break;

                    //    case "LXDH":
                    //        Caption = item.ColumnName + ":联系电话";
                    //        break;
                    //    case "XXDZ":
                    //        Caption = item.ColumnName + ":详细地址";
                    //        break;
                    //    case "JLXH":
                    //        Caption = item.ColumnName + ":街路巷号";
                    //        break;
                    //    case "LDH":
                    //        Caption = item.ColumnName + ":楼栋号";
                    //        break;
                    //    case "LFH":
                    //        Caption = item.ColumnName + ":楼房号";
                    //        break;
                    //    case "MPH":
                    //        Caption = item.ColumnName + ":门牌号";
                    //        break;
                    //    case "GM":
                    //        Caption = item.ColumnName + ":规模";
                    //        break;
                    //    case "JYFW":
                    //        Caption = item.ColumnName + ":经营范围";
                    //        break;
                    //    case "QYXZ":
                    //        Caption = item.ColumnName + ":企业性质";
                    //        break;
                    //    case "ZCZJ":
                    //        Caption = item.ColumnName + ":注册资金";
                    //        break;
                    //    case "FR":
                    //        Caption = item.ColumnName + ":法人";
                    //        break;
                    //    case "ZCSJ":
                    //        Caption = item.ColumnName + ":注册时间";
                    //        break;
                    //    case "ZCDD":
                    //        Caption = item.ColumnName + ":注册地点";
                    //        break;
                    //    case "FZRDH":
                    //        Caption = item.ColumnName + ":负责人电话";
                    //        break;
                    //    case "FZRDZ":
                    //        Caption = item.ColumnName + ":负责人地址";
                    //        break;
                    //    default:
                    //        Caption = item.ColumnName;
                    //        break;
                    //}
                   

                }
            }
            else if (type ==1)
            {
                ds = m_SqlHelper.selectTableAll("modelgroup");
                // this.gridControl1.DataSource = ds.Tables[0];
                DataTable dt = ds.Tables[0];
                foreach (DataColumn item in dt.Columns)
                {
                    string Caption = "";

                    switch (item.ColumnName)
                    {
                        case "ID":
                            Caption = item.ColumnName+ ":建筑主键";
                            break;
                        case "groupname":
                            Caption = item.ColumnName + ":建筑名称";
                            break;

                        case "buildLongitude":
                            Caption = item.ColumnName + ":建筑经度";
                            break;

                        case "buildLatitude":
                            Caption = item.ColumnName + ":建筑纬度";
                            break;
                        case "buildType":
                            Caption = item.ColumnName + ":建筑类型";
                            break;
                        case "groupinfo":
                            Caption = item.ColumnName + ":建筑描述";
                            break;

                        default:
                            Caption = item.ColumnName;
                            break;
                    }
                    AttributeCollistBox.Items.Add(Caption);

                }
            }
         
        }
    
        /// <summary>
        /// 图层选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }
        /// <summary>
        /// 获取属性表主键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetOnlyValue_Click(object sender, EventArgs e)
        {
         
        }
        /// <summary>
        /// 获取属性表列字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AttributeCollistBox_DoubleClick(object sender, EventArgs e)
        {
            string whereCluse = SQLRichTextBox.Text;
            whereCluse = whereCluse + ConvertEnglish(AttributeCollistBox.SelectedItem.ToString()) + " ";
            SQLRichTextBox.Text = whereCluse;
        }

        private string ConvertEnglish(string dant)
        {
            string[] dantArry = dant.Split(':');
            dant = dantArry[0];

            return dant;
        }
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="layerName"></param>
        /// <param name="whereClause"></param>
        private void getFeaturesList(string layerName, string pwhereClause)
        {
           
           
        
        
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            //this.AttributeCollistBox.Items.Clear();
            this.SQLRichTextBox.Clear();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close(); ;
        }

        private void simpleButton17_Click(object sender, EventArgs e)
        {
            if (TYPE == 0)
            {
                try
                {
                    string sql = String.Format("{0} {1}", labelControl2.Text, this.SQLRichTextBox.Text);
                    DataSet _dataset = new DataSet();
                    _dataset = this.m_OracleHelper.OpenDS(sql);
                   // _dataset = m_SqlHelper.selectSQL("builderObject", sql);
                    this.gridControl1.DataSource = _dataset.Tables[0];
                    
                    foreach (DevExpress.XtraGrid.Columns.GridColumn item in this.gridView1.Columns)
                    {
                        try
                        {
                            string Asql = "select t.fieldalias from ATTR3DFIELDMAPPING t where t.fieldname ='" + item.FieldName + "'";
                            item.Caption = this.m_OracleHelper.ExecSQLScalar(Asql).ToString();
                        }
                        catch (Exception)
                        {

                            continue;
                        }
                      
                        //switch (item.FieldName)
                        //{
                        //    case "OBJECTID":
                        //        item.Caption = "模型主键";
                        //        break;
                        //    case "YSDM":
                        //        item.Caption = "要素代码";
                        //        break;
                        //    case "YSLX":
                        //        item.Caption = "要素类型";
                        //        break;
                        //    case "MC":
                        //        item.Caption = "模型名称";
                        //        break;
                        //    case "LXR":
                        //        item.Caption = "联系人";
                        //        break;
                        //    case "GXSJ":
                        //        item.Caption = "更新时间";
                        //        break;
                        //    case "LXDH":
                        //        item.Caption = "联系电话";
                        //        break;
                        //    case "XXDZ":
                        //        item.Caption = "详细地址";
                        //        break;
                        //    case "JLXH":
                        //        item.Caption = "街路巷号";
                        //        break;
                        //    case "LDH":
                        //        item.Caption = "楼栋号";
                        //        break;
                        //    case "LFH":
                        //        item.Caption = "楼房号";
                        //        break;
                        //    case "MPH":
                        //        item.Caption = "门牌号";
                        //        break;
                        //    case "GM":
                        //        item.Caption = "规模";
                        //        break;
                        //    case "JYFW":
                        //        item.Caption = "经营范围";
                        //        break;
                        //    case "QYXZ":
                        //        item.Caption = "企业性质";
                        //        break;
                        //    case "ZCZJ":
                        //        item.Caption = "注册资金";
                        //        break;
                        //    case "FR":
                        //        item.Caption = "法人";
                        //        break;
                        //    case "ZCSJ":
                        //        item.Caption = "注册时间";
                        //        break;
                        //    case "ZCDD":
                        //        item.Caption = "注册地点";
                        //        break;
                        //    case "FZRDH":
                        //        item.Caption = "负责人电话";
                        //        break;
                        //    case "FZRDZ":
                        //        item.Caption = "负责人地址";
                        //        break;
                        //}

                    }
                    this.gridView1.BestFitColumns();
                }
                catch (Exception ex)
                {
                    
                   
                }
               
            }
            else if (TYPE == 1)
            {
                try
                {
                    string sql = labelControl2.Text + " " + this.SQLRichTextBox.Text;
                    DataSet _dataset = new DataSet();
                    _dataset = m_SqlHelper.selectSQL("modelgroup", sql);
                    this.gridControl1.DataSource = _dataset.Tables[0];

                    foreach (DevExpress.XtraGrid.Columns.GridColumn item in this.gridView1.Columns)
                    {
                        switch (item.FieldName)
                        {
                            case "ID":
                                item.Caption = "建筑主键";
                                break;
                            case "groupname":
                                item.Caption = "建筑名称";
                                break;

                            case "buildLongitude":
                                item.Caption = "建筑经度";
                                break;

                            case "buildLatitude":
                                item.Caption = "建筑纬度";
                                break;
                            case "buildType":
                                item.Caption = "建筑类型";
                                break;
                            case "groupinfo":
                                item.Caption = "建筑描述";
                                break;


                        }

                    }
                    //this.gridControl1.Views. = this.gridView1;
                    //this.gridView1this.gridControl1
                }
                catch (Exception)
                {
                    
                   // throw;
                }
               
            }
                    
           
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SQLRichTextBox.Text = SQLRichTextBox.Text + "=";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SQLRichTextBox.Text = SQLRichTextBox.Text + "<>";
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            SQLRichTextBox.Text = SQLRichTextBox.Text + ">";
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            SQLRichTextBox.Text = SQLRichTextBox.Text + ">=";
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            SQLRichTextBox.Text = SQLRichTextBox.Text + "<";
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            SQLRichTextBox.Text = SQLRichTextBox.Text + "<=";
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            SQLRichTextBox.Text = SQLRichTextBox.Text + "_";
        }

        private void simpleButton19_Click(object sender, EventArgs e)
        {
            SQLRichTextBox.Text = SQLRichTextBox.Text + "%";
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            SQLRichTextBox.Text = SQLRichTextBox.Text + "()";
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            SQLRichTextBox.Text = SQLRichTextBox.Text + " like ";
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            SQLRichTextBox.Text = SQLRichTextBox.Text + " and ";
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            SQLRichTextBox.Text = SQLRichTextBox.Text + " or ";
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            SQLRichTextBox.Text = SQLRichTextBox.Text + " not ";
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            SQLRichTextBox.Text = SQLRichTextBox.Text + " is ";
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this.TYPE == 0 && this.gridView1.RowCount != 0)
                {
                    string FID = this.gridView1.GetFocusedRowCellValue("OBJECTID").ToString();
                    IQueryFilter pQuery = new QueryFilterClass();

                    pQuery.WhereClause = ModelID + " = " + FID;
                    IFeatureCursor featureCursor = this.tFeatureClass.Search(pQuery, true);
                    IFeature esriFeature = featureCursor.NextFeature();
                    ESRI.ArcGIS.Geometry.IPoint pPoint = esriFeature.Shape as ESRI.ArcGIS.Geometry.IPoint;
                    IPosition61 _Position6 = Program.pCreator6.CreatePosition(pPoint.X, pPoint.Y, 100, AltitudeTypeCode.ATC_TERRAIN_RELATIVE, 0, -89, 0, 100);
                    Program.pNavigate6.FlyTo(_Position6, ActionCode.AC_FLYTO);
                    //var x = this.gridView1.GetFocusedRowCellValue("CX");
                    //var y = this.gridView1.GetFocusedRowCellValue("CY");
                    //ActionClass.Vertical3DFlyTo(Convert.ToDouble(x), Convert.ToDouble(y),-89,100);
                }
                else if (this.TYPE == 1 && this.gridView1.RowCount != 0)
                {
                    var Longitude = this.gridView1.GetFocusedRowCellValue("CX");
                    var Latitude = this.gridView1.GetFocusedRowCellValue("CY");
                    IPosition61 _Position6 = Program.pCreator6.CreatePosition(Convert.ToDouble(Longitude), Convert.ToDouble(Latitude), 100, AltitudeTypeCode.ATC_TERRAIN_RELATIVE, 0, -89, 0, 820);
                    Program.pNavigate6.FlyTo(_Position6, ActionCode.AC_FLYTO);
                }
            }
            catch (Exception ex)
            {
                
               // throw;
            }
            
        
        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {
            SQLRichTextBox.Text = SQLRichTextBox.Text + " '' ";
        }

        private void LayersComboBox_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.LayersComboBox.Text == "部分模型")
                {
                    TYPE = 0;
                    labelControl2.Text = "select * from FILE3DATTRIBUTE where";
                    getFieldNames(TYPE);
                    this.SQLRichTextBox.Clear();

                    gridControl1.DataSource = null;
                    gridControl1.MainView.Dispose();
                    gridView1 = new GridView(gridControl1);
                    gridControl1.MainView = gridView1;
                    gridControl1.ForceInitialize();
                    gridView1.OptionsBehavior.Editable = false;
                    gridView1.OptionsView.ShowGroupPanel = false;
                    gridView1.OptionsView.ColumnAutoWidth = false;
                    gridControl1.RefreshDataSource();
                    this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);

                }
                else if (this.LayersComboBox.Text == "整体建筑")
                {
                    TYPE = 1;
                    labelControl2.Text = "select * from modelgroup where";
                    getFieldNames(TYPE);
                    this.SQLRichTextBox.Clear();
                    gridControl1.DataSource = null;
                    gridControl1.MainView.Dispose();
                    gridView1 = new GridView(gridControl1);
                    gridControl1.MainView = gridView1;
                    gridControl1.ForceInitialize();
                    gridView1.OptionsView.ShowGroupPanel = false;
                    gridView1.OptionsView.ColumnAutoWidth = false;
                    gridView1.OptionsBehavior.Editable = false;
                    gridControl1.RefreshDataSource();
                    //this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
                    this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
                }
            }
            catch (Exception)
            {
               
            }
            
        }

        private void FrmSearchByAttribute_FormClosing(object sender, FormClosingEventArgs e)
        {
            _frmMain.RemoveOwnedForm(this);
        }

     
    }
}