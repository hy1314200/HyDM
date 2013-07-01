using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Check.Define;

using Check.Rule.Helper;
using Check.Utility;

namespace Check.Rule
{
    public class RuleFtCode : BaseRule
    {

        //参数结构体
        public RuleExpression.LRFtCodePara m_psPara;

        public override string Name
        {
            get { return "要素类编码检查"; }
        }

        public override IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {
            BinaryReader pParameter = new BinaryReader(new MemoryStream(objParamters));

            m_psPara = new RuleExpression.LRFtCodePara();
            pParameter.BaseStream.Position = 0;
            int nCount1 = pParameter.ReadInt32();

            //解析字符串
            Byte[] bb = new byte[nCount1];
            pParameter.Read(bb, 0, nCount1);
            string para_str = Encoding.Default.GetString(bb);

            para_str.Trim();

            string[] strResult = para_str.Split('|');

            int i = 0;
            m_psPara.strName = strResult[i++];
            m_psPara.strAlias = strResult[i++];
            m_psPara.strRemark = strResult[i++];
            m_psPara.strCodeField = strResult[i++];
            m_psPara.strTargetLayer = strResult[i++];

        }

        public override bool Verify()
        {
            standarID = SysDbHelper.GetStandardIDBySchemaID(this.m_SchemaID);

            StandardLayer lyr = LayerReader.GetLayerByAliasName(m_psPara.strTargetLayer, standarID);
            if (lyr == null)
            {
                SendMessage(enumMessageType.VerifyError, "当前方案所在的标准中找不到名为“" + m_psPara.strTargetLayer + "”的图层");
                return false;
            }

            strLayerName = lyr.Name;
            strCodeField = FieldReader.GetNameByAliasName(m_psPara.strCodeField, lyr.ID);
            if (string.IsNullOrEmpty(strCodeField))
            {
                SendMessage(enumMessageType.VerifyError, string.Format("当前方案所在的标准中找不到名为“{0}”的图层", m_psPara.strCodeField));
                return false;
            }

            return true;
        }
        private string strLayerName;
        private string strCodeField;
        private int standarID;
        public override bool Check(ref List<Error> checkResult)
        {
            try
            {
                List<Error> m_pRuleResult = new List<Error>();

            List<string> aryFtCode = new List<string>();
            Helper.StandardHelper StdHelp = new Check.Rule.Helper.StandardHelper(SysDbHelper.GetSysDbConnection());
            StdHelp.GetLayerCodes(ref aryFtCode, m_psPara.strTargetLayer, standarID);


            if (aryFtCode == null) //如果编码类型为空
            {
                string strSql = "select OBJECTID,BSM from " + strLayerName;

                DataTable ipRecordset = new DataTable();
                ipRecordset = Common.Utility.Data.AdoDbHelper.GetDataTable(this.m_QueryConnection, strSql);
                if (ipRecordset == null)
                {
                    return false;
                }

                // 从DataTable中获取名称
                foreach (DataRow dr in ipRecordset.Rows) // 遍历结果集
                {
                    if (dr != null)
                    {
                        int nOID = Convert.ToInt32(dr["ObjectID"]);

                        // 添家结果记录
                        Error pResInfo = new Error();
                        pResInfo.DefectLevel = this.m_DefectLevel;
                        pResInfo.RuleID = this.InstanceID;

                        pResInfo.OID = nOID;
                        pResInfo.BSM = dr["BSM"].ToString();
                        pResInfo.LayerName = m_psPara.strTargetLayer;                            // 目标图层

                        // 错误信息
                        string strMsg;
                        strMsg = string.Format("'{0}'层标识码为'{1}'的'{2}'字段对应的要素类型代码为空", pResInfo.LayerName, pResInfo.BSM, strCodeField);
                        if (m_psPara.strRemark != null && m_psPara.strRemark.Trim() != "")
                        {
                            pResInfo.Description = m_psPara.strRemark;
                        }
                        else
                        {
                            pResInfo.Description = strMsg;
                        }
                        m_pRuleResult.Add(pResInfo);

                        break;
                    }
                }

                checkResult = m_pRuleResult;

                // 关闭记录集
                ipRecordset.Dispose();
            }
            else
            {
                try
                {
                    string strSql;
                    string strFtCode = "";
                    for (int i = 0; i < aryFtCode.Count; i++)
                    {
                        string strTmp;
                        strTmp = aryFtCode[i];
                        strFtCode += strTmp;
                    }

                    strSql = "select OBJECTID,BSM,YSDM from " + strLayerName + " where (" + strCodeField + " not in ('" +
                             strFtCode.Substring(0, strFtCode.Length) + "')) or (" + strCodeField + " is null )";

                    DataTable ipRecordset = new DataTable();
                    ipRecordset = Common.Utility.Data.AdoDbHelper.GetDataTable(this.m_QueryConnection, strSql);
                    if (ipRecordset == null)
                    {
                        return false;
                    }

                    // 从DataTable中获取名称
                    foreach (DataRow dr in ipRecordset.Rows) // 遍历结果集
                    {
                        if (dr != null)
                        {

                            int nOID = Convert.ToInt32(dr["ObjectID"]);

                            // 添家结果记录
                            Error pResInfo = new Error();
                            pResInfo.DefectLevel = this.m_DefectLevel;
                            pResInfo.RuleID = this.InstanceID;

                            pResInfo.OID = nOID;
                            pResInfo.BSM = dr["BSM"].ToString();
                            pResInfo.LayerName = m_psPara.strTargetLayer;                            // 目标图层

                            // 错误信息
                            string strMsg;
                            strMsg = string.Format("'{0}'层标识码为'{1}'的'{2}({3})'字段的值'{4}'不正确。应为：{5}", pResInfo.LayerName, pResInfo.BSM, m_psPara.strCodeField, strCodeField, dr["YSDM"], strFtCode);
                            if (m_psPara.strRemark != null && !string.IsNullOrEmpty(m_psPara.strRemark.Trim()))
                            {
                                pResInfo.Description = m_psPara.strRemark;
                            }
                            else
                            {
                                pResInfo.Description = strMsg;
                            }
                            m_pRuleResult.Add(pResInfo);

                        }
                    }

                    checkResult = m_pRuleResult;

                    // 关闭记录集
                    ipRecordset.Dispose();
                }
                catch (Exception ex)
                {
                    SendMessage(enumMessageType.Exception, ex.ToString());
                    return false;
                }
            }
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                return false;
            }
            return true;
        }



    }
}