using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Hy.Check.Define;
using Hy.Common.Utility.Data;

namespace Hy.Check.Rule
{
    public class RulePlotClass : BaseRule
    {
        //参数结构体
        private PlotClassPara m_structPara = new PlotClassPara();
        private  string m_strName="";
        //根据别名取图层名
        private string layerName = "";
        private List<RuleExpression.RESULT> m_arrResult = null;
        public RulePlotClass()
        {
            m_strName = "地类面积对比统计规则";
            m_arrResult = new List<RuleExpression.RESULT>();
        }

        private bool CheckbyAdo(string strTableName)
        {
            DataTable ipRecordset = new DataTable();

            //根据级别，取相应的所有地类代码
            string strSql = "";
            string strWhere = "";
            if (m_structPara.strClass.CompareTo("一级地类") == 0)
            {
                strSql = "SELECT DISTINCT(LEFT(" + m_structPara.strClassField + ",1)) FROM " + strTableName + "";
                strWhere = "LEFT(" + m_structPara.strClassField + ",1)";
            }
            else if (m_structPara.strClass.CompareTo("二级地类") == 0)
            {
                strSql = "SELECT DISTINCT(LEFT(" + m_structPara.strClassField + ",2)) FROM " + strTableName + "";
                strWhere = "LEFT(" + m_structPara.strClassField + ",2)";
            }
            else if (m_structPara.strClass.CompareTo("三级地类") == 0)
            {
                strSql = "SELECT DISTINCT(LEFT(" + m_structPara.strClassField + ",3)) FROM " + strTableName + "";
                strWhere = "LEFT(" + m_structPara.strClassField + ",3)";
            }
            else if (m_structPara.strClass.CompareTo("四级地类") == 0)
            {
                strSql = "SELECT DISTINCT(LEFT(" + m_structPara.strClassField + ",4)) FROM " + strTableName + "";
                strWhere = "LEFT(" + m_structPara.strClassField + ",4)";
            }
            //打开记录集，并分组
            ipRecordset = Hy.Common.Utility.Data.AdoDbHelper.GetDataTable(this.m_QueryConnection, strSql);

            if (ipRecordset.Rows.Count==0)
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
                    string strSql1 = "Select SUM(Shape_Area),SUM(" + m_structPara.strExpression +
                                     "),SUM(Shape_Area-(" +
                                     m_structPara.strExpression + ")) FROM " + strTableName + " Where " + strWhere +
                                     "='" + strCode + "'";

                    //打开记录集，并分组
                    ipRecordsetRes = Hy.Common.Utility.Data.AdoDbHelper.GetDataTable(this.m_QueryConnection, strSql1);

                    if (ipRecordsetRes.Rows.Count == 0)
                    {
                        continue;
                    }
                    foreach (DataRow dr1 in ipRecordsetRes.Rows)
                    {
                        RuleExpression.RESULT res = new RuleExpression.RESULT();

                        res.dbError = Convert.ToDouble(dr1[2]);

                        if (Math.Round(Math.Abs(res.dbError), 2) > m_structPara.dbThreshold)
                        {

                            res.dbCalArea = Convert.ToDouble(dr1[0]);
                            res.dbSurveyArea = Convert.ToDouble(dr1[1]);
                            res.IDName = strCode;

                            res.strErrInfo = "ABS(计算面积:" + Math.Round(res.dbCalArea, 2) + "-调查面积:" +
                                             res.dbSurveyArea.ToString("F2") + ")=" +
                                             Math.Abs(res.dbError).ToString("F2") +
                                             ",大于设定的阈值" + m_structPara.dbThreshold + "";
                            
                            m_arrResult.Add(res);
                        }
                    }
                    ipRecordsetRes.Dispose();
                }
            }
            ipRecordset.Dispose();
            return true;
        }

        public override string Name
        {
            get { return m_structPara.AliasName; }
        }

        public override IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {
            MemoryStream  stream=new MemoryStream(objParamters);
            BinaryReader pParameter = new BinaryReader(stream);

            pParameter.BaseStream.Position = 0;

            // 字符串总长度
            int nStrSize = pParameter.ReadInt32();

            //解析字符串
            Byte[] bb = new byte[nStrSize];
            pParameter.Read(bb, 0, nStrSize);
            string para_str = Encoding.Default.GetString(objParamters);
            para_str.Trim();

            string[] strResult = para_str.Split('|');

            int i = 0;
            m_structPara.AliasName = strResult[i++];
            m_structPara.Remark = strResult[i++];
            m_structPara.strFtName = strResult[i++];
            m_structPara.strClassField = strResult[i++];
            m_structPara.strExpression = strResult[i++];
            m_structPara.strClass = strResult[i];

            //阈值
            m_structPara.dbThreshold = pParameter.ReadDouble();

            return;
        }

        public override bool Verify()
        {
            if (base.m_QueryConnection == null)
            {
                return false;
            }
            if (base.m_ResultConnection== null) return false;

            return true;
        }

        public override bool Check(ref List<Error> checkResult)
        {

            layerName = base.GetLayerName(m_structPara.AliasName);

            if (!CheckbyAdo(layerName))
            {
                return false;
            }
            if (!SaveResult(m_structPara.strFtName))
            {
                return false;
            }
            return true;
        }

        // 保存结果,主要用于CRuleDistrict,CRulePlotClass,CRuleSheet,CRuleStatAdminRegion
        public bool SaveResult(string strTargetFc)
        {
            try
            {
                string strSql = "delete * from LR_ResAutoStat_PlotClass where RuleInstID='" + base.m_InstanceID + "'";
               Hy.Common.Utility.Data.AdoDbHelper.ExecuteSql(base.m_ResultConnection, strSql);
                //------------------------------------------------//
                //				在结果表中存储结果				  //														
                //------------------------------------------------//
                DataTable ipRecordset = new DataTable();

                if (! AdoDbHelper.OpenTable("LR_ResAutoStat_PlotClass", ref ipRecordset, base.m_ResultConnection))
                {
                    return false;
                }

                for (int i = 0; i < m_arrResult.Count; i++)
                {
                    RuleExpression.RESULT res = m_arrResult[i];
                    //------设置字段数据
                    DataRow dr = ipRecordset.NewRow();
                    dr["RuleInstID"] = base.m_InstanceID;
                    dr["目标图层"] = strTargetFc;
                    dr["统计内容名称"] = res.IDName;
                    dr["计算面积"] = res.dbCalArea;
                    dr["调查面积"] = res.dbSurveyArea;
                    string strErr = "" + Math.Abs(res.dbError / res.dbCalArea) * 100 + "";
                    dr["误差(百分比)"] = strErr;
                    dr["错误消息"] = res.strErrInfo;

                    //------添加新记录
                    ipRecordset.Rows.Add(dr);

                    //更新记录
                    ipRecordset.AcceptChanges();
                }

                AdoDbHelper.UpdateTable("LR_ResAutoStat_PlotClass", ipRecordset, base.m_ResultConnection);

                //关闭记录集
                ipRecordset.Dispose();
            }
            catch (Exception ex)
            {
                //Hy.Check.Rule.Helper.LogAPI.CheckLog.AppendErrLogs(ex.ToString());
                //显示错误信息
                //XtraMessageBox.Show("CRulePlot::SaveResult():" + ex.Message + "");
                return false;
            }

            return true;
        }

    }

    /// <summary>
    ///  地类面积对比参数类
    /// </summary>
    public class PlotClassPara
    {
        public string AliasName;
        public string Remark;
        public string strFtName; //被检图层名
        public string strExpression; //调查面积计算表达式
        public string strClassField; //地类代码字段名
        public double dbThreshold; //容差阈值
        public string strClass; //统计级别(一级地类，二级地类，三级地类，四级地类）
    }
}