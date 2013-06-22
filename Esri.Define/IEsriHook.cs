using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.Define
{
    /// <summary>
    /// 为支持ESRI提供的Hook接口
    /// </summary>
    public interface IEsriHook
    {
        /// <summary>
        /// ESRI的IMapControl，或IPageLayoutControl对象
        /// </summary>
        ESRI.ArcGIS.Controls.IHookHelper HookHelper { get; }
    }
}
