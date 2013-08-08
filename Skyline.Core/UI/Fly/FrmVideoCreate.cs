using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TerraExplorerX;

namespace Skyline.Core.UI
{
    public partial class FrmVideoCreate : FrmBase
    {
        private Form _frmMain;
        private IPresentation61 CurrentVideo;
        public FrmVideoCreate(Form frmMain)
        {
            _frmMain = frmMain;
            if (base.BeginForm(frmMain))
            {
                InitializeComponent();
            }
            else
            {
                this.Close();
            }
        }

        private void FrmVideoCreate_Load(object sender, EventArgs e)
        {
            try
            {
                int GroupID = Program.sgworld.ProjectTree.FindItem(@"Video\temp");
                if (GroupID > 0)
                {
                    CurrentVideo = (IPresentation61)Program.sgworld.ProjectTree.GetObject(GroupID);
                    this.labelRecord.Text = "开始录制";
                    this.labelPlay.Text = "播放";
                    this.simpleBtnPlay.Image = Properties.Resources.GenericBlueRightArrowNoTail32;
                    this.simpleBtnPlay.Enabled = false;
                    this.simpleBtnStop.Enabled = false;
                    this.simpleBtnVideoExport.Enabled = false;
                }
                else
                    MessageBox.Show("创建视频不成功！");
            }
            catch
            {
                DialogResult dr = MessageBox.Show("遇到问题!可能原因：缺乏必要图标文件，如果窗体显示无异常，可以选择忽略以继续","",MessageBoxButtons.AbortRetryIgnore,MessageBoxIcon.Stop);
                if (dr == DialogResult.Abort)
                    this.Close();
            }
        }

        //开始/结束录制
        private bool CurrentRecordState = false;//false为非录制状态，true为录制状态
        private void simpleBtnRecord_Click(object sender, EventArgs e)
        {
            try
            {
                //录制与播放状态不能共存
                if (CurrentVideo != null)
                {
                    //若已录制则提示是否重录
                    if (CurrentVideo.Steps.Count > 0 & CurrentRecordState == false)
                    {
                        DialogResult dr = MessageBox.Show("已录制视频，是否重录?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.Yes)
                        {
                            for (int i = CurrentVideo.Steps.Count - 1; i >= 0; i--)
                            {
                                CurrentVideo.DeleteStep(i);
                            }
                        }
                    }

                    if (CurrentRecordState)
                    {
                        //停止录制
                        CurrentVideo.StopRecord();
                        this.labelRecord.Text = "开始录制";
                        this.simpleBtnRecord.Image = Properties.Resources.Record32;
                        CurrentRecordState = false;
                        this.simpleBtnPlay.Enabled = true;
                        this.simpleBtnStop.Enabled = true;
                        this.simpleBtnVideoExport.Enabled = true;
                    }
                    else
                    {
                        //开始录制
                        if (CurrentVideo.Steps.Count == 0)
                        {
                            CurrentVideo.StartRecord();
                            this.labelRecord.Text = "停止录制";
                            this.simpleBtnRecord.Image = Properties.Resources.RecordStop32;
                            CurrentRecordState = true;
                            this.simpleBtnPlay.Enabled = false;
                            this.simpleBtnPlay.Enabled = false;
                            this.simpleBtnVideoExport.Enabled = false;

                        }
                    }
                }
            }
            catch
            {
               DialogResult dr = MessageBox.Show("遇到问题!可能原因：缺乏必要图标文件，如果窗体显示无异常，可以选择忽略以继续", "", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
               if (dr == DialogResult.Abort)
                   this.Close();
            }
        }

        //重录
        //private void simpleBtnReRecord_Click(object sender, EventArgs e)
        //{
               
        //    if (CurrentVideo != null)
        //    {
        //        if (CurrentRecordState)
        //        {
        //            //录制中则停止录制，若已有steps则清空,播放设为false
        //            CurrentVideo.StopRecord();
        //            this.labelRecord.Text = "开始录制";
        //            CurrentRecordState = false;
        //            if (CurrentVideo.Steps.Count > 0)
        //            {
        //                for (int i = CurrentVideo.Steps.Count-1; i >= 0; i--)
        //                {
        //                    CurrentVideo.DeleteStep(i);
        //                }
        //            }
        //            if (this.simpleBtnPlay.Enabled == true)
        //            {
        //                this.simpleBtnPlay.Enabled = false;
        //                if (this.simpleBtnRecord.Enabled == false)
        //                    this.simpleBtnRecord.Enabled = true;
        //            }

        //        }
        //        else
        //        {
        //            //播放或暂停状态先停止播放，清空step
        //            if (CurrentVideo.PresentationStatus != PresentationStatus.PS_NOTPLAYING)
        //            {
        //                CurrentVideo.Stop();
        //                if (this.simpleBtnPlay.Enabled == true)
        //                {
        //                    this.simpleBtnPlay.Image = Properties.Resources.GenericBlueRightArrowNoTail32;
        //                    this.labelPlay.Text = "播放";
        //                }
        //            }
        //            if (CurrentVideo.Steps.Count > 0)
        //            {
        //                for (int i = CurrentVideo.Steps.Count - 1; i >= 0; i--)
        //                {
        //                    CurrentVideo.DeleteStep(i);
        //                }
        //            }
        //            if (this.simpleBtnPlay.Enabled == true)
        //            {
        //                this.simpleBtnPlay.Enabled = false;
        //                if (this.simpleBtnRecord.Enabled == false)
        //                    this.simpleBtnRecord.Enabled = true;
        //            }
        //        }
        //    }
            
        //}

        //播放或暂停
        private void simpleBtnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentVideo != null)
                {
                    if (CurrentVideo.Steps.Count > 0)
                    {
                        if (this.simpleBtnRecord.Enabled == true)
                            this.simpleBtnRecord.Enabled = false;
                        Program.sgworld.OnPresentationStatusChanged += new _ISGWorld61Events_OnPresentationStatusChangedEventHandler(sgworld_OnPresentationStatusChanged);
                        if (CurrentVideo.PresentationStatus == PresentationStatus.PS_NOTPLAYING)
                        {
                            //由停止开始播放
                            CurrentVideo.PlayAlgorithm = PresentationPlayAlgorithm.PPA_SPLINE;
                            //CurrentVideo.LoopRoute = true;
                            CurrentVideo.PlaySpeedFactor = PresentationPlaySpeed.PPS_SLOW;
                            CurrentVideo.Play(0);
                            this.simpleBtnPlay.Image = Properties.Resources.GenericBluePause32;
                            this.labelPlay.Text = "暂停";
                        }
                        else
                        {
                            if (CurrentVideo.PresentationStatus == PresentationStatus.PS_PLAYING)
                            {
                                //暂停播放
                                CurrentVideo.Pause();
                                this.simpleBtnPlay.Image = Properties.Resources.GenericBlueRightArrowNoTail32;
                                this.labelPlay.Text = "播放";
                            }
                            else
                            {
                                //由暂停开始播放
                                CurrentVideo.PlayAlgorithm = PresentationPlayAlgorithm.PPA_SPLINE;
                                //CurrentVideo.LoopRoute = true;
                                CurrentVideo.PlaySpeedFactor = PresentationPlaySpeed.PPS_SLOW;
                                CurrentVideo.Resume();
                                this.simpleBtnPlay.Image = Properties.Resources.GenericBluePause32;
                                this.labelPlay.Text = "暂停";
                            }
                        }
                    }
                }

            }
            catch
            {
                DialogResult dr = MessageBox.Show("遇到问题!可能原因：缺乏必要图标文件，如果窗体显示无异常，可以选择忽略以继续", "", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                if (dr == DialogResult.Abort)
                    this.Close();
            }
        }

        void sgworld_OnPresentationStatusChanged(string PresentationID, PresentationStatus Status)
        {
            //throw new NotImplementedException();
            try
            {
                if (Status == PresentationStatus.PS_NOTPLAYING)
                {
                    if (this.simpleBtnRecord.Enabled == false)
                        this.simpleBtnRecord.Enabled = true;
                    this.simpleBtnPlay.Image = Properties.Resources.GenericBlueRightArrowNoTail32;
                    this.labelPlay.Text = "播放";
                }
            }
            catch
            {
                DialogResult dr = MessageBox.Show("遇到问题!可能原因：缺乏必要图标文件，如果窗体显示无异常，可以选择忽略以继续", "", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                if (dr == DialogResult.Abort)
                    this.Close();
            }
        }


        //停止
        private void simpleBtnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentVideo != null)
                {
                    if (CurrentVideo.PresentationStatus != PresentationStatus.PS_NOTPLAYING)
                    {
                        CurrentVideo.Stop();
                        if (this.simpleBtnRecord.Enabled == false)
                            this.simpleBtnRecord.Enabled = true;
                        this.simpleBtnPlay.Image = Properties.Resources.GenericBlueRightArrowNoTail32;
                        this.labelPlay.Text = "播放";
                    }
                }
            }
            catch
            {
                DialogResult dr = MessageBox.Show("遇到问题!可能原因：缺乏必要图标文件，如果窗体显示无异常，可以选择忽略以继续", "", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                if (dr == DialogResult.Abort)
                    this.Close();
            }
        }
        //输出视频
        private void simpleBtnVideoExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentVideo != null)
                {
                    // 已录制
                    if (CurrentVideo.Steps.Count > 0)
                    {
                        ////停止录制状态
                        //if (!CurrentRecordState)
                        //{
                        SaveFileDialog dr = new SaveFileDialog();
                        dr.Title = "输出视频";
                        dr.Filter = "视频文件(*.avi)|*.avi";
                        if (dr.ShowDialog() == DialogResult.OK)
                        {
                            IAviWriter61 AviExport = CurrentVideo.AviWriter;
                            AviExport.CreateMovie(dr.FileName, Program.sgworld.Window.Rect.Width, Program.sgworld.Window.Rect.Height, 25.0, null);
                            //AviExport.CreateMovie(dr.FileName, 400, 320, 25.0, null);
                            MessageBox.Show("视频输出成功！");
                        }
                        //}
                        //else
                        //    MessageBox.Show("视频录制中，请先停止录制再导出！");
                    }
                    else
                        MessageBox.Show("当前视频内容为空，请先录制视频！");
                }
            }
            catch
            {
                 MessageBox.Show("视频输出遇到问题!");
            }
               
        }

        private void FrmVideoCreate_FormClosing(object sender, FormClosingEventArgs e)
        {
            _frmMain.RemoveOwnedForm(this);
            if (CurrentVideo != null)
            {
                int GroupID = Program.sgworld.ProjectTree.FindItem("Video");
                if(GroupID != 0)
                Program.sgworld.ProjectTree.DeleteItem(GroupID);
            }
        }
    
    }
}
