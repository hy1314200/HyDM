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


        AxTerraExplorerX.AxTE3DWindow m_TeWindow = null;
        public virtual System.Windows.Forms.Control GetHookControl()
        {
            m_TeWindow = new AxTerraExplorerX.AxTE3DWindow();
            return m_TeWindow;
        }

        public virtual IHook CreateHook(System.Windows.Forms.Form frmMain, System.Windows.Forms.Control leftDock, System.Windows.Forms.Control rightDock, System.Windows.Forms.Control bottomDock)
        {
            return new SkylineFrameHook(frmMain,m_TeWindow, new TerraExplorerX.TerraExplorerClass(), new TerraExplorerX.SGWorld61Class(), leftDock, rightDock, bottomDock);
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

        private class SkylineFrameHook : IHook, IUIHook, ISkylineHook
        {
            public SkylineFrameHook(
                Form frmMain,
                Control teWindow,
                TerraExplorerX.TerraExplorerClass te,
                TerraExplorerX.ISGWorld61 sgWorld,
                Control leftPanel,
                Control rightPanel,
                Control bottomPanel
                )
            {
                this.MainForm = frmMain;
                this.Window = teWindow;
                this.SGWorld = sgWorld;
                this.TerraExplorer = te;
                this.LeftDockPanel = leftPanel;
                this.RightDockPanel = rightPanel;
                this.BottomDockPanel = bottomPanel;
            }

            public Form MainForm
            {
                get ;private set;
            }

            public Control Window { get; private set; }

            public TerraExplorerX.TerraExplorerClass TerraExplorer
            {
                get;
                private set;
            }

            public TerraExplorerX.ISGWorld61 SGWorld
            {
                get; private set;
            }
       

            public Control RightDockPanel
            {
                get; private set;
            }


            public object Tag
            {
                get;
                set;
            }

            public Control LeftDockPanel
            {
                get;
                private set;
            }

            public Control BottomDockPanel
            {
                get;
                private set;
            }
        }

    }
}
