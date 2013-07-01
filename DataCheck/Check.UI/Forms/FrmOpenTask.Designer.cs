namespace Check.UI.Forms
{
   partial class FrmOPenTask
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
          DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
          this.lblTaskText = new DevExpress.XtraEditors.LabelControl();
          this.btnCheck = new DevExpress.XtraEditors.SimpleButton();
          this.btnExit = new DevExpress.XtraEditors.SimpleButton();
          this.listBoxTask = new DevExpress.XtraEditors.ListBoxControl();
          this.btnImportToSDE = new DevExpress.XtraEditors.SimpleButton();
          this.btnOpenTask = new DevExpress.XtraEditors.SimpleButton();
          this.simpleButtonExit = new DevExpress.XtraEditors.SimpleButton();
          this.btnDeleteTask = new DevExpress.XtraEditors.SimpleButton();
          this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
          this.gridControlTasks = new DevExpress.XtraGrid.GridControl();
          this.gridViewTasks = new DevExpress.XtraGrid.Views.Grid.GridView();
          this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
          this.btnOpenFromFile = new DevExpress.XtraEditors.SimpleButton();
          this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
          ((System.ComponentModel.ISupportInitialize)(this.listBoxTask)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.gridControlTasks)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.gridViewTasks)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
          this.panelControl1.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
          this.panelControl2.SuspendLayout();
          this.SuspendLayout();
          // 
          // lblTaskText
          // 
          this.lblTaskText.Location = new System.Drawing.Point(7, 7);
          this.lblTaskText.Name = "lblTaskText";
          this.lblTaskText.Size = new System.Drawing.Size(156, 14);
          this.lblTaskText.TabIndex = 1;
          this.lblTaskText.Text = "请选择需要执行检查的任务：";
          // 
          // btnCheck
          // 
          this.btnCheck.Location = new System.Drawing.Point(12, 310);
          this.btnCheck.Name = "btnCheck";
          this.btnCheck.Size = new System.Drawing.Size(76, 23);
          this.btnCheck.TabIndex = 2;
          this.btnCheck.Text = "执行检查";
          // 
          // btnExit
          // 
          this.btnExit.Location = new System.Drawing.Point(249, 310);
          this.btnExit.Name = "btnExit";
          this.btnExit.Size = new System.Drawing.Size(76, 23);
          this.btnExit.TabIndex = 3;
          this.btnExit.Text = "退出";
          // 
          // listBoxTask
          // 
          this.listBoxTask.Location = new System.Drawing.Point(7, 30);
          this.listBoxTask.Name = "listBoxTask";
          this.listBoxTask.Size = new System.Drawing.Size(318, 274);
          this.listBoxTask.TabIndex = 9;
          // 
          // btnImportToSDE
          // 
          this.btnImportToSDE.Location = new System.Drawing.Point(91, 310);
          this.btnImportToSDE.Name = "btnImportToSDE";
          this.btnImportToSDE.Size = new System.Drawing.Size(76, 23);
          this.btnImportToSDE.TabIndex = 11;
          this.btnImportToSDE.Text = "导入SDE";
          // 
          // btnOpenTask
          // 
          this.btnOpenTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.btnOpenTask.Enabled = false;
          this.btnOpenTask.Location = new System.Drawing.Point(385, 8);
          this.btnOpenTask.Name = "btnOpenTask";
          this.btnOpenTask.Size = new System.Drawing.Size(81, 23);
          this.btnOpenTask.TabIndex = 1;
          this.btnOpenTask.Text = "打开任务";
          this.btnOpenTask.Click += new System.EventHandler(this.btnOpenTask_Click);
          // 
          // simpleButtonExit
          // 
          this.simpleButtonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.simpleButtonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
          this.simpleButtonExit.Location = new System.Drawing.Point(585, 8);
          this.simpleButtonExit.Name = "simpleButtonExit";
          this.simpleButtonExit.Size = new System.Drawing.Size(81, 23);
          this.simpleButtonExit.TabIndex = 3;
          this.simpleButtonExit.Text = "关闭";
          this.simpleButtonExit.Click += new System.EventHandler(this.btnExit_Click);
          // 
          // btnDeleteTask
          // 
          this.btnDeleteTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.btnDeleteTask.Enabled = false;
          this.btnDeleteTask.Location = new System.Drawing.Point(485, 8);
          this.btnDeleteTask.Name = "btnDeleteTask";
          this.btnDeleteTask.Size = new System.Drawing.Size(81, 23);
          this.btnDeleteTask.TabIndex = 2;
          this.btnDeleteTask.Text = "删除任务";
          this.btnDeleteTask.Click += new System.EventHandler(this.btnDeleteTask_Click);
          // 
          // gridControlTasks
          // 
          this.gridControlTasks.Dock = System.Windows.Forms.DockStyle.Fill;
          this.gridControlTasks.EmbeddedNavigator.Name = "";
          this.gridControlTasks.Font = new System.Drawing.Font("Tahoma", 9F);
          gridLevelNode1.RelationName = "Level1";
          this.gridControlTasks.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
          this.gridControlTasks.Location = new System.Drawing.Point(2, 2);
          this.gridControlTasks.MainView = this.gridViewTasks;
          this.gridControlTasks.Name = "gridControlTasks";
          this.gridControlTasks.Size = new System.Drawing.Size(674, 308);
          this.gridControlTasks.TabIndex = 0;
          this.gridControlTasks.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTasks});
          this.gridControlTasks.DoubleClick += new System.EventHandler(this.gridControlTasks_DoubleClick);
          // 
          // gridViewTasks
          // 
          this.gridViewTasks.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
          this.gridViewTasks.Appearance.FocusedRow.Options.UseBackColor = true;
          this.gridViewTasks.GridControl = this.gridControlTasks;
          this.gridViewTasks.Name = "gridViewTasks";
          this.gridViewTasks.OptionsBehavior.Editable = false;
          this.gridViewTasks.OptionsCustomization.AllowColumnMoving = false;
          this.gridViewTasks.OptionsCustomization.AllowFilter = false;
          this.gridViewTasks.OptionsCustomization.AllowGroup = false;
          this.gridViewTasks.OptionsMenu.EnableColumnMenu = false;
          this.gridViewTasks.OptionsMenu.EnableFooterMenu = false;
          this.gridViewTasks.OptionsMenu.EnableGroupPanelMenu = false;
          this.gridViewTasks.OptionsView.ShowGroupPanel = false;
          this.gridViewTasks.OptionsView.ShowIndicator = false;
          this.gridViewTasks.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewTasks_FocusedRowChanged);
          // 
          // panelControl1
          // 
          this.panelControl1.Controls.Add(this.btnOpenFromFile);
          this.panelControl1.Controls.Add(this.btnDeleteTask);
          this.panelControl1.Controls.Add(this.simpleButtonExit);
          this.panelControl1.Controls.Add(this.btnOpenTask);
          this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
          this.panelControl1.Location = new System.Drawing.Point(0, 312);
          this.panelControl1.Name = "panelControl1";
          this.panelControl1.Size = new System.Drawing.Size(678, 43);
          this.panelControl1.TabIndex = 30;
          // 
          // btnOpenFromFile
          // 
          this.btnOpenFromFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.btnOpenFromFile.Location = new System.Drawing.Point(268, 8);
          this.btnOpenFromFile.Name = "btnOpenFromFile";
          this.btnOpenFromFile.Size = new System.Drawing.Size(98, 23);
          this.btnOpenFromFile.TabIndex = 5;
          this.btnOpenFromFile.Text = "从文件打开任务";
          this.btnOpenFromFile.Click += new System.EventHandler(this.btnOpenFromFile_Click);
          // 
          // panelControl2
          // 
          this.panelControl2.Controls.Add(this.gridControlTasks);
          this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panelControl2.Location = new System.Drawing.Point(0, 0);
          this.panelControl2.Name = "panelControl2";
          this.panelControl2.Size = new System.Drawing.Size(678, 312);
          this.panelControl2.TabIndex = 31;
          // 
          // FrmOPenTask
          // 
          this.CancelButton = this.simpleButtonExit;
          this.ClientSize = new System.Drawing.Size(678, 355);
          this.Controls.Add(this.panelControl2);
          this.Controls.Add(this.panelControl1);
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "FrmOPenTask";
          this.ShowIcon = false;
          this.ShowInTaskbar = false;
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "打开任务";
          ((System.ComponentModel.ISupportInitialize)(this.listBoxTask)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.gridControlTasks)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.gridViewTasks)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
          this.panelControl1.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
          this.panelControl2.ResumeLayout(false);
          this.ResumeLayout(false);

      }

      #endregion

      private DevExpress.XtraEditors.LabelControl lblTaskText;
      private DevExpress.XtraEditors.SimpleButton btnCheck;
       private DevExpress.XtraEditors.SimpleButton btnExit;
      private DevExpress.XtraEditors.ListBoxControl listBoxTask;
       private DevExpress.XtraEditors.SimpleButton btnImportToSDE;
       private DevExpress.XtraEditors.SimpleButton btnDeleteTask;
       private DevExpress.XtraEditors.SimpleButton simpleButtonExit;
       private DevExpress.XtraEditors.SimpleButton btnOpenTask;
       private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
       private DevExpress.XtraGrid.GridControl gridControlTasks;
       private DevExpress.XtraGrid.Views.Grid.GridView gridViewTasks;
       private DevExpress.XtraEditors.PanelControl panelControl1;
       private DevExpress.XtraEditors.PanelControl panelControl2;
       private DevExpress.XtraEditors.SimpleButton btnOpenFromFile;
   }
}
