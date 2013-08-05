using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;
using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandQueryBoth:Skyline.Define.SkylineBaseCommand
    {
        public CommandQueryBoth()
        {
            this.m_Category = "三维查询";
            this.m_Caption = "图属互查";

            this.m_Message = "图属互查";
            this.m_Tooltip = "从模型查询属性信息或从属性查询场景中的模型";
        }

        public override void OnClick()
        {
            try
            {
                FrmAttributeMapQuery frmAttMapQuery = new FrmAttributeMapQuery(base.m_Hook.UIHook.MainForm);
                base.m_Hook.UIHook.MainForm.AddOwnedForm(frmAttMapQuery);
                frmAttMapQuery.Show();
            }
            catch
            {
                MessageBox.Show("发生错误!");
            }
        }
    }
}
