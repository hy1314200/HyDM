using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Check.Define
{

    /// <summary>
    /// 规则模板类型
    /// </summary>
    public enum RuleTempletType : int
    {
        /// <summary>
        /// And Or模板类型
        /// </summary>
        lrTempletAndOr = 0,

        /// <summary>
        /// 普通单层类型
        /// </summary>
        lrTempletOneFc = 1,

        /// <summary>
        /// 完整性模板-多层模板 
        /// </summary>
        lrTempletMultiFc = 2,
    } ;

    /// <summary>
    /// 方案中的规则实体类
    /// </summary>
    public class SchemaRule
    {
        public string SchemaId;

        public string TemplateName;

        public string ChkTypeName;

        public string TempInstID;
        // 模板的类型
        public RuleTempletType RuleTmpletType;

        // 针对拓扑模板而言,如果是Catalog形式的,则为FALSE;模板指针对一个层进行检查,则为TRUE
        public bool bIsCatalogTopo;

        // 待检查图层(即目标图层)的名称, 注: 一个模板只能检查一个图层
        public string FeaClassName;

        // 逻辑AndOr字符串
        public string strLogicalDesc;

        //// 创建者
        //public string strPerson;

        //// 创建日期
        //public DateTime CreateTime;

        // 描述信息
        public string strRemark;

        // 规则数组
        public List<TemplateRuleParas> arrayRuleParas;

        /// <summary>
        /// 规则dll信息
        /// </summary>
        public RuleDllInfo ruleDllInfo;
    }

    /// <summary>
    /// 方案中的规则实体扩展类
    /// </summary>
    public class SchemaRuleEx : SchemaRule
    {
        /// <summary>
        /// 
        /// </summary>
        public int FirstClassificationBM;

        /// <summary>
        /// 
        /// </summary>
        public int SecondeClassificationBM;

        /// <summary>
        /// 
        /// </summary>
        public int ThridClassificationBM;

        /// <summary>
        /// 缺陷分级
        /// </summary>
        public string Weights;
    }
}
