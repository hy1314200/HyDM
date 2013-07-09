using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;


namespace Common.Operate
{
    public abstract class CommandDockable : BaseCommand, ITool
    {
        public CommandDockable()
        {
            this.m_Category = "可停靠";
            this.m_Caption = "停靠面版";
            this.m_Message = string.Format("打开{0}",this.Caption);
            this.m_Tooltip = string.Format("点击以打开或关闭{0}", this.Caption); 
        }

        protected Control m_Control;
        protected Control m_DockPanel;

        protected virtual string DockCaption { get { return this.Caption; } }
        protected abstract Control CreateControl();
        protected abstract enumDockPosition DockPosition { get; }
        protected abstract void Init();


        public override void OnClick()
        {
            if (this.m_Control != null && this.m_Control.Visible)
            {
                this.m_DockPanel.Hide();
                if (m_DockPanel is DockPanel)
                    (m_DockPanel as DockPanel).Hide();
            }
            else
            {
                if (this.m_Control == null)
                {
                    this.m_Control = this.CreateControl();
                    m_DockPanel = m_Hook.UIHook.AddControl(m_Control, this.DockPosition);
                    m_DockPanel.Text = this.DockCaption;
                    this.Init();
                }
                m_DockPanel.Show();
                if (m_DockPanel is DockPanel)
                    (m_DockPanel as DockPanel).Show();
            }
            
            //IUIHook uiHook = m_Hook as IUIHook;
            //if (uiHook != null && uiHook.LeftDockPanel != null)
            //{
            //    bool panelCreateFlag = (m_DockPanel == null);
            //    if (panelCreateFlag)
            //    {
            //        DockPanel dockContianer = uiHook.LeftDockPanel as DockPanel;
            //        if (this.DockPosition == enumDockPosition.Right)
            //        {
            //            dockContianer = uiHook.RightDockPanel as DockPanel;
            //        }
            //        if (this.DockPosition == enumDockPosition.Bottom)
            //        {
            //            dockContianer = uiHook.BottomDockPanel as DockPanel;
            //        }
            //        m_DockPanel = dockContianer.AddPanel();
            //        m_DockPanel.Text = this.DockCaption;
            //        //m_DockPanel.Controls.Add(dockLeft.Container as Control);
            //        //m_DockPanel.DockTo(dockLeft);             
            //    }
            //    if (m_DockPanel.Visibility == DockVisibility.Visible && m_Control != null && m_Control.Visible)
            //    {
            //        m_DockPanel.Visibility = DockVisibility.Hidden;
            //    }
            //    else
            //    {
            //        if (m_Control == null)
            //        {
            //            m_Control = this.CreateControl();

            //            if (m_Control is System.ComponentModel.ISupportInitialize)
            //                ((System.ComponentModel.ISupportInitialize)(m_Control)).BeginInit();

            //            m_DockPanel.Controls.Add(m_Control);
            //            m_Control.Dock = System.Windows.Forms.DockStyle.Fill;

            //            if (m_Control is System.ComponentModel.ISupportInitialize)
            //                ((System.ComponentModel.ISupportInitialize)(m_Control)).EndInit();
            //        }
            //        else
            //        {
            //            m_DockPanel.Controls.Add(m_Control);
            //            m_Control.Dock = System.Windows.Forms.DockStyle.Fill;
            //        }

            //        this.Init();

            //        m_DockPanel.Visibility = DockVisibility.Visible;
            //        if (panelCreateFlag)
            //        {
            //            m_DockPanel.Visibility = DockVisibility.Hidden;
            //            m_DockPanel.Visibility = DockVisibility.Visible;
            //        }                
            //        //m_DockPanel.BringToFront();
            //        m_DockPanel.Show();
            //    }
            //}
        }

        public override bool Enabled
        {
            get
            {
                return true;
            }
        }

        public override bool Checked
        {
            get
            {
                return m_Control!=null && m_Control.Visible;
            }
        }

        public override string Message
        {
            get
            {
                this.m_Message = string.Format(this.Checked ? "关闭{0}" : "打开{0}", Caption);
                return base.Message;
            }
        }

        public object Resource
        {
            get { return m_Control; }
        }

        public bool Release()
        {
            return true;
        }
    }
}
