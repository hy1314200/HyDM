using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Define;
using DevExpress.XtraBars;
using Skyline.Core;

namespace Skyline.Commands
{
    public class CommandViewZoom:SkylineBaseCommand,ICommandEx
    {
        public CommandViewZoom()
        {
            this.m_Category = "三维浏览";
            this.m_Caption = "缩放";

            this.m_Message = "缩放";
            this.m_Tooltip = "从下拉中选择一个缩放级别";

            AddSubItem("地球", InvokeNumber.Globe);
            AddSubItem("省", InvokeNumber.State);
            AddSubItem("市", InvokeNumber.City);
            AddSubItem("街道", InvokeNumber.Street);
            AddSubItem("房屋", InvokeNumber.House);

        }

        private void AddSubItem(string caption, InvokeNumber num)
        {
            BarButtonItem barItem = new BarButtonItem();
            barItem.Caption =caption;
            barItem.ItemClick += delegate
            {
                this.m_SkylineHook.TerraExplorer.Invoke((int)num);
            };
            m_Control.AddItem(barItem);
        }

        public override void OnClick()
        {
        }
        public override bool Enabled
        {
            get
            {
                return base.Enabled;
            }
        }

        private BarSubItem m_Control=new BarSubItem();
        public object ExControl
        {
            get { return m_Control; }
        }
    }
}
