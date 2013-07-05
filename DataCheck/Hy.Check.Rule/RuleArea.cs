using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geodatabase;

using Hy.Check.Define;
using Hy.Check.Utility;

namespace Hy.Check.Rule
{
    /// <summary>
    /// 面碎屑质检规则
    /// </summary>
    public class RuleArea :BaseRule 
    {

        //检测参数结构体
        public RuleExpression.AreaParameter m_structAreaPara;

        public override string Name
        {
            get { return "面碎屑质检规则"; }
        }

        public override IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {
            m_structAreaPara = new RuleExpression.AreaParameter();

            BinaryReader pParameter = new BinaryReader(new MemoryStream(objParamters));
            pParameter.BaseStream.Position = 0;

            int nCount1 = pParameter.ReadInt32();

            //解析字符串
            int nSize = sizeof(int);
            Byte[] bb = new byte[nCount1];
            pParameter.Read(bb, 0, nCount1);
            string para_str = Encoding.Default.GetString(bb);
            para_str.Trim();

            string[] strResult = para_str.Split('|');

            int i = 0;
            m_structAreaPara.strAlias = strResult[i++];
            m_structAreaPara.strScript = strResult[i++];
            m_structAreaPara.strFtName = strResult[i++];

            m_structAreaPara.dbThreshold = pParameter.ReadDouble();

            int fieldLength = pParameter.ReadInt32();

            //解析字段名
            m_structAreaPara.fieldArray = new List<string>();
            if (fieldLength > 0)
            {
                Byte[] bb1 = new byte[fieldLength];
                pParameter.Read(bb1, 0, fieldLength);
                string strFields = Encoding.Default.GetString(bb1);

                strFields.Trim();

                string[] strResult1 = strFields.Split('|');


                for (int j = 0; j < strResult1.Length; j++)
                {
                    m_structAreaPara.fieldArray.Add(strResult1[j]);
                }
            }

            //取字段类型
            m_structAreaPara.fieldTypeArray = new ArrayList();
            int fieldNum = pParameter.ReadInt32();
            int fType = -1;
            if (fieldNum > 0)
            {
                for (int f = 0; f < fieldNum; f++)
                {
                    fType = pParameter.ReadInt32();
                    m_structAreaPara.fieldTypeArray.Add(fType);
                }
            }
        }

        public override bool Verify()
        {
            if (m_structAreaPara == null)
            {
                SendMessage(enumMessageType.VerifyError, "参数读取不成功");
                return false;
            }

            try
            {
                m_LayerName = base.GetLayerName(m_structAreaPara.strFtName);
                (m_BaseWorkspace as IFeatureWorkspace).OpenFeatureClass(m_LayerName);
            }
            catch
            {
                SendMessage(enumMessageType.VerifyError,string.Format("图层{0}不存在",m_structAreaPara.strFtName));
                return false;
            }

            return true;
        }

        private string m_LayerName;
        public override bool Check(ref List<Error> checkResult)
        {
            ITable ipTable = null;
            ICursor ipCursor = null;
            try
            {
                //根据别名取图层名
                m_LayerName = base.GetLayerName(m_structAreaPara.strFtName);// m_structAreaPara.strFtName; //LayerReader.GetAliasName(m_structAreaPara.strFtName, this.m_SchemaID);

                //打开相应的featureclass
                IFeatureWorkspace ipFtWS;
                ipFtWS = (IFeatureWorkspace)this.m_BaseWorkspace;
                if (ipFtWS != null)
                {
                    try
                    {
                        ipTable = ipFtWS.OpenTable(m_LayerName);
                    }
                    catch (Exception ex)
                    {
                        SendMessage(enumMessageType.RuleError, "当前工作数据库中不存在图层" + m_structAreaPara.strFtName + "，无法执行面碎屑检查!");
                        return false;
                    }
                }
                else
                {
                    SendMessage(enumMessageType.RuleError, "当前工作数据库中不存在图层" + m_structAreaPara.strFtName + "，无法执行面碎屑检查!");
                    return false;
                }

                //执行查询操作	       
                IQueryFilter pFilter = new QueryFilterClass();
                pFilter.WhereClause = ConstructClause();
                pFilter.SubFields = "OBJECTID,BSM,shape_area";

                //试着查询
                try
                {
                    ipCursor = ipTable.Search(pFilter, true);
                }
                catch (Exception ex)
                {
                    SendMessage(enumMessageType.RuleError, "当前工作数据库的图层" + m_structAreaPara.strFtName + "中，SQL语句" + pFilter.WhereClause + "无法执行!");
                    return false;
                }
                //若结果为空，则表达式不正确
                if (ipCursor == null)
                {
                    return false;
                }
               checkResult= GetResult(ipCursor);
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
                SendMessage(enumMessageType.RuleError, "当前检查的对象不是面要素类（没有OID），不进行检查");
                return null;
            }

            IFields pFields = ipRow.Fields;
            int nIndex = pFields.FindField("BSM");

            int nIndexShapeArea = pFields.FindField("Shape_area");

            List<Error> errorList = new List<Error>();
            while (ipRow != null)
            {
                // 添家结果记录
                Error error = new Error();
                error.DefectLevel = this.m_DefectLevel;
                error.RuleID = this.InstanceID;

                error.OID = ipRow.OID;

                if (nIndex >= 0)
                {
                    error.BSM = ipRow.get_Value(nIndex).ToString();

                    double dArea = Convert.ToDouble(ipRow.get_Value(nIndexShapeArea));
                    error.Description = string.Format("'{0}'内标识码为'{1}'、面积为{2}的图斑是碎片多边形。不符合图斑最小上图面积({3})的要求", m_structAreaPara.strFtName, error.BSM, dArea.ToString("f2"), COMMONCONST.dAreaThread);
                }
                else
                {
                    // 错误信息
                    error.Description = m_structAreaPara.strScript;
                }

                error.LayerName = m_structAreaPara.strFtName;


                errorList.Add(error);

                ipRow = pCursor.NextRow();
            }

            return errorList;
        }

        private string ConstructClause()
        {
            string strClause;
            try
            {
                
                //strClause = "abs(shape_Area) <" + m_structAreaPara.dbThreshold + "";
                strClause = "abs(shape_Area) <" + COMMONCONST.dAreaThread+ "";
                
                string strMid = " and ";

                if (m_structAreaPara.fieldTypeArray.Count > 0)
                {
                    if (Convert.ToInt32(m_structAreaPara.fieldTypeArray[0]) != 5)
                    {
                        strClause = strClause + strMid + m_structAreaPara.fieldArray[0] + " IS NULL ";
                    }
                    else
                    {
                        strClause = strClause + strMid + " (" + m_structAreaPara.fieldArray[0]
                                    + " is null or " + m_structAreaPara.fieldArray[0] + " = '')";
                    }
                    for (int i = 1; i < m_structAreaPara.fieldArray.Count; i++)
                    {
                        if (Convert.ToInt32(m_structAreaPara.fieldTypeArray[i]) != 5)
                            strClause = strClause + strMid + m_structAreaPara.fieldArray[i] + " IS NULL ";
                        else
                        {
                            strClause = strClause + strMid + " ( " + m_structAreaPara.fieldArray[i]
                                        + " IS NULL OR " + m_structAreaPara.fieldArray[i] + " = '' )";
                        }
                    }
                }
            }
            catch
            {
                return "";
            }
            return strClause;

        }

    }
}