using System.IO;
using System.Text;
using System.Diagnostics;
using System;

namespace DIST.DGP.DataExchange.VCT
{
    class StreamWriterEx : StreamWriter
    {

        private StreamWriter m_pStreamWriterTemp;

        private FileStream m_pFileStreamTemp;

        private string m_strPath;

        public StreamWriter StreamWriterTemp
        {
            get
            {
                return m_pStreamWriterTemp;
            }
        }

        public StreamWriterEx(string strPath, Stream stream, Encoding encoding)
            : base(stream,encoding)
        {
            m_strPath = strPath;
            m_pFileStreamTemp = new FileStream(m_strPath + ".tmp", FileMode.Create);
            m_pStreamWriterTemp = new StreamWriter(m_pFileStreamTemp, encoding);
        }

        public override void Close()
        {
            //关闭文件
            if (m_pStreamWriterTemp != null)
                m_pStreamWriterTemp.Close();

            if (m_pFileStreamTemp != null)
                m_pFileStreamTemp.Close();

            base.Close();

            //合并文件
            Process proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            proc.StandardInput.WriteLine("copy " + m_strPath + "+" + m_strPath + ".tmp  " + m_strPath);
            //proc.StandardInput.WriteLine("del " + m_strPath + ".tmp");
            //proc.StandardInput.WriteLine("delete " + m_strPath + ".tmp.mdb");
            proc.Close();

            //try
            //{

            //    if (File.Exists(m_strPath + ".tmp"))
            //    {
            //        File.Delete(m_strPath + ".tmp");
            //    }
            //    //if (File.Exists(m_strPath + ".tmp.mdb"))
            //    //{
            //    //    File.Delete(m_strPath + ".tmp.mdb");
            //    //}
            //}
            //catch (Exception ex)
            //{
            //}

        }
    }
}
