using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.GuiHua.Bussiness;
using System.Windows.Forms;
using System.IO;

namespace Skyline.GuiHua.Operate
{
    public class CommandModelBuilding:GuiHuaBaseCommand
    {
        public CommandModelBuilding()
        {
            this.m_Caption = "建筑模型构建";

            this.m_Message = "构建建筑模型";
            this.m_Tooltip = "根据居民地矢量图层生成房屋模型";
        }

        public override bool Enabled
        {
            get
            {
                return (m_SkylineHook != null && m_SkylineHook.SGWorld != null && !string.IsNullOrEmpty(m_SkylineHook.SGWorld.Project.Name));
            }
        }

        public override void OnClick()
        {
            //FrmBatchModeling frmBatch = new SunzSoft.UrbanConstruction.FrmBatchModeling();
            //frmBatch.Show(this);
        }
    }
}
