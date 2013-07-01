using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Data.Common;
using System.Data;
using Common.Utility.Encryption;
using Common.Utility.Log;
using System.Collections;
using Common.Utility.Data;

namespace Check.Utility
{
    /// <summary>
    /// 系统库连接操作类(sysdb)
    /// </summary>
    public class SysDbHelper
    {
        /// <summary>
        /// 默认路径
        /// </summary>
        private  static  string m_SysDbPath=string.Format("{0}\\{1}",new FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName,COMMONCONST.SYSDBName);

        /// <summary>
        /// 静态全局函数，记录系统库的数据库链接
        /// </summary>
        private static IDbConnection m_syscon;

        /// <summary>
        /// 指定系统库的类型，如果类型修改，直接改变此变量
        /// </summary>
        public static DatabaseType CurrentDatabaseType = DatabaseType.Access;

        private static DbProviderFactory m_Dbfactory = DBConnFactory.GetDbProviderFactory(CurrentDatabaseType);

        /// <summary>
        /// 系统库路径
        /// </summary>
        public static string SysDbPath
        {
            set { m_SysDbPath = value; }
            get { return m_SysDbPath; }
        }

        /// <summary>
        /// Get_s the sysdb connection.
        /// </summary>
        /// <returns></returns>
        public static IDbConnection GetSysDbConnection()
        {
            try
            {
                if (m_syscon != null && m_syscon.State != ConnectionState.Closed) return m_syscon;

                string strConn;
                if (string.IsNullOrEmpty(m_SysDbPath))
                {
                    OperationalLogManager.AppendMessage("找不到系统库！请正确设置系统库路径");
                    return null;
                }

                AccessMDBEncrypt AccessMDB = new AccessMDBEncrypt();
                AccessMDB.FileToUntie(m_SysDbPath);
                strConn = @"PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + m_SysDbPath + ";Persist Security Info=False";

                m_syscon = m_Dbfactory.CreateConnection();
                m_syscon.ConnectionString = strConn;

                while (m_syscon.State.Equals(ConnectionState.Closed))
                {
                    try
                    {
                        m_syscon.Open();
                    }
                    catch (Exception exp)
                    {
                        OperationalLogManager.AppendMessage(exp.ToString());
                        return null;
                    }
                }
                AccessMDB.FileToEncrypt(m_SysDbPath);
                return m_syscon;
            }
            catch (Exception exp)
            {
                OperationalLogManager.AppendMessage(exp.ToString());
                m_syscon = null;
            }
            return null;
        }

        /// <summary>
        /// Disposes the sys db conn.
        /// </summary>
        public static void DisposeSysDbConn()
        {
            if (m_syscon == null) return;
            try
            {
                m_syscon.Close();
                m_syscon.Dispose();
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());
            }
            finally
            {
                GC.Collect(GC.GetGeneration(m_syscon));
                m_syscon = null;
            }
            return;
        }

        /// <summary>
        /// 获取任务的编号
        /// </summary>
        /// <param name="strModelTaskNumber"></param>
        /// <returns></returns>
        public static bool GetTaskID(out string strModelTaskNumber)//, IDbConnection pConnection)
        {
            IDbConnection pConnection = GetSysDbConnection();
            IDataReader reader = null;
            strModelTaskNumber = string.Empty;
            try
            {
                string strdate = DateTime.Today.Year.ToString();
                //给任务添加编号
                strdate = "T" + strdate;
                string strSql = string.Format("SELECT max(TaskID) FROM LR_ModelTask where Left(TaskID,5) =  '{0}'", strdate);

                reader = AdoDbHelper.GetQueryReader(pConnection, strSql);

                //如果为空，表示获取不成功，返回结果
                if (reader == null)
                {
                    return false;
                }

                if (!(reader as System.Data.OleDb.OleDbDataReader).HasRows)
                {
                    strModelTaskNumber = strdate + "001";
                    return true;
                }
                while (reader.Read())
                {
                    string strNum = reader[0].ToString();
                    if (string.IsNullOrEmpty(strNum))
                    {
                        strModelTaskNumber = strdate + "001";
                    }
                    else
                    {
                        int nID = Convert.ToInt32(strNum.Substring(1)) + 1;
                        strModelTaskNumber = strNum.Substring(0, 1) + nID.ToString();
                    }
                    return true;
                }
            }
            catch (Exception exp) //捕捉异常
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
            finally
            {
                if(reader!=null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
            return true;
        }

        public static string GetStandardName(int standardID)
        {
            DataTable tStandard = AdoDbHelper.GetDataTable(GetSysDbConnection(), string.Format("select StandardName from LR_DicStandard where StandardID={0}", standardID));
            if (tStandard == null || tStandard.Rows.Count == 0)
                return null;

            return tStandard.Rows[0][0] as string;
        }

        public static int GetStandardID(string standardName)
        {
            DataTable tStandard = AdoDbHelper.GetDataTable(GetSysDbConnection(), string.Format("select StandardID  from LR_DicStandard where StandardName='{0}'", standardName));
            if (tStandard == null || tStandard.Rows.Count == 0)
                return -1;

            return Convert.ToInt32(tStandard.Rows[0][0]);
        }
        public static int GetStandardIDBySchemaID(string schemaID)
        {
            DataTable tStandard = AdoDbHelper.GetDataTable(GetSysDbConnection(), string.Format("select a.StandardID from LR_DicStandard  as a, LR_ModelSchema as b where b.SchemaID='{0}' and a.StandardName=b.StandardName", schemaID));
            if (tStandard == null || tStandard.Rows.Count == 0)
                return -1;

            return Convert.ToInt32(tStandard.Rows[0][0]);
        }

        public static string GetStandardNameBySchemaID(string schemaID)
        {
            DataTable tStandard = AdoDbHelper.GetDataTable(GetSysDbConnection(), string.Format("select StandardName  from LR_ModelSchema where SchemaID='{0}'", schemaID));
            if (tStandard == null || tStandard.Rows.Count == 0)
                return null;

            return tStandard.Rows[0][0] as string;
        }

        /// <summary>
        /// 获取系统库中的标准信息.
        /// </summary>
        /// <returns>Dictionary<standardId,standardName></returns>
        public static Dictionary<int,string > GetStandardInfo()
        {
            IDataReader reader = null;
            Dictionary<int,string> standardInfo=new Dictionary<int,string>();
            try
            {
                string strSql = "SELECT StandardID,StandardName  FROM LR_DicStandard order by StandardID";

                reader = AdoDbHelper.GetQueryReader(GetSysDbConnection(), strSql);

                //如果为空，表示获取不成功，返回结果
                if (reader == null)
                {
                    return null;
                }
                while (reader.Read())
                {
                   standardInfo.Add(Convert.ToInt32(reader[0]),reader[1].ToString());
                }
                return standardInfo;
            }
            catch (Exception exp) //捕捉异常
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return null;
            }
            finally
            {
                if(reader!=null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
        }

        /// <summary>
        /// 获取系统库中的方案信息.
        /// </summary>
        /// <returns>Dictionary&lt;SchemaID,SchemaName&gt;</returns>
        public static Dictionary<string, string> GetSchemasInfo(string standardName)
        {
            IDataReader reader = null;
            Dictionary<string, string> schemaInfo = new Dictionary<string, string>();
            try
            {
                if (string.IsNullOrEmpty(standardName))
                {
                    return null;
                }
                string strSql =string.Format("SELECT SchemaID,SchemaName  FROM LR_ModelSchema where  standardName='{0}'",standardName);

                reader = AdoDbHelper.GetQueryReader(GetSysDbConnection(), strSql);

                //如果为空，表示获取不成功，返回结果
                if (reader == null)
                {
                    return null;
                }
                while (reader.Read())
                {
                    schemaInfo.Add(reader[0].ToString(), reader[1].ToString());
                }
                return schemaInfo;
            }
            catch (Exception exp) //捕捉异常
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return null;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
        }

        /// <summary>
        /// 获取权重信息对照表
        /// </summary>
        /// <param name="TopoTable">拓扑错误信息的权重信息：key:ArcGISRule,value:ErrType</param>
        /// <returns>返回属性表的权重信息，key:RuleInstID,value:ErrType</returns>
        public static Hashtable GetEvaWeightTable(string schemaId,ref Hashtable topoTable)
        {

            DataTable dt = new DataTable();

            DataTable dt1 = null;
            try
            {
                if (string.IsNullOrEmpty(schemaId))
                {
                    return null;
                }
                string strSql = String.Format("SELECT * FROM LR_EvaluateModel WHERE SchemaId='{0}'", schemaId);
                dt1 = AdoDbHelper.GetDataTable(GetSysDbConnection(), strSql);
                if (dt1 == null || dt1.Rows.Count == 0)
                {
                    return null;
                }
                string strModelId = dt1.Rows[0]["ModelID"].ToString();

                return GetEvaWeightTableByModelID(strModelId, ref topoTable);
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return null ;
            }
            finally
            {
                dt.Dispose();

                if (dt1!= null)
                {
                    dt1.Dispose();
                }
            }
        }

        /// <summary>
        /// 获取权重信息对照表
        /// </summary>
        /// <param name="TopoTable">拓扑错误信息的权重信息：key:ArcGISRule,value:ErrType</param>
        /// <returns>返回属性表的权重信息，key:RuleInstID,value:ErrType</returns>
        public static Hashtable GetEvaWeightTableByModelID(string EvModelId, ref Hashtable TopoTable)
        {
            Hashtable AttrHash = new Hashtable();
            TopoTable = new Hashtable();
            DataTable dt = new DataTable();
            try
            {
                if (string.IsNullOrEmpty(EvModelId))
                {
                    return null;
                }
                string strSql = string.Format("select * from LR_EvaHMWeight where ModelID='{0}'", EvModelId);

                dt = AdoDbHelper.GetDataTable(GetSysDbConnection(), strSql);
                if (dt == null || dt.Rows.Count == 0)
                {
                    return null;
                }

                foreach (DataRow dr in dt.Rows)
                {
                    if (!Convert.ToBoolean(dr["IsTopoRule"]))
                    {
                        if (!AttrHash.Contains(dr["ElementID"].ToString()))
                        {
                            AttrHash.Add(dr["ElementID"].ToString(), dr["ErrType"].ToString());
                        }
                    }
                    else
                    {
                        if (!TopoTable.Contains(dr["ArcGISRule"].ToString()))
                        {
                            TopoTable.Add(dr["ArcGISRule"].ToString(), dr["ErrType"].ToString());
                        }
                    }
                }
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return null;
            }
            finally
            {
                dt.Dispose();
            }
            return AttrHash;
        }

        public static DataTable GetEvaluateModel(string EvModelId)
        {
            DataTable dt = null;
            try
            {
                if (string.IsNullOrEmpty(EvModelId))
                {
                    return dt;
                }
                string strSql = String.Format("SELECT * FROM LR_EvaluateModel WHERE ModelID='{0}'", EvModelId);
                dt = AdoDbHelper.GetDataTable(GetSysDbConnection(), strSql);
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return dt;
            }
            return dt;
        }

        /// <summary>
        /// 获取上一级规则的规则列表.用于规则列表分级
        /// </summary>
        /// <param name="schemaId">The schema id.</param>
        /// <returns></returns>
        public static DataTable GetRulesClassifyList(string schemaId)
        {
            DataTable dt=null;
            try
            {
                if (string.IsNullOrEmpty(schemaId))
                {
                    return dt;
                }
                string strSql = String.Format("SELECT * FROM LR_CheckTypeModel WHERE SchemaID='{0}' order by YJMLBM,EJMLBM,SJMLBM", schemaId);
                dt = AdoDbHelper.GetDataTable(GetSysDbConnection(), strSql);
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return dt;
            }
            return dt;
        }


        public static DataTable GetSchemaTemplateRules(string schemaId)
        {
            DataTable dt = null;
            try
            {
                if (string.IsNullOrEmpty(schemaId))
                {
                    return dt;
                }
                string strSql = String.Format("SELECT * FROM LR_ModelSchemaTemp WHERE SchemaID='{0}'", schemaId);
                dt = AdoDbHelper.GetDataTable(GetSysDbConnection(), strSql);
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return dt;
            }
            return dt;
        }

        public static DataTable GetSchemaRulesPara(string schemaId)
        {
            DataTable dt = null;
            try
            {
                if (string.IsNullOrEmpty(schemaId))
                {
                    return dt;
                }
                string strSql = String.Format("SELECT * FROM LR_ModelSchemaPara WHERE SchemaID='{0}'", schemaId);
                dt = AdoDbHelper.GetDataTable(GetSysDbConnection(), strSql);
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return dt;
            }
            return dt;
        }

        /// <summary>
        /// 获取所有规则类信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllModelRules()
        {
            DataTable dt = null;
            try
            {
                string strSQL = "select * from LR_ModelRule";
                dt = AdoDbHelper.GetDataTable(GetSysDbConnection(), strSQL);
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return dt;
            }
            return dt;
        }
    }
}