using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Define
{
    /// <summary>
    /// 工具接口
    /// 当前接口代表了必须与Hook界面交互，即实现当前接口即具备了“IsTool”属性
    /// </summary>
    public  interface ITool:IExclusive
    {
    }
}
