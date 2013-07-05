namespace Hy.Check.UI.UC
{
    partial class UCSQLSearch
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
            this.lblNums = new DevExpress.XtraEditors.LabelControl();
            this.combMethods = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnUniqueValue = new DevExpress.XtraEditors.SimpleButton();
            this.listboxValues = new DevExpress.XtraEditors.ListBoxControl();
            this.btn11 = new DevExpress.XtraEditors.SimpleButton();
            this.btn10 = new DevExpress.XtraEditors.SimpleButton();
            this.lblLayer = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btn12 = new DevExpress.XtraEditors.SimpleButton();
            this.btn13 = new DevExpress.XtraEditors.SimpleButton();
            this.btn9 = new DevExpress.XtraEditors.SimpleButton();
            this.btn6 = new DevExpress.XtraEditors.SimpleButton();
            this.btn3 = new DevExpress.XtraEditors.SimpleButton();
            this.btn8 = new DevExpress.XtraEditors.SimpleButton();
            this.btn7 = new DevExpress.XtraEditors.SimpleButton();
            this.btn5 = new DevExpress.XtraEditors.SimpleButton();
            this.btn4 = new DevExpress.XtraEditors.SimpleButton();
            this.btn2 = new DevExpress.XtraEditors.SimpleButton();
            this.btn1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.combLayers = new DevExpress.XtraEditors.ComboBoxEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.listboxFields = new DevExpress.XtraEditors.ListBoxControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.combMethods.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listboxValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.combLayers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listboxFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNums
            // 
            this.lblNums.Location = new System.Drawing.Point(11, 337);
            this.lblNums.Name = "lblNums";
            this.lblNums.Size = new System.Drawing.Size(0, 14);
            this.lblNums.TabIndex = 55;
            // 
            // combMethods
            // 
            this.combMethods.EditValue = "创建新的选择集";
            this.combMethods.Location = new System.Drawing.Point(47, 29);
            this.combMethods.Name = "combMethods";
            this.combMethods.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combMethods.Properties.Items.AddRange(new object[] {
            "创建新的选择集",
            "添加到当前选择集",
            "从当前选择集移除",
            "在当前选择集中选择"});
            this.combMethods.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.combMethods.Size = new System.Drawing.Size(286, 21);
            this.combMethods.TabIndex = 2;
            this.combMethods.SelectedIndexChanged += new System.EventHandler(this.combMethods_SelectedIndexChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 32);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 53;
            this.labelControl2.Text = "方法：";
            // 
            // txtExpression
            // 
            this.txtExpression.Location = new System.Drawing.Point(5, 178);
            this.txtExpression.Multiline = true;
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(327, 75);
            this.txtExpression.TabIndex = 52;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(138, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(59, 23);
            this.btnClear.TabIndex = 51;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnUniqueValue
            // 
            this.btnUniqueValue.Location = new System.Drawing.Point(245, 110);
            this.btnUniqueValue.Name = "btnUniqueValue";
            this.btnUniqueValue.Size = new System.Drawing.Size(74, 23);
            this.btnUniqueValue.TabIndex = 50;
            this.btnUniqueValue.Text = "获取唯一值";
            this.btnUniqueValue.Click += new System.EventHandler(this.btnUniqueValue_Click);
            // 
            // listboxValues
            // 
            this.listboxValues.Location = new System.Drawing.Point(245, 0);
            this.listboxValues.Name = "listboxValues";
            this.listboxValues.Size = new System.Drawing.Size(87, 104);
            this.listboxValues.TabIndex = 49;
            this.listboxValues.DoubleClick += new System.EventHandler(this.listboxValues_DoubleClick);
            // 
            // btn11
            // 
            this.btn11.Location = new System.Drawing.Point(143, 92);
            this.btn11.Name = "btn11";
            this.btn11.Size = new System.Drawing.Size(20, 23);
            this.btn11.TabIndex = 48;
            this.btn11.Text = "%";
            this.btn11.Click += new System.EventHandler(this.btn11_Click);
            // 
            // btn10
            // 
            this.btn10.Location = new System.Drawing.Point(125, 92);
            this.btn10.Name = "btn10";
            this.btn10.Size = new System.Drawing.Size(18, 23);
            this.btn10.TabIndex = 47;
            this.btn10.Text = "_";
            this.btn10.Click += new System.EventHandler(this.btn10_Click);
            // 
            // lblLayer
            // 
            this.lblLayer.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLayer.Appearance.Options.UseFont = true;
            this.lblLayer.Location = new System.Drawing.Point(5, 158);
            this.lblLayer.Name = "lblLayer";
            this.lblLayer.Size = new System.Drawing.Size(63, 14);
            this.lblLayer.TabIndex = 46;
            this.lblLayer.Text = "where语句:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(208, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(59, 23);
            this.btnOK.TabIndex = 45;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btn12
            // 
            this.btn12.Location = new System.Drawing.Point(165, 92);
            this.btn12.Name = "btn12";
            this.btn12.Size = new System.Drawing.Size(34, 23);
            this.btn12.TabIndex = 44;
            this.btn12.Text = "()";
            this.btn12.Click += new System.EventHandler(this.btn12_Click);
            // 
            // btn13
            // 
            this.btn13.Location = new System.Drawing.Point(205, 92);
            this.btn13.Name = "btn13";
            this.btn13.Size = new System.Drawing.Size(34, 23);
            this.btn13.TabIndex = 43;
            this.btn13.Text = "Not";
            this.btn13.Click += new System.EventHandler(this.btn13_Click);
            // 
            // btn9
            // 
            this.btn9.Location = new System.Drawing.Point(205, 63);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(34, 23);
            this.btn9.TabIndex = 42;
            this.btn9.Text = "Or";
            this.btn9.Click += new System.EventHandler(this.btn9_Click);
            // 
            // btn6
            // 
            this.btn6.Location = new System.Drawing.Point(205, 34);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(34, 23);
            this.btn6.TabIndex = 41;
            this.btn6.Text = "And";
            this.btn6.Click += new System.EventHandler(this.btn6_Click);
            // 
            // btn3
            // 
            this.btn3.Location = new System.Drawing.Point(205, 5);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(34, 23);
            this.btn3.TabIndex = 40;
            this.btn3.Text = "Like";
            this.btn3.Click += new System.EventHandler(this.btn3_Click);
            // 
            // btn8
            // 
            this.btn8.Location = new System.Drawing.Point(164, 63);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(34, 23);
            this.btn8.TabIndex = 39;
            this.btn8.Text = "<=";
            this.btn8.Click += new System.EventHandler(this.btn8_Click);
            // 
            // btn7
            // 
            this.btn7.Location = new System.Drawing.Point(125, 63);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(34, 23);
            this.btn7.TabIndex = 38;
            this.btn7.Text = "<";
            this.btn7.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn5
            // 
            this.btn5.Location = new System.Drawing.Point(163, 34);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(34, 23);
            this.btn5.TabIndex = 37;
            this.btn5.Text = ">=";
            this.btn5.Click += new System.EventHandler(this.btn5_Click);
            // 
            // btn4
            // 
            this.btn4.Location = new System.Drawing.Point(125, 34);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(34, 23);
            this.btn4.TabIndex = 36;
            this.btn4.Text = ">";
            this.btn4.Click += new System.EventHandler(this.btn4_Click);
            // 
            // btn2
            // 
            this.btn2.Location = new System.Drawing.Point(165, 5);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(34, 23);
            this.btn2.TabIndex = 35;
            this.btn2.Text = "<>";
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // btn1
            // 
            this.btn1.Location = new System.Drawing.Point(125, 5);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(34, 23);
            this.btn1.TabIndex = 34;
            this.btn1.Text = "=";
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 32;
            this.labelControl1.Text = "图层：";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(273, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(59, 23);
            this.btnCancel.TabIndex = 31;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // combLayers
            // 
            this.combLayers.Location = new System.Drawing.Point(47, 2);
            this.combLayers.Name = "combLayers";
            this.combLayers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combLayers.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.combLayers.Size = new System.Drawing.Size(285, 21);
            this.combLayers.TabIndex = 30;
            this.combLayers.SelectedIndexChanged += new System.EventHandler(this.combLayers_SelectedIndexChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.combLayers);
            this.panelControl1.Controls.Add(this.combMethods);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Location = new System.Drawing.Point(8, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(338, 56);
            this.panelControl1.TabIndex = 56;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.listboxFields);
            this.panelControl2.Controls.Add(this.btn1);
            this.panelControl2.Controls.Add(this.btn2);
            this.panelControl2.Controls.Add(this.txtExpression);
            this.panelControl2.Controls.Add(this.lblLayer);
            this.panelControl2.Controls.Add(this.btn3);
            this.panelControl2.Controls.Add(this.btn4);
            this.panelControl2.Controls.Add(this.btn11);
            this.panelControl2.Controls.Add(this.btn10);
            this.panelControl2.Controls.Add(this.btn5);
            this.panelControl2.Controls.Add(this.btn8);
            this.panelControl2.Controls.Add(this.btn12);
            this.panelControl2.Controls.Add(this.btn6);
            this.panelControl2.Controls.Add(this.btnUniqueValue);
            this.panelControl2.Controls.Add(this.listboxValues);
            this.panelControl2.Controls.Add(this.btn13);
            this.panelControl2.Controls.Add(this.btn9);
            this.panelControl2.Controls.Add(this.btn7);
            this.panelControl2.Location = new System.Drawing.Point(8, 61);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(337, 258);
            this.panelControl2.TabIndex = 57;
            // 
            // listboxFields
            // 
            this.listboxFields.Location = new System.Drawing.Point(5, 11);
            this.listboxFields.Name = "listboxFields";
            this.listboxFields.Size = new System.Drawing.Size(114, 122);
            this.listboxFields.TabIndex = 59;
            this.listboxFields.DoubleClick += new System.EventHandler(this.listboxFields_DoubleClick);
            this.listboxFields.SelectedIndexChanged += new System.EventHandler(this.listboxFields_SelectedIndexChanged);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btnClear);
            this.panelControl3.Controls.Add(this.btnCancel);
            this.panelControl3.Controls.Add(this.btnOK);
            this.panelControl3.Location = new System.Drawing.Point(8, 325);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(337, 32);
            this.panelControl3.TabIndex = 58;
            // 
            // UCSQLSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.lblNums);
            this.Name = "UCSQLSearch";
            this.Size = new System.Drawing.Size(358, 363);
            ((System.ComponentModel.ISupportInitialize)(this.combMethods.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listboxValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.combLayers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listboxFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblNums;
        private DevExpress.XtraEditors.ComboBoxEdit combMethods;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.TextBox txtExpression;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnUniqueValue;
        private DevExpress.XtraEditors.ListBoxControl listboxValues;
        private DevExpress.XtraEditors.SimpleButton btn11;
        private DevExpress.XtraEditors.SimpleButton btn10;
        private DevExpress.XtraEditors.LabelControl lblLayer;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btn12;
        private DevExpress.XtraEditors.SimpleButton btn13;
        private DevExpress.XtraEditors.SimpleButton btn9;
        private DevExpress.XtraEditors.SimpleButton btn6;
        private DevExpress.XtraEditors.SimpleButton btn3;
        private DevExpress.XtraEditors.SimpleButton btn8;
        private DevExpress.XtraEditors.SimpleButton btn7;
        private DevExpress.XtraEditors.SimpleButton btn5;
        private DevExpress.XtraEditors.SimpleButton btn4;
        private DevExpress.XtraEditors.SimpleButton btn2;
        private DevExpress.XtraEditors.SimpleButton btn1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.ComboBoxEdit combLayers;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.ListBoxControl listboxFields;
    }
}
