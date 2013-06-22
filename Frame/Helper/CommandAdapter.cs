using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars;
using Define;

namespace Frame
{
    public class CommandAdapter
    {
        public static void LoadAssimbly(string assemblyName)
        {
            System.Reflection.Emit.AssemblyBuilder.Load(assemblyName);
        }
        private  Dictionary<string, ICommand> m_DictCommands = new Dictionary<string, ICommand>();
        private object m_Hook;
        public CommandAdapter(object objHook)
        {
            m_Hook=objHook;
        }
        
        /// <summary>
        /// 切换Hook
        /// </summary>
        /// <param name="objHook"></param>
        public void ChangeHook(object objHook)
        {
            m_Hook=objHook;

            int cmdCount = m_DictCommands.Count;
            for (int i = 0; i < cmdCount; i++)
            {
                ICommand cmdCurrent = m_DictCommands.Values.ElementAt(i);
                if (cmdCurrent == null)
                    continue;

                cmdCurrent.OnCreate(m_Hook);
            }
        }


        public void AddCommand(ICommand cmdNew)
        {
            if (cmdNew == null)
                return;

            string strKey=cmdNew.GetType().FullName;
            if (m_DictCommands.ContainsKey(strKey))
                return;

            cmdNew.OnCreate(m_Hook);
            m_DictCommands.Add(strKey,cmdNew);
        }

        public void AddCommand(string strCommandName)
        {
            try
            {
                ICommand cmdNew = Activator.CreateInstance(Type.GetType(strCommandName)) as ICommand;
                if (cmdNew == null)
                    return;

                if (m_DictCommands.ContainsKey(strCommandName))
                    return;

                cmdNew.OnCreate(m_Hook);
                m_DictCommands.Add(strCommandName, cmdNew);
            }
            catch
            {
            }
        }

        public  void AddCommands(string[] strCommandNames)
        {
            if (strCommandNames == null)
                return;

            for (int i = 0; i < strCommandNames.Length; i++)
            {
                AddCommand(strCommandNames[i]);
            }
        }

        public void BandToHook()
        {
            for (int i = 0; i < m_DictCommands.Count; i++)
            {
                ICommand cmdCurrent=m_DictCommands.ElementAt(i).Value;
                if(cmdCurrent!=null)
                   cmdCurrent.OnCreate(m_Hook);
            }
        }

        public void Adapter(BarManager barManager)
        {
            m_BarItems = new List<BarItem>();
            int itemCount = barManager.Items.Count;
            for (int i = 0; i < itemCount; i++)
            {
                m_BarItems.Add(barManager.Items[i]);

                // 将Tag作为Command解析出来
                if (barManager.Items[i].Tag == null)
                    continue;

                AddCommand(barManager.Items[i].Tag as string);
            }
            barManager.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BarItemClick);

            System.Windows.Forms.Application.Idle += new EventHandler(Application_Idle);

        }

        public void Adapter(BarManager barManager, string[] strCommands)
        {
            AddCommands(strCommands);

            m_BarItems = new List<BarItem>();
            int itemCount = barManager.Items.Count;
            for (int i = 0; i < itemCount; i++)
            {
                m_BarItems.Add(barManager.Items[i]);
            }
            barManager.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BarItemClick);

            System.Windows.Forms.Application.Idle += new EventHandler(Application_Idle);
            //System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            //timer.Interval = 1000;
            //timer.Tick += new EventHandler(timer_Tick);
            //timer.Start();
        }

        public void Adapter(Bar barTarget)
        {
            m_BarItems = new List<BarItem>();
            BandItemLinks2(barTarget.ItemLinks, ref m_BarItems);

            System.Windows.Forms.Application.Idle += new EventHandler(Application_Idle);
        }


        public void Adapter(Bar barTarget, string[] strCommands)
        {
            AddCommands(strCommands);
            m_BarItems = new List<BarItem>();
            BandItemLinks(barTarget.ItemLinks, ref m_BarItems);

            System.Windows.Forms.Application.Idle += new EventHandler(Application_Idle);
        }

        private void BandItemLinks(BarItemLinkCollection linkCollection, ref List<BarItem> itemList)
        {
            int itemCount = linkCollection.Count;
            for (int i = 0; i < itemCount; i++)
            {
                BarItem barItem = linkCollection[i].Item;
                m_BarItems.Add(barItem);
                barItem.ItemClick += new ItemClickEventHandler(BarItemClick);

                if (barItem is BarSubItem)
                {
                    BandItemLinks((barItem as BarSubItem).ItemLinks, ref itemList);
                }
            }
        }

        private void BandItemLinks2(BarItemLinkCollection linkCollection,ref List<BarItem> itemList)
        {
            int itemCount = linkCollection.Count;
            for (int i = 0; i < itemCount; i++)
            {
                BarItem barItem = linkCollection[i].Item;

                if (barItem.Tag != null) // 将Tag作为Command解析出来
                {
                    AddCommand(barItem.Tag as string);
                }

                m_BarItems.Add(barItem);
                barItem.ItemClick += new ItemClickEventHandler(BarItemClick);

                if (barItem is BarSubItem)
                {
                    BandItemLinks2((barItem as BarSubItem).ItemLinks, ref itemList);
                } 
            }
        }
        
        //void timer_Tick(object sender, EventArgs e)
        //{

        //}
        private List<BarItem> m_BarItems;
        void Application_Idle(object sender, EventArgs e)
        {
            foreach (BarItem barItem in m_BarItems)
            {
                if (barItem.Tag == null)
                    continue;

                if (!m_DictCommands.ContainsKey(barItem.Tag as string))
                    continue;

                ICommand cmdCurrent = m_DictCommands[barItem.Tag as string] as ICommand;
                if (cmdCurrent == null)
                    continue;

                barItem.Enabled = cmdCurrent.Enabled;

                //if (barItem is BarCheckItem && cmdCurrent is ITool)
                //{
                //    (barItem as BarCheckItem).Checked = cmdCurrent.Checked;
                //}
            }
        }

        private ITool m_CurrentTool = null;
        private void BarItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (e.Item.Tag == null)
                    return;

                if (!m_DictCommands.ContainsKey(e.Item.Tag as string))
                    return;
                ICommand cmdCurrent = m_DictCommands[e.Item.Tag as string] as ICommand;
                if (cmdCurrent == null)
                    return;


                // 调用Command
                cmdCurrent.OnClick();

                if (cmdCurrent is ITool)
                {
                    ITool toolCurrent = cmdCurrent as ITool;
                    // 制裁当前Tool
                    if (m_CurrentTool != null)
                        m_CurrentTool.Release();

                    m_CurrentTool = toolCurrent;

                    //if (m_Hook is IMapControl4)
                    //{
                    //    (m_Hook as IMapControl4).CurrentTool = toolCurrent;
                    //}
                    //else if (m_Hook is IPageLayoutControl)
                    //{
                    //    (m_Hook as IPageLayoutControl).CurrentTool = toolCurrent;
                    //}
                    //else if (m_Hook is IGlobeControl)
                    //{
                    //    (m_Hook as IGlobeControl).CurrentTool = toolCurrent;
                    //}
                    //else if (m_Hook is ISceneControl)
                    //{
                    //    (m_Hook as ISceneControl).CurrentTool = toolCurrent;
                    //}
                    //else if (m_Hook is TEHookHelper)
                    //{
                    //    (m_Hook as TEHookHelper).TETool = toolCurrent;
                    //}

                    //更新状态
                    foreach (BarItem barItem in m_BarItems)
                    {
                        if (barItem is BarCheckItem && barItem != e.Item)
                            (barItem as BarCheckItem).Checked = false;
                    }
                    if (e.Item is BarCheckItem)
                    {
                        (e.Item as BarCheckItem).Checked = true;
                    }

                }
            }
            catch
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("抱歉，操作出现了意外错误，详情请联系三正科技有限公司咨询!");
            }

        }
    }
}
