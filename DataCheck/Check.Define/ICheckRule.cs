using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Check.Define
{
    /// <summary>
    /// 规则接口
    /// </summary>
    public interface ICheckRule:IEnvironment
    {
        /// <summary>
        /// 实例标识
        /// 指HyDC中配置的模板的标识，由检查驱动从数据库中读取进行设置（为检查时赋值给错误类@see Error并存入结果库）
        /// </summary>
        string InstanceID {  get; set;}

        /// <summary>
        /// 实例名
        /// 指HyDC中配置的模板的名称，由检查驱动从数据库中读取进行设置
        /// </summary>
        string InstanceName { get; set; }

        /// <summary>
        /// 缺陷级别
        /// 由检查驱动从数据库读取进行设置
        /// </summary>
        enumDefectLevel DefectLevel { get; set; }

        /// <summary>
        /// 规则名
        /// 配置端反射时读取并列出
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 规则描述
        /// 配置端反射时读取并列出
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 错误类型
        /// 用于结果保存时选择结果表
        /// </summary>
        enumErrorType ErrorType { get; }

        /// <summary>
        /// 参数设置器
        /// </summary>
        /// <returns></returns>
        IParameterSetter GetParameterSetter();

        /// <summary>
        /// 初始化规则
        /// </summary>
        /// <param name="objParamters"></param>
        void SetParamters(byte[] objParamters);

        /// <summary>
        /// 预处理
        /// </summary>
        /// <returns></returns>
        bool Pretreat();

        /// <summary>
        /// 规则验证
        /// </summary>
        /// <returns></returns>
        bool Verify();

        /// <summary>
        /// 执行规则检查
        /// </summary>
        /// <param name="checkResult"></param>
        /// <returns></returns>
        bool Check(ref List<Error> checkResult);
    }
}
