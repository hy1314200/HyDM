using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Define
{
    /// <summary>
    /// 通用命令接口
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 图标
        /// </summary>
        Image Icon { get; }
       
        /// <summary>
        /// 标题
        /// </summary>
        string Caption { get; }
      
        /// <summary>
        /// 类别
        /// </summary>
        string Category { get; }
    
        /// <summary>
        /// 是否按下
        /// </summary>
        bool Checked { get; }
      
        /// <summary>
        /// 是否可用
        /// </summary>
        bool Enabled { get; }
      
        /// <summary>
        /// 消息
        /// </summary>
        string Message { get; }
     
        /// <summary>
        /// 名称，请提供唯一标识的名称
        /// </summary>
        string Name { get; }
    
        /// <summary>
        /// 提示信息
        /// </summary>
        string Tooltip { get; }
        
        /// <summary>
        /// 单击
        /// </summary>
        void OnClick();
    
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="Hook"></param>
        void OnCreate(object Hook);

        event MessageHandler OnMessage;
    }


    public delegate void MessageHandler(string strMsg);
}
