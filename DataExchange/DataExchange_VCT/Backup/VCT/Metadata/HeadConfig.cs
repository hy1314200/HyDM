using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DIST.DGP.DataExchange.VCT.Metadata
{
    /// <summary>
    /// 数据转换头文件配置信息类
    /// </summary>
    internal class HeadConfig
    {

        public HeadConfig(EnumDBStandard pEnumDBStandard)
        {
            m_DBStandard = pEnumDBStandard;
        }
        private ConfigHeadItem m_CoordSystemType;
        /// <summary>
        /// 坐标系统类型
        /// </summary>
        public ConfigHeadItem CoordSystemType
        {
            get
            {
                return m_CoordSystemType;
            }   
        }

        private ConfigHeadItem m_CoordinateDim;
        /// <summary>
        /// 坐标维数
        /// </summary>
        public ConfigHeadItem CoordinateDim
        {
            get
            {
                return m_CoordinateDim;
            }
          
        }

        private ConfigHeadItem m_XAxisDirection;
        /// <summary>
        /// 坐标轴方向
        /// </summary>
        public ConfigHeadItem XAxisDirection
        {
            get
            {
                return m_XAxisDirection;
            }
           
        }

        private ConfigHeadItem m_YAxisDirection;
        /// <summary>
        /// 坐标轴方向
        /// </summary>
        public ConfigHeadItem YAxisDirection
        {
            get
            {
                return m_YAxisDirection;
            }

        }

        private ConfigHeadItem m_CoordinateUnit;
        /// <summary>
        /// 坐标单位
        /// </summary>
        public ConfigHeadItem CoordinateUnit
        {
            get
            {
                return m_CoordinateUnit;
            }
          
        }

        private ConfigInfo m_Sparator;
        /// <summary>
        /// 分隔符
        /// </summary>
        public ConfigInfo Sparator
        {
            get
            {
                return m_Sparator;
            }
           
        }

        private ConfigInfo m_Spheroid;
        /// <summary>
        /// 参考椭球
        /// </summary>
        public ConfigInfo Spheroid
        {
            get
            {
                return m_Spheroid;
            }
        }

        private ConfigInfo m_DataMark;
        /// <summary>
        /// 数据标识
        /// </summary>
        public ConfigInfo DataMark
        {
            get
            {
                return m_DataMark;
            }

        }

       
        private ConfigHeadItem m_Version;
        /// <summary>
        /// 版本号
        /// </summary>
        public ConfigHeadItem Version
        {
            get
            {
                return m_Version;
            }
        }

        private ConfigInfo m_Projection;
        /// <summary>
        /// 投影类型
        /// </summary>
        public ConfigInfo Projection
        {
            get
            {
                return m_Projection;
            }

        }

        private ConfigInfo m_PrimeMeridian;
        /// <summary>
        /// 首子午线
        /// </summary>
        public ConfigInfo PrimeMeridian
        {
            get
            {
                return m_PrimeMeridian;
            }
        }

        //private ConfigInfo m_Scale;
        ///// <summary>
        ///// 首子午线
        ///// </summary>
        //public ConfigInfo Scale
        //{
        //    get
        //    {
        //        return m_Scale;
        //    }
        //}

        private ConfigInfo m_ProjectionParator;
        /// <summary>
        /// 投影参数
        /// </summary>
        public ConfigInfo ProjectionParator
        {
            get
            {
                return m_ProjectionParator;
            }

        }

        private ConfigInfo m_Vertical;
        /// <summary>
        /// 高程基准
        /// </summary>
        public ConfigInfo Vertical
        {
            get
            {
                return m_Vertical;
            }
        }

        private ConfigInfo m_TemporalReferenceSystem;
        /// <summary>
        /// 时间参考系
        /// </summary>
        public ConfigInfo TemporalReferenceSystem
        {
            get
            {
                return m_TemporalReferenceSystem;
            }
        }

        private ConfigInfo m_MaxCoordinate;
        /// <summary>
        /// 最大坐标
        /// </summary>
        public ConfigInfo MaxCoordinate
        {
            get
            {
                return m_MaxCoordinate;
            }

        }

        private ConfigInfo m_MinCoordinate;
        /// <summary>
        /// 最小坐标
        /// </summary>
        public ConfigInfo MinCoordinate
        {
            get
            {
                return m_MinCoordinate;
            }

        }

        private ConfigInfo m_Scale;
        /// <summary>
        /// 比例尺
        /// </summary>
        public ConfigInfo Scale
        {
            get
            {
                return m_Scale;
            }
        }

        private ConfigInfo m_Excurtion;
        /// <summary>
        /// 坐标偏移量
        /// </summary>
        public ConfigInfo Excurtion
        {
            get
            {
                return m_Excurtion;
            }

        }

        private ConfigInfo m_PassTime;
        /// <summary>
        /// 土地规划批准时间
        /// </summary>
        public ConfigInfo PassTime
        {
            get
            {
                return m_PassTime;
            }
        }

        private ConfigInfo m_HeadSpilit;
        /// <summary>
        /// 头文件中的信息分隔符
        /// </summary>
        public ConfigInfo HeadSpilit
        {
            get
            {
                return m_HeadSpilit;
            }
        }


        /// <summary>
        /// 当前所用的标准
        /// </summary>
        private EnumDBStandard m_DBStandard;

        /// <summary>
        /// 初始化头文件配置信息
        /// </summary>
        /// <param name="pHeadNode"></param>
        /// <returns></returns>
        public bool Initial(XmlNode pHeadNode)
        {
            if (pHeadNode == null)
                return false;
            foreach (XmlNode pHeadInfo in pHeadNode.ChildNodes)
            {
                ///处理坐标系统类型
                if (pHeadInfo.Name == "COORDINATESYSTEMTYPE")
                {
                    m_CoordSystemType = new ConfigHeadItem(pHeadInfo);
                    continue;
                }

                ///处理坐标维数
                if (pHeadInfo.Name == "COORDINATEDIM")
                {
                    m_CoordinateDim = new ConfigHeadItem(pHeadInfo);
                    continue;
                }

                ///X坐标轴方向
                if (pHeadInfo.Name == "XAXISDIRECTION")
                {
                    m_XAxisDirection = new ConfigHeadItem(pHeadInfo);
                    continue;
                }

                ///Y坐标轴方向
                if (pHeadInfo.Name == "YAXISDIRECTION")
                {
                    m_YAxisDirection = new ConfigHeadItem(pHeadInfo);
                    continue;
                }

                 ///坐标单位
                if (pHeadInfo.Name == "COORDINATEUNIT")
                {
                    m_CoordinateUnit = new ConfigHeadItem(pHeadInfo);
                    continue;
                }

                ///版本
                if (pHeadInfo.Name == "VERSION")
                {
                    m_Version = new ConfigHeadItem(pHeadInfo);
                    if (m_Version != null)
                    {
                        ////修改默认值为当前标准
                      string strMark = m_DBStandard.ToString();
                      foreach (ConfigInfo item in m_Version.ConfigInfoList)
                      {
                          if (strMark.Trim() == item.Mark.Trim())
                          {
                              m_Version.Defualt = item.Symbol;
                              break;
                          }
                      }
                    }
                    continue;
                }

                ///分隔符
                if (pHeadInfo.Name == "SEPARATOR")
                {
                    m_Sparator = GetConfigInfo(pHeadInfo);
                    continue;
                }

                ///头信息分隔符
                if (pHeadInfo.Name == "HEADSEPARATOR")
                {
                    m_HeadSpilit = GetConfigInfo(pHeadInfo);
                    continue;
                }

                ///参考椭球
                if (pHeadInfo.Name == "SPHEROID")
                {
                    m_Spheroid = GetConfigInfo(pHeadInfo);
                    continue;
                }

                ///数据标识
                if (pHeadInfo.Name == "DATAMERK")
                {
                    m_DataMark = GetConfigInfo(pHeadInfo);
                    continue;
                }

                /////版本
                //if (pHeadInfo.Name == "VERSION")
                //{
                //    m_Version = GetConfigInfo(pHeadInfo);
                //    continue;
                //}

                //投影类型
                if (pHeadInfo.Name == "PROJECTION")
                {
                    m_Projection = GetConfigInfo(pHeadInfo);
                    continue;
                }

                ///首子午线
                if (pHeadInfo.Name == "PRIMEMERIDIAN")
                {
                    m_PrimeMeridian = GetConfigInfo(pHeadInfo);
                    continue;
                }

                /////投影类型
                //if (pHeadInfo.Name == "PROJECTIONTYPE")
                //{
                //    m_ProjectionType = GetConfigInfo(pHeadInfo);
                //    continue;
                //}

                ///投影参数
                if (pHeadInfo.Name == "PROJECTIONPARAMETOR")
                {
                    m_ProjectionParator = GetConfigInfo(pHeadInfo);
                    continue;
                }

                ///高程基准
                if (pHeadInfo.Name == "VERTICAL")
                {
                    m_Vertical = GetConfigInfo(pHeadInfo);
                    continue;
                }

                ///时间参考系
                if (pHeadInfo.Name == "TEMPORALREFERENCESYSTEM")
                {
                    m_TemporalReferenceSystem = GetConfigInfo(pHeadInfo);
                    continue;
                }

                ///最大坐标
                if (pHeadInfo.Name == "MAXLOCAL")
                {
                    m_MaxCoordinate = GetConfigInfo(pHeadInfo);
                    continue;
                }

                ///最小坐标
                if (pHeadInfo.Name == "MINLOCAL")
                {
                    m_MinCoordinate = GetConfigInfo(pHeadInfo);
                    continue;
                }

                ///比例尺
                if (pHeadInfo.Name == "SCALE")
                {
                    m_Scale = GetConfigInfo(pHeadInfo);
                    continue;
                }

                ///偏移量
                if (pHeadInfo.Name == "EXCURSION")
                {
                    m_Excurtion = GetConfigInfo(pHeadInfo);
                    continue;
                }
                 ///土地规划批准时间
                if (pHeadInfo.Name == "PASSTIME")
                {
                    m_PassTime = GetConfigInfo(pHeadInfo);
                    continue;
                }
            }
            return true;
        }

        /// <summary>
        /// 解析xml属性获取配置信息
        /// </summary>
        /// <param name="pInfoNode"></param>
        /// <returns></returns>
        private ConfigInfo GetConfigInfo(XmlNode pInfoNode)
        {  
            string sSymbol="";
            string sValue="";
            string sMark = "";
            XmlAttributeCollection pAttributeCollection = pInfoNode.Attributes;

            ///读取特征属性
            XmlAttribute pAttribute = pAttributeCollection["Symbol"];
            if (pAttribute != null)
                sSymbol = pAttribute.Value;

            ///读取值
            pAttribute = pAttributeCollection["Value"];
            if (pAttribute != null)
                sValue = pAttribute.Value;

            ///读取特征值
            pAttribute = pAttributeCollection["Mark"];
            if (pAttribute != null)
                sMark = pAttribute.Value;

            ConfigInfo info = new ConfigInfo(sSymbol, sValue, sMark);
            return info;
        }

        /// <summary>
        /// 根据配置表中设置的strMark值获取对应的vct命名
        /// </summary>
        /// <param name="sNodeName">所在标准文件的节点名称</param>
        /// <param name="strMark">程序中约定的标志值</param>
        /// <returns></returns>
        public  string GetHeadSymbol(string strMark)
        {
            try
            {
                #region
                ///坐标系统类型

                ConfigHeadItem pSystemTypeInfo = this.CoordSystemType;
                if (strMark == pSystemTypeInfo.Mark)
                    return pSystemTypeInfo.Symbol;

                ////坐标维数

                ConfigHeadItem pSystemDimInfo = this.CoordinateDim;
                if (strMark == pSystemDimInfo.Mark)
                    return pSystemDimInfo.Symbol;


                ///x坐标轴方向


                ConfigHeadItem pAxisDirection = this.XAxisDirection;
                if (strMark == pAxisDirection.Mark)
                    return pAxisDirection.Symbol;


                ///y坐标轴方向
                pAxisDirection = this.YAxisDirection;
                if (strMark == pAxisDirection.Mark)
                    return pAxisDirection.Symbol;

                ///坐标单位

                ConfigHeadItem pCoordinateUnit = this.CoordinateUnit;
                if (strMark == pCoordinateUnit.Mark)
                    return pCoordinateUnit.Symbol;

                ///版本

                ConfigHeadItem pVersion = this.Version;
                if (strMark == pVersion.Mark)
                    return pVersion.Symbol;

                ///首子午线
                ConfigInfo pConfig = this.PrimeMeridian;
                if (strMark == pConfig.Mark)
                    return pConfig.Symbol;

                ///分隔符

                pConfig = this.Sparator;
                if (strMark == pConfig.Mark)
                    return pConfig.Symbol;


                ///数据标识
                pConfig = this.DataMark;
                if (strMark == pConfig.Mark)
                    return pConfig.Symbol;


                ///参考椭球
                pConfig = this.Spheroid;
                if (strMark == pConfig.Mark)
                    return pConfig.Symbol;

                ///投影类型
                pConfig = this.Projection;
                if (strMark == pConfig.Mark)
                    return pConfig.Symbol;

                ///投影参数
                pConfig = this.ProjectionParator;
                if (strMark == pConfig.Mark)
                    return pConfig.Symbol;

                ///高程基准
                pConfig = this.Vertical;
                if (strMark == pConfig.Mark)
                    return pConfig.Symbol;

                ///时间参考系
                pConfig = this.TemporalReferenceSystem;
                if (strMark == pConfig.Mark)
                    return pConfig.Symbol;

                ///最大坐标
                pConfig = this.MaxCoordinate;
                if (strMark == pConfig.Mark)
                    return pConfig.Symbol;

                ///最小坐标
                pConfig = this.MinCoordinate;
                if (strMark == pConfig.Mark)
                    return pConfig.Symbol;

                ///比例尺
                pConfig = this.Scale;
                if (strMark == pConfig.Mark)
                    return pConfig.Symbol;

                ///坐标偏移量
                pConfig = this.Excurtion;
                if (strMark == pConfig.Mark)
                    return pConfig.Symbol;

                ///土地规划批准时间
                pConfig = this.PassTime;
                if (strMark == pConfig.Mark)
                    return pConfig.Symbol;

                return "";
                #endregion
            }
            catch (Exception ex)
            {
                LogAPI.WriteErrorLog(ex);
                return "";
            }
        }

        /// <summary>
        /// 根据vct文件中的特征值获取标志
        /// </summary>
        /// <param name="sNodeName">所在标准文件的节点名称</param>
        /// <param name="strSymbol">vct文件中的特征值</param>
        /// <returns></returns>
        public  string GetHeadMark(string strSymbol)
        {
            try
            {
                #region
                ///坐标系统类型

                ConfigHeadItem pSystemTypeInfo = this.CoordSystemType;
                if (strSymbol == pSystemTypeInfo.Symbol)
                    return pSystemTypeInfo.Mark;

                ////坐标维数

                ConfigHeadItem pSystemDimInfo = this.CoordinateDim;
                if (strSymbol == pSystemDimInfo.Symbol)
                    return pSystemDimInfo.Mark;

                ////版本
                ConfigHeadItem pVersion = this.Version;
                if (strSymbol == pVersion.Symbol)
                    return pVersion.Mark;

                ///x坐标轴方向


                ConfigHeadItem pAxisDirection = this.XAxisDirection;
                if (strSymbol == pAxisDirection.Symbol)
                    return pAxisDirection.Mark;


                ///y坐标轴方向
                pAxisDirection = this.YAxisDirection;
                if (strSymbol == pAxisDirection.Symbol)
                    return pAxisDirection.Mark;

                ///坐标单位

                ConfigHeadItem pCoordinateUnit = this.CoordinateUnit;
                if (strSymbol == pCoordinateUnit.Symbol)
                    return pCoordinateUnit.Mark;

                ///首子午线

                ConfigInfo pConfig = this.PrimeMeridian;
                if (strSymbol == pConfig.Symbol)
                    return pConfig.Mark;

                ///分隔符

                pConfig = this.Sparator;
                if (strSymbol == pConfig.Symbol)
                    return pConfig.Mark;


                ///数据标识
                pConfig = this.DataMark;
                if (strSymbol == pConfig.Symbol)
                    return pConfig.Mark;


                ///参考椭球
                pConfig = this.Spheroid;
                if (strSymbol == pConfig.Symbol)
                    return pConfig.Mark;

                ///投影类型
                pConfig = this.Projection;
                if (strSymbol == pConfig.Symbol)
                    return pConfig.Mark;

                ///投影参数
                pConfig = this.ProjectionParator;
                if (strSymbol == pConfig.Symbol)
                    return pConfig.Mark;

                ///高程基准
                pConfig = this.Vertical;
                if (strSymbol == pConfig.Symbol)
                    return pConfig.Mark;

                ///时间参考系
                pConfig = this.TemporalReferenceSystem;
                if (strSymbol == pConfig.Symbol)
                    return pConfig.Mark;

                ///最大坐标
                pConfig = this.MaxCoordinate;
                if (strSymbol == pConfig.Symbol)
                    return pConfig.Mark;

                ///最小坐标
                pConfig = this.MinCoordinate;
                if (strSymbol == pConfig.Symbol)
                    return pConfig.Mark;

                ///比例尺
                pConfig = this.Scale;
                if (strSymbol == pConfig.Symbol)
                    return pConfig.Mark;

                ///坐标偏移量
                pConfig = this.Excurtion;
                if (strSymbol == pConfig.Symbol)
                    return pConfig.Mark;

                ///土地规划批准时间
                pConfig = this.PassTime;
                if (strSymbol == pConfig.Symbol)
                    return pConfig.Mark;

                return "";
                #endregion
            }
            catch (Exception ex)
            {
                LogAPI.WriteErrorLog(ex);
                return "";
            }
        }
    }

    public class ConfigHeadItem
    {
        private string m_Name = "";
        /// <summary>
        /// 字典项名称
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
        }
        private string m_AlisName = "";
        /// <summary>
        /// 字典项中文名称
        /// </summary>
        public string AlisName
        {
            get
            {
                return m_AlisName;
            }
        }
        private string m_Defualt = "";
        /// <summary>
        /// 默认值
        /// </summary>
        public string Defualt
        {
            get
            {
                return m_Defualt;
            }
            set
            {
                m_Defualt = value;
            }
        }
        private string m_Mark = "";
        /// <summary>
        /// 程序中识别的标识
        /// </summary>
        public string Mark
        {
            get
            {
                return m_Mark;
            }
        }
        private string m_Symbol;
        /// <summary>
        /// 数据中的标识表现
        /// </summary>
        public string Symbol
        {
            get
            {
                return m_Symbol;
            }
        }
        private List<ConfigInfo> m_ConfigInfo = new List<ConfigInfo>();
        /// <summary>
        /// 字典项配置集合
        /// </summary>
        public List<ConfigInfo> ConfigInfoList
        {
            get
            {
                return m_ConfigInfo;
            }
        }

        public ConfigHeadItem(XmlNode headNode)
        {
            XmlAttributeCollection pAttributeCollection = headNode.Attributes;

            ///读取特征属性
            XmlAttribute pAttribute = pAttributeCollection["Symbol"];
            if (pAttribute != null)
                m_Symbol = pAttribute.Value;

            ///读取特征说明
            pAttribute = pAttributeCollection["AlisName"];
            if (pAttribute != null)
                m_AlisName = pAttribute.Value;

            ///读取特征值
            pAttribute = pAttributeCollection["Mark"];
            if (pAttribute != null)
                m_Mark = pAttribute.Value;

            ///读取特征值
            pAttribute = pAttributeCollection["Defualt"];
            if (pAttribute != null)
                m_Defualt = pAttribute.Value;

            m_Name = headNode.Name;
            foreach (XmlNode item in headNode.ChildNodes)
            {
                ConfigInfo info = GetConfigInfo(item);
                m_ConfigInfo.Add(info);
            }

        }
        private ConfigInfo GetConfigInfo(XmlNode pInfoNode)
        {
            string sSymbol = "";
            string sValue = "";
            string sMark = "";
            XmlAttributeCollection pAttributeCollection = pInfoNode.Attributes;

            ///读取特征属性
            XmlAttribute pAttribute = pAttributeCollection["Symbol"];
            if (pAttribute != null)
                sSymbol = pAttribute.Value;

            ///读取值
            pAttribute = pAttributeCollection["Value"];
            if (pAttribute != null)
                sValue = pAttribute.Value;

            ///读取特征值
            pAttribute = pAttributeCollection["Mark"];
            if (pAttribute != null)
                sMark = pAttribute.Value;

            ConfigInfo info = new ConfigInfo(sSymbol, sValue, sMark);
            return info;
        }
    }
}
