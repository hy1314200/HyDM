using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace TestOracle.TekOracle
{
    public  class HyOracleParameter:DbParameter
    {
        DDTek.Oracle.OracleParameter m_Parameter = null;

        public DDTek.Oracle.OracleParameter InnerParameter { get { return this.m_Parameter; } }

        internal HyOracleParameter(DDTek.Oracle.OracleParameter innerParameter)
        {
            this.m_Parameter = innerParameter;
        }

        public override System.Data.DbType DbType
        {
            get
            {
                return this.m_Parameter.DbType;
            }
            set
            {
                this.m_Parameter.DbType = value;
            }
        }

        public override System.Data.ParameterDirection Direction
        {
            get
            {
                return this.m_Parameter.Direction;
            }
            set
            {
                this.m_Parameter.Direction = value;
            }
        }

        public override bool IsNullable
        {
            get
            {
                return this.m_Parameter.IsNullable;
            }
            set
            {
                this.m_Parameter.IsNullable = value;
            }
        }

        public override string ParameterName
        {
            get
            {
                return this.m_Parameter.ParameterName;
            }
            set
            {
                this.m_Parameter.ParameterName = value;
            }
        }

        public override void ResetDbType()
        {
            this.m_Parameter.DbType = System.Data.DbType.Object;
        }

        public override int Size
        {
            get
            {
                return  this.m_Parameter.Size;
            }
            set
            {
                this.m_Parameter.Size = value;
            }
        }

        public override string SourceColumn
        {
            get
            {
                return this.m_Parameter.SourceColumn;
            }
            set
            {
                this.m_Parameter.SourceColumn = value;
            }
        }

        private bool m_SourceColumnNullMapping = true;
        public override bool SourceColumnNullMapping
        {
            get
            {
                return m_SourceColumnNullMapping;
            }
            set
            {
                m_SourceColumnNullMapping = value;
            }
        }

        public override System.Data.DataRowVersion SourceVersion
        {
            get
            {
                return this.m_Parameter.SourceVersion;
            }
            set
            {
                this.m_Parameter.SourceVersion = value;
            }
        }

        public override object Value
        {
            get
            {
                return this.m_Parameter.Value;
            }
            set
            {
                this.m_Parameter.Value = value;
            }
        }
    }
}
