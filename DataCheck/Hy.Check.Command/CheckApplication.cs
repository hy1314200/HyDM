using System.Collections.Generic;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Display;
using Common.UI;
using Hy.Check.Task;
using Hy.Check.Engine;
using Hy.Check.Utility;

namespace Hy.Check.Command
{
    /// <summary>
    /// 存储主窗体中的全局变量
    /// </summary>
    public class CheckApplication
    {

        private static XProgress progressBar;

        /// <summary>
        /// 获取全局的进度条.
        /// </summary>
        /// <value>The progress bar.</value>
        public static XProgress ProgressBar
        {
            get
            {
                if (progressBar == null)
                    progressBar = new XProgress();
                return progressBar;
            }
        }

        private static XGifProgress gifProgressBar;


        /// <summary>
        /// 获取全局的动画进度条.
        /// </summary>
        /// <value>The GIF progress.</value>
        public static XGifProgress GifProgress
        {
            get 
            {
                if (gifProgressBar == null)
                    gifProgressBar = new XGifProgress();
                return gifProgressBar;
            }
        }

        /// <summary>
        /// 当前任务
        /// </summary>
        public static Hy.Check.Task.Task CurrentTask{get;set;}

        /// <summary>
        /// 当前任务结果是否预检，如果是，则为true
        /// </summary>
        public static bool IsPreCheck;
 
        /// <summary>
        /// 当前是否有一按钮正在使用中，如果是，则为true
        /// </summary>
        public static bool IsInUse = false;

        public static Hy.Check.UI.UC.UCMapControl m_UCDataMap ;
        
        /// <summary>
        /// 任务改变时触发，一个系统只单独存在一个任务
        /// </summary>
        /// <param name="NewTask"></param>
        public static void TaskChanged(Hy.Check.Task.Task NewTask)
        {
            //先将当前质检软件中的任务清空，然后再加载其他质检任务
            //m_UCDataMap.SetTask(null);
            m_UCDataMap.SetTask(NewTask);
        }

        private static TemplateRules m_CurrentTemplateRules = null;

        /// <summary>
        /// 根据方案id初始化当前任务所需的规则类
        /// </summary>
        /// <returns></returns>
        public  static TemplateRules InitCurrentTemplateRules()
        {
            if (m_CurrentTemplateRules == null)
            {
                if (CurrentTask == null)
                    return null;

                if (string.IsNullOrEmpty(CurrentTask.SchemaID)) return null;

                m_CurrentTemplateRules = new TemplateRules(CurrentTask.SchemaID);
            }
            return m_CurrentTemplateRules;
        }

        /// <summary>
        /// 批量创建任务参数，表示是否忽略路径根目录下的数据源
        /// </summary>
        public static bool BolIgnoreRootFile;

        /// <summary>
        /// 批量创建任务参数，指定数据源存在的相对路径
        /// </summary>
        public static string RelationalPath;
    }
}