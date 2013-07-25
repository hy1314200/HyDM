using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;


using Hy.Common.Utility.Esri;
using Hy.Check.Define;
using Hy.Check.Utility;

using System.Data;

namespace Hy.Check.Task.DataImport
{
    /// <summary>
    /// 默认IDataImport实现，已实现PGDB，FileGDB，Shp的导入
    /// </summary>
    public abstract class DefaultDataImport:IDataImport 
    {
        protected MessageHandler m_Messager;
        protected string m_Datasource;
        protected enumDataType m_DataType;
        protected string m_TargetPath;
        protected bool m_JustCopy;
        //protected bool m_BaseAsQuery;
        protected ESRI.ArcGIS.Geometry.ISpatialReference m_SpatialReference;
        protected string m_DataPrefix;
        protected string m_SchemaID;

        public virtual MessageHandler Messager
        {
            set
            {
                m_Messager = value;
            }
        }

        public event ImportingObjectChangedHandler ImportingObjectChanged;


        public virtual string Datasource
        {
            set
            {
                m_Datasource = value;
            }
        }

        public virtual enumDataType DataType
        {
            set
            {
                m_DataType = value;
            }
        }

        public virtual string TargetPath
        {
            set
            {
                m_TargetPath = value;
            }
        }

        public virtual bool JustCopy
        {
            set
            {
                m_JustCopy = value;
            }
        }

        public virtual ESRI.ArcGIS.Geometry.ISpatialReference SpatialReference
        {
            set
            {
                m_SpatialReference = value;
            }
        }

        public virtual string DataPrefix
        {
            set
            {
                m_DataPrefix = value;
            }
        }

        public virtual string SchemaID
        {
            set
            {
                m_SchemaID = value;
            }
        }


        //public virtual bool BaseAsQuery
        //{
        //    set
        //    {
        //        m_BaseAsQuery = value;
        //    }
        //}

        public virtual bool Import()
        {

            IWorkspace wsBase = null;

            // Base库
            try
            {
                SendEvent("Base库");
                if (ImportToBase(ref wsBase))
                {
                    if (this.ImportingObjectChanged != null)
                        ImportingObjectChanged("Base库导入完成");
                }
                else
                {
                    if (this.ImportingObjectChanged != null)
                        ImportingObjectChanged("Base库导入失败");

                    return false;
                } 
            }
            catch (Exception exp)
            {
                SendMessage(enumMessageType.Exception, "数据导入失败，错误信息：" + exp.Message);

                return false;
            }

            // 


            // Query 库
            if (!m_JustCopy || this.m_DataType != enumDataType.PGDB)
            {
                try
                {
                    SendEvent("Query库");
                    if (GenerateQuery(wsBase))
                    {
                        if(this.ImportingObjectChanged!=null)
                            ImportingObjectChanged("Query库导入完成");

                        return true;
                    }
                    else
                    {
                        if (this.ImportingObjectChanged != null)
                            ImportingObjectChanged("Query库导入失败");

                        return false;
                    }
                    
                }
                catch (Exception exp)
                {
                    SendMessage(enumMessageType.Exception, "生成Query库失败，错误信息：" + exp.Message);
                    return false;
                }

            }

            return true;
        }

        protected void SendMessage(enumMessageType msgType, string strMsg)
        {
            if (m_Messager != null)
            {
                m_Messager(msgType, strMsg);
            }
        }

        protected void SendEvent(string strObject)
        {
            if (this.ImportingObjectChanged != null)
                this.ImportingObjectChanged(string.Format("正在导入{0}…",strObject));
        }

        /// <summary>
        /// 复制文件夹，为Shp复制使用
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="strTarget"></param>
        private void CopyDirectory(string strSource, string strTarget,string folderName)
        {
            string strNewDir = strTarget + "\\" + folderName;// +System.IO.Path.GetDirectoryName(strSource);
            System.IO.Directory.CreateDirectory(strNewDir);
            string[] strFiles = System.IO.Directory.GetFiles(strSource);
            foreach (string strFile in strFiles)
            {
                System.IO.File.Copy(strFile, strNewDir + "\\" + System.IO.Path.GetFileName(strFile));
            }

            //// 复制
            //string[] strFolders = System.IO.Directory.GetDirectories(strSource);
            //foreach (string strFolder in strFolders)
            //{
            //    CopyDirectory(strFolder, strNewDir + "\\" + System.IO.Path.GetDirectoryName(strFolder));
            //}
        }

        /// <summary>
        /// 去掉前缀
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        private string GetObjectName(string objName)
        {
            if (string.IsNullOrEmpty(this.m_DataPrefix) || string.IsNullOrEmpty(objName))
                return objName;

            if (objName.StartsWith(this.m_DataPrefix))
            {
                return objName.Remove(0, this.m_DataPrefix.Length);
            }
            else
            {
                return objName;
            }
        }

        private bool CopyToBase(ref IWorkspace wsBase)
        {
            // VCT数据不允许直接复制
            if (m_DataType == enumDataType.VCT)
            {
                SendMessage(enumMessageType.Exception, "导入VCT数据不允许直接复制");

                return false;
            }

            //if (ImportingObjectChanged != null)
            //{
            //    ImportingObjectChanged("数据复制");
            //}

            // MDB使用文件复制
            if (m_DataType == enumDataType.PGDB)
            {
                try
                {
                    string strDBPath = this.m_TargetPath + "\\Base.MDB";
                    System.IO.File.Copy(this.m_Datasource, strDBPath);//+ COMMONCONST.DB_Name_Base);

                    if (!Hy.Common.Utility.Esri.AEAccessFactory.OpenPGDB(ref wsBase, strDBPath))
                    {
                        SendMessage(enumMessageType.Exception, "导入数据（复制文件）后打开出错，请确认数据源为正确的PGDB文件");
                        return false;
                    }
                }
                catch
                {
                    SendMessage(enumMessageType.Exception, "导入数据（复制文件）出错");

                    return false;
                }
            }
            else // Shp和FileGDB使用文件夹复制
            {
                try
                {
                    string strFolderName = "Base";
                    if (m_DataType == enumDataType.SHP)
                    {
                        strFolderName = "Base"; 
                        CopyDirectory(this.m_Datasource, this.m_TargetPath, strFolderName);
                    }
                    else
                    {
                        strFolderName = COMMONCONST.DB_Name_Base;
                        Hy.Common.Utility.Esri.GPTool gpTool = new Hy.Common.Utility.Esri.GPTool();
                        gpTool.Copy(this.m_Datasource, this.m_TargetPath + "\\" + strFolderName);
                    }

                    wsBase =AEAccessFactory.OpenWorkspace(m_DataType, this.m_TargetPath + "\\" + strFolderName);

                }
                catch
                {
                    SendMessage(enumMessageType.Exception, "导入数据（复制文件夹）出错");

                    return false;
                }

            }

            return true;
        }

        /// <summary>
        /// （从数据源）导入到Base库，并返回Base库Workspace对象
        /// </summary>
        /// <param name="wsSource"></param>
        /// <returns></returns>
        protected virtual bool ImportToBase(ref IWorkspace wsBase)
        {
            // 直接复制的方式
            if (m_JustCopy )
            {
                return CopyToBase(ref wsBase);
            }

            // 导入的方式 
            string strWorkspace = this.m_TargetPath + "\\" + COMMONCONST.DB_Name_Base;
            if (!Hy.Common.Utility.Esri.AEAccessFactory.OpenFGDB(ref wsBase, strWorkspace))
            {
                SendMessage(enumMessageType.Exception, "导入数据失败：无法打开Base库，请确认在创建任务文件结构时已创建Base库");
                return false;
            }// Hy.Common.Utility.Esri.AEAccessFactory.CreateFGDB(this.m_TargetPath, COMMONCONST.DB_Name_Base);
            IFeatureDataset fdsTarget = CreateFeatureDataset(wsBase, this.m_SpatialReference);
            if (fdsTarget == null)
            {
                SendMessage(enumMessageType.Exception, "“Dataset”没有创建成功，无法继续导入");
                return false;
            }
            Hy.Common.Utility.Esri.GPTool gpTool = new Hy.Common.Utility.Esri.GPTool();

            // 打开数据源
            IWorkspace wsSource = null;
            try
            {
                wsSource = AEAccessFactory.OpenWorkspace(this.m_DataType, this.m_Datasource);
                if (wsSource == null)
                {
                    SendMessage(enumMessageType.Exception, "打开数据源出错");
                    return false;
                }

                // 获取FeatureClass名列表 
                IEnumDataset enDataset = wsSource.get_Datasets(esriDatasetType.esriDTAny);
                IDataset dataset = enDataset.Next();
                while (dataset != null)
                {
                    switch (dataset.Type)
                    {
                        case esriDatasetType.esriDTTable:
                            SendEvent(dataset.Name);
                            Hy.Common.Utility.Esri.DataConverter.ConvertTable(wsSource, wsBase, dataset, GetObjectName(dataset.Name));
                            break;

                        case esriDatasetType.esriDTFeatureClass:
                            SendEvent(dataset.Name);
                            gpTool.CopyFeatureClass(string.Format("{0}\\{1}", this.m_Datasource, dataset.Name + (this.m_DataType == enumDataType.SHP ? ".shp" : "")), string.Format("{0}\\{1}\\{2}", strWorkspace, fdsTarget.Name, GetObjectName(dataset.Name)));
                            //Hy.Common.Utility.Esri.DataConverter.ConvertFeatureClass(wsSource as IDataset,fdsTarget as IDataset, dataset as IFeatureClass, GetObjectName(dataset.Name));
                            break;

                        case esriDatasetType.esriDTFeatureDataset:
                            IFeatureClassContainer fsContainer = dataset as IFeatureClassContainer;
                            for (int i = 0; i < fsContainer.ClassCount; i++)
                            {
                                string strFcName = (fsContainer.get_Class(i) as IDataset).Name;
                                SendEvent(strFcName);
                                gpTool.CopyFeatureClass(string.Format("{0}\\{1}\\{2}", this.m_Datasource, dataset.Name, strFcName), string.Format("{0}\\{1}\\{2}", strWorkspace, fdsTarget.Name, GetObjectName(strFcName)));
                                //Hy.Common.Utility.Esri.DataConverter.ConvertFeatureClass(dataset, fdsTarget as IDataset, fsContainer.get_Class(i), (fsContainer.get_Class(i) as IDataset).Name);
                            }

                            break;

                        default: break;
                    }

                    dataset = enDataset.Next();
                }

                // 释放
                enDataset = null;
                dataset = null;

                // 改别名
                this.RenameClassObjects(wsBase);
            }
            catch(Exception exp)
            {
                SendMessage(enumMessageType.Exception, "导入Base库出错:" + exp.ToString());
            }
            finally
            {
                if (wsSource != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wsSource);
                    wsSource = null;
                }
            }

            return true;

        }

        /// <summary>
        /// （从Base库）生成Query库
        /// 默认的实现是从Base库导入
        /// </summary>
        /// <returns></returns>
        protected virtual bool GenerateQuery(IWorkspace wsBase)
        {
            string strWorkspace = this.m_TargetPath + "\\" + COMMONCONST.DB_Name_Query;
            IWorkspace wsQuery = null;
            if (!Hy.Common.Utility.Esri.AEAccessFactory.OpenPGDB(ref wsQuery, strWorkspace))
            {
                SendMessage(enumMessageType.Exception, "导入数据失败：无法打开Query库，请确认在创建任务文件结构时已创建Query库");
                return false;
            }
            //Hy.Common.Utility.Esri.AEAccessFactory.CreatePGDB(this.m_TargetPath, COMMONCONST.DB_Name_Query);
            //IFeatureDataset fdsTarget = CreateFeatureDataset(wsQuery, this.m_SpatialReference);

            Hy.Common.Utility.Esri.GPTool gpTool = new Hy.Common.Utility.Esri.GPTool();

            // 打开数据源           
            if (wsBase == null)
            {
                SendMessage(enumMessageType.Exception, "创建Query库出错：无法打开Base库");

                return false;
            }

            // 获取FeatureClass名列表           
            IEnumDataset enDataset = wsBase.get_Datasets(esriDatasetType.esriDTAny);
            IDataset dataset = enDataset.Next();
            while (dataset != null)
            {
                switch (dataset.Type)
                {
                    case esriDatasetType.esriDTTable:
                    case esriDatasetType.esriDTFeatureClass:
                        SendEvent(dataset.Name);
                        Hy.Common.Utility.Esri.DataConverter.ConvertTable(wsBase, wsQuery, dataset, GetObjectName(dataset.Name));
                        break;

                    case esriDatasetType.esriDTFeatureDataset:
                        IFeatureClassContainer fcContianer = dataset as IFeatureClassContainer;
                        for (int i = 0; i < fcContianer.ClassCount; i++)
                        {
                            IDataset dsSub = fcContianer.get_Class(i) as IDataset;
                            SendEvent(dsSub.Name);
                            Hy.Common.Utility.Esri.DataConverter.ConvertTable(wsBase, wsQuery, dsSub, GetObjectName(dsSub.Name));
                        }
                        break;

                    default:
                        break;
                }

                dataset = enDataset.Next();
            }

            // 释放
            enDataset = null;
            dataset = null;

            System.Runtime.InteropServices.Marshal.ReleaseComObject(wsBase);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wsQuery);
            wsBase = null;
            wsQuery = null;
            GC.Collect();

            return true;
        }

        /// <summary>
        /// 创建Dataset
        /// </summary>
        /// <param name="wsTarget"></param>
        /// <param name="pSpatialRef"></param>
        /// <returns></returns>
        protected  virtual IFeatureDataset CreateFeatureDataset(IWorkspace wsTarget, ISpatialReference pSpatialRef)
        {
            if (wsTarget == null)
            {
                return null;
            }
            try
            {
                if (pSpatialRef == null)
                {
                    pSpatialRef = new UnknownCoordinateSystemClass();

                    ISpatialReferenceTolerance pSpatialTolerance = pSpatialRef as ISpatialReferenceTolerance;
                    double dXYTolerance = pSpatialTolerance.XYTolerance;
                    double dZTolerance = pSpatialTolerance.ZTolerance;
                    ISpatialReferenceResolution pSpatialRefResolution =pSpatialRef as ISpatialReferenceResolution;

                    pSpatialRefResolution.set_XYResolution(true, dXYTolerance * 0.1);
                    pSpatialRefResolution.set_ZResolution(true, dZTolerance * 0.1);
                    pSpatialRefResolution.MResolution = pSpatialTolerance.MTolerance * 0.1;
                }
               
                return ((IFeatureWorkspace)wsTarget).CreateFeatureDataset(COMMONCONST.Dataset_Name, pSpatialRef);
                 
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, string.Format("创建Dataset时出错，信息：{0}", ex.Message));
 
                return null;
            }
        }


        protected void RenameClassObjects(IWorkspace wsTarget)
        {
            int standardID = SysDbHelper.GetStandardIDBySchemaID(this.m_SchemaID);
            IEnumDataset enDataset = wsTarget.get_Datasets(esriDatasetType.esriDTAny);
            IDataset dsCurrent = enDataset.Next();
            while (dsCurrent!=null)
            {
                switch (dsCurrent.Type)
                {
                    case esriDatasetType.esriDTTable:
                    case esriDatasetType.esriDTFeatureClass:
                        RenameClassObject(dsCurrent, standardID);
                        break;

                    case esriDatasetType.esriDTFeatureDataset:

                        //RenameClassObject(dsCurrent, standardID);  // FeatureDataset需要改吗

                        IEnumDataset enSubDataset = dsCurrent.Subsets;
                        IDataset subDataset = enSubDataset.Next();
                        while (subDataset != null)
                        {
                            RenameClassObject(subDataset, standardID);
                            subDataset = enSubDataset.Next();
                        }
                        break;
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(dsCurrent);

                dsCurrent = enDataset.Next();
            }
        }

        private void RenameClassObject(IDataset dsCurrent,int standardID)
        {
            if (dsCurrent == null)
                return;

            Hy.Check.Define.StandardLayer standardLayer = LayerReader.GetLayerByName(dsCurrent.Name, standardID);
            //string strAliasName = Hy.Check.Rule.Helper.LayerReader.GetAliasName(dsCurrent.Name, standardID);
            if (standardLayer!=null)// dsCurrent.Name != strAliasName)
            {
                ISchemaLock schemaLock = dsCurrent as ISchemaLock;
                schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);

                IClassSchemaEdit classSchemaEdit = dsCurrent as IClassSchemaEdit;
                if (classSchemaEdit != null)
                {
                    classSchemaEdit.AlterAliasName(standardLayer.AliasName);

                    ITable tCurrent = dsCurrent as ITable;
                    if (tCurrent != null)
                    {
                        List<Hy.Check.Define.StandardField> fields = FieldReader.GetFieldsByLayer(standardLayer.ID);
                        for (int i = 0; i < fields.Count; i++)
                        {
                            if (tCurrent.Fields.FindField(fields[i].Name) > -1)
                            {
                                classSchemaEdit.AlterFieldAliasName(fields[i].Name, fields[i].AliasName);
                            }
                        }
                    }
                }

            }

        }

    }
}
