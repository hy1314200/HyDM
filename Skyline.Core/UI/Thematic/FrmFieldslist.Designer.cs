namespace Skyline.Core.UI
{
    partial class FrmFieldslist
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listViewFields = new System.Windows.Forms.ListView();
            this.simpleBtnCaculator = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listViewFields);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.simpleBtnCaculator);
            this.splitContainer1.Panel2.Controls.Add(this.simpleButtonCancel);
            this.splitContainer1.Panel2.Controls.Add(this.simpleButtonOK);
            this.splitContainer1.Size = new System.Drawing.Size(210, 260);
            this.splitContainer1.SplitterDistance = 230;
            this.splitContainer1.TabIndex = 0;
            // 
            // listViewFields
            // 
            this.listViewFields.CheckBoxes = true;
            this.listViewFields.Cursor = System.Windows.Forms.Cursors.Default;
            this.listViewFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFields.FullRowSelect = true;
            this.listViewFields.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewFields.Location = new System.Drawing.Point(0, 0);
            this.listViewFields.Name = "listViewFields";
            this.listViewFields.Size = new System.Drawing.Size(210, 230);
            this.listViewFields.TabIndex = 0;
            this.listViewFields.UseCompatibleStateImageBehavior = false;
            this.listViewFields.View = System.Windows.Forms.View.List;
            // 
            // simpleBtnCaculator
            // 
            this.simpleBtnCaculator.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.simpleBtnCaculator.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.simpleBtnCaculator.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.simpleBtnCaculator.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.simpleBtnCaculator.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.simpleBtnCaculator.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.simpleBtnCaculator.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.simpleBtnCaculator.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.simpleBtnCaculator.DialogResult = System.Windows.Forms.DialogResult.None;
            this.simpleBtnCaculator.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.simpleBtnCaculator.Location = new System.Drawing.Point(3, 1);
            this.simpleBtnCaculator.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.simpleBtnCaculator.Name = "simpleBtnCaculator";
            this.simpleBtnCaculator.Size = new System.Drawing.Size(45, 23);
            this.simpleBtnCaculator.TabIndex = 2;
            this.simpleBtnCaculator.Text = "表达式";
            this.simpleBtnCaculator.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.simpleBtnCaculator.Visible = false;
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.simpleButtonCancel.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.simpleButtonCancel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.simpleButtonCancel.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.simpleButtonCancel.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.simpleButtonCancel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.simpleButtonCancel.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.simpleButtonCancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.simpleButtonCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.simpleButtonCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.simpleButtonCancel.Location = new System.Drawing.Point(153, 1);
            this.simpleButtonCancel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(45, 23);
            this.simpleButtonCancel.TabIndex = 1;
            this.simpleButtonCancel.Text = "取消";
            this.simpleButtonCancel.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.simpleButtonOK.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.simpleButtonOK.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.simpleButtonOK.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.simpleButtonOK.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.simpleButtonOK.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.simpleButtonOK.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.simpleButtonOK.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.simpleButtonOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.simpleButtonOK.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.simpleButtonOK.Location = new System.Drawing.Point(94, 1);
            this.simpleButtonOK.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(45, 23);
            this.simpleButtonOK.TabIndex = 0;
            this.simpleButtonOK.Text = "确定";
            this.simpleButtonOK.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // FrmFieldslist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 260);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFieldslist";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "请选择字段！";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmFieldslist_FormClosed);
            this.Load += new System.EventHandler(this.FrmStaThemsFieldslist_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listViewFields;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleBtnCaculator;
    }
}