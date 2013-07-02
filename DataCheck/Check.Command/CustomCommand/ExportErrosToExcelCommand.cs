using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;

using DevExpress.XtraEditors;
using System.Windows.Forms;
using Check.Utility;

namespace Check.Command.CustomCommand
{
    /// <summary>
    /// Summary description for ExportErrosToExcelCommand.
    /// </summary>
    [Guid("2d3194e1-3bc0-4d01-be87-d810b1965a64")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Check.Command.CustomCommand.ExportErrosToExcelCommand")]
    public sealed class ExportErrosToExcelCommand : BaseCommand
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

        public ExportErrosToExcelCommand()
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

            System.Windows.Forms.SaveFileDialog dlgExcelFile = new System.Windows.Forms.SaveFileDialog();
            dlgExcelFile.FileName = Environment.CurrentDirectory+"\\"+CheckApplication.CurrentTask.Name+".xls";
            dlgExcelFile.Filter = "Excel 文件|*.excel";
            dlgExcelFile.OverwritePrompt = true;
            if (dlgExcelFile.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            if (System.IO.File.Exists(dlgExcelFile.FileName))
            {
                try
                {
                    System.IO.File.Delete(dlgExcelFile.FileName);
                }
                catch
                {
                    XtraMessageBox.Show("文件删除失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            string strFile = dlgExcelFile.FileName;
            CheckApplication.ProgressBar.ShowHint("正在读取错误记录……");
            System.Data.DataTable dtError= ErrorExporter.GetError(resultConnection);
            if (ErrorExporter.ExportToExcel(CheckApplication.ProgressBar, dtError, strFile))
            {
                XtraMessageBox.Show("导出错误记录到Excel文件成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                XtraMessageBox.Show("导出错误记录到Excel文件失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            CheckApplication.ProgressBar.Hide();

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
