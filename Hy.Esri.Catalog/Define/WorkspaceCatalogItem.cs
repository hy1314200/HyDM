using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using Hy.Esri.Utility;

namespace Hy.Esri.Catalog.Define
{
    public class WorkspaceCatalogItem : CatalogItem,IWorkspaceCatalogItem
    {
        private enumWorkspaceType m_WorkspaceType;
        private object m_WorkapcePropertySet;
        private string m_Caption;

        //public WorkspaceCatalogItem(IDataset dsWorkspace,ICatalogItem parent):base(dsWorkspace,parent)
        //{
        //    if (!(dsWorkspace is IWorkspace))
        //        throw new Exception("内部错误：WorksapceCatalogItem 的构造参数必须为Workspace");

        //    this.m_Dataset = dsWorkspace;
        //    m_Caption = this.m_Dataset.BrowseName ?? this.m_Dataset.Name;
        //    IWorkspace wsSource=(dsWorkspace as IWorkspace);
        //    m_WorkapcePropertySet = wsSource.ConnectionProperties;
        //    m_WorkspaceType = (wsSource.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace ? enumWorkspaceType.SDE :
        //        (wsSource.Type == esriWorkspaceType.esriFileSystemWorkspace ? enumWorkspaceType.File : enumWorkspaceType.Unknown));
        //}

        public WorkspaceCatalogItem(IDatasetName dsName, ICatalogItem parent)
            : base(dsName, parent)
        {
            if (!(dsName is IWorkspaceName))
                throw new Exception("内部错误：WorksapceCatalogItem 的构造参数必须为WorkspaceName");

            //this.m_Dataset = (dsName as IName).Open() as IDataset;
            this.m_DatasetName = dsName;
            m_Caption = this.m_DatasetName.WorkspaceName.BrowseName ?? this.m_DatasetName.Name;
            IWorkspaceName wsSource = this.m_DatasetName.WorkspaceName;
            m_WorkapcePropertySet = wsSource.ConnectionProperties;
            //m_WorkspaceType = (wsSource.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace ? enumWorkspaceType.SDE :
            //    (wsSource.Type == esriWorkspaceType.esriFileSystemWorkspace ? enumWorkspaceType.File : enumWorkspaceType.Unknown));
            if (wsSource.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
            {
                m_WorkspaceType = enumWorkspaceType.SDE;
            }
            else if (wsSource.Type == esriWorkspaceType.esriFileSystemWorkspace)
            {
                m_WorkspaceType = enumWorkspaceType.File;
            }
            else
            {
                string strPath = wsSource.PathName;
                string strType = System.IO.Path.GetExtension(strPath);
                if (string.IsNullOrEmpty(strType))
                    m_WorkspaceType = enumWorkspaceType.Unknown;

                strType = strType.ToUpper();
                if (strType == "MDB")
                    m_WorkspaceType = enumWorkspaceType.PGDB;
                else if (strType == "GDB")
                    m_WorkspaceType = enumWorkspaceType.FileGDB;
                else
                    m_WorkspaceType = enumWorkspaceType.Unknown;
            }

        }

        public WorkspaceCatalogItem(object objWorksapceConnction, enumWorkspaceType wsType, ICatalogItem parent,string strName):base(null,parent)
        {
            this.m_WorkapcePropertySet = objWorksapceConnction;
            this.m_WorkspaceType = wsType;
            this.m_Caption = strName;
        }
        
        public override string Name
        {
            get
            {
                return m_Caption;
            }
        }

        public override IDataset Dataset
        {
            get
            {
                if (this.m_Dataset == null)
                {
                    this.m_Dataset = Hy.Esri.Utility.WorkspaceHelper.OpenWorkspace(this.m_WorkspaceType, this.m_WorkapcePropertySet) as IDataset;
                }

                return this.m_Dataset;
            }
        }

        public override ICatalogItem Parent
        {
            get { return null; }
        }

        public override enumCatalogType Type
        {
            get
            {
                return enumCatalogType.Workpace;
            }
        }

        public override List<ICatalogItem> Childrens
        {
            get {
                if (m_Children == null)
                {
                    IWorkspace wsCurrent=this.Dataset as IWorkspace;
                    if (wsCurrent == null)
                    {
                        //throw new Exception("无法打开当前Worksapce");
                    }
                    else
                    {
                        m_Children = new List<ICatalogItem>();
                        IEnumDatasetName enDatasetName = wsCurrent.get_DatasetNames(esriDatasetType.esriDTAny);
                        IDatasetName dsNameSub = enDatasetName.Next();
                        while (dsNameSub != null)
                        {
                            ICatalogItem subItem = CatalogItemFactory.CreateCatalog(dsNameSub, this,this);
                            if (subItem != null)
                                m_Children.Add(subItem);                            

                            dsNameSub = enDatasetName.Next();
                        }
                    }
                }

                return m_Children;
            }
        }

        
        public override bool HasChild
        {
            get
            {
                // 对Workspace，我们认为它是有子项的
                return true;
            }
        }

        public override string GetGpString()
        {
            return Utility.WorkspaceHelper.GetGpString(this.Dataset as IWorkspace);
        }

        public override IWorkspaceCatalogItem WorkspaceItem { get { return this; } set { } }

        public bool Open()
        {
            if (this.m_Dataset == null)
            {
                this.m_Dataset = Hy.Esri.Utility.WorkspaceHelper.OpenWorkspace(this.m_WorkspaceType, this.m_WorkapcePropertySet) as IDataset;

                return this.m_Dataset != null; 
            }
            return true;
        }

        public object WorkspacePropertySet
        {
            get
            {
                return m_WorkapcePropertySet;
            }
        }

        public enumWorkspaceType WorkspaceType
        {
            get
            {
                return m_WorkspaceType;
            }
        }

    }
}
