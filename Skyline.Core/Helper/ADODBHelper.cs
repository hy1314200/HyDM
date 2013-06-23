using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.Common;
using System.Windows.Forms;
using System.Configuration;

namespace Skyline.Core.Helper
{
    
    public enum DatabaseType
    {
        Access,
        SQLServer,
        Oracle
        // any other data source type
    }

    public enum ParameterType
    {
        Integer,
        Char,
        VarChar
        // define a common parameter type set
    }

    public class ADODBHelper : IDisposable
    {
        private DatabaseType currDbType;
        private IDbConnection cnn = null;
        private IDbTransaction tran = null;
        private SortedList paramcollection = null;

        /// <summary>
        /// 2012-11-19 张航宇
        /// 修改从配置读取数据库类型，使第二个参数失效
        /// </summary>
        /// <param name="sConnStr"></param>
        /// 
        /// <param name="bOpen"></param>
        public ADODBHelper(string sConnStr, bool bOpen)
        {
            this.paramcollection = new SortedList();
            // 2012-11-19 张航宇
            // 修改从配置读取数据库类型
            //this.currDbType = dbtype;
            this.currDbType = DBTypeFromConfig();
            this.cnn = CreateConnection(sConnStr, this.currDbType);

            if (bOpen)
            {
                this.cnn.Open();
            }
        }

        public static DatabaseType DBTypeFromConfig()
        {
            string strType = ConfigurationManager.AppSettings["Type"].ToUpper();
            switch (strType)
            {
                case "ORACLE":
                    return DatabaseType.Oracle;

                case "MSSQL":
                case "SQLSERVER":
                case "SQL SERVER":
                   return DatabaseType.SQLServer;

                case "MDB":
                case "ACCESS":
                   return DatabaseType.Access;

                default:
                    throw new Exception("配置的ADO数据库类型不被支持，应该在ORACLE、SQLSERVER、ACCESS当中");


            }
        }
        
        public static string ConfigConnectionString
        {
            get
            {
                return string.Format(ConfigurationManager.AppSettings["ADOConnection"], System.Windows.Forms.Application.StartupPath);
            }
        }

        public ADODBHelper()
        {

            this.paramcollection = new SortedList();
            this.currDbType = DBTypeFromConfig();
            this.cnn = CreateConnection(ConfigConnectionString, this.currDbType);

            this.cnn.Open();
        }

        public static DatabaseType GetDbTypeByName(string sName)
        {
            DatabaseType result = DatabaseType.SQLServer;
            for (int i = 0; i < 3; i++)
            {
                result = (DatabaseType)i;
                if (result.ToString().ToLower() == sName.ToLower())
                {
                    break;
                }
            }
            return result;
        }

        public SortedList Parameters
        {
            get
            {
                return this.paramcollection;
            }
        }

        public IDbConnection DBConnection
        {
            get { return this.cnn; }
        }

        public IDbConnection CreateConnection(string ConnectionString, DatabaseType dbtype)
        {
            try
            {
                switch (dbtype)
                {
                    case DatabaseType.Access:
                        cnn = new OleDbConnection
                            (ConnectionString);
                        break;
                    case DatabaseType.SQLServer:
                        cnn = new SqlConnection
                            (ConnectionString);
                        break;
                    case DatabaseType.Oracle:
                        cnn = new OracleConnection
                            (ConnectionString);
                        break;
                    default:
                        cnn = new SqlConnection
                            (ConnectionString);
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }


            return cnn;
        }
        public IDbCommand CreateCommand(string CommandText, DatabaseType dbtype, IDbConnection cnn)
        {
            IDbCommand cmd;
            switch (dbtype)
            {
                case DatabaseType.Access:
                    cmd = new OleDbCommand
                       (CommandText,
                       (OleDbConnection)cnn);
                    break;

                case DatabaseType.SQLServer:
                    cmd = new SqlCommand
                       (CommandText,
                       (SqlConnection)cnn);
                    break;

                case DatabaseType.Oracle:
                    cmd = new OracleCommand
                       (CommandText,
                       (OracleConnection)cnn);
                    break;
                default:
                    cmd = new SqlCommand
                       (CommandText,
                       (SqlConnection)cnn);
                    break;
            }

            return cmd;
        }


        public DbDataAdapter CreateAdapter(IDbCommand cmd, DatabaseType dbtype)
        {
            DbDataAdapter da;
            switch (dbtype)
            {
                case DatabaseType.Access:
                    da = new OleDbDataAdapter
                       ((OleDbCommand)cmd);
                    break;

                case DatabaseType.SQLServer:
                    da = new SqlDataAdapter
                       ((SqlCommand)cmd);
                    break;

                case DatabaseType.Oracle:
                    da = new OracleDataAdapter
                       ((OracleCommand)cmd);
                    break;

                default:
                    da = new SqlDataAdapter
                       ((SqlCommand)cmd);
                    break;
            }
            return da;
        }

        public IDbTransaction BeginTrans()
        {
            if (this.tran == null)
                switch (this.currDbType)
                {
                    case DatabaseType.Access:
                        this.tran = ((OleDbConnection)cnn).BeginTransaction();
                        break;

                    case DatabaseType.SQLServer:
                        this.tran = ((SqlConnection)cnn).BeginTransaction();
                        break;

                    case DatabaseType.Oracle:
                        this.tran = ((OracleConnection)cnn).BeginTransaction();
                        break;

                    default:
                        this.tran = ((SqlConnection)cnn).BeginTransaction();
                        break;
                }
            return this.tran;
        }

        public void Commit()
        {
            if (this.tran != null)
                this.tran.Commit();

            this.tran = null;
        }

        public void Rollback()
        {
            if (this.tran != null)
                this.tran.Rollback();

            this.tran = null;
        }

        public bool OpenDB()
        {
            bool bOpen = false;
            if (this.cnn != null && this.cnn.State != ConnectionState.Open)
            {
                this.cnn.Open();
                bOpen = this.cnn.State == ConnectionState.Open;

            }
            return bOpen;
        }

        public void CloseDB()
        {
            this.Dispose();
        }

        public DataSet OpenDS(string sql)
        {
            DataSet dsResult = null;
            if (this.cnn.State == ConnectionState.Open)
            {
                IDbCommand cmd = this.CreateCommand(sql, this.currDbType, this.cnn);

                this.SetParameters(cmd);
                IDbDataAdapter adp = this.CreateAdapter(cmd, this.currDbType);

                dsResult = new DataSet();
                adp.Fill(dsResult);
            }

            return dsResult;
        }

        private void SetParameters(IDbCommand cmd)
        {
            if (this.paramcollection.Count > 0)
                for (int i = 0; i < paramcollection.Count; i++)
                {
                    // 修正参数值为null时,把其值改为数据库的空值
                    object val = paramcollection.GetByIndex(i);
                    if (val == null)
                        val = DBNull.Value;
                    switch (this.currDbType)
                    {
                        case DatabaseType.Access:
                            cmd.Parameters.Add(new OleDbParameter(paramcollection.GetKey(i).ToString(), val));
                            break;

                        case DatabaseType.SQLServer:
                            cmd.Parameters.Add(new SqlParameter(paramcollection.GetKey(i).ToString(), val));
                            break;

                        case DatabaseType.Oracle:
                            cmd.Parameters.Add(new OracleParameter(paramcollection.GetKey(i).ToString(), val));
                            break;

                        //default:
                        //    cmd.Parameters.Add(new SqlParameter(paranName, paramcollection[paranName]));
                    }
                }
        }

        public int ExecSQL(string sql)
        {
            int nResult = -1;
            try
            {
                if (this.cnn.State == ConnectionState.Open)
                {
                    IDbCommand cmd = this.CreateCommand(sql, this.currDbType, this.cnn);
                    cmd.Transaction = this.tran;
                    this.SetParameters(cmd);

                    nResult = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return nResult;
        }

        public int ExecStoredProc(string sql)
        {
            int nResult = -1;
            if (this.cnn.State == ConnectionState.Open)
            {
                IDbCommand cmd = this.CreateCommand(sql, this.currDbType, this.cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = this.tran;
                this.SetParameters(cmd);
                //nResult = cmd.ExecuteNonQuery();
            }
            return nResult;
        }

        public object ExecSQLScalar(string sql)
        {
            object nResult = null;
            if (this.cnn.State == ConnectionState.Open)
            {
                IDbCommand cmd = this.CreateCommand(sql, this.currDbType, this.cnn);
                cmd.Transaction = this.tran;
                this.SetParameters(cmd);

                nResult = cmd.ExecuteScalar();
            }
            return nResult;
        }

        public IDataReader ExecSQLReader(string sql)
        {
            IDataReader nResult = null;
            if (this.cnn.State == ConnectionState.Open)
            {
                IDbCommand cmd = this.CreateCommand(sql, this.currDbType, this.cnn);
                cmd.Transaction = this.tran;
                this.SetParameters(cmd);

                nResult = cmd.ExecuteReader();
            }
            return nResult;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            if (this.paramcollection != null)
            {
                this.paramcollection.Clear();
                this.paramcollection = null;
            }
            if (this.cnn != null)
            {
                if (this.cnn.State == ConnectionState.Open)
                    this.cnn.Close();

                this.cnn.Dispose();
                this.cnn = null;
            }
        }
        #endregion
    }
    
}
