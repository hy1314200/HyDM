using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using System.Runtime.InteropServices;
using Hy.Check.Define;
using Hy.Check.Utility;

namespace Hy.Check.Rule
{
    public class RuleSQLExpression : BaseRule
    {
        private string strField1 = "";
        private string strField2 = "";
        //根据别名取图层名
        private string layerName = "";

        //查询参数结构体
        private SQLPARA m_structSqlPara = new SQLPARA();

        public RuleSQLExpression()
        {
            //清空查询参数
            m_structSqlPara.strSQLName = "属性表达式(SQL)质检规则";
            m_structSqlPara.strFtName = "";
            m_structSqlPara.strWhereClause = "";
        }

        // 获取检查结果
        private List<Error> GetResult(ICursor pCursor)
        {
            IRow ipRow;
            ipRow = pCursor.NextRow();
            if (ipRow == null)
            {
                return null;
            }
            // 判断是否有OID列
            bool bHasOID;
            bHasOID = ipRow.HasOID;

            if (!bHasOID)
            {
                return null;
            }
            IFields pFields = ipRow.Fields;
            int nIndex = pFields.FindField("BSM");

            int nIndexJSMJ = pFields.FindField(strField1);
            int nIndexJSMJDIST = pFields.FindField(strField2);

            //string strErrInfo = ConstructErrorInfo();
            List<Error> pResAttr = new List<Error>();

            while (ipRow != null)
            {
                int OID;
                OID = ipRow.OID;

                Error err = new Error();
                err.DefectLevel = this.m_DefectLevel;
                err.RuleID = this.InstanceID;

                err.OID = OID;
                err.LayerName = m_structSqlPara.strFtName; // 目标图层              
                if (nIndex >= 0)
                {
                    err.BSM =ipRow.get_Value(nIndex).ToString();
                }

                // 错误信息
                if (m_structSqlPara.strScript.Contains("椭球面积计算不正确"))
                {
                    double dJSMJ = Convert.ToDouble(ipRow.get_Value(nIndexJSMJ));
                    double dJSMJDIST = Convert.ToDouble(ipRow.get_Value(nIndexJSMJDIST));
                    double dPlus = Math.Abs(dJSMJ-dJSMJDIST);
                    //pResInfo.strErrInfo = "数据库椭球面积为"+dJSMJ.ToString("f2")+"平方米，质检软件计算椭球面积为"+dJSMJDIST.ToString("f2")+"平方米，二者相差"+dPlus.ToString("f2")+"平方米";
                    //pResInfo.strErrInfo = string.Format(Helper.ErrMsgFormat.ERR_4401, m_structSqlPara.strFtName, pResInfo.BSM, "数据库椭球面积", dJSMJ.ToString("f2") + "平方米", dJSMJDIST.ToString("f2") + "平方米", dPlus.ToString("f2") + "平方米");
                    err.Description = string.Format("数据库中'{0}'层中标识码为'{1}'的{2}({3}平方米)与计算的椭球面积({4}平方米)不一致，两者差值为{5}平方米",
                        m_structSqlPara.strFtName, err.BSM, "数据库椭球面积", dJSMJ.ToString("f2"), dJSMJDIST.ToString("f2"), dPlus.ToString("f2"));
                }              
                else
                {
                    err.Description = m_structSqlPara.strScript;
                    //pResInfo.strErrInfo = string.Format("'{0}'图层标识码为'{1}'的图斑地类面积的值不正确，应大于0", pResInfo.strTargetLayer, pResInfo.BSM);
                }

                pResAttr.Add(err);
                ipRow = pCursor.NextRow();
            }
            return pResAttr;
        }

        public override string Name
        {
            get
            {
                return m_structSqlPara.strAlias;
            }
        }

        public override IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {
            if (objParamters == null || objParamters.Length == 0) return;

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
            m_structSqlPara.strAlias = strResult[i++];
            m_structSqlPara.strScript = strResult[i++];
            m_structSqlPara.strFtName = strResult[i++];
            m_structSqlPara.strWhereClause = strResult[i];
            return ;
        }

        public override bool Verify()
        {
            try
            {
                if (string.IsNullOrEmpty(m_structSqlPara.strFtName) ||
                    string.IsNullOrEmpty(m_structSqlPara.strWhereClause))
                {
                    //XtraMessageBox.Show("检查目标或检查表达式不存在，无法执行检查！");
                    SendMessage(enumMessageType.VerifyError, "读取规则参数失败！");
                    return false;
                }

                //打开相应的featureclass
                if (base.m_QueryWorkspace is IFeatureWorkspace)
                {
                    IFeatureWorkspace ipFtWS = (IFeatureWorkspace)base.m_QueryWorkspace;
                    if(!(ipFtWS  as IWorkspace2).get_NameExists(esriDatasetType.esriDTTable,layerName))
                    {
                        string strLog = string.Format("当前数据库中不存在图层 {0}，无法执行属性表达式(SQL)检查!", layerName);
                        SendMessage(enumMessageType.VerifyError, strLog);
                        return false;
                    }
                    //获取图层别名
                    layerName = base.GetLayerName(m_structSqlPara.strAlias);
                }
                else
                {
                    SendMessage(enumMessageType.VerifyError, "当前工作数据库无法打开，无法执行属性表达式(SQL)检查!");
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public override bool Check(ref List<Error> checkResult)
        {
            ITable ipTable = null;
            ICursor ipCursor = null;
            checkResult = new List<Error>();
            try
            {
                if (m_structSqlPara.strFtName == "线状地物" && 
                    m_structSqlPara.strAlias == "SQL_线状地物宽度")
                {
                    int nKDThread = Convert.ToInt32(COMMONCONST.nMapScale * 0.002);
                    m_structSqlPara.strWhereClause =string.Format("[KD] >= {0} or [KD] < 0", nKDThread);
                    m_structSqlPara.strScript =string.Format("线状地物宽度字段值大于等于{0} 或者小于0", nKDThread);
                }

                //执行查询操作	       
                IQueryFilter pFilter = new QueryFilterClass();

                pFilter.WhereClause = m_structSqlPara.strWhereClause;

                if (m_structSqlPara.strScript.Contains("椭球面积计算不正确"))
                {
                    string strTemp = m_structSqlPara.strWhereClause;
                    int n1 = strTemp.IndexOf('[');
                    int n2 = strTemp.IndexOf(']');

                    int n3 = strTemp.LastIndexOf('[');
                    int n4 = strTemp.LastIndexOf(']');

                    strField1 = strTemp.Substring(n1 + 1, n2 - n1 - 1);

                    strField2 = strTemp.Substring(n3 + 1, n4 - n3 - 1);
                    pFilter.SubFields = "OBJECTID,BSM," + strField1 + "," + strField2;
                }
                else
                {
                    pFilter.SubFields = "OBJECTID,BSM";
                }

                //试着查询
                try
                {
                    ipCursor = ipTable.Search(pFilter, true);
                }
                catch (Exception ex)
                {
                    SendMessage(enumMessageType.RuleError, string.Format("Sql语句{0}无法在{1}图层中执行", pFilter.WhereClause, layerName));
                    SendMessage(enumMessageType.Exception, string.Format("Sql语句{0}无法在{1}图层中执行，原因:{2}", pFilter.WhereClause, layerName, ex.Message));
                    return false;
                }

                //若结果为空，则表达式不正确
                if (ipCursor == null)
                {

                    return false;
                }

                checkResult = GetResult(ipCursor);

                if (checkResult == null)
                    return true;
            }
            catch
            {
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
    }

    //SQL查询检测参数结构体
    public struct SQLPARA
    {
        public string strSQLName; //质检规则类别
        public string strAlias; //查询规则别名
        public string strScript; //描述
        public string strFtName; //要素类名,是否需要
        public string strWhereClause; //查询条件
    } ;
}