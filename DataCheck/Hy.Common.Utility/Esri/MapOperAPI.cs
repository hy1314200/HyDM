using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Controls;
using System.Collections;

namespace Hy.Common.Utility.Esri
{
    public class MapOperAPI
    {

        /// <summary>
        /// 聚焦到目标地物
        /// </summary>
        /// <param name="pMapCtrl">地图控件control</param>
        /// <param name="pGeo">地物</param>
        public static void ZoomToFeature(AxMapControl pMapCtrl, IGeometry pGeo)
        {

            if (pGeo == null || pGeo.IsEmpty)
            {
                return;
            }
            IEnvelope pEnvelope = pGeo.Envelope;

            //pMapCtrl.ActiveView.Extent = pEnvelope;
            //如果是点类型则集中显示
            if (pGeo.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                //IEnvelope pEnv = pMapCtrl.ActiveView.FullExtent;
                //pEnv.Expand(0.005, 0.005, false);
                //pMapCtrl.ActiveView.Extent = pEnv;

                IPoint pPoint = (IPoint)pGeo;
                pMapCtrl.CenterAt(pPoint);
                pMapCtrl.MapScale = 2000;

            }
            else if (pGeo.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                if (!pEnvelope.IsEmpty)
                {
                    pEnvelope.Expand(3, 3, true);
                    pMapCtrl.ActiveView.Extent = pEnvelope;
                }
                else
                {
                    IPointCollection pPntColl = pGeo as IPointCollection;
                    IPoint pt = pPntColl.get_Point(1);
                    pMapCtrl.CenterAt(pt);
                    pMapCtrl.MapScale = 2000;
                }
            }
            else if (pGeo.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                pEnvelope.Expand(2, 2, true);
                pMapCtrl.ActiveView.Extent = pEnvelope;
            }

            //pMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            pMapCtrl.ActiveView.Refresh();
            pMapCtrl.ActiveView.ScreenDisplay.UpdateWindow();
            FlashGeometry(pMapCtrl.Object as IMapControl4, pGeo);
            //pMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            //pMapCtrl.ActiveView.ScreenDisplay.UpdateWindow();
        }

        /// <summary>
        /// 聚焦到目标地物
        /// </summary>
        /// <param name="pActiveView">地图视图</param>
        /// <param name="pFtLayer">图层</param>
        /// <param name="nOID">地物ID</param>
        public static void AddElement(IActiveView pActiveView, IFeatureLayer pFtLayer, int nOID)
        {
            IFeatureClass pFtCls = pFtLayer.FeatureClass;
            IFeature pFt = pFtCls.GetFeature(nOID);
            if (pFt == null)
            {
                return;
            }
            IGeometry pGeo = pFt.Shape;
            if (pGeo == null)
            {
                return;
            }
            IEnvelope pEnvelope = pGeo.Envelope;

            IGraphicsContainer pGraphContainer = pActiveView.GraphicsContainer;
            //根据拓扑错误图形的几何类型，来创建相应的element
            IElement ipElement = null;
            RgbColor ipColor = new RgbColor();
            if (pGeo.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                IMarkerElement ipMarkElement = new MarkerElementClass();
                ipElement = (IElement)ipMarkElement;
                ipElement.Geometry = pGeo;
                ISimpleMarkerSymbol ipMarkerSymbol = new SimpleMarkerSymbolClass();
                ipColor.Red = 255;
                ipColor.Blue = 0;
                ipColor.Green = 0;
                ipMarkerSymbol.Color = (IColor)ipColor;
                ipMarkElement.Symbol = ipMarkerSymbol;
            }
            else if (pGeo.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                ILineElement ipLineElement = new LineElementClass();
                ipElement = (IElement)ipLineElement;
                ipElement.Geometry = pGeo;
                ISimpleLineSymbol ipLineSymbol = new SimpleLineSymbolClass();
                ipColor.Red = 255;
                ipColor.Blue = 0;
                ipColor.Green = 0;
                ipLineSymbol.Color = (IColor)ipColor;
                ipLineSymbol.Width = 2.0;
                ipLineElement.Symbol = ipLineSymbol;
            }
            else if (pGeo.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                IPolygonElement ipPolygonElement = new PolygonElementClass();
                ipElement = (IElement)ipPolygonElement;
                ipElement.Geometry = pGeo;

                ISimpleFillSymbol ipFillSymbol = new SimpleFillSymbolClass();
                ILineSymbol ipLineSymbol = ipFillSymbol.Outline;
                ipLineSymbol.Width = 2.0;
                ipColor.Red = 255;
                ipColor.Blue = 0;
                ipColor.Green = 0;
                ipLineSymbol.Color = (IColor)ipColor;
                ipFillSymbol.Outline = ipLineSymbol;
                IFillShapeElement pFillElement = (IFillShapeElement)ipPolygonElement;
                ipFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow;
                pFillElement.Symbol = ipFillSymbol;
            }

            pGraphContainer.DeleteAllElements();
            pGraphContainer.AddElement(ipElement, 0);

            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            pActiveView.ScreenDisplay.UpdateWindow();
        }

        /// <summary>
        /// 聚焦到目标地物
        /// </summary>
        /// <param name="pMapCtrl">地图控件control</param>
        /// <param name="pFt">地物</param>
        public static void ZoomToElement(AxMapControl pMapCtrl, IGeometry pGeo)
        {

            if (pGeo == null || pGeo.IsEmpty)
            {
                return;
            }
            IEnvelope pEnvelope = pGeo.Envelope;

            IGraphicsContainer pGraphContainer = pMapCtrl.ActiveView.GraphicsContainer;
            //根据拓扑错误图形的几何类型，来创建相应的element
            IElement ipElement = null;
            RgbColor ipColor = new RgbColor();
            if (pGeo.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                IMarkerElement ipMarkElement = new MarkerElementClass();
                ipElement = (IElement)ipMarkElement;
                ipElement.Geometry = pGeo;
                ISimpleMarkerSymbol ipMarkerSymbol = new SimpleMarkerSymbolClass();
                ipColor.Red = 0;
                ipColor.Blue = 255;
                ipColor.Green = 0;

                RgbColor ipColor1 = new RgbColor();

                ipColor1.NullColor = true;
                ipMarkerSymbol.Color = (IColor)ipColor1;
                ipMarkerSymbol.OutlineColor = (IColor)ipColor;
                ipMarkerSymbol.Outline = true;
                ipMarkerSymbol.OutlineSize = 2;
                ipMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                ipMarkerSymbol.Size = 14;
                ipMarkElement.Symbol = ipMarkerSymbol;
            }
            else if (pGeo.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                ILineElement ipLineElement = new LineElementClass();
                ipElement = (IElement)ipLineElement;
                ipElement.Geometry = pGeo;
                ISimpleLineSymbol ipLineSymbol = new SimpleLineSymbolClass();
                ipColor.Red = 255;
                ipColor.Blue = 0;
                ipColor.Green = 0;
                ipLineSymbol.Color = (IColor)ipColor;
                ipLineSymbol.Width = 2.0;
                ipLineElement.Symbol = ipLineSymbol;
            }
            else if (pGeo.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                IPolygonElement ipPolygonElement = new PolygonElementClass();
                ipElement = (IElement)ipPolygonElement;
                ipElement.Geometry = pGeo;
                ISimpleFillSymbol ipFillSymbol = new SimpleFillSymbolClass();
                ILineSymbol ipLineSymbol = ipFillSymbol.Outline;
                ipLineSymbol.Width = 2.0;
                ipColor.Red = 255;
                ipColor.Blue = 0;
                ipColor.Green = 0;
                ipLineSymbol.Color = (IColor)ipColor;
                ipFillSymbol.Outline = ipLineSymbol;
                IFillShapeElement pFillElement = (IFillShapeElement)ipPolygonElement;
                ipFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow;
                pFillElement.Symbol = ipFillSymbol;
            }

            //pGraphContainer.DeleteAllElements();

            pGraphContainer.AddElement(ipElement, 0);

            //pMapCtrl.ActiveView.Extent = pEnvelope;
            //如果是点类型则集中显示
            if (pGeo.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                //IEnvelope pEnv = pMapCtrl.ActiveView.FullExtent;
                //pEnv.Expand(0.005, 0.005, false);
                //pMapCtrl.ActiveView.Extent = pEnv;

                IPoint pPoint = (IPoint)pGeo;
                pMapCtrl.CenterAt(pPoint);
                pMapCtrl.MapScale = 2000;

            }
            else if (pGeo.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                if (!pEnvelope.IsEmpty)
                {
                    pEnvelope.Expand(3, 3, true);
                    pMapCtrl.ActiveView.Extent = pEnvelope;
                }
                else
                {
                    IPointCollection pPntColl = pGeo as IPointCollection;
                    IPoint pt = pPntColl.get_Point(1);
                    pMapCtrl.CenterAt(pt);
                    pMapCtrl.MapScale = 2000;
                }
            }
            else if (pGeo.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                pEnvelope.Expand(2, 2, true);
                pMapCtrl.ActiveView.Extent = pEnvelope;
            }

            //pMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            pMapCtrl.ActiveView.Refresh();
            pMapCtrl.ActiveView.ScreenDisplay.UpdateWindow();
            FlashGeometry(pMapCtrl.Object as IMapControl4, pGeo);
            //pMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            //pMapCtrl.ActiveView.ScreenDisplay.UpdateWindow();
        }

        /// <summary>
        /// 设置IGeometry的ISymbol
        /// </summary>
        /// <param name="pGeo"></param>
        /// 函数需要改进
        public static void SetGeoColor(IGeometry pGeo, IGraphicsContainer pGraphContainer, string strElementName)
        {
            IElement ipElement = null;

            if (pGeo.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                ILineElement ipLineElement = new LineElementClass();
                RgbColor ipColor = new RgbColor();
                ipElement = (IElement)ipLineElement;
                ipElement.Geometry = pGeo;
                ISimpleFillSymbol ipFillSymbol = new SimpleFillSymbolClass();
                ILineSymbol ipLineSymbol = ipFillSymbol.Outline;
                ipLineSymbol.Width = 2.0;
                ipColor.Red = 0;
                ipColor.Blue = 0;
                ipColor.Green = 255;
                ipLineSymbol.Color = (IColor)ipColor;
                ipLineElement.Symbol = ipLineSymbol;
                ((IElementProperties)ipElement).Name = strElementName;
                pGraphContainer.AddElement(ipElement, 0);
            }
            else if (pGeo.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                RgbColor ipColor = new RgbColor();
                IPolygonElement ipPolygonElement = new PolygonElementClass();
                ipElement = (IElement)ipPolygonElement;
                ipElement.Geometry = pGeo;
                ISimpleFillSymbol ipFillSymbol = new SimpleFillSymbolClass();
                ILineSymbol ipLineSymbol = ipFillSymbol.Outline;
                ipLineSymbol.Width = 2.0;
                ipColor.Red = 0;
                ipColor.Blue = 0;
                ipColor.Green = 255;
                ipLineSymbol.Color = (IColor)ipColor;
                ipFillSymbol.Outline = ipLineSymbol;
                IFillShapeElement pFillElement = (IFillShapeElement)ipPolygonElement;
                ipFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow;
                pFillElement.Symbol = ipFillSymbol;
                ((IElementProperties)ipElement).Name = strElementName;
                pGraphContainer.AddElement(ipElement, 0);
            }
        }

        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <param name="pGeo">The p geo.</param>
        /// <param name="strElementName">Name of the STR element.</param>
        /// <returns></returns>
        /// 函数需要改进
        private static IElement GetElement(IGeometry pGeo, string strElementName)
        {

            IElement ipElement = null;

            if (pGeo.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                ILineElement ipLineElement = new LineElementClass();
                RgbColor ipColor = new RgbColor();
                ipElement = (IElement)ipLineElement;
                ipElement.Geometry = pGeo;
                ISimpleFillSymbol ipFillSymbol = new SimpleFillSymbolClass();
                ILineSymbol ipLineSymbol = ipFillSymbol.Outline;
                ipLineSymbol.Width = 0.5;
                ipColor.Red = 255;
                ipColor.Blue = 0;
                ipColor.Green = 0;
                ipLineSymbol.Color = (IColor)ipColor;
                ipLineElement.Symbol = ipLineSymbol;
            }
            else if (pGeo.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                RgbColor ipColor = new RgbColor();
                IPolygonElement ipPolygonElement = new PolygonElementClass();
                ipElement = (IElement)ipPolygonElement;
                ipElement.Geometry = pGeo;
                ISimpleFillSymbol ipFillSymbol = new SimpleFillSymbolClass();
                ILineSymbol ipLineSymbol = ipFillSymbol.Outline;
                ipLineSymbol.Width = 1.0;
                ipColor.Red = 255;
                ipColor.Blue = 0;
                ipColor.Green = 0;
                ipLineSymbol.Color = (IColor)ipColor;
                ipFillSymbol.Outline = ipLineSymbol;
                IFillShapeElement pFillElement = (IFillShapeElement)ipPolygonElement;
                ipFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow;
                pFillElement.Symbol = ipFillSymbol;
            }

            if (ipElement != null)
            {
                ((IElementProperties)ipElement).Name = strElementName;
            }
            return ipElement;
        }
        
        /// <summary>
        /// 设置IGeometry的ISymbol
        /// </summary>
        /// <param name="pGeo"></param>
        /// 函数需要改进
        public static void SetGeoColor(IGeometry pGeo, IGraphicsContainer pGraphContainer, string strRasterName, ref Hashtable hashElementList)
        {
            IElement ipElement = GetElement(pGeo, strRasterName);

                ((IElementProperties)ipElement).Name = strRasterName;

                pGraphContainer.AddElement(ipElement, 0);

                if (!hashElementList.Contains(strRasterName))
                {
                    hashElementList.Add(strRasterName, ipElement);
                }
        }

        /// <summary>
        /// 将Envelope转换为多边形
        /// </summary>
        /// <param name="layerExtent">Envelope</param>
        /// <param name="pPolygon">结果多边形</param>
        /// <returns>操作结果</returns>
        public static bool ExchangeToPolygon(IEnvelope layerExtent, ref IPolygon4 pPolygon)
        {
            try
            {
                object Missing = Type.Missing;
                IPoint pPoint = new PointClass();
                IPointCollection pPointCol = new PolygonClass();
                pPoint.PutCoords(layerExtent.XMin, layerExtent.YMax);
                pPointCol.AddPoint(pPoint, ref Missing, ref Missing);
                pPoint.PutCoords(layerExtent.XMax, layerExtent.YMax);
                pPointCol.AddPoint(pPoint, ref Missing, ref Missing);
                pPoint.PutCoords(layerExtent.XMax, layerExtent.YMin);
                pPointCol.AddPoint(pPoint, ref Missing, ref Missing);
                pPoint.PutCoords(layerExtent.XMin, layerExtent.YMin);
                pPointCol.AddPoint(pPoint, ref Missing, ref Missing);
                pPoint.PutCoords(layerExtent.XMin, layerExtent.YMax);
                pPointCol.AddPoint(pPoint, ref Missing, ref Missing);
                pPolygon = pPointCol as IPolygon4;
                return true;
            }
            catch(Exception exp)
            {
                Hy.Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
        }

        /// <summary>
        /// 闪烁地物
        /// </summary>
        /// <param name="pMapControl"></param>
        /// <param name="pGeometry"></param>
        public static void FlashGeometry(IMapControl4 pMapControl, IGeometry pGeometry)
        {
            ICartographicLineSymbol ipCartographicLineSymbol;
            ISimpleFillSymbol ipSimpleFillSymbol;
            ISimpleMarkerSymbol ipSimpleMarkersymbol;
            ISymbol ipSymbol = null;
            IRgbColor ipColor;
            int Size;

            ipColor = new RgbColor();
            ipColor.Blue = 255;
            Size = 2;

            esriGeometryType type = pGeometry.GeometryType;

            if (type == esriGeometryType.esriGeometryPolyline)
            {
                ipCartographicLineSymbol = new CartographicLineSymbol();
                ipSymbol = (ISymbol)ipCartographicLineSymbol;
                ipSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
                ipCartographicLineSymbol.Width = Size;
                ipCartographicLineSymbol.Color = ipColor;
                ipCartographicLineSymbol.Cap = esriLineCapStyle.esriLCSRound;
            }
            else if (type == esriGeometryType.esriGeometryPolygon)
            {
                ipSimpleFillSymbol = new SimpleFillSymbol();
                ipSymbol = (ISymbol)ipSimpleFillSymbol;
                ipSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
                ipColor.Red = 0;
                ipColor.Blue = 255;
                ipColor.Green = 0;
                ipSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
                ipSimpleFillSymbol.Color = ipColor;
            }
            else if (type == esriGeometryType.esriGeometryPoint || type == esriGeometryType.esriGeometryMultipoint)
            {
                ipSimpleMarkersymbol = new SimpleMarkerSymbol();
                ipSymbol = (ISymbol)ipSimpleMarkersymbol;
                ipSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
                ipSimpleMarkersymbol.Color = ipColor;
                ipSimpleMarkersymbol.Size = 8;
            }
            pMapControl.FlashShape(pGeometry, 2, 100, ipSymbol);
        }

        /// <summary>根据选中的要素从图层组中取得目标图层
        /// <param name="pLayer">图层组</param>
        /// <returns>目标图层</returns>
        public static ILayer GetLayerFromComposit(ILayer pLayer)
        {
            ICompositeLayer pComLayer = (ICompositeLayer)pLayer;
            ILayer tempLyr = null;
            IFeatureLayer tempFeatLyr = null;
            IFeatureSelection tempFeatSlctn = null;
            for (int i = 0; i < pComLayer.Count; i++)
            {
                tempLyr = pComLayer.get_Layer(i);
                if (tempLyr is ICompositeLayer)
                {
                    tempLyr = GetLayerFromComposit(tempLyr);
                }
                else
                {
                    tempFeatLyr = (IFeatureLayer)tempLyr;
                    tempFeatSlctn = (IFeatureSelection)tempFeatLyr;
                    if (tempFeatSlctn.SelectionSet.Count > 0)
                    {
                        return tempLyr;
                    }
                }
            }

            return tempLyr;
        }

        /// <summary>根据图层名从图层组中选择图层
        /// <param name="pLayer">图层组</param>
        /// <param name="layerName">图层名</param>
        /// <returns>目标图层</returns>
        public static ILayer GetLayerFromComposit(ILayer pLayer, string layerName)
        {
            ICompositeLayer pComLayer = (ICompositeLayer)pLayer;
            ILayer tempLyr = null;
            for (int i = 0; i < pComLayer.Count; i++)
            {
                tempLyr = pComLayer.get_Layer(i);

                if (!tempLyr.Valid)
                {
                    continue;
                }


                if (tempLyr is ICompositeLayer)
                {
                    tempLyr = GetLayerFromComposit(tempLyr, layerName);
                }
                else
                {
                    if (tempLyr.Name == layerName)
                    {
                        return tempLyr;
                    }
                }
            }

            if (pLayer != null)
            {
                if (pLayer.Name != layerName)
                    return null;
            }

            return tempLyr;
        }


        /// <summary>
        ///根据图层名选择图层
        /// </summary>
        /// <param name="pMap">地图</param>
        /// <param name="layerName">图层名</param>
        /// <returns>目标图层</returns>
        /// <summary>  
        public static ILayer GetLayerFromMapByName(IMap pMap, string layerName)
        {
            ILayer pLayer = null;
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                pLayer = pMap.get_Layer(i);

                if (!pLayer.Valid)
                {
                    continue;
                }

                if (pLayer is ICompositeLayer)
                {
                    pLayer = GetLayerFromComposit(pLayer, layerName);
                    if (pLayer == null)
                    {
                        continue;
                    }
                    if (pLayer.Name == layerName)
                    {
                        return pLayer;
                    }
                }
                else
                {
                    if (pLayer.Name == layerName)
                    {
                        return pLayer;
                    }
                }
            }

            if (pLayer != null)
            {
                if (pLayer.Name != layerName)
                    return null;
            }


            return pLayer;
        }

        /// <summary>
        /// 设置复合图层（ICompositeLayer）是否可见
        /// </summary>
        /// <param name="pLayer">复合图层</param>
        /// <param name="IsVisible">是否可见，可见则为true，不可见则为false</param>
        public static void SetCompositeLayerVisible(ILayer pLayer, bool IsVisible)
        {
            pLayer.Visible = IsVisible;
            ICompositeLayer pComLayer = (ICompositeLayer)pLayer;
            ILayer tempLyr = null;
            for (int i = 0; i < pComLayer.Count; i++)
            {
                tempLyr = pComLayer.get_Layer(i);
                if (tempLyr is ICompositeLayer)
                {
                    SetCompositeLayerVisible(tempLyr, IsVisible);
                }
                else
                {
                    tempLyr.Visible = IsVisible;
                }
            }
        }

        /// <summary>
        /// 设置地图的所有图层是否可见与不可见
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="bIsVisible"></param>
        public static void SetMapsLayerVisible(IMap pMap, bool bIsVisible)
        {
            for (int j = 0; j < pMap.LayerCount; j++)
            {
                ILayer pLayer = pMap.get_Layer(j);
                if (pLayer is ICompositeLayer)
                {
                    SetCompositeLayerVisible(pLayer, bIsVisible);
                }
                else
                {
                    pLayer.Visible = bIsVisible;
                }
            }
        }

        /// <summary>
        /// 设置复合图层（ICompositeLayer）是否可见
        /// </summary>
        /// <param name="pLayer">复合图层</param>
        /// <param name="IsVisible">是否可见，可见则为true，不可见则为false</param>
        public static void SetCompositeLayerVisible1(ILayer pLayer, bool IsVisible)
        {
            ICompositeLayer pComLayer = (ICompositeLayer)pLayer;
            ILayer tempLyr = null;
            for (int i = 0; i < pComLayer.Count; i++)
            {
                tempLyr = pComLayer.get_Layer(i);
                if (tempLyr is ICompositeLayer)
                {
                    SetCompositeLayerVisible(tempLyr, IsVisible);
                }
                else
                {
                    tempLyr.Visible = IsVisible;
                }
            }
        }

        /// <summary>
        /// 取得图层的上一级对象，可能是igrouplayer,或ibasicmap
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static object GetLayerParent(IBasicMap map, ILayer layer)
        {
            ILayer tmpLayer;
            for (int i = 0; i <= map.LayerCount - 1; i++)
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

        /// <summary>
        /// 寻找某一图层的上一级grouplayer名
        /// </summary>
        /// <param name="groupLayer"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static IGroupLayer FindParentGroupLayer(IGroupLayer groupLayer, ILayer layer)
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
