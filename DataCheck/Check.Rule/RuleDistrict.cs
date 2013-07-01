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
    public class RuleDistrict : BaseRule
    {


        public override string Name
        {
            get { return "辖区面积对比统计规则"; }
        }

        public override IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {
            BinaryReader pParameter = new BinaryReader(new MemoryStream(objParamters));
            pParameter.BaseStream.Position = 0;

            // 字符串总长度
            int nStrSize = pParameter.ReadInt32();

            //解析字符串
            Byte[] bb = new byte[nStrSize];
            pParameter.Read(bb, 0, nStrSize);
            string para_str = Encoding.Default.GetString(bb);
            para_str.Trim();

            string[] strResult = para_str.Split('|');

            int i = 0;
            string m_strAlias = strResult[i++];
            string m_strRemark = strResult[i++];
            m_structPara.strFtName = strResult[i++];
            m_structPara.strDistrictField = strResult[i++];
            m_structPara.strExpression = strResult[i];
            //级别
            m_structPara.iClass = pParameter.ReadInt32();
            //阈值
            m_structPara.dbThreshold = pParameter.ReadDouble();

        }

        public override bool Verify()
        {
            //根据别名取图层名
            int standardID = SysDbHelper.GetStandardIDBySchemaID(this.m_SchemaID);
            layerName = LayerReader.GetNameByAliasName(m_structPara.strFtName, standardID);
            if (string.IsNullOrEmpty(layerName))
            {
                SendMessage(enumMessageType.VerifyError, string.Format("当前标准中不存在图层“{0}”", m_structPara.strFtName));
                return false;
            }
            return true;
        }

        private string layerName;
        public override bool Check(ref List<Error> checkResult)
        {
            try
            {
                //清除以前结果
                List<Error> m_arrResult = new List<Error>();
                DataTable ipRecordset = new DataTable();
                string strSql = "";
                string strWhere = "";
                if (m_structPara.iClass == 0)
                {
                    strSql = "SELECT DISTINCT(LEFT(" + m_structPara.strDistrictField + ",6)) FROM " + layerName + "";
                    strWhere = "LEFT(" + m_structPara.strDistrictField + ",6)";
                }
                else if (m_structPara.iClass == 1)
                {
                    strSql = "SELECT DISTINCT(LEFT(" + m_structPara.strDistrictField + ",9)) FROM " + layerName + "";
                    strWhere = "LEFT(" + m_structPara.strDistrictField + ",9)";
                }
                else if (m_structPara.iClass == 2)
                {
                    strSql = "SELECT DISTINCT(LEFT(" + m_structPara.strDistrictField + ",12)) FROM " + layerName + "";
                    strWhere = "LEFT(" + m_structPara.strDistrictField + ",12)";
                }

                //打开记录集，并分组
                ipRecordset = Common.Utility.Data.AdoDbHelper.GetDataTable(this.m_QueryConnection, strSql);
                if (ipRecordset == null)
                {
                    return false;
                }

                foreach (DataRow dr in ipRecordset.Rows) //遍历结果集
                {
                    if (dr != null)
                    {
                        string strCode = dr[0].ToString();

                        DataTable ipRecordsetRes = new DataTable();

                        //根据所在辖区再查
                        string strSql1 = "Select SUM(Shape_Area),SUM(" + m_structPara.strExpression + "),SUM(Shape_Area-(" +
                                         m_structPara.strExpression + ")) FROM " + layerName + " Where " + strWhere +
                                         "='" + strCode + "'";

                        //打开字段表记录集
                        ipRecordsetRes = Common.Utility.Data.AdoDbHelper.GetDataTable(this.m_QueryConnection, strSql1);
                        if (ipRecordsetRes == null)
                        {
                            continue;
                        }

                        foreach (DataRow dr1 in ipRecordsetRes.Rows)
                        {

                            Error res = new Error();
                            res.DefectLevel = this.m_DefectLevel;
                            res.RuleID = this.InstanceID;

                            double dblError = Convert.ToDouble(dr1[2]);
                            double dblCalculateArea = Convert.ToDouble(dr1[0]);
                            double dblSurveyArea = Convert.ToDouble(dr1[1]);
                            res.Remark = dblError.ToString();

                            if (Math.Round(Math.Abs(dblError), 2) > m_structPara.dbThreshold)
                            {
                                res.Remark += "|" + dblSurveyArea.ToString();
                                res.Remark += "|" + dblSurveyArea.ToString();
                                res.Remark += "|" + strCode;

                                res.Description = "ABS(计算面积:" + Math.Round(dblCalculateArea, 2) + "-调查面积:" +
                                                dblSurveyArea.ToString("F2") + ")=" +
                                                Math.Abs(dblError).ToString("F2") +
                                                ",大于设定的阈值" + m_structPara.dbThreshold + "";
                                m_arrResult.Add(res);
                            }
                            #region  原程序中将“计算面积”，“调查面积”进行了存储////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            //RuleExpression.RESULT res = new RuleExpression.RESULT();
                            //res.dbError = Convert.ToDouble(dr1[2]);

                            //if (Math.Round(Math.Abs(res.dbError), 2) > m_structPara.dbThreshold)
                            //{
                            //    res.dbCalArea = Convert.ToDouble(dr1[0]);
                            //    res.dbSurveyArea = Convert.ToDouble(dr1[1]);
                            //    res.IDName = strCode;

                            //    res.strErrInfo = "ABS(计算面积:" + Math.Round(res.dbCalArea, 2) + "-调查面积:" +
                            //                    res.dbSurveyArea.ToString("F2") + ")=" +
                            //                    Math.Abs(res.dbError).ToString("F2") +
                            //                    ",大于设定的阈值" + m_structPara.dbThreshold + "";
                            //    //res.strErrInfo = string.Format("{0}标识码为{1}的图斑面积{2}与(图斑地类面积+零星地物面积+扣除地类面积+线状地物面积){3}之差为{4}平方米,应≤0.01平方米", m_structPara.strFtName, res.BSM, res.dbSurveyArea, res.dbCalArea, Math.Abs(res.dbError).ToString("F2"));
                            //    m_arrResult.Add(res);
                            //}

                            #endregion
                        }

                        ipRecordsetRes.Dispose();
                    }
                }
                ipRecordset.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                return false;
            }
            return true;
        }

        public RuleExpression.DISTRICTPARA m_structPara = new RuleExpression.DISTRICTPARA(); //参数结构体      
     
    }
}