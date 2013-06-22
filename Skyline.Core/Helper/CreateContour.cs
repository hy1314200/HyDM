using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerraExplorerX;
using ESRI.ArcGIS.Geodatabase;
using System.IO;
using ESRI.ArcGIS.DataSourcesFile;
using System.Windows.Forms;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.ConversionTools;
using ESRI.ArcGIS.Geoprocessing;
using System.Runtime.InteropServices;

namespace Skyline.Core.Helper
{
    public class CreateContour
    {
        /// <summary>
        /// 随机Shap文件后缀名称
        /// </summary>
        private string Random = "";
        /// <summary>
        /// 采样间隔
        /// </summary>
        private double Interval = 10;


        /// <summary>
        /// 创建等高线*.shp文件的主方法
        /// </summary>
        /// <param name="lux">左上投影x坐标</param>
        /// <param name="luy">左上投影y坐标</param>
        /// <param name="luz">左上投影z坐标</param>
        /// <param name="rlx">右下投影x坐标</param>
        /// <param name="rly">右下投影y坐标</param>
        /// <param name="rlz">右下投影z坐标</param>
        /// <param name="m_interval">采样间隔</param>
        /// <returns>等高线*.shp文件的文件名</returns>
        public string CreateContourShape(ISGWorld61 sgworld, double lux, double luy, double luz, double rlx, double rly, double rlz,double m_interval)
        {
            double width = sgworld.CoordServices.GetDistance(lux, luy, rlx, luy);
            double hight = sgworld.CoordServices.GetDistance(lux, luy, lux, rly);
            if (width<5||hight<10)
            {
                MessageBox.Show("范围过小！","SUNZ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return null;

            }
            if ((width/1000)*(hight/1000)>40)
            {
                if (MessageBox.Show("范围超出40平分公里,系统计算时间比较长，是否继续？","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
                {
                    
                }
                else
                {
                    return null;
                }  
            }
            this.Interval = m_interval;
            this.Random = System.Guid.NewGuid().ToString().Substring(0, 6).ToLower();
            CopyFolder(Application.StartupPath + "\\Convert\\TemPoints", Application.StartupPath + "\\Convert\\PointsResult\\" + this.Random);
            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();
            IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(Application.StartupPath + "\\Convert\\PointsResult\\" + this.Random, 0);
            IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
            IFeatureClass pFeatureClass = pFeatureWorkspace.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(Application.StartupPath + "\\Convert\\PointsResult\\" + Random + "\\TemPointLayer.shp"));
            IFeatureClassWrite fr = (IFeatureClassWrite)pFeatureClass;
            IWorkspaceEdit w = (pFeatureClass as IDataset).Workspace as IWorkspaceEdit;
            IFeature f;
            w.StartEditing(true);
            w.StartEditOperation();
            int EastPointCount = (int)(width / 5);
            int NothPointCount = (int)(hight / 5);
            for (int j = 0; j < NothPointCount; j++)
            {
                ESRI.ArcGIS.Geometry.IPoint p;
                p = new ESRI.ArcGIS.Geometry.PointClass();
                ESRI.ArcGIS.Geometry.IZAware iz = p as ESRI.ArcGIS.Geometry.IZAware;
                iz.ZAware = true;
                p.X = lux;
                p.Y = luy;
                p.Z = luz;
              
                ESRI.ArcGIS.Geometry.IGeometry peo;
                peo = p;
                f = pFeatureClass.CreateFeature();
                f.Shape = peo;
                f.set_Value(3, p.Z);
                f.Store();
                fr.WriteFeature(f);

                for (int i = 0; i < EastPointCount; i++)
                {

                    IPosition61 positionLU = null;
                    ICoord2D pPoint = null;
                    if ((j + 2) % 2 == 0)
                    {
                        pPoint = sgworld.CoordServices.MoveCoord(lux, luy, 5, 0);
                    }
                    else
                    {
                        pPoint = sgworld.CoordServices.MoveCoord(lux, luy, -5, 0);
                    }

                    IWorldPointInfo61 pW = sgworld.Terrain.GetGroundHeightInfo(pPoint.X, pPoint.Y, AccuracyLevel.ACCURACY_BEST_FROM_MEMORY, true);
                    positionLU = pW.Position;
                    luz = positionLU.Altitude;
                    lux = positionLU.X;
                    luy = positionLU.Y;
                    p = new ESRI.ArcGIS.Geometry.PointClass();
                    iz = p as ESRI.ArcGIS.Geometry.IZAware;
                    iz.ZAware = true;
                    p.X = lux;
                    p.Y = luy;
                    p.Z = luz;
                    peo = p;
                    f = pFeatureClass.CreateFeature();
                    f.Shape = peo;
                    f.set_Value(3, p.Z);
                    f.Store();
                    fr.WriteFeature(f);
                    
                }
                ICoord2D pPointL = sgworld.CoordServices.MoveCoord(lux, luy, 0, -5);
                IWorldPointInfo61 pWL = sgworld.Terrain.GetGroundHeightInfo(pPointL.X, pPointL.Y, AccuracyLevel.ACCURACY_FORCE_BEST_RENDERED, true);
                luz = pWL.Position.Altitude;
                lux = pWL.Position.X;
                luy = pWL.Position.Y;

            }
            w.StopEditOperation();
            w.StopEditing(true);
            CreateTinFromFeature(pFeatureClass);
            return this.Random;
        }

        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="sourceFolder">源文件路径</param>
        /// <param name="destFolder">目标路径</param>
        private void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);

                string dest = Path.Combine(destFolder, name);

                File.Copy(file, dest);
            }
        }

        /// <summary>
        /// 创建Tin文件
        /// </summary>
        /// <param name="pFeatureClass">要素类</param>
        private void CreateTinFromFeature(IFeatureClass pFeatureClass)
        {
            try
            {
                IGeoDataset pGeoDataset = pFeatureClass as IGeoDataset;
                ESRI.ArcGIS.Geometry.IEnvelope pExtent = pGeoDataset.Extent;
                pExtent.SpatialReference = pGeoDataset.SpatialReference;

                //获得高程值
                IFields pFields = pFeatureClass.Fields;
                IField pHeightField;
                pHeightField = pFields.get_Field(3);

                ITinEdit pTinEdit = new TinClass();
                pTinEdit.InitNew(pExtent);
                object Missing = Type.Missing;
                object pbUseShapeZ = Missing;
                object pOverWrite = Missing;

                pTinEdit.AddFromFeatureClass(pFeatureClass, null, pHeightField, null, esriTinSurfaceType.esriTinMassPoint, ref pbUseShapeZ);

                pTinEdit.SaveAs(Application.StartupPath + "\\Convert\\TemTIN\\" + this.Random, ref pOverWrite);
                pTinEdit.StopEditing(false);
                CreateContourData(Application.StartupPath + "\\Convert\\TemTIN");
            }
            catch (Exception)
            { 
              
            }
            

        }

        /// <summary>
        /// 通过TIN文件，建立*.shp文件
        /// </summary>
        /// <param name="TINDir">TIN文件的文件夹路径</param>
        private void CreateContourData(string TINDir)
        {
            try
            {
                IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();
                CopyFolder(Application.StartupPath + "\\Convert\\TemContour", Application.StartupPath + "\\Convert\\ContourResult\\" + this.Random);
                IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(Application.StartupPath + "\\Convert\\ContourResult\\" + this.Random, 0);
                IWorkspaceEdit w = pWorkspace as IWorkspaceEdit;
                IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
                IFeatureClass tFeatureClass = pFeatureWorkspace.OpenFeatureClass("Contour.shp");

                ITinWorkspace pTinWorkspace;
                IWorkspace pWS;
                IWorkspaceFactory pWSFact = new TinWorkspaceFactoryClass();

                pWS = pWSFact.OpenFromFile(TINDir, 0);

                ITin pTin;

                pTinWorkspace = pWS as ITinWorkspace;
                w.StartEditing(true);
                w.StartEditOperation();

                if (pTinWorkspace.get_IsTin(this.Random))
                {
                    pTin = pTinWorkspace.OpenTin(this.Random);

                    ITinSurface pTinSurface = pTin as ITinSurface;

                    pTinSurface.Contour(0, this.Interval, tFeatureClass, "Z", 0);

                    w.StopEditOperation();
                    w.StopEditing(true);
                }
            }
            catch (Exception)
            {
               
            }
           
        }

        /// <summary>
        /// 创建拓扑
        /// </summary>
        /// <param name="workspaceName">被检查文件的路径名称</param>
        public void CreateTopology(string workspaceName)
        { 

            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            IWorkspace workspace = workspaceFactory.OpenFromFile(Application.StartupPath + @"\Convert\GeoDataBase.gdb", 0);
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
            IFeatureDataset featureDataset = featureWorkspace.OpenFeatureDataset("ContourData");
            ITopologyContainer2 topologyContainer = (ITopologyContainer2)featureDataset;
            IFeatureClass CurrContour = null;
            try
            {
                
                CurrContour = featureWorkspace.OpenFeatureClass("CurrContour");
                ITopology topology = topologyContainer.get_TopologyByName("eeee");
                IDataset fDataset = (IDataset)topology;
                fDataset.Delete();
            }
            catch (Exception)
            {
                
               // throw;
            }
            
            if (CurrContour != null)
            {
                IDataset fDataset = (IDataset)CurrContour;

                fDataset.Delete();
            }
           
                Geoprocessor gp = new Geoprocessor();
                FeatureClassToFeatureClass pFFeatureClassToFeatureClass = new FeatureClassToFeatureClass();
                pFFeatureClassToFeatureClass.in_features = Application.StartupPath + "\\Convert\\ContourResult\\"+workspaceName+"\\Contour.shp";
                pFFeatureClassToFeatureClass.out_path = Application.StartupPath + "\\Convert\\GeoDataBase.gdb\\ContourData";

                pFFeatureClassToFeatureClass.out_name = "CurrContour";


                IGeoProcessorResult geoProcessorResult = (IGeoProcessorResult)gp.Execute(pFFeatureClassToFeatureClass, null);
                CurrContour = featureWorkspace.OpenFeatureClass("CurrContour");

                ISchemaLock schemaLock = (ISchemaLock)featureDataset;
                try
                {
                    schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);

                    // Create the topology.
                    
                    ITopology topology = topologyContainer.CreateTopology("eeee",
                        topologyContainer.DefaultClusterTolerance,  - 1, "");
                   
                    // Add feature classes and rules to the topology.
                    topology.AddClass((IClass)CurrContour, 5, 1, 1, false);
                    
                    AddRuleToTopology(topology, esriTopologyRuleType.esriTRTLineNoIntersection,
                        "must not intersect", CurrContour);

                    int ErrorCount = 0;

                    // Get an envelope with the topology's extents and validate the topology.
                    IGeoDataset geoDataset = (IGeoDataset)topology;
                    ESRI.ArcGIS.Geometry.IEnvelope envelope = geoDataset.Extent;
                    ValidateTopology(topology, envelope);
                    IGeoDataset geoDS = topology as IGeoDataset;
                    IErrorFeatureContainer errorContainer = topology as IErrorFeatureContainer;
                    IEnumTopologyErrorFeature eErrorFeat;
                    eErrorFeat = errorContainer.get_ErrorFeaturesByRuleType(geoDS.SpatialReference,
                        esriTopologyRuleType.esriTRTAreaNoGaps, null, true, false);
                    ITopologyErrorFeature topoError;
                    topoError = eErrorFeat.Next();
                    while (topoError != null)
                    {
                        ErrorCount++;
                        topoError = eErrorFeat.Next();
                    }
                    if (ErrorCount==0)
                    {
                        MessageBox.Show("无拓扑错误！","Sunz",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("拓扑错误" + "" + ErrorCount + "" + "个！");
                    }
                    
                }
                catch (COMException comExc)
                {
                    throw new Exception(String.Format(
                        "Error creating topology: {0} Message: {1}", comExc.ErrorCode,
                        comExc.Message), comExc);
                }
                finally
                {
                    schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
                }
        
        }

        /// <summary>
        /// 添加拓扑规则条件 
        /// </summary>
        /// <param name="topology">拓扑对象</param>
        /// <param name="ruleType">规则类型</param>
        /// <param name="ruleName">规则名称</param>
        /// <param name="featureClass">要素类</param>
        private void AddRuleToTopology(ITopology topology, esriTopologyRuleType ruleType, String ruleName, IFeatureClass featureClass)
        {
            try
            {
                // Create a topology rule.
                ITopologyRule topologyRule = new TopologyRuleClass();
                topologyRule.TopologyRuleType = ruleType;
                topologyRule.Name = ruleName;
                topologyRule.OriginClassID = featureClass.FeatureClassID;
                topologyRule.AllOriginSubtypes = true;

                // Cast the topology to the ITopologyRuleContainer interface and add the rule.
                ITopologyRuleContainer topologyRuleContainer = (ITopologyRuleContainer)topology;
                if (topologyRuleContainer.get_CanAddRule(topologyRule))
                {
                    topologyRuleContainer.AddRule(topologyRule);
                }
                else
                {
                    throw new ArgumentException("Could not add specified rule to the topology.");
                }
            }
            catch (Exception)
            {
    
            }
           
        }

        /// <summary>
        /// 验证拓扑
        /// </summary>
        /// <param name="topology">拓扑对象</param>
        /// <param name="envelope">验证范围</param>
        private void ValidateTopology(ITopology topology, ESRI.ArcGIS.Geometry.IEnvelope envelope)
        {
            try
            {
                // Get the dirty area within the provided envelope.
                ESRI.ArcGIS.Geometry.IPolygon locationPolygon = new ESRI.ArcGIS.Geometry.PolygonClass();
                ESRI.ArcGIS.Geometry.ISegmentCollection segmentCollection = (ESRI.ArcGIS.Geometry.ISegmentCollection)locationPolygon;
                segmentCollection.SetRectangle(envelope);
                ESRI.ArcGIS.Geometry.IPolygon polygon = topology.get_DirtyArea(locationPolygon);

                // If a dirty area exists, validate the topology.if (!polygon.IsEmpty)
                {
                    // Define the area to validate and validate the topology.
                    ESRI.ArcGIS.Geometry.IEnvelope areaToValidate = polygon.Envelope;
                    ESRI.ArcGIS.Geometry.IEnvelope areaValidated = topology.ValidateTopology(areaToValidate);

                }
            }
            catch (Exception)
            {
               
            }
          
        }



    }
}
