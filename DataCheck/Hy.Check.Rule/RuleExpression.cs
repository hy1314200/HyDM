using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Check.Rule
{
    /// <summary>
    /// 记录规则对应的结构体参数描述。
    /// </summary>
    public class RuleExpression
    {
        /// <summary>
        /// 编码类
        /// </summary>
        public class LRCodePara
        {
            // 为了可以使得规范名称重名，因此使用ID号
            public int nVersionID;

            // 规则类别(名称)
            public string strName;

            // 规则别名
            public string strAlias;

            // 被检图层名称
            public string strFtName;

            // 规则描述
            public string strRemark;

            // 要进行检测的编码字段名称，如“土地用途分区分类代码”
            public string strCodeField;

            // 检测的编码类型，如“当前标准代码库代码”
            public string strCodeType;

            // 编码名称字段,如果为空,则无需进行编码与编码名称的对比检查,反之则需进行对比检查
            public string strNameField;

            // 编码库的表名，如“LR_DictFtCode_Description”
            public string strCodeLibTable;

            // 编码库数据表名称，如“LR_DictFtCode”
            public string strCodeDataTable;

            // 编码规范的名称,来自编码库表的"名称"字段,因此,这个字段需要唯一，如“土地利用规划期信息分类与编码”
            public string strCodeNorm;
        }

        // 属性表结构检测规则参数类
        public class LRFieldCheckPara
        {
            // 规则类别(名称)
            public string strName;

            // 规则别名
            public string strAlias;

            // 规则描述
            public string strRemark;

            // 字段代码，如“DLMC”
            public bool m_bCode;

            // 字段小数位数
            public bool m_bDecimal;

            // 字段存在性检测
            public bool m_bNull;

            // 字段长度
            public bool m_bLength;

            // 字段名称，中文名称 ，如“地类代码”
            public bool m_bName;

            // 字段类型
            public bool m_bType;

            //C#中没有CMapStringToString，因此在此采用list<string> hehy2008年1月25日.
            public List<string> m_LyrFldMap; //public CMapStringToString m_LyrFldMap;待检图层名称和字段对照表
        }

        // 字段完整性检查
        public class LRFieldErrrorInfo
        {
            // 唯一表示ID
            public int nErrID;

            // 错误字段名称
            public string strFieldName;

            // 属性表
            public string strAttrTabName;

            // 标准字段类型
            public string strStdFieldType;

            // 错误原因
            public string strErrorMsg;

            // 是否是例外
            public bool m_bIsException;
        }

        /// <summary>
        /// 要素类型编码检测规则参数类
        /// </summary>
        public class LRFtCodePara
        {
            // 规则类别(名称)
            public string strName;

            // 规则别名
            public string strAlias;

            // 注释
            public string strRemark;

            // 目标图层名称
            public string strTargetLayer;

            // 要进行检测的编码字段名称
            public string strCodeField;
        }

        // 图层完整性质检规则参数类
        public class LRLayerCheckPara
        {
            // 规则类别(名称)
            public string strName;

            // 规则别名
            public string strAlias;

            // 规则描述
            public string strRemark;

            // 是否检测图层属性表名称 - 图层的英文名称
            public bool bAttrubuteName;

            // 是否检测图层名称 - 图层的中文名称
            public bool bLayerName;

            // 是否检测图层缺失
            public bool bIsNull;

            //待检图层名称列表；
            //原来是全部检查，现在由于图形表和属性表合放在一块；为统计考虑需分开设规则；
            public List<string> strLyrList;
        }

        // 图层名称完整性检查
        public class LRLayerErrrorInfo
        {
            // 唯一表示ID
            public int nErrID;

            // 错误文件名称
            public string strLayerName;

            // 错误原因
            public string strErrorMsg;

            // 是否是例外
            public bool m_bIsException;
        }

        /// <summary>
        /// 根据RuleInterface.h中重写
        /// 返回的错误信息类
        /// </summary>
        public class LRResultInfo
        {
            // 要素ID
            public int OID;
            /// <summary>
            /// 标识码
            /// </summary>
            public int BSM;
            /// <summary>
            ///  与OID关联的OID字符串
            /// </summary>           
            public string OID2;
            /// <summary>
            /// 与BSM关联的BSM串
            /// </summary>
            public string BSM2;
            // 错误信息
            public string strErrInfo;
            // 备注信息
            public string strRemark;
            // 目标字段
            public string strTargetField;
            // 目标图层
            public string strTargetLayer;
            // 目标图层
            public string strTargetLayer2;
        }

        /// <summary>
        /// 建库标准类
        /// </summary>
        public struct LRStandard
        {
            // 名称
            public string strName;

            // id
            public int nStandadID;

            // 标准类型id
            public int nStandadType;

            // 创建日期
            public DateTime time;
        } ;

        /// <summary>
        /// 编码检测规则参数
        /// </summary>
        //public struct PubType
        //{
        //    // 为了可以使得规范名称重名，因此使用ID号
        //    private int nVersionID;
        //    // 规则类别(名称)
        //    private string strAliasName;
        //    // 规则别名
        //    private string strAlias;
        //    // 被检图层名称
        //    private string strFtName;
        //    // 规则描述
        //    private string strRemark;
        //    // 要进行检测的编码字段名称，如“土地用途分区分类代码”
        //    private string strCodeField;
        //    // 检测的编码类型，如“当前标准代码库代码”
        //    private string strCodeType;
        //    // 编码名称字段,如果为空,则无需进行编码与编码名称的对比检查,反之则需进行对比检查
        //    private string strNameField;
        //    // 编码库的表名，如“LR_DictFtCode_Description”
        //    private string strCodeLibTable;
        //    // 编码库数据表名称，如“LR_DictFtCode”
        //    private string strCodeDataTable;
        //    // 编码规范的名称,来自编码库表的"名称"字段,因此,这个字段需要唯一，如“土地利用规划期信息分类与编码”
        //    private string strCodeNorm;
        //};
        /// <summary>
        /// 规则信息
        /// </summary>
        public class LRTopoRule
        {
            // 源图层
            public string strSourceLayerName;
            // 拓扑规则
            public string strTopoRuleName;
            // 目标图层
            public string strTargetLayerName;
            // 规则描述
            public string strRuleDesc;
            // 规则别名	
            public string strRuleAliasName;
            // 规则描述图片ID
            public int lPicID;
            // 源图层几何类型
            public int nGeoTSrc;
            // 目标图标几何类型
            public int nGeoTTarget;
            // 源图层Rank
            public int nRankSrc;
            // 目标图标Rank
            public int nRankTarget;
            // 是否显示错误
            public bool bShowError;
        }

        /// <summary>
        /// 拓扑规则类
        /// </summary>
        public class LRTopoParas
        {
            // Base : 拓扑名称
            public string strTopoName;
            // Base : 标准名称
            public string strStandardName;
            // Base : 待检查图层,即目标图层
            public string strSourceLayer;
            // Base : 容差
            public double dTolerance;

            // Layer : 原始图层
            public List<string> arrayOrigLayers;
            // Layer : 原始图层几何类型
            public List<int> arrayOrigGeoT;
            // Layer : 选中图层——拓扑图层
            public List<string> arraySeledLayers;
            // Layer : 选中图层几何类型
            public List<int> arraySeledGeoT;

            // Rank : 图层的优先级
            public List<int> arrayRanks;

            // Rule : 规则数组
            public List<LRTopoRule> arrayRules;
        }

        // 两点距离检测规则
        public class LRPointDistPara
        {
            public string strName;
            public string strAlias;

            public string strRemark;

            // 标准名称
            public string strStdName;

            // 目标图层
            public string strTargetLayer;

            // 目标图层
            public string strBufferLayer;

            // 两点最小距离
            public double dPointDist;

            // 搜索类型
            public int nSearchType;

            // 是否搜索相同点
            public bool bSearchSamePt;
        }

   

        // 	辖区面积对比参数类
        public class DISTRICTPARA
        {
            public string strFtName; //被检图层名
            public string strExpression; //调查面积计算表达式
            public string strDistrictField; //所在辖区字段名
            public double dbThreshold; //容差阈值
            public int iClass; //统计级别（县级，乡级，村级）
        }

        /// <summary>
        /// 结果保存类，主要用于RuleDistrict，RulePlot,RulePlotClass,RuleSheet,RuleStatAdminRegion
        /// </summary>
        public class RESULT
        {
            public int nOID;
            public int BSM;
            public string IDName; //标识名
            public double dbCalArea; //计算面积
            public double dbSurveyArea; //调查面积
            public double dbError; //误差值
            public string strErrInfo; //错误信息
        }

        public struct XMap
        {
            public int nID;

            public string strName;
        } ;

        /// <summary>
        /// 拓扑创建类
        /// </summary>
        public class LRTopoStruct
        {
            // DestinationClassID
            public int nOriLayerID;

            // Destination子类代码
            public int nDestinationSubtype;

            // OriginClassID
            public int nDesitLayerID;

            // Origin子类代码
            public int nOriginSubtype;

            // 规则ID
            public int nRule;

            // 规则名称
            public string strRuleName;
        }

        /// <summary>
        /// 接边检查参数类
        /// </summary>
        public class LRJoinSidePara
        {
            //别名
            public string strAlias;

            //描述
            public string strRemark;

            // 标准名称
            public string strStdName;

            // 要素图层
            public string strFeatureLayer;

            // 范围图层
            public string strBoundLayer;

            // 最大容错值
            public double dLimit;

            //接边字段集合
            public List<string> arrayFieldName;

            //切边线字段
            public string strInciseField;
        }

        public struct JoinSideInfo
        {
            public int OID1; //要素编号
            public string strError; //错误原因
        } ;

       

        //SQL查询检测参数结构体
        public struct SQLPARA
        {
            public string strSQLName; //质检规则类别
            public string strAlias; //查询规则别名
            public string strScript; //描述
            public string strFtName; //要素类名,是否需要
            public string strWhereClause; //查询条件
        } ;

        //非法字符检测参数结构体
        public struct INVALIDPARA
        {
            public string strInvalidName; //质检规则名称
            public string strAlias; //质检规则名称
            public string strScript; //描述
            public string strFtName; //要素类名
            public List<string> fieldArray; //需检查字段名列表
            public List<string> charSetArray; //需检查的非法字符
        } ;

        //频度检测参数结构体
        public struct FREQUENCYPARA
        {
            public string strName; //质检规则名称
            public string strAlias; //质检规则名称
            public string strScript; //描述
            public string strFtName; //要素类名
            public List<string> arrayFields; //需检查字段名数组
            public int nType; //类型，0为and,1为or
            public int nMaxTime; //出现的最大的次数
            public int nMinTime; //出现的最小次数
        } ;

        //空值检测参数结构体
        public class BLANKVALPARA
        {
            public string strName; //质检规则名称
            public string strAlias; //质检规则名称
            public string strScript; //描述
            public string strFtName; //要素类名
            public int iType; //检测类型,0为所有，1为任意
            public List<string> fieldArray; //需检查字段名列表
            public ArrayList fieldTypeArray; //字段类型列表   C++中为CArray<int,int> fieldTypeArray; 
        } ;

        //频度检测结果结构体
        public struct FRERESULT
        {
            public string value; //值
            public List<string> featureIDArray; //要素ID列表
        } ;

        //面积检测参数结构体
        public class AreaParameter
        {
            public string strName; //质检规则名称
            public string strAlias; //质检规则名称
            public string strScript; //描述
            public string strFtName; //要素类名
            public double dbThreshold; //检测阈值
            public List<string> fieldArray; //需检查字段名列表
            public ArrayList fieldTypeArray; //字段类型列表 C++中为CArray<int,int> fieldTypeArray; 
        } ;

        //长度检测参数结构体
        public struct LENGTHPARA
        {
            public string strName; //质检规则名称
            public string strAlias; //质检规则名称
            public string strScript; //描述
            public string strFtName; //要素类名
            public double dbThreshold; //检测阈值
            public List<string> fieldArray; //需检查字段名列表
            public List<int> fieldTypeArray; //字段类型列表  C++中为CArray<int,int> fieldTypeArray; 
        } ;

     
        /// <summary>
        /// 条件重合参数结构体
        /// </summary>
        public struct CONDITIONCOINCIDEPARA
        {
            /// <summary>
            /// 质检规则别名
            /// </summary>
            public string strName;

            /// <summary>
            /// 待检图层
            /// </summary>
            public string strFtName;

            /// <summary>
            /// strFtName符合的查询条件
            /// </summary>
            public string strWhereClause;

            /// <summary>
            /// 待检图层2
            /// </summary>
            public string strFtName2;

            /// <summary>
            /// 错误原因
            /// </summary>
            public string strErrorReason;
        } ;

        /// <summary>
        /// 空间条件关系质检规则结构体
        /// </summary>
        public struct SPATIALCONDITIONPARA
        {
            /// <summary>
            /// 质检规则别名
            /// </summary>
            public string strName;

            /// <summary>
            /// 待检图层
            /// </summary>
            public string strFtName;

            /// <summary>
            /// 待检图层要判断空间关系的图层列表
            /// </summary>
            public List<string> listFtName;

            /// <summary>
            /// strFtName符合的查询条件
            /// </summary>
            public string strWhereClause;

            /// <summary>
            ///  空间关系
            /// </summary>
            public esriSpatialRelEnum eSpatialRel;

            /// <summary>
            /// 满足条件的空间要素最大值
            /// </summary>
            public int nIndex;

            /// <summary>
            /// 错误原因
            /// </summary>
            public string strErrorReason;
        } ;

        /// <summary>
        /// 线面关系质检规则结构体
        /// </summary>
        public struct LINE2POLYGONPARA
        {
            /// <summary>
            /// 质检规则别名
            /// </summary>
            public string strName;

            /// <summary>
            /// 线图层名称
            /// </summary>
            public string strLineName;

            /// <summary>
            /// 面图层名称
            /// </summary>
            public string strPolygonName;
        } ;

        /// <summary>
        /// 穿越面层质检规则结构体
        /// </summary>
        public struct SPATIALTHROUGHPARA
        {
            /// <summary>
            /// 质检规则别名
            /// </summary>
            public string strName;

            /// <summary>
            /// 待检图层名
            /// </summary>
            public string strFtName;

            /// <summary>
            /// 待检图层要穿越的图层列表
            /// </summary>
            public List<string> ListFtName;
        } ;


        /// <summary>
        /// 空间属性关系质检规则结构体的子类，表示待检图层某一字段的待检字段值，以及获取该值时需要判断的空间关系图层列表和该图层对应的SQL语句
        /// </summary>
        public class SpatialAttrParam
        {
            /// <summary>
            /// 待检图层某一字段的待检字段值
            /// </summary>
            public string strFieldCode;

            /// <summary>
            /// 与待检字段存在空间关系的图层
            /// </summary>
            public List<string> listFtName;

            /// <summary>
            /// listFtName所满足的条件
            /// </summary>
            public List<string> listSQLClause;
        }

        public struct LRGraphAttributeCollectPara
        {
            /// <summary>
            /// 别名
            /// </summary>
            public string strAlias;

            /// <summary>
            /// 描述
            /// </summary>
            public string strRemark;

            /// <summary>
            /// 标准名称
            /// </summary>
            public string strStdName;

            /// <summary>
            /// 地物层
            /// </summary>
            public string strGeographyObject;

            /// <summary>
            /// 图斑层
            /// </summary>
            public string strGraphSpeckle;

            /// <summary>
            /// 地物层字段集合
            /// </summary>
            public List<string> arrayGeographyObjectField;

            /// <summary>
            /// 图斑层字段集合
            /// </summary>
            public List<string> arrayGraphSpeckleField;

            /// <summary>
            /// xj 相交，bh包含
            /// </summary>
            public string strCheckKind;

            /// <summary>
            /// 最大容错值
            /// </summary>
            public double dLimit;
        } ;

        public struct FREFIELDVALS
        {
            // 属于该唯一值的记录数量
            public int nRecordCount;

            // 属于该唯一值的各个字段的值
            public List<string> arrayFieldsVals;

            /// <summary>
            /// 该唯一值组成的字符串
            /// </summary>
            public string strFieldsValues;

            
        } ;
    }
}