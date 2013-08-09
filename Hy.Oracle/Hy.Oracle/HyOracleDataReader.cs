using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Hy.Oracle
{
    public  class HyOracleDataReader:DbDataReader
    {
        private DDTek.Oracle.OracleDataReader m_Reader;
        internal DDTek.Oracle.OracleDataReader InnerReader { get { return this.m_Reader; } }
        internal HyOracleDataReader(DDTek.Oracle.OracleDataReader innerReader)
        {
            this.m_Reader = innerReader;
        }

        public override void Close()
        {
            this.m_Reader.Close();
        }

        public override int Depth
        {
            get { return this.m_Reader.Depth; }
        }

        public override int FieldCount
        {
            get { return this.m_Reader.FieldCount; }
        }

        public override bool GetBoolean(int ordinal)
        {
            return this.m_Reader.GetBoolean(ordinal);
        }

        public override byte GetByte(int ordinal)
        {
            return this.m_Reader.GetByte(ordinal);
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            return this.m_Reader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
        }

        public override char GetChar(int ordinal)
        {
            return this.m_Reader.GetChar(ordinal);
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            return this.m_Reader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
        }

        public override string GetDataTypeName(int ordinal)
        {
            return this.m_Reader.GetDataTypeName(ordinal);
        }

        public override DateTime GetDateTime(int ordinal)
        {
            return this.m_Reader.GetDateTime(ordinal);
        }

        public override decimal GetDecimal(int ordinal)
        {
            return this.m_Reader.GetDecimal(ordinal);
        }

        public override double GetDouble(int ordinal)
        {
            return this.m_Reader.GetDouble(ordinal);
        }

        public override System.Collections.IEnumerator GetEnumerator()
        {
            throw new Exception("不支持此操作");
        }


        public override Type GetFieldType(int ordinal)
        {
            return this.m_Reader.GetFieldType(ordinal);
        }

        public override float GetFloat(int ordinal)
        {
            return this.m_Reader.GetFloat(ordinal);
        }

        public override Guid GetGuid(int ordinal)
        {
            return this.m_Reader.GetGuid(ordinal);
        }

        public override short GetInt16(int ordinal)
        {
            return this.m_Reader.GetInt16(ordinal);
        }

        public override int GetInt32(int ordinal)
        {
            return this.m_Reader.GetInt32(ordinal);
        }

        public override long GetInt64(int ordinal)
        {
            return this.m_Reader.GetInt64(ordinal);
        }

        public override string GetName(int ordinal)
        {
            return this.m_Reader.GetName(ordinal);
        }

        public override int GetOrdinal(string name)
        {
            return this.m_Reader.GetOrdinal(name);
        }

        public override System.Data.DataTable GetSchemaTable()
        {
            return this.m_Reader.GetSchemaTable();
        }

        public override string GetString(int ordinal)
        {
            return this.m_Reader.GetString(ordinal);
        }

        public override object GetValue(int ordinal)
        {
            return this.m_Reader.GetValue(ordinal);
        }

        public override int GetValues(object[] values)
        {
            return this.m_Reader.GetValues(values);
        }

        public override bool HasRows
        {
            get { return this.m_Reader.HasRows; }
        }

        public override bool IsClosed
        {
            get { return this.m_Reader.IsClosed; }
        }

        public override bool IsDBNull(int ordinal)
        {
            return this.m_Reader.IsDBNull(ordinal);
        }

        public override bool NextResult()
        {
            return this.m_Reader.NextResult();
        }

        public override bool Read()
        {
            return this.m_Reader.Read();
        }

        public override int RecordsAffected
        {
            get { return this.m_Reader.RecordsAffected; }
        }

        public override object this[string name]
        {
            get { return this.m_Reader[name]; }
        }

        public override object this[int ordinal]
        {
            get { return this.m_Reader[ordinal]; }
        }

    }
}
