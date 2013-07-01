using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using Check.Engine;
using Check.UI.Forms;
using System.Windows.Forms;
using Check.Define;
using System.Collections.Generic;

namespace CheckCommand.CustomCommand
{
    /// <summary>
    /// Summary description for PreCheckCommand.
    /// </summary>
    [Guid("46b91a85-8aa4-4a2a-beeb-f7b806a0c09a")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CheckCommand.CustomCommand.PreCheckCommand")]
    public sealed class PreCheckCommand : BaseCommand
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

        public PreCheckCommand()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text
            base.m_caption = "";  //localizable text
            base.m_message = "";  //localizable text 
            base.m_toolTip = "";  //localizable text 
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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

            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;

            // TODO:  Add other initialization code
        }

        public override void OnClick()
        {
            List<SchemaRuleEx> ruleSelection = null;
            FrmPreCheck frmPreCheck = new FrmPreCheck();
            frmPreCheck.CurrentTemplateRules = CheckApplication.InitCurrentTemplateRules();
            frmPreCheck.SchemaRulesSelection = ruleSelection;
            if (frmPreCheck.ShowDialog() == DialogResult.Yes)
            {
                Check.Task.Task task = CheckCommand.CheckApplication.CurrentTask;
                task.ReadyForCheck(false);
                ruleSelection = frmPreCheck.SchemaRulesSelection;
                CheckApplication.TaskChanged(null);
                Check.UI.Forms.FrmTaskCheck frmCheck = new Check.UI.Forms.FrmTaskCheck(task, ruleSelection);
                frmCheck.CheckTask();
                CheckApplication.TaskChanged(task);
            }
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
