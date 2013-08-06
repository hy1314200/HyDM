using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Skyline.GuiHua.Bussiness
{

    public partial class UCSchemaCompare : DevExpress.XtraEditors.XtraUserControl
    {
        private AxTerraExplorerX.AxTE3DWindow teTopLeft;
        public UCSchemaCompare(AxTerraExplorerX.AxTE3DWindow teTL)
        {
            this.teTopLeft = teTL;
            this.splitLeft.Panel1.Controls.Add(teTL);
            InitializeComponent();
        }

        public enum3DControlMode ControlMode
        {
            set
            {
                switch (value)
                {
                    case enum3DControlMode.One:
                        splitAll.PanelVisibility = SplitPanelVisibility.Panel1;
                        splitLeft.PanelVisibility = SplitPanelVisibility.Panel1;
                        break;

                    case enum3DControlMode.Tow:
                        splitAll.PanelVisibility = SplitPanelVisibility.Panel1;
                        splitLeft.PanelVisibility = SplitPanelVisibility.Both;
                        splitLeft.Horizontal = true;
                        break;

                    case enum3DControlMode.Three:
                        splitAll.PanelVisibility = SplitPanelVisibility.Both;
                        splitLeft.PanelVisibility = SplitPanelVisibility.Panel1;
                        //splitLeft.Horizontal = false;
                        splitRight.PanelVisibility = SplitPanelVisibility.Both;
                        break;

                    case enum3DControlMode.Four:
                        splitAll.PanelVisibility = SplitPanelVisibility.Both;
                        splitLeft.PanelVisibility = SplitPanelVisibility.Both;
                        splitLeft.Horizontal = false;
                        splitRight.PanelVisibility = SplitPanelVisibility.Both;
                        break;

                }
            }
        }
        
        private TerraExplorerX.SGWorld61 m_sgwTopLeft;
        private TerraExplorerX.SGWorld61 m_sgwTopRight;
        private TerraExplorerX.SGWorld61 m_sgwBottomLeft;
        private TerraExplorerX.SGWorld61 m_sgwBottomRight;

        private bool m_FlagTR = false, m_FlagBL = false, m_FlagBR = false;

        public void CreateHooker(out TerraExplorerX.SGWorld61 sgwTopLeft, out TerraExplorerX.SGWorld61 sgwTopRight, out TerraExplorerX.SGWorld61 sgwBottomLeft, out TerraExplorerX.SGWorld61 sgwBottomRight)
        {
            if (m_sgwTopLeft == null)
            {
                m_sgwTopLeft = new TerraExplorerX.SGWorld61Class();
                m_sgwTopRight = this.teTopRight.CreateInstance("TerraExplorerX.SGWorld61") as TerraExplorerX.SGWorld61;
                m_sgwBottomLeft = this.teBottomLeft.CreateInstance("TerraExplorerX.SGWorld61") as TerraExplorerX.SGWorld61;
                m_sgwBottomRight = this.teBottomRight.CreateInstance("TerraExplorerX.SGWorld61") as TerraExplorerX.SGWorld61;
            }
            sgwTopLeft = m_sgwTopLeft;
            sgwTopRight = m_sgwTopRight;
            sgwBottomLeft = m_sgwBottomLeft;
            sgwBottomRight = m_sgwBottomRight;

        }

        public void CompareSchema(string flyTopRight, string flyBottomLeft, string flyBottomRight)
        {
            m_sgwTopLeft.Application.Multiple3DWindows.SetAsLeader();

            if (!string.IsNullOrWhiteSpace(flyTopRight))
            {
                m_sgwTopRight.Open(flyTopRight);
                m_sgwTopRight.Application.Multiple3DWindows.LinkPosition(m_sgwTopLeft);

                m_FlagTR = true;
            }
            if (!string.IsNullOrWhiteSpace(flyBottomLeft))
            {
                m_sgwBottomLeft.Open(flyBottomLeft);
                m_sgwBottomLeft.Application.Multiple3DWindows.LinkPosition(m_sgwTopLeft);

                m_FlagBL = true;
            }

            if (!string.IsNullOrWhiteSpace(flyBottomRight))
            {
                m_sgwBottomRight.Open(flyBottomRight);
                m_sgwBottomRight.Application.Multiple3DWindows.LinkPosition(m_sgwTopLeft);

                m_FlagBR = true;
            }

            m_Flag = true;
        }

        public void FinishCompare()
        {
            m_Flag = false;

            if (m_FlagTR)
            {
                m_sgwTopRight.Application.Multiple3DWindows.UnlinkPosition();
            }
            if (m_FlagBL)
            {
                m_sgwBottomLeft.Application.Multiple3DWindows.UnlinkPosition();
            }
            if (m_FlagBR)
            {
                m_sgwBottomRight.Application.Multiple3DWindows.UnlinkPosition();
            }
            m_FlagTR = false;
            m_FlagBL = false;
            m_FlagBR = false;
        }

        private bool m_Flag = false;       

    }

    public enum enum3DControlMode
    {
        One,
        Tow,
        Three,
        Four
    }
}
