using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace TestOracle.TekOracle
{
    public  class HyOracleConnection:DbConnection
    {
        private DDTek.Oracle.OracleConnection m_Connection = new DDTek.Oracle.OracleConnection();
        internal DDTek.Oracle.OracleConnection InnerConnection { get { return this.m_Connection; } }

        protected override DbTransaction BeginDbTransaction(System.Data.IsolationLevel isolationLevel)
        {
            return new HyOracleTransaction(this.m_Connection.BeginTransaction(isolationLevel), this);
        }

        public override void ChangeDatabase(string databaseName)
        {
            throw new Exception("不支持数据库切换");
        }

        public override void Close()
        {
            this.m_Connection.Close();
        }

        public override string ConnectionString
        {
            get
            {
                return this.m_Connection.ConnectionString;
            }
            set
            {
                this.m_Connection.ConnectionString = value;
            }
        }

        protected override DbCommand CreateDbCommand()
        {
            return new HyOracleCommand(this.m_Connection.CreateCommand(), this);
        }

        public override string DataSource
        {
            get { return this.m_Connection.ServerName; }
        }

        public override string Database
        {
            get { return this.m_Connection.ServiceName; }
        }

        public override void Open()
        {
            this.m_Connection.Open();
        }

        public override string ServerVersion
        {
            get { return this.m_Connection.ServerVersion; }
        }

        public override System.Data.ConnectionState State
        {
            get { return this.m_Connection.State; }
        }
    }
}
