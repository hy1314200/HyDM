using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using TerraExplorerX;

namespace Skyline.Core.UI
{
    public partial class FrmGetExtentFromFiles : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 录入点数据的数组
        /// </summary>
        private double[] cVerticesArray;

        private string pPath = "";

        public FrmGetExtentFromFiles()
        {
            InitializeComponent();
        }

        public ISGWorld61 SgWorld { private get; set; }
        public ICreator61 Creator { private get; set; }
        private void LoadFile_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Title = "加载";
            this.openFileDialog1.Filter = "ShapeFile(*.shp)|*.shp";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pPath = this.openFileDialog1.FileName;
                this.textEdit1.Text = this.pPath;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.pPath == null || this.pPath=="")
            {
                MessageBox.Show("请添加数据！","SUNZ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            try
            {
                IWorkspaceFactory pShpWorkspaceFactory = new ShapefileWorkspaceFactoryClass();

                if (pShpWorkspaceFactory.IsWorkspace(System.IO.Path.GetDirectoryName(this.pPath)))
                {
                    IWorkspace pWorkspace = pShpWorkspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(this.pPath), 0);
                    IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
                    IFeatureClass pFeatureClass = pFeatureWorkspace.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(this.pPath));
                    if (pFeatureClass.ShapeType != ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon)
                    {
                        MessageBox.Show("请导入面图层！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    int GroupID = SgWorld.ProjectTree.FindItem("区域挖开");
                    if (GroupID == 0)
                    {
                        GroupID = SgWorld.ProjectTree.CreateGroup("区域挖开", 0);

                    }
                    IFeatureCursor fc = pFeatureClass.Search(null, false);
                    IFeature pFeature = fc.NextFeature();
                    while (pFeature != null)
                    {
                        int sq = 0;
                        ESRI.ArcGIS.Geometry.IGeometry geo = pFeature.Shape;
                        ESRI.ArcGIS.Geometry.IPointCollection pPointCollection = geo as ESRI.ArcGIS.Geometry.IPointCollection;
                        this.cVerticesArray = new double[(pPointCollection.PointCount - 1) * 3];
                        for (int i = 0; i < pPointCollection.PointCount - 1; i++)
                        {
                            ESRI.ArcGIS.Geometry.IPoint pPoint = pPointCollection.get_Point(i);
                            cVerticesArray[sq] = pPoint.X;
                            sq++;
                            cVerticesArray[sq] = pPoint.Y;
                            sq++;
                            // cVerticesArray[sq] = item[2];
                            cVerticesArray[sq] = 0.1;
                            sq++;
                        }
                        ILinearRing cRing = SgWorld.Creator.GeometryCreator.CreateLinearRingGeometry(cVerticesArray);
                        IPolygon cPolygonGeometry = SgWorld.Creator.GeometryCreator.CreatePolygonGeometry(cRing, null);

                        IGeometry geoX = cPolygonGeometry as IGeometry;
                        Creator.CreateHoleOnTerrain(geoX, GroupID, "Hole" + System.Guid.NewGuid().ToString().Substring(0, 6).ToUpper());
                        pFeature = fc.NextFeature();

                        this.Hide();
                    }
                }
            }
            catch (Exception)
            {
             
            }
            
   
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}