using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Define
{
    /// <summary>
    /// 需要使用UI（这里单指使用DockPanel,任何Command实现可以对这些Panel进行操作）
    /// </summary>
    public interface IUIHook:IHook
    {
        /// <summary>
        /// 左边的DockPanel
        /// </summary>
        System.Windows.Forms.Control LeftDockPanel { get; }

        /// <summary>
        /// 右边的DockPanel
        /// </summary>
        System.Windows.Forms.Control RightDockPanel { get; }
        
        /// <summary>
        /// 下边的DockPanel
        /// </summary>
        System.Windows.Forms.Control BottomDockPanel { get; }
    }
}
