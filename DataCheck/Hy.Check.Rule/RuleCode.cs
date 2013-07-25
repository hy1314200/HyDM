using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Hy.Check.Define;

using Hy.Check.Rule.Helper;
using Hy.Check.Utility;

namespace Hy.Check.Rule
{
    /// <summary>
    /// 值符合性检查
    /// </summary>
    public class RuleCode : BaseRule
    {

        public override string Name
        {
            get { return "值符合性检查"; }
        }

        public override IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {
            BinaryReader pParameter = new BinaryReader(new MemoryStream(objParamters));
            if (m_psPara == null)
            {
                m_psPara = new RuleExpression.LRCodePara();
            }

            pParameter.BaseStream.Position = 0;

            int nCount1 = pParameter.ReadInt32();
            m_psPara.nVersionID = pParameter.ReadInt32();

            //解析字符串
            int nSize = sizeof(int) * 2;
            Byte[] bb = new byte[nCount1 - nSize];
            pParameter.Read(bb, 0, nCount1 - nSize);
            string para_str = Encoding.Default.GetString(bb);

            para_str.Trim();


            string[] strResult = para_str.Split('|');

            int i = 0;
            m_psPara.strName = strResult[i++];
            m_psPara.strAlias = strResult[i++];
            m_psPara.strFtName = strResult[i++];
            m_psPara.strRemark = strResult[i++];
            m_psPara.strCodeField = strResult[i++];
            m_psPara.strCodeType = strResult[i++];
            m_psPara.strNameField = strResult[i++];
            m_psPara.strCodeLibTable = strResult[i++];
            m_psPara.strCodeDataTable = strResult[i++];
            m_psPara.strCodeNorm = strResult[i++];
            m_strStdName = strResult[i++];

        }

        public override bool Verify()
        {
            // 获取标准ID
            int nStdID = SysDbHelper.GetStandardID(this.m_strStdName);
            lyr = LayerReader.GetLayerByAliasName(m_psPara.strFtName, nStdID);
            if (lyr == null)
            {
                SendMessage(enumMessageType.VerifyError, string.Format("标准中不存在图层“{0}”", m_psPara.strFtName));
                return false;
            }

            // 获取实际字值名                
            Helper.StandardHelper helper = new Hy.Check.Rule.Helper.StandardHelper(this.m_QueryConnection);
            strFieldCode = FieldReader.GetNameByAliasName(m_psPara.strCodeField, lyr.ID);
            if (string.IsNullOrEmpty(strFieldCode))
            {
                SendMessage(enumMessageType.VerifyError, string.Format("标准图层中不存在字段“{0}”", m_psPara.strCodeField));
                return false;
            }

            strFieldCodeType = helper.GetLayerFieldType(strFieldCode, lyr.AttributeTableName);
            if (string.IsNullOrEmpty(strFieldCodeType))
            {
                SendMessage(enumMessageType.VerifyError, string.Format("数据图层中不存在字段“{0}”", m_psPara.strCodeField));
                return false;
            }

            if (m_psPara.strNameField != "")
            {
                strFieldName = FieldReader.GetNameByAliasName(m_psPara.strNameField, lyr.ID);
            }
            else
            {
                SendMessage(enumMessageType.VerifyError,"没有配置字段");
                return false;
            }

            return true;
        }

        private StandardLayer lyr;
        private string strFieldName = "";
        private string strFieldCode = "";
        private string strFieldCodeType;
        public override bool Check(ref List<Error> checkResult)
        {
            try
            {
                string strFeatAlias = m_psPara.strFtName;
                string strFieldCodeAlias = m_psPara.strCodeField;
                string strLibName = m_psPara.strCodeDataTable;
                string strSQL;
                string strFeat ;
                DataTable dt;
                string var = null;

                long nVersion = m_psPara.nVersionID;

                var = lyr.AttributeTableName;
                string pLayerName = var;
                strFeat = pLayerName;


                // 错误列表对象
                List<Error> pResult = new List<Error>();
                checkResult = pResult;

                // 1、进行第一次搜索，所以不满足条件的记录加入错误结果集
                if (strFieldName == "")
                {
                    strSQL = "select distinct " + strFieldCode + " from " + strFeat + "";
                }
                else
                {
                    strSQL = "select distinct " + strFieldCode + "," + strFieldName + " from " + strFeat + "";
                }

                dt = Hy.Common.Utility.Data.AdoDbHelper.GetDataTable(this.m_QueryConnection, strSQL);

                //strfieldcode 编码字段是否为字符类型；
                bool bIsStrType = false;

                if (dt != null)
                {
                    // 遍历唯一值
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            // 获取唯一值
                            string pCode = "";


                            DataRow dr1 = dr;

                            var = dr1[0].ToString();
                            pCode = var;

                            if (string.IsNullOrEmpty(var))
                            {
                                if (strFieldName == "")
                                {
                                    strSQL = "select OBJECTID,BSM from " + strFeat + " where (" + strFieldCode +
                                             " is Null or " + strFieldCode + "='')";
                                }
                                else
                                {
                                    var = dr1[1].ToString();

                                    if (var != "") //如果var的类型为string型
                                    {
                                        string strName = var.ToString();

                                        strSQL = "select OBJECTID,BSM from " + strFeat + " where (" + strFieldCode +
                                                 " is null or " + strFieldCode + " ='' ) and " + strFieldName + " = '" +
                                                 strName + "'";
                                    }
                                    else if (var == null || var == "")
                                    {
                                        strSQL = "select OBJECTID,BSM from " + strFeat + " where (" + strFieldCode +
                                                 " is Null or " + strFieldCode + " ='' ) and (" +
                                                 strFieldName + " is null or " + strFieldName + " ='' )";
                                    }
                                }
                                DataTable pDt = Hy.Common.Utility.Data.AdoDbHelper.GetDataTable(this.m_QueryConnection, strSQL);

                                if (pDt != null)
                                {
                                    // 遍历记录集，返回错误
                                    foreach (DataRow datarow in pDt.Rows)
                                    {
                                        if (datarow != null)
                                        {
                                            // 获取OID
                                            //object varOID = datarow[0];

                                            // 生成错误结构
                                            Error pInfo = new Error();
                                            pInfo.DefectLevel = this.m_DefectLevel;
                                            pInfo.RuleID = this.InstanceID;

                                            pInfo.OID = Convert.ToInt32(datarow["OBJECTID"]);
                                            pInfo.BSM = datarow["BSM"].ToString();
                                            pInfo.LayerName = strFeatAlias;

                                            //if (strFieldName == "")
                                            //{
                                            //    //pInfo.strErrInfo = "该图层的字段" + strFieldCodeAlias + "(" + strFieldCode +
                                            //    //                   ")值为空！";
                                            //    //pInfo.strErrInfo = string.Format(Helper.ErrMsgFormat.ERR_4201_1, strFeatAlias, pInfo.BSM, strFieldCodeAlias);
                                                pInfo.Description = string.Format("'{0}'层标识码为'{1}'的'{2}'字段的值不正确，不能为空", strFeatAlias, pInfo.BSM, strFieldCodeAlias);
                                            //}

                                            pResult.Add(pInfo);
                                        }
                                    }

                                    pDt.Dispose();
                                }
                            }
                            else
                            {
                                bIsStrType = true;

                                pCode = var;
                            }

                            string strName1 = "";
                            if (strFieldName == "")
                            {
                                strSQL = "select 编码 from " + strLibName + " where 规范号 = " + nVersion + " and 编码 = '" +
                                         pCode +
                                         "'";
                            }
                            else
                            {
                                DataRow dr3 = dr;

                                var = dr3[1].ToString();

                                string pName = var;
                                strName1 = pName;
                                strSQL = "select 编码,名称 from " + strLibName + " where 规范号 = " + nVersion + " and 编码 = '" +
                                         pCode +
                                         "' and 名称='" + strName1 + "'";
                            }

                            // 在字典中查找这个唯一值, 如果找不到，那么字段属性为这个值的所有记录都必须找出来，返回其OID
                            DataTable ipRSCode = Hy.Common.Utility.Data.AdoDbHelper.GetDataTable(SysDbHelper.GetSysDbConnection(), strSQL);


                            if (ipRSCode == null || ipRSCode.Rows.Count == 0)
                            {
                                // 搜索字段属性为这个唯一值的所有记录
                                if (strFieldName == "")
                                {
                                    if (bIsStrType)
                                    {
                                        if (strFieldCodeType == "System.Int32" || strFieldCodeType == "System.Int64" ||
                                            strFieldCodeType == "System.Double")
                                            strSQL = "select OBJECTID,BSM from " + strFeat + " where " + strFieldCode +
                                                     " = " +
                                                     pCode +
                                                     "";
                                        else if (strFieldCodeType == "System.String" || strFieldCodeType == "System.Char")
                                            strSQL = "select OBJECTID,BSM from " + strFeat + " where " +
                                                     strFieldCode + " = '" +
                                                     pCode +
                                                     "'";
                                        else
                                            strSQL = "select OBJECTID,BSM from " + strFeat + " where " +
                                                     strFieldCode + " = '" +
                                                     pCode +
                                                     "'";
                                    }
                                    else
                                    {
                                        if (pCode.Length > 0)
                                            strSQL = "select OBJECTID,BSM from " + strFeat + " where " + strFieldCode +
                                                     "= " +
                                                     pCode +
                                                     "";
                                        else
                                            strSQL = "select OBJECTID,BSM from " + strFeat + " where " + strFieldCode +
                                                     " is null ";
                                    }
                                }
                                else
                                {
                                    if (bIsStrType)
                                        strSQL = "select OBJECTID,BSM from " + strFeat + " where " + strFieldCode +
                                                 "= '" +
                                                 pCode +
                                                 "' and " +
                                                 strFieldName + " = '" + strName1 + "'";
                                    else
                                    {
                                        if (pCode.Length > 0)
                                            strSQL = "select OBJECTID,BSM from '" + strFeat + "' where '" + strFieldCode +
                                                     "'= " +
                                                     pCode +
                                                     "and '" +
                                                     strFieldName + "'='" + strName1 + "'";
                                        else
                                            strSQL = "select OBJECTID,BSM from " + strFeat + " where " + strFieldCode +
                                                     " is null and " +
                                                     strFieldName + " = '" + strName1 + "'";
                                    }
                                }

                                DataTable pDt = Hy.Common.Utility.Data.AdoDbHelper.GetDataTable(this.m_QueryConnection, strSQL);

                                // 遍历记录集，返回错误
                                foreach (DataRow pDr in pDt.Rows)
                                {
                                    if (pDr != null)
                                    {
                                        if (pCode == "")
                                        {
                                            continue;
                                        }

                                        // 生成错误结构
                                        Error pInfo = new Error();
                                        pInfo.DefectLevel = this.m_DefectLevel;
                                        pInfo.RuleID = this.InstanceID;
                                        pInfo.LayerName = strFeatAlias;

                                        // 获取OID
                                        pInfo.OID = Convert.ToInt32(pDr["OBJECTID"]);

                                        pInfo.BSM = pDr["BSM"].ToString();


                                        if (strFieldName == "")
                                        {
                                            //pInfo.strErrInfo = "该图层的字段" + strFieldCodeAlias + "(" + strFieldCode + ")编码" +
                                            //                   pCode + "在标准库中不存在！";
                                            //pInfo.strErrInfo = string.Format(Helper.ErrMsgFormat.ERR_RuleCode_1_1, strFeatAlias,pInfo.BSM, strFieldCodeAlias, strFieldCode, pCode);
                                            pInfo.Description = string.Format("'{0}'层标识码为'{1}'的'{2}({3})'字段的值'{4}'在标准编码中不存在", strFeatAlias, pInfo.BSM, strFieldCodeAlias, strFieldCode, pCode);
                                        }
                                        else
                                        {
                                            pInfo.Description = pCode;
                                            pInfo.Description += "|";
                                            pInfo.Description += strName1;
                                        }

                                        pResult.Add(pInfo);
                                    }
                                }
                                if (pDt != null)
                                {
                                    pDt.Dispose();
                                }
                            }
                        }
                    }
                    dt.Dispose();
                } // 遍历唯一值结束


                // 如果只进行编码的检查，那不需要进行第二步了
                if (strFieldName == "")
                {
                    return true;
                }

                int nCount = pResult.Count;

                // 2、进行第二次搜索，将代码存在的设置成不匹配，代码不存在的设置成代码不存在
                for (int i = 0; i < nCount; i++)
                {
                    Error pInfo = pResult[i];


                    string[] strArray = pInfo.Description.Split('|');

                    string strCode = "";
                    string strName = "";
                    if (strArray.Length == 2)
                    {
                        strCode = strArray[0];
                        strName = strArray[1];
                    }

                    strSQL = "select 编码 from " + strLibName + " where 规范号 = " + nVersion + " and 编码 = '" + strCode + "'";

                    // 在字典中查找这编码，找到说明错误类型是代码和名称不匹配，反之是代码不存在
                    DataTable ipRSCode = Hy.Common.Utility.Data.AdoDbHelper.GetDataTable(SysDbHelper.GetSysDbConnection(), strSQL);

                    if (ipRSCode != null && ipRSCode.Rows.Count != 0)
                    {
                        //pInfo.strErrInfo = strFieldCodeAlias + "'" + strCode + "'和" + m_psPara.strNameField + "'" + strAliasName +
                        //                   "'不匹配！";
                        //pInfo.strErrInfo = string.Format(Helper.ErrMsgFormat.ERR_RuleCode_2, strFeatAlias, pInfo.BSM, strFieldCodeAlias, strCode, m_psPara.strNameField, strAliasName);
                        pInfo.Description = string.Format("'{0}'层标识码为'{1}'的'{2}'的值'{3}'与'{4}'的值'{5}'不匹配", strFeatAlias, pInfo.BSM, strFieldCodeAlias, strCode, m_psPara.strNameField, strName);
                    }
                    else
                    {
                        //pInfo.strErrInfo = strFieldCodeAlias + "'" + strCode + "'在标准库中不存在！";
                        //pInfo.strErrInfo = string.Format(Helper.ErrMsgFormat.ERR_RuleCode_1_2, strFeatAlias,pInfo.BSM ,strFieldCodeAlias, strCode);
                        pInfo.Description = string.Format("'{0}'层标识码为'{1}'的'{2}({3})'字段的值'{4}'在标准编码中不存在", strFeatAlias, pInfo.BSM, strFieldCodeAlias, strFieldCode, strCode);
                    }
                    if (pInfo.Description == null)
                    {
                        pInfo.Description = "";
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

        //参数结构体
        public RuleExpression.LRCodePara m_psPara;

        private string m_strStdName;
    }
}