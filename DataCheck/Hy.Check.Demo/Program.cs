using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Hy.Common.Utility.Esri;
using DevExpress.Utils.Drawing.Helpers;
using Hy.Common.Utility.Log;
using Hy.Common.UI;
using Hy.Check.Demo.Helper;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using Hy.Check.Utility;

namespace Hy.Check.Demo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SplashScreen.ShowSplashScreen();

            //1、检查只允许一个实例运行
            Process[] ps = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (ps.Length > 1)
            {
                XtraMessageBox.Show("质检Demo程序已经启动！", COMMONCONST.MESSAGEBOX_WARING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SplashScreen.CloseForm();
                return;
            }


            //2、检查公司的lic权限

            //3、检查arcgis license权限
            LicenseInitializer m_AOLicenseInitializer = new LicenseInitializer();
            if (!m_AOLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB },
            new esriLicenseExtensionCode[] { esriLicenseExtensionCode.esriLicenseExtensionCodeSpatialAnalyst }))
            {
                SplashScreen.CloseForm();
                XtraMessageBox.Show("Arcgis许可验证失败", COMMONCONST.MESSAGEBOX_WARING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //应用dev的主题
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            if (!DevExpress.Skins.SkinManager.AllowFormSkins ||
               !NativeVista.IsVista)
            {
                DevExpress.Skins.SkinManager.EnableFormSkins();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            //开启日志记录
            OperationalLogManager.SetAutoFlush(true);

            OperationalLogManager.AppendSeparator();
            OperationalLogManager.AppendSeparator();

            //读取环境变量
            ConfigManager.GetEnvironVariable();

            //运行主程序
            Application.Run(new RibbonFrmMain());
        }

        static void Application_ApplicationExit(object sender, System.EventArgs e)
        {

            //ESRI License Initializer generated code.
            //Do not make any call to ArcObjects after ShutDownApplication()
            //m_AOLicenseInitializer.ShutdownApplication();
        }

        static void Application_ThreadException(Object seder, System.Threading.ThreadExceptionEventArgs e)
        {
            DialogResult result = DialogResult.Cancel;

            OperationalLogManager.AppendMessage(e.Exception.Message);
            OperationalLogManager.AppendMessage(e.Exception.ToString());

            string errorMsg = "程序出现错误需要关闭，请联系数慧客服,解决此问题! ";
            try
            {
                result = MessageBox.Show(errorMsg, COMMONCONST.MESSAGEBOX_ERROR, MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                // Exits the program when the user clicks Abort.
                if (result == DialogResult.OK)
                    //关闭当前程序
                    System.Environment.Exit(System.Environment.ExitCode);
                    //Application.ExitThread();
                    //Application.Exit();
            }
            catch
            {

            }
        }

        static void CurrentDomain_UnhandledException(Object seder, System.UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            OperationalLogManager.AppendMessage(ex.Message);
            OperationalLogManager.AppendMessage(ex.ToString());
        }
    }
}