using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hy.Check.UI
{
    public delegate void RequiredPageChangedHandle(int pageIndex);

    public partial class UCNavigate : DevExpress.XtraEditors.XtraUserControl
    {
        public UCNavigate()
        {
            InitializeComponent();
        }


        public event RequiredPageChangedHandle RequiredPageChanged;

        private int m_PageCount;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return m_PageCount;
            }
            set
            {
                m_PageCount = value;                
                if (m_PageCount < 2)
                {
                    this.btnNext.Enabled = false;
                }
                else
                {
                    this.btnNext.Enabled = true;
                }
                ShowPage();
            }
        }

        private int m_PageIndex;
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get
            {
                return m_PageIndex;
            }
            set
            {
                m_PageIndex = value;
                // Enabled设置
                if (this.m_PageIndex == 0)
                {
                    this.btnPre.Enabled = false;
                }
                else
                {
                    this.btnPre.Enabled = true;
                }

                if (this.m_PageIndex+1 >= this.m_PageCount)
                {
                    this.btnNext.Enabled = false;
                }
                else
                {
                    this.btnNext.Enabled = true;
                }
                ShowPage();
            }
        }

        private void ShowPage()
        {
            txtPage.Text = string.Format("{0}/{1}",(this.m_PageCount==0?0: this.m_PageIndex+1), this.m_PageCount);
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if ( this.m_PageIndex>0)
            {
                this.m_PageIndex--;
                if (this.RequiredPageChanged != null)
                {
                    this.RequiredPageChanged.Invoke(this.m_PageIndex);
                }
                this.PageIndex = this.m_PageIndex;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.m_PageCount > this.m_PageIndex)
            {
                this.m_PageIndex++;
                if (this.RequiredPageChanged != null)
                {
                    this.RequiredPageChanged.Invoke(this.m_PageIndex);
                }
                this.PageIndex = this.m_PageIndex;
            }
        }


    }
}
