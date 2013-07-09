namespace Hy.Metadata.UI
{
    partial class UCStandardList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbStandards = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.lbStandards)).BeginInit();
            this.SuspendLayout();
            // 
            // lbStandards
            // 
            this.lbStandards.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.lbStandards.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.lbStandards.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.lbStandards.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.lbStandards.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.lbStandards.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.lbStandards.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.lbStandards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbStandards.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Default;
            this.lbStandards.HotTrackSelectMode = DevExpress.XtraEditors.HotTrackSelectMode.SelectItemOnHotTrack;
            this.lbStandards.Location = new System.Drawing.Point(0, 0);
            this.lbStandards.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.lbStandards.Name = "lbStandards";
            this.lbStandards.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.lbStandards.Size = new System.Drawing.Size(168, 400);
            this.lbStandards.SortOrder = System.Windows.Forms.SortOrder.None;
            this.lbStandards.TabIndex = 0;
            this.lbStandards.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.lbStandards.SelectedIndexChanged += new System.EventHandler(this.lbStandards_SelectedIndexChanged);
            // 
            // UCStandardList
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbStandards);
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.Name = "UCStandardList";
            this.Size = new System.Drawing.Size(168, 400);
            ((System.ComponentModel.ISupportInitialize)(this.lbStandards)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl lbStandards;

    }
}
