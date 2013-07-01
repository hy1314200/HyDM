using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Common.Utility.Esri;
using Check.Define;

namespace Check.Task.DataImport
{
    /// <summary>
    /// 未指定空间参考（而通过指定空间参考来源图层）的数据导入实现
    /// 继承于DefaultDataImport，包括PGDB，FileGDB，SHP的实现
    /// </summary>
    public class NoReferenceDataImport:DefaultDataImport
    {
        /// <summary>
        /// 指定创建Dataset时空间参考的来源图层（FeatureClass或Dataset）名
        /// </summary>
        public virtual string ReferenceLayer { set; protected get; }

        protected override IFeatureDataset CreateFeatureDataset(IWorkspace wsTarget,ISpatialReference pSpatialRef)
        {
            if (wsTarget == null)
                return null;

            ISpatialReference spatialRef = pSpatialRef;
            if (spatialRef == null)
            {
                if (!string.IsNullOrEmpty(this.ReferenceLayer))
                {
                    IWorkspace wsSource = AEAccessFactory.OpenWorkspace(this.m_DataType, this.m_Datasource);
                    IGeoDataset geoDataset = null;
                    // Shp判断使用Try Catch
                    if (this.m_DataType == enumDataType.SHP)
                    {
                        try
                        {  
                            geoDataset = (wsSource as IFeatureWorkspace).OpenFeatureClass(this.ReferenceLayer) as IGeoDataset;
                        }
                        catch
                        {
                        }
                    }
                    else
                    {

                        if ((wsSource as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, this.ReferenceLayer))
                        {
                            geoDataset = (wsSource as IFeatureWorkspace).OpenFeatureClass(this.ReferenceLayer) as IGeoDataset;
                        }
                        else if ((wsSource as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureDataset, this.ReferenceLayer))
                        {
                            geoDataset = (wsSource as IFeatureWorkspace).OpenFeatureDataset(this.ReferenceLayer) as IGeoDataset;
                        }
                    }

                    if (geoDataset != null)
                    {
                        spatialRef = geoDataset.SpatialReference;
                    }
                    else
                    {
                        SendMessage(enumMessageType.Exception, "NoReferenceDataImport调用错误：数据源中未找到指定的空间参考图层，将按未知参考创建“Dataset”");
                    }
                }
                else
                {
                    SendMessage(enumMessageType.Exception, "NoReferenceDataImport调用错误：未指定空间参考图层，将按未知参考创建“Dataset”");
                }
            }

            return base.CreateFeatureDataset(wsTarget, spatialRef);
        }
    }
}
