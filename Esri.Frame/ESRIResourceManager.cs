using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using System.Windows.Forms;
using Define;
using Esri.Define;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesFile;
using Frame.Define;

namespace Esri.Frame
{
    public class EsriResourceManager:IResourceManager
    {
        private LicenseInitializer m_AOLicenseInitializer = new LicenseInitializer();
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

        public ICommand CommandProxy(object objCommand)
        {
            if (objCommand is ESRI.ArcGIS.SystemUI.ICommandSubType)
                return null;

            if (objCommand is ESRI.ArcGIS.SystemUI.IToolControl)
            {
                objCommand = new EsriExProxy(objCommand as ESRI.ArcGIS.SystemUI.IToolControl);
            }
            else if (objCommand is ESRI.ArcGIS.SystemUI.ICommand)
            {
                objCommand = new EsriCommandProxy(objCommand as ESRI.ArcGIS.SystemUI.ICommand);
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

        public Control GetHookControl()
        {
            m_MapControl = new ESRI.ArcGIS.Controls.AxMapControl();
            return m_MapControl;
        }

        ESRI.ArcGIS.Controls.AxMapControl m_MapControl;
        public IHook CreateHook(Form frmMain, Control leftDock, Control rightDock, Control bottomDock)
        {
            return new EsriFrameHook(frmMain, m_MapControl.Object as ESRI.ArcGIS.Controls.IMapControl4, leftDock, rightDock, bottomDock);
        }
        private class EsriFrameHook : IHook, IUIHook, IEsriHook
        {
            public EsriFrameHook(Form frmMain, ESRI.ArcGIS.Controls.IMapControl4 mapControl, Control leftPanel, Control rightPanel, Control bottomPanel)
            {
                this.MainForm = frmMain;
                ESRI.ArcGIS.Controls.IHookHelper esriHookHelper = new ESRI.ArcGIS.Controls.HookHelperClass();
                esriHookHelper.Hook = mapControl.Object;
                this.HookHelper = esriHookHelper;
                this.RightDockPanel = rightPanel;
                this.LeftDockPanel = leftPanel;
                this.BottomDockPanel = bottomPanel;
            }

            public Form MainForm { get;private set; }

            public object Tag { get; set; }

            public Control LeftDockPanel { get; private set; }

            public Control RightDockPanel { get; private set; }

            public Control BottomDockPanel { get; private set; }

            public ESRI.ArcGIS.Controls.IHookHelper HookHelper { get; private set; }
        }



        public bool IsResource(string strInterfaceName)
        {
            return strInterfaceName == "ESRI.ArcGIS.SystemUI.ICommand";
        }
    }
}
