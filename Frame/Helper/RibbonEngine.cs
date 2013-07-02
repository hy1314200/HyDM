using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using Define;

using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Drawing;
using Frame.Define;


namespace Frame
{
    public delegate RibbonCommandInfo RibbonCommandInfoGetter(string resourceID);
    public class RibbonEngine
    {
        public RibbonControl Ribbon { set; private get; }

        public ApplicationMenu MainMenu { set; private get; }

        public List<RibbonCommandInfo> CommandInfoList { set; private get; }

        public event MessageHandler OnMessageChanged;


        private  Dictionary<string, RibbonPage> m_DictPage;
        private Dictionary<string, RibbonPageGroup> m_DictPageGroup;
        private Dictionary<string, object> m_DictResource;

        public RibbonCommandInfoGetter CommandInfoFinder;

        private void Init()
        {
            if (m_DictResource == null)
                m_DictResource = new Dictionary<string, object>();

            if (m_DictPage == null) // Flag
            {
                m_DictPage = new Dictionary<string, RibbonPage>();
                m_DictPageGroup = new Dictionary<string, RibbonPageGroup>();

                foreach (RibbonPage page in Ribbon.Pages)
                {
                    m_DictPage.Add(page.Text, page);

                    foreach (RibbonPageGroup pGroup in page.Groups)
                    {
                        m_DictPageGroup.Add(pGroup.Text, pGroup);
                    }
                }
            }
        }

        private object CreateControl(ICommand cmd, string caption, Image icon)
        {
            BarItem barItem = null;
            if (cmd is ICommandEx)
            {
                object objControl = (cmd as ICommandEx).ExControl;
                if (objControl is BarItem)
                {
                    barItem = objControl as BarItem;
                }
                else
                {
                    if (objControl is Control)
                    {
                        (objControl as Control).Text = caption;
                        (objControl as Control).BackgroundImage = icon;
                    }
                    return objControl;
                }
            }
            else  if (cmd is ICommand)
            {
                if (cmd is ITool)
                {
                    barItem = new BarCheckItem();
                }
                else
                {
                    barItem = new BarButtonItem();
                }
            }
            barItem.Caption = string.IsNullOrWhiteSpace(caption) ? cmd.Caption : caption;
            barItem.Glyph = icon;// (icon == null ? cmd.Icon : icon);
            barItem.Hint = cmd.Tooltip;
            barItem.Tag = cmd.Name;
            barItem.RibbonStyle = RibbonItemStyles.Large;

            return barItem;
        }

        public void Load(ref List<ICommand> cmdList)
        {
            if (Ribbon == null || CommandInfoList == null)
                return;

            if (cmdList == null)
                cmdList = new List<ICommand>();

            Init();
            int count = CommandInfoList.Count;
            for (int i = 0; i < count; i++)
            {
                RibbonCommandInfo cmdInfo = CommandInfoList[i];
                if (cmdInfo == null)
                    continue;

                ICommand cmd = LoadFromCommandInfo(cmdInfo);
                if (cmd == null)
                    continue;

                cmdList.Add(cmd);
            }
        }

        private ICommand LoadFromCommandInfo(RibbonCommandInfo cmdInfo)
        {
            if (cmdInfo == null)
                return null;

            string curPage = cmdInfo.Page;
            string curPageGroup = cmdInfo.PageGroup;

            // BarItem
            ICommand cmd = null;
            try
            {
                cmd = ResourceFactory.CreateCommand(cmdInfo.CommandClass);
                if (cmd != null)
                    LoadFromCommand(cmd, curPage, curPageGroup, cmdInfo.Caption, cmdInfo.Icon, cmdInfo.Type, cmdInfo.ID, cmdInfo.Parent == null ? null : cmdInfo.Parent.ID);

            }
            catch (Exception exp)
            {
                SendMessage(string.Format("创建{0}实例时出错，信息：{1};将跳过此命令。", cmdInfo.CommandClass.ClassName, exp.ToString(), exp.ToString()));
            }

            return cmd;
        }

        public void LoadFromCommand(ICommand cmd, string curPage, string curPageGroup, string caption, Image icon, enumCommandType cmdType,string resourceID,string parent)
        {
            Init();

            // 不重复加载
            if (m_DictResource.ContainsKey(resourceID))
                return;

            // BarItem
            object barItem = null;
            try
            {
                barItem = CreateControl(cmd, caption, icon);
            }
            catch (Exception exp)
            {
                SendMessage(string.Format("从命令{0}创建控件时出错，信息：{1}；将跳过此命令。", cmd.Name, exp.ToString(), exp.ToString()));
            }

            if (barItem == null)
                return;

            // 记录资源
            m_DictResource.Add(resourceID, barItem);

            // 如果找到父资源，则从父资源加载
            if (parent != null && barItem is BarItem)
            {
                if (!m_DictResource.ContainsKey(parent))
                {
                    if (this.CommandInfoFinder != null)
                        LoadFromCommandInfo(this.CommandInfoFinder(parent));
                }
                if (m_DictResource.ContainsKey(parent) && m_DictResource[parent] is BarSubItem)
                {
                    (m_DictResource[parent] as BarSubItem).AddItem(barItem as BarItem);
                    return;
                }
            }

            // ****** 快速访问工具栏命令 ********
            if ( cmdType == enumCommandType.Quick && barItem is BarItem)
            {                
                this.Ribbon.Toolbar.ItemLinks.Add(barItem as BarItem);
            }
            // ****** 开始工具栏命令 ********
            else if (cmdType == enumCommandType.Menu && barItem is BarItem)
            {
                if (this.MainMenu != null)
                    this.MainMenu.ItemLinks.Add(barItem as BarItem);
            }
            // ****** 状态栏命令 ********
            else if (cmdType == enumCommandType.Status && barItem is BarItem)
            {
                this.Ribbon.StatusBar.ItemLinks.Insert(0,barItem as BarItem);
            }
            // ****** 右上角 ********
            else if (cmdType == enumCommandType.PageHead && barItem is BarItem)
            {
                this.Ribbon.PageHeaderItemLinks.Add(barItem as BarItem);
            }
            else  // ****** 普通工具栏命令 ********
            {
                // Page
                if (!m_DictPage.ContainsKey(curPage))
                {

                    RibbonPage newPage = new RibbonPage(curPage);
                    Ribbon.Pages.Add(newPage);

                    m_DictPage.Add(curPage, newPage);
                }
                // PageGroup
                string groupKey = curPage + "." + curPageGroup;
                if (!m_DictPageGroup.ContainsKey(groupKey))
                {

                    RibbonPageGroup rpgNew = new RibbonPageGroup(curPageGroup);
                    m_DictPage[curPage].Groups.Add(rpgNew);

                    m_DictPageGroup.Add(groupKey, rpgNew);
                }
                if (barItem is BarItem)
                {
                    m_DictPageGroup[groupKey].ItemLinks.Add(barItem as BarItem);

                    if (barItem is BarSubItem)
                        AddSubItem(barItem as BarSubItem);

                }
                else if (barItem is Control)
                {
                    Ribbon.Controls.Add(barItem as Control);
                }
            }
        }

        private void AddSubItem(BarSubItem subItem)
        {
            foreach (LinkPersistInfo lInfo in subItem.LinksPersistInfo)
            {
                BarItem item = lInfo.Item;
                this.Ribbon.Manager.Items.Add(item);
                if (item is BarSubItem)
                    AddSubItem(item as BarSubItem);
            }
        }

        private void SendMessage(string strMsg)
        {
            if (this.OnMessageChanged != null)
                this.OnMessageChanged.Invoke(strMsg);
        }
    }
}
