using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;
using Hy.Metadata.UI;

namespace Hy.Metadata.Operate
{
    public  class CommandStandardAddNew : StandardBaseCommand
    {
        public CommandStandardAddNew()
        {
            this.m_Category = "元数据";
            this.m_Caption = "添加元数据标准";
            this.m_Message = "添加元数据标准定义";
        }

        FrmStandardProperty m_FrmAdd;
        public override void OnClick()
        {
            MetaStandard newStandard=new MetaStandard();
            newStandard.Name="新建标准";
            newStandard.Creator = Environment.Application.UserName;
            newStandard.CreateTime = DateTime.Now;

            if (m_FrmAdd == null || m_FrmAdd.IsDisposed)
            {
                m_FrmAdd = new FrmStandardProperty();
                m_FrmAdd.ViewMode = FrmStandardProperty.enumPropertyViewMode.New;
                m_FrmAdd.Text = "新建元数据标准";
            }
            m_FrmAdd.CurrentStandard = newStandard;
            if (m_FrmAdd.ShowDialog(base.m_Hook.UIHook.MainForm) == DialogResult.OK)
            {
                MetaStandardHelper.SaveStandard(m_FrmAdd.CurrentStandard);
            }

            this.m_Manager.Refresh();
        }
    }
}
