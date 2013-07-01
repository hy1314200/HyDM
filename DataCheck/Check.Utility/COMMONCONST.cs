using System.Collections;
using System.Collections.Generic;

namespace Check.Utility
{
    public class COMMONCONST
    {
        public static bool IsGeographicCoordinateSystem = false;
        public static readonly string DB_Name_Temp = "tempdist.mdb";

        public static readonly string DB_Name_Base = "Base.gdb";
        public static readonly string DB_Name_Query = "Query.mdb";
        public static readonly string DB_Name_Topo = "Topo.gdb";
        public static readonly string DB_Name_Result = "Result.mdb";
        public static readonly string Dataset_Name = "Dataset";
        public static readonly string Topology_Name = "拓扑错误";

        /// <summary>
        /// topo错误数限制常量 
        /// </summary>
        public static readonly int TopoError_MaxCount = -1; ///-1为不限制最大结果数
                                                            ///
        public static readonly string Folder_Name_PartlyCheck = "预检";

        public const string File_Name_SystemConfig = "SystemConfig.xml";
        public const string RelativePath_MXD = "Template\\Map";
        public const string File_Name_XSD  = "任务执行格式验证.xsd";


        /// <summary>
        /// 默认系统库名称
        /// </summary>
        public static readonly string SYSDBName = "SysDb.mdb";

        public static readonly string MESSAGEBOX_WARING = "警告";

        public static readonly string MESSAGEBOX_ERROR = "错误";

        public static readonly string MESSAGEBOX_HINT = "提示";

        public static readonly string TEMPLATE_MAP_PATH = @"\template\map";

        public static readonly string C_CONNECTION_TYPE = "System.Data.OleDb";

        /// <summary>
        /// 取错误结果的最大行数
        /// </summary>
        public static readonly int MAX_ROWS = 5000;

        /// <summary>
        /// 图层中具有唯一性的字段名（除了objectid之外），默认为空
        /// </summary>
        public static string m_strDistinctField = "";
        /// <summary>
        /// 加了唯一性字段后，sql语句的变化
        /// </summary>
        private static string strDistinctField_SQL = "";
        /// <summary>
        /// 加了唯一性字段后，sql语句的变化
        /// </summary>
        public static string m_strDistinctField_SQL
        {
            get
            {
                //if (strDistinctField_SQL != "")
                //    return strDistinctField_SQL;
                if (m_strDistinctField == "")
                {
                    strDistinctField_SQL = "OBJECTID";

                }
                else
                {
                    strDistinctField_SQL = "OBJECTID," + m_strDistinctField;
                }
                return strDistinctField_SQL;
            }
        }

        /// <summary>
        /// 导入的图层中统一的去除前缀或者后缀
        /// </summary>
        public static string RemovePlus = "";


        //拓扑错误容限值，用户在创建任务时输入
        public static double TOPOTOLORANCE = 0.001;
        /// <summary>
        /// 碎面检查容差，根据比例尺的不同而不同
        /// </summary>
        public static double dAreaThread = 400.0;
        /// <summary>
        /// 碎面检查容差，比例尺分母×dAreaThreadConst=dAreaThread
        /// </summary>
        public static double dAreaThreadConst = 0.04;

        /// <summary>
        /// 碎线检查容差，根据比例尺的不同而不同
        /// </summary>
        public static double dLengthThread = 0.2;
        /// <summary>
        /// 碎线检查容差，比例尺分母×dLengthThreadConst=dLengthThread
        /// </summary>
        public static double dLengthTreadConst = 0.0002;
        /// <summary>
        /// 地图比例尺,方便矢量化精度检查超限检查
        /// </summary>
        public static int nMapScale = 10000;




        /// <summary>
        /// 接边检查接边缓冲范围
        /// </summary>
        public const double dEdgeBuffer = 300.0;
        /// <summary>
        /// 手动检查中，图幅的选择比例
        /// </summary>
        public const double dSampleRatio = 0.1;

        //西安80坐标系的长短半轴参数
        public const double SemiMajorAxis = 6378140;
        public const double SemiMinorAxis = 6356755.29;
        /// <summary>
        /// 向东偏移距离
        /// </summary>
        public const double FalseEasting = 500000;

        public const string HELP_CHECKRULE = "第二次全国土地调查成果数据质量检查细则v1.1.chm";
        public const string HELP_SYSTEM="第二次全国土地调查成果数据质量检查软件-操作手册.chm";


        public const string CHECKTASKHISTORY = "Check.TaskHistory.xml";

        public const string TB_TKXS = "TKXS";
        public const string TB_TKXS1 = "TKXS1";
        public const string TB_DIST_TKXS = "DIST_TKXS";
        public const string DLTBFCName = "DLTB";

        public const string TABLENAME = "表格数据";

        public const string RESULT_TB_RESULT_ENTRY_RULE = "LR_ResultEntryRule";
    }
}