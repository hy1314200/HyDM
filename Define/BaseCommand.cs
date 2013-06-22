using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Define;

namespace Define
{
    /// <summary>
    /// 为通用Command提供基础实现
    /// </summary>
    public abstract class BaseCommand:ICommand
    {
        protected System.Drawing.Image m_Icon= Properties.Resources.DefaultIcon;
        protected string m_Category="未分类";
        protected string m_Name = null;
        protected string m_Caption = null;
        protected string m_Message = null;
        protected string m_Tooltip = null;
        protected IHook m_Hooker;

        public virtual System.Drawing.Image Icon
        {
            get { return m_Icon; }
        }

        public virtual string Name
        {
            get
            {
                if (string.IsNullOrEmpty(m_Name))
                    return this.GetType().FullName;

                return m_Name;
            }
        }

        public virtual string Category
        {
            get { return m_Category; }
        }

        public virtual string Caption
        {
            get
            {
                return m_Caption;
            }
        }

        public virtual string Message
        {
            get
            {
                if(m_Message==null)
                    return this.Caption;

                return m_Message;
            }
        }

        public virtual string Tooltip
        {
            get
            {
                if (string.IsNullOrEmpty(m_Tooltip))
                    return this.Message;

                return m_Tooltip;
            }
        }

        public virtual bool Checked
        {
            get { return false; }
        }

        public virtual bool Enabled
        {
            get
            {
                return (m_Hooker != null);
            }
        }


        public virtual void OnCreate(object Hook)
        {
            this.m_Hooker = Hook as IHook;
        }

        public abstract void OnClick();

        public event MessageHandler OnMessage;

        /// <summary>
        /// 为所有子类提供发送消息的方法
        /// </summary>
        /// <param name="strMsg"></param>
        protected void SendMessage(string strMsg)
        {
            if (this.OnMessage != null)
                this.OnMessage.Invoke(strMsg);
        }
    }
}
