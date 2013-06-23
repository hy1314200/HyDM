// XDwgWriter.h: interface for the XDwgWriter class.
//
//////////////////////////////////////////////////////////////////////


#ifndef AFX_XDWGWriter_H____INCLUDED_
#define AFX_XDWGWriter_H____INCLUDED_

#include "SharedUsed.h"


//扩展属性Map
//扩展属性注册应用名称+数据库中的扩展属性字段名称
typedef CTypedPtrMap<CMapStringToPtr, CString, CStringList*> XDataAttrLists;



class XDwgWriter
{
public:
    XDwgWriter();
    virtual ~XDwgWriter();


	//设置并生成日志文件
	void PutLogFilePath(CString sLogFile);

    //指定生成的DWG文件路径名称
    BOOL PrepareOutPut(LPCTSTR szCadOutFile);

    //把Feature Class写入DWG文件, pExtraAttribs:定义扩展属性名称和，对应的字段
    void FeatureClass2Dwgfile(IFeatureClass* pFeatureClass, XDataAttrLists* pExtraAttribs = NULL);

    //把SelectionSet写入DWG文件, pExtraAttribs:定义扩展属性名称和，对应的字段
    void SelectionSet2Dwgfile(ISelectionSet* pSelectionSet, XDataAttrLists* pExtraAttribs = NULL);


    //把FeatureDataset写入DWG文件, 不写入扩展属性
    void FeatureDataset2DwgFile(IFeatureDataset* pFeatureDataset);

    //把Cursor写入DWG文件, 不写入扩展属性
    void Cursor2Dwgfile(IFeatureCursor* pFeatureCursor,
                        CString sFeatureClassName,
                        long lFeatureNums);

    //指定模版文件
    BOOL DoModalGetOutDwgSetting();

    //完成写入后调用
    void FlushOutPut();

    //保存日志
    void SaveLogFile(CString sFilePath);

    //输出范围
    IEnvelopePtr m_ipOutExtent;
    // dwg的模板文件
    CString m_szCadTempFile;
    //打开日志文件
    void OpenLogFile();
    // 是否输出扩展属性参数
    BOOL m_isOutXData;

    //当前输出要素类名称
    CString m_sFeatureClassName;


    //是否把角度转化为弧度, 默认是
    BOOL m_bConvertAngle;

    //写日志文件
    void WriteLog(CString sLog);

	CTextProgressCtrl* m_pProgressBar;

	//写入到CAD的图层名称
	CString m_sDwgLayer;



	//属性对照字段
	CString m_sCompareField;

	//属性对照表
	ITable* m_pCompareTable;


	//保存FeatureClass应用名与需要写入的扩展属性字段
	XDataAttrLists* m_pXDataAttrLists;


private:

	//是否成功初始化
	BOOL m_bInit;

    // 模板文件后缀DXF或DWG
    CString m_FileExt;
    // Dwg输出文件名
    char m_destfile[265];
    // 日志文件
    CStringList m_LogInfo;
    CString GetFieldValueAsString(CComVariant& var);
    BOOL GetAttribute(IFeature* pFeature, CString sAttrName, CString& sAttrValue);

    OdDbObjectId addLayer(OdDbDatabase* pDb, LPCTSTR layerName);
    BOOL FindLayerByName(OdDbDatabase* pDb, char* name, OdDbObjectId* ObId);
    BOOL FindLinttypeByName(OdDbDatabase* pDb, char* name, OdDbObjectId* ObId);
    BOOL FindBlockByName(OdDbDatabase* pDb, char* name, OdDbObjectId* ObId);
    BOOL FindTextStyleByName(OdDbDatabase* pDb, char* name, OdDbObjectId* ObId);

    void WriteInsert(IFeature* pFeature,
                     OdDbDatabase* pDb,
                     const OdDbObjectId& layerId,
                     const OdDbObjectId& blockId);

    void WritePoint(IFeature* pFeature,
                    OdDbDatabase* pDb,
                    const OdDbObjectId& layerId,
                    const OdDbObjectId& styleId);


	//写注记数据
    void WriteAnnotation(IFeature* pFeature, OdDbDatabase* pDb, const OdDbObjectId& layerId, const OdDbObjectId& styleId);


    void WriteText(IFeature* pFeature,
                   OdDbDatabase* pDb,
                   const OdDbObjectId& layerId,
                   const OdDbObjectId& styleId);

    void WriteGeometryPoint(IFeature* pFeature,
                            OdDbDatabase* pDb,
                            const OdDbObjectId& layerId,
                            const OdDbObjectId& styleId);
    void WriteLine(IFeature* pFeature,
                   OdDbDatabase* pDb,
                   const OdDbObjectId& layerId,
                   const OdDbObjectId& styleId);
    void WriteCircularArc(IFeature* pFeature,
                          IGeometry* pShape,
                          OdDbDatabase* pDb,
                          const OdDbObjectId& layerId,
                          const OdDbObjectId& styleId);
    void WriteEllipticArc(IFeature* pFeature,
                          OdDbDatabase* pDb,
                          const OdDbObjectId& layerId,
                          const OdDbObjectId& styleId);
    void WriteMultipoint(IFeature* pFeature,
                         OdDbDatabase* pDb,
                         const OdDbObjectId& layerId,
                         const OdDbObjectId& styleId);
    void WriteBezier3Curve(IFeature* pFeature,
                           OdDbDatabase* pDb,
                           const OdDbObjectId& layerId,
                           const OdDbObjectId& styleId);
    void WritePath(IFeature* pFeature,
                   IGeometry* pShape,
                   OdDbDatabase* pDb,
                   const OdDbObjectId& layerId,
                   const OdDbObjectId& styleId,
                   int isClosed);
    void WriteBag(IFeature* pFeature,
                  OdDbDatabase* pDb,
                  const OdDbObjectId& layerId,
                  const OdDbObjectId& styleId);
    void WritePolygon(IFeature* pFeature,
                      OdDbDatabase* pDb,
                      const OdDbObjectId& layerId,
                      const OdDbObjectId& styleId);
    void WriteRing(IFeature* pFeature,
                   OdDbDatabase* pDb,
                   const OdDbObjectId& layerId,
                   const OdDbObjectId& styleId);

    void WritePolyline(IFeature* pFeature,
                       OdDbDatabase* pDb,
                       const OdDbObjectId& layerId,
                       const OdDbObjectId& styleId,
                       BOOL isClosed = FALSE);

    OdResBuf* appendXDataPair(OdResBuf* pCurr, int code);
    void addExtraAttribs(IFeature* pFeature, OdDbEntity* pEntity);
    BOOL GetDefaultTempFile();

private:
    // 获得错误信息
    CString CatchErrorInfo();
	OdDbHatch::HatchPatternType GetHatchPatternName(CString csText, CString& csPatternName);

protected:
    //保存整个DWG文件需要注册的扩展属性应用名称
    CStringList m_registeredAppNames;

    //注册扩展属性应用名称
    void RegAppName(OdDbDatabase* pDb, CString sRegAppName);

    //清除扩展属性名和对应的字段MAP
    void ClearXDataAttrLists();

	//日志记录器
	CLogRecorder* m_pLogRec;

	//日志文件路径
	CString m_sLogFilePath;

public:
	BOOL m_bWidthCompareField2;
	CString m_csConfigField2;
	CString m_csGdbField2;
};

#endif