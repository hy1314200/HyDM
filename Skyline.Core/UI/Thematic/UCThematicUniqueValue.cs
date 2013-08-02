using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using TerraExplorerX;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections;
using System.Diagnostics;
using System.Security.Permissions;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DataSourcesFile;
using DevExpress.XtraCharts;

namespace Skyline.Core.UI
{

    public partial class UCThematicUniqueValue : DevExpress.XtraBars.Ribbon.RibbonForm
    {
    
        public UCThematicUniqueValue()
        {
            InitializeComponent();
           
        }

        public DataGridView MFGridView = null;
        public DataGridView MFGridView2 = null;
        public List<IPosition61> StaThemePos = null;
        public DataGridView SimpleThemeGridView = null;
        public DataGridView BreakThemeGridView = null;
        public PictureBox StartColor = null;
        public PictureBox EndColor = null;
        public DevExpress.XtraEditors.SimpleButton BtnSymbolType = null;
        public DevExpress.XtraEditors.SimpleButton BtnSymbolType2 = null;
        public SymbolType CurrentSymbol = new SymbolType();
       
        
        #region 单值专题
        private List<int> ShpLyrID = new List<int>();//所有矢量图层在信息树中的索引值
        public FrmPointSymbol myPointSymbol = null;
        public FrmPolylineSymbol myPolylineSymbol = null;
        public int CurrentThemeType = 0;//ThemeType专题类型，0为无专题，1为单值专题，2为分段专题,3为标签专题,4为统计专题
        private bool SaveOrNot_Simple = false;
        //private void SimpleTheme_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        if (CurrentThemeType != 0 & CurrentThemeType != 1)
        //        {
        //            //关闭当前面板
        //            DialogResult dr = MessageBox.Show("关闭当前专题面板？", "关闭提示", MessageBoxButtons.OKCancel);

        //            if (dr == DialogResult.OK)
        //            {
        //                switch (CurrentThemeType)
        //                {
        //                    case 2:
        //                        this.dockPanel5_ClassBreak.Close();
        //                        CurrentThemeType = 0;
        //                        break;
        //                    case 3:
        //                        this.dockPanel5_LabelTheme.Close();
        //                        CurrentThemeType = 0;
        //                        break;
        //                    case 4:
        //                        this.dockPanel5_StatisticTheme.Close();
        //                        CurrentThemeType = 0;
        //                        break;
        //                    default:
        //                        break;
        //                }
        //            }
        //        }
        //        if (CurrentThemeType == 1 || CurrentThemeType == 0)
        //        {
        //            //遍历信息树，加载shapefile图层名
        //            int current = Program.sgworld.ProjectTree.GetNextItem(0, ItemCode.ROOT);
        //            if (current > 0)
        //            {
        //                //清除单值面板已有数据
        //                this.splitContainer1.SplitterDistance = 95;
        //                this.comboBoxLayer.Items.Clear();
        //                this.comboBoxValue.Items.Clear();
        //                this.comboBoxValue.Text = "";
        //                this.simpleBtnSymbol.Image = null;
        //                this.tabControl1.TabPages[0].Text = "";
        //                this.tabControl1.TabPages[0].ImageIndex = -1;
        //                this.dataGridView1.Rows.Clear();
        //                this.pictureBoxStartCLR3.BackColor = Color.PaleGreen;
        //                this.pictureBoxEndCLR3.BackColor = Color.DarkGreen;

        //                ShpLyrID.Clear();
        //                ScanTree(current, 1);
        //                if (ShpLyrID.Count > 0)
        //                {
        //                    CurrentThemeType = 1;
        //                    this.comboBoxLayer.Text = "请选择图层!";
        //                    //显示单值专题面板
        //                    this.dockPanel5_SimpleTheme.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        //                }
        //                else
        //                    MessageBox.Show("请先添加矢量图层!");
        //            }
        //            else
        //                MessageBox.Show("请先添加图层");

        //        }

        //    }
        //    catch 
        //    {
        //        MessageBox.Show("面板初始化遇到问题");
        //    }

        //}
        private void ScanTree(int Current, int ThemeType)//ThemeType专题类型，1为单值专题，2为分段专题,3为标签专题,4为统计专题
        {
            //当要素图层包含Annotations时，被当做组，要先判断是否为图层
            
                while (Current > 0)
                {
                    if (Program.sgworld.ProjectTree.IsLayer(Current))//是图层读取图层名
                    {
                        string ShpLyrName = Program.sgworld.ProjectTree.GetItemName(Current);
                        switch (ThemeType)
                        {
                            case 1:
                                this.comboBoxLayer.Items.Add(ShpLyrName);
                                break;
                            case 2:
                                this.comboBoxLayer2.Items.Add(ShpLyrName);
                                break;
                            case 3:
                                this.comboBoxLayer3.Items.Add(ShpLyrName);
                                break;
                            case 4:
                                this.comboBoxLayer4.Items.Add(ShpLyrName);
                                break;
                            default:
                                break;
                        }
                        ShpLyrID.Add(Current);
                    }
                    else                                             //非图层且是组，往下层递归继续判断
                    {
                        if (Program.sgworld.ProjectTree.IsGroup(Current))
                        {
                            int child = Program.sgworld.ProjectTree.GetNextItem(Current, ItemCode.CHILD);
                            ScanTree(child, ThemeType);
                        }
                    }
                    Current = Program.sgworld.ProjectTree.GetNextItem(Current, ItemCode.NEXT);
                }
            /***20130228杨漾（去除trycatch，由上层trycatch处理异常）***/

        }
        private void comboBoxLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///根据选择的图层加载相应的属性字段
            SaveOrNot_Simple = false;
            this.simpleBtnSymbol.Image = null;
            this.comboBoxValue.Items.Clear();
            this.comboBoxValue.Text = "";
            this.dataGridView1.Rows.Clear();
            this.pictureBoxStartCLR3.BackColor = Color.PaleGreen;
            this.pictureBoxEndCLR3.BackColor = Color.DarkGreen;
            bool SurportOrNot = true;
            try
            {
                if (ShpLyrID.Count > 0)
                {
                    ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer.SelectedIndex]);
                    InitialCurrentSymbol(SlctdLyr);
                    switch (SlctdLyr.GeometryType)
                    {
                        case LayerGeometryType.LGT_POINT:
                            IFeatureGroup61 pFeatureGroup = SlctdLyr.FeatureGroups.Point;

                            if (this.CurrentSymbol.CurrentPointSymbol.PointType == "Other")
                            {
                                this.splitContainer1.SplitterDistance = 95;
                                if (this.simpleBtnSymbol.Image == null)
                                    this.simpleBtnSymbol.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Polygonblue.png");
                            }
                            else
                            {
                                this.splitContainer1.SplitterDistance = 125;
                                if (this.simpleBtnSymbol.Image == null)
                                    this.simpleBtnSymbol.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\" + this.CurrentSymbol.CurrentPointSymbol.PointType + "blue.png");
                            }
                            this.tabControl1.TabPages[0].ImageIndex = 0;
                            this.tabControl1.TabPages[0].Text = "点";
                            break;
                        case LayerGeometryType.LGT_POLYLINE:
                            this.splitContainer1.SplitterDistance = 125;
                            this.tabControl1.TabPages[0].ImageIndex = 1;
                            this.tabControl1.TabPages[0].Text = "线";
                            if (this.simpleBtnSymbol.Image == null)
                                this.simpleBtnSymbol.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\" + this.CurrentSymbol.CurrentPolylineSymbol.PolylineType + ".png");

                            break;
                        case LayerGeometryType.LGT_POLYGON:
                            this.splitContainer1.SplitterDistance = 95;
                            this.tabControl1.TabPages[0].ImageIndex = 2;
                            this.tabControl1.TabPages[0].Text = "面";
                            if (this.simpleBtnSymbol.Image == null)
                                this.simpleBtnSymbol.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Polygonblue.png");
                            break;
                        default:
                            break;
                    }
                    if (SurportOrNot)
                    {
                        IDataSourceInfo61 SlctdLyrDataSource = SlctdLyr.DataSourceInfo;
                        if (SlctdLyrDataSource.Attributes.Count > 0)
                        {
                            SlctdLyrDataSource.Attributes.ImportAll = true;
                            foreach (IAttribute61 Attribute in SlctdLyrDataSource.Attributes)
                            {
                                this.comboBoxValue.Items.Add(Attribute.Name);
                            }
                            this.comboBoxValue.Text = "请选择字段！";
                        }
                        else
                            MessageBox.Show("该要素无属性！");
                    }
                }
            }
            catch
            {
                MessageBox.Show("操作遇到问题!如面板显示正常，可以忽略以继续");
            }
        }
        private void InitialCurrentSymbol(ILayer61 CurrentLayer)
        {
            IFeatureGroup61 pFeatureGroup;
            switch (CurrentLayer.GeometryType)
            {
                case LayerGeometryType.LGT_POINT:

                    pFeatureGroup = CurrentLayer.FeatureGroups.Point;
                    PointSymbol pPoint = new PointSymbol();
                    #region
                    switch (pFeatureGroup.DisplayAs)
                    {
                        case ObjectTypeCode.OT_ARROW:
                            pPoint.PointType = "Arrow";
                            if (pFeatureGroup.IsClassified("Length"))
                                pPoint.PointSizeClass = pFeatureGroup.GetClassification("Length").ToString();
                            else
                            {
                                pPoint.PointSize = (double)pFeatureGroup.GetProperty("Length");
                                //pPoint.PointSizeClass = "NoClass";
                            }
                            if (pFeatureGroup.IsClassified("Fill Color"))
                                pPoint.PointFillcolorClass = pFeatureGroup.GetClassification("Fill Color").ToString();
                            else
                            {
                                Color pColor = Color.FromArgb((int)pFeatureGroup.GetProperty("Fill Color"));
                                pPoint.PointFillcolor = pColor;
                                //pPoint.PointFillcolorClass = "NoClass";
                            }
                            if (pFeatureGroup.IsClassified("Fill Opacity"))
                                pPoint.PointFillOpacityClass = pFeatureGroup.GetClassification("Fill Opacity").ToString();
                            else
                            {
                                pPoint.PointFillOpacity = (double)pFeatureGroup.GetProperty("Fill Opacity");
                                //pPoint.PointFillOpacityClass = "NoClass";
                            }
                            pPoint.AltitMethod = (int)pFeatureGroup.GetProperty("Altitude Method");
                            break;
                        case ObjectTypeCode.OT_CIRCLE:
                            pPoint.PointType = "Circle";
                            if (pFeatureGroup.IsClassified("Radius X"))
                                pPoint.PointSizeClass = pFeatureGroup.GetClassification("Radius X").ToString();
                            else
                            {
                                pPoint.PointSize = (double)pFeatureGroup.GetProperty("Radius X");
                                //pPoint.PointSizeClass = "NoClass";
                            }
                            if (pFeatureGroup.IsClassified("Number of sides"))
                                pPoint.NumOfSidesClass = pFeatureGroup.GetClassification("Number of sides").ToString();
                            else
                            {
                                pPoint.NumOfSides = (int)pFeatureGroup.GetProperty("Number of sides");
                            }
                            if (pFeatureGroup.IsClassified("Fill Color"))
                                pPoint.PointFillcolorClass = pFeatureGroup.GetClassification("Fill Color").ToString();
                            else
                            {
                                Color pColor = Color.FromArgb((int)pFeatureGroup.GetProperty("Fill Color"));
                                pPoint.PointFillcolor = pColor;
                            }
                            if (pFeatureGroup.IsClassified("Fill Opacity"))
                                pPoint.PointFillOpacityClass = pFeatureGroup.GetClassification("Fill Opacity").ToString();
                            else
                            {
                                pPoint.PointFillOpacity = (double)pFeatureGroup.GetProperty("Fill Opacity");
                            }
                            pPoint.AltitMethod = (int)pFeatureGroup.GetProperty("Altitude Method");
                            break;
                        case ObjectTypeCode.OT_RECTANGLE:
                            pPoint.PointType = "Rectangle";
                            if (pFeatureGroup.IsClassified("Length"))
                                pPoint.PointSizeClass = pFeatureGroup.GetClassification("Length").ToString();
                            else
                                pPoint.PointSize = (double)pFeatureGroup.GetProperty("Length");
                            if (pFeatureGroup.IsClassified("Width"))
                                pPoint.PointSizeClass2 = pFeatureGroup.GetClassification("Width").ToString();
                            else
                                pPoint.PointSize2 = (double)pFeatureGroup.GetProperty("Width");
                            if (pFeatureGroup.IsClassified("Fill Color"))
                                pPoint.PointFillcolorClass = pFeatureGroup.GetClassification("Fill Color").ToString();
                            else
                            {
                                Color pColor = Color.FromArgb((int)pFeatureGroup.GetProperty("Fill Color"));
                                pPoint.PointFillcolor = pColor;
                            }
                            if (pFeatureGroup.IsClassified("Fill Opacity"))
                                pPoint.PointFillOpacityClass = pFeatureGroup.GetClassification("Fill Opacity").ToString();
                            else
                            {
                                pPoint.PointFillOpacity = (double)pFeatureGroup.GetProperty("Fill Opacity");
                            }
                            pPoint.AltitMethod = (int)pFeatureGroup.GetProperty("Altitude Method");
                            break;
                        case ObjectTypeCode.OT_REGULAR_POLYGON:
                            if (pFeatureGroup.IsClassified("Number of sides"))
                            {
                                pPoint.NumOfSidesClass = pFeatureGroup.GetClassification("Number of sides").ToString();
                                pPoint.PointType = "Other";
                            }
                            else
                            {
                                pPoint.NumOfSides = (int)pFeatureGroup.GetProperty("Number of sides");
                                switch (pPoint.NumOfSides)
                                {
                                    case 3:
                                        pPoint.PointType = "Triangle";
                                        break;
                                    case 5:
                                        pPoint.PointType = "Pentagon";
                                        break;
                                    case 6:
                                        pPoint.PointType = "Hexagon";
                                        break;
                                    default:
                                        pPoint.PointType = "Other";
                                        break;
                                }
                            }
                            if (pFeatureGroup.IsClassified("Radius X"))
                                pPoint.PointSizeClass = pFeatureGroup.GetClassification("Radius X").ToString();
                            else
                                pPoint.PointSize = (double)pFeatureGroup.GetProperty("Radius X");
                            if (pFeatureGroup.IsClassified("Fill Color"))
                                pPoint.PointFillcolorClass = pFeatureGroup.GetClassification("Fill Color").ToString();
                            else
                            {
                                Color pColor = Color.FromArgb((int)pFeatureGroup.GetProperty("Fill Color"));
                                pPoint.PointFillcolor = pColor;
                            }
                            if (pFeatureGroup.IsClassified("Fill Opacity"))
                                pPoint.PointFillOpacityClass = pFeatureGroup.GetClassification("Fill Opacity").ToString();
                            else
                            {
                                pPoint.PointFillOpacity = (double)pFeatureGroup.GetProperty("Fill Opacity");
                            }
                            pPoint.AltitMethod = (int)pFeatureGroup.GetProperty("Altitude Method");
                            break;
                        case ObjectTypeCode.OT_LABEL:
                            pPoint.PointType = "Other";
                            if (pFeatureGroup.IsClassified("Text Color"))
                                pPoint.PointFillcolorClass = pFeatureGroup.GetClassification("Text Color").ToString();
                            else
                            {
                                Color pColor = Color.FromArgb((int)pFeatureGroup.GetProperty("Text Color"));
                                pPoint.PointFillcolor = pColor;
                            }
                            pPoint.AltitMethod = (int)pFeatureGroup.GetProperty("Altitude Method");
                            break;
                        case ObjectTypeCode.OT_IMAGE_LABEL:
                            pPoint.PointType = "Other";
                            if (pFeatureGroup.IsClassified("Image Color"))
                                pPoint.PointFillcolorClass = pFeatureGroup.GetClassification("Image Color").ToString();
                            else
                            {
                                Color pColor = Color.FromArgb((int)pFeatureGroup.GetProperty("Image Color"));
                                pPoint.PointFillcolor = pColor;
                            }
                            if (pFeatureGroup.IsClassified("Image Opacity"))
                                pPoint.PointFillOpacityClass = pFeatureGroup.GetClassification("Image Opacity").ToString();
                            else
                            {
                                pPoint.PointFillOpacity = (double)pFeatureGroup.GetProperty("Image Opacity");
                            }
                            pPoint.AltitMethod = (int)pFeatureGroup.GetProperty("Altitude Method");
                            break;
                        case ObjectTypeCode.OT_MODEL:
                            pPoint.PointType = "Other";
                            if (pFeatureGroup.IsClassified("Tint Color"))
                                pPoint.PointFillcolorClass = pFeatureGroup.GetClassification("Tint Color").ToString();
                            else
                            {
                                Color pColor = Color.FromArgb((int)pFeatureGroup.GetProperty("Tint Color"));
                                pPoint.PointFillcolor = pColor;
                            }
                            if (pFeatureGroup.IsClassified("Tint Opacity"))
                                pPoint.PointFillOpacityClass = pFeatureGroup.GetClassification("Tint Opacity").ToString();
                            else
                            {

                                pPoint.PointFillOpacity = (double)pFeatureGroup.GetProperty("Tint Opacity");
                            }
                            pPoint.AltitMethod = (int)pFeatureGroup.GetProperty("Altitude Method");
                            break;
                        default:
                            pPoint.PointType = "Other";
                            if (pFeatureGroup.IsClassified("Fill Color"))
                                pPoint.PointFillcolorClass = pFeatureGroup.GetClassification("Fill Color").ToString();
                            else
                            {
                                Color pColor = Color.FromArgb((int)pFeatureGroup.GetProperty("Fill Color"));
                                pPoint.PointFillcolor = pColor;
                            }
                            if (pFeatureGroup.IsClassified("Fill Opacity"))
                                pPoint.PointFillOpacityClass = pFeatureGroup.GetClassification("Fill Opacity").ToString();
                            else
                            {

                                pPoint.PointFillOpacity = (double)pFeatureGroup.GetProperty("Fill Opacity");
                            }
                            pPoint.AltitMethod = (int)pFeatureGroup.GetProperty("Altitude Method");
                            break;
                    }
                    #endregion
                    this.CurrentSymbol.CurrentPointSymbol = pPoint;
                    this.CurrentSymbol.PrePointSymbol = pPoint;
                    break;
                case LayerGeometryType.LGT_POLYLINE:
                    pFeatureGroup = CurrentLayer.FeatureGroups.Polyline;
                    PolylineSymbol pPolyline = new PolylineSymbol();
                    #region
                    string LinePattern = pFeatureGroup.GetProperty("Line Pattern").ToString();
                    switch (LinePattern)
                    {
                        case "-1":
                            pPolyline.PolylineType = "Solidline";
                            break;
                        case "-1044481":
                            pPolyline.PolylineType = "Dottedline";
                            break;
                        case "-16776961":
                            pPolyline.PolylineType = "Dottedline2";
                            break;
                        case "-267390961":
                            pPolyline.PolylineType = "Dottedline3";
                            break;
                        case "-1010580541":
                            pPolyline.PolylineType = "Dottedline4";
                            break;
                        case "-1717986919":
                            pPolyline.PolylineType = "Dottedline5";
                            break;
                        case "-1431655766":
                            pPolyline.PolylineType = "Dottedline6";
                            break;
                        case "-16678657":
                            pPolyline.PolylineType = "Dottedline7";
                            break;
                        case "-15978241":
                            pPolyline.PolylineType = "Dottedline8";
                            break;
                    }
                    if (pFeatureGroup.IsClassified("Line Width"))
                        pPolyline.PolylineWidthClass = pFeatureGroup.GetClassification("Line Width").ToString();
                    else
                        pPolyline.PolylineWidth = (double)pFeatureGroup.GetProperty("Line Width");
                    if (pFeatureGroup.IsClassified("Line Color"))
                    {
                        pPolyline.PolylineColorClass = pFeatureGroup.GetClassification("Line Color").ToString();
                    }
                    else
                    {
                        Color pColor = Color.FromArgb((int)pFeatureGroup.GetProperty("Line Color"));
                        pPolyline.PolylineColor = pColor;
                    }
                    if (pFeatureGroup.IsClassified("Line Opacity"))
                        pPolyline.PolylineOpacityClass = pFeatureGroup.GetClassification("Line Opacity").ToString();
                    else
                    {
                        pPolyline.PolylineOpacity = (double)pFeatureGroup.GetProperty("Line Opacity");
                    }
                    if (pFeatureGroup.IsClassified("Line Back Color"))
                    {
                        pPolyline.PolylineBackColorClass = pFeatureGroup.GetClassification("Line Back Color").ToString();
                    }
                    else
                    {
                        Color pColor = Color.FromArgb((int)pFeatureGroup.GetProperty("Line Back Color"));
                        pPolyline.PolylineBackColor = pColor;
                    }
                    pPolyline.PolylineBackOpacity = (int)pFeatureGroup.GetProperty("Line Back Opacity");
                    if (pFeatureGroup.IsClassified("Line Back Opacity"))
                        pPolyline.PolylineBackOpacityClass = pFeatureGroup.GetClassification("Line Back Opacity").ToString();
                    else
                    {
                        pPolyline.PolylineBackOpacity = (int)pFeatureGroup.GetProperty("Line Back Opacity");
                    }
                    pPolyline.AltitMethod = (int)pFeatureGroup.GetProperty("Altitude Method");
                    #endregion
                    this.CurrentSymbol.CurrentPolylineSymbol = pPolyline;
                    this.CurrentSymbol.PrePolylineSymbol = pPolyline;
                    break;
                case LayerGeometryType.LGT_POLYGON:
                    pFeatureGroup = CurrentLayer.FeatureGroups.Polygon;
                    PolygonSymbol pPolygon = new PolygonSymbol();
                    #region
                    if (pFeatureGroup.IsClassified("Fill Color"))
                        pPolygon.PolygonFillcolorClass = pFeatureGroup.GetClassification("Fill Color").ToString();
                    else
                    {
                        Color pColor = Color.FromArgb((int)pFeatureGroup.GetProperty("Fill Color"));
                        pPolygon.PolygonFillcolor = pColor;
                    }
                    if (pFeatureGroup.IsClassified("Fill Opacity"))
                        pPolygon.PolygonFillOpacityClass = pFeatureGroup.GetClassification("Fill Opacity").ToString();
                    else
                    {
                        pPolygon.PolygonFillOpacity = (double)pFeatureGroup.GetProperty("Fill Opacity");
                    }
                    pPolygon.AltitMethod = (int)pFeatureGroup.GetProperty("Altitude Method");
                    #endregion
                    this.CurrentSymbol.PrePolygonSymbol = pPolygon;
                    break;
                default:
                    break;
            }
        }
        private void comboBoxValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //根据选择的字段加载相应的值并随机分配颜色，在datagridview中显示
                dataGridView1.Rows.Clear();
                ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer.SelectedIndex]);
                //Layer的Import Option要以ALL Features 方式导入，以Streaming方式导入要素数为0
                if (SlctdLyr.Streaming == true)
                    SlctdLyr.Streaming = false;
                IFeatureGroup61 SFeatureGroup = null;
                switch (SlctdLyr.GeometryType)
                {
                    case LayerGeometryType.LGT_POINT:
                        SFeatureGroup = SlctdLyr.FeatureGroups.Point;
                        if (this.CurrentSymbol.CurrentPointSymbol.PointType == "Other")
                            ((DataGridViewImageColumn)this.dataGridView1.Columns[0]).Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Polygon55b.png");
                        else
                            ((DataGridViewImageColumn)this.dataGridView1.Columns[0]).Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\" + this.CurrentSymbol.CurrentPointSymbol.PointType + "55b.png");

                        break;
                    case LayerGeometryType.LGT_POLYLINE:
                        SFeatureGroup = SlctdLyr.FeatureGroups.Polyline;
                        ((DataGridViewImageColumn)this.dataGridView1.Columns[0]).Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\" + this.CurrentSymbol.CurrentPolylineSymbol.PolylineType + "55.png");
                        break;
                    case LayerGeometryType.LGT_POLYGON:
                        SFeatureGroup = SlctdLyr.FeatureGroups.Polygon;
                        ((DataGridViewImageColumn)this.dataGridView1.Columns[0]).Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Polygon55b.png");
                        break;
                    default:
                        break;
                }
                #region
                IFeature61 SFeature = null;
                string AValue = "";
                List<string> Valuelist = new List<string>();
                Random Ra = new Random();
                int r = 0; int g = 0; int b = 0;

                //获取不重复值列表
                for (int i = 0; i < SFeatureGroup.Count; i++)
                {
                    SFeature = SFeatureGroup[i] as IFeature61;
                    AValue = SFeature.FeatureAttributes.GetFeatureAttribute(this.comboBoxValue.Text).Value;
                    bool RepeatOrNot = false;
                    //判断值是否重复，若重复则不需再添加入表中
                    if (Valuelist.Count == 0)
                    {
                        Valuelist.Add(AValue);
                        this.dataGridView1.Rows.Add();
                        this.dataGridView1.Rows[0].Cells[1].Value = AValue;
                    }
                    else
                    {
                        RepeatOrNot = IFRepeat(AValue, Valuelist);
                        if (!RepeatOrNot)
                        {
                            Valuelist.Add(AValue);
                            this.dataGridView1.Rows.Add();
                            this.dataGridView1.Rows[this.dataGridView1.RowCount - 1].Cells[1].Value = AValue;
                        }
                    }

                    if (!RepeatOrNot)
                    {
                        r = Ra.Next(0, 255); g = Ra.Next(0, 255); b = Ra.Next(0, 255);
                        this.dataGridView1.Rows[this.dataGridView1.RowCount - 1].Cells[0].Style.BackColor = Color.FromArgb(255, r, g, b);
                    }
                }
                dataGridView1.ClearSelection();
                #endregion
            }
            catch 
            {
                MessageBox.Show("操作遇到问题!如面板显示正常，可以忽略以继续");
            }
        }
        private void pictureBoxStartCLR3_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = true;
            cd.Color = this.pictureBoxStartCLR3.BackColor;
            cd.ShowDialog();
            this.pictureBoxStartCLR3.BackColor = cd.Color;
        }
        private void pictureBoxStartCLR3_BackColorChanged(object sender, EventArgs e)
        {
            try
            {
                //切换颜色，同时生成色带分配到单元格
                if (this.dataGridView1.Rows.Count > 0)
                {
                    if (this.dataGridView1.Rows.Count == 1)
                    {
                        this.dataGridView1.Rows[0].Cells[0].Style.BackColor = pictureBoxStartCLR3.BackColor;
                        this.dataGridView1.ClearSelection();
                    }
                    else
                    {
                        List<Color> Colorlist = new List<Color>();
                        Colorlist = ProduceColors(this.pictureBoxStartCLR3.BackColor, this.pictureBoxEndCLR3.BackColor, this.dataGridView1.Rows.Count);
                        for (int m = 0; m < this.dataGridView1.Rows.Count; m++)
                        {
                            this.dataGridView1.Rows[m].Cells[0].Style.BackColor = Colorlist[m];
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("切换颜色遇到问题！");
            }
        }
        private void pictureBoxEndCLR3_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = true;
            cd.Color = this.pictureBoxEndCLR3.BackColor;
            cd.ShowDialog();
            this.pictureBoxEndCLR3.BackColor = cd.Color;
        }
        private void pictureBoxEndCLR3_BackColorChanged(object sender, EventArgs e)
        {
            try
            {
                //切换颜色，同时生成色带分配到单元格
                //获取颜色分段方案
                if (this.dataGridView1.Rows.Count > 0)
                {
                    if (this.dataGridView1.Rows.Count == 1)
                    {
                        this.dataGridView1.Rows[0].Cells[0].Style.BackColor = pictureBoxEndCLR3.BackColor;
                        this.dataGridView1.ClearSelection();
                    }
                    else
                    {
                        List<Color> Colorlist = new List<Color>();
                        Colorlist = ProduceColors(this.pictureBoxStartCLR3.BackColor, this.pictureBoxEndCLR3.BackColor, this.dataGridView1.Rows.Count);
                        for (int m = 0; m < this.dataGridView1.Rows.Count; m++)
                        {
                            this.dataGridView1.Rows[m].Cells[0].Style.BackColor = Colorlist[m];

                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("切换颜色遇到问题！");
            }
        }
        private void simpleBtnSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                if (ShpLyrID.Count > 0)
                {
                    if (this.comboBoxLayer.SelectedIndex >= 0)
                    {    //根据图层类型弹出相应符号化
                        ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer.SelectedIndex]);
                        switch (SlctdLyr.GeometryType)
                        {
                            case LayerGeometryType.LGT_POINT:
                                if (this.myPointSymbol == null)
                                {
                                    myPointSymbol = new FrmPointSymbol(this);
                                    Point p = new Point(Control.MousePosition.X - myPointSymbol.Width, Control.MousePosition.Y);
                                    myPointSymbol.Location = p;
                                    myPointSymbol.Show();
                                }
                                else
                                    this.myPointSymbol.Focus();
                                break;
                            case LayerGeometryType.LGT_POLYLINE:
                                if (this.myPolylineSymbol == null)
                                {
                                    myPolylineSymbol = new FrmPolylineSymbol(this);
                                    Point p1 = new Point(Control.MousePosition.X - myPolylineSymbol.Width, Control.MousePosition.Y);
                                    myPolylineSymbol.Location = p1;
                                    myPolylineSymbol.Show();
                                }
                                else
                                    this.myPolylineSymbol.Focus();
                                break;
                            case LayerGeometryType.LGT_POLYGON:
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("窗口初始化遇到问题!");
            }
        }
        private bool IFRepeat(string value, List<string> valuelist)
        {
            bool result = false;
            if (valuelist.Count > 0)
            {
                for (int i = 0; i < valuelist.Count; i++)
                {
                    if (value == valuelist[i])
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //点击‘颜色‘列单元格弹出颜色选择对话框,由用户选择颜色
            if (e.ColumnIndex == 0)
            {
                ColorDialog cd = new ColorDialog();
                cd.AnyColor = true;
                cd.Color = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor;
                cd.ShowDialog();
                this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = cd.Color;
                this.dataGridView1.ClearSelection();

            }
        }
        private void simpleButtonApply_Click(object sender, EventArgs e)
        {
            try
            {
                //将用户自主选择的颜色应用到地图上
                if (this.comboBoxLayer.Items.Count == 0)
                    MessageBox.Show("请先加载图层!");
                else
                {
                    if (this.comboBoxValue.Items.Count == 0)
                        MessageBox.Show("请先选择图层!");
                    else
                    {
                        if (dataGridView1.Rows.Count == 0)
                            MessageBox.Show("请先选择字段!");
                        else
                        {
                            CreateSimpleTheme();
                            IfDisplayOnType = true;

                        }
                    }
                }
            }
            catch 
            {
                MessageBox.Show("生成单值专题遇到问题！");
            }
        }
        private void CreateSimpleTheme()
        {
            ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer.SelectedIndex]);

            IFeatureGroup61 SFeatureGroup = null;
            switch (SlctdLyr.GeometryType)
            {
                case LayerGeometryType.LGT_POINT:
                    SFeatureGroup = SlctdLyr.FeatureGroups.Point;
                    #region
                    //修改Type、Altitude Method、Number of sizes、Radius X、Fill Color属性
                    switch (this.CurrentSymbol.CurrentPointSymbol.PointType)
                    {
                        case "Circle":
                            SFeatureGroup.DisplayAs = ObjectTypeCode.OT_CIRCLE;
                            SFeatureGroup.SetProperty("Radius X", this.CurrentSymbol.CurrentPointSymbol.PointSize);
                            break;
                        case "Triangle":
                            SFeatureGroup.DisplayAs = ObjectTypeCode.OT_REGULAR_POLYGON;
                            SFeatureGroup.SetProperty("Number of sides", 3);
                            SFeatureGroup.SetProperty("Radius X", this.CurrentSymbol.CurrentPointSymbol.PointSize);
                            break;
                        case "Rectangle":
                            SFeatureGroup.DisplayAs = ObjectTypeCode.OT_RECTANGLE;
                            SFeatureGroup.SetProperty("Length", this.CurrentSymbol.CurrentPointSymbol.PointSize);
                            SFeatureGroup.SetProperty("Width", this.CurrentSymbol.CurrentPointSymbol.PointSize);
                            break;
                        case "Pentagon":
                            SFeatureGroup.DisplayAs = ObjectTypeCode.OT_REGULAR_POLYGON;
                            SFeatureGroup.SetProperty("Number of sides", 5);
                            SFeatureGroup.SetProperty("Radius X", this.CurrentSymbol.CurrentPointSymbol.PointSize);
                            break;
                        case "Hexagon":
                            SFeatureGroup.DisplayAs = ObjectTypeCode.OT_REGULAR_POLYGON;
                            SFeatureGroup.SetProperty("Number of sides", 6);
                            SFeatureGroup.SetProperty("Radius X", this.CurrentSymbol.CurrentPointSymbol.PointSize);
                            break;
                        case "Arrow":
                            SFeatureGroup.DisplayAs = ObjectTypeCode.OT_ARROW;
                            SFeatureGroup.SetProperty("Length", this.CurrentSymbol.CurrentPointSymbol.PointSize);
                            break;
                        default:
                            break;
                    }
                    #endregion
                    break;
                case LayerGeometryType.LGT_POLYLINE:
                    SFeatureGroup = SlctdLyr.FeatureGroups.Polyline;
                    #region
                    //修改Line Pattern、Altitude Method、Line Back Color、Line Back Opacity、Line Width、Line Color属性,给单元格覆盖相应图片
                    switch (this.CurrentSymbol.CurrentPolylineSymbol.PolylineType)
                    {
                        case "Solidline":
                            SFeatureGroup.SetProperty("Line Pattern", -1);
                            break;
                        case "Dottedline":
                            SFeatureGroup.SetProperty("Line Pattern", -1044481);
                            break;
                        case "Dottedline2":
                            SFeatureGroup.SetProperty("Line Pattern", -16776961);
                            break;
                        case "Dottedline3":
                            SFeatureGroup.SetProperty("Line Pattern", -267390961);
                            break;
                        case "Dottedline4":
                            SFeatureGroup.SetProperty("Line Pattern", -1010580541);
                            break;
                        case "Dottedline5":
                            SFeatureGroup.SetProperty("Line Pattern", -1717986919);
                            break;
                        case "Dottedline6":
                            SFeatureGroup.SetProperty("Line Pattern", -1431655766);
                            break;
                        case "Dottedline7":
                            SFeatureGroup.SetProperty("Line Pattern", -16678657);
                            break;
                        case "Dottedline8":
                            SFeatureGroup.SetProperty("Line Pattern", -15978241);
                            break;
                    }
                    SFeatureGroup.SetProperty("Line Width", this.CurrentSymbol.CurrentPolylineSymbol.PolylineWidth);
                    if (SFeatureGroup.IsClassified("Line Back Color"))
                    {
                        SFeatureGroup.SetClassification("Line Back Color", this.CurrentSymbol.CurrentPolylineSymbol.PolylineBackColorClass);
                    }
                    else
                    {
                        SFeatureGroup.SetProperty("Line Back Color", Program.sgworld.Creator.CreateColor(this.CurrentSymbol.CurrentPolylineSymbol.PolylineBackColor.R,
                                                                                                         this.CurrentSymbol.CurrentPolylineSymbol.PolylineBackColor.G,
                                                                                                         this.CurrentSymbol.CurrentPolylineSymbol.PolylineBackColor.B,
                                                                                                         this.CurrentSymbol.CurrentPolylineSymbol.PolylineBackColor.A));
                    }
                    SFeatureGroup.SetProperty("Line Back Opacity", this.CurrentSymbol.CurrentPolylineSymbol.PolylineBackOpacity);

                    #endregion
                    break;
                case LayerGeometryType.LGT_POLYGON:
                    SFeatureGroup = SlctdLyr.FeatureGroups.Polygon;
                    break;
                default:
                    break;
            }
            SFeatureGroup.SetProperty("Altitude Method", 2);

            string FillcolorXML = "<Classification FuncType=\"0\">";
            IColor61 IFillcolor = null;
            Color CellBackcolor = Color.Empty;
            #region
            if (this.dataGridView1.Rows.Count == 1)
            {
                CellBackcolor = this.dataGridView1.Rows[0].Cells[0].Style.BackColor;
                IFillcolor = Program.sgworld.Creator.CreateColor(CellBackcolor.R, CellBackcolor.G, CellBackcolor.B, CellBackcolor.A);
                if (SlctdLyr.GeometryType == LayerGeometryType.LGT_POLYLINE)
                {
                    if (SFeatureGroup.GetProperty("Line Opacity").ToString() != "1")
                        SFeatureGroup.SetProperty("Line Opacity", 1);
                    SFeatureGroup.SetProperty("Line Color", IFillcolor);
                }
                else
                {
                    //if (SFeatureGroup.DisplayAs == ObjectTypeCode.OT_LABEL)
                    //{
                    //    SFeatureGroup.SetProperty("Text Color", IFillcolor);
                    //}
                    //else
                    //{
                    //    if (SFeatureGroup.DisplayAs == ObjectTypeCode.OT_IMAGE_LABEL)
                    //    {
                    //        if (SFeatureGroup.GetProperty("Image Opacity").ToString() != "1")
                    //            SFeatureGroup.SetProperty("Image Opacity", 1);
                    //        SFeatureGroup.SetProperty("Image Color", IFillcolor);
                    //    }
                    //    else
                    //    {

                    //        if (SFeatureGroup.GetProperty("Fill Opacity").ToString() != "1")
                    //            SFeatureGroup.SetProperty("Fill Opacity", 1);
                    //        SFeatureGroup.SetProperty("Fill Color", IFillcolor);
                    //    }
                    //}
                    switch (SFeatureGroup.DisplayAs)
                    {
                        case ObjectTypeCode.OT_LABEL:
                            SFeatureGroup.SetProperty("Text Color", IFillcolor);
                            break;
                        case ObjectTypeCode.OT_IMAGE_LABEL:
                            if (SFeatureGroup.GetProperty("Image Opacity").ToString() != "1")
                                SFeatureGroup.SetProperty("Image Opacity", 1);
                            SFeatureGroup.SetProperty("Image Color", IFillcolor);
                            break;
                        case ObjectTypeCode.OT_MODEL:
                            if (SFeatureGroup.GetProperty("Tint Opacity").ToString() != "1")
                                SFeatureGroup.SetProperty("Tint Opacity", 1);
                            SFeatureGroup.SetProperty("Tint Color", IFillcolor);
                            break;
                        default:
                            if (SFeatureGroup.GetProperty("Fill Opacity").ToString() != "1")
                                SFeatureGroup.SetProperty("Fill Opacity", 1);
                            SFeatureGroup.SetProperty("Fill Color", IFillcolor);
                            break;
                    }
                }

            }
            else
            {
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    CellBackcolor = this.dataGridView1.Rows[i].Cells[0].Style.BackColor;
                    IFillcolor = Program.sgworld.Creator.CreateColor(CellBackcolor.R, CellBackcolor.G, CellBackcolor.B, CellBackcolor.A);

                    if (SlctdLyr.DataSourceInfo.Attributes.get_Attribute(this.comboBoxValue.SelectedIndex).Type == AttributeTypeCode.AT_TEXT)
                        FillcolorXML += "<Class><Condition>&lt;\"[" + this.comboBoxValue.Text + "]\"=\"" + this.dataGridView1.Rows[i].Cells[1].Value.ToString()
                                      + "\"&gt;</Condition><Value>" + IFillcolor.ToBGRColor().ToString() + "</Value></Class>";
                    else
                    {
                        if (SlctdLyr.DataSourceInfo.Attributes.get_Attribute(this.comboBoxValue.SelectedIndex).Type == AttributeTypeCode.AT_INTEGER |
                           SlctdLyr.DataSourceInfo.Attributes.get_Attribute(this.comboBoxValue.SelectedIndex).Type == AttributeTypeCode.AT_DOUBLE)
                            FillcolorXML += "<Class><Condition>&lt;[" + this.comboBoxValue.Text + "]=" + this.dataGridView1.Rows[i].Cells[1].Value.ToString()
                                          + "&gt;</Condition><Value>" + IFillcolor.ToBGRColor().ToString() + "</Value></Class>";
                    }
                }
                FillcolorXML += "<DefaultValue>6579300</DefaultValue></Classification>";

                if (SlctdLyr.GeometryType == LayerGeometryType.LGT_POLYLINE)
                {
                    if (SFeatureGroup.GetProperty("Line Opacity").ToString() != "1")
                        SFeatureGroup.SetProperty("Line Opacity", 1);
                    SFeatureGroup.SetClassification("Line Color", FillcolorXML);
                }
                else
                {
                    //if (SFeatureGroup.DisplayAs == ObjectTypeCode.OT_LABEL)
                    //{
                    //    SFeatureGroup.SetProperty("Text Color", FillcolorXML);
                    //}
                    //else
                    //{
                    //    if (SFeatureGroup.DisplayAs == ObjectTypeCode.OT_IMAGE_LABEL)
                    //    {
                    //        if (SFeatureGroup.GetProperty("Image Opacity").ToString() != "1")
                    //            SFeatureGroup.SetProperty("Image Opacity", 1);
                    //        SFeatureGroup.SetProperty("Image Color", FillcolorXML);
                    //    }
                    //    else
                    //    {
                    //        if (SFeatureGroup.GetProperty("Fill Opacity").ToString() != "1")
                    //            SFeatureGroup.SetProperty("Fill Opacity", 1);
                    //        SFeatureGroup.SetClassification("Fill Color", FillcolorXML);
                    //    }
                    //}
                    switch (SFeatureGroup.DisplayAs)
                    {
                        case ObjectTypeCode.OT_LABEL:
                            SFeatureGroup.SetClassification("Text Color", FillcolorXML);
                            break;
                        case ObjectTypeCode.OT_IMAGE_LABEL:
                            if (SFeatureGroup.GetProperty("Image Opacity").ToString() != "1")
                                SFeatureGroup.SetProperty("Image Opacity", 1);
                            SFeatureGroup.SetClassification("Image Color", FillcolorXML);
                            break;
                        case ObjectTypeCode.OT_MODEL:
                            if (SFeatureGroup.GetProperty("Tint Opacity").ToString() != "1")
                                SFeatureGroup.SetProperty("Tint Opacity", 1);
                            SFeatureGroup.SetClassification("Tint Color", FillcolorXML);
                            break;
                        default:
                            if (SFeatureGroup.GetProperty("Fill Opacity").ToString() != "1")
                                SFeatureGroup.SetProperty("Fill Opacity", 1);
                            SFeatureGroup.SetClassification("Fill Color", FillcolorXML);
                            break;
                    }
                }
            }
            #endregion
        }
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            try
            {
                //保存对属性图层的改变
                if (this.comboBoxLayer.SelectedIndex >= 0)
                {
                    ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer.SelectedIndex]);
                    SlctdLyr.Save();
                    Program.sgworld.Project.Save();
                    SaveOrNot_Simple = true;
                    MessageBox.Show("保存成功！");
                }
            }
            catch
            {
                MessageBox.Show("保存遇到问题!");
            }
        }
        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SaveOrNot_Simple)
                {
                    if (this.comboBoxLayer.Text != "" & this.comboBoxLayer.Text != "请选择图层")
                    {
                        ////保存后关闭？
                        //DialogResult dr = MessageBox.Show("是否保存当前专题？", "关闭提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        //if (dr == System.Windows.Forms.DialogResult.Yes)
                        //{
                        //    Program.sgworld.Project.Save();
                        //    SaveOrNot_Simple = true;
                        //    CurrentThemeType = 0;
                        //    dockPanel5_SimpleTheme.Close();
                        //}
                        //else
                        //{
                        //    if (dr == System.Windows.Forms.DialogResult.No)
                        //    {
                        //还原图层
                        ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer.SelectedIndex]);
                        ReturnToOriginal(SlctdLyr);
                        CurrentThemeType = 0;
                        IfDisplayOnType = false;
                        //dockPanel5_SimpleTheme.Close();
                        //    }
                        //}
                    }
                }
            }
            catch
            {
                MessageBox.Show("还原遇到问题!");
            }
        }
        private void ReturnToOriginal(ILayer61 SlctdLyr)
        {
            IFeatureGroup61 SFeatureGroup = null;
            switch (SlctdLyr.GeometryType)
            {
                case LayerGeometryType.LGT_POINT:
                    SFeatureGroup = SlctdLyr.FeatureGroups.Point;
                    PointSymbol pPoint = this.CurrentSymbol.PrePointSymbol;
                    switch (pPoint.PointType)
                    {
                        #region
                        case "Circle":
                            SFeatureGroup.DisplayAs = ObjectTypeCode.OT_CIRCLE;
                            if (pPoint.PointSizeClass == null)
                                SFeatureGroup.SetProperty("Radius X", pPoint.PointSize);
                            else
                                SFeatureGroup.SetClassification("Radius X", pPoint.PointSizeClass);
                            if (pPoint.NumOfSidesClass == null)
                                SFeatureGroup.SetProperty("Number of sides", pPoint.NumOfSides);
                            else
                                SFeatureGroup.SetClassification("Number of sides", pPoint.NumOfSidesClass);
                            if (pPoint.PointFillcolorClass == null)
                            {
                                Color pColor = pPoint.PointFillcolor;
                                IColor61 IFillcolor = Program.sgworld.Creator.CreateColor(pColor.R, pColor.G, pColor.B, pColor.A);
                                SFeatureGroup.SetProperty("Fill Color", pColor);
                            }
                            else
                                SFeatureGroup.SetClassification("Fill Color", pPoint.PointFillcolorClass);
                            if (pPoint.PointFillOpacityClass == null)
                                SFeatureGroup.SetProperty("Fill Opacity", pPoint.PointFillOpacity);
                            else
                                SFeatureGroup.SetClassification("Fill Opacity", pPoint.PointFillOpacityClass);
                            SFeatureGroup.SetProperty("Altitude Method", pPoint.AltitMethod);
                            break;
                        case "Triangle":
                            SFeatureGroup.DisplayAs = ObjectTypeCode.OT_REGULAR_POLYGON;
                            if (pPoint.PointSizeClass == null)
                                SFeatureGroup.SetProperty("Radius X", pPoint.PointSize);
                            else
                                SFeatureGroup.SetClassification("Radius X", pPoint.PointSizeClass);
                            SFeatureGroup.SetProperty("Number of sides", 3);
                            if (pPoint.PointFillcolorClass == null)
                            {
                                Color pColor = pPoint.PointFillcolor;
                                IColor61 IFillcolor = Program.sgworld.Creator.CreateColor(pColor.R, pColor.G, pColor.B, pColor.A);
                                SFeatureGroup.SetProperty("Fill Color", pColor);
                            }
                            else
                                SFeatureGroup.SetClassification("Fill Color", pPoint.PointFillcolorClass);
                            if (pPoint.PointFillOpacityClass == null)
                                SFeatureGroup.SetProperty("Fill Opacity", pPoint.PointFillOpacity);
                            else
                                SFeatureGroup.SetClassification("Fill Opacity", pPoint.PointFillOpacityClass);
                            SFeatureGroup.SetProperty("Altitude Method", pPoint.AltitMethod);
                            break;
                        case "Rectangle":
                            SFeatureGroup.DisplayAs = ObjectTypeCode.OT_RECTANGLE;
                            if (pPoint.PointSizeClass == null)
                                SFeatureGroup.SetProperty("Length", pPoint.PointSize);
                            else
                                SFeatureGroup.SetClassification("Length", pPoint.PointSizeClass);
                            if (pPoint.PointSizeClass2 == null)
                                SFeatureGroup.SetProperty("Width", pPoint.PointSize2);
                            else
                                SFeatureGroup.SetClassification("Width", pPoint.PointSizeClass2);
                            if (pPoint.PointFillcolorClass == null)
                            {
                                Color pColor = pPoint.PointFillcolor;
                                IColor61 IFillcolor = Program.sgworld.Creator.CreateColor(pColor.R, pColor.G, pColor.B, pColor.A);
                                SFeatureGroup.SetProperty("Fill Color", pColor);
                            }
                            else
                                SFeatureGroup.SetClassification("Fill Color", pPoint.PointFillcolorClass);
                            if (pPoint.PointFillOpacityClass == null)
                                SFeatureGroup.SetProperty("Fill Opacity", pPoint.PointFillOpacity);
                            else
                                SFeatureGroup.SetClassification("Fill Opacity", pPoint.PointFillOpacityClass);
                            SFeatureGroup.SetProperty("Altitude Method", pPoint.AltitMethod);
                            break;
                        case "Pentagon":
                            SFeatureGroup.DisplayAs = ObjectTypeCode.OT_REGULAR_POLYGON;
                            if (pPoint.PointSizeClass == null)
                                SFeatureGroup.SetProperty("Radius X", pPoint.PointSize);
                            else
                                SFeatureGroup.SetClassification("Radius X", pPoint.PointSizeClass);
                            SFeatureGroup.SetProperty("Number of sides", 5);
                            if (pPoint.PointFillcolorClass == null)
                            {
                                Color pColor = pPoint.PointFillcolor;
                                IColor61 IFillcolor = Program.sgworld.Creator.CreateColor(pColor.R, pColor.G, pColor.B, pColor.A);
                                SFeatureGroup.SetProperty("Fill Color", pColor);
                            }
                            else
                                SFeatureGroup.SetClassification("Fill Color", pPoint.PointFillcolorClass);
                            if (pPoint.PointFillOpacityClass == null)
                                SFeatureGroup.SetProperty("Fill Opacity", pPoint.PointFillOpacity);
                            else
                                SFeatureGroup.SetClassification("Fill Opacity", pPoint.PointFillOpacityClass);
                            SFeatureGroup.SetProperty("Altitude Method", pPoint.AltitMethod);
                            break;
                        case "Hexagon":
                            SFeatureGroup.DisplayAs = ObjectTypeCode.OT_REGULAR_POLYGON;
                            if (pPoint.PointSizeClass == null)
                                SFeatureGroup.SetProperty("Radius X", pPoint.PointSize);
                            else
                                SFeatureGroup.SetClassification("Radius X", pPoint.PointSizeClass);
                            SFeatureGroup.SetProperty("Number of sides", 6);
                            if (pPoint.PointFillcolorClass == null)
                            {
                                Color pColor = pPoint.PointFillcolor;
                                IColor61 IFillcolor = Program.sgworld.Creator.CreateColor(pColor.R, pColor.G, pColor.B, pColor.A);
                                SFeatureGroup.SetProperty("Fill Color", pColor);
                            }
                            else
                                SFeatureGroup.SetClassification("Fill Color", pPoint.PointFillcolorClass);
                            if (pPoint.PointFillOpacityClass == null)
                                SFeatureGroup.SetProperty("Fill Opacity", pPoint.PointFillOpacity);
                            else
                                SFeatureGroup.SetClassification("Fill Opacity", pPoint.PointFillOpacityClass);
                            SFeatureGroup.SetProperty("Altitude Method", pPoint.AltitMethod);
                            break;
                        case "Arrow":
                            SFeatureGroup.DisplayAs = ObjectTypeCode.OT_ARROW;
                            if (pPoint.PointSizeClass == null)
                                SFeatureGroup.SetProperty("Length", pPoint.PointSize);
                            else
                                SFeatureGroup.SetClassification("Length", pPoint.PointSizeClass);
                            if (pPoint.PointFillcolorClass == null)
                            {
                                Color pColor = pPoint.PointFillcolor;
                                IColor61 IFillcolor = Program.sgworld.Creator.CreateColor(pColor.R, pColor.G, pColor.B, pColor.A);
                                SFeatureGroup.SetProperty("Fill Color", pColor);
                            }
                            else
                                SFeatureGroup.SetClassification("Fill Color", pPoint.PointFillcolorClass);
                            if (pPoint.PointFillOpacityClass == null)
                                SFeatureGroup.SetProperty("Fill Opacity", pPoint.PointFillOpacity);
                            else
                                SFeatureGroup.SetClassification("Fill Opacity", pPoint.PointFillOpacityClass);
                            SFeatureGroup.SetProperty("Altitude Method", pPoint.AltitMethod);
                            break;
                        default:
                            //图像标签，文本标签及其他
                            #region
                            switch (SFeatureGroup.DisplayAs)
                            {
                                case ObjectTypeCode.OT_LABEL:
                                    if (pPoint.PointFillcolorClass == null)
                                    {
                                        Color pColor = pPoint.PointFillcolor;
                                        IColor61 IFillcolor = Program.sgworld.Creator.CreateColor(pColor.R, pColor.G, pColor.B, pColor.A);
                                        SFeatureGroup.SetProperty("Text Color", pColor);
                                    }
                                    else
                                        SFeatureGroup.SetClassification("Text Color", pPoint.PointFillcolorClass);
                                    break;
                                case ObjectTypeCode.OT_IMAGE_LABEL:
                                    if (pPoint.PointFillcolorClass == null)
                                    {
                                        Color pColor = pPoint.PointFillcolor;
                                        IColor61 IFillcolor = Program.sgworld.Creator.CreateColor(pColor.R, pColor.G, pColor.B, pColor.A);
                                        SFeatureGroup.SetProperty("Image Color", pColor);
                                    }
                                    else
                                        SFeatureGroup.SetClassification("Image Color", pPoint.PointFillcolorClass);
                                    if (pPoint.PointFillOpacityClass == null)
                                        SFeatureGroup.SetProperty("Image Opacity", pPoint.PointFillOpacity);
                                    else
                                        SFeatureGroup.SetClassification("Image Opacity", pPoint.PointFillOpacityClass);
                                    break;
                                case ObjectTypeCode.OT_MODEL:
                                    if (pPoint.PointFillcolorClass == null)
                                    {
                                        Color pColor = pPoint.PointFillcolor;
                                        IColor61 IFillcolor = Program.sgworld.Creator.CreateColor(pColor.R, pColor.G, pColor.B, pColor.A);
                                        SFeatureGroup.SetProperty("Tint Color", pColor);
                                    }
                                    else
                                        SFeatureGroup.SetClassification("Tint Color", pPoint.PointFillcolorClass);
                                    if (pPoint.PointFillOpacityClass == null)
                                        SFeatureGroup.SetProperty("Tint Opacity", pPoint.PointFillOpacity);
                                    else
                                        SFeatureGroup.SetClassification("Tint Opacity", pPoint.PointFillOpacityClass);
                                    break;
                                default:
                                    if (pPoint.PointFillcolorClass == null)
                                    {
                                        Color pColor = pPoint.PointFillcolor;
                                        IColor61 IFillcolor = Program.sgworld.Creator.CreateColor(pColor.R, pColor.G, pColor.B, pColor.A);
                                        SFeatureGroup.SetProperty("Fill Color", pColor);
                                    }
                                    else
                                        SFeatureGroup.SetClassification("Fill Color", pPoint.PointFillcolorClass);
                                    if (pPoint.PointFillOpacityClass == null)
                                        SFeatureGroup.SetProperty("Fill Opacity", pPoint.PointFillOpacity);
                                    else
                                        SFeatureGroup.SetClassification("Fill Opacity", pPoint.PointFillOpacityClass);
                                    break;
                            }
                            SFeatureGroup.SetProperty("Altitude Method", pPoint.AltitMethod);
                            #endregion
                            break;
                        #endregion
                    }
                    break;
                case LayerGeometryType.LGT_POLYLINE:
                    SFeatureGroup = SlctdLyr.FeatureGroups.Polyline;
                    PolylineSymbol pPolyline = this.CurrentSymbol.PrePolylineSymbol;
                    #region
                    switch (pPolyline.PolylineType)
                    {
                        case "Solidline":
                            SFeatureGroup.SetProperty("Line Pattern", -1);
                            break;
                        case "Dottedline":
                            SFeatureGroup.SetProperty("Line Pattern", -1044481);
                            break;
                        case "Dottedline2":
                            SFeatureGroup.SetProperty("Line Pattern", -16776961);
                            break;
                        case "Dottedline3":
                            SFeatureGroup.SetProperty("Line Pattern", -267390961);
                            break;
                        case "Dottedline4":
                            SFeatureGroup.SetProperty("Line Pattern", -1010580541);
                            break;
                        case "Dottedline5":
                            SFeatureGroup.SetProperty("Line Pattern", -1717986919);
                            break;
                        case "Dottedline6":
                            SFeatureGroup.SetProperty("Line Pattern", -1431655766);
                            break;
                        case "Dottedline7":
                            SFeatureGroup.SetProperty("Line Pattern", -16678657);
                            break;
                        case "Dottedline8":
                            SFeatureGroup.SetProperty("Line Pattern", -15978241);
                            break;
                    }
                    if (pPolyline.PolylineWidthClass == null)
                        SFeatureGroup.SetProperty("Line Width", pPolyline.PolylineWidth);
                    else
                        SFeatureGroup.SetClassification("Line Width", pPolyline.PolylineWidthClass);

                    if (pPolyline.PolylineColorClass == null)
                    {
                        Color pColor = pPolyline.PolylineColor;
                        IColor61 IFillcolor = Program.sgworld.Creator.CreateColor(pColor.R, pColor.G, pColor.B, pColor.A);
                        SFeatureGroup.SetProperty("Line Color", pColor);
                    }
                    else
                        SFeatureGroup.SetClassification("Line Color", pPolyline.PolylineColorClass);
                    if (pPolyline.PolylineOpacityClass == null)
                        SFeatureGroup.SetProperty("Line Opacity", pPolyline.PolylineOpacity);
                    else
                        SFeatureGroup.SetClassification("Line Opacity", pPolyline.PolylineOpacityClass);
                    if (pPolyline.PolylineBackColorClass == null)
                    {
                        Color pColor = pPolyline.PolylineBackColor;
                        IColor61 IFillcolor = Program.sgworld.Creator.CreateColor(pColor.R, pColor.G, pColor.B, pColor.A);
                        SFeatureGroup.SetProperty("Line Back Color", pColor);
                    }
                    else
                        SFeatureGroup.SetClassification("Line Back Color", pPolyline.PolylineBackColorClass);
                    if (pPolyline.PolylineBackOpacityClass == null)
                        SFeatureGroup.SetProperty("Line Back Opacity", pPolyline.PolylineBackOpacity);
                    else
                        SFeatureGroup.SetClassification("Line Back Opacity", pPolyline.PolylineBackOpacityClass);
                    SFeatureGroup.SetProperty("Altitude Method", pPolyline.AltitMethod);
                    #endregion
                    break;
                case LayerGeometryType.LGT_POLYGON:
                    SFeatureGroup = SlctdLyr.FeatureGroups.Polygon;
                    PolygonSymbol pPolygon = this.CurrentSymbol.PrePolygonSymbol;
                    #region
                    if (pPolygon.PolygonFillcolorClass == null)
                    {
                        Color pColor = pPolygon.PolygonFillcolor;
                        IColor61 IFillcolor = Program.sgworld.Creator.CreateColor(pColor.R, pColor.G, pColor.B, pColor.A);
                        SFeatureGroup.SetProperty("Fill Color", pColor);

                    }
                    else
                        SFeatureGroup.SetClassification("Fill Color", pPolygon.PolygonFillcolorClass);
                    if (pPolygon.PolygonFillOpacityClass == null)
                        SFeatureGroup.SetProperty("Fill Opacity", pPolygon.PolygonFillOpacity);
                    else
                        SFeatureGroup.SetClassification("Fill Opacity", pPolygon.PolygonFillOpacityClass);
                    SFeatureGroup.SetProperty("Altitude Method", pPolygon.AltitMethod);
                    #endregion
                    break;
            }

        }
        private bool IfDisplayOnType = false;
        private void dockPanel5_SimpleTheme_ClosingPanel(object sender, DevExpress.XtraBars.Docking.DockPanelCancelEventArgs e)
        {
            try
            {
                if (CurrentThemeType != 0)
                {
                    if (!SaveOrNot_Simple)
                    {
                        if (this.comboBoxLayer.Text != "" & this.comboBoxLayer.Text != "请选择图层")
                        {
                            if (IfDisplayOnType)
                            {
                                //保存后关闭？
                                DialogResult dr = MessageBox.Show("是否保留分类显示？", "关闭提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                if (dr == System.Windows.Forms.DialogResult.Yes)
                                {
                                    Program.sgworld.Project.Save();
                                    SaveOrNot_Simple = true;
                                    CurrentThemeType = 0;
                                }
                                else
                                {
                                    if (dr == System.Windows.Forms.DialogResult.No)
                                    {
                                        //还原图层
                                        ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer.SelectedIndex]);
                                        ReturnToOriginal(SlctdLyr);
                                        CurrentThemeType = 0;
                                    }
                                    else
                                        e.Cancel = true;
                                }
                            }
                        }
                    }

                }
            }
            catch
            {
                MessageBox.Show("关闭遇到问题!");
            }
        }
        #endregion

        #region 分段专题
        public FrmPointSymbol myPointBreakSymbol = null;
        public FrmPolylineSymbol myPolylineBreakSymbol = null;
        private List<int> FieldIndex = new List<int>();
        private bool SaveOrNot_Class = false;
        //private void ClassBreaksTheme_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        //打开面板情况下，重新加入一个文件再点击弹出超出索引错误
        //        if (CurrentThemeType != 0 & CurrentThemeType != 2)
        //        {
        //            //关闭当前面板
        //            DialogResult dr = MessageBox.Show("关闭当前专题面板？", "关闭提示", MessageBoxButtons.OKCancel);
        //            if (dr == DialogResult.OK)
        //            {
        //                switch (CurrentThemeType)
        //                {
        //                    case 1:
        //                        this.dockPanel5_SimpleTheme.Close();
        //                        CurrentThemeType = 0;
        //                        break;
        //                    case 3:
        //                        this.dockPanel5_LabelTheme.Close();
        //                        CurrentThemeType = 0;
        //                        break;
        //                    case 4:
        //                        this.dockPanel5_StatisticTheme.Close();
        //                        CurrentThemeType = 0;
        //                        break;
        //                    default:
        //                        break;
        //                }
        //            }
        //        }
        //        if (CurrentThemeType == 2 || CurrentThemeType == 0)
        //        {
        //            //遍历信息树，加载shapefile图层名
        //            int current = Program.sgworld.ProjectTree.GetNextItem(0, ItemCode.ROOT);
        //            if (current > 0)
        //            {
        //                //清除分段面板已有数据，恢复默认设置
        //                this.comboBoxLayer2.Items.Clear();
        //                this.comboBoxValue2.Items.Clear();
        //                this.comboBoxValue2.Text = "";
        //                this.comboBoxBreakMethods.Text = "等距分段";
        //                this.comboBoxBreakNums.Text = "5";
        //                this.pictureBoxStartCLR.BackColor = Color.PaleGreen;
        //                this.pictureBoxEndCLR.BackColor = Color.DarkGreen;
        //                this.txtBoxSizeMin.Text = "0";
        //                this.txtBoxSizeMax.Text = "100";
        //                this.simpleBtnBreakSymbol.Image = null;
        //                this.pictureBoxBreakCLR.BackColor = Color.PaleGreen;
        //                this.tabControl4.TabPages[0].Text = "";
        //                this.tabControl4.TabPages[0].ImageIndex = -1;
        //                this.dataGridView2.Rows.Clear();
        //                this.comboBoxClassBreakType.Text ="分级色彩";
        //                if(this.comboBoxClassBreakType.Items.Count == 2)
        //                {
        //                    this.comboBoxClassBreakType.Items.Remove("分级符号");
        //                }
                        

        //                ShpLyrID.Clear();
        //                ScanTree(current, 2);
        //                if (ShpLyrID.Count > 0)
        //                {
        //                    CurrentThemeType = 2;
        //                    this.comboBoxLayer2.Text = "请选择图层!";
        //                    //显示分段专题面板
        //                    this.dockPanel5_ClassBreak.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        //                }
        //                else
        //                    MessageBox.Show("请先添加矢量图层!");
        //            }
        //            else
        //                MessageBox.Show("请先添加图层");
        //        }
        //    }
        //    catch
        //    {
        //        MessageBox.Show("面板初始化遇到问题");
        //    }


        //}
        private void comboBoxLayer2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //根据选择的图层加载相应的属性字段
            SaveOrNot_Class = false;
            this.simpleBtnBreakSymbol.Image = null;
            this.comboBoxValue2.Items.Clear();
            this.comboBoxValue2.Text = "";
            this.comboBoxBreakMethods.Text = "等距分段";
            this.comboBoxBreakNums.Text = "5";
            this.pictureBoxStartCLR.BackColor = Color.PaleGreen;
            this.pictureBoxEndCLR.BackColor = Color.DarkGreen;
            this.txtBoxSizeMin.Text = "0";
            this.txtBoxSizeMax.Text = "100";
            this.pictureBoxBreakCLR.BackColor = Color.PaleGreen;
            this.comboBoxClassBreakType.Text = "分级色彩";
            try
            {
                if (this.comboBoxClassBreakType.Items.Count == 2)
                {
                    this.comboBoxClassBreakType.Items.Remove("分级符号");
                }
                this.dataGridView2.Rows.Clear();
                bool SurportOrNot = true;
                if (ShpLyrID.Count > 0)
                {
                    ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer2.SelectedIndex]);
                    InitialCurrentSymbol(SlctdLyr);
                    switch (SlctdLyr.GeometryType)
                    {
                        case LayerGeometryType.LGT_POINT:
                            IFeatureGroup61 pFeatureGroup = SlctdLyr.FeatureGroups.Point;
                            if (pFeatureGroup.DisplayAs == ObjectTypeCode.OT_MODEL)
                            {
                                SurportOrNot = false;
                                MessageBox.Show("模型点要素不支持分段专题");
                            }
                            else
                            {
                                if (this.CurrentSymbol.CurrentPointSymbol.PointType == "Other")
                                {
                                    if (this.simpleBtnBreakSymbol.Image == null)
                                        this.simpleBtnBreakSymbol.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Polygonblue.png");
                                }
                                else
                                {
                                    if (this.comboBoxClassBreakType.Items.Count == 1)
                                        this.comboBoxClassBreakType.Items.Add("分级符号");

                                    if (this.simpleBtnBreakSymbol.Image == null)
                                        this.simpleBtnBreakSymbol.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\" + this.CurrentSymbol.CurrentPointSymbol.PointType + "blue.png");

                                }
                                this.tabControl4.TabPages[0].ImageIndex = 0;
                                this.tabControl4.TabPages[0].Text = "点";
                            }
                            break;
                        case LayerGeometryType.LGT_POLYLINE:
                            if (this.comboBoxClassBreakType.Items.Count == 1)
                                this.comboBoxClassBreakType.Items.Add("分级符号");
                            this.tabControl4.TabPages[0].ImageIndex = 1;
                            this.tabControl4.TabPages[0].Text = "线";
                            if (this.simpleBtnBreakSymbol.Image == null)
                                this.simpleBtnBreakSymbol.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\" + this.CurrentSymbol.CurrentPolylineSymbol.PolylineType + ".png");
                            break;
                        case LayerGeometryType.LGT_POLYGON:
                            if (this.comboBoxClassBreakType.Items.Count > 1)
                                this.comboBoxClassBreakType.Items.Remove("分级符号");
                            this.tabControl4.TabPages[0].ImageIndex = 2;
                            this.tabControl4.TabPages[0].Text = "面";
                            if (this.simpleBtnBreakSymbol.Image == null)
                                this.simpleBtnBreakSymbol.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Polygonblue.png");
                            break;
                        default:
                            break;
                    }
                    if (SurportOrNot)
                    {
                        IDataSourceInfo61 SlctdLyrDataSource = SlctdLyr.DataSourceInfo;
                        if (SlctdLyrDataSource.Attributes.Count > 0)
                        {
                            SlctdLyrDataSource.Attributes.ImportAll = true;
                            FieldIndex.Clear();
                            for (int i = 0; i < SlctdLyrDataSource.Attributes.Count; i++)
                            {
                                IAttribute61 Attribute = SlctdLyrDataSource.Attributes.get_Attribute(i);
                                if (Attribute.Type == AttributeTypeCode.AT_INTEGER || Attribute.Type == AttributeTypeCode.AT_DOUBLE)
                                {
                                    this.comboBoxValue2.Items.Add(Attribute.Name);
                                    FieldIndex.Add(i);
                                }

                            }

                            if (this.comboBoxLayer2.Items.Count > 0)
                                this.comboBoxValue2.Text = "请选择字段！";
                            else
                                this.comboBoxValue2.Text = "无合适字段！";
                        }
                        else
                            MessageBox.Show("该要素无属性！");
                    }
                }
            }
            catch
            {
                MessageBox.Show("操作遇到问题!如面板显示正常，可以忽略以继续");
            }

        }
        private void pictureBoxStartCLR_Click(object sender, EventArgs e)
        {

            ColorDialog cd = new ColorDialog();
            cd.AnyColor = true;
            cd.Color = this.pictureBoxStartCLR.BackColor;
            cd.ShowDialog();
            this.pictureBoxStartCLR.BackColor = cd.Color;
        }
        private void pictureBoxEndCLR_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = true;
            cd.Color = this.pictureBoxEndCLR.BackColor;
            cd.ShowDialog();
            this.pictureBoxEndCLR.BackColor = cd.Color;
        }
        private void pictureBoxBreakCLR_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = true;
            cd.Color = this.pictureBoxBreakCLR.BackColor;
            cd.ShowDialog();
            this.pictureBoxBreakCLR.BackColor = cd.Color;
        }
        private void comboBoxBreakMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //重新载入值列表
                if (this.dataGridView2.Rows.Count > 1)
                {
                    ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer2.SelectedIndex]);
                    IFeatureGroup61 SFeatureGroup = null;
                    switch (SlctdLyr.GeometryType)
                    {
                        case LayerGeometryType.LGT_POINT:
                            SFeatureGroup = SlctdLyr.FeatureGroups.Point;
                            break;
                        case LayerGeometryType.LGT_POLYLINE:
                            SFeatureGroup = SlctdLyr.FeatureGroups.Polyline;
                            break;
                        case LayerGeometryType.LGT_POLYGON:
                            SFeatureGroup = SlctdLyr.FeatureGroups.Polygon;
                            break;
                        default:
                            break;
                    }
                    //获取不重复值列表
                    #region
                    IFeature61 SFeature = null;
                    string AValue = "";
                    List<string> Valuelist = new List<string>();
                    for (int i = 0; i < SFeatureGroup.Count; i++)
                    {
                        SFeature = SFeatureGroup[i] as IFeature61;
                        AValue = SFeature.FeatureAttributes.GetFeatureAttribute(this.comboBoxValue2.Text).Value;
                        bool RepeatOrNot = false;

                        if (AValue != "")
                        {
                            //判断值是否重复，若重复则不添加入列表中
                            if (Valuelist.Count == 0)
                                Valuelist.Add(AValue);
                            else
                            {
                                RepeatOrNot = IFRepeat(AValue, Valuelist);
                                if (!RepeatOrNot)
                                {
                                    Valuelist.Add(AValue);
                                }
                            }
                        }
                    }
                    #endregion

                    //获取字段类型
                    IDataSourceInfo61 SlctdLyrDataSource = SlctdLyr.DataSourceInfo;
                    SlctdLyrDataSource.Attributes.ImportAll = true;
                    AttributeTypeCode FieldType = SlctdLyrDataSource.Attributes.get_Attribute(this.comboBoxValue2.SelectedIndex).Type;

                    //获取值分段方案,重新加载
                    IList Breaklist = ClassBreaklist(Valuelist, FieldType, Convert.ToInt32(this.comboBoxBreakNums.Text), this.comboBoxBreakMethods.Text);
                    for (int m = 0; m < (Breaklist.Count - 1); m++)
                    {
                        dataGridView2.Rows[m].Cells[1].Value = Breaklist[m] + " - " + Breaklist[m + 1];
                    }
                    dataGridView2.ClearSelection();
                }

            }
            catch 
            {
                MessageBox.Show("切换分段方案遇到问题！");
            }
        }
        private void comboBoxValue2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.comboBoxValue2.Items.Count > 0)
                    CreateClassBreak();
            }
            catch
            {
                MessageBox.Show("切换分段值遇到问题！");
            }
        }
        private void comboBoxBreakNums_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //重新载入颜色和值
                if (this.dataGridView2.Rows.Count > 1)
                    CreateClassBreak();
            }
            catch
            {
                MessageBox.Show("切换分段数遇到问题！");
            }

        }
        private void comboBoxClassBreakType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //根据当前设置切换datagridview2中的值，若图层已选择
                if (this.comboBoxClassBreakType.Text == "分级色彩")
                {
                    this.panel1.Show();
                    this.panel2.Hide();
                    this.splitContainer3.SplitterDistance = 180;
                    //修改图片、颜色带，单元格高度
                    #region
                    if (this.dataGridView2.RowCount > 0)
                    {
                        //重加载datagridview2
                        if (this.dataGridView2.RowCount == 1)
                        {
                            this.dataGridView2[0, 0].Value = Image.FromFile(Application.StartupPath + @"\SymbolImage\Polygon55b.png");
                            this.dataGridView2[0, 0].Style.BackColor = this.pictureBoxStartCLR.BackColor;
                        }
                        else
                        {
                            int ClassNum = this.dataGridView2.RowCount;
                            List<Color> Colorlist = new List<Color>();
                            Colorlist = ProduceColors(pictureBoxStartCLR.BackColor, pictureBoxEndCLR.BackColor, ClassNum);
                            for (int i = 0; i < ClassNum; i++)
                            {
                                this.dataGridView2.Rows[i].Height = 23;
                                this.dataGridView2[0, i].Style.BackColor = Colorlist[i];
                                this.dataGridView2[0, i].Value = Image.FromFile(Application.StartupPath + @"\SymbolImage\Polygon55b.png");

                            }
                        }
                        this.dataGridView2.ClearSelection();
                    }
                    #endregion
                }
                else
                {
                    this.panel1.Hide();
                    this.panel2.Location = this.panel1.Location;
                    this.txtBoxSizeMax.Text = "100";
                    this.txtBoxSizeMin.Text = "0";
                    this.panel2.Show();
                    this.splitContainer3.SplitterDistance = 227;
                    //修改图片、单元格高度，颜色

                    if (this.dataGridView2.RowCount > 0)
                    {
                        ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer2.SelectedIndex]);
                        //重加载datagridview2
                        if (this.dataGridView2.RowCount == 1)
                        {
                            this.dataGridView2[0, 0].Style.BackColor = this.pictureBoxBreakCLR.BackColor;
                            switch (SlctdLyr.GeometryType)
                            {
                                case LayerGeometryType.LGT_POINT:
                                    this.dataGridView2[0, 0].Value = Image.FromFile(Application.StartupPath + this.CurrentSymbol.CurrentPointSymbol.PointType + "55b.png");
                                    break;
                                case LayerGeometryType.LGT_POLYLINE:
                                    this.dataGridView2[0, 0].Value = Image.FromFile(Application.StartupPath + this.CurrentSymbol.CurrentPolylineSymbol.PolylineType + "55.png");
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            #region
                            int ClassNum = this.dataGridView2.RowCount;
                            double Scalestep = 2.0 / (ClassNum - 1);
                            Image pImage = null;
                            switch (SlctdLyr.GeometryType)
                            {
                                case LayerGeometryType.LGT_POINT:
                                    pImage = Image.FromFile(Application.StartupPath + @"\SymbolImage\" + this.CurrentSymbol.CurrentPointSymbol.PointType + "55b.png");
                                    break;
                                case LayerGeometryType.LGT_POLYLINE:
                                    pImage = Image.FromFile(Application.StartupPath + @"\SymbolImage\" + this.CurrentSymbol.CurrentPolylineSymbol.PolylineType + "55.png");
                                    break;
                                default:
                                    break;
                            }
                            for (int i = 0; i < ClassNum; i++)
                            {
                                this.dataGridView2.Rows[i].Height = (int)(pImage.Height * (1 + Scalestep * i));
                                dataGridView2[0, i].Style.BackColor = pictureBoxBreakCLR.BackColor;
                                //根据分段设置赋予图片
                                this.dataGridView2[0, i].Value = KiResizeImage(pImage, (int)(pImage.Width * (1 + Scalestep * i)), (int)(pImage.Height * (1 + Scalestep * i)));
                            }
                            #endregion
                        }
                        this.dataGridView2.ClearSelection();
                    }
                }

            }
            catch
            {
                MessageBox.Show("切换分级类型遇到问题！");
            }
        }
        private void simpleBtnBreakSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                if (ShpLyrID.Count > 0)
                {
                    if (this.comboBoxLayer2.SelectedIndex >= 0)
                    {    //根据图层类型弹出相应符号化
                        ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer2.SelectedIndex]);
                        switch (SlctdLyr.GeometryType)
                        {
                            case LayerGeometryType.LGT_POINT:
                                if (this.myPointBreakSymbol == null)
                                {
                                    //myPointBreakSymbol = new FrmPointSymbol(this);
                                    Point p = new Point(Control.MousePosition.X - myPointBreakSymbol.Width, Control.MousePosition.Y);
                                    myPointBreakSymbol.Location = p;
                                    myPointBreakSymbol.Show();
                                }
                                else
                                    this.myPointBreakSymbol.Focus();

                                break;
                            case LayerGeometryType.LGT_POLYLINE:
                                if (this.myPolylineSymbol == null)
                                {
                                    //myPolylineBreakSymbol = new FrmPolylineSymbol(this);
                                    Point p1 = new Point(Control.MousePosition.X - myPolylineBreakSymbol.Width, Control.MousePosition.Y);
                                    myPolylineBreakSymbol.Location = p1;
                                    myPolylineBreakSymbol.Show();
                                }
                                else
                                    this.myPolylineBreakSymbol.Focus();
                                break;
                            case LayerGeometryType.LGT_POLYGON:
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("弹出符号化窗体遇到问题！");
            }
        }
        private void CreateClassBreak()//载入值列表并分配颜色或图片
        {
            //根据选择的值字段，分段方法（默认等距）、分段数（默认为5），起止颜色方案（默认深绿到浅绿）
            
                dataGridView2.Rows.Clear();
                ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer2.SelectedIndex]);
                //Layer的Import Option要以ALL Features 方式导入，以Streaming方式导入要素数为0
                if (SlctdLyr.Streaming == true)
                    SlctdLyr.Streaming = false;
                IFeatureGroup61 SFeatureGroup = null;
                //根据分级类型赋予列图片
                switch (SlctdLyr.GeometryType)
                {
                    case LayerGeometryType.LGT_POINT:
                        SFeatureGroup = SlctdLyr.FeatureGroups.Point;
                        if (this.CurrentSymbol.CurrentPointSymbol.PointType != "Other"&this.comboBoxClassBreakType.Text == "分级符号")
                            ((DataGridViewImageColumn)this.dataGridView2.Columns[0]).Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\" + this.CurrentSymbol.CurrentPointSymbol.PointType + "55b.png");
                        else
                            ((DataGridViewImageColumn)this.dataGridView2.Columns[0]).Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Polygon55b.png");
                        break;
                    case LayerGeometryType.LGT_POLYLINE:
                        SFeatureGroup = SlctdLyr.FeatureGroups.Polyline;
                        if (this.comboBoxClassBreakType.Text == "分级符号")
                            ((DataGridViewImageColumn)this.dataGridView2.Columns[0]).Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\" + this.CurrentSymbol.CurrentPolylineSymbol.PolylineType + "55.png");
                        else
                            ((DataGridViewImageColumn)this.dataGridView2.Columns[0]).Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Polygon55b.png");
                        break;
                    case LayerGeometryType.LGT_POLYGON:
                        SFeatureGroup = SlctdLyr.FeatureGroups.Polygon;
                        ((DataGridViewImageColumn)this.dataGridView2.Columns[0]).Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Polygon55b.png");
                        break;
                    default:
                        break;
                }

                //获取不重复值列表
                #region
                IFeature61 SFeature = null;
                string AValue = "";
                List<string> Valuelist = new List<string>();
                for (int i = 0; i < SFeatureGroup.Count; i++)
                {
                    SFeature = SFeatureGroup[i] as IFeature61;
                    AValue = SFeature.FeatureAttributes.GetFeatureAttribute(this.comboBoxValue2.Text).Value;
                    bool RepeatOrNot = false;

                    if (AValue != "")
                    {
                        //判断值是否重复，若重复则不添加入列表中
                        if (Valuelist.Count == 0)
                            Valuelist.Add(AValue);
                        else
                        {
                            RepeatOrNot = IFRepeat(AValue, Valuelist);
                            if (!RepeatOrNot)
                            {
                                Valuelist.Add(AValue);
                            }
                        }
                    }
                }
                #endregion

                if (Valuelist.Count == 0)
                    MessageBox.Show("空字段");
                else
                {
                    if (Valuelist.Count == 1)
                    {
                        //仅存在一个不重复值，分配一个颜色
                        this.dataGridView2.Rows.Add();
                        this.dataGridView2.Rows[0].Cells[1].Value = Valuelist[0];
                        if (this.comboBoxClassBreakType.Text == "分级色彩")
                            dataGridView2.Rows[0].Cells[0].Style.BackColor = pictureBoxStartCLR.BackColor;
                        else
                            dataGridView2.Rows[0].Cells[0].Style.BackColor = pictureBoxBreakCLR.BackColor;
                        this.dataGridView2.ClearSelection();
                    }
                    else
                    {
                        //获取字段类型
                        IDataSourceInfo61 SlctdLyrDataSource = SlctdLyr.DataSourceInfo;
                        SlctdLyrDataSource.Attributes.ImportAll = true;
                        AttributeTypeCode FieldType = SlctdLyrDataSource.Attributes.get_Attribute(FieldIndex[this.comboBoxValue2.SelectedIndex]).Type;
                        //获取值分段方案
                        IList Breaklist = ClassBreaklist(Valuelist, FieldType, Convert.ToInt32(this.comboBoxBreakNums.Text), this.comboBoxBreakMethods.Text);
                        if (this.comboBoxClassBreakType.Text == "分级色彩")
                        {
                            //获取颜色分段方案
                            List<Color> Colorlist = new List<Color>();
                            Colorlist = ProduceColors(pictureBoxStartCLR.BackColor, pictureBoxEndCLR.BackColor, Convert.ToInt32(this.comboBoxBreakNums.Text));
                            IColor61 IFillcolor = null;
                            for (int i = 0; i < (Breaklist.Count - 1); i++)
                            {
                                dataGridView2.Rows.Add();
                                dataGridView2.Rows[i].Cells[1].Value = Breaklist[i] + " - " + Breaklist[i + 1];
                                dataGridView2.Rows[i].Cells[0].Style.BackColor = Colorlist[i];
                            }
                        }
                        else
                        {
                            #region
                            int ClassNum = Breaklist.Count - 1;
                            double Scalestep = 2.0 / (ClassNum - 1);
                            Image pImage = null;
                            switch (SlctdLyr.GeometryType)
                            {
                                case LayerGeometryType.LGT_POINT:
                                    pImage = Image.FromFile(Application.StartupPath + @"\SymbolImage\" + this.CurrentSymbol.CurrentPointSymbol.PointType + "55b.png");
                                    break;
                                case LayerGeometryType.LGT_POLYLINE:
                                    pImage = Image.FromFile(Application.StartupPath + @"\SymbolImage\" + this.CurrentSymbol.CurrentPolylineSymbol.PolylineType + "55.png");
                                    break;
                                default:
                                    break;
                            }
                            for (int i = 0; i < ClassNum; i++)
                            {
                                dataGridView2.Rows.Add();
                                dataGridView2[1, i].Value = Breaklist[i] + " - " + Breaklist[i + 1];
                                dataGridView2[0, i].Style.BackColor = pictureBoxBreakCLR.BackColor;
                                //根据分段设置赋予图片
                                this.dataGridView2.Rows[i].Height = (int)(pImage.Height * (1 + Scalestep * i));
                                this.dataGridView2[0, i].Value = KiResizeImage(pImage, (int)(pImage.Width * (1 + Scalestep * i)), (int)(pImage.Height * (1 + Scalestep * i)));

                            }
                            #endregion
                        }
                        dataGridView2.ClearSelection();
                    }
                }
        }
        public static Image KiResizeImage(Image bmp, int newW, int newH)
        {
            try
            {
                Image b = (Image)new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Unexpected Error:" + ex.Message);
                return null;
            }
        }
        private List<string> ClassBreaklist(List<string> valuelist, AttributeTypeCode Fieldtype, int ClassNum, string ClassBreakMethod)
        {
            List<string> result = null;
            //根据字段类型，将值列表转换为相应类型并获取分段方案
            if (valuelist.Count > 0)
            {
                if (Fieldtype == AttributeTypeCode.AT_INTEGER)
                {
                    List<int> Outputlist = new List<int>();
                    Outputlist = valuelist.ConvertAll<int>(new Converter<string, int>(StringToInt));
                    Outputlist.Sort();

                    List<int> ResultInt = new List<int>();
                    if (ClassBreakMethod == "等距分段")
                    {
                        ResultInt.Add(Outputlist[0]);
                        int Step = (Outputlist[Outputlist.Count - 1] - Outputlist[0]) / ClassNum;
                        for (int i = 1; i < ClassNum; i++)
                        {
                            ResultInt.Add(ResultInt[i - 1] + Step);
                        }
                        ResultInt.Add(Outputlist[Outputlist.Count - 1]);
                        result = ResultInt.ConvertAll<string>(new Converter<int, string>(IntToString));
                    }
                    else //对数分段
                    {
                        if (Outputlist[0] > 0)
                        {
                            double Max = Math.Log10((double)Outputlist[Outputlist.Count - 1]);
                            double Min = Math.Log10((double)Outputlist[0]);

                            ResultInt.Add(Outputlist[0]);
                            double Step = (Max - Min) / ClassNum;
                            for (int i = 1; i < ClassNum; i++)
                            {
                                ResultInt.Add((int)Math.Pow(10, (Min + Step * i)));
                            }
                            ResultInt.Add(Outputlist[Outputlist.Count - 1]);
                            result = ResultInt.ConvertAll<string>(new Converter<int, string>(IntToString));
                        }
                        else
                            MessageBox.Show("字段值存在负值，无法采用对数分段");
                    }
                }
                else
                {
                    List<double> Outputlist = new List<double>();
                    Outputlist = valuelist.ConvertAll<double>(new Converter<string, double>(StringToDouble));
                    Outputlist.Sort();

                    Double Step;
                    List<double> Resultdouble = new List<double>();
                    if (ClassBreakMethod == "等距分段")
                    {
                        Resultdouble.Add(Outputlist[0]);
                        Step = (Outputlist[Outputlist.Count - 1] - Outputlist[0]) / ClassNum;
                        for (int i = 1; i < ClassNum; i++)
                        {
                            Resultdouble.Add(Resultdouble[i - 1] + Step);
                        }
                        Resultdouble.Add(Outputlist[Outputlist.Count - 1]);
                        result = Resultdouble.ConvertAll<string>(new Converter<double, string>(DoubleToString));
                    }
                    else //对数分段
                    {
                        if (Outputlist[0] > 0)
                        {
                            double Max = Math.Log10(Outputlist[Outputlist.Count - 1]);
                            double Min = Math.Log10(Outputlist[0]);

                            Resultdouble.Add(Outputlist[0]);
                            Step = (Max - Min) / ClassNum;
                            for (int i = 1; i < ClassNum; i++)
                            {
                                Resultdouble.Add(Math.Pow(10, (Min + Step * i)));
                            }
                            Resultdouble.Add(Outputlist[Outputlist.Count - 1]);
                            result = Resultdouble.ConvertAll<string>(new Converter<double, string>(DoubleToString));
                        }
                        else
                            MessageBox.Show("字段值存在负值，无法采用对数分段");
                    }
                }

            }
            else
                MessageBox.Show("该字段空白!");

            return result;
        }
        public int StringToInt(string value)
        {
            return Convert.ToInt32(value);
        }
        public string IntToString(int value)
        {
            return value.ToString();
        }
        public double StringToDouble(string value)
        {
            return Convert.ToDouble(value);
        }
        public string DoubleToString(double value)
        {
            return value.ToString();
        }
        //根据起点颜色、终点颜色和级别数目，产生色带   
        public List<Color> ProduceColors(Color start, Color end, int gradecount)
        {
            //创建一个新AlgorithmicColorRampClass对象   
            IAlgorithmicColorRamp algColorRamp = new AlgorithmicColorRampClass();
            algColorRamp.ToColor = this.ConvertColorToIColor(end);//从.net的颜色转换   
            algColorRamp.FromColor = this.ConvertColorToIColor(start);
            //设置梯度类型   
            algColorRamp.Algorithm = esriColorRampAlgorithm.esriCIELabAlgorithm;
            //设置颜色带颜色数量   
            algColorRamp.Size = gradecount;
            //创建颜色带   
            bool bture = true;
            algColorRamp.CreateRamp(out bture);
            //使用IEnumColors获取颜色带  
            IEnumColors pEnumColors = algColorRamp.Colors;

            List<Color> result = new List<Color>();
            pEnumColors.Reset();//必须重设   
            ESRI.ArcGIS.Display.IColor pC = pEnumColors.Next();
            while (pC != null)
            {
                result.Add(ColorTranslator.FromOle(pC.RGB));//ESRI颜色转换为.net颜色
                pC = pEnumColors.Next();
            }
            return result;
        }
        //将.net颜色转变为ESRI的颜色   
        public IColor ConvertColorToIColor(Color color)
        {
            IColor pColor = new RgbColorClass();
            pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
            return pColor;
        }
        private void pictureBoxStartCLR_BackColorChanged(object sender, EventArgs e)
        {
            try
            {
                //切换颜色时，同时生成色带分配到单元格
                //获取颜色分段方案
                if (this.dataGridView2.Rows.Count > 0)
                {
                    if (this.dataGridView2.Rows.Count == 1)
                    {
                        this.dataGridView2.Rows[0].Cells[0].Style.BackColor = pictureBoxStartCLR.BackColor;
                        this.dataGridView2.ClearSelection();
                    }
                    else
                    {
                        List<Color> Colorlist = new List<Color>();
                        Colorlist = ProduceColors(this.pictureBoxStartCLR.BackColor, this.pictureBoxEndCLR.BackColor, this.dataGridView2.Rows.Count);
                        for (int m = 0; m < this.dataGridView2.Rows.Count; m++)
                        {
                            this.dataGridView2.Rows[m].Cells[0].Style.BackColor = Colorlist[m];

                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("切换颜色时遇到问题!");
            }
        }
        private void pictureBoxEndCLR_BackColorChanged(object sender, EventArgs e)
        {
            try
            {
                //切换颜色，同时生成色带分配到单元格
                //获取颜色分段方案
                if (this.dataGridView2.Rows.Count > 0)
                {
                    if (this.dataGridView2.Rows.Count == 1)
                    {
                        this.dataGridView2.Rows[0].Cells[0].Style.BackColor = pictureBoxEndCLR.BackColor;
                        this.dataGridView2.ClearSelection();
                    }
                    else
                    {
                        List<Color> Colorlist = new List<Color>();
                        Colorlist = ProduceColors(this.pictureBoxStartCLR.BackColor, this.pictureBoxEndCLR.BackColor, this.dataGridView2.Rows.Count);
                        for (int m = 0; m < this.dataGridView2.Rows.Count; m++)
                        {
                            this.dataGridView2.Rows[m].Cells[0].Style.BackColor = Colorlist[m];

                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("切换颜色时遇到问题!");
            }
        }
        private void pictureBoxBreakCLR_BackColorChanged(object sender, EventArgs e)
        {
                if (this.dataGridView2.Rows.Count > 0)
                {
                    if (this.dataGridView2.Rows.Count == 1)
                    {
                        this.dataGridView2.Rows[0].Cells[0].Style.BackColor = pictureBoxBreakCLR.BackColor;
                        this.dataGridView2.ClearSelection();
                    }
                    else
                    {
                        for (int m = 0; m < this.dataGridView2.Rows.Count; m++)
                        {
                            this.dataGridView2.Rows[m].Cells[0].Style.BackColor = pictureBoxBreakCLR.BackColor;

                        }
                    }
                }
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //点击‘颜色‘列单元格弹出颜色选择对话框,由用户选择颜色
            if (e.ColumnIndex == 0)
            {
                ColorDialog cd = new ColorDialog();
                cd.AnyColor = true;
                cd.Color = this.dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor;
                cd.ShowDialog();
                this.dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = cd.Color;
                this.dataGridView2.ClearSelection();
            }
        }
        private void simpleButtonApply2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.comboBoxLayer2.Items.Count == 0)
                    MessageBox.Show("请先加载图层！");
                else
                {
                    if (this.comboBoxValue2.Items.Count == 0)
                        MessageBox.Show("请先选择图层！");
                    else
                    {
                        if (this.dataGridView2.Rows.Count == 0)
                            MessageBox.Show("请先选择字段！");
                        else
                        {
                            #region
                            string FillcolorXML = "";
                            string FillXML = "";
                            ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer2.SelectedIndex]);
                            IFeatureGroup61 SFeatureGroup = null;

                            switch (SlctdLyr.GeometryType)
                            {
                                case LayerGeometryType.LGT_POINT:
                                    SFeatureGroup = SlctdLyr.FeatureGroups.Point;
                                    break;
                                case LayerGeometryType.LGT_POLYLINE:
                                    SFeatureGroup = SlctdLyr.FeatureGroups.Polyline;
                                    break;
                                case LayerGeometryType.LGT_POLYGON:
                                    SFeatureGroup = SlctdLyr.FeatureGroups.Polygon;
                                    break;
                                default:
                                    break;
                            }
                            SFeatureGroup.SetProperty("Altitude Method", 2);
                            if (this.comboBoxClassBreakType.Text == "分级色彩")
                            {
                                #region
                                if (this.dataGridView2.Rows.Count == 1)
                                {
                                    IColor61 IFillcolor = null;
                                    Color CellBackcolor = this.dataGridView2.Rows[0].Cells[0].Style.BackColor;
                                    IFillcolor = Program.sgworld.Creator.CreateColor(CellBackcolor.R, CellBackcolor.G, CellBackcolor.B, CellBackcolor.A);
                                    if (SlctdLyr.GeometryType == LayerGeometryType.LGT_POLYLINE)
                                    {
                                        if (SFeatureGroup.GetProperty("Line Opacity").ToString() != "1")
                                            SFeatureGroup.SetProperty("Line Opacity", 1);
                                        SFeatureGroup.SetProperty("Line Color", IFillcolor);
                                    }
                                    else
                                    {
                                        if (SFeatureGroup.DisplayAs == ObjectTypeCode.OT_LABEL)
                                        {
                                            SFeatureGroup.SetProperty("Text Color", IFillcolor);
                                        }
                                        else
                                        {
                                            if (SFeatureGroup.DisplayAs == ObjectTypeCode.OT_IMAGE_LABEL)
                                            {
                                                if (SFeatureGroup.GetProperty("Image Opacity").ToString() != "1")
                                                    SFeatureGroup.SetProperty("Image Opacity", 1);
                                                SFeatureGroup.SetProperty("Image Color", IFillcolor);
                                            }
                                            else
                                            {
                                                if (SFeatureGroup.GetProperty("Fill Opacity").ToString() != "1")
                                                    SFeatureGroup.SetProperty("Fill Opacity", 1);
                                                SFeatureGroup.SetProperty("Fill Color", IFillcolor);
                                            }
                                        }
                                       
                                    }
                                }
                                else
                                {
                                    List<string> valuelist = new List<string>();
                                    string[] Split = new string[2];
                                    for (int i = 0; i < dataGridView2.RowCount; i++)
                                    {
                                        Split = this.dataGridView2.Rows[i].Cells[1].Value.ToString().Split(new char[] { '-' });
                                        valuelist.Add(Split[0]);
                                    }
                                    valuelist.Add(Split[1]);

                                    if (this.comboBoxBreakMethods.Text == "等距分段")
                                        FillcolorXML = "<Classification FuncType=\"5\">";
                                    else
                                        FillcolorXML = "<Classification FuncType=\"9\">";

                                    IColor61 IFillcolor = null;
                                    Color CellBackcolor = Color.Empty;
                                    for (int i = 0; i < this.dataGridView2.Rows.Count; i++)
                                    {
                                        CellBackcolor = this.dataGridView2.Rows[i].Cells[0].Style.BackColor;
                                        IFillcolor = Program.sgworld.Creator.CreateColor(CellBackcolor.R, CellBackcolor.G, CellBackcolor.B, CellBackcolor.A);

                                        FillcolorXML += "<Class><Condition>&lt;" + valuelist[i].Trim() + "=&lt;[" + this.comboBoxValue2.Text + "] and [" + this.comboBoxValue2.Text + "]=&lt;" + valuelist[i + 1].Trim() +
                                                        "&gt;</Condition><Value>" + IFillcolor.ToBGRColor().ToString() + "</Value></Class>";
                                    }
                                    FillcolorXML += "<DefaultValue>6579300</DefaultValue></Classification>";
                                    if (SlctdLyr.GeometryType == LayerGeometryType.LGT_POLYLINE)
                                    {
                                        if (SFeatureGroup.GetProperty("Line Opacity").ToString() != "1")
                                            SFeatureGroup.SetProperty("Line Opacity", 1);
                                        SFeatureGroup.SetClassification("Line Color", FillcolorXML);
                                    }
                                    else
                                    {
                                         if (SFeatureGroup.DisplayAs == ObjectTypeCode.OT_LABEL)
                                        {
                                            SFeatureGroup.SetClassification("Text Color", FillcolorXML);
                                        }
                                        else
                                        {
                                            if (SFeatureGroup.DisplayAs == ObjectTypeCode.OT_IMAGE_LABEL)
                                            {
                                                if (SFeatureGroup.GetProperty("Image Opacity").ToString() != "1")
                                                    SFeatureGroup.SetProperty("Image Opacity", 1);
                                                SFeatureGroup.SetClassification("Image Color", FillcolorXML);
                                            }
                                            else
                                            {
                                                if (SFeatureGroup.GetProperty("Fill Opacity").ToString() != "1")
                                                    SFeatureGroup.SetProperty("Fill Opacity", 1);
                                                SFeatureGroup.SetClassification("Fill Color", FillcolorXML);
                                            }
                                       
                                        }
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                if (this.txtBoxSizeMin.Text == "" || this.txtBoxSizeMax.Text == "")
                                    MessageBox.Show("请正确填写符号大小范围");
                                else
                                {
                                    //分级符号
                                    if (this.dataGridView2.Rows.Count == 1)
                                    {
                                        #region
                                        IColor61 IFillcolor = null;
                                        Color CellBackcolor = this.dataGridView2.Rows[0].Cells[0].Style.BackColor;
                                        IFillcolor = Program.sgworld.Creator.CreateColor(CellBackcolor.R, CellBackcolor.G, CellBackcolor.B, CellBackcolor.A);
                                        if (SlctdLyr.GeometryType == LayerGeometryType.LGT_POLYLINE)
                                        {
                                            switch (this.CurrentSymbol.CurrentPolylineSymbol.PolylineType)
                                            {
                                                case "Solidline":
                                                    SFeatureGroup.SetProperty("Line Pattern", -1);
                                                    break;
                                                case "Dottedline":
                                                    SFeatureGroup.SetProperty("Line Pattern", -1044481);
                                                    break;
                                                case "Dottedline2":
                                                    SFeatureGroup.SetProperty("Line Pattern", -16776961);
                                                    break;
                                                case "Dottedline3":
                                                    SFeatureGroup.SetProperty("Line Pattern", -267390961);
                                                    break;
                                                case "Dottedline4":
                                                    SFeatureGroup.SetProperty("Line Pattern", -1010580541);
                                                    break;
                                                case "Dottedline5":
                                                    SFeatureGroup.SetProperty("Line Pattern", -1717986919);
                                                    break;
                                                case "Dottedline6":
                                                    SFeatureGroup.SetProperty("Line Pattern", -1431655766);
                                                    break;
                                                case "Dottedline7":
                                                    SFeatureGroup.SetProperty("Line Pattern", -16678657);
                                                    break;
                                                case "Dottedline8":
                                                    SFeatureGroup.SetProperty("Line Pattern", -15978241);
                                                    break;
                                            }
                                            SFeatureGroup.SetProperty("Line Width", this.CurrentSymbol.CurrentPolylineSymbol.PolylineWidth);
                                            if (SFeatureGroup.IsClassified("Line Back Color"))
                                            {
                                                SFeatureGroup.SetClassification("Line Back Color", this.CurrentSymbol.CurrentPolylineSymbol.PolylineBackColorClass);
                                            }
                                            else
                                            {
                                                SFeatureGroup.SetProperty("Line Back Color", Program.sgworld.Creator.CreateColor(this.CurrentSymbol.CurrentPolylineSymbol.PolylineBackColor.R,
                                                                                                                                 this.CurrentSymbol.CurrentPolylineSymbol.PolylineBackColor.G,
                                                                                                                                 this.CurrentSymbol.CurrentPolylineSymbol.PolylineBackColor.B,
                                                                                                                                 this.CurrentSymbol.CurrentPolylineSymbol.PolylineBackColor.A));
                                            } 
                                            SFeatureGroup.SetProperty("Line Back Opacity", this.CurrentSymbol.CurrentPolylineSymbol.PolylineBackOpacity);

                                            if (SFeatureGroup.GetProperty("Line Opacity").ToString() != "1")
                                                SFeatureGroup.SetProperty("Line Opacity", 1);
                                            SFeatureGroup.SetProperty("Line Color", IFillcolor);
                                        }
                                        else
                                        {
                                            switch (this.CurrentSymbol.CurrentPointSymbol.PointType)
                                            {
                                                //根据分段设置赋予图片，及显示图形大小?
                                                case "Circle":
                                                    SFeatureGroup.DisplayAs = ObjectTypeCode.OT_CIRCLE;
                                                    SFeatureGroup.SetProperty("Radius X", this.CurrentSymbol.CurrentPointSymbol.PointSize);
                                                    break;
                                                case "Triangle":
                                                    SFeatureGroup.DisplayAs = ObjectTypeCode.OT_REGULAR_POLYGON;
                                                    SFeatureGroup.SetProperty("Number of sides", 3);
                                                    SFeatureGroup.SetProperty("Radius X", this.CurrentSymbol.CurrentPointSymbol.PointSize);
                                                    break;
                                                case "Rectangle":
                                                    SFeatureGroup.DisplayAs = ObjectTypeCode.OT_RECTANGLE;
                                                    SFeatureGroup.SetProperty("Length", this.CurrentSymbol.CurrentPointSymbol.PointSize);
                                                    SFeatureGroup.SetProperty("Width", this.CurrentSymbol.CurrentPointSymbol.PointSize);
                                                    break;
                                                case "Pentagon":
                                                    SFeatureGroup.DisplayAs = ObjectTypeCode.OT_REGULAR_POLYGON;
                                                    SFeatureGroup.SetProperty("Number of sides", 5);
                                                    SFeatureGroup.SetProperty("Radius X", this.CurrentSymbol.CurrentPointSymbol.PointSize);
                                                    break;
                                                case "Hexagon":
                                                    SFeatureGroup.DisplayAs = ObjectTypeCode.OT_REGULAR_POLYGON;
                                                    SFeatureGroup.SetProperty("Number of sides", 6);
                                                    SFeatureGroup.SetProperty("Radius X", this.CurrentSymbol.CurrentPointSymbol.PointSize);
                                                    break;
                                                case "Arrow":
                                                    SFeatureGroup.DisplayAs = ObjectTypeCode.OT_ARROW;
                                                    SFeatureGroup.SetProperty("Length", this.CurrentSymbol.CurrentPointSymbol.PointSize);
                                                    break;
                                            }
                                            if (SFeatureGroup.GetProperty("Fill Opacity").ToString() != "1")
                                                SFeatureGroup.SetProperty("Fill Opacity", 1);
                                            SFeatureGroup.SetProperty("Fill Color", IFillcolor);
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        List<double> valuelist = new List<double>();
                                        string[] Split = new string[2];
                                        for (int i = 0; i < dataGridView2.RowCount; i++)
                                        {
                                            Split = this.dataGridView2.Rows[i].Cells[1].Value.ToString().Split(new char[] { '-' });
                                            valuelist.Add(Convert.ToDouble(Split[0]));
                                        }
                                        valuelist.Add(Convert.ToDouble(Split[1]));

                                        if (this.comboBoxBreakMethods.Text == "等距分段")
                                        {
                                            FillcolorXML = "<Classification FuncType=\"5\">";
                                            FillXML = "<Classification FuncType=\"6\">";
                                        }
                                        else
                                        {
                                            FillcolorXML = "<Classification FuncType=\"9\">";
                                            FillXML = "<Classification FuncType=\"10\">";
                                        }

                                        double Min = Convert.ToDouble(this.txtBoxSizeMin.Text);
                                        double Max = Convert.ToDouble(this.txtBoxSizeMax.Text);
                                        double Sizestep = (Max - Min) / (this.dataGridView2.Rows.Count - 1);
                                        IColor61 IFillcolor = null;
                                        Color CellBackcolor = Color.Empty;
                                        for (int i = 0; i < this.dataGridView2.Rows.Count; i++)
                                        {
                                            CellBackcolor = this.dataGridView2.Rows[i].Cells[0].Style.BackColor;
                                            IFillcolor = Program.sgworld.Creator.CreateColor(CellBackcolor.R, CellBackcolor.G, CellBackcolor.B, CellBackcolor.A);

                                            FillcolorXML += "<Class><Condition>&lt;" + valuelist[i].ToString() + "=&lt;[" + this.comboBoxValue2.Text + "] and [" + this.comboBoxValue2.Text + "]=&lt;" + valuelist[i + 1].ToString() +
                                                            "&gt;</Condition><Value>" + IFillcolor.ToBGRColor().ToString() + "</Value></Class>";

                                            FillXML += "<Class><Condition>&lt;" + valuelist[i].ToString("0.000000") + "=&lt;[" + this.comboBoxValue2.Text + "] and [" + this.comboBoxValue2.Text + "]=&lt;" + valuelist[i + 1].ToString("0.000000") +
                                                            "&gt;</Condition><Value>" + (Min + i * Sizestep) + "</Value></Class>";

                                        }
                                        FillcolorXML += "<DefaultValue>6579300</DefaultValue></Classification>";
                                        FillXML += "<DefaultValue>10</DefaultValue></Classification>";

                                        #region
                                        if (SlctdLyr.GeometryType == LayerGeometryType.LGT_POLYLINE)
                                        {
                                            switch (this.CurrentSymbol.CurrentPolylineSymbol.PolylineType)
                                            {
                                                //根据分段设置赋予图片
                                                case "Solidline":
                                                    SFeatureGroup.SetProperty("Line Pattern", -1);
                                                    break;
                                                case "Dottedline":
                                                    SFeatureGroup.SetProperty("Line Pattern", -1044481);
                                                    break;
                                                case "Dottedline2":
                                                    SFeatureGroup.SetProperty("Line Pattern", -16776961);
                                                    break;
                                                case "Dottedline3":
                                                    SFeatureGroup.SetProperty("Line Pattern", -267390961);
                                                    break;
                                                case "Dottedline4":
                                                    SFeatureGroup.SetProperty("Line Pattern", -1010580541);
                                                    break;
                                                case "Dottedline5":
                                                    SFeatureGroup.SetProperty("Line Pattern", -1717986919);
                                                    break;
                                                case "Dottedline6":
                                                    SFeatureGroup.SetProperty("Line Pattern", -1431655766);
                                                    break;
                                                case "Dottedline7":
                                                    SFeatureGroup.SetProperty("Line Pattern", -16678657);
                                                    break;
                                                case "Dottedline8":
                                                    SFeatureGroup.SetProperty("Line Pattern", -15978241);
                                                    break;
                                            }
                                            SFeatureGroup.SetClassification("Line Width", FillXML);
                                            if (SFeatureGroup.IsClassified("Line Back Color"))
                                            {
                                                SFeatureGroup.SetClassification("Line Back Color", this.CurrentSymbol.CurrentPolylineSymbol.PolylineBackColorClass);
                                            }
                                            else
                                            {
                                                SFeatureGroup.SetProperty("Line Back Color", Program.sgworld.Creator.CreateColor(this.CurrentSymbol.CurrentPolylineSymbol.PolylineBackColor.R,
                                                                                                                                 this.CurrentSymbol.CurrentPolylineSymbol.PolylineBackColor.G,
                                                                                                                                 this.CurrentSymbol.CurrentPolylineSymbol.PolylineBackColor.B,
                                                                                                                                 this.CurrentSymbol.CurrentPolylineSymbol.PolylineBackColor.A));
                                            } 
                                            SFeatureGroup.SetProperty("Line Back Opacity", this.CurrentSymbol.CurrentPolylineSymbol.PolylineBackOpacity);
                                            if (SFeatureGroup.GetProperty("Line Opacity").ToString() != "1")
                                                SFeatureGroup.SetProperty("Line Opacity", 1);
                                            SFeatureGroup.SetClassification("Line Color", FillcolorXML);
                                        }
                                        else
                                        {
                                            switch (this.CurrentSymbol.CurrentPointSymbol.PointType)
                                            {
                                                //根据分段设置赋予图片，及显示图形大小?
                                                case "Circle":
                                                    SFeatureGroup.DisplayAs = ObjectTypeCode.OT_CIRCLE;
                                                    SFeatureGroup.SetClassification("Radius X", FillXML);
                                                    break;
                                                case "Triangle":
                                                    SFeatureGroup.DisplayAs = ObjectTypeCode.OT_REGULAR_POLYGON;
                                                    SFeatureGroup.SetProperty("Number of sides", 3);
                                                    SFeatureGroup.SetClassification("Radius X", FillXML);
                                                    break;
                                                case "Rectangle":
                                                    SFeatureGroup.DisplayAs = ObjectTypeCode.OT_RECTANGLE;
                                                    SFeatureGroup.SetClassification("Length", FillXML);
                                                    SFeatureGroup.SetClassification("Width", FillXML);
                                                    break;
                                                case "Pentagon":
                                                    SFeatureGroup.DisplayAs = ObjectTypeCode.OT_REGULAR_POLYGON;
                                                    SFeatureGroup.SetProperty("Number of sides", 5);
                                                    SFeatureGroup.SetClassification("Radius X", FillXML);
                                                    break;
                                                case "Hexagon":
                                                    SFeatureGroup.DisplayAs = ObjectTypeCode.OT_REGULAR_POLYGON;
                                                    SFeatureGroup.SetProperty("Number of sides", 6);
                                                    SFeatureGroup.SetClassification("Radius X", FillXML);
                                                    break;
                                                case "Arrow":
                                                    SFeatureGroup.DisplayAs = ObjectTypeCode.OT_ARROW;
                                                    SFeatureGroup.SetClassification("Length", FillXML);
                                                    break;
                                            }
                                            if (SFeatureGroup.GetProperty("Fill Opacity").ToString() != "1")
                                                SFeatureGroup.SetProperty("Fill Opacity", 1);
                                            SFeatureGroup.SetClassification("Fill Color", FillcolorXML);
                                        }
                                        #endregion
                                    }
                                }
                            }
                            #endregion
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成分段专题图遇到问题！");
            }
        }
        //private void simpleButtonCancel2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!SaveOrNot_Class)
        //        {
        //            if (this.comboBoxLayer2.Text != "" & this.comboBoxLayer2.Text != "请选择图层")
        //            {
        //                //保存后关闭？
        //                DialogResult dr = MessageBox.Show("是否保存当前专题？", "关闭提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        //                if (dr == System.Windows.Forms.DialogResult.Yes)
        //                {
        //                    Program.sgworld.Project.Save();
        //                    SaveOrNot_Class = true;
        //                    CurrentThemeType = 0;
        //                    dockPanel5_ClassBreak.Close();
        //                }
        //                else
        //                {
        //                    if (dr == System.Windows.Forms.DialogResult.No)
        //                    {
        //                        //还原图层
        //                        ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer2.SelectedIndex]);
        //                        ReturnToOriginal(SlctdLyr);
        //                        CurrentThemeType = 0;
        //                        dockPanel5_ClassBreak.Close();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        MessageBox.Show("关闭遇到问题！");
        //    }
        //}
        private void simpleButtonOK2_Click(object sender, EventArgs e)
        {
            try
            {
                //保存对属性图层的改变
                if (this.comboBoxLayer2.SelectedIndex >= 0)
                {
                    ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer2.SelectedIndex]);
                    SlctdLyr.Save();
                    Program.sgworld.Project.Save();
                    SaveOrNot_Class = true;
                    MessageBox.Show("保存成功！");
                }
            }
            catch
            {
                MessageBox.Show("保存遇到问题!");
            }
        }
        private void dockPanel5_ClassBreak_ClosingPanel(object sender, DevExpress.XtraBars.Docking.DockPanelCancelEventArgs e)
        {
            try
            {
                if (CurrentThemeType != 0)
                {
                    if (!SaveOrNot_Class)
                    {
                        if (this.comboBoxLayer2.Text != "" & this.comboBoxLayer2.Text != "请选择图层")
                        {
                            //保存后关闭？
                            DialogResult dr = MessageBox.Show("是否保存当前专题？", "关闭提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (dr == System.Windows.Forms.DialogResult.Yes)
                            {
                                Program.sgworld.Project.Save();
                                SaveOrNot_Class = true;
                                CurrentThemeType = 0;
                            }
                            else
                            {
                                if (dr == System.Windows.Forms.DialogResult.No)
                                {
                                    //还原图层
                                    ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer2.SelectedIndex]);
                                    ReturnToOriginal(SlctdLyr);
                                    CurrentThemeType = 0;
                                }
                                else
                                    e.Cancel = true;
                            }
                        }
                    }

                }
            }
            catch
            {
                MessageBox.Show("关闭时遇到问题！");
            }

        }

        #endregion

        #region 标签专题
        //private void LabelTheme_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        if (CurrentThemeType != 0 & CurrentThemeType != 3)
        //        {
        //            //关闭当前面板
        //            DialogResult dr = MessageBox.Show("关闭当前专题面板？", "关闭提示", MessageBoxButtons.OKCancel);
        //            //保存后关闭？
        //            //DialogResult dr = MessageBox.Show("是否保存当前专题？", "关闭提示", MessageBoxButtons.OKCancel);
        //            if (dr == DialogResult.OK)
        //            {
        //                switch (CurrentThemeType)
        //                {
        //                    case 1:
        //                        this.dockPanel5_SimpleTheme.Close();
        //                        CurrentThemeType = 0;
        //                        break;
        //                    case 2:
        //                        this.dockPanel5_ClassBreak.Close();
        //                        CurrentThemeType = 0;
        //                        break;
        //                    case 4:
        //                        this.dockPanel5_StatisticTheme.Close();
        //                        CurrentThemeType = 0;
        //                        break;
        //                    default:
        //                        break;
        //                }
        //            }
        //        }
        //        if (CurrentThemeType == 3 || CurrentThemeType == 0)
        //        {
        //            //遍历信息树，加载shapefile图层名
        //            int current = Program.sgworld.ProjectTree.GetNextItem(0, ItemCode.ROOT);
        //            if (current > 0)
        //            {
        //                //清除单值面板已有数据
        //                this.comboBoxLayer3.Items.Clear();
        //                this.comboBoxValue3.Items.Clear();
        //                this.comboBoxValue3.Text = "";
        //                this.checkVisibility.Checked = true;
        //                this.comboBoxFontSize.Text = "8";
        //                this.pictureBoxFontColor.BackColor = System.Drawing.Color.Red;
        //                this.pictureBoxBackColor.BackColor = System.Drawing.Color.Transparent;
        //                //加载字体,默认宋体
        //                this.comboBoxFont.Items.Clear();
        //                System.Drawing.Text.InstalledFontCollection MyFonts = new System.Drawing.Text.InstalledFontCollection();
        //                foreach (FontFamily currFont in MyFonts.Families)
        //                {
        //                    this.comboBoxFont.Items.Add(currFont.Name);
        //                }
        //                this.comboBoxFont.Text = "宋体";
        //                //文本框图像清空
        //                this.txtBoxFontFrame.Text = "";
        //                this.comboBoxMltJusification.Text = "居中";
        //                this.checkBold.Checked = false;
        //                this.checkItalic.Checked = false;
        //                this.checkUnderline.Checked = false;
        //                this.txtBoxImage.Text = "";
        //                this.comboBoxTextOnImage.Text = "图像之上";
        //                this.comboBoxTextAlignment.Text = "中下";

        //                ShpLyrID.Clear();
        //                ScanTree(current, 3);
        //                if (ShpLyrID.Count > 0)
        //                {
        //                    CurrentThemeType = 3;
        //                    this.comboBoxLayer3.Text = "请选择图层!";
        //                    //显示单值专题面板，打开面板时应判断是否有其他专题面板同时打开，若有则关闭？
        //                    this.dockPanel5_LabelTheme.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        //                }
        //                else
        //                    MessageBox.Show("请先添加矢量图层!");
        //            }
        //            else
        //                MessageBox.Show("请先添加图层!");
        //        }

        //    }
        //    catch 
        //    {
        //        MessageBox.Show("窗体加载遇到问题！");
        //    }

        //}
        private void pictureBoxFontColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = true;
            cd.Color = this.pictureBoxFontColor.BackColor;
            cd.ShowDialog();
            this.pictureBoxFontColor.BackColor = cd.Color;
        }
        private void pictureBoxBackColor_Click_1(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = true;
            cd.Color = this.pictureBoxBackColor.BackColor;
            cd.ShowDialog();
            this.pictureBoxBackColor.BackColor = cd.Color;
        }
        private void comboBoxLayer3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //根据选择的图层加载相应的属性字段
            //其余设置恢复默认设置
            SaveOrNot_Label = false;
            this.comboBoxValue3.Items.Clear();
            this.comboBoxValue3.Text = "";
            this.checkVisibility.Checked = true;
            this.comboBoxFontSize.Text = "8";
            this.pictureBoxFontColor.BackColor = System.Drawing.Color.Red;
            this.pictureBoxBackColor.BackColor = System.Drawing.Color.Transparent;
            //加载字体,默认宋体
            //this.comboBoxFont.Items.Clear();
            //System.Drawing.Text.InstalledFontCollection MyFonts = new System.Drawing.Text.InstalledFontCollection();
            //foreach (FontFamily currFont in MyFonts.Families)
            //{
            //    this.comboBoxFont.Items.Add(currFont.Name);
            //}
            this.comboBoxFont.Text = "宋体";
            //文本框图像清空
            this.txtBoxFontFrame.Text = "";
            this.comboBoxMltJusification.Text = "居中";
            this.checkBold.Checked = false;
            this.checkItalic.Checked = false;
            this.checkUnderline.Checked = false;
            this.txtBoxImage.Text = "";
            this.comboBoxTextOnImage.Text = "图像之上";
            this.comboBoxTextAlignment.Text = "中下";
            try
            {
                if (ShpLyrID.Count > 0)
                {
                    ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer3.SelectedIndex]);
                    IDataSourceInfo61 SlctdLyrDataSource = SlctdLyr.DataSourceInfo;
                    if (SlctdLyrDataSource.Attributes.Count > 0)
                    {
                        SlctdLyrDataSource.Attributes.ImportAll = true;
                        foreach (IAttribute61 Attribute in SlctdLyrDataSource.Attributes)
                        {
                            this.comboBoxValue3.Items.Add(Attribute.Name);
                        }

                        if (this.comboBoxLayer3.Items.Count > 0)
                            this.comboBoxValue3.Text = "请选择字段！";
                        else
                            this.comboBoxValue3.Text = "无合适字段！";
                    }
                    else
                        MessageBox.Show("该要素无属性！");
                }
            }
            catch
            {
                MessageBox.Show("切换图层遇到问题！");
            }

        }
        private void buttonFontFrame_Click(object sender, EventArgs e)//选择文本框图像
        {
            try
            {
                OpenFileDialog FontFrame = new OpenFileDialog();
                FontFrame.Filter = "Image(*.bmp;*.gif)|*.bmp;*.gif|BMP (*.bmp)|*.bmp|GIF (*.gif)|*.gif";
                FontFrame.Title = "选择文本框图像";
                DialogResult dr = FontFrame.ShowDialog();
                if (dr == DialogResult.OK)
                    this.txtBoxFontFrame.Text = FontFrame.FileName;
            }
            catch
            {
                MessageBox.Show("字体设置不正确！");
            }

        }
        private void btnImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog BackImage = new OpenFileDialog();
                BackImage.Filter = "Image(*.bmp;*.gif;*.jpg;*.jpeg;*.png;*.ico)|*.bmp;*.gif;*.jpg;*.jpeg;*.png;*.ico|" +
                                   "BMP (*.bmp)|*.bmp|GIF (*.gif)|*.gif|JPG (*.jpg)|*.jpg|JPEG (*.jpeg)|*.jpeg|PNG (*.png)|*.png|ICO (*.ico)|*.ico";
                BackImage.Title = "选择图像";
                DialogResult dr = BackImage.ShowDialog();
                if (dr == DialogResult.OK)
                    this.txtBoxImage.Text = BackImage.FileName;
            }
            catch
            {
                MessageBox.Show("图像设置不正确！");
            }

        }
        private bool SaveOrNot_Label = false;
        //private void simpleButtonCancel3_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!SaveOrNot_Label)
        //        {
        //            if (this.comboBoxLayer3.Text != "" & this.comboBoxLayer3.Text != "请选择图层")
        //            {
        //                int ItemID = IfExistTheme("标签专题_" + this.comboBoxLayer3.Text);
        //                if (ItemID > 0)
        //                {
        //                    //保存后关闭？
        //                    DialogResult dr = MessageBox.Show("是否保存当前专题？", "关闭提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        //                    if (dr == System.Windows.Forms.DialogResult.Yes)
        //                    {
        //                        Program.sgworld.Project.Save();
        //                        SaveOrNot_Label = true;
        //                        CurrentThemeType = 0;
        //                        this.dockPanel5_LabelTheme.Close();
        //                    }
        //                    else
        //                    {
        //                        if (dr == System.Windows.Forms.DialogResult.No)
        //                        {
        //                            Program.sgworld.ProjectTree.DeleteItem(ItemID);
        //                            CurrentThemeType = 0;
        //                            this.dockPanel5_LabelTheme.Close();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        MessageBox.Show("窗口关闭遇到问题！");
        //    }
        //}
        private void simpleButtonOK3_Click(object sender, EventArgs e)
        {
            try
            {
                //保存对属性图层的改变
                if (this.comboBoxLayer3.SelectedIndex >= 0)
                {
                    ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer3.SelectedIndex]);
                    SlctdLyr.Save();
                    Program.sgworld.Project.Save();
                    SaveOrNot_Label = true;
                    MessageBox.Show("保存成功！");
                }
            }
            catch
            {
                MessageBox.Show("专题图保存遇到问题！");
            }
        }
        private void comboBoxValue3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //判断是否已存在该图层的专题标签，若不存在则创建，若存在则仅修改Text属性，无需重新建立
                int GroupID = IfExistTheme("标签专题_" + this.comboBoxLayer3.Text);
                if (GroupID < 0)
                    this.CreateLabelTheme(this.comboBoxLayer3.Text);
                else
                    this.ModifyTextAttribute(GroupID);
            }
            catch
            {
                MessageBox.Show("生成专题图遇到问题！");
            }

        }
        private void CreateLabelTheme(string LayerName)
        {
            
                //创建一个组
                string GroupName = "标签专题_" + LayerName;
                int GroupID = Program.sgworld.ProjectTree.CreateGroup(GroupName);
                //获得每个要素的中心点坐标及要显示的字段值
                List<IPosition61> Positions = new List<IPosition61>();
                List<string> FieldValue = new List<string>();
                GetPosiionAndValue(ref Positions, ref FieldValue, GroupID);
                //MessageBox.Show("Pos:" + Positions.Count.ToString() + "Values:" + FieldValue.Count.ToString());
                //根据设置在每个中心点坐标上CreatLabel
                ILabelStyle61 cLabelStyle = null;
                ITerrainLabel61 cTextLabel = null;
                //根据设置创建位置，暂略，单独选择每个标签应能在标签窗口显示其位置？放在Modify函数里，此处为默认创建专题标签

                //根据设置创建标签风格
                #region
                SGLabelStyle eLabelStyle = SGLabelStyle.LS_DEFAULT;
                cLabelStyle = Program.sgworld.Creator.CreateLabelStyle(eLabelStyle);
                //字号
                cLabelStyle.FontSize = Convert.ToInt32(this.comboBoxFontSize.Text);
                //字体
                cLabelStyle.FontName = this.comboBoxFont.Text;
                //文本颜色
                cLabelStyle.TextColor = Program.sgworld.Creator.CreateColor(pictureBoxFontColor.BackColor.R,
                                        pictureBoxFontColor.BackColor.G, pictureBoxFontColor.BackColor.B, 255);
                //背景颜色
                //cLabelStyle.BackgroundColor = Program.sgworld.Creator.CreateColor(pictureBoxBackColor.BackColor.R,
                //                        pictureBoxBackColor.BackColor.G, pictureBoxBackColor.BackColor.B, 0);
                if (pictureBoxBackColor.BackColor == Color.Transparent)
                    cLabelStyle.BackgroundColor = Program.sgworld.Creator.CreateColor(pictureBoxBackColor.BackColor.R,
                                                  pictureBoxBackColor.BackColor.G, pictureBoxBackColor.BackColor.B, 0);
                else
                    cLabelStyle.BackgroundColor = Program.sgworld.Creator.CreateColor(pictureBoxBackColor.BackColor.R,
                                                  pictureBoxBackColor.BackColor.G, pictureBoxBackColor.BackColor.B, 255);
                //文本框图像
                if (txtBoxFontFrame.Text != "")
                    cLabelStyle.FrameFileName = txtBoxFontFrame.Text;
                //多行对齐
                switch (this.comboBoxMltJusification.Text)
                {
                    case "左对齐":
                        cLabelStyle.MultilineJustification = "Left";
                        break;
                    case "居中":
                        cLabelStyle.MultilineJustification = "Center";
                        break;
                    case "右对齐":
                        cLabelStyle.MultilineJustification = "Right";
                        break;
                    default:
                        break;
                }
                //文本效果
                //加粗
                if (this.checkBold.Checked)
                    cLabelStyle.Bold = true;
                else
                    cLabelStyle.Bold = false;
                //倾斜
                if (this.checkItalic.Checked)
                    cLabelStyle.Italic = true;
                else
                    cLabelStyle.Italic = false;
                //下划线
                if (this.checkUnderline.Checked)
                    cLabelStyle.Underline = true;
                else
                    cLabelStyle.Underline = false;
                //文本与图像
                //文本相对图像位置
                if (this.comboBoxTextOnImage.Text == "图像之上")
                    cLabelStyle.TextOnImage = true;
                else
                    cLabelStyle.TextOnImage = false;
                //文本相对图像对齐
                switch (this.comboBoxTextAlignment.Text)
                {
                    case "左上":
                        cLabelStyle.TextAlignment = "TopLeft";
                        break;
                    case "中上":
                        cLabelStyle.TextAlignment = "Top";
                        break;
                    case "右上":
                        cLabelStyle.TextAlignment = "TopRight";
                        break;
                    case "左中":
                        cLabelStyle.TextAlignment = "Left";
                        break;
                    case "居中":
                        cLabelStyle.TextAlignment = "Center";
                        break;
                    case "右中":
                        cLabelStyle.TextAlignment = "Right";
                        break;
                    case "左下":
                        cLabelStyle.TextAlignment = "BottomLeft";
                        break;
                    case "中下":
                        cLabelStyle.TextAlignment = "Bottom";
                        break;
                    case "右下":
                        cLabelStyle.TextAlignment = "RightBottom";
                        break;
                    default:
                        break;
                }
                #endregion
                //创建Label
                if (Positions.Count > 0)
                {
                    for (int i = 0; i < Positions.Count; i++)
                    {
                        cTextLabel = Program.sgworld.Creator.CreateTextLabel(Positions[i], FieldValue[i], cLabelStyle, GroupID, FieldValue[i]);
                        //图像Label
                        if (this.txtBoxImage.Text != "")
                            cTextLabel.ImageFileName = this.txtBoxImage.Text;
                    }
                }
                else
                    MessageBox.Show("图层中不包含任何要素!");
        }
        //遍历信息树判断是否已存在标签专题组，若存在则返回该组的ItemID
        private int IfExistTheme(string GroupName)
        {
            int GroupID = -1;
            int current = Program.sgworld.ProjectTree.GetNextItem(0, ItemCode.ROOT);
            while (current > 0)
            {
                if (Program.sgworld.ProjectTree.IsGroup(current))
                {
                    if (Program.sgworld.ProjectTree.GetItemName(current) == GroupName)
                    {
                        GroupID = current;
                        break;
                    }
                }
                current = Program.sgworld.ProjectTree.GetNextItem(current, ItemCode.NEXT);
            }
            return GroupID;
        }
        private void GetPosiionAndValue(ref List<IPosition61> Positions, ref List<string> Values, int GroupID)
        {
            ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer3.SelectedIndex]);
            IPolygon pBound= Program.sgworld.Creator.GeometryCreator.CreatePolygonGeometry(new double[] {
                SlctdLyr.BBox.MinX,SlctdLyr.BBox.MinY,0,
                SlctdLyr.BBox.MinX,SlctdLyr.BBox.MaxY,0,
                SlctdLyr.BBox.MaxX,SlctdLyr.BBox.MaxY,0,
                SlctdLyr.BBox.MaxX,SlctdLyr.BBox.MinY,0
            });

            double dAltitude = 0;
            AltitudeTypeCode eAltitudeTypeCode = AltitudeTypeCode.ATC_ON_TERRAIN;
            double dYaw = 0.0;
            double dPitch = 0.0;
            double dRoll = 0.0;
            double dDistance = 20000;

            IFeatureGroup61 fGroup=SlctdLyr.FeatureGroups.Point;
            if (SlctdLyr.GeometryType== LayerGeometryType.LGT_POLYLINE)
                fGroup=SlctdLyr.FeatureGroups.Polyline;
            else if(SlctdLyr.GeometryType== LayerGeometryType.LGT_POLYGON)
                fGroup=SlctdLyr.FeatureGroups.Polygon;

           
            for(int i=0;i<fGroup.Count;i++)
            {
                IFeature61 fCurrent = fGroup[i] as IFeature61;

                IGeometry geo = fCurrent.Geometry;
                IPoint pCenter = null;
                switch (SlctdLyr.GeometryType)
                {
                    case LayerGeometryType.LGT_POINT:
                        pCenter = geo as IPoint;
                        break;

                    case LayerGeometryType.LGT_POLYLINE:
                        pCenter = (geo.Envelope as IPolygon).PointOnSurface;
                        break;

                    case LayerGeometryType.LGT_POLYGON:
                        pCenter = (geo as IPolygon).PointOnSurface;
                        break;

                    default:
                        continue;
                }
                Values.Add(fCurrent.FeatureAttributes.GetFeatureAttribute(this.comboBoxValue3.Text).Value as string);
                Positions.Add(Program.sgworld.Creator.CreatePosition(pCenter.X, pCenter.Y, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance));
            }
        }
        //private void GetPosiionAndValue(ref List<IPosition61> Positions, ref List<string> Values, int GroupID)
        //{
           
        //        ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer3.SelectedIndex]);
        //        string[] ExtractFile = SlctdLyr.DataSourceInfo.ConnectionString.Split(';');
        //        string FilePath = ExtractFile[0].Substring(9);

        //        IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
        //        IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)pWorkspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(FilePath), 0);
        //        IFeatureClass pFeatureClass = (IFeatureClass)pFeatureWorkspace.OpenFeatureClass(System.IO.Path.GetFileName(FilePath));

        //        bool ReProjectOrNot = false;//标记是否需要重投影
        //        string LayerCord = SlctdLyr.CoordinateSystem.WktDescription;
        //        string TerrainCord = Program.sgworld.Terrain.CoordinateSystem.WktDescription;
        //        if (LayerCord != TerrainCord)
        //            ReProjectOrNot = true;

        //        string AValue = "";
        //        IPosition61 cPos = null;
        //        double dXCoord;
        //        double dYCoord;
        //        double dAltitude = 0;
        //        AltitudeTypeCode eAltitudeTypeCode = AltitudeTypeCode.ATC_ON_TERRAIN;
        //        double dYaw = 0.0;
        //        double dPitch = 0.0;
        //        double dRoll = 0.0;
        //        double dDistance = 20000;
        //        for (int i = 0; i < pFeatureClass.FeatureCount(null); i++)
        //        {
        //            //获取值,若为浮点型且小数位数超过5位，则四舍五入到五位
        //            int Index = pFeatureClass.FindField(this.comboBoxValue3.Text);
        //            AValue = pFeatureClass.GetFeature(i).get_Value(Index).ToString();
        //            if (pFeatureClass.Fields.get_Field(Index).Type == esriFieldType.esriFieldTypeDouble)
        //            {
        //                string[] strArr = AValue.Split('.');
        //                int precision = strArr[strArr.Length - 1].Length;
        //                if (precision > 5)
        //                    AValue = Math.Round(Convert.ToDecimal(Convert.ToDouble(AValue)), 5).ToString();

        //            }
        //            Values.Add(AValue);
        //            //获取位置
        //            ESRI.ArcGIS.Geometry.IGeometry pGeometry = pFeatureClass.GetFeature(i).Shape;
        //            if (ReProjectOrNot)
        //            {
        //                ESRI.ArcGIS.Geometry.ISpatialReferenceFactory srFactory = new ESRI.ArcGIS.Geometry.SpatialReferenceEnvironmentClass();
        //                ESRI.ArcGIS.Geometry.IGeographicCoordinateSystem gcs = srFactory.CreateGeographicCoordinateSystem((int)ESRI.ArcGIS.Geometry.esriSRGeoCSType.esriSRGeoCS_WGS1984);
        //                ESRI.ArcGIS.Geometry.ISpatialReference sr1 = gcs;
        //                pGeometry.Project(sr1);
        //            }
        //            ESRI.ArcGIS.Geometry.IPoint pPoint = null;
        //            switch (pFeatureClass.ShapeType)
        //            {
        //                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
        //                    pPoint = (ESRI.ArcGIS.Geometry.IPoint)pGeometry;

        //                    break;
        //                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
        //                    //pPoint = ((ESRI.ArcGIS.Geometry.IPolyline)pGeometry).FromPoint;
        //                    pPoint = ((ESRI.ArcGIS.Geometry.IPolyline)pGeometry).ToPoint;
        //                    break;
        //                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
        //                    //pPoint = ((ESRI.ArcGIS.Geometry.IArea)(ESRI.ArcGIS.Geometry.IPolygon)pGeometry).Centroid;
        //                    pPoint = ((ESRI.ArcGIS.Geometry.IArea)(ESRI.ArcGIS.Geometry.IPolygon)pGeometry).LabelPoint;
        //                    break;
        //                default:
        //                    break;
        //            }
        //            dXCoord = pPoint.X;
        //            dYCoord = pPoint.Y;
        //            cPos = Program.sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);
        //            Positions.Add(cPos);
        //        }
        //}
        private void ModifyAttribute(string AttriName, int GroupID)
        {
           
                if (AttriName == "Visibility")//修改可见性
                {
                    Program.sgworld.ProjectTree.SetVisibility(GroupID, this.checkVisibility.Checked);
                }
                else
                {
                    int current = Program.sgworld.ProjectTree.GetNextItem(GroupID, ItemCode.CHILD);
                    while (current > 0)
                    {
                        ITerrainLabel61 ALabel = Program.sgworld.ProjectTree.GetObject(current) as ITerrainLabel61;
                        switch (AttriName)
                        {
                            case "FontSize"://字号
                                ALabel.Style.FontSize = Convert.ToInt32(this.comboBoxFontSize.Text);
                                break;
                            case "FontName"://字体
                                ALabel.Style.FontName = this.comboBoxFont.Text;
                                break;
                            case "TextColor"://文本颜色
                                ALabel.Style.TextColor = Program.sgworld.Creator.CreateColor(this.pictureBoxFontColor.BackColor.R, this.pictureBoxFontColor.BackColor.G,
                                                                                             this.pictureBoxFontColor.BackColor.B, this.pictureBoxFontColor.BackColor.A);
                                break;
                            case "BackgroundColor"://文本背景颜色
                                ALabel.Style.BackgroundColor = Program.sgworld.Creator.CreateColor(this.pictureBoxBackColor.BackColor.R, this.pictureBoxBackColor.BackColor.G,
                                                                                             this.pictureBoxBackColor.BackColor.B, this.pictureBoxBackColor.BackColor.A);
                                break;
                            case "FrameFileName"://文本框图像
                                ALabel.Style.FrameFileName = this.txtBoxFontFrame.Text;
                                break;
                            case "MultilineJustification"://多行对齐
                                #region
                                switch (this.comboBoxMltJusification.Text)
                                {
                                    case "左对齐":
                                        ALabel.Style.MultilineJustification = "Left";
                                        break;
                                    case "居中":
                                        ALabel.Style.MultilineJustification = "Center";
                                        break;
                                    case "右对齐":
                                        ALabel.Style.MultilineJustification = "Right";
                                        break;
                                    default:
                                        break;
                                }
                                #endregion
                                break;
                            case "Bold"://加粗
                                ALabel.Style.Bold = this.checkBold.Checked;
                                break;
                            case "Italic"://倾斜
                                ALabel.Style.Italic = this.checkItalic.Checked;
                                break;
                            case "Underline"://下划线
                                ALabel.Style.Underline = this.checkUnderline.Checked;
                                break;
                            case "ImageFileName"://图像
                                ALabel.ImageFileName = this.txtBoxImage.Text;
                                break;
                            case "TextOnImage"://文本相对图像位置
                                if (this.comboBoxTextOnImage.Text == "图像之上")
                                    ALabel.Style.TextOnImage = true;
                                else
                                    ALabel.Style.TextOnImage = false;
                                break;
                            case "TextAlignment"://文本相对图像对齐
                                #region
                                switch (this.comboBoxTextAlignment.Text)
                                {
                                    case "左上":
                                        ALabel.Style.TextAlignment = "TopLeft";
                                        break;
                                    case "中上":
                                        ALabel.Style.TextAlignment = "Top";
                                        break;
                                    case "右上":
                                        ALabel.Style.TextAlignment = "TopRight";
                                        break;
                                    case "左中":
                                        ALabel.Style.TextAlignment = "Left";
                                        break;
                                    case "居中":
                                        ALabel.Style.TextAlignment = "Center";
                                        break;
                                    case "右中":
                                        ALabel.Style.TextAlignment = "Right";
                                        break;
                                    case "左下":
                                        ALabel.Style.TextAlignment = "BottomLeft";
                                        break;
                                    case "中下":
                                        ALabel.Style.TextAlignment = "Bottom";
                                        break;
                                    case "右下":
                                        ALabel.Style.TextAlignment = "RightBottom";
                                        break;
                                    default:
                                        break;
                                }
                                #endregion
                                break;
                        }
                        current = Program.sgworld.ProjectTree.GetNextItem(current, ItemCode.NEXT);
                    }
                }
        }
        private void ModifyTextAttribute(int GroupID)
        {
            
                //获得每个要素的中心点坐标及要显示的字段值
                List<IPosition61> Positions = new List<IPosition61>();
                List<string> FieldValue = new List<string>();
                //获取当前LabelStyle，ImageFileName，Visibility属性
                GetPosiionAndValue(ref Positions, ref FieldValue, GroupID);
                int FirstChild = Program.sgworld.ProjectTree.GetNextItem(GroupID, ItemCode.CHILD);
                ILabelStyle61 LabelStyle = (Program.sgworld.ProjectTree.GetObject(FirstChild) as ITerrainLabel61).Style;
                string ImageFile = (Program.sgworld.ProjectTree.GetObject(FirstChild) as ITerrainLabel61).ImageFileName;
                //删除原Label
                while (FirstChild > 0)
                {
                    Program.sgworld.ProjectTree.DeleteItem(FirstChild);
                    FirstChild = Program.sgworld.ProjectTree.GetNextItem(GroupID, ItemCode.CHILD);
                }
                //重新建立Label
                ITerrainLabel61 cTextLabel = null;
                if (Positions.Count > 0)
                {
                    for (int i = 0; i < Positions.Count; i++)
                    {
                        cTextLabel = Program.sgworld.Creator.CreateTextLabel(Positions[i], FieldValue[i], LabelStyle, GroupID, FieldValue[i]);
                        //图像Label
                        cTextLabel.ImageFileName = ImageFile;
                    }
                    this.checkVisibility.Checked = true;
                }
                else
                    MessageBox.Show("该图层不包含任何要素!");
        }
        private void checkVisibility_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int GroupID = IfExistTheme("标签专题_" + this.comboBoxLayer3.Text);
                if (GroupID >= 0)
                    this.ModifyAttribute("Visibility", GroupID);
            }
            catch
            {
                MessageBox.Show("修改标签专题遇到问题！");
            }
        }
        private void comboBoxFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int GroupID = IfExistTheme("标签专题_" + this.comboBoxLayer3.Text);
                if (GroupID >= 0)
                    this.ModifyAttribute("FontSize", GroupID);
            }
            catch
            {
                MessageBox.Show("修改标签专题图遇到问题！");
            }
        }
        private void comboBoxFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int GroupID = IfExistTheme("标签专题_" + this.comboBoxLayer3.Text);
                if (GroupID >= 0)
                    this.ModifyAttribute("FontName", GroupID);
            }
            catch
            {
                MessageBox.Show("修改标签专题图遇到问题！");
            }
        }
        private void pictureBoxFontColor_BackColorChanged(object sender, EventArgs e)
        {
            try
            {
                int GroupID = IfExistTheme("标签专题_" + this.comboBoxLayer3.Text);
                if (GroupID >= 0)
                    this.ModifyAttribute("TextColor", GroupID);
            }
            catch
            {
                MessageBox.Show("修改标签专题图遇到问题！");
            }
        }
        private void pictureBoxBackColor_BackColorChanged(object sender, EventArgs e)
        {
            try
            {
                int GroupID = IfExistTheme("标签专题_" + this.comboBoxLayer3.Text);
                if (GroupID >= 0)
                    this.ModifyAttribute("BackgroundColor", GroupID);
            }
            catch
            {
                MessageBox.Show("修改标签专题图遇到问题！");
            }
        }
        private void txtBoxFontFrame_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int GroupID = IfExistTheme("标签专题_" + this.comboBoxLayer3.Text);
                if (GroupID >= 0)
                    this.ModifyAttribute("FrameFileName", GroupID);
            }
            catch
            {
                MessageBox.Show("修改标签专题图遇到问题！");
            }
        }
        private void comboBoxMltJusification_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int GroupID = IfExistTheme("标签专题_" + this.comboBoxLayer3.Text);
                if (GroupID >= 0)
                    this.ModifyAttribute("MultilineJustification", GroupID);
            }
            catch
            {
                MessageBox.Show("修改标签专题图遇到问题！");
            }
        }
        private void checkBold_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int GroupID = IfExistTheme("标签专题_" + this.comboBoxLayer3.Text);
                if (GroupID >= 0)
                    this.ModifyAttribute("Bold", GroupID);
            }
            catch
            {
                MessageBox.Show("修改标签专题图遇到问题！");
            }
        }
        private void checkUnderline_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int GroupID = IfExistTheme("标签专题_" + this.comboBoxLayer3.Text);
                if (GroupID >= 0)
                    this.ModifyAttribute("Underline", GroupID);
            }
            catch
            {
                MessageBox.Show("修改标签专题图遇到问题！");
            }
        }
        private void checkItalic_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int GroupID = IfExistTheme("标签专题_" + this.comboBoxLayer3.Text);
                if (GroupID >= 0)
                    this.ModifyAttribute("Italic", GroupID);
            }
            catch
            {
                MessageBox.Show("修改标签专题图遇到问题！");
            }
        }
        private void txtBoxImage_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int GroupID = IfExistTheme("标签专题_" + this.comboBoxLayer3.Text);
                if (GroupID >= 0)
                    this.ModifyAttribute("ImageFileName", GroupID);
            }
            catch
            {
                MessageBox.Show("修改标签专题图遇到问题！");
            }
        }
        private void comboBoxTextAlignment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int GroupID = IfExistTheme("标签专题_" + this.comboBoxLayer3.Text);
                if (GroupID >= 0)
                    this.ModifyAttribute("TextAlignment", GroupID);
            }
            catch
            {
                MessageBox.Show("修改标签专题图遇到问题！");
            }
        }
        private void comboBoxTextOnImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int GroupID = IfExistTheme("标签专题_" + this.comboBoxLayer3.Text);
                if (GroupID >= 0)
                    this.ModifyAttribute("TextOnImage", GroupID);
            }
            catch
            {
                MessageBox.Show("修改标签专题图遇到问题！");
            }
        }
        private void dockPanel5_LabelTheme_ClosingPanel(object sender, DevExpress.XtraBars.Docking.DockPanelCancelEventArgs e)
        {
            try
            {
                if (CurrentThemeType != 0)
                {
                    if (!SaveOrNot_Label)
                    {
                        if (this.comboBoxLayer3.Text != "" & this.comboBoxLayer3.Text != "请选择图层")
                        {
                            int ItemID = IfExistTheme("标签专题_" + this.comboBoxLayer3.Text);
                            if (ItemID > 0)
                            {
                                //保存后关闭？
                                DialogResult dr = MessageBox.Show("是否保存当前专题？", "关闭提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                if (dr == System.Windows.Forms.DialogResult.Yes)
                                {
                                    Program.sgworld.Project.Save();
                                    SaveOrNot_Label = true;
                                    CurrentThemeType = 0;
                                }
                                else
                                {
                                    if (dr == System.Windows.Forms.DialogResult.No)
                                    {
                                        Program.sgworld.ProjectTree.DeleteItem(ItemID);
                                        CurrentThemeType = 0;
                                    }
                                    else
                                        e.Cancel = true;
                                }
                            }
                        }
                    }

                }
            }
            catch
            {
                MessageBox.Show("关闭遇到问题！");
            }
        }
        #endregion

        #region 统计专题
        //private void StatisticTheme_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        if (CurrentThemeType != 0 & CurrentThemeType != 4)
        //        {
        //            //关闭当前面板
        //            DialogResult dr = MessageBox.Show("关闭当前专题面板？", "关闭提示", MessageBoxButtons.OKCancel);
        //            //保存后关闭？
        //            //DialogResult dr = MessageBox.Show("是否保存当前专题？", "关闭提示", MessageBoxButtons.OKCancel);
        //            if (dr == DialogResult.OK)
        //            {
        //                switch (CurrentThemeType)
        //                {
        //                    case 1:
        //                        this.dockPanel5_SimpleTheme.Close();
        //                        CurrentThemeType = 0;
        //                        break;
        //                    case 2:
        //                        this.dockPanel5_ClassBreak.Close();
        //                        CurrentThemeType = 0;
        //                        break;
        //                    case 3:
        //                        this.dockPanel5_LabelTheme.Close();
        //                        CurrentThemeType = 0;
        //                        break;
        //                    default:
        //                        break;
        //                }
        //            }
        //        }
        //        if (CurrentThemeType == 4 || CurrentThemeType == 0)
        //        {
        //            int current = Program.sgworld.ProjectTree.GetNextItem(0, ItemCode.ROOT);
        //            if (current >= 0)
        //            {
        //                //清除统计面板已有数据，恢复默认设置
        //                this.comboBoxLayer4.Items.Clear();
        //                this.comboBoxChartType.Text = "";
        //                this.comboBoxChartTitle.Items.Clear();
        //                this.comboBoxChartTitle.Text = "";//不选则默认为ID字段
        //                this.pictureBoxStartCLR2.BackColor = Color.Blue;
        //                this.pictureBoxEndCLR2.BackColor = Color.Red;
        //                this.dataGridView3.Rows.Clear();
        //                this.dataGridView4.Rows.Clear();
        //                this.dataGridView4.Columns.Clear();
        //                // this.checkLimitGrowth.Checked = true;
        //                this.checkPercent.Checked = false;
        //                this.checkPercent.Visible = false;
        //                this.progressBarControl1.Position = 0;
        //                //遍历信息树，加载shapefile图层名

        //                ShpLyrID.Clear();
        //                ScanTree(current, 4);
        //                if (ShpLyrID.Count > 0)
        //                {
        //                    CurrentThemeType = 4;
        //                    this.comboBoxLayer4.Text = "请选择图层!";
        //                    //显示统计专题面板
        //                    this.dockPanel5_StatisticTheme.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        //                }
        //                else
        //                    MessageBox.Show("请先添加矢量图层!");
        //            }
        //            else
        //                MessageBox.Show("请先添加图层");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("窗体加载遇到问题！");
        //    }
        //}
        private void comboBoxLayer4_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBoxChartType.Text = "";
            this.comboBoxChartTitle.Items.Clear();
            this.comboBoxChartTitle.Text = "";//不选则默认为ID字段
            this.pictureBoxStartCLR2.BackColor = Color.Blue;
            this.pictureBoxEndCLR2.BackColor = Color.Red;
            this.dataGridView3.Rows.Clear();
            this.dataGridView4.Rows.Clear();
            this.dataGridView4.Columns.Clear();
            // this.checkLimitGrowth.Checked = true;
            this.checkPercent.Checked = false;
            this.checkPercent.Visible = false;
            SaveOrNot_Sta = false;
            this.progressBarControl1.Position = 0;
            try
            {
                //根据选择的图层加载文本字段,为空则自动默认为ID
                if (ShpLyrID.Count > 0)
                {
                    ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer4.SelectedIndex]);
                    IDataSourceInfo61 SlctdLyrDataSource = SlctdLyr.DataSourceInfo;
                    if (SlctdLyrDataSource.Attributes.Count > 0)
                    {
                        SlctdLyrDataSource.Attributes.ImportAll = true;
                        this.comboBoxChartTitle.Items.Clear();
                        foreach (IAttribute61 Attribute in SlctdLyrDataSource.Attributes)
                        {
                            if (Attribute.Type == AttributeTypeCode.AT_TEXT)
                            {
                                this.comboBoxChartTitle.Items.Add(Attribute.Name);
                            }
                        }

                    }
                    else
                        MessageBox.Show("该要素无属性！");
                }
            }
            catch
            {
                MessageBox.Show("修改图层遇到问题！");
            }
        }
        private void comboBoxChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.progressBarControl1.Position = 0;
            if (this.comboBoxChartType.Text == "饼图")
                this.checkPercent.Visible = true;
            else
                this.checkPercent.Visible = false;
        }
        private void comboBoxChartTitle_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.progressBarControl1.Position = 0;
                if (ShpLyrID.Count > 0)
                {
                    if (this.comboBoxLayer4.Text != "" & this.comboBoxLayer4.Text != "请选择图层!")
                    {
                        if (this.dataGridView4.ColumnCount > 0)
                        {
                            if (this.comboBoxChartTitle.Text == "")
                            {
                                this.dataGridView4.Columns[0].Name = "Feature ID";
                                this.dataGridView4.Columns[0].HeaderText = "Feature ID";
                            }
                            else
                            {
                                this.dataGridView4.Columns[0].Name = this.comboBoxChartTitle.Text;
                                this.dataGridView4.Columns[0].HeaderText = this.comboBoxChartTitle.Text;
                            }

                            ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer4.SelectedIndex]);
                            IFeatureGroup61 pFeatureGroup = null;
                            switch (SlctdLyr.GeometryType)
                            {
                                case LayerGeometryType.LGT_POINT:
                                    pFeatureGroup = SlctdLyr.FeatureGroups.Point;
                                    break;
                                case LayerGeometryType.LGT_POLYLINE:
                                    pFeatureGroup = SlctdLyr.FeatureGroups.Polyline;
                                    break;
                                case LayerGeometryType.LGT_POLYGON:
                                    pFeatureGroup = SlctdLyr.FeatureGroups.Polygon;
                                    break;
                                default:
                                    break;

                            }
                            if (pFeatureGroup.Count > 0)
                            {
                                for (int i = 0; i < pFeatureGroup.Count; i++)
                                {
                                    if (this.comboBoxChartTitle.Text == "")
                                    {
                                        this.dataGridView4.Rows[i].Cells[0].Value = i;
                                    }
                                    else
                                    {
                                        IFeature61 pFeature = pFeatureGroup[i] as IFeature61;
                                        string TitleValue = pFeature.FeatureAttributes.GetFeatureAttribute(this.comboBoxChartTitle.Text).Value;
                                        this.dataGridView4.Rows[i].Cells[0].Value = TitleValue;
                                    }

                                }
                                this.dataGridView4.ClearSelection();
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("修改专题图标题遇到问题！");
            }
        }
        private void pictureBoxStartCLR2_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = true;
            cd.Color = this.pictureBoxStartCLR2.BackColor;
            cd.ShowDialog();
            this.pictureBoxStartCLR2.BackColor = cd.Color;
        }
        private void pictureBoxEndCLR2_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = true;
            cd.Color = this.pictureBoxEndCLR2.BackColor;
            cd.ShowDialog();
            this.pictureBoxEndCLR2.BackColor = cd.Color;
        }
        public FrmFieldslist myFields = null;
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.comboBoxLayer4.SelectedIndex >= 0)
                {
                    if (myFields == null)
                    {
                        myFields = new FrmFieldslist(ShpLyrID[this.comboBoxLayer4.SelectedIndex], this.comboBoxChartTitle.Text);
                        myFields.fatherform = this;
                        Point p = new Point(Control.MousePosition.X - myFields.Width, Control.MousePosition.Y);
                        myFields.Location = p;
                        myFields.Show();
                    }
                    else
                        myFields.Focus();
                }
                else
                    MessageBox.Show("请先选择图层!");
            }
            catch
            {
                MessageBox.Show("添加字段窗体加载失败！");
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                //删除选定字段，颜色不改变，点应用后修改
                if (this.dataGridView3.Rows.Count > 0)
                {
                    if (this.dataGridView3.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow a in this.dataGridView3.SelectedRows)
                        {
                            //同时删除表中相应字段
                            if (this.dataGridView4.ColumnCount == 2)
                                this.dataGridView4.Columns.Clear();
                            else
                                this.dataGridView4.Columns.Remove(a.Cells[1].Value.ToString());

                            this.dataGridView3.Rows.Remove(a);

                        }
                        this.dataGridView3.ClearSelection();
                        this.dataGridView4.ClearSelection();
                    }
                    else
                    {
                        MessageBox.Show("请先选择需删除字段");
                    }

                }
                else
                    MessageBox.Show("当前无可删除字段!");
            }
            catch
            {
                MessageBox.Show("删除字段遇到问题！");
            }
        }
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //点击‘颜色‘列单元格弹出颜色选择对话框,由用户选择颜色
                if (e.ColumnIndex == 0)
                {
                    ColorDialog cd = new ColorDialog();
                    cd.AnyColor = true;
                    cd.Color = this.dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor;
                    cd.ShowDialog();
                    this.dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = cd.Color;
                    this.dataGridView3.ClearSelection();

                }
            }
            catch
            {
                MessageBox.Show("修改单元格颜色遇到问题！");
            }

        }
        private void simpleButtonApply4_Click(object sender, EventArgs e)
        {
            try
            {
                //根据全局设置生成专题图
                if (this.comboBoxLayer4.SelectedIndex < 0)
                    MessageBox.Show("请先选择图层!");
                else
                {
                    if (this.comboBoxChartType.SelectedIndex < 0)
                        MessageBox.Show("请选择统计专题图类型！");
                    else
                    {
                        if (this.dataGridView3.RowCount == 0)
                            MessageBox.Show("请添加专题字段!");
                        else
                        {
                            //判断该图层专题图层是否已存在,若存则删除旧的再建立，若不存在则重新建立
                            int GroupID = IfExistTheme("统计专题_" + this.comboBoxLayer4.Text);
                            if (GroupID < 0)
                                this.CreateStatisticTheme(this.comboBoxLayer4.Text);
                            else
                            {
                                Program.sgworld.ProjectTree.DeleteItem(GroupID);
                                this.CreateStatisticTheme(this.comboBoxLayer4.Text);
                            }

                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("生成统计专题图遇到问题！");
            }
        }
        private bool SaveOrNot_Sta = false;
        //private void simpleButtonCancel4_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!SaveOrNot_Sta)
        //        {
        //            if (this.comboBoxLayer4.Text != "" & this.comboBoxLayer4.Text != "请选择图层")
        //            {
        //                int ItemID = IfExistTheme("统计专题_" + this.comboBoxLayer4.Text);
        //                if (ItemID > 0)
        //                {
        //                    //保存后关闭？
        //                    DialogResult dr = MessageBox.Show("是否保存当前专题？", "关闭提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        //                    if (dr == System.Windows.Forms.DialogResult.Yes)
        //                    {
        //                        Program.sgworld.Project.Save();
        //                        SaveOrNot_Sta = true;
        //                        CurrentThemeType = 0;
        //                        this.dockPanel5_StatisticTheme.Close();
        //                    }
        //                    else
        //                    {
        //                        if (dr == System.Windows.Forms.DialogResult.No)
        //                        {
        //                            Program.sgworld.ProjectTree.DeleteItem(ItemID);
        //                            CurrentThemeType = 0;
        //                            this.dockPanel5_StatisticTheme.Close();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        MessageBox.Show("关闭时遇到问题！");
        //    }
        //}
        private void simpleButtonOK4_Click(object sender, EventArgs e)
        {
            try
            {
                //保存对属性图层的改变
                if (this.comboBoxLayer4.SelectedIndex >= 0)
                {
                    ILayer61 SlctdLyr = Program.sgworld.ProjectTree.GetLayer(ShpLyrID[this.comboBoxLayer4.SelectedIndex]);
                    SlctdLyr.Save();

                    SaveOrNot_Sta = true;
                    MessageBox.Show("保存成功！");
                }
            }
            catch
            {
                MessageBox.Show("保存遇到问题！");
            }
        }
        private void CreateStatisticTheme(string LayerName)
        {
            //创建组
            string GroupName = "统计专题_" + LayerName;
            int GroupID = Program.sgworld.ProjectTree.CreateGroup(GroupName);
            //根据获取的位置及表中的值建立
           
                if (StaThemePos != null)
                {
                    this.progressBarControl1.Properties.Minimum = 0;
                    this.progressBarControl1.Properties.Maximum = StaThemePos.Count;
                    this.progressBarControl1.Properties.Step = 1;
                    this.progressBarControl1.Position = 0;
                   
                    for (int i = 0; i < StaThemePos.Count; i++)
                    {
                        //根据图表类型获取字段值根据设置生成图像
                        #region
                        ChartControl Chart3D = new ChartControl();
                        ChartTitle chartTitle1 = new ChartTitle();
                        Chart3D.Titles.Add(chartTitle1);
                        string AValue = "";
                        switch (this.comboBoxChartType.Text)
                        {
                            case "柱状图":
                                //每个字段对应一个Series，字段名以及该要素该字段的值构成一个Point
                                foreach (DataGridViewRow ARow in this.dataGridView3.Rows)
                                {
                                    Series Aseries = new Series("Aseries", ViewType.Bar3D);
                                    AValue = this.dataGridView4.Rows[i].Cells[ARow.Cells[1].Value.ToString()].Value.ToString();
                                    Aseries.Points.Add(new SeriesPoint(ARow.Cells[1].Value.ToString(), Convert.ToDouble(AValue)));
                                    Chart3D.Series.Add(Aseries);

                                    ((BarSeriesLabel)Aseries.Label).Visible = true;
                                    Font myFont = new Font(new FontFamily("宋体"),20);
                                    ((BarSeriesLabel)Aseries.Label).Font = myFont;
                                    ((BarSeriesLabel)Aseries.Label).BackColor = Color.FromArgb(0, 0, 0, 0);
                                    ((BarSeriesLabel)Aseries.Label).Border.Visible = false;
                                    ((BarSeriesLabel)Aseries.Label).TextColor = Color.Black;
                                    ((BarSeriesLabel)Aseries.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                                    Aseries.PointOptions.PointView = PointView.ArgumentAndValues;
                                    //是否勾选百分比
                                    //if (this.checkPercent.Checked)
                                    //    Aseries.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;

                                    ((Bar3DSeriesView)Aseries.View).Model = Bar3DModel.Cylinder;
                                    //修改柱状体颜色
                                    ((Bar3DSeriesView)Aseries.View).Color = ARow.Cells[0].Style.BackColor;

                                    ((XYDiagram3D)Chart3D.Diagram).AxisX.GridLines.Visible = false;
                                    ((XYDiagram3D)Chart3D.Diagram).AxisX.Label.Visible = false;
                                    ((XYDiagram3D)Chart3D.Diagram).AxisY.GridLines.Visible = false;
                                    ((XYDiagram3D)Chart3D.Diagram).AxisY.Label.Visible = false;
                                    ((XYDiagram3D)Chart3D.Diagram).AxisY.Interlaced = false;
                                    ((XYDiagram3D)Chart3D.Diagram).BackColor = Color.FromArgb(0, 0, 0, 0);
                                }
                                break;
                            case "堆叠柱状图":
                                //每个字段对应一个Series，每个Series一个Point，Argument值相同，Value值对应各字段值，起码2个Series才有堆叠效果 
                                foreach (DataGridViewRow ARow in this.dataGridView3.Rows)
                                {
                                    Series Aseries = new Series("Aseries", ViewType.StackedBar3D);
                                    AValue = this.dataGridView4.Rows[i].Cells[ARow.Cells[1].Value.ToString()].Value.ToString();
                                    Aseries.Points.Add(new SeriesPoint(this.dataGridView4.Rows[i].Cells[0].Value.ToString(), Convert.ToDouble(AValue)));
                                    Chart3D.Series.Add(Aseries);

                                    ((BarSeriesLabel)Aseries.Label).Visible = true;
                                    Font myFont = new Font(new FontFamily("宋体"), 20);
                                    ((BarSeriesLabel)Aseries.Label).Font = myFont;
                                    ((BarSeriesLabel)Aseries.Label).BackColor = Color.FromArgb(0, 0, 0, 0);
                                    ((BarSeriesLabel)Aseries.Label).Border.Visible = false;
                                    ((BarSeriesLabel)Aseries.Label).TextColor = Color.Black;
                                    ((BarSeriesLabel)Aseries.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                                    Aseries.PointOptions.PointView = PointView.ArgumentAndValues;
                                    //是否勾选百分比
                                    //if (this.checkPercent.Checked)
                                    //    Aseries.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;

                                    ((Bar3DSeriesView)Aseries.View).Model = Bar3DModel.Cylinder;
                                    //修改柱状体颜色
                                    ((Bar3DSeriesView)Aseries.View).Color = ARow.Cells[0].Style.BackColor;

                                    ((XYDiagram3D)Chart3D.Diagram).AxisX.GridLines.Visible = false;
                                    ((XYDiagram3D)Chart3D.Diagram).AxisX.Label.Visible = false;
                                    ((XYDiagram3D)Chart3D.Diagram).AxisY.GridLines.Visible = false;
                                    ((XYDiagram3D)Chart3D.Diagram).AxisY.Label.Visible = false;
                                    ((XYDiagram3D)Chart3D.Diagram).AxisY.Interlaced = false;
                                    ((XYDiagram3D)Chart3D.Diagram).BackColor = Color.FromArgb(0, 0, 0, 0);
                                }
                                break;
                            case "饼图":
                                //每个要素仅含一个Series，每个字段对应一个Point，分别由字段名和字段值组成
                                Series Aseries1 = new Series("Aseries1", ViewType.Pie3D);
                                Palette Colorlist = new Palette("Colorlist");
                                foreach (DataGridViewRow ARow in this.dataGridView3.Rows)
                                {
                                    AValue = this.dataGridView4.Rows[i].Cells[ARow.Cells[1].Value.ToString()].Value.ToString();
                                    Aseries1.Points.Add(new SeriesPoint(ARow.Cells[1].Value.ToString(), Convert.ToDouble(AValue)));
                                    //Aseries1.Points.Add(new SeriesPoint(ARow.Cells[1].Value.ToString(), 50));
                                    Colorlist.Add(ARow.Cells[0].Style.BackColor);
                                }
                                Chart3D.Series.Add(Aseries1);
                                //修改饼颜色
                                Chart3D.PaletteRepository.Add("Colorlist", Colorlist);
                                Chart3D.PaletteName = "Colorlist";
                                Chart3D.PaletteBaseColorNumber = 0;

                                ((Pie3DSeriesLabel)Aseries1.Label).Visible = true;
                                 Font myFont1 = new Font(new FontFamily("宋体"),10);
                                ((Pie3DSeriesLabel)Aseries1.Label).Font = myFont1;
                                ((Pie3DSeriesLabel)Aseries1.Label).BackColor = Color.FromArgb(0, 0, 0, 0);
                                ((Pie3DSeriesLabel)Aseries1.Label).Border.Visible = false;
                                ((Pie3DSeriesLabel)Aseries1.Label).TextColor = Color.Black;
                                ((Pie3DSeriesLabel)Aseries1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                                Aseries1.PointOptions.PointView = PointView.ArgumentAndValues;

                                //是否勾选百分比
                                if (this.checkPercent.Checked)
                                {
                                    Aseries1.PointOptions.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Percent;
                                    Aseries1.PointOptions.ValueNumericOptions.Precision = 1;
                                }

                                break;
                            default:
                                break;
                        }
                        chartTitle1.Text = this.dataGridView4.Rows[i].Cells[0].Value.ToString();
                        chartTitle1.TextColor = Color.White;
                        chartTitle1.WordWrap = true;
                        Chart3D.BackColor = Color.FromArgb(0, 0, 0, 0);
                        Chart3D.BorderOptions.Visible = false;
                        Chart3D.Size =new Size(400,400);
                        Chart3D.Legend.Visible = false;
                      
                        Chart3D.ExportToImage(Application.StartupPath + "temp_" + i + ".png", System.Drawing.Imaging.ImageFormat.Png);

                        //使图片透明
                        Bitmap oldbmp = new Bitmap(Application.StartupPath + "temp_" + i + ".png");
                        Bitmap newbmp = new Bitmap(oldbmp.Width, oldbmp.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        newbmp.MakeTransparent();
                        for (int x = 0; x != oldbmp.Width; x++)
                        {
                            for (int y = 0; y != oldbmp.Height; y++)
                            {
                                Color AColor = oldbmp.GetPixel(x, y);
                                if (!(AColor.A == 255 & AColor.R == 238 & AColor.G == 238 & AColor.B == 238))
                                {
                                    newbmp.SetPixel(x, y, Color.FromArgb(AColor.A, AColor.R, AColor.G, AColor.B));
                                }
                            }
                        }
                        newbmp.Save(Application.StartupPath + "temp2_" + i + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        oldbmp.Dispose();
                        #endregion
                        //创建Label,后删除图片
                        ILabelStyle61 cLabelStyle = null;
                        ITerrainImageLabel61 cImageLabel = null;
                        //根据设置创建标签风格
                        SGLabelStyle eLabelStyle = SGLabelStyle.LS_DEFAULT;
                        cLabelStyle = Program.sgworld.Creator.CreateLabelStyle(eLabelStyle);
                        //随图缩放
                        cLabelStyle.LimitScreenSize = true;
                        //最大可视高度
                        cLabelStyle.MaxViewingHeight = 50000;
                        //最小可视高度
                        //cLabelStyle.MinViewingHeight = 10;
                        cLabelStyle.PivotAlignment = "Bottom,Left";
                        cImageLabel = Program.sgworld.Creator.CreateImageLabel(StaThemePos[i], Application.StartupPath + "temp2_" + i + ".png", cLabelStyle, GroupID, this.dataGridView4.Rows[i].Cells[0].Value.ToString());
                        File.Delete(Application.StartupPath + "temp_" + i + ".png");
                        File.Delete(Application.StartupPath + "temp2_" + i + ".png");
                        System.Windows.Forms.Application.DoEvents();
                        this.progressBarControl1.PerformStep();
                    }
                    
                }
        }
        private void pictureBoxStartCLR2_BackColorChanged(object sender, EventArgs e)
        {
            try
            {
                //颜色改变时刷新表格里的颜色,点击"应用"后修改图上的
                if (this.dataGridView3.Rows.Count > 0)
                {
                    if (this.dataGridView3.Rows.Count == 1)
                    {
                        this.dataGridView3.Rows[0].Cells[0].Style.BackColor = pictureBoxStartCLR2.BackColor;
                        this.dataGridView3.ClearSelection();
                    }
                    else
                    {
                        List<Color> Colorlist = new List<Color>();
                        Colorlist = ProduceColors(this.pictureBoxStartCLR2.BackColor, this.pictureBoxEndCLR2.BackColor, this.dataGridView3.Rows.Count);
                        for (int m = 0; m < this.dataGridView3.Rows.Count; m++)
                        {
                            this.dataGridView3.Rows[m].Cells[0].Style.BackColor = Colorlist[m];

                        }
                        this.dataGridView3.ClearSelection();
                    }
                }
            }
            catch
            {
                MessageBox.Show("修改颜色遇到问题！");
            }
        }
        private void pictureBoxEndCLR2_BackColorChanged(object sender, EventArgs e)
        {
            try
            {
                //获取颜色分段方案
                if (this.dataGridView3.Rows.Count > 0)
                {
                    if (this.dataGridView3.Rows.Count == 1)
                    {
                        this.dataGridView3.Rows[0].Cells[0].Style.BackColor = pictureBoxEndCLR2.BackColor;
                        this.dataGridView3.ClearSelection();
                    }
                    else
                    {
                        List<Color> Colorlist = new List<Color>();
                        Colorlist = ProduceColors(this.pictureBoxStartCLR2.BackColor, this.pictureBoxEndCLR2.BackColor, this.dataGridView3.Rows.Count);
                        for (int m = 0; m < this.dataGridView3.Rows.Count; m++)
                        {
                            this.dataGridView3.Rows[m].Cells[0].Style.BackColor = Colorlist[m];

                        }
                        this.dataGridView3.ClearSelection();
                    }
                }
            }
            catch
            {
                MessageBox.Show("修改颜色遇到问题！");
            }

        }
        private void dockPanel5_StatisticTheme_ClosingPanel(object sender, DevExpress.XtraBars.Docking.DockPanelCancelEventArgs e)
        {
            try
            {
                if (CurrentThemeType != 0)
                {
                    if (!SaveOrNot_Sta)
                    {
                        if (this.comboBoxLayer4.Text != "" & this.comboBoxLayer4.Text != "请选择图层")
                        {
                            int ItemID = IfExistTheme("统计专题_" + this.comboBoxLayer4.Text);
                            if (ItemID > 0)
                            {
                                //保存后关闭？
                                DialogResult dr = MessageBox.Show("是否保存当前专题？", "关闭提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                if (dr == System.Windows.Forms.DialogResult.Yes)
                                {

                                    Program.sgworld.Project.Save();
                                    SaveOrNot_Sta = true;
                                    CurrentThemeType = 0;
                                }
                                else
                                {
                                    if (dr == System.Windows.Forms.DialogResult.No)
                                    {
                                        Program.sgworld.ProjectTree.DeleteItem(ItemID);
                                        CurrentThemeType = 0;
                                    }
                                    else
                                        e.Cancel = true;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("关闭遇到问题！");
            }
        }
        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int GroupID = IfExistTheme("统计专题_" + this.comboBoxLayer4.Text);
                if (GroupID < 0)
                    MessageBox.Show("还未创建该专题!");
                else
                {
                    if (this.dataGridView4.Rows.Count != 0)
                    {
                        string ItemID = Program.sgworld.ProjectTree.FindItem("统计专题_" + this.comboBoxLayer4.Text + @"\" + this.dataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString()).ToString();
                        Program.TE.FlyToObject(ItemID, TerraExplorerX.ActionCode.AC_FLYTO);
                    }

                }
            }
            catch
            {
                MessageBox.Show("操作遇到问题！");
            }
        }
        #endregion
        

    }
           
}
