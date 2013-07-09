using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Define
{
    /// <summary>
    /// Hooker
    /// 提供HookControl和Hook
    /// </summary>
    public interface IHooker
    {
        /// <summary>
        /// 标题
        /// </summary>
        string Caption { get; }
        /// <summary>
        /// 标识
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// Hook控件
        /// </summary>
        System.Windows.Forms.Control Control { get; }

        /// <summary>
        /// Hook
        /// </summary>
        object Hook { get; }
    }
}
