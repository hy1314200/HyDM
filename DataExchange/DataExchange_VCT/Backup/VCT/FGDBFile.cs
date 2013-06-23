using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesGDB;
using System.IO;
using ESRI.ArcGIS.Geometry;

namespace DIST.DGP.DataExchange.VCT 
{
    internal class FGDBFile :EsriDataSource
    {
        /// <summary>
		/// 是否是读取
		/// </summary>
		private bool m_bRead;
		/// <summary>
		/// 数据文件名称
		/// </summary>
		private string m_strFilePathName;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bRead">是否是读取</param>
        /// <param name="strFilePathName">数据文件名称</param>
        public FGDBFile(bool bRead, string strFilePathName)
        {
            this.m_bRead = bRead;
            this.m_strFilePathName = strFilePathName;
            base.m_dataType = ArcDataType.FGDB;
            if (this.m_bRead == true)
            {
                //判断MDB文件是否存在
                if (System.IO.Directory.Exists(this.m_strFilePathName) == true)
                {

                   this.Workspace= ConnectWorkspace();
                }
            }
            else
            {
                //判断MDB文件是否存在
                if (System.IO.Directory.Exists(this.m_strFilePathName) == true)
                {
                    //ESRI.ArcGIS.esriSystem.AoInitialize pLicense = new AoInitialize();
                    //pLicense.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngine);
                    //连接PGDB数据源
                    this.Workspace = ConnectWorkspace();
                }
                else
                {
                    //创建PGDB数据源
                    this.Workspace = CreateWorkspace();
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bRead">是否是读取</param>
        /// <param name="strFilePathName">数据文件名称</param>
        /// <param name="pQueryFilter">过滤条件</param>
        ///  <param name="pCutGeometry">裁切范围</param>
        public FGDBFile(bool bRead, string strFilePathName,bool bCut,IGeometry pCutGeometry)
        {
            this.m_bRead = bRead;
            this.m_strFilePathName = strFilePathName;
            base.m_dataType = ArcDataType.FGDB;
            base.m_CutGeometry = pCutGeometry;
            base.m_bCut = bCut;
            if (this.m_bRead == true)
            {
                //判断MDB文件是否存在
                if (System.IO.Directory.Exists(this.m_strFilePathName) == true)
                {

                    this.Workspace = ConnectWorkspace();
                }
            }
            else
            {
                //判断MDB文件是否存在
                if (System.IO.Directory.Exists(this.m_strFilePathName) == true)
                {
                    //ESRI.ArcGIS.esriSystem.AoInitialize pLicense = new AoInitialize();
                    //pLicense.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngine);
                    //连接PGDB数据源
                    this.Workspace = ConnectWorkspace();
                }
                else
                {
                    //创建PGDB数据源
                    this.Workspace = CreateWorkspace();
                }
            }
        }
        ~FGDBFile()
        {

		}

        public virtual void Dispose()
        {
            base.Dispose();
        }

		/// <summary>
		/// 连接GDB数据源
		/// </summary>
		private IWorkspace ConnectWorkspace()
        {
            IWorkspaceFactory factory = new FileGDBWorkspaceFactoryClass();
            try
            {
                return factory.OpenFromFile(m_strFilePathName, 0);
            }
            catch (Exception ex)
            {
                //ProjectData.SetProjectError(exception1);
                LogAPI.WriteErrorLog(ex);
                //ProjectData.ClearProjectError();
            }
            return null;
		}

		/// <summary>
		/// 创建PGDB数据源
		/// </summary>
		private IWorkspace CreateWorkspace()
        {
            IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
            int nIndex = m_strFilePathName.LastIndexOf("\\");
            string sPath = m_strFilePathName.Remove(nIndex);
            string sName = m_strFilePathName.Substring(nIndex + 1) ;
            IWorkspaceName workspaceName = workspaceFactory.Create(sPath,
                sName, null, 0);

            // Cast the workspace name object to the IName interface and open the workspace.
            IName name = (IName)workspaceName;
            IWorkspace workspace = (IWorkspace)name.Open();
            return workspace;

		}
    }
}
