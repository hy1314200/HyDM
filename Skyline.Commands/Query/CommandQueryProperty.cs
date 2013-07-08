using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;
using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandQueryProperty:SkylineBaseCommand
    {
        public CommandQueryProperty()
        {
            this.m_Category = "三维查询";
            this.m_Caption = "属性查询";

            this.m_Message = "属性查询";
            this.m_Tooltip = "查看场景中模型的属性信息";
        }

        public override void OnClick()
        {
            FrmQueryObject fqo = new FrmQueryObject(base.m_Hook.MainForm);
            try
            {
                if (!fqo.IsDisposed)
                {
                    base.m_Hook.MainForm.AddOwnedForm(fqo);
                    fqo.Show();
                }
            }
            catch
            {

                MessageBox.Show("发生错误!");

            }
        }
    }
}
