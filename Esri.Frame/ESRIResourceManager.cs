using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using System.Windows.Forms;
using Define;

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
        
        public bool IsResource(string strInterfaceName)
        {
            return strInterfaceName == "ESRI.ArcGIS.SystemUI.ICommand";
        }

        private class EsriHooker : IHooker
        {

            private Guid m_Guid = Guid.NewGuid();
            public Guid ID
            {
                get { return m_Guid; }
            }

            ESRI.ArcGIS.Controls.AxMapControl m_MapControl;
            public Control Control
            {
                get
                {
                    if (m_MapControl == null)
                        m_MapControl = new ESRI.ArcGIS.Controls.AxMapControl();

                    return m_MapControl;
                }
            }

            
            ESRI.ArcGIS.Controls.IHookHelper m_HookHelper;
              
            public object Hook
            {
                get {
                    if (m_HookHelper == null)
                    {
                        m_HookHelper = new ESRI.ArcGIS.Controls.HookHelperClass();
                        m_HookHelper.Hook = m_MapControl.Object;
                    }
                    return m_HookHelper;
                }
            }

            public string Caption
            {
                get { return "地图"; }
            }
        }

        public IHooker GetHooker()
        {
            return new EsriHooker();
        }
    }
}
