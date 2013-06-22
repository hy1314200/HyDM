using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Define;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace Frame
{
    internal class RibbonCommandAdapter
    {
        private  Dictionary<string, ICommand> m_DictCommands = new Dictionary<string, ICommand>();
        private object m_Hook;

        public event MessageHandler OnMessageChanged;
        protected void SendMessage(string strMsg)
        {
            if (OnMessageChanged != null)
                OnMessageChanged.Invoke(strMsg);
        }

        public RibbonCommandAdapter(object objHook)
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

            BandToHook();
            //int cmdCount = m_DictCommands.Count;
            //for (int i = 0; i < cmdCount; i++)
            //{
            //    ICommand cmdCurrent = m_DictCommands.Values.ElementAt(i);
            //    if (cmdCurrent == null)
            //        continue;

            //    cmdCurrent.OnCreate(m_Hook);
            //}
        }


        public void AddCommand(ICommand cmdNew)
        {
            if (cmdNew == null)
                return;

            string strKey=cmdNew.Name;
            if (m_DictCommands.ContainsKey(strKey))
                return;

            cmdNew.OnCreate(m_Hook);
            m_DictCommands.Add(strKey,cmdNew);
            cmdNew.OnMessage += this.SendMessage;
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
                cmdNew.OnMessage += this.SendMessage;
                m_DictCommands.Add(strCommandName, cmdNew);
            }
            catch(Exception exp)
            {
                Utility.Log.AppendMessage(enumLogType.Debug, string.Format("加载命令对象出错：{0}", exp.ToString()));
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

        public void AddCommands(ICommand[] cmds)
        {
            if (cmds == null)
                return;

            for (int i = 0; i < cmds.Length; i++)
            {
                try
                {
                    AddCommand(cmds[i]);
                }
                catch(Exception exp)
                {
                    Utility.Log.AppendMessage(enumLogType.Debug, string.Format("加载命令出错：{0}", exp.ToString()));
                }
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
        
        public void Adapter(RibbonControl ribbon)
        {            
            m_BarItems = new List<BarItem>();
            int itemCount = ribbon.Items.Count;
            for (int i = 0; i < itemCount; i++)
            {
                m_BarItems.Add(ribbon.Items[i]);

                // 将Tag作为Command解析出来
                //if (ribbon.Items[i].Tag == null)
                //    continue;

                //AddCommand(ribbon.Items[i].Tag as string);
            }
            ribbon.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BarItemClick);
            System.Windows.Forms.Application.Idle += new EventHandler(Application_Idle);

        }

        public void RefreshItem(RibbonControl ribbon)
        {
            m_BarItems = new List<BarItem>();
            int itemCount = ribbon.Items.Count;
            for (int i = 0; i < itemCount; i++)
            {
                m_BarItems.Add(ribbon.Items[i]);
            }
        }
       
        /// <summary>
        ///
        /// </summary>
        /// <param name="barItem"></param>
        public void AddItem(BarItem barItem,bool band)
        {
            m_BarItems.Add(barItem);
            if(band)
                barItem.ItemClick += new ItemClickEventHandler(BarItemClick);
        }

        public void Adapter(RibbonPage ribbonPage)
        {
            m_BarItems = new List<BarItem>();
            foreach (RibbonPageGroup rpg in ribbonPage.Groups)
            {
                BandItemLinks(rpg.ItemLinks, ref m_BarItems);
            }

            System.Windows.Forms.Application.Idle += new EventHandler(Application_Idle);
        }


        public void Adapter(RibbonPage ribbonPage, string[] strCommands)
        {
            AddCommands(strCommands);

            Adapter(ribbonPage);
        }

        private void BandItemLinks(BarItemLinkCollection linkCollection,ref List<BarItem> itemList)
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
                    BandItemLinks((barItem as BarSubItem).ItemLinks, ref itemList);
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

                if (barItem is BarCheckItem && cmdCurrent is ITool)
                {
                    (barItem as BarCheckItem).Checked = cmdCurrent.Checked;
                }
            }
        }

        private IExclusive m_ExclusiveCommand = null;
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

                // 判断独占
                if (m_ExclusiveCommand != null)
                {
                    // 必须独占的是同一对象
                    if (cmdCurrent is IExclusive && (cmdCurrent as IExclusive).Resource == m_ExclusiveCommand.Resource)
                    {
                        if (!m_ExclusiveCommand.Release())
                        {
                            SendMessage("当前命令与正在进行的操作冲突，操作中止。");
                            return;
                        }
                        //if (m_ExclusiveCommand.IsExclusiving)
                        //{
                        //    if (XtraMessageBox.Show(string.Format("当前正在进行{0}操作，您确定要中断此操作吗", (m_ExclusiveCommand as ICommand).Caption), "系统提示", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                        //    {
                        //        m_ExclusiveCommand.Release();
                        //    }
                        //    else
                        //    {
                        //        SendMessage("操作冲突，用户取消");
                        //        return;
                        //    }
                        //}

                        m_ExclusiveCommand = (cmdCurrent as IExclusive);
                    }
                }
                SendMessage(string.Format("当前操作：{0}" , cmdCurrent.Message));
                // 调用Command
                cmdCurrent.OnClick();

                //if (cmdCurrent is ITool)
                //{
                //    ITool toolCurrent = cmdCurrent as ITool;
                //    //// 制裁当前Tool
                //    //if (m_ExclusiveCommand != null)
                //    //    m_ExclusiveCommand.Release();

                //    //m_ExclusiveCommand = toolCurrent;


                //    //更新状态
                //    //foreach (BarItem barItem in m_BarItems)
                //    //{
                //    //    if (barItem is BarCheckItem && barItem != e.Item)
                //    //        (barItem as BarCheckItem).Checked = false;
                //    //}
                //    if (e.Item is BarCheckItem)
                //    {
                //        (e.Item as BarCheckItem).Checked = true;
                //    }

                //}
            }
            catch(Exception exp)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("抱歉，操作出现了意外错误，详情请查看日志!");
                Utility.Log.AppendMessage(enumLogType.Error, exp.ToString());
            }

        }
    }
}
