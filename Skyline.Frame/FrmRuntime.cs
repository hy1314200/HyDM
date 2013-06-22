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
using ESRIDefine;

namespace Frame
{
    public partial class FrmRuntime : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private class FrameHook : IHook,IESRIHook,IUIHook
        {
            public FrameHook(
                Form frmMain,
                TerraExplorerX.ITerraExplorer te,
                TerraExplorerX.ISGWorld61 sgWorld,
                ESRI.ArcGIS.Controls.IMapControl4 mapControl,
                Control dockPanel
                )
            {
                this.MainForm = frmMain;
                this.MapControl = mapControl;
                this.RightDockPanel = dockPanel;
                this.SGWorld = sgWorld;
                this.TerraExplorer = te;
            }

            public Form MainForm
            {
                get ;private set;
            }

            public TerraExplorerX.ITerraExplorer TerraExplorer
            {
                get;
                private set;
            }

            public TerraExplorerX.ISGWorld61 SGWorld
            {
                get; private set;
            }

            public ESRI.ArcGIS.Controls.IMapControl4 MapControl
            {
                get ;private set;
            }

            public Control RightDockPanel
            {
                get; private set;
            }


            public object Tag
            {
                get;
                set;
            }
        }


        
        public FrmRuntime()
        {

            FrmLoging frmLogin = null;
            bool flag = true;
            ThreadStart t = delegate
            {
                frmLogin= new FrmLoging();
                flag = false;
                frmLogin.ShowDialog();
            };
            Thread thread = new Thread(t);
            thread.Start();

            while (flag)
            {
                Thread.Sleep(10);
            }

            frmLogin.SetMessage("正在加载框架界面方案...");

            InitializeComponent();

            frmLogin.SetMessage("正在创建三维控件对象...");
            this.Text = ConfigManager.AppName;
            splitControlMain.PanelVisibility = SplitPanelVisibility.Panel1;


            TerraExplorerX.SGWorld61 sgwTopLeft;
            TerraExplorerX.SGWorld61 sgwTopRight;
            TerraExplorerX.SGWorld61 sgwBottomLeft;
            TerraExplorerX.SGWorld61 sgwBottomRight;
            this.uC3DWindow1.CreateHooker(out sgwTopLeft, out sgwTopRight, out sgwBottomLeft, out sgwBottomRight);

            frmLogin.SetMessage("正在创建绑定对象...");
            FrameHook hook = new FrameHook(this, new TerraExplorerX.TerraExplorerClass(), sgwTopLeft, this.axMapControl1.Object as ESRI.ArcGIS.Controls.IMapControl4, this.dockPanelRight);

            // 测试
           // // Dictionary<string, Utility.enumCommandType> d = Utility.CommandFactory.GetCommandClasses(@"C:\Program Files (x86)\ArcGIS\DeveloperKit10.0\DotNet\ESRI.ArcGIS.Controls.dll");
           //  Dictionary<string, enumResourceType> dic = Utility.ResourceFactory.GetResources(@"C:\Program Files (x86)\ArcGIS\DeveloperKit10.0\DotNet\ESRI.ArcGIS.Controls.dll");
           // int count = dic.Count;
           //// List<Utility.RibbonCommandInfo> 
           //     infoList = new List<RibbonCommandInfo>();
           // for (int i = 0; i < count; i++)
           // {
           //     RibbonCommandInfo info = new RibbonCommandInfo();

           //     ClassInfo cInfo = new ClassInfo();
           //     cInfo.DllName = "ESRI.ArcGIS.Controls.dll";
           //     cInfo.ClassName = dic.Keys.ElementAt(i);// +", ESRI.ArcGIS.Controls";

           //     info.CommandClass = cInfo;
           //     info.Page = "ESRI";
           //     info.PageGroup = "ESRI命令测试";

           //     infoList.Add(info);
           // }
            IList listPlugin = Environment.NHibernateHelper.GetObjectByCondition("from ClassInfo cInfo where cInfo.Type=1");
            foreach (object cInfo in listPlugin)
            {
                IPlugin plugin= Utility.ResourceFactory.CreatePlugin(cInfo as ClassInfo);
                if (plugin != null)
                {
                    plugin.Logger = Environment.Logger;
                    plugin.NhibernateHelper = Environment.NHibernateHelper;
                    plugin.SysConnection = Environment.SysDbConnection;
                    plugin.GisWorkspace = Environment.Workspace;
                }
            }
           
            frmLogin.SetMessage("正在读取界面配置...");
            IList listCommand = Environment.NHibernateHelper. GetObjectByCondition("from RibbonCommandInfo rcInfo order by Order asc"); ;

            infoList = new List<RibbonCommandInfo>();
            int count = listCommand.Count;
            for (int i = 0; i < count; i++)
            {
                infoList.Add(listCommand[i] as RibbonCommandInfo);
            }

              
            frmLogin.SetMessage("正在创建资源...");
            cmdEngine = new RibbonEngine();
            cmdEngine.CommandInfoList = infoList;
            cmdEngine.Ribbon = this.ribbon;
            cmdEngine.OnMessageChanged += delegate(string strMsg)
            {
                Utility.Log.AppendMessage(enumLogType.Operate, strMsg);
                frmLogin.SetMessage(strMsg);
            };

            //List<ICommand>
                cmdList=new List<ICommand>();
            //cmdList.Add(new Commands.CommandLinkage());
            //cmdList.Add(new Utility.EsriCommandProxy(new ESRI.ArcGIS.Controls.ControlsAddDataCommandClass()));
            //cmdEngine.LoadFromCommand(new Commands.CommandLinkage(), "ESRI", "ESRI命令In Frame",null,null);
            //cmdEngine.LoadFromCommand(new Utility.EsriCommandProxy(new ESRI.ArcGIS.Controls.ControlsAddDataCommandClass()), "ESRI", "ESRI命令In Frame",null,null);

            cmdEngine.Load(ref cmdList);

            
            frmLogin.SetMessage("正在绑定资源...");
            //RibbonCommandAdapter 
                cmdAdapter = new RibbonCommandAdapter(hook);
            cmdAdapter.OnMessageChanged += delegate(string strMsg)
            {
                //this.statusBarMessage.Caption = strMsg;
                frmLogin.SetMessage(strMsg);
            };
            cmdAdapter.Adapter(this.ribbon);
            cmdAdapter.AddCommands(cmdList.ToArray());


            axTOCControl1.SetBuddyControl(this.axMapControl1);

            frmLogin.SetMessage("正在绘制界面...");
            Thread.Sleep(1000);

            thread.Abort();
        }
        List<RibbonCommandInfo> infoList = null;
        RibbonCommandAdapter cmdAdapter = null;
        List<ICommand> cmdList = null;
        RibbonEngine cmdEngine = null;
        int m_count = 30;

        /// <summary>
        /// 二维相关控件可见的消息标志
        /// </summary>
        const int MSGID_Set2Domen = 40001;
        /// <summary>
        /// 三维控件模式（多屏对比）消息标志
        /// </summary>
        const int MSGID_Set3DMode = 40002;

        private void Set2DomenVisible(bool visible)
        {
            splitControlMain.PanelVisibility = (visible ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel1);
            tpToc.PageVisible = visible;
            tabControlTree.ShowTabHeader = (visible ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False);
            if (visible)
            {
                splitControlMain.SplitterPosition = (int)(splitControlMain.Width / 2);
            }
        }

        private void Set3DControlMode(enum3DControlMode mode)
        {
            this.uC3DWindow1.ControlMode = mode;
        }

        protected override void WndProc(ref Message msg)
        {
            switch (msg.Msg)
            {
                    // 设置二三维
                case MSGID_Set2Domen:
                    bool visible = ((int)msg.WParam > 0);
                    this.Set2DomenVisible(visible);
                    return;

                    // 设置三维多屏对比
                case MSGID_Set3DMode:
                    enum3DControlMode mode = (enum3DControlMode)((int)msg.WParam);
                    this.Set3DControlMode(mode);
                    return;
            }

            base.WndProc(ref msg);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_count++;
            ICommand cmd=Utility.ResourceFactory.CreateCommand(infoList[m_count].CommandClass);
            cmdEngine.LoadFromCommand(cmd, "测试", "测试",null,null);

            cmdAdapter.AddCommand(cmd);

            cmdAdapter.RefreshItem(ribbon);
        }
    }
}