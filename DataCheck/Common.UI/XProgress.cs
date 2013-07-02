//using System.Net.Mime;
using System.Threading;
using System.Windows.Forms;

namespace Common.UI
{
    /// <summary>
    /// 进度条
    /// </summary>
    public class XProgress
    {
        private frmProgress m_frmProgress = new frmProgress();

        /// <summary>
        /// 显示处理的文字信息内容
        /// </summary>
        /// <param name="sWhat">显示内容</param>
        public void ShowHint(string sWhat)
        {
            if (m_frmProgress != null)
            {
                //MediaTypeNames.Application.DoEvents();
                m_frmProgress.ShowDoing(sWhat);
                if (!m_frmProgress.Visible)
                    m_frmProgress.Show();
                Application.DoEvents();
            }
        }

        /// <summary>
        /// 显示精确精度控制进度条
        /// </summary>
        /// <param name="lMin">进度条最小值</param>
        /// <param name="lMax">进度条最大值</param>
        /// <param name="lStep">步进</param>
        /// <param name="parant">父窗体</param>
        public void ShowProgress(int lMin, int lMax, int lStep, IWin32Window parant)
        {
            m_frmProgress.Owner = (Form) parant;
            m_frmProgress.Show();
            //m_frmProgress.Show(parant);
            m_frmProgress.ShowProgress(lMin, lMax, lStep);
        }

        //private Thread pThread;

        /// <summary>
        /// 显示动画处理进度，用于处理无法精确判断进度的问题
        /// </summary>
        public void ShowGifProgress(IWin32Window parant)
        {
            //m_frmProgress.Show(parant);
            m_frmProgress.Owner = (Form) parant;
            //m_frmProgress.Show();
            m_frmProgress.ShowGifProgress();
            m_frmProgress.Show();

            //pThread = new Thread(StartRefreshGif);
            //pThread.Start();
        }


        //private void StartRefreshGif()
        //{
        //    //new System.Threading.Thread(new System.Threading.ThreadStart(StartDownload)).Start();
        //    //marqueeProgressBarControl1.Update();
        //    System.Threading.Thread.Sleep(1000);
        //    m_frmProgress.RefreshGif();
        //}

        /// <summary>
        /// 不显示进度控制窗体
        /// </summary>
        public void Hide()
        {
            if (m_frmProgress != null)
            {
                m_frmProgress.Visible = false;
                m_frmProgress.Update();
                //if (pThread != null)
                //{
                //    pThread.Abort();
                //}
            }
        }

        /// <summary>
        /// 步进
        /// </summary>
        public void Step()
        {
            if (m_frmProgress != null)
            {
                m_frmProgress.Step();
            }
        }

        /// <summary>
        /// 手动设定进度条的值
        /// </summary>
        /// <param name="intValue">进度条的值</param>
        public void SetValue(int intValue)
        {
            if (m_frmProgress != null)
            {
                m_frmProgress.SetValue(intValue);
            }
        }
    }
}