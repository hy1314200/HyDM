using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Check.Define
{
    /// <summary>
    /// 参数设置器
    /// @remark 本接口被设计为应继承于Control，以便于在HyDC以配置工具中统一调用
    /// </summary>
    public interface IParameterSetter
    {
        /// <summary>
        /// 初始化（反显）
        /// </summary>
        /// <param name="objParameters"></param>
        void Init(Standard standard,StandardLayer targetLayer, byte[] objParameters);

        /// <summary>
        /// 验证输入（参数设置）
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        bool Validate(ref string errMsg);

        /// <summary>
        /// 获取设置好的参数
        /// </summary>
        /// <returns></returns>
        byte[] GetParamters();
    }
}
