using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Hy.Esri.Catalog.Utility;
using ESRI.ArcGIS.esriSystem;

namespace Hy.Esri.Catalog.Define
{
    public delegate void CatalogItemEventHandler(ICatalogItem catalogItem);
    public delegate void CatalogItemOperateHandler(ICatalogItem target,bool succeed);
    public interface ICatalogItem
    {
        event CatalogItemEventHandler OnRefresh;
        event CatalogItemOperateHandler OnOpen;

        string Name { get; }

        enumCatalogType Type { get; }

        IWorkspaceCatalogItem WorkspaceItem { get; set; }

        ICatalogItem Parent { get; }

        bool HasChild { get; }

        List<ICatalogItem> Childrens { get; }

        IDataset Dataset { get; }

        IDatasetName DatasetName { get; }

        string GetGpString();

        string GetTELayerString();

        bool Openned { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="refresh">是否刷新（但不引发刷新事件）</param>
        /// <returns></returns>
        bool Open(bool refresh);

        void Refresh();

        object Tag { get; set; }


    }

    public abstract class CatalogItem : ICatalogItem
    {
        protected IDataset m_Dataset;
        protected IDatasetName m_DatasetName;
        protected List<ICatalogItem> m_Children;
        protected ICatalogItem m_Parent;
        protected enumCatalogType m_Type=enumCatalogType.Undefine;

        //protected CatalogItem(IDataset ds, ICatalogItem parent)
        //{
        //    this.m_Dataset = ds;
        //    this.m_Parent = parent;
        //    this.m_Type = GetCatalogType();
        //}

        protected CatalogItem(IDatasetName dsName, ICatalogItem parent)
        {
            this.m_DatasetName = dsName;
            this.m_Parent = parent;
            this.m_Type = GetCatalogType(dsName);
        }

        public virtual string Name
        {
            get
            {
                if (m_DatasetName == null)
                    return null;

                return m_DatasetName.Name;
            }
        }
        public virtual enumCatalogType Type
        {
            get
            {
                return m_Type;                
            }
        }
        public virtual ICatalogItem Parent
        {
            get { return m_Parent; }
        }
        public virtual bool HasChild
        {
            get { return this.Childrens.Count > 0; }
        }
        public virtual IDataset Dataset
        {
            get
            {
                if (m_Dataset == null && m_DatasetName!=null                    )
                    m_Dataset = (m_DatasetName as IName).Open() as IDataset;

                return m_Dataset;
            }
        }
        public virtual IDatasetName DatasetName
        {
            get
            {
                return m_DatasetName;
            }
        }
        public event CatalogItemEventHandler OnRefresh;
        public event CatalogItemOperateHandler OnOpen;

        public abstract List<ICatalogItem> Childrens { get; }
        public abstract string GetGpString();
        public virtual string GetTELayerString()
        {
            return null;
        }
        public virtual bool Open(bool refresh)
        {
            if (refresh)
                m_Children = null;

            bool succeed= this.Childrens != null;
            SendOpenEvent(succeed);

            return succeed;
        }
        protected void SendOpenEvent(bool succeed)
        {
            if (this.OnOpen != null)
                this.OnOpen.Invoke(this, succeed);
        }
        public virtual bool Openned
        {
            get
            {
                return m_Dataset != null;
            }
        }
        public virtual void Refresh()
        {
            // 释放
            if (m_Children != null)
            {
                int count=m_Children.Count;
                for (int i = 0; i < count;i++ )
                {
                    ICatalogItem subItem = m_Children[i];
                    //if (subItem.Openned)
                    //{
                    //    IDataset ds = subItem.Dataset;
                    //    if (ds != null)
                    //    {
                    //        try
                    //        {
                    //            System.Runtime.InteropServices.Marshal.ReleaseComObject(ds);
                    //            ds = null;
                    //        }
                    //        catch
                    //        {
                    //        }
                    //    }
                    //}
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(subItem.DatasetName);
                    subItem.Refresh();
                    subItem = null;
                }
            }
            // 置位，就相当于刷新
            m_Children = null;
            if(m_Dataset!=null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_Dataset);

            SendRefreshEvent();
        }

        protected void SendRefreshEvent()
        {
            if (OnRefresh != null)
                this.OnRefresh.Invoke(this);
        }

        public virtual IWorkspaceCatalogItem WorkspaceItem { get; set; }

        public virtual object Tag { get; set; }

        private enumCatalogType GetCatalogType()
        {
            if (m_Dataset == null)
                return enumCatalogType.Undefine;
            if (m_Dataset is IWorkspace)
                return enumCatalogType.Workpace;

            switch (m_Dataset.Type)
            {
                case esriDatasetType.esriDTFeatureDataset:
                    return enumCatalogType.FeatureDataset;

                case esriDatasetType.esriDTFeatureClass:
                    IFeatureClass fClass = m_Dataset as IFeatureClass;
                    if (fClass.FeatureType == esriFeatureType.esriFTAnnotation || fClass.FeatureType == esriFeatureType.esriFTCoverageAnnotation)
                        return enumCatalogType.FeatureClassAnnotation;

                    switch (fClass.ShapeType)
                    {
                        case esriGeometryType.esriGeometryMultiPatch:
                        case esriGeometryType.esriGeometrySphere:
                        case esriGeometryType.esriGeometryTriangleFan:
                        case esriGeometryType.esriGeometryTriangleStrip:
                        case esriGeometryType.esriGeometryTriangles:
                            return enumCatalogType.FeatureClass3D;

                        case esriGeometryType.esriGeometryPoint:
                        case esriGeometryType.esriGeometryMultipoint:
                            return enumCatalogType.FeatureClassPoint;

                        case esriGeometryType.esriGeometryLine:
                        case esriGeometryType.esriGeometryPolyline:
                            return enumCatalogType.FeatureClassLine;

                        case esriGeometryType.esriGeometryPolygon:
                        case esriGeometryType.esriGeometryBezier3Curve:
                        case esriGeometryType.esriGeometryCircularArc:
                        case esriGeometryType.esriGeometryEllipticArc:
                        case esriGeometryType.esriGeometryEnvelope:
                            return enumCatalogType.FeatureClassArea;

                        default:
                            return enumCatalogType.FeatureClassEmpty;
                    }

                case esriDatasetType.esriDTTable:
                    return enumCatalogType.Table;

                case esriDatasetType.esriDTTerrain:
                    return enumCatalogType.Terrain;
                case esriDatasetType.esriDTTin:
                    return enumCatalogType.Tin;

                case esriDatasetType.esriDTTopology:
                    return enumCatalogType.Topology;

                case esriDatasetType.esriDTRasterCatalog:
                    return enumCatalogType.RasterCatalog;

                case esriDatasetType.esriDTRasterDataset:
                    return enumCatalogType.RasterSet;

                case esriDatasetType.esriDTRasterBand:
                    return enumCatalogType.RasterBand;

                case esriDatasetType.esriDTMosaicDataset:
                    return enumCatalogType.RasterMosaic;

                default:
                    return enumCatalogType.Undefine;
            }
        }

        private enumCatalogType GetCatalogType(IDatasetName dsName)
        {
            if (dsName == null)
                return enumCatalogType.Undefine;
            if (dsName is IWorkspaceName)
                return enumCatalogType.Workpace;

            switch (dsName.Type)
            {
                case esriDatasetType.esriDTFeatureDataset:
                    return enumCatalogType.FeatureDataset;

                case esriDatasetType.esriDTFeatureClass:
                    IFeatureClassName fClassName = dsName as IFeatureClassName;
                    if (fClassName.FeatureType == esriFeatureType.esriFTAnnotation || fClassName.FeatureType == esriFeatureType.esriFTCoverageAnnotation)
                        return enumCatalogType.FeatureClassAnnotation;

                    switch (fClassName.ShapeType)
                    {
                        case esriGeometryType.esriGeometryMultiPatch:
                        case esriGeometryType.esriGeometrySphere:
                        case esriGeometryType.esriGeometryTriangleFan:
                        case esriGeometryType.esriGeometryTriangleStrip:
                        case esriGeometryType.esriGeometryTriangles:
                            return enumCatalogType.FeatureClass3D;

                        case esriGeometryType.esriGeometryPoint:
                        case esriGeometryType.esriGeometryMultipoint:
                            return enumCatalogType.FeatureClassPoint;

                        case esriGeometryType.esriGeometryLine:
                        case esriGeometryType.esriGeometryPolyline:
                            return enumCatalogType.FeatureClassLine;

                        case esriGeometryType.esriGeometryPolygon:
                        case esriGeometryType.esriGeometryBezier3Curve:
                        case esriGeometryType.esriGeometryCircularArc:
                        case esriGeometryType.esriGeometryEllipticArc:
                        case esriGeometryType.esriGeometryEnvelope:
                            return enumCatalogType.FeatureClassArea;

                        default:
                            return enumCatalogType.FeatureClassEmpty;
                    }

                case esriDatasetType.esriDTTable:
                    return enumCatalogType.Table;

                case esriDatasetType.esriDTTerrain:
                    return enumCatalogType.Terrain;
                case esriDatasetType.esriDTTin:
                    return enumCatalogType.Tin;

                case esriDatasetType.esriDTTopology:
                    return enumCatalogType.Topology;

                case esriDatasetType.esriDTRasterCatalog:
                    return enumCatalogType.RasterCatalog;

                case esriDatasetType.esriDTRasterDataset:
                    return enumCatalogType.RasterSet;

                case esriDatasetType.esriDTRasterBand:
                    return enumCatalogType.RasterBand;

                case esriDatasetType.esriDTMosaicDataset:
                    return enumCatalogType.RasterMosaic;

                default:
                    return enumCatalogType.Undefine;
            }
        }
    }

    public interface IWorkspaceCatalogItem:ICatalogItem
    {

        object WorkspacePropertySet { get; }

        enumWorkspaceType WorkspaceType { get; }
    }
}
