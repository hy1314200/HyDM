using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyline.GuiHua.Bussiness
{
    /// <summary>
    /// 信号灯分析结果类，全局唯一实例
    /// </summary>
    public class LampAnalysisResult
    {
        private LampAnalysisResult()
        {
        }

        private static LampAnalysisResult m_Instance;
        public static LampAnalysisResult Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new LampAnalysisResult();

                return m_Instance;
            }
        }

        public LampSetting Setting { get; set; }

        private List<LampInfo> m_LampList;

        public List<LampInfo> LampList
        {
            get
            {
                if (m_LampList == null)
                    m_LampList = new List<LampInfo>();

                return m_LampList;
            }
            set
            {
                m_LampList = value;
            }
        }

        public void AddLamp(LampInfo lampInfo)
        {
            this.LampList.Add(lampInfo);
        }

        public void Clear()
        {
            this.LampList.Clear();
            this.ClearLog();

            this.LampAnalysisFinished();
        }

        public int LampCount
        {
            get
            {
                return this.LampList.Count;
            }
        }

        private string m_AnalysisLog;
        public string AnalysisLog
        {
            get { return m_AnalysisLog; }
            set { m_AnalysisLog = value; }
        }

        public void AddLogMessage(string strMsg)
        {
            if (m_AnalysisLog == null)
                m_AnalysisLog = "";

            m_AnalysisLog += strMsg + "\r\n";
        }

        public void ClearLog()
        {
            m_AnalysisLog = "";
        }

        public event System.Threading.ThreadStart LampAnalysised;

        public void LampAnalysisFinished()
        {
            if (this.LampAnalysised != null)
                this.LampAnalysised.Invoke();
        }
    }
}
