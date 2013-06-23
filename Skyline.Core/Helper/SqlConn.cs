using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Windows.Forms;

namespace Skyline.Core.Helper
{
    public class SqlConn : IDisposable
    {
        static string url = System.Windows.Forms.Application.StartupPath + @"\data\guangdong.mdb";

        private static string CON_STRING = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + url;
        //System.Environment.CurrentDirectory + @"\data\guangdong.mdb";


        private static OleDbConnection dbConn;


        public static OleDbConnection getOleConn()
        {
            dbConn = new OleDbConnection(CON_STRING);
            dbConn.Open();
            return dbConn;
        }

        #region IDisposable 成员

        //用using不是可以不用调用这个Dispose方法
        public void Dispose()
        {
            dbConn.Dispose();
            dbConn.Close();
        }

        #endregion

    }
}




//上述的数据文件的地址在程序中写死了
//试着找出一个比较灵活的scheme！