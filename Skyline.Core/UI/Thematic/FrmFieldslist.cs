using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TerraExplorerX;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;

namespace Skyline.Core.UI
{
    public partial class FrmFieldslist : Form
    {
        private int LayerID = -1;
        private string TitleField = "";
        public UCThematicUniqueValue fatherform;
        public FrmFieldslist()
        {
            InitializeComponent();
        }
        public FrmFieldslist(int GroupID,string FieldName)
        {
            LayerID = GroupID;
            TitleField = FieldName;
            InitializeComponent();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmStaThemsFieldslist_Load(object sender, EventArgs e)
        {
            try
            {
                //获取指定图层值字段列表
                if (LayerID >= 0)
                {
                    this.listViewFields.Items.Clear();
                    ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(LayerID);
                    IDataSourceInfo61 SlctdLyrDataSource = SlctdLyr.DataSourceInfo;
                    if (SlctdLyrDataSource.Attributes.Count > 0)
                    {
                        SlctdLyrDataSource.Attributes.ImportAll = true;
                        foreach (IAttribute61 Attribute in SlctdLyrDataSource.Attributes)
                        {
                            if (Attribute.Type == AttributeTypeCode.AT_INTEGER || Attribute.Type == AttributeTypeCode.AT_DOUBLE)
                            {
                                ListViewItem AItem = new ListViewItem(Attribute.Name);
                                this.listViewFields.Items.Add(AItem);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("该要素无属性！");
                    }
                }
            }
            catch
            {
                MessageBox.Show("字段列表加载失败", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close();
            }
            /***20130227杨漾（添加trycatch）****/
    
        }
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            try
            {
                //将选定字段名写入Fields
                if (this.listViewFields.Items.Count > 0)
                {
                    if (this.listViewFields.CheckedItems.Count > 0)
                    {
                        if (fatherform.MFGridView != null)
                        {
                            //判断是否添加重复字段
                            for (int i = 0; i < this.listViewFields.CheckedItems.Count; i++)
                            {

                                if (!ReaptOrNot(this.listViewFields.CheckedItems[i].Text))
                                {
                                    fatherform.MFGridView.Rows.Add("", this.listViewFields.CheckedItems[i].Text);
                                    InsertValueAndGetPos2(this.listViewFields.CheckedItems[i].Text);
                                    this.fatherform.MFGridView2.ClearSelection();
                                }
                                else
                                    MessageBox.Show("字段:" + this.listViewFields.CheckedItems[i].Text + "已添加!");

                            }

                        }
                        //根据添加字段后datagridview中选择的字段总数生成颜色带并分配颜色
                        //获取颜色分段方案
                        if (fatherform.MFGridView.Rows.Count > 0)
                        {
                            if (fatherform.MFGridView.Rows.Count == 1)
                            {
                                fatherform.MFGridView.Rows[0].Cells[0].Style.BackColor = fatherform.StartColor.BackColor;
                                this.fatherform.MFGridView.ClearSelection();
                            }
                            else
                            {
                                List<Color> Colorlist = new List<Color>();
                                Colorlist = fatherform.ProduceColors(fatherform.StartColor.BackColor, fatherform.EndColor.BackColor, fatherform.MFGridView.Rows.Count);
                                for (int m = 0; m < fatherform.MFGridView.Rows.Count; m++)
                                {
                                    fatherform.MFGridView.Rows[m].Cells[0].Style.BackColor = Colorlist[m];

                                }
                                this.fatherform.MFGridView.ClearSelection();
                            }
                        }

                    }
                    else
                        MessageBox.Show("请先勾选字段！");

                }
                else
                    MessageBox.Show("无合适值字段！");
                this.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加字段失败", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close();
            }
            /***20130227杨漾（添加trycatch）****/
        }
        private void InsertValueAndGetPos2(string FieldName)//位置和要素要同时获取，否则值和位置可能不对应
        {
            //try
            //{
                if (fatherform.MFGridView2 != null)
                {
                    //添加列
                    if (fatherform.MFGridView2.ColumnCount == 0)
                    {
                        if(TitleField == "")
                            fatherform.MFGridView2.Columns.Add("FeatureID", "FeatureID");
                        else
                            fatherform.MFGridView2.Columns.Add(TitleField, TitleField);
                        fatherform.MFGridView2.Columns.Add(FieldName, FieldName);
                    }
                    else
                    {
                        fatherform.MFGridView2.Columns.Add(FieldName, FieldName);
                    }
                    //添加值
                    ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(LayerID);
                    string[] ExtractFile = SlctdLyr.DataSourceInfo.ConnectionString.Split(';');
                    string FilePath = ExtractFile[0].Substring(9);

                    IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
                    IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)pWorkspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(FilePath), 0);
                    IFeatureClass pFeatureClass = (IFeatureClass)pFeatureWorkspace.OpenFeatureClass(System.IO.Path.GetFileName(FilePath));

                    bool ReProjectOrNot = false;//标记是否需要重投影
                    string LayerCord = SlctdLyr.CoordinateSystem.WktDescription;
                    string TerrainCord = Program.sgworld.Terrain.CoordinateSystem.WktDescription;
                    if (LayerCord != TerrainCord)
                        ReProjectOrNot = true;

                    string AValue = "";
                    IPosition61 cPos = null;
                    List<IPosition61> Positions = new List<IPosition61>();
                    double dXCoord;
                    double dYCoord;
                    double dAltitude = 0;
                    AltitudeTypeCode eAltitudeTypeCode = AltitudeTypeCode.ATC_ON_TERRAIN;
                    double dYaw = 0.0;
                    double dPitch = 0.0;
                    double dRoll = 0.0;
                    double dDistance = 20000;
                    for (int i = 0; i < pFeatureClass.FeatureCount(null); i++)
                    {
                        //插入值
                        AValue = pFeatureClass.GetFeature(i).get_Value(pFeatureClass.FindField(FieldName)).ToString();
                        if (fatherform.MFGridView2.ColumnCount == 2)
                        {
                            if (fatherform.MFGridView2.Columns[0].Name == "FeatureID")
                                this.fatherform.MFGridView2.Rows.Add(pFeatureClass.GetFeature(i).OID, AValue);
                            else
                                this.fatherform.MFGridView2.Rows.Add(pFeatureClass.GetFeature(i).get_Value(pFeatureClass.FindField(TitleField)).ToString(), AValue);
                        }
                        else
                        {
                            if (this.fatherform.MFGridView2.RowCount > 0)
                                fatherform.MFGridView2.Rows[i].Cells[FieldName].Value = AValue;
                        }
                        //获取位置
                        ESRI.ArcGIS.Geometry.IGeometry pGeometry = pFeatureClass.GetFeature(i).Shape;
                        if (ReProjectOrNot)
                        {
                            ESRI.ArcGIS.Geometry.ISpatialReferenceFactory srFactory = new ESRI.ArcGIS.Geometry.SpatialReferenceEnvironmentClass();
                            ESRI.ArcGIS.Geometry.IGeographicCoordinateSystem gcs = srFactory.CreateGeographicCoordinateSystem((int)ESRI.ArcGIS.Geometry.esriSRGeoCSType.esriSRGeoCS_WGS1984);
                            ESRI.ArcGIS.Geometry.ISpatialReference sr1 = gcs;
                            pGeometry.Project(sr1);
                        }
                        ESRI.ArcGIS.Geometry.IPoint pPoint = null;
                        switch (pFeatureClass.ShapeType)
                        {
                            case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                                pPoint = (ESRI.ArcGIS.Geometry.IPoint)pGeometry;

                                break;
                            case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                                pPoint = ((ESRI.ArcGIS.Geometry.IPolyline)pGeometry).ToPoint;
                                break;
                            case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                                pPoint = ((ESRI.ArcGIS.Geometry.IArea)(ESRI.ArcGIS.Geometry.IPolygon)pGeometry).Centroid;
                                break;
                            default:
                                break;
                        }
                        dXCoord = pPoint.X;
                        dYCoord = pPoint.Y;
                        cPos = Program.sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);
                        Positions.Add(cPos);
                    }
                    if (Positions != null)
                        fatherform.StaThemePos = Positions;
                }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Unexpected Error:" + ex.Message);
            //}
            /***20130227杨漾（去掉trycatch，抛给上层trycatch处理异常）****/
        }
        private bool ReaptOrNot(string FieldsName)
        {
            bool result = false;
            if (fatherform.MFGridView.Rows.Count > 0)
            {
                for (int i = 0; i < fatherform.MFGridView.Rows.Count; i++)
                {
                    if (FieldsName == fatherform.MFGridView.Rows[i].Cells[1].Value.ToString())
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        private void FrmFieldslist_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.fatherform.myFields = null;
        }


   
    }
}
