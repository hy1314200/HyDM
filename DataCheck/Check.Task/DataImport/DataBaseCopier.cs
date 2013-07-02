using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using Common.Utility.Esri;
using Check.Define;
using Check.Utility;

namespace Check.Task.DataImport
{
    public class DataBaseCopier
    {
        protected MessageHandler m_Messager;
        protected string m_Datasource;
        protected enumDataType m_DataType;
        protected string m_TargetPath;
        protected bool m_JustCopy;
        protected ESRI.ArcGIS.Geometry.ISpatialReference m_SpatialReference;
        protected string m_DataPrefix;
        private string m_TargetName;

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
        public virtual string TargetName
        {
            set
            {
                m_TargetName = value;
            }
        }

        /// <summary>
        /// 导入
        /// </summary>
        public virtual bool Import()
        {
            IWorkspace wsTarget = null;
            try
            {

                // 直接复制的方式
                if (m_JustCopy)
                {
                    return CopyDirectly();//ref wsSource);
                }

                // 导入的方式 
                string strWorkspace = this.m_TargetPath + "\\" + this.m_TargetName;
                if (!Common.Utility.Esri.AEAccessFactory.OpenFGDB(ref wsTarget, strWorkspace))
                {
                    SendMessage(enumMessageType.Exception, "导入数据失败：无法打开目标库，请确认目标库已经创建");
                    return false;
                }
                IFeatureDataset fdsTarget = CreateFeatureDataset(wsTarget, this.m_SpatialReference);
                if (fdsTarget == null)
                {
                    SendMessage(enumMessageType.Exception, "“Dataset”没有创建成功，无法继续导入");
                    return false;
                }
                Common.Utility.Esri.GPTool gpTool = new Common.Utility.Esri.GPTool();

                // 打开数据源
                try
                {
                    IWorkspace wsSource = AEAccessFactory.OpenWorkspace(this.m_DataType, this.m_Datasource);
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
                                Common.Utility.Esri.DataConverter.ConvertTable(wsSource, wsTarget, dataset, GetObjectName(dataset.Name));
                                break;

                            case esriDatasetType.esriDTFeatureClass:
                                SendEvent(dataset.Name);
                                gpTool.CopyFeatureClass(string.Format("{0}\\{1}", this.m_Datasource, dataset.Name + (this.m_DataType == enumDataType.SHP ? ".shp" : "")), string.Format("{0}\\{1}\\{2}", strWorkspace, fdsTarget.Name, GetObjectName(dataset.Name)));
                                break;

                            case esriDatasetType.esriDTFeatureDataset:
                                IFeatureClassContainer fsContainer = dataset as IFeatureClassContainer;
                                for (int i = 0; i < fsContainer.ClassCount; i++)
                                {
                                    string strFcName = (fsContainer.get_Class(i) as IDataset).Name;
                                    SendEvent(strFcName);
                                    gpTool.CopyFeatureClass(string.Format("{0}\\{1}\\{2}", this.m_Datasource, dataset.Name, strFcName), string.Format("{0}\\{1}\\{2}", strWorkspace, fdsTarget.Name, GetObjectName(strFcName)));
                                }

                                break;

                            default: break;
                        }

                        dataset = enDataset.Next();
                    }

                    // 释放
                    enDataset = null;
                    dataset = null;
                }
                catch (Exception exp)
                {
                    SendMessage(enumMessageType.Exception, "导入数据出错:" + exp.ToString());
                    return false;
                }
                finally
                {
                    if (wsTarget != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(wsTarget);
                        wsTarget = null;
                    }
                }

                return true;
            }
            catch (Exception exp)
            {
                SendMessage(enumMessageType.Exception, "数据导入失败，错误信息：" + exp.Message);

                return false;
            }
        }

        protected void SendMessage(enumMessageType msgType, string strMsg)
        {
            if (m_Messager != null)
            {
                m_Messager(msgType, strMsg);
            }
        }

        private void SendEvent(string strObject)
        {
            if (this.ImportingObjectChanged != null)
                this.ImportingObjectChanged(string.Format("正在导入{0}……", strObject));
        }

        /// <summary>
        /// 复制文件夹，为Shp复制使用
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="strTarget"></param>
        private void CopyDirectory(string strSource, string strTarget, string folderName)
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

        private bool CopyDirectly()//ref IWorkspace wsSource)
        {
            // VCT数据不允许直接复制
            if (m_DataType == enumDataType.VCT)
            {
                SendMessage(enumMessageType.Exception, "导入VCT数据不允许直接复制");

                return false;
            }

            // MDB使用文件复制
            if (m_DataType == enumDataType.PGDB)
            {
                try
                {
                    string strDBPath = this.m_TargetPath + "\\"+this.m_TargetName;
                    System.IO.File.Copy(this.m_Datasource, strDBPath);

                    //if (!Common.Utility.Esri.AEAccessFactory.OpenPGDB(ref wsSource, strDBPath))
                    //{
                    //    SendMessage(enumMessageType.Exception, "导入数据（复制文件）后打开出错，请确认数据源为正确的PGDB文件");
                    //    return false;
                    //}
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
                    if (m_DataType == enumDataType.SHP)
                    {
                        CopyDirectory(this.m_Datasource, this.m_TargetPath, this.m_TargetName);
                    }
                    else
                    {
                        Common.Utility.Esri.GPTool gpTool = new Common.Utility.Esri.GPTool();
                        gpTool.Copy(this.m_Datasource, this.m_TargetPath + "\\" + this.m_TargetName);
                    }

                    //wsSource = AEAccessFactory.OpenWorkspace(m_DataType, this.m_TargetPath + "\\" + this.m_TargetName);

                }
                catch
                {
                    SendMessage(enumMessageType.Exception, "导入数据（复制文件夹）出错");

                    return false;
                }

            }

            return true;
        }
    
        protected virtual IFeatureDataset CreateFeatureDataset(IWorkspace wsTarget, ISpatialReference pSpatialRef)
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
                    ISpatialReferenceResolution pSpatialRefResolution = pSpatialRef as ISpatialReferenceResolution;

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

    }
}
