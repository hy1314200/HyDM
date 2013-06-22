using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Define;
using System.Reflection;
using System.IO;
using Frame.Define;

namespace Utility
{
    /// <summary>
    /// Command工厂类，负责Command的实例化以及从dll文件获取Rule类信息
    /// </summary>
    public class ResourceFactory
    {  
        private static Dictionary<string, Assembly> m_DictAssembly = new Dictionary<string, Assembly>();

        public static string DefaultDllPath = System.Windows.Forms.Application.StartupPath;

        /// <summary>
        /// 根据dll名和类型类创建实例
        /// </summary>
        /// <param name="dllName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static object CreateInstance(string dllName, string className)
        {
            return CreateInstance(DefaultDllPath,dllName, className);
        }

        private static object CreateObject(string strPath, string className)
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
                //return Activator.CreateInstance(Type.GetType(className));
                //return Activator.CreateComInstanceFrom(strPath, className);
                return assembly.CreateInstance(className);
                //Activator.CreateInstance(Type.GetType(className));
                //ICommand cmd = objCommand as ICommand;
                //return cmd;
            }
            catch(Exception exp)
            {
                Log.AppendMessage(enumLogType.Error, className+":"+exp.ToString());
                return null;
            }
        }

        /// <summary>
        /// 根据指定dll路径、dll名和类型类创建实例
        /// </summary>
        /// <param name="dllPath"></param>
        /// <param name="dllName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static object CreateInstance(string dllPath, string dllName, string className)
        {
            string strPath = System.IO.Path.Combine(dllPath, dllName);
            return CreateObject(strPath, className);
        }

        /// <summary>
        /// 获取dll文件中所有规则类名
        /// 此方法将主要用于配置端注册时
        /// </summary>
        /// <param name="strFile"></param>
        /// <returns></returns>
        public static Dictionary<string,enumResourceType> GetResources(string strFile)
        {
            if (!File.Exists(strFile))
                return null;

            Type typeFrameCommand = typeof(ICommand);
            Type typePlugin = typeof(IPlugin);
            Dictionary<string,enumResourceType> cmdClassList = new Dictionary<string,enumResourceType>();
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
                            if (!_type.IsClass)
                                continue;

                            //获得一个类型所有实现的接口
                            Type[] _interfaces = _type.GetInterfaces();
                            //遍历接口类型
                            foreach (Type curInterface in _interfaces)
                            {
                                if (curInterface == typeFrameCommand || Frame.Environment.ResourceManager.IsResource(curInterface.FullName))
                                {
                                    cmdClassList.Add(_type.FullName,enumResourceType.Command);
                                    break;
                                }
                                if(curInterface==typePlugin)
                                {
                                    cmdClassList.Add(_type.FullName,enumResourceType.Plugin);
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

            return cmdClassList;
        }
        
        /// <summary>
        /// 从ClassInfo创建Command
        /// </summary>
        /// <param name="classInfo"></param>
        /// <returns></returns>
        public static ICommand CreateCommand(ClassInfo classInfo)
        {
            object obj = CreateInstance(classInfo.DllName, classInfo.ClassName);
            //if (obj is ESRI.ArcGIS.SystemUI.ICommandSubType)
            //    return null;

            //if (obj is ESRI.ArcGIS.SystemUI.IToolControl)
            //{
            //    obj = new EsriExProxy(obj as ESRI.ArcGIS.SystemUI.IToolControl);
            //}else if (obj is ESRI.ArcGIS.SystemUI.ICommand)
            //{
            //    obj = new EsriCommandProxy(obj as ESRI.ArcGIS.SystemUI.ICommand);
            //}
            //return obj as ICommand;

            return Frame.Environment.ResourceManager.CommandProxy(obj);
        }

        public static IPlugin CreatePlugin(ClassInfo classInfo)
        {
            return CreateInstance(classInfo.DllName, classInfo.ClassName) as IPlugin;
        }

        ///// <summary>
        ///// 获取程序中所有Plugin
        ///// </summary>
        ///// <param name="strFile"></param>
        ///// <returns></returns>
        //public static List<string> GetPlugins(string strFile)
        //{
        //    if (!File.Exists(strFile))
        //        return null;

        //    Type pluginDef = typeof(IPlugin);
        //    List<string> pluginList = new List<string>();
        //    try
        //    {
        //        Assembly _assembly = Assembly.LoadFile(strFile);
        //        if (_assembly != null)
        //        {
        //            //获取程序集中定义的类型
        //            Type[] _types = _assembly.GetTypes();
        //            if (_types != null)
        //            {
        //                foreach (Type _type in _types)
        //                {
        //                    if (!_type.IsClass)
        //                        continue;

        //                    //获得一个类型所有实现的接口
        //                    Type[] _interfaces = _type.GetInterfaces();
        //                    //遍历接口类型
        //                    foreach (Type curInterface in _interfaces)
        //                    {
        //                        if (curInterface == pluginDef)
        //                        {
        //                            pluginList.Add(_type.FullName);
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    return pluginList;
        //}

    }
}
