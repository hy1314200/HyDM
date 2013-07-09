using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Define;
using ESRI.ArcGIS.Controls;

namespace Esri.Frame
{
    /// <summary>
    /// ESRI 命令和工具代理类，方便ESRI下的功能直接在本框架中使用
    /// </summary>
    public class EsriCommandProxy : BaseCommand, ICommand, ITool, IExclusive
    {
        private ESRI.ArcGIS.SystemUI.ICommand m_EsriCommand;

        private IToolbarBuddy  m_EsriBuddy;
        private IHookHelper m_HookHelper = null;
        

        public EsriCommandProxy(ESRI.ArcGIS.SystemUI.ICommand cmdProxed)
        {
            this.m_EsriCommand = cmdProxed;
            if (!(this.m_EsriCommand is ESRI.ArcGIS.SystemUI.ICommand))
            {
                throw new Exception("ESRI命令代理类调用错误：传入的参数不是正确的ESRI命令。");
            }
        }

        public override void OnCreate(object Hook)
        {
            base.OnCreate(Hook);
            m_HookHelper = m_Hook.Hook as IHookHelper;
            if (m_HookHelper == null)
                return;

            this.m_EsriBuddy = m_HookHelper.Hook as IToolbarBuddy;

            //if ((esriHook ==null) || (m_EsriBuddy == null))
            //{
            //    throw new Exception("ESRI命令代理类初始化错误：内部错误，Hook不是正确的ESRI MapControl或PageLayout对象。");
            //}

            try
            {
                m_EsriCommand.OnCreate(m_HookHelper.Hook);
            }
            catch
            {
                //throw new Exception(string.Format("当前Hook对象不是命令【{0}】的正确Hook。", m_EsriCommand.Name));
            }
        }

        public override string Caption
        {
            get
            {
                return m_EsriCommand.Caption;
            }
        }

        public override string Category
        {
            get
            {
                return m_EsriCommand.Category;
            }
        }

        public override bool Checked
        {
            get
            {
                //return m_EsriCommand.Checked;

                if (m_EsriBuddy != null && m_EsriCommand != null)
                    return (m_EsriCommand is ESRI.ArcGIS.SystemUI.ITool) && m_EsriBuddy.CurrentTool == m_EsriCommand;
                
                return false;
                //return (m_EsriCommand is ESRI.ArcGIS.SystemUI.ITool) && ((m_EsriHooker.HookHelper.Hook is ESRI.ArcGIS.Controls.IMapControl4?(m_EsriHooker.HookHelper.Hook as ESRI.ArcGIS.Controls.IMapControl4).CurrentTool:(m_EsriHooker.HookHelper.Hook as ESRI.ArcGIS.Controls.IPageLayoutControl).CurrentTool)==m_EsriCommand);
            }
        }

        public override bool Enabled
        {
            get
            {
                if (m_HookHelper != null && m_EsriCommand != null)
                    return m_EsriCommand.Enabled;

                return false;
            }
        }

        public override System.Drawing.Image Icon
        {
            get
            {

                try
                {
                    return System.Drawing.Image.FromHbitmap((IntPtr)m_EsriCommand.Bitmap);
                }
                catch
                {
                    return Properties.Resources.DefaultIcon;
                }
            }
        }

        public override string Message
        {
            get
            {
                return m_EsriCommand.Message;
            }
        }

        public override string Name
        {
            get
            {
                return m_EsriCommand.Name;
            }
        }

        public override string Tooltip
        {
            get
            {
                return m_EsriCommand.Tooltip;
            }
        }

        public override void OnClick()
        {
            m_EsriCommand.OnClick();

            if (m_EsriCommand is ESRI.ArcGIS.SystemUI.ITool)
            {
                bool oldToolFlag = true;
                if (m_EsriBuddy.CurrentTool != null)
                {
                    oldToolFlag = m_EsriBuddy.CurrentTool.Deactivate();
                }
                if (oldToolFlag)
                {
                    m_EsriBuddy.CurrentTool = (m_EsriCommand as ESRI.ArcGIS.SystemUI.ITool);
                }
                else
                {
                    // 当前的Tool不允许
                    SendMessage(string.Format("正在进行“{0}”操作，当前操作无法完成", (m_EsriBuddy.CurrentTool as ESRI.ArcGIS.SystemUI.ICommand).Caption));
                }
            }
        }

        public bool Release()
        {
            return m_EsriBuddy.CurrentTool.Deactivate();
        }

        public object Resource
        {
            get { return m_HookHelper.Hook; }
        }
    }

    public class EsriExProxy : EsriCommandProxy, ICommandEx
    {
        public EsriExProxy(ESRI.ArcGIS.SystemUI.IToolControl toolControl)
            : base(toolControl as ESRI.ArcGIS.SystemUI.ICommand)
        {
            ExControl = System.Windows.Forms.Control.FromHandle((IntPtr)toolControl.hWnd);
        }

        public object ExControl
        {
            get;
            private set;
        }
    }
}
