
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Skyline.Core.UI
{
    public partial class FrmBase : DevExpress.XtraEditors.XtraForm
    {
        private string _frmName;

        /// <summary>
        /// 设置当前文件的功能说明 如"体块建模"
        /// </summary>
        public string FrmName
        {
            get { return _frmName; }
            set { _frmName = value; }
        }

        public FrmBase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 每次窗体构造前执行此方法 
        /// 作用为保证窗体操作唯一性
        /// </summary>
        /// <param name="frmMain">此窗体的 所属OwnedForm窗体对象</param>
        public bool BeginForm(Form frmMain)
        {
            if (frmMain.OwnedForms.Length >0)
            {
                FrmBase temp = null;
                try
                {
                    temp = (FrmBase)frmMain.OwnedForms[0];
                
                }
                
                catch (Exception)
                {
                    return false;
                }
    
                if (MessageBox.Show("当前正在操作" + temp.FrmName + "，是否关闭？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //temp.Dispose();
                    temp.Close();
                    return true;
                }
                else
                {
                    // MessageBox.Show("运行到了FormBae中的BeginForm这个方法，遇到错误需要退出");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 关闭信息提醒
        /// </summary>
        public void CloseForm()
        { 
            
        }
    }
}