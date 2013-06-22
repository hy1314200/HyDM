using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerraExplorerX;
using ESRI.ArcGIS.Geodatabase;

using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geometry;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.DataManagementTools;


namespace Skyline.Core.Helper
{
    public class ArcGISDataManager
    {       

        /// <summary>
        /// 加载shape文件添加模型
        /// 模型数据要与shape在同一路径下
        /// 2012-09-21 张航宇
        /// 添加Z修正 -56
        /// </summary>
        /// <param name="FileName"></param>
        /// 
        public void LoadShapeAdd3model(ISGWorld61 sgworld,ITerraExplorer te, string FileName)
        {
            IInformationTree5 infoTree5 = te as IInformationTree5;
            if (string.IsNullOrEmpty(FileName))
            {
                return;
            }
            if (File.Exists(FileName))
            {
                bool PathType = true;
                string PathFileName = "";
                string GroupName = "模型组群" + System.Guid.NewGuid().ToString().Substring(0, 6);
                infoTree5.CreateGroup(GroupName, 0);
                int GroupID = sgworld.ProjectTree.FindItem(GroupName);
                string ModelFilename = "";
                string UNModelFilename = "";
                string _XMLLayerInfo = "<PlugData>" +

                "<PlugType>shape</PlugType>" +

                "<LayerName>" + FileName + "</LayerName>" +

                "<Server></Server>" +

                "<user></user>" +

                "<password></password>" +

                "<TableName></TableName>" +

                "<AttributesToLoad>*</AttributesToLoad >" +

                "<Feature>1</Feature >" +

                "<Annotation>1</Annotation>" +

                "<SaveItems>0</SaveItems>" +

                "<GroupKey>LAT-LONG</GroupKey>" +

                "<SysKey>LAT-LONG</SysKey>" +

                "<DatumKey>WGS84</DatumKey>" +

                "<UnitKey>METERS</UnitKey>" +

                "<UseZValue>0</UseZValue>" +

                "<AltitudeUnit>Meters</AltitudeUnit>" +

                "<Reproject>1</Reproject>" +

                "<StreamedLayer>0</StreamedLayer></PlugData >";
                ILayer5 iLyr = infoTree5.CreateLayer("3DModleGoto", _XMLLayerInfo, GroupID);//在根目录下装载shp数据
                iLyr.Load();
                IFeature61 sqfeature61 = null;
                int itemid = sgworld.ProjectTree.FindItem("" + GroupName + "\\3DModleGoto");

                ILayer61 m_layer61 = sgworld.ProjectTree.GetLayer(itemid);
                m_layer61.Streaming = false;

                IFeatureGroups61 pFeatureGroups61 = m_layer61.FeatureGroups;

                IFeatureGroup61 pFeatureGroup61 = pFeatureGroups61.Point as IFeatureGroup61;
                if (pFeatureGroup61 == null)
                {
                    MessageBox.Show("当前操作要求是正确的点图层");
                    return;
                }
                else
                {
                    if (pFeatureGroup61.Count == 0)
                    {
                        MessageBox.Show("图层为空!");
                        return;
                    }
                    else
                    {
                        sqfeature61 = pFeatureGroup61[0] as IFeature61;
                        IFeatureAttributes61 m_FeatureAttributes = sqfeature61.FeatureAttributes;
                        int AttributesCount = m_FeatureAttributes.Count;
                        string[] FiledArry = new string[AttributesCount];
                        for (int f = 0; f < AttributesCount; f++)
                        {
                            IFeatureAttribute61 m_FeatureAttribute = m_FeatureAttributes[f] as IFeatureAttribute61;
                            FiledArry[f] = m_FeatureAttribute.Name;
                        }
                        Skyline.Core.UI.FrmAddModelShape pFrmAddModelShape = new Skyline.Core.UI.FrmAddModelShape();
                        pFrmAddModelShape.GetFiledName = FiledArry;
                        pFrmAddModelShape.ShowDialog();
                        PathFileName = pFrmAddModelShape.Filed;
                        PathType = pFrmAddModelShape.PathType;
                        pFrmAddModelShape.Dispose();

                        int m_FeatureCount = pFeatureGroup61.Count;
                        int ImportFeatureCount = pFeatureGroup61.Count;
                        for (int i = 0; i < m_FeatureCount; i++)
                        {
                            sqfeature61 = pFeatureGroup61[i] as IFeature61;
                            IFeatureAttributes61 _FeatureAttributes = sqfeature61.FeatureAttributes;
                            TerraExplorerX.IGeometry _Geometry = sqfeature61.GeometryZ;
                            TerraExplorerX.IPoint pPoint = _Geometry as TerraExplorerX.IPoint;
                            //IPosition61 TPosition61 = sgworld.Window.PixelToWorld(pPoint.X, pPoint.Y, WorldPointType.WPT_ALL).Position;
                            IPosition61 TPosition61 =sgworld.Creator.CreatePosition(pPoint.X, pPoint.Y, -56, AltitudeTypeCode.ATC_TERRAIN_RELATIVE, 0, 0);
                            IFeatureAttribute61 _FeatureAttribute = _FeatureAttributes.GetFeatureAttribute(PathFileName);
                            UNModelFilename = _FeatureAttribute.Value.ToString();
                            if (PathType)
                            {
                                ModelFilename = System.IO.Path.GetDirectoryName(FileName) + "\\" + UNModelFilename;
                            }
                            else
                            {
                                ModelFilename = UNModelFilename;
                            }

                            try
                            {
                               sgworld.Creator.CreateModel(TPosition61, ModelFilename, 1, ModelTypeCode.MT_NORMAL, GroupID, UNModelFilename);
                            }
                            catch
                            {
                                ImportFeatureCount--;
                                continue;
                            }

                        }
                        MessageBox.Show(ImportFeatureCount + "个模型加载成功," + (m_FeatureCount - ImportFeatureCount) + "个模型加载失败!");

                        /****20130227杨漾（添加文件有效性判断，分步判断图层有效性，增加加载统计情况提示）****/

                    }
                }
            }
            
        }

        /// <summary>
        /// 通过url获取Xml结构
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public XmlDocument GetXMLFromUrl(string strUrl)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(strUrl);
                return doc;  
            }
            catch (Exception)
            {
                return null;  
            }
             
        }

        /// <summary>
        /// 加载WMS服务
        /// </summary>
        /// <param name="EXURl">解析范围用的URL</param>
        /// <param name="URl">服务地址</param>
        /// <param name="ServerName">服务名称</param>
        public void LoadWMS(ISGWorld61 sgworld, string EXURl, string URl,string ServerName)
        {
            XmlDocument doc = new XmlDocument();

            doc = GetXMLFromUrl(EXURl);
            if (doc == null)
            {
                
            }
            XmlNode Capnode = null;

            XmlNodeList Childrennodes = doc.ChildNodes;
            foreach (XmlNode item in Childrennodes)
            {
                if (item.Name =="WMS_Capabilities")
                {
                    Capnode = item.LastChild;
                    break;
                }
            }

            XmlNode Exnode = Capnode.LastChild;//layer
            XmlNode Extentnode = Exnode.ChildNodes[3];
            double[] extentArr = new double[4];
            extentArr[0] = Convert.ToDouble(Extentnode.ChildNodes[0].InnerText);
            extentArr[1] = Convert.ToDouble(Extentnode.ChildNodes[1].InnerText);
            extentArr[2] = Convert.ToDouble(Extentnode.ChildNodes[2].InnerText);
            extentArr[3] = Convert.ToDouble(Extentnode.ChildNodes[3].InnerText);

            string name = ServerName;
            var wmsFile = "[INFO]" + "\n" +
            "Meters=0" + "\n" +
            "MPP=2.68220901489258E-06" + "\n" +
            "Url=" + URl + "&request=GetMap&Version=1.1.1&Service=WMS&SRS=EPSG:4326&BBOX=0,0,0,0&HEIGHT=256&WIDTH=256&Styles=&Format=image/png&TRANSPARENT=FALSE" + "\n" +
            "xul=" + extentArr[0] + "\n" +
            "ylr=" + extentArr[2] + "\n" +
            "xlr=" + extentArr[1] + "\n" +
            "yul=" + extentArr[3] + "\n";
            try
            {
                var newlayer = sgworld.Creator.CreateImageryLayer(name, extentArr[0], extentArr[3], extentArr[1], extentArr[2], "<EXT><ExtInfo><![CDATA[" + wmsFile + "]]></ExtInfo><ExtType>wms</ExtType></EXT>", "gisplg.rct", 0, name);

                newlayer.UseNull = true;
                newlayer.NullValue = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("该服务地址不正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
           
        }

        /// <summary>
        /// 加载WFS矢量地图服务
        /// </summary>
        /// <param name="WFSURl">获取范围URL</param>
        /// <param name="URl">服务URL</param>
        /// <param name="ServerName">服务名称</param>
        public void LoadWFS(ISGWorld61 sgworld, string WFSURl,string URl,string ServerName)
        {

            XmlDocument doc = new XmlDocument();

            doc = GetXMLFromUrl(WFSURl);

            XmlNodeList Childrennodes = doc.ChildNodes;
            XmlNode Childrennode = Childrennodes[1];
            XmlNode FeatureType = Childrennode.ChildNodes[3].FirstChild;

            string LayerName = FeatureType.FirstChild.InnerXml;
            string Connstr = URl;
            Connstr ="Server="+ Connstr + ";User=admin;WFSVersion=1.0.0;LayerName=";
            // string Connstr = "Server=http://gisserver:8399/arcgis/services/line2/MapServer/WFSServer;User=admin;WFSVersion=1.0.0;LayerName=line2:WGSline1;TEPlugName=WFS;";
            Connstr = Connstr + "" + LayerName + "" + ";TEPlugName=WFS;";
            try
            {
                ILayer61 pILayer = sgworld.Creator.CreateFeatureLayer(ServerName, Connstr, 0);
                string wellKtext = sgworld.CoordServices.ChooseCSDialog("", "");
                pILayer.CoordinateSystem.WellKnownText = wellKtext;

                pILayer.Reproject = true;
                pILayer.Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show("该服务地址不正确！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
          
        }

        /// <summary>
        /// 添加Shape文件
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="layerName"></param>
        public void LoadShapeFile(ISGWorld61 sgworld, string FilePath,string layerName,int GroupID)
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                return;
            }
            if (File.Exists(FilePath))
            {
                string connnstr = "FileName=" + FilePath + ";TEPlugName=OGR;";
                ILayer61 layer61 = null;
                layer61 = sgworld.Creator.CreateFeatureLayer(layerName, connnstr, GroupID);

                // 2013-04-11 张航宇
                // 当坐标系不一致时，Streaming=false会使Load方法出错
                layer61.Streaming = true;
                layer61.Load();

                string wellKtext = sgworld.CoordServices.ChooseCSDialog("", "");
                layer61.CoordinateSystem.WellKnownText = wellKtext;
                //layer61.Streaming = false;

                // 2013-04-11 张航宇
                // 若坐标系不一致，则skyline会自动弹出投影对话框
                //if (MessageBox.Show("是否投影？", "SUNZ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    layer61.Reproject = true;
                //}
                //else
                //{
                //    layer61.Reproject = false;
                //}
                layer61.DataSourceInfo.Attributes.ImportAll = true;
                layer61.Refresh();
            }
            /*****20130227杨漾(添加文件有效性判断，去掉trycatch，上层trycatch(MainForm)处理)*****/
        }

        /// <summary>
        /// DWG转换SHP
        /// </summary>
        /// <param name="DWGPath">CAD文件路径</param>
        /// <returns>返回转化好的*.shp文件路径</returns>
        public void  ConvertToShp(string DWGPath)
        {
            if (string.IsNullOrEmpty(DWGPath))
            {
                return;
            }
            if (File.Exists(DWGPath))
             {
                 //try
                 //{
                 //string temshp = "";
                 IWorkspaceFactory pWorkspaceFactory;
                 IFeatureWorkspace pFeatureWorkspace;
                 //IFeatureLayer pFeatureLayer;
                 IFeatureDataset pFeatureDataset;
                 //打开CAD数据集

                 pWorkspaceFactory = new CadWorkspaceFactoryClass();
                 pFeatureWorkspace = (IFeatureWorkspace)pWorkspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(DWGPath), 0);
                 //打开一个要素集
                 pFeatureDataset = pFeatureWorkspace.OpenFeatureDataset(System.IO.Path.GetFileName(DWGPath));
                 //IFeaturClassContainer可以管理IFeatureDataset中的每个要素类
                 IFeatureClassContainer pFeatureClassContainer = (IFeatureClassContainer)pFeatureDataset;
                 //当前过滤只保留面
                 for (int i = 0; i < pFeatureClassContainer.ClassCount; i++)
                 {
                     IFeatureClass pFeatureClass = pFeatureClassContainer.get_Class(i);
                     if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                     {
                         ExportFeatureClassToConTempShp(pFeatureClass);
                         break;
                     }
                 }
                 //return temshp;
                 //}
                 //catch (Exception)
                 //{
                 //    return "";
                 //}
             }
            /*****20130227杨漾(添加文件有效性判断，去掉trycatch，上层trycatch(MainForm)处理,无返回值)*****/
        }

        /// <summary>
        /// 要素类转Shape
        /// </summary>
        /// <param name="apFeatureClass"></param>
        private void ExportFeatureClassToConTempShp(IFeatureClass apFeatureClass)
        {
            if (Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Convert\\DWGConvert\\ConTempShp"))
            {
                Directory.Delete(System.Windows.Forms.Application.StartupPath + "\\Convert\\DWGConvert\\ConTempShp", true);
            }

            //设置导出要素类的参数
            IFeatureClassName pOutFeatureClassName = new FeatureClassNameClass();
            IDataset pOutDataset = (IDataset)apFeatureClass;
            pOutFeatureClassName = (IFeatureClassName)pOutDataset.FullName;
            //创建一个输出shp文件的工作空间
            IWorkspaceFactory pShpWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
            IWorkspaceName pInWorkspaceName = new WorkspaceNameClass();
            pInWorkspaceName = pShpWorkspaceFactory.Create(System.Windows.Forms.Application.StartupPath + "\\Convert\\DWGConvert", "ConTempShp", null, 0);

            //创建一个要素集合
            IFeatureDatasetName pInFeatureDatasetName = null;
            //创建一个要素类
            IFeatureClassName pInFeatureClassName = new FeatureClassNameClass();
            IDatasetName pInDatasetClassName;
            pInDatasetClassName = (IDatasetName)pInFeatureClassName;
            pInDatasetClassName.Name = "ConTempShp";//作为输出参数
            pInDatasetClassName.WorkspaceName = pInWorkspaceName;
            //通过FIELDCHECKER检查字段的合法性，为输出SHP获得字段集合
            long iCounter;
            IFields pOutFields, pInFields;
            IFieldChecker pFieldChecker;
            IField pGeoField;
            IEnumFieldError pEnumFieldError = null;
            pInFields = apFeatureClass.Fields;
            pFieldChecker = new FieldChecker();
            pFieldChecker.Validate(pInFields, out pEnumFieldError, out pOutFields);
            //通过循环查找几何字段
            pGeoField = null;
            for (iCounter = 0; iCounter < pOutFields.FieldCount; iCounter++)
            {
                if (pOutFields.get_Field((int)iCounter).Type == esriFieldType.esriFieldTypeGeometry)
                {
                    pGeoField = pOutFields.get_Field((int)iCounter);
                    break;
                }
            }
            //得到几何字段的几何定义
            IGeometryDef pOutGeometryDef;
            IGeometryDefEdit pOutGeometryDefEdit;
            pOutGeometryDef = pGeoField.GeometryDef;
            
            //设置几何字段的空间参考和网格
            pOutGeometryDefEdit = (IGeometryDefEdit)pOutGeometryDef;
            pOutGeometryDefEdit.GridCount_2 = 1;
            pOutGeometryDefEdit.set_GridSize(0, 1500000);
            
            //try
            //{
                IFeatureDataConverter pShpToClsConverter = new FeatureDataConverterClass();
                pShpToClsConverter.ConvertFeatureClass(pOutFeatureClassName, null, pInFeatureDatasetName, pInFeatureClassName, pOutGeometryDef, pOutFields, "", 1000, 0);
                // MessageBox.Show("导出成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


            //}
            //catch (Exception ex)
            //{

            //}
            /*****20130227杨漾(去掉trycatch，上层trycatch(MainForm)处理)*****/
        }

        /// <summary>
        /// 加载SDE图层
        /// </summary>
        /// <param name="layerName">图层名</param>
        /// <param name="server">sde服务IP</param>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        public void ConnArcSDE(ISGWorld61 sgworld, string layerName,string server,string user ,string password)
        {
            try
            {
                ILayer61 pILayer = sgworld.Creator.CreateFeatureLayer(layerName, "Server=" + server + ";User=" + user + ";Password=" + password + ";LayerName=" + layerName + ";Instance=5151/tcp;TEPlugName=arcsde;", 0);//在根目录下装载shp数据
                pILayer.Load();
            }
            catch (Exception)
            {

            }
          

        }

        /// <summary>
        /// 加载OracleSpatial数据
        /// </summary>
        /// <param name="server">服务IP地址</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="layername">图层名</param>
        public void ConnOracleSpatialDatabase(ISGWorld61 sgworld, string server, string username, string password, string layername)
        {
            string ConnStr = "OGRConnectionString=OCI:" + username + "/" + password + "@" + server + ":" + layername + ";";
            ILayer61 layer61 = sgworld.Creator.CreateFeatureLayer(layername, ConnStr, 0);
            layer61.Streaming = false;
            try
            {
                string wellKtext = sgworld.CoordServices.ChooseCSDialog("", "");
                layer61.CoordinateSystem.WellKnownText = wellKtext;
                layer61.Reproject = true;
                layer61.Load();
            }
            catch (Exception)
            {
                layer61.Load();
            }
        }
    }
}
