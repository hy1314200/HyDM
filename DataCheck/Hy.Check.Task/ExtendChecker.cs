using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.IO;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using Hy.Check.Define;
using Hy.Check.Utility;
using Hy.Common.Utility;
using Hy.Common.Utility.Esri;
using Hy.Common.Utility.Data;
using Hy.Check.Engine;
using Hy.Check.Task.DataImport;

namespace Hy.Check.Engine
{
    /// <summary>
    /// @brief 针对第三方调用封装的检查类
    /// 可检查Workspace，FeatureClass和Table
    /// @example ExtendChecker调用
    /// public class Example
    /// {
    ///    public void InvokeChecker()
    ///    {
    ///        string strSourcePath = @@"C:\Documents and Settings\Xingzhe\桌面\BASE.gdb";
    ///        string strTempPath = @@"C:\Documents and Settings\Xingzhe\桌面\Export";
    ///        CheckExtension.ExtendChecker.TempPath = strTempPath;
    ///
    ///        CheckExtension.ExtendChecker extChecker = new CheckExtension.ExtendChecker(); 
    ///        extChecker.CheckingMessageChanged += new CheckExtension.StringMessageHandler(CheckingMessageChanged);
    ///        extChecker.DataImportingMessageChanged+=new CheckExtension.StringMessageHandler(CheckingMessageChanged);
    ///        //// 设置需要检查的规则列表
    ///        //TemplateRules templateRules = new TemplateRules(task.SchemaID); 
    ///        //extChecker.RuleInfos= templateRules.CurrentSchemaRules
    ///        ESRI.ArcGIS.Geodatabase.IWorkspace wsTarget = EngineHelper.AEDataAccess.AEAccessFactory.OpenWorkspace(EngineHelper.AEDataAccess.enumDataType.FileGDB, strSourcePath);
    ///
    ///
    ///        m_FrmProgress.Show();
    ///        // 多线程模式
    ///        //System.Threading.ThreadStart threadStart = delegate
    ///        //{
    ///            System.Data.DataTable dtError = eChecker.Check(wsTarget as ESRI.ArcGIS.Geodatabase.IDataset);
    ///        //};
    ///        //System.Threading.Thread thread = new System.Threading.Thread(threadStart);
    ///        //thread.Start();
    ///        //m_FrmProgress.ShowDialog();
    ///
    ///        m_FrmProgress.Hide();// Close()/Dispose();
    ///    }
    ///
    ///    private FrmProgress m_FrmProgress = new FrmProgress();
    ///    private void CheckingMessageChanged(string strMsg)
    ///    {
    ///        m_FrmProgress.ShowMessage(strMsg);
    ///    }
    /// }
    /// </summary>
    public class ExtendChecker
    {
        public ExtendChecker()
        {
            this.TopoTolerence = 0.0001;
            this.MapScale = 10000;
        }

        private static string m_TempPath;
        /// <summary>
        /// Temp路径，对目标的数据检查将在此文件夹下进行
        /// 请确保此文件夹可用的大小为目标数据大小3倍以上
        /// 默认在Bin目录的CheckTemp下
        /// </summary>
        public static string TempPath
        {
            set
            {
                m_TempPath = value;
            }

            private get
            {
                if (string.IsNullOrEmpty(m_TempPath))
                {
                    m_TempPath = System.Windows.Forms.Application.StartupPath + "\\CheckTemp";
                }

                return m_TempPath;
            }
        }

        /// <summary>
        /// 拓扑容限
        /// </summary>
        public double TopoTolerence { set; private get; }

        /// <summary>
        /// 地图比例尺
        /// </summary>
        public int MapScale { set; private get; }

        /// <summary>
        /// 指定对目标进行检查的规则列表
        /// @remark 当为null时，将根据方案ID@see ::SchemaID 获取方案下所有配置的规则
        /// </summary>
        public List<SchemaRuleEx> RuleInfos { set; private get; }

        /// <summary>
        /// 方案ID
        /// @remark 当为null/空时，将从系统库获取第一个标准下的第一个方案，若获取失败，检查将不会开始
        /// </summary>
        public string SchemaID { set; private get; }


        /// <summary>
        /// 使用统一入口进行质检
        /// </summary>
        /// <param name="dsTarget">可以是IWorkspace，IFeatureClass或ITable</param>
        /// <returns></returns>
        public DataTable Check(IDataset dsTarget)
        {
            // 数据导入
            string strBaseName = "Base.gdb";
            string strQueryDB = "Base.mdb";
            bool ready = false;
            enumDataType baseDataType = enumDataType.FileGDB;

            SendCheckingMessage("正在进行数据导入…");

            try
            {
                Clear();
                if (dsTarget is IWorkspace)
                {
                    ready = PrepareForWorkspace(dsTarget as IWorkspace, out strBaseName, out strQueryDB, out baseDataType);
                }
                else if (dsTarget is IFeatureClass)
                {
                    ready = PrepareForFeatureClass(dsTarget, out strBaseName, out strQueryDB);
                    baseDataType = enumDataType.PGDB;

                }
                else if (dsTarget is ITable)
                {
                    ready = PrepareForTable(dsTarget, out strBaseName, out strQueryDB);
                    baseDataType = enumDataType.PGDB;
                }


                if (!ready)
                {
                }

                // 开始准备检查

                string strBaseFullName = string.Format("{0}\\{1}", TempPath, strBaseName);
                string strQueryFullName = string.Format("{0}\\{1}", TempPath, strQueryDB);
                IWorkspace wsBase = AEAccessFactory.OpenWorkspace(baseDataType, strBaseFullName);
                IWorkspace wsQuery = AEAccessFactory.OpenWorkspace(enumDataType.PGDB, strQueryFullName);
                IDbConnection queryConnection = AdoDbHelper.GetDbConnection(strQueryFullName);

                // 开始执行检查
                Checker curChecker = new Checker();
                curChecker.VerifyingRuleChanged += new DealingRuleChangedHandler(VerifyingRuleChanged);
                curChecker.VerifyedComplete += new VerifyedCompleteHandler(VerifyedComplete);
                curChecker.PretreatingRuleChanged += new DealingRuleChangedHandler(PretreatingRuleChanged);
                curChecker.PretreatComplete += new CheckEventHandler(PretreatComplete);
                curChecker.CheckingRuleChanged += new DealingRuleChangedHandler(CheckingRuleChanged);
                curChecker.CheckComplete += new CheckEventHandler(CheckComplete);
                curChecker.RuleChecked += new RuleCheckedHandler(RuleChecked);
                curChecker.TopoRuleCheckBegin += new CheckEventHandler(TopoRuleCheckBegin);

                curChecker.BaseWorkspace = wsBase;
                curChecker.QueryWorkspace = wsQuery;
                curChecker.QueryConnection = queryConnection;
                curChecker.ResultPath = TempPath;
                // 如果没有设置SchemaID，获取第一个
                // 如果没有设置RuleInfo列表，获取所有
                if (string.IsNullOrEmpty(this.SchemaID))
                {
                    Dictionary<int, string> dictStandard = SysDbHelper.GetStandardInfo();
                    if (dictStandard == null || dictStandard.Count == 0)
                        return null;

                    Dictionary<string, string> dictSchema = SysDbHelper.GetSchemasInfo(dictStandard.ElementAt(0).Value);
                    if (dictSchema == null || dictSchema.Count == 0)
                        return null;

                    this.SchemaID = dictSchema.ElementAt(0).Key;
                }
                if (this.RuleInfos == null)
                {
                    TemplateRules templateRule = new TemplateRules(this.SchemaID);
                    this.RuleInfos = templateRule.CurrentSchemaRules;
                }

                curChecker.RuleInfos = this.RuleInfos;
                curChecker.SchemaID = this.SchemaID;
                curChecker.TopoDBPath = TempPath;
                curChecker.TopoTolerance = this.TopoTolerence;

                COMMONCONST.TOPOTOLORANCE = this.TopoTolerence;
                COMMONCONST.dAreaThread = this.MapScale * 0.04;
                COMMONCONST.dLengthThread = this.MapScale * 0.2 / 10000;

                //SendCheckBeginEvent(curChecker);

                curChecker.Check();            // 检查

                // 获取结果
                string strResultFullName = string.Format("{0}\\{1}", TempPath, "Result.mdb");
                IDbConnection resultConnection = AdoDbHelper.GetDbConnection(strResultFullName);

                return GetErrors(resultConnection);
            }
            catch
            {
                return null;
            }
            finally
            {
                GC.Collect();

                //Clear();
            }
        }

        private void Clear()
        {
            GC.Collect();

            #region Base库删除
            string strPath = string.Format("{0}\\{1}", TempPath, "Base.gdb");
            if (Directory.Exists(strPath))
            {
                try
                {
                    Directory.Delete(strPath,true);
                }
                catch
                {
                }
            }

            strPath = string.Format("{0}\\{1}", TempPath, "Base.mdb");
            if (File.Exists(strPath))
            {
                try
                {
                    File.Delete(strPath);
                }
                catch
                {
                }
            }

            strPath = string.Format("{0}\\{1}", TempPath, "Base");
            if (Directory.Exists(strPath))
            {
                try
                {
                    Directory.Delete(strPath,true);
                }
                catch
                {
                }
            }

            #endregion

            // Query库删除
            strPath = string.Format("{0}\\{1}", TempPath, "Query.mdb");
            if (File.Exists(strPath))
            {
                try
                {
                    File.Delete(strPath);
                }
                catch
                {
                }
            }

            // Topo库
            strPath = string.Format("{0}\\{1}", TempPath, "Topo.gdb");
            if (Directory.Exists(strPath))
            {
                try
                {
                    Directory.Delete(strPath,true);
                }
                catch
                {
                }
            }

            // 结果库
            strPath = string.Format("{0}\\{1}", TempPath, "Result.mdb");
            if (File.Exists(strPath))
            {
                try
                {
                    File.Delete(strPath);
                }
                catch
                {
                }
            }
        }

        void VerifyingRuleChanged(Checker sender, ICheckRule dealingRule)
        {
            if (dealingRule == null)
                return;

            SendCheckingMessage(string.Format("正在验证“{0}”规则…", dealingRule.Name));

        }
        void VerifyedComplete(Checker sender, int availCount)
        {
            SendCheckingMessage("验证完成。");
        }
        void PretreatingRuleChanged(Checker sender, ICheckRule dealingRule)
        {
            if (dealingRule == null)
                return;

            SendCheckingMessage(string.Format("正在预处理“{0}”规则…", dealingRule.Name));
        }
        void PretreatComplete(Checker sender)
        {
            SendCheckingMessage("预处理完成。");
        }
        void CheckingRuleChanged(Checker sender, ICheckRule dealingRule)
        {
            if (dealingRule == null)
                return;

            SendCheckingMessage(string.Format("正在检查“{0}”规则…", dealingRule.Name));
        }
        void RuleChecked(Checker sender, ICheckRule checkedRule, int errorCount)
        {
            //SendCheckingMessage(string.Format("规则“{0}”：
        }
        void TopoRuleCheckBegin(Checker sender)
        {
            SendCheckingMessage("开始检查拓扑规则…");
        }
        void CheckComplete(Checker sender)
        {
            SendCheckingMessage("检查完成。");
        }

        void DataImport_ImportingObjectChanged(string strOjbectName)
        {
            //if (this.DataImportingMessageChanged != null)
            //    this.DataImportingMessageChanged.Invoke(strOjbectName);
            SendCheckingMessage(strOjbectName);
        }

        ///// <summary>
        ///// 若希望详细监听事件，使用此事件，对产此事件的参数Checker进行详细监听
        ///// </summary>
        //public event ExtendCheckEventHandler CheckBegin;
        //private void SendCheckBeginEvent(Checker checker)
        //{
        //    if (this.CheckBegin != null)
        //        this.CheckBegin.Invoke(checker);
        //}
        ///// <summary>
        ///// 数据导入时的消息抛出
        ///// @remark 针对Workspace检查时而抛出的事件，FeatureClass和ITable将不引发此事件
        ///// </summary>
        //public event StringMessageHandler DataImportingMessageChanged;
        /// <summary>
        /// 检查过程中的消息抛出
        /// </summary>
        public event StringMessageHandler CheckingMessageChanged;
        private void SendCheckingMessage(string strMsg)
        {
            if (this.CheckingMessageChanged != null)
                this.CheckingMessageChanged.Invoke(strMsg);
        }


        private bool PrepareForWorkspace(IWorkspace wsTarget, out string strBaseName, out string strQueryName, out enumDataType baseDataType)
        {
            strBaseName = null;
            strQueryName = null;
            baseDataType = enumDataType.FileGDB;

            try
            {
                string strPath = wsTarget.PathName;
                IDataImport dataImport = new NoReferenceDataImport();
                dataImport.ImportingObjectChanged += new ImportingObjectChangedHandler(DataImport_ImportingObjectChanged);
                dataImport.Datasource = strPath;
                dataImport.TargetPath = TempPath;
                dataImport.JustCopy = true;

                // 获取空间参考
                ISpatialReference spatialRef = null;
                IEnumDataset enDataset = wsTarget.get_Datasets(esriDatasetType.esriDTAny);
                IDataset dsCurrent = enDataset.Next();
                while (dsCurrent != null)
                {
                    if (dsCurrent is IGeoDataset)
                    {
                        spatialRef = (dsCurrent as IGeoDataset).SpatialReference;
                        break;
                    }

                    dsCurrent = enDataset.Next();
                }
                dataImport.SpatialReference = spatialRef;
                enumDataType dataType = enumDataType.PGDB;

                // 设置数据类型
                string strName = System.IO.Path.GetExtension(strPath);
                if (string.IsNullOrEmpty(strName))  // Shp File
                {
                    dataType = enumDataType.SHP;
                }
                else if (strName.ToLower() == ".gdb")
                {
                    dataType = enumDataType.FileGDB;
                }
                else // MDB
                {
                    dataType = enumDataType.PGDB;
                }
                dataImport.DataType = dataType;
                if (dataType != enumDataType.PGDB)
                {
                    AEAccessFactory.CreatePGDB(TempPath, "Query.mdb");
                }

                dataImport.Import();

                // 获取Base/Query库Workspace和ADO连接
                strBaseName = (dataType == enumDataType.PGDB ? "Base.mdb" : (dataType == enumDataType.FileGDB ? "Base.gdb" : "Base"));
                strQueryName = (dataType == enumDataType.PGDB ? strBaseName : "Query.mdb");
                baseDataType = dataType;

                return true;
            }
            catch
            {
                return false;
            }

        }
        private bool PrepareForFeatureClass(IDataset dsTarget, out string strBaseName, out string strQueryName)
        {
            strQueryName = strBaseName = "Base.mdb";
            try
            {

                AEAccessFactory.CreateFGDB(TempPath, strBaseName);
                IWorkspace wsBase = AEAccessFactory.OpenWorkspace(enumDataType.FileGDB, string.Format("{0}\\{1}", TempPath, strBaseName));
                GPTool gpTool = new GPTool();
                DataConverter.ConvertFeatureClass(dsTarget.Workspace, wsBase, dsTarget.Name, dsTarget.Name);

                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool PrepareForTable(IDataset dsTarget, out string strBaseName, out string strQueryName)
        {
            strQueryName = strBaseName = "Base.mdb";
            try
            {

                AEAccessFactory.CreateFGDB(TempPath, strBaseName);
                IWorkspace wsBase = AEAccessFactory.OpenWorkspace(enumDataType.FileGDB, string.Format("{0}\\{1}", TempPath, strBaseName));
                GPTool gpTool = new GPTool();
                DataConverter.ConvertTable(dsTarget.Workspace, wsBase, dsTarget, dsTarget.Name);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private DataTable GetErrors(IDbConnection resultConnection)
        {
            string strSQL = @"SELECT
                            b.CheckType as 检查类型,
                            a.TargetFeatClass1 as 源图层,
                            a.BSM as 标识码,
                            a.TargetFeatClass2 as 目标图层,
                            a.BSM2 as 标识码2,
                            b.GZBM as 规则编码,
                            a.ErrMsg as 错误描述
                            from LR_ResAutoAttr as a, LR_ResultEntryRule as b where a.RuleInstID=b.RuleInstID

                                union all

                            SELECT
                            b.CheckType as 检查类型,
                            a.YSTC  as 源图层,
                            a.SourceBSM as 标识码,
                            a.MBTC as 目标图层,
                            a.TargetBSM as 标识码2,
                            b.GZBM as 规则编码,
                            a.Reason as 错误描述
                            from LR_ResAutoTopo as a, LR_ResultEntryRule as b where a.RuleInstID=b.RuleInstID

                                union all

                            SELECT
                            b.CheckType as 检查类型,
                            a.AttrTabName as 源图层,
                            '' as 标识码,
                            '' as 目标图层,
                            '' as 标识码2,
                            b.GZBM as 规则编码,
                            a.ErrorReason as 错误描述
                            from LR_ResIntField as a, LR_ResultEntryRule as b where a.RuleInstID=b.RuleInstID

                                union all

                            SELECT
                            b.CheckType as 检查类型,
                            a.ErrorLayerName as 源图层,
                            '' as 标识码,
                            '' as 目标图层,
                            '' as 标识码2,
                            b.GZBM as 规则编码,
                            a.ErrorReason as 错误描述
                            from LR_ResIntLayer as a, LR_ResultEntryRule as b where a.RuleInstID=b.RuleInstID";

            return AdoDbHelper.GetDataTable(resultConnection, strSQL);
        }
    }

    public delegate void StringMessageHandler(string strMsg);

    public delegate void ExtendCheckEventHandler(Checker checker);
}
