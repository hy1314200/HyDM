using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;

using System.IO;
using System.Data;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.InteropServices;


using Common.Utility.Esri;
using Hy.Check.Task.DataImport;
using Hy.Check.Define;
using Hy.Check.Utility;

namespace Hy.Check.Task
{
    /// <summary>
    /// 任务类
    /// @remark 任务结构保存为任务配置文件时，采用Micro的序列化和反序列化，如结构进行修改，请注意修改相应Xml标签
    /// </summary>
    [XmlRoot("Hy.Check.Task")]
    public class Task
    {

        public Task()
        {
 
        }
        #region 基本（必须）信息

        private string m_ID;
        /// <summary>
        /// 标识
        /// </summary>
        [XmlIgnore()]
        public string ID
        {
            get {
                if (string.IsNullOrEmpty(m_ID))
                {
                    if (!SysDbHelper.GetTaskID(out m_ID))
                    {
                        SendMessage(enumMessageType.Exception, "获取新任务ID时出错");
                    }
                }

                return m_ID;
            }
            internal set
            {
                m_ID = value;
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// （任务）路径
        /// </summary>
        [XmlElement("Path")]
        public string Path { get; set; }

        /// <summary>
        /// 方案标识
        /// </summary>
        [XmlElement("SchemaID")]
        public string SchemaID { get; set; }

        /// <summary>
        /// 标准名（标识）
        /// </summary>
        [XmlElement("StandardName")]
        public string StandardName { get; set; }

        /// <summary>
        /// 数据源的数据类型
        /// </summary>
        [XmlElement("DatasourceType")]
        public enumDataType DatasourceType { get; set; }

        /// <summary>
        /// 数据源路径
        /// </summary>
        [XmlElement("SourcePath")]
        public string SourcePath { get; set; }

        /// <summary>
        /// Topo容限，数据导入时使用
        /// </summary>
        [XmlElement("Tolerance")]
        public double TopoTolerance { get; set; }

        /// <summary>
        /// 是否直接使用数据源进行检查（当做Base库）
        /// </summary>
        public bool UseSourceDirectly { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [XmlIgnore()]// XmlElement("State")]
        public enumTaskState State { get; set; }

        /// <summary>
        /// 空间参考
        /// </summary>
        [XmlIgnore()]
        public ISpatialReference SpatialReference { get; set; }

        #endregion

        #region 扩展信息

        /// <summary>
        /// 当前数据比例尺
        /// </summary>
        [XmlElement("MapScale")]
        public int MapScale { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [XmlElement("Institution")]
        public string Institution { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [XmlElement("Creator")]
        public string Creator { get; set; }

        private string m_CreateTime = DateTime.Now.ToString();
        /// <summary>
        /// 创建时间
        /// </summary>
        [XmlElement("CreateTime")]
        public string CreateTime
        {
            get
            {
                return m_CreateTime;
            }
            set
            {
                m_CreateTime = value;
            }
        }
        /// <summary>
        /// 说明
        /// </summary>
        [XmlElement("Remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 数据名前缀，指FeatureClass/Table名，执行数据导入时去掉
        /// </summary>
        [XmlElement("DataPrefix")]
        public string DataPrefix { get; set; }

        #endregion

        /// <summary>
        /// 设置数据导入器
        /// </summary>
        [XmlIgnore()]
        public IDataImport DataImporter { set; get; }


        private IWorkspace m_BaseWorkspace;
        private IWorkspace m_QueryWorkspace;
        private IWorkspace m_TopoWorkspace;
        private IDbConnection m_QueryConnection;
        private IDbConnection m_ResultConnection;
        //private IFeatureDataset m_TopoDataset;

        /// <summary>
        /// Base库 Workspace
        /// 使用前请确认已创建好任务
        /// </summary>
        [XmlIgnore()]
        public ESRI.ArcGIS.Geodatabase.IWorkspace BaseWorkspace
        {
            get
            {
                if (m_BaseWorkspace == null)
                {
                    string strPath = GetBaseDBPath();

                    if (this.UseSourceDirectly)
                    {
                        try
                        {
                            if (this.DatasourceType == enumDataType.VCT)
                            { 
                                SendMessage(enumMessageType.Exception, "逻辑错误：VCT不允许直接使用数据源作为Base库");
                            }

                            this.m_BaseWorkspace= Common.Utility.Esri.AEAccessFactory.OpenWorkspace(this.DatasourceType, strPath);
                        }
                        catch (Exception exp)
                        {
                            SendMessage(enumMessageType.Exception, "打开Base库出错:" + exp.Message + "\n导致此错误的原因可能是任务未创建！");
                        }

                    }
                    else
                    {
                       bool isSucceed= Common.Utility.Esri.AEAccessFactory.OpenFGDB(ref m_BaseWorkspace, strPath);
                       if (!isSucceed)
                       {
                           SendMessage(enumMessageType.Exception, "打开Base库出错：可能的原因是任务未创建！");
                       }
                    }

                    return m_BaseWorkspace;
                }

                return m_BaseWorkspace;
            }
        }

        /// <summary>
        /// Query库 Workspace
        /// 使用前请确认已创建好任务
        /// </summary>
        [XmlIgnore()]
        public ESRI.ArcGIS.Geodatabase.IWorkspace QueryWorkspace
        {
            get
            {
                if (m_QueryWorkspace == null)
                {
                    if (UseSourceDirectly && this.DatasourceType == enumDataType.PGDB)
                    {
                        m_QueryWorkspace = m_BaseWorkspace;
                    }
                    else
                    {
                        bool isSucceed = Common.Utility.Esri.AEAccessFactory.OpenPGDB(ref m_QueryWorkspace, string.Format("{0}\\{1}", GetTaskFolder(), COMMONCONST.DB_Name_Query));
                        if (!isSucceed)
                        {
                            SendMessage(enumMessageType.Exception, "打开Query出错：可能的原因是任务未创建！");
                        }
                    }
                }

                return m_QueryWorkspace;
            }
        }

        /// <summary>
        /// Query库 的ADO连接
        /// 使用前请确认已创建好任务
        /// </summary>
        [XmlIgnore()]
        public System.Data.IDbConnection QueryConnection
        {
            get
            {
                if (m_QueryConnection == null)
                {
                    string strPath = string.Format("{0}\\{1}", GetTaskFolder(), (this.UseSourceDirectly && this.DatasourceType == enumDataType.PGDB) ? "Base.MDB" : COMMONCONST.DB_Name_Query);
                    m_QueryConnection = Common.Utility.Data.AdoDbHelper.GetDbConnection(strPath);
                }

                return m_QueryConnection;
            }
        }
 
        /// <summary>
        /// Topo库 Workspace
        /// 使用前请确认已创建好任务
        /// </summary>
        [XmlIgnore()]
        public ESRI.ArcGIS.Geodatabase.IWorkspace TopoWorkspace
        {
            get
            {
                if (m_TopoWorkspace == null)
                {
                    string strPath = string.Format("{0}\\{1}", GetTaskFolder(), COMMONCONST.DB_Name_Topo);
                    if (Directory.Exists(strPath))
                    {
                        bool isSucceed = Common.Utility.Esri.AEAccessFactory.OpenFGDB(ref m_TopoWorkspace, strPath);
                        if (!isSucceed)
                        {
                            SendMessage(enumMessageType.Exception, "打开Topo库出错");
                        }
                    }
                    else
                    {
                        SendMessage(enumMessageType.Exception, "打开Topo库失败：Topo库未被创建");
                    }
                }

                return m_TopoWorkspace;
            }
        }

        private ESRI.ArcGIS.Geodatabase.ITopology m_Topology;
        public ESRI.ArcGIS.Geodatabase.ITopology Topology
        {
            get
            {
                if (this.m_Topology == null)
                {
                    ITopologyWorkspace topoWs = this.TopoWorkspace as ITopologyWorkspace;
                    if (topoWs == null)
                        return null;

                    this.m_Topology= topoWs.OpenTopology(COMMONCONST.Topology_Name);
                }

                return this.m_Topology;
            }
        }

        ///// <summary>
        ///// 拓扑图层（将要）所在的Dataset
        ///// </summary>
        //[XmlIgnore()]
        //public ESRI.ArcGIS.Geodatabase.IFeatureDataset TopoDataset
        //{
        //    get
        //    {
        //        if (this.m_TopoDataset == null)
        //        {
        //            IWorkspace wsTopology = this.TopoWorkspace;
        //            if (wsTopology == null)
        //                return null;

        //            this.m_TopoDataset = (wsTopology as IFeatureWorkspace).OpenFeatureDataset(COMMONCONST.Dataset_Name);
        //        }

        //        return this.m_TopoDataset;
        //    }
        //}

        /// <summary>
        /// 结果库 的ADO连接
        /// 使用前请确认已创建好任务
        /// </summary>
        [XmlIgnore()]
        public System.Data.IDbConnection ResultConnection
        {
            get
            {
                if (m_ResultConnection == null)
                {
                    string strPath = string.Format("{0}\\{1}", GetResultDBPath(), COMMONCONST.DB_Name_Result);
                    m_ResultConnection = Common.Utility.Data.AdoDbHelper.GetDbConnection(strPath);
                }

                return m_ResultConnection;
            }
        }


        protected MessageHandler m_Messager;
        [XmlIgnore()]
        public virtual MessageHandler Messager
        {
            get
            {
                return m_Messager;

            }
            set
            {
                m_Messager = value;
            }
        }
        private void SendMessage(enumMessageType msgType, string strMsg)
        {
            if (m_Messager != null)
            {
                m_Messager(msgType, strMsg);
            }
        }


        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool ImportData()
        {
            // 如果没有显式设置，根据数据类型选择导入器
            if (this.DataImporter == null)
            {
                switch (this.DatasourceType)
                {
                    case enumDataType.FileGDB:
                        this.DataImporter = new FileGDBDataImport();
                        break;

                    case enumDataType.PGDB:
                        this.DataImporter = new MDBDataImport();
                        break;

                    case enumDataType.SHP:
                        this.DataImporter = new SHPDataImport();
                        break;

                    case enumDataType.VCT:
                        this.DataImporter = new VCTDataImport();
                        break;
                }
            }

            this.DataImporter.Datasource = this.SourcePath;
            this.DataImporter.DataType = this.DatasourceType;
            this.DataImporter.JustCopy = this.UseSourceDirectly;
            this.DataImporter.TargetPath = GetTaskFolder();
            this.DataImporter.SpatialReference = this.SpatialReference;
            this.DataImporter.DataPrefix = this.DataPrefix;
            this.DataImporter.Messager = this.m_Messager;
            this.DataImporter.SchemaID = this.SchemaID;

            return this.DataImporter.Import();
        }

        /// <summary>
        /// 创建任务
        /// 包括步骤：
        /// 1. 文件夹结构
        /// 2. 2个库创建
        /// 3. 创建System.config
        /// 4. 数据导入
        /// 5. 存入数据库
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool Create()
        {
            // 第一步 文件夹结构
            bool isSucceed = CreateTaskFolder();

            // 第二步 2个库创建
            isSucceed = isSucceed && CreateCheckDB();

            // 第三步 创建System.config
            isSucceed = isSucceed && CreateTaskConfig();

            // 第四步 数据导入
            isSucceed = isSucceed && ImportData();

            // 第五步 存入数据库
            isSucceed =isSucceed && TaskHelper.AddTask(this);
            
            if (!isSucceed)
            {
                RollBack();
            }

            return isSucceed;
        }

        /// <summary>
        /// 回滚任务
        /// </summary>
        private void RollBack()
        {
            // 释放所有连接
            Release();

            try
            {
                Directory.Delete(GetTaskFolder(), true);
            }
            catch (Exception exp)
            {
                SendMessage(enumMessageType.Exception, "删除任务失败：" + exp.Message);
            }
        }

        /// <summary>
        /// 释放所有数据库连接
        /// </summary>
        public void Release()
        {
            try
            {
                if (m_BaseWorkspace != null)
                {
                    Marshal.ReleaseComObject(m_BaseWorkspace);
                    m_BaseWorkspace = null;
                }

                if (m_QueryWorkspace != null)
                {
                    Marshal.ReleaseComObject(m_QueryWorkspace);
                    m_QueryWorkspace = null;
                }

                if (m_TopoWorkspace != null)
                {
                    Marshal.ReleaseComObject(m_TopoWorkspace);
                    m_TopoWorkspace = null;
                }

                if (m_QueryConnection != null)
                {
                    if (m_QueryConnection.State != ConnectionState.Closed)
                    {
                        m_QueryConnection.Close();
                    }
                    m_QueryConnection = null;
                }

                if (m_ResultConnection != null)
                {
                    if (m_ResultConnection.State != ConnectionState.Closed)
                    {
                        m_ResultConnection.Close();
                    }

                    m_ResultConnection = null;
                }
            }
            catch (Exception exp)
            {
                SendMessage(enumMessageType.Exception, "释放连接时出错：" + exp.Message);
            }

            GC.Collect();
        }


        /// <summary>
        /// 获取MXD路径
        /// </summary>
        /// <returns></returns>
        public string GetMXDFile()
        {
            return string.Format("{0}\\{1}.MXD", this.GetTaskFolder(), this.StandardName);
        }


        /// <summary>
        /// 获取Base库路径
        /// </summary>
        /// <returns></returns>
        public string GetBaseDBPath()
        {
            string strPath = string.Format("{0}\\{1}", GetTaskFolder(), COMMONCONST.DB_Name_Base);

            if (this.UseSourceDirectly)
            {

                strPath = GetTaskFolder() + "\\Base" + (this.DatasourceType == enumDataType.PGDB ? ".MDB" : (this.DatasourceType == enumDataType.FileGDB ? ".gdb" : ""));
            }

            return strPath;
        }

        /// <summary>
        /// 获取任务文件夹路径（而非“任务所在路径”）
        /// </summary>
        /// <returns></returns>
        public string GetTaskFolder()
        {
            return string.Format("{0}\\{1}", this.Path, this.Name);
        }


        /// <summary>
        /// 使用XmlSerializer进行序列化
        /// </summary>
        /// <returns></returns>
        private string ToXml()
        {
            TextWriter txtWriter = new StringWriter();
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            xmlSerializer.Serialize(txtWriter, this);

            return txtWriter.ToString();
        }



        /// <summary>
        /// 生成SystemConfig.xml文件
        /// </summary>
        /// <returns></returns>
        public bool CreateTaskConfig()
        {
            string strXml = this.ToXml();
            string strPath=GetTaskFolder() + "\\" + COMMONCONST.File_Name_SystemConfig;
            if (File.Exists(strPath))
            {
                try
                {
                    File.Delete(strPath);
                }
                catch
                {
                    return false;
                }
            }
            return Common.Utility.Encryption.FileEncryption.EncryptKey(strPath, strXml);
        }

        /// <summary>
        /// 创建任务文件夹（注意不是“任务所在文件夹”）
        /// </summary>
        /// <returns></returns>
        public bool CreateTaskFolder()
        {
            string errMsg = null;
            if (!TaskHelper.CreateTaskFolder(this.Path, this.Name, ref errMsg))
            {
                SendMessage(enumMessageType.Exception, errMsg);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 创建检查所需要的库
        /// </summary>
        /// <returns></returns>
        public bool CreateCheckDB()
        {
            string errMsg = null;
            if (!TaskHelper.CreateCheckDB(this.Path, this.Name, this.UseSourceDirectly, this.UseSourceDirectly && (this.DatasourceType == enumDataType.PGDB), ref errMsg))
            {
                SendMessage(enumMessageType.Exception, errMsg);
                return false;
            }

            return true;
        }

      

        public string GetResultDBPath( )
        {
            if (!this.m_CheckWholly )//|| this.State == enumTaskState.PartlyExcuted)
            {
                return this.GetTaskFolder() + "\\PartlyCheck";
            }
            else
            {
                return this.GetTaskFolder();
            }
        }

        ///// <summary>
        ///// 创建结果库
        ///// </summary>
        ///// <returns></returns>
        //public bool CreateResultDB()
        //{
        //    string errMsg=null;
        //    if(!TaskHelper.CreateResultDB(this.Path,this.Name,ref errMsg))
        //    {
        //        SendMessage(enumMessageType.Exception,errMsg);
        //        return false;
        //    }

        //    return true;
        //}

        /// <summary>
        /// 创建抽检文件夹（和结果库）
        /// </summary>
        /// <returns></returns>
        public bool CreatePartlyCheckFolder()
        {
            string errMsg = null;
            if (!TaskHelper.CreatePartlyCheckFolder(this.Path, this.Name, ref errMsg))
            {
                SendMessage(enumMessageType.Exception, errMsg);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 生成MXD，并将数据库重置
        /// </summary>
        /// <returns></returns>
        public bool CreateMXD()
        {
            try
            {
                string strMxdFile = GetMXDFile();
                File.Copy(System.Windows.Forms.Application.StartupPath + "\\" + COMMONCONST.RelativePath_MXD +"\\"+ this.StandardName + ".MXD", strMxdFile);
                IMapDocument mapDoc = new MapDocumentClass();
                mapDoc.Open(strMxdFile, null);

                // 重定向数据库
                IMap mapTarget = mapDoc.get_Map(0);
                Common.Utility.Esri.AEAccessFactory.ResetMapWorkspace(this.BaseWorkspace, mapTarget);
                // 修改空间参考及范围
                IEnumDataset enDataset = this.BaseWorkspace.get_Datasets(esriDatasetType.esriDTAny);
                IDataset dataset = enDataset.Next();
                double xMin=double.MaxValue, xMax=double.MinValue, yMin=double.MaxValue, yMax=double.MinValue;
                IEnvelope envExtent = null;
                ISpatialReference spatialRef = null;
                while (dataset != null)
                {
                    IGeoDataset geoDataset = dataset as IGeoDataset;
                    if (geoDataset != null)
                    {
                        //mapTarget.SpatialReference = geoDataset.SpatialReference;
                        //mapDoc.ActiveView.Extent.SpatialReference = geoDataset.SpatialReference;
                        //mapDoc.ActiveView.Extent = geoDataset.Extent;
                        //mapDoc.ActiveView.FullExtent.SpatialReference = geoDataset.SpatialReference;
                        //mapDoc.ActiveView.FullExtent = geoDataset.Extent;

                        spatialRef = geoDataset.SpatialReference;
                        envExtent = geoDataset.Extent;
                        if (!envExtent.IsEmpty)
                        {

                            if (xMin > envExtent.XMin) xMin = envExtent.XMin;
                            if (yMin > envExtent.YMin) yMin = envExtent.YMin;
                            if (xMax < envExtent.XMax) xMax = envExtent.XMax;
                            if (yMax < envExtent.YMax) yMax = envExtent.YMax;
                        }
                        //break;
                    }
                    dataset = enDataset.Next();
                }
                mapTarget.SpatialReference = spatialRef;
                envExtent.PutCoords(xMin, yMin, xMax, yMax);
                //mapTarget.RecalcFullExtent();
                mapDoc.ActiveView.Extent = envExtent;
                mapDoc.ActiveView.FullExtent = envExtent;
                (mapTarget as IActiveView).FullExtent = envExtent;

                mapDoc.Save(true, false);
                mapDoc.Close();

                // 再复制 “任务执行格式验证.xsd”文件
                File.Copy(System.Windows.Forms.Application.StartupPath + "\\" + COMMONCONST.RelativePath_MXD + "\\" + COMMONCONST.File_Name_XSD, GetTaskFolder() + "\\" + COMMONCONST.File_Name_XSD);
                return true;
            }
            catch (Exception exp)
            {
                SendMessage(enumMessageType.Exception, "生成MXD失败，错误信息：" + exp.Message);
                return false;
            }
        }


        ///// <summary>
        ///// 创建拓扑库（并创建“Dataset”）
        ///// </summary>
        ///// <returns></returns>
        //public bool CreateTopoDB()
        //{
        //    // 创建Workspace
        //    this.m_TopoWorkspace = Common.Utility.Esri.AEAccessFactory.CreateFGDB(this.GetTaskFolder(), COMMONCONST.DB_Name_Topo);
        //    if (this.m_TopoWorkspace == null)
        //        return false;

        //    IFeatureDataset fDataset = (this.m_TopoWorkspace as IFeatureWorkspace).CreateFeatureDataset(COMMONCONST.Dataset_Name, this.SpatialReference);
        //    return fDataset != null;

        //    //// 导入数据
        //    //DataBaseCopier dbCopier = new DataBaseCopier();
        //    //dbCopier.Datasource = this.GetBaseDBPath();
        //    //dbCopier.DataType = this.DatasourceType;
        //    //dbCopier.DataPrefix = this.DataPrefix;
        //    //dbCopier.JustCopy = !this.UseSourceDirectly;
        //    //dbCopier.Messager = this.Messager;
        //    //dbCopier.SpatialReference = this.SpatialReference;
        //    //dbCopier.TargetName = COMMONCONST.DB_Name_Topo;
        //    //dbCopier.TargetPath = this.Path;

        //    //return dbCopier.Import();
        //}


        private bool m_CheckWholly = true;

        /// <summary>
        /// 为检查作准备
        /// </summary>
        /// <param name="checkWholly"></param>
        public void ReadyForCheck(bool checkWholly)
        {
            m_CheckWholly = checkWholly;
            if (!m_CheckWholly)
            {
                string strPath = this.GetResultDBPath();
                if (!Directory.Exists(strPath))
                {
                    try
                    {
                        Directory.CreateDirectory(strPath);
                    }
                    catch(Exception exp)
                    {
                        SendMessage(enumMessageType.OperationalLog, "创建抽检文件夹出错：" + exp.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// （因执行完检查）更新状态
        /// </summary>
        public void UpdateStateForExcuted()
        {
            if (m_CheckWholly)
                this.State = enumTaskState.WhollyExcuted;
            else
                this.State = enumTaskState.PartlyExcuted;
        }
    }
}
