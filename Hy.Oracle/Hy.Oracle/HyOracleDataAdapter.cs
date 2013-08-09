using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace Hy.Oracle
{
    public class HyOracleDataAdapter : DbDataAdapter
    {
        internal DDTek.Oracle.OracleDataAdapter InnerAdapter { get; private set; }

        public HyOracleDataAdapter(DDTek.Oracle.OracleDataAdapter innerAdapter)
        {
            this.InnerAdapter = innerAdapter;
        }

        public HyOracleDataAdapter():this(new DDTek.Oracle.OracleDataAdapter())
        {
             
        }

        public new DbCommand SelectCommand
        {
            get
            {
                return new HyOracleCommand(this.InnerAdapter.SelectCommand);
            }
            set
            {
                this.InnerAdapter.SelectCommand = (value as HyOracleCommand).InnerCommand;

            }
        }
        public new DbCommand DeleteCommand
        {
            get
            {
                return new HyOracleCommand(this.InnerAdapter.DeleteCommand);
            }
            set
            {
                this.InnerAdapter.DeleteCommand = (value as HyOracleCommand).InnerCommand;

            }
        }
        public new DbCommand InsertCommand
        {
            get
            {
                return new HyOracleCommand(this.InnerAdapter.InsertCommand);
            }
            set
            {
                this.InnerAdapter.InsertCommand = (value as HyOracleCommand).InnerCommand;

            }
        }
        public new DbCommand UpdateCommand
        {
            get
            {
                return new HyOracleCommand(this.InnerAdapter.UpdateCommand);
            }
            set
            {
                this.InnerAdapter.UpdateCommand = (value as HyOracleCommand).InnerCommand;

            }
        }

        public override int Fill(DataSet dataSet)
        {
            return this.InnerAdapter.Fill(dataSet);
        }

        public new int Fill(DataTable dataTable)
        {
            return this.InnerAdapter.Fill(dataTable);
        }

        public new int Fill(DataSet dataSet, string srcTable)
        {
            return this.InnerAdapter.Fill(dataSet, srcTable);
        }

        public new int Fill(int startRecord, int maxRecords, params DataTable[] dataTables)
        {
            return this.InnerAdapter.Fill(startRecord, maxRecords, dataTables);
        }

        public new int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable)
        {
            return this.InnerAdapter.Fill(dataSet, startRecord, maxRecords, srcTable);
        }

        public override DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
        {
            return this.InnerAdapter.FillSchema(dataSet, schemaType);
        }
        public new DataTable FillSchema(DataTable dataTable, SchemaType schemaType)
        {
            return this.InnerAdapter.FillSchema(dataTable, schemaType);
        }

        public new DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable)
        {
            return this.InnerAdapter.FillSchema(dataSet, schemaType,srcTable);
        }

        public override IDataParameter[] GetFillParameters()
        {
            IDataParameter[] paramList= this.InnerAdapter.GetFillParameters();
            if (paramList != null)
            {
                HyOracleParameter[] hyParams = new HyOracleParameter[paramList.Length];
                for(int i=0;i<paramList.Length;i++)                     
                {
                    hyParams[i] = new HyOracleParameter(paramList[i] as DDTek.Oracle.OracleParameter);
                }

                return hyParams;
            }
            return null;        
        }

        public new int Update(DataRow[] dataRows)
        {
            return this.InnerAdapter.Update(dataRows);
        }
        public override int Update(DataSet dataSet)
        {
            return this.InnerAdapter.Update(dataSet);
        }
        public new int Update(DataTable dataTable)
        {
            return this.InnerAdapter.Update(dataTable);
        }

        public new int Update(DataSet dataSet, string srcTable)
        {
            return this.InnerAdapter.Update(dataSet, srcTable);
        }
    }
}
