//////////////////////////////////////////////////////////////////////////
//文 件 名 : XDwgDirectReader.h
//创建日期 : 2008/09/26,BeiJing.
//////////////////////////////////////////////////////////////////////////
#ifndef AFX_XDWGREADER_H____INCLUDED_
#define AFX_XDWGREADER_H____INCLUDED_
#include <afxtempl.h>
#include "SharedUsed.h"

//////////////////////////////////////////////////////////////////////////
class OdDbDatabase;
class OdDbObjectId;
class OdResBuf;
class OdDbEntity;
class OdRxObject;
struct DwgPoint
{
	double x;
	double y;
	double z;
};

///////////////////XDWGReader/////////////////////////////////////////////////
class XDWGReader
{
public:
	XDWGReader();
	virtual ~XDWGReader();

	//////////////////////接口函数////////////////////////////////////////////////////
	//批量读取前的准备工作
	BOOL PrepareReadDwg(IWorkspace* pTargetWS, IDataset* pTargetDataset, ISpatialReference* pSpRef);

	//逐个读取CAD文件
	BOOL ReadFile(LPCTSTR lpdwgFilename);

	//设置日志存放路径
	void PutLogFilePath(CString sLogFile);

	//结束DWG的读取工作
	BOOL CommitReadDwg();

public:
	////////////////////////读CAD的参数属性设置//////////////////////////////////////////
	// 空间参考
	ISpatialReference* m_pSpRef;

	// 判断是否读取不可视图层, 默认读取
	BOOL m_IsReadInvisible;

	// 判断是否打散块，默认TRUE为打散
	BOOL m_IsBreakBlock;

	//是否读取面数据,默认不读取
	BOOL m_IsReadPolygon;

	//闭合线是否生成面
	BOOL m_IsLine2Polygon;

	//是否直接挂接扩展属性
	BOOL m_IsJoinXDataAttrs;

	//是否读块数据的定位点
	BOOL m_IsReadBlockPoint;

	//是否生成注记图层
	BOOL m_IsCreateAnnotation;

	//不打散块的模式  0：不打散按块名  1：不打散按CAD图层名
	short m_iUnbreakBlockMode;

	//是否把弧度转换为角度,默认把CAD中的弧度转换为角度
	BOOL m_bConvertAngle;

	//注记图层显示比例
	double m_dAnnoScale;

	//进度条
	CTextProgressCtrl* m_pProgressBar;

	//CAD扩展属性注册应用名
	CStringList m_Regapps;

	//不打散的块名
	CStringList m_unExplodeBlocks;

public:
	//////////////////////实现函数////////////////////////////////////////////////
	// 给Field添加值
	void AddAttributes(LPCTSTR csFieldName, LPCTSTR csFieldValue, IFeatureBuffer*& pFeatureBuffer);

	// 添加基本属性 （颜色，层等）
	HRESULT AddBaseAttributes(OdDbEntity* pEnt, LPCTSTR strEnType, IFeatureBuffer*& pFeatureBuffer);

	// 初始化insert属性
	//void IniBlockAttributes(IFeatureBuffer* pFeatureBuffer);

	// 初始化属性
	void CleanFeatureBuffer(IFeatureBuffer* pFeatureBuffer);

	// 初始化所有featurebuffer
	void CleanAllFeatureBuffers();

	// 初始化insert的所有featurebuffer
	//void BlockIniAttributes();

	void Bspline(int n, int t, DwgPoint* control, DwgPoint* output, int num_output);

	// 获得错误信息
	CString CatchErrorInfo();

	//打开日志文件
	//void OpenLogFile();

	ITextElement* MakeTextElementByStyle(CString strText, double dblAngle,
		double dblHeight, double dblX,
		double dblY, double ReferenceScale, esriTextHorizontalAlignment horizAlign, esriTextVerticalAlignment vertAlign);

	//初始化重命名记录集
	void InitRenameLayers(ITable* pRenameTable);

	//插入注记实体
	void InsertAnnoFeature(OdRxObject* pEnt);

	//插入属性实体
	void InsertDwgAttribFeature(OdRxObject* pEnt);

	//写日志文件
	void WriteLog(CString sLog);

	//判断增加附加关联字段
	BOOL CompareCodes(IFeatureBuffer*& pFeatureBuffer);

	// 释放接口对象
	void ReleaseAOs(void);

	//初始化接口
	void InitAOPointers(void);

	//添加扩展属性字段
	void AddExtraFields(CStringList* pRegapps);

public:

	//注记要素字体
	IFontDisp* m_pAnnoTextFont;

	CString m_strDwgName;

	long m_lEntityNum;

	long m_lUnReadEntityNum;

	// 记录 insert类型个数
	long m_lBlockNum;

	// 记录insert类型的头信息
	// 判断是否是第一个insert
	long m_bn;	

	// 记录第一个insert的名字、层、颜色、线型
	CString m_szblockname, m_szBlockLayer, m_szBlockColor, m_szBlockLT;

	//目标库工作空间
	IWorkspace* m_pTargetWS;

	//定义点线面文本FeatureClass
	IFeatureClass* m_pFeatClassPoint;
	IFeatureClass* m_pFeatClassLine;
	IFeatureClass* m_pFeatClassPolygon;
	//注记图层
	IFeatureClass* m_pAnnoFtCls;
	//文本点
	IFeatureClass* m_pFeatClassText;

	//扩展属性数据表
	ITable* m_pExtendTable; 

	// 定义相应的cursor及buffer
	IFeatureCursor* m_pPointFeatureCursor;
	IFeatureCursor* m_pLineFeatureCursor;
	IFeatureCursor* m_pPolygonFeatureCursor;
	IFeatureCursor* m_pAnnoFeatureCursor;
	IFeatureCursor* m_pTextFeatureCursor;
	IEsriCursor* m_pExtentTableRowCursor;


	
	IFeatureBuffer* m_pPointFeatureBuffer;
	IFeatureBuffer* m_pLineFeatureBuffer;
	IFeatureBuffer* m_pPolygonFeatureBuffer;
	IFeatureBuffer* m_pAnnoFeatureBuffer;
	IFeatureBuffer* m_pTextFeatureBuffer;
	IRowBuffer* m_pExtentTableRowBuffer;
	
	// table表
	CComVariant m_vID;
	CComVariant m_TableId;
	// 写入数据库的间隔数
	long m_StepNum;
	CString m_sEntityHandle;

	//日志文件保存路径
	CString m_sLogFilePath;
	

protected:

	//创建目标要素类
	BOOL CreateTargetAllFeatureClass();

	//删除已存在的要素类
	void CheckDeleteFtCls(IFeatureWorkspace* pFtWS, CString sFtClsName);

	// 定义点线面文本Field
	HRESULT CreateDwgPointFields(ISpatialReference* ipSRef, IFields** ppfields);
	HRESULT CreateDwgLineFields(ISpatialReference* ipSRef, IFields** ppfields);
	HRESULT CreateDwgPolygonFields(ISpatialReference* ipSRef, IFields** ppfields);
	HRESULT CreateDwgTextPointFields(ISpatialReference* ipSRef, IFields** ppfields);
	HRESULT CreateDwgAnnotationFields(ISpatialReference* ipSRef, IFields** ppfields);


	// 创建ExtendTable表
	HRESULT CreateExtendTable(IFeatureWorkspace* pFeatWorkspace, BSTR bstrName, ITable** pTable);	
	// 创建FTC
	HRESULT CreateDatasetFeatureClass(IFeatureWorkspace* pFWorkspace, IFeatureDataset* pFDS, IFields* pFields, BSTR bstrName, esriFeatureType featType, IFeatureClass*& ppFeatureClass);
	
	//从FeatureBuffer中取给定字段的值
	CString GetFeatureBufferFieldValue(IFeatureBuffer*& pFeatureBuffer, CString sFieldName);

	//添加扩展属性值
	BOOL PutExtendAttribsValue(IFeatureBuffer*& pFtBuf, OdResBuf* xIter);
	
	// 是否存在FeatureClass要重置范围
	BOOL IsResetDomain(IFeatureWorkspace* pFWorkspace, CString szFCName);
	
	// 重置存在FeatureClass范围
	void ResetDomain(IFeatureWorkspace* pFWorkspace, CString szFCName, ISpatialReference* ipSRef);
	
	// 由FeatureClass得到GeotryDef
	void GetGeometryDef(IFeatureClass* pClass, IGeometryDef** pDef);

	// spline线算法
	void ComputeIntervals(int* u, int n, int t);
	double Blend(int k, int t, int* u, double v);
	void ComputePoint(int* u, int n, int t, double v, DwgPoint* control, DwgPoint* output);
	void ReleaseFeatureBuffer(IFeatureBufferPtr& pFeatureBuffer);

	HRESULT EndLoadOnlyMode(IFeatureClass*& pTargetClass);
	HRESULT BeginLoadOnlyMode(IFeatureClass*& pTargetClass);

	//创建注记图层
	IFeatureClass* CreateAnnoFtCls(IWorkspace* pWS, CString sAnnoName, IFields* pFields);

	// 释放接口指针
	int ReleasePointer(IUnknown*& pInterface);

private:
	//  与dwg相关的
	// 读dwg头信息
	void ReadHeader(OdDbDatabase* pDb);

	// 读dwgtable信息
	void ReadSymbolTable(OdDbObjectId tableId);

	//读dwg层信息
	void ReadLayers(OdDbDatabase* pDb);

	//读dwg扩展信息
	void ReadExtendAttribs(OdResBuf* xIter, CString sEntityHandle);

	// 读每个实体
	void ReadEntity(OdDbObjectId id);

	// 读块信息（包含所有实体）
	void ReadBlock(OdDbDatabase* pDb);

	// 读dwg文件（包含所有块信息）
	//日志记录器
	CLogRecorder* m_pLogRec;

	//不读的CAD图层
	CStringList m_UnReadLayers;

	//是否已经创建目标要素类
	BOOL m_bFinishedCreateFtCls;

};	
#endif
