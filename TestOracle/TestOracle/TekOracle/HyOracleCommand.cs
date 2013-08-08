using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace TestOracle.TekOracle
{
    public class HyOracleCommand:DbCommand
    {
        private DDTek.Oracle.OracleCommand m_Command;
        internal DDTek.Oracle.OracleCommand InnerCommand { get { return this.m_Command; } }
        HyOracleConnection m_Connection;
        internal HyOracleCommand(DDTek.Oracle.OracleCommand cmd, HyOracleConnection conn)
        {
            this.m_Command = cmd;
            this.m_Connection = conn;
        }

        public override void Cancel()
        {
            this.m_Command.Cancel();
        }

        public override string CommandText
        {
            get
            {
                return this.m_Command.CommandText;
            }
            set
            {
                this.m_Command.CommandText = value;
            }
        }

        public override int CommandTimeout
        {
            get
            {
                return this.m_Command.CommandTimeout;
            }
            set
            {
                this.m_Command.CommandTimeout = value;
            }
        }

        public override System.Data.CommandType CommandType
        {
            get
            {
                return this.m_Command.CommandType;
            }
            set
            {
                this.m_Command.CommandType = value;
            }
        }

        protected override DbParameter CreateDbParameter()
        {
            return new HyOracleParameter(this.m_Command.CreateParameter());
        }

        protected override DbConnection DbConnection
        {
            get
            {
                return this.m_Connection;
            }
            set
            {
                this.m_Connection = value as HyOracleConnection;
                this.m_Command.Connection = this.m_Connection.InnerConnection;
            }
        }

        private HyOracleParameterCollection m_ParameterCollection;
        protected override DbParameterCollection DbParameterCollection
        {
            get
            {
                if (this.m_ParameterCollection == null)
                    this.m_ParameterCollection = new HyOracleParameterCollection(this.m_Command.Parameters);

                return this.m_ParameterCollection;
            }
        }

        HyOracleTransaction m_Transaction = null;
        protected override DbTransaction DbTransaction
        {
            get
            {
                if (this.m_Transaction == null)
                    this.m_Transaction = new HyOracleTransaction(this.m_Command.Transaction, this.m_Connection);

                return this.m_Transaction;
            }
            set
            {
                this.m_Transaction = value as HyOracleTransaction;
                this.m_Command.Transaction = this.m_Transaction.InnerTransaction;
            }
        }

        public override bool DesignTimeVisible
        {
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        protected override DbDataReader ExecuteDbDataReader(System.Data.CommandBehavior behavior)
        {
            return new HyOracleDataReader(this.m_Command.ExecuteReader(behavior));
        }

        public override int ExecuteNonQuery()
        {
            return this.m_Command.ExecuteNonQuery();
        }

        public override object ExecuteScalar()
        {
            return this.m_Command.ExecuteScalar();
        }

        public override void Prepare()
        {
            this.m_Command.Prepare();
        }

        public override System.Data.UpdateRowSource UpdatedRowSource
        {
            get
            {
                return this.m_Command.UpdatedRowSource;
            }
            set
            {
                this.m_Command.UpdatedRowSource = value;
            }
        }
    }
}
