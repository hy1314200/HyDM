using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NHibernate.SqlTypes;
using NHibernate.Engine.Query;

namespace Hy.Oracle.NHibernateDriver
{
    public class HyOracleDriver:NHibernate.Driver.OracleClientDriver
    {
        private static readonly SqlType GuidSqlType = new SqlType(DbType.Binary, 16);
        private readonly Hy.Oracle.HyOracleDbProviderFactory m_ProviderFactory = new HyOracleDbProviderFactory();
        public override System.Data.IDbConnection CreateConnection()
        {
            return this.m_ProviderFactory.CreateConnection();
          
        }

        public override System.Data.IDbCommand CreateCommand()
        {
            return this.m_ProviderFactory.CreateCommand();
        }

        //public override bool UseNamedPrefixInSql
        //{
        //    get { return true; }
        //}

        //public override bool UseNamedPrefixInParameter
        //{
        //    get { return true; }
        //}

        //public override string NamedPrefix
        //{
        //    get { return ":"; }
        //}


        //protected override void InitializeParameter(IDbDataParameter dbParam, string name, SqlType sqlType)
        //{
        //    if (sqlType.DbType == DbType.Guid)
        //    {
        //        base.InitializeParameter(dbParam, name, GuidSqlType);
        //    }
        //    else
        //    {
        //        base.InitializeParameter(dbParam, name, sqlType);
        //    }
        //}

        //protected override void OnBeforePrepare(IDbCommand command)
        //{
        //    base.OnBeforePrepare(command);

        //    CallableParser.Detail detail = CallableParser.Parse(command.CommandText);

        //    if (!detail.IsCallable)
        //        return;

        //    throw new System.NotImplementedException(GetType().Name +
        //        " does not support CallableStatement syntax (stored procedures)." +
        //        " Consider using OracleDataClientDriver instead.");
        //}
    }
}
