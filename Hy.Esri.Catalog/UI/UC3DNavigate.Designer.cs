namespace ThreeDimenDataManage.UI
{
    partial class UC3DNavigate
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC3DNavigate));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.te3Dwindow = new AxTerraExplorerX.AxTE3DWindow();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te3Dwindow)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem2,
            this.barSubItem1,
            this.barButtonItem1,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barButtonItem5,
            this.barButtonItem6,
            this.barButtonItem7,
            this.barButtonItem8});
            this.barManager1.MaxItemId = 12;
            this.barManager1.MdiMenuMergeStyle = DevExpress.XtraBars.BarMdiMenuMergeStyle.Always;
            // 
            // bar1
            // 
            this.bar1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.bar1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.bar1.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.bar1.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.bar1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.bar1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.bar1.BarName = "Tools";
            this.bar1.CanDockStyle = ((DevExpress.XtraBars.BarCanDockStyle)((((((DevExpress.XtraBars.BarCanDockStyle.Floating | DevExpress.XtraBars.BarCanDockStyle.Left)
                        | DevExpress.XtraBars.BarCanDockStyle.Top)
                        | DevExpress.XtraBars.BarCanDockStyle.Right)
                        | DevExpress.XtraBars.BarCanDockStyle.Bottom)
                        | DevExpress.XtraBars.BarCanDockStyle.Standalone)));
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem7)});
            this.bar1.OptionsBar.BarState = DevExpress.XtraBars.BarState.Expanded;
            this.bar1.Text = "Tools";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.barButtonItem2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem2.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem2.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Default;
            this.barButtonItem2.Caption = "打开工程";
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.barButtonItem2.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.Tag = "ThreeDimenDataManage.Command.TE.CommandLoadProject";
            this.barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            // 
            // barSubItem1
            // 
            this.barSubItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.barSubItem1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barSubItem1.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barSubItem1.Caption = "缩放";
            this.barSubItem1.Id = 4;
            this.barSubItem1.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem8),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem6)});
            this.barSubItem1.MenuAppearance.Menu.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barSubItem1.MenuAppearance.Menu.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barSubItem1.MenuAppearance.Menu.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barSubItem1.MenuAppearance.Menu.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barSubItem1.MenuAppearance.Menu.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barSubItem1.MenuAppearance.Menu.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barSubItem1.MenuAppearance.MenuBar.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barSubItem1.MenuAppearance.MenuBar.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barSubItem1.MenuAppearance.MenuBar.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barSubItem1.MenuAppearance.MenuBar.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barSubItem1.MenuAppearance.MenuBar.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barSubItem1.MenuAppearance.MenuBar.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barSubItem1.MenuAppearance.MenuCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barSubItem1.MenuAppearance.MenuCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barSubItem1.MenuAppearance.MenuCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barSubItem1.MenuAppearance.MenuCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barSubItem1.MenuAppearance.MenuCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barSubItem1.MenuAppearance.MenuCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barSubItem1.MenuAppearance.SideStrip.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barSubItem1.MenuAppearance.SideStrip.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barSubItem1.MenuAppearance.SideStrip.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barSubItem1.MenuAppearance.SideStrip.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barSubItem1.MenuAppearance.SideStrip.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barSubItem1.MenuAppearance.SideStrip.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barSubItem1.MenuAppearance.SideStripNonRecent.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barSubItem1.MenuAppearance.SideStripNonRecent.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barSubItem1.MenuAppearance.SideStripNonRecent.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barSubItem1.MenuAppearance.SideStripNonRecent.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barSubItem1.MenuAppearance.SideStripNonRecent.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barSubItem1.MenuAppearance.SideStripNonRecent.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barSubItem1.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.Default;
            this.barSubItem1.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.barSubItem1.Name = "barSubItem1";
            this.barSubItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.barButtonItem1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem1.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Default;
            this.barButtonItem1.Caption = "全球";
            this.barButtonItem1.Id = 5;
            this.barButtonItem1.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.barButtonItem1.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.Tag = "ThreeDimenDataManage.Command.TE.CommandViewGlobe";
            this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.barButtonItem3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem3.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem3.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Default;
            this.barButtonItem3.Caption = "省级";
            this.barButtonItem3.Id = 6;
            this.barButtonItem3.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.barButtonItem3.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.Tag = "ThreeDimenDataManage.Command.TE.CommandViewState";
            this.barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.barButtonItem4.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem4.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem4.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Default;
            this.barButtonItem4.Caption = "城市";
            this.barButtonItem4.Id = 7;
            this.barButtonItem4.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.barButtonItem4.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.Tag = "ThreeDimenDataManage.Command.TE.CommandViewCity";
            this.barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.barButtonItem5.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem5.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem5.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Default;
            this.barButtonItem5.Caption = "街道";
            this.barButtonItem5.Id = 8;
            this.barButtonItem5.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.barButtonItem5.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.Tag = "ThreeDimenDataManage.Command.TE.CommandViewStreet";
            this.barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.barButtonItem6.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem6.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem6.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Default;
            this.barButtonItem6.Caption = "房屋";
            this.barButtonItem6.Id = 9;
            this.barButtonItem6.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.barButtonItem6.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.barButtonItem6.Name = "barButtonItem6";
            this.barButtonItem6.Tag = "ThreeDimenDataManage.Command.TE.CommandViewHouse";
            this.barButtonItem6.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.barButtonItem7.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem7.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem7.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Default;
            this.barButtonItem7.Caption = "加载Shp图层";
            this.barButtonItem7.Id = 10;
            this.barButtonItem7.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.barButtonItem7.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.barButtonItem7.Name = "barButtonItem7";
            this.barButtonItem7.Tag = "ThreeDimenDataManage.Command.TE.CommandAddShpLayer";
            this.barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barDockControlTop.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barDockControlTop.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barDockControlTop.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barDockControlTop.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barDockControlTop.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(567, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barDockControlBottom.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barDockControlBottom.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barDockControlBottom.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barDockControlBottom.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barDockControlBottom.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 383);
            this.barDockControlBottom.Size = new System.Drawing.Size(567, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barDockControlLeft.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barDockControlLeft.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barDockControlLeft.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barDockControlLeft.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barDockControlLeft.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 359);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barDockControlRight.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barDockControlRight.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barDockControlRight.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barDockControlRight.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barDockControlRight.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(567, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 359);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog1";
            // 
            // te3Dwindow
            // 
            this.te3Dwindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.te3Dwindow.Enabled = true;
            this.te3Dwindow.Location = new System.Drawing.Point(0, 24);
            this.te3Dwindow.Name = "te3Dwindow";
            this.te3Dwindow.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("te3Dwindow.OcxState")));
            this.te3Dwindow.Size = new System.Drawing.Size(567, 359);
            this.te3Dwindow.TabIndex = 11;
            // 
            // barButtonItem8
            // 
            this.barButtonItem8.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.barButtonItem8.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem8.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem8.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Default;
            this.barButtonItem8.Caption = "国家";
            this.barButtonItem8.Id = 11;
            this.barButtonItem8.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.barButtonItem8.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.barButtonItem8.Name = "barButtonItem8";
            this.barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            // 
            // UC3DNavigate
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.te3Dwindow);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.Name = "UC3DNavigate";
            this.Size = new System.Drawing.Size(567, 383);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te3Dwindow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private AxTerraExplorerX.AxTE3DWindow te3Dwindow;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
    }
}
