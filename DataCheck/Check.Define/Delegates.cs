using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Check.Define
{

    /// <summary>
    /// 消息处理委托
    /// </summary>
    /// <param name="msgType"></param>
    /// <param name="strMessage"></param>
    public delegate void MessageHandler(enumMessageType msgType, string strMessage);
}
