using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using Hy.Esri.Catalog.Utility;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Hy.Esri.Catalog.Command.Globe
{
    /// <summary>
    /// Command that works in ArcGlobe or GlobeControl
    /// </summary>
    [Guid("202933eb-ec12-48bb-9cfc-694a7abb1203")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Command.CommandAddElevation")]
    public sealed class CommandAddElevation : BaseCommand
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
            GMxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            GMxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        private IGlobeHookHelper m_globeHookHelper = null;

        public CommandAddElevation()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "3D数据管理"; //localizable text
            base.m_caption = "加载高程";  //localizable text
            base.m_message = "";  //localizable text 
            base.m_toolTip = "加载高程";  //localizable text
            base.m_name = "加载高程";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            try
            {
                m_globeHookHelper = new GlobeHookHelperClass();
                m_globeHookHelper.Hook = hook;
                if (m_globeHookHelper.ActiveViewer == null)
                {
                    m_globeHookHelper = null;
                }
            }
            catch
            {
                m_globeHookHelper = null;
            }

            if (m_globeHookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;

            m_DialogDem.Filter = "所有类型(*.img;*.dem;*.tif;*.ovr;*.sid) | *.img;*.dem;*.tif;*.ovr;*.sid";
            // TODO:  Add other initialization code
        }

        private  OpenFileDialog m_DialogDem = new OpenFileDialog();
        public override void OnClick()
        {
            //ESRI.ArcGIS.Carto.ILayer lyrElevation=m_globeHookHelper.Globe.AddLayerType(

            //ESRI.ArcGIS.Controls.ControlsAddDataCommand cmdAddData = new ControlsAddDataCommandClass();
            //cmdAddData.OnCreate(m_globeHookHelper.Hook);
            //cmdAddData.OnClick();
            
            //OpenFileDialog m_DialogDem = new OpenFileDialog();
            //m_DialogDem.Filter = "所有类型(*.img;*.dem;*.tif;*.ovr;*.sid) | *.img;*.dem;*.tif;*.ovr;*.sid";
            if (m_DialogDem.ShowDialog() != DialogResult.OK)
                return;

            string strFile = m_DialogDem.FileName;
            ILayer lyrRaster =Utility.LayerHelper.GetRasterLayer(strFile);
            if (lyrRaster != null)
            {
                try
                {
                    ISpatialReference spatialRef = (lyrRaster as IGeoDataset).SpatialReference;
                    if (spatialRef == null)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("当前选择的图层没有空间参考，无法添加!");
                        return;
                    }
                    if (spatialRef is IUnknownCoordinateSystem)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("当前选择的图层空间参考为未知空间参考类型，无法添加!");
                        return;
                    }

                    m_globeHookHelper.Globe.AddLayerType(lyrRaster, ESRI.ArcGIS.GlobeCore.esriGlobeLayerType.esriGlobeLayerTypeElevation, true);
                    //m_globeHookHelper.Camera.ZoomToRect(lyrRaster.AreaOfInterest);
                    m_globeHookHelper.ActiveViewer.Redraw(true);
                }
                catch (Exception exp)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(string.Format("抱歉，添加操作出现意外错误，信息：{0}", exp.Message));
                }
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("当前选择的图层无法打开，可能不是正确的高程图层");
            }


        }

    }
}
