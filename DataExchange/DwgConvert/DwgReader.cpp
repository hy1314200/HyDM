// DwgReader.cpp : CDwgReader 的实现

#include "stdafx.h"
#include "DwgReader.h"


//////////////////////////////////////////////////////////////////////////



//////////////////////////////////////////////////////////////////////////
//简要描述 : 解析字符串
//输入参数 :
//返 回 值 :
//修改日志 :
//////////////////////////////////////////////////////////////////////////
void ParseStr(CString sSrcStr, char chrSeparator, CStringList& lstItems)
{
    int iPos = -1;
    while ((iPos = sSrcStr.Find(chrSeparator)) != -1)
    {
        CString stmp = sSrcStr.Mid(0, iPos);
        sSrcStr = sSrcStr.Mid(iPos + 1);

        stmp.Trim();
        if (!stmp.IsEmpty())
        {
            lstItems.AddTail(stmp);
        }

    }
    sSrcStr.Trim();
    if (!sSrcStr.IsEmpty())
    {
        lstItems.AddTail(sSrcStr);
    }
}

// CDwgReader


//////////////////////////////////////////////////////////////////////////
//简要描述 : 取得是否打散块设置
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::get_BreakBlock(VARIANT_BOOL* pVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    *pVal = (m_pDwgReader->m_IsBreakBlock == TRUE ? VARIANT_TRUE : VARIANT_FALSE);

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 设置是否打散块
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::put_BreakBlock(VARIANT_BOOL newVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    m_pDwgReader->m_IsBreakBlock = (newVal == VARIANT_TRUE ? TRUE : FALSE);

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 取得是否读不可视图层设置
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::get_ReadInvisible(VARIANT_BOOL* pVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    *pVal = (m_pDwgReader->m_IsReadInvisible == TRUE ? VARIANT_TRUE : VARIANT_FALSE);

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 设置是否读不可视图层
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::put_ReadInvisible(VARIANT_BOOL newVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    m_pDwgReader->m_IsReadInvisible = (newVal == VARIANT_TRUE ? TRUE : FALSE);

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 取得是否读面数据设置
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::get_ReadPolygon(VARIANT_BOOL* pVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    *pVal = (m_pDwgReader->m_IsReadPolygon == TRUE ? VARIANT_TRUE : VARIANT_FALSE);

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 是否读面数据
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::put_ReadPolygon(VARIANT_BOOL newVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    m_pDwgReader->m_IsReadPolygon = (newVal == VARIANT_TRUE ? TRUE : FALSE);

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 取得是否闭合线构面
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::get_Line2Polygon(VARIANT_BOOL* pVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    *pVal = (m_pDwgReader->m_IsLine2Polygon == TRUE ? VARIANT_TRUE : VARIANT_FALSE);

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 设置是否闭合线构面
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::put_Line2Polygon(VARIANT_BOOL newVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    m_pDwgReader->m_IsLine2Polygon = (newVal == VARIANT_TRUE ? TRUE : FALSE);

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 取得是否读取块的定位点设置
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::get_ReadBlockPoint(VARIANT_BOOL* pVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    *pVal = (m_pDwgReader->m_IsReadBlockPoint == TRUE ? VARIANT_TRUE : VARIANT_FALSE);

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 是否读块的定位点
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::put_ReadBlockPoint(VARIANT_BOOL newVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    m_pDwgReader->m_IsReadBlockPoint = (newVal == VARIANT_TRUE ? TRUE : FALSE);

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 取得是否挂接扩展属性
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::get_JoinXDataAttrib(VARIANT_BOOL* pVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    *pVal = (m_pDwgReader->m_IsJoinXDataAttrs == TRUE ? VARIANT_TRUE : VARIANT_FALSE);

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 设置是否直接挂接扩展属性
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::put_JoinXDataAttrib(VARIANT_BOOL newVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    m_pDwgReader->m_IsJoinXDataAttrs = (newVal == VARIANT_TRUE ? TRUE : FALSE);

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 取得CAD注册应用名
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::get_XDataRegAppNames(BSTR* pVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    *pVal = m_bsRegappNames;

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 设置CAD注册应用名
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::put_XDataRegAppNames(BSTR newVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    m_bsRegappNames = newVal;
    CString sRegs = newVal;

    CStringList pRegApps;

    ParseStr(sRegs, ',', pRegApps);

    m_pDwgReader->AddExtraFields(&pRegApps);

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 取得注记图层显示比例设置
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::get_AnnoScale(SHORT* pVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    *pVal = m_pDwgReader->m_dAnnoScale;

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 设置注记图层显示比例
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::put_AnnoScale(SHORT newVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    m_pDwgReader->m_dAnnoScale = newVal;

    //HRESULT hr;
    //IUnknownPtr pUnk;
    //m_pDwgReader->m_pAnnoFtCls->get_Extension(&pUnk);
    //IAnnoClassAdminPtr pAnnoClassAdmin = pUnk;
    //if (pAnnoClassAdmin != NULL)
    //{
    //    hr = pAnnoClassAdmin->put_ReferenceScale(m_pDwgReader->m_dAnnoScale);
    //    hr = pAnnoClassAdmin->UpdateProperties();
    //}

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 得到不打散的块
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::get_UnBreakBlocks(BSTR* pVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    *pVal = m_bsUnBreakBlocks;

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 设置不打散的块
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::put_UnBreakBlocks(BSTR newVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    m_bsUnBreakBlocks = newVal;
    CString sUnBreakBlocks = newVal;
    ParseStr(sUnBreakBlocks, ',', m_pDwgReader->m_unExplodeBlocks);

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 初始化读CAD参数
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::InitReadDwg(IWorkspace* targetGDB, ISpatialReference* spRef)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    //准备读
    m_pDwgReader->PrepareReadDwg(targetGDB, NULL, spRef);

    try
    {
		CWnd* pWnd = NULL;
        pWnd = CWnd::FromHandle((HWND)(m_parentHandle));
		m_prgDlg.CreateDlg(pWnd);
    }
    catch (...)
    {
    }

    m_pDwgReader->m_pProgressBar = &m_prgDlg.m_progressBar;

    m_prgDlg.Show();

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 读单个CAD文件
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::ReadDwgFile(BSTR sDwgFile)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    CString sDwgFilePath = sDwgFile;
    m_pDwgReader->ReadFile(sDwgFilePath);

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 关闭读操作，释放资源
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::Close(void)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

	try
	{
		m_pDwgReader->CommitReadDwg();
	}
	catch (...)
	{
	}

	m_prgDlg.Close();

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 取得日志文件保存路径
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::get_LogFilePath(BSTR* pVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    *pVal = CComBSTR(m_pDwgReader->m_sLogFilePath);

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 设置日志文件保存路径
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgReader::put_LogFilePath(BSTR newVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    m_pDwgReader->PutLogFilePath(CString(newVal));

    return S_OK;
}

STDMETHODIMP CDwgReader::get_ParentHandle(LONG* pVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    *pVal = m_parentHandle;

    return S_OK;
}

STDMETHODIMP CDwgReader::put_ParentHandle(LONG newVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    m_parentHandle = newVal;

    try
    {
		CWnd* pWnd = NULL;
        pWnd = CWnd::FromHandle((HWND)(m_parentHandle));
		m_prgDlg.SetOwner(pWnd);
		m_prgDlg.CenterWindow(pWnd);
    }
    catch (...)
    {
    }

    return S_OK;
}

STDMETHODIMP CDwgReader::get_CreateAnnotation(VARIANT_BOOL* pVal)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	*pVal = (m_pDwgReader->m_IsCreateAnnotation == TRUE ? VARIANT_TRUE : VARIANT_FALSE);

	return S_OK;
}

STDMETHODIMP CDwgReader::put_CreateAnnotation(VARIANT_BOOL newVal)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	m_pDwgReader->m_IsCreateAnnotation = (newVal == VARIANT_TRUE ? TRUE : FALSE);

	return S_OK;
}

STDMETHODIMP CDwgReader::get_UnbreakblockMode(SHORT* pVal)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	* pVal = m_pDwgReader->m_iUnbreakBlockMode;

	return S_OK;
}

STDMETHODIMP CDwgReader::put_UnbreakblockMode(SHORT newVal)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	m_pDwgReader->m_iUnbreakBlockMode = newVal;

	return S_OK;
}
