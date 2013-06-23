// XDBPreProcessDriver.h: interface for the XDBPreProcessDriver class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_XDBPREPROCESSDRIVER_H__858DD9F2_3E02_4CE2_8DF2_0A572D8E2465__INCLUDED_)
#define AFX_XDBPREPROCESSDRIVER_H__858DD9F2_3E02_4CE2_8DF2_0A572D8E2465__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "XJoinExtendTable.h"
#include "XDWGDirectReader.h"
#include "SharedUsed.h"

//////////////////////////////////////////////////////////////////////////
typedef CTypedPtrList<CPtrList, IDatasetName*>  CDsNameList;


class AFX_CLASS_EXPORT XDBPreProcessDriver  
{
public:

	CLogRecorder* m_pLogRec;

	IPolygon* CreatePolygon(IPolyline* pPLine, ISpatialReference* pSpRef);

	HRESULT PostBuildPolygon2(CString sPolygonLayerName, IFeatureClassPtr pInFtCls, CString sAreaField);


	//校准图层别名
	HRESULT AdjustSdeLayerAlias();

	//防止有的时候界面无法刷新的问题
	void DoEvents();

	void GetFtClsNames(IWorkspace* pWorkspace, CDsNameList& lstNames);

	//修改要素集和要素类名称 Creator: zl
	HRESULT ChangeDatasetAndFtClsName(CString sPrefix, CString sPrjName);

	//构面后处理 by zl
	HRESULT DoPolygonPostProcess();

	void CopyFeatureUIDTable();

	XDBPreProcessDriver();
	virtual ~XDBPreProcessDriver();


	//要处理cad文件队列
	CStringArray CadArrays;

	IFeatureWorkspace* m_pSysFtWs;

	CTextProgressCtrl* pTextProgressCtrl;


	//cad数据格式转换以及模版对照后的工作空间
	IWorkspace* m_pInWS;

	//数据分层的工作空间（预处理数据的最终工作空间
	IWorkspace* m_pOutWS;

	//数据转换后的目标数据的坐标系统
	ISpatialReferencePtr ipSpatialReference;

	//格式转换的目标要素集
	IFeatureDatasetPtr ipFeatureDataset;

	//分层数据所在工作空间Name
	IWorkspaceNamePtr ipWorkspace2Name;

	//对照表内表名字枚举
	IEnumDatasetNamePtr m_pEnumConfigDatasetName;

	//连接扩展表类
	//XJoinExtendTable* joinExdDriver;


	//扩展属性表
	ITable* m_ipExtendTable;

	//对照表
	ITablePtr ipCompareTable;

	//日志内容列表
	CStringList m_LogList;

	//格式转换和模版对照
//	BOOL ReadCADAndConvert();

	//自动分层
	BOOL AutoSplitLayers(IWorkspace* pInWS, IWorkspace* pOutWS);

	//挂接扩展属性
	BOOL JoinExtendTable();

	//获取连接扩展属性表所需要的对照表
	ITablePtr GetExtendCompareTable(ITablePtr ipTable);

	//记录操作内容
	void WriteLog(CString sLog);

	//保存日志
	void SaveLogList(BOOL bShow =TRUE);

private:

	//转换VARIANT到CString
	CString GetStringByVar(VARIANT var);

	CString GetSdeFtClsName(CComBSTR bsFtClsName);

protected:
	ITable* GetExtendFieldsConfigTable(CString sLayerName);
//	IFeatureWorkspacePtr GetSysWorkspace();
	void GetExtraAttribRegAppNames(CMapStringToString& mapRegAppNames);
	
public:
	bool SplitOneLayer(CMapStringToString* pSplitLayerNames, IFeatureClass* pInFtCls, IWorkspace* pTargetFtWS);
public:

	void ParseStr(CString sSrcStr, char chrSeparator, CStringList& lstItems);


	// 由线构面
	void BuildPolygon(void);

	void CopyFeatureAttr(IFeaturePtr pSourceFeature, IFeaturePtr pTargetFeature);
public:
	// //创建注记类型的要素类
	HRESULT CreateAnnoFtCls(IFeatureDataset* pTarFDS, BSTR bsAnnoName, IFeatureClass** ppAnnoFtCls);

	HRESULT CreateAnnoFtCls(IWorkspace* pWS, BSTR bsAnnoName, IFeatureClass** ppAnnoFtCls);

	//创建注记图层
	HRESULT CreateAnnoFtCls(IFeatureDatasetPtr pTarFDS, BSTR bsAnnoName, IFieldsPtr pFields, IFeatureClass** ppAnnoFtCls);
public:
	// CAD文件注记图层数据转换
	BOOL CAD_AnnotationConvert(IWorkspace* pTargetWS, IDataset* pTargetDataset, CString sDwgFilePath, CString sShowedFilePath = "");

	//得到文件所在的目录
	CString GetFileDirectory(const CString& sFullPath);

	// 生成注记Element
	ITextElement* MakeTextElementByStyle(CString strStyle, CString strText, double dblAngle,
		double dblHeight, double dblX,
		double dblY, double ReferenceScale);

	//对注记图层分层
	bool SplitAnnotationLayer(CString sBaseLayerName, CMapStringToString* pSplitLayerNames, IFeatureClass* pInFtCls, IWorkspace* pTargetWS);

public:
	// CAD数据读取类
	XDWGReader* m_pDwgReader;
public:

	//线构面后期处理
	HRESULT PostBuildPolygon(IFeatureClassPtr pInFtCls);
public:
	// 得到所有的注册应用名
	void GetRegAppNames(CStringList& lstAppNames);
public:
	// 挂接外接表
	void JoinAddinTable(ITablePtr pExtraTable);

	//复制要素
	void CopyFeature(IFeaturePtr pSrcFeat, IFeaturePtr& pDestFeat);

	// 拷贝数据到目标库
	bool CopyToTargetDB(IFeatureWorkspacePtr pSrcFtWS, IFeatureWorkspacePtr pTargetFtWS);
	// 挂接扩展属性
	bool JoinExtendTable2(void);
	void PrgbarRange(int iLower, int iUpper);
	void PrgbarSetPos(int iPos);
	void PrgbarSetText(CString sText);
	void PrgbarStepIt(void);
	void SaveLogFile(CString sFilePath);
};

#endif // !defined(AFX_XDBPREPROCESSDRIVER_H__858DD9F2_3E02_4CE2_8DF2_0A572D8E2465__INCLUDED_)