using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using Hy.Check.UI;
using System.Data;
using Hy.Check.UI.Forms;

namespace Hy.Check.Command.CustomCommand
{
    /// <summary>
    /// Summary description for ViewFeaturesStatisticCommand.
    /// </summary>
    [Guid("efdccda7-5a69-4929-8f94-d455ddbc27e1")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Hy.Check.Command.CustomCommand.ViewFeaturesStatisticCommand")]
    public sealed class ViewFeaturesStatisticCommand : BaseCommand
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

        public ViewFeaturesStatisticCommand()
        {
            //
            base.m_category = ""; //localizable text
            base.m_caption = "要素统计";  //localizable text
            base.m_message = "";  //localizable text 
            base.m_toolTip = "查看任务中的要素个数";  //localizable text 
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")
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
            // TODO: Add ViewFeaturesStatisticCommand.OnClick implementation
            if (CheckApplication.CurrentTask.BaseWorkspace == null)
            {
                XtraMessageBox.Show("当前任务不存在图形数据库", "警告",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            FeaturesStatistic feastres = new FeaturesStatistic(CheckApplication.CurrentTask.BaseWorkspace);
            DataTable result = feastres.GetFeaturesStatDt();

            if (result.Rows.Count==0)
            {
                XtraMessageBox.Show("当前任务中没有任何图层", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            FrmShowFeatureStatistic FrmShowFeatures = new FrmShowFeatureStatistic(result);
            FrmShowFeatures.Text = CheckApplication.CurrentTask.Name;
            FrmShowFeatures.ShowDialog();
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
