using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Hy.Check.Define;

using ESRI.ArcGIS.AnalysisTools;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.esriSystem;

using Hy.Check.Rule.Helper;
using Hy.Check.Utility;


namespace Hy.Check.Rule
{
    /// <summary>
    /// 图属一致性检查
    /// </summary>
    public class RuleGraphAttribute : BaseRule
    {
        

        private RuleExpression.LRGraphAttributeCollectPara m_pPara;

        private Geoprocessor m_gp = null;
        private string m_srcfcname;     
        private double m_UnitScale = 1;
       

        

        public RuleGraphAttribute()
        {
            if (COMMONCONST.IsGeographicCoordinateSystem)
                this.m_UnitScale = 0.000009;
        }


        public override string Name
        {
            get { return "图属一致性检查"; }
        }

        public override IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {          
            BinaryReader pParameter=new BinaryReader(new MemoryStream(objParamters));
            m_pPara = new RuleExpression.LRGraphAttributeCollectPara();

            pParameter.BaseStream.Position = 0;

            int nCount1 = pParameter.ReadInt32();

            int nStrSize = nCount1 - sizeof (int);

            Byte[] bb = new byte[nStrSize];
            pParameter.Read(bb, 0, nStrSize);
            string para_str = Encoding.Default.GetString(bb);

            para_str.Trim();
            string[] strResult = para_str.Split('|');

            m_pPara.arrayGeographyObjectField = new List<string>();
            m_pPara.arrayGraphSpeckleField = new List<string>();
            for (int k = 0; k < strResult.Length; k++)
            {
                switch (k)
                {
                    case 0:
                        m_pPara.strAlias = strResult[k];
                        break;
                    case 1:
                        m_pPara.strRemark = strResult[k];
                        break;
                    case 2:
                        m_pPara.strGeographyObject = strResult[k];
                        break;
                    case 3:
                        m_pPara.strGraphSpeckle = strResult[k];
                        break;
                    case 4:
                        m_pPara.strStdName = strResult[k];
                        break;
                    case 5:
                        m_pPara.strCheckKind = strResult[k];
                        break;
                    default:
                        {
                            int index = k % 2;
                            if (index != 0)
                            {
                                m_pPara.arrayGraphSpeckleField.Add(strResult[k]);
                            }
                            else
                            {
                                m_pPara.arrayGeographyObjectField.Add(strResult[k]);
                            }
                            break;
                        }
                }
            }
        }

        public override bool Verify()
        {
            //先取得要进行空间关系查询的ILayer
            IFeatureWorkspace ipFtWS = (IFeatureWorkspace)m_BaseWorkspace;

            int standarID = SysDbHelper.GetStandardIDBySchemaID(this.m_SchemaID);

            StandardLayer lyrSource = LayerReader.GetLayerByAliasName(m_pPara.strGeographyObject, standarID);
            if (lyrSource == null)
            {
                SendMessage(enumMessageType.VerifyError, string.Format("当前方案所在的标准中批不到名为“{0}”的图层", m_pPara.strGeographyObject));
                return false;
            }

            strSrcLayer = lyrSource.Name;
            if (m_pPara.arrayGraphSpeckleField.Count > 0)
            {
                strSrcFiled = FieldReader.GetNameByAliasName(m_pPara.arrayGraphSpeckleField[0], lyrSource.ID);
                if (string.IsNullOrEmpty(strSrcFiled))
                {
                    SendMessage(enumMessageType.VerifyError, string.Format("当前方案所在的标准中批不到名为“{0}”的图层", m_pPara.arrayGraphSpeckleField[0]));
                    return false;
                }
            }

            StandardLayer lyrRel = LayerReader.GetLayerByAliasName(m_pPara.strGraphSpeckle, standarID);
            strRelLayer = lyrRel.Name;
            if (m_pPara.arrayGeographyObjectField.Count > 0)
            {
                strRelField = FieldReader.GetNameByAliasName(m_pPara.arrayGeographyObjectField[0], lyrRel.ID);
                if (string.IsNullOrEmpty(strRelField))
                {
                    SendMessage(enumMessageType.VerifyError, string.Format("当前方案所在的标准中批不到名为“{0}”的图层", m_pPara.arrayGeographyObjectField[0]));
                    return false;
                }
            }
            //得到目标图层和关系图层的featureclass
            IFeatureClass pSrcFeatClass = null;
            IFeatureClass pRelFeatClass = null;
            IWorkspace TempWorkspace = null;

            IWorkspace2 wsTest = ipFtWS as IWorkspace2;
            // 原始数据准备
            // 打开“源”
            try
            {
                pSrcFeatClass = ipFtWS.OpenFeatureClass(strSrcLayer);
            }
            catch
            {
                SendMessage(enumMessageType.VerifyError, "当前工作数据库的关系图层" + strSrcLayer + "不存在,无法执行检查!");
                return false;
            }
            m_srcfcname = strSrcLayer;
            if (pSrcFeatClass.FeatureCount(null) == 0)
            {
                Marshal.ReleaseComObject(pSrcFeatClass);
                SendMessage(enumMessageType.VerifyError, "当前工作数据库的关系图层" + strSrcLayer + "为空,无法执行检查!");
                return false;
            }
            if (pSrcFeatClass != null)
            {
                Marshal.ReleaseComObject(pSrcFeatClass);
                pSrcFeatClass = null;
            }
            // 打开“目标”
            try
            {
                pRelFeatClass = ipFtWS.OpenFeatureClass(strRelLayer);
            }
            catch
            {
                SendMessage(enumMessageType.VerifyError, "当前工作数据库的目标图层" + strRelLayer + "不存在,无法执行检查!");
                return false;
            }

            if (pRelFeatClass.FeatureCount(null) == 0)
            {
                Marshal.ReleaseComObject(pRelFeatClass);
                SendMessage(enumMessageType.VerifyError, "当前工作数据库的关系图层" + strSrcLayer + "为空,无法执行检查!");
                return false;
            }

            if (pRelFeatClass != null)
            {
                Marshal.ReleaseComObject(pRelFeatClass);
                pRelFeatClass = null;
            }

            return true;
        }

        private IWorkspace TempWorkspace = null;
        private string strRelLayer = null;
        private string strSrcLayer = null;
        private string strRelField = null;  //关联图层的字段名
        private string strSrcFiled = null;  //源图层的字段名
        public override bool Check(ref List<Error> checkResult)
        {
            IFeatureClass pResultFc = null;
            IFeatureCursor pResultCur = null;
            try{
                string path = string.Format("{0}\\{1}", System.IO.Path.GetDirectoryName(m_QueryWorkspace.PathName),COMMONCONST.DB_Name_Temp);
                //创建临时操作tempdist.mdb,防止把所有结果库数据存储到query库，引起query库超限
                if (File.Exists(path))
                {
                    Common.Utility.Esri.AEAccessFactory.OpenPGDB(ref TempWorkspace, path);
                }
                else
                {
                    Common.Utility.Esri.AEAccessFactory.CreatePGDB(System.IO.Path.GetDirectoryName(m_QueryWorkspace.PathName), COMMONCONST.DB_Name_Temp, ref TempWorkspace);
                }
                if(TempWorkspace==null)
                {
                    SendMessage(enumMessageType.RuleError,"创建临时操作数据库失败！无法执行检查!");
                    return false;
                }
                // 生成中间结果图层
                // 线物不能穿越地类图斑的检查使用Identity进行，其余都使用Intersect
                // 仅适用于二调质检
                string Resultlayer = strSrcLayer + "_Intersect";
                IGPProcess gpProcess = null;
                string strKey = strSrcLayer.ToUpper();
                string inputtables = string.Format(@"""{0}\{1}\{2}"";""{0}\{1}\{3}""",m_BaseWorkspace.PathName, "Dataset", strSrcLayer, strRelLayer);
                Intersect spIntersect = new Intersect();
                spIntersect.in_features = inputtables;
                spIntersect.cluster_tolerance = "0.001 Meters";
                spIntersect.join_attributes = "ALL";
                spIntersect.out_feature_class = TempWorkspace.PathName + "\\" + Resultlayer;
                spIntersect.output_type = "INPUT";

                gpProcess = spIntersect;
                m_gp = new Geoprocessor();
                Execute(gpProcess);

                // 等待gp
                int counter = 0;
                while (counter++ < 100)
                {
                    if ((TempWorkspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, Resultlayer))
                        break;

                    System.Threading.Thread.Sleep(100);
                }
                if (m_gp != null)
                {
                    gpProcess = null;
                    m_gp = null;
                    GC.Collect();
                }
                // 从结果图层中取出错误
                pResultFc = ((IFeatureWorkspace)TempWorkspace).OpenFeatureClass(Resultlayer);
                IQueryFilter pQry = new QueryFilterClass();

                switch (strKey)
                {
                    case "LXDW":
                        pQry.WhereClause = "ZLTBBH<>TBBH";
                        break;

                    case "XZDW":    //线物不能穿越地类图斑、线物要素的扣除检查                    
                        if (m_pPara.strAlias.IndexOf("线物不能穿越地类图斑") > -1)
                        {
                            string strClause = " and Avg(len_dist)-sum(shape_length)< " + (1 * this.m_UnitScale).ToString("N7");
                            TempWorkspace.ExecuteSQL(string.Format("delete from XZDW_Intersect where  fid_XZDW in (SELECT fid_xzdw FROM xzdw_Intersect GROUP BY fid_xzdw,fid_dltb having count(0)>1 {0}) and fid_dltb in (SELECT fid_dltb FROM xzdw_Intersect GROUP BY fid_xzdw,fid_dltb having count(0)>1 {0})",strClause));

                            pQry.SubFields = " Distinct BSM,FID_XZDW,BSM_1";
                            pQry.WhereClause = "len_dist-shape_length>" + (1 * this.m_UnitScale).ToString("N7") + " and shape_length>" + (0.2 * this.m_UnitScale).ToString("N7");
//                            pQry.WhereClause = "len_dist-shape_length>" + (1 * this.m_UnitScale).ToString("N7") + " and shape_length>" + (0.2 * this.m_UnitScale).ToString("N7") + @" and
//                                                    fid_xzdw in
//                                                    (
//                                                    select a.fid_xzdw from
//                                                    (select fid_xzdw from xzdw_Intersect group by fid_xzdw having count(0)>1) as a,
//                                                    (SELECT fid_xzdw FROM xzdw_Intersect GROUP BY fid_xzdw,fid_dltb having count(0)=1) as b
//                                                    where a.fid_xzd9w=b.fid_xzdw
//                                                    )";
                        }
                        else
                        {
                            if (m_pPara.strAlias.IndexOf("线物要素的扣除检查") > -1)
                                pQry.WhereClause = getkcblsql();
                        }
                        break;

                    default:
                        pQry.WhereClause = "left(ZLDWDM,12)<>XZQDM and shape_Area>" + (400 * this.m_UnitScale).ToString("N7");
                        break;
                }
                pResultCur = pResultFc.Search(pQry, true);
                checkResult = GetResult(pResultCur as ICursor);

                // 更新状态
                string _strSql = "update LR_ResultEntryRule set TargetFeatClass1= '" + m_pPara.strGeographyObject +
                "',TargetFeatClass2='" + m_pPara.strGraphSpeckle +
                "|' where RuleInstID='" + this.m_InstanceID + "'";

                Common.Utility.Data.AdoDbHelper.ExecuteSql(this.m_ResultConnection, _strSql);
                // 释放资源，删除中间结果
                if (pResultCur != null)
                {
                    Marshal.ReleaseComObject(pResultCur);
                    pResultCur = null;
                }
                ((IDataset)pResultFc).Delete();

                return true;
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                return false;
            }
            finally
            {
                if (m_gp != null)
                {
                    m_gp = null;
                }
                // 释放资源，删除中间结果
                if (pResultCur != null)
                {
                    Marshal.ReleaseComObject(pResultCur);
                    pResultCur = null;
                }
                if (pResultFc != null)
                {
                    Marshal.ReleaseComObject(pResultFc);
                    pResultFc = null;
                }
                //压缩临时数据库
                IDatabaseCompact databaseCompact = TempWorkspace as IDatabaseCompact;
                if (databaseCompact != null)
                {
                    if (databaseCompact.CanCompact())
                        databaseCompact.Compact();
                }
                if (TempWorkspace != null)
                {
                    Marshal.ReleaseComObject(TempWorkspace);
                    TempWorkspace = null;
                }
                GC.Collect();
            }
        }

        /// <summary>
        /// Getkcblsqls this instance.
        /// </summary>
        /// <returns></returns>
        private string getkcblsql()
        {
            string sql = "SELECT bsm FROM XZDW_Intersect where kcbl=0.5 and (LEN_DIST-SHAPE_LENGTH)<" + (1 * this.m_UnitScale).ToString("N7") + " group by bsm having count(bsm)=1";
            sql += " union SELECT bsm FROM XZDW_Intersect where (shape_length  in (select shape_length from XZDW_Intersect group by bsm,shape_length having count(*)>1)) and kcbl=1";

            DataTable dt = Common.Utility.Data.AdoDbHelper.GetDataTable(this.m_QueryConnection,sql);
            if (dt.Rows.Count < 1) return "";
            string oids = "(";
            foreach (DataRow dr in dt.Rows)
            {
                oids += dr[0].ToString() + ",";
            }
            oids = oids.Remove(oids.Length - 1);
            oids += ")";
            oids="bsm in "+oids;
            if (dt!=null)
            {
                dt.Dispose();
                dt = null;
            }
            return oids;
        }

        /// <summary>
        /// 执行GP工具
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        private object Execute(IGPProcess process)
        {
            m_gp.OverwriteOutput = true;
            m_gp.TemporaryMapLayers = false;

            object sev = null;
            object obj = null;
            try
            {
                obj = m_gp.Execute(process, null);
                object subObj = null;
                string msg = m_gp.GetMessages(ref subObj);
                SendMessage(enumMessageType.OperationalLog, msg);
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                string msg = m_gp.GetMessage(2);
            }
            finally
            {
                if(obj!=null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                }
            }
            return obj;
        }

        private List<Error> GetResult(ICursor pCursor)
        {
            IRow ipRow;
            ipRow = pCursor.NextRow();
            if (ipRow == null)
            {
                return null;
            }

            int fidindex = ipRow.Fields.FindField("FID_"+m_srcfcname);
            int bsmindex = ipRow.Fields.FindField("BSM");
            int bsm2index = ipRow.Fields.FindField("BSM_1");
            int zldwindex = ipRow.Fields.FindField(m_pPara.arrayGeographyObjectField[0]);

            List<Error> pResAttr = new List<Error>();
            while (ipRow != null)
            {
                // 添家结果记录
                Error pResInfo = new Error();
                pResInfo.DefectLevel = this.m_DefectLevel;
                pResInfo.RuleID = this.InstanceID;

                string fid = ipRow.get_Value(fidindex).ToString();
                pResInfo.OID = Convert.ToInt32(fid);//ipRow.OID;

                string bsm="";
                string xzqdm = "";
                if (bsmindex>0 )
                {
                    bsm = ipRow.get_Value(bsmindex).ToString();
                }
                if (zldwindex>0)
                {
                    xzqdm = ipRow.get_Value(zldwindex).ToString();
                }

                pResInfo.BSM =bsm;

                // 目标图层
                pResInfo.LayerName = m_pPara.strGeographyObject;
                pResInfo.ReferLayerName = m_pPara.strGraphSpeckle;

                // 错误信息
                string strErrMsg="";
                if (m_srcfcname.Equals("XZDW",StringComparison.OrdinalIgnoreCase))
                {
                    if (bsm2index > 0)
                    {
                        string bsm2 = ipRow.get_Value(bsm2index).ToString();
                        strErrMsg = string.Format("标识码为{0}的线状地物穿越了标识码为{1}的地类图斑", pResInfo.BSM, bsm2);
                    }
                }
                else
                {
                    strErrMsg = string.Format("标识码为{1}的{0}，没有坐落在{2}为{3}的{4}内", pResInfo.LayerName, pResInfo.BSM,  m_pPara.arrayGeographyObjectField[0], xzqdm, m_pPara.strGraphSpeckle);
                
                }

                pResInfo.Description = strErrMsg;

                pResAttr.Add(pResInfo);

                ipRow = pCursor.NextRow();
            }

            return pResAttr;
        }

    }
}