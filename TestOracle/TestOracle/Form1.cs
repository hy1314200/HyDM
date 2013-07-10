using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.IO;
using NHibernate;
using NHibernate.Cfg;

namespace TestOracle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            DataTable dtProviderFactorys = DbProviderFactories.GetFactoryClasses();


            DbConnectionStringBuilder dcsBuilder = new DbConnectionStringBuilder();
            dcsBuilder.Add("User ID", "hzzgis");
            dcsBuilder.Add("Password", "hzzgis");
            dcsBuilder.Add("Service Name", "sunz");
            dcsBuilder.Add("Host", "172.16.1.9");
            dcsBuilder.Add("Integrated Security", false);


            string licPath = Application.StartupPath + "\\DDTek.lic";
            if (!System.IO.File.Exists(licPath))
                licPath = CretateDDTekLic.CreateLic();
            dcsBuilder.Add("License Path", licPath);
            //若路径中存在空格，则会在路径名称前加上"\""
            string conStr = dcsBuilder.ConnectionString;
            conStr = conStr.Replace("\"", "");

            DDTek.Oracle.OracleConnection orclConnection = new DDTek.Oracle.OracleConnection(conStr);
            orclConnection.Open();

            Configuration config = new Configuration();
           
            ISessionFactory pFactory = config.BuildSessionFactory();
            ISession pSession= pFactory.OpenSession(orclConnection as IDbConnection);

            //DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OracleClient");
            //IDbConnection dbConn = factory.CreateConnection();
            //if (dbConn != null)
            //    MessageBox.Show("Connection Created");
            //Conn.ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + Server + ")(PORT=" + Port + ")))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + Service + ")));user id=" + User + ";password=" + PWD + ";pooling = true;Unicode=True";

            IDbConnection dbConn=new System.Data.OleDb.OleDbConnection();
            string Server = "sunzvm-lc", Port = "1521", Service = "sunz", User = "hzzgis", PWD = "hzzgis";
            //dbConn.ConnectionString = "Provider=OraOLEDB.Oracle.1;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + Server + ")(PORT=" + Port + ")))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + Service + ")));user id=" + User + ";password=" + PWD + ";pooling = true;Unicode=True";
            dbConn.ConnectionString = "Provider=MSDAORA.1;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + Server + ")(PORT=" + Port + ")))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + Service + ")));user id=" + User + ";password=" + PWD + ";pooling = true;Unicode=True";

            try
            {
                dbConn.Open();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }            
        }

        internal static class CretateDDTekLic
        {
            public static string CreateLic()
            {

                byte[] financeCounter = TestOracle.Properties.Resources.DDTek;
                string licPath = Application.StartupPath + "\\DDTek.lic";
                if (SaveByteArray2File(licPath, ref financeCounter))
                    return licPath;
                return "";
            }

            public static bool SaveByteArray2File(string filePath, ref byte[] content)
            {
                try
                {
                    FileStream output = new FileStream(filePath, FileMode.Create);
                    BinaryWriter writer = new BinaryWriter(output);
                    writer.Write(content);
                    writer.Close();
                    writer = null;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
