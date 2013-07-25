using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Hy.Check.Rule;
using Hy.Check.Utility;
using Hy.Common.Utility.Data;
using Hy.Check.Define;

namespace Rule
{

    public class RuleStatAdminRegion : BaseRule
    {

        //检查结构体
        private REGIONPARA m_structPara = new REGIONPARA();

        private string m_strName;
        //根据别名取图层名
        private string FatherLayerName = "";
        private string ChildLayerName = "";

        public RuleStatAdminRegion()
        {
            m_strName = "统计对比_行政区域面积对比质检规则";
            m_structPara.strChildFtName = "";
            m_structPara.strFatherFtName = "";
        }

        private bool CheckbyAdo(ref List<Hy.Check.Define.Error> checkResult)
        {
            DataTable ipRecordset = new DataTable();

            try
            {
                //根据级别，取相应的所有地类代码
                string strSql = "";
                strSql = "SELECT " + m_structPara.strCodeField + ",Shape_Area FROM " + FatherLayerName + "";

                //打开记录集
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

                       string IDName = dr[0].ToString();
                       double dbCalArea = Convert.ToDouble(dr[1]);

                        //与子图层关联的子段值，如：行政区名
                        DataTable ipRecordsetRes = new DataTable();

                        //根据所属区域所指定面积进行统计
                        string strSql1 = "Select SUM(" + m_structPara.strCompareField + ") FROM " + ChildLayerName +
                                         " Where " + m_structPara.strOwnerField + "='" +IDName + "'";

                        ipRecordsetRes = AdoDbHelper.GetDataTable(this.m_QueryConnection, strSql1);
                        //打开字段表记录集
                        if (ipRecordsetRes==null)
                        {
                            continue;
                        }

                        foreach (DataRow dr1 in ipRecordsetRes.Rows)
                        {

                            Error res = new Error();

                            double dbSurveyArea = Convert.ToDouble(dr1[0]);
                            double dbError = dbCalArea - dbSurveyArea;
                            res.LayerName = FatherLayerName;
                            res.ReferLayerName = ChildLayerName;

                            if (Math.Round(Math.Abs(dbError), 2) > m_structPara.dbThreshold)
                            {
                                res.Description = "ABS(计算面积:" + Math.Round(dbCalArea, 2) + "-调查面积:" +
                                                 dbSurveyArea.ToString("F2") + ")=" +
                                                 Math.Abs(dbError).ToString("F2") +
                                                 ",大于设定的阈值" + m_structPara.dbThreshold + "";
                                checkResult.Add(res);
                            }
                        }
                        ipRecordsetRes.Dispose();
                    }
                }
            }
            catch
            {
                return false;
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
            m_structPara.strAlias = strResult[i++];
            m_structPara.strRemark = strResult[i++];
            m_structPara.strFatherFtName = strResult[i++];
            m_structPara.strChildFtName = strResult[i++];
            m_structPara.strCodeField = strResult[i++];
            m_structPara.strOwnerField = strResult[i++];
            m_structPara.strCompareField = strResult[i];


            //阈值
            m_structPara.dbThreshold = pParameter.ReadDouble();

            return;
        }

        public override bool Verify()
        {
            //根据别名取featureclass的名字
            int standardID = SysDbHelper.GetStandardIDBySchemaID(this.m_SchemaID);
            FatherLayerName = LayerReader.GetNameByAliasName(m_structPara.strFatherFtName, standardID);
            ChildLayerName = LayerReader.GetNameByAliasName(m_structPara.strChildFtName, standardID);

            if (this.m_QueryConnection == null)
            {
                return false;
            }

            return true;
        }

        public override bool Check(ref List<Hy.Check.Define.Error> checkResult)
        {
            checkResult = new List<Error>();

            if (!CheckbyAdo(ref checkResult))
            {
                return false;
            }
            return true;
        }
    }

    public class REGIONPARA
    {
        public string strFatherFtName; //检查上一级区域
        public string strChildFtName; //检查下一级区域
        public string strCodeField; //行政区名或代码字段
        public string strOwnerField; //所属行政区名或代码字段（在下级表中）
        public string strCompareField; //面积比较字段
        public double dbThreshold; //阈值
       public string  strAlias;
        public string strRemark;
    }
}