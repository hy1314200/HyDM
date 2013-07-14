using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreeDimenDataManage.Command.TE
{
    public enum enumTECommandType
    {
        /// <summary>
        /// 显示级别 ---到房屋
        /// </summary>
        House = 32771,
        /// <summary>
        /// 显示级别 ---到街道
        /// </summary>
        Street = 32772,
        /// <summary>
        /// 显示级别 ---到城市
        /// </summary>
        City = 32773,
        /// <summary>
        /// 显示级别 ---到省级
        /// </summary>
        State = 32774,
        /// <summary>
        /// 显示级别 ---到地球
        /// </summary>
        Globe = 32775,
        /// <summary>
        /// 立即执行上一次 执行的动画操作
        /// </summary>
        Play = 1011,
        /// <summary>
        /// 立即停止 执行的动作
        /// </summary>
        Stop = 1010,
        /// <summary>
        /// 环绕模式 以屏幕中点为心环绕旋转
        /// </summary>
        FlyAround = 34026,
        /// <summary>
        /// 指北模式 立即使mpt球指向北方
        /// </summary>
        FaceNorth = 7008,
        /// <summary>
        /// 拖拽模式 （默认的）
        /// </summary>
        Drag = 1022,
        /// <summary>
        /// 浏览模式
        /// </summary>
        Slide = 1021,
        /// <summary>
        /// 旋转模式
        /// </summary>
        TurnAndTilt = 1023,
        /// <summary>
        /// 照相 输出当前视角显示的图片
        /// </summary>
        Snapshot = 32783,
        /// <summary>
        /// 水平距离测量 贴地水平距离
        /// </summary>
        Horizontal = 33326,
        /// <summary>
        /// 空间距离测量  空中水平距离
        /// </summary>
        Aerial = 33327,
        /// <summary>
        /// 垂直距离测量
        /// </summary>
        Vertical = 33330,
        /// <summary>
        /// 保存当前文件
        /// </summary>
        Save = 57603,
        /// <summary>
        /// 面积测量
        /// </summary>
        Area = 33350,
        /// <summary>
        /// 地下模式
        /// </summary>
        Underground = 33372,
        /// <summary>
        /// 后部平视模式(注意使用前要激活对象)
        /// </summary>
        BehindObject = 34200,
        /// <summary>
        /// 复制
        /// </summary>
        Copy = 32817,
        /// <summary>
        /// 粘贴
        /// </summary>
        Paste = 32819,
        /// <summary>
        /// 选择
        /// </summary>
        SelectObject = 33432

    }
}
