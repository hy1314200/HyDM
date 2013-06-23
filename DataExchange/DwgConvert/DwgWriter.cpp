// DwgWriter.cpp : CDwgWriter 的实现

#include "stdafx.h"
#include "DwgWriter.h"
#include "XMLFile.h"

// CDwgWriter


//////////////////////////////////////////////////////////////////////////
//简要描述 : 写要素类到CAD文件
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgWriter::FeatureClass2Dwgfile(IFeatureClass* pFtCls)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    // TODO: 在此添加实现代码

    if (pFtCls == NULL) return S_FALSE;

    //m_pgrsDlg.Show();

    CString sLog;

    CString sFtClsName;
    CComBSTR bsFtClsName;
    IDatasetPtr pDataset = pFtCls;
    if (pDataset != NULL)
    {
        pDataset->get_Name(&bsFtClsName);
    }
    else
    {
        pFtCls->get_AliasName(&bsFtClsName);
    }

    sFtClsName = bsFtClsName;

    int pos = sFtClsName.ReverseFind('.');
    if (pos > 0)
    {
        sFtClsName = sFtClsName.Right(sFtClsName.GetLength() - pos - 1);
    }

    try
    {
        if (m_XDataCfgs.GetCount() == 0) //没有扩展属性
        {
            m_dwgWriter.FeatureClass2Dwgfile(pFtCls, NULL);
        }
        else
        {
            XDataAttrLists* pXDataAttrLists = NULL;

            m_XDataCfgs.Lookup(sFtClsName, pXDataAttrLists);
            m_dwgWriter.FeatureClass2Dwgfile(pFtCls, pXDataAttrLists);
        }
    }
    catch (...)
    {
        sLog = sFtClsName + "图层写入到CAD文件时出错。";
        m_dwgWriter.WriteLog(sLog);

        m_pgrsDlg.Hide();
        return S_FALSE;
    }

    //m_pgrsDlg.Hide();

    return S_OK;
}


//////////////////////////////////////////////////////////////////////////
//简要描述 : 初始化写DWG文件
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgWriter::InitWriteDwg(BSTR sDwgFile, BSTR sTemplateFile)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    CComBSTR bsTempletFile = sTemplateFile;

    USES_CONVERSION;
    CString sOutFile = W2A(sDwgFile);
    m_dwgWriter.m_szCadTempFile = bsTempletFile;
    m_dwgWriter.PrepareOutPut(sOutFile);

    m_pgrsDlg.CreateDlg();
    m_dwgWriter.m_pProgressBar = &m_pgrsDlg.m_progressBar;
    m_pgrsDlg.Show();

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 设置扩展属性配置文件
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgWriter::put_XDataXMLConfigFile(BSTR sXMLFile)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    if (sXMLFile == NULL) return S_OK;

    CString sXml = sXMLFile;
    if (sXml.IsEmpty()) return S_OK;

    m_XDataCfgs.RemoveAll();

    try
    {
        CXMLFile xmlfile;
        xmlfile.load(sXml);
        MSXML2::IXMLDOMNodePtr pNode;
        MSXML2::IXMLDOMNodePtr pExtAttrNode = NULL;
        xmlfile.GetNode("LAYERS", pNode);
        if (pNode == NULL)
        {
            //AfxMessageBox("XML配置文件不正确，请检查。");
            m_dwgWriter.WriteLog("XML配置文件不正确，请检查。");
            return S_FALSE;
        }
        pNode = pNode->GetfirstChild();
        if (pNode == NULL)
        {
            //AfxMessageBox("XML配置文件不正确，请检查。");
            m_dwgWriter.WriteLog("XML配置文件不正确，请检查。");
            return S_FALSE;
        }
        CComBSTR bsNodeName;
        CComBSTR bsExtAttrs;
        CString sLayerName;
        CString sRegAppName;
        CString sExtAttrs;
        while (pNode != NULL)
        {
            //得到图层名
            pNode->get_nodeName(&bsNodeName);
            sLayerName = bsNodeName;

            //去掉前面的_前缀,解决数字开头的节点问题
            CString sSign = "";
            sSign = sLayerName.Mid(0, 1);
            if (sSign.CompareNoCase("_") == 0)
            {
                sLayerName = sLayerName.Mid(1);
            }

            XDataAttrLists* pExtAttrs = new XDataAttrLists();
            //得到图层下的注册应用名
            if (pNode->hasChildNodes())
            {
                pExtAttrNode = pNode->GetfirstChild();
                while (pExtAttrNode != NULL)
                {
                    pExtAttrNode->get_nodeName(&bsNodeName);
                    sRegAppName = bsNodeName;

                    //去掉前面的_前缀,解决数字开头的节点问题
                    sSign = sRegAppName.Mid(0, 1);
                    if (sSign.CompareNoCase("_") == 0)
                    {
                        sRegAppName = sRegAppName.Mid(1);
                    }

                    pExtAttrNode->get_text(&bsExtAttrs);
                    sExtAttrs = bsExtAttrs;
                    CStringList* pAttrLst = new CStringList();
                    //解析注册应用名下的属性字段名称
                    CString sAttr;
                    int iPos  = sExtAttrs.Find(',');
                    while (iPos > 0)
                    {
                        sAttr = sExtAttrs.Mid(0, iPos);
                        sExtAttrs = sExtAttrs.Mid(iPos + 1);
                        if (!sAttr.IsEmpty())
                        {
                            pAttrLst->AddTail(sAttr);
                        }
                        iPos  = sExtAttrs.Find(',');
                    }
                    if (iPos == -1)
                    {
                        if (!sExtAttrs.IsEmpty())
                        {
                            pAttrLst->AddTail(sExtAttrs);
                        }
                    }
                    pExtAttrs->SetAt(sRegAppName, pAttrLst);
                    //得到下一个注册应用名的配置
                    pExtAttrNode = pExtAttrNode->GetnextSibling();
                }
            }

            m_XDataCfgs.SetAt(sLayerName, pExtAttrs);
            //得到下一个图层的扩展属性的配置
            pNode = pNode->GetnextSibling();
        }
    }
    catch (...)
    {
		m_dwgWriter.WriteLog("解析XML文件出错，请检查。");
        return S_FALSE;
    }

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 设置日志保存路径
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgWriter::put_LogFilePath(BSTR newVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    m_dwgWriter.PutLogFilePath(newVal);

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 完成对DWG文件的写入操作，清理相关资源
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgWriter::Close(void)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    m_pgrsDlg.Close();

    m_dwgWriter.WriteLog("完成CAD文件的写入。");

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 游标写入DWG文件
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgWriter::Cursor2Dwgfile(BSTR sFeatureClass, IFeatureCursor* pFtCur, LONG numFeatures)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    CString sFtCls = sFeatureClass;

    CString sLog;
    sLog.Format("开始把%s图层的指定数据写入CAD文件。", sFtCls);
    m_dwgWriter.WriteLog(sLog);

	try
	{
		m_XDataCfgs.Lookup(sFtCls, m_dwgWriter.m_pXDataAttrLists);
		m_dwgWriter.Cursor2Dwgfile(pFtCur, sFeatureClass, numFeatures);
	}
	catch (...)
	{
		sLog = sFtCls + "图层写入到CAD文件时出错。";
		m_dwgWriter.WriteLog(sLog);
		m_pgrsDlg.Hide();
		return S_FALSE;
	}

    sLog.Format("完成对%s图层数据的写入，写入要素个数：%d", sFtCls, numFeatures);
    m_dwgWriter.WriteLog(sLog);

    return S_OK;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 设置对照表
//输入参数 :
//返 回 值 :
//日    期 : 2008/10/29,BeiJing.
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgWriter::SetCompareTable(BSTR sCompareField, ITable* pCompareTable)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    m_dwgWriter.m_sCompareField = sCompareField;
    m_dwgWriter.m_pCompareTable = pCompareTable;

    return S_OK;

}


//////////////////////////////////////////////////////////////////////////
//简要描述 : 游标写入DWG文件指定的图层
//输入参数 :
//返 回 值 :
//
//
//修改日志 :
//////////////////////////////////////////////////////////////////////////
STDMETHODIMP CDwgWriter::Cursor2DwgLayer(BSTR sFeatureClass, IFeatureCursor* pFtCur, LONG numFeatures, BSTR sDwgLayer)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

    CString sFtCls = sFeatureClass;
    CString sCadLayer = sDwgLayer;

    CString sLog;
    sLog.Format("开始把%s图层的数据写入到CAD的%s图层。", sFtCls, sCadLayer);
    m_dwgWriter.WriteLog(sLog);

	try
	{
		m_dwgWriter.m_sDwgLayer = sDwgLayer;
		m_XDataCfgs.Lookup(sFtCls, m_dwgWriter.m_pXDataAttrLists);
		m_dwgWriter.Cursor2Dwgfile(pFtCur, sFeatureClass, numFeatures);
	}
	catch (...)
	{
		sLog = sFtCls + "图层写入到CAD文件时出错。";
		m_dwgWriter.WriteLog(sLog);
		m_pgrsDlg.Hide();
		return S_FALSE;
	}

    sLog.Format("完成把%s图层数据写入到CAD的%s图层，写入要素个数：%d", sFtCls, sCadLayer, numFeatures);
    m_dwgWriter.WriteLog(sLog);

    return S_OK;
}

STDMETHODIMP CDwgWriter::SetCompareField2(BSTR sConfigField, BSTR sGdbField)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	m_dwgWriter.m_bWidthCompareField2 = FALSE;
	CString csConfigField = sConfigField;
	CString csGdbField = sGdbField;
	if (csConfigField.IsEmpty() || csGdbField.IsEmpty())
	{
		return FALSE;
	}

	m_dwgWriter.m_bWidthCompareField2 = TRUE;
	m_dwgWriter.m_csConfigField2 = csConfigField;
	m_dwgWriter.m_csGdbField2 = csGdbField;

	return S_OK;
}
