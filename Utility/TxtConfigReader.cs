using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace Utility
{
    public class TxtConfigReader
    {
        public string FileName { private get; set; }

        public string SplitString { private get; set; }
        //{
        //    set
        //    {
        //        m_SplitChar = new string[] { value };
        //    }
        //}
        //private string[] m_SplitChar;

        StreamReader m_Reader;
        public void Reset()
        {
            Release();
            m_Reader = new StreamReader(FileName);
        }

        private string m_CurrentKey;
        private string m_CurrentValue;
        public bool Next()
        {
            m_CurrentKey = null;
            m_CurrentValue = null;
            if (m_Reader.EndOfStream)
                return false;

            for (string strLine = m_Reader.ReadLine(); !m_Reader.EndOfStream; strLine = m_Reader.ReadLine())
            {
                if (!string.IsNullOrWhiteSpace(strLine))
                {
                    //string[] strs = strLine.Split(m_SplitChar, StringSplitOptions.None);
                    //m_Current = new KeyValuePair<string, string>(strs[0], strs.Length > 1 ? strs[1] : null);
                    int splitIndex = strLine.IndexOf(this.SplitString);
                    m_CurrentKey  = splitIndex > -1 ? strLine.Substring(0, splitIndex) : strLine;
                    m_CurrentValue = (splitIndex > -1 && splitIndex + 1 < strLine.Length) ? strLine.Substring(splitIndex + 1) : null;

                    return true;
                }
            }
            return false;
        }

        public void Release()
        {
            if (m_Reader != null)
            {
                m_Reader.Close();
                m_Reader.Dispose();
                m_Reader = null;

                m_CurrentKey = null;
                m_CurrentValue = null;
            }
        }

        public KeyValuePair<string, string> Current
        {
            get
            {
                return new KeyValuePair<string,string>(m_CurrentKey,m_CurrentValue);
            }
        }

        public Dictionary<string, string> ReadToDictionary()
        {
            this.Reset();

            Dictionary<string, string> dict = new Dictionary<string, string>();
            while (this.Next())
            {
                if (m_CurrentKey != null)
                    dict[m_CurrentKey] = m_CurrentValue;
            }

            return dict;
        }

        public DataTable ReadToDataTable()
        {
            this.Reset();

            DataTable dt = new DataTable();
            dt.Columns.Add("Key", typeof(string));
            dt.Columns.Add("Value");

            while (this.Next())
            {
                dt.Rows.Add(new object[] { m_CurrentKey, m_CurrentValue });
            }

            return dt;

        }

        public DataTable ReadMultiToDataTable()
        {
            Reset();

            string[] strSplits = { this.SplitString };
            DataTable dt = new DataTable();
            for (string strLine = m_Reader.ReadLine(); !m_Reader.EndOfStream; strLine = m_Reader.ReadLine())
            {
                // 找到第一行有文字的行，作为Schema
                if (!string.IsNullOrWhiteSpace(strLine))
                {
                    string[] strSchemas = strLine.Split(strSplits, StringSplitOptions.None);
                    for (int i = 0; i < strSchemas.Length; i++)
                    {
                        dt.Columns.Add(strSchemas[i].Trim());
                    }
                    break;
                }
            }
            // 从此都读为数据
            for (string strLine = m_Reader.ReadLine(); !m_Reader.EndOfStream; strLine = m_Reader.ReadLine())
            {
                if (!string.IsNullOrWhiteSpace(strLine))
                {
                    string[] strSchemas = strLine.Split(strSplits, StringSplitOptions.None);
                    for (int i = 0; i < strSchemas.Length; i++)
                    {
                        strSchemas[i] = strSchemas[i].Trim();
                    }

                    dt.Rows.Add(strSchemas);
                }
            }
            return dt;
        }
    }
}
