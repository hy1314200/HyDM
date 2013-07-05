using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Hy.Check.Rule;
using Common.Utility.Data;
using Hy.Check.Utility;
using Hy.Check.Define;


namespace Rule
{
    public class RuleSheet : BaseRule
    {
        //参数结构体
        private SHEETPARA m_structPara = new SHEETPARA();
        private string m_strName;
        private string layerName;

        public RuleSheet()
        {
            m_strName = "图幅面积对比统计规则";
        }

        public override string Name
        {
            get { return m_strName; }
        }

        public override Hy.Check.Define.IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {
            MemoryStream stream = new MemoryStream(objParamters);
            BinaryReader pParameter = new BinaryReader(stream);

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
             m_structPara.strAlias= strResult[i++];
             m_structPara.strRemark= strResult[i++];
            m_structPara.strFtName = strResult[i++];
            m_structPara.strSheetField = strResult[i++];
            m_structPara.strExpression = strResult[i];

            //阈值
            m_structPara.dbThreshold = pParameter.ReadDouble();
            return;
        }

        public override bool Verify()
        {
            //根据别名取featureclass的名字
            int standardID = SysDbHelper.GetStandardIDBySchemaID(this.m_SchemaID);
            layerName = LayerReader.GetNameByAliasName(m_structPara.strFtName, standardID);

            //清除以前结果
            if (this.m_QueryConnection == null)
            {
                return false;
            }
            return true;
        }

        public override bool Check(ref List<Hy.Check.Define.Error> checkResult)
        {
            if (!CheckbyAdo(ref checkResult))
            {
                return false;
            }

            return true;
        }

        private bool CheckbyAdo(ref List<Hy.Check.Define.Error> checkResult)
        {
            DataTable ipRecordset = new DataTable();
            try
            {
                string strSql = "Select " + m_structPara.strSheetField + ",SUM(Shape_Area),SUM(" +
                                m_structPara.strExpression + "),SUM(Shape_Area-(" + m_structPara.strExpression + ")) From " +
                                layerName + " GROUP BY " + m_structPara.strSheetField + "";

                ipRecordset = AdoDbHelper.GetDataTable(this.m_QueryConnection, strSql);

                if (ipRecordset == null)
                {
                    return false;
                }

                checkResult = new List<Hy.Check.Define.Error>();

                foreach (DataRow dr in ipRecordset.Rows) //遍历结果集
                {
                    if (dr != null)
                    {
                        Hy.Check.Define.Error res = new Hy.Check.Define.Error();
                         
                        //误差值
                         double dbError = Convert.ToDouble(dr[3]);
                        //计算面积
                        double dbCalArea = Convert.ToDouble(dr[1]);
                        //调查面积
                        double dbSurveyArea = Convert.ToDouble(dr[2]);

                        res.Description = "ABS(计算面积:" + Math.Round(dbCalArea, 2) + "-调查面积:" +
                                             dbSurveyArea.ToString("F2") + ")=" +
                                             Math.Abs(dbError).ToString("F2") +
                                             ",大于设定的阈值" + m_structPara.dbThreshold + "";

                        checkResult.Add(res);
                        }
                }
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
            }
            finally
            {
                if (ipRecordset != null)
                {
                    ipRecordset.Dispose();
                }
            }
            return true;
        }
    }

    /// <summary>
    /// 图幅面积对比参数类
    /// </summary>
    public class SHEETPARA
    {
        public string strFtName; //被检图层名
        public string strSheetField; //所在图幅字段名
        public string strExpression; //调查面积计算表达式
        public double dbThreshold; //容差阈值
        public string strAlias;
        public string strRemark;
    }
}