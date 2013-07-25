using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hy.Check.Define;

namespace Hy.Check.UI.UC.Sundary
{
    public class ErrorHelper
    {
        public IDbConnection ResultConnection
        {
            private get;
            set;
        }

        /// <summary>
        /// 获取指定错误ID下，指定页的错误结果
        /// @remark 
        /// 1.错误ID可以是集合，用逗号隔开
        /// 2.errCount参数，当第一次调用时使用-1，将返回错误数，此后调用将此错误数传入，可加快效率
        /// </summary>
        /// <param name="errType"></param>
        /// <param name="ruleIDs"></param>
        /// <param name="countPerPage"></param>
        /// <param name="pageIndex"></param>
        /// <param name="errCount"></param>
        /// <returns></returns>
        public DataTable GetErrors(enumErrorType errType, string ruleIDs,int countPerPage,int pageIndex,ref int errCount)
        {
            if (this.ResultConnection == null || this.ResultConnection.State == ConnectionState.Closed)
                return null;

            string resultTableName= GetResultTableNameByType(errType);
            string strFields = GetErrorFields(errType);
            if (errCount < 0)
            {
                DataTable dtCount = Hy.Common.Utility.Data.AdoDbHelper.GetDataTable(this.ResultConnection, string.Format("select count(0) from {0} where RuleInstID in ('{1}')", resultTableName,  ruleIDs.Replace(",", "','")));
                errCount = (int)dtCount.Rows[0][0];
            }
            int resultCount = countPerPage;
            if(countPerPage * (pageIndex + 1) > errCount)
                resultCount=errCount - countPerPage * pageIndex ;

            if (resultCount == 0) resultCount = 1;
            object[] objArgs = { resultCount, countPerPage * (pageIndex + 1), resultTableName, ruleIDs.Replace(",", "','"),strFields };
            string strSQL = string.Format("select top {0} * from (select top {1} {4} from {2} as Result,LR_ResultEntryRule as Entry where Result.RuleInstID in ('{3}') and Result.RuleInstID=Entry.RuleInstID order by ErrNum Asc) Order by ErrNum Desc", objArgs);

            DataTable dtErrors= Hy.Common.Utility.Data.AdoDbHelper.GetDataTable(this.ResultConnection, strSQL);
            dtErrors.TableName = resultTableName;

            //DataColumn colErrorType = new DataColumn("错误级别",typeof(string));
            //colErrorType.Expression = "IIf(DefectLevel=0,'轻缺陷',IIf(DefectLevel=1,'重缺陷','严重缺陷'))";
            //dtErrors.Columns.Add(colErrorType);
            return dtErrors;
        }

        private string GetErrorFields(enumErrorType errType)
        {
            string strFields = "";
            switch (errType)
            {

                case enumErrorType.Topology:
                    strFields = @"
Result.ErrNum,
Entry.GZBM as 规则编码,
Result.YSTC as 图层,
Result.SourceBSM as 标识码11,
Result.SourceOID as OID,
Result.MBTC as 图层2,
Result.TargetBSM as 标识码222,
Result.TargetOID as OID2,
Result.Reason as 错误描述,
IIF(Result.DefectLevel=-1,'未定义',IIf(Result.DefectLevel=0,'轻缺陷',IIf(Result.DefectLevel=1,'重缺陷','严重缺陷'))) as 缺陷级别,
Result.IsException as 是否例外,
Result.Remark as 备注,
Result.DefectLevel as DefectLevel,
Result.JHLX as JHLX,
Entry.ErrorType as ErrorType,
Result.SourceLayerID as SourceLayerID,
Result.TargetLayerID as TargetLayerID,
Result.TPTC as TPTC,
Result.ArcGISRule as ArcGISRule
";
                    break;

                case enumErrorType.FieldIntegrity:
                    strFields = @"
Result.ErrNum,
Entry.GZBM as 规则编码,
Result.AttrTabName as 图层,
Result.ErrorReason as 错误描述,
IIF(Result.DefectLevel=-1,'未定义',IIf(Result.DefectLevel=0,'轻缺陷',IIf(Result.DefectLevel=1,'重缺陷','严重缺陷'))) as 缺陷级别,
Result.IsException as 是否例外,
Result.Remark as 备注,
Result.DefectLevel as DefectLevel,
Entry.ErrorType as ErrorType,
Result.FieldName as FieldName,
Result.FieldType as FieldType
";
                    break;

                case enumErrorType.LayerIntegrity:
                    strFields = @"
Result.ErrNum,
Entry.GZBM as 规则编码,
Result.ErrorLayerName as 图层,
Result.ErrorReason as 错误描述,
IIF(Result.DefectLevel=-1,'未定义',IIf(Result.DefectLevel=0,'轻缺陷',IIf(Result.DefectLevel=1,'重缺陷','严重缺陷'))) as 缺陷级别,
Result.IsException as 是否例外,
Result.Remark as 备注,
Result.DefectLevel as DefectLevel,
Entry.ErrorType as ErrorType
";
                    break;

                default:
                    strFields = @"
Result.ErrNum,
Entry.GZBM as 规则编码,
Result.TargetFeatClass1 as 图层,
Result.BSM as 标识码,
Result.TargetFeatClass2 as 图层2,
Result.BSM2 as 标识码2,
Result.ErrMsg as 错误描述,
IIF(Result.DefectLevel=-1,'未定义',IIf(Result.DefectLevel=0,'轻缺陷',IIf(Result.DefectLevel=1,'重缺陷','严重缺陷'))) as 缺陷级别,
Result.OID as ObjectID,
Result.OID2 as ObjectID2,
Result.IsException as 是否例外,
Result.Remark as 备注,
Result.DefectLevel as DefectLevel,
Entry.ErrorType as ErrorType
";
                    break;
            }

            return strFields;
        }

        public enumDefectLevel GetDefectLevel(string ruleID)
        {
            DataTable dtDefect= Hy.Common.Utility.Data.AdoDbHelper.GetDataTable(this.ResultConnection, string.Format("select IIf(ErrType='轻缺陷',0,IIF(ErrType='重缺陷',1,2)) from LR_EvaHMWeight where ElementID='{0}'", ruleID));
            if (dtDefect == null || dtDefect.Rows.Count == 0)
            {

            }

            return (enumDefectLevel) dtDefect.Rows[0][0];
        }

        public bool CommitExceptionEdit(enumErrorType errorType, DataTable dtError)
        {
            string strTable = GetResultTableNameByType(errorType);
            return Hy.Common.Utility.Data.AdoDbHelper.UpdateTable(strTable, dtError, this.ResultConnection);          
        }

        /// <summary>
        /// 提交意外编辑结果
        /// </summary>
        /// <param name="errType"></param>
        /// <param name="errID"></param>
        /// <param name="isException"></param>
        /// <returns></returns>
        public bool CommitExceptionEdit(enumErrorType errType, string errID, bool isException, string remark)
        {
            string strTable = GetResultTableNameByType(errType);
            object[] objArgs = { strTable, isException, errID, remark==null?remark:remark.Replace("'","''") };
            return Hy.Common.Utility.Data.AdoDbHelper.ExecuteSql(this.ResultConnection, string.Format("Update {0} set IsException={1},Remark='{3}' where ErrNum={2}", objArgs));
        }

        public static string GetResultTableNameByType(enumErrorType errType)
        {
            string strResultTableName = "LR_ResAutoAttr";
            switch (errType)
            {

                case enumErrorType.Topology:
                    strResultTableName = "LR_ResAutoTopo";
                    break;

                case enumErrorType.LayerIntegrity:
                    strResultTableName = "LR_ResIntLayer";
                    break;

                case enumErrorType.FieldIntegrity:
                    strResultTableName = "LR_ResIntField";
                    break;

                default:
                    strResultTableName = "LR_ResAutoAttr";
                    break;

            }
            return strResultTableName;
        }

        /// <summary>
        /// Generates the standard error dt.
        /// </summary>
        /// <returns></returns>
        public static DataTable GenerateStandardErrorDt()
        {
            DataTable result = new DataTable();

            DataColumn dc = new DataColumn();
             dc.Caption = "规则编码";
            dc.ColumnName = "GZBM";
            dc.DataType = typeof(System.String);
            result.Columns.Add(dc);

            dc = new DataColumn();
            dc.Caption = "图层";
            dc.ColumnName = "SourceLayerID";
            dc.DataType = typeof(System.String);
            result.Columns.Add(dc);

            dc = new DataColumn();
            dc.Caption = "标识码";
            dc.ColumnName = "SourceBSM";
            dc.DataType = typeof(System.String);
            result.Columns.Add(dc);

            dc = new DataColumn();
            dc.Caption = "错误描述";
            dc.ColumnName = "Reason";
            dc.DataType = typeof(System.String);
            result.Columns.Add(dc);

            dc = new DataColumn();
            dc.Caption = "缺陷级别";
            dc.ColumnName = "DefectLevel";
            dc.DataType = typeof(System.String);
            result.Columns.Add(dc);

            dc = new DataColumn();
            dc.Caption = "是否例外";
            dc.ColumnName = "IsException";
            dc.DataType = typeof(System.String);
            result.Columns.Add(dc);

            dc = new DataColumn();
            dc.Caption = "备注";
            dc.ColumnName = "Remark";
            dc.DataType = typeof(System.String);
            result.Columns.Add(dc);
            return result;
        }
    }
}
