using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;
using TerraExplorerX;

namespace Skyline.GuiHua.Bussiness
{
    public partial class FrmLampAnalysis : DevExpress.XtraEditors.XtraForm
    {
        public FrmLampAnalysis(TerraExplorerX.ISGWorld61 hook)
        {
            InitializeComponent();

            this.m_Hook = hook;
        }

        private LampAnalysisResult m_Result = LampAnalysisResult.Instance;

        private TerraExplorerX.ISGWorld61 m_Hook;
        private bool m_Flag = false;
        //private string m_TempGroup = "LampAnalysis";
        private string m_LampGroup = "信号灯";
        private string[] m_RoadWidthFields = { "EastWidth", "SouthWidth", "WestWidth", "NorthWidth" };
        private string[] m_RoadOffsetFields = {"EOffset","SOffset","WOffset","NOffset" };
        private string m_ModelFile = ConfigurationManager.AppSettings["LampModelFile"];
        private string m_FlagField = "Flag";

        private void Clear()
        {
            m_Result.Clear();

            this.txtResult.Clear();
            Application.DoEvents();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (m_Flag)
            {
                if (MessageBox.Show("已经分析过了，确定要再次分析吗?", "Sunz", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
            }
            else
            {
                this.Height = this.Height + txtResult.Height;
                this.Refresh();
                Application.DoEvents();
            }

            Clear();

            try
            {
                // 获取设置
                double lampHeight = (double)spinEdit1.Value;
                double carHeight = (double)spinEdit2.Value;
                double setHeight = (double)spinEdit3.Value;
                double carLength = (double)spinEdit4.Value;
                double lampMustDistance = (double)spinEdit5.Value;

                LampSetting lampSetting = new LampSetting();
                lampSetting.MostLampHeight = lampHeight;
                lampSetting.MostCarHeight = carHeight;
                lampSetting.LestSetHeight = setHeight;
                lampSetting.MostCarLength = carLength;
                lampSetting.MustViewDistance = lampMustDistance;
                m_Result.Setting = lampSetting;

                object[] args = { lampHeight, carHeight, setHeight, carLength, lampMustDistance };
                string strArgs = string.Format("信号灯最大高度：{0}米；车辆最大高度：{1}米；车座最小高度：{2}米；车辆最大长度：{3}米；信号灯最小必须可见距离：{4}米", args);

                string lyr = ConfigurationManager.AppSettings["CrossLayer"];
                int lyrID = m_Hook.ProjectTree.FindItem(lyr);
                if (lyrID < 0)
                {
                    MessageBox.Show("您的配置有问题或者路口图层没有加载");
                    return;
                }

                ILayer61 teLayer = null;
                try
                {
                    teLayer = m_Hook.ProjectTree.GetLayer(lyrID);
                    if (teLayer == null)
                    {
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("您配置的路口图层不是矢量图层");
                    return;
                }

                // 准备信号灯图层

                int lampGroup = m_Hook.ProjectTree.FindItem(m_LampGroup);
                if (lampGroup > -1)
                {
                    m_Hook.ProjectTree.DeleteItem(lampGroup);
                }
                lampGroup = m_Hook.ProjectTree.CreateGroup(m_LampGroup);

                int fCount = teLayer.FeatureGroups.Point.Count;
                string[] strDirects = { "东", "南", "西", "北" };
                int fIndex = 0;
                while (true)
                {

                    double crossWidth = 10;

                    SendMessage(string.Format("正在分析第{0}个路口...", fIndex + 1));

                    IFeature61 tefCurrent = teLayer.FeatureGroups.Point[fIndex] as IFeature61;
                    for (int i = 0; i < 4; i++)
                    {
                        SendMessage(string.Format("　　正在分析{0}面的信号灯...", strDirects[i]));
                        crossWidth = Convert.ToDouble(tefCurrent.FeatureAttributes.GetFeatureAttribute(m_RoadWidthFields[i]).Value);
                        if (crossWidth <= 0)
                        {
                            SendMessage(string.Format("　　　　{0}没有支路，不需要信号灯。", strDirects[i]));
                            continue;
                        }

                        string strRoadInfo = string.Format("路口宽度：{0}米", crossWidth);

                        SendMessage(string.Format("　　　　加载参数：{0}；{1}...", strArgs, strRoadInfo));
                        SendMessage(string.Format("　　　　正在添加信号灯..."));
                        // 添加信号灯
                        IPoint fPoint = tefCurrent.Geometry as IPoint;
                        double offSet = crossWidth / 2.0;
                        if (fPoint.X < 180) // 坐标转换
                            offSet = offSet / 102166.66666666666666666666666667;

                        // 按东南西北移动到对面的斑马线
                        double modelX = fPoint.X + (i == 0 ? -offSet : (i == 2 ? offSet : 0));
                        double modelY = fPoint.Y + (i == 1 ? offSet : (i == 3 ? -offSet : 0));

                        // 按东南西北移动到右侧
                        modelX = modelX + (i == 1 ? offSet : (i == 3 ? -offSet : 0));
                        modelY = modelY + (i == 0 ? offSet : (i == 2 ? -offSet : 0));

                        // 根据路口实际方位与正东南西北方位偏移角做位置调整
                        double rawOffSet = Convert.ToDouble(tefCurrent.FeatureAttributes.GetFeatureAttribute(m_RoadOffsetFields[i]).Value);
                        double dgree = rawOffSet / 180 * Math.PI;
                        double dgreeBase = Math.PI / 4;
                        double dOffsetX = 0;
                        double dOffsetY = 0;
                        switch (i)
                        {
                            case 0:
                                dOffsetX = offSet * (Math.Cos(dgreeBase) - Math.Cos(dgree + dgreeBase));
                                dOffsetY = offSet * (Math.Sin(dgree + dgreeBase) - Math.Sin(dgreeBase));
                                break;

                            case 1:
                                dOffsetX = offSet * (Math.Sin(dgree + dgreeBase) - Math.Sin(dgreeBase));
                                dOffsetY = -offSet * (Math.Cos(dgreeBase) - Math.Cos(dgree + dgreeBase));
                                break;

                            case 2:
                                dOffsetX = -offSet * (Math.Cos(dgreeBase) - Math.Cos(dgree + dgreeBase));
                                dOffsetY = -offSet * (Math.Sin(dgree + dgreeBase) - Math.Sin(dgreeBase));
                                break;

                            case 3:
                                dOffsetX = -offSet * (Math.Sin(dgree + dgreeBase) - Math.Sin(dgreeBase));
                                dOffsetY = offSet * (Math.Cos(dgreeBase) - Math.Cos(dgree + dgreeBase));
                                break;
                        }
                        modelX = modelX + dOffsetX;
                        modelY = modelY + dOffsetY;

                        double raw = (rawOffSet + 90 * (1 + i)) % 360;
                        IPosition61 position = m_Hook.Creator.CreatePosition(modelX, modelY, fPoint.Z, AltitudeTypeCode.ATC_TERRAIN_RELATIVE, raw);
                        string strName = string.Format("第{0}个路口{1}面支路", fIndex + 1, strDirects[i]);
                        ITerrainModel61 theModel = m_Hook.Creator.CreateModel(position, m_ModelFile, 1, ModelTypeCode.MT_NORMAL, lampGroup, strName);
                        //m_Hook.Navigate.SetPosition(position);
                        //Application.DoEvents();


                        // 信号灯信息
                        LampInfo lampInfo = new LampInfo();
                        lampInfo.CrossName = (fIndex + 1).ToString();
                        lampInfo.Name = strName;
                        lampInfo.DirectString = strDirects[i];
                        lampInfo.CrossWidth = crossWidth;
                        lampInfo.RawOffset = rawOffSet;
                        lampInfo.TheLamp = theModel;
                        lampInfo.X = modelX;
                        lampInfo.Y = modelY;
                        m_Result.AddLamp(lampInfo);         // 无论后来计算的结果如何，先添加到结果

                        // 计算大车遮挡
                        // 计算时间太短，停一秒
                        System.Threading.Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["HeightSleep"]));
                        double lampMustHeight = (carHeight - setHeight) * (lampMustDistance + crossWidth) / (lampMustDistance - carLength) + setHeight;
                        lampInfo.LestHeight = lampMustHeight;

                        SendMessage(string.Format("　　　　信号最小理论高度为{0}米...", lampMustHeight));
                        if (lampHeight < lampMustHeight)
                        {
                            lampInfo.HeightFlag = true;

                            SendMessage(string.Format("　　　　信号灯在有大车情况下不能在规定的最小距离内看到信号灯，必须在路对面增加辅助信号灯."));
                            SendMessage(string.Format("　　　　正在添加辅助信号灯..."));

                            // 添加辅助信号灯
                            position = m_Hook.Creator.CreatePosition(fPoint.X + (i < 1 ? -offSet : offSet), fPoint.Y + (i == 1 || i == 2 ? offSet : -offSet), fPoint.Z);
                            ITerrainModel61 assistantModel = m_Hook.Creator.CreateModel(position, m_ModelFile, 1, ModelTypeCode.MT_NORMAL, lampGroup, string.Format("第{0}个路口{1}辅助", fIndex + 1, strDirects[i]));

                            lampInfo.AssistantLamp = assistantModel;

                            continue;
                        }

                        SendMessage(string.Format("　　　　正在计算是否会因为建筑物及绿化带等引起信号灯盲区..."));

                        // 计算因为建筑物及绿化带等引起的
                        // 计算过程就先不计算了，从数据里读吧，停2秒
                        System.Threading.Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["BuildingSleep"]));
                        if (Convert.ToInt32(tefCurrent.FeatureAttributes.GetFeatureAttribute(m_FlagField).Value) > 0)
                        // 引起盲区
                        {
                            lampInfo.ViewFlag = false;

                            SendMessage(string.Format("　　　　由于建筑物及绿化带等将引起信号灯盲区，必须在路对面增加辅助信号灯."));
                            SendMessage(string.Format("　　　　正在添加辅助信号灯..."));

                            // 添加辅助信号灯
                            position = m_Hook.Creator.CreatePosition(fPoint.X + (i < 1 ? -offSet : offSet), fPoint.Y + (i == 1 || i == 2 ? offSet : -offSet), fPoint.Z);
                            ITerrainModel61 assistantModel = m_Hook.Creator.CreateModel(position, m_ModelFile, 1, ModelTypeCode.MT_NORMAL, lampGroup, string.Format("第{0}个路口{1}辅助", fIndex + 1, strDirects[i]));

                            lampInfo.AssistantLamp = assistantModel;

                        }

                    }

                    fIndex++;
                    if (fIndex == fCount)
                        break;
                }

                SendMessage("自动化分析已完成。");
                m_Result.LampAnalysisFinished();

                m_Flag = true;
            }
            catch
            {
                MessageBox.Show("对不起，分析过程出现错误，这通常是由配置与数据不匹配引起的");
                return;
            }
        }

        private void SendMessage(string strMsg)
        {
            m_Result.AddLogMessage(strMsg);

            txtResult.AppendText(strMsg+"\r\n");
            txtResult.ScrollToCaret();
            Application.DoEvents();
        }

        
    }
}