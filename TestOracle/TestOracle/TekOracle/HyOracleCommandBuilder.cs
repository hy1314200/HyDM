using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace TestOracle.TekOracle
{
    public class HyOracleCommandBuilder:DbCommandBuilder
    {
        internal DDTek.Oracle.OracleCommandBuilder InnerBuilder { private set; get; }

        internal HyOracleCommandBuilder(DDTek.Oracle.OracleCommandBuilder innerBuilder)
        {
            this.InnerBuilder = innerBuilder;
        }

        protected override void ApplyParameterInfo(DbParameter parameter, System.Data.DataRow row, System.Data.StatementType statementType, bool whereClause)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
