using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Check.UI.Forms;
using ESRI.ArcGIS.Geometry;

namespace Check.Command.CustomCommand
{
    /// <summary>
    /// Summary description for Identify.
    /// </summary>
    [Guid("a7d4bac8-4a8f-459a-879a-209c6d8d64ad")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Check.Command.CustomCommand.Identify")]
    public sealed class Identify : BaseTool
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
        //Windows API functions to capture mouse and keyboard input to a window when the mouse is outside the window
        [DllImport("user32.dll")]
        public static extern long SetCapture(int hwnd);
        [DllImport("user32.dll")]
        public static extern long ReleaseCapture();
        public IMap m_pMap;
        public IActiveView m_pActiveView;

        public FrmFtAttr frmFtAttrForm;

        private IHookHelper m_hookHelper = null;
        private IPoint m_Point;
        private INewEnvelopeFeedback m_Feedback;
        private bool m_InUse;
        private bool m_InOpen;

        public Identify()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "地图工具"; //localizable text 
            base.m_caption = "属性显示";  //localizable text 
            base.m_message = "属性显示";  //localizable text
            base.m_toolTip = "显示要素的属性信息";  //localizable text
            base.m_name = "属性显示";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_pActiveView == null)
                {
                    m_hookHelper = null;
                }
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;

            // TODO:  Add Identify.OnCreate implementation
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add Identify.OnClick implementation
            m_pMap = CheckApplication.m_UCDataMap.CurActivityMapControl.Map;
            m_pActiveView = CheckApplication.m_UCDataMap.CurActivityMapControl.ActiveView;
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (Button == 2)
            {
                return;
            }

            m_Feedback = null;
            //Get the focus map 
            IActiveView activeView = (IActiveView)m_pMap;
            //Get the point to start the feedback with
            m_Point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            m_InUse = true;
            SetCapture(m_pActiveView.ScreenDisplay.hWnd);
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            if (Button == 2)
            {
                return;
            }

            if (!m_InUse) return;

            IActiveView activeView = (IActiveView)m_pMap;
            //Start the feedback if this is the first mouse move event
            if (m_Feedback == null)
            {
                m_Feedback = new NewEnvelopeFeedbackClass();
                m_Feedback.Display = activeView.ScreenDisplay;
                m_Feedback.Start(m_Point);
            }
            //Move the feedback to the new mouse coordinates
            m_Feedback.MoveTo(activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y));
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            if (Button == 2)
            {
                return;
            }

            //if (m_InUse == false) return;
            ReleaseCapture();

            //Get the search geometry
            IGeometry geom;
            if (m_Feedback == null)
            {
                geom = m_Point;
            }
            else
            {
                geom = m_Feedback.Stop();
                if (geom.IsEmpty)
                    geom = m_Point;
            }

            //Set the spatial reference of the search geometry to that of the Map
            IMap map = m_pMap;
            ISpatialReference spatialReference = map.SpatialReference;
            geom.SpatialReference = spatialReference;

            map.SelectByShape(geom, null, false);
            //m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            m_pActiveView.Refresh();

            //Refresh the active view
            if (frmFtAttrForm == null || frmFtAttrForm.Visible == false)
            {

                frmFtAttrForm = new FrmFtAttr(m_pActiveView, CheckApplication.m_UCDataMap.CurActivityMapControl);

                frmFtAttrForm.Show();

            }

            frmFtAttrForm.UpdateFeature(map, geom);
            //m_Feedback = null;
            m_InUse = false;
            ReleaseCapture();
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
