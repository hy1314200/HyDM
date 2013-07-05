using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Check.Define
{
    /// <summary>
    /// 规则（实现）类信息（对应HyDC模型中的规则）
    /// </summary>
    public class RuleDllInfo
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 规则名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// dll文件名
        /// </summary>
        public string DllName { get; set; }

        /// <summary>
        /// 规则类全名
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }


        public static RuleDllInfo FromDataRow(System.Data.DataRow rowRuleClassInfo)
        {
            RuleDllInfo ruleClassInfo = new RuleDllInfo();
            ruleClassInfo.ID = int.Parse(rowRuleClassInfo["ID"].ToString());
            ruleClassInfo.Name = rowRuleClassInfo["RuleName"].ToString();
            ruleClassInfo.DllName = rowRuleClassInfo["DllFile"] as string;
            ruleClassInfo.ClassName = rowRuleClassInfo["ClassName"] as string;
            ruleClassInfo.Description = rowRuleClassInfo["Remark"] as string;

            return ruleClassInfo;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}