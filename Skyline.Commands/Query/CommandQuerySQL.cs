using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;
using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandQuerySQL : SkylineBaseCommand
    {
        public CommandQuerySQL()
        {
            this.m_Category = "三维查询";
            this.m_Caption = "SQL查询";

            this.m_Message = "属性查询";
            this.m_Tooltip = "查看场景中模型的属性信息";
        }

        public override void OnClick()
        {
            FrmSearchByAttribute pFrmSearch = new FrmSearchByAttribute(base.m_Hooker.MainForm);
            try
            {
                if (!pFrmSearch.IsDisposed)
                {
                    base.m_Hooker.MainForm.AddOwnedForm(pFrmSearch);
                    pFrmSearch.Show();
                }
            }
            catch
            {

                MessageBox.Show("发生错误!");

            }
        }
    }
}
