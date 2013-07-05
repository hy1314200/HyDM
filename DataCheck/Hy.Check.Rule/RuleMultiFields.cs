using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using Hy.Check.Define;
using System.Runtime.InteropServices;
using Common.Utility.Data;
using System.Data.OleDb;
using Hy.Check.Rule.Helper;
using Hy.Check.Utility;

namespace Hy.Check.Rule
{
    /// <summary>
    /// 多字段关系质检规则类
    /// </summary>
    public class RuleMultiFields : BaseRule
    {
        //具体的规则描述信息，如：拓扑规则－基本农田层内要素不能叠盖等
        //private string m_strDesc;

        //查询参数结构体
        private MULTIFIELDSPARA m_structPara = new MULTIFIELDSPARA();

        private string FtName1 = "";
        private string FtName2 = "";

        private IFeatureWorkspace m_ipFtWS;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleMultiFields"/> class.
        /// </summary>
        public RuleMultiFields()
        {
            //清空查询参数
            m_structPara.strName = "多字段关系质检规则";
        }

        /// <summary>
        ///  获取SQL规则的检查结果
        /// </summary>
        /// <param name="pRecord"></param>
        /// <param name="bTable"></param>
        /// <returns></returns>
        private List<Error> GetResult(DataTable pRecord,bool bTable)
        {
            if (pRecord == null || pRecord.Rows.Count == 0) return null;

            string strErrIfo = ConstructErrorInfo();
            List<Error> pResAttr = new List<Error>();

            foreach (DataRow dr in pRecord.Rows)
            {
                // 添家结果记录
                Error pResInfo = new Error();
                pResInfo.DefectLevel = this.m_DefectLevel;
                pResInfo.RuleID = this.InstanceID;

                if (bTable)
                {
                    pResInfo.LayerName = COMMONCONST.TABLENAME;
                }
                else
                {
                    pResInfo.LayerName = FtName1;
                }

                pResInfo.ReferLayerName = FtName2;
                //pResInfo.strErrInfo = strErrIfo;

                if(dr.Table.Columns.Contains("ObjectID"))
                {
                    pResInfo.OID = Convert.ToInt32(dr["ObjectID"]);

                }

                if(dr.Table.Columns.Contains("BSM"))
                {
                    pResInfo.BSM =dr["BSM"].ToString();
                }

                if (m_structPara.strScript == "行政区层中行政区代码与行政区名称不匹配")
                {
                    //pResInfo.strErrInfo = "行政区代码为'" + dr["XZQDM"].ToString() + "',行政区名称为'" + dr["XZQMC"].ToString() + "',但权属单位代码表中行政区名称为'" + dr["qsdwmc"].ToString() + "'";
                    pResInfo.Description = string.Format(Helper.ErrMsgFormat.ERR_4201_5_2, pResInfo.LayerName,pResInfo.BSM,"行政区代码" ,dr["XZQDM"].ToString(), "权属单位代码表");
                }
                else if (m_structPara.strScript == "行政区层中的行政区代码与权属代码表不一致")
                {
                    pResInfo.Description = string.Format("行政区层ObjectID为{0}的行政区代码({1})不正确，正确值参见：权属代码表", dr[0], dr[1]);
                }
                else if (m_structPara.strAlias == "扣除地类面积检查" || m_structPara.strAlias == "基本农田图斑扣除地类面积检查")
                {
                    //pResInfo.strErrInfo = m_structPara.strScript + ",二者相差" + Convert.ToDouble(dr["diff"]).ToString("F2") + "平方米";
                    pResInfo.Description = string.Format("标识码为{0}的扣除地类面积与'（{2}面积-线状地物面积-零星地物面积）*扣除地类系数'的面积不一致，两者相差{1}", dr[1].ToString(), dr[2].ToString(), FtName1);
                    //pResInfo.strErrInfo = string.Format("{0}{1}与{2}{3}不一致,两者相差{4}", dr.Table.Columns[2].ColumnName, Str2Double(dr[2]).ToString("F2"), dr.Table.Columns[3].ColumnName, Str2Double(dr[3]).ToString("F2"), Str2Double(dr["diff"]).ToString("f2"));
                }
                else if (m_structPara.strScript == "线状地物层中的权属单位代码1、权属单位代码2与权属单位代码表不一致")
                {
                    string qsdwdm1 = dr[2].ToString();
                    string qsdwdm2 = dr[3].ToString();
                    if (string.IsNullOrEmpty(qsdwdm1))
                    {
                        pResInfo.Description = "权属单位代码2与权属单位代码表不一致";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(qsdwdm2))
                        {
                            pResInfo.Description = "权属单位代码1与权属单位代码表不一致";
                        }
                        else
                        {
                            pResInfo.Description = "权属单位代码1、权属单位代码2与权属单位代码表不一致";
                        }
                    }
                }
                else if (m_structPara.strAlias.Contains("一致性检查") && m_structPara.strAlias.Contains("表中"))
                {
                    string table_name = m_structPara.strScript.Substring(0, m_structPara.strScript.IndexOf("表中"));
                    string xzqdm = dr[0].ToString();
                    //pResInfo.strErrInfo =string.Format("{0},行政区代码为{1},差值为{2}公顷",m_structPara.strScript ,dr[0].ToString(),Convert.ToDouble(dr[1]).ToString("F2"));
                    if (xzqdm.Length == 12)
                    {
                        pResInfo.Description = string.Format("{0}表中{1}的{2}({3})与{4}({5})不一致，两者差值({6})", table_name, dr[0], dr.Table.Columns[2].ColumnName, Str2Double(dr[2]).ToString("f2"), dr.Table.Columns[3].ColumnName, Str2Double(dr[3]).ToString("f2"), Str2Double(dr[1]).ToString("f2"));
                    }
                    else
                    {
                        pResInfo.Description = string.Format("{0}表中{2}({3})与{4}({5})不一致，两者差值({6})", table_name, dr[0], dr.Table.Columns[2].ColumnName, Str2Double(dr[2]).ToString("f2"), dr.Table.Columns[3].ColumnName, Str2Double(dr[3]).ToString("f2"), Str2Double(dr[1]).ToString("f2"));
                    }
                }

                else if (m_structPara.strScript == "农村土地利用现状一级分类面积汇总表与农村土地利用现状二级分类面积汇总表面积不一致")
                {
                    foreach (DataColumn pColumn in pRecord.Columns)
                    {
                        if (pColumn.Caption.Equals("xzqdm") || (pColumn.Caption.Contains("差异") == false)) continue;
                        double val = Convert.ToDouble(dr[pColumn]);
                        if (val > 0.0)
                        {
                           Error tempResInfo = new Error();
                           tempResInfo.DefectLevel = this.m_DefectLevel;
                           tempResInfo.RuleID = this.InstanceID;

                           tempResInfo.LayerName = pResInfo.LayerName;
                           tempResInfo.ReferLayerName = pResInfo.ReferLayerName;
                            tempResInfo.OID = pResInfo.OID;
                            tempResInfo.BSM = pResInfo.BSM;
                            //tempResInfo.strErrInfo = pColumn.Caption.Replace("差异", "不一致") + ",差值为" + val.ToString();
                            string col_name = pColumn.Caption.Replace("差异", "");
                            string mj_dist = Str2Double(dr[col_name + "_DIST"]).ToString("f2");
                            string mj = Str2Double(dr[col_name]).ToString("f2");
                            tempResInfo.Description = string.Format("农村土地利用现状一级分类面积汇总表的{0}({1})与农村土地利用现状二级分类面积汇总表的{0}({2})不一致，两者差值({3})", col_name, mj_dist, mj, val.ToString());
                            pResAttr.Add(tempResInfo);
                            break;
                        }
                    }
                    return pResAttr;
                }
                else if (m_structPara.strScript == "农村土地利用现状一级分类面积按权属性质汇总表与农村土地利用现状二级分类面积汇总表总面积不一致" ||
                         m_structPara.strScript == "农村土地利用现状一级分类面积按权属性质汇总表与农村土地利用现状一级分类面积汇总表总面积不一致" ||
                         m_structPara.strScript == "耕地坡度分级面积汇总表与农村土地利用现状一级分类面积汇总表耕地面积不一致")
                {
                    double douDiff = 0.00;
                    double.TryParse(dr[3].ToString(), out douDiff);//"面积差异"
                    if (Math.Round(douDiff, 3) > 0.001)
                    {
                        if (m_structPara.strScript == "耕地坡度分级面积汇总表与农村土地利用现状一级分类面积汇总表耕地面积不一致")
                        {
                            pResInfo.Description = string.Format("耕地坡度分级面积汇总表中的耕地面积({0})与农村土地利用现状一级分类面积汇总表中的耕地面积({1})不一致，两者差值{2}", Str2Double(dr[1]).ToString("f2"), Str2Double(dr[2]).ToString("f2"), douDiff);
                        }
                        else if (m_structPara.strScript == "农村土地利用现状一级分类面积按权属性质汇总表与农村土地利用现状二级分类面积汇总表总面积不一致")
                        {
                            pResInfo.Description = string.Format("农村土地利用现状一级分类面积按权属性质汇总表总面积({0})与农村土地利用现状二级分类面积汇总表总面积({1})不一致，两者差值({2})", Str2Double(dr[1]).ToString("f2"), Str2Double(dr[2]).ToString("f2"), douDiff);
                        }
                        else if (m_structPara.strScript == "农村土地利用现状一级分类面积按权属性质汇总表与农村土地利用现状一级分类面积汇总表总面积不一致")
                        {
                            pResInfo.Description = string.Format("农村土地利用现状一级分类面积按权属性质汇总表总面积({0})与农村土地利用现状一级分类面积汇总表总面积({1})不一致，两者差值({2})", Str2Double(dr[1]).ToString("f2"), Str2Double(dr[2]).ToString("f2"), douDiff);
                        }
                        else
                        {
                            pResInfo.Description = string.Format("{0},两个面积的差异值为{1}", m_structPara.strScript, Math.Round(douDiff, 2).ToString());
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (m_structPara.strAlias == "地类图斑和线状地物坐落关系面积检查" || m_structPara.strAlias == "地类图斑和零星地物座落关系面积检查")
                {
                    double dDiff = Convert.ToDouble(dr["DIfferent"]);
                    if (dDiff < 0.1)
                    {
                        continue;
                    }
                    if (m_structPara.strScript.Trim() != "" && m_structPara.strScript != null)
                    {
                        pResInfo.Description = string.Format("{0}两个面积的差异值为{1}", m_structPara.strScript, Math.Round(dDiff, 3).ToString());
                    }
                }
                else if (m_structPara.strAlias == "图斑层中的权属单位代码是否与权属代码表一致检查" || m_structPara.strAlias == "图斑层中的座落单位代码是否与权属代码表一致检查")
                {
                    pResInfo.Description = string.Format("地类图斑层中标识码为{0}的图斑的权属单位代码({0})与权属单位代码表中的权属单位代码不匹配", dr["bsm"], dr[3].ToString());
                }
                else
                {
                    if (m_structPara.strScript.Trim() != "" && m_structPara.strScript != null)
                    {
                        pResInfo.Description = m_structPara.strScript;
                    }
                }

                pResAttr.Add(pResInfo);
            }
            return pResAttr;
        }

        private string ConstructErrorInfo()
        {
            int layerid = -1;
            StandardHelper pStand = new StandardHelper(SysDbHelper.GetSysDbConnection());
            pStand.GetLayerIDByTableName(ref layerid, this.m_SchemaID, m_structPara.strFtName);

            string strSql1 = "Select FieldCode,FieldName From LR_DicField Where LayerID = " + layerid;

            List<FIELDMAP> arrFieldMap = new List<FIELDMAP>();
            DataTable dt = new DataTable();
            AdoDbHelper.GetDataTable(SysDbHelper.GetSysDbConnection(), strSql1);
            if (dt.Rows.Count==0)
            {
                return null;
            }
            foreach (DataRow dr in dt.Rows)
            {
                string FName = dr["FieldCode"].ToString(); //字段名
                string FAlias = dr["FieldName"].ToString(); //字段别名

                FIELDMAP fMap = new FIELDMAP();
                fMap.strAlias = FAlias;
                fMap.strName = FName;
                arrFieldMap.Add(fMap);
            }

            /*string Alias = "", Alias2 = "";
            pStand.GetAliasByLayerName(ref Alias, m_structPara.strFtName, m_strStdName);
            pStand.GetAliasByLayerName(ref Alias2, m_structPara.strFtName2, m_strStdName);*/
            int standardID = SysDbHelper.GetStandardIDBySchemaID(this.m_SchemaID);
            if (!string.IsNullOrEmpty(FtName1))
            {

                FtName1 = LayerReader.GetNameByAliasName(m_structPara.strFtName, standardID);
            }
            if (!string.IsNullOrEmpty(FtName2))
            {
                FtName2=LayerReader.GetNameByAliasName(m_structPara.strFtName, standardID);
            }
            string strErrInfo = m_structPara.strClause;

            for (int i = 0; i < arrFieldMap.Count; i++)
            {
                string strR = arrFieldMap[i].strAlias;
                string strS = arrFieldMap[i].strName;
                strErrInfo.Replace(strS, strR);
            }
            strErrInfo.Replace(m_structPara.strFtName, FtName1);
            strErrInfo.Replace(m_structPara.strFtName2, FtName2);
            strErrInfo = "字段值符合表达式 (" + strErrInfo + ")";

            return strErrInfo;
        }


        /// <summary>
        /// 二调项目质检的属性检查
        /// </summary>
        /// <param name="pRecord"></param>
        /// <returns></returns>
        private List<Error> GetAttrResult(DataTable pRecord)
        {
            List<Error> pResAttr = new List<Error>();
            //string strZLDWDM = "座落单位代码";
            //string strXZQDM = "行政区代码";
            if(pRecord.Rows.Count>0)
            {
                DataRow dr = pRecord.Rows[0];
                DataColumnCollection dcCollection = pRecord.Columns;
                for (int i = 0; i < dcCollection.Count; i++)
                {
                    DataColumn dc = dcCollection[i];
                    string strColumnName = dc.ColumnName;
                    if (strColumnName.Trim().Equals( "zldwdm" ,StringComparison.OrdinalIgnoreCase)
                        || strColumnName.Trim().Equals("xzqdm", StringComparison.OrdinalIgnoreCase)
                        || strColumnName.Trim().Equals("szxzqdm", StringComparison.OrdinalIgnoreCase)
                        || (strColumnName.Contains("差异")==false)
                        )
                    {
                       continue;
                    }
                    else if (strColumnName.Trim() == "县控制面积与图斑汇总面积差异")
                    {
                        double value = Math.Abs(Convert.ToDouble(dr[strColumnName]));
                        if (Math.Round(value, 2) > 10.00)
                        {
                            Error pResInfo = new Error();
                            pResInfo.DefectLevel = this.m_DefectLevel;
                            pResInfo.RuleID = this.InstanceID;

                            //string strCY = strColumnName.Replace("差异", "");
                            pResInfo.LayerName = COMMONCONST.TABLENAME;
                            pResInfo.ReferLayerName = m_structPara.strFtName2;
                            //pResInfo.strErrInfo = strColumnName +"为"+ value.ToString("F2") + "平方米";
                            pResInfo.Description = string.Format(Helper.ErrMsgFormat.ERR_450101031, Str2Double(dr[0]).ToString("F2"), Str2Double(dr[1]).ToString("F2"), value.ToString("F2"));
                            pResAttr.Add(pResInfo);
                        }
                    }
                    else
                    {
                        double value = Convert.ToDouble(dr[strColumnName]);
                        if (Math.Round(value, 2) > 0.10)
                        {
                            // 添家结果记录
                            Error pResInfo = new Error();
                            pResInfo.DefectLevel = this.m_DefectLevel;
                            pResInfo.RuleID = this.InstanceID;

                            string strCY = strColumnName.Replace("差异", "");
                            pResInfo.LayerName = COMMONCONST.TABLENAME;
                            pResInfo.ReferLayerName = m_structPara.strFtName2;
                            //pResInfo.strErrInfo = FtName1 + "和" + FtName2 + "的" + strColumnName + "不相等，二者相差" + value.ToString("F2") + "公顷";
                            string tempColumnName = strCY + "_DIST";
                            if (!pRecord.Columns.Contains(tempColumnName))
                            {
                                continue;
                            }
                            string mj_dist = Str2Double(dr[tempColumnName]).ToString("f2");
                            string mj = Str2Double(dr[strCY]).ToString("f2");
                            pResInfo.Description = string.Format("{0}中{1}({2})与数据库汇总面积({3})不一致，两者差值({4})", FtName1, strCY, mj,mj_dist, value.ToString("F2"));
                            pResAttr.Add(pResInfo);

                        }
                    }
                }
            }

            return pResAttr;
        }

        private List<Error> GetTKXSResult(DataTable pRecord)
        {
            DataTable pDt = null;
            DataTable tkxs1Dt = null;
            try
            {
                List<Error> pResAttr = new List<Error>();
                string strZLDWDM = "座落单位代码";
                string strXZQDM = "行政区代码";
                if (pRecord.Rows.Count > 0)
                {
                    DataRow dr = pRecord.Rows[0];
                    DataColumnCollection dcCollection = pRecord.Columns;

                    string sqlStr = "select * from " + COMMONCONST.TB_DIST_TKXS;

                    AdoDbHelper.OpenTable(COMMONCONST.TB_DIST_TKXS,ref pDt, base.m_QueryConnection);

                    if (pDt == null || pDt.Rows.Count == 0) return null;

                    //到TKXS1表中查找相应数据（全部tkxs的集合），字段顺序与DIST_TKXS一致
                    sqlStr = "select XZQDM,XZQMC,T2DEGREE,T6DEGREE,T15DEGREE,T25DEGREE,P2DEGREE,P6DEGREE,P15DEGREE,P25DEGREE from " + COMMONCONST.TB_TKXS1 + " where XZQDM = '" + pDt.Rows[0]["XZQDM"].ToString() + "'";

                    tkxs1Dt = AdoDbHelper.GetDataTable(base.m_QueryConnection,sqlStr);

                    for (int i = 0; i < dcCollection.Count; i++)
                    {
                        DataColumn dc = dcCollection[i];
                        string strColumnName = dc.ColumnName;

                        double value;
                        if (!double.TryParse(dr[strColumnName].ToString(), out value) || 
                            strColumnName.Trim().Equals("xzqdm", StringComparison.OrdinalIgnoreCase) ||
                            strColumnName.Trim().Equals("xzqmc", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                        if (Math.Round(value, 4) > 0.10)
                        {
                            bool flag = false;
                            int index = (i == 2 ? i : ((2 * i) + (2 * (i - 3))));
                            string[] tkxs1Values = tkxs1Dt.Rows[0][i].ToString().Split(',');
                            Hashtable tkxs1HsTable = new Hashtable();
                            foreach (string dnStr in tkxs1Values)
                            {
                                if (!tkxs1HsTable.ContainsKey(dnStr))
                                    tkxs1HsTable.Add(double.Parse(dnStr), null);
                            }
                            for (int j = 0; j < 4; j++)
                            {
                                double tempValue;
                                double.TryParse(pDt.Rows[0][index + j].ToString(), out tempValue);
                                if (tempValue != 0 && !tkxs1HsTable.ContainsKey(tempValue))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                pRecord.Rows[0][i] = 0;
                                continue;
                            }
                            // 添家结果记录
                           Error  err = new Error();
                           err.DefectLevel = this.m_DefectLevel;
                           err.RuleID = this.InstanceID;

                           err.LayerName = COMMONCONST.TABLENAME;
                           err.ReferLayerName = m_structPara.strFtName2;
                            string str = strColumnName.Replace(COMMONCONST.TB_DIST_TKXS, "田坎系数");
                            err.Description = "本软件提取的'" + str + "'与省级上报的'" + str + "'不相等";
                            pResAttr.Add(err);
                        }
                    }
                }

                return pResAttr;
            }
            catch (Exception ex)
            {
                //Hy.Check.Rule.Helper.LogAPI.CheckLog.AppendErrLogs(ex.ToString());
                return null;
            }
            finally
            {
                if(pDt != null)
                {
                    pDt.Dispose();
                }
                if(tkxs1Dt!=null)
                {
                    tkxs1Dt.Dispose();
                }
            }
        }
        
        /// <summary>
        /// 获取需要特殊处理的规则别名
        /// </summary>
        /// <returns></returns>
        private Hashtable GetEnumHash()
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("农村土地利用现状一级分类面积汇总表与数据库汇总面积一致性检查","");
            hashtable.Add("农村土地利用现状二级分类面积汇总表与数据库汇总面积一致性检查", "");
            hashtable.Add("农村土地利用现状一级分类面积按权属性质汇总表与数据库汇总面积一致性检查", "");
            hashtable.Add("基本农田情况统计表与数据库汇总面积一致性检查", "");
            hashtable.Add("耕地坡度分级面积汇总表与数据库汇总面积一致性检查", "");

            hashtable.Add("地类图斑层图斑面积和行政区汇总面积一致性检查","");

            hashtable.Add("省级上报田坎系数与上报数据中田坎系数是否一致检查", "");                 //王君
            //hashtable.Add("多表关系质检规则", "");

            hashtable.Add("基本农田补划面积汇总表与数据库汇总面积一致性检查","");
            //hashtable.Add(" 基本农田补划面积汇总表中县级耕地面积与乡级耕地面积之和一致性检查", "");
            //hashtable.Add("基本农田补划面积汇总表中乡级耕地面积与村级耕地面积之和一致性检查", "");
            //hashtable.Add("基本农田补划面积汇总表中小计面积与各分地类面积之和一致性检查", "");

            //增加海岛、飞入地一级、二级统计表检查的规则
            //jinjj  2010-11-09
            hashtable.Add("飞入地土地利用现状一级分类面积汇总表与数据库汇总面积一致性检查", "");
            hashtable.Add("飞入地土地利用现状二级分类面积汇总表与数据库汇总面积一致性检查", "");
            hashtable.Add("海岛土地利用现状一级分类面积汇总表与数据库汇总面积一致性检查", "");
            hashtable.Add("海岛土地利用现状二级分类面积汇总表与数据库汇总面积一致性检查", "");
            return hashtable;
        }

        private double Str2Double(object obj)
        {
            try
            {
                return Convert.ToDouble(obj);
            }
            catch
            {
                return 0.0;
            }
        }



        public override string Name
        {
            get { return m_structPara.strAlias; }
        }

        public override IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(objParamters));
            reader.BaseStream.Position = 0;
            int nStrSize = reader.ReadInt32(); // 字符串总长度

            //解析字符串
            Byte[] bb = new byte[nStrSize];
            reader.Read(bb, 0, nStrSize);
            string para_str = Encoding.Default.GetString(bb);

            para_str.Trim();
            string[] strResult = para_str.Split('|');

            int i = 0;

            m_structPara.strAlias = strResult[i++];
            m_structPara.strScript = strResult[i++];
            m_structPara.strFtName = strResult[i++];
            m_structPara.strFtName2 = strResult[i++];
            if (strResult.Length == 8)
            {
                m_structPara.strWhereClause = strResult[6];
                m_structPara.strClause = strResult[7];
            }
            else
            {
                m_structPara.strWhereClause = strResult[i++];
                m_structPara.strClause = strResult[i];
            }
            //if (m_structPara.strAlias.Contains("一致性检查") && m_structPara.strAlias.Contains("表中"))
            //{
            //}

            // 经测试，数据库中保存的是图层本身
            FtName1 = m_structPara.strFtName;
            FtName2 = m_structPara.strFtName2;
            return;
        }

        public override bool Verify()
        {
            if (m_structPara.strFtName == "" || m_structPara.strFtName2 == "" || m_structPara.strWhereClause == "")
            {
                //XtraMessageBox.Show("检查目标或检查表达式不存在，无法执行检查！");
                string strLog = "当前工作数据库的检查目标或检查表达式不存在，无法执行检查!";
                SendMessage(enumMessageType.VerifyError, strLog);
                return false;
            }
            if(base.m_QueryWorkspace is IFeatureWorkspace)
            {

                m_ipFtWS= (IFeatureWorkspace)base.m_QueryWorkspace;

                if(!(m_ipFtWS as IWorkspace2).get_NameExists(esriDatasetType.esriDTTable,m_structPara.strFtName))
                {
                    //Hy.Check.Rule.Helper.LogAPI.CheckLog.AppendErrLogs(ex.ToString());
                    string strLog = "当前工作数据库的检查目标图层" + FtName1 + "不存在，无法执行检查!";
                    SendMessage(enumMessageType.VerifyError, strLog);
                    return false;
                }

                if (!m_structPara.strAlias.Equals("行政区一致性") && 
                    !m_structPara.strAlias.Equals("图斑地类一致性"))
                {
                    if(!(m_ipFtWS as IWorkspace2).get_NameExists(esriDatasetType.esriDTTable,m_structPara.strFtName))
                    {
                        //Hy.Check.Rule.Helper.LogAPI.CheckLog.AppendErrLogs(ex.ToString());
                       string strLog = "当前工作数据库的检查目标图层" + FtName2 + "不存在，无法执行检查!";
                        SendMessage(enumMessageType.VerifyError, strLog);
                        return false;
                    }
                }
            }
            else
            {
                string strLog = "当前工作数据库的工作空间" + base.m_QueryWorkspace.PathName + "不存在，无法执行检查!";
                SendMessage(enumMessageType.VerifyError, strLog);
                return false;
            }
        
            // 经测试，数据库中保存的是图层本身
            //FtName1=base.GetLayerName(m_structPara.strFtName);
            //FtName2=base.GetLayerName(m_structPara.strFtName2);

            return true;
        }

        public override bool Check(ref List<Error> checkResult)
        {
            //System.Diagnostics.Stopwatch MyWatch = new System.Diagnostics.Stopwatch();
            //MyWatch.Start();
            System.Data.OleDb.OleDbDataReader reader = null;
            try
            {

                DataTable pRecordset = new DataTable();

                if (m_structPara.strScript == "行政区层中行政区代码与行政区名称不匹配")
                {
                    string strTemp = "select objectid,BSM,xzqdm, xzqmc,qsdwmc from(select a2.objectid,a2.BSM,a2.xzqdm,a2.xzqmc, a1.qsdwdm,a1.qsdwmc from qsdmb a1 inner join xzq a2 on left(a1.qsdwdm,12)=left(a2.xzqdm,12) where mid(qsdwdm,12,1)<>'0'  and right(qsdwdm,7)='0000000') where Trim(a2.xzqmc)<>Trim(a1.qsdwmc)";
                    pRecordset = AdoDbHelper.GetDataTable(base.m_QueryConnection, strTemp);
                    if (pRecordset==null || pRecordset.Rows.Count==0)
                    {
                        string strLog = "当前规则的SQL查询语句设置有误，无法执行多表关系检查!";
                        SendMessage(enumMessageType.RuleError, strLog);
                        return false;
                    }
                }
                else if (m_structPara.strScript.Contains("权属代码表不一致") ||
                           m_structPara.strScript.Contains("权属单位代码表不一致"))
                {
                    reader = AdoDbHelper.GetQueryReader(base.m_QueryConnection, m_structPara.strClause) as OleDbDataReader;
                    if (reader == null)
                    {
                        string strLog = "当前规则的SQL查询语句设置有误，无法执行多表关系检查!";
                        SendMessage(enumMessageType.RuleError, strLog);
                        return false;
                    }
                    pRecordset.Load(reader);
                }
                else
                {
                    pRecordset = AdoDbHelper.GetDataTable(base.m_QueryConnection, m_structPara.strClause);
                    if (pRecordset == null || pRecordset.Rows.Count==0)
                    {
                        string strLog = "当前规则的SQL查询语句设置有误，无法执行多表关系检查!";
                        SendMessage(enumMessageType.RuleError, strLog);
                        return false;
                    }
                }

                string strSql = "";
                bool bTable = false;

                if (FtName1.Contains("表") || FtName2.Contains("表"))
                {
                    if (m_structPara.strFtName2.Equals("qsdmb", StringComparison.OrdinalIgnoreCase))
                    {
                        strSql = "update LR_ResultEntryRule set TargetFeatClass1= '" + FtName1 + "',TargetFeatClass2='" +
                             FtName2 + "|' where RuleInstID='" + base.m_InstanceID + "'";
                    }
                    else
                    {
                        strSql = "update LR_ResultEntryRule set TargetFeatClass1= '" + COMMONCONST.TABLENAME +
                             "',TargetFeatClass2='|' where RuleInstID='" + base.m_InstanceID + "'";
                        bTable = true;
                    }
                }
                else
                {
                    strSql = "update LR_ResultEntryRule set TargetFeatClass1= '" + FtName1 + "',TargetFeatClass2='" +
                             FtName2 + "|' where RuleInstID='" + base.m_InstanceID + "'";
                }
                AdoDbHelper.ExecuteSql(m_ResultConnection, strSql);

                //获取需要特殊处理的规则别名
                Hashtable hashtable = GetEnumHash();
                if (hashtable.Contains(m_structPara.strAlias))
                {
                    if (m_structPara.strAlias.Equals("省级上报田坎系数与上报数据中田坎系数是否一致检查"))
                    {
                        checkResult = GetTKXSResult(pRecordset);
                    }
                    else
                    {
                        checkResult = GetAttrResult(pRecordset);
                    }
                }
                else
                {
                    checkResult = GetResult(pRecordset, bTable);
                }
                if (pRecordset != null)
                {
                    pRecordset.Dispose();
                    return true;
                }
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.RuleError, string.Format("意外失败，信息：{0}",ex.Message));
                SendMessage(enumMessageType.Exception, ex.ToString());
                return false;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                    GC.Collect();
                }
            }
            //MyWatch.Stop();
            //System.Windows.Forms.MessageBox.Show("时间:" + MyWatch.ElapsedMilliseconds.ToString() + "毫秒");
            return true;
        }
    }

    //多字段质检参数结构体
    public struct MULTIFIELDSPARA
    {
        public string strName; //质检规则类别
        public string strAlias; //查询规则别名
        public string strScript; //描述
        public string strFtName; //待检图层
        public string strFtName2; //待检图层2
        public string strWhereClause; //查询条件
        public List<string> strMLyrList; //图层列表
        public string strClause;
    } ;
    /// <summary>
    /// 字段映射
    /// </summary>
    public struct FIELDMAP
    {
        public string strAlias; //字段别名
        public string strName; //字段名
        public int fieldType; //字段类型
        public int fieldLength; //字段长度
    } ;
}