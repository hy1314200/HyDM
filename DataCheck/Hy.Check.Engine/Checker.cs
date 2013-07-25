using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Hy.Check.Define;
using Hy.Check.Engine.Helper;
using Hy.Check.Utility;

namespace Hy.Check.Engine
{
    /// <summary>
    /// （将要）处理的规则发生变化句柄
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="dealingRule"></param>
    public delegate void DealingRuleChangedHandler(Checker sender, ICheckRule dealingRule);
    /// <summary>
    /// 验证完成句柄
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="availCount"></param>
    public delegate void VerifyedCompleteHandler(Checker sender, int availCount);
    /// <summary>
    /// 基本的检查事件句柄
    /// </summary>
    /// <param name="sender"></param>
    public delegate void CheckEventHandler(Checker sender);
    /// <summary>
    /// 规则检查完成事件句柄
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="checkedRule"></param>
    /// <param name="errorCount"></param>
    public delegate void RuleCheckedHandler(Checker sender,ICheckRule checkedRule, int errorCount);


    /// <summary>
    /// 质检执行类，负责执行质检
    /// @remark 
    /// 
    /// 质检是基于Base库，Query库，Topo库进行检查的，但Topo库是在本类中根据规则动态生成的
    /// 另外结果库也是在检查开始前创建的
    /// 
    /// 步骤：
    /// @li 1.根据规则列表反射出规则实例，将环境和参数设置给规则实例
    /// @li 2.验证规则实例（获得可执行的规则数目，失败者从规则列表删除）
    /// @li 3.对所有规则执行预处理（失败者从规则列表删除）
    /// @li 4.对所有规则执行检查（分别获得错误）
    /// @li 5.写入错误到结果库，释放资源
    /// </summary>
    public class Checker
    {
        private MessageHandler m_Messager;
        private IWorkspace m_BaseWorkspace;
        private IWorkspace m_QueryWorkspace;
        private IDbConnection m_QueryConnection;
        private string m_SchemaID;
        private IDbConnection m_ResultConnection;
        private IWorkspace m_TopoWorkspace;
        private double m_TopoTolerance;
        private string m_ResultPath;
        private string m_TopoDBPath;

        /// <summary>
        /// 设置消息处理器
        /// </summary>
        public MessageHandler Messager
        {
            set { m_Messager = value; }
        }
        /// <summary>
        /// 设置Base库
        /// </summary>
        public IWorkspace BaseWorkspace
        {
            set
            {
                m_BaseWorkspace = value;
            }
        }
        /// <summary>
        /// 设置Query库
        /// </summary>
        public IWorkspace QueryWorkspace
        {
            set
            {
                m_QueryWorkspace = value;
            }
        }
        /// <summary>
        /// 设置Query库的ADO连接
        /// </summary>
        public IDbConnection QueryConnection
        {
            set
            {
                m_QueryConnection = value;
            }
        }
        /// <summary>
        /// 设置方案ID
        /// </summary>
        public string SchemaID
        {
            set
            {
                this.m_SchemaID = value;
            }
        }
        //public IDbConnection ResultConnection
        //{
        //    set
        //    {
        //        m_ResultConnection = value;
        //    }
        //}
        //public  IWorkspace TopoWorkspace
        //{
        //    set
        //    {
        //        m_TopoWorkspace = value;
        //    }
        //}
        //public IFeatureDataset TopoDataset
        //{
        //    set
        //    {
        //        m_TopoDataset = value;
        //    }
        //}

        /// <summary>
        /// 设置Topo库路径，若检查中有拓扑规则，Topo库将在此路径下创建
        /// </summary>
        public string TopoDBPath
        {
            set
            {
                m_TopoDBPath = value;
            }
        }
        /// <summary>
        /// 设置结果存放路径
        /// </summary>
        public string ResultPath
        {
            set
            {
                m_ResultPath = value;
            }
        }
        /// <summary>
        /// 设置拓扑容限
        /// </summary>
        public double TopoTolerance
        {
            set
            {
                m_TopoTolerance = value;
            }
        }

        /// <summary>
        /// 规则列表
        /// </summary>
        public List<SchemaRuleEx> RuleInfos { set; private get; }
        /// <summary>
        /// （将要）预处理的规则发生变化
        /// </summary>
        public event DealingRuleChangedHandler PretreatingRuleChanged;
        /// <summary>
        /// （将要）验证的规则发生变化
        /// </summary>
        public event DealingRuleChangedHandler VerifyingRuleChanged;
        /// <summary>
        /// （将要）检查的规则发生变化
        /// </summary>
        public event DealingRuleChangedHandler CheckingRuleChanged;
        /// <summary>
        /// 验证完成
        /// </summary>
        public event VerifyedCompleteHandler VerifyedComplete;
        /// <summary>
        /// 预处理完成
        /// </summary>
        public event CheckEventHandler PretreatComplete;
        /// <summary>
        /// 规则检查完成（预处理失败、检查失败都属于检查完成）
        /// </summary>
        public event RuleCheckedHandler RuleChecked;
        /// <summary>
        /// 开始检查拓扑规则
        /// </summary>
        public event CheckEventHandler TopoRuleCheckBegin;
        /// <summary>
        /// 检查完成事件
        /// </summary>
        public event CheckEventHandler CheckComplete;
    
        private void SendPretreatingEvent(ICheckRule pretreatingRule)
        {
            if (this.PretreatingRuleChanged != null)
                this.PretreatingRuleChanged.Invoke(this,pretreatingRule);
        }
        private void SendVerifyingEvent(ICheckRule verifyingRule)
        {
            if (this.VerifyingRuleChanged != null)
                this.VerifyingRuleChanged.Invoke(this,verifyingRule);
        }
        private void SendCheckingEvent(ICheckRule checkingRule)
        {
            if (this.CheckingRuleChanged != null)
                this.CheckingRuleChanged.Invoke(this,checkingRule);
        }
        private void SendRuleCheckedEvent(ICheckRule checkedRule, int errCount)
        {
            if (this.RuleChecked != null)
                this.RuleChecked.Invoke(this,checkedRule, errCount);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="strMessage"></param>
        private void SendMessage(enumMessageType msgType, string strMessage)
        {
            if (m_Messager != null)
                m_Messager.Invoke(msgType, strMessage);
        }

        //private List<ICheckRule> m_RuleList;
        private List<ICheckRule> m_TopoRuleList;
        private List<ICheckRule> m_NormalRuleList;
        private Dictionary<ICheckRule, SchemaRule> m_DictRuleAndInfo;

        //private bool m_Verifyed = false;
        //private bool m_Pretreated = false;
        private ITopology m_Topology = null;

        private void Init()
        {
            this.m_ErrorCount = 0;
            this.m_SucceedCount = 0;

            // 验证
            if (this.m_BaseWorkspace == null)
            {
                SendMessage(enumMessageType.Exception, "检查驱动的Base库没有设置，无法继续检查");
                return;
            }
            if (this.m_QueryWorkspace == null || this.m_QueryConnection == null)
            {
                SendMessage(enumMessageType.Exception, "检查驱动的Query库没有设置，无法继续检查");
                return;
            }

            // 结果库
            try
            {
                string strResultDBFile = this.m_ResultPath + "\\" + COMMONCONST.DB_Name_Result;
                System.IO.File.Copy(System.Windows.Forms.Application.StartupPath + "\\template\\report\\result_AutoTmpl.mdb", strResultDBFile, true);
                this.m_ResultConnection = Hy.Common.Utility.Data.AdoDbHelper.GetDbConnection(strResultDBFile);

            }
            catch (Exception exp)
            {
                SendMessage(enumMessageType.Exception, string.Format("创建结果库出错，信息：{0}", exp.Message));
                return;
            }

            // 创建规则实例，赋值，分类

            if (RuleInfos == null || RuleInfos.Count == 0)
                return;

            int count = RuleInfos.Count;
            m_NormalRuleList = new List<ICheckRule>();
            m_TopoRuleList = new List<ICheckRule>();
            m_DictRuleAndInfo = new Dictionary<ICheckRule, SchemaRule>();
            for (int i = 0; i < count; i++)
            {
                if (this.RuleInfos[i] == null || this.RuleInfos[i].ruleDllInfo == null)
                    continue;

                RuleInfo ruleInfo = new RuleInfo();// this.RuleInfos[i].ruleDllInfo;
                ruleInfo.ID = this.RuleInfos[i].arrayRuleParas[0].strInstID;
                ruleInfo.Name = this.RuleInfos[i].arrayRuleParas[0].strName;
                ruleInfo.Paramters = this.RuleInfos[i].arrayRuleParas[0].pParas;
                ruleInfo.RuleClassInfo = this.RuleInfos[i].ruleDllInfo;
                ruleInfo.Description = this.RuleInfos[i].strRemark;

                //if (ruleClassInfo == null)
                //    continue;

                if (ruleInfo.RuleClassInfo == null)
                {
                    SendMessage(enumMessageType.OperationalLog, string.Format("规则“{0}”无类信息，跳过检查", ruleInfo.Name));
                    continue;
                }

                ICheckRule checkRule = RuleFactory.CreateRuleInstance(ruleInfo.RuleClassInfo.DllName, ruleInfo.RuleClassInfo.ClassName);
                if (checkRule == null)
                {
                    SendMessage(enumMessageType.OperationalLog, string.Format("规则“{0}”反射未成功，跳过检查", ruleInfo.Name));
                    continue;

                }

                try
                {
                    // 参数设置
                    checkRule.BaseWorkspace = this.m_BaseWorkspace;
                    checkRule.InstanceName = ruleInfo.Name;
                    checkRule.InstanceID = ruleInfo.ID;
                    checkRule.DefectLevel = DefectHelper.GetRuleDefectLevel(ruleInfo.ID);
                    checkRule.MessageHandler = this.m_Messager;
                    checkRule.QueryConnection = this.m_QueryConnection;
                    checkRule.QueryWorkspace = this.m_QueryWorkspace;
                    checkRule.TopoWorkspace = this.m_TopoWorkspace;
                    checkRule.ResultConnection = this.m_ResultConnection;
                    checkRule.SchemaID = this.m_SchemaID;
                    checkRule.SetParamters(ruleInfo.Paramters);

                    if (checkRule.ErrorType == enumErrorType.Topology)
                    {
                        if (m_Topology == null)
                        {
                            try
                            {
                                // 先创建Topo库（空库）和结果库
                                if (System.IO.Directory.Exists(this.m_TopoDBPath + "\\" + COMMONCONST.DB_Name_Topo))
                                {
                                    System.IO.Directory.Delete(this.m_TopoDBPath + "\\" + COMMONCONST.DB_Name_Topo, true);
                                }
                                Hy.Common.Utility.Esri.AEAccessFactory.CreateFGDB(this.m_TopoDBPath, COMMONCONST.DB_Name_Topo, ref this.m_TopoWorkspace);
                                if (this.m_TopoWorkspace == null)
                                {
                                    SendMessage(enumMessageType.Exception, "创建拓扑库失败");
                                }

                                // 根据Base库找第一个Geodataset的空间参考，用来创建拓扑库
                                ISpatialReference topoSptatialRef = null;
                                IEnumDataset enDataset = this.m_BaseWorkspace.get_Datasets(esriDatasetType.esriDTAny);
                                IDataset ds = enDataset.Next();
                                while (ds != null)
                                {
                                    if (ds is IGeoDataset)
                                    {
                                        topoSptatialRef = (ds as IGeoDataset).SpatialReference;
                                        break;
                                    }
                                    ds = enDataset.Next();
                                }
                                IFeatureDataset fDataset = (this.m_TopoWorkspace as IFeatureWorkspace).CreateFeatureDataset(COMMONCONST.Dataset_Name, topoSptatialRef);
                                if (fDataset == null)
                                {
                                    SendMessage(enumMessageType.Exception, "创建拓扑库Dataset失败");
                                }

                                ITopologyContainer topoContainer = fDataset as ITopologyContainer;
                                //m_Topology = topoContainer.get_TopologyByName(COMMONCONST.Topology_Name);    // 若已有Topology，则不再创建

                                //if (m_Topology == null)
                                    m_Topology = topoContainer.CreateTopology(COMMONCONST.Topology_Name, this.m_TopoTolerance, COMMONCONST.TopoError_MaxCount, "esriConfigurationKeywordTopology");
                            }
                            catch (Exception exp)
                            {
                                SendMessage(enumMessageType.Exception, "创建Topology出错，信息：" + exp.ToString());
                            }
                        }
                        (checkRule as ITopologicalRule).Topology = m_Topology;

                        m_TopoRuleList.Add(checkRule);
                    }
                    else
                    {
                        m_NormalRuleList.Add(checkRule);
                    }
                    //m_RuleList.Add(checkRule);
                    m_DictRuleAndInfo.Add(checkRule, this.RuleInfos[i]);

                }
                catch (Exception ex)
                {
                    SendMessage(enumMessageType.Exception, "初始化规则失败，信息：" + ex.ToString());
                    continue;
                }
            }
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool Verify()
        {
            CheckHelper checkHelper = new CheckHelper(this.m_ResultConnection);
            //Init();
            if (m_NormalRuleList == null)
                return false;

            if (m_TopoRuleList == null)
                return false;

            bool isSucceed = true;

            for (int i = 0; i < m_NormalRuleList.Count; i++)
            {
                try
                {
                    SendVerifyingEvent(m_NormalRuleList[i]);
                    if (!m_NormalRuleList[i].Verify())
                    {
                        SendMessage(enumMessageType.VerifyError, string.Format("规则“{0}”验证失败\r\n", m_NormalRuleList[i].InstanceName));
                        m_NormalRuleList.RemoveAt(i);
                        i--;
                        isSucceed = false;
                    }
                    else
                    {
                        checkHelper.AddVerifiedRule(m_DictRuleAndInfo[m_NormalRuleList[i]],m_NormalRuleList[i].ErrorType, enumRuleState.ExecuteFailed);
                    }
                }
                catch (Exception exp)
                {
                    SendMessage(enumMessageType.Exception, exp.ToString());

                    SendMessage(enumMessageType.VerifyError, string.Format("规则“{0}”验证失败\r\n", m_NormalRuleList[i].InstanceName));
                    m_NormalRuleList.RemoveAt(i);
                    i--;
                    isSucceed = false;
                }
            }
            for (int i = 0; i < m_TopoRuleList.Count; i++)
            {
                try
                {
                    SendVerifyingEvent(m_TopoRuleList[i]);
                    if (!m_TopoRuleList[i].Verify())
                    {
                        SendMessage(enumMessageType.VerifyError, string.Format("规则“{0}”验证失败\r\n", m_TopoRuleList[i].InstanceName));
                        m_TopoRuleList.RemoveAt(i);
                        i--;
                        isSucceed = false;
                    }
                    else
                    {
                        checkHelper.AddVerifiedRule(m_DictRuleAndInfo[m_TopoRuleList[i]],m_TopoRuleList[i].ErrorType, enumRuleState.ExecuteFailed);
                    }
                }
                catch (Exception exp)
                {
                    SendMessage(enumMessageType.Exception, exp.ToString());

                    SendMessage(enumMessageType.VerifyError, string.Format("规则“{0}”验证失败{1}\r\n", m_TopoRuleList[i].InstanceName,exp.Message));
                    m_TopoRuleList.RemoveAt(i);
                    i--;
                    isSucceed = false;
                }
            }

            if (this.VerifyedComplete != null)
                this.VerifyedComplete.Invoke(this,m_NormalRuleList.Count + m_TopoRuleList.Count);

            return isSucceed;
        }

        /// <summary>
        /// 预处理
        /// </summary>
        /// <returns></returns>
        private bool Pretreat()
        {
            //Init();
            bool isSucceed = true;
            for (int i = 0; i < m_NormalRuleList.Count; i++)
            {
                try
                {
                    SendPretreatingEvent(m_NormalRuleList[i]);
                    if (!m_NormalRuleList[i].Pretreat())
                    {
                        SendMessage(enumMessageType.PretreatmentError, string.Format("规则“{0}”预处理失败\r\n", m_NormalRuleList[i].InstanceName));
                        
                        // 预处理失败的要算作检查完成了
                        SendRuleCheckedEvent(m_NormalRuleList[i], 0);

                        m_NormalRuleList.RemoveAt(i);
                        i--;
                        isSucceed = false;
                    }
                }
                catch (Exception exp)
                {
                    SendMessage(enumMessageType.Exception, exp.ToString());

                    // 预处理失败的要算作检查完成了
                    SendRuleCheckedEvent(m_NormalRuleList[i], 0);

                    m_NormalRuleList.RemoveAt(i);
                    i--;
                    isSucceed = false;

                    continue;
                }
            }
            for (int i = 0; i < m_TopoRuleList.Count; i++)
            {
                try
                {
                    SendPretreatingEvent(m_TopoRuleList[i]);
                    if (!m_TopoRuleList[i].Pretreat())
                    {
                        SendMessage(enumMessageType.PretreatmentError, string.Format("规则“{0}”检查失败\r\n", m_TopoRuleList[i].InstanceName));

                        // 预处理失败的要算作检查完成了
                        SendRuleCheckedEvent(m_TopoRuleList[i], 0);

                        m_TopoRuleList.RemoveAt(i);
                        i--;
                        isSucceed = false;

                    }
                }
                catch (Exception exp)
                {
                    SendMessage(enumMessageType.Exception, exp.ToString());

                    // 预处理失败的要算作检查完成了
                    SendRuleCheckedEvent(m_TopoRuleList[i], 0);

                    m_TopoRuleList.RemoveAt(i);
                    i--;
                    isSucceed = false;
                    continue;

                }
            }

            if (this.PretreatComplete != null)
                PretreatComplete.Invoke(this);

            return isSucceed;
        }


        private void ReOpenTopo()
        {
            if (this.m_Topology == null)
                return;

            System.Runtime.InteropServices.Marshal.ReleaseComObject(this.m_Topology);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(this.m_TopoWorkspace);

            Hy.Common.Utility.Esri.AEAccessFactory.OpenFGDB(ref this.m_TopoWorkspace,this.m_TopoDBPath+"\\"+ COMMONCONST.DB_Name_Topo);
            this.m_Topology= (this.m_TopoWorkspace as ITopologyWorkspace).OpenTopology(COMMONCONST.Topology_Name);

            for (int i = 0; i < m_NormalRuleList.Count; i++)
            {
                m_NormalRuleList[i].TopoWorkspace = this.m_TopoWorkspace;
            }

            for (int i = 0; i < m_TopoRuleList.Count; i++)
            {
                m_TopoRuleList[i].TopoWorkspace = this.m_TopoWorkspace;
                (m_TopoRuleList[i] as ITopologicalRule).Topology = this.m_Topology;
            }
        }

        /// <summary>
        /// 执行检查
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            //初始化
            Init();

            if (m_NormalRuleList == null && m_TopoRuleList == null)
            {
                SendMessage(enumMessageType.RuleError, "没有可检查的规则，检查结束");
                return false;
            }

            // 验证
            Verify();

            ReOpenTopo();

            // 预处理
            Pretreat();


            if (m_NormalRuleList.Count == 0 && m_TopoRuleList.Count == 0)
            {
                SendMessage(enumMessageType.RuleError, "没有可检查的规则，检查结束");
                return false;
            }


            bool isSucceed = true;
            ErrorHelper errHelper = ErrorHelper.Instance;
            errHelper.ResultConnection = this.m_ResultConnection;
            CheckHelper checkHelper = new CheckHelper(this.m_ResultConnection);

            // 自动检查
            for (int i = 0; i < m_NormalRuleList.Count; i++)
            {
                SendCheckingEvent(m_NormalRuleList[i]);
                SendMessage(enumMessageType.RuleError, string.Format("规则“{0}”开始检查", m_NormalRuleList[i].InstanceName));

                List<Error> errList = new List<Error>();
                int errCount = 0;
                try
                {
                    bool ruleSucceed = m_NormalRuleList[i].Check(ref errList);
                    if (ruleSucceed)
                    {
                        this.m_SucceedCount++;
                        if (errList != null)
                            errCount = errList.Count;

                        this.m_ErrorCount += 0;
                        SendMessage(enumMessageType.RuleError, string.Format("规则“{0}”检查成功，错误数:{1}\r\n", m_NormalRuleList[i].InstanceName, errCount));


                        checkHelper.UpdateRuleState(m_DictRuleAndInfo[m_NormalRuleList[i]].arrayRuleParas[0].strInstID, errCount, enumRuleState.ExecuteSucceed);
                    }
                    else
                    {
                        // 添加时默认了为失败，不用更新
                        //checkHelper.UpdateRuleState(m_DictRuleAndInfo[m_NormalRuleList[i]].arrayRuleParas[0].strInstID, 0, enumRuleState.ExecuteFailed);

                        SendMessage(enumMessageType.RuleError, string.Format("规则“{0}”检查失败\r\n", m_NormalRuleList[i].InstanceName));
                        isSucceed = false;
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    // 添加时默认了为失败，不用更新
                    //checkHelper.UpdateRuleState(m_DictRuleAndInfo[m_NormalRuleList[i]].arrayRuleParas[0].strInstID, 0, enumRuleState.ExecuteFailed);

                    SendMessage(enumMessageType.Exception, string.Format("规则“{0}”检查失败，信息：{1}\r\n", m_NormalRuleList[i].InstanceName, ex.Message));
                    continue;
                }
                finally
                {
                    // 无论如何，检查完成了
                    SendRuleCheckedEvent(m_NormalRuleList[i], errCount);
                }

                errHelper.AddErrorList(errList);
            }

            // Topo检查
                // 由本类在Init方法中创建拓扑
                // 在Topo规则的预处理中进行拓扑规则添加
                // 在这里统一进行拓扑验证
                // 规则的Hy.Check方法只负责错误结果的读取
            if (m_TopoRuleList.Count > 0)
            {
                if (this.TopoRuleCheckBegin != null)
                    this.TopoRuleCheckBegin.Invoke(this);

                if (this.m_Topology != null)
                {
                    this.m_Topology.ValidateTopology((this.m_Topology.FeatureDataset as IGeoDataset).Extent);
                }

                for (int i = 0; i < m_TopoRuleList.Count; i++)
                {
                    SendCheckingEvent(m_TopoRuleList[i]);
                    SendMessage(enumMessageType.RuleError, string.Format("规则“{0}”开始获取（拓扑）结果", m_TopoRuleList[i].InstanceName));

                    List<Error> errList = new List<Error>();
                    int errCount = 0;
                    try
                    {
                        bool ruleSucceed = m_TopoRuleList[i].Check(ref errList);
                        if (ruleSucceed)
                        {
                            this.m_SucceedCount++;
                            if (errList != null)
                                errCount = errList.Count;

                            this.m_ErrorCount += errCount;
                            SendMessage(enumMessageType.RuleError, string.Format("规则“{0}”获取（拓扑）结果成功，错误数:{1}\r\n", m_TopoRuleList[i].InstanceName, errCount));

                            checkHelper.UpdateRuleState(m_DictRuleAndInfo[m_TopoRuleList[i]].arrayRuleParas[0].strInstID, errCount, enumRuleState.ExecuteSucceed, m_TopoRuleList[i].InstanceName);
                        }
                        else
                        {

                            // 添加时默认了为失败，不用更新
                            //checkHelper.UpdateRuleState(m_DictRuleAndInfo[m_TopoRuleList[i]].arrayRuleParas[0].strInstID, 0, enumRuleState.ExecuteFailed,m_TopoRuleList[i].InstanceName);

                            SendMessage(enumMessageType.RuleError, string.Format("规则“{0}”获取（拓扑）结果失败\r\n", m_TopoRuleList[i].InstanceName));
                            isSucceed = false;
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {

                        // 添加时默认了为失败，不用更新
                        //checkHelper.UpdateRuleState(m_DictRuleAndInfo[m_TopoRuleList[i]].arrayRuleParas[0].strInstID, 0, enumRuleState.ExecuteFailed,m_TopoRuleList[i].InstanceName);

                        SendMessage(enumMessageType.Exception, string.Format("规则“{0}”获取（拓扑）结果失败，信息：{1}\r\n", m_TopoRuleList[i].InstanceName, ex.Message));
                        continue;
                    }
                    finally
                    {
                        // 无论如何，检查完成了
                        SendRuleCheckedEvent(m_TopoRuleList[i], errCount);
                    }

                    errHelper.AddErrorList(errList);
                }
            }

            errHelper.Flush();
            this.Release();

            if (this.CheckComplete != null)
                this.CheckComplete.Invoke(this);

            return isSucceed;
        }

        public void Release()
        {
            if (m_NormalRuleList != null)
            {
                for (int i = 0; i < m_NormalRuleList.Count; i++)
                {
                    m_NormalRuleList[i] = null;
                }
            }
            if (m_TopoRuleList != null)
            {
                for (int i = 0; i < m_TopoRuleList.Count; i++)
                {
                    m_TopoRuleList[i] = null;
                }
            }

            this.BaseWorkspace = null;
            this.QueryConnection = null;
            this.QueryWorkspace = null;
            this.RuleInfos = null;
            this.SchemaID = null;
            this.TopoDBPath = null;
            this.TopoTolerance = -1;
            //this.Messager = null;

            if (this.m_ResultConnection != null)
            {
                if (this.m_ResultConnection.State != ConnectionState.Closed)
                    this.m_ResultConnection.Close();
            }
            if (this.m_Topology != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(this.m_Topology);
                this.m_Topology = null;
            }
            if (this.m_TopoWorkspace != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(this.m_TopoWorkspace);
                this.m_TopoWorkspace = null;
            }
        }

        /// <summary>
        /// （验证通过的，注意不是系统库配置的）规则数
        /// </summary>
        public int RuleCount
        {
            get
            {
                int rCount = 0;
                if (m_NormalRuleList != null)
                    rCount = m_NormalRuleList.Count;
                if (m_TopoRuleList != null)
                    rCount += m_TopoRuleList.Count;

                return rCount;
            }
        }

        private int m_ErrorCount = 0;
        /// <summary>
        /// 错误数
        /// </summary>
        public int ErrorCount
        {
            get
            {
                return m_ErrorCount;
            }
        }

        private int m_SucceedCount = 0;
        /// <summary>
        /// 检查成功的规则数目
        /// </summary>
        public int SucceedCount
        {
            get
            {
                return m_SucceedCount;
            }
        }

        private class CheckHelper
        {
            private IDbConnection m_ResultConnection;

            public CheckHelper(IDbConnection resultConnection)
            {
                this.m_ResultConnection = resultConnection;
            }


            //public bool ClearResultRuleList(IDbConnection resultConnection)
            //{
            //    return Hy.Common.Utility.Data.AdoDbHelper.ExecuteSql(resultConnection, "delete from LR_ResultEntryRule");
            //}

            public bool AddVerifiedRule(SchemaRule schemaRule,enumErrorType errType, enumRuleState defaultState)
            {
                string strObject = null;
                StringBuilder strBuilder = new StringBuilder("insert into LR_ResultEntryRule(CheckType,RuleInstID,TempInstID,ErrorCount,RuleExeState,TargetFeatClass1,TargetFeatClass2,GZBM,ErrorType) Values ('");
                strObject = schemaRule.ChkTypeName;
                if (strObject == null) strObject = "";
                strBuilder.Append(strObject.Replace("'", "''")); strBuilder.Append("','");

                strObject = schemaRule.arrayRuleParas[0].strInstID;
                if (strObject == null) strObject = "";
                strBuilder.Append(strObject.Replace("'", "''")); strBuilder.Append("','");

                strObject = schemaRule.TempInstID;
                if (strObject == null) strObject = "";
                strBuilder.Append(strObject.Replace("'", "''")); strBuilder.Append("',");

                strBuilder.Append(0); strBuilder.Append(",");
                strBuilder.Append((int)defaultState); strBuilder.Append(",'");

                strObject = schemaRule.FeaClassName;
                if (strObject == null) strObject = "";
                strBuilder.Append(strObject.Replace("'", "''")); strBuilder.Append("','");

                strBuilder.Append("','");


                strObject = schemaRule.arrayRuleParas[0].strGZBM;
                if (strObject == null) strObject = "";
                strBuilder.Append(strObject.Replace("'", "''"));strBuilder.Append("',");

                strBuilder.Append((int)errType);

                strBuilder.Append(")");

                return Hy.Common.Utility.Data.AdoDbHelper.ExecuteSql(this.m_ResultConnection, strBuilder.ToString());

            }

            public bool UpdateRuleState(string ruleInstanceID, int errorCount, enumRuleState ruleState)
            {
                string strSQL = string.Format("Update LR_ResultEntryRule set ErrorCount={0} ,RuleExeState={1} where RuleInstID='{2}'", errorCount, (int)ruleState, ruleInstanceID);
                return Hy.Common.Utility.Data.AdoDbHelper.ExecuteSql(this.m_ResultConnection,strSQL);
            }

            public bool UpdateRuleState(string ruleInstanceID, int errorCount, enumRuleState ruleState, string arcgisRule)
            {
                object[] objArgs = { errorCount, (int)ruleState, ruleInstanceID, arcgisRule };
                string strSQL = string.Format("Update LR_ResultEntryRule set ArcGiSRule='{3}', ErrorCount={0} ,RuleExeState={1} where RuleInstID='{2}'", objArgs);
                return Hy.Common.Utility.Data.AdoDbHelper.ExecuteSql(this.m_ResultConnection, strSQL);
            }

        }

        
    }
}