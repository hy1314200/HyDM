namespace Hy.Check.UI.Forms
{
    partial class FrmSQLSearch
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cboLayers = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cboFields = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cboFieldsValue = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnlist = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cboLayers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFields.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFieldsValue.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "图层名：";
            // 
            // cboLayers
            // 
            this.cboLayers.Location = new System.Drawing.Point(10, 32);
            this.cboLayers.Name = "cboLayers";
            this.cboLayers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboLayers.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboLayers.Size = new System.Drawing.Size(105, 21);
            this.cboLayers.TabIndex = 1;
            this.cboLayers.SelectedIndexChanged += new System.EventHandler(this.cboLayers_SelectedIndexChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(138, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "字段名：";
            // 
            // cboFields
            // 
            this.cboFields.Location = new System.Drawing.Point(136, 32);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFields.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboFields.Size = new System.Drawing.Size(111, 21);
            this.cboFields.TabIndex = 3;
            this.cboFields.SelectedIndexChanged += new System.EventHandler(this.cboFields_SelectedIndexChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(267, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "字段值：";
            // 
            // cboFieldsValue
            // 
            this.cboFieldsValue.Location = new System.Drawing.Point(266, 32);
            this.cboFieldsValue.Name = "cboFieldsValue";
            this.cboFieldsValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFieldsValue.Properties.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboFieldsValue_Properties_KeyPress);
            this.cboFieldsValue.Size = new System.Drawing.Size(127, 21);
            this.cboFieldsValue.TabIndex = 5;
            this.cboFieldsValue.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cboFieldsValue_ButtonClick);
            this.cboFieldsValue.DragDrop += new System.Windows.Forms.DragEventHandler(this.cboFieldsValue_DragDrop);
            this.cboFieldsValue.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cboFieldsValue_ButtonPressed);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(291, 70);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(61, 20);
            this.btnQuery.TabIndex = 6;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(371, 70);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(55, 20);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnlist
            // 
            this.btnlist.Location = new System.Drawing.Point(395, 33);
            this.btnlist.Name = "btnlist";
            this.btnlist.Size = new System.Drawing.Size(31, 20);
            this.btnlist.TabIndex = 6;
            this.btnlist.Text = "列举";
            this.btnlist.Click += new System.EventHandler(this.btnlist_Click);
            // 
            // frmSQLSearch1
            // 
            this.AcceptButton = this.btnQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(442, 102);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnlist);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.cboFieldsValue);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.cboFields);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.cboLayers);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSQLSearch1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL查询";
            ((System.ComponentModel.ISupportInitialize)(this.cboLayers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFields.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFieldsValue.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cboLayers;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cboFields;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cboFieldsValue;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnlist;
    }
}