using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Hy.Oracle
{
    public class HyOracleCommandBuilder : DbCommandBuilder
    {
        public HyOracleCommandBuilder()
            : this(new DDTek.Oracle.OracleCommandBuilder())
        {
        }

        internal DDTek.Oracle.OracleCommandBuilder InnerBuilder { private set; get; }

        internal HyOracleCommandBuilder(DDTek.Oracle.OracleCommandBuilder innerBuilder)
        {
            this.InnerBuilder = innerBuilder;
        }

        protected override void ApplyParameterInfo(DbParameter parameter, System.Data.DataRow row, System.Data.StatementType statementType, bool whereClause)
        {

        }

        protected override string GetParameterName(string parameterName)
        {
            throw new NotImplementedException();
        }

        protected override string GetParameterName(int parameterOrdinal)
        {
            throw new NotImplementedException();
        }

        protected override string GetParameterPlaceholder(int parameterOrdinal)
        {
            throw new NotImplementedException();
        }

        protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
        {
            this.InnerBuilder.DataAdapter = adapter as DDTek.Oracle.OracleDataAdapter;
        }

        public new DbDataAdapter DataAdapter
        {
            get
            {
                return new HyOracleDataAdapter(this.InnerBuilder.DataAdapter);
            }

            set
            {
                this.InnerBuilder.DataAdapter = (value as HyOracleDataAdapter).InnerAdapter;
            }
        }

        public DbCommand GetDeleteCommand()
        {
            return new HyOracleCommand(this.InnerBuilder.GetDeleteCommand());
        }
        public new DbCommand GetDeleteCommand(bool useColumnsForParameterNames)
        {
            return new HyOracleCommand(this.InnerBuilder.GetDeleteCommand());
        }
        public new DbCommand GetInsertCommand()
        {
            return new HyOracleCommand(this.InnerBuilder.GetInsertCommand());
        }
        public new DbCommand GetInsertCommand(bool useColumnsForParameterNames)
        {
            return new HyOracleCommand(this.InnerBuilder.GetInsertCommand());
        }
        public new DbCommand GetUpdateCommand()
        {
            return new HyOracleCommand(this.InnerBuilder.GetUpdateCommand());
        }
        public new DbCommand GetUpdateCommand(bool useColumnsForParameterNames)
        {
            return new HyOracleCommand(this.InnerBuilder.GetUpdateCommand());
        }
    }
}
