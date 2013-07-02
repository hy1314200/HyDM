using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Check.Define
{
    /// <summary>
    /// 规则信息实体类
    /// </summary>
    public class TemplateRuleParas
    {
        // 规则名称
        public string strName;

        // 规则别名
        public string strAlias;

        // 实例化的规则编号-已经有实例化的参数,在模板中实例化
        public string strInstID;

        /// <summary>
        /// 规则dll编码
        /// </summary>
        public int nSerialID;

        ///// <summary>
        ///// 方案id
        ///// </summary>
        //public string SchemaID;

        // 规则二进制参数
        public Byte[] pParas;

        // 规则描述
        //public string strDesc;

        // 规则参数二进制块的长度
        public int nParaLength;

        //规则编号，和文档编号挂钩
        public string strGZBM;
    }
}
