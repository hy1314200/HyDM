using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Management;
using Microsoft.Win32;
using System.Data;

namespace Common.Utility.Encryption
{
    /// <summary>
    ///  加/解密类，主要指公/私钥的生成/读取等操作
    ///  @remark RSA:公钥加密算法
    /// </summary>
    public class Encryption
    {
        /// <summary>
        /// 生成公私钥
        /// </summary>
        /// <param name="strPrivateKeyPath"></param>
        /// <param name="strPublicKeyPath"></param>
        public static void RSAKey(string strPrivateKeyPath, string strPublicKeyPath)
        {
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                WriteToFile(strPrivateKeyPath, provider.ToXmlString(true));
                WriteToFile(strPublicKeyPath, provider.ToXmlString(false));
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());
            }
        }


        /// <summary>
        /// 对原始数据进行MD5加密
        /// </summary>
        /// <param name="strSource">待加密数据</param>
        /// <returns>返回机密后的数据</returns>
        public static string GetHash(string strSource)
        {
            HashAlgorithm algorithm = HashAlgorithm.Create("MD5");
            byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(strSource);
            byte[] inArray = algorithm.ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }

        private static readonly string m_KeyString = "distghzj";
        /// <summary>
        /// 字符串加密
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static string EncryptString(string strSource)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                //把字符串放到byte数组中

                //原来使用的UTF8编码，我改成Unicode编码了，不行
                byte[] inputByteArray = Encoding.Default.GetBytes(strSource);

                //建立加密对象的密钥和偏移量


                //使得输入密码必须输入英文文本
                des.Key = ASCIIEncoding.ASCII.GetBytes(m_KeyString);
                des.IV = ASCIIEncoding.ASCII.GetBytes(m_KeyString);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                ret.ToString();
                return ret.ToString();
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

            }

            return "";
        }

        /// <summary>
        /// 字符串解密
        /// </summary>
        /// <param name="strEncrypted"></param>
        /// <returns></returns>
        public static string DecryptString(string strEncrypted)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = new byte[strEncrypted.Length / 2];
                for (int x = 0; x < strEncrypted.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(strEncrypted.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                //建立加密对象的密钥和偏移量，此值重要，不能修改
                des.Key = ASCIIEncoding.ASCII.GetBytes(m_KeyString);
                des.IV = ASCIIEncoding.ASCII.GetBytes(m_KeyString);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象
                StringBuilder ret = new StringBuilder();
                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

            }
            return "";
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="m_strEncryptString">MD5加密后的数据</param>
        /// <returns>RSA公钥加密后的数据</returns>
        public static string RSAEncrypt(string xmlPublicKey, string m_strEncryptString)
        {
            string str2;
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                FileStream pStream = new FileStream(xmlPublicKey, FileMode.Open);
                StreamReader pReader = new StreamReader(pStream);
                string xmlStr = pReader.ReadToEnd();
                pReader.Close();
                pStream.Close();
                provider.FromXmlString(xmlStr);
                byte[] bytes = new UnicodeEncoding().GetBytes(m_strEncryptString);
                str2 = Convert.ToBase64String(provider.Encrypt(bytes, false));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return str2;
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="m_strDecryptString">待解密的数据</param>
        /// <returns>解密后的结果</returns>
        public static string RSADecrypt(string xmlPrivateKey, string m_strDecryptString)
        {
            string str2;
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlPrivateKey);
                byte[] rgb = Convert.FromBase64String(m_strDecryptString);
                byte[] buffer2 = provider.Decrypt(rgb, false);
                str2 = new UnicodeEncoding().GetString(buffer2);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return str2;
        }

        /// <summary>
        /// 对MD5加密后的密文进行签名
        /// </summary>
        /// <param name="strKey">私钥</param>
        /// <param name="strEncrypted">MD5加密后的密文</param>
        /// <returns></returns>
        public static string SignString(string strKey, string strEncrypted)
        {
            byte[] rgbHash = Convert.FromBase64String(strEncrypted);
            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            key.FromXmlString(strKey);
            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
            formatter.SetHashAlgorithm("MD5");
            byte[] inArray = formatter.CreateSignature(rgbHash);
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="strKey">公钥</param>
        /// <param name="strSignature">待验证的用户名</param>
        /// <param name="strRegCode">注册码</param>
        /// <returns></returns>
        public static bool VerifySignature(string strKey, string strSignature, string strRegCode)
        {
            try
            {
                byte[] rgbHash = Convert.FromBase64String(strSignature);
                RSACryptoServiceProvider key = new RSACryptoServiceProvider();
                key.FromXmlString(strKey);
                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);
                deformatter.SetHashAlgorithm("MD5");
                byte[] rgbSignature = Convert.FromBase64String(strRegCode);
                if (deformatter.VerifySignature(rgbHash, rgbSignature))
                {
                    return true;
                }
                return false;
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());
                return false;
            }
        }

        /// <summary>
        /// 获取第一块硬盘ID（Model值）
        /// </summary>
        public static string GetHardDistID()
        {
            string strHardID = "";
            ManagementClass sysManager = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc1 = sysManager.GetInstances();
            ManagementObjectCollection.ManagementObjectEnumerator objEnumerator = moc1.GetEnumerator();
            objEnumerator.MoveNext();
            strHardID = objEnumerator.Current.GetPropertyValue("Model").ToString();

            return strHardID;
        }
     
        /// <summary>
        /// 读取公钥
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string ReadPublicKey(string strPath)
        {
            StreamReader reader = new StreamReader(strPath);
            string publickey = reader.ReadToEnd();
            reader.Close();
            return publickey;
        }

        /// <summary>
        /// 读取私钥
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string ReadPrivateKey(string strPath)
        {
            StreamReader reader = new StreamReader(strPath);
            string privatekey = reader.ReadToEnd();
            reader.Close();
            return privatekey;
        }

        /// <summary>
        /// 初始化注册表，程序运行时调用，在调用之前更新公钥xml
        /// </summary>
        /// <param name="strPath">公钥路径</param>
        public static void InitRegister(string strPath)
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\JX\Register");
            Random ra = new Random();
            string publickey = ReadPublicKey(strPath);
            if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JX\Register").ValueCount <= 0)
            {
                WriteRegister("RegisterRandom", ra.Next(1, 100000).ToString());
                WriteRegister("RegisterPublicKey", publickey);
            }
            else
            {
                WriteRegister("RegisterPublicKey", publickey);
            }
        }

        /* 加密表字段

        /// <summary>
        /// 为指定表的指定字段值进行加密
        /// </summary>
        /// <param name="strTable">表名</param>
        /// <param name="strSourceField">需要加密的字段名</param>
        /// <param name="strFlagField">标识加密的字段名</param>
        /// <param name="strIdentityField">唯一标识字段名</param>
        /// <returns>操作结果</returns>
        public static bool EncryptFieldValues(string strTable, string strSourceField, string strFlagField, string strIdentityField)
        {
            try
            {
                string sqlStr = "select " + strSourceField + " ," + strFlagField + "," + strIdentityField + " from " + strTable + " where " + strFlagField + " = 0";
                DataTable pDt = null;
                CommonAPI.ado_OpenTable(ref pDt, sqlStr, CommonAPI.Get_DBConnection());
                if (pDt == null) return false;
                foreach (DataRow pRow in pDt.Rows)
                {
                    sqlStr = string.Format("update {0} set {1} = '{2}' ,{3} ='1' where {4} = {5}", strTable, strSourceField, EncryptString(pRow[strSourceField].ToString()), strFlagField, strIdentityField, pRow[strIdentityField].ToString());
                    if (!CommonAPI.ado_ExecuteSQL(CommonAPI.Get_DBConnection(), sqlStr))
                    {
                        sqlStr = string.Format("update {0} set{1} = '{2}' ,{3} ='1' where {4} = '{5}'", strTable, strSourceField, EncryptString(pRow[strSourceField].ToString()), strFlagField, strIdentityField, pRow[strIdentityField].ToString());
                        CommonAPI.ado_ExecuteSQL(CommonAPI.Get_DBConnection(), sqlStr);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        */

        /* 检查应用权限，应在应用相关工程中定义
        /// <summary>
        /// 检查sn
        /// </summary>
        /// <param name="pConnection"></param>
        /// <returns></returns>
        public static bool CheckRight()
        {
            try
            {
                DataTable licDt = null;
                string strSql = "select SN,MS FROM LR_LIC";
                if (!GetDataFromDB.ado_OpenTable(ref licDt, strSql, CommonAPI.Get_DBConnection()))
                {
                    return false;

                }
                if (licDt == null || licDt.Rows.Count == 0)
                {
                    return false;
                }

                Encryption enCry = new Encryption();
                string encryPrivateStr = licDt.Rows[0][1].ToString();
                string disEncPrivateStr = enCry.DecryptString(encryPrivateStr);
                string diskid = enCry.RSADecrypt(disEncPrivateStr, licDt.Rows[0][0].ToString());

                string HDid = "";
                ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc1 = cimobject1.GetInstances();
                foreach (ManagementObject mo in moc1)
                {
                    HDid = (string)mo.Properties["Model"].Value;
                }
                if (enCry.GetHash(HDid).Equals(diskid))
                    return true;
                else
                    return false;
            }
            catch (Exception exp)
            {
                return false;
            }
        }
        */

        /// <summary>
        /// 读注册表中"SOFTWARE\JX\Register"下指定键的值
        /// </summary>
        /// <param name="strKey">键名</param>
        /// <returns>返回键值</returns>
        private static string ReadRegister(string strKey)
        {
            string temp = "";
            try
            {
                RegistryKey myKey = Registry.LocalMachine;
                RegistryKey subKey = myKey.OpenSubKey(@"SOFTWARE\JX\Register");
                temp = subKey.GetValue(strKey).ToString();
                subKey.Close();
                myKey.Close();
                return temp;
            }
            catch (Exception exp)
            {
                //Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());
                throw;//可能没有此注册项;
            }
        }

        /// <summary>
        /// 创建注册表中指定的键和值
        /// </summary>
        /// <param name="strKey">键名</param>
        /// <param name="strValue">键值</param>
        private static void WriteRegister(string strKey, string strValue)
        {
            try
            {
                RegistryKey rootKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\JX\Register");
                rootKey.SetValue(strKey, strValue);
                rootKey.Close();
            }
            catch (Exception exp)
            {
                //Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());
                throw;
            }
        }

        /// <summary>
        /// 将字符串内容（创建并）写到文件
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="strContent"></param>
        private static void WriteToFile(string strPath, string strContent)
        {
            try
            {
                FileStream publickeyxml = new FileStream(strPath, FileMode.Create);
                StreamWriter sw = new StreamWriter(publickeyxml);
                sw.WriteLine(strContent);
                sw.Close();
                publickeyxml.Close();
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());
            }
        }
    }
}
