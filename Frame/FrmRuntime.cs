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
using DevExpress.XtraBars.Docking;
using DevExpress.XtraTab;

namespace Frame
{
    public partial class FrmRuntime : DevExpress.XtraBars.Ribbon.RibbonForm,IUIHook,IHook
    {
        public FrmRuntime()
        {   
            // 加载登陆
            string[] strSplit = { "," };
            string[] strLoginor=ConfigManager.Loginor.Split(strSplit, StringSplitOptions.RemoveEmptyEntries);
            ILogin loginor = ResourceFactory.CreateInstance(strLoginor[0], strLoginor[1]) as Frame.Define.ILogin;
            if (loginor == null)
                loginor = new FrmLogin();

            loginor.Logger = Environment.LogWriter;
            loginor.NhibernateHelper = Environment.NHibernateHelper;
            loginor.AdodbHelper = Environment.AdodbHelper;
            global::Define.IApplication app = Environment.Application;
            if (!loginor.Login(ref app))
            {
                Application.Exit();
                return;
            }
            Environment.Application = app;

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
            //splitControlMain.PanelVisibility = SplitPanelVisibility.Panel1;


            loginor.ShowMessage("正在创建GIS控件对象...");
            IHooker hooker = Environment.ResourceManager.GetHooker();
            this.AddHooker(hooker, enumDockPosition.Center);
            this.Hook = hooker.Hook;
            //Control hookControl = hooker.Control;
            //if (hookControl != null)
            //{
            //    if (hookControl is System.ComponentModel.ISupportInitialize) ((System.ComponentModel.ISupportInitialize)(hookControl)).BeginInit();
            //    //this.splitControlMain.Panel1.Controls.Add(hookControl);
            //    this.tpDefalut.Controls.Add(hookControl);
            //    this.tpDefalut.Text = hooker.Caption;
            //    hookControl.Dock = DockStyle.Fill;
            //    if (hookControl is System.ComponentModel.ISupportInitialize) ((System.ComponentModel.ISupportInitialize)(hookControl)).EndInit();
            //}
            //this.Hook = hooker.Hook;
            //this.m_DictHooker[hooker.ID] = this.tpDefalut;
         
            loginor.ShowMessage("正在加载插件...");
            IList<Define.ClassInfo> listPlugin = Environment.NHibernateHelper.GetObjectsByCondition<Define.ClassInfo>("from ClassInfo cInfo where cInfo.Type=1");
            foreach (Define.ClassInfo cInfo in listPlugin)
            {
                IPlugin plugin= Utility.ResourceFactory.CreatePlugin(cInfo);
                if (plugin != null)
                {
                    plugin.Logger = Environment.LogWriter;
                    plugin.NhibernateHelper = Environment.NHibernateHelper;
                    plugin.AdodbHelper = Environment.AdodbHelper;
                    plugin.GisWorkspace = Environment.Workspace;
                    plugin.Application = Environment.Application;
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
            ribbonEngine.OnMessage += delegate(string strMsg)
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
                m_Adapter = new RibbonCommandAdapter(this);
            m_Adapter.OnMessage += delegate(string strMsg)
            {
                this.statusBarMessage.Caption = strMsg;
                Application.DoEvents();
                //frmLogin.SetMessage(strMsg);
            };
            m_Adapter.Adapter(this.ribbon);
            m_Adapter.AddCommands(m_CommandList.ToArray());
            
            loginor.ShowMessage("正在绘制界面...");
            Thread.Sleep(1000);

            loginor.Dispose();

            //this.dockPanelBottom.Visibility = DockVisibility.Hidden;
            //this.dockPanelLeft.Visibility = DockVisibility.Hidden;
            //this.dockPanelRight.Visibility = DockVisibility.Hidden;
          
        }
        List<RibbonCommandInfo> m_CommandInfoList = null;
        RibbonCommandAdapter m_Adapter = null;
        List<ICommand> m_CommandList = null;
        RibbonEngine ribbonEngine = null;


        public Form MainForm
        {
            get { return this; }
        }

        public Control AddControl(Control ctrlTarget, enumDockPosition dockPosition)
        {
            Control ctrlNew = null;
            switch (dockPosition)
            {
                case enumDockPosition.Center:
                    XtraTabPage tpNew = new XtraTabPage();
                    this.tabCenter.TabPages.Add(tpNew);
                    ctrlNew = tpNew;
                    break;

                case enumDockPosition.Left:
                    ctrlNew =this.dockManager1.AddPanel(DockingStyle.Left);// this.dockPanelLeft.AddPanel();
                    break;

                case enumDockPosition.Right:
                    ctrlNew =this.dockManager1.AddPanel(DockingStyle.Right);// this.dockPanelRight.AddPanel();
                    break;

                case enumDockPosition.Top:
                    ctrlNew = this.dockManager1.AddPanel(DockingStyle.Top);
                    break;
                    
                case enumDockPosition.Bottom:
                    ctrlNew = this.dockManager1.AddPanel(DockingStyle.Bottom);
                    break;

                default:
                    ctrlNew= this.dockManager1.AddPanel(DockingStyle.Float);
                    break;
            }

            if (ctrlTarget is System.ComponentModel.ISupportInitialize)
                ((System.ComponentModel.ISupportInitialize)(ctrlTarget)).BeginInit();

            ctrlNew.Controls.Add(ctrlTarget);
            ctrlTarget.Dock = System.Windows.Forms.DockStyle.Fill;

            if (ctrlTarget is System.ComponentModel.ISupportInitialize)
                ((System.ComponentModel.ISupportInitialize)(ctrlTarget)).EndInit();
            
            return ctrlNew;
        }

        private Dictionary<Guid, Control> m_DictHooker = new Dictionary<Guid, Control>();
        public void AddHooker(IHooker hooker, enumDockPosition dockPosition)
        {
            if (hooker == null)
                return;

            Control ctrlParent= AddControl(hooker.Control, dockPosition);
            ctrlParent.Text = hooker.Caption;
            ctrlParent.Tag = hooker;
            m_DictHooker[hooker.ID] = ctrlParent;

            if (ctrlParent is DockPanel)
                (ctrlParent as DockPanel).Enter+=new EventHandler(ChangeHook);

            //hooker.Control .GotFocus += new EventHandler(ChangeHook);
        }

        private object m_Hook = null;
        void ChangeHook(object sender, EventArgs e)
        {
            IHooker hooker= (sender as Control).Tag as IHooker;
            if(hooker==null)
                return;

            m_Hook = hooker.Hook;
            if (m_Hook == this.Hook)
                return;

            this.Hook = m_Hook;
            this.m_Adapter.ChangeHook(this);
        }

        public void ActiveHookControl(Guid hookerID)
        {
            if (m_DictHooker.ContainsKey(hookerID))
            {
                Control ctrl = m_DictHooker[hookerID];
                if (ctrl is DockPanel)
                    (ctrl as DockPanel).Show();

                if (ctrl is XtraTabPage)
                    this.tabCenter.SelectedTabPage = (ctrl as XtraTabPage);
            }
        }

        public void CloseHookControl(Guid hookerID)
        {
            if (m_DictHooker.ContainsKey(hookerID))
            {
                Control ctrl = m_DictHooker[hookerID];
                if (ctrl is DockPanel)
                    (ctrl as DockPanel).Hide();

                if (ctrl is XtraTabPage)
                    this.tabCenter.SelectedTabPage = m_PreSelectedPage;
            }
           
        }

        public object Hook
        {
            get;
            private set;
        }

        public IUIHook UIHook
        {
            get { return this; }
        }

        private XtraTabPage m_PreSelectedPage = null;
        private void tabCenter_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            this.m_PreSelectedPage = e.PrevPage;
            ChangeHook(e.Page, null);
        }
    }
}