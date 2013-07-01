using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Check.Define;
using System.Reflection;
using System.IO;

namespace Check.Engine.Helper
{
    /// <summary>
    /// Rule工厂类，负责Rule的实例化以及从dll文件获取Rule类信息
    /// </summary>
    public class RuleFactory
    {
        private static Dictionary<string, Assembly> m_DictAssembly = new Dictionary<string, Assembly>();

        public static string DefaultRuleDllPath = System.Windows.Forms.Application.StartupPath + "\\Plugin";

        ///// <summary>
        ///// 从规则类信息创建规则实例
        ///// </summary>
        ///// <param name="ruleClassInfo"></param>
        ///// <returns></returns>
        //public static ICheckRule CreateRuleInstance(RuleClassInfo ruleClassInfo)
        //{
        //    return CreateRuleInstance(ruleClassInfo.DllName, ruleClassInfo.ClassName);
        //}

        /// <summary>
        /// 根据dll名和类型类创建规则实例
        /// </summary>
        /// <param name="dllName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static ICheckRule CreateRuleInstance(string dllName, string className)
        {
            return CreateRuleInstance(DefaultRuleDllPath,dllName, className);
        }

        private static ICheckRule CreateInstance(string strPath, string className)
        {
            if (!System.IO.File.Exists(strPath))
                return null;

            Assembly assembly = null;
            if (m_DictAssembly.ContainsKey(strPath))
            {
                assembly = m_DictAssembly[strPath];
            }
            else
            {
                assembly = Assembly.LoadFile(strPath);
                m_DictAssembly.Add(strPath, assembly);
            }

            try
            {
                object objRule = assembly.CreateInstance(className);
                //Activator.CreateInstance(Type.GetType(className));
                ICheckRule checkRule = objRule as ICheckRule;
                return checkRule;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 根据指定dll路径、dll名和类型类创建规则实例
        /// </summary>
        /// <param name="dllPath"></param>
        /// <param name="dllName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static ICheckRule CreateRuleInstance(string dllPath, string dllName, string className)
        {
            string strPath = System.IO.Path.Combine(dllPath, dllName);
            return CreateInstance(strPath, className);
        }

        /// <summary>
        /// 获取dll文件中所有规则类名
        /// 此方法将主要用于配置端注册时
        /// </summary>
        /// <param name="strFile"></param>
        /// <returns></returns>
        public static List<string> GetRuleClasses(string strFile)
        {
            if (!File.Exists(strFile))
                return null;

            Type typeRule = typeof(ICheckRule);
            List<string> ruleClassList = new List<string>();
            try
            {
                Assembly _assembly = Assembly.LoadFile(strFile);
                if (_assembly != null)
                {
                    //获取程序集中定义的类型
                    Type[] _types = _assembly.GetTypes();
                    if (_types != null)
                    {
                        foreach (Type _type in _types)
                        {
                            //获得一个类型所有实现的接口
                            Type[] _interfaces = _type.GetInterfaces();
                            //遍历接口类型
                            foreach (Type curInterface in _interfaces)
                            {
                                if (curInterface == typeRule)
                                {
                                    ruleClassList.Add(_type.FullName);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            return ruleClassList;
        }
    }
}
