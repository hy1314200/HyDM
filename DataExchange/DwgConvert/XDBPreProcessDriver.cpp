// XDBPreProcessDriver.cpp: implementation of the XDBPreProcessDriver class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "XDBPreProcessDriver.h"
#include <math.h>

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#define new DEBUG_NEW
#endif



//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

XDBPreProcessDriver::XDBPreProcessDriver()
{
    m_pInWS = NULL;
    m_pOutWS = NULL;
    //joinExdDriver = NULL;
    m_ipExtendTable = NULL;
    ipCompareTable = NULL;

    pTextProgressCtrl = NULL;

    m_pSysFtWs = NULL;
}

XDBPreProcessDriver::~XDBPreProcessDriver()
{
    //if (joinExdDriver)
    //    delete joinExdDriver;
}

//类似vb中的函数，释放系统控权，防止有的时候界面无法刷新的问题
void XDBPreProcessDriver::DoEvents()
{
    MSG message;
    for (int i = 0; i < 10; i++)
    {
        if (::PeekMessage(&message, NULL, 0, 0, PM_REMOVE))
        {
            ::TranslateMessage(&message);
            ::DispatchMessage(&message);
        }
    }
}

/************************************************************************
简要描述 :写处理日志
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
void XDBPreProcessDriver::WriteLog(CString sLog)
{
    if (!sLog.IsEmpty())
    {
        CTime dtCur = CTime::GetCurrentTime();
        CString sLogTime = dtCur.Format("%Y/%m/%d %H:%M:%S");
        sLog = sLogTime + "-" + sLog;
        m_LogList.AddTail(sLog);
    }
}

void XDBPreProcessDriver::SaveLogList(BOOL bShow/*=TRUE */)
{

    if (m_LogList.GetCount() > 0)
    {
        CTime dtCur = CTime::GetCurrentTime();
        CString sName = dtCur.Format("%y%m%d_%H%M%S");
        CString sLogFileName;
        sLogFileName.Format("%sDwg转换日志_%s.log", GetLogPath(), sName);

        CStdioFile f3(sLogFileName, CFile::modeCreate | CFile::modeWrite | CFile::typeText);
        for (POSITION pos = m_LogList.GetHeadPosition(); pos != NULL;)
        {
            f3.WriteString(m_LogList.GetNext(pos) + "\n");
        }
        f3.Close();
        WinExec("Notepad.exe " + sLogFileName, SW_SHOW);
        m_LogList.RemoveAll();
    }

}

/************************************************************************
简要描述 : 分层处理
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
BOOL XDBPreProcessDriver::AutoSplitLayers(IWorkspace* pInWS, IWorkspace* pOutWS)
{
    //CWaitCursor w;
    CString sLogInfo;
    sLogInfo.Format("2 开始自动分层");
    WriteLog(sLogInfo);

    PrgbarSetText("开始自动分层前的准备处理...");
    PrgbarRange(0, 4);
    PrgbarSetPos(0);
    PrgbarStepIt();

    CMapStringToString mapLayers;

    HRESULT hr;
    ITablePtr pFtClsLayers;
    hr = m_pSysFtWs->OpenTable(CComBSTR("ALL_LAYERS"), &pFtClsLayers);

    long numLayers;
    hr = pFtClsLayers->RowCount(NULL, &numLayers);

    if (numLayers <= 0) return FALSE;

    long lLayerName;
    //long lLayerAlias;
    pFtClsLayers->FindField(CComBSTR("GDB_LAYER"), &lLayerName);
    //pFtClsLayers->FindField(CComBSTR("LAYER_ALIAS"), &lLayerAlias);

    IEsriCursorPtr pRowCursor;
    hr = pFtClsLayers->Search(NULL, VARIANT_FALSE, &pRowCursor);

    IEsriRowPtr pRowLayerName;
    CComVariant vtVal;
    CString sLayerName;
    CString sLayerAlias;

    while (pRowCursor->NextRow(&pRowLayerName) == S_OK)
    {
        pRowLayerName->get_Value(lLayerName, &vtVal);
        sLayerName = vtVal.bstrVal;

        //pRowLayerName->get_Value(lLayerAlias, &vtVal);
        sLayerAlias = "";

        mapLayers.SetAt(sLayerName, sLayerAlias);
    }

    IFeatureWorkspacePtr pInFtWS = pInWS;


    //线
    PrgbarSetText("正在对线图层进行分层...");
    PrgbarStepIt();
    IFeatureClassPtr pLineFtCls;
    hr = pInFtWS->OpenFeatureClass(CComBSTR("Line"), &pLineFtCls);
    if (SplitOneLayer(&mapLayers, pLineFtCls, pOutWS))
    {
        WriteLog("完成Line图层数据的分层。");
        m_pLogRec->WriteLog("完成Line图层数据的分层。");
    }


    //点
    PrgbarSetText("正在对点图层进行分层...");
    PrgbarStepIt();
    IFeatureClassPtr pPointFtCls;
    hr = pInFtWS->OpenFeatureClass(CComBSTR("Point"), &pPointFtCls);
    if (SplitOneLayer(&mapLayers, pPointFtCls, pOutWS))
    {
        WriteLog("完成Point图层数据的分层。");
        m_pLogRec->WriteLog("完成Point图层数据的分层。");
    }

    //注记
    PrgbarSetText("正在对注记图层进行分层...");
    PrgbarStepIt();
    IFeatureClassPtr pAnnoFtCls;
    hr = pInFtWS->OpenFeatureClass(CComBSTR("Annotation"), &pAnnoFtCls);
    if (SplitAnnotationLayer("Annotation", &mapLayers, pAnnoFtCls, pOutWS))
    {
        WriteLog("完成Annotation图层数据的分层。");
        m_pLogRec->WriteLog("完成Annotation图层数据的分层。");
    }

    hr = pInFtWS->OpenFeatureClass(CComBSTR("DwgDimension"), &pAnnoFtCls);
    if (SplitAnnotationLayer("DwgDimension", &mapLayers, pAnnoFtCls, pOutWS))
    {
        WriteLog("完成DwgDimension图层数据的分层。");
        m_pLogRec->WriteLog("完成DwgDimension图层数据的分层。");
    }


    //点文本(注记)
    /*IFeatureClassPtr pLabelTxtFtCls;
    hr = pInFtWS->OpenFeatureClass(CComBSTR("LabelTxt"), &pLabelTxtFtCls);
    SplitOneLayer(&mapLayers, pLabelTxtFtCls, m_pOutWS);
    WriteLog("完成LabelTxt图层数据的分层。");*/


    PrgbarSetText("");
    PrgbarSetPos(0);

    return TRUE;
}

ITablePtr XDBPreProcessDriver::GetExtendCompareTable(ITablePtr ipTable)
{
    if (m_pEnumConfigDatasetName == NULL)
    {
        IWorkspacePtr ipSysWksp = m_pSysFtWs;

        ipSysWksp->get_DatasetNames(esriDTTable, &m_pEnumConfigDatasetName);
    }
    else
    {
        m_pEnumConfigDatasetName->Reset();
        IDatasetNamePtr ipDatasetName = NULL;
        m_pEnumConfigDatasetName->Next(&ipDatasetName);
        IDatasetPtr ipDataset = ipTable;
        CComBSTR bsLayerName, bsTableName;
        ipDataset->get_Name(&bsLayerName);
        CString sLayerName = GetSdeFtClsName(bsLayerName);
        CString sTableName;
        while (ipDatasetName)
        {
            ipDatasetName->get_Name(&bsTableName);
            sTableName = bsTableName;
            if (sTableName.CompareNoCase(sLayerName) == 0)
            {
                IUnknownPtr ipUnknown;
                ITablePtr ipTable;
                INamePtr ipName = ipDatasetName;
                ipName->Open(&ipUnknown);
                ipTable = ipUnknown;
                return ipTable;
            }
            m_pEnumConfigDatasetName->Next(&ipDatasetName);
        }
    }

    return NULL;
}



BOOL XDBPreProcessDriver::JoinExtendTable()
{
    /*CWaitCursor w;
    CString sLogInfo;
    sLogInfo.Format("3 挂接扩展属性");
    WriteLog(sLogInfo);
    //pTextProgressCtrl->ShowWindow(SW_SHOW);

    if (m_pOutWS)
    {
        IEnumDatasetPtr ipEnumDS;
        m_pOutWS->get_Datasets(esriDTFeatureDataset, &ipEnumDS);
        IFeatureDatasetPtr ipFeatureDS ;
        IDatasetPtr ipDataset = NULL;
        ipEnumDS->Next(&ipDataset);
        ipFeatureDS = ipDataset;
        if (ipFeatureDS)
        {
            ipFeatureDS->get_Subsets(&ipEnumDS);
        }
        else
        {
            m_pOutWS->get_Datasets(esriDTFeatureClass, &ipEnumDS);
        }

        CMapStringToString mapRegAppNames;
        //得到所有FeatureClass对应的扩展属性注册应用名称
        GetExtraAttribRegAppNames(mapRegAppNames);


        //得到扩展属性字段的注册应用名
        CStringList lstAppNames;
        GetRegAppNames(lstAppNames);


        ipEnumDS->Reset();
        ipEnumDS->Next(&ipDataset);
        while (ipDataset)
        {
            ITablePtr ipTable = ipDataset;
            if (ipTable != NULL)
            {
                CComBSTR bsLayerName;
                ipDataset->get_Name(&bsLayerName);
                CString sLayerName = GetSdeFtClsName(bsLayerName);
                ITablePtr ipExtendFieldConfigTable = GetExtendFieldsConfigTable(sLayerName);//GetExtendCompareTable(ipTable);
                if (ipExtendFieldConfigTable)
                {
                    //pTextProgressCtrl->UpdateWindow();
                    //挂接扩展表类
                    if (joinExdDriver == NULL)
                    {
                        joinExdDriver = new XJoinExtendTable;
                        joinExdDriver->m_pProgressCtrl = pTextProgressCtrl;

                        if (m_ipExtendTable == NULL)
                        {
                            IFeatureWorkspacePtr ipSourceFeatureWks = m_pInWS ;
                            ipSourceFeatureWks->OpenTable(CComBSTR("ExtendTable"), &m_ipExtendTable);
                        }
                        joinExdDriver->m_ipExtendTable = m_ipExtendTable;
                    }

                    CString sLogInfo;
                    sLogInfo.Format("正在对数据:%s进行扩展属性挂接", sLayerName);
                    WriteLog(sLogInfo);

                    joinExdDriver->m_pLogList = &m_LogList;
                    joinExdDriver->m_ipConfigTable = ipExtendFieldConfigTable;
                    joinExdDriver->m_ipTargetTable = ipTable;

                    joinExdDriver->m_mapRegAppName = &mapRegAppNames;//图层对应的扩展属性应用名称

                    joinExdDriver->m_lstRegApps = &lstAppNames;


              
                    joinExdDriver->AddExtendFieldsValue(sLayerName);

                    sLogInfo.Format("数据:%s扩展属性挂接操作完成", sLayerName);
                    WriteLog(sLogInfo);
                }
            }
            ipEnumDS->Next(&ipDataset);
        }
    }

    sLogInfo.Format("3 挂接扩展属性完成!");
    WriteLog(sLogInfo);*/

    return TRUE;
}

/********************************************************************
简要描述 : 挂接扩展属性
输入参数 :
返 回 值 :

//
修改日志 :
*********************************************************************/
bool XDBPreProcessDriver::JoinExtendTable2(void)
{

    //CWaitCursor w;
    CString sLogInfo;
    sLogInfo.Format("3 挂接扩展属性");
    WriteLog(sLogInfo);

    if (m_pInWS == NULL || m_pOutWS == NULL) return false;

    IFeatureDatasetPtr pFtDataset;
    ((IFeatureWorkspacePtr)m_pInWS)->OpenFeatureDataset(CComBSTR("DWG_X"), &pFtDataset);
    if (pFtDataset == NULL) return false;

    IEnumDatasetPtr pEnumDS;
    pFtDataset->get_Subsets(&pEnumDS);
    if (pEnumDS == NULL) return false;


    CMapStringToString mapRegAppNames;
    //得到所有FeatureClass对应的扩展属性注册应用名称
    GetExtraAttribRegAppNames(mapRegAppNames);

    //得到扩展属性字段的注册应用名
    CStringList lstAppNames;
    GetRegAppNames(lstAppNames);

    //挂接扩展表类
    XJoinExtendTable joinExdDriver;
    joinExdDriver.m_pProgressCtrl = pTextProgressCtrl;
    ITablePtr ipExtendTable;
    ((IFeatureWorkspacePtr)m_pInWS)->OpenTable(CComBSTR("ExtendTable"), &ipExtendTable);
    joinExdDriver.m_ipExtendTable = ipExtendTable;

    int iRst = 0;

    pEnumDS->Reset();
    IDatasetPtr ipDataset = NULL;
    pEnumDS->Next(&ipDataset);
    while (ipDataset != NULL)
    {
        ITablePtr ipTable = ipDataset;
        if (ipTable != NULL)
        {
            CComBSTR bsLayerName;
            ipDataset->get_Name(&bsLayerName);
            CString sLayerName = GetSdeFtClsName(bsLayerName);
            ITablePtr ipExtendFieldConfigTable = GetExtendFieldsConfigTable(sLayerName);
            if (ipExtendFieldConfigTable)
            {
                CString sLogInfo;
                sLogInfo.Format("正在对数据:%s进行扩展属性挂接", sLayerName);
                WriteLog(sLogInfo);

                joinExdDriver.m_pLogList = &m_LogList;
                joinExdDriver.m_ipConfigTable = ipExtendFieldConfigTable;
                joinExdDriver.m_ipTargetTable = ipTable;

                joinExdDriver.m_mapRegAppName = &mapRegAppNames;//图层对应的扩展属性应用名称

                joinExdDriver.m_lstRegApps = &lstAppNames;


                /******************************************
                修改原因 :
                
                
                * *****************************************/
                joinExdDriver.AddExtendFieldsValue(sLayerName);

                sLogInfo.Format("数据:%s扩展属性挂接操作完成", sLayerName);
                WriteLog(sLogInfo);

                //iRst = ipExtendFieldConfigTable->Release();

            }
        }

        pEnumDS->Next(&ipDataset);
    }

    sLogInfo.Format("3 挂接扩展属性完成!");
    WriteLog(sLogInfo);

    return true;
}



/*BOOL XDBPreProcessDriver::GetExtendTable()
{
if (m_ipExtendTable == NULL)
{
IFeatureWorkspacePtr ipSourceFeatureWks = m_pInWS ;
ipSourceFeatureWks->OpenTable(CComBSTR("ExtendTable"), &m_ipExtendTable);
}
return m_ipExtendTable != NULL;
}
*/
/*BOOL XDBPreProcessDriver::GetCompareTable()
{
//IUnknown* pUnk = 0;
//Sys_Getparameter(SYSSET_MDB_CONNECT, (void * *) &pUnk);
//IWorkspacePtr ipSysWksp = pUnk;
IWorkspacePtr ipSysWksp = API_GetSysWorkspace();

IFeatureWorkspacePtr ipFeatureWorkspace(ipSysWksp);
if (ipFeatureWorkspace != NULL)
{
ipFeatureWorkspace->OpenTable(CComBSTR("CAD2GDB"), &ipCompareTable);
}
return ipCompareTable != NULL;
}*/

/*BOOL XDBPreProcessDriver::CopyFieldValue(IEsriRow* ipRow, IFeatureBuffer* pFeatureBuffer)
{
long lindex;
IFieldsPtr ipSFields, ipTFields;
long lFieldNum;
BSTR FieldName;

if (pFeatureBuffer == NULL)
return S_FALSE;
ipRow->get_Fields(&ipSFields);
pFeatureBuffer->get_Fields(&ipTFields);
ipSFields->get_FieldCount(&lFieldNum);
COleVariant var;
for (int i = 0; i < lFieldNum; i++)
{
IFieldPtr ipField;
ipSFields->get_Field(i, &ipField);
ipField->get_Name(&FieldName);

ipTFields->FindField(FieldName, &lindex);
if (lindex != -1)
{
ipRow->get_Value(i, var);
pFeatureBuffer->put_Value(lindex, var);
}
}
return TRUE;
}*/

/************************************************************************
简要描述 : 根据名称从系统数据库中得到扩展属性字段配置表
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
ITable* XDBPreProcessDriver::GetExtendFieldsConfigTable(CString sLayerName)
{

    ITable* pRstTable = NULL;
    HRESULT hr = m_pSysFtWs->OpenTable(CComBSTR(sLayerName), &pRstTable);
    return pRstTable;

    /*if (m_pEnumConfigDatasetName == NULL)
    {
        IWorkspacePtr ipSysWksp = m_pSysFtWs;
        ipSysWksp->get_DatasetNames(esriDTTable, &m_pEnumConfigDatasetName);
    }

    ITable* pRstTable = NULL;

    CComBSTR bsTableName;
    CString sTableName;
    m_pEnumConfigDatasetName->Reset();
    IDatasetNamePtr ipDatasetName = NULL;
    m_pEnumConfigDatasetName->Next(&ipDatasetName);
    while (ipDatasetName)
    {
        ipDatasetName->get_Name(&bsTableName);
        sTableName = bsTableName;
        if (sTableName.CompareNoCase(sLayerName) == 0)
        {
            IUnknown* ipUnknown;
            ITablePtr ipTable;
            INamePtr ipName = ipDatasetName;
            ipName->Open(&ipUnknown);
            ipTable = ipUnknown;

    		pRstTable = ipTable.GetInterfacePtr();
    		//int iRst = ipTable->Release();
    		
    		//int iRst = ipUnknown->Release();
    		//ipName.Release();
    		ipDatasetName.Release();
    		break;
        }
    	ipDatasetName.Release();
        m_pEnumConfigDatasetName->Next(&ipDatasetName);
    }*/
}


/************************************************************************
简要描述 :
输入参数 :
返 回 值 :
//
修改日志 :
************************************************************************/
//IFeatureWorkspacePtr XDBPreProcessDriver::GetSysWorkspace()
//{
//    if (m_pSysFtWs == NULL)
//    {
//        IUnknown* pUnk = 0;
//        pUnk = API_GetSysWorkspace();
//
//        m_pSysFtWs = pUnk;
//        if (m_pSysFtWs == NULL)
//        {
//            AfxMessageBox("无法连接到系统数据库，请检查。");
//            return NULL;
//        }
//    }
//
//    return m_pSysFtWs;
//}

/************************************************************************
简要描述 : 得到FeatureClass对应的扩展属性注册应用名称
输入参数 :
返 回 值 :
//
修改日志 :
************************************************************************/
void XDBPreProcessDriver::GetExtraAttribRegAppNames(CMapStringToString& mapRegAppNames)
{
    mapRegAppNames.RemoveAll();
    if (m_pSysFtWs != NULL)
    {
        ITablePtr pTable;
        m_pSysFtWs->OpenTable(CComBSTR("EXTRA_ATTRIB_CONFIG"), &pTable);

        if (pTable != NULL)
        {
            HRESULT hr;
            IEsriCursorPtr pCursor;
            hr = pTable->Search(NULL, VARIANT_FALSE, &pCursor);
            if (pCursor != NULL)
            {
                long lFeatureClassNameFieldIndex, lRegAppName;
                pTable->FindField(CComBSTR("FeatureClassName"), &lFeatureClassNameFieldIndex);
                pTable->FindField(CComBSTR("RegAppName"), &lRegAppName);

                IEsriRowPtr pRow;
                CComVariant vtVal;
                pCursor->NextRow(&pRow);
                while (pRow != NULL)
                {
                    CString sFeatureClassName, sRegAppName;
                    //FeatureClassName
                    hr = pRow->get_Value(lFeatureClassNameFieldIndex, &vtVal);
                    if (vtVal.vt != VT_NULL && vtVal.vt != VT_EMPTY)
                    {
                        sFeatureClassName = vtVal.bstrVal;
                    }

                    //RegAppName
                    hr = pRow->get_Value(lRegAppName, &vtVal);
                    if (vtVal.vt != VT_NULL && vtVal.vt != VT_EMPTY)
                    {
                        sRegAppName = vtVal.bstrVal;
                    }

                    mapRegAppNames.SetAt(sFeatureClassName, sRegAppName);

                    pRow.Release();

                    hr = pCursor->NextRow(&pRow);
                }

                pCursor.Release();
            }

            pTable.Release();
        }
    }
}

/************************************************************************
简要描述 : 拷贝要素ID表到MDB正式库中
输入参数 :
返 回 值 :
//
//
修改日志 :
************************************************************************/
void XDBPreProcessDriver::CopyFeatureUIDTable()
{
    if (m_pInWS == NULL || m_pOutWS == NULL)
    {
        return;
    }

    IFeatureWorkspacePtr pTempFtWksp(m_pInWS);
    //IFeatureWorkspacePtr pTargetFtWksp(m_pOutWS);

    //打开源数据表
    ITablePtr pSrcTable;
    pTempFtWksp->OpenTable(CComBSTR("FeatureUID"), &pSrcTable);
    if (pSrcTable == NULL)
    {
        return;
    }

    // 	IFieldsEditPtr pFields(CLSID_Fields);
    // 	IFieldEditPtr pField(CLSID_Field);
    // 	pField->put_Name(CComBSTR("FEATURE_UID"));
    // 	pField->put_Type(esriFieldTypeString);
    // 	pField->put_Length(150);
    //
    // 	pFields->AddField(pField);
    //
    // 	//创建目的表
    // 	ITablePtr pTargetTable;
    // 	pTargetFtWksp->CreateTable(CComBSTR("FeatureUID"), pFields, 0, 0, 0, &pTargetTable);
    // 	if (pTargetTable==NULL)
    // 	{
    // 		return;
    // 	}

    HRESULT hr;
    //src datasetname
    INamePtr pSrcName;
    IDatasetPtr pDataset(pSrcTable);
    pDataset->get_FullName(&pSrcName);
    IDatasetNamePtr pInputDsName(pSrcName);

    //target datasetname
    IDatasetNamePtr pOutDsName(CLSID_TableName);
    pOutDsName->put_Name(CComBSTR("FeatureUID"));
    //Target WorkspaceName
    IWorkspaceNamePtr ipTargetWorkspaceName(CLSID_WorkspaceName);
    ipTargetWorkspaceName->put_WorkspaceFactoryProgID(CComBSTR(_T("esriDataSourcesGDB.AccessWorkspaceFactory.1")));
    CComBSTR bsTargetPath;
    m_pOutWS->get_PathName(&bsTargetPath);
    hr = ipTargetWorkspaceName->put_PathName(bsTargetPath);
    pOutDsName->putref_WorkspaceName(ipTargetWorkspaceName);

    //IFields
    IFieldsPtr pSrcFields;
    pSrcTable->get_Fields(&pSrcFields);
    IFieldsPtr pTargetFields;
    IEnumFieldErrorPtr pEnumFieldErr;
    IFieldCheckerPtr ipFieldChecker(CLSID_FieldChecker);
    ipFieldChecker->Validate(pSrcFields, &pEnumFieldErr, &pTargetFields);

    IEnumInvalidObjectPtr pEnumInvalid;
    IFeatureDataConverterPtr pFeatureDataConverter(CLSID_FeatureDataConverter);
    hr = pFeatureDataConverter->ConvertTable(pInputDsName, NULL, pOutDsName, pTargetFields, 0, 1000, 0, &pEnumInvalid);
}


//转换VARIANT到CString
CString XDBPreProcessDriver::GetStringByVar(VARIANT var)
{
    CString str;
    if (var.vt == VT_I2)
    {
        str.Format("%d", var.iVal);
    }
    else if (var.vt == VT_I4)
    {
        str.Format("%d", var.lVal);
    }
    else if (var.vt == VT_R4)
    {
        str.Format("%g", var.fltVal);
    }
    else if (var.vt == VT_R8)
    {
        str.Format("%g", var.dblVal);
    }
    else if (var.vt == VT_BSTR)
    {
        str = var.bstrVal;
    }
    else if (var.vt == VT_BOOL)
    {
        if (var.scode)
            str.Format( _T("%d") , TRUE );
        else
            str.Format( _T("%d") , FALSE );
    }
    else if (var.vt == VT_DATE)
    {
        COleDateTime time;
        time = var.date;
        str.Format("%d-%d-%d  %d:%d:%d", time.GetYear(), time.GetMonth(), time.GetDay(),
                   time.GetHour(), time.GetMinute(), time.GetSecond());
    }
    return str;
}

/************************************************************************
简要描述 :得到去掉前缀的要素类名称
输入参数 :
返 回 值 :
//
修改日志 :
************************************************************************/
CString XDBPreProcessDriver::GetSdeFtClsName(CComBSTR bsFtClsName)
{
    CString sFtClsName = CW2A(bsFtClsName);
    int iPos = sFtClsName.ReverseFind('.');
    if (iPos > 0)
    {
        sFtClsName = sFtClsName.Mid(iPos + 1);
    }
    return sFtClsName;
}

/************************************************************************
简要描述 : 对单个图层进行分层
输入参数 :
返 回 值 :
//
修改日志 :
************************************************************************/
bool XDBPreProcessDriver::SplitOneLayer(CMapStringToString* pSplitLayerNames, IFeatureClass* pInFtCls, IWorkspace* pTargetWS)
{

    if (pInFtCls == NULL) return false;


    HRESULT hr;
    POSITION pos = pSplitLayerNames->GetStartPosition();
    CString sLayerName;
    CString sLayerAlias;

    esriGeometryType geoType;
    hr = pInFtCls->get_ShapeType(&geoType);

    CString sBaseFtClsName;
    if (geoType == esriGeometryPolyline)
    {
        sBaseFtClsName = "Line";

    }
    else if (geoType == esriGeometryPoint)
    {
        sBaseFtClsName = "Point";
    }


    while (pos != NULL)
    {
        pSplitLayerNames->GetNextAssoc(pos, sLayerName, sLayerAlias);

        if (sLayerName.IsEmpty()) continue;

        //针对济南项目图层类型进行过滤

        CString sGeoType = sLayerName.Right(2);

        //线
        if (geoType == esriGeometryPolyline)
        {
            if (sGeoType.CompareNoCase("LN") != 0)
            {
                continue;
            }
        }
        else if (geoType == esriGeometryPoint)
        {
            if (sGeoType.CompareNoCase("PT") != 0)
            {
                continue;
            }
        }


        //根据LayerName分层
        IQueryFilterPtr pSplitFilter(CLSID_QueryFilter);
        CString sWhereClause;
        sWhereClause.Format("GDB_LAYER='%s'", sLayerName);
        hr = pSplitFilter->put_WhereClause(CComBSTR(sWhereClause));

        long numFeats;

        hr = pInFtCls->FeatureCount(pSplitFilter, &numFeats);
        if (numFeats <= 0) continue;

        CHAR strLayer[255] = {0};
        strcpy(strLayer, sLayerName);
        //分层
        try
        {
            API_ConvertFeatureClass(pInFtCls, pSplitFilter, pTargetWS, "DWG_X", strLayer);
        }
        catch (...)
        {
            CString sLog;
            sLog.Format("SplitOneLayer() 在进行%s图层的 [%s] 分层时发生错误。", sBaseFtClsName, sWhereClause);
            m_pLogRec->WriteLog(sLog);
        }
    }

    return true;
}

/********************************************************************
简要描述 :  由线构面
输入参数 :
返 回 值 :
//
修改日志 :
*********************************************************************/
IPolygon* XDBPreProcessDriver::CreatePolygon(IPolyline* pPLine, ISpatialReference* pSpRef)
{
    if (pPLine == NULL) return NULL;

    HRESULT hr;

    ITopologicalOperator3Ptr pTopo = pPLine;
    if (pTopo != NULL)
    {
        hr = pTopo->put_IsKnownSimple(VARIANT_FALSE);
        hr = pTopo->Simplify();
    }

    ///////构面//////////////
    IPolygonPtr pGeoPolygon;
    ISegmentCollectionPtr pPolygonSegm(CLSID_Polygon);
    ISegmentCollectionPtr pSegColl;
    pSegColl = pPLine;
    if (pSegColl == NULL) return NULL;

    /*/生成面
    long numSegCol;
    pSegColl->get_SegmentCount(&numSegCol);
    for (int i = 0; i < numSegCol; i++)
    {
        ISegmentPtr pSegment;
        pSegColl->get_Segment(i, &pSegment);
        pPolygonSegm->AddSegment(pSegment);
    }*/

    pPolygonSegm->AddSegmentCollection(pSegColl);
    pGeoPolygon = pPolygonSegm;

    hr = pGeoPolygon->Project(pSpRef);

    ITopologicalOperator3Ptr pTopoPolygon = pGeoPolygon;
    if (pTopoPolygon != NULL)
    {
        hr = pTopoPolygon->put_IsKnownSimple(VARIANT_FALSE);
        hr = pTopoPolygon->Simplify();
    }


    return pGeoPolygon.Detach();

}


/************************************************************************
简要描述 : 由线构面
输入参数 :
返 回 值 :
//
修改日志 :
************************************************************************/
void XDBPreProcessDriver::BuildPolygon(void)
{
    //CWaitCursor w;
    CString sLogInfo;
    sLogInfo.Format("4 开始构面");
    WriteLog(sLogInfo);

    CMapStringToString polygonLayers;

    HRESULT hr;
    ITablePtr pTbLayers;
    hr = m_pSysFtWs->OpenTable(CComBSTR("POLYGON_LAYERS"), &pTbLayers);
    if (pTbLayers == NULL) return;

    long numLayers;
    hr = pTbLayers->RowCount(NULL, &numLayers);
    if (numLayers <= 0) return ;

    long lLayerName;
    long lPolygonName;
    pTbLayers->FindField(CComBSTR("GDB_LAYER"), &lLayerName);
    pTbLayers->FindField(CComBSTR("GDB_POLYGONLAYER"), &lPolygonName);

    if (lLayerName == -1 || lPolygonName == -1)
    {
        WriteLog("POLYGON_LAYERS配置表字段不正确，请检查。");
        return;
    }

    IEsriCursorPtr pRowCursor;
    hr = pTbLayers->Search(NULL, VARIANT_FALSE, &pRowCursor);

    IEsriRowPtr pRowLayerName;
    CComVariant vtVal;
    CString sLayerName;
    CString sPolygonName;

    while (pRowCursor->NextRow(&pRowLayerName) == S_OK)
    {
        pRowLayerName->get_Value(lLayerName, &vtVal);
        sLayerName = vtVal.bstrVal;

        pRowLayerName->get_Value(lPolygonName, &vtVal);
        sPolygonName = vtVal.bstrVal;

        polygonLayers.SetAt(sLayerName, sPolygonName);
    }

    if (polygonLayers.GetCount() <= 0) return;

    CString sTextProgress;
    //pTextProgressCtrl->ShowWindow(SW_SHOW);
    //pTextProgressCtrl->SetPos(0);

    //pTextProgressCtrl->SetWindowText("开始构面前的准备处理...");
    ////pTextProgressCtrl->StepIt();

    //pTextProgressCtrl->SetRange(0, polygonLayers.GetCount());


    //PrgbarRange(0, polygonLayers.GetCount());
    //PrgbarSetPos(0);
    //PrgbarSetText("开始构面前的准备处理...");
    //PrgbarStepIt();

    IFeatureWorkspacePtr pFtWS = m_pOutWS;

    POSITION pos = polygonLayers.GetStartPosition();
    while (pos != NULL)
    {
        polygonLayers.GetNextAssoc(pos, sLayerName, sPolygonName);

        //sTextProgress.Format("正在对%s图层进行构面...", sLayerName);
        //PrgbarSetText(sTextProgress);
        //PrgbarStepIt();

        IFeatureClassPtr pLineFtCls;
        IFeatureClassPtr pPolygonFtCls;
        IFeatureDatasetPtr pFtDS;
        IFieldsPtr pSrFields;
        IFieldsPtr pTargetFields;

        hr = pFtWS->OpenFeatureClass(CComBSTR(sLayerName), &pLineFtCls);

        //打不开则进行下一个图层的构面
        if (pLineFtCls == NULL) continue;

        //得到目标字段
        pTargetFields.CreateInstance(CLSID_Fields);
        IFieldsEditPtr pTargetFieldsEdit = pTargetFields;

        long numFields;
        hr = pLineFtCls->get_Fields(&pSrFields);
        pSrFields->get_FieldCount(&numFields);

        //复制线要素的图形属性字段
        for (int i = 0; i < numFields; i++)
        {
            CComBSTR bsFieldName;
            VARIANT_BOOL vbEditable;
            esriFieldType fieldType;
            IFieldPtr pField;
            pSrFields->get_Field(i, &pField);
            hr = pField->get_Name(&bsFieldName);
            pField->get_Editable(&vbEditable);
            if (vbEditable == VARIANT_TRUE)
            {
                pField->get_Type(&fieldType);
                if (fieldType != esriFieldTypeGeometry)
                {
                    IClonePtr pClone = pField;
                    IClonePtr pClonedField;
                    hr = pClone->Clone(&pClonedField);
                    IFieldPtr pTarField = pClonedField;
                    hr = pTargetFieldsEdit->AddField(pTarField);
                }
            }
        }

        //添加OBJECTID字段
        IFieldEditPtr pTarField(CLSID_Field);
        hr = pTarField->put_Name(CComBSTR("OBJECTID"));
        hr = pTarField->put_AliasName(CComBSTR("要素ID"));
        hr = pTarField->put_Type(esriFieldTypeOID);
        hr = pTargetFieldsEdit->AddField(pTarField);

        IGeometryDefEditPtr pGeoDef(CLSID_GeometryDef);
        hr = pGeoDef->put_GeometryType(esriGeometryPolygon);

        ISpatialReferencePtr pSpRef = API_GetSpatialReference(pLineFtCls);//GetSysSpatialRef();

        pGeoDef->put_GridCount(1);
        pGeoDef->put_AvgNumPoints(2);
        pGeoDef->put_HasM(VARIANT_FALSE);
        pGeoDef->put_HasZ(VARIANT_FALSE);

        //double dGridSize = 1000;
        //VARIANT_BOOL bhasXY;
        //pSpRef->HasXYPrecision(&bhasXY);
        //if (bhasXY)
        //{
        //    double xmin, ymin, xmax, ymax, dArea;
        //    pSpRef->GetDomain(&xmin, &xmax, &ymin, &ymax);
        //    dArea = (xmax - xmin) * (ymax - ymin);
        //    dGridSize = sqrt(dArea / 100);
        //}
        //if (dGridSize <= 0)
        //    dGridSize = 1000;
        pGeoDef->put_GridSize(0, 120);

        hr = pGeoDef->putref_SpatialReference(pSpRef);

        IFieldEditPtr pShapeField(CLSID_Field);
        hr = pShapeField->put_Name(CComBSTR("SHAPE"));
        hr = pShapeField->put_AliasName(CComBSTR("面要素"));
        hr = pShapeField->put_Type(esriFieldTypeGeometry);
        hr = pShapeField->putref_GeometryDef(pGeoDef);

        hr = pTargetFieldsEdit->AddField(pShapeField);

        hr = pLineFtCls->get_FeatureDataset(&pFtDS);
        if (pFtDS == NULL) continue;

        //生成面图层
        hr = pFtDS->CreateFeatureClass(CComBSTR(sPolygonName), pTargetFields, 0, 0, esriFTSimple, CComBSTR("SHAPE"), NULL, &pPolygonFtCls);

        if (pPolygonFtCls == NULL) continue;

        long numFeatures;

        //遍历线图层中的每一个要素,生成面要素, 然后插入到面图层中
        hr = pLineFtCls->FeatureCount(NULL, &numFeatures);
        if (numFeatures <= 0) continue;

        PrgbarRange(0, numFeatures);
        PrgbarSetPos(0);
        sTextProgress.Format("正在对%s图层进行构面...", sLayerName);
        PrgbarSetText(sTextProgress);

        IFeaturePtr pLineFeat;
        IFeaturePtr pPolygonFeat;
        IGeometryPtr pFeatGeo;
        IFeatureCursorPtr pFtCur;
        hr = pLineFtCls->Search(NULL, VARIANT_FALSE, &pFtCur);
        while (pFtCur->NextFeature(&pLineFeat) == S_OK)
        {
            PrgbarStepIt();

            //得到构面线要素
            hr = pLineFeat->get_ShapeCopy(&pFeatGeo);
            if (pFeatGeo == NULL) continue;

            IPolylinePtr pPolyLine(pFeatGeo);
            if (pPolyLine == NULL) continue;

            //检查线是否闭合,如果不闭合则不构面
            VARIANT_BOOL IsClosed;
            pPolyLine->get_IsClosed(&IsClosed);
            if (IsClosed == VARIANT_FALSE) continue;

            ///////构面//////////////
            IPolygonPtr pGeoPolygon = CreatePolygon(pPolyLine, pSpRef);

            /*ISegmentCollectionPtr pPolygonSegm(CLSID_Polygon);
            ISegmentCollectionPtr pSegColl;

            pSegColl = pFeatGeo;

            if (pSegColl == NULL) continue;

            //生成面
            long numSegCol;
            pSegColl->get_SegmentCount(&numSegCol);
            for (int i = 0; i < numSegCol; i++)
            {
                ISegmentPtr pSegment;
                pSegColl->get_Segment(i, &pSegment);
                pPolygonSegm->AddSegment(pSegment);
            }

            pGeoPolygon = pPolygonSegm;
            //ISpatialReferencePtr pSpRef = GetSysSpatialRef();
            hr = pGeoPolygon->Project(pSpRef);*/

            hr = pPolygonFtCls->CreateFeature(&pPolygonFeat);
            if (pPolygonFeat == NULL)
            {
                WriteLog("创建面状要素失败.");
                continue;
            }

            //复制线属性
            CopyFeatureAttr(pLineFeat, pPolygonFeat);

            /* /修正拓扑
            ITopologicalOperator3Ptr pPolygonOper = pGeoPolygon;
            hr = pPolygonOper->put_IsKnownSimple(VARIANT_FALSE);
            hr = pPolygonOper->Simplify();*/


            hr = pPolygonFeat->putref_Shape(pGeoPolygon);

            //保存面要素
            hr = pPolygonFeat->Store();

        }

        sLogInfo.Format("完成对%s图层的构面。", sLayerName);
        WriteLog(sLogInfo);
    }

    PrgbarSetPos(0);
    PrgbarSetText("");

    sLogInfo.Format("4 完成构面。");
    WriteLog(sLogInfo);

}


/************************************************************************
简要描述 :	复制要素属性
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
void XDBPreProcessDriver::CopyFeatureAttr(IFeaturePtr pSourceFeature, IFeaturePtr pTargetFeature)
{
    if (pSourceFeature == NULL || pTargetFeature == NULL) return;

    HRESULT hr;
    IFieldsPtr pTarFields;
    IFieldsPtr pSrcFields;
    IFieldPtr pTarField;
    IEsriRowPtr pRow;
    long lFieldCount;
    VARIANT_BOOL vbEditable;
    esriFieldType fieldType;
    CComVariant vtVal;
    IGeometryPtr pShape;
    CComBSTR bsFieldName;
    long lFieldInd;

    hr = pSourceFeature->get_Fields(&pSrcFields);

    hr = pTargetFeature->get_Fields(&pTarFields);
    pTarFields->get_FieldCount(&lFieldCount);
    for (int iTarFieldInd = 0; iTarFieldInd < lFieldCount; iTarFieldInd++)
    {
        pTarFields->get_Field(iTarFieldInd, &pTarField);
        pTarField->get_Editable(&vbEditable);
        if (vbEditable == VARIANT_TRUE)
        {
            pTarField->get_Type(&fieldType);
            if (fieldType != esriFieldTypeOID && fieldType != esriFieldTypeGeometry)
            {
                hr = pTarField->get_Name(&bsFieldName);
                hr = pSrcFields->FindField(bsFieldName, &lFieldInd);
                if (lFieldInd != -1)
                {
                    pSourceFeature->get_Value(lFieldInd, &vtVal);
                    pTargetFeature->put_Value(iTarFieldInd, vtVal);
                }
            }
        }
    }

}

/************************************************************************
简要描述 : 创建注记类型的要素类
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
HRESULT XDBPreProcessDriver::CreateAnnoFtCls(IFeatureDataset* pTarFDS, BSTR bsAnnoName, IFeatureClass** ppAnnoFtCls)
{
    HRESULT hr;
    IWorkspacePtr pWS;
    hr = pTarFDS->get_Workspace(&pWS);

    IFeatureWorkspaceAnnoPtr PFWSAnno = pWS;

    IGraphicsLayerScalePtr pGLS(CLSID_GraphicsLayerScale);
    pGLS->put_Units(esriMeters);
    pGLS->put_ReferenceScale(1);

    //' set up symbol collection
    ISymbolCollectionPtr pSymbolColl(CLSID_SymbolCollection);

    ITextSymbolPtr myTxtSym(CLSID_TextSymbol);
    //Set the font for myTxtSym
    IFontDispPtr myFont(CLSID_StdFont);
    IFontPtr pFt = myFont;
    pFt->put_Name(CComBSTR("Courier New"));
    CY cy;
    cy.Hi = 0;
    cy.Lo = 9;
    pFt->put_Size(cy);
    myTxtSym->put_Font(myFont);

    // Set the Color for myTxtSym to be Dark Red
    IRgbColorPtr myColor(CLSID_RgbColor);
    myColor->put_Red(150);
    myColor->put_Green(0);
    myColor->put_Blue (0);
    myTxtSym->put_Color(myColor);

    // Set other properties for myTxtSym
    myTxtSym->put_Angle(0);
    myTxtSym->put_RightToLeft(VARIANT_FALSE);
    myTxtSym->put_VerticalAlignment(esriTVABaseline);
    myTxtSym->put_HorizontalAlignment(esriTHAFull);
    myTxtSym->put_Size(200);
    //myTxtSym->put_Case(esriTCNormal);

    ISymbolPtr pSymbol = myTxtSym;
    pSymbolColl->putref_Symbol(0, pSymbol);

    //set up the annotation labeling properties including the expression
    IAnnotateLayerPropertiesPtr pAnnoProps(CLSID_LabelEngineLayerProperties);
    pAnnoProps->put_FeatureLinked(VARIANT_TRUE);
    pAnnoProps->put_AddUnplacedToGraphicsContainer(VARIANT_FALSE);
    pAnnoProps->put_CreateUnplacedElements(VARIANT_TRUE);
    pAnnoProps->put_DisplayAnnotation(VARIANT_TRUE);
    pAnnoProps->put_UseOutput(VARIANT_TRUE);

    ILabelEngineLayerPropertiesPtr pLELayerProps = pAnnoProps;
    IAnnotationExpressionEnginePtr aAnnoVBScriptEngine(CLSID_AnnotationVBScriptEngine);
    pLELayerProps->putref_ExpressionParser(aAnnoVBScriptEngine);
    pLELayerProps->put_Expression(CComBSTR("[DESCRIPTION]"));
    pLELayerProps->put_IsExpressionSimple(VARIANT_TRUE);
    pLELayerProps->put_Offset(0);
    pLELayerProps->put_SymbolID(0);
    pLELayerProps->putref_Symbol(myTxtSym);

    IAnnotateLayerTransformationPropertiesPtr pATP = pAnnoProps;
    double dRefScale;
    pGLS->get_ReferenceScale(&dRefScale);
    pATP->put_ReferenceScale(dRefScale);
    pATP->put_Units(esriMeters);
    pATP->put_ScaleRatio(1);

    IAnnotateLayerPropertiesCollectionPtr pAnnoPropsColl(CLSID_AnnotateLayerPropertiesCollection);
    pAnnoPropsColl->Add(pAnnoProps);

    //' use the AnnotationFeatureClassDescription co - class to get the list of required fields and the default name of the shape field
    IObjectClassDescriptionPtr pOCDesc(CLSID_AnnotationFeatureClassDescription);
    IFeatureClassDescriptionPtr pFDesc = pOCDesc;

    IFieldsPtr pReqFields;
    IUIDPtr pInstCLSID;
    IUIDPtr pExtCLSID;
    CComBSTR bsShapeFieldName;

    pOCDesc->get_RequiredFields(&pReqFields);
    pOCDesc->get_InstanceCLSID(&pInstCLSID);
    pOCDesc->get_ClassExtensionCLSID(&pExtCLSID);
    pFDesc->get_ShapeFieldName(&bsShapeFieldName);

    IFieldsEditPtr ipFieldsEdit = pReqFields;

    //创建CAD文件中注记图层字段
    IFieldEditPtr ipFieldEdit;
    IFieldPtr ipField;

    // 创建 Entity ，记录esri实体类型
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR("Entity"));
    ipFieldEdit->put_AliasName(CComBSTR("Entity"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldsEdit->AddField(ipField);


    // 创建 Handle ，记录DWG实体编号
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Handle"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Handle"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);

    // 创建 BaseName ，记录DWG实体层名即DWG文件名
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"BaseName"));
    ipFieldEdit->put_AliasName(CComBSTR(L"BaseName"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(250);
    ipFieldsEdit->AddField(ipField);

    // 创建 Layer ，记录DWG实体层名
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Layer"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Layer"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(250);
    ipFieldsEdit->AddField(ipField);

    // 创建 Color ，记录DWG实体符号颜色
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Color"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Color"));
    ipFieldEdit->put_Type(esriFieldTypeInteger);
    ipFieldsEdit->AddField(ipField);

    // 创建 Thickness ，记录DWG实体厚度
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Thickness"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Thickness"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);

    // 创建 Elevation ，记录DWG实体高程值
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Elevation"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Elevation"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);

    /* / 创建 Text ，记录Text
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Text"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Text"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);*/

    // 创建 Height ，记录高度
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Height"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Height"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);


    // 创建 FontID ，记录FontID
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"FontId"));
    ipFieldEdit->put_AliasName(CComBSTR(L"FontId"));
    ipFieldEdit->put_Type(esriFieldTypeSmallInteger);
    ipFieldsEdit->AddField(ipField);


    //////////创建对照表字段/////////////////////////////////////
    // 创建 GDB_LAYER字段
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    CComBSTR bsStr;
    bsStr = "GDB_LAYER";
    ipFieldEdit->put_Name(bsStr);
    ipFieldEdit->put_AliasName(bsStr);
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(200);
    ipFieldsEdit->AddField(ipField);

    // 创建 YSDM字段
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    bsStr = "YSDM";
    ipFieldEdit->put_Name(bsStr);
    ipFieldEdit->put_AliasName(bsStr);
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(200);
    ipFieldsEdit->AddField(ipField);

    // 创建 YSMC字段
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    bsStr = "YSMC";
    ipFieldEdit->put_Name(bsStr);
    ipFieldEdit->put_AliasName(bsStr);
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(200);
    ipFieldsEdit->AddField(ipField);

    //' create the new class
    hr = PFWSAnno->CreateAnnotationClass(bsAnnoName, pReqFields, pInstCLSID, pExtCLSID, bsShapeFieldName, CComBSTR(""), pTarFDS, 0, pAnnoPropsColl, pGLS, pSymbolColl, VARIANT_TRUE, ppAnnoFtCls);

    return hr;
}


/************************************************************************
简要描述 : 创建注记类型的要素类
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
HRESULT XDBPreProcessDriver::CreateAnnoFtCls(IWorkspace* pWS, BSTR bsAnnoName, IFeatureClass** ppAnnoFtCls)
{
    HRESULT hr;
    //IWorkspacePtr pWS = pFtWS;
    //hr = pTarFDS->get_Workspace(&pWS);

    IFeatureWorkspaceAnnoPtr PFWSAnno = pWS;

    IGraphicsLayerScalePtr pGLS(CLSID_GraphicsLayerScale);
    pGLS->put_Units(esriMeters);
    pGLS->put_ReferenceScale(1);

    //' set up symbol collection
    ISymbolCollectionPtr pSymbolColl(CLSID_SymbolCollection);

    ITextSymbolPtr myTxtSym(CLSID_TextSymbol);
    //Set the font for myTxtSym
    IFontDispPtr myFont(CLSID_StdFont);
    IFontPtr pFt = myFont;
    pFt->put_Name(CComBSTR("Courier New"));
    CY cy;
    cy.Hi = 0;
    cy.Lo = 9;
    pFt->put_Size(cy);
    myTxtSym->put_Font(myFont);

    // Set the Color for myTxtSym to be Dark Red
    IRgbColorPtr myColor(CLSID_RgbColor);
    myColor->put_Red(150);
    myColor->put_Green(0);
    myColor->put_Blue (0);
    myTxtSym->put_Color(myColor);

    // Set other properties for myTxtSym
    myTxtSym->put_Angle(0);
    myTxtSym->put_RightToLeft(VARIANT_FALSE);
    myTxtSym->put_VerticalAlignment(esriTVABaseline);
    myTxtSym->put_HorizontalAlignment(esriTHAFull);
    myTxtSym->put_Size(200);
    //myTxtSym->put_Case(esriTCNormal);

    ISymbolPtr pSymbol = myTxtSym;
    pSymbolColl->putref_Symbol(0, pSymbol);

    //set up the annotation labeling properties including the expression
    IAnnotateLayerPropertiesPtr pAnnoProps(CLSID_LabelEngineLayerProperties);
    pAnnoProps->put_FeatureLinked(VARIANT_TRUE);
    pAnnoProps->put_AddUnplacedToGraphicsContainer(VARIANT_FALSE);
    pAnnoProps->put_CreateUnplacedElements(VARIANT_TRUE);
    pAnnoProps->put_DisplayAnnotation(VARIANT_TRUE);
    pAnnoProps->put_UseOutput(VARIANT_TRUE);

    ILabelEngineLayerPropertiesPtr pLELayerProps = pAnnoProps;
    IAnnotationExpressionEnginePtr aAnnoVBScriptEngine(CLSID_AnnotationVBScriptEngine);
    pLELayerProps->putref_ExpressionParser(aAnnoVBScriptEngine);
    pLELayerProps->put_Expression(CComBSTR("[DESCRIPTION]"));
    pLELayerProps->put_IsExpressionSimple(VARIANT_TRUE);
    pLELayerProps->put_Offset(0);
    pLELayerProps->put_SymbolID(0);
    pLELayerProps->putref_Symbol(myTxtSym);

    IAnnotateLayerTransformationPropertiesPtr pATP = pAnnoProps;
    double dRefScale;
    pGLS->get_ReferenceScale(&dRefScale);
    pATP->put_ReferenceScale(dRefScale);
    pATP->put_Units(esriMeters);
    pATP->put_ScaleRatio(1);

    IAnnotateLayerPropertiesCollectionPtr pAnnoPropsColl(CLSID_AnnotateLayerPropertiesCollection);
    pAnnoPropsColl->Add(pAnnoProps);

    //' use the AnnotationFeatureClassDescription co - class to get the list of required fields and the default name of the shape field
    IObjectClassDescriptionPtr pOCDesc(CLSID_AnnotationFeatureClassDescription);
    IFeatureClassDescriptionPtr pFDesc = pOCDesc;

    IFieldsPtr pReqFields;
    IUIDPtr pInstCLSID;
    IUIDPtr pExtCLSID;
    CComBSTR bsShapeFieldName;

    pOCDesc->get_RequiredFields(&pReqFields);
    pOCDesc->get_InstanceCLSID(&pInstCLSID);
    pOCDesc->get_ClassExtensionCLSID(&pExtCLSID);
    pFDesc->get_ShapeFieldName(&bsShapeFieldName);

    IFieldsEditPtr ipFieldsEdit = pReqFields;

    //创建CAD文件中注记图层字段
    IFieldEditPtr ipFieldEdit;
    IFieldPtr ipField;

    // 创建 Entity ，记录esri实体类型
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR("Entity"));
    ipFieldEdit->put_AliasName(CComBSTR("Entity"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldsEdit->AddField(ipField);


    // 创建 Handle ，记录DWG实体编号
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Handle"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Handle"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);

    // 创建 BaseName ，记录DWG实体层名即DWG文件名
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"BaseName"));
    ipFieldEdit->put_AliasName(CComBSTR(L"BaseName"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(250);
    ipFieldsEdit->AddField(ipField);

    // 创建 Layer ，记录DWG实体层名
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Layer"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Layer"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(250);
    ipFieldsEdit->AddField(ipField);

    // 创建 Color ，记录DWG实体符号颜色
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Color"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Color"));
    ipFieldEdit->put_Type(esriFieldTypeInteger);
    ipFieldsEdit->AddField(ipField);

    // 创建 Thickness ，记录DWG实体厚度
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Thickness"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Thickness"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);

    // 创建 Elevation ，记录DWG实体高程值
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Elevation"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Elevation"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);

    /* / 创建 Text ，记录Text
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Text"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Text"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);*/

    // 创建 Height ，记录高度
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Height"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Height"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);


    // 创建 FontID ，记录FontID
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"FontId"));
    ipFieldEdit->put_AliasName(CComBSTR(L"FontId"));
    ipFieldEdit->put_Type(esriFieldTypeSmallInteger);
    ipFieldsEdit->AddField(ipField);


    //////////创建对照表字段/////////////////////////////////////
    // 创建 GDB_LAYER字段
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    CComBSTR bsStr;
    bsStr = "GDB_LAYER";
    ipFieldEdit->put_Name(bsStr);
    ipFieldEdit->put_AliasName(bsStr);
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(200);
    ipFieldsEdit->AddField(ipField);

    // 创建 YSDM字段
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    bsStr = "YSDM";
    ipFieldEdit->put_Name(bsStr);
    ipFieldEdit->put_AliasName(bsStr);
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(200);
    ipFieldsEdit->AddField(ipField);

    // 创建 YSMC字段
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    bsStr = "YSMC";
    ipFieldEdit->put_Name(bsStr);
    ipFieldEdit->put_AliasName(bsStr);
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(200);
    ipFieldsEdit->AddField(ipField);

    //' create the new class
    hr = PFWSAnno->CreateAnnotationClass(bsAnnoName, pReqFields, pInstCLSID, pExtCLSID, bsShapeFieldName, CComBSTR(""), NULL, 0, pAnnoPropsColl, pGLS, pSymbolColl, VARIANT_TRUE, ppAnnoFtCls);

    return hr;
}

/************************************************************************
简要描述 : 创建注记图层
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
HRESULT XDBPreProcessDriver::CreateAnnoFtCls(IFeatureDatasetPtr pTarFDS, BSTR bsAnnoName, IFieldsPtr pFields, IFeatureClass** ppAnnoFtCls)
{
    HRESULT hr;
    IWorkspacePtr pWS;
    hr = pTarFDS->get_Workspace(&pWS);

    IFeatureWorkspaceAnnoPtr PFWSAnno = pWS;

    IGraphicsLayerScalePtr pGLS(CLSID_GraphicsLayerScale);
    pGLS->put_Units(esriMeters);
    pGLS->put_ReferenceScale(1);

    //' set up symbol collection
    ISymbolCollectionPtr pSymbolColl(CLSID_SymbolCollection);

    ITextSymbolPtr myTxtSym(CLSID_TextSymbol);
    //Set the font for myTxtSym
    IFontDispPtr myFont(CLSID_StdFont);
    IFontPtr pFt = myFont;
    pFt->put_Name(CComBSTR("Courier New"));
    CY cy;
    cy.Hi = 0;
    cy.Lo = 9;
    pFt->put_Size(cy);
    myTxtSym->put_Font(myFont);

    // Set the Color for myTxtSym to be Dark Red
    IRgbColorPtr myColor(CLSID_RgbColor);
    myColor->put_Red(150);
    myColor->put_Green(0);
    myColor->put_Blue (0);
    myTxtSym->put_Color(myColor);

    // Set other properties for myTxtSym
    myTxtSym->put_Angle(0);
    myTxtSym->put_RightToLeft(VARIANT_FALSE);
    myTxtSym->put_VerticalAlignment(esriTVABaseline);
    myTxtSym->put_HorizontalAlignment(esriTHAFull);
    myTxtSym->put_Size(200);
    //myTxtSym->put_Case(esriTCNormal);

    ISymbolPtr pSymbol = myTxtSym;
    pSymbolColl->putref_Symbol(0, pSymbol);

    //set up the annotation labeling properties including the expression
    IAnnotateLayerPropertiesPtr pAnnoProps(CLSID_LabelEngineLayerProperties);
    pAnnoProps->put_FeatureLinked(VARIANT_TRUE);
    pAnnoProps->put_AddUnplacedToGraphicsContainer(VARIANT_FALSE);
    pAnnoProps->put_CreateUnplacedElements(VARIANT_TRUE);
    pAnnoProps->put_DisplayAnnotation(VARIANT_TRUE);
    pAnnoProps->put_UseOutput(VARIANT_TRUE);

    ILabelEngineLayerPropertiesPtr pLELayerProps = pAnnoProps;
    IAnnotationExpressionEnginePtr aAnnoVBScriptEngine(CLSID_AnnotationVBScriptEngine);
    pLELayerProps->putref_ExpressionParser(aAnnoVBScriptEngine);
    pLELayerProps->put_Expression(CComBSTR("[DESCRIPTION]"));
    pLELayerProps->put_IsExpressionSimple(VARIANT_TRUE);
    pLELayerProps->put_Offset(0);
    pLELayerProps->put_SymbolID(0);
    pLELayerProps->putref_Symbol(myTxtSym);

    IAnnotateLayerTransformationPropertiesPtr pATP = pAnnoProps;
    double dRefScale;
    pGLS->get_ReferenceScale(&dRefScale);
    pATP->put_ReferenceScale(dRefScale);
    pATP->put_Units(esriMeters);
    pATP->put_ScaleRatio(1);

    IAnnotateLayerPropertiesCollectionPtr pAnnoPropsColl(CLSID_AnnotateLayerPropertiesCollection);
    pAnnoPropsColl->Add(pAnnoProps);

    //' use the AnnotationFeatureClassDescription co - class to get the list of required fields and the default name of the shape field
    IObjectClassDescriptionPtr pOCDesc(CLSID_AnnotationFeatureClassDescription);
    IFeatureClassDescriptionPtr pFDesc = pOCDesc;

    IFieldsPtr pReqFields;
    IUIDPtr pInstCLSID;
    IUIDPtr pExtCLSID;
    CComBSTR bsShapeFieldName;

    pOCDesc->get_RequiredFields(&pReqFields);
    pOCDesc->get_InstanceCLSID(&pInstCLSID);
    pOCDesc->get_ClassExtensionCLSID(&pExtCLSID);
    pFDesc->get_ShapeFieldName(&bsShapeFieldName);

    /*IFieldsEditPtr ipFieldsEdit = pReqFields;

    //创建CAD文件中注记图层字段
    IFieldEditPtr ipFieldEdit;
    IFieldPtr ipField;

    // 创建 Entity ，记录esri实体类型
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR("Entity"));
    ipFieldEdit->put_AliasName(CComBSTR("Entity"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldsEdit->AddField(ipField);


    // 创建 Handle ，记录DWG实体编号
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Handle"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Handle"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);

    // 创建 BaseName ，记录DWG实体层名即DWG文件名
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"BaseName"));
    ipFieldEdit->put_AliasName(CComBSTR(L"BaseName"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);

    // 创建 Layer ，记录DWG实体层名
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Layer"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Layer"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);

    // 创建 Color ，记录DWG实体符号颜色
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Color"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Color"));
    ipFieldEdit->put_Type(esriFieldTypeInteger);
    ipFieldsEdit->AddField(ipField);

    // 创建 Thickness ，记录DWG实体厚度
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Thickness"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Thickness"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);

    // 创建 Elevation ，记录DWG实体高程值
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Elevation"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Elevation"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);

    / * / 创建 Text ，记录Text
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Text"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Text"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);* /

    // 创建 Height ，记录高度
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Height"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Height"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);


    // 创建 FontID ，记录FontID
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"FontId"));
    ipFieldEdit->put_AliasName(CComBSTR(L"FontId"));
    ipFieldEdit->put_Type(esriFieldTypeSmallInteger);
    ipFieldsEdit->AddField(ipField);


    //////////创建对照表字段/////////////////////////////////////
    // 创建 FeatureCode字段
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    CComBSTR bsStr;
    bsStr = "FeatureCode";
    ipFieldEdit->put_Name(bsStr);
    ipFieldEdit->put_AliasName(bsStr);
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);

    // 创建 FeatureName字段
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    bsStr = "FeatureName";
    ipFieldEdit->put_Name(bsStr);
    ipFieldEdit->put_AliasName(bsStr);
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);

    // 创建 LayerName字段
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    bsStr = "LayerName";
    ipFieldEdit->put_Name(bsStr);
    ipFieldEdit->put_AliasName(bsStr);
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);*/

    //' create the new class
    hr = PFWSAnno->CreateAnnotationClass(bsAnnoName, pFields, pInstCLSID, pExtCLSID, bsShapeFieldName, CComBSTR(""), pTarFDS, 0, pAnnoPropsColl, pGLS, pSymbolColl, VARIANT_TRUE, ppAnnoFtCls);

    return hr;
}


/************************************************************************
简要描述 : CAD文件注记图层数据转换
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
BOOL XDBPreProcessDriver::CAD_AnnotationConvert(IWorkspace* pTargetWS, IDataset* pTargetDataset, CString sDwgFilePath, CString sShowedFilePath)
{
    HRESULT hr;

    IFeatureWorkspacePtr pFWS = pTargetWS;

    PrgbarSetText("正在准备读取注记数据...");
    PrgbarSetPos(0);


    if (sShowedFilePath.IsEmpty())
    {
        sShowedFilePath = sDwgFilePath;
    }

    //IFeatureDatasetPtr pFDataset;
    //hr = pFWS->OpenFeatureDataset(CComBSTR("DWG_X"), &pFDataset);

    //创建目标注记图层
    IFeatureClassPtr pTarAnnoFtCls;

    hr = pFWS->OpenFeatureClass(CComBSTR("Annotation"), &pTarAnnoFtCls);
    if (pTarAnnoFtCls == NULL)
    {
        if (pTargetDataset != NULL)
        {
            IFeatureDatasetPtr pFDataset = pTargetDataset;
            CreateAnnoFtCls(pFDataset, CComBSTR("Annotation"), &pTarAnnoFtCls);
        }
        else
        {
            CreateAnnoFtCls(pTargetWS, CComBSTR("Annotation"), &pTarAnnoFtCls);
        }

        if (pTarAnnoFtCls == NULL)
        {
            return FALSE;
        }
    }

    //打开CAD注记图层数据
    IWorkspaceFactory2Ptr pCadWSFact(CLSID_CadWorkspaceFactory);
    IWorkspacePtr pCadWS;
    hr = pCadWSFact->OpenFromFile(CComBSTR(GetFileDirectory(sDwgFilePath)), 0, &pCadWS);

    IFeatureWorkspacePtr pCadFtWS = pCadWS;

    int iPos = -1;
    iPos = sDwgFilePath.ReverseFind('\\');
    if ( iPos == -1 )
    {
        iPos = sDwgFilePath.ReverseFind('/');
    }


    CString  sFileName = sDwgFilePath.Mid(iPos + 1);
    CString sBaseName;
    sBaseName = sFileName.Mid(0, sFileName.Find('.'));
    IFeatureDatasetPtr pCadFtDS;
    pCadFtWS->OpenFeatureDataset(CComBSTR(sFileName), &pCadFtDS);

    IFeatureClassContainerPtr pFeatureClassContainer = pCadFtDS;

    long numFtCls;
    pFeatureClassContainer->get_ClassCount(&numFtCls);

    IFeatureClassPtr pCadFtCls;
    for (int i = 0; i < numFtCls; i++)
    {
        hr = pFeatureClassContainer->get_Class(i, &pCadFtCls);

        BSTR bsFtName;
        pCadFtCls->get_AliasName(&bsFtName);
        CString sCadAnnoLayerName = bsFtName ;
        esriFeatureType ftType;
        hr = pCadFtCls->get_FeatureType(&ftType);
        if (ftType == esriFTCoverageAnnotation)
        {
            break;
        }

    }

    long numCadFeats;
    pCadFtCls->FeatureCount(NULL, &numCadFeats);

    if (numCadFeats <= 0)
    {
        return TRUE;
    }

    //pTextProgressCtrl->SetPos(0);
    //pTextProgressCtrl->SetRange(0, numCadFeats);
    //pTextProgressCtrl->SetWindowText("正在读取" + sDwgFilePath + "中的注记数据...");

    PrgbarRange(0, numCadFeats);
    PrgbarSetText("正在读取" + sShowedFilePath + "中的注记数据...");
    PrgbarSetPos(0);


    //初始化编码对照表
    //if (m_dwgReader.m_aryCodes.GetCount() <= 0)
    //{
    //    m_dwgReader.InitCompareCodes();
    //}

    IFeatureCursorPtr pCadCur;
    hr = pCadFtCls->Search(NULL, VARIANT_FALSE, &pCadCur);

    long lStyleFldInd;
    long lTextFldInd;
    long lHeightFldInd;
    long lAngleFldInd;
    long lLayerIdx;

    //目标要素类中的BaseName索引值
    long lBaseNameFldInd;

    CString sStyle = "";
    CString sText = "";
    double dHeight = 0;
    double dAngle = 0;
    CString sLayer = "";

    CComVariant vtVal;

    pCadFtCls->FindField(CComBSTR("Style"), &lStyleFldInd);
    pCadFtCls->FindField(CComBSTR("Text"), &lTextFldInd);
    pCadFtCls->FindField(CComBSTR("Height"), &lHeightFldInd);
    //pCadFtCls->FindField(CComBSTR("TxtAngle"), &lAngleFldInd);
    pCadFtCls->FindField(CComBSTR("TxtAngle"), &lAngleFldInd);
    pCadFtCls->FindField(CComBSTR("Layer"), &lLayerIdx);

    pTarAnnoFtCls->FindField(CComBSTR("BaseName"), &lBaseNameFldInd);

    IFeaturePtr pCreatedFeat;
    IAnnotationFeaturePtr pTarAnnoFeat;
    IFeaturePtr pCadFeature;
    IGeometryPtr pCadGeo;
    while (pCadCur->NextFeature(&pCadFeature) == S_OK)
    {
        //pTextProgressCtrl->StepIt();
        PrgbarStepIt();

        //创建注记图层要素
        hr = pTarAnnoFtCls->CreateFeature(&pCreatedFeat);
        pTarAnnoFeat = pCreatedFeat;

        //Style
        pCadFeature->get_Value(lStyleFldInd, &vtVal);
        if (vtVal.vt != VT_NULL && vtVal.vt != VT_EMPTY)
        {
            sStyle = vtVal.bstrVal;
        }

        //Text
        pCadFeature->get_Value(lTextFldInd, &vtVal);
        if (vtVal.vt != VT_NULL && vtVal.vt != VT_EMPTY)
        {
            sText = vtVal.bstrVal;
        }

        //Height
        pCadFeature->get_Value(lHeightFldInd, &vtVal);
        if (vtVal.vt != VT_NULL && vtVal.vt != VT_EMPTY)
        {
            dHeight = vtVal.dblVal;
        }

        //Angle
        pCadFeature->get_Value(lAngleFldInd, &vtVal);
        if (vtVal.vt != VT_NULL && vtVal.vt != VT_EMPTY)
        {
            dAngle = vtVal.dblVal;
        }

        //Layer
        pCadFeature->get_Value(lLayerIdx, &vtVal);
        if (vtVal.vt != VT_NULL && vtVal.vt != VT_EMPTY)
        {
            sLayer = vtVal.bstrVal;
        }


        //创建 Element
        hr = pCadFeature->get_ShapeCopy(&pCadGeo);

        IEnvelopePtr pCadEnvelope;
        hr = pCadGeo->get_Envelope(&pCadEnvelope);

        double dMinX, dMaxX, dMinY, dMaxY;
        pCadEnvelope->get_XMin(&dMinX);
        pCadEnvelope->get_XMax(&dMaxX);
        pCadEnvelope->get_YMin(&dMinY);
        pCadEnvelope->get_YMax(&dMaxY);

        double pos_x, pos_y;

        pos_x = (dMinX + dMaxX) / 2;
        pos_y = (dMinY + dMaxY) / 2;

        //下移40
        //if (sLayer.Find("线标注")>=0)
        {
            //pos_y = pos_y - 40;
            sText.Replace('\\', 0x0a);
        }



        //ITextElementPtr pTextElement = MakeTextElementByStyle(sStyle, sText, dAngle, dHeight, (dMinX + dMaxX) / 2, (dMinY + dMaxY) / 2, 1);
        ITextElementPtr pTextElement = MakeTextElementByStyle(sStyle, sText, dAngle, dHeight, pos_x, pos_y, 1);

        IElementPtr pElement = pTextElement;
        hr = pTarAnnoFeat->put_Annotation(pElement);

        //复制要素属性
        CopyFeatureAttr(pCadFeature, pCreatedFeat);

        //添加BASENAME值
        pCreatedFeat->put_Value(lBaseNameFldInd, CComVariant(sBaseName));


        //编码对照
        IFeatureBufferPtr pFeatBuf = pCreatedFeat;
        m_pDwgReader->CompareCodes(pFeatBuf.GetInterfacePtr());

        hr = pCreatedFeat->Store();

    }

    //清理对照表
    //    m_dwgReader.CleanCompareCodes();

    return TRUE;

}


//得到文件所在的目录
CString XDBPreProcessDriver::GetFileDirectory(const CString& sFullPath)
{
    int iPos = -1;
    iPos = sFullPath.ReverseFind('\\');
    if (iPos == -1)
    {
        iPos = sFullPath.ReverseFind('/');
    }

    if (iPos >= 0)
    {
        return sFullPath.Left(iPos);
    }

    return "";
}

/************************************************************************
简要描述 : 生成注记Element
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
ITextElement* XDBPreProcessDriver::MakeTextElementByStyle(CString strStyle, CString strText, double dblAngle,
        double dblHeight, double dblX,
        double dblY, double ReferenceScale)
{

    HRESULT hr;

    ITextElementPtr pTextElement;

    ISimpleTextSymbolPtr pTextSymbol;

    CString strHeight;

    pTextSymbol.CreateInstance(CLSID_TextSymbol);

    IFontDispPtr fntDisp(CLSID_StdFont);
    IFontPtr fnt = fntDisp;
    fnt->put_Name(CComBSTR("宋体"));
    CY cy;
    cy.int64 = 9;
    fnt->put_Size(cy);

    //'Set the text symbol font by getting the IFontDisp interface
    pTextSymbol->put_Font(fntDisp);

    double mapUnitsInches;

    IUnitConverterPtr pUnitConverter(CLSID_UnitConverter);

    pUnitConverter->ConvertUnits(dblHeight, esriMeters, esriInches, &mapUnitsInches);

    strHeight.Format("%f", (mapUnitsInches * 72) / ReferenceScale);

    double dSize = atof(strHeight);
    pTextSymbol->put_Size(dSize);

    pTextSymbol->put_HorizontalAlignment(esriTHALeft);
    pTextSymbol->put_VerticalAlignment(esriTVABaseline);
    //pTextSymbol->put_XOffset(strText.GetLength() / 4*dSize);

    pTextElement.CreateInstance(CLSID_TextElement);
    hr = pTextElement->put_ScaleText(VARIANT_FALSE);// VARIANT_TRUE);

    hr = pTextElement->put_Text(CComBSTR(strText));
    hr = pTextElement->put_Symbol(pTextSymbol);

    IElementPtr pElement = pTextElement;
    IPointPtr pPoint(CLSID_Point);
    hr = pPoint->PutCoords(dblX, dblY);
    hr = pElement->put_Geometry(pPoint);

    if (fabs(dblAngle) > 0)
    {
        ITransform2DPtr pTransform2D = pTextElement;
        double rotationAngle = dblAngle / 180 * 3.1415926;
        pTransform2D->Rotate(pPoint, rotationAngle);
    }

    return pTextElement.Detach();

}

/************************************************************************
简要描述 : 对注记图层进行分层
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
bool XDBPreProcessDriver::SplitAnnotationLayer(CString sBaseLayerName, CMapStringToString* pSplitLayerNames, IFeatureClass* pInFtCls, IWorkspace* pTargetWS)
{

    if (pInFtCls == NULL) return false;

    HRESULT hr;
    POSITION pos = pSplitLayerNames->GetStartPosition();
    CString sLayerName;
    CString sLayerAlias;

    while (pos != NULL)
    {
        pSplitLayerNames->GetNextAssoc(pos, sLayerName, sLayerAlias);

        if (sLayerName.IsEmpty()) continue;

        //针对济南项目,判断图层名的后两位是不是AN
        CString sGeoType = sLayerName.Right(2);
        if (sGeoType.CompareNoCase("AN") != 0 )
        {
            continue;
        }

        //根据LayerName分层
        IQueryFilterPtr pSplitFilter(CLSID_QueryFilter);
        CString sWhereClause;
        sWhereClause.Format("GDB_LAYER='%s'", sLayerName);
        hr = pSplitFilter->put_WhereClause(CComBSTR(sWhereClause));

        long numFeats;

        hr = pInFtCls->FeatureCount(pSplitFilter, &numFeats);
        if (numFeats <= 0) continue;

        IFeatureWorkspacePtr pTarFtWS = pTargetWS;

        ISpatialReferencePtr pSpRef = API_GetSpatialReference(pInFtCls);

        IFeatureDatasetPtr pTarAnnoDS;
        hr = pTarFtWS->OpenFeatureDataset(CComBSTR("DWG_X"), &pTarAnnoDS);
        if (pTarAnnoDS == NULL)
        {
            hr = pTarFtWS->CreateFeatureDataset(CComBSTR("DWG_X"), pSpRef, &pTarAnnoDS);
        }

        try
        {
            //创建目标注记图层
            IFeatureClassPtr pTarAnnoFtCls;
            hr = pTarFtWS->OpenFeatureClass(CComBSTR(sLayerName), &pTarAnnoFtCls);
            if (pTarAnnoFtCls == NULL)
            {
                CreateAnnoFtCls(pTarAnnoDS, CComBSTR(sLayerName), &pTarAnnoFtCls);
            }

            IFeatureCursorPtr pTarAnnoCur;
            pTarAnnoFtCls->Insert(VARIANT_TRUE, &pTarAnnoCur);

            //复制要素到目标图层
            IFeaturePtr pSrcFeat;
            CComVariant OID;
            IFeatureCursorPtr pFtCur;
            pInFtCls->Search(pSplitFilter, VARIANT_FALSE, &pFtCur);
            while (pFtCur->NextFeature(&pSrcFeat) == S_OK)
            {
                IFeatureBufferPtr pTarAnnoFtBuf = pSrcFeat;
                hr = pTarAnnoCur->InsertFeature(pTarAnnoFtBuf, &OID);

                hr = pTarAnnoCur->Flush();

                pSrcFeat.Release();
            }
        }
        catch (...)
        {
            CString sLog;
            sLog.Format("SplitAnnotationLayer() 在进行%s图层的 [%s] 分层时发生错误。", sBaseLayerName, sWhereClause);
            m_pLogRec->WriteLog(sLog);
        }

    }

    return true;
}

/********************************************************************
简要描述 : 分解字符串
输入参数 :
返 回 值 :
修改日志 :
*********************************************************************/
void XDBPreProcessDriver::ParseStr(CString sSrcStr, char chrSeparator, CStringList& lstItems)
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


/********************************************************************
简要描述 : 线构面后期处理(针对济南项目，没有特征值，需要遍历所有数据)
输入参数 :
返 回 值 :
修改日志 :
*********************************************************************/
HRESULT XDBPreProcessDriver::PostBuildPolygon2(CString sPolygonLayerName, IFeatureClassPtr pInFtCls, CString sAreaField)
{
    //try
    //{
    HRESULT hr;

    ISpatialReferencePtr pSpRef = API_GetSpatialReference(pInFtCls);

    //被完全包含该面要素的面只生成环状面
    IQueryFilterPtr pBigRegionFilter(CLSID_QueryFilter);
    CString sWhereClause;
    sWhereClause.Format("%s like '*-*'", sAreaField);
    hr = pBigRegionFilter->put_WhereClause(CComBSTR(sWhereClause));

    long numFeats;
    hr = pInFtCls->FeatureCount(pBigRegionFilter , &numFeats);

    if (numFeats <= 0)
    {
        return S_OK;
    }

    long lAreaFld;
    pInFtCls->FindField(CComBSTR(sAreaField), &lAreaFld);

    PrgbarRange(0, numFeats);
    PrgbarSetText("正在进行" + sPolygonLayerName + "图层的构面后处理...");
    PrgbarSetPos(0);

    CList<long, long> bigPolygonOIDsList;
    CList<long, long> delOIDsList;

    IFeaturePtr pFeature;

    IFeatureCursorPtr pFtCur;
    hr = pInFtCls->Update(pBigRegionFilter, VARIANT_FALSE, &pFtCur);
    while (pFtCur->NextFeature(&pFeature) == S_OK)
    {
        PrgbarStepIt();

        IGeometryPtr pBigGeoShape;
        hr = pFeature->get_ShapeCopy(&pBigGeoShape);
        hr = pBigGeoShape->Project(pSpRef);

        ISpatialFilterPtr pSpFilter(CLSID_SpatialFilter);
        pSpFilter->putref_Geometry(pBigGeoShape);
        pSpFilter->put_SpatialRel(esriSpatialRelIntersects);//esriSpatialRelContains);

        long numInsidePolygon;
        hr = pInFtCls->FeatureCount(pSpFilter, &numInsidePolygon);
        if (numInsidePolygon <= 0) continue;

        //外部部大面的要素ID
        long lBigPolygonOID;
        hr = pFeature->get_OID(&lBigPolygonOID);

        bigPolygonOIDsList.AddTail(lBigPolygonOID);

        IClonePtr pSClone = pBigGeoShape;
        IClonePtr pTClone;
        hr = pSClone->Clone(&pTClone);
        IGeometryPtr pBigGeoCopy = pTClone;
        hr = pBigGeoCopy->Project(pSpRef);

        //重新构面
        ISegmentCollectionPtr pPolygonSegm(pBigGeoCopy);

        IGeometryPtr pInsidePolygonShape;
        IFeaturePtr pInsidePolygonFeat;
        IFeatureCursorPtr pInsidePolygonCur;
        hr = pInFtCls->Search(pSpFilter, VARIANT_FALSE, &pInsidePolygonCur);
        while (pInsidePolygonCur->NextFeature(&pInsidePolygonFeat) == S_OK)
        {
            //外部部大面的要素ID
            //去掉搜索面本身
            long lInsideOID;
            hr = pInsidePolygonFeat->get_OID(&lInsideOID);
            if (lBigPolygonOID == lInsideOID)
            {
                continue;
            }

            hr = pInsidePolygonFeat->get_ShapeCopy(&pInsidePolygonShape);
            hr = pInsidePolygonShape->Project(pSpRef);

            //过滤掉错误的搜索结果
            IRelationalOperatorPtr pRelaOper = pInsidePolygonShape ;
            VARIANT_BOOL vbWithin;
            hr = pRelaOper->Within(pBigGeoShape, &vbWithin);
            if (vbWithin == VARIANT_FALSE) continue;

            ISegmentCollectionPtr pSegColl;
            pSegColl = pInsidePolygonShape;
            if (pSegColl == NULL) continue;

            hr = pPolygonSegm->AddSegmentCollection(pSegColl);

            //保存需要删除内部的小面的OID
            if (delOIDsList.Find(lInsideOID) == NULL && bigPolygonOIDsList.Find(lInsideOID) == NULL)
            {
                delOIDsList.AddTail(lInsideOID);
            }

            //hr = pInsidePolygonFeat->Delete();

        }

        ITopologicalOperator3Ptr pBigPolygonOper = pPolygonSegm;//pBigGeoShape;
        hr = pBigPolygonOper->put_IsKnownSimple(VARIANT_FALSE);
        hr = pBigPolygonOper->Simplify();

        //修改大面的外形
        hr = pFeature->putref_Shape(pBigGeoCopy);


        try
        {
            //修改面积字段属性值
            CComVariant vtVal;
            pFeature->get_Value(lAreaFld, &vtVal);
            CString sArea = vtVal.bstrVal;
            CStringList lstArea;

            ParseStr(sArea, '-', lstArea);

            double dArea = atof(lstArea.GetAt(lstArea.FindIndex(0)));
            for (int i = 1;i < lstArea.GetCount();i++)
            {
                double dtmp = atof(lstArea.GetAt(lstArea.FindIndex(i)));
                dArea -= dtmp;
            }

            sArea.Format("%0.2f", dArea);
            pFeature->put_Value(lAreaFld, CComVariant(sArea));
        }
        catch (...)
        {
            int iErr = ::GetLastError();
        }

        //保存
        int ihr = pFeature->Store();

        pFeature.Release();

    }

    /*/删除内部小面
    if (delOIDsList.GetCount() > 0)
    {
    	POSITION pos = delOIDsList.GetHeadPosition();
    	while (pos != NULL)
    	{
    		IFeaturePtr pDelFeat;
    		long lOID = delOIDsList.GetNext(pos);

    		hr = pInFtCls->GetFeature(lOID, &pDelFeat);
    		if (pDelFeat != NULL)
    		{
    			hr = pDelFeat->Delete();
    		}
    		
    	}
    }*/

    return S_OK;
    /*}
    catch (...)
    {
    	return S_FALSE;
    }*/

}

/************************************************************************
简要描述 : 线构面后期处理
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
HRESULT XDBPreProcessDriver::PostBuildPolygon(IFeatureClassPtr pInFtCls)
{
    HRESULT hr;

    IQueryFilterPtr pFilter(CLSID_QueryFilter);

    CString sWhereClause;
    long numFeats;

    ISpatialReferencePtr pSpRef = API_GetSpatialReference(pInFtCls);//GetSysSpatialRef();

    CComVariant vtVal;
    long lFldThickness;
    CString sThickness;
    pInFtCls->FindField(CComBSTR("Thickness"), &lFldThickness);

    //被完全包含该面要素的面只生成环状面
    sWhereClause.Format("Thickness like '%s'", "*5");
    hr = pFilter->put_WhereClause(CComBSTR(sWhereClause));
    hr = pInFtCls->FeatureCount(pFilter, &numFeats);
    if (numFeats > 0)
    {
        IFeaturePtr pFeature;

        IFeatureCursorPtr pFtCur;
        hr = pInFtCls->Search(pFilter, VARIANT_FALSE, &pFtCur);
        while (pFtCur->NextFeature(&pFeature) == S_OK)
        {
            IGeometryPtr pGeoShape;
            hr = pFeature->get_ShapeCopy(&pGeoShape);
            hr = pGeoShape->Project(pSpRef);

            ISpatialFilterPtr pSpFilter(CLSID_SpatialFilter);
            pSpFilter->putref_Geometry(pGeoShape);
            pSpFilter->put_SpatialRel(esriSpatialRelWithin);

            long numBigPolygon;
            hr = pInFtCls->FeatureCount(pSpFilter, &numBigPolygon);
            if (numBigPolygon <= 0) continue;

            //内部小面的要素ID
            long lInsideOID;
            hr = pFeature->get_OID(&lInsideOID);

            //内部小面Thickness
            pFeature->get_Value(lFldThickness, &vtVal);
            sThickness = vtVal.bstrVal;

            IGeometryPtr pBigPolygonShape;
            IFeaturePtr pBigPolygonFeat;
            IFeatureCursorPtr pBigPolygonCur;
            hr = pInFtCls->Update(pSpFilter, VARIANT_FALSE, &pBigPolygonCur);
            while (pBigPolygonCur->NextFeature(&pBigPolygonFeat) == S_OK)
            {
                //外部部大面的要素ID
                //去掉搜索面本身
                long lOutsideOID;
                hr = pBigPolygonFeat->get_OID(&lOutsideOID);
                if (lInsideOID == lOutsideOID)
                {
                    continue;
                }

                //过滤掉与小面编码不一致的外面大面
                CString sBigPolygonThickness;
                pBigPolygonFeat->get_Value(lFldThickness, &vtVal);
                sBigPolygonThickness = vtVal.bstrVal;
                if (sThickness.CompareNoCase(sBigPolygonThickness) != 0)
                {
                    continue;
                }

                hr = pBigPolygonFeat->get_ShapeCopy(&pBigPolygonShape);
                hr = pBigPolygonShape->Project(pSpRef);

                ISegmentCollectionPtr pPolygonSegm(pBigPolygonShape);
                ISegmentCollectionPtr pSegColl;

                pSegColl = pGeoShape;

                if (pSegColl == NULL) continue;

                //生成面
                long numSegCol;
                pSegColl->get_SegmentCount(&numSegCol);
                for (int i = 0; i < numSegCol; i++)
                {
                    ISegmentPtr pSegment;
                    pSegColl->get_Segment(i, &pSegment);
                    pPolygonSegm->AddSegment(pSegment);
                }

                ITopologicalOperator3Ptr pBigPolygonOper = pBigPolygonShape;
                hr = pBigPolygonOper->put_IsKnownSimple(VARIANT_FALSE);
                hr = pBigPolygonOper->Simplify();

                hr = pBigPolygonFeat->putref_Shape(pBigPolygonShape);

                //修改大面的外形
                hr = pBigPolygonFeat->Store();

                //删除内部的小面
                hr = pFeature->Delete();

            }
        }
    }


    //被完全包含该面要素的面生成环和保留环内部的面
    sWhereClause.Format("Thickness like '%s'", "*6");
    hr = pFilter->put_WhereClause(CComBSTR(sWhereClause));
    hr = pInFtCls->FeatureCount(pFilter, &numFeats);
    if (numFeats > 0)
    {
        IFeaturePtr pFeature;

        IFeatureCursorPtr pFtCur;
        hr = pInFtCls->Search(pFilter, VARIANT_FALSE, &pFtCur);
        while (pFtCur->NextFeature(&pFeature) == S_OK)
        {
            IGeometryPtr pGeoShape;
            hr = pFeature->get_ShapeCopy(&pGeoShape);
            hr = pGeoShape->Project(pSpRef);

            ISpatialFilterPtr pSpFilter(CLSID_SpatialFilter);
            pSpFilter->putref_Geometry(pGeoShape);
            pSpFilter->put_SpatialRel(esriSpatialRelWithin);

            long numBigPolygon;
            hr = pInFtCls->FeatureCount(pSpFilter, &numBigPolygon);
            if (numBigPolygon <= 0) continue;

            //内部小面的要素ID
            long lInsideOID;
            hr = pFeature->get_OID(&lInsideOID);

            IGeometryPtr pBigPolygonShape;
            IFeaturePtr pBigPolygonFeat;
            IFeatureCursorPtr pBigPolygonCur;
            hr = pInFtCls->Update(pSpFilter, VARIANT_FALSE, &pBigPolygonCur);
            while (pBigPolygonCur->NextFeature(&pBigPolygonFeat) == S_OK)
            {
                //外部部大面的要素ID
                //去掉搜索面本身
                long lOutsideOID;
                hr = pBigPolygonFeat->get_OID(&lOutsideOID);
                if (lInsideOID == lOutsideOID)
                {
                    continue;
                }

                hr = pBigPolygonFeat->get_ShapeCopy(&pBigPolygonShape);
                hr = pBigPolygonShape->Project(pSpRef);

                ISegmentCollectionPtr pPolygonSegm(pBigPolygonShape);
                ISegmentCollectionPtr pSegColl;

                pSegColl = pGeoShape;

                if (pSegColl == NULL) continue;

                //生成面
                long numSegCol;
                pSegColl->get_SegmentCount(&numSegCol);
                for (int i = 0; i < numSegCol; i++)
                {
                    ISegmentPtr pSegment;
                    pSegColl->get_Segment(i, &pSegment);
                    pPolygonSegm->AddSegment(pSegment);
                }

                ITopologicalOperator3Ptr pBigPolygonOper = pBigPolygonShape;
                hr = pBigPolygonOper->put_IsKnownSimple(VARIANT_FALSE);
                hr = pBigPolygonOper->Simplify();

                hr = pBigPolygonFeat->putref_Shape(pBigPolygonShape);

                //修改大面的外形
                hr = pBigPolygonFeat->Store();

                //不删除内部的小面

            }

        }

    }

    return S_OK;
}

/************************************************************************
简要描述 : 得到所有的注册应用名
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
void XDBPreProcessDriver::GetRegAppNames(CStringList& lstAppNames)
{
    lstAppNames.RemoveAll();
    if (m_pSysFtWs != NULL)
    {
        ITablePtr pTable;
        m_pSysFtWs->OpenTable(CComBSTR("EXTRA_ATTRIB_CONFIG"), &pTable);

        if (pTable != NULL)
        {
            HRESULT hr;
            IEsriCursorPtr pCursor;
            hr = pTable->Search(NULL, VARIANT_FALSE, &pCursor);
            if (pCursor != NULL)
            {
                long lRegAppName;
                //pTable->FindField(CComBSTR("FeatureClassName"), &lFeatureClassNameFieldIndex);
                pTable->FindField(CComBSTR("RegAppName"), &lRegAppName);

                IEsriRowPtr pRow;
                CComVariant vtVal;
                pCursor->NextRow(&pRow);
                while (pRow != NULL)
                {
                    CString sRegAppName;
                    //RegAppName
                    hr = pRow->get_Value(lRegAppName, &vtVal);
                    if (vtVal.vt != VT_NULL && vtVal.vt != VT_EMPTY)
                    {
                        sRegAppName = vtVal.bstrVal;

                        lstAppNames.AddTail(sRegAppName);
                    }

                    hr = pCursor->NextRow(&pRow);
                }
            }
        }
    }

}

/************************************************************************
简要描述 : 挂接外接表
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
void XDBPreProcessDriver::JoinAddinTable(ITablePtr pExtraTable)
{

    HRESULT hr;

    WriteLog("开始进行外挂表的属性挂接...");
    //////////得到配置文件设置////////////////////////////////////////////////
    TCHAR lpFile[MAX_PATH];
    ::GetModuleFileName(NULL, lpFile, MAX_PATH);
    *_tcsrchr(lpFile, '\\') = 0;

    CString sModuleDir = lpFile;

    // 保存配置文件
    CString sIniPath;
    sIniPath.Format("%s\\sys_config.ini", lpFile);

    CHAR inBuf[MAX_PATH];

    CString sJoinedLayerName;
    //CString sExtraMdbPath;
    CString sExtraTableName;

    ::GetPrivateProfileString("APP", "JoinedLayerName", "", inBuf, 255, sIniPath);
    sJoinedLayerName = inBuf;
    //////////////////////////////////////////////////////////////////////////
    //得到数据集名称
    CComBSTR bsDsName;
    IEnumDatasetNamePtr pDsNames;
    hr = m_pOutWS->get_DatasetNames(esriDTFeatureDataset, &pDsNames);
    IDatasetNamePtr pDsName;
    hr = pDsNames->Next(&pDsName);
    if (pDsName != NULL)
    {
        hr = pDsName->get_Name(&bsDsName);
    }

    sJoinedLayerName = CString(bsDsName) + "_" + sJoinedLayerName;

    //打开要挂接外接表数据的要素类
    IFeatureWorkspacePtr pFtWS = m_pOutWS;
    IFeatureClassPtr pTargetFtCls;
    hr = pFtWS->OpenFeatureClass(CComBSTR(sJoinedLayerName), &pTargetFtCls);
    if (pTargetFtCls == NULL)
    {
        WriteLog("临时库中不存在" + sJoinedLayerName + "图层，无法进行外挂表的挂接。");
        return;
    }


    //::GetPrivateProfileString("APP", "ExtraMdb", "", inBuf, 255, sIniPath);
    //sExtraMdbPath = inBuf;
    //sExtraMdbPath = sModuleDir + "\\" + sExtraMdbPath;

    /*::GetPrivateProfileString("APP", "ExtraTableName", "", inBuf, 255, sIniPath);
    sExtraTableName = inBuf;



    //////////////////////////////////////////////////////////////////////////
    // 打开外挂表
    ITablePtr pExtraTable;
    IWorkspacePtr pExtraWS;
    IWorkspaceFactoryPtr pWSFact(CLSID_AccessWorkspaceFactory);
    hr = pWSFact->OpenFromFile(CComBSTR(sExtraFilePath), 0, &pExtraWS);
    IFeatureWorkspacePtr pExtraFtWS = pExtraWS;
    if (pExtraFtWS == NULL)
    {
    WriteLog("指定的外挂MDB文件不是数据文件，无法进行外接属性的挂接。");
    return;
    }
    hr = pExtraFtWS->OpenTable(CComBSTR(sExtraTableName), &pExtraTable);
    if (pExtraTable == NULL)
    {
    WriteLog("配置文件中指定的外挂表不存在，无法进行外接属性的挂接。");
    return;
    }*/


    //保存所有需要添加的字段名称
    CStringList lstAddFieldNames;

    //////////////////////////////////////////////////////////////////////////
    //扩展字段的添加处理
    IFieldsPtr pExtraFields;
    IFieldPtr pExtFld;
    CComBSTR bsFldName;
    hr = pExtraTable->get_Fields(&pExtraFields);
    long numExtraField;
    hr = pExtraFields->get_FieldCount(&numExtraField);
    for (int i = 0; i < numExtraField; i++)
    {
        hr = pExtraFields->get_Field(i, &pExtFld);
        hr = pExtFld->get_Name(&bsFldName);

        long lFldInd;
        hr = pTargetFtCls->FindField(bsFldName, &lFldInd);
        //目标要素类中不存在该字段则添加
        if (lFldInd == -1)
        {
            hr = pTargetFtCls->AddField(pExtFld);
        }

        CString sAddFieldName = bsFldName;
        lstAddFieldNames.AddTail(sAddFieldName);

    }


    //////////////////////////////////////////////////////////////////////////
    //根据DKBH（地块编号）挂接属性值
    IQueryFilterPtr pFilter(CLSID_QueryFilter);
    CString sWhereClause;
    sWhereClause = "DKBH<>''";
    pFilter->put_WhereClause(CComBSTR(sWhereClause));
    long numFeats;
    hr = pTargetFtCls->FeatureCount(pFilter, &numFeats);
    if (numFeats <= 0) return;

    //pTextProgressCtrl->SetRange(0, numFeats);
    //pTextProgressCtrl->SetPos(0);
    //pTextProgressCtrl->SetWindowText("正在进行外挂表数据的挂接处理...");

    int iSucceedJoined = 0;
    CString sLog;

    IFeaturePtr pTarFeat;
    IFeatureCursorPtr pTarFtCur;
    long lTarFldInd;
    CComVariant vtVal;

    hr = pTargetFtCls->FindField(CComBSTR("DKBH"), &lTarFldInd);
    hr = pTargetFtCls->Update(pFilter, VARIANT_FALSE, &pTarFtCur);
    while (pTarFtCur->NextFeature(&pTarFeat) == S_OK)
    {
        IQueryFilterPtr pExtTabFilter(CLSID_QueryFilter);
        CString sExtTabWhereClause;
        CString sDKBH;

        hr = pTarFeat->get_Value(lTarFldInd, &vtVal);
        if (vtVal.vt == VT_NULL || vtVal.vt == VT_EMPTY) continue;
        sDKBH = vtVal.bstrVal;

        sExtTabWhereClause.Format("DKBH='%s'", sDKBH);
        pExtTabFilter->put_WhereClause(CComBSTR(sExtTabWhereClause));

        long numRows;
        hr = pExtraTable->RowCount(pExtTabFilter, &numRows);
        if (numRows <= 0)
        {
            sLog.Format("地块编号为%s的记录在外挂表中不存在，请检查。", sDKBH);
            WriteLog(sLog);
            continue;
        }

        IEsriCursorPtr pTabCur;
        pExtraTable->Search(pExtTabFilter, VARIANT_FALSE, &pTabCur);

        IEsriRowPtr pTabRow;
        hr = pTabCur->NextRow(&pTabRow);

        CComVariant vtExtVal;
        CString sAddFieldName;
        POSITION pos = lstAddFieldNames.GetHeadPosition();
        while (pos != NULL)
        {
            sAddFieldName = lstAddFieldNames.GetNext(pos);

            long lSrcFldInd, lTarFldInd;

            hr = pExtraTable->FindField(CComBSTR(sAddFieldName), &lSrcFldInd);
            hr = pTargetFtCls->FindField(CComBSTR(sAddFieldName), &lTarFldInd);

            hr = pTabRow->get_Value(lSrcFldInd, &vtExtVal);

            if (vtExtVal.vt == VT_NULL || vtExtVal.vt == VT_EMPTY) continue;

            ////去字符空格
            //if(vtExtVal.vt == VT_BSTR)
            //{
            //	CString sTmp = vtExtVal.bstrVal;
            //	sTmp.Trim();
            //	vtExtVal = sTmp;
            //}

            //赋扩展属性值
            hr = pTarFeat->put_Value(lTarFldInd, vtExtVal);


        }

        hr = pTarFeat->Store();

        if (SUCCEEDED(hr))
        {
            iSucceedJoined++;
        }

        //        pTextProgressCtrl->StepIt();
    }

    sLog.Format("完成外挂表的属性挂接。成功挂接%d条记录，失败%d条。", iSucceedJoined, numFeats - iSucceedJoined);
    WriteLog(sLog);

}

/********************************************************************
简要描述 : 处理大面套小面的图形
输入参数 :
返 回 值 :
修改日志 :
*********************************************************************/
HRESULT XDBPreProcessDriver::DoPolygonPostProcess()
{
    HRESULT hr;
    IWorkspaceNamePtr pWSName;

    IFeatureWorkspace* pSysFtWS = m_pSysFtWs;
    ITablePtr pTbLayers;
    hr = pSysFtWS->OpenTable(CComBSTR("POLYGON_LAYERS"), &pTbLayers);
    if (pTbLayers == NULL) return S_FALSE;

    long numLayers;
    hr = pTbLayers->RowCount(NULL, &numLayers);

    if (numLayers <= 0) return S_FALSE;

    long lPolygonName;
    pTbLayers->FindField(CComBSTR("GDB_POLYGONLAYER"), &lPolygonName);
    if (lPolygonName == -1)
    {
        return S_FALSE;
    }

    //面积判断字段
    long lAreaField;
    pTbLayers->FindField(CComBSTR("GDB_AREAFIELD"), &lAreaField);

    IEsriCursorPtr pRowCursor;
    hr = pTbLayers->Search(NULL, VARIANT_FALSE, &pRowCursor);

    CMapStringToString mapPolygonLayer;

    IEsriRowPtr pRowLayerName;
    CComVariant vtVal;
    CString sPolygonName;
    CString sAreaField;

    while (pRowCursor->NextRow(&pRowLayerName) == S_OK)
    {
        pRowLayerName->get_Value(lPolygonName, &vtVal);
        sPolygonName = vtVal.bstrVal;

        if (lAreaField != -1)
        {
            pRowLayerName->get_Value(lAreaField, &vtVal);
            sAreaField  = vtVal.bstrVal;
        }
        else
        {
            sAreaField  = "";
        }

        mapPolygonLayer.SetAt(sPolygonName, sAreaField);

    }

    if (mapPolygonLayer.GetCount() <= 0)
    {
        WriteLog("不存在后期处理的图层。");
        return S_FALSE;
    }

    POSITION pos;
    pos = mapPolygonLayer.GetStartPosition();
    while (pos != NULL)
    {
        mapPolygonLayer.GetNextAssoc(pos, sPolygonName, sAreaField);

        IFeatureClassPtr pFtCls;
        hr = ((IFeatureWorkspacePtr)m_pOutWS)->OpenFeatureClass(CComBSTR(sPolygonName), &pFtCls);

        if (pFtCls == NULL)
        {
            continue;
        }

        if (!sAreaField.IsEmpty())
        {
            PostBuildPolygon2(sPolygonName, pFtCls, sAreaField);
        }

    }

    return S_OK;

}

/************************************************************************
简要描述 :得到图层名称
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
void XDBPreProcessDriver::GetFtClsNames(IWorkspace* pWorkspace, CDsNameList& lstNames)
{
    IEnumDatasetNamePtr pEnumDatasetName;
    pWorkspace->get_DatasetNames(esriDTFeatureDataset, &pEnumDatasetName);
    if (pEnumDatasetName != NULL)
    {
        IDatasetName* pDatasetName;
        pEnumDatasetName->Next(&pDatasetName);
        while (pDatasetName != NULL)
        {
            IEnumDatasetName* pEnumFeatureName;
            pDatasetName->get_SubsetNames(&pEnumFeatureName);
            if (pEnumFeatureName == NULL)
            {
                continue;
            }
            IDatasetName* pFtName;
            pEnumFeatureName->Next(&pFtName);
            while (pFtName != NULL)
            {
                lstNames.AddTail(pFtName);
                pEnumFeatureName->Next(&pFtName);
            }
            pEnumDatasetName->Next(&pDatasetName);
        }
    }
    else
    {
        pWorkspace->get_DatasetNames(esriDTFeatureClass, &pEnumDatasetName);
        if (pEnumDatasetName == NULL)
        {
            //AfxMessageBox("更新文件中无图层数据.", MB_ICONINFORMATION);
            return ;
        }
        IDatasetName* pFtName;
        pEnumDatasetName->Next(&pFtName);
        while (pFtName != NULL)
        {
            lstNames.AddTail(pFtName);
            pEnumDatasetName->Next(&pFtName);
        }
    }
}

/************************************************************************
简要描述 : 删除多余字段
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
HRESULT DeleteUnusedField(IFeatureClassPtr pFtCls, CString sUnusedField)
{
    if (pFtCls == NULL)  return S_FALSE;

    HRESULT hr;
    long lFlgIdx;
    IFieldPtr pDelField;
    IFieldsPtr pFields;
    hr = pFtCls->get_Fields(&pFields);
    hr = pFields->FindField(CComBSTR(sUnusedField), &lFlgIdx);
    if (lFlgIdx != -1 )
    {
        hr = pFields->get_Field(lFlgIdx, &pDelField);
        hr = pFtCls->DeleteField(pDelField);
    }

    return hr;
}

//修改要素集和要素类名称 Creator: zl
HRESULT XDBPreProcessDriver::ChangeDatasetAndFtClsName(CString sPrefix, CString sPrjName)
{

    CString sDatasetName = sPrefix;
    CString sFtClsNamePrefix;
    if (sPrjName.IsEmpty())
    {
        sFtClsNamePrefix = sPrefix + "_";
    }
    else
    {
        sFtClsNamePrefix = sPrefix + "_" + sPrjName + "_";
    }

    HRESULT hr;

    IFeatureDatasetPtr pFtDS;

    ((IFeatureWorkspacePtr)m_pOutWS)->OpenFeatureDataset(CComBSTR("DWG_X"), &pFtDS);
    if (pFtDS != NULL)
    {
        hr = pFtDS->Rename(CComBSTR(sDatasetName));
    }


    //得到图层别名
    CMapStringToString mapLayers;
    ITablePtr pFtClsLayers;
    /*   if (m_pSysFtWs == NULL)
    {
    IUnknown* pUnk = 0;
    pUnk = API_GetSysWorkspace();

    m_pSysFtWs = pUnk;
    if (m_pSysFtWs == NULL)
    {
    AfxMessageBox("无法连接到系统数据库，请检查。");
    return S_FALSE;
    }
    }*/


    //m_pSysFtWs = g_pSysFtWS;

    hr = m_pSysFtWs->OpenTable(CComBSTR("ALL_LAYERS"), &pFtClsLayers);
    long numLayers;
    hr = pFtClsLayers->RowCount(NULL, &numLayers);

    if (numLayers > 0)
    {
        long lLayerName;
        long lLayerAlias;
        pFtClsLayers->FindField(CComBSTR("LAYER_NAME"), &lLayerName);
        pFtClsLayers->FindField(CComBSTR("LAYER_ALIAS"), &lLayerAlias);

        IEsriCursorPtr pRowCursor;
        hr = pFtClsLayers->Search(NULL, VARIANT_FALSE, &pRowCursor);

        IEsriRowPtr pRowLayerName;
        CComVariant vtVal;
        CString sLayerName;
        CString sLayerAlias;

        while (pRowCursor->NextRow(&pRowLayerName) == S_OK)
        {
            pRowLayerName->get_Value(lLayerName, &vtVal);
            sLayerName = vtVal.bstrVal;

            pRowLayerName->get_Value(lLayerAlias, &vtVal);
            sLayerAlias = vtVal.bstrVal;

            mapLayers.SetAt(sLayerName, sLayerAlias);
        }
    }


    CComBSTR bsFtCls;
    CString sFtCls;

    CDsNameList lstFtClsName;
    GetFtClsNames(m_pOutWS, lstFtClsName);

    long numFtCls = lstFtClsName.GetCount();
    //pTextProgressCtrl->SetRange(0, numFtCls);
    //pTextProgressCtrl->SetPos(0);
    //pTextProgressCtrl->SetWindowText("正在进行图层名称和字段处理...");

    if ( numFtCls > 0)
    {
        POSITION pos = lstFtClsName.GetHeadPosition();
        while (pos != NULL)
        {
            IDatasetName* pFtClsName = lstFtClsName.GetNext(pos);

            hr = pFtClsName->get_Name(&bsFtCls);

            CString sLayerAlias;
            mapLayers.Lookup(CString(bsFtCls), sLayerAlias);


            sFtCls = sFtClsNamePrefix + CString(bsFtCls);

            IUnknown* pUnk;
            ((INamePtr)pFtClsName)->Open(&pUnk);
            IDatasetPtr pFtClsDs(pUnk);
            if (pFtClsDs != NULL)
            {
                hr = pFtClsDs->Rename(CComBSTR(sFtCls));
            }

            IFeatureClassPtr pFtCls(pUnk);

            //修改图层别名
            IClassSchemaEditPtr pFtClsSchemaEdit = pFtCls;
            if (pFtClsSchemaEdit != NULL)
            {
                hr = pFtClsSchemaEdit->AlterAliasName(CComBSTR(sLayerAlias));
            }

            //删除多余字段
            hr = DeleteUnusedField(pFtCls, "ENTITY");
            hr = DeleteUnusedField(pFtCls, "ENTITY_TYPE");
            hr = DeleteUnusedField(pFtCls, "DwgGeometry");
            hr = DeleteUnusedField(pFtCls, "Handle");
            hr = DeleteUnusedField(pFtCls, "BaseName");
            hr = DeleteUnusedField(pFtCls, "Layer");
            hr = DeleteUnusedField(pFtCls, "Color");
            hr = DeleteUnusedField(pFtCls, "Linetype");
            hr = DeleteUnusedField(pFtCls, "Thickness");
            hr = DeleteUnusedField(pFtCls, "Scale");
            hr = DeleteUnusedField(pFtCls, "Elevation");
            hr = DeleteUnusedField(pFtCls, "Blockname");
            hr = DeleteUnusedField(pFtCls, "Blocknumber");
            hr = DeleteUnusedField(pFtCls, "Visible");
            hr = DeleteUnusedField(pFtCls, "Angle");
            hr = DeleteUnusedField(pFtCls, "Width");
            hr = DeleteUnusedField(pFtCls, "FeatureCode");
            hr = DeleteUnusedField(pFtCls, "FeatureName");
            hr = DeleteUnusedField(pFtCls, "LayerName");

            //            pTextProgressCtrl->StepIt();
        }
    }

    return S_OK;
}

/********************************************************************
简要描述 : 校准别名
输入参数 :
返 回 值 :
修改日志 :
*********************************************************************/
HRESULT XDBPreProcessDriver::AdjustSdeLayerAlias()
{
    HRESULT hr;
    //CWaitCursor wait;

    //pTextProgressCtrl->SetRange(0, 4);
    //pTextProgressCtrl->SetWindowText("正在连接SDE数据服务器...");
    //pTextProgressCtrl->SetPos(0);

    //sde数据库
    IWorkspacePtr pSdeWS = NULL;//API_GetSdeWorkspace();
    if (pSdeWS == NULL)
    {
        MessageBox(NULL, "无法连接到SDE服务器，请检查。", "错误", MB_ICONERROR);
        return S_FALSE;
    }

    //pTextProgressCtrl->SetWindowText("正在读取图层别名...");
    //pTextProgressCtrl->SetPos(2);
    //得到图层别名
    CMapStringToString mapLayers;
    ITablePtr pFtClsLayers;
    //if (m_pSysFtWs == NULL)
    //{
    //    IUnknown* pUnk = 0;
    //    pUnk = API_GetSysWorkspace();

    //    m_pSysFtWs = pUnk;
    //    if (m_pSysFtWs == NULL)
    //    {
    //        AfxMessageBox("无法连接到系统数据库，请检查。");
    //        return S_FALSE;
    //    }
    //}
    //m_pSysFtWs = g_pSysFtWS;
    hr = m_pSysFtWs->OpenTable(CComBSTR("ALL_LAYERS"), &pFtClsLayers);
    long numLayers;
    hr = pFtClsLayers->RowCount(NULL, &numLayers);

    if (numLayers > 0)
    {
        long lLayerName;
        long lLayerAlias;
        pFtClsLayers->FindField(CComBSTR("LAYER_NAME"), &lLayerName);
        pFtClsLayers->FindField(CComBSTR("LAYER_ALIAS"), &lLayerAlias);

        IEsriCursorPtr pRowCursor;
        hr = pFtClsLayers->Search(NULL, VARIANT_FALSE, &pRowCursor);

        IEsriRowPtr pRowLayerName;
        CComVariant vtVal;
        CString sLayerName;
        CString sLayerAlias;

        while (pRowCursor->NextRow(&pRowLayerName) == S_OK)
        {
            pRowLayerName->get_Value(lLayerName, &vtVal);
            sLayerName = vtVal.bstrVal;

            pRowLayerName->get_Value(lLayerAlias, &vtVal);
            sLayerAlias = vtVal.bstrVal;

            mapLayers.SetAt(sLayerName, sLayerAlias);
        }
    }


    CComBSTR bsFtCls;
    CString sFtCls;

    //pTextProgressCtrl->SetWindowText("正在准备需要规范化的图层...");
    //pTextProgressCtrl->SetPos(3);

    CDsNameList lstFtClsName;
    GetFtClsNames(pSdeWS, lstFtClsName);

    //	pTextProgressCtrl->SetPos(4);

    long numFtCls = lstFtClsName.GetCount();
    //pTextProgressCtrl->SetRange(0, numFtCls);
    //pTextProgressCtrl->SetPos(0);
    //pTextProgressCtrl->SetWindowText("正在进行图层别名的规范化...");

    if ( numFtCls > 0)
    {
        POSITION pos = lstFtClsName.GetHeadPosition();
        while (pos != NULL)
        {
            IDatasetName* pFtClsName = lstFtClsName.GetNext(pos);
            hr = pFtClsName->get_Name(&bsFtCls);
            sFtCls = GetSdeFtClsName(bsFtCls);

            int iPos = sFtCls.Find('_');
            //if (iPos == -1) continue;

            CString sHead = sFtCls.Mid(0, iPos + 1);

            sFtCls = sFtCls.Mid(iPos + 1);//过滤前缀

            CString sLayerAlias;
            mapLayers.Lookup(sFtCls, sLayerAlias);
            if (!sLayerAlias.IsEmpty())
            {
                IUnknown* pUnk;
                ((INamePtr)pFtClsName)->Open(&pUnk);
                IFeatureClassPtr pFtCls(pUnk);
                //修改图层别名
                IClassSchemaEditPtr pFtClsSchemaEdit = pFtCls;
                if (pFtClsSchemaEdit != NULL)
                {
                    hr = pFtClsSchemaEdit->AlterAliasName(CComBSTR(sHead + sLayerAlias));
                }
            }

            //pTextProgressCtrl->StepIt();
        }
    }

    return S_OK;
}


/************************************************************************
简要描述 :	复制要素
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
void XDBPreProcessDriver::CopyFeature(IFeaturePtr pSrcFeat, IFeaturePtr& pDestFeat)
{
    if (pSrcFeat == NULL || pDestFeat == NULL) return;

    IFieldPtr pField;
    IFieldsPtr pFields;
    long lFieldCount;
    VARIANT_BOOL vbEditable;
    esriFieldType fieldType;
    CComVariant vtVal;
    CComBSTR bsFldName;
    HRESULT hr;

    IFieldsPtr pDestFields;
    hr = pDestFeat->get_Fields(&pDestFields);

    hr = pSrcFeat->get_Fields(&pFields);
    pFields->get_FieldCount(&lFieldCount);
    for (int i = 0; i < lFieldCount; i++)
    {
        pFields->get_Field(i, &pField);
        pField->get_Editable(&vbEditable);
        if (vbEditable == VARIANT_TRUE)
        {
            pField->get_Type(&fieldType);
            if (fieldType != esriFieldTypeOID && fieldType != esriFieldTypeGeometry)
            {

                hr = pField->get_Name(&bsFldName);

                long lDestFldInd = -1;
                hr = pDestFields->FindField(bsFldName, &lDestFldInd);
                if (lDestFldInd != -1)
                {
                    CString sValue = "";
                    hr = pSrcFeat->get_Value(i, &vtVal);
                    if (vtVal.vt == VT_BSTR)
                    {
                        sValue = vtVal.bstrVal;
                        if (sValue.CompareNoCase("-") == 0 || sValue.CompareNoCase("－") == 0)
                        {
                            sValue = "-1";
                        }
                        hr = pDestFeat->put_Value(lDestFldInd, CComVariant(sValue));
                    }
                    else
                    {
                        hr = pDestFeat->put_Value(lDestFldInd, vtVal);
                    }

                }
            }
        }
    }

    IGeometry* pShape;
    hr = pSrcFeat->get_ShapeCopy(&pShape);

    ITopologicalOperator2Ptr pTopo = pShape;
    if (pTopo != NULL)
    {
        pTopo->put_IsKnownSimple(VARIANT_FALSE);
        pTopo->Simplify();
    }

    hr = pDestFeat->putref_Shape(pShape);

}

/********************************************************************
简要描述 : 拷贝数据到目标库
输入参数 :
返 回 值 :
修改日志 :
*********************************************************************/
bool XDBPreProcessDriver::CopyToTargetDB(IFeatureWorkspacePtr pSrcFtWS, IFeatureWorkspacePtr pTargetFtWS)
{
    CString sLogInfo;
    sLogInfo.Format("5 拷贝数据到目标库");
    WriteLog(sLogInfo);

    if ( pSrcFtWS == NULL || pTargetFtWS == NULL)
    {
        WriteLog("目标数据库或源数据库为空，无法完成数据拷贝。");
        return false;
    }

    HRESULT hr;

    IFeatureDatasetPtr pFtDataset;
    pSrcFtWS->OpenFeatureDataset(CComBSTR("DWG_X"), &pFtDataset);

    if (pFtDataset == NULL) return false;

    IEnumDatasetPtr pEnumDS;
    pFtDataset->get_Subsets(&pEnumDS);
    if (pEnumDS == NULL) return false;

    pEnumDS->Reset();
    IDatasetPtr ipDataset = NULL;
    while (pEnumDS->Next(&ipDataset) == S_OK)
    {
        CComBSTR bsLayerName;
        ipDataset->get_Name(&bsLayerName);
        CString sLayerName = GetSdeFtClsName(bsLayerName);

        IFeatureClassPtr pInFtCls;
        IFeatureClassPtr pTarFtCls;
        pSrcFtWS->OpenFeatureClass(CComBSTR(sLayerName), &pInFtCls);
        pTargetFtWS->OpenFeatureClass(CComBSTR(sLayerName), &pTarFtCls);

        if (pTarFtCls == NULL)
        {
            sLogInfo.Format("目标数据库中不存在%s图层，无法完成数据拷贝。", sLayerName);
            WriteLog(sLogInfo);
            continue;
        }

        long numFeats;
        pInFtCls->FeatureCount(NULL, &numFeats);
        if (numFeats <= 0) continue;

        CString sProgressText;

        sProgressText.Format("正在提交%s图层的数据", sLayerName);

        PrgbarRange(0, numFeats);
        PrgbarSetPos(0);
        PrgbarSetText(sProgressText);

        IFeatureCursorPtr pCur;
        pInFtCls->Search(NULL, VARIANT_FALSE, &pCur);

        IFeaturePtr pTargetFeat;

        IFeaturePtr pFeat;
        pCur->NextFeature(&pFeat);
        while (pFeat != NULL)
        {
            pTarFtCls->CreateFeature(&pTargetFeat);

            CopyFeature(pFeat, pTargetFeat);

            hr = pTargetFeat->Store();

            pTargetFeat.Release();

            pFeat.Release();
            pCur->NextFeature(&pFeat);

            PrgbarStepIt();
        }

    }

    PrgbarSetPos(0);
    PrgbarSetText("");

    sLogInfo.Format("5 拷贝数据到目标库完成!");
    WriteLog(sLogInfo);

    return false;
}

/********************************************************************
简要描述 : 进度条范围
输入参数 :
返 回 值 :
修改日志 :
*********************************************************************/
void XDBPreProcessDriver::PrgbarRange(int iLower, int iUpper)
{
    if (pTextProgressCtrl != NULL)
    {
        pTextProgressCtrl->SetRange(iLower, iUpper);
    }
}

void XDBPreProcessDriver::PrgbarSetPos(int iPos)
{
    if (pTextProgressCtrl != NULL)
    {
        pTextProgressCtrl->SetPos(iPos);
    }
}

void XDBPreProcessDriver::PrgbarSetText(CString sText)
{
    if (pTextProgressCtrl != NULL)
    {
        pTextProgressCtrl->SetWindowText(sText);
        pTextProgressCtrl->DoEvents();
    }
}

void XDBPreProcessDriver::PrgbarStepIt(void)
{
    if (pTextProgressCtrl != NULL)
    {
        pTextProgressCtrl->StepIt();
    }
}

void XDBPreProcessDriver::SaveLogFile(CString sFilePath)
{
    try
    {
        if (m_LogList.GetCount() > 0)
        {
            //COleDateTime dtCur = COleDateTime::GetCurrentTime();
            //CString sName = dtCur.Format("%y%m%d_%H%M%S");
            CString sLogFileName = sFilePath;
            //sLogFileName.Format("%sDwg转换日志_%s.log", GetLogPath(), sName);

            CStdioFile f3(sLogFileName, CFile::modeCreate | CFile::modeWrite | CFile::typeText);
            for (POSITION pos = m_LogList.GetHeadPosition(); pos != NULL;)
            {
                f3.WriteString(m_LogList.GetNext(pos) + "\n");
            }

            f3.Close();

            //WinExec("Notepad.exe " + sLogFileName, SW_SHOW);
            //m_LogList.RemoveAll();
        }
    }
    catch (...)
    {
        CString sErr;
        sErr.Format("写日志到%s错误，请检查文件路径是否正确。", sFilePath);
        AfxMessageBox(sErr);
    }
}
