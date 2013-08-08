using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace TestOracle.TekOracle
{
    public  class HyOracleParameterCollection:DbParameterCollection
    {
        DDTek.Oracle.OracleParameterCollection m_ParameterCollection = null;

        internal DDTek.Oracle.OracleParameterCollection InnerParameterCollection { get { return this.m_ParameterCollection; } }

        internal HyOracleParameterCollection(DDTek.Oracle.OracleParameterCollection innerCollection)
        {
            this.m_ParameterCollection = innerCollection;
        }
             

        public override int Add(object value)
        {
            return this.m_ParameterCollection.Add(value);
        }

        public override void AddRange(Array values)
        {
            if (values == null)
                return;

            foreach (object objValue in values)
            {
                this.Add(objValue);
            }

        }

        public override void Clear()
        {
            this.m_ParameterCollection.Clear();
        }

        public override bool Contains(string value)
        {

            return m_ParameterCollection.Contains(value);
        }

        public override bool Contains(object value)
        {
            return m_ParameterCollection.Contains(value);
        }

        public override void CopyTo(Array array, int index)
        {
             m_ParameterCollection.CopyTo(array, index);
        }

        public override int Count
        {
            get { return m_ParameterCollection.Count; }
        }

        public override System.Collections.IEnumerator GetEnumerator()
        {
            return m_ParameterCollection.GetEnumerator();
        }

        protected override DbParameter GetParameter(string parameterName)
        {
            return new HyOracleParameter(m_ParameterCollection[parameterName]);
        }

        protected override DbParameter GetParameter(int index)
        {
            return new HyOracleParameter(m_ParameterCollection[index]);
        }

        public override int IndexOf(string parameterName)
        {
            return m_ParameterCollection.IndexOf(parameterName);
        }

        public override int IndexOf(object value)
        {
            return m_ParameterCollection.IndexOf(value);
        }

        public override void Insert(int index, object value)
        {
            this.m_ParameterCollection.Insert(index, value);
        }

        public override bool IsFixedSize
        {
            get { return this.m_ParameterCollection.IsFixedSize; }
        }

        public override bool IsReadOnly
        {
            get { return this.m_ParameterCollection.IsReadOnly; }
        }

        public override bool IsSynchronized
        {
            get { return this.m_ParameterCollection.IsSynchronized; }
        }

        public override void Remove(object value)
        {
             this.m_ParameterCollection.Remove(value);
        }

        public override void RemoveAt(string parameterName)
        {
            this.m_ParameterCollection.RemoveAt(parameterName);
        }

        public override void RemoveAt(int index)
        {
            this.m_ParameterCollection.RemoveAt(index);
        }

        protected override void SetParameter(string parameterName, DbParameter value)
        {
            this.m_ParameterCollection[parameterName] = (value as HyOracleParameter).InnerParameter;
        }

        protected override void SetParameter(int index, DbParameter value)
        {
            this.m_ParameterCollection[index] = (value as HyOracleParameter).InnerParameter;
        }

        public override object SyncRoot
        {
            get { return this.m_ParameterCollection.SyncRoot; }
        }
    }
}
