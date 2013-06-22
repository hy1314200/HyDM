using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerraExplorerX;

namespace Skyline.Define
{
    /// <summary>
    /// Skyline的Hook定义
    /// </summary>
    public interface ISkylineHook
    {
        /// <summary>
        /// 窗口，不同于ESRI的（如MapControl），窗口对象无法通过Hook直接获取到
        /// </summary>
        System.Windows.Forms.Control Window { get; }

        /// <summary>
        /// TE对象
        /// </summary>
        TerraExplorerClass TerraExplorer { get; }

        /// <summary>
        /// SGWorld对象
        /// </summary>
        ISGWorld61 SGWorld { get; }
    }
}
