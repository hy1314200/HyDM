using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Frame.Define
{
    /// <summary>
    /// Ribbon风格下命令信息
    /// </summary>
    public class RibbonCommandInfo
    {
        public RibbonCommandInfo()
        {
        } 

        /// <summary>
        /// 唯一标识
        /// </summary>
        public virtual string ID { get; set; }
        /// <summary>
        /// 所在Page
        /// </summary>
        public virtual string Page { get; set; }

        /// <summary>
        /// 所在PageGroup
        /// </summary>
        public virtual string PageGroup { get; set; }

        /// <summary>
        /// 类信息
        /// </summary>
        public virtual ClassInfo CommandClass { get; set; }

        /// <summary>
        /// 允许自定义图标
        /// </summary>
        public virtual Image Icon 
        {
            get
            {
                if (m_Icon == null)
                {
                    if (IconByte != null)
                    {
                        try
                        {
                            m_Icon = Image.FromStream(new System.IO.MemoryStream(IconByte));
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }

                return m_Icon;
            }

            set
            {
                m_Icon = value;

                IconByte = null;
                if (m_Icon != null)
                {
                    System.IO.MemoryStream stream=new System.IO.MemoryStream();
                    m_Icon.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    IconByte = stream.ToArray();
                }
            }
        }
        private Image m_Icon;

        private  byte[] IconByte { get; set; }

        /// <summary>
        /// 允许自定义标题
        /// </summary>
        public virtual string Caption { get; set; }      

        /// <summary>
        /// 顺序
        /// </summary>
        public virtual string Order { get; set; }
        
        /// <summary>
        /// 类型（为主工具栏以外的界面提供扩展使用）
        /// </summary>
        public virtual enumCommandType Type { get; set; }

        /// <summary>
        /// 父对象（专指BarSubItem）
        /// </summary>
        public virtual RibbonCommandInfo Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || !(obj is RibbonCommandInfo) ) return false;
            RibbonCommandInfo castObj = (RibbonCommandInfo)obj;
            return castObj.ID == this.ID;
        }

        /// <summary>
        /// 
        /// </summary>
        public override int GetHashCode()
        {
            return this.ID.GetHashCode();

        }
    }
}
