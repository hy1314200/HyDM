using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Define;
using System.Windows.Forms;
namespace Frame.Define
{
    /// <summary>
    /// 资源管理器接口
    /// 1.负责GIS控件及Hook对象的创建及消毁
    /// 2.负责资源（接口）识别
    /// 3.允许对Command进行封装
    /// 4.负责建立GIS连接
    /// </summary>
    public interface IResourceManager
    {
        /// <summary>
        /// 许可验证（因为GIS控件通常为第三方，需要许可）
        /// </summary>
        /// <param name="errMsg">如果许可错误，返回错误信息</param>
        /// <returns></returns>
        bool LicenseVerify(ref string errMsg);

        /// <summary>
        /// 创建返回的GIS控件
        /// </summary>
        /// <returns></returns>
        Control GetHookControl();

        /// <summary>
        /// 创建Hook对象
        /// </summary>
        /// <param name="frmMain">环境主窗口</param>
        /// <param name="leftDock">环境左面版</param>
        /// <param name="rightDock">环境右面版</param>
        /// <param name="bottomDock">环境下面版</param>
        /// <returns></returns>
        IHook CreateHook(Form frmMain,Control leftDock,Control rightDock,Control bottomDock);
        
        /// <summary>
        /// 资源消毁
        /// </summary>
        void Release();

        /// <summary>
        /// 允许包装ICommand，通常指对接口进行托管，以支持第三方定义的ICommand
        /// </summary>
        /// <param name="objCommand"></param>
        /// <returns></returns>
        ICommand CommandProxy(object objCommand);

        /// <summary>
        /// 创建GIS连接
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="strArgs"></param>
        /// <returns></returns>
        object GetWorkspace(string strType, string strArgs);

        /// <summary>
        /// 识别接口是否是资源
        /// </summary>
        /// <param name="strInterfaceName"></param>
        /// <returns></returns>
        bool IsResource(string strInterfaceName);
    }
}
