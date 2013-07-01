namespace Check.UI.Forms
{
    partial class FrmTaskCheck
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
            this.lblTotalCount = new DevExpress.XtraEditors.LabelControl();
            this.lblAvaliateCount = new DevExpress.XtraEditors.LabelControl();
            this.lblErrorCount = new DevExpress.XtraEditors.LabelControl();
            this.lblOperate = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.Location = new System.Drawing.Point(31, 13);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(48, 14);
            this.lblTotalCount.TabIndex = 0;
            this.lblTotalCount.Text = "总规则数";
            // 
            // lblAvaliateCount
            // 
            this.lblAvaliateCount.Location = new System.Drawing.Point(31, 33);
            this.lblAvaliateCount.Name = "lblAvaliateCount";
            this.lblAvaliateCount.Size = new System.Drawing.Size(72, 14);
            this.lblAvaliateCount.TabIndex = 0;
            this.lblAvaliateCount.Text = "可执行规则数";
            // 
            // lblErrorCount
            // 
            this.lblErrorCount.Location = new System.Drawing.Point(241, 33);
            this.lblErrorCount.Name = "lblErrorCount";
            this.lblErrorCount.Size = new System.Drawing.Size(72, 14);
            this.lblErrorCount.TabIndex = 0;
            this.lblErrorCount.Text = "可执行规则数";
            // 
            // lblOperate
            // 
            this.lblOperate.Location = new System.Drawing.Point(31, 70);
            this.lblOperate.Name = "lblOperate";
            this.lblOperate.Size = new System.Drawing.Size(24, 14);
            this.lblOperate.TabIndex = 0;
            this.lblOperate.Text = "动作";
            // 
            // FrmTaskCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 105);
            this.Controls.Add(this.lblOperate);
            this.Controls.Add(this.lblErrorCount);
            this.Controls.Add(this.lblAvaliateCount);
            this.Controls.Add(this.lblTotalCount);
            this.Name = "FrmTaskCheck";
            this.Text = "任务检查";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTotalCount;
        private DevExpress.XtraEditors.LabelControl lblAvaliateCount;
        private DevExpress.XtraEditors.LabelControl lblErrorCount;
        private DevExpress.XtraEditors.LabelControl lblOperate;
    }
}