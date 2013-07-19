using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Define
{
    public delegate void MessageHandler(string strMsg);


    public abstract class Messager
    {
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
