using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Hy.Oracle
{
    public  class HyOracleTransaction:DbTransaction
    {
        private DDTek.Oracle.OracleTransaction m_Transaction;
        internal DDTek.Oracle.OracleTransaction InnerTransaction { get { return this.m_Transaction; } }

        HyOracleConnection m_Connection;


        internal HyOracleTransaction(DDTek.Oracle.OracleTransaction trans)
        {
            this.m_Transaction = trans;
            this.m_Connection = new HyOracleConnection(trans.Connection);
        }

        public override void Commit()
        {
            m_Transaction.Commit();
        }

        protected override DbConnection DbConnection
        {
            get { return this.m_Connection; }
        }

        public override System.Data.IsolationLevel IsolationLevel
        {
            get { return m_Transaction.IsolationLevel; }
        }

        public override void Rollback()
        {
            m_Transaction.Rollback();
        }
    }
}
