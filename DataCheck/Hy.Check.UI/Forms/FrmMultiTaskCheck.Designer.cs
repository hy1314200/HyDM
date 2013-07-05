namespace Check.UI.Forms
{
    partial class FrmMultiTaskCheck
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblOperateType = new DevExpress.XtraEditors.LabelControl();
            this.clbTask = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.marqueeProgressBarControl1 = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clbTask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Location = new System.Drawing.Point(2, 185);
            this.progressBarControl1.Size = new System.Drawing.Size(469, 22);
            // 
            // panelControl2
            // 
            this.panelControl2.Location = new System.Drawing.Point(2, 207);
            this.panelControl2.Size = new System.Drawing.Size(469, 36);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(412, 5);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.clbTask);
            this.panelControl3.Controls.Add(this.marqueeProgressBarControl1);
            this.panelControl3.Controls.Add(this.lblOperateType);
            this.panelControl3.Location = new System.Drawing.Point(149, 0);
            this.panelControl3.Size = new System.Drawing.Size(473, 245);
            this.panelControl3.Controls.SetChildIndex(this.lblOperateType, 0);
            this.panelControl3.Controls.SetChildIndex(this.labelControl1, 0);
            this.panelControl3.Controls.SetChildIndex(this.lblRuleCount, 0);
            this.panelControl3.Controls.SetChildIndex(this.labelControl2, 0);
            this.panelControl3.Controls.SetChildIndex(this.lblTime, 0);
            this.panelControl3.Controls.SetChildIndex(this.labelControl3, 0);
            this.panelControl3.Controls.SetChildIndex(this.lblExcutedRuleCount, 0);
            this.panelControl3.Controls.SetChildIndex(this.panelControl2, 0);
            this.panelControl3.Controls.SetChildIndex(this.lblOperate, 0);
            this.panelControl3.Controls.SetChildIndex(this.progressBarControl1, 0);
            this.panelControl3.Controls.SetChildIndex(this.labelControl5, 0);
            this.panelControl3.Controls.SetChildIndex(this.lblErrorCount, 0);
            this.panelControl3.Controls.SetChildIndex(this.marqueeProgressBarControl1, 0);
            this.panelControl3.Controls.SetChildIndex(this.clbTask, 0);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(114, 120);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(279, 120);
            // 
            // lblTime
            // 
            this.lblTime.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblTime.Appearance.Options.UseForeColor = true;
            this.lblTime.Location = new System.Drawing.Point(381, 143);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(279, 143);
            // 
            // lblRuleCount
            // 
            this.lblRuleCount.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblRuleCount.Appearance.Options.UseForeColor = true;
            this.lblRuleCount.Location = new System.Drawing.Point(202, 121);
            // 
            // lblExcutedRuleCount
            // 
            this.lblExcutedRuleCount.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblExcutedRuleCount.Appearance.Options.UseForeColor = true;
            this.lblExcutedRuleCount.Location = new System.Drawing.Point(381, 120);
            // 
            // lblOperate
            // 
            this.lblOperate.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblOperate.Appearance.Options.UseForeColor = true;
            this.lblOperate.Location = new System.Drawing.Point(6, 163);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Size = new System.Drawing.Size(149, 245);
            // 
            // lblErrorCount
            // 
            this.lblErrorCount.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblErrorCount.Appearance.Options.UseForeColor = true;
            this.lblErrorCount.Location = new System.Drawing.Point(202, 143);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(114, 143);
            // 
            // btnViewLog
            // 
            this.btnViewLog.Location = new System.Drawing.Point(302, 5);
            // 
            // lblOperateType
            // 
            this.lblOperateType.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblOperateType.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblOperateType.Appearance.Options.UseFont = true;
            this.lblOperateType.Appearance.Options.UseForeColor = true;
            this.lblOperateType.Location = new System.Drawing.Point(6, 119);
            this.lblOperateType.Name = "lblOperateType";
            this.lblOperateType.Size = new System.Drawing.Size(92, 14);
            this.lblOperateType.TabIndex = 11;
            this.lblOperateType.Text = "正在创建任务……";
            // 
            // clbTask
            // 
            this.clbTask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.clbTask.Appearance.BackColor = System.Drawing.Color.White;
            this.clbTask.Appearance.Options.UseBackColor = true;
            this.clbTask.Location = new System.Drawing.Point(5, 5);
            this.clbTask.Name = "clbTask";
            this.clbTask.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.clbTask.Size = new System.Drawing.Size(463, 98);
            this.clbTask.TabIndex = 7;
            // 
            // marqueeProgressBarControl1
            // 
            this.marqueeProgressBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.marqueeProgressBarControl1.EditValue = 0;
            this.marqueeProgressBarControl1.Location = new System.Drawing.Point(2, 186);
            this.marqueeProgressBarControl1.Name = "marqueeProgressBarControl1";
            this.marqueeProgressBarControl1.Size = new System.Drawing.Size(471, 20);
            this.marqueeProgressBarControl1.TabIndex = 12;
            // 
            // FrmMultiTaskCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 245);
            this.Name = "FrmMultiTaskCheck";
            this.Text = "批量任务处理";
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clbTask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MarqueeProgressBarControl marqueeProgressBarControl1;
        private DevExpress.XtraEditors.LabelControl lblOperateType;
        private DevExpress.XtraEditors.CheckedListBoxControl clbTask;
    }
}