using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.VisualBasic;

namespace Common.Utility.Encryption
{
    /// <summary>
    /// 文件加解密
    /// </summary>
    public class FileEncryption
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public static bool EncryptKey(string strFileName, string strSource)
        {
            try
            {
                byte[] arrData = Encoding.UTF8.GetBytes(strSource);

                for (int i = 0; i < arrData.Length; i++)
                {
                    arrData[i] = (byte)(arrData[i] ^ 0xff);
                }

                FileStream fileStream = new FileStream(strFileName, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Write(arrData, 0, arrData.Length);
                fileStream.Close();
                return true;
            }
            catch (Exception exp)
            {
                throw new Exception("加密文件时发生错误", exp);
            }
            return false;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <returns></returns>
        public static string DecryptKey(string strFileName)
        {
            string xml = "";
            try
            {
                FileStream fileStream = new FileStream(strFileName, FileMode.Open, FileAccess.Read);
                int nOffset = 0;
                int nCount = 1024;
                long nMaxLength = fileStream.Length;
                byte[] arrData = new byte[nMaxLength];
                do
                {
                    if (nMaxLength - nOffset < nCount)
                    {
                        nCount = (int)(nMaxLength - nOffset);
                    }
                    byte[] temp = new byte[nCount];
                    fileStream.Read(temp, 0, nCount);   // 2012-02-16 张航宇 偏移量从当前位置开始
                    for (int i = 0; i < nCount; i++)
                    {
                        arrData[nOffset + i] = temp[i]; // 索引位置要加上偏移
                    }
                    nOffset += nCount;
                } while (nOffset < nMaxLength);

                //fileStream.Read(arrData, 0, (int)nMaxLength); // 觉得可以直接读
                fileStream.Close();

                for (int i = 0; i < nMaxLength; i++)
                {
                    arrData[i] = (byte)(arrData[i] ^ 0xff);
                }

                xml = Encoding.UTF8.GetString(arrData);
            }
            catch (Exception exp)
            {
                throw new Exception("加密文件时发生错误", exp);
            }
            return xml;
        }
    }
}
