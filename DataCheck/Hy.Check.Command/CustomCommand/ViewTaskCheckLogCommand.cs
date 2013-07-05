using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;

namespace Hy.Check.Command.CustomCommand
{
    /// <summary>
    /// Summary description for ViewTaskCheckLogCommand.
    /// </summary>
    [Guid("9ff972e2-767e-4ce2-8538-ceb92466edc5")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Hy.Check.Command.CustomCommand.ViewTaskCheckLogCommand")]
    public sealed class ViewTaskCheckLogCommand : BaseCommand
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

        public ViewTaskCheckLogCommand()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text
            base.m_caption = "查看日志";  //localizable text
            base.m_message = "查看日志";  //localizable text 
            base.m_toolTip = "查看日志";  //localizable text 
            base.m_name = "查看检查日志";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

          
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
            Hy.Check.Task.Task curTask = CheckApplication.CurrentTask;
            string strLogFile = curTask.GetTaskFolder() + "\\任务日志_" + curTask.Name + "_" + curTask.ID + ".txt";
            if (!System.IO.File.Exists(strLogFile))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("当前任务日志文件不存在，请确认是否人为丢失。");
                return;
            }
            System.Diagnostics.Process.Start(System.Environment.SystemDirectory + "\\notepad.exe", strLogFile);
        }
        public override bool Enabled
        {
            get
            {
                return (CheckApplication.CurrentTask != null &&
                    CheckApplication.CurrentTask.State != Hy.Check.Task.enumTaskState.Created);
            }
        }
    }
}
