using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Check.Define;

namespace Check.Engine.Helper
{
    /// <summary>
    /// 规则和规则类信息处理类
    /// </summary>
    public class RuleInfoHelper
    {
//        /// <summary>
//        /// 根据指定方案ID获取所有规则信息
//        /// </summary>
//        /// <param name="schemaID"></param>
//        /// <returns></returns>
//        public static List<RuleInfo> GetRuleInfos(string schemaID)
//        {
//            // // 包括缺陷级别
//            // string strSQL = @"select schemaPara.RuleInstID,schemaTemp.TempletName,schemaPara.RuleAlias , schemaPara.Parameters ,ruleClass.* ,iif(Evaluate.ErrType='轻缺陷',0,iif(Evaluate.ErrType='重缺陷',1,2)) as ErrType
//            //                   from   LR_ModelSchemaPara as SchemaPara,LR_modelRule as ruleClass,LR_ModelSchemaTemp as SchemaTemp,LR_EvaHMWeight as Evaluate
//            //                   where  ruleClass.ID=schemapara.ruleserialid and schemapara.TempInstID=schemaTemp.TempInstID and Evaluate.ElementID=schemaPara.RuleInstID and schemaPara.schemaid='";
            
//            string strSQL = @"select schemaPara.RuleInstID as RuleInfoID,schemaTemp.TempletName as RuleInfoName,schemaTemp.remark as RuleInfoDescription , schemaPara.Parameters as RuleInfoParameters,ruleClass.* 
//                              from   LR_ModelSchemaPara as SchemaPara,LR_modelRule as ruleClass,LR_ModelSchemaTemp as SchemaTemp
//                              where  ruleClass.ID=schemapara.ruleserialid and schemapara.TempInstID=schemaTemp.TempInstID  and schemaPara.schemaid='";
//            strSQL += schemaID + "'";
//            string strSQL = @"select schemaPara.RuleInstID as RuleInfoID,schemaTemp.TempletName as RuleInfoName,schemaTemp.SM as RuleInfoDescription , schemaPara.Parameters as RuleInfoParameters,ruleClass.* 
//                              from   LR_ModelSchemaPara as SchemaPara,LR_modelRule as ruleClass,LR_ModelSchemaTemp as SchemaTemp
//                              where  ruleClass.ID=schemapara.ruleserialid and schemapara.TempInstID=schemaTemp.TempInstID  and schemaPara.schemaid='";
//            strSQL += schemaID + "'";

//            DataTable dtRuleInfo = Common.Utility.Data.AdoDbHelper.GetDataTable(SysDbHelper.GetSysDbConnection(), strSQL);
//            List<RuleInfo> ruleInfoList = new List<RuleInfo>();
//            foreach (DataRow rowRuleInfo in dtRuleInfo.Rows)
//            {
//                RuleInfo ruleClassInfo = new RuleInfo();
//                ruleClassInfo.ID = rowRuleInfo["RuleInfoID"] as string;
//                ruleClassInfo.Name = rowRuleInfo["RuleInfoname"] as string;
//                ruleClassInfo.Paramters=(byte[]) rowRuleInfo["RuleInfoParameters"];
//                ruleClassInfo.Description = rowRuleInfo["RuleInfoDescription"] as string;
                
//                ruleClassInfo.RuleClassInfo = RuleClassFromDataRow(rowRuleInfo);

//                ruleInfoList.Add(ruleClassInfo);
//            }
//                ruleClassInfo.RuleClassInfo = RuleClassFromDataRow(rowRuleInfo);
//            }

//            return ruleInfoList;
//        }

//        /// <summary>
//        /// 获取所有规则类信息
//        /// </summary>
//        /// <returns></returns>
//        public static List<RuleClassInfo> GetAllRuleClassInfo()
//        {
//            string strSQL = "select * from LR_ModelRule";

//            IDbConnection sysConnection= SysDbHelper.GetSysDbConnection();

//            DataTable dtRuleClass = Common.Utility.Data.AdoDbHelper.GetDataTable(sysConnection, strSQL);
//            List<RuleClassInfo> ruleClassInfos = new List<RuleClassInfo>();
//            foreach (DataRow rowClassInfo in dtRuleClass.Rows)
//            {
//                ruleClassInfos.Add(RuleClassFromDataRow(rowClassInfo));
//            }

//            return ruleClassInfos;
//        }

//        /// <summary>
//        /// 从数据记录获取
//        /// </summary>
//        /// <param name="rowClassInfo"></param>
//        /// <returns></returns>
//        public static RuleClassInfo RuleClassFromDataRow(DataRow rowClassInfo)
//        {
//            if (rowClassInfo == null)
//                return null;

//            RuleClassInfo ruleClassInfo = new RuleClassInfo();
//            ruleClassInfo.ID = (int)rowClassInfo["ID"];
//            ruleClassInfo.Name = rowClassInfo["RuleName"] as string;
//            string strName = rowClassInfo["RuleDllName"] as string;
//            string[] strDllAndName = strName.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
//            ruleClassInfo.DllName = strDllAndName[0];
//            if (strDllAndName.Length > 1)
//                ruleClassInfo.ClassName = strDllAndName[1];

//            ruleClassInfo.Description = rowClassInfo["Remark"] as string;

//            return ruleClassInfo;
//        }

    }
}
