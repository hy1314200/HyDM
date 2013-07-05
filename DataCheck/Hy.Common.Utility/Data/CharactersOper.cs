using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Utility.Data
{
    public class CharactersOper
    {
        /// <summary>
        /// 解析竖线隔开的字符串，如：“aaa|bbb”
        /// </summary>
        /// <param name="aryStr"></param>
        /// <param name="strString"></param>
        public static void ParseStr(ref List<string> aryStr, string strString)
        {
            aryStr.Clear();

            //解析字符串
            string strTmp = strString;
            strTmp.Trim();
            while (strTmp.IndexOf('|') != -1)
            {
                int left = strTmp.IndexOf('|');

                strString = strTmp.Substring(0, left);
                aryStr.Add(strString);

                strTmp = strTmp.Substring(left, strTmp.Length - 1 - left);
            }

            aryStr.Add(strTmp);
        }

        /// <summary>
        /// 根据数字获取字符串
        /// </summary>
        /// <param name="nNum"></param>
        /// <returns></returns>
        public static string GetStringFromNum(int nNum)
        {
            string strTemp = "";
            if (nNum == 0)
            {
                strTemp = "允许为空";
            }
            else //nNum=1
            {
                strTemp = "不允许为空";
            }
            return strTemp;
        }
    }
}
