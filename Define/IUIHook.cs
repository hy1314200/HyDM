using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Define
{
    /// <summary>
    /// 需要使用UI（这里单指使用DockPanel,任何Command实现可以对这些Panel进行操作）
    /// </summary>
    public interface IUIHook
    {
        /// <summary>
        /// 主窗体
        /// </summary>
        Form MainForm { get; }

        /// <summary>
        /// 添加（Dock）控件
        /// </summary>
        /// <param name="ctrlTarget"></param>
        /// <param name="dockPosition"></param>
        /// <returns></returns>
        Control AddControl(Control ctrlTarget, enumDockPosition dockPosition);

        /// <summary>
        /// 添加Hooker
        /// </summary>
        /// <param name="hooker"></param>
        Control AddHooker(IHooker hooker,enumDockPosition dockPosition);

        /// <summary>
        /// 激活Hook控件
        /// </summary>
        void ActiveHookControl(Guid hookerID);

        /// <summary>
        /// 关闭Hook控件
        /// </summary>
        /// <param name="hookerID"></param>
        void CloseHookControl(Guid hookerID);

    }

    /// <summary>
    /// 停靠位置
    /// </summary>
    public enum enumDockPosition
    {
        /// <summary>
        /// 左
        /// </summary>
        Left = 0,
        /// <summary>
        /// 右
        /// </summary>
        Right = 1,
        /// <summary>
        /// 下
        /// </summary>
        Bottom = 2,
        /// <summary>
        /// 上
        /// </summary>
        Top=3,
        /// <summary>
        /// 中心
        /// </summary>
        Center=4
    }
}
