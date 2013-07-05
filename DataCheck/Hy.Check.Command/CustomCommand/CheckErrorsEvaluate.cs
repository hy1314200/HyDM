using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;

using Hy.Check.UI.Forms;

namespace Hy.Check.Command.CustomCommand
{
    /// <summary>
    /// Summary description for CheckErrorsEvaluate.
    /// </summary>
    [Guid("84d08911-b89e-4c41-a6e6-c4928a23746d")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Hy.Check.Command.CustomCommand.CheckErrorsEvaluate")]
    public sealed class CheckErrorsEvaluate : BaseCommand
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

        public CheckErrorsEvaluate()
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
            FrmAppraise frm = new FrmAppraise();
            System.Data.OleDb.OleDbConnection resultConnection=CheckApplication.CurrentTask.ResultConnection as System.Data.OleDb.OleDbConnection;
            if (resultConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("当前任务状态被标记为已创建，但结果库可能已被移除。");
                return;
            }
            frm.StateApp.ResultConnection = resultConnection;

            frm.ShowDialog();

        }
        public override bool Enabled
        {
            get
            {
                return (CheckApplication.CurrentTask != null && CheckApplication.CurrentTask.State != Hy.Check.Task.enumTaskState.Created);
            }
        }
    }
}
