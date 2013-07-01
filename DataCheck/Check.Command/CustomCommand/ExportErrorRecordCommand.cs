using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;

using DevExpress.XtraEditors;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Check.Utility;
using Common.Utility.Esri;

namespace CheckCommand.CustomCommand
{
    /// <summary>
    /// Summary description for ExportErrorRecordCommand.
    /// </summary>
    [Guid("cadce9ae-99fd-489e-9ee8-857283d0f674")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CheckCommand.CustomCommand.ExportErrorRecordCommand")]
    public sealed class ExportErrorRecordCommand : BaseCommand
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

        public ExportErrorRecordCommand()
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

            System.Data.IDbConnection resultConnection = CheckApplication.CurrentTask.ResultConnection as System.Data.OleDb.OleDbConnection;
            if (resultConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("当前任务状态被标记为已创建，但结果库可能已被移除。");
                return;
            }

            System.Windows.Forms.SaveFileDialog dlgShpFile = new System.Windows.Forms.SaveFileDialog();
            dlgShpFile.FileName = Environment.CurrentDirectory + "\\" + CheckApplication.CurrentTask.Name + ".shp";
            dlgShpFile.Filter = "SHP 文件|*.Shp";
            dlgShpFile.OverwritePrompt = true;
            if (dlgShpFile.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            string strFile = dlgShpFile.FileName;
            string strPath = System.IO.Path.GetDirectoryName(strFile);
            string strName = System.IO.Path.GetFileNameWithoutExtension(strFile);

            // 检查已存在
            if (System.IO.File.Exists(strFile))
            {
                //System.IO.File.Delete(dlgShpFile.FileName);
                try
                {
                    IWorkspace ws = Common.Utility.Esri.AEAccessFactory.OpenWorkspace(enumDataType.SHP, strPath);
                    IDataset dsShp = (ws as IFeatureWorkspace).OpenFeatureClass(strName) as IDataset;
                    if (dsShp != null)
                        dsShp.Delete();
                }
                catch
                {
                    try
                    {
                        System.IO.File.Delete(strFile);
                    }
                    catch
                    {
                        XtraMessageBox.Show("文件删除失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            // 开始导出
            CheckApplication.GifProgress.ShowHint("正在导出错误结果到Shp文件，请稍候……");

            Check.Task.Task curTask = CheckApplication.CurrentTask;
            ErrorExporter exporter = new ErrorExporter();
            exporter.BaseWorkspace = curTask.BaseWorkspace;
            exporter.ResultConnection = curTask.ResultConnection;
            exporter.SchemaID = curTask.SchemaID;
            exporter.Topology = curTask.Topology;

            bool isSucceed= exporter.ExportToShp(strPath, strName);

            CheckApplication.GifProgress.Hide();
            GC.Collect();

            if (isSucceed)
            {
                XtraMessageBox.Show("导出Shp成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                XtraMessageBox.Show("导出Shp失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }

        public override bool Enabled
        {
            get
            {
                return (CheckApplication.CurrentTask != null && CheckApplication.CurrentTask.State != Check.Task.enumTaskState.Created);
            }
        }
        #endregion
    }
}
