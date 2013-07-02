using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using Check.Engine;
using Check.Utility;

namespace Check.Command.CustomCommand
{
    /// <summary>
    /// Summary description for ExcuteTaskCommand.
    /// </summary>
    [Guid("932fc2a3-6ed4-4447-9156-2d8f62c9262e")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Check.Command.CustomCommand.ExcuteTaskCommand")]
    public sealed class ExcuteTaskCommand : BaseCommand
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

        public ExcuteTaskCommand()
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
            Check.Task.Task task = Check.Command.CheckApplication.CurrentTask;
            if (task == null)
                return;

            if (task.State != Check.Task.enumTaskState.Created)
            {
                if (DevExpress.XtraEditors.XtraMessageBox.Show("当前任务已经执行过检查，您确定要覆盖之前的检查结果吗？", "提示", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    return;
            }

            task.ReadyForCheck(true);
            TemplateRules templateRules = new TemplateRules(task.SchemaID);
            CheckApplication.TaskChanged(null);

            Check.UI.Forms.FrmTaskCheck frmCheck = new Check.UI.Forms.FrmTaskCheck(task,templateRules.CurrentSchemaRules);
            frmCheck.CheckTask();
            //if (frmCheck.DialogResult == System.Windows.Forms.DialogResult.Abort)
            //    return;

            CheckApplication.TaskChanged(task);
        }

        public override bool Enabled
        {
            get
            {
                return CheckApplication.CurrentTask != null;
            }
        }
    }
}
