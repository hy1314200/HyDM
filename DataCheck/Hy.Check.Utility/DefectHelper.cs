using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.Data;
using Hy.Check.Define;

namespace Hy.Check.Utility
{
    public class DefectHelper
    {
        static DefectHelper()
        {
            try
            {
                DataTable dtDefectLevel = Common.Utility.Data.AdoDbHelper.GetDataTable(SysDbHelper.GetSysDbConnection(), "select ElementID as RuleID,IIF(ErrType='轻缺陷',0,IIF(ErrType='重缺陷',1,2)) as DefectLevel from LR_EvaHMWeight");
                m_DictDefectLevel = new Dictionary<string, enumDefectLevel>();
                for (int i = 0; i < dtDefectLevel.Rows.Count; i++)
                {
                    m_DictDefectLevel.Add(dtDefectLevel.Rows[i][0] as string, (enumDefectLevel)Convert.ToInt32(dtDefectLevel.Rows[i][1]));
                }
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());
            }
        }

        private static Dictionary<string,enumDefectLevel> m_DictDefectLevel;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ruleID">指Rule Instance ID</param>
        /// <returns></returns>
        public static enumDefectLevel GetRuleDefectLevel(string ruleID)
        {
            if (m_DictDefectLevel.ContainsKey(ruleID))
                return m_DictDefectLevel[ruleID];

            return enumDefectLevel.UnKnown;
        }


    }
}
