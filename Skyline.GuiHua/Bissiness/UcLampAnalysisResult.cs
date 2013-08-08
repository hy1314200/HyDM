using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using System.Configuration;

namespace Skyline.GuiHua.Bussiness
{
    public partial class UcLampAnalysisResult : UserControl
    {
        public UcLampAnalysisResult()
        {
            InitializeComponent();
        }

        private string m_TempGroupName = "LampAnalysis";
        private string m_TempAnalysisName = "Analysis";
        private LampAnalysisResult m_Result;
        public LampAnalysisResult AnalysisResult
        {
            set
            {
                m_Result = value;

                // 清空
                spinEdit1.Value = 0;
                spinEdit2.Value = 0;
                spinEdit3.Value = 0;
                spinEdit4.Value = 0;
                spinEdit5.Value = 0;
                this.tlResult.ClearNodes();
                this.txtResult.Text = "";

                if (m_Result == null)
                    return;

                // 获取设置
                LampSetting lampSetting = m_Result.Setting;
                if (lampSetting != null)
                {
                    spinEdit1.Value=(decimal)lampSetting.MostLampHeight;
                    spinEdit2.Value=(decimal)lampSetting.MostCarHeight;
                    spinEdit3.Value =(decimal) lampSetting.LestSetHeight;
                    spinEdit4.Value=(decimal)lampSetting.MostCarLength;
                    spinEdit5.Value=(decimal)lampSetting.MustViewDistance;
                }

                // 结果
                List<LampInfo> lampList = m_Result.LampList;
                int count = m_Result.LampCount;
                TreeListNode curCrossNode = null;
                string curCrossName=null;
                for (int i = 0; i < count; i++)
                {
                    LampInfo lampInfo = lampList[i];
                    string crossName = lampInfo.CrossName;

                    if (curCrossName != crossName)
                    {
                        curCrossNode = this.tlResult.AppendNode(new object[] { crossName, null, null, null, null, null ,null,null,null}, null);
                        curCrossName = crossName;
                    }

                    TreeListNode nodeLamp= this.tlResult.AppendNode(new object[] { null, lampInfo.DirectString, lampInfo.RawOffset, lampInfo.CrossWidth, lampInfo.X, lampInfo.Y, lampInfo.LestHeight, lampInfo.HeightFlag, lampInfo.ViewFlag }, curCrossNode);
                    nodeLamp.Tag = lampInfo.TheLamp;

                }

                this.tlResult.ExpandAll();

                // 日志
                this.txtResult.Text = m_Result.AnalysisLog;

            }
        }

        private TerraExplorerX.ISGWorld61 m_Hook;
        public TerraExplorerX.ISGWorld61 Hook
        {
            set
            {
                m_Hook = value;
            }
        }

        private void tlResult_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.tlResult.FocusedNode == null || this.tlResult.FocusedNode.Tag == null)
                return;

            if (m_Hook == null)
                return;


            string strDirect = tlResult.FocusedNode.GetValue(colDirect) as string;
            int direct = 1;
            switch (strDirect)
            {
                case "南":
                    direct = 2;
                    break;
                case "西":
                    direct = 3;
                    break;
                case "北":
                    direct = 4;
                    break;
            }
            double x = (double)tlResult.FocusedNode.GetValue(colX);
            double y = (double)tlResult.FocusedNode.GetValue(colY);
            double dis = m_Result.Setting.MustViewDistance + (double)tlResult.FocusedNode.GetValue(colCrossWidth);

            // 这个方向正好是信号灯的方向减去90
            double raw = ((double)tlResult.FocusedNode.GetValue(colRaw) + 90 * direct) % 360;
            raw = 360 - raw;    // 平面坐标系中与skyline中的方向不一致

            // 从信号杆算到信号灯
            double offset = Convert.ToDouble(ConfigurationManager.AppSettings["LampOffset"]);
            x = x + offset * Math.Cos(raw * Math.PI / 180);//(360-raw)/180); 
            y = y + offset * Math.Sin(raw * Math.PI / 180);//(360-raw)/180);


            TerraExplorerX.IPosition61 position = m_Hook.Creator.CreatePosition(
                           x,
                           y);
            int tempGroup = this.m_Hook.ProjectTree.FindItem(m_TempGroupName);
            if (tempGroup > -1)
            {
                this.m_Hook.ProjectTree.DeleteItem(tempGroup);
            }
            tempGroup = this.m_Hook.ProjectTree.CreateGroup(m_TempGroupName);
            m_Hook.Creator.CreateImageLabel(position,
                ConfigurationManager.AppSettings["LabelModelFile"], null, tempGroup, "路灯位置");


            // 飞到
            //m_Hook.Navigate.JumpTo(this.tlResult.FocusedNode.Tag);
            //m_Hook.Navigate.ZoomOut(25);
            position = m_Hook.Creator.CreatePosition(
                x,
                y,
                 (double)tlResult.FocusedNode.GetValue(colHeight),
                 TerraExplorerX.AltitudeTypeCode.ATC_TERRAIN_RELATIVE,
                 180-(raw + Convert.ToDouble(ConfigurationManager.AppSettings["RawOffset"])),
                 0,
                 0,
                 (double)tlResult.FocusedNode.GetValue(colCrossWidth) + Convert.ToDouble(ConfigurationManager.AppSettings["NavigateOffset"])
                 );


            m_Hook.Navigate.FlyTo(position);

            // 视域分析 
            if (cbView.Checked)
            {

                //tempGroup = this.m_Hook.ProjectTree.FindItem(m_TempGroupName);
                //if (tempGroup > -1)
                //{
                //    this.m_Hook.ProjectTree.DeleteItem(tempGroup);
                //}
                //tempGroup = this.m_Hook.ProjectTree.CreateGroup(m_TempGroupName);

                position = m_Hook.Creator.CreatePosition(
                x,
                y,
                 (double)tlResult.FocusedNode.GetValue(colHeight),
                 TerraExplorerX.AltitudeTypeCode.ATC_TERRAIN_RELATIVE,
                 raw + Convert.ToDouble(ConfigurationManager.AppSettings["RawOffset"]),
                 0,
                 0,
                 dis
                 );

                m_Hook.Analysis.CreateViewshed(
                    position,
                    50, 1, 1,
                    m_Result.Setting.LestSetHeight,
                    null,
                    null,
                    tempGroup,
                    m_TempAnalysisName
                    );

            }


        }

        private void btnViewLog_Click(object sender, EventArgs e)
        {
            tabResult.SelectedTabPageIndex = (btnViewLog.Checked ? 1 : 0);
        }
    }
}
