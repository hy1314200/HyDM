using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Windows.Forms;

namespace DIST.DGP.DataExchange.MapInfoConvertor
{
    /// <summary>
    /// 日志操作类
    /// </summary>
    public class LogAPI
    {
        /// <summary>
        /// 日志文件路径
        /// </summary>
        private static  string LOG_FILE =Application.StartupPath+ "\\MapInfoConvertorLog.txt";

        /// <summary>
        /// 设置日志文件路径
        /// </summary>
        /// <param name="strPath"></param>
        public static void SetLogPath(string strPath)
        {
            LOG_FILE = strPath;
        }
        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="ep"></param>
        public static void WriteErrorLog(Exception ep)
        {
            DateTime pNowTime = DateTime.Now;
            using (FileStream pFileStream = new FileStream(LOG_FILE, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter pStreamWrite = new StreamWriter(pFileStream))
                {
                    pStreamWrite.WriteLine();
                    pStreamWrite.WriteLine("错误发生时间 ：" + pNowTime);
                    pStreamWrite.WriteLine("Message :" + ep.Message);
                    pStreamWrite.WriteLine("Source :" + ep.Source);
                    pStreamWrite.WriteLine("StackTrace :" + ep.StackTrace);
                    pStreamWrite.WriteLine("TargetSite :" + ep.TargetSite);

                    pStreamWrite.Flush();
                }
            }
        }

        /// <summary>
        /// 记录日志信息
        /// </summary>
        /// <param name="sMessage"></param>
        public static void WriteLog(string sMessage)
        {
            using (FileStream pFileStream = new FileStream(LOG_FILE, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter pStreamWrite = new StreamWriter(pFileStream))
                {
                    pStreamWrite.WriteLine();
                    pStreamWrite.WriteLine("Log :" + sMessage);
                    pStreamWrite.Flush();
                }
            }
        }
    }
}
