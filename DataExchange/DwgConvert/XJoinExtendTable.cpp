// XJoinExtendTable.cpp: implementation of the XJointExtendTable class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "XJoinExtendTable.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

XJoinExtendTable::XJoinExtendTable()
{
    m_ipExtendTable = NULL;
    m_ipConfigTable = NULL;
    m_ipTargetTable = NULL;

	m_pProgressCtrl = NULL;
}

XJoinExtendTable::~XJoinExtendTable()
{
}
//DEL void XJoinExtendTable::GenerateFieldsMap()
//DEL {
//DEL 	m_sMapOldNameToNewName.RemoveAll();
//DEL 	m_sMapNameToAlias.RemoveAll();
//DEL
//DEL 	long lXOldNameIndex, lXNewNameIndex, lXAliasNameIndex;
//DEL
//DEL 	m_ipConfigTable->FindField(CComBSTR("XOldName"), &lXOldNameIndex);
//DEL 	m_ipConfigTable->FindField(CComBSTR("XNewName"), &lXNewNameIndex);
//DEL 	m_ipConfigTable->FindField(CComBSTR("XAliasName"), &lXAliasNameIndex);
//DEL
//DEL 	if (lXOldNameIndex != -1 && lXNewNameIndex != -1 && lXAliasNameIndex != -1)
//DEL 	{
//DEL 		ICursorPtr ipCursor;
//DEL 		IEsriRowPtr ipRow;
//DEL 		m_ipConfigTable->Search(NULL, VARIANT_TRUE, &ipCursor);
//DEL 		ipCursor->NextRow(&ipRow);
//DEL 		CComVariant vtOld, vtNew, vtAlias;
//DEL
//DEL 		while (ipRow != NULL)
//DEL 		{
//DEL 			ipRow->get_Value(lXOldNameIndex, &vtOld);
//DEL 			ipRow->get_Value(lXNewNameIndex, &vtNew);
//DEL 			ipRow->get_Value(lXAliasNameIndex, &vtAlias);
//DEL
//DEL 			m_sMapOldNameToNewName[CString(vtOld.bstrVal)] = CString(vtNew.bstrVal);
//DEL 			m_sMapNameToAlias[CString(vtNew.bstrVal)] = CString(vtAlias.bstrVal);
//DEL
//DEL 			ipCursor->NextRow(&ipRow);
//DEL 		}
//DEL 	}
//DEL }
//DEL BOOL XJoinExtendTable::Run()
//DEL {
//DEL 	if (m_ipExtendTable != NULL && m_ipTargetTable != NULL && m_ipConfigTable != NULL)
//DEL 	{
//DEL 		GenerateFieldsMap();
//DEL 		HRESULT hr;
//DEL 		//遍历要连接的要素类
//DEL 		ICursorPtr ipCursor;
//DEL 		ICursorPtr ipExtendCursor;
//DEL 		ICursorPtr ipCompareCursor;
//DEL
//DEL 		//获取连接表中Handle字段的索引
//DEL 		long lHandleIndex, lBaseNameIndex;
//DEL 		m_ipTargetTable->FindField(CComBSTR("BaseName"), &lBaseNameIndex);
//DEL 		m_ipTargetTable->FindField(CComBSTR("Handle"), &lHandleIndex);
//DEL
//DEL
//DEL 		//获取扩展表中XDataName,XDataValue的索引
//DEL 		long lXDataNameIndex, lXDataValueIndex;
//DEL 		m_ipExtendTable->FindField(CComBSTR("XDataName"), &lXDataNameIndex);
//DEL 		m_ipExtendTable->FindField(CComBSTR("XDataValue"), &lXDataValueIndex);
//DEL
//DEL 		//获取对照表中XOldName,XNewName,XAliasName的索引
//DEL
//DEL 		//XOldName对应XDataName
//DEL
//DEL 		long lXNewNameIndex, lXAliasNameIndex;
//DEL 		m_ipConfigTable->FindField(CComBSTR("XNewName"), &lXNewNameIndex);
//DEL 		m_ipConfigTable->FindField(CComBSTR("XAliasName"), &lXAliasNameIndex);
//DEL
//DEL 		IEsriRowPtr ipRow;
//DEL 		IEsriRowPtr ipExtendRow;
//DEL 		IEsriRowPtr ipCompareRow;
//DEL
//DEL 		CComVariant vt;
//DEL 		CComVariant vtXDataName, vtXDataValue;
//DEL 		CComVariant vtXAliasName, vtXNewName;
//DEL 		CString sHandle, sBaseName;
//DEL 		CString sTemp = "0";
//DEL 		IQueryFilterPtr ipExtendQueryFilter(CLSID_QueryFilter);
//DEL 		IQueryFilterPtr ipCompareFilter(CLSID_QueryFilter);
//DEL 		CString sWhereClause;
//DEL 		CString sCompareWhereClause;
//DEL
//DEL 		long lRowCount;
//DEL 		m_ProgressCtrl->SetPos(0);
//DEL 		m_ipTargetTable->RowCount(NULL, &lRowCount);
//DEL 		m_ProgressCtrl->SetRange(0, lRowCount + 1);
//DEL
//DEL 		CString sInfo;
//DEL 		sInfo.Format("正在对连接表增加新字段...");
//DEL
//DEL 		m_ProgressCtrl->ShowWindow(SW_SHOW);
//DEL 		WriteLog(sInfo);
//DEL 		m_ProgressCtrl->SetWindowText(sInfo);
//DEL 		m_ProgressCtrl->StepIt();
//DEL 		//增加字段
//DEL 		//在连接表中按照m_sMapNameToAlias的记录增加相应的字段
//DEL 		POSITION pos = m_sMapNameToAlias.GetStartPosition();
//DEL 		CString sName, sAlias;
//DEL 		IFieldsPtr ipFields;
//DEL 		m_ipTargetTable->get_Fields(&ipFields);
//DEL 		long lIndex;
//DEL
//DEL 		while (pos != NULL)
//DEL 		{
//DEL 			m_sMapNameToAlias.GetNextAssoc(pos, sName, sAlias);
//DEL 			ipFields->FindField(CComBSTR(sName), &lIndex);
//DEL 			if (lIndex == -1)
//DEL 			{
//DEL 				sInfo.Format("成功增加字段:%s", sName);
//DEL 				WriteLog(sInfo);
//DEL 				//可以新建字段
//DEL 				IFieldPtr ipField(CLSID_Field);
//DEL 				IFieldEditPtr ipFieldEdit = ipField;
//DEL 				ipFieldEdit->put_AliasName(CComBSTR(sAlias));
//DEL 				ipFieldEdit->put_Name(CComBSTR(sName));
//DEL
//DEL 				/******************************************
//DEL 								 修改原因 :  把挂接的层数、层高和面积字段类型设置为整形和double型
//DEL 								   * *****************************************/
//DEL 				if (sAlias == "层数")
//DEL 					ipFieldEdit->put_Type(esriFieldTypeInteger);
//DEL 				else if (sAlias == "面积" || sAlias == "层高")
//DEL 					ipFieldEdit->put_Type(esriFieldTypeDouble);
//DEL 				else
//DEL 					ipFieldEdit->put_Type(esriFieldTypeString);
//DEL
//DEL 				hr = m_ipTargetTable->AddField(ipField);
//DEL 				if (FAILED(hr))
//DEL 				{
//DEL 					XConvertHelper::CatchErrorInfo();
//DEL 				}
//DEL 			}
//DEL 		}
//DEL
//DEL 		m_ipTargetTable->Update(NULL, VARIANT_FALSE, &ipCursor);
//DEL
//DEL 		ipCursor->NextRow(&ipRow);
//DEL
//DEL 		m_ipTargetTable->get_Fields(&ipFields);
//DEL
//DEL 		long lCount = 0;
//DEL
//DEL 		CString sProgressText;
//DEL 		long lValueIndex;
//DEL
//DEL 		long lUpdateCount = 0;
//DEL
//DEL 		while (ipRow != NULL)
//DEL 		{
//DEL 			lCount++;
//DEL 			sProgressText.Format("处理连接表第%d个要素与CAD扩展属性之间的关系...", lCount);
//DEL 			m_ProgressCtrl->SetWindowText(sProgressText);
//DEL 			m_ProgressCtrl->StepIt();
//DEL
//DEL 			//获取Handle字段的值
//DEL 			ipRow->get_Value(lHandleIndex, &vt);
//DEL 			sHandle = vt.bstrVal;
//DEL 			ipRow->get_Value(lBaseNameIndex, &vt);
//DEL 			sBaseName = vt.bstrVal;
//DEL
//DEL 			//去ExtendTable寻找这个Handle对应的字段
//DEL 			sWhereClause.Format("Handle = '%s' and BaseName ='%s'", sHandle, sBaseName);
//DEL 			ipExtendQueryFilter->put_WhereClause(CComBSTR(sWhereClause));
//DEL 			m_ipExtendTable->Search(ipExtendQueryFilter, TRUE, &ipExtendCursor);
//DEL 			ipExtendCursor->NextRow(&ipExtendRow);
//DEL
//DEL 			CString sXDataName;
//DEL 			while (ipExtendRow != NULL)
//DEL 			{
//DEL 				ipExtendRow->get_Value(lXDataNameIndex, &vtXDataName);
//DEL 				sXDataName = vtXDataName.bstrVal;
//DEL
//DEL 				if (m_sMapOldNameToNewName.Lookup(sXDataName, sTemp))
//DEL 				{
//DEL 					ipFields->FindField(CComBSTR(sTemp), &lValueIndex);
//DEL 					if (lValueIndex != -1)
//DEL 					{
//DEL 						ipExtendRow->get_Value(lXDataValueIndex, &vtXDataValue);
//DEL 						ipRow->put_Value(lValueIndex, vtXDataValue);
//DEL 					}
//DEL 				}
//DEL
//DEL 				ipExtendCursor->NextRow(&ipExtendRow);
//DEL 			}
//DEL 			ipCursor->UpdateRow(ipRow);
//DEL 			ipCursor->NextRow(&ipRow);
//DEL 		}
//DEL
//DEL 		sInfo.Format("成功更新连接表");
//DEL 		WriteLog(sInfo);
//DEL
//DEL 		ipCursor.Release();
//DEL 		m_ProgressCtrl->SetPos(0);
//DEL 		m_ProgressCtrl->ShowWindow(SW_HIDE);
//DEL 		return TRUE;
//DEL 	}
//DEL 	return FALSE;
//DEL }


/************************************************************************
简要描述 :
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
void XJoinExtendTable::WriteLog(CString sLog)
{
    if (!sLog.IsEmpty())
    {
        COleDateTime dtCur = COleDateTime::GetCurrentTime();
        CString sLogTime = dtCur.Format("%Y/%m/%d %H:%M:%S");
        sLog = sLogTime + "-" + sLog;
        m_pLogList->AddTail(sLog);
    }
}

/************************************************************************
简要描述 : 增加扩展字段,并赋值
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
void XJoinExtendTable::AddExtendFieldsValue(CString sLayerName)
{
    if (m_ipExtendTable == NULL || m_ipTargetTable == NULL || m_ipConfigTable == NULL) return;

    HRESULT hr;
    long lFieldIndex;
    CString sInfo;
    CString sProgressText;
    CComVariant vtFieldVal;
    CStringList lstNewFieldsName;

    //////////////////////////////////////////////////////////////////////////
    // 1 增加扩展字段
    IFieldsPtr ipTargetTableFields;
    m_ipTargetTable->get_Fields(&ipTargetTableFields);

    //根据顺序号读取,根据字段配置表增加字段 无法使用ITableSortPtr,因为记录集不是ArcObject
    long lNumRows;
    m_ipConfigTable->RowCount(NULL, &lNumRows);
    for (int i = 1; i <= lNumRows; i++)
    {
        IQueryFilterPtr pConfigFilter(CLSID_QueryFilter);
        CString sWhereClause;
        sWhereClause.Format("FIELD_INDEX=%d", i);
        pConfigFilter->put_WhereClause(CComBSTR(sWhereClause));

        IEsriCursorPtr pConfigCursor;
        m_ipConfigTable->Search(pConfigFilter, VARIANT_FALSE, &pConfigCursor);
        if (pConfigCursor == NULL)
        {
            sInfo.Format("%s图层配置表数据错误!请检查.", sLayerName);
            WriteLog(sInfo);
            return;
        }

        IEsriRowPtr pConfigRow;
        pConfigCursor->NextRow(&pConfigRow);
        if (pConfigRow == NULL)
        {
            sInfo.Format("%s图层配置表数据错误!请检查.", sLayerName);
            WriteLog(sInfo);
            return;
        }

        CString sNewFieldName, sNewFieldAlias;

        IFieldsPtr pFields;
        pConfigRow->get_Fields(&pFields);

        //新增字段名称
        pFields->FindField(CComBSTR("FIELD_NAME"), &lFieldIndex);
        pConfigRow->get_Value(lFieldIndex, &vtFieldVal);
        if (vtFieldVal.vt != VT_NULL && vtFieldVal.vt != VT_EMPTY)
        {
            sNewFieldName = vtFieldVal.bstrVal;
        }

        //新增字段别名
        pFields->FindField(CComBSTR("FIELD_ALIAS"), &lFieldIndex);
        pConfigRow->get_Value(lFieldIndex, &vtFieldVal);
        if (vtFieldVal.vt != VT_NULL && vtFieldVal.vt != VT_EMPTY)
        {
            sNewFieldAlias = vtFieldVal.bstrVal;
        }

        long lTargetFieldIndex;
        ipTargetTableFields->FindField(CComBSTR(sNewFieldName), &lTargetFieldIndex);
        if (lTargetFieldIndex == -1)
        {
            //Add new Field
            //新增字段
            IFieldPtr ipField(CLSID_Field);
            IFieldEditPtr ipFieldEdit = ipField;
            ipFieldEdit->put_Name(CComBSTR(sNewFieldName));
            ipFieldEdit->put_AliasName(CComBSTR(sNewFieldAlias));
            ipFieldEdit->put_Type(esriFieldTypeString);
            ipFieldEdit->put_Length(1024);

            hr = m_ipTargetTable->AddField(ipField);
            if (SUCCEEDED(hr))
            {
                sInfo.Format("成功增加字段:%s", sNewFieldAlias);
                WriteLog(sInfo);
            }

            lstNewFieldsName.AddTail(sNewFieldName);
        }
    }

    //////////////////////////////////////////////////////////////////////////
    // 2 给扩展字段赋值
    long lRowCount;
    if (m_pProgressCtrl != NULL)
    {
        m_pProgressCtrl->SetPos(0);
        m_ipTargetTable->RowCount(NULL, &lRowCount);
        m_pProgressCtrl->SetRange(0, lRowCount + 1);
    }


    IEsriCursorPtr pTargetCursor;
    IEsriCursorPtr ipExtendCursor;
    IEsriCursorPtr ipCompareCursor;

    IEsriRowPtr pTargetRow;
    IEsriRowPtr ipExtendRow;
    IEsriRowPtr ipCompareRow;


    long lTargetHandleFieldIndex, lTargetBaseNameFieldIndex;
    m_ipTargetTable->FindField(CComBSTR("Handle"), &lTargetHandleFieldIndex);
    m_ipTargetTable->FindField(CComBSTR("BaseName"), &lTargetBaseNameFieldIndex);
    if (lTargetHandleFieldIndex == -1 || lTargetHandleFieldIndex == -1)
    {
        sInfo.Format("%s图层字段错误!", sLayerName);
        WriteLog(sInfo);
        return;
    }

    long lExtendTableDataNameIndex, lExtendTableDataValueIndex;
    m_ipExtendTable->FindField(CComBSTR("XDataName"), &lExtendTableDataNameIndex);
    m_ipExtendTable->FindField(CComBSTR("XDataValue"), &lExtendTableDataValueIndex);
    if (lExtendTableDataNameIndex == -1 || lExtendTableDataValueIndex == -1)
    {
        sInfo.Format("扩展表图层字段错误!");
        WriteLog(sInfo);
        return;
    }

    lRowCount = 0;
    m_ipTargetTable->Update(NULL, VARIANT_FALSE, &pTargetCursor);
    pTargetCursor->NextRow(&pTargetRow);
    while (pTargetRow != NULL)
    {
        CStringList lstNewFieldValues;
        CString sTargetHandle, sTargetBaseName;

        //目标层Handle
        pTargetRow->get_Value(lTargetHandleFieldIndex, &vtFieldVal);
        if (vtFieldVal.vt != VT_NULL && vtFieldVal.vt != VT_EMPTY)
        {
            sTargetHandle = vtFieldVal.bstrVal;
        }

        //目标层BaseName
        pTargetRow->get_Value(lTargetBaseNameFieldIndex, &vtFieldVal);
        if (vtFieldVal.vt != VT_NULL && vtFieldVal.vt != VT_EMPTY)
        {
            sTargetBaseName = vtFieldVal.bstrVal;
        }

        CString sExtendName, sExtendValues;
        IQueryFilterPtr pExtendQueryFilter(CLSID_QueryFilter);
        CString sWhereClause;

        CString sRegAppName = "";

        /*if (m_mapRegAppName != NULL) //不判断注册应用名
        {
            m_mapRegAppName->Lookup(sLayerName, sRegAppName); //扩展数据注册名称
        }*/

        if (!sRegAppName.IsEmpty())
        {
            sWhereClause.Format("Handle='%s' and BaseName='%s' and XDataName='%s'", sTargetHandle, sTargetBaseName, sRegAppName);
        }
        else
        {
            sWhereClause.Format("Handle = '%s' and BaseName ='%s'", sTargetHandle, sTargetBaseName);
        }
        pExtendQueryFilter->put_WhereClause(CComBSTR(sWhereClause));
        m_ipExtendTable->Search(pExtendQueryFilter, FALSE, &ipExtendCursor);
        if (ipExtendCursor != NULL)
        {
            while (ipExtendCursor->NextRow(&ipExtendRow) == S_OK)
            {
                if (ipExtendRow != NULL)
                {

                    //判断注册应用名
                    {
                        CString sAppName;
                        //注册应用名
                        ipExtendRow->get_Value(lExtendTableDataNameIndex, &vtFieldVal);
                        if (vtFieldVal.vt != VT_NULL && vtFieldVal.vt != VT_EMPTY)
                        {
                            sAppName = vtFieldVal.bstrVal;
                        }
                        POSITION posRegApp = m_lstRegApps->Find(sAppName);
                        if (posRegApp == NULL) continue;
                    }

                    //扩展数据
                    ipExtendRow->get_Value(lExtendTableDataValueIndex, &vtFieldVal);
                    if (vtFieldVal.vt != VT_NULL && vtFieldVal.vt != VT_EMPTY)
                    {
                        sExtendValues = vtFieldVal.bstrVal;
                    }

                    int iBeginTokenPos = 0, iEndTokenPos = 0;
                    if (!sExtendValues.IsEmpty())
                    {
                        CString sVal;
                        int iBeginTokenPos = sExtendValues.Find("[");
                        int iEndTokenPos = sExtendValues.Find("]");

                        while (iEndTokenPos != -1)
                        {
                            sVal = sExtendValues.Mid(iBeginTokenPos + 1,
                                                     iEndTokenPos - (iBeginTokenPos + 1));
                            sExtendValues = sExtendValues.Mid(iEndTokenPos + 1);
                            lstNewFieldValues.AddTail(sVal);

                            iBeginTokenPos = sExtendValues.Find("[");
                            iEndTokenPos = sExtendValues.Find("]");
                        }
                    }
                }
            }
        }

        //判断值是否与新加字段个数相同
        if (lstNewFieldValues.GetCount() >= lstNewFieldsName.GetCount())
        {
            //赋值给新增字段
            POSITION posFields = lstNewFieldsName.GetHeadPosition();
            POSITION posValues = lstNewFieldValues.GetHeadPosition();

            CString sFieldName, sFieldValue;
            while (posFields != NULL)
            {
                sFieldName = lstNewFieldsName.GetNext(posFields);
                sFieldValue = lstNewFieldValues.GetNext(posValues);

                m_ipTargetTable->FindField(CComBSTR(sFieldName), &lFieldIndex);
                pTargetRow->put_Value(lFieldIndex, CComVariant(sFieldValue));
            }
        }

        if (m_pProgressCtrl != NULL)
        {
            sProgressText.Format("正在处理%s层的第%d个要素与扩展属性的挂接...", sLayerName, lRowCount++);
            m_pProgressCtrl->SetWindowText(sProgressText);
            m_pProgressCtrl->StepIt();
        }

        pTargetCursor->UpdateRow(pTargetRow);
        //下一行
        pTargetCursor->NextRow(&pTargetRow);
    }

    pTargetCursor.Release();

	if (m_pProgressCtrl != NULL)
	{
		m_pProgressCtrl->SetWindowText("");
		m_pProgressCtrl->SetPos(0);
	}

}
