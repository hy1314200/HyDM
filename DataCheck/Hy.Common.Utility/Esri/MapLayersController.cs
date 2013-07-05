using System;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.Carto;
using System.Runtime.InteropServices;

namespace Common.Utility.Esri
{
    /// <summary>
    /// 地图图层控制器
    /// </summary>
    public class MapLayersController
    {
        private IMap m_Map = null;

        public MapLayersController(IMap currentMap)
        {
            m_Map = currentMap;
        }

        /// <summary>
        /// 设置所有图层的可见性
        /// </summary>
        /// <param name="bvisible"></param>
        public void SetAllLayersVisible(bool bvisible)
        {
            if (m_Map.LayerCount == 0) return;
            for (int i = 0; i < m_Map.LayerCount; i++)
            {
                if (m_Map.get_Layer(i) is ICompositeLayer)
                {
                    SetCompositeLayerVisible(m_Map.get_Layer(i), bvisible);
                    m_Map.get_Layer(i).Visible = bvisible;
                }
                else
                {
                    if (!m_Map.get_Layer(i).Valid)
                    {
                        continue;
                    }
                    m_Map.get_Layer(i).Visible = bvisible;
                }
            }
        }

        /// <summary>
        /// 设置复合图层（ICompositeLayer）是否可见
        /// </summary>
        /// <param name="pLayer">复合图层</param>
        /// <param name="visible">是否可见，可见则为true，不可见则为false</param>
        public void SetCompositeLayerVisible(ILayer pLayer, bool visible)
        {
            ICompositeLayer pComLayer = (ICompositeLayer)pLayer;
            //pLayer.Visible = visible;
            for (int i = 0; i < pComLayer.Count; i++)
            {
                if (pComLayer.get_Layer(i) is ICompositeLayer)
                {
                    SetCompositeLayerVisible(pComLayer.get_Layer(i), visible);
                }
                else
                {
                    if (!pComLayer.get_Layer(i).Valid)
                    {
                        continue;
                    }
                    pComLayer.get_Layer(i).Visible = visible;
                }
            }
        }

        /// <summary>
        /// 设置某个图层是否可见
        /// </summary>
        /// <param name="lyrName">图层名</param>
        /// <param name="visible">是否可见</param>
        public void SetLayerVisible(string lyrName, bool visible)
        {
            if (string.IsNullOrEmpty(lyrName))
                return;

            for (int i = 0; i < m_Map.LayerCount; i++)
            {
                //else if (pLayer.Name == lyrName)
                if(lyrName.Contains(m_Map.get_Layer(i).Name))
                {
                    m_Map.get_Layer(i).Visible = visible;
                }
                else if(lyrName.Equals(m_Map.get_Layer(i).Name,StringComparison.OrdinalIgnoreCase))
                {
                    m_Map.get_Layer(i).Visible = visible;
                }

                if (m_Map.get_Layer(i) is ICompositeLayer)
                {
                    SetCompositeLayerVisible(m_Map.get_Layer(i), lyrName,visible);
                }
            }
        }
        
        /// <summary>
        /// 设置某个图层是否可见
        /// </summary>
        /// <param name="classID"></param>
        /// <param name="visible"></param>
        public void SetLayerVisible(int lyrIndex, bool visible)
        {
            ILayer pLayer = m_Map.get_Layer(lyrIndex);
            try
            {
                if (pLayer == null)
                {
                    return;
                }

                if (pLayer is ICompositeLayer)
                {
                    SetCompositeLayerVisible(pLayer, visible);
                }

                pLayer.Visible = visible;
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

            }
            finally
            {
                if (pLayer!=null)
                {
                    Marshal.ReleaseComObject(pLayer);
                }
            }
        }

        public ILayer GetLayer(string lyrName)
        {
            if (m_Map.LayerCount == 0) return null;

            ILayer pLayer = null;

            for (int j = 0; j < m_Map.LayerCount; j++)
            {
                ILayer layer = m_Map.get_Layer(j);
                if (layer is ICompositeLayer)
                {
                    pLayer = GetLayerForCompositeLayer(layer, lyrName);
                    if (pLayer != null)
                    {
                        return pLayer;
                    }
                }
                else
                {
                    if (m_Map.get_Layer(j).Name.Equals(lyrName, StringComparison.OrdinalIgnoreCase))
                    {
                        pLayer = m_Map.get_Layer(j);
                        return pLayer;
                    }
                }
            }
            return pLayer;
        }

        public ILayer GetLayer(string lyrName, out int classID)
        {
            int id = -1;

            if (m_Map.LayerCount == 0)
            {
                classID = id;
                return null;
            }

            ILayer pLayer = GetLayer(lyrName);
            
            if(pLayer!=null)
            {
                classID = (pLayer as IFeatureLayer).FeatureClass.FeatureClassID;
                return pLayer;
            }
            else
            {
                classID = id;
                return null;
            }
        }

        private ILayer GetLayerForCompositeLayer(ILayer layer,string lyrName)
        {
            ILayer pLayer = null;
            ICompositeLayer pComLayer = (ICompositeLayer)layer;

            for (int i = 0; i < pComLayer.Count; i++)
            {
                if (pComLayer.get_Layer(i) is ICompositeLayer)
                {
                    pLayer=GetLayerForCompositeLayer(pComLayer.get_Layer(i), lyrName);
                    if (pLayer != null)
                    {
                        return pLayer;
                    }
                }
                else
                {
                    if (pComLayer.get_Layer(i).Name.Equals(lyrName, StringComparison.OrdinalIgnoreCase))
                    {
                        pLayer = pComLayer.get_Layer(i);
                        return pLayer;
                    }
                }
            }
            return pLayer;
        }

        /// <summary>
        /// 设置复合图层（ICompositeLayer）是否可见
        /// </summary>
        /// <param name="pLayer">复合图层</param>
        /// <param name="visible">是否可见，可见则为true，不可见则为false</param>
        public void SetCompositeLayerVisible(ILayer pLayer, string lyrName,bool visible)
        {
            ICompositeLayer pComLayer = (ICompositeLayer)pLayer;
            //pLayer.Visible = visible;
            for (int i = 0; i < pComLayer.Count; i++)
            {
                if (pComLayer.get_Layer(i) is ICompositeLayer)
                {
                    SetCompositeLayerVisible(pComLayer.get_Layer(i),lyrName, visible);
                }
                else
                {
                    if (!pComLayer.get_Layer(i).Valid)
                    {
                        continue;
                    }

                    if (lyrName.Contains(pComLayer.get_Layer(i).Name))
                    {
                        pComLayer.get_Layer(i).Visible = visible;
                        pLayer.Visible = visible;
                    }
                    else if (lyrName.Equals(pComLayer.get_Layer(i).Name, StringComparison.OrdinalIgnoreCase))
                    {
                        pComLayer.get_Layer(i).Visible = visible;
                        pLayer.Visible = visible;
                    }
                }
            }
        }

        /// <summary>
        /// 获取此图层的group图层
        /// </summary>
        /// <param name="lyrName">Name of the layer.</param>
        /// <returns></returns>
        public ICompositeLayer GetCompositeLayer(string lyrName)
        {
            if (m_Map.LayerCount == 0) return null;
            if (string.IsNullOrEmpty(lyrName))  return null;

            ICompositeLayer compositeLayer = null;

            for (int i = 0; i < m_Map.LayerCount; i++)
            {
                if (m_Map.get_Layer(i) is ICompositeLayer)
                {
                    compositeLayer = (ICompositeLayer)m_Map.get_Layer(i);
                    //pLayer.Visible = visible;
                    for (int j = 0; j< compositeLayer.Count; j++)
                    {
                        if(compositeLayer.get_Layer(j).Name.Equals(lyrName,StringComparison.OrdinalIgnoreCase))
                        {
                            return compositeLayer;
                        }
                    }
                }
            }
            return compositeLayer;
        }


        /// <summary>
        /// 取得图层的上一级对象，可能是igrouplayer,或ibasicmap
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public object GetLayerParent(ILayer layer)
        {
            ILayer tmpLayer;
            IBasicMap map = m_Map as IBasicMap;
            for (int i = 0; i <= m_Map.LayerCount - 1; i++)
            {
                tmpLayer = map.get_Layer(i);
                if (tmpLayer == layer)
                {
                    return map;
                }
                else if (tmpLayer is IGroupLayer)
                {
                    IGroupLayer ly = FindParentGroupLayer(tmpLayer as IGroupLayer, layer);
                    if (ly != null)
                        return ly;
                }
            }
            return map;
        }

        public IGroupLayer FindParentGroupLayer(IGroupLayer groupLayer, ILayer layer)
        {
            if (!(groupLayer is ICompositeLayer))
            {
                return groupLayer;
            }

            ICompositeLayer comLayer = groupLayer as ICompositeLayer;
            ILayer tmpLayer;
            for (int i = 0; i <= comLayer.Count - 1; i++)
            {
                tmpLayer = comLayer.get_Layer(i);
                if (tmpLayer == layer)
                    return groupLayer;
                else if (tmpLayer is IGroupLayer)
                    return FindParentGroupLayer(tmpLayer as IGroupLayer, layer);
            }
            return null;
        }
    }
}
