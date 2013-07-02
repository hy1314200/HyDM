using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using Check.Utility;

namespace Check.Command.CustomCommand
{
    /// <summary>
    /// Summary description for ExportToVCTCommand.
    /// </summary>
    [Guid("11c6c2e2-3716-4a69-979b-0aee369ffb8c")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Check.Command.CustomCommand.ExportToVCTCommand")]
    public sealed class ExportToVCTCommand : BaseCommand
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

        public ExportToVCTCommand()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text
            base.m_caption = "";  //localizable text
            base.m_message = "";  //localizable text 
            base.m_toolTip = "";  //localizable text 
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

        
        }

 
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;

            // TODO:  Add other initialization code
        }


        public override void OnClick()
        {

            System.Data.IDbConnection resultConnection = CheckApplication.CurrentTask.ResultConnection as System.Data.OleDb.OleDbConnection;
            if (resultConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("当前任务状态被标记为已创建，但结果库可能已被移除。");
                return;
            }

            System.Windows.Forms.SaveFileDialog dlgShpFile = new System.Windows.Forms.SaveFileDialog();
            dlgShpFile.FileName = Environment.CurrentDirectory + "\\" + CheckApplication.CurrentTask.Name + ".xls";
            dlgShpFile.Filter = "SHP 文件|*.Shp";
            if (dlgShpFile.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            string strFile = dlgShpFile.FileName;
            string strPath = System.IO.Path.GetDirectoryName(strFile);
            string strName = System.IO.Path.GetFileNameWithoutExtension(strFile);

            Check.Task.Task curTask = CheckApplication.CurrentTask;
           ErrorExporter exporter = new ErrorExporter();
            exporter.BaseWorkspace = curTask.BaseWorkspace;
            exporter.ResultConnection = curTask.ResultConnection;
            exporter.SchemaID = curTask.SchemaID;
            exporter.Topology = curTask.Topology;
            exporter.ExportToShp(strPath, strName);


        }

        public override bool Enabled
        {
            get
            {
                return (CheckApplication.CurrentTask != null && CheckApplication.CurrentTask.State != Check.Task.enumTaskState.Created);
            }
        }

    }
}
