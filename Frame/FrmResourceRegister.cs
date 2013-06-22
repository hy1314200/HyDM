using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Define;
using System.Collections;
using Frame.Define;

namespace Frame
{
    public partial class FrmResourceRegister : Form
    {
        public FrmResourceRegister()
        {
            InitializeComponent();
        }


        private void SendMessage(string strMsg)
        {
            this.lblStatus.Text = strMsg;
            Application.DoEvents();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            IList existList = Environment.NHibernateHelper.GetAll(typeof(ClassInfo));
            IEnumerable<ClassInfo> eList = existList.Cast<ClassInfo>();
            int count = existList.Count;

            List<ClassInfo> infoList = ucResourceRegister1.SelectedClasses;
            int selCount = infoList.Count;
            int curIndex = 0;
            foreach (ClassInfo info in infoList)
            {
                SendMessage(string.Format("正在注册{0}/{1}", ++curIndex, selCount));
                bool flag = true;
                for (int i = 0; i < count; i++)
                {
                    ClassInfo eInfo = eList.ElementAt(i);
                    if (eInfo.ClassName == info.ClassName && eInfo.DllName == info.DllName)
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    Environment.NHibernateHelper.SaveObject(info);
                }
            }
            Environment.NHibernateHelper.Flush();
            SendMessage("注册完成");
            this.DialogResult = DialogResult.OK;
        }
    }
}
