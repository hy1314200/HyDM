using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Define
{
    /// <summary>
    /// （交互）独占接口
    /// 实现当前接口即表示（交互）独占
    /// </summary>
    public  interface IExclusive
    {
        /// <summary>
        /// [要]被独占的资源
        /// </summary>
        object Resource { get; }
        
        /// <summary>
        /// 释放（交互）
        /// </summary>
        /// <returns>返回True表示可以被另一实例独占，否则不允许</returns>
        bool Release();
    }
}
