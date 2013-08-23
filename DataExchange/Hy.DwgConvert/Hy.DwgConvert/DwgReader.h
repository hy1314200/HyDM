// DwgReader.h : CDwgReader 的声明

#pragma once
#include "resource.h"       // 主符号



#include "HyDwgConvert_i.h"



#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Windows CE 平台(如不提供完全 DCOM 支持的 Windows Mobile 平台)上无法正确支持单线程 COM 对象。定义 _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA 可强制 ATL 支持创建单线程 COM 对象实现并允许使用其单线程 COM 对象实现。rgs 文件中的线程模型已被设置为“Free”，原因是该模型是非 DCOM Windows CE 平台支持的唯一线程模型。"
#endif

using namespace ATL;


// CDwgReader

class ATL_NO_VTABLE CDwgReader :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CDwgReader, &CLSID_DwgReader>,
	public IDispatchImpl<IDwgReader, &IID_IDwgReader, &LIBID_HyDwgConvert, /*wMajor =*/ 1, /*wMinor =*/ 0>
{
public:
	CDwgReader()
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_DWGREADER)

DECLARE_NOT_AGGREGATABLE(CDwgReader)

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
		Close();
	}

private:
	BSTR m_FileName;
	OdDbDatabasePtr m_DwgDatabase;
	OdDbObjectIteratorPtr m_EntityIterator;
public:
	STDMETHOD(GetEntityCount)(LONG* count);
	STDMETHOD(put_FileName)(BSTR* DwgFile);
	STDMETHOD(Init)(VARIANT_BOOL* succeed);
	STDMETHOD(Close)(void);
	STDMETHOD(Read)(IDwgEntity* curEntity);
};

OBJECT_ENTRY_AUTO(__uuidof(DwgReader), CDwgReader)
