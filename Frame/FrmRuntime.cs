using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Define;

using System.Linq;
using Utility;
using System.Collections;
using System.Threading;
using Frame.Define;

namespace Frame
{
    public partial class FrmRuntime : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FrmRuntime()
        {   
            // 加载登陆
            string[] strSplit = { "," };
            string[] strLoginor=ConfigManager.Loginor.Split(strSplit, StringSplitOptions.RemoveEmptyEntries);
            ILogin loginor = ResourceFactory.CreateInstance(strLoginor[0], strLoginor[1]) as Frame.Define.ILogin;
            if (loginor == null)
                loginor = new FrmLogin();

            loginor.Logger = Environment.Logger;
            loginor.NhibernateHelper = Environment.NHibernateHelper;
            loginor.SysConnection = Environment.SysDbConnection;
            if (!loginor.Login())
            {
                Application.Exit();
                return;
            }

            loginor.ShowMessage("正在验证GIS控件权限...");
            string errMsg = null;
            if (!Environment.ResourceManager.LicenseVerify(ref errMsg))
            {
                MessageBox.Show(errMsg);
                Application.Exit();
                return;
            }


            loginor.ShowMessage("正在加载框架界面方案...");
            InitializeComponent();
            this.Text = ConfigManager.AppName;
            this.Icon = ConfigManager.Logo;
            splitControlMain.PanelVisibility = SplitPanelVisibility.Panel1;


            loginor.ShowMessage("正在创建GIS控件对象...");
            Control hookControl = Environment.ResourceManager.GetHookControl();
            if (hookControl != null)
            {
                if (hookControl is System.ComponentModel.ISupportInitialize) ((System.ComponentModel.ISupportInitialize)(hookControl)).BeginInit();
                this.splitControlMain.Panel1.Controls.Add(hookControl);
                hookControl.Dock = DockStyle.Fill;
                if (hookControl is System.ComponentModel.ISupportInitialize) ((System.ComponentModel.ISupportInitialize)(hookControl)).EndInit();
            }
            IHook hook = Environment.ResourceManager.CreateHook(this, this.dockPanelLeft, this.dockPanelRight, this.dockPanelBottom);
         
            loginor.ShowMessage("正在加载插件...");
            IList<Define.ClassInfo> listPlugin = Environment.NHibernateHelper.GetObjectsByCondition<Define.ClassInfo>("from ClassInfo cInfo where cInfo.Type=1");
            foreach (Define.ClassInfo cInfo in listPlugin)
            {
                IPlugin plugin= Utility.ResourceFactory.CreatePlugin(cInfo);
                if (plugin != null)
                {
                    plugin.Logger = Environment.Logger;
                    plugin.NhibernateHelper = Environment.NHibernateHelper;
                    plugin.SysConnection = Environment.SysDbConnection;
                    plugin.GisWorkspace = Environment.Workspace;
                }
            }
           
            loginor.ShowMessage("正在读取界面配置...");
            IList<RibbonCommandInfo> listCommand = Environment.NHibernateHelper. GetObjectsByCondition<RibbonCommandInfo>("from RibbonCommandInfo rcInfo order by Order asc"); ;

            m_CommandInfoList = new List<RibbonCommandInfo>();
            int count = listCommand.Count;
            for (int i = 0; i < count; i++)
            {
                m_CommandInfoList.Add(listCommand[i]);
            }

              
            loginor.ShowMessage("正在创建资源...");
            ribbonEngine = new RibbonEngine();
            ribbonEngine.CommandInfoList = m_CommandInfoList;
            ribbonEngine.Ribbon = this.ribbon;
            ribbonEngine.MainMenu = this.mainMenu;
            ribbonEngine.CommandInfoFinder = delegate(string resourceID)
            {
                return m_CommandInfoList.Find(delegate(RibbonCommandInfo cmdInfo) { return cmdInfo.ID == resourceID; });
            };
            ribbonEngine.OnMessageChanged += delegate(string strMsg)
            {
                Utility.Log.AppendMessage(enumLogType.Operate, strMsg);
                loginor.ShowMessage(strMsg);
            };

            //List<ICommand>
                m_CommandList=new List<ICommand>();
            //cmdList.Add(new Commands.CommandLinkage());
            //cmdList.Add(new Utility.EsriCommandProxy(new ESRI.ArcGIS.Controls.ControlsAddDataCommandClass()));
            //cmdEngine.LoadFromCommand(new Commands.CommandLinkage(), "ESRI", "ESRI命令In Frame",null,null);
            //cmdEngine.LoadFromCommand(new Utility.EsriCommandProxy(new ESRI.ArcGIS.Controls.ControlsAddDataCommandClass()), "ESRI", "ESRI命令In Frame",null,null);

            ribbonEngine.Load(ref m_CommandList);

            
            loginor.ShowMessage("正在绑定资源...");
            //RibbonCommandAdapter 
                m_Adapter = new RibbonCommandAdapter(hook);
            m_Adapter.OnMessageChanged += delegate(string strMsg)
            {
                this.statusBarMessage.Caption = strMsg;
                //frmLogin.SetMessage(strMsg);
            };
            m_Adapter.Adapter(this.ribbon);
            m_Adapter.AddCommands(m_CommandList.ToArray());
            
            loginor.ShowMessage("正在绘制界面...");
            Thread.Sleep(1000);

            loginor.Dispose();
        }
        List<RibbonCommandInfo> m_CommandInfoList = null;
        RibbonCommandAdapter m_Adapter = null;
        List<ICommand> m_CommandList = null;
        RibbonEngine ribbonEngine = null;
        
    }
}