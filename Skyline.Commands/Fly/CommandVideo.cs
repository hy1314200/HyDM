using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;
using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandVideo:Skyline.Define.SkylineBaseCommand
    {
        public CommandVideo()
        {
            this.m_Category = "飞行浏览";
            this.m_Caption = "视频创建";

            this.m_Message = "视频创建";
            this.m_Tooltip = "视频创建";
        }

        public override void OnClick()
        {
            try
            {

                //先判断是否已创建
                int GroupID = m_SkylineHook.SGWorld.ProjectTree.FindItem(@"Video\temp");
                if (GroupID == 0)
                {
                    GroupID = m_SkylineHook.SGWorld.ProjectTree.CreateGroup("Video", 0);
                    m_SkylineHook.SGWorld.Creator.CreatePresentation(GroupID, "temp");
                    try
                    {
                        FrmVideoCreate myVideoCreate = new FrmVideoCreate(this.m_Hook.UIHook.MainForm);
                        if (!myVideoCreate.IsDisposed)
                        {
                            this.m_Hook.UIHook.MainForm.AddOwnedForm(myVideoCreate);
                            myVideoCreate.Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        //  被释放
                    }
                }
                else
                    MessageBox.Show("创建视频已开始!");
            }
            catch
            {
                MessageBox.Show("发生错误!");
            }
        }
    }
}
