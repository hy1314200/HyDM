using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ESRI.ArcGIS.Geodatabase;
using Hy.Check.Define;

using System.Data;
using System.Runtime.InteropServices;


namespace Hy.Check.Rule
{
    public class RuleBSMUniqueness : BaseRule
    {

        public override string Name
        {
            get { return "标识码唯一性检查"; }
        }

        public override IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {

        }

        public override bool Verify()
        {
            return true;
        }

        public override bool Check(ref List<Error> checkResult)
        {
            try
            {
                Hashtable pHtable = new Hashtable();
                IFeatureWorkspace pFWs = m_BaseWorkspace as IFeatureWorkspace;
                IEnumDataset pEmDs =m_BaseWorkspace. get_Datasets(esriDatasetType.esriDTFeatureClass);
                IDataset pDt = null;
                IQueryFilter pQueryFilter = new QueryFilterClass();

                List<Error> pResAttr = new List<Error>();
                while ((pDt = pEmDs.Next()) != null)
                {
                    IFeatureClass pFc = pDt as IFeatureClass;
                    int bsmFieldIndex = pFc.FindField("bsm");
                    if (bsmFieldIndex == -1) continue;
                    pQueryFilter.SubFields = pFc.OIDFieldName + ",bsm";
                    IFeatureCursor pFeatCur = pFc.Search(pQueryFilter, false);
                    IFeature pFeat = null;
                    while ((pFeat = pFeatCur.NextFeature()) != null)
                    {
                        string bsm = pFeat.get_Value(bsmFieldIndex).ToString();
                        if (pHtable.Contains(bsm))
                        {
                            Error pResInfo = new Error();
                            pResInfo.DefectLevel = this.m_DefectLevel;
                            pResInfo.RuleID = this.InstanceID;

                            pResInfo.OID = pFeat.OID;
                            pResInfo.BSM = bsm;
                            pResInfo.Description = string.Format(Helper.ErrMsgFormat.ERR_630100001, pFc.AliasName, pHtable[bsm].ToString(), bsm);
                            pResInfo.LayerName = pFc.AliasName;
                            pResAttr.Add(pResInfo);
                        }
                        else
                            pHtable.Add(bsm, (pFc as IDataset).Name);
                    }
                    Marshal.ReleaseComObject(pFeatCur);
                }
                pEmDs =m_BaseWorkspace.get_Datasets(esriDatasetType.esriDTFeatureDataset);
                while ((pDt = pEmDs.Next()) != null)
                {
                    IEnumDataset subEnumDs = pDt.Subsets;
                    IDataset pSubDt = null;
                    while ((pSubDt = subEnumDs.Next()) != null)
                    {
                        IFeatureClass pFc = pSubDt as IFeatureClass;

                        if (pFc.AliasName == "注记" || pFc.AliasName.Equals("ZJ", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                        int bsmFieldIndex = pFc.FindField("bsm");
                        if (bsmFieldIndex == -1) continue;

                        pQueryFilter.SubFields = pFc.OIDFieldName + ",bsm";
                        IFeatureCursor pFeatCur = pFc.Search(pQueryFilter, false);
                        bsmFieldIndex = pFeatCur.FindField("bsm");
                        if (bsmFieldIndex == -1) continue;
                        IFeature pFeat = null;
                        while ((pFeat = pFeatCur.NextFeature()) != null)
                        {
                            string bsm = pFeat.get_Value(bsmFieldIndex).ToString();
                            if (pHtable.Contains(bsm))
                            {
                                Error pResInfo = new Error();
                                pResInfo.DefectLevel = this.m_DefectLevel;
                                pResInfo.RuleID = this.InstanceID;

                                pResInfo.OID = pFeat.OID;
                                pResInfo.BSM = bsm;
                                pResInfo.Description = string.Format(Helper.ErrMsgFormat.ERR_630100001, pFc.AliasName, pHtable[bsm].ToString(), bsm);
                                pResInfo.LayerName = pFc.AliasName;//(pFc as IDataset).Name;
                                pResAttr.Add(pResInfo);
                            }
                            else
                                pHtable.Add(bsm, (pFc as IDataset).Name);
                        }
                        Marshal.ReleaseComObject(pFeatCur);
                    }
                }
                pHtable.Clear();
                Marshal.ReleaseComObject(pQueryFilter);
                checkResult = pResAttr;

                return true;
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                return false;
            }
        }
              
    

    }
}
