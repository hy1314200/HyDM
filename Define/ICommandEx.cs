using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Define
{
    /// <summary>
    /// 针对按钮以外项提供接口
    /// </summary>
    public interface ICommandEx:ICommand
    {
        /// <summary>
        /// 自定义非按钮类型的控件（就三维平台框架而言（2013.2.27创建时），必须可在RibbonControl中添加的控件）
        /// </summary>
        object ExControl { get; }
    }
}
