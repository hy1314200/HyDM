using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
//using System.Windows.Forms;

namespace DIST.DGP.DataExchange.VCT
{
    /// <summary>
    /// 日志操作类
    /// </summary>
    public class LogAPI
    {
        /// <summary>
        /// 日志文件路径
        /// </summary>
        //private static  string LOG_FILE ;//=Application.StartupPath+ "\\ErrorLog.txt";

        private static FileStream pFileStream ; //= new FileStream(LOG_FILE, FileMode.Append, FileAccess.Write);
        private static StreamWriter pStreamWrite;// = new StreamWriter(LogAPI.FileLog);

        private static FileStream FileLog
        {
            get
            {
                //if (pFileStream == null)
                //{
                //    pFileStream = new FileStream(LOG_FILE, FileMode.Append, FileAccess.Write);
                //}
                return pFileStream;
            }
        }

        private static StreamWriter LogWriter
        {
            get
            {
                //if (pStreamWrite == null)
                //{
                //    pStreamWrite = new StreamWriter(LogAPI.FileLog);
                //}
                return pStreamWrite;
            }
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="ep"></param>
        public static void WriteErrorLog(Exception ep)
        {
            try
            {

                //DateTime pNowTime = DateTime.Now;
                //using (FileStream pFileStream = new FileStream(LOG_FILE, FileMode.Append, FileAccess.Write))
                //{
                //    using (StreamWriter pStreamWrite = new StreamWriter(pFileStream))
                //    {
                //        pStreamWrite.WriteLine();
                //        pStreamWrite.WriteLine("错误发生时间 ：" + pNowTime);
                //        pStreamWrite.WriteLine("Message :" + ep.Message);
                //        pStreamWrite.WriteLine("Source :" + ep.Source);
                //        pStreamWrite.WriteLine("StackTrace :" + ep.StackTrace);
                //        pStreamWrite.WriteLine("TargetSite :" + ep.TargetSite);

                //        pStreamWrite.Flush();
                //    }
                //}
                if (LogAPI.LogWriter != null)
                {
                    LogAPI.LogWriter.WriteLine();
                    LogAPI.LogWriter.WriteLine("错误发生时间 ：" + DateTime.Now);
                    LogAPI.LogWriter.WriteLine("Message :" + ep.Message);
                    LogAPI.LogWriter.WriteLine("Source :" + ep.Source);
                    LogAPI.LogWriter.WriteLine("StackTrace :" + ep.StackTrace);
                    LogAPI.LogWriter.WriteLine("TargetSite :" + ep.TargetSite);

                    LogAPI.LogWriter.Flush();
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 修改日志文件路径
        /// </summary>
        /// <param name="strFilePath">日志路径</param>
        /// <returns></returns>
        public static bool SetLogFilePath(string strFilePath)
        {
            //if (!File.Exists(strFilePath))
            //    return false;
            //LOG_FILE = strFilePath;
            pFileStream = new FileStream(strFilePath, FileMode.Append, FileAccess.Write);
            if (pFileStream != null)
                pStreamWrite = new StreamWriter(LogAPI.FileLog);
            return true;
        }
        /// <summary>
        /// 记录日志信息
        /// </summary>
        /// <param name="sMessage"></param>
        public static void WriteLog(string sMessage)
        {
            //using (FileStream pFileStream = new FileStream(LOG_FILE, FileMode.Append, FileAccess.Write))
            //{
            //    using (StreamWriter pStreamWrite = new StreamWriter(pFileStream))
            //    {
            //        pStreamWrite.WriteLine();
            //        pStreamWrite.WriteLine("Log :" + sMessage);
            //        pStreamWrite.Flush();
            //    }
            //}
            if (LogAPI.LogWriter != null)
            {
                LogAPI.LogWriter.WriteLine();
                LogAPI.LogWriter.WriteLine("Log :" + sMessage);
                LogAPI.LogWriter.Flush();
            }
        }
    }
}
