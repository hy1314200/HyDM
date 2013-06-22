using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Text.RegularExpressions;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using Skyline.Core.Helper;
using TerraExplorerX;

namespace Skyline.Core.UI
{
    public partial class FrmTerrainProfileArrPoints : FrmBase
    {
        /// <summary>
        /// 点集合的内存表
        /// </summary>
        private DataTable PointsDt = new DataTable();
        private Form _frmMain;
        private TerraExplorerX.ISGWorld61 m_Sgworld = null;

        public FrmTerrainProfileArrPoints(TerraExplorerX.ISGWorld61 sgworld, Form frmMain)
        {

            this.m_Sgworld = sgworld;
            this._frmMain = frmMain;
            if (base.BeginForm(frmMain))
            {
                InitializeComponent();
            }
            else
            {
                this.Close();
            }
        }

        private void FrmTerrainProfileArrPoints_Load(object sender, EventArgs e)
        {
            DataColumn colX = new DataColumn("X", Type.GetType("System.Double"));
            DataColumn colY = new DataColumn("Y", Type.GetType("System.Double"));
            PointsDt.Columns.Add(colX);
            PointsDt.Columns.Add(colY);
        }

        private void AddPoint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AddCoorText();
        }
        private void AddCoorText()
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    double x, y;
                    if (Double.TryParse(textBox1.Text, out x) && Double.TryParse(textBox2.Text, out y))
                    {
                        DataRow dr = PointsDt.NewRow();
                        dr["X"] = textBox1.Text;
                        dr["Y"] = textBox2.Text;
                        PointsDt.Rows.Add(dr);

                        this.gridControl1.DataSource = PointsDt;
                        textBox1.Text = "";
                        textBox2.Text = "";
                    
                    }
                    

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("请输入文字！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);

            }

           
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (textBox2.Text != "" && textBox1.Text != "")
                    {
                        AddCoorText();
                        textBox1.Focus();
                    }

                }
            }
            catch (Exception)
            {
                
              //  throw;
            }
            
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }
        public static bool IsInt(string inString)
        {
            Regex regex = new Regex("^(-?\\d+)(");
            return regex.IsMatch(inString.Trim());
        }


        private void FlyToTeePoint_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("请输入坐标！");
                return;
            }
            double x ;
            double y ;
            if (Double.TryParse(textBox1.Text, out x) && Double.TryParse(textBox2.Text, out y))
            {

                IPosition61 position = this.m_Sgworld.Creator.CreatePosition(x, y, 1000, AltitudeTypeCode.ATC_TERRAIN_RELATIVE, 0, -89, 0, 1000);
                this.m_Sgworld.Navigate.FlyTo(position, ActionCode.AC_FLYTO);
                
            }
            else
            {
                MessageBox.Show("请输入正确格式！");
            }
           
           // creatpoint
        }

        private void AddASCFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.openFileDialog1.Title = "加载路线数据";
            this.openFileDialog1.Filter = "My file(*.txt)|*.txt|ShapeFile(*.shp)|*.shp";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.gridControl1.DataSource = null;
                if (this.PointsDt.Rows.Count != 0)
                {
                    this.PointsDt.Clear();


                }
                // int rCount = this.PointsDt.Rows.Count;
                if (this.openFileDialog1.FileName.ToLower().Contains(".txt"))
                {
                    string[] CoorPoint = File.ReadAllLines(this.openFileDialog1.FileName, Encoding.ASCII);
                    try
                    {
                        for (int j = 0; j < CoorPoint.Length; j++)
                        {
                            if (CoorPoint[j] != "" && CoorPoint[j] != null)
                            {
                                DataRow dr = PointsDt.NewRow();
                                string[] newStrArr = new string[2];
                                newStrArr = CoorPoint[j].Split(',');
                                dr["X"] = newStrArr[0];
                                dr["Y"] = newStrArr[1];
                                PointsDt.Rows.Add(dr);
                            }


                        }
                        this.gridControl1.DataSource = PointsDt;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("加载文件格式不正确！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //throw;
                    }
                }
                else if (this.openFileDialog1.FileName.ToLower().Contains(".shp"))
                {
                    try
                    {
                        IWorkspaceFactory pShpWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
                        IWorkspace pWorkspace = pShpWorkspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(this.openFileDialog1.FileName), 0);
                        IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
                        IFeatureClass pFeatureClass = pFeatureWorkspace.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(this.openFileDialog1.FileName));
                        if (pFeatureClass.ShapeType != ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline)
                        {
                            MessageBox.Show("请导入线图层！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        IFeatureCursor fc = pFeatureClass.Search(null, false);
                        IFeature pFeature = fc.NextFeature();
                        if (pFeature != null)
                        {
                            int sq = 0;
                            ESRI.ArcGIS.Geometry.IGeometry geo = pFeature.Shape;
                            ESRI.ArcGIS.Geometry.IPointCollection pPointCollection = geo as ESRI.ArcGIS.Geometry.IPointCollection;
                            for (int i = 0; i < pPointCollection.PointCount; i++)
                            {
                                ESRI.ArcGIS.Geometry.IPoint pPoint = pPointCollection.get_Point(i);
                                DataRow dr = PointsDt.NewRow();
                                dr["X"] = pPoint.X;
                                dr["Y"] = pPoint.Y;
                                PointsDt.Rows.Add(dr);

                            }
                        }
                    }
                    catch (Exception)
                    {
                      
                    }
                    if (PointsDt!=null)
                    {
                        this.gridControl1.DataSource = PointsDt;
                    }

                    
                }
                else
                {
                    return;
                }
            }
        }
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.PointsDt.Rows.Count != 0)
            {
                this.PointsDt.Clear();

            }
            this.gridControl1.DataSource = null;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (PointsDt.Rows.Count<=1)
            {
                MessageBox.Show("数据不完整,无法完成分析！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            List<double> listDouble = new List<double>();
            for (int i = 0; i < PointsDt.Rows.Count; i++)
            {
                listDouble.Add((double)PointsDt.Rows[i][0]);
                listDouble.Add((double)PointsDt.Rows[i][1]);
            }
            try
            {
                 CSharpAPIsClass CSHarp = new CSharpAPIsClass();
        
            CSharpAPIsClass.WindowInfo[] df = CSHarp.GetAllDesktopWindows();

            for (int i = 0; i < df.Length; i++)
            {
                if (df[i].szWindowName == "地形剖面分析" || df[i].szWindowName == "地形剖面")
                {
                    CSharpAPIsClass.RECT rc = new CSharpAPIsClass.RECT();

                    rc = CSharpAPIsClass.getRect(df[i].hWnd);

                    CSHarp.ToChange(df[i].hWnd, false);
                }
            }
                this.m_Sgworld.Analysis.CreateTerrainProfile(listDouble.ToArray());
            }
            catch
            {
            }
        }

        private void layoutControlItem6_Click(object sender, EventArgs e)
        {
          
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.AddCoorText();
        }

        private void FrmTerrainProfileArrPoints_FormClosing(object sender, FormClosingEventArgs e)
        {
            _frmMain.RemoveOwnedForm(this);
        }




    }
}