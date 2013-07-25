using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using System.Runtime.InteropServices;
using Hy.Check.Utility;

namespace Hy.Common.Utility.Esri
{
    public class TopoOperAPI
    {
        /// <summary>
        /// 获取当前任务子库中的图层集名
        /// </summary>
        /// <param name="ipSonFWS"></param>
        /// <returns></returns>
        public static IFeatureDataset GetCurrentSonDataSet(IFeatureWorkspace ipSonFWS)
        {
            try
            {
                IWorkspace ipWks = (IWorkspace)ipSonFWS;
                IEnumDatasetName ipDatasetNames = ipWks.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
                IDatasetName ipDatasetName = ipDatasetNames.Next();
                IFeatureDataset ipDataset = null;
                if (ipDatasetName != null)
                {
                    ipDataset = ipSonFWS.OpenFeatureDataset(ipDatasetName.Name);
                }
                return ipDataset;
            }
            catch (Exception ex)
            {
                //GT_CONST.LogAPI.CheckLog.AppendErrLogs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        ///  获取子库中所有的拓扑图层
        /// </summary>
        /// <param name="axMapControl"></param>
        /// <param name="strSonPath"></param>
        /// <param name="ipSonFWS"></param>
        /// <param name="ipDataset"></param>
        /// <returns></returns>
        public static bool GetFcTopoNameInSon(AxMapControl axMapControl, IFeatureWorkspace ipSonFWS, 
                 IFeatureDataset ipDataset)
        {
            ITopologyWorkspace ipTopologyWS = null;
            IFeatureClassContainer pFeatClassContainer = null;
            try
            {
                ipTopologyWS = (ITopologyWorkspace)ipSonFWS;

                if (ipDataset == null)
                {
                    return false;
                }

                pFeatClassContainer = (IFeatureClassContainer)ipDataset;

                List<string> arrayTopoLayers = new List<string>();

                arrayTopoLayers.Add(COMMONCONST.Topology_Name);

                //-------------------------------获取拓扑图层，并在地图上显示------------------------------------//
                AddTopoLayer(ref axMapControl, arrayTopoLayers[0], ipTopologyWS, pFeatClassContainer, ipDataset);

            }
            catch (Exception ex)
            {
                //GT_CONST.LogAPI.CheckLog.AppendErrLogs(ex.Message);
                return false;
            }
            finally
            {
                //释放接口
                if (pFeatClassContainer != null)
                {
                    Marshal.ReleaseComObject(pFeatClassContainer);
                    pFeatClassContainer = null;
                }

                if (ipTopologyWS != null)
                {
                    Marshal.ReleaseComObject(ipTopologyWS);
                    ipTopologyWS = null;
                }
            }
            return true;
            //------------------------------------------------------------------------------------------------//
        }

        //加载拓扑图层
        public static void AddTopoLayer(ref AxMapControl pMapCtrl, string strTopoLayerName, ITopologyWorkspace ipTopologyWS,IFeatureClassContainer ipFeatClassContainer, IFeatureDataset ipFeatDataset)
        {
            int nOriginClassID, nDestClassID;

            ITopology ipTopology;

            //打开数据集
            //ITopologyContainer ipTopoContainer = (ITopologyContainer)ipFeatDataset;

            IWorkspace2 pWorkspace = (IWorkspace2)ipFeatDataset.Workspace;

            if (pWorkspace.get_NameExists(esriDatasetType.esriDTTopology, strTopoLayerName))
            {
                ipTopology = ipTopologyWS.OpenTopology(strTopoLayerName);

                if (ipTopology == null)
                {
                    return;
                }

                ITopologyLayer ipTopologyLayer = new TopologyLayerClass();
                ipTopologyLayer.Topology = ipTopology;



                ILegendInfo legendInfo = (ILegendInfo)ipTopologyLayer;

                for (int i = 0; i < legendInfo.LegendGroupCount; i++)
                {
                    ILegendGroup legendgroup = legendInfo.get_LegendGroup(i);

                    ILegendClass legendclass = legendgroup.get_Class(0);

                    switch (legendgroup.Heading)
                    {
                        case "Area Errors":
                            {
                                legendgroup.Heading = "";
                                legendclass.Label = "面状错误";
                                legendclass.Description = "面状错误";
                                break;
                            }
                        case "Line Errors":
                            {
                                legendgroup.Heading = "";
                                legendclass.Label = "线状错误";
                                legendclass.Description = "线状错误";
                                break;
                            }
                        case "Point Errors":
                            {
                                legendgroup.Heading = "";
                                legendclass.Label = "点状错误";
                                legendclass.Description = "点状错误";
                                break;
                            }
                    }

                }


                ILayer ipLayer = (ILayer)ipTopologyLayer;
                ipLayer.Name = strTopoLayerName; //将拓扑检查合并后，将拓扑图层名指定为“拓扑错误”后，采用此方法命名  hehy20080724

                /*  将拓扑检查合并后，将拓扑图层名指定为“拓扑错误”后，注销以下代码 hehy20080724
                ///////////////////////////////////////////////////
                //得到拓扑层相对应的规则名称
                CModelSchema pModelSchema = new CModelSchema();
                string strRuleName = pModelSchema.GetRuleNameByTopoLayerName(m_pTask.pSchema, strTopoLayerName);
                //////////////////////////////////////////////////
                if (strRuleName.Length == 0)
                {
                    ipLayer.Name = strTopoLayerName;
                }
                else
                {
                    ipLayer.Name = strRuleName;
                }
                */
                //把拓扑图层加载到map控件中
                //pMapCtrl.AddLayer(ipLayer, pMapCtrl.LayerCount);
                pMapCtrl.Map.AddLayer(ipLayer);
                //pMapCtrl.ActiveView.Extent = ipLayer.AreaOfInterest;
            }
        }
    }
}
