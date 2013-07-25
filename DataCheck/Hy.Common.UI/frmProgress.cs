using System;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Hy.Common.UI
{
    public partial class frmProgress : XtraForm
    {
        public frmProgress()
        {
            InitializeComponent();
        }

        //public delegate void RefreshGif();

        private void frmProgress_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 初始化进度条
        /// </summary>
        /// <param name="lMin">最小值</param>
        /// <param name="lMax">最大值</param>
        /// <param name="lStep">步进</param>
        public void ShowProgress(int lMin, int lMax, int lStep)
        {
            progressBarControl1.Visible = true;
            marqueeProgressBarControl1.Visible = false;
            progressBarControl1.Properties.PercentView = true;
            
            
            progressBarControl1.Properties.Minimum = lMin;
            progressBarControl1.Properties.Maximum = lMax;
            progressBarControl1.Properties.Step = lStep;
            progressBarControl1.Position = lMin;
            //progressBarControl1.Update();
            Show();
        }

        /// <summary>
        /// 显示进度动画
        /// </summary>
        public void ShowGifProgress()
        {
            progressBarControl1.Visible = false;
            marqueeProgressBarControl1.Visible = true;
            marqueeProgressBarControl1.Properties.ProgressKind = ProgressKind.Horizontal;
            marqueeProgressBarControl1.Update();
        }

        //private delegate void _RefreshGif();
        //public void RefreshGif()
        //{
        //    _RefreshGif rf = delegate
        //        {
        //            marqueeProgressBarControl1.Update();
        //        };
        //    BeginInvoke(rf);
        //}
        
        /// <summary>
        /// 显示进度条
        /// </summary>
        /// <param name="sWhat">显示内容</param>
        public void ShowDoing(string sWhat)
        {
            labelControl1.Text = sWhat;
            labelControl1.Update();
        }

        /// <summary>
        /// 步进方法
        /// </summary>
        public void Step()
        {
            if (progressBarControl1.Visible)
            {
                progressBarControl1.PerformStep();
                progressBarControl1.Update();
                Application.DoEvents();
            }
        }

        /// <summary>
        /// 手动设置进度条值
        /// </summary>
        /// <param name="intValue">进度条值</param>
        public void SetValue(int intValue)
        {
            if (progressBarControl1.Visible)
            {
                progressBarControl1.Position = intValue;
                progressBarControl1.Update();
            }
        }

        /// <summary>
        /// 显示进度提示
        /// </summary>
        protected internal void ShowProgress()
        {
            try
            {
                if (Visible == false)
                {
                    ShowDialog();
                }                
            }
            catch(Exception ex)
            {}
        }

        private void progressBarControl1_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            double v = Convert.ToDouble(e.Value);
            e.DisplayText = v.ToString("n0");
        }
    }
}