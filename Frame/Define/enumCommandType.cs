using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame.Define
{
    public enum enumCommandType
    {
        /// <summary>
        /// 普通工具栏命令
        /// </summary>
        Normal=0,

        /// <summary>
        /// 快速访问栏命令
        /// </summary>
        Quick=1,

        /// <summary>
        /// 开始菜单栏命令
        /// </summary>
        Menu=2,

        /// <summary>
        /// 状态栏
        /// </summary>
        Status=3,

        /// <summary>
        /// 右上角
        /// </summary>
        PageHead=4

    }
}
