namespace CheckCommand.MeasureCommand
{
    partial class FormDis
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
            this.components = new System.ComponentModel.Container();
            this.m_LabelMeasureType = new DevExpress.XtraEditors.LabelControl();
            this.m_labelSegment = new DevExpress.XtraEditors.LabelControl();
            this.m_labelLength = new DevExpress.XtraEditors.LabelControl();
            this.m_labelArea = new DevExpress.XtraEditors.LabelControl();
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // m_LabelMeasureType
            // 
            this.m_LabelMeasureType.Location = new System.Drawing.Point(16, 12);
            this.m_LabelMeasureType.Name = "m_LabelMeasureType";
            this.m_LabelMeasureType.Size = new System.Drawing.Size(73, 14);
            this.m_LabelMeasureType.TabIndex = 0;
            this.m_LabelMeasureType.Text = "MeasureType";
            this.m_LabelMeasureType.Visible = false;
            // 
            // m_labelSegment
            // 
            this.m_labelSegment.Location = new System.Drawing.Point(131, 12);
            this.m_labelSegment.Name = "m_labelSegment";
            this.m_labelSegment.Size = new System.Drawing.Size(61, 14);
            this.m_labelSegment.TabIndex = 1;
            this.m_labelSegment.Text = "Segment:0";
            this.m_labelSegment.Visible = false;
            // 
            // m_labelLength
            // 
            this.m_labelLength.Location = new System.Drawing.Point(131, 25);
            this.m_labelLength.Name = "m_labelLength";
            this.m_labelLength.Size = new System.Drawing.Size(50, 14);
            this.m_labelLength.TabIndex = 2;
            this.m_labelLength.Text = "Length:0";
            this.m_labelLength.Visible = false;
            // 
            // m_labelArea
            // 
            this.m_labelArea.Location = new System.Drawing.Point(16, 15);
            this.m_labelArea.Name = "m_labelArea";
            this.m_labelArea.Size = new System.Drawing.Size(36, 14);
            this.m_labelArea.TabIndex = 3;
            this.m_labelArea.Text = "Area:0";
            // 
            // m_timer
            // 
            this.m_timer.Tick += new System.EventHandler(this.m_timer_Tick);
            // 
            // FormDis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 57);
            this.Controls.Add(this.m_labelArea);
            this.Controls.Add(this.m_labelLength);
            this.Controls.Add(this.m_labelSegment);
            this.Controls.Add(this.m_LabelMeasureType);
            this.Location = new System.Drawing.Point(30, 150);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDis";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "²âÁ¿½á¹û";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormDis_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl m_LabelMeasureType;
        private DevExpress.XtraEditors.LabelControl m_labelSegment;
        private DevExpress.XtraEditors.LabelControl m_labelLength;
        private DevExpress.XtraEditors.LabelControl m_labelArea;
        public System.Windows.Forms.Timer m_timer;
    }
}