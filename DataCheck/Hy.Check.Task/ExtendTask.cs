using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Check.Task
{
    /// <summary>
    /// 检查方式
    /// </summary>
    public enum enumCheckMode
    {
        /// <summary>
        /// 仅创建
        /// </summary>
        CreateOnly = 0,
        /// <summary>
        /// 抽检
        /// </summary>
        CheckPartly = 1,
        /// <summary>
        /// 全检
        /// </summary>
        CheckAll = 2
    }

    /// <summary>
    /// 扩展任务
    /// </summary>
    public class ExtendTask:Task
    {
        /// <summary>
        /// 是否立即执行（检查）
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public enumCheckMode CheckMode { get; set; }

        [System.Xml.Serialization.XmlIgnore()]
        public List<Hy.Check.Define.SchemaRuleEx> RuleInfos { get; set; }


        public void ReadyForCheck()
        {
            bool checkAll = (this.CheckMode == enumCheckMode.CheckAll);
            base.ReadyForCheck(checkAll);
        }
    }
}
