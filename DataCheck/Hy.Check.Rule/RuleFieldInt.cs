using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using Hy.Check.Define;

using System.Runtime.InteropServices;
using Hy.Check.Rule.Helper;
using Hy.Check.Utility;

namespace Hy.Check.Rule
{
    public class RuleFieldInt : BaseRule
    {
        public RuleFieldInt()
        {
        }

        public override enumErrorType ErrorType
        {
            get
            {
                return enumErrorType.FieldIntegrity;
            }
        }

        public override string Name
        {
            get { return "字段完整性检查"; }
        }

        public override IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {
            BinaryReader pParameter = new BinaryReader(new MemoryStream(objParamters));
            if (m_pFieldPara != null)
            {
                m_pFieldPara = null;
            }
            m_pFieldPara = new RuleExpression.LRFieldCheckPara();

            pParameter.BaseStream.Position = 0;
            int nCount1 = pParameter.ReadInt32();
            m_pFieldPara.m_bCode = Convert.ToBoolean(pParameter.ReadInt32());
            m_pFieldPara.m_bDecimal = Convert.ToBoolean(pParameter.ReadInt32());
            m_pFieldPara.m_bLength = Convert.ToBoolean(pParameter.ReadInt32());
            m_pFieldPara.m_bName = Convert.ToBoolean(pParameter.ReadInt32());
            m_pFieldPara.m_bNull = Convert.ToBoolean(pParameter.ReadInt32());
            m_pFieldPara.m_bType = Convert.ToBoolean(pParameter.ReadInt32());


            //解析字符串
            int nSize = sizeof(int) * 7;
            Byte[] bb = new byte[nCount1 - nSize];
            pParameter.Read(bb, 0, nCount1 - nSize);
            string para_str = Encoding.Default.GetString(bb);

            para_str.Trim();

            string[] strResult = para_str.Split('|');

            int i = 0;
            m_pFieldPara.strAlias = strResult[i++];
            m_pFieldPara.strRemark = strResult[i++];

            m_pFieldPara.m_LyrFldMap = new List<string>();

            for (int j = i; j < strResult.Length; j++)
            {
                m_pFieldPara.m_LyrFldMap.Add(strResult[j]);
            }
        }

        public override bool Verify()
        {
            return true;
        }

        public override bool Check(ref List<Error> checkResult)
        {
            IFeatureWorkspace ipFtWS = (IFeatureWorkspace)m_BaseWorkspace;

            try
            {
                List<Error> pResult = new List<Error>();
                string strAlias = m_pFieldPara.strAlias;
                List<string> listLayer = m_pFieldPara.m_LyrFldMap;
                System.Collections.Hashtable hashtable = new System.Collections.Hashtable();

                for (int i = 0; i < listLayer.Count; i++)
                {
                    string strTemp = listLayer[i];
                    int nIndex = strTemp.IndexOf('&');
                    if (nIndex < 0)
                    {
                        continue;
                    }
                    string str = strTemp.Substring(0, nIndex);
                    if (!hashtable.Contains(str))
                    {
                        hashtable.Add(str, "");
                    }
                }

                DataTable dtLayer = new DataTable();
                string strSQL = "select AttrTableName,LayerName,LayerID from LR_DicLayer";

                dtLayer = Common.Utility.Data.AdoDbHelper.GetDataTable(SysDbHelper.GetSysDbConnection(), strSQL);
                if (dtLayer==null)
                {
                    return false;
                }

                foreach (DataRow drLayer in dtLayer.Rows)
                {
                    if (drLayer != null)
                    {
                        string strLayerCode = drLayer["AttrTableName"].ToString();
                        string strLayerName = drLayer["LayerName"].ToString();
                        int nLayerID = Convert.ToInt32(drLayer["LayerID"]);

                        if (!hashtable.Contains(strLayerName))
                        {
                            continue;
                        }

                        IFeatureClass pFtCls = null;
                        try
                        {
                            pFtCls = ipFtWS.OpenFeatureClass(strLayerCode);
                        }
                        catch
                        {
                            continue;
                        }
                        IFields pFields = pFtCls.Fields;

                        if (pFields == null)
                        {
                            continue;
                        }
                        int lFieldCount = pFields.FieldCount;
                        IField pField;

                        DataTable dtFields = new DataTable();
                        string strSQLFields = "select * from LR_DicField where LayerID = " + nLayerID + "";
                        dtFields = Common.Utility.Data.AdoDbHelper.GetDataTable(SysDbHelper.GetSysDbConnection(), strSQLFields);
                        if (dtFields==null)
                        {
                            FieldError LRFieldErrorInfo = new FieldError();
                            LRFieldErrorInfo.DefectLevel = this.DefectLevel;
                            LRFieldErrorInfo.strAttrTabName = strLayerName;
                            LRFieldErrorInfo.strFieldName = null;
                            LRFieldErrorInfo.m_strRuleInstID = this.m_InstanceID;
                            LRFieldErrorInfo.strErrorMsg = string.Format("{0}层对应的属性字段，在《土地利用现状数据库标准》中不存在", strLayerName);

                            pResult.Add(LRFieldErrorInfo);

                            continue;
                        }

                        ///检查图层中是否存在多余字段
                        for (int i = 0; i < lFieldCount; i++)
                        {
                            if (strLayerName == "注记")
                            {
                                break;
                            }
                            pField = pFields.get_Field(i);
                            if (pField.Name.ToUpper().Contains("OBJECTID") ||
                                pField.Name.ToLower().Contains("shape"))
                            {
                                continue;
                            }

                            int k = 0;
                            int nFieldCount = dtFields.Rows.Count;
                            for (k = 0; k < nFieldCount; k++)
                            {
                                DataRow drField = dtFields.Rows[k];
                                string strStdName = drField["FieldName"].ToString();
                                string strStdCode = drField["FieldCode"].ToString();
                                if (strStdCode.Trim().Equals("objectid", StringComparison.OrdinalIgnoreCase) ||
                                    strStdCode.Trim().Equals("object id", StringComparison.OrdinalIgnoreCase) ||
                                    strStdCode.Trim().Equals("shape", StringComparison.OrdinalIgnoreCase))
                                {
                                    continue;
                                }
                                if (pField.Name.Trim().Equals(strStdCode.Trim(), StringComparison.OrdinalIgnoreCase))
                                {
                                    break;
                                }
                            }
                            if (k == nFieldCount)
                            {
                                if (!pField.AliasName.Contains("本软件"))
                                {

                                    FieldError LRFieldErrorInfo2 = new FieldError();
                                    LRFieldErrorInfo2.DefectLevel = this.DefectLevel;
                                    LRFieldErrorInfo2.strAttrTabName = strLayerName;
                                    LRFieldErrorInfo2.strFieldName = pField.Name;
                                    LRFieldErrorInfo2.m_strRuleInstID = this.m_InstanceID;
                                    LRFieldErrorInfo2.strErrorMsg = string.Format(Helper.ErrMsgFormat.ERR_410100001_1, strLayerName, pField.Name);

                                    pResult.Add(LRFieldErrorInfo2);
                                }

                            }
                        }

                        ///检查标准中的字段在图层中是否存在，已经图层的字段是否和标准相符合
                        //二次for循环迭代控制器，add by wangxiang 20111201
                        int flag = 0;
                        foreach (DataRow drField in dtFields.Rows)
                        {
                            if (drField != null)
                            {
                                string strStdName = drField["FieldName"].ToString();
                                string strStdCode = drField["FieldCode"].ToString();

                                if (strStdCode.Trim().Equals("objectid", StringComparison.OrdinalIgnoreCase) ||
                                    strStdCode.Trim().Equals("object id", StringComparison.OrdinalIgnoreCase) ||
                                    strStdCode.Trim().Equals("shape", StringComparison.OrdinalIgnoreCase))
                                {
                                    continue;
                                }

                                int nStdType = Convert.ToInt32(drField["FieldType"]);

                                
                                string strStdFldType = Common.Utility.Data.AdoDbHelper.GetFieldTypeName(nStdType);

                                FieldError FieldErrInfo1  = new FieldError();
                                FieldErrInfo1.DefectLevel = this.DefectLevel;
                                FieldErrInfo1.strAttrTabName = strLayerName;
                                FieldErrInfo1.strFieldName = "" + strStdCode + "(" + strStdName + ")";
                                FieldErrInfo1.strStdFieldType = strStdFldType;
                                FieldErrInfo1.m_strRuleInstID = this.m_InstanceID;

                                int i = 0;
                                for (i = 0; i < lFieldCount && flag < lFieldCount; i++)
                                {
                                    pField = pFields.get_Field(i);


                                    if (pField.Name.Trim() == strStdCode.Trim())
                                    {
                                        flag++;
                                        esriFieldType pType = pField.Type;

                                        if (nStdType == 3)
                                        {
                                            nStdType = 4;
                                        }
                                        esriFieldType pDTType = TopoHelper.en_GetEsriFieldByEnum(nStdType);
                                        if (pType == pDTType)
                                        {
                                            if (pType != esriFieldType.esriFieldTypeString)
                                            {
                                                break;
                                            }

                                            if (pField.Length != Convert.ToInt32(drField["Length"])) //字段长度不正确
                                            {
                                                if (strLayerCode.Equals("JBNTBHTB", StringComparison.OrdinalIgnoreCase) && pField.Name.Trim().Equals("jbnttbbh", StringComparison.OrdinalIgnoreCase))
                                                {
                                                    FieldErrInfo1.strErrorMsg = string.Format(Helper.ErrMsgFormat.ERR_410100001_4, strLayerName, pField.Name, pField.Length, Convert.ToInt32(drField["Length"]));
                                                }
                                                else
                                                {
                                                    FieldErrInfo1.strErrorMsg = string.Format(Helper.ErrMsgFormat.ERR_410100001_4, strLayerName, pField.Name, pField.Length, Convert.ToInt32(drField["Length"]));
                                                }
                                                pResult.Add(FieldErrInfo1);
                                                break;
                                            }

                                       
                                            break;
                                        }
                                        else 
                                        {
                                            if (pDTType != esriFieldType.esriFieldTypeBlob)
                                            {
                                                FieldErrInfo1.strErrorMsg = string.Format(Helper.ErrMsgFormat.ERR_410100001_3, strLayerName, pField.Name, TopoHelper.en_GetFieldTypebyEsriField(pType), TopoHelper.en_GetFieldTypebyEsriField(pDTType));
                                                pResult.Add(FieldErrInfo1);
                                            }
                                         
                                            break;
                                        }
                                    }
                                }

                                if (i == lFieldCount)
                                {
                                    if (drField["FieldOption"].ToString().Trim() != "fz")
                                    {
                                        FieldErrInfo1.strErrorMsg = string.Format(Helper.ErrMsgFormat.ERR_410100001_2, strLayerName, drField["FieldName"].ToString());
                                        pResult.Add(FieldErrInfo1);
                                    }

                                 }
                            }
                        }
                        if (pFtCls != null)
                        {
                            Marshal.ReleaseComObject(pFtCls);
                            pFtCls = null;
                        }
                    }
                }

            checkResult = pResult;
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                return false;
            }

            return true;
        }

        private RuleExpression.LRFieldCheckPara m_pFieldPara;
   
        // 判断图层是否已经导入
        private bool IsLayerLoad(ref List<string> arrayLoadLayer, string strAttrTabName)
        {
            for (int i = 0; i < arrayLoadLayer.Count; i++)
            {
                if (strAttrTabName.CompareTo(arrayLoadLayer[i]) == 0)
                {
                    return true;
                }
            }

            return false;
        }


        // 获取已经导入的所有图层
        private bool GetLoadLayers(ref List<string> arrayLoadLayer)
        {
            try
            {
                DataTable ipRecordset = new DataTable();

                if (!Common.Utility.Data.AdoDbHelper.OpenTable("LR_CheckLayer", ref ipRecordset, this.m_QueryConnection))
                {
                    
                    SendMessage(enumMessageType.RuleError, "当前工作数据库中不存在对照表LR_CheckLayer，无法执行字段完整性检查!");
                    return false;
                }

                // 从DataTable中获取名称
                foreach (DataRow dr in ipRecordset.Rows)
                {
                    if (dr != null)
                    {
                        string strOrigAttrTabName = dr["OrigAttrTableName"].ToString();
                        string strStdAttrTabName = dr["StdAttrTableName"].ToString();
                        if (strOrigAttrTabName != "")
                        {
                            arrayLoadLayer.Add(strStdAttrTabName);
                        }
                    }
                }
                // 关闭记录集
                ipRecordset.Dispose();
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                return false;
            }

            return true;
        }


        private class FieldError : Error
        {
            public FieldError()
            {
                this.strFieldName = "";
                this.strAttrTabName = "";
                this.strStdFieldType = "";
                this.strErrorMsg = "";
                this.m_strRuleInstID = "";
            }



            // 唯一表示ID
            public int nErrID;

            // 错误字段名称
            public string strFieldName;

            // 属性表
            public string strAttrTabName;

            // 标准字段类型
            public string strStdFieldType;

            // 错误原因
            public string strErrorMsg;

            public string m_strRuleInstID;


            public override string ToSQLString()
            {
                if (this.m_strRuleInstID == null)
                    this.m_strRuleInstID = "";

                StringBuilder strBuilder = new StringBuilder("Insert into LR_ResIntField(");
                strBuilder.Append("ErrID,ErrorReason,AttrTabName,FieldName,FieldType,IsException,RuleInstID,Remark,DefectLevel)");
                strBuilder.Append(" Values(");

                strBuilder.Append(this.nErrID); strBuilder.Append(",'");
                strBuilder.Append(this.strErrorMsg.Replace("'", "''")); strBuilder.Append("','");
                strBuilder.Append(this.strAttrTabName.Replace("'", "''")); strBuilder.Append("','");
                strBuilder.Append(this.strFieldName.Replace("'", "''")); strBuilder.Append("','");
                strBuilder.Append(this.strStdFieldType.Replace("'", "''")); strBuilder.Append("',");
                strBuilder.Append(this.IsException); strBuilder.Append(",'");
                strBuilder.Append(this.m_strRuleInstID.Replace("'", "''")); strBuilder.Append("','");
                strBuilder.Append(this.Description.Replace("'", "''")); strBuilder.Append("',");
                strBuilder.Append((int)this.DefectLevel);
                strBuilder.Append(")");

                return strBuilder.ToString();

            }
        }

    }
}