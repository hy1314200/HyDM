using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using Check.Define;

using System.Runtime.InteropServices;
using Check.Rule.Helper;
using Check.Utility;

namespace Check.Rule
{
    /// <summary>
    /// 属性空值质检规则
    /// </summary>
    public class RuleBlankVal : BaseRule
    {
        

        //质检参数结构体
        public RuleExpression.BLANKVALPARA m_structBlankPara;


        public override string Name
        {
            get { return "属性空值质检规则"; }
        }

        public override IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {
            m_structBlankPara = new RuleExpression.BLANKVALPARA();
            BinaryReader pParameter = new BinaryReader(new MemoryStream(objParamters));
            pParameter.BaseStream.Position = 0;

            int nCount1 = pParameter.ReadInt32();

            //解析字符串
            Byte[] bb = new byte[nCount1];
            pParameter.Read(bb, 0, nCount1);
            string para_str = Encoding.Default.GetString(bb);
            para_str.Trim();

            string[] strResult = para_str.Split('|');

            int i = 0;
            m_structBlankPara.strAlias = strResult[i++];
            m_structBlankPara.strScript = strResult[i++];
            m_structBlankPara.strFtName = strResult[i++];

            m_structBlankPara.iType = pParameter.ReadInt32();
            int fieldLength = pParameter.ReadInt32();

            //解析字段名
            if (fieldLength > 0)
            {
                Byte[] bb1 = new byte[fieldLength];
                pParameter.Read(bb1, 0, fieldLength);
                string strFields = Encoding.Default.GetString(bb1);

                strFields.Trim();

                //解析字段名
                string[] strResult1 = strFields.Split('|');

                m_structBlankPara.fieldArray = new List<string>();

                for (int j = 0; j < strResult1.Length; j++)
                {
                    m_structBlankPara.fieldArray.Add(strResult1[j]);
                }
            }

            int fieldNum = pParameter.ReadInt32();
            m_structBlankPara.fieldTypeArray = new ArrayList();

            int fType = -1;

            for (int f = 0; f < fieldNum; f++)
            {
                fType = pParameter.ReadInt32();
                m_structBlankPara.fieldTypeArray.Add(fType);
            }
        }

        public override bool Verify()
        {
            if (this.m_structBlankPara == null)
            {
                SendMessage(enumMessageType.VerifyError, "参数读取不成功");
                return false;
            }

            //根据别名取图层名
            m_LayerName = this.GetLayerName(m_structBlankPara.strFtName);
            //打开相应的featureclass

            try
            {
                (this.m_BaseWorkspace as IFeatureWorkspace).OpenTable(m_LayerName);
            }
            catch
            {
                SendMessage(enumMessageType.VerifyError, "当前工作数据库中不存在图层" + m_LayerName + "，无法执行属性空值检查!");
                return false;
            }
            return true;
        }
        string m_LayerName = null;
        public override bool Check(ref List<Error> checkResult)
        {

            ITable ipTable = null;
            ICursor ipCursor = null;
            try
            {

                //打开相应的featureclass
                IFeatureWorkspace ipFtWS;
                ipFtWS = (IFeatureWorkspace)this.m_BaseWorkspace;
                if (ipFtWS != null)
                {
                    try
                    {
                        ipTable = ipFtWS.OpenTable(m_LayerName);
                    }
                    catch
                    {
                        SendMessage(enumMessageType.RuleError, "当前工作数据库中不存在图层" + m_LayerName + "，无法执行属性空值检查!");
                        return false;
                    }
                }
                else
                {
                    SendMessage(enumMessageType.RuleError, "Base库居然不是FeatureWorkspace!");
                    return false;
                }

                //执行查询操作
                IQueryFilter pFilter = new QueryFilterClass();
                pFilter.WhereClause = ConstructClause();
                pFilter.SubFields = "OBJECTID,BSM";

                //试着查询
                try
                {
                    ipCursor = ipTable.Search(pFilter, true);
                    if (ipCursor == null)
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    SendMessage(enumMessageType.RuleError, "当前工作数据库的图层" + m_LayerName + "中，SQL语句" + pFilter.WhereClause + "无法执行!");
                    return false;
                }

                //若结果为空，则表达式不正确
                if (ipCursor == null)
                {
                    return false;
                }
                checkResult = GetResult(ipCursor);

                return checkResult != null;
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                return false;
            }
            finally
            {
                if (ipCursor != null)
                {
                    Marshal.ReleaseComObject(ipCursor);
                    ipCursor = null;
                }
                if (ipTable != null)
                {
                    Marshal.ReleaseComObject(ipTable);
                    ipTable = null;
                }
            }
            return true;
        }

        

        

        private string ConstructClause()
        {
            string strMid;
            if (m_structBlankPara.iType == 0)
            {
                strMid = " AND ";
            }
            else
            {
                strMid = " OR ";
            }
            string strClause;
            if (Convert.ToInt32(m_structBlankPara.fieldTypeArray[0]) != 5)
            {
                strClause = m_structBlankPara.fieldArray[0] + " IS NULL ";
            }
            else
            {
                strClause = "( " + m_structBlankPara.fieldArray[0] + " IS NULL OR " +
                            m_structBlankPara.fieldArray[0] + " = '' ) ";
            }
            for (int i = 1; i < m_structBlankPara.fieldArray.Count; i++)
            {
                if (Convert.ToInt32(m_structBlankPara.fieldTypeArray[i]) != 5)
                    strClause = strClause + strMid + m_structBlankPara.fieldArray[i] + " IS NULL ";
                else
                {
                    strClause = strClause + strMid + " ( " + m_structBlankPara.fieldArray[i]
                                + " IS NULL OR " + m_structBlankPara.fieldArray[i] + " = '' )";
                }
            }
            return strClause;
        }

        private List<Error> GetResult(ICursor pCursor)
        {
            IRow ipRow;
            ipRow = pCursor.NextRow();
            if (ipRow == null)
            {
                return new List<Error>();
            }

            // 判断是否有OID列
            bool bHasOID;
            bHasOID = ipRow.HasOID;

            if (!bHasOID)
            {
                SendMessage(enumMessageType.RuleError, "当前检查的对象没有OID字段，不进行检查");
                return null;
            }

            string strErrMsg;
            if (m_structBlankPara.iType == 0)
            {
                strErrMsg = "目标字段值均为空";
            }
            else
            {
                strErrMsg = "任意目标字段值为空";
            }

            string strErrInfo = ConstructErrorInfo();
            List<Error> pResAttr = new List<Error>();

            IFields pFields = ipRow.Fields;
            int nIndex = pFields.FindField("BSM");

            while (ipRow != null)
            {
                // 添家结果记录
                Error pResInfo = new Error();
                pResInfo.DefectLevel = this.m_DefectLevel;
                pResInfo.RuleID = this.InstanceID;

                int OID;
                OID = ipRow.OID;
                pResInfo.OID = OID;

                if (nIndex >= 0)
                {
                    pResInfo.BSM = ipRow.get_Value(nIndex).ToString();
                }

                pResInfo.LayerName = m_structBlankPara.strFtName;

                // 错误信息
                if (m_structBlankPara.strScript.Trim() != "" && m_structBlankPara.strScript != null)
                {
                    pResInfo.Description = m_structBlankPara.strScript;
                }
                else
                {
                    pResInfo.Description = strErrInfo;
                }
                
                pResAttr.Add(pResInfo);

                ipRow = pCursor.NextRow();
            }
            return pResAttr;
        }

        private string ConstructErrorInfo()
        {
            string strInfo = "";
            string strFields = "";
            StandardLayer distLayer = LayerReader.GetLayerByName(m_structBlankPara.strFtName, SysDbHelper.GetStandardIDBySchemaID(this.m_SchemaID));
            if (m_structBlankPara.fieldArray.Count > 0)
            {
                for (int i = 1; i < m_structBlankPara.fieldArray.Count; i++)
                {
                    strFields = strFields + "|" + FieldReader.GetAliasName(m_structBlankPara.fieldArray[i], distLayer.ID);
                }
                strFields = strFields.Remove(0, 1);
            }
            if (m_structBlankPara.iType == 0)
            {
                strInfo = "字段 " + strFields + " 都为空";
            }
            else if (m_structBlankPara.iType == 1)
            {
                strInfo = "字段 " + strFields + " 中有一个或多个为空";
            }
            return strInfo;
        }


    }
}