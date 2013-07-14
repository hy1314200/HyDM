using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;

namespace Hy.Esri.Catalog.Command.Globe
{
    /// <summary>
    /// Command that works in ArcGlobe or GlobeControl
    /// </summary>
    [Guid("21206365-455b-46d2-ac6d-139e2499ac17")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Command.CommandSaveGlobe")]
    public sealed class CommandSaveGlobe : BaseCommand
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

        public CommandSaveGlobe()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "3D数据管理"; //localizable text
            base.m_caption = "保存场景";  //localizable text
            base.m_message = "";  //localizable text 
            base.m_toolTip = "保存场景";  //localizable text
            base.m_name = "保存场景";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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


            m_DialogSaveGlobe = new SaveFileDialog();
            m_DialogSaveGlobe.Filter = "Globe场景(*.3dd) |*.3dd";
        }
        private SaveFileDialog m_DialogSaveGlobe;
        public override void OnClick()
        {
            if (m_DialogSaveGlobe.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    IMemoryBlobStream pMemoryBlobStream = new MemoryBlobStream();
                    IObjectStream pObjectStream = new ObjectStream();
                    pObjectStream.Stream = pMemoryBlobStream;
                    IPersistStream pPersistStream = m_globeHookHelper.Globe as IPersistStream;
                    pPersistStream.Save(pObjectStream, 1);
                    pMemoryBlobStream.SaveToFile(m_DialogSaveGlobe.FileName);
                    IMapDocument mapDoc;
                }
                catch(Exception exp)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(string.Format("抱歉，保存操作出现意外错误，信息：{0}", exp.Message));
                }
            }
        }

    }
}
