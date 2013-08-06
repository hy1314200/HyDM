using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Frame.Define;
using Define;
using Skyline.Define;
using System.Windows.Forms;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesFile;

namespace Skyline.Frame
{
    public class SkylineResourceManager:IResourceManager
    {
        private EsriLicenseInitializer m_AOLicenseInitializer = new EsriLicenseInitializer();
        public bool LicenseVerify(ref string errMsg)
        {
            if (!m_AOLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB },
               new esriLicenseExtensionCode[] { }))
            {
                System.Windows.Forms.MessageBox.Show(m_AOLicenseInitializer.LicenseMessage() +
                "\n\n当前应用程序无法正确初始化许可，即将关闭。",
                "ArcGIS许可失败");
                m_AOLicenseInitializer.ShutdownApplication();
                return false; ;
            }

            return true;
        }

        public void Release()
        {
            m_AOLicenseInitializer.ShutdownApplication();
        }



        public virtual ICommand CommandProxy(object objCommand)
        {
            if (objCommand is ThreeDimension.BasicEngine.ICommand)
            {
                return new Old3DCommandProxy(objCommand as ThreeDimension.BasicEngine.ICommand);
            }
            return objCommand as ICommand;
        }

        public object GetWorkspace(string strType, string strArgs)
        {
            IWorkspaceFactory wsf = null;
            IWorkspace m_SystemWorkspace = null;
            switch (strType)
            {
                case "PGDB":
                    wsf = new AccessWorkspaceFactoryClass();
                    m_SystemWorkspace = wsf.OpenFromFile(strArgs, 0);
                    break;

                case "FILEGDB":
                    wsf = new ShapefileWorkspaceFactoryClass();
                    m_SystemWorkspace = wsf.OpenFromFile(strArgs, 0);
                    break;

                case "SDE":
                    IPropertySet pSet = new PropertySetClass();
                    string[] argList = strArgs.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strArg in argList)
                    {
                        string[] argPair = strArg.Split(new char[] { ':' });
                        pSet.SetProperty(argPair[0], argPair[1]);
                    }
                    wsf = new SdeWorkspaceFactoryClass();
                    m_SystemWorkspace = wsf.Open(pSet, 0);
                    break;

                default:
                    throw new Exception("系统Workspace配置了无法识别的数据库:Workspace类型应该在PGDB、FILEGDB和SDE之内");
            }

            return m_SystemWorkspace;
        }


        public virtual bool IsResource(string strInterfaceName)
        {
            return strInterfaceName == "ThreeDimension.BasicEngine.ICommand";
        }

        private class SkylineFrameHook : ISkylineHook,IHooker
        {
            public SkylineFrameHook()
            {
            }
                       
            private TerraExplorerX.TerraExplorerClass m_TerraExplorer;
            public TerraExplorerX.TerraExplorerClass TerraExplorer
            {
                get
                {
                    if(this.m_TerraExplorer==null)
                    { 
                        this.m_TerraExplorer = new TerraExplorerX.TerraExplorerClass();
                    }

                    return m_TerraExplorer;
                }

            }

            public TerraExplorerX.ISGWorld61 m_SGWorld;
            public TerraExplorerX.ISGWorld61 SGWorld
            {
                get
                {
                    if(this.m_SGWorld==null)
                    {
                        this.m_SGWorld = new TerraExplorerX.SGWorld61Class();
                    }

                    return m_SGWorld;
                }
            }


            AxTerraExplorerX.AxTE3DWindow m_TeWindow = null;
            public Control Control
            {
                get
                {
                    if (m_TeWindow == null)
                    {
                        m_TeWindow = new AxTerraExplorerX.AxTE3DWindow();
                    }

                    return m_TeWindow;
                }
            }

            public object Hook
            {
                get { return this; }
            }

            private Guid m_Guid = Guid.NewGuid();
            public Guid ID
            {
                get { return m_Guid; }
            }

            public Control Window
            {
                get { return m_TeWindow; }
            }

            public string Caption
            {
                get { return "三维"; }
            }
        }

        
        public IHooker GetHooker()
        {
            return new SkylineFrameHook();
        }
    }
}
