using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Frame.Define;
using System.Threading;

namespace Frame
{
    public partial class FrmLogin : DevExpress.XtraEditors.XtraForm,ILogin
    {
        public FrmLogin()
        {
            InitializeComponent();

            string[] strSplit = { "," };
            string[] strSize = ConfigManager.LoginSize.Split(strSplit, StringSplitOptions.RemoveEmptyEntries);
            this.Size = new Size(int.Parse(strSize[0]), int.Parse(strSize[1]));

            if (System.IO.File.Exists(ConfigManager.LoginBackground))
            {
                this.BackgroundImage = Image.FromFile(ConfigManager.LoginBackground);
            }
        }
               

        public bool Login(ref global::Define.IApplication application)
        {
            ThreadStart d = delegate { this.ShowDialog(); };
            Thread t = new Thread(d);
            t.Start();
            return true;
        }

        public void ShowMessage(string strMsg)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker d = delegate { ShowMessage(strMsg); };
                this.Invoke(d);
            }
            else
            {
                this.lblMessage.Text = strMsg;
                Application.DoEvents();
            }            
        }

        public IDbConnection SysConnection
        {
            set {  }
        }

        public object GisWorkspace
        {
            set {}
        }

        public global::Define.INhibernateHelper NhibernateHelper
        {
            set {  }
        }

        public global::Define.ILogWriter Logger
        {
            set {  }
        }

    }
}