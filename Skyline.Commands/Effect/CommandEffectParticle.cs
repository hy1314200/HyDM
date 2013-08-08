using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Define;
using DevExpress.XtraBars;
using Skyline.Core;
using System.Windows.Forms;
using TerraExplorerX;

namespace Skyline.Commands
{
    public class CommandEffectParticle:Skyline.Define.SkylineBaseCommand,ICommandEx
    {
        public CommandEffectParticle()
        {
            this.m_Category = "三维特效";
            this.m_Caption = "粒子效果";

            this.m_Message = "添加粒子效果";
            this.m_Tooltip = "从下拉中选择一个缩放级别";

            AddSubItem("火焰", "Fire", "火焰");
            AddSubItem("喷泉", "Fountain", "喷泉");
            AddSubItem("烟花", "Fireworks", "烟花");
            AddSubItem("烟雾", "Smoke", "烟雾");
            AddSubItem("爆炸", "Boom", "爆炸");

        }

        private void AddSubItem(string caption, string gifName, string strPrefix)
        {
            BarButtonItem barItem = new BarButtonItem();
            barItem.Caption = caption;
            barItem.ItemClick += delegate
            {
                this.m_SkylineHook.TerraExplorer.OnLButtonDown += new TerraExplorerX._ITerraExplorerEvents5_OnLButtonDownEventHandler(TerraExplorer_OnLButtonDown);
                this.m_GifName = gifName;
                this.m_Prefix = strPrefix;
            };
            m_Control.AddItem(barItem);
        }

        private string m_GifName;
        private string m_Prefix;
        void TerraExplorer_OnLButtonDown(int Flags, int X, int Y, ref object pbHandled)
        {
            try
            {
                IPosition61 _Position6 = this.m_SkylineHook.SGWorld.Window.PixelToWorld(X, Y, WorldPointType.WPT_ALL).Position;
                int GroupID = this.m_SkylineHook.SGWorld.ProjectTree.FindItem("粒子特效");
                if (GroupID == 0)
                {
                    GroupID = this.m_SkylineHook.SGWorld.ProjectTree.CreateGroup("粒子特效", 0);
                }
                this.m_SkylineHook.SGWorld.Creator.CreateImageLabel(_Position6, Application.StartupPath + "\\Particle\\"+m_GifName+".gif", null, GroupID, m_Prefix + System.Guid.NewGuid().ToString().Substring(0, 6));
            }
            catch
            {
            }
            finally
            {
                this.m_SkylineHook.TerraExplorer.OnLButtonDown -= new TerraExplorerX._ITerraExplorerEvents5_OnLButtonDownEventHandler(TerraExplorer_OnLButtonDown);
            }
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
        public override void OnCreate(object Hook)
        {
            base.OnCreate(Hook);
        }


        private BarSubItem m_Control=new BarSubItem();
        public object ExControl
        {
            get { return m_Control; }
        }
    }
}
