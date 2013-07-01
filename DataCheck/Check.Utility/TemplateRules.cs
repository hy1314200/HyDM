using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Collections;

using Check.Define;
using Common.Utility.Data;

namespace Check.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class ClassifyRule
    {
        public string ruleName;

        public int  ruleBm;

        public int SubRulesCount;

        public List<ClassifyRule> SubRules;
    }

    /// <summary>
    /// 返回三个结果 1、规则的1、2、3级树列表，用于加载显示 2、所有的规则对象列表 3、所有的规则datable
    /// </summary>
    public class TemplateRules
    {
        private List<SchemaRuleEx> m_CurrentSchemaRules = null;

        /// <summary>
        /// Gets the current schema rules.
        /// </summary>
        /// <value>The current schema rules.</value>
        public List<SchemaRuleEx> CurrentSchemaRules
        {
            get{return m_CurrentSchemaRules;}
        }

        /// <summary>
        /// Gets or sets the current schema rules dt.
        /// </summary>
        /// <value>The current schema rules dt.</value>
        public DataTable CurrentSchemaRulesDt
        {
            get;
            set;
        }


        private List<ClassifyRule> m_ClassifyLevelRules = null;

        /// <summary>
        /// Gets the classify rules.
        /// </summary>
        /// <value>The classify rules.</value>
        public List<ClassifyRule> ClassifyRules
        {
            get { return m_ClassifyLevelRules; }
        }

        private string m_strSchemaId=string.Empty;
        private DataTable m_RulesParaDt = null;
        private DataTable m_ruleDt = null;
        private DataTable m_RuleClassDt = null;
        private Hashtable m_EvaWeightHt = null;
        private Hashtable m_TopoWeightHt = null;

        private List<ClassifyRule> secondLevelRules = new List<ClassifyRule>();
        private List<ClassifyRule> thirdLevelRules = new List<ClassifyRule>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateRules"/> class.
        /// </summary>
        /// <param name="schemaId">The schema id.</param>
        public TemplateRules(string schemaId)
        {
            m_strSchemaId=schemaId;
            m_RulesParaDt = SysDbHelper.GetSchemaRulesPara(m_strSchemaId);
            m_EvaWeightHt = SysDbHelper.GetEvaWeightTable(schemaId, ref m_TopoWeightHt);
            m_ruleDt = SysDbHelper.GetSchemaTemplateRules(m_strSchemaId);
            m_RuleClassDt = SysDbHelper.GetAllModelRules();
            init();
        }
        
        private void init()
        {
            m_CurrentSchemaRules = new List<SchemaRuleEx>();
            try
            {
                GetRuleClassify();

                if (m_ruleDt == null || m_ruleDt.Rows.Count == 0)
                {
                    return;
                }

                SchemaRuleEx ruleEx = null;
                //实例化ruleDt
                GenerateRulesTable();

                foreach (DataRow dr in m_ruleDt.Rows)
                {
                    try
                    {
                        ruleEx = new SchemaRuleEx();
                        DataRow row = CurrentSchemaRulesDt.NewRow();

                        string chkTypeName = dr["ChkTypeName"].ToString();
                        string strTemplateId = dr["TempInstID"].ToString();
                        ruleEx.SchemaId = m_strSchemaId;

                        ruleEx.bIsCatalogTopo = Convert.ToBoolean(dr["IsCatalogTopo"]);
                        ruleEx.ChkTypeName = chkTypeName;
                        //ruleEx.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                        ruleEx.RuleTmpletType = (RuleTempletType)dr["TempletType"];
                        //ruleEx.strPerson = dr["Person"].ToString();
                        ruleEx.FeaClassName = dr["SourceLayer"].ToString();
                        ruleEx.strLogicalDesc = dr["LogicalDesc"].ToString();
                        ruleEx.strRemark = dr["Remark"].ToString();
                        ruleEx.TemplateName = dr["TempletName"].ToString();

                        ruleEx.ThridClassificationBM = GetParentId(thirdLevelRules, chkTypeName) == -1 ? -1 : GetParentId(thirdLevelRules, chkTypeName);
                        if (ruleEx.ThridClassificationBM > -1)
                        {
                            int ibm = Convert.ToInt32(ruleEx.ThridClassificationBM.ToString().Substring(0, 2));
                            ruleEx.SecondeClassificationBM = ibm;
                            string secondeBm = ruleEx.SecondeClassificationBM.ToString();
                            ibm = Convert.ToInt32(ruleEx.ThridClassificationBM.ToString().Substring(0, 1));
                            ruleEx.FirstClassificationBM = ibm;
                        }
                        else
                        {
                            ruleEx.SecondeClassificationBM = GetParentId(secondLevelRules, chkTypeName) == -1 ? -1 : GetParentId(secondLevelRules, chkTypeName);
                            int ibm =(ruleEx.SecondeClassificationBM==-1)?-1:Convert.ToInt32(ruleEx.SecondeClassificationBM.ToString().Substring(0, 1));
                            ruleEx.FirstClassificationBM = ibm;
                        }
                        ruleEx.TempInstID = strTemplateId;
                        string str = strTemplateId.Replace("M", "R");
                        ruleEx.Weights = GetRulesWeights(str);

                        //**************************************
                        row[0] = strTemplateId;
                        row[1] = chkTypeName;
                        //**************************************
                        ruleEx.arrayRuleParas = GetTemplateRuleParas(strTemplateId, row);
                        ruleEx.ruleDllInfo = GetRuleDllInfo(ruleEx.arrayRuleParas[0].nSerialID);
                        m_CurrentSchemaRules.Add(ruleEx);
                        row[2] = dr["TempletName"].ToString();
                        CurrentSchemaRulesDt.Rows.Add(row);
                    }
                    catch (Exception exp)
                    {
                        Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());
                        continue;
                    }
                }
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

            }
            finally
            {
                if (m_ruleDt != null)
                {
                    m_ruleDt.Dispose();
                    m_ruleDt = null;
                }
            }
        }

        private bool GetRuleClassify()
        {
            m_ClassifyLevelRules = new List<ClassifyRule>();
            DataTable result = null;
            DataTable yjdatatable = null;
            try
            {
                result = SysDbHelper.GetRulesClassifyList(m_strSchemaId);

                if (result == null || result.Rows.Count == 0)
                {
                    return false;
                }
                yjdatatable = AdoDbHelper.GetDataTable(SysDbHelper.GetSysDbConnection(), string.Format("select yjmlmc,yjmlbm from LR_CheckTypeModel where SchemaId='{0}' group by yjmlmc,yjmlbm order by yjmlbm ", m_strSchemaId));

                foreach (DataRow dr in yjdatatable.Rows)
                {
                    int parseBm = int.Parse(dr["YJMLBM"].ToString());
                    ClassifyRule rule = InitRuleLevel(dr["YJMLMC"].ToString(), parseBm);
                    int rulesCount = 0;
                    rule.SubRules = GetChildRules(parseBm.ToString(), "EJMLMC", "EJMLBM", 1, result, out rulesCount);
                    rule.SubRulesCount = rulesCount;//CalculateRulesCount(rule.SubRules);
                    m_ClassifyLevelRules.Add(rule);
                }
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

            }
            finally
            {
                if (result != null)
                {
                    result.Dispose();
                    result = null;
                }
                if (yjdatatable != null)
                {
                    yjdatatable.Dispose();
                    yjdatatable = null;
                }
            }

            return true;
        }

        private List<ClassifyRule> GetChildRules(string parentBm,string childMC,string childBm,int length,DataTable result,out int childRulesCount)
        {
            List<ClassifyRule> classifyLevelRules = new List<ClassifyRule>();
            DataRow[] drs = result.Select(string.Format("substring({0},1,{1})='{2}'", childBm, length, parentBm));
            int count = 0;
            int temp = 0;
            int bm = -1;
            string name="";
            int index = 0;
            foreach (DataRow dr in drs)
            {
                if (int.Parse(dr[childBm].ToString()) != bm && 
                    name !=dr[childMC].ToString())
                {
                    ClassifyRule classifyRule = new ClassifyRule();
                    classifyRule = InitRuleLevel(dr[childMC].ToString(), int.Parse(dr[childBm].ToString()));

                    //classifyRule.SubRulesCount = GetRulesCount(dr[childMC].ToString());
                    classifyRule.SubRules = GetChildRules(dr[childBm].ToString(), "SJMLMC", "SJMLBM", 2, result, out temp);
                    if (classifyRule.SubRulesCount > 0 && temp == 0)
                    {
                        count += classifyRule.SubRulesCount;
                    }

                    if (temp> 0)
                    {
                        classifyRule.SubRulesCount = temp; 
                        count+= classifyRule.SubRulesCount;
                    }

                    classifyLevelRules.Add(classifyRule);

                    bm = int.Parse(drs[index][childBm].ToString());
                    name = drs[index][childMC].ToString();
                    ++index;
                }
            }
            if (classifyLevelRules.Count > 0)
            {
                if (childMC == "SJMLMC")
                {
                    thirdLevelRules.AddRange(classifyLevelRules);
                }
                else
                {
                    secondLevelRules.AddRange(classifyLevelRules);
                }
            }
            childRulesCount = count;
            return classifyLevelRules;
        }

        public int CalculateRulesCount(List<ClassifyRule> rules)
        {
            int count = 0;
            foreach (ClassifyRule rule in rules)
            {
                count += rule.SubRulesCount;
            }
            return count;
        }

        private ClassifyRule InitRuleLevel(string name, int bm)
        {
            ClassifyRule rlev = new ClassifyRule();
            rlev.ruleBm = bm;
            rlev.ruleName = name;
            rlev.SubRulesCount = GetRulesCount(name);
            return rlev;
        }

        private int GetParentId(List<ClassifyRule> levels,int bm,int strsublength)
        {
            ClassifyRule rule = levels.Find(delegate(ClassifyRule T) { return T.ruleBm.ToString().Substring(0,strsublength) == bm.ToString(); });
            if (rule == null)
            {
                return -1;
            }
            return rule.ruleBm;
        }

        private int GetParentId(List<ClassifyRule> levels, string name)
        {
            ClassifyRule rule = levels.Find(delegate(ClassifyRule T) { return T.ruleName == name; });
            if (rule == null)
            {
                return -1;
            }
            return rule.ruleBm;
        }

        private List<TemplateRuleParas> GetTemplateRuleParas(string tempInstID,DataRow row)
        {
            List<TemplateRuleParas> RuleParas=new List<TemplateRuleParas>();

            DataRow[] drs=m_RulesParaDt.Select(string.Format("TempInstID='{0}'",tempInstID));
            if(drs==null || drs.Length==0) return RuleParas;

            //BinaryReader binReader=null;
            //int length=0;
            foreach(DataRow dr in drs)
            {
                TemplateRuleParas para=new TemplateRuleParas();
                para.strName=dr["RuleName"].ToString();
                para.strGZBM=dr["GZBM"].ToString();
                para.strAlias=dr["RuleAlias"].ToString();
                para.nSerialID=int.Parse(dr["RuleSerialID"].ToString());
                para.strInstID=dr["RuleInstID"].ToString();
                //GetBinaryPara(dr["Parameters"],out binReader,out length);
                para.nParaLength = (dr["Parameters"] as Byte[]).Length;
                para.pParas = dr["Parameters"] as Byte[];
                RuleParas.Add(para);
                row["RuleName"] = dr["RuleAlias"].ToString();
            }

            return RuleParas;
        }

        private RuleDllInfo GetRuleDllInfo(int SerialID)
        {

            RuleDllInfo ruleInfo = null;

            DataRow[] drs = m_RuleClassDt.Select(string.Format("ID={0}", SerialID));

            if (drs == null || drs.Length == 0) return null;

            return RuleDllInfo.FromDataRow(drs[0]);
            //foreach (DataRow dr in drs)
            //{

                //ruleInfo = new RuleDllInfo();
                //ruleInfo.ID = int.Parse(dr["ID"].ToString());
                //ruleInfo.Name = dr["RuleName"].ToString();
                ////string strName = dr["RuleDllName"].ToString();
                ////string[] strDllAndName = strName.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                ////ruleClassInfo.DllName = strDllAndName[0];
                ////if (strDllAndName.Length > 1)
                ////    ruleClassInfo.ClassName = strDllAndName[1];

                //ruleInfo.DllName = dr["DllFile"] as string;
                //ruleInfo.ClassName = dr["ClassName"] as string;
                //ruleInfo.Description = dr["Remark"] as string;

            //}
            //return ruleInfo;
        }

        private bool GetBinaryPara(object obj,out BinaryReader binReader, out int length)
        {
            binReader=null;
            length=0;

            if(obj==DBNull.Value)
            {
                return false;
            }

            Byte[] bByte = (byte[]) obj;
            int nDataSize = bByte.Length;
            MemoryStream stream = new MemoryStream(bByte);
            binReader = new BinaryReader(stream);

            return true;
        }

        private int GetRulesCount(string ruleName)
        {
            int count = 0;
            DataRow[] drs=m_ruleDt.Select(string.Format("ChkTypeName='{0}'",ruleName));
            if(drs ==null || drs.Length==0) return count;
            count=drs.Length;
            return count;
        }

        private string GetRulesWeights(string RuleId)
        {
            string strWeights = "";
            if (m_EvaWeightHt == null)
                return strWeights;

            if (m_EvaWeightHt.Contains(RuleId))
            {
                strWeights = m_EvaWeightHt[RuleId].ToString();
                return strWeights;
            }
            if (this.m_TopoWeightHt.Contains(RuleId))
            {
                strWeights = m_TopoWeightHt[RuleId].ToString();
                return strWeights;
            }
            return strWeights;
        }

        private void GenerateRulesTable()
        {

            CurrentSchemaRulesDt = new DataTable();
            DataColumn dc = new DataColumn();
            dc.Caption = "规则编码";
            dc.ColumnName = "TempInstID";

            dc.DataType = typeof(System.String);

            CurrentSchemaRulesDt.Columns.Add(dc);

            dc = new DataColumn();
            dc.Caption = "检查内容";
            dc.ColumnName = "ChkTypeName";

            dc.DataType = typeof(System.String);

            CurrentSchemaRulesDt.Columns.Add(dc);

            dc = new DataColumn();
            dc.Caption = "规则名称";
            dc.ColumnName = "RuleName";

            dc.DataType = typeof(System.String);

            CurrentSchemaRulesDt.Columns.Add(dc);

            dc = new DataColumn();
            dc.Caption = "缺陷等级";
            dc.ColumnName = "ErrType";

            dc.DataType = typeof(System.String);

            CurrentSchemaRulesDt.Columns.Add(dc);
        }
    }
}