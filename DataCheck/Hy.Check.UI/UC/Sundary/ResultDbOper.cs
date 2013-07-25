using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hy.Common.Utility.Data;
using System.Data.Common;
using Hy.Check.Utility;

namespace Hy.Check.UI.UC.Sundary
{
    public class ResultDbOper
    {
        private IDbConnection m_ResultDbConn = null;

        public ResultDbOper(IDbConnection resultDb)
        {
            m_ResultDbConn = resultDb;
        }
        public DataTable GetAllResults()
        {
            DataTable res = new DataTable();
            try
            {
                string strSql = string.Format("select RuleErrId,CheckType ,RuleInstID,RuleExeState,ErrorCount,TargetFeatClass1,GZBM,ErrorType from  {0}", COMMONCONST.RESULT_TB_RESULT_ENTRY_RULE);

                return AdoDbHelper.GetDataTable(m_ResultDbConn, strSql);
            }
            catch
            {
                return res;
            }
        }

        public DataTable GetLayersResults()
        {
             DataTable res = new DataTable();
            try
            {
                string strSql = string.Format("SELECT checkType,targetfeatclass1,sum(errorcount)  as ErrCount,max(RuleErrID) as ruleId from {0} group by targetfeatclass1,checktype order by max(RuleErrID)", COMMONCONST.RESULT_TB_RESULT_ENTRY_RULE);

                return AdoDbHelper.GetDataTable(m_ResultDbConn, strSql);
            }
            catch
            {
                return res;
            }
            
        }

        public int GetResultsCount()
        {
            int count = 0;
             DbDataReader reader =null;
            try
            {
                string strSql = string.Format("select sum(errorcount) as cout from  {0}", COMMONCONST.RESULT_TB_RESULT_ENTRY_RULE);

                reader = AdoDbHelper.GetQueryReader(m_ResultDbConn, strSql) as DbDataReader;
                if (reader.HasRows)
                {
                    reader.Read();
                    count = int.Parse(reader[0].ToString());
                }
                return count;
            }
            catch
            {
                return count;
            }
            finally
            {
                reader.Close();
                reader.Dispose();
            }
        }
    }
}
