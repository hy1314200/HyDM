using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System.Collections;
using Hy.VCTConverter;

namespace EngineTest
{
    static class Program
    {
        public static IWorkspace GetWorkspace(Hashtable pPropList, string progID)
        {
            try
            {
                IPropertySet2 propertySets = new PropertySetClass();

                foreach (string paraName in pPropList.Keys)
                    propertySets.SetProperty(paraName.ToUpper(), pPropList[paraName]);

                IWorkspaceFactory wsf = new ESRI.ArcGIS.DataSourcesGDB.SdeWorkspaceFactoryClass();
                return wsf.Open(propertySets, 0);
                return (new ESRI.ArcGIS.DataSourcesGDB.SdeWorkspaceFactoryClass()).Open(propertySets, 0);
                IWorkspaceName2 workspaceName = new WorkspaceNameClass();
                workspaceName.ConnectionProperties = propertySets;
                // 类型库需要动态设置
                workspaceName.WorkspaceFactoryProgID = progID;

                IName pName = workspaceName as IName;
                IWorkspace workspace = pName.Open() as IWorkspace;
                //IWorkspace workspace = pName.Open() as IWorkspace;
                return workspace;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SDE连接参数不正确，请和系统管理员联系！");
                return null;
            }
        }

        private static LicenseInitializer m_AOLicenseInitializer = new EngineTest.LicenseInitializer();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //ESRI License Initializer generated code.
            if (!m_AOLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeEngine },
            new esriLicenseExtensionCode[] { }))
            {
                System.Windows.Forms.MessageBox.Show(m_AOLicenseInitializer.LicenseMessage() +
                "\n\nThis application could not initialize with the correct ArcGIS license and will shutdown.",
                "ArcGIS License Failure");
                m_AOLicenseInitializer.ShutdownApplication();
                Application.Exit();
                return;
            }


            string strHyDwg=@"E:\吴江\建库数据实验2013-06-18.dwg";
            HyDwgConvert.IDwgReader hyReader = new HyDwgConvert.DwgReaderClass();
            hyReader.set_FileName(strHyDwg);
            hyReader.Init();
            
            System.Array regAppNames = hyReader.GetRegAppNames();

            int eCount = hyReader.GetEntityCount();
            int index = 0;
            string strValue = "";
            object xType, xValue;
            for (HyDwgConvert.IDwgEntity dwgEntity1 = hyReader.Read(); dwgEntity1 != null; dwgEntity1 = hyReader.Read())
            {
                if (dwgEntity1.Shape == null)
                {
                }
               
                dwgEntity1.GetXData(null,out xType,out xValue);
                //foreach (var pValue in xValue)
                //{
                //    strValue += string.Format("{0}\r\n", pValue);
                //}
                strValue += "\r\n";
                index++;
            }
            hyReader.Close();


            HyDwgConvert.IDwgEntity dwgEntity = new HyDwgConvert.DwgEntityClass();
            dwgEntity.GeometryType = "我靠了";
            dwgEntity.Color = 123;
            int color = dwgEntity.Color;
            dwgEntity.Handle = "adfas廿的了";
            MessageBox.Show(dwgEntity.GeometryType);

            string strTarget = @"e:\Dwg\Test.mdb";
            string strDwgFile = @"e:\Dwg\TestCass.dwg";// @"E:\吴江\建库数据实验2013-06-18.dwg";
            IWorkspace wsTarget = (new ESRI.ArcGIS.DataSourcesGDB.AccessWorkspaceFactoryClass()).OpenFromFile(strTarget, 0);
            DwgConvertLib.DwgReaderClass dwgReader = new DwgConvertLib.DwgReaderClass();
            dwgReader.LogFilePath = @"E:\dwg\log.log";
            dwgReader.InitReadDwg(wsTarget, null);
            dwgReader.ReadPolygon = true;
            dwgReader.ReadInvisible = true;
            dwgReader.ReadBlockPoint = true;
            dwgReader.CreateAnnotation = true;
            dwgReader.Line2Polygon = false;
            dwgReader.ReadDwgFile(strDwgFile);
            dwgReader.JoinXDataAttrib = true;
            dwgReader.Close();

            string strSource = @"e:\Dwg\TestSource.mdb";
            string strDwgTarget = @"e:\Dwg\TestOut.dwg";
            DwgConvertLib.DwgWriterClass dwgWriter = new DwgConvertLib.DwgWriterClass();
            dwgWriter.XDataXMLConfigFile = @"E:\dwg\CAD扩展属性配置.xml";
            dwgWriter.LogFilePath = @"E:\dwg\logOut.log";
            dwgWriter.InitWriteDwg(strDwgTarget, @"E:\Dwg\template.dwg");        
            IWorkspace wsSource = (new ESRI.ArcGIS.DataSourcesGDB.AccessWorkspaceFactoryClass()).OpenFromFile(strSource, 0);
            IEnumDataset enDsSource=wsSource.get_Datasets(esriDatasetType.esriDTFeatureClass);

            for (IDataset dsSource = enDsSource.Next(); dsSource != null; dsSource = enDsSource.Next())
            {
                dwgWriter.FeatureClass2Dwgfile(dsSource as IFeatureClass);
            }

            Form frmProcess = new Form();
            Label lblMessage = new Label();
            lblMessage.Location = new System.Drawing.Point(0, 0);
            frmProcess.Controls.Add(lblMessage);

            Label lblMessage2 = new Label();
            lblMessage2.Location = new System.Drawing.Point(0, 30);
            frmProcess.Controls.Add(lblMessage2);
            frmProcess.Show();
            Application.DoEvents();

            string strVct = @"E:\HyDM\DataExchange\Data\样例.vct";
            string strMDB=@"E:\HyDM\DataExchange\Data\Example.mdb";
            VctDocument vctDoc = new VctDocument();
            int lineIndex = 0, vctRowIndex = 0;
            vctDoc.OnReadingLineChanged+=delegate
            {
               lblMessage.Text = (++lineIndex).ToString();
                Application.DoEvents();
            };

            string strLog = "";
            vctDoc.OnMessage  += delegate(string strMsg)
            {
                //lblMessage2.Text = strMsg;
                strLog += strMsg+Environment.NewLine;
                //Application.DoEvents();
            };

            vctDoc.OnImportingRowChanged += delegate
            {
                lblMessage2.Text = (++vctRowIndex).ToString();
                Application.DoEvents();
            };
            vctDoc.Open(strVct);
            vctDoc.ReadData();

            IWorkspace ws = (new ESRI.ArcGIS.DataSourcesGDB.AccessWorkspaceFactoryClass()).OpenFromFile(strMDB, 0);
            foreach (VctLayer vctLayer in vctDoc.Map.Layers)
            {
                string strMsg="";
                vctRowIndex = 0;
                if (!vctDoc.ConvertVctFileToSDE(vctLayer, ws, vctLayer.Name, "TTT", vctDoc.Header.GetSpatialReference(), ref strMsg))
                {
                    MessageBox.Show(strMsg);
                }
            }







            Hashtable htParameters = new Hashtable();
            htParameters.Add("SERVER", "172.16.1.9");
            htParameters.Add("USER", "wjzgis");
            htParameters.Add("PASSWORD", "wjzgis");
            htParameters.Add("VERSION", "SDE.DEFAULT");
            htParameters.Add("INSTANCE", "5151");

            IWorkspace wsVCT= GetWorkspace(htParameters, "esriDataSourcesGDB.SdeWorkspaceFactory");

            IEnumDataset dsen = ws.get_Datasets(esriDatasetType.esriDTFeatureDataset);

            IDataset ds = dsen.Next();

            ds = (wsVCT as IFeatureWorkspace).OpenFeatureDataset("WJZGIS.FdsTest");
            IVersionedObject vo = ds as IVersionedObject;
            if(vo !=null)
            {

            }













            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //ESRI License Initializer generated code.
            //Do not make any call to ArcObjects after ShutDownApplication()
            m_AOLicenseInitializer.ShutdownApplication();
        }







    }
}
