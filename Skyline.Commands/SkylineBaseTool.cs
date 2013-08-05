using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;
using Define;
namespace Skyline.Commands
{
    public abstract class SkylineBaseTool:Skyline.Define.SkylineBaseCommand,ITool
    {
        public SkylineBaseTool()
        {
            this.m_Category = "三维工具命令";
        }

        protected static bool m_Flag = false;
        protected abstract void SetTool();
        public override void OnClick()
        {
            m_Flag = !m_Flag;
        }

        public override bool Checked
        {
            get
            {
                return m_Flag;
            }
        }

        public bool Release()
        {
            return true;
        }

        public object Resource
        {
            get {return m_SkylineHook; }
        }
    }
}
