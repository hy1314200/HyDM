// DwgReader.h : CDwgReader 的声明

#pragma once
#include "resource.h"       // 主符号

#include "DwgConvert.h"

#include "XDwgDirectReader.h"
//#include "XDBPreProcessDriver.h"

#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Windows CE 平台(如不提供完全 DCOM 支持的 Windows Mobile 平台)上无法正确支持单线程 COM 对象。定义 _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA 可强制 ATL 支持创建单线程 COM 对象实现并允许使用其单线程 COM 对象实现。rgs 文件中的线程模型已被设置为“Free”，原因是该模型是非 DCOM Windows CE 平台支持的唯一线程模型。"
#endif



// CDwgReader

class ATL_NO_VTABLE CDwgReader :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CDwgReader, &CLSID_DwgReader>,
	public IDispatchImpl<IDwgReader, &IID_IDwgReader, &LIBID_DwgConvertLib, /*wMajor =*/ 1, /*wMinor =*/ 0>
{
public:
	CDwgReader()
	{
		//创建读实现实例
		m_pDwgReader = new XDWGReader();
		m_parentHandle = 0;
	}

DECLARE_REGISTRY_RESOURCEID(IDR_DWGREADER)


BEGIN_COM_MAP(CDwgReader)
	COM_INTERFACE_ENTRY(IDwgReader)
	COM_INTERFACE_ENTRY(IDispatch)
END_COM_MAP()


	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
		//释放读实例
		if (m_pDwgReader != NULL)
		{
			delete m_pDwgReader;
		}
	}

private:

	//CAD读取实现类
	XDWGReader* m_pDwgReader;

	//进度条窗口
	CDlgProgressBar m_prgDlg;

	//父窗口句柄
	long m_parentHandle;

	CComBSTR m_bsRegappNames;
	CComBSTR m_bsUnBreakBlocks;

	
public:
	STDMETHOD(get_BreakBlock)(VARIANT_BOOL* pVal);
	STDMETHOD(put_BreakBlock)(VARIANT_BOOL newVal);
	STDMETHOD(get_ReadInvisible)(VARIANT_BOOL* pVal);
	STDMETHOD(put_ReadInvisible)(VARIANT_BOOL newVal);
	STDMETHOD(get_ReadPolygon)(VARIANT_BOOL* pVal);
	STDMETHOD(put_ReadPolygon)(VARIANT_BOOL newVal);
	STDMETHOD(get_Line2Polygon)(VARIANT_BOOL* pVal);
	STDMETHOD(put_Line2Polygon)(VARIANT_BOOL newVal);
	STDMETHOD(get_ReadBlockPoint)(VARIANT_BOOL* pVal);
	STDMETHOD(put_ReadBlockPoint)(VARIANT_BOOL newVal);
	STDMETHOD(get_JoinXDataAttrib)(VARIANT_BOOL* pVal);
	STDMETHOD(put_JoinXDataAttrib)(VARIANT_BOOL newVal);
	STDMETHOD(get_XDataRegAppNames)(BSTR* pVal);
	STDMETHOD(put_XDataRegAppNames)(BSTR newVal);
	STDMETHOD(get_AnnoScale)(SHORT* pVal);
	STDMETHOD(put_AnnoScale)(SHORT newVal);
	STDMETHOD(get_UnBreakBlocks)(BSTR* pVal);
	STDMETHOD(put_UnBreakBlocks)(BSTR newVal);
	STDMETHOD(InitReadDwg)(IWorkspace* targetGDB, ISpatialReference* spRef);
	STDMETHOD(ReadDwgFile)(BSTR sDwgFile);
	STDMETHOD(Close)(void);
	STDMETHOD(get_LogFilePath)(BSTR* pVal);
	STDMETHOD(put_LogFilePath)(BSTR newVal);
	STDMETHOD(get_ParentHandle)(LONG* pVal);
	STDMETHOD(put_ParentHandle)(LONG newVal);
	STDMETHOD(get_CreateAnnotation)(VARIANT_BOOL* pVal);
	STDMETHOD(put_CreateAnnotation)(VARIANT_BOOL newVal);
	STDMETHOD(get_UnbreakblockMode)(SHORT* pVal);
	STDMETHOD(put_UnbreakblockMode)(SHORT newVal);
};

OBJECT_ENTRY_AUTO(__uuidof(DwgReader), CDwgReader)
