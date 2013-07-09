using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using Utility;
using System.Collections;
using System.Drawing;
using Define;

namespace Frame
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string[] strSplit = { "," };
            string[] strCreators=ConfigManager.EnvironmentCreator.Split(strSplit,StringSplitOptions.RemoveEmptyEntries);

            Define.IEnvironmentCreator envCreator = ResourceFactory.CreateInstance(strCreators[0], strCreators[1]) as Frame.Define.IEnvironmentCreator;
            Environment.Application = envCreator.Application;
            Environment.LogWriter = envCreator.LogWriter;
            Environment.NHibernateHelper = envCreator.NhibernateHelper;
            Environment.AdodbHelper = envCreator.AdodbHelper;

            string[] strResources = ConfigManager.ResourceManager.Split(strSplit, StringSplitOptions.RemoveEmptyEntries);
            Frame.Define.IResourceManager rManager=ResourceFactory.CreateInstance(strResources[0],strResources[1]) as Frame.Define.IResourceManager;
            if (rManager == null)
            {
                MessageBox.Show("框架资源处理器加载失败，请确认配置正确！");
                Application.Exit();
                return;
            }
            Environment.ResourceManager = rManager; 

            IList<string> li=Environment.NHibernateHelper.GetObjectsByCondition<string>("select cInfo.ClassName from ClassInfo cInfo");

            //IDbConnection sysConnection = Utility.DataFactory.GetConnection(ConfigManager.ADOType, ConfigManager.ADOConnection);
            //NhibernateHelper nhHelper = Utility.DataFactory.GetNhibernateHelper(sysConnection, ConfigManager.HibernateAssemblys);// new NHibernate.JetDriver.JetDbConnection(sysConnection as System.Data.OleDb.OleDbConnection), ConfigManager.HibernateAssemblys);

            //IList list = nhHelper.GetAll(typeof(RibbonCommandInfo));

            //RibbonCommandInfo rcInfo = list[0] as RibbonCommandInfo;
            //RibbonCommandInfo rcInfo2 = new RibbonCommandInfo();
            //rcInfo2.Page = "ass";
            //rcInfo2.PageGroup = "sadfas";
            //rcInfo2.Icon=Bitmap.FromFile(@"E:\Icons\GeoprocessingModelLocked32.png");
            //rcInfo2.CommandClass = rcInfo.CommandClass;
            //rcInfo2.Caption = "Asdfasdf";
            //nhHelper.SaveObject(rcInfo2);

            //应用dev的主题
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            if (!DevExpress.Skins.SkinManager.AllowFormSkins ||
               !DevExpress.Utils.Drawing.Helpers.NativeVista.IsVista)
            {
                DevExpress.Skins.SkinManager.EnableFormSkins();
            }

            Form frmStart = null;
            if (args != null && args.Length>0)
            {
                if (args[0] == "c")
                {
                    string errMsg = null;
                    if (!Environment.ResourceManager.LicenseVerify(ref errMsg))
                    {
                        if (MessageBox.Show("GIS控件权限验证失败！\n可能无法正确进行配置和显示，但与GIS无关的资源仍能正常使用！\n要继续吗？\n","GIS控件权限提示",MessageBoxButtons.YesNo) != DialogResult.Yes)
                        {
                            Application.Exit();
                            return;
                        }
                    }
                    frmStart = new FrmConfig();
                }
                else
                {
                    frmStart = new FrmRuntime();
                }
            }
            else
            {
                frmStart = new FrmRuntime();
            }
            Application.Run(frmStart);

            Environment.ResourceManager.Release();
        }
    }
}
