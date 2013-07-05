using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using Hy.Check.Define;

using System.Runtime.InteropServices;
using Hy.Check.Rule.Helper;
using Hy.Check.Utility;

namespace Hy.Check.Rule
{
    public class RuleLength : BaseRule
    {
       
        public RuleLength()
        {
            m_structLengthPara.strName = "线碎屑质检规则";
        }

        //参数结构体
        private RuleExpression.LENGTHPARA m_structLengthPara = new RuleExpression.LENGTHPARA();

        // 目标字段数组字符串
        private string m_strTargetFields;


        public override string Name
        {
            get { return "线碎屑质检规则"; }
        }

        public override IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {
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
            m_structLengthPara.strAlias = strResult[i++];
            m_structLengthPara.strScript = strResult[i++];
            m_structLengthPara.strFtName = strResult[i];

            m_structLengthPara.dbThreshold = pParameter.ReadDouble();

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
                m_structLengthPara.fieldArray = new List<string>();


                for (int j = 0; j < strResult1.Length; j++)
                {
                    m_structLengthPara.fieldArray.Add(strResult1[j]);
                }
            }

            //取字段类型
            int fieldNum = pParameter.ReadInt32();
            int fType = -1;

            if (fieldNum > 0)
            {
                m_structLengthPara.fieldTypeArray = new List<int>();
                for (int f = 0; f < fieldNum; f++)
                {
                    fType = pParameter.ReadInt32();
                    m_structLengthPara.fieldTypeArray.Add(fType);
                }
            }

        }

        public override bool Verify()
        {
            try
            {
                //根据别名取图层名
               m_LayerName = base.GetLayerName(m_structLengthPara.strFtName);
                (m_BaseWorkspace as IFeatureWorkspace).OpenTable(m_LayerName);
            }
            catch
            {
                SendMessage(enumMessageType.VerifyError, "当前工作数据库中不存在图层" + m_structLengthPara.strFtName + "，无法执行线碎屑检查!");
            }

            return true;
        }

        private string m_LayerName = null;
        public override bool Check(ref List<Error> checkResult)
        {
            ITable ipTable = null;
            ICursor ipCursor = null;
            try
            {
              
                //打开相应的featureclass
                try
                {
                    ipTable = (m_BaseWorkspace as IFeatureWorkspace).OpenTable(m_LayerName);
                }
                catch
                {
                    SendMessage(enumMessageType.RuleError, "当前工作数据库中不存在图层" + m_structLengthPara.strFtName + "，无法执行线碎屑检查!");
                    return false;
                }
                

                //执行查询操作	       
                IQueryFilter pFilter = new QueryFilterClass();

                pFilter.WhereClause = ConstructClause();
                pFilter.SubFields = "OBJECTID,BSM,Shape_Length";

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
                    SendMessage(enumMessageType.RuleError, "当前工作数据库的图层" + m_structLengthPara.strFtName + "中，SQL语句" + pFilter.WhereClause + "无法执行!");
                    SendMessage(enumMessageType.Exception,ex.ToString());
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
            catch(Exception exp)
            {
                SendMessage(enumMessageType.RuleError,string.Format("意外失败，信息：{0}",exp.Message));
                SendMessage(enumMessageType.Exception,exp.ToString());
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
           

        private string ConstructErrorInfo()
        {
            string strInfo;
            strInfo = "线长度小于域值" +COMMONCONST.dLengthThread + " ";
            string strFields = "";
            StandardLayer distLayer = LayerReader.GetLayerByName(m_structLengthPara.strFtName, SysDbHelper.GetStandardIDBySchemaID(this.m_SchemaID));
            if (m_structLengthPara.fieldArray != null)
            {
                for (int i = 0; i < m_structLengthPara.fieldArray.Count; i++)
                {
                    strFields = strFields + "|" + FieldReader.GetAliasName(m_structLengthPara.fieldArray[i], distLayer.ID);
                }
                strFields = strFields.Remove(0, 1);
                strFields = strFields + " 字段都为空";
                strInfo = strInfo + ",并且 " + strFields;
            }


            return strInfo;
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
                SendMessage(enumMessageType.RuleError, "当前检查的对象不是线要素类（没有OID），不进行检查");
                return null;
            }

            IFields pFields = ipRow.Fields;
            int nIndex = pFields.FindField("BSM");

            int nIndexShapeLength = pFields.FindField("Shape_Length");

            //string strErrInfo = ConstructErrorInfo();
            List<Error> pResAttr = new List<Error>();
            while (ipRow != null)
            {
                // 添家结果记录
                Error pResInfo = new Error();
                pResInfo.DefectLevel = this.m_DefectLevel;
                pResInfo.RuleID = this.InstanceID;

                int OID = ipRow.OID;

                if (nIndex >= 0)
                {
                    pResInfo.BSM = ipRow.get_Value(nIndex).ToString();

                    if (nIndexShapeLength > 0)
                    {
                        double dLength = Convert.ToDouble(ipRow.get_Value(nIndexShapeLength));
                        pResInfo.Description = string.Format("'{0}'层内标识码为'{1}'、长度为'{2}'的线状地物是碎线。不符合最小上图距离({3})的要求。", m_structLengthPara.strFtName, pResInfo.BSM, dLength.ToString("f2"), COMMONCONST.dLengthThread);
                    }
                    else
                    {
                        pResInfo.Description = string.Format("'{0}'层内标识码为'{1}'的线状地物是碎线。不符合最小上图距离({2})的要求。", m_structLengthPara.strFtName, pResInfo.BSM, COMMONCONST.dLengthThread);
                    }
                    
                }
                else
                {
                    pResInfo.Description = m_structLengthPara.strScript;   // 错误信息
                
                }
                pResInfo.OID = OID;
                pResInfo.LayerName = m_structLengthPara.strFtName;              
            
                
                pResAttr.Add(pResInfo);
                ipRow = pCursor.NextRow();
            }

            return pResAttr;
        }

        private string ConstructClause()
        {
            string strClause;
            //strClause = "abs(shape_length) < " + m_structLengthPara.dbThreshold + "";
            strClause = "abs(shape_length) < " + COMMONCONST.dLengthThread + "";

            string strMid = " AND ";

            if (m_structLengthPara.fieldTypeArray != null)
            {
                if (Convert.ToInt32(m_structLengthPara.fieldTypeArray[0]) != 5)
                {
                    strClause = strClause + strMid + m_structLengthPara.fieldArray[0] + " IS NULL ";
                }
                else
                {
                    strClause = strClause + strMid + " ( " + m_structLengthPara.fieldArray[0]
                                + " IS NULL OR " + m_structLengthPara.fieldArray[0] + " = '' )";
                }

                for (int i = 1; i < m_structLengthPara.fieldArray.Count; i++)
                {
                    if (Convert.ToInt32(m_structLengthPara.fieldTypeArray[i]) != 5)
                        strClause = strClause + strMid + m_structLengthPara.fieldArray[i] + " IS NULL ";
                    else
                    {
                        strClause = strClause + strMid + " ( " + m_structLengthPara.fieldArray[i]
                                    + " IS NULL OR " + m_structLengthPara.fieldArray[i] + " = '' )";
                    }
                }
            }

            return strClause;
        }



    }
}