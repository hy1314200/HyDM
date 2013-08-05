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
    public partial class UCHoleResult : DevExpress.XtraEditors.XtraUserControl
    {
        public UCHoleResult()
        {
            InitializeComponent();
        }

        public PipeAnalysis AnalysisResult
        {
            set
            {
                lblResult.Text = "请选择一个房屋进行开挖分析";
                lblArea.Visible = false;
                lblAreaTitle.Visible = false;

                if (value == null)
                    return;

                if (value.HasError)
                {
                    lblResult.Text = "分析过程出现了错误！";
                    return;
                }

                if (value.AnalysisResult)
                {
                    lblResult.Text = "◆当前地基下存在管线！";
                }
                else
                {
                    lblResult.Text = "●当前地基下不存在管线。";
                }

                lblAreaTitle.Visible = true;
                lblArea.Visible = true;
                lblArea.Text = string.Format("{0}（平方米）", value.GetArea());
            
            }        
        }
    }
}
