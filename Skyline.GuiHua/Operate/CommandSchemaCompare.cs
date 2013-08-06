using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.GuiHua.Bussiness;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using TerraExplorerX;
using Define;

namespace Skyline.GuiHua.Operate
{
    public class CommandSchemaCompare:GuiHuaBaseCommand,ITool
    {
        public CommandSchemaCompare()
        {
            this.m_Caption = "方案对比";

            this.m_Message = "方案对比";
            this.m_Tooltip = "以多窗口的形式在场景中对比项目中不同方案";
        }

        private UCSchemaCompare m_UcCompare;
        private bool m_Flag = true;


        TerraExplorerX.SGWorld61 teTopRight;
        TerraExplorerX.SGWorld61 teBottomLeft;
        TerraExplorerX.SGWorld61 teBottomRight;
        public override void OnClick()
        {
            m_Flag = !m_Flag;
            if (m_Flag)
            {
                m_UcCompare.FinishCompare();
                m_UcCompare.ControlMode = enum3DControlMode.One;
            }
            else
            {
                if (m_UcCompare == null )
                {
                    Control parent=m_SkylineHook.Window.Parent;
                    parent.Controls.Remove(m_SkylineHook.Window);
                    m_UcCompare = new UCSchemaCompare(m_SkylineHook.Window as AxTerraExplorerX.AxTE3DWindow);
                    m_UcCompare.CreateHooker(out Program.sgworld, out teTopRight, out teBottomLeft, out teBottomRight);
                    parent.Controls.Add(m_UcCompare);
                    m_UcCompare.Dock = DockStyle.Fill;
                }
                
                if (Bussiness.Environment. m_Project.Schemas == null)
                    return;

                // 加载fly
                string flyURL = Bussiness.Environment.m_Project.File;
                string flyURL1 = null, flyURL2 = null, flyURL3 = null;
                string urlTopRight = null, urlBottomLeft = null, urlBottomRight = null;

                enum3DControlMode modeCurrent = enum3DControlMode.One;
                // 设置屏模式
                if (Bussiness.Environment.m_Project.Schemas.Count > 0 && Bussiness.Environment.m_Project.Schemas.Count < 4)
                {
                    modeCurrent = (enum3DControlMode)(Bussiness.Environment.m_Project.Schemas.Count);
                    if (modeCurrent == enum3DControlMode.Tow)
                    {
                        flyURL2 = flyURL;
                        urlBottomLeft = Bussiness.Environment.m_Project.Schemas[0].File;
                    }
                    if (modeCurrent == enum3DControlMode.Three)
                    {
                        flyURL1 = flyURL;
                        flyURL3 = flyURL;
                        urlTopRight = Bussiness.Environment.m_Project.Schemas[0].File;
                        urlBottomRight = Bussiness.Environment.m_Project.Schemas[1].File;
                    }
                    if (modeCurrent == enum3DControlMode.Four)
                    {
                        urlTopRight = Bussiness.Environment.m_Project.Schemas[0].File;
                        urlBottomLeft = Bussiness.Environment.m_Project.Schemas[1].File;
                        urlBottomRight = Bussiness.Environment.m_Project.Schemas[2].File;
                        flyURL1 = flyURL;
                        flyURL2 = flyURL;
                        flyURL3 = flyURL;
                    }
                }
                m_UcCompare.CompareSchema(flyURL1, flyURL2, flyURL3);


                try
                {

                    // 修改模型shp
                    string strLabel = ConfigurationManager.AppSettings["LayerPath"];
                    int GroupID = -1;
                    ILayer61 lyrModel = null;
                    if (!string.IsNullOrWhiteSpace(urlTopRight))
                    {
                        GroupID = teTopRight.ProjectTree.FindItem(strLabel);
                        if (GroupID > 0)
                        {
                            lyrModel = teTopRight.ProjectTree.GetLayer(GroupID);
                            lyrModel.DataSourceInfo.ConnectionString = string.Format("FileName={0};TEPlugName=OGR;", urlTopRight);
                            lyrModel.Save();
                            lyrModel.Refresh();
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(urlBottomLeft))
                    {
                        GroupID = teBottomLeft.ProjectTree.FindItem(strLabel);
                        if (GroupID > 0)
                        {
                            lyrModel = teBottomLeft.ProjectTree.GetLayer(GroupID);
                            lyrModel.DataSourceInfo.ConnectionString = string.Format("FileName={0};TEPlugName=OGR;", urlBottomLeft);
                            lyrModel.Save();
                            lyrModel.Refresh();
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(urlBottomRight))
                    {
                        GroupID = teBottomRight.ProjectTree.FindItem(strLabel);
                        if (GroupID > 0)
                        {
                            lyrModel = teBottomRight.ProjectTree.GetLayer(GroupID);
                            lyrModel.DataSourceInfo.ConnectionString = string.Format("FileName={0};TEPlugName=OGR;", urlBottomRight);
                            lyrModel.Save();
                            lyrModel.Refresh();
                        }
                    }

                    m_UcCompare.ControlMode = modeCurrent;
                }
                catch
                {
                    MessageBox.Show("抱歉，出错了，很可能的原因是方案模型文件不存在或移动了");
                }

            }
        }

        public override bool Checked
        {
            get
            {
                return !m_Flag;
            }
        }

        public object Resource
        {
            get { return -1; }
        }

        public bool Release()
        {
            return true;
        }
    }
}
