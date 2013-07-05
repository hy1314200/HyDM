using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

namespace Hy.Check.Command.CustomCommand
{
    /// <summary>
    /// Summary description for ClearSelectionCommand.
    /// </summary>
    [Guid("2831a3d3-66f2-444a-ba67-e49930d9d755")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Hy.Check.Command.CustomCommand.ClearSelectionCommand")]
    public sealed class ClearSelectionCommand : BaseCommand
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper;

        public ClearSelectionCommand()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "地图工具"; //localizable text
            base.m_caption = "清除选择";  //localizable text
            base.m_message = "清除选择";  //localizable text 
            base.m_toolTip = "清除选择";  //localizable text 
        
        }

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;
            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            CheckApplication.m_UCDataMap.CurActivityMapControl.CurrentTool = null;
            IActiveView pActiveView = CheckApplication.m_UCDataMap.CurActivityMapControl.ActiveView;
            IMap pMap = CheckApplication.m_UCDataMap.CurActivityMapControl.Map;

            IGraphicsContainer pGraphContainer = pActiveView.GraphicsContainer;
            int index = 0;
            //清除绘制的要素
            //Engine_API.DeleteElementExceptionManualCheck(pMap, pGraphContainer, ref index);

            int nCount = pMap.SelectionCount;
            if (nCount > 0)
            {
                pMap.ClearSelection();
            }

            if (nCount > 0 || index > 0)
            {
                //pActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
                pActiveView.Refresh();
                pActiveView.ScreenDisplay.UpdateWindow();
            }
        }

        public override bool Enabled
        {
            get
            {
                return CheckApplication.CurrentTask != null;
            }
        }
        #endregion
    }
}
