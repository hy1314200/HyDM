using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Check.Utility
{
   
    /// <summary>
    /// 统计项
    /// </summary>
    public class StateItem
    {
        private string checkType;

        /// <summary>
        /// 检查类型
        /// </summary>
        public string CheckType
        {
            get { return checkType; }
            set { checkType = value; }
        }

        private int light;

        /// <summary>
        /// 轻缺陷
        /// </summary>
        public int Light
        {
            get { return light; }
            set { light = value; }
        }

        private int weight;

        /// <summary>
        /// 重缺陷
        /// </summary>
        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        private int graveness;

        /// <summary>
        /// 严重缺陷
        /// </summary>
        public int Graveness
        {
            get { return graveness; }
            set { graveness = value; }
        }


        /// <summary>
        /// 总数
        /// </summary>
        public int MaxCount
        {
            get { return light + weight + graveness; }
        }
    }

    /// <summary>
    /// 统计评价
    /// </summary>
    public class StateAppraise
    {
        #region 内部成员

        private DataTable m_pTable = null;

        //private RuleRightWeight[] arrRuleRightWeight = null;
        private const double MARK_WEIGHT = 2;
        private const double MARK_LIGHT = 0.5;
        //private static int gravenessCoeff = 100; //严重缺陷分数
        //private static int weightCoeff = 10; //重缺陷分数
        //private static int weightNumber = 3; //重缺陷调整个数
        //private static int lightCoeff = 1; //轻缺陷分数
        //private static int lightNumber = 9; //轻缺陷调整个数

        private Hashtable hashRuleWeight = new Hashtable();
        private Hashtable hashArcGisRule = new Hashtable();

        private OleDbConnection _resultConn;

        #endregion

        public StateAppraise()
        {
        }

        public OleDbConnection ResultConnection
        {
            set
            {
                _resultConn = value;
                //OleDbConnection oleconn = CCheckApplication.m_CurrentModelTask.
                if (_resultConn != null)
                {
                    try
                    {

                        // 2012-07-12 张航宇
                        // 直接从结果库获取结果表记录，而关闭ResultTable属性 

                        this.ResultTable = Hy.Common.Utility.Data.AdoDbHelper.GetDataTable(_resultConn, @"SELECT
                            b.CheckType,b.RuleInstID,b.ArcGisRule ,a.TargetFeatClass1 as YSTC,
                            a.BSM as SourceBSM,'LR_ResAutoAttr' as SysTab,a.IsException from LR_ResAutoAttr as a, LR_ResultEntryRule as b where a.RuleInstID=b.RuleInstID

                            union all

                            SELECT
                            b.CheckType,b.RuleInstID,b.ArcGisRule ,a.YSTC as YSTC,
                            a.SourceBSM as SourceBSM,'LR_ResAutoTopo' as SysTab,a.IsException from LR_ResAutoTopo as a, LR_ResultEntryRule as b where a.RuleInstID=b.RuleInstID

                            union all

                            SELECT
                            b.CheckType,b.RuleInstID,b.ArcGisRule ,a.AttrTabName as YSTC,
                            '' as SourceBSM,'LR_ResIntField' as SysTab,a.IsException from LR_ResIntField as a, LR_ResultEntryRule as b where a.RuleInstID=b.RuleInstID

                            union all

                            SELECT
                            b.CheckType,b.RuleInstID,b.ArcGisRule ,a.ErrorLayerName as YSTC,
                            '' as SourceBSM,'LR_ResIntLayer' as SysTab,a.IsException from LR_ResIntLayer as a, LR_ResultEntryRule as b where a.RuleInstID=b.RuleInstID");


                        DataTable dt = null;
                        // = GT_CARTO.CommonAPI.ado_OpenTable() .ado_Execute(CommonAPI.Get_DBConnection(), sql);
                        string str = string.Format("select * from LR_ResultEntryRule where ArcGisRule<>''");
                        dt = Hy.Common.Utility.Data.AdoDbHelper.GetDataTable(_resultConn, str);
                        if (dt == null) return;
                        if (dt.Rows.Count < 1) return;
                        foreach (DataRow dr in dt.Rows)
                        {
                            string key = dr["ArcGisRule"].ToString();
                            if (key != "" && !hashArcGisRule.ContainsKey(key))
                                hashArcGisRule[key] = dr["RuleInstID"].ToString();
                        }
                    }
                    catch (Exception exp)
                    {
                        Hy.Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                        //GT_CONST.LogAPI.CheckLog.AppendErrLogs(ex.ToString());
                    }
                }
            }
        }

        // 2012-07-12 张航宇
        // 直接从结果库获取结果表记录，而关闭ResultTable属性
        private DataTable ResultTable
        {
            set
            {
                m_pTable = value;
                if (m_pTable != null)
                {
                    if (hashRuleWeight.Count < 1) GetRuleRightWeight();

                    //更新拓扑规则对应的RuleInstID
                    int RuleIDColumnIndex = m_pTable.Columns.IndexOf("RuleInstID");
                    int ArcGISRuleColumnIndex = m_pTable.Columns.IndexOf("ArcGISRule");

                    DataRow[] rows = m_pTable.Select("CheckType like '%拓扑关系%'");
                    //DataTable dt = new DataTable();
                    for (int i = 0; i < rows.Length; i++)
                    {
                        //string ruleid = rows[i][RuleIDColumnIndex].ToString();
                        string arcgisrule = rows[i][ArcGISRuleColumnIndex].ToString();
                        if (hashArcGisRule.Contains(arcgisrule)) //已存在
                        {
                            rows[i][RuleIDColumnIndex] = hashArcGisRule[arcgisrule];
                        }
                    }
                    m_pTable.AcceptChanges();
                }
            }
        }

        /// <summary>
        /// 获取所有的权重信息的哈希表
        /// </summary>
        /// <returns></returns>
        private void GetRuleRightWeight()
        {
            try
            {
                //DataTable dt = null; // = GT_CARTO.CommonAPI.ado_OpenTable() .ado_Execute(CommonAPI.Get_DBConnection(), sql);
                //GT_CARTO.CommonAPI.ado_OpenTable("LR_EvaHMWeight", ref dt, GT_CARTO.CommonAPI.Get_DBConnection());
                DataTable dt = Hy.Common.Utility.Data.AdoDbHelper.GetDataTable(SysDbHelper.GetSysDbConnection(), "select * from LR_EvaHMWeight");
                if (dt.Rows.Count < 1) return;
                foreach (DataRow dr in dt.Rows)
                {
                    string key = dr["ElementID"].ToString();
                    if (key != "" && !hashRuleWeight.ContainsKey(key))
                        hashRuleWeight[key] = dr["ErrType"].ToString();
                }
            }
            catch (Exception exp)
            {
                Hy.Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                //GT_CONST.LogAPI.CheckLog.AppendErrLogs(ex.ToString());
            }
        }

        /// <summary>
        /// 构建规则类型结果表
        /// </summary>
        public DataTable BuilderRuleStyleTable()
        {
            DataTable resultTable = new DataTable("ResultCollect");
            resultTable.Columns.Add("检查类型", Type.GetType("System.String"));
            resultTable.Columns.Add("严重缺陷", Type.GetType("System.Int32"));
            resultTable.Columns.Add("重缺陷", Type.GetType("System.Int32"));
            resultTable.Columns.Add("轻缺陷", Type.GetType("System.Int32"));
            resultTable.Columns.Add("错误合计", Type.GetType("System.Int32"));
            resultTable.Columns.Add("包含BSM", Type.GetType("System.String"));

            //不统计例外
            DataRow[] rows = m_pTable.Select("isException=0");
            DataRow dr = null;
            DataRow pRow = null;
            //foreach (DataRow dr in m_pTable.Rows)
            for (int i = 0; i < rows.Length;i++ )
            {
                
                try
                {
                    dr = rows[i];
                    string checktype = dr["CheckType"].ToString();
                    string ruleid = dr["RuleInstID"].ToString();

                    DataRow[] drw = resultTable.Select(string.Format("检查类型='{0}'", checktype));
                    if (drw.Length > 0)
                    {
                        pRow = drw[0];
                    }
                    else
                    {
                        pRow = resultTable.NewRow();
                        pRow["检查类型"] = checktype;
                        pRow["错误合计"] = 0;
                        pRow["严重缺陷"] = 0;
                        pRow["重缺陷"] = 0;
                        pRow["轻缺陷"] = 0;
                        pRow["包含BSM"] = "";
                        resultTable.Rows.Add(pRow);
                    }

                    if ((!hashRuleWeight.ContainsKey(ruleid)) &&
                        !dr["SysTab"].ToString().Equals("LR_ManualCheckError",StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    //对于标示码相同，规则ID也相同的，算同一个错误
                    // 2012-07-26 张航宇
                    // 同一个标识码在同一个规则下错误只计一次分
                    string srbsm = dr["SourceBSM"].ToString().Trim();
                    string tarbsm = pRow["包含BSM"].ToString().Trim();
                    if (!srbsm.Equals(""))
                    {
                        srbsm = ruleid + "_" + srbsm;
                        if (tarbsm.IndexOf(srbsm) == -1)
                            pRow["包含BSM"] = srbsm + "," + tarbsm;
                        else
                            continue;
                    }

                    string value = dr["SysTab"].ToString().Equals("LR_ManualCheckError") ? "轻缺陷" : hashRuleWeight[ruleid].ToString();
                    switch (value)
                    {
                        case "严重缺陷":
                            {
                                pRow["严重缺陷"] = (Int32)pRow["严重缺陷"] + 1;
                                pRow["错误合计"] = (Int32)pRow["错误合计"] + 1;
                            }
                            break;
                        case "重缺陷":
                            {
                                pRow["重缺陷"] = (Int32)pRow["重缺陷"] + 1;
                                pRow["错误合计"] = (Int32)pRow["错误合计"] + 1;
                            }
                            break;
                        case "轻缺陷":
                            {
                                pRow["轻缺陷"] = (Int32)pRow["轻缺陷"] + 1;
                                pRow["错误合计"] = (Int32)pRow["错误合计"] + 1;
                            }
                            break;
                    }
                }
                catch (Exception exp)
                {
                    Hy.Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                    //GT_CONST.LogAPI.CheckLog.AppendErrLogs(ex.ToString());
                }
            }
            resultTable.Columns.Remove("包含BSM");
            return resultTable;
        }

        /// <summary>
        /// 构建图层检查结果列表
        /// </summary>
        public DataTable BuilderLayerStyleTable()
        {
            DataTable resultTable = new DataTable("ResultCollect");
            resultTable.Columns.Add("图层", Type.GetType("System.String"));
            resultTable.Columns.Add("严重缺陷", Type.GetType("System.Int32"));
            resultTable.Columns.Add("重缺陷", Type.GetType("System.Int32"));
            resultTable.Columns.Add("轻缺陷", Type.GetType("System.Int32"));
            resultTable.Columns.Add("错误合计", Type.GetType("System.Int32"));
            //resultTable.Columns.Add("记录总数", Type.GetType("System.Int32"));
            resultTable.Columns.Add("包含BSM", Type.GetType("System.String"));

            //不统计例外
            DataRow[] rows = m_pTable.Select("isException=0");
            DataRow dr = null;
            //foreach (DataRow dr in m_pTable.Rows)
            for (int i = 0; i < rows.Length; i++)
            {
                try
                {
                    dr = rows[i];
                    string LayerName = dr["YSTC"].ToString();
                    if (LayerName.Equals("")) continue;

                    //速度太慢
                    //IFeatureLayer pFeatLayer = EngineAPI.GetLayerFromMapByName(m_Map, LayerName) as IFeatureLayer;
                    //if (pFeatLayer == null)
                    //{
                    //    continue;
                    //}

                    DataRow pRow = null;
                    DataRow[] drw = resultTable.Select(string.Format("图层='{0}'", LayerName));
                    if (drw.Length > 0)
                    {
                        pRow = drw[0];
                    }
                    else
                    {
                        pRow = resultTable.NewRow();
                        pRow["图层"] = LayerName;
                        pRow["错误合计"] = 0;
                        pRow["严重缺陷"] = 0;
                        pRow["重缺陷"] = 0;
                        pRow["轻缺陷"] = 0;

                        //IFeatureLayer pFeatLayer = EngineAPI.GetLayerFromMapByName(m_Map, LayerName) as IFeatureLayer;
                        //IFeatureClass pFeatClass = pFeatLayer.FeatureClass;
                        //pRow["记录总数"] = pFeatClass.FeatureCount(null);
                        resultTable.Rows.Add(pRow);
                    }

                    string ruleid = dr["RuleInstID"].ToString();
                    if (!hashRuleWeight.ContainsKey(ruleid) && !dr["SysTab"].ToString().Equals("LR_ManualCheckError")) continue;

                    //对于标示码相同，规则ID也相同的，算同一个错误
                    string srbsm = dr["SourceBSM"].ToString().Trim();
                    string tarbsm = pRow["包含BSM"].ToString().Trim();
                    if (!srbsm.Equals(""))
                    {
                        srbsm = ruleid + "_" + srbsm;
                        if (tarbsm.IndexOf(srbsm) == -1)
                            pRow["包含BSM"] = srbsm + "," + tarbsm;
                        else
                            continue;
                    }

                    string value = dr["SysTab"].ToString().Equals("LR_ManualCheckError") ? "轻缺陷" : hashRuleWeight[ruleid].ToString();
                    switch (value)
                    {
                        case "严重缺陷":
                            {
                                pRow["严重缺陷"] = (Int32) pRow["严重缺陷"] + 1;
                                pRow["错误合计"] = (Int32) pRow["错误合计"] + 1;
                            }
                            break;
                        case "重缺陷":
                            {
                                pRow["重缺陷"] = (Int32) pRow["重缺陷"] + 1;
                                pRow["错误合计"] = (Int32) pRow["错误合计"] + 1;
                            }
                            break;
                        case "轻缺陷":
                            {
                                pRow["轻缺陷"] = (Int32) pRow["轻缺陷"] + 1;
                                pRow["错误合计"] = (Int32) pRow["错误合计"] + 1;
                            }
                            break;
                    }
                }
                catch (Exception exp)
                {
                    Hy.Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                    //GT_CONST.LogAPI.CheckLog.AppendErrLogs(ex.ToString());
                }
            }
            resultTable.Columns.Remove("包含BSM");
            return resultTable;
        }

        /// <summary>
        /// 计算分数
        /// </summary>
        /// <param name="resultTb"></param>
        /// <returns></returns>
        public double GetResultMark(DataTable resultTb)
        {
            long zqx = 0;
            double totalMark = 100;
            foreach (DataRow dr in resultTb.Rows)
            {
                if (Convert.ToInt32(dr["严重缺陷"]) > 0) return 0;

                zqx += Convert.ToInt32(dr["重缺陷"]);
                if (zqx * MARK_WEIGHT > 100) return 0;
            }

            totalMark -= zqx * MARK_WEIGHT;
            try
            {
                //Hashtable hashLayerWeight = new Hashtable();
                //hashLayerWeight["行政区"] = 2/11*0.9;
                //hashLayerWeight["行政区界线"] = 3/11*1/7*0.9;
                //hashLayerWeight["地类图斑"] = 3/11*0.9;
                //hashLayerWeight["地类界线"] = 3/11*1/7*0.9;
                //hashLayerWeight["线状地物"] = 1/11*0.9;
                //hashLayerWeight["宗地"] = 2/11*0.9;
                //hashLayerWeight["界址线"] = 3/11*1/7*0.9;
                //hashLayerWeight["界址点"] = 3/11*1/7*0.9;
                //hashLayerWeight["坡度图"] = 3/11*1/7*0.9;
                //hashLayerWeight["基本农田保护片"] = 3/11*1/7*0.9;
                //hashLayerWeight["基本农田保护图斑"] = 3/11*1/7*0.9;

                DataTable pTb = BuilderRuleStyleTable();
                DataRow[] drRow = pTb.Select("严重缺陷=0");
                for (int i = 0; i < drRow.Length; i++)
                {
                    DataRow dr = drRow[i];
                    long lErrorcount = Convert.ToInt32(dr["轻缺陷"]);
                    //long lRecordCount = Convert.ToInt32(dr["记录总数"]);
                    if (lErrorcount > 0)
                    {
                        //if (hashLayerWeight.ContainsKey(dr["图层"].ToString()))
                        {
                            //totalMark -= Math.Ceiling((double) (lErrorcount/9))*0.1;
                            totalMark -= lErrorcount * MARK_LIGHT;
                        }
                    }
                }
                return totalMark > 0 ? totalMark : 0;
            }
            catch(Exception exp)
            {
                Hy.Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

            }
            return totalMark;
        }

        /// <summary>
        /// 构建缺陷等级表
        /// </summary>
        /// <param name="resultTable"></param>
        public void BuilderBugGradeTable(ref DataTable resultTable, StateItem[] arrItem)
        {
            resultTable = new DataTable("ResultCollect");
            DataColumn col = resultTable.Columns.Add();
            col.ColumnName = "缺陷等级";
            col.DataType = Type.GetType("System.String");

            col = resultTable.Columns.Add();
            col.ColumnName = "错误合计";
            col.DataType = Type.GetType("System.Int32");

            int LesserLight = 0;
            int Deadliness = 0;
            int Graveness = 0;
            int LesserWeight = 0;
            int Light = 0;
            int Weight = 0;
            for (int i = 0; i < arrItem.Length; i++)
            {
                Graveness += arrItem[i].Graveness;
                Light += arrItem[i].Light;
                Weight += arrItem[i].Weight;
            }

            DataRow dr = resultTable.NewRow();
            dr["缺陷等级"] = "轻";
            dr["错误合计"] = Light;
            resultTable.Rows.Add(dr);

            dr = resultTable.NewRow();
            dr["缺陷等级"] = "重";
            dr["错误合计"] = Weight;
            resultTable.Rows.Add(dr);

            dr = resultTable.NewRow();
            dr["缺陷等级"] = "严重";
            dr["错误合计"] = Graveness;
            resultTable.Rows.Add(dr);
        }
    }
}