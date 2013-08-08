using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace TestOracle.TekOracle
{
    public class TekOracleDbProviderFactory : DbProviderFactory
    {
        public override DbCommand CreateCommand()
        {
            return new HyOracleCommand(new DDTek.Oracle.OracleCommand(), null);
        }

        public override DbCommandBuilder CreateCommandBuilder()
        {
            return new HyOracleCommandBuilder(new DDTek.Oracle.OracleCommandBuilder());
        }

        public override DbConnection CreateConnection()
        {
            return new HyOracleConnection();
        }

        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return base.CreateConnectionStringBuilder();
        }

        public override DbDataAdapter CreateDataAdapter()
        {
            return new DDTek.Oracle.OracleDataAdapter();
        }

        public override DbParameter CreateParameter()
        {
            return new HyOracleParameter(new DDTek.Oracle.OracleParameter());
        }

        public override System.Security.CodeAccessPermission CreatePermission(System.Security.Permissions.PermissionState state)
        {
            return new DDTek.Oracle.OraclePermission(state);
        }

        public override DbDataSourceEnumerator CreateDataSourceEnumerator()
        {
            return base.CreateDataSourceEnumerator();
        }

    }
}
