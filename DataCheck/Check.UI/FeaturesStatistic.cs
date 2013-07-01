using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using System.Data;
using System.Runtime.InteropServices;
using Check.Define;

namespace Check.UI
{
    public class FeaturesStatistic
    {
        private IWorkspace  m_Workspace = null;
        //private int m_StandardID;

        public FeaturesStatistic(IWorkspace workspace)//, int standardID)
        {
            if(workspace!=null)
            {
                m_Workspace = workspace;
            }
            //m_StandardID = standardID;
        }

        public DataTable GetFeaturesStatDt()
        {
            DataTable result = GenerateDataTable();
            IFeatureDataset pDataset = null;
            IFeatureClassContainer pFeatClsContainer = null;
            IFeatureClass pFeatureCls = null;
            try
            {
                //List<StandardLayer> layers = LayerReader.GetLayersByStandard(m_StandardID);
                //获取标准的图层列表
                if (m_Workspace == null)
                {
                    return result;
                }
                IEnumDataset  enumDataset = m_Workspace.get_Datasets(esriDatasetType.esriDTFeatureDataset);
                enumDataset.Reset();
                IFeatureDataset subDataset = enumDataset.Next() as IFeatureDataset;

                while (subDataset != null)
                {
                    pFeatClsContainer = subDataset as IFeatureClassContainer;
                    int iCount = 0;
                    DataRow dr = null;
                    for (int i = 0; i < pFeatClsContainer.ClassCount; i++)
                    {
                        pFeatureCls = pFeatClsContainer.get_Class(i);
                        string featClsName = (pFeatureCls as IDataset).Name;

                        iCount = pFeatureCls.FeatureCount(null);
                        dr = result.NewRow();
                        dr[0] = featClsName;
                        dr[1] = pFeatureCls.AliasName;
                        dr[2] = iCount;
                        result.Rows.Add(dr);
                        Marshal.ReleaseComObject(pFeatureCls);
                    }
                    subDataset = enumDataset.Next() as IFeatureDataset;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("获取要素个数失败！原因：" + ex.Message, "警告");
                return result;
            }
            finally
            {
                if (pFeatureCls != null)
                {
                    Marshal.ReleaseComObject(pFeatureCls);
                }
                if (pDataset != null)
                {
                    Marshal.ReleaseComObject(pDataset);
                }
            }
            return result;
        }

        private DataTable  GenerateDataTable()
        {
            DataTable pDt = new DataTable();
            DataColumn pDc = new DataColumn();
            pDc.ColumnName = "featureClsName";
            pDc.Caption = "图层名";
            pDc.DataType = Type.GetType("System.String");
            pDt.Columns.Add(pDc);

            pDc = new DataColumn();
            pDc.ColumnName = "featureClsAliasName";
            pDc.Caption = "图层别名";
            pDc.DataType = Type.GetType("System.String");
            pDt.Columns.Add(pDc);

            pDc = new DataColumn();
            pDc.ColumnName = "Count";
            pDc.Caption = "要素个数";
            pDc.DataType = Type.GetType("System.String");
            pDt.Columns.Add(pDc);
            return pDt;
        }

        /// <summary>
        /// Gets the feature CLS count.
        /// </summary>
        /// <param name="workspace">The workspace.</param>
        /// <param name="FeatClsName">Name of the feat CLS.</param>
        /// <param name="isHave">if set to <c>true</c> [is have].</param>
        /// <returns></returns>
        private int GetFeatureClsCount(string FeatClsName, out bool isHave)
        {
            int iCount = 0;
            isHave = true;
            IFeatureClass featureCls = null;
            try
            {
                if (!(m_Workspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, FeatClsName))
                {
                    isHave = false;
                    return iCount;
                }

                featureCls = (m_Workspace as IFeatureWorkspace).OpenFeatureClass(FeatClsName);

                iCount = featureCls.FeatureCount(null);
            }
            catch
            {
                return iCount;
            }
            finally
            {
                if (featureCls != null)
                {
                    Marshal.ReleaseComObject(featureCls);
                    featureCls = null;
                }
            }
            return iCount;
        }

    }
}
