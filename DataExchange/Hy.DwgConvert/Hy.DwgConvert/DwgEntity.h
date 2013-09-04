// DwgEntity.h : CDwgEntity 的声明

#pragma once
#include "resource.h"       // 主符号


#include "HyDwgConvert_i.h"



#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Windows CE 平台(如不提供完全 DCOM 支持的 Windows Mobile 平台)上无法正确支持单线程 COM 对象。定义 _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA 可强制 ATL 支持创建单线程 COM 对象实现并允许使用其单线程 COM 对象实现。rgs 文件中的线程模型已被设置为“Free”，原因是该模型是非 DCOM Windows CE 平台支持的唯一线程模型。"
#endif

using namespace ATL;


// CDwgEntity

class ATL_NO_VTABLE CDwgEntity :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CDwgEntity, &CLSID_DwgEntity>,
	public IDispatchImpl<IDwgEntity, &IID_IDwgEntity, &LIBID_HyDwgConvert, /*wMajor =*/ 1, /*wMinor =*/ 0>

{
public:
	CDwgEntity()
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_DWGENTITY)

DECLARE_NOT_AGGREGATABLE(CDwgEntity)

BEGIN_COM_MAP(CDwgEntity)
	COM_INTERFACE_ENTRY(IDwgEntity)
END_COM_MAP()



	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}

public:
	void setInnerEntity (OdDbEntityPtr oddbEntity);
	OdDbEntityPtr getInnerEntity();

private:
	CComBSTR m_GeometryType;
	USHORT m_Color;
	CComBSTR m_Handle;
	IGeometry* m_Shape;
	CComBSTR m_Layer;
	IXData* m_XData;
	OdDbEntityPtr m_InnerEntity;
public:


	STDMETHOD(get_GeometryType)(BSTR*  pVal);
	STDMETHOD(put_GeometryType)(BSTR  newVal);
	STDMETHOD(get_Color)(long* pVal);
	STDMETHOD(put_Color)(long newVal);
	STDMETHOD(get_Handle)(BSTR* pVal);
	STDMETHOD(put_Handle)(BSTR newVal);
	STDMETHOD(get_Shape)(IGeometry** pVal);
	STDMETHOD(put_Shape)(IGeometry* newVal);	
	/*STDMETHOD(get_Layer)(BSTR*  pVal);
	STDMETHOD(put_Layer)(BSTR  newVal);*/
	STDMETHOD(get_Layer)(BSTR* pVal);
	STDMETHOD(put_Layer)(BSTR newVal);
	STDMETHOD(get_XData)(IXData** pVal);
	STDMETHOD(put_XData)(IXData* newVal);
	STDMETHOD(GetXData)(BSTR appName,VARIANT* xType,VARIANT* xValue); /* SAFEARRAY** xType, SAFEARRAY** xValue)*/
};

OBJECT_ENTRY_AUTO(__uuidof(DwgEntity), CDwgEntity)
