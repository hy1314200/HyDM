using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Common.Utility.Encryption
{
    /// <summary>
    /// 时间限制
    /// </summary>
    public class TimeEncryption
    {
        private string m_LastUseTimeString = "2010-09-24 14:30:00";
        private string m_ProductReleaseTimeString = "2010-08-24 11:00:00";
        private string m_ValueTime;

        private readonly string m_EncryptFile = "esriSystemCheck1.dat";
        private string m_Path;

        private long m_UsedSeconds=1;    

        public TimeEncryption()
        {
            m_Path = Environment.SystemDirectory + "\\" + m_EncryptFile;
        }

        private void TimerCall(object obj)
        {
            m_UsedSeconds += 100;
        }

        ~TimeEncryption()
        {          

            SaveUsedDate(m_UsedSeconds);
        }

        #region 内部方法

        private string OpenValidateFile(string strCurrentTime)
        {
            try
            {
                TextReader reader = null;
                bool Result = true;
                try
                {
                    reader = new StreamReader(m_Path, Encoding.Default);
                }
                catch (Exception exp)
                {
                    Result = false;
                }
                string strValue = "";
                if (Result == false)
                {
                    //m_Path = "D:\\Program Files\\ESRI\\License\\sysgen\\esriSystemCheck.dat";
                    try
                    {
                        reader = new StreamReader(m_Path, Encoding.Default);
                    }
                    catch (Exception exp)
                    {
                        Result = false;
                    }
                    if (Result == false)
                    {
                        strValue = CreateValidateFile(strCurrentTime);
                    }
                }

                if (strValue == "")
                {
                    strValue = reader.ReadLine();
                    reader.Close();
                }
                strValue = StringEncrypt(strValue);
                return strValue.Substring(0, strValue.Length - 1);
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());
            }
            return "";
        }

        private string CreateValidateFile(string strCurrentTime)
        {
            try
            {
                strCurrentTime = strCurrentTime + " ";
                //string strPath = "E:\\Program Files\\ESRI\\License\\sysgen";
                //if (!System.IO.Directory.Exists(strPath))
                //{
                //    if (!System.IO.Directory.Exists("E:\\"))
                //    {
                //        strPath = "D:\\Program Files\\ESRI\\License\\sysgen";
                //    }
                //    System.IO.Directory.CreateDirectory(strPath);
                //}
                //m_Path = strPath + "\\esriSystemCheck.dat";
                FileStream file = new FileStream(m_Path, FileMode.Create, FileAccess.Write);
                file.Seek(0, SeekOrigin.Begin);
                string strKey = strCurrentTime;
                strKey = StringEncrypt(strKey);
                byte[] arrData = Encoding.Default.GetBytes(strKey);
                file.Write(arrData, 0, arrData.Length);
                file.Close();

                //为稳妥起见，在D盘下再创建一个，对于过于精明的用户还是防不住
                //strPath = "D:\\Program Files\\ESRI\\License\\sysgen\\";
                //if(!System.IO.Directory.Exists(strPath))
                //{
                //    System.IO.Directory.CreateDirectory(strPath);
                //}
                //FileStream file1 = new FileStream(strPath + "\\esriSystemCheck.dat", FileMode.Create, FileAccess.Write);

                //file1.Seek(0, SeekOrigin.Begin);
                //file1.Write(arrData, 0, arrData.Length);
                //file1.Close();

                /*
                string pFileName = "E:\\Program Files\\ESRI\\License\\sysgen\\esriSystemCheck.dat";
                //extern BYTE newAttribute;
                CFileStatus status;
                CFile::GetStatus( pFileName, status );
                status.m_attribute = 0x02;
                CFile::SetStatus( pFileName, status );

                char* pFileName1 = "D:\\Program Files\\ESRI\\License\\sysgen\\esriSystemCheck.dat";
                //extern BYTE newAttribute;
                CFile::GetStatus( pFileName1, status );
                status.m_attribute = 0x02;
                CFile::SetStatus( pFileName1, status );
                */
                return strKey;
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());
            }
            return "";
        }

        private bool WriteValidateFile(string strLastTime)
        {
            try
            {
                FileStream file = new FileStream(m_Path, FileMode.Create, FileAccess.Write);
                file.Seek(0, SeekOrigin.Begin);

                string strKey = strLastTime + " ";

                strKey = StringEncrypt(strKey);
                byte[] arrData = Encoding.Default.GetBytes(strKey);
                file.Write(arrData, 0, arrData.Length);
                file.Close();
                return true;
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());
                return false;
            }
            return false;
        }

        private string ProgressionTime(long nSecond, string strSource)
        {
            string strResult;

            int nYear;
            int nMonth;
            int nDay;
            int nHour;
            int nMinute;
            int nSec;

            nYear = nMonth = nDay = nHour = nMinute = nSec = 0;

            //由于考虑到白天使用，所以一天按12小时算，因此将累计时间乘2
            long tSecondCount = nSecond << 1;
            long nAddSec = nSecond%60;
            long nAddMinute = (nSecond/60)%60;
            long nAddHour = nSecond/3600;

            GetYMD_HMS(m_ValueTime, ref nYear, ref nMonth, ref nDay, ref nHour, ref nMinute, ref nSec);
            DateTime time = new DateTime(nYear, nMonth, nDay, nHour, nMinute, nSec);

            DateTime timeNew = time.AddSeconds(nSecond);

            string strNew;
            //取出日期
            nMonth = timeNew.Month;
            nDay = timeNew.Day;
            nHour = timeNew.Hour;
            nMinute = timeNew.Minute;
            nSec = timeNew.Second;

            string strMonth, strDay, strHour, strMinute, strSec;
            strMonth = nMonth > 9 ? string.Format("{0}", nMonth) : string.Format("0{0}", nMonth);
            strDay = nDay > 9 ? string.Format("{0}", nDay) : string.Format("0{0}", nDay);
            strHour = nHour > 9 ? string.Format("{0}", nHour) : string.Format("0{0}", nHour);
            strMinute = nMinute > 9 ? string.Format("{0}", nMinute) : string.Format("0{0}", nMinute);
            strSec = nSec > 9 ? string.Format("{0}", nSec) : string.Format("0{0}", nSec);

            strNew = string.Format("{0}-{1}-{2} {3}:{4}:{5}", timeNew.Year, strMonth, strDay, strHour, strMinute, strSec);
            return strNew;
        }

        private bool GetYMD_HMS(string strTime, ref int nYear, ref int nMonth, ref int nDay, ref int nHour,
                                ref int nMinute, ref int nSecond)
        {
            try
            {
                //取出时间
                int nTemp = 0;
                int nStartPos = 0;
                int nEndPos = 0;
                int nStrlength = strTime.Length;
                string[] temp = strTime.Split(' ')[0].Split('-');
                nYear = Convert.ToInt32(temp[0]);
                nMonth = Convert.ToInt32(temp[1]);
                nDay = Convert.ToInt32(temp[2]);

                temp = strTime.Split(' ')[1].Split(':');
                nHour = Convert.ToInt32(temp[0]);
                nMinute = Convert.ToInt32(temp[1]);
                nSecond = Convert.ToInt32(temp[2]);
                return true;
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());
            }
            return true;
        }

        private string StringEncrypt(string strSource)
        {
            try
            {
                
                Byte[] data = Encoding.Default.GetBytes(strSource);
                byte[] newData = new byte[data.Length];
                long nCount = data.Length;
                for (int i = 0; i < nCount; i++)
                {
                    byte th = data[i];
                    th = (byte) (th ^ 0xFF);
                    newData[i] = th;
                }
                return Encoding.Default.GetString(newData);
                
                //return strSource;
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());
            }
            return "";
        }

        #endregion

        #region 外部方法

        /// <summary>
        /// 验证使用权限
        /// </summary>
        /// <returns></returns>
        public bool CheckUsePermissions()
        {
            m_ValueTime = OpenValidateFile(m_ProductReleaseTimeString);
            bool bIsValid = true;
            string strNow = System.DateTime.Now.ToString();
            ///系统当前时间与系统使用截止日期之间的差值
            TimeSpan timeSpanNowToEnd = System.DateTime.Now.Subtract(Convert.ToDateTime(m_LastUseTimeString));
            long nDiffNowToEnd = timeSpanNowToEnd.Ticks;

            ///上一次使用时间与系统使用截止日期之间的差值
            TimeSpan timeSpanLastToEnd = Convert.ToDateTime(m_ValueTime).Subtract(Convert.ToDateTime(m_LastUseTimeString));
            long nDiffLastToEnd = timeSpanLastToEnd.Ticks;

            if (nDiffLastToEnd >= 0)
            {
                m_ValueTime = ProgressionTime(m_UsedSeconds, m_ValueTime);
                bIsValid = false;                
            }
            else if (nDiffNowToEnd >= 0)
            {
                m_ValueTime = strNow;
                bIsValid = false;               
            }

            if (!bIsValid)
            {               
                return false;
            }
            m_ValueTime = strNow;
            return true;
        }

        /// <summary>
        /// 保存使用的天数
        /// </summary>
        /// <param name="nSecond"></param>
        /// <returns></returns>
        public bool SaveUsedDate(long nSecond)
        {
            if (nSecond <= 0)
            {
                return false;
            }        

            //m_ValueTime = DateTime.Now.ToString();

            return WriteValidateFile(m_ValueTime);
        }

        /// <summary>
        /// 设置使用参数（上次使用时间和到期时间）
        /// </summary>
        /// <param name="strReleaseTime"></param>
        /// <param name="strLastUseTerm"></param>
        /// <returns></returns>
        public bool SetUseParameter(string strReleaseTime, string strLastUseTerm)
        {
            m_ProductReleaseTimeString = strReleaseTime;
            m_LastUseTimeString = strLastUseTerm;
            return true;
        }

        #endregion
    } 
}