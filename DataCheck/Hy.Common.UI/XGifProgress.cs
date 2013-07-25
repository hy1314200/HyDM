using System.Threading;
using System.Windows.Forms;

namespace Hy.Common.UI
{
    /// <summary>
    /// 动画进度
    /// </summary>
    public class XGifProgress
    {
        private frmProgress _progressForm = null;


        /// <summary>
        /// 构造方法
        /// </summary>
        public XGifProgress()
        {
            _progressForm = new frmProgress();
        }

        /// <summary>
        /// 提示框
        /// </summary>
        protected internal frmProgress ProgressForm
        {
            get { return _progressForm; }
            set { _progressForm = value; }
        }

        /// <summary>
        /// 显示进度提示
        /// </summary>
        /// <param name="toolstip">提示内容</param>
        public void ShowHint(string toolstip)
        {
            Application.DoEvents();
            ShowHint(null, toolstip);
        }


        /// <summary>
        /// 显示进度提示
        /// </summary>
        /// <param name="owner">谁调用的?</param>
        /// <param name="toolstip">提示内容</param>
        public void ShowHint(Control owner, string toolstip)
        {
            //if (ProgressForm.Visible == true)
            //{
            //    return;
            //}

            ProgressForm.Owner = (Form) owner;
            if (owner != null)
            {
                owner.UseWaitCursor = true;
                //this.ProgressForm.Parent = owner;
            }
            m_ToolStip = toolstip;
            ThreadStart start = new ThreadStart(ShowHintInthread);
            new Thread(start).Start();
        }
        private string m_ToolStip;
        private delegate void NoneHandler();
        private delegate void ShowStringHandler(string strContent);
        private void ShowHintInthread()
        {
            if (ProgressForm.InvokeRequired)
            {
                ProgressForm.Invoke(new NoneHandler(ProgressForm.ShowGifProgress));
                object[] objStip = { m_ToolStip };
                ProgressForm.Invoke(new ShowStringHandler(ProgressForm.ShowDoing), objStip);
                ProgressForm.Invoke(new NoneHandler(ProgressForm.ShowProgress));
            }
            else
            {
                ProgressForm.ShowGifProgress();
                ProgressForm.ShowDoing(m_ToolStip);
                ProgressForm.ShowProgress();
            }
        }

        /// <summary>
        /// 关闭进度提示
        /// </summary>
        public void Hide()
        {
            if (ProgressForm.Visible == false)
            {
                return;
            }

            //ProgressForm.Dispose();

            ThreadStart start = new ThreadStart(ProgressForm.Hide);
            ProgressForm.Invoke(start);
            //ProgressForm.BeginInvoke(start);
           

            //this.ProgressForm = new FrmSimpleProgress();
        }
    }
}