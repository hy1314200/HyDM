namespace Hy.Check.UI.Forms
{
    partial class FrmLayerTransparency
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
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.trackBarLayerTransparency = new DevExpress.XtraEditors.TrackBarControl();
            this.txtLayerTransparency = new DevExpress.XtraEditors.TextEdit();
            this.btnQuit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLayerTransparency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLayerTransparency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLayerTransparency.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(30, 79);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(74, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(118, 79);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(120, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "请设置图层的透明度：";
            // 
            // trackBarLayerTransparency
            // 
            this.trackBarLayerTransparency.EditValue = null;
            this.trackBarLayerTransparency.Location = new System.Drawing.Point(8, 28);
            this.trackBarLayerTransparency.Name = "trackBarLayerTransparency";
            this.trackBarLayerTransparency.Size = new System.Drawing.Size(275, 45);
            this.trackBarLayerTransparency.TabIndex = 3;
            this.trackBarLayerTransparency.ValueChanged += new System.EventHandler(this.trackBarLayerTransparency_ValueChanged);
            // 
            // txtLayerTransparency
            // 
            this.txtLayerTransparency.Enabled = false;
            this.txtLayerTransparency.Location = new System.Drawing.Point(129, 3);
            this.txtLayerTransparency.Name = "txtLayerTransparency";
            this.txtLayerTransparency.Size = new System.Drawing.Size(153, 21);
            this.txtLayerTransparency.TabIndex = 4;
            // 
            // btnQuit
            // 
            this.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuit.Location = new System.Drawing.Point(206, 79);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(74, 23);
            this.btnQuit.TabIndex = 5;
            this.btnQuit.Text = "关闭";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // FrmLayerTransparency
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnQuit;
            this.ClientSize = new System.Drawing.Size(285, 110);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.txtLayerTransparency);
            this.Controls.Add(this.trackBarLayerTransparency);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmLayerTransparency";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图层透明度设置";
            this.Load += new System.EventHandler(this.frmLayerTransparency_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLayerTransparency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLayerTransparency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLayerTransparency.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TrackBarControl trackBarLayerTransparency;
        private DevExpress.XtraEditors.TextEdit txtLayerTransparency;
        private DevExpress.XtraEditors.SimpleButton btnQuit;
    }
}