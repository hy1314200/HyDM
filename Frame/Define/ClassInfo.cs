using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame.Define
{
    /// <summary>
    /// 类信息
    /// </summary>
    public class ClassInfo
    {
        public ClassInfo()
        {
        }
        
        /// <summary>
        /// 标识
        /// </summary>
        public virtual string ID { get; set; }
        /// <summary>
        /// Dll路径
        /// </summary>
        public virtual string DllName { get; set; }

        /// <summary>
        /// Class名称
        /// </summary>
        public virtual string ClassName { get; set; }

        /// <summary>
        /// 分类（便于管理）
        /// </summary>
        public virtual string Category { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }


        /// <summary>
        /// 返回用于反射的类型名
        /// </summary>
        /// <returns></returns>
        public string GetTypeName()
        {
            return string.Format("{0},{1}", ClassName, DllName);
        }

        public virtual enumResourceType Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || !(obj is ClassInfo)) return false;
            ClassInfo castObj = (ClassInfo)obj;
            return castObj.ID == this.ID;
        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode()
        {
            return this.ID.GetHashCode();

        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Description)? ClassName:Description;
        }
    }

    public enum enumResourceType
    {
        Command=0,
        Plugin
    }
}
