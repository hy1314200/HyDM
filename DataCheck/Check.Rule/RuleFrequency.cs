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
    public class RuleFrequency : BaseRule
    {

        public RuleFrequency()
        {
            m_structFrePara.nMaxTime = 0;
            m_structFrePara.nMinTime = 0;
            m_structFrePara.nType = -1;
            m_structFrePara.strAlias = "";
            m_structFrePara.strFtName = "";
            m_structFrePara.strName = "属性频度（唯一性）质检规则";
            m_structFrePara.strScript = "";
        }

        public override string Name
        {
            get { return "属性频度（唯一性）质检规则"; }
        }

        public override IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {
            BinaryReader pParameter = new BinaryReader(new MemoryStream(objParamters));
            pParameter.BaseStream.Position = 0;

            int nCount1 = pParameter.ReadInt32();

            //解析字符串
            //int nSize = sizeof(int) * 4;
            Byte[] bb = new byte[nCount1];
            pParameter.Read(bb, 0, nCount1);

            m_structFrePara.nType = pParameter.ReadInt32();
            m_structFrePara.nMinTime = pParameter.ReadInt32();
            m_structFrePara.nMaxTime = pParameter.ReadInt32();

            string para_str = Encoding.Default.GetString(bb);

            para_str.Trim();

            string[] strResult = para_str.Split('|');

            int i = 0;
            m_structFrePara.strAlias = strResult[i++];
            m_structFrePara.strScript = strResult[i++];
            m_structFrePara.strFtName = strResult[i++];

            m_structFrePara.arrayFields = new List<string>();

            for (int j = i; j < strResult.Length; j++)
            {
                m_structFrePara.arrayFields.Add(strResult[j]);
            }

        }

        public override bool Verify()
        {
            int standardID = SysDbHelper.GetStandardIDBySchemaID(this.m_SchemaID);
            lyr = LayerReader.GetLayerByAliasName(m_structFrePara.strFtName, standardID);
            if (lyr == null)
            {
                SendMessage(enumMessageType.VerifyError, "当前方案所在的标准中找不到名为“" + m_structFrePara.strFtName + "”的图层");
                return false;
            }

            return true;
        }
        private StandardLayer lyr = null;
        public override bool Check(ref List<Error> checkResult)
        {
            try
            {
                //根据别名取图层名
                string layerName = lyr.Name;
                List<Error> m_pResAttr = new List<Error>();

                if (m_structFrePara.strAlias.Equals("频度_BSM全局唯一性检查"))
                {
                    // 2012-07-03 暂时不实现
                    //long longCount = 0;
                    //CCommonCheck.g_pBsmResAttr.GetResultCount(ref longCount);
                    //if (longCount == 0)
                    //{
                    //    CCommonCheck.GetBsmErrorForXml(m_TaskPath);
                    //}
                    //CCommonCheck.g_pBsmResAttr.SetFcAlias(ref Xstand);
                    //ppResult = CCommonCheck.g_pBsmResAttr as ICheckResult;
                }
                else
                {
                    //通过ADO来检测，并将结果放入m_arrResult
                    if (m_QueryConnection == null) return false;
                    string strField = m_structFrePara.arrayFields[0];

                    // 字段别名转换为真实名称
                    for (int j = 0; j < m_structFrePara.arrayFields.Count; j++)
                    {
                        strField = FieldReader.GetNameByAliasName(m_structFrePara.arrayFields[j], lyr.ID);
                        m_structFrePara.arrayFields[j] = strField;
                    }

                    string strSql, strGroup = "";
                    string strNullWhere = "";
                    for (int i = 0; i < m_structFrePara.arrayFields.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(m_structFrePara.arrayFields[i]))
                        {
                            strGroup += m_structFrePara.arrayFields[i] + ",";
                            strNullWhere += "((" + m_structFrePara.arrayFields[i] + " is not null) or " + m_structFrePara.arrayFields[i] + " <>'') and ";

                        }
                    }

                    if (strGroup == "")
                    {
                        return false;
                    }
                    strGroup = strGroup.Substring(0, strGroup.Length - 1);

                    strNullWhere = strNullWhere.Substring(0, strNullWhere.Length - 4);

                    strSql = "Select TOTAL," + strGroup + " from (Select count(*) as TOTAL," + strGroup + " From " + layerName + " GROUP BY " + strGroup + ") where ( TOTAL>=2 and " + strNullWhere + ")";

                    //打开记录集，并分组	         
                    DataTable ipRecordset = new DataTable();
                    ipRecordset = Common.Utility.Data.AdoDbHelper.GetDataTable(this.m_QueryConnection, strSql);
                    if (ipRecordset == null)
                    {
                        return false;
                    }

                    int index = 0;
                    List<string> listWhereClause = new List<string>();

                    foreach (DataRow dr in ipRecordset.Rows) //遍历结果集
                    {

                        string strFieldsValue = "";

                        for (int i = 0; i < m_structFrePara.arrayFields.Count; i++)
                        {
                            string strValue = "";
                            if (m_structFrePara.arrayFields[i] != "")
                            {
                                object varValue = dr[m_structFrePara.arrayFields[i]];

                                Type t = varValue.GetType();
                                TypeCode typeCode = Type.GetTypeCode(t);

                                switch (typeCode)
                                {
                                    case TypeCode.Int32:
                                    case TypeCode.Int64:
                                    case TypeCode.Double:
                                        {
                                            if (varValue == null || Convert.ToString(varValue) == "")
                                            {
                                                strValue = "(" + m_structFrePara.arrayFields[i] + " is null) and";

                                            }
                                            else
                                            {
                                                strValue = "(" + m_structFrePara.arrayFields[i] + " = " + varValue.ToString() +
                                                           ") and";
                                            }

                                            break;
                                        }
                                    case TypeCode.String:
                                    case TypeCode.Char:
                                        {
                                            if (varValue == null || Convert.ToString(varValue) == "")
                                            {
                                                strValue = "(" + m_structFrePara.arrayFields[i] + " is null or " +
                                                           m_structFrePara.arrayFields[i] + " = '' ) and ";

                                            }
                                            else
                                            {
                                                strValue = "(" + m_structFrePara.arrayFields[i] + " = '" + varValue.ToString() +
                                                           "') and ";
                                            }
                                            break;
                                        }
                                }
                                strFieldsValue += strValue;

                            }
                        }
                        strFieldsValue = strFieldsValue.Substring(0, strFieldsValue.Length - 4);
                        listWhereClause.Add(strFieldsValue);

                        index++;
                    }

                    ipRecordset.Dispose();

                    if (listWhereClause.Count == 0)
                        return true;

                    // 目标字段字符串
                    string strTargetField = GetTargetField();
                    string strSql1 = "Select " + strGroup + ",BSM,ObjectID From " + layerName;
                    DataTable ipRecordsetRes = new DataTable();
                    ipRecordsetRes = Common.Utility.Data.AdoDbHelper.GetDataTable(this.m_QueryConnection, strSql1);
                    if (ipRecordsetRes == null)
                    {
                        return false;
                    }

                    DataRow[] listRow = null;
                    for (int i = 0; i < listWhereClause.Count; i++)
                    {
                        string strWhere = listWhereClause[i];
                        listRow = ipRecordsetRes.Select(strWhere);
                        if (listRow != null && listRow.Length > 0)
                        {
                            string strListBSM = "";  //重复的BSM字符串
                            int nIndex = 0;
                            int nOID = 0;
                            int BSM = 0;
                            foreach (DataRow dr in listRow) //遍历结果集
                            {
                                nIndex++;
                                if (nIndex >= 2)
                                {
                                    strListBSM += dr["BSM"].ToString() + "|";
                                }
                                else
                                {
                                    BSM = Convert.ToInt32(dr["BSM"]);
                                    nOID = Convert.ToInt32(dr["ObjectID"]);
                                }
                            }
                            strListBSM = strListBSM.Substring(0, strListBSM.Length - 1);

                            // 添加结果记录
                            Error pResInfo = new Error();
                            pResInfo.DefectLevel = this.m_DefectLevel;
                            pResInfo.RuleID = this.InstanceID;

                            // OID
                            pResInfo.OID = nOID;
                            pResInfo.BSM = BSM.ToString();
                            pResInfo.ReferBSM = strListBSM;

                            // 目标图层
                            pResInfo.LayerName = m_structFrePara.strFtName;
                            // 目标字段
                            pResInfo.ReferLayerName = strTargetField;
                            // 错误信息
                            pResInfo.Description = string.Format("'{0}'层中标识码为'{1}'的图形与标识码为'{2}'的图形'{3}({4})'字段值({5})存在重复", pResInfo.LayerName, BSM, strListBSM, strField, m_structFrePara.arrayFields[0], listRow[0][0]);

                            m_pResAttr.Add(pResInfo);

                            ipRecordsetRes.Dispose();
                        }


                    }

                    checkResult = m_pResAttr;
                    return true;
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
        private RuleExpression.FREQUENCYPARA m_structFrePara = new RuleExpression.FREQUENCYPARA();


        private bool CheckCount(long count, ref string strErrorMsg)
        {
            //如果为第一种情况，大于等于并且小于等于
            if (m_structFrePara.nType == 0)
            {
                if (count > m_structFrePara.nMinTime)
                {
                    if (m_structFrePara.nMaxTime != -1)
                    {
                        if (count < m_structFrePara.nMaxTime)
                            return true;
                        else
                        {
                            if (m_structFrePara.nMinTime == -1)
                            {
                                strErrorMsg = "地物的属性值存在重复，设定的重复条件为：目标字段属性值频度大于等于" + m_structFrePara.nMaxTime;
                            }
                            else
                            {
                                strErrorMsg = "地物的属性值存在重复，设定的重复条件为：目标字段属性值频度大于等于" + m_structFrePara.nMaxTime + "，并且小于等于" +
                                              (m_structFrePara.nMinTime);
                            }
                            return false;
                        }
                    }
                    return true;
                }
                strErrorMsg = "地物的属性值存在重复，设定的重复条件为：目标字段属性值频度大于等于" + m_structFrePara.nMaxTime + "，并且小于等于" +
                              (m_structFrePara.nMinTime);
            }
            //第二种情况，小于等于或者大于等于
            else
            {
                if (m_structFrePara.nMinTime != -1)
                {
                    if (count < m_structFrePara.nMinTime)
                    {
                        if (count > m_structFrePara.nMaxTime)
                            return true;
                        strErrorMsg = "地物的属性值存在重复，设定的重复条件为：目标字段属性值频度小于等于" + m_structFrePara.nMaxTime + "或者大于等于" +
                                      m_structFrePara.nMinTime;
                        return false;
                    }
                    strErrorMsg = "地物的属性值存在重复，设定的重复条件为：目标字段属性值频度小于等于" + m_structFrePara.nMaxTime + "或者大于等于" +
                                  m_structFrePara.nMinTime;
                }
                else
                {
                    if (count > m_structFrePara.nMaxTime)
                        return true;
                    strErrorMsg = "地物的属性值存在重复，设定的重复条件为：目标字段属性值频度小于等于" + m_structFrePara.nMaxTime;
                    return false;
                }
            }
            return false;
        }


        private bool CheckCount(long count)
        {
            //如果为第一种情况，大于等于并且小于等于
            if (m_structFrePara.nType == 0)
            {
                if (count >= m_structFrePara.nMinTime)
                {
                    if (m_structFrePara.nMaxTime != -1)
                    {
                        if (count < m_structFrePara.nMaxTime)
                            return true;
                        else
                            return false;
                    }
                    return true;
                }
            }
            //第二种情况，小于等于或者大于等于
            else
            {
                if (count <= m_structFrePara.nMinTime)
                {
                    if (m_structFrePara.nMinTime != -1)
                    {
                        if (count >= m_structFrePara.nMaxTime)
                            return true;
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }

        // 获取目标字段字符串
        private string GetTargetField()
        {
            string strTargetField = "";

            // 目标字段
            for (int i = 0; i < m_structFrePara.arrayFields.Count; i++)
            {
                strTargetField += m_structFrePara.arrayFields[i] + "|";
            }
            strTargetField = strTargetField.Substring(0, strTargetField.Length - 1);

            return strTargetField;
        }




    }
}