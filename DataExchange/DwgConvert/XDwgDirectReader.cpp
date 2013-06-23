// XDwgDirectReader.cpp: implementation of the XDwgDirectReader class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include "atlbase.h"
#include "XDwgDirectReader.h"
#include "db.h"
#include "DwgEntityDumper.h"
#include "ExSystemServices.h"
#include "ExHostAppServices.h"
#include "RxDynamicModule.h"

//////////////////////////////////////////////////////////////////////////


/////////////DwgReaderServices//////////////////////////////////////////////
class DwgReaderServices : public ExSystemServices, public ExHostAppServices
{
protected:
    ODRX_USING_HEAP_OPERATORS(ExSystemServices);
};

OdRxObjectImpl<DwgReaderServices> svcs;
ExProtocolExtension theProtocolExtensions;

const CString g_szEntityType = "ENTITY_TYPE";

//gis数据格网大小
const double DEFAULT_GIS_GRID_SIZE = 120.0;

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

XDWGReader::XDWGReader()
{

    //初始化DwgDirect库
    odInitialize(&svcs);
    theProtocolExtensions.initialize();

    //默认读取CAD参数设置
    m_IsReadPolygon = FALSE;
    m_IsLine2Polygon = FALSE;
    m_IsBreakBlock = FALSE;
    m_IsReadInvisible = FALSE;
    m_IsJoinXDataAttrs = FALSE;
    m_IsReadBlockPoint = TRUE;
    m_IsCreateAnnotation = TRUE;

	m_iUnbreakBlockMode = 0;

    m_pSpRef = NULL;
    m_dAnnoScale = 1;
    m_bConvertAngle = TRUE;
    m_pProgressBar = NULL;
    m_pLogRec = NULL;

    InitAOPointers();

    m_Regapps.RemoveAll();
    m_unExplodeBlocks.RemoveAll();

    m_bFinishedCreateFtCls = FALSE;

    m_StepNum = 5000;

}

XDWGReader::~XDWGReader()
{
    m_unExplodeBlocks.RemoveAll();
    theProtocolExtensions.uninitialize();
    odUninitialize();

    if (m_pLogRec != NULL)
    {
        delete m_pLogRec;
    }

}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 删除已存在的要素类
//输入参数 :
//返 回 值 :
//修改日志 :
//////////////////////////////////////////////////////////////////////////
void XDWGReader::CheckDeleteFtCls(IFeatureWorkspace* pFtWS, CString sFtClsName)
{
    if (pFtWS == NULL) return;
    IFeatureClass* pFtCls = NULL;
    pFtWS->OpenFeatureClass(CComBSTR(sFtClsName), &pFtCls);
    if (pFtCls != NULL)
    {
        IDatasetPtr pDs = pFtCls;
        if (pDs != NULL)
        {
            pDs->Delete();
        }
    }
}

/********************************************************************
简要描述 : 批量读取前的准备工作
输入参数 :
返 回 值 :
修改日志 :
*********************************************************************/
BOOL XDWGReader::PrepareReadDwg(IWorkspace* pTargetWS, IDataset* pTargetDataset, ISpatialReference* pSpRef)
{
    try
    {
        m_pTargetWS = pTargetWS;

        //初始化指针
        //InitAOPointers();

        //无法读取的实体个数
        m_lUnReadEntityNum = 0;

        //////////////////////////////////////////////////////////////////////////
        IFeatureDatasetPtr pFeatDataset(pTargetDataset);

        if (pSpRef == NULL)
        {
            ISpatialReferencePtr pUnknownSpRef(CLSID_UnknownCoordinateSystem);
            m_pSpRef = pUnknownSpRef.Detach();
            m_pSpRef->SetDomain(0.0, 1000000000, 0.0, 1000000000);
        }
        else
        {
            m_pSpRef = pSpRef;
        }

        //////////////////////////////////////////////////////////////////////////
        //设置为高精度，否则无法创建表或FEATURECLASS
        IControlPrecision2Ptr pControlPrecision(m_pSpRef);
        if (pControlPrecision != NULL)
        {
            pControlPrecision->put_IsHighPrecision(VARIANT_TRUE);
        }

		//设置空间参考精度值
		ISpatialReferenceResolutionPtr spatialReferenceResolution = m_pSpRef;
		spatialReferenceResolution->SetDefaultMResolution();
		spatialReferenceResolution->SetDefaultZResolution();
		spatialReferenceResolution->SetDefaultXYResolution();

		//设置空间坐标误差值
		ISpatialReferenceTolerancePtr spatialReferenceTolerance = m_pSpRef;
		spatialReferenceTolerance->SetDefaultMTolerance();
		spatialReferenceTolerance->SetDefaultZTolerance();
		spatialReferenceTolerance->SetDefaultXYTolerance();

        m_bFinishedCreateFtCls = FALSE;

        return TRUE;
    }
    catch (...)
    {
        WriteLog("初始化异常,请检查工作空间和空间参考是否正确.");
        return FALSE;
    }
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 创建目标要素类
//输入参数 :
//返 回 值 :
//修改日志 :
//////////////////////////////////////////////////////////////////////////
BOOL XDWGReader::CreateTargetAllFeatureClass()
{
    try
    {
        IFeatureWorkspacePtr pFtWS(m_pTargetWS);
        if (pFtWS == NULL)
            return FALSE;

        HRESULT hr;
        CString sInfoText;

        //创建系统表结构
        IFieldsPtr ipFieldsPoint = 0;
        IFieldsPtr ipFieldsLine = 0;
        IFieldsPtr ipFieldsPolygon = 0;
        IFieldsPtr ipFieldsText = 0;
        IFieldsPtr ipFieldsAnnotation = 0;

        //生成普通点类字段
        CreateDwgPointFields(m_pSpRef, &ipFieldsPoint);

        //生成注记点类字段
        CreateDwgTextPointFields(m_pSpRef, &ipFieldsText);

        //生成线要素类字段
        CreateDwgLineFields(m_pSpRef, &ipFieldsLine);

        //生成面要素类字段
        CreateDwgPolygonFields(m_pSpRef, &ipFieldsPolygon);

        //生成注记图层字段
        CreateDwgAnnotationFields(m_pSpRef, &ipFieldsAnnotation);


        //////////////////////////////////////////////////////////////////////////
        //增加扩展属性字段
        if (m_IsJoinXDataAttrs && m_Regapps.GetCount() > 0)
        {
            IFieldsEditPtr ipEditFieldsPoint = ipFieldsPoint;
            IFieldsEditPtr ipEditFieldsLine = ipFieldsLine;
            IFieldsEditPtr ipEditFieldsPolygon = ipFieldsPolygon;
            IFieldsEditPtr ipEditFieldsText = ipFieldsText;
            IFieldsEditPtr ipEditFieldsAnnotation = ipFieldsAnnotation;

            CString sRegappName;

            for (int i = 0; i < m_Regapps.GetCount(); i++)
            {
                //创建扩展属性字段
                IFieldPtr ipField(CLSID_Field);
                IFieldEditPtr ipFieldEdit = ipField;

                sRegappName = m_Regapps.GetAt(m_Regapps.FindIndex(i));
                CComBSTR bsStr = sRegappName;

                ipFieldEdit->put_Name(bsStr);
                ipFieldEdit->put_AliasName(bsStr);
                ipFieldEdit->put_Type(esriFieldTypeString);
                ipFieldEdit->put_Length(2000);

                long lFldIndex = 0;

                ipEditFieldsPoint->FindField(bsStr, &lFldIndex);
                if (lFldIndex == -1)
                {
                    ipEditFieldsPoint->AddField(ipField);
                }

                ipEditFieldsLine->FindField(bsStr, &lFldIndex);
                if (lFldIndex == -1)
                {
                    ipEditFieldsLine->AddField(ipField);
                }

                ipEditFieldsPolygon->FindField(bsStr, &lFldIndex);
                if (lFldIndex == -1)
                {
                    ipEditFieldsPolygon->AddField(ipField);
                }

                ipEditFieldsText->FindField(bsStr, &lFldIndex);
                if (lFldIndex == -1)
                {
                    ipEditFieldsText->AddField(ipField);
                }

                ipEditFieldsAnnotation->FindField(bsStr, &lFldIndex);
                if (lFldIndex == -1)
                {
                    ipEditFieldsAnnotation->AddField(ipField);
                }
            }
        }

        //如果有图层先删除
        CheckDeleteFtCls(pFtWS, "Point");
        CheckDeleteFtCls(pFtWS, "TextPoint");
        CheckDeleteFtCls(pFtWS, "Line");
        CheckDeleteFtCls(pFtWS, "Polygon");
        CheckDeleteFtCls(pFtWS, "Annotation");
        CheckDeleteFtCls(pFtWS, "ExtendTable");


        //创建点图层
        hr = CreateDatasetFeatureClass(pFtWS, NULL, ipFieldsPoint, CComBSTR("Point"), esriFTSimple, m_pFeatClassPoint);
        if (m_pFeatClassPoint != NULL)
        {
            hr = m_pFeatClassPoint->Insert(VARIANT_TRUE, &m_pPointFeatureCursor);
            if (FAILED(hr))
            {
                sInfoText.Format("创建点FeatureCursor失败:%s", CatchErrorInfo());
                WriteLog(sInfoText);
            }
            hr = m_pFeatClassPoint->CreateFeatureBuffer(&m_pPointFeatureBuffer);
            if (FAILED(hr))
            {
                sInfoText.Format("创建点FeautureBuffer失败:%s", CatchErrorInfo());
                WriteLog(sInfoText);
            }
        }
        else
        {
            sInfoText.Format("创建Point要素类失败:%s", CatchErrorInfo());
            WriteLog(sInfoText);
            return FALSE;
        }

        //创建线要素类
        hr = CreateDatasetFeatureClass(pFtWS, NULL, ipFieldsLine, CComBSTR("Line"), esriFTSimple, m_pFeatClassLine);
        if (m_pFeatClassLine != NULL)
        {
            hr = m_pFeatClassLine->Insert(VARIANT_TRUE, &m_pLineFeatureCursor);
            if (FAILED(hr))
            {
                sInfoText.Format("创建线FeatureCursor失败:%s", CatchErrorInfo());
                WriteLog(sInfoText);
            }
            hr = m_pFeatClassLine->CreateFeatureBuffer(&m_pLineFeatureBuffer);
            if (FAILED(hr))
            {
                sInfoText.Format("创建线FeautureBuffer失败:%s", CatchErrorInfo());
                WriteLog(sInfoText);
            }
        }
        else
        {
            sInfoText.Format("创建Line要素类失败:%s", CatchErrorInfo());
            WriteLog(sInfoText);
            return FALSE;
        }

        if (m_IsReadPolygon || m_IsLine2Polygon)
        {
            //创建面要素类
            hr = CreateDatasetFeatureClass(pFtWS, NULL, ipFieldsPolygon, CComBSTR("Polygon"), esriFTSimple, m_pFeatClassPolygon);
            if (m_pFeatClassPolygon != NULL)
            {
                hr = m_pFeatClassPolygon->Insert(VARIANT_TRUE, &m_pPolygonFeatureCursor);
                if (FAILED(hr))
                {
                    sInfoText.Format("创建面FeatureCursor失败:%s", CatchErrorInfo());
                    WriteLog(sInfoText);
                }
                hr = m_pFeatClassPolygon->CreateFeatureBuffer(&m_pPolygonFeatureBuffer);
                if (FAILED(hr))
                {
                    sInfoText.Format("创建面FeautureBuffer失败:%s", CatchErrorInfo());
                    WriteLog(sInfoText);
                }
            }
            else
            {
                sInfoText.Format("创建Polygon要素类失败:%s", CatchErrorInfo());
                WriteLog(sInfoText);
                return FALSE;
            }
        }

        //arcgis 注记图层
        if (m_IsCreateAnnotation)
        {
            m_pAnnoFtCls = CreateAnnoFtCls(m_pTargetWS, "Annotation", ipFieldsAnnotation);
            if (m_pAnnoFtCls != NULL)
            {
                //设置现实比例尺
                IUnknownPtr pUnk;
                m_pAnnoFtCls->get_Extension(&pUnk);
                IAnnoClassAdminPtr pAnnoClassAdmin = pUnk;
                if (pAnnoClassAdmin != NULL)
                {
                    hr = pAnnoClassAdmin->put_ReferenceScale(m_dAnnoScale);
                    hr = pAnnoClassAdmin->UpdateProperties();
                }

                hr = m_pAnnoFtCls->Insert(VARIANT_TRUE, &m_pAnnoFeatureCursor);
                if (FAILED(hr))
                {
                    sInfoText.Format("创建注记FeatureCursor失败:%s", CatchErrorInfo());
                    WriteLog(sInfoText);
                }
                hr = m_pAnnoFtCls->CreateFeatureBuffer(&m_pAnnoFeatureBuffer);
                if (FAILED(hr))
                {
                    sInfoText.Format("创建注记FeautureBuffer失败:%s", CatchErrorInfo());
                    WriteLog(sInfoText);
                }
            }
            else
            {
                sInfoText.Format("创建Annotation要素类失败:%s", CatchErrorInfo());
                WriteLog(sInfoText);
                return FALSE;
            }

            //创建注记图层字体
            IFontDispPtr pFont(CLSID_StdFont);
            IFontPtr fnt = pFont;
            fnt->put_Name(CComBSTR("宋体"));
            CY cy;
            cy.int64 = 9;
            fnt->put_Size(cy);
            m_pAnnoTextFont = pFont.Detach();
        }
        else
        {
            //文本点
            hr = CreateDatasetFeatureClass(pFtWS, NULL, ipFieldsText, CComBSTR("TextPoint"), esriFTSimple, m_pFeatClassText);
            if (m_pFeatClassText != NULL)
            {
                hr = m_pFeatClassText->Insert(VARIANT_TRUE, &m_pTextFeatureCursor);
                if (FAILED(hr))
                {
                    sInfoText.Format("创建文本点FeatureCursor失败:%s", CatchErrorInfo());
                    WriteLog(sInfoText);
                }
                hr = m_pFeatClassText->CreateFeatureBuffer(&m_pTextFeatureBuffer);
                if (FAILED(hr))
                {
                    sInfoText.Format("创建文本FeautureBuffer失败:%s", CatchErrorInfo());
                    WriteLog(sInfoText);
                }
            }
            else
            {
                sInfoText.Format("创建Text要素类失败:%s", CatchErrorInfo());
                WriteLog(sInfoText);
                return FALSE;
            }
        }

        //扩展属性表
        hr = CreateExtendTable(pFtWS, CComBSTR("ExtendTable"), &m_pExtendTable);
        if (m_pExtendTable != NULL)
        {
            hr = m_pExtendTable->Insert(VARIANT_TRUE, &m_pExtentTableRowCursor);
            if (FAILED(hr))
            {
                sInfoText.Format("创建TableBuffer失败:%s", CatchErrorInfo());
                WriteLog(sInfoText);
            }
            hr = m_pExtendTable->CreateRowBuffer(&m_pExtentTableRowBuffer);
            if (FAILED(hr))
            {
                sInfoText.Format("创建TableCursor失败:%s", CatchErrorInfo());
                WriteLog(sInfoText);
            }
        }
        else
        {
            sInfoText.Format("创建ExtendTable失败:%s", CatchErrorInfo());
            WriteLog(sInfoText);
            return FALSE;
        }

        m_bFinishedCreateFtCls = TRUE;

        return TRUE;
    }
    catch (...)
    {
        return FALSE;
    }
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 逐个读取CAD文件
//输入参数 :
//返 回 值 :
//修改日志 :
//////////////////////////////////////////////////////////////////////////
BOOL XDWGReader::ReadFile(LPCTSTR lpdwgFilename)
{
    try
    {
        //生成目标GDB图层
        if (!m_bFinishedCreateFtCls)
        {
            if (!CreateTargetAllFeatureClass())
            {
                WriteLog("创建目标要素类出现异常，无法进行格式转换。");
                return FALSE;
            }
        }

        //清除不读的图层列表
        m_UnReadLayers.RemoveAll();

        //打开CAD文件并读取
        //得到DWG图名和日志文件名
        CString szDatasetName ;
        CString szLogFileName;
        int index;

        CString sFileName = lpdwgFilename;
        sFileName = sFileName.Mid(sFileName.ReverseFind('\\') + 1);

        index = ((CString) lpdwgFilename).ReverseFind('\\');
        int ilength = ((CString) lpdwgFilename).GetLength();
        szDatasetName = CString(lpdwgFilename).Right(ilength - 1 - index);
        index = szDatasetName.ReverseFind('.');
        szDatasetName = szDatasetName.Left(index);
        m_strDwgName = szDatasetName;

        // 记录开始处理时间
        CTime tStartTime = CTime::GetCurrentTime();

        CString sInfoText;
        sInfoText.Format("开始读 %s 文件.", lpdwgFilename);
        WriteLog(sInfoText);

        if (m_pProgressBar != NULL)
        {
            m_pProgressBar->SetPos(0);
            CString sProgressText;
            sProgressText.Format("正在读取%s, 请稍候...", lpdwgFilename);
            m_pProgressBar->SetWindowText(sProgressText);
        }

        OdDbDatabasePtr pDb;
        pDb = svcs.readFile(lpdwgFilename, false, false, Oda::kShareDenyReadWrite);
        if (pDb.isNull())
        {
            WriteLog("DWG文件为空!");
        }

        // 从dwg文件获得范围
        sInfoText.Format("图幅范围: 最小X坐标:%f, 最大X坐标:%f, 最小Y坐标:%f, 最大Y坐标:%f \n", 0.9 * pDb->getEXTMIN().x, 1.1 * pDb->getEXTMAX().x, 0.9 * pDb->getEXTMIN().y, 1.1 * pDb->getEXTMAX().y);
        WriteLog(sInfoText);

        //读CAD文件
        ReadBlock(pDb);
        pDb.release();



        //记录完成时间
        CTime tEndTime = CTime::GetCurrentTime();
        CTimeSpan span = tEndTime - tStartTime;
        sInfoText.Format("%s文件转换完成!共耗时%d时%d分%d秒.", lpdwgFilename, span.GetHours(), span.GetMinutes(), span.GetSeconds());
        WriteLog(sInfoText);
        WriteLog("==============================================================");

        return TRUE;

    }
    catch (...)
    {
        CString sErr;
        sErr.Format("%s文件不存在或正处于打开状态，无法进行数据读取，请检查。", lpdwgFilename);
        WriteLog(sErr);

        return FALSE;
    }
}

/********************************************************************
简要描述 : 设置日志存放路径
输入参数 :
返 回 值 :
日    期 : 2008/09/27,BeiJing.
作    者 : 宗亮 <zongliang@Hy.com.cn>
修改日志 :
*********************************************************************/
void XDWGReader::PutLogFilePath(CString sLogFile)
{
    m_pLogRec = new CLogRecorder(sLogFile);
    m_sLogFilePath = sLogFile;
}

//写日志文件
void XDWGReader::WriteLog(CString sLog)
{
    if (m_pLogRec == NULL)
    {
        return;
    }

    if (!sLog.IsEmpty())
    {
        m_pLogRec->WriteLog(sLog);
    }
}

//结束DWG的读取工作
BOOL XDWGReader::CommitReadDwg()
{
    //释放用到的对象
    ReleaseAOs();

	if (m_pLogRec != NULL)
	{
		m_pLogRec->CloseFile();
	}

    return TRUE;
}


void XDWGReader::ReadHeader(OdDbDatabase* pDb)
{
    OdString sName = pDb->getFilename();
    CString sInfoText;
    sInfoText.Format("Database was loaded from:%s", sName.c_str());
    WriteLog(sInfoText);
    OdDb::DwgVersion vVer = pDb->originalFileVersion();
    sInfoText.Format("File version is: %s", OdDb::DwgVersionToStr(vVer));
    WriteLog(sInfoText);
    sInfoText.Format("Header Variables: %f,%f", pDb->getLTSCALE(), pDb->getATTMODE());
    WriteLog(sInfoText);
    OdDbDate d = pDb->getTDCREATE();
    short month, day, year, hour, min, sec, msec;
    d.getDate(month, day, year);
    d.getTime(hour, min, sec, msec);
    sInfoText.Format("  TDCREATE:  %d-%d-%d,%d:%d:%d", month, day, year, hour, min, sec);
    WriteLog(sInfoText);

    d = pDb->getTDUPDATE();
    d.getDate(month, day, year);
    d.getTime(hour, min, sec, msec);
    sInfoText.Format("  TDCREATE:  %d-%d-%d,%d:%d:%d", month, day, year, hour, min, sec);
    WriteLog(sInfoText);
}

void XDWGReader::ReadSymbolTable(OdDbObjectId tableId)
{
    OdDbSymbolTablePtr pTable = tableId.safeOpenObject();
    CString sInfoText;
    sInfoText.Format("表名:%s", pTable->isA()->name());
    WriteLog(sInfoText);
    OdDbSymbolTableIteratorPtr pIter = pTable->newIterator();
    for (pIter->start(); !pIter->done(); pIter->step())
    {
        OdDbSymbolTableRecordPtr pTableRec = pIter->getRecordId().safeOpenObject();
        CString TableRecName;
        TableRecName.Format("%s", pTableRec->getName().c_str());
        TableRecName.MakeUpper();
        sInfoText.Format("  %s<%s>", TableRecName, pTableRec->isA()->name());
        WriteLog(sInfoText);
    }
}

void XDWGReader::ReadLayers(OdDbDatabase* pDb)
{
    OdDbLayerTablePtr pLayers = pDb->getLayerTableId().safeOpenObject();
    CString sInfoText;

    sInfoText.Format("层名:%s", pLayers->desc()->name());
    WriteLog(sInfoText);

    OdDbSymbolTableIteratorPtr pIter = pLayers->newIterator();
    for (pIter->start(); !pIter->done(); pIter->step())
    {
        OdDbLayerTableRecordPtr pLayer = pIter->getRecordId().safeOpenObject();
        CString LayerName;
        LayerName.Format("%s", pLayer->desc()->name());
        LayerName.MakeUpper();
        sInfoText.Format("  %s<%s>,layercolor:%d,%s,%s,%s,%s", pLayer->getName().c_str(), LayerName, pLayer->colorIndex(), pLayer->isOff() ? "Off" : "On", pLayer->isLocked() ? "Locked" : "Unlocked", pLayer->isFrozen() ? "Frozen" : "UnFrozen", pLayer->isDependent() ? "Dep. on XRef" : "Not dep. on XRef");
        WriteLog(sInfoText);
    }
}

/************************************************************************
简要描述 : 读DWG扩展属性,并写入到扩展属性表中
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
void XDWGReader::ReadExtendAttribs(OdResBuf* xIter, CString sEntityHandle)
{
    if (xIter == 0 || m_pExtendTable == NULL)
        return;

    CMapStringToPtr mapExtraRes; //保存所有应用及扩展属性 (应用名+CStringList*)

    //Registered Application Name
    CString sAppName;
    CString sExtendValue;
    //CStringList lstExtendValues;//所有扩展属性,用[]号分隔

    OdResBuf* xIterLoop = xIter;
    for (; xIterLoop != 0; xIterLoop = xIterLoop->next())
    {
        int code = xIterLoop->restype();
        switch (OdDxfCode::_getType(code))
        {
        case OdDxfCode::Name:
        case OdDxfCode::String:
            sExtendValue.Format("%s", xIterLoop->getString().c_str());
            break;

        case OdDxfCode::Bool:
            sExtendValue.Format("%d", xIterLoop->getBool());
            break;

        case OdDxfCode::Integer8:
            sExtendValue.Format("%d", xIterLoop->getInt8());
            break;

        case OdDxfCode::Integer16:
            sExtendValue.Format("%d", xIterLoop->getInt16());
            break;

        case OdDxfCode::Integer32:
            sExtendValue.Format("%d", xIterLoop->getInt32());
            break;

        case OdDxfCode::Double:
            sExtendValue.Format("%f", xIterLoop->getDouble());
            break;

        case OdDxfCode::Angle:
            sExtendValue.Format("%f", xIterLoop->getDouble());
            break;

        case OdDxfCode::Point:
        {
            OdGePoint3d p = xIterLoop->getPoint3d();
            sExtendValue.Format("%f,%f,%f", p.x, p.y, p.z);
        }
        break;

        case OdDxfCode::BinaryChunk:
            sExtendValue = "<Binary Data>";
            break;

        case OdDxfCode::Handle:
        case OdDxfCode::LayerName:
            sExtendValue.Format("%s", xIterLoop->getString().c_str());
            break;

        case OdDxfCode::ObjectId:
        case OdDxfCode::SoftPointerId:
        case OdDxfCode::HardPointerId:
        case OdDxfCode::SoftOwnershipId:
        case OdDxfCode::HardOwnershipId:
        {
            OdDbHandle h = xIterLoop->getHandle();
            sExtendValue.Format("%s", h.ascii());
        }
        break;
        case OdDxfCode::Unknown:
        default:
            sExtendValue = "Unknown";
            break;
        }

        //Registered Application Name
        if (code == OdResBuf::kDxfRegAppName)
        {
            sAppName = sExtendValue;
            //生成对应于该应用的StringList
            CStringList* pLstExtra = new CStringList();
            mapExtraRes.SetAt(sAppName, pLstExtra);
        }
        else if (code == OdResBuf::kDxfXdAsciiString || code == OdResBuf::kDxfXdReal)
        {
            void* rValue;
            if (mapExtraRes.Lookup(sAppName, rValue))
            {
                CStringList* pLstExtra = (CStringList*)rValue;

                //保存到对应于该APPName的List中
                pLstExtra->AddTail(sExtendValue);
            }
        }
    }

    POSITION mapPos = mapExtraRes.GetStartPosition();
    while (mapPos)
    {
        CString sAppName;
        void* rValue;
        mapExtraRes.GetNextAssoc(mapPos, sAppName, rValue);

        CStringList* pList = (CStringList*) rValue;

        HRESULT hr;
        long lFieldIndex;

        CComBSTR bsStr;
        CComVariant vtVal;
        //生成扩展属性字符串
        POSITION pos = pList->GetHeadPosition();
        if (pos != NULL)
        {
            CString sAllValues = "[" + pList->GetNext(pos) + "]";
            while (pos != NULL)
            {
                sAllValues = sAllValues + "[" + pList->GetNext(pos) + "]";
            }

            //Add Extend data to Extend Table
            bsStr = "Handle";
            m_pExtendTable->FindField(bsStr, &lFieldIndex);
            vtVal = sEntityHandle;
            m_pExtentTableRowBuffer->put_Value(lFieldIndex, vtVal);

            bsStr = "BaseName";
            m_pExtendTable->FindField(bsStr, &lFieldIndex);
            vtVal = m_strDwgName;
            m_pExtentTableRowBuffer->put_Value(lFieldIndex, vtVal);

            bsStr = "XDataName";
            m_pExtendTable->FindField(bsStr, &lFieldIndex);
            sAppName.MakeUpper();
            vtVal = sAppName;
            m_pExtentTableRowBuffer->put_Value(lFieldIndex, vtVal);

            bsStr = "XDataValue";
            m_pExtendTable->FindField(bsStr, &lFieldIndex);
            vtVal = sAllValues;
            m_pExtentTableRowBuffer->put_Value(lFieldIndex, vtVal);

            hr = m_pExtentTableRowCursor->InsertRow(m_pExtentTableRowBuffer, &m_TableId);
            if (FAILED(hr))
            {
                WriteLog("扩展属性读取失败:" + CatchErrorInfo());
            }
        }

        m_pExtentTableRowCursor->Flush();

        vtVal.Clear();
        bsStr.Empty();

        pList->RemoveAll();
        delete pList;
    }

    mapExtraRes.RemoveAll();
}


/********************************************************************
简要描述 : 重命名图层名，针对济南项目点和线在同以图层下，分离出线要素到指定图层下
输入参数 :
返 回 值 :
修改日志 :
*********************************************************************/
/*void XDWGReader::RenameEntityLayerName(CString sDwgOriLayerName, IFeatureBuffer*& pFeatBuffer)
{
	if (m_lstRenameLayers.GetCount() <= 0)
	{
		return;
	}

	//重新指定图层，把线层放到相应的辅助线图层中去
	POSITION pos = m_lstRenameLayers.GetHeadPosition();
	while (pos != NULL)
	{
		RenameLayerRecord* pRenameRec = m_lstRenameLayers.GetNext(pos);

		if (pRenameRec->sDWG_LAYERNAME_CONTAINS.IsEmpty()||pRenameRec->sNEW_DWG_LAYERNAME.IsEmpty()||pRenameRec->sNEW_LAYERTYPE.IsEmpty())
		{
			continue;
		}

		if (pRenameRec->sNEW_LAYERTYPE.CompareNoCase("Line") == 0)
		{
			CStringList lstKeys;
			CString sKeyStr;
			CString sLayerNameContains = pRenameRec->sDWG_LAYERNAME_CONTAINS;
			int iPos = sLayerNameContains.Find(',');
			while (iPos != -1)
			{
				sKeyStr = sLayerNameContains.Mid(0, iPos);
				sLayerNameContains = sLayerNameContains.Mid(iPos + 1);
				iPos = sLayerNameContains.Find(',');

				lstKeys.AddTail(sKeyStr);
			}

			sKeyStr = sLayerNameContains;

			lstKeys.AddTail(sKeyStr);

			bool bFindKey = true;
			for (int ki=0; ki< lstKeys.GetCount(); ki++)
			{
				sKeyStr = lstKeys.GetAt(lstKeys.FindIndex(ki));
				if (sDwgOriLayerName.Find(sKeyStr) == -1)
				{
					bFindKey = false;
					break;
				}
			}
			//如果包含所有特征值，则更名
			if (bFindKey)
			{
				AddAttributes("Layer", pRenameRec->sNEW_DWG_LAYERNAME, pFeatBuffer);
				break;
			}

		}
	}
}

*/

/********************************************************************
简要描述 : 插入注记要素
输入参数 :
返 回 值 :
修改日志 :
*********************************************************************/
void XDWGReader::InsertAnnoFeature(OdRxObject* pEnt)
{
    HRESULT hr;
    OdDbEntityPtr pOdDbEnt = pEnt;
    if (pOdDbEnt.isNull()) return;
    CString sEntType = pOdDbEnt->isA()->name();
    if (sEntType.Compare("AcDbMText") == 0 || sEntType.Compare("AcDbText") == 0 || sEntType.Compare("AcDbAttribute") == 0)
    {
        // 添加属性
        AddBaseAttributes(pOdDbEnt, "Annotation", m_pAnnoFeatureBuffer);

        //CString sTempVal;
        CString sText = "";
        double dHeight = 0;
        double dWeight = 0;
        double dAngle = 0;
        OdGePoint3d textPos;
		//对齐点
		OdGePoint3d alignPoint;

        esriTextHorizontalAlignment horizAlign = esriTHALeft;
        esriTextVerticalAlignment vertAlign = esriTVABaseline;

        CString sTextStyle = "STANDARD";
        CString sHeight = "0";
        CString sElevation = "0";
        CString sThickness = "0";
        CString sOblique = "0";

        if (sEntType.Compare("AcDbMText") == 0)
        {
            OdDbMTextPtr pMText = OdDbMTextPtr(pEnt);

            //文本内容
            sText = pMText->contents();
            int iPos = sText.ReverseFind(';');
            sText = sText.Mid(iPos + 1);
            sText.Replace("{", "");
            sText.Replace("}", "");

            //文本风格
            OdDbSymbolTableRecordPtr symbolbRec = OdDbSymbolTableRecordPtr(pMText->textStyle().safeOpenObject());
            if (!symbolbRec.isNull())
            {
                sTextStyle.Format("%s", symbolbRec->getName());
            }
            //高度
            sHeight.Format("%f", pMText->textHeight());
            //高程值
            sElevation.Format("%f", pMText->location().z);

            ////生成注记需要的参数////
            //角度
            dAngle = pMText->rotation();
            //高和宽
            dHeight = pMText->textHeight();
            dWeight = pMText->width();
            //位置点
            textPos = pMText->location();

            //设置对齐方式
            if (pMText->horizontalMode() == OdDb::kTextLeft)
            {
                horizAlign = esriTHALeft;
            }
            else if (pMText->horizontalMode() == OdDb::kTextCenter)
            {
                horizAlign = esriTHACenter;
            }
            else if (pMText->horizontalMode() == OdDb::kTextRight)
            {
                horizAlign = esriTHARight;
            }
            else if (pMText->horizontalMode() == OdDb::kTextFit)
            {
                horizAlign = esriTHAFull;
            }
            if (pMText->verticalMode() == OdDb::kTextBase)
            {
                vertAlign = esriTVABaseline;
            }
            else if (pMText->verticalMode() == OdDb::kTextBottom)
            {
                vertAlign = esriTVABottom;
            }
            else if (pMText->verticalMode() == OdDb::kTextTop)
            {
                vertAlign = esriTVATop;
            }
            else if (pMText->verticalMode() == OdDb::kTextVertMid)
            {
                vertAlign = esriTVACenter;
            }

        }
        else if (sEntType.Compare("AcDbText") == 0 || sEntType.Compare("AcDbAttribute") == 0)
        {
            OdDbTextPtr pText = OdDbTextPtr(pEnt);

            //文本内容
            sText = pText->textString();

            //文本风格
            OdDbSymbolTableRecordPtr symbolbRec = OdDbSymbolTableRecordPtr(pText->textStyle().safeOpenObject());
            if (!symbolbRec.isNull())
            {
                sTextStyle.Format("%s", symbolbRec->getName());
            }
            //高程值
            sElevation.Format("%f", pText->position().z);
            //高度
            sHeight.Format("%f", pText->height());
            //厚度
            sThickness.Format("%.f", pText->thickness());

            //倾角
            sOblique.Format("%f", pText->oblique());

            ////生成注记需要的参数////
            //角度
            dAngle = pText->rotation();
            dHeight = pText->height();
            dWeight = 0;
			
			textPos = pText->position();

            alignPoint = pText->alignmentPoint();

			//if (textPos.x <= 0.0001 && textPos.y <= 0.0001) //如果没有对齐点，则使用位置点
			//{
			//	textPos = pText->position();
			//}

			
			CString tempstr;

			tempstr.Format("%f", alignPoint.x);
			AddAttributes("AlignPtX", tempstr, m_pAnnoFeatureBuffer);

			tempstr.Format("%f", alignPoint.y);
			AddAttributes("AlignPtY", tempstr, m_pAnnoFeatureBuffer);
			


            //OdGePoint3dArray boundingPoints;
            //pText->getBoundingPoints(boundingPoints);
            //OdGePoint3d topLeft = boundingPoints[0];
            //OdGePoint3d topRight = boundingPoints[1];
            //OdGePoint3d bottomLeft  = boundingPoints[2];
            //OdGePoint3d bottomRight = boundingPoints[3];

            //设置对齐方式
            if (pText->horizontalMode() == OdDb::kTextLeft)
            {
                horizAlign = esriTHALeft;
            }
            else if (pText->horizontalMode() == OdDb::kTextCenter)
            {
                horizAlign = esriTHACenter;
            }
            else if (pText->horizontalMode() == OdDb::kTextRight)
            {
                horizAlign = esriTHARight;
            }
            else if (pText->horizontalMode() == OdDb::kTextFit)
            {
                horizAlign = esriTHAFull;
            }
            if (pText->verticalMode() == OdDb::kTextBase)
            {
                vertAlign = esriTVABaseline;
            }
            else if (pText->verticalMode() == OdDb::kTextBottom)
            {
                vertAlign = esriTVABottom;
            }
            else if (pText->verticalMode() == OdDb::kTextTop)
            {
                vertAlign = esriTVATop;
            }
            else if (pText->verticalMode() == OdDb::kTextVertMid)
            {
                vertAlign = esriTVACenter;
            }
        }

        //设置注记文本风格
        AddAttributes("TextStyle", sTextStyle, m_pAnnoFeatureBuffer);
        AddAttributes("Height", sHeight, m_pAnnoFeatureBuffer);
        AddAttributes("Elevation", sElevation, m_pAnnoFeatureBuffer);
        AddAttributes("Thickness", sThickness, m_pAnnoFeatureBuffer);
        AddAttributes("Oblique", sOblique, m_pAnnoFeatureBuffer);

        //创建 Element
        ITextElementPtr pTextElement = MakeTextElementByStyle(sText, dAngle, dHeight, textPos.x, textPos.y, m_dAnnoScale, horizAlign, vertAlign);
        IElementPtr pElement = pTextElement;
        IAnnotationFeaturePtr pTarAnnoFeat = m_pAnnoFeatureBuffer;
        hr = pTarAnnoFeat->put_Annotation(pElement);
        PutExtendAttribsValue(m_pAnnoFeatureBuffer, OdDbEntityPtr(pEnt)->xData());
        CComVariant OID;
        hr = m_pAnnoFeatureCursor->InsertFeature(m_pAnnoFeatureBuffer, &OID);
        if (FAILED(hr))
        {
            CString sInfoText;
            sInfoText = "Annotation对象写入到PGDB失败." + CatchErrorInfo();
            WriteLog(sInfoText);
            m_lUnReadEntityNum++;
        }
    }
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 插入CAD属性对象
//输入参数 :
//返 回 值 :
//修改日志 :
//////////////////////////////////////////////////////////////////////////
void XDWGReader::InsertDwgAttribFeature(OdRxObject* pEnt)
{
    HRESULT hr;
    OdDbEntityPtr pOdDbEnt = pEnt;
    if (pOdDbEnt.isNull()) return;
    CString sEntType = pOdDbEnt->isA()->name();
    if (strcmp(sEntType, "AcDbAttributeDefinition") == 0)
    {
        // 添加属性
        AddBaseAttributes(pOdDbEnt, "Annotation", m_pAnnoFeatureBuffer);

        //CString sTempVal;
        CString sText = "";
        double dHeight = 0;
        double dWeight = 0;
        double dAngle = 0;
        OdGePoint3d textPos;

        esriTextHorizontalAlignment horizAlign = esriTHALeft;
        esriTextVerticalAlignment vertAlign = esriTVABaseline;

        CString sTextStyle = "STANDARD";
        CString sHeight = "0";
        CString sElevation = "0";
        CString sThickness = "0";
        CString sOblique = "0";

        OdDbAttributeDefinitionPtr pText = OdDbAttributeDefinitionPtr(pEnt);

        //文本内容
        CString sTag = pText->tag();
        CString sPrompt = pText->prompt();
        sText = sTag;

        //文本风格
        OdDbSymbolTableRecordPtr symbolbRec = OdDbSymbolTableRecordPtr(pText->textStyle().safeOpenObject());
        if (!symbolbRec.isNull())
        {
            sTextStyle.Format("%s", symbolbRec->getName());
        }
        //高程值
        sElevation.Format("%f", pText->position().z);
        //高度
        sHeight.Format("%f", pText->height());
        //厚度
        sThickness.Format("%.f", pText->thickness());

        //倾角
        sOblique.Format("%f", pText->oblique());

        ////生成注记需要的参数////
        //角度
        dAngle = pText->rotation();
        dHeight = pText->height();
        dWeight = 0;
        
		textPos = pText->alignmentPoint();
		if (textPos.x <= 0.0001 && textPos.y <= 0.0001) //如果没有对齐点，则使用位置点
		{
			textPos = pText->position();
		}


        //设置对齐方式
        if (pText->horizontalMode() == OdDb::kTextLeft)
        {
            horizAlign = esriTHALeft;
        }
        else if (pText->horizontalMode() == OdDb::kTextCenter)
        {
            horizAlign = esriTHACenter;
        }
        else if (pText->horizontalMode() == OdDb::kTextRight)
        {
            horizAlign = esriTHARight;
        }
        else if (pText->horizontalMode() == OdDb::kTextFit)
        {
            horizAlign = esriTHAFull;
        }
        if (pText->verticalMode() == OdDb::kTextBase)
        {
            vertAlign = esriTVABaseline;
        }
        else if (pText->verticalMode() == OdDb::kTextBottom)
        {
            vertAlign = esriTVABottom;
        }
        else if (pText->verticalMode() == OdDb::kTextTop)
        {
            vertAlign = esriTVATop;
        }
        else if (pText->verticalMode() == OdDb::kTextVertMid)
        {
            vertAlign = esriTVACenter;
        }

        //设置注记文本风格
        AddAttributes("TextStyle", sTextStyle, m_pAnnoFeatureBuffer);
        AddAttributes("Height", sHeight, m_pAnnoFeatureBuffer);
        AddAttributes("Elevation", sElevation, m_pAnnoFeatureBuffer);
        AddAttributes("Thickness", sThickness, m_pAnnoFeatureBuffer);
        AddAttributes("Oblique", sOblique, m_pAnnoFeatureBuffer);

        //创建 Element
        ITextElementPtr pTextElement = MakeTextElementByStyle(sText, dAngle, dHeight, textPos.x, textPos.y, m_dAnnoScale, horizAlign, vertAlign);
        IElementPtr pElement = pTextElement;
        IAnnotationFeaturePtr pTarAnnoFeat = m_pAnnoFeatureBuffer;
        hr = pTarAnnoFeat->put_Annotation(pElement);
        PutExtendAttribsValue(m_pAnnoFeatureBuffer, OdDbEntityPtr(pEnt)->xData());
        CComVariant OID;
        hr = m_pAnnoFeatureCursor->InsertFeature(m_pAnnoFeatureBuffer, &OID);
        if (FAILED(hr))
        {
            CString sInfoText;
            sInfoText = "Annotation对象写入到PGDB失败." + CatchErrorInfo();
            WriteLog(sInfoText);
            m_lUnReadEntityNum++;
        }
    }
}

/********************************************************************
简要描述 : 添加扩展属性值
输入参数 :
返 回 值 :
修改日志 :
*********************************************************************/
BOOL XDWGReader::PutExtendAttribsValue(IFeatureBuffer*& pFtBuf, OdResBuf* xIter)
{
    if (m_IsJoinXDataAttrs == FALSE || m_Regapps.GetCount() <= 0 || xIter == NULL)
    {
        return FALSE;
    }

    CMapStringToPtr mapExtraRes; //保存所有应用及扩展属性 (应用名+CStringList*)

    //Registered Application Name
    CString sAppName;
    CString sExtendValue;
    //CStringList lstExtendValues;//所有扩展属性,用,号分隔

    OdResBuf* xIterLoop = xIter;

    for (; xIterLoop != 0; xIterLoop = xIterLoop->next())
    {
        int code = xIterLoop->restype();
        switch (OdDxfCode::_getType(code))
        {
        case OdDxfCode::Name:
        case OdDxfCode::String:
            sExtendValue.Format("%s", xIterLoop->getString().c_str());
            break;

        case OdDxfCode::Bool:
            sExtendValue.Format("%d", xIterLoop->getBool());
            break;

        case OdDxfCode::Integer8:
            sExtendValue.Format("%d", xIterLoop->getInt8());
            break;

        case OdDxfCode::Integer16:
            sExtendValue.Format("%d", xIterLoop->getInt16());
            break;

        case OdDxfCode::Integer32:
            sExtendValue.Format("%d", xIterLoop->getInt32());
            break;

        case OdDxfCode::Double:
            sExtendValue.Format("%f", xIterLoop->getDouble());
            break;

        case OdDxfCode::Angle:
            sExtendValue.Format("%f", xIterLoop->getDouble());
            break;

        case OdDxfCode::Point:
        {
            OdGePoint3d p = xIterLoop->getPoint3d();
            sExtendValue.Format("%f,%f,%f", p.x, p.y, p.z);
        }
        break;

        case OdDxfCode::BinaryChunk:
            sExtendValue = "<Binary Data>";
            break;

        case OdDxfCode::Handle:
        case OdDxfCode::LayerName:
            sExtendValue.Format("%s", xIterLoop->getString().c_str());
            break;

        case OdDxfCode::ObjectId:
        case OdDxfCode::SoftPointerId:
        case OdDxfCode::HardPointerId:
        case OdDxfCode::SoftOwnershipId:
        case OdDxfCode::HardOwnershipId:
        {
            OdDbHandle h = xIterLoop->getHandle();
            sExtendValue.Format("%s", h.ascii());
        }
        break;
        case OdDxfCode::Unknown:
        default:
            sExtendValue = "Unknown";
            break;
        }

        //Registered Application Name
        if (code == OdResBuf::kDxfRegAppName)
        {
            sAppName = sExtendValue;
            //生成对应于该应用的StringList
            CStringList* pLstExtra = new CStringList();
            mapExtraRes.SetAt(sAppName, pLstExtra);
        }
        else if (code == OdResBuf::kDxfXdAsciiString || code == OdResBuf::kDxfXdReal)
        {
            void* rValue;
            if (mapExtraRes.Lookup(sAppName, rValue))
            {
                CStringList* pLstExtra = (CStringList*) rValue;

                //保存到对应于该APPName的List中
                pLstExtra->AddTail(sExtendValue);
            }
        }
    }

    //得到字段
    IFieldsPtr pFields;
    pFtBuf->get_Fields(&pFields);

    POSITION mapPos = mapExtraRes.GetStartPosition();
    while (mapPos)
    {
        CString sAppName;
        void* rValue;
        mapExtraRes.GetNextAssoc(mapPos, sAppName, rValue);
        CStringList* pList = (CStringList*) rValue;

        long lIdx = 0;
        pFields->FindField(CComBSTR(sAppName), &lIdx);
        if (lIdx != -1)
        {
            CString sAllValues = "";
            //生成扩展属性字符串
            POSITION pos = pList->GetHeadPosition();
            if (pos != NULL)
            {
                sAllValues = pList->GetNext(pos);
                while (pos != NULL)
                {
                    sAllValues = sAllValues + "," + pList->GetNext(pos) ;
                }

                pFtBuf->put_Value(lIdx, CComVariant(sAllValues));
            }
        }

        pList->RemoveAll();
        delete pList;

    }

    mapExtraRes.RemoveAll();

    return TRUE;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 读CAD的每个实体要素
//输入参数 :
//返 回 值 :
//修改日志 :
//////////////////////////////////////////////////////////////////////////
void XDWGReader::ReadEntity(OdDbObjectId id)
{
    OdDbEntityPtr pEnt = id.safeOpenObject();
    OdDbLayerTableRecordPtr pLayerTableRecord = pEnt->layerId().safeOpenObject();

    CString sInfoText;

    if ((pLayerTableRecord->isOff() || pLayerTableRecord->isLocked() || pLayerTableRecord->isFrozen()) && (m_IsReadInvisible == FALSE))
    {
        //避免日志重复内容
        CString sUnReadLayer = pEnt->layer().c_str();
        POSITION pos = m_UnReadLayers.Find(sUnReadLayer);
        if (pos == NULL)
        {
            m_UnReadLayers.AddTail(sUnReadLayer);
            sInfoText.Format("<%s>层要素不可视不处理!", sUnReadLayer);
            WriteLog(sInfoText);
        }

        m_lUnReadEntityNum++;
    }
    else
    {
        OdDbHandle hTmp;
        char szEntityHandle[50] = {0};
        hTmp = pEnt->getDbHandle();
        hTmp.getIntoAsciiBuffer(szEntityHandle);

        //记录当前handle值
        m_sEntityHandle = szEntityHandle;

        //清空FeatureBuffer
        CleanAllFeatureBuffers();

        OdSmartPtr<OdDbEntity_Dumper> pEntDumper = pEnt;
        IGeometryPtr pShape;
        HRESULT hr;
        CComVariant OID;

        pEntDumper->m_DwgReader = this;
        // 获得几何数据
        pShape = pEntDumper->dump(pEnt);

        if (pShape == NULL)
        {
            m_lUnReadEntityNum++;
            return ;
        }

        //修正空间参考
        hr = pShape->Project(m_pSpRef);

        // 文本
        CString sEntType = OdDbEntityPtr(pEnt)->isA()->name();
        if ((strcmp(sEntType, "AcDbMText") == 0) || (strcmp(sEntType, "AcDbText") == 0) || (strcmp(sEntType, "AcDbShape") == 0))
        {
            if (m_IsCreateAnnotation)
            {
                //插入注记对象
                InsertAnnoFeature(pEnt);
            }
            else
            {
                hr = m_pTextFeatureBuffer->putref_Shape(pShape);
                if (SUCCEEDED(hr))
                {
                    AddBaseAttributes(pEnt, "Annotation", m_pTextFeatureBuffer);

                    //编码对照
                    if (CompareCodes(m_pTextFeatureBuffer))
                    {
						PutExtendAttribsValue(m_pTextFeatureBuffer, pEnt->xData());
                        hr = m_pTextFeatureCursor->InsertFeature(m_pTextFeatureBuffer, &OID);
                        if (FAILED(hr))
                        {
                            sInfoText = "Text对象写入到PGDB失败。" + CatchErrorInfo();
                            WriteLog(sInfoText);
                            m_lUnReadEntityNum++;
                        }
                    }
                }
                else
                {
                    sInfoText = "Text对象坐标不正确。" + CatchErrorInfo();
                    WriteLog(sInfoText);
                    m_lUnReadEntityNum++;
                }
            }
        }
        else
        {
            esriGeometryType shapeType;
            pShape->get_GeometryType(&shapeType);
            if (shapeType == esriGeometryPoint) //点
            {
                hr = m_pPointFeatureBuffer->putref_Shape(pShape);
                if (SUCCEEDED(hr))
                {
                    AddBaseAttributes(pEnt, "Point", m_pPointFeatureBuffer);

                    //编码对照
                    if (CompareCodes(m_pPointFeatureBuffer))
                    {
                        PutExtendAttribsValue(m_pPointFeatureBuffer, pEnt->xData());

                        hr = m_pPointFeatureCursor->InsertFeature(m_pPointFeatureBuffer, &OID);
                        if (FAILED(hr))
                        {
                            sInfoText = "Point对象写入到PGDB失败." + CatchErrorInfo();
                            WriteLog(sInfoText);
                            m_lUnReadEntityNum++;
                        }
                    }
                }
                else
                {
                    sInfoText = "Point对象坐标不正确." + CatchErrorInfo();
                    WriteLog(sInfoText);
                    m_lUnReadEntityNum++;
                }

                if (strcmp(pEnt->isA()->name(), "AcDbBlockReference") == 0)
                    m_lBlockNum++;
            }
            else if (shapeType == esriGeometryPolyline) //线
            {
                hr = m_pLineFeatureBuffer->putref_Shape(pShape);
                if (SUCCEEDED(hr))
                {
                    AddBaseAttributes(pEnt, "Line", m_pLineFeatureBuffer);

                    CString sDwgLayer;
                    sDwgLayer.Format("%s", pEnt->layer().c_str());

                    if (CompareCodes(m_pLineFeatureBuffer))
                    {
                        PutExtendAttribsValue(m_pLineFeatureBuffer, pEnt->xData());

                        hr = m_pLineFeatureCursor->InsertFeature(m_pLineFeatureBuffer, &OID);
                        if (FAILED(hr))
                        {
                            IFieldsPtr pFlds;
                            m_pLineFeatureBuffer->get_Fields(&pFlds);
                            long numFields;
                            pFlds->get_FieldCount(&numFields);
                            for (int t = 0; t < numFields; t++)
                            {
                                CComVariant tVal;
                                IFieldPtr pFld;
                                pFlds->get_Field(t, &pFld);
                                CComBSTR bsName;
                                pFld->get_Name(&bsName);
                                m_pLineFeatureBuffer->get_Value(t, &tVal);
                            }

                            sInfoText = "Line对象写入到PGDB失败." + CatchErrorInfo();
                            WriteLog(sInfoText);
                            m_lUnReadEntityNum++;
                        }
                    }
                }
                else
                {
                    sInfoText = "Line对象坐标不正确." + CatchErrorInfo();
                    WriteLog(sInfoText);
                    m_lUnReadEntityNum++;
                }

                // 如果闭合就再生成面
                VARIANT_BOOL isclosed;
                IPolylinePtr pPolyline(CLSID_Polyline);
                pPolyline = pShape;
                pPolyline->get_IsClosed(&isclosed);

                if (isclosed && m_IsLine2Polygon)
                {
                    IPolygonPtr pPolygon(CLSID_Polygon);
                    ((ISegmentCollectionPtr) pPolygon)->AddSegmentCollection((ISegmentCollectionPtr) pPolyline);
					IAreaPtr pArea = (IAreaPtr)pPolygon;
					double dArea = 0.0;
					pArea->get_Area(&dArea);
					if (dArea < 0.0)
					{
						pPolygon->ReverseOrientation();
					}
                    hr = m_pPolygonFeatureBuffer->putref_Shape((IGeometryPtr)pPolygon);
                    if (SUCCEEDED(hr))
                    {
                        AddBaseAttributes(pEnt, "Polygon", m_pPolygonFeatureBuffer);

                        //编码对照
                        if (CompareCodes(m_pPolygonFeatureBuffer))
                        {
                            //挂接扩展属性
                            PutExtendAttribsValue(m_pPolygonFeatureBuffer, pEnt->xData());

                            hr = m_pPolygonFeatureCursor->InsertFeature(m_pPolygonFeatureBuffer, &OID);
                            if (FAILED(hr))
                            {
                                sInfoText = "Polygon对象写入到PGDB失败." + CatchErrorInfo();
                                WriteLog(sInfoText);
                            }
                        }
                    }
                    else
                    {
                        sInfoText = "Polyline对象坐标不正确." + CatchErrorInfo();
                        WriteLog(sInfoText);
                    }
                }
            }
            else if (shapeType == esriGeometryPolygon) //面、填充
            {
				if(m_IsReadPolygon)
				{
					IPolygonPtr pPolygon(CLSID_Polygon);
					pPolygon = pShape;
					IAreaPtr pArea = (IAreaPtr)pPolygon;
					double dArea = 0.0;
					pArea->get_Area(&dArea);
					if (dArea < 0.0)
					{
						pPolygon->ReverseOrientation();
					}

					hr = m_pPolygonFeatureBuffer->putref_Shape((IGeometryPtr)pPolygon);
					if (SUCCEEDED(hr))
					{
						AddBaseAttributes(pEnt, "Polygon", m_pPolygonFeatureBuffer);
						PutExtendAttribsValue(m_pPolygonFeatureBuffer, pEnt->xData());
						hr = m_pPolygonFeatureCursor->InsertFeature(m_pPolygonFeatureBuffer, &OID);
						if (FAILED(hr))
						{
							sInfoText = "Polygon对象写入到PGDB失败." + CatchErrorInfo();
							WriteLog(sInfoText);
							m_lUnReadEntityNum++;
						}
					}
					else
					{
						sInfoText = "Polygon对象坐标不正确." + CatchErrorInfo();
						WriteLog(sInfoText);
						m_lUnReadEntityNum++;
					}
				}
            }
            else
            {
                sInfoText.Format("%s图层中Handle值为:%s 的要素无法处理.", pEnt->layer().c_str(), szEntityHandle);
                WriteLog(sInfoText);

                //无法识别计数加1
                m_lUnReadEntityNum++;
            }
        }

        //读取扩展属性到扩展属性表
        ReadExtendAttribs(pEnt->xData(), szEntityHandle);
    }
}

//读CAD文件
void XDWGReader::ReadBlock(OdDbDatabase* pDb)
{
    // Open ModelSpace
    OdDbBlockTableRecordPtr pBlock = pDb->getModelSpaceId().safeOpenObject();

    // 初始化
    m_lBlockNum = 0;
    m_bn = -1;
    m_lEntityNum = 0;

    //无法读取的实体个数
    m_lUnReadEntityNum = 0;

    m_vID = 0;
    if (m_StepNum < 0)
        m_StepNum = 5000;
    // Get an entity iterator
    OdDbObjectIteratorPtr pEntIter = pBlock->newIterator();
    for (; !pEntIter->done(); pEntIter->step())
    {
        m_lEntityNum++;
    }

    //设定进度条范围
    if (m_pProgressBar)
    {
        m_pProgressBar->SetRange(0, m_lEntityNum);
        m_pProgressBar->SetPos(0);
    }

    pEntIter.release();
    // For each entity in the block
    pEntIter = pBlock->newIterator();
    int iReadCount = 0;
    for (; !pEntIter->done(); pEntIter->step())
    {
        try
        {
            ReadEntity(pEntIter->objectId());
        }
        catch (...)
        {
            char szEntityHandle[50] = {0};
            pEntIter->objectId().getHandle().getIntoAsciiBuffer(szEntityHandle);
            CString sErr;
            sErr.Format("读取Handle为%s的实体出现异常.", szEntityHandle);
            WriteLog(sErr);
        }

        //设定进度条步长
        if (m_pProgressBar)
        {
            m_pProgressBar->StepIt();
        }

        if (++iReadCount % m_StepNum == 0)
        {
            if (m_pPointFeatureCursor)
                m_pPointFeatureCursor->Flush();
            if (m_pTextFeatureCursor)
                m_pTextFeatureCursor->Flush();
            if (m_pLineFeatureCursor)
                m_pLineFeatureCursor->Flush();
            if (m_pAnnoFeatureCursor)
                m_pAnnoFeatureCursor->Flush();
            if (m_pPolygonFeatureCursor)
                m_pPolygonFeatureCursor->Flush();
            if (m_pExtentTableRowCursor)
                m_pExtentTableRowCursor->Flush();
        }
    }

    if (m_pPointFeatureCursor)
        m_pPointFeatureCursor->Flush();
    if (m_pTextFeatureCursor)
        m_pTextFeatureCursor->Flush();
    if (m_pLineFeatureCursor)
        m_pLineFeatureCursor->Flush();
    if (m_pAnnoFeatureCursor)
        m_pAnnoFeatureCursor->Flush();
    if (m_pPolygonFeatureCursor)
        m_pPolygonFeatureCursor->Flush();
    if (m_pExtentTableRowCursor)
        m_pExtentTableRowCursor->Flush();

    pEntIter.release();

    CString sResult;
    sResult.Format("处理要素总数:%d", m_lEntityNum - m_lUnReadEntityNum);
    WriteLog(sResult);
}


// arcgis 相关函数
HRESULT XDWGReader::AddBaseAttributes(OdDbEntity* pEnt, LPCTSTR strEnType, IFeatureBuffer*& pFeatureBuffer)
{
    long lindex;
    int ival ;
    CString strval;
    IFieldsPtr ipFields;
    char buff[20];
    OdDbHandle hTmp;
    hTmp = pEnt->getDbHandle();
    hTmp.getIntoAsciiBuffer(buff);

    if (pFeatureBuffer == NULL)
        return S_FALSE;
    pFeatureBuffer->get_Fields(&ipFields);
    //得到esri几何类型
    CComBSTR bsStr;
    CComVariant vtVal;
    bsStr = g_szEntityType;
    ipFields->FindField(bsStr, &lindex);
    vtVal = strEnType;
    pFeatureBuffer->put_Value(lindex, vtVal);

    //得到dwg几何类型
    bsStr = "DwgGeometry";
    ipFields->FindField(bsStr, &lindex);
    vtVal = pEnt->isA()->name();
    pFeatureBuffer->put_Value(lindex, vtVal);

    // 得到dwg实体编号
    bsStr = "Handle";
    ipFields->FindField(bsStr, &lindex);
    vtVal = buff;
    pFeatureBuffer->put_Value(lindex, vtVal);

    // 得到dwg图名，即dwg文件名。以确保handle唯一
    bsStr = "BaseName";
    ipFields->FindField(bsStr, &lindex);
    vtVal = m_strDwgName;
    pFeatureBuffer->put_Value(lindex, vtVal);

    // 得到dwg层名
    bsStr = "Layer";
    ipFields->FindField(bsStr, &lindex);
    strval.Format("%s", pEnt->layer().c_str());
    strval.MakeUpper();
    vtVal = strval;
    pFeatureBuffer->put_Value(lindex, vtVal);

    //	TRACE("Put Layer(AddBaseAttributes): "+ strval+" \r\n");

    // 得到dwg符号颜色,只能得到层的颜色，应该是每个要素的
    bsStr = "Color";
    ipFields->FindField(bsStr, &lindex);
    if (pEnt->colorIndex() > 255 || pEnt->colorIndex() < 1)
    {
        OdDbLayerTableRecordPtr pLayer = pEnt->layerId().safeOpenObject();
        ival = pLayer->colorIndex();
    }
    else
        ival = pEnt->colorIndex();
    vtVal = ival;
    pFeatureBuffer->put_Value(lindex, vtVal);

    // 得到 Linetype ，记录线型
    bsStr = "Linetype";
    ipFields->FindField(bsStr, &lindex);
    strval.Format("%s", pEnt->linetype().c_str());
    strval.MakeUpper();
    vtVal = strval;
    pFeatureBuffer->put_Value(lindex, vtVal);

    //对象可见性（可选）：0 = 可见；1 = 不可见
    // kInvisible  1 	kVisible  0
    bsStr = "Visible";
    ipFields->FindField(bsStr, &lindex);
    if (pEnt->visibility() == 1)
    {
        ival = 0;
    }
    else
    {
        ival = 1;
    }
    vtVal = ival;
    pFeatureBuffer->put_Value(lindex, vtVal);

    //扩展属性FeatureUID
    //bsStr = "FEATURE_UID";
    //ipFields->FindField(bsStr, &lindex);
    //if (lindex != -1)
    //{
    //    CString sFeatureUID = ReadFeatureUID(pEnt->xData());
    //    vtVal = sFeatureUID;
    //    pFeatureBuffer->put_Value(lindex, vtVal);
    //}

    vtVal.Clear();
    bsStr.Empty();

    return 0;
}

void XDWGReader::AddAttributes(LPCTSTR csFieldName, LPCTSTR csFieldValue, IFeatureBuffer*& pFeatureBuffer)
{
    try
    {
        long lindex;
        IFieldsPtr ipFields;
        CString strval;

        if (pFeatureBuffer == NULL)
            return;
        pFeatureBuffer->get_Fields(&ipFields);
        CComBSTR bsStr = csFieldName;
        ipFields->FindField(bsStr, &lindex);
        if (lindex != -1)
        {
            CComVariant vtVal;

            //把弧度值转换为角度值
            if (m_bConvertAngle && (strcmp("Angle", csFieldName) == 0))
            {
                double dRadian = atof(csFieldValue);
                double dAngle = dRadian * g_dAngleParam;
                vtVal = dAngle;
            }
            else
            {
                vtVal = csFieldValue;
            }

            HRESULT hr = pFeatureBuffer->put_Value(lindex, vtVal);

            vtVal.Clear();
        }

        bsStr.Empty();
    }
    catch (...)
    {
        CString sError;
        sError.Format("%s字段写入%s值时出错.", csFieldName, csFieldValue);
        WriteLog(sError);
    }
}

void XDWGReader::CleanAllFeatureBuffers()
{
	if (m_pAnnoFeatureBuffer)
		CleanFeatureBuffer(m_pAnnoFeatureBuffer);
    if (m_pTextFeatureBuffer)
        CleanFeatureBuffer(m_pTextFeatureBuffer);
    if (m_pLineFeatureBuffer)
        CleanFeatureBuffer(m_pLineFeatureBuffer);
    if (m_pPointFeatureBuffer)
        CleanFeatureBuffer(m_pPointFeatureBuffer);
    if (m_pPolygonFeatureBuffer)
        CleanFeatureBuffer(m_pPolygonFeatureBuffer);
}


//void XDWGReader::BlockIniAttributes()
//{
//    if (m_pTextFeatureBuffer)
//        IniBlockAttributes(m_pTextFeatureBuffer);
//    if (m_pLineFeatureBuffer)
//        IniBlockAttributes(m_pLineFeatureBuffer);
//    if (m_pPointFeatureBuffer)
//        IniBlockAttributes(m_pPointFeatureBuffer);
//    if (m_pPolygonFeatureBuffer)
//        IniBlockAttributes(m_pPolygonFeatureBuffer);
//}

//////////////////////////////////////////////////////////////////////////
//找出并解决内存泄漏问题 by zl
void XDWGReader::CleanFeatureBuffer(IFeatureBuffer* pFeatureBuffer)
{
    if (pFeatureBuffer == NULL)
        return;

    //释放内存
    IGeometryPtr pShape;
    HRESULT hr = pFeatureBuffer->get_Shape(&pShape);
    if (SUCCEEDED(hr))
    {
        if (pShape != NULL)
        {
            pShape->SetEmpty();
        }
    }

    IFieldsPtr ipFields;
    long iFieldCount;
    VARIANT_BOOL isEditable;
    esriFieldType fieldType;

    VARIANT emptyVal;
    ::VariantInit(&emptyVal);
    CComVariant emptyStr = "";

    pFeatureBuffer->get_Fields(&ipFields);
    ipFields->get_FieldCount(&iFieldCount);
    for (int i = 0; i < iFieldCount; i++)
    {
        IFieldPtr pFld;
        ipFields->get_Field(i, &pFld);

        pFld->get_Editable(&isEditable);
        pFld->get_Type(&fieldType);

        if (isEditable == VARIANT_TRUE && fieldType != esriFieldTypeGeometry)
        {
            if (fieldType == esriFieldTypeString)
            {
                pFeatureBuffer->put_Value(i, emptyStr);
            }
            else
            {
                pFeatureBuffer->put_Value(i, emptyVal);
            }
        }
    }
}

//void XDWGReader::IniBlockAttributes(IFeatureBuffer* pFeatureBuffer)
//{
//    long lindex;
//    //	double dbval;
//    CString strval;
//    IFieldsPtr ipFields;
//    if (pFeatureBuffer == NULL)
//        return;
//
//    //释放内存
//    IGeometry* pShape;
//    HRESULT hr = pFeatureBuffer->get_Shape(&pShape);
//    if (SUCCEEDED(hr))
//    {
//        if (pShape != NULL)
//        {
//            pShape->SetEmpty();
//        }
//    }
//
//    // 清空，否则会保留前一个的属性
//    pFeatureBuffer->get_Fields(&ipFields);
//    CComBSTR bsStr;
//    CComVariant vtVal;
//    bsStr = "Thickness";
//    ipFields->FindField(bsStr, &lindex);
//    if (lindex != -1)
//    {
//        vtVal = 0;
//        pFeatureBuffer->put_Value(lindex, vtVal);
//    }
//
//    bsStr = "Scale";
//    ipFields->FindField(bsStr, &lindex);
//    if (lindex != -1)
//    {
//        vtVal = 0;
//        pFeatureBuffer->put_Value(lindex, vtVal);
//    }
//
//    bsStr = "Angle";
//    ipFields->FindField(bsStr, &lindex);
//    if (lindex != -1)
//    {
//        vtVal = 0;
//        pFeatureBuffer->put_Value(lindex, vtVal);
//    }
//
//    bsStr = "Elevation";
//    ipFields->FindField(bsStr, &lindex);
//    if (lindex != -1)
//    {
//        vtVal = 0;
//        pFeatureBuffer->put_Value(lindex, vtVal);
//    }
//
//    bsStr = "Width";
//    ipFields->FindField(bsStr, &lindex);
//    if (lindex != -1)
//    {
//        vtVal = 0;
//        pFeatureBuffer->put_Value(lindex, vtVal);
//    }
//
//    bsStr.Empty();
//
//    //IniExtraAttributes(pFeatureBuffer, ipFields);
//
//    return;
//}

//void XDWGReader::OpenLogFile()
//{
//    //if (m_pLogRec != NULL)
//    //{
//    //	WinExec("Notepad.exe " + m_sLogFilePath, SW_SHOW);
//    //}
//
//    //if (m_LogList.GetCount() > 0)
//    //{
//    //    COleDateTime dtCur = COleDateTime::GetCurrentTime();
//    //    CString sName = dtCur.Format("%y%m%d_%H%M%S");
//    //    CString sLogFileName;
//    //    sLogFileName.Format("%sDwg转换日志_%s.log", GetLogPath(), sName);
//
//    //    CStdioFile f3(sLogFileName, CFile::modeCreate | CFile::modeWrite | CFile::typeText);
//    //    for (POSITION pos = m_LogList.GetHeadPosition(); pos != NULL;)
//    //    {
//    //        f3.WriteString(m_LogList.GetNext(pos) + "\n");
//    //    }
//    //    f3.Close();
//    //    WinExec("Notepad.exe " + sLogFileName, SW_SHOW);
//    //    m_LogList.RemoveAll();
//    //}
//}

CString XDWGReader::CatchErrorInfo()
{
    IErrorInfoPtr ipError;
    CComBSTR bsStr;
    CString sError;
    ::GetErrorInfo(0, &ipError);
    if (ipError)
    {
        ipError->GetDescription(&bsStr);
        sError = bsStr;
    }

    CString sRetErr;
    sRetErr.Format("读取Handle值为:%s 的对象时出错.错误原因:%s", m_sEntityHandle, sError);
    return sRetErr;
}

HRESULT XDWGReader::CreateDwgPointFields(ISpatialReference* ipSRef, IFields** ppfields)
{
    IFieldsPtr ipFields;

    ipFields.CreateInstance(CLSID_Fields);
    IFieldsEditPtr ipFieldsEdit(ipFields);
    IFieldPtr ipField;
    ipField.CreateInstance(CLSID_Field);
    IFieldEditPtr ipFieldEdit(ipField);

    // create the geometry field
    IGeometryDefPtr ipGeomDef(CLSID_GeometryDef);
    IGeometryDefEditPtr ipGeomDefEdit(ipGeomDef);

    // assign the geometry definiton properties.
    ipGeomDefEdit->put_GeometryType(esriGeometryPoint);
    ipGeomDefEdit->put_GridCount(1);
    //double dGridSize = 1000;
    //VARIANT_BOOL bhasXY;
    //ipSRef->HasXYPrecision(&bhasXY);
    //if (bhasXY)
    //{
    //    double xmin, ymin, xmax, ymax, dArea;
    //    ipSRef->GetDomain(&xmin, &xmax, &ymin, &ymax);
    //    dArea = (xmax - xmin) * (ymax - ymin);
    //    dGridSize = sqrt(dArea / 100);
    //}
    //if (dGridSize <= 0)
    //    dGridSize = 1000;
    ipGeomDefEdit->put_GridSize(0, DEFAULT_GIS_GRID_SIZE);
    ipGeomDefEdit->put_AvgNumPoints(2);
    ipGeomDefEdit->put_HasM(VARIANT_FALSE);
    ipGeomDefEdit->put_HasZ(VARIANT_FALSE);
    ipGeomDefEdit->putref_SpatialReference(ipSRef);

    ipFieldEdit->put_Name(CComBSTR(L"SHAPE"));
    ipFieldEdit->put_AliasName(CComBSTR(L"SHAPE"));
    ipFieldEdit->put_Type(esriFieldTypeGeometry);
    ipFieldEdit->putref_GeometryDef(ipGeomDef);
    ipFieldsEdit->AddField(ipField);

    // create the object id field
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"OBJECTID"));
    ipFieldEdit->put_AliasName(CComBSTR(L"OBJECT ID"));
    ipFieldEdit->put_Type(esriFieldTypeOID);
    ipFieldsEdit->AddField(ipField);

    // 创建 Entity ，记录esri实体类型
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(g_szEntityType));
    ipFieldEdit->put_AliasName(CComBSTR(g_szEntityType));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldsEdit->AddField(ipField);
    // 创建 DwgGeometry ，记录DWG实体类型
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"DwgGeometry"));
    ipFieldEdit->put_AliasName(CComBSTR(L"DwgGeometry"));
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
    // 创建 Linetype ，记录线型
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Linetype"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Linetype"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);
    // 创建 Thickness ，记录DWG实体厚度
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Thickness"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Thickness"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);
    // 创建 Scale ，记录DWG实体符号比例大小
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Scale"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Scale"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);
    // 创建 Elevation ，记录DWG实体高程值
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Elevation"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Elevation"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);
    // 创建 Blockname ，记录Block名字
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Blockname"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Blockname"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);
    // 创建 Blocknumber ，记录每个Block编号
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Blocknumber"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Blocknumber"));
    ipFieldEdit->put_Type(esriFieldTypeInteger);
    ipFieldsEdit->AddField(ipField);
    // 创建 Angle ，记录DWG实体旋转角度
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Angle"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Angle"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);

    // 创建 Visible ，记录DWG实体是否可见，0不可见，1可见
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Visible"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Visible"));
    ipFieldEdit->put_Type(esriFieldTypeInteger);
    ipFieldsEdit->AddField(ipField);

    *ppfields = ipFields.Detach();
    return 0;
}

HRESULT XDWGReader::CreateDwgLineFields(ISpatialReference* ipSRef, IFields** ppfields)
{
    IFieldsPtr ipFields;

    ipFields.CreateInstance(CLSID_Fields);
    IFieldsEditPtr ipFieldsEdit(ipFields);
    IFieldPtr ipField;
    ipField.CreateInstance(CLSID_Field);
    IFieldEditPtr ipFieldEdit(ipField);

    // create the geometry field
    IGeometryDefPtr ipGeomDef(CLSID_GeometryDef);
    IGeometryDefEditPtr ipGeomDefEdit(ipGeomDef);

    // assign the geometry definiton properties.
    ipGeomDefEdit->put_GeometryType(esriGeometryPolyline);
    ipGeomDefEdit->put_GridCount(1);
    //double dGridSize = 1000;
    //VARIANT_BOOL bhasXY;
    //ipSRef->HasXYPrecision(&bhasXY);
    //if (bhasXY)
    //{
    //    double xmin, ymin, xmax, ymax, dArea;
    //    ipSRef->GetDomain(&xmin, &xmax, &ymin, &ymax);
    //    dArea = (xmax - xmin) * (ymax - ymin);
    //    dGridSize = sqrt(dArea / 100);
    //}
    //if (dGridSize <= 0)
    //    dGridSize = 1000;
    ipGeomDefEdit->put_GridSize(0, DEFAULT_GIS_GRID_SIZE);
    ipGeomDefEdit->put_AvgNumPoints(2);
    ipGeomDefEdit->put_HasM(VARIANT_FALSE);
    ipGeomDefEdit->put_HasZ(VARIANT_FALSE);
    ipGeomDefEdit->putref_SpatialReference(ipSRef);

    ipFieldEdit->put_Name(CComBSTR(L"SHAPE"));
    ipFieldEdit->put_AliasName(CComBSTR(L"SHAPE"));
    ipFieldEdit->put_Type(esriFieldTypeGeometry);
    ipFieldEdit->putref_GeometryDef(ipGeomDef);
    ipFieldsEdit->AddField(ipField);

    // create the object id field
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"OBJECTID"));
    ipFieldEdit->put_AliasName(CComBSTR(L"OBJECT ID"));
    ipFieldEdit->put_Type(esriFieldTypeOID);
    ipFieldsEdit->AddField(ipField);

    // 创建 Entity ，记录esri实体类型
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(g_szEntityType));
    ipFieldEdit->put_AliasName(CComBSTR(g_szEntityType));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldsEdit->AddField(ipField);
    // 创建 DwgGeometry ，记录DWG实体类型
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"DwgGeometry"));
    ipFieldEdit->put_AliasName(CComBSTR(L"DwgGeometry"));
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
    // 创建 Color ，记录DWG线实体颜色
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Color"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Color"));
    ipFieldEdit->put_Type(esriFieldTypeInteger);
    ipFieldsEdit->AddField(ipField);
    // 创建 Linetype ，记录DWG线实体线型名
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Linetype"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Linetype"));
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
    // 创建 Thickness ，记录DWG实体厚度
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Thickness"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Thickness"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);
    // 创建 Width ，记录DWG实体线宽
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Width"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Width"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);
    // 创建 Blockname ，记录Block名字
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Blockname"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Blockname"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);
    // 创建 Blocknumber ，记录每个Block编号
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Blocknumber"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Blocknumber"));
    ipFieldEdit->put_Type(esriFieldTypeInteger);
    ipFieldsEdit->AddField(ipField);

    // 创建 Visible ，记录DWG实体是否可见，0不可见，1可见
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Visible"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Visible"));
    ipFieldEdit->put_Type(esriFieldTypeInteger);
    ipFieldsEdit->AddField(ipField);

    *ppfields = ipFields.Detach();
    return 0;
}

HRESULT XDWGReader::CreateDwgPolygonFields(ISpatialReference* ipSRef, IFields** ppfields)
{
    IFieldsPtr ipFields;

    ipFields.CreateInstance(CLSID_Fields);
    IFieldsEditPtr ipFieldsEdit(ipFields);
    IFieldPtr ipField;
    ipField.CreateInstance(CLSID_Field);
    IFieldEditPtr ipFieldEdit(ipField);

    ipFieldEdit->put_Name(CComBSTR(L"SHAPE"));
    ipFieldEdit->put_Type(esriFieldTypeGeometry);
    // create the geometry field
    IGeometryDefPtr ipGeomDef(CLSID_GeometryDef);
    IGeometryDefEditPtr ipGeomDefEdit;
    ipGeomDefEdit = ipGeomDef;
    // assign the geometry definiton properties.
    ipGeomDefEdit->put_GeometryType(esriGeometryPolygon);
    ipGeomDefEdit->put_GridCount(1);

    ipGeomDefEdit->put_AvgNumPoints(2);
    ipGeomDefEdit->put_HasM(VARIANT_FALSE);
    ipGeomDefEdit->put_HasZ(VARIANT_FALSE);
    //double dGridSize = 1000;
    //VARIANT_BOOL bhasXY;
    //ipSRef->HasXYPrecision(&bhasXY);
    //if (bhasXY)
    //{
    //    double xmin, ymin, xmax, ymax, dArea;
    //    ipSRef->GetDomain(&xmin, &xmax, &ymin, &ymax);
    //    dArea = (xmax - xmin) * (ymax - ymin);
    //    dGridSize = sqrt(dArea / 100);
    //}
    //if (dGridSize <= 0)
    //    dGridSize = 1000;
    ipGeomDefEdit->put_GridSize(0, DEFAULT_GIS_GRID_SIZE);
    ipGeomDefEdit->putref_SpatialReference(ipSRef);
    ipFieldEdit->putref_GeometryDef(ipGeomDef);
    ipFieldsEdit->AddField(ipField);

    // create the object id field
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"OBJECTID"));
    ipFieldEdit->put_AliasName(CComBSTR(L"OBJECT ID"));
    ipFieldEdit->put_Type(esriFieldTypeOID);
    ipFieldsEdit->AddField(ipField);

    // 创建 Entity ，记录esri实体类型
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(g_szEntityType));
    ipFieldEdit->put_AliasName(CComBSTR(g_szEntityType));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldsEdit->AddField(ipField);
    // 创建 DwgGeometry ，记录DWG实体类型
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"DwgGeometry"));
    ipFieldEdit->put_AliasName(CComBSTR(L"DwgGeometry"));
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
    // 创建 Color ，记录DWG线实体颜色
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Color"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Color"));
    ipFieldEdit->put_Type(esriFieldTypeInteger);
    ipFieldsEdit->AddField(ipField);
    // 创建 Linetype ，记录DWG线实体线型名
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Linetype"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Linetype"));
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
    // 创建 Thickness ，记录DWG实体厚度
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Thickness"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Thickness"));
    //ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);
    // 创建 Width ，记录DWG实体线宽
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Width"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Width"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);
    // 创建 Blockname ，记录Block名字
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Blockname"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Blockname"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);
    // 创建 Blocknumber ，记录每个Block编号
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Blocknumber"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Blocknumber"));
    ipFieldEdit->put_Type(esriFieldTypeInteger);
    ipFieldsEdit->AddField(ipField);

    // 创建 Visible ，记录DWG实体是否可见，0不可见，1可见
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Visible"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Visible"));
    ipFieldEdit->put_Type(esriFieldTypeInteger);
    ipFieldsEdit->AddField(ipField);

    *ppfields = ipFields.Detach();
    return 0;
}

HRESULT XDWGReader::CreateDwgTextPointFields(ISpatialReference* ipSRef, IFields** ppfields)
{
    IFieldsPtr ipFields;

    ipFields.CreateInstance(CLSID_Fields);
    IFieldsEditPtr ipFieldsEdit(ipFields);
    IFieldPtr ipField;
    ipField.CreateInstance(CLSID_Field);
    IFieldEditPtr ipFieldEdit(ipField);

    // create the geometry field
    IGeometryDefPtr ipGeomDef(CLSID_GeometryDef);
    IGeometryDefEditPtr ipGeomDefEdit(ipGeomDef);

    // assign the geometry definiton properties.
    ipGeomDefEdit->put_GeometryType(esriGeometryPoint);
    ipGeomDefEdit->put_GridCount(1);
    //double dGridSize = 1000;
    //VARIANT_BOOL bhasXY;
    //ipSRef->HasXYPrecision(&bhasXY);
    //if (bhasXY)
    //{
    //    double xmin, ymin, xmax, ymax, dArea;
    //    ipSRef->GetDomain(&xmin, &xmax, &ymin, &ymax);
    //    dArea = (xmax - xmin) * (ymax - ymin);
    //    dGridSize = sqrt(dArea / 100);
    //}
    //if (dGridSize <= 0)
    //    dGridSize = 1000;
    ipGeomDefEdit->put_GridSize(0, DEFAULT_GIS_GRID_SIZE);
    ipGeomDefEdit->put_AvgNumPoints(2);
    ipGeomDefEdit->put_HasM(VARIANT_FALSE);
    ipGeomDefEdit->put_HasZ(VARIANT_FALSE);
    ipGeomDefEdit->putref_SpatialReference(ipSRef);

    ipFieldEdit->put_Name(CComBSTR(L"SHAPE"));
    ipFieldEdit->put_AliasName(CComBSTR(L"SHAPE"));
    ipFieldEdit->put_Type(esriFieldTypeGeometry);
    ipFieldEdit->putref_GeometryDef(ipGeomDef);
    ipFieldsEdit->AddField(ipField);

    // create the object id field
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"OBJECTID"));
    ipFieldEdit->put_AliasName(CComBSTR(L"OBJECT ID"));
    ipFieldEdit->put_Type(esriFieldTypeOID);
    ipFieldsEdit->AddField(ipField);

    // 创建 Entity ，记录esri实体类型
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(g_szEntityType));
    ipFieldEdit->put_AliasName(CComBSTR(g_szEntityType));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldsEdit->AddField(ipField);
    // 创建 DwgGeometry ，记录DWG实体类型
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"DwgGeometry"));
    ipFieldEdit->put_AliasName(CComBSTR(L"DwgGeometry"));
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
    // 创建 Color ，记录DWG线实体颜色
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Color"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Color"));
    ipFieldEdit->put_Type(esriFieldTypeInteger);
    ipFieldsEdit->AddField(ipField);
    // 创建 Linetype ，记录DWG线实体线型名
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Linetype"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Linetype"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);
    // 创建 Thickness ，记录DWG实体厚度
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Thickness"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Thickness"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);
    // 创建 Blockname ，记录Block名字
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Blockname"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Blockname"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);
    // 创建 Blocknumber ，记录每个Block编号
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Blocknumber"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Blocknumber"));
    ipFieldEdit->put_Type(esriFieldTypeInteger);
    ipFieldsEdit->AddField(ipField);
    // 创建 Angle  ，记录DWG文字实体旋转角度
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Angle"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Angle"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);
    // 创建 TextString  ，记录 DWG文字实体字内容
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"TextString"));
    ipFieldEdit->put_AliasName(CComBSTR(L"TextString"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(255);
    ipFieldsEdit->AddField(ipField);
    // 创建 Height  ，记录DWG文字实体字高
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Height"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Height"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);
    // 创建 WidthFactor   ，
    // is an additional scaling applied in the x direction which makes the text either fatter or thinner.
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"WidthFactor"));
    ipFieldEdit->put_AliasName(CComBSTR(L"WidthFactor"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);
    // 创建 Oblique   ，倾斜角度
    // is an obliquing angle to be applied to the text, which causes it to "lean" either to the right or left.
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Oblique"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Oblique"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);
    // 创建 VerticalMode  ，记录DWG文字实体字对齐方式
    // kTextBase  0 		kTextBottom  1 		kTextVertMid 2 		kTextTop  3
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"VtMode"));
    ipFieldEdit->put_AliasName(CComBSTR(L"VtMode"));
    ipFieldEdit->put_Type(esriFieldTypeInteger);
    ipFieldsEdit->AddField(ipField);
    // 创建 HorizontalMode  ，记录DWG文字实体字对齐方式
    //kTextLeft  0 	kTextCenter  1 		kTextRight  2 		kTextAlign  3
    // kTextMid  4 		kTextFit  5
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"HzMode"));
    ipFieldEdit->put_AliasName(CComBSTR(L"HzMode"));
    ipFieldEdit->put_Type(esriFieldTypeInteger);
    ipFieldsEdit->AddField(ipField);
    // 创建 AlignmentPointX
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"AlignPtX"));
    ipFieldEdit->put_AliasName(CComBSTR(L"AlignPtX"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);
    // 创建 AlignmentPointY
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"AlignPtY"));
    ipFieldEdit->put_AliasName(CComBSTR(L"AlignPtY"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);
    // 创建 BoundingPointMinX
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"PtMinX"));
    ipFieldEdit->put_AliasName(CComBSTR(L"PtMinX"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);
    // 创建 BoundingPointMinY
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"PtMinY"));
    ipFieldEdit->put_AliasName(CComBSTR(L"PtMinY"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);
    // 创建 BoundingPointMaxX
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"PtMaxX"));
    ipFieldEdit->put_AliasName(CComBSTR(L"PtMaxX"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);
    // 创建 BoundingPointMaxY
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"PtMaxY"));
    ipFieldEdit->put_AliasName(CComBSTR(L"PtMaxY"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);
    // 创建 BigFontname  ，记录 DWG文字实体字体
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"BigFontname"));
    ipFieldEdit->put_AliasName(CComBSTR(L"BigFontname"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);
    // 创建 ShapeFilename  ，记录 DWG文字实体字体
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"ShapeFilename"));
    ipFieldEdit->put_AliasName(CComBSTR(L"ShapeFilename"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);
    // 创建 ShapeName  ，记录 DWG文字实体字体
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"ShapeName"));
    ipFieldEdit->put_AliasName(CComBSTR(L"ShapeName"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);

    // 创建 Visible ，记录DWG实体是否可见，0不可见，1可见
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Visible"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Visible"));
    ipFieldEdit->put_Type(esriFieldTypeInteger);
    ipFieldsEdit->AddField(ipField);

    *ppfields = ipFields.Detach();
    return 0;
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 创建注记图层字段
//输入参数 :
//返 回 值 :
//修改日志 :
//////////////////////////////////////////////////////////////////////////
HRESULT XDWGReader::CreateDwgAnnotationFields(ISpatialReference* ipSRef, IFields** ppfields)
{
    HRESULT hr;

    IObjectClassDescriptionPtr pOCDesc(CLSID_AnnotationFeatureClassDescription);
    IFieldsPtr pReqFields;
    pOCDesc->get_RequiredFields(&pReqFields);

    //设置空间参考
    if (ipSRef != NULL)
    {
        long numFields;
        pReqFields->get_FieldCount(&numFields);
        for (int i = 0; i < numFields; i++)
        {
            IFieldPtr pField;
            pReqFields->get_Field(i, &pField);

            esriFieldType fldType;
            pField->get_Type(&fldType);
            if (fldType == esriFieldTypeGeometry)
            {
                IFieldEditPtr pEdtField = pField;
                IGeometryDefPtr pGeoDef;
                hr = pEdtField->get_GeometryDef(&pGeoDef);

                IGeometryDefEditPtr pEdtGeoDef = pGeoDef;
                hr = pEdtGeoDef->putref_SpatialReference(ipSRef);

                hr = pEdtField->putref_GeometryDef(pGeoDef);

                break;
            }

        }
    }

    IFieldsEditPtr ipFieldsEdit = pReqFields;

    //创建CAD文件中注记图层字段
    IFieldEditPtr ipFieldEdit;
    IFieldPtr ipField;

    // 创建 Entity ，记录esri实体类型
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR("Entity_Type"));
    ipFieldEdit->put_AliasName(CComBSTR("Entity_Type"));
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

    // 创建 Height ，记录高度
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Height"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Height"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);

    // 创建 TextStyle ，记录文字样式
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"TextStyle"));
    ipFieldEdit->put_AliasName(CComBSTR(L"TextStyle"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);

    // 创建 Oblique ，记录倾角
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Oblique"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Oblique"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);

	// 创建 AlignmentPointX
	ipField.CreateInstance(CLSID_Field);
	ipFieldEdit = ipField;
	ipFieldEdit->put_Name(CComBSTR(L"AlignPtX"));
	ipFieldEdit->put_AliasName(CComBSTR(L"AlignPtX"));
	ipFieldEdit->put_Type(esriFieldTypeDouble);
	ipFieldsEdit->AddField(ipField);
	// 创建 AlignmentPointY
	ipField.CreateInstance(CLSID_Field);
	ipFieldEdit = ipField;
	ipFieldEdit->put_Name(CComBSTR(L"AlignPtY"));
	ipFieldEdit->put_AliasName(CComBSTR(L"AlignPtY"));
	ipFieldEdit->put_Type(esriFieldTypeDouble);
	ipFieldsEdit->AddField(ipField);

    *ppfields = ipFieldsEdit.Detach();
    return 0;
}



/************************************************************************
简要描述 : 创建扩展属性表
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
HRESULT XDWGReader::CreateExtendTable(IFeatureWorkspace* pFeatWorkspace, BSTR bstrName, ITable** pTable)
{
    HRESULT hr;
    if (pFeatWorkspace == NULL)
        return E_FAIL;
    // 只创建：BaseName--图名；Handle--要素ID;XDataName--扩展属性名称;XDataNum--扩展属性编号;XDataValue--扩展说明值
    hr = pFeatWorkspace->OpenTable(bstrName, pTable);
    // 如果打不开table就认为不存在就重建table
    if (*pTable == NULL)
    {
        IFieldsPtr ipFields;
        ipFields.CreateInstance(CLSID_Fields);

        IFieldsEditPtr ipIndexFields;
        ipIndexFields.CreateInstance(CLSID_Fields);

        IFieldsEditPtr ipFieldsEdit = ipFields;
        if (ipFieldsEdit == NULL)
            return E_FAIL;
        // Add a field for the user name
        IFieldEditPtr ipField;
        hr = ipField.CreateInstance(CLSID_Field);
        if (FAILED(hr))
            return hr;
        hr = ipField->put_Name(CComBSTR(L"Handle"));
        if (FAILED(hr))
            return hr;
        hr = ipField->put_Type(esriFieldTypeString);
        if (FAILED(hr))
            return hr;
        hr = ipField->put_Length(150);
        if (FAILED(hr))
            return hr;

        hr = ipField->put_Required(VARIANT_TRUE);
        if (FAILED(hr))
            return hr;

        hr = ipFieldsEdit->AddField(ipField);
        if (FAILED(hr))
            return hr;

        //添加索引字段1
        hr = ipIndexFields->AddField(ipField);
        if (FAILED(hr))
            return hr;

        hr = ipField.CreateInstance(CLSID_Field);
        if (FAILED(hr))
            return hr;
        hr = ipField->put_Name(CComBSTR(L"BaseName"));
        if (FAILED(hr))
            return hr;
        hr = ipField->put_Type(esriFieldTypeString);
        if (FAILED(hr))
            return hr;
        hr = ipField->put_Length(250);
        if (FAILED(hr))
            return hr;

        hr = ipField->put_Required(VARIANT_TRUE);
        if (FAILED(hr))
            return hr;

        hr = ipFieldsEdit->AddField(ipField);
        if (FAILED(hr))
            return hr;

        //添加索引字段2
        hr = ipIndexFields->AddField(ipField);
        if (FAILED(hr))
            return hr;

        hr = ipField.CreateInstance(CLSID_Field);
        if (FAILED(hr))
            return hr;
        hr = ipField->put_Name(CComBSTR(L"XDataName"));
        if (FAILED(hr))
            return hr;
        hr = ipField->put_Type(esriFieldTypeString);
        if (FAILED(hr))
            return hr;
        hr = ipField->put_Length(250);
        if (FAILED(hr))
            return hr;
        hr = ipFieldsEdit->AddField(ipField);
        if (FAILED(hr))
            return hr;

        // 2050 为最大容量
        hr = ipField.CreateInstance(CLSID_Field);
        if (FAILED(hr))
            return hr;
        hr = ipField->put_Name(CComBSTR(L"XDataValue"));
        if (FAILED(hr))
            return hr;
        hr = ipField->put_Type(esriFieldTypeString);
        if (FAILED(hr))
            return hr;
        hr = ipField->put_Length(65535);
        if (FAILED(hr))
            return hr;
        hr = ipFieldsEdit->AddField(ipField);
        if (FAILED(hr))
            return hr;
        // Try to Create the table
        hr = pFeatWorkspace->CreateTable(bstrName, ipFields, NULL, NULL, NULL, pTable);
        if (FAILED(hr))
            return hr;

        IIndexEditPtr ipIndexEdit;
        ipIndexEdit.CreateInstance(CLSID_Index);
        ipIndexEdit->putref_Fields(ipIndexFields);
        hr = (*pTable)->AddIndex(ipIndexEdit);
        if (FAILED(hr))
            return hr;
    }
    return S_OK;
}

HRESULT XDWGReader::CreateDatasetFeatureClass(IFeatureWorkspace* pFWorkspace, IFeatureDataset* pFDS, IFields* pFields, BSTR bstrName, esriFeatureType featType, IFeatureClass*& ppFeatureClass)
{
    if (!pFDS && !pFWorkspace)
        return S_FALSE;

    BSTR bstrConfigWord = L"";
    IFieldPtr ipField;
    CComBSTR bstrShapeFld;
    esriFieldType fieldType;
    long lNumFields;
    pFields->get_FieldCount(&lNumFields);
    for (int i = 0; i < lNumFields; i++)
    {
        pFields->get_Field(i, &ipField);
        ipField->get_Type(&fieldType);
        if (esriFieldTypeGeometry == fieldType)
        {
            ipField->get_Name(&bstrShapeFld);
            break;
        }
    }
    HRESULT hr;
    if (pFDS)
    {
        hr = pFDS->CreateFeatureClass(bstrName, pFields, 0, 0, featType, bstrShapeFld, 0, &ppFeatureClass);
    }
    else
    {
        // 直接打开FeatureClass,如果不成功就再创建
        hr = pFWorkspace->OpenFeatureClass(bstrName, &ppFeatureClass);
        if (ppFeatureClass == NULL)
            hr = pFWorkspace->CreateFeatureClass(bstrName, pFields, 0, 0, featType, bstrShapeFld, 0, &ppFeatureClass);
    }
    return hr;
}

void XDWGReader::GetGeometryDef(IFeatureClass* pClass, IGeometryDef** pDef)
{
    try
    {
        BSTR shapeName;
        pClass->get_ShapeFieldName(&shapeName);
        IFieldsPtr pFields;
        pClass->get_Fields(&pFields);

        long lGeomIndex;
        pFields->FindField(shapeName, &lGeomIndex);
        IFieldPtr pField;
        pFields->get_Field(lGeomIndex, &pField);
        pField->get_GeometryDef(pDef);
    }
    catch (...)
    {
    }
}

BOOL XDWGReader::IsResetDomain(IFeatureWorkspace* pFWorkspace, CString szFCName)
{
    IWorkspace2Ptr iws2(pFWorkspace);
    VARIANT_BOOL isexist = FALSE;
    if (iws2)
    {
        iws2->get_NameExists(esriDTFeatureClass, CComBSTR(szFCName), &isexist);
    }
    return isexist;
}

void XDWGReader::ResetDomain(IFeatureWorkspace* pFWorkspace, CString szFCName, ISpatialReference* ipSRef)
{
    IGeometryDefPtr ipGeomDef;
    ISpatialReferencePtr ipOldSRef;
    double mOldMinX, mOldMinY, mOldMaxY, mOldMaxX;
    double mMinX, mMinY, mMaxY, mMaxX;
    double mNewMinX, mNewMinY, mNewMaxY, mNewMaxX, dFX, dFY, mNewXYScale ;
    HRESULT hr;

    pFWorkspace->OpenFeatureClass(CComBSTR(szFCName), &m_pFeatClassPolygon);
    GetGeometryDef(m_pFeatClassPolygon, &ipGeomDef);
    pFWorkspace->OpenFeatureClass(CComBSTR(szFCName), &m_pFeatClassPoint);
    GetGeometryDef(m_pFeatClassPoint, &ipGeomDef);
    pFWorkspace->OpenFeatureClass(CComBSTR(szFCName), &m_pFeatClassLine);
    GetGeometryDef(m_pFeatClassLine, &ipGeomDef);
    //pFWorkspace->OpenFeatureClass(CComBSTR(szFCName), &m_pFeatClassText);
    //GetGeometryDef(m_pFeatClassText, &ipGeomDef);

    ipGeomDef->get_SpatialReference(&ipOldSRef);
    ipOldSRef->GetDomain(&mOldMinX, &mOldMaxX, &mOldMinY, &mOldMaxY);
    ipSRef->GetDomain(&mMinX, &mMaxX, &mMinY, &mMaxY);
    if (mMinX < mOldMinX)
        mNewMinX = mMinX;
    else
        mNewMinX = mOldMinX;
    if (mMinY < mOldMinY)
        mNewMinY = mMinY;
    else
        mNewMinY = mOldMinY;
    if (mMaxX > mOldMaxX)
        mNewMaxX = mMaxX;
    else
        mNewMaxX = mOldMaxX;
    if (mMaxY > mOldMaxY)
        mNewMaxY = mMaxY;
    else
        mNewMaxY = mOldMaxY;
    ipOldSRef->SetDomain(mNewMinX, mNewMaxX, mNewMinY, mNewMaxY);
    ipOldSRef->GetFalseOriginAndUnits(&dFX, &dFY, &mNewXYScale);
    ipOldSRef->GetDomain(&mNewMinX, &mNewMaxX, &mNewMinY, &mNewMaxY);
    IGeometryDefEditPtr ipGeomDefEdit(ipGeomDef);
    hr = ipGeomDefEdit->putref_SpatialReference(ipOldSRef);
    if (FAILED(hr))
    {
        WriteLog(CatchErrorInfo());
    }
}

// bspline算法
/*********************************************************************

参考:
  n 		 -  控制点数 - 1
  t 		 - the polynomial等级 + 1
  control    - 控制点坐标集
  output	 - 输出拟合点坐标集
  num_output - 输出点数

条件:
  n+2>t  (否则无曲线)
  控制点坐标集和点数一致
  分配输出点集点数和 num_output一致


**********************************************************************/

void XDWGReader::Bspline(int n, int t, DwgPoint* control, DwgPoint* output, int num_output)
{
    int* u;
    double increment, interval;
    DwgPoint calcxyz;
    int output_index;

    u = new int[n + t + 1];
    ComputeIntervals(u, n, t);

    increment = (double) (n - t + 2) / (num_output - 1);  // how much parameter goes up each time
    interval = 0;

    for (output_index = 0; output_index < num_output - 1; output_index++)
    {
        ComputePoint(u, n, t, interval, control, &calcxyz);
        output[output_index].x = calcxyz.x;
        output[output_index].y = calcxyz.y;
        output[output_index].z = calcxyz.z;
        interval = interval + increment;  // increment our parameter
    }
    output[num_output - 1].x = control[n].x;   // put in the last DwgPoint
    output[num_output - 1].y = control[n].y;
    output[num_output - 1].z = control[n].z;

    delete u;
}

double XDWGReader::Blend(int k, int t, int* u, double v)  // calculate the blending value
{
    double value;

    if (t == 1)			// base case for the recursion
    {
        if ((u[k] <= v) && (v < u[k + 1]))
            value = 1;
        else
            value = 0;
    }
    else
    {
        if ((u[k + t - 1] == u[k]) && (u[k + t] == u[k + 1]))  // check for divide by zero
        {
            value = 0;
        }
        else if (u[k + t - 1] == u[k]) // if a term's denominator is zero,use just the other
        {
            value = (u[k + t] - v) / (u[k + t] - u[k + 1]) * Blend(k + 1, t - 1, u, v);
        }
        else if (u[k + t] == u[k + 1])
        {
            value = (v - u[k]) / (u[k + t - 1] - u[k]) * Blend(k, t - 1, u, v);
        }
        else
        {
            value = (v - u[k]) / (u[k + t - 1] - u[k]) * Blend(k, t - 1, u, v) + (u[k + t] - v) / (u[k + t] - u[k + 1]) * Blend(k + 1, t - 1, u, v);
        }
    }
    return value;
}


void XDWGReader::ComputeIntervals(int* u, int n, int t)   // figure out the knots
{
    int j;

    for (j = 0; j <= n + t; j++)
    {
        if (j < t)
            u[j] = 0;
        else if ((t <= j) && (j <= n))
            u[j] = j - t + 1;
        else if (j > n)
            u[j] = n - t + 2;  // if n-t=-2 then we're screwed, everything goes to 0

    }
}

void XDWGReader::ComputePoint(int* u, int n, int t, double v, DwgPoint* control, DwgPoint* output)
{
    int k;
    double temp;

    // initialize the variables that will hold our outputted DwgPoint
    output->x = 0;
    output->y = 0;
    output->z = 0;

    for (k = 0; k <= n; k++)
    {
        temp = Blend(k, t, u, v);  // same blend is used for each dimension coordinate
        output->x = output->x + (control[k]).x * temp;
        output->y = output->y + (control[k]).y * temp;
        output->z = output->z + (control[k]).z * temp;
    }
}

/************************************************************************
简要描述 : 添加扩展属性字段
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
void XDWGReader::AddExtraFields(CStringList* pRegapps)
{
    if (pRegapps == NULL) return;
    if (m_IsJoinXDataAttrs == FALSE || pRegapps->GetCount() <= 0)
    {
        return;
    }

    m_Regapps.AddTail(pRegapps);

}


/************************************************************************
简要描述 : 初始化编码对照表
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
//void XDWGReader::InitCompareCodes(ITable* pCompareTable)
//{
//if (pCompareTable==NULL) return;

//   CleanCompareCodes();

//   //IFeatureWorkspacePtr ipFeatureWorkspace = API_GetSysWorkspace();
//   //if (ipFeatureWorkspace == NULL)
//   //{
//   //    AfxMessageBox("打开系统表出错！", MB_ICONERROR);
//   //    return;
//   //}

//   //ITablePtr pCompareTable;
//   //ipFeatureWorkspace->OpenTable(CComBSTR("CAD2GDB"), &pCompareTable);
//   //if (pCompareTable == NULL)
//   //{
//   //    AfxMessageBox("编码对照表不存在，无法进行编码对照。", MB_ICONERROR);
//   //    return;
//   //}

//   CComBSTR bsStr;

//   IEsriCursorPtr ipCursor;
//   pCompareTable->Search(NULL, VARIANT_FALSE, &ipCursor);
//   if (ipCursor != NULL)
//   {
//       long lFieldIndex = -1;
//       IEsriRowPtr ipRow;
//       IFieldsPtr pFields = NULL;

//       ipCursor->NextRow(&ipRow);
//       while (ipRow != NULL)
//       {
//           CComVariant vt;
//           XDwg2GdbRecord* pTbRow = new XDwg2GdbRecord();

//           lFieldIndex = -1;

//           ipRow->get_Fields(&pFields);

//           bsStr = "DWG_LAYER";
//           pFields->FindField(bsStr, &lFieldIndex);
//           if (lFieldIndex != -1)
//           {
//               ipRow->get_Value(lFieldIndex, &vt);
//               if (vt.vt != VT_EMPTY && vt.vt != VT_NULL)
//               {
//                   pTbRow->DWG_LAYER = (CString) vt.bstrVal;
//               }
//           }

//           bsStr = "DWG_BLOCKNAME";
//           pFields->FindField(bsStr, &lFieldIndex);
//           if (lFieldIndex != -1)
//           {
//               ipRow->get_Value(lFieldIndex, &vt);
//               if (vt.vt != VT_EMPTY && vt.vt != VT_NULL)
//               {
//                   pTbRow->DWG_BLOCKNAME = (CString) vt.bstrVal;
//               }
//           }

//           bsStr = "GDB_LAYER";
//           pFields->FindField(bsStr, &lFieldIndex);
//           if (lFieldIndex != -1)
//           {
//               ipRow->get_Value(lFieldIndex, &vt);
//               if (vt.vt != VT_EMPTY && vt.vt != VT_NULL)
//               {
//                   pTbRow->GDB_LAYER = (CString) vt.bstrVal;
//               }
//           }

//           bsStr = "YSDM";
//           pFields->FindField(bsStr, &lFieldIndex);
//           if (lFieldIndex != -1)
//           {
//               ipRow->get_Value(lFieldIndex, &vt);
//               if (vt.vt != VT_EMPTY && vt.vt != VT_NULL)
//               {
//                   pTbRow->YSDM = (CString) vt.bstrVal;
//               }
//           }

//           bsStr = "YSMC";
//           pFields->FindField(bsStr, &lFieldIndex);
//           if (lFieldIndex != -1)
//           {
//               ipRow->get_Value(lFieldIndex, &vt);
//               if (vt.vt != VT_EMPTY && vt.vt != VT_NULL)
//               {
//                   pTbRow->YSMC = (CString) vt.bstrVal;
//               }
//           }

//           ipCursor->NextRow(&ipRow);

//           //加入对照值
//           m_aryCodes.Add(pTbRow);
//       }
//   }

//   bsStr.Empty();
//}

/************************************************************************
简要描述 : 从FeatureBuffer中得到给定字段名的值
输入参数 : pFeatureBuffer：源pFeatureBuffer, sFieldName：需要取值的字段名
返 回 值 : 该字段在FeatureBuffer中的值
修改日志 :
************************************************************************/
CString XDWGReader::GetFeatureBufferFieldValue(IFeatureBuffer*& pFeatureBuffer, CString sFieldName)
{
    CComVariant vtFieldValue;
    CString sFieldValue;
    long lIndex;
    IFieldsPtr pFields;

    pFeatureBuffer->get_Fields(&pFields);
    CComBSTR bsStr = sFieldName;
    pFields->FindField(bsStr, &lIndex);
    bsStr.Empty();
    if (lIndex == -1)
    {
        sFieldValue = "";
    }
    else
    {
        pFeatureBuffer->get_Value(lIndex, &vtFieldValue);

        switch (vtFieldValue.vt)
        {
        case VT_EMPTY:
        case VT_NULL:
            sFieldValue = "";
            break;

        case VT_BOOL:
            sFieldValue = vtFieldValue.boolVal == TRUE ? "1" : "0";
            break;

        case VT_UI1:
            sFieldValue.Format("%d", vtFieldValue.bVal);
            break;

        case VT_I2:
            sFieldValue.Format("%d", vtFieldValue.iVal);
            break;

        case VT_I4:
            sFieldValue.Format("%d", vtFieldValue.lVal);
            break;

        case VT_R4:
        {
            long lVal = vtFieldValue.fltVal;
            sFieldValue.Format("%d", lVal);
        }
        break;

        case VT_R8:
        {
            long lVal = vtFieldValue.dblVal;
            sFieldValue.Format("%d", lVal);
        }
        break;

        case VT_BSTR:
            sFieldValue = vtFieldValue.bstrVal;
            break;

        default:
            sFieldValue = "";
            break;
        }
    }
    return sFieldValue;
}

/************************************************************************
简要描述 :
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
//void XDWGReader::PutExtraAttributes(IFeatureBuffer*& pFeatureBuffer, XDwg2GdbRecord* pCode)
//{
//    HRESULT hr;
//    LONG lFieldIndex;
//
//    IFieldsPtr ipFields;
//    pFeatureBuffer->get_Fields(&ipFields);
//
//
//    CComBSTR bsStr;
//    CComVariant vtVal;
//
//    bsStr = "GDB_LAYER";
//    ipFields->FindField(bsStr, &lFieldIndex);
//    if (lFieldIndex != -1)
//    {
//        vtVal = pCode->GDB_LAYER;
//        hr = pFeatureBuffer->put_Value(lFieldIndex, vtVal);
//    }
//
//    bsStr = "YSDM";
//    ipFields->FindField(bsStr, &lFieldIndex);
//    if (lFieldIndex != -1)
//    {
//        vtVal = pCode->YSDM;
//        hr = pFeatureBuffer->put_Value(lFieldIndex, vtVal);
//    }
//
//    bsStr = "YSMC";
//    ipFields->FindField(bsStr, &lFieldIndex);
//    if (lFieldIndex != -1)
//    {
//        vtVal = pCode->YSMC;
//        hr = pFeatureBuffer->put_Value(lFieldIndex, vtVal);
//    }
//
//    //bsStr = "SymbolCode";
//    //ipFields->FindField(bsStr, &lFieldIndex);
//    //if (lFieldIndex != -1)
//    //{
//    //    vtVal = pCode->SymbolCode;
//    //    hr = pFeatureBuffer->put_Value(lFieldIndex, vtVal);
//    //}
//
//    bsStr.Empty();
//}


//supported by feature classes in ArcSDE and feature classes and tables in File Geodatabase. It improves performance of data loading.
HRESULT XDWGReader::BeginLoadOnlyMode(IFeatureClass*& pTargetClass)
{
    //if (pTargetClass == NULL)
    //{
    //    return S_FALSE;
    //}

    //IFeatureClassLoadPtr pClassLoad(pTargetClass);
    //if (pClassLoad)
    //{
    //    ISchemaLockPtr pSchemaLock(pTargetClass);
    //    if (pSchemaLock)
    //    {
    //        if (SUCCEEDED(pSchemaLock->ChangeSchemaLock(esriExclusiveSchemaLock)))
    //        {
    //            VARIANT_BOOL bLoadOnly;
    //            pClassLoad->get_LoadOnlyMode(&bLoadOnly);
    //            if (!bLoadOnly)
    //                return pClassLoad->put_LoadOnlyMode(VARIANT_TRUE);
    //            else
    //                return S_OK;
    //        }
    //    }
    //}

    //return S_FALSE;

    return S_OK;

}

HRESULT XDWGReader::EndLoadOnlyMode(IFeatureClass*& pTargetClass)
{
    //if (pTargetClass == NULL)
    //{
    //    return S_FALSE;
    //}

    //IFeatureClassLoadPtr pClassLoad(pTargetClass);
    //if (pClassLoad)
    //{
    //    ISchemaLockPtr pSchemaLock(pTargetClass);
    //    if (pSchemaLock)
    //    {
    //        if (SUCCEEDED(pSchemaLock->ChangeSchemaLock(esriSharedSchemaLock)))
    //        {
    //            VARIANT_BOOL bLoadOnly;
    //            pClassLoad->get_LoadOnlyMode(&bLoadOnly);
    //            if (bLoadOnly)
    //                return pClassLoad->put_LoadOnlyMode(VARIANT_FALSE);
    //            else
    //                return S_OK;
    //        }
    //    }
    //}

    //return S_FALSE;

    return S_OK;

}


void XDWGReader::ReleaseFeatureBuffer(IFeatureBufferPtr& pFeatureBuffer)
{
    if (pFeatureBuffer == NULL)
    {
        return;
    }
    //释放内存
    IGeometry* pShape;
    HRESULT hr = pFeatureBuffer->get_Shape(&pShape);
    if (SUCCEEDED(hr))
    {
        if (pShape != NULL)
        {
            pShape->SetEmpty();
        }
    }
}

/************************************************************************
简要描述 : 编码对照
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
BOOL XDWGReader::CompareCodes(IFeatureBuffer*& pFeatureBuffer)
{
    return TRUE;

    //try
    //{
    //	if (pFeatureBuffer == NULL)
    //		return FALSE;

    //	int iCompareCodes = m_aryCodes.GetSize();
    //	if (iCompareCodes <= 0)
    //	{
    //		return TRUE;
    //	}

    //	//CString sThickness = GetFeatureBufferFieldValue(pFeatureBuffer, "Thickness");
    //	CString sBlockname = GetFeatureBufferFieldValue(pFeatureBuffer, "Blockname");
    //	CString sLayer = GetFeatureBufferFieldValue(pFeatureBuffer, "Layer");
    //	CString sEntityType = GetFeatureBufferFieldValue(pFeatureBuffer, g_szEntityType);

    //	//点１Blockname->DWG_BLOCKNAME
    //	//点２Layer->DWG_LAYER

    //	//线，Layer->DWG_LAYER

    //	XDwg2GdbRecord* pDwg2GdbRecord = NULL;

    //	IGeometryPtr pGeometry;
    //	pFeatureBuffer->get_Shape(&pGeometry);
    //	if (pGeometry == NULL)
    //	{
    //		return FALSE;
    //	}

    //	esriFeatureType featType;

    //	IFeaturePtr pFeat;
    //	pFeat = pFeatureBuffer;
    //	if (pFeat != NULL)
    //	{
    //		pFeat->get_FeatureType(&featType);
    //	}
    //	else
    //	{
    //		featType = esriFTSimple;
    //	}

    //	//注记图层
    //	if (featType == esriFTAnnotation)
    //	{
    //		if (!sLayer.IsEmpty())
    //		{
    //			for (int i = 0; i < iCompareCodes; i++)
    //			{
    //				pDwg2GdbRecord = m_aryCodes.GetAt(i);

    //				if (pDwg2GdbRecord->DWG_LAYER.CompareNoCase(sLayer) == 0)
    //				{
    //					PutExtraAttributes(pFeatureBuffer, pDwg2GdbRecord);
    //					return TRUE;
    //				}
    //			}
    //		}
    //		return FALSE;
    //	}
    //	else if (featType == esriFTSimple) //一般图层
    //	{
    //		//HRESULT hr;
    //		CComVariant OID;
    //		esriGeometryType shapeType;
    //		pGeometry->get_GeometryType(&shapeType);
    //		if (shapeType == esriGeometryPoint)
    //		{
    //			//点１Blockname->DWG_BLOCKNAME
    //			//点２Layer->DWG_LAYER

    //			if (!sBlockname.IsEmpty())
    //			{
    //				for (int i = 0; i < iCompareCodes; i++)
    //				{
    //					pDwg2GdbRecord = m_aryCodes.GetAt(i);

    //					if (pDwg2GdbRecord->DWG_BLOCKNAME.CompareNoCase(sBlockname) == 0)
    //					{
    //						PutExtraAttributes(pFeatureBuffer, pDwg2GdbRecord);
    //						return TRUE;
    //					}
    //				}
    //			}
    //			else
    //			{
    //				if (!sLayer.IsEmpty())
    //				{
    //					for (int i = 0; i < iCompareCodes; i++)
    //					{
    //						pDwg2GdbRecord = m_aryCodes.GetAt(i);

    //						if (pDwg2GdbRecord->DWG_LAYER.CompareNoCase(sLayer) == 0)
    //						{
    //							PutExtraAttributes(pFeatureBuffer, pDwg2GdbRecord);
    //							return TRUE;
    //						}
    //					}
    //				}
    //			}

    //			return FALSE;
    //		}
    //		else //if(shapeType == esriGeometryPolyline)
    //		{
    //			//线，Layer->DWG_LAYER
    //			if (!sLayer.IsEmpty())
    //			{
    //				for (int i = 0; i < iCompareCodes; i++)
    //				{
    //					pDwg2GdbRecord = m_aryCodes.GetAt(i);

    //					if (pDwg2GdbRecord->DWG_LAYER.CompareNoCase(sLayer) == 0)
    //					{
    //						PutExtraAttributes(pFeatureBuffer, pDwg2GdbRecord);
    //						return TRUE;
    //					}
    //				}
    //			}

    //			return FALSE;
    //		}
    //	}
    //}
    //catch (...)
    //{
    //	CString sError;
    //	sError.Format("编码转换出错。");
    //	WriteLog(sError);
    //	return FALSE;
    //}

    //return FALSE;
}

/************************************************************************
简要描述 : 创建注记类型的要素类
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
IFeatureClass* XDWGReader::CreateAnnoFtCls(IWorkspace* pWS, CString sAnnoName, IFields* pFields)
{
    HRESULT hr;

    IFeatureWorkspaceAnnoPtr PFWSAnno = pWS;

    IGraphicsLayerScalePtr pGLS(CLSID_GraphicsLayerScale);
    pGLS->put_Units(esriMeters);
    pGLS->put_ReferenceScale(m_dAnnoScale);

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

    IUIDPtr pInstCLSID;
    IUIDPtr pExtCLSID;
    CComBSTR bsShapeFieldName;

    pOCDesc->get_InstanceCLSID(&pInstCLSID);
    pOCDesc->get_ClassExtensionCLSID(&pExtCLSID);
    pFDesc->get_ShapeFieldName(&bsShapeFieldName);

    /*IFieldsPtr pReqFields;
    pOCDesc->get_RequiredFields(&pReqFields);

    //设置空间参考
    if (m_pSpRef != NULL)
    {
        long numFields;
        pReqFields->get_FieldCount(&numFields);
        for (int i = 0; i < numFields; i++)
        {
            IFieldPtr pField;
            pReqFields->get_Field(i, &pField);

            esriFieldType fldType;
            pField->get_Type(&fldType);
            if (fldType == esriFieldTypeGeometry)
            {
                IFieldEditPtr pEdtField = pField;
                IGeometryDefPtr pGeoDef;
                hr = pEdtField->get_GeometryDef(&pGeoDef);

                IGeometryDefEditPtr pEdtGeoDef = pGeoDef;
                hr = pEdtGeoDef->putref_SpatialReference(m_pSpRef);

                hr = pEdtField->putref_GeometryDef(pGeoDef);

                break;
            }

        }
    }

    IFieldsEditPtr ipFieldsEdit = pReqFields;

    //创建CAD文件中注记图层字段
    IFieldEditPtr ipFieldEdit;
    IFieldPtr ipField;

    // 创建 Entity ，记录esri实体类型
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR("Entity_Type"));
    ipFieldEdit->put_AliasName(CComBSTR("Entity_Type"));
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

    // 创建 Height ，记录高度
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Height"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Height"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);

    // 创建 TextStyle ，记录文字样式
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"TextStyle"));
    ipFieldEdit->put_AliasName(CComBSTR(L"TextStyle"));
    ipFieldEdit->put_Type(esriFieldTypeString);
    ipFieldEdit->put_Length(150);
    ipFieldsEdit->AddField(ipField);

    // 创建 Oblique ，记录倾角
    ipField.CreateInstance(CLSID_Field);
    ipFieldEdit = ipField;
    ipFieldEdit->put_Name(CComBSTR(L"Oblique"));
    ipFieldEdit->put_AliasName(CComBSTR(L"Oblique"));
    ipFieldEdit->put_Type(esriFieldTypeDouble);
    ipFieldsEdit->AddField(ipField);*/

    IFeatureClass* pAnnoFtCls;
    //' create the new class
    hr = PFWSAnno->CreateAnnotationClass(CComBSTR(sAnnoName), pFields, pInstCLSID, pExtCLSID, bsShapeFieldName, CComBSTR(""), NULL, 0, pAnnoPropsColl, pGLS, pSymbolColl, VARIANT_TRUE, &pAnnoFtCls);

    return pAnnoFtCls;
}


/************************************************************************
简要描述 : 生成注记Element
输入参数 :
返 回 值 :
修改日志 :
************************************************************************/
ITextElement* XDWGReader::MakeTextElementByStyle(CString strText, double dblAngle,
        double dblHeight, double dblX,
        double dblY, double ReferenceScale, esriTextHorizontalAlignment horizAlign, esriTextVerticalAlignment vertAlign)
{

    HRESULT hr;

    ITextElementPtr pTextElement;

    ISimpleTextSymbolPtr pTextSymbol;

    CString strHeight;

    pTextSymbol.CreateInstance(CLSID_TextSymbol);

    //'Set the text symbol font by getting the IFontDisp interface
    pTextSymbol->put_Font(m_pAnnoTextFont);

    double mapUnitsInches;

    IUnitConverterPtr pUnitConverter(CLSID_UnitConverter);

    pUnitConverter->ConvertUnits(dblHeight, esriMeters, esriInches, &mapUnitsInches);

    strHeight.Format("%f", (mapUnitsInches * 72) / ReferenceScale);

    double dSize = atof(strHeight);
    pTextSymbol->put_Size(dSize);

    pTextSymbol->put_HorizontalAlignment(horizAlign);
    pTextSymbol->put_VerticalAlignment(vertAlign);

    pTextElement.CreateInstance(CLSID_TextElement);
    hr = pTextElement->put_ScaleText(VARIANT_TRUE);

    hr = pTextElement->put_Text(CComBSTR(strText));
    hr = pTextElement->put_Symbol(pTextSymbol);

    IElementPtr pElement = pTextElement;
    IPointPtr pPoint(CLSID_Point);
    hr = pPoint->PutCoords(dblX, dblY);
    hr = pElement->put_Geometry(pPoint);

    if (fabs(dblAngle) > 0)
    {
        ITransform2DPtr pTransform2D = pTextElement;
        pTransform2D->Rotate(pPoint, dblAngle);
    }

    return pTextElement.Detach();

}

/********************************************************************
简要描述 : 释放接口指针
输入参数 :
返 回 值 :
修改日志 :
*********************************************************************/
int XDWGReader::ReleasePointer(IUnknown*& pInterface)
{
    int iRst = 0;

    if (pInterface != NULL)
    {
		try
		{
			iRst = pInterface->Release();
			pInterface = NULL;
		}
		catch(...)
		{
		}
    }

    return iRst;
}

// 释放接口对象
void XDWGReader::ReleaseAOs(void)
{
    int iRst = 0;

    iRst = ReleasePointer((IUnknown*&)m_pPointFeatureCursor);
    iRst = ReleasePointer((IUnknown*&)m_pTextFeatureCursor);
    iRst = ReleasePointer((IUnknown*&)m_pLineFeatureCursor);
    iRst = ReleasePointer((IUnknown*&)m_pAnnoFeatureCursor);
    iRst = ReleasePointer((IUnknown*&)m_pPolygonFeatureCursor);
    iRst = ReleasePointer((IUnknown*&)m_pExtentTableRowCursor);

    iRst = ReleasePointer((IUnknown*&)m_pPointFeatureBuffer);
    iRst = ReleasePointer((IUnknown*&)m_pTextFeatureBuffer);
    iRst = ReleasePointer((IUnknown*&)m_pLineFeatureBuffer);
    iRst = ReleasePointer((IUnknown*&)m_pAnnoFeatureBuffer);
    iRst = ReleasePointer((IUnknown*&)m_pPolygonFeatureBuffer);
    iRst = ReleasePointer((IUnknown*&)m_pExtentTableRowBuffer);

    iRst = ReleasePointer((IUnknown*&)m_pSpRef);

    iRst = ReleasePointer((IUnknown*&)m_pFeatClassPoint);
    iRst = ReleasePointer((IUnknown*&)m_pFeatClassText);
    iRst = ReleasePointer((IUnknown*&)m_pFeatClassLine);
    iRst = ReleasePointer((IUnknown*&)m_pFeatClassPolygon);
    iRst = ReleasePointer((IUnknown*&)m_pAnnoFtCls);
    iRst = ReleasePointer((IUnknown*&)m_pExtendTable);

}
/********************************************************************
简要描述 :初始化对象指针
输入参数 :
返 回 值 :
修改日志 :
*********************************************************************/
void XDWGReader::InitAOPointers(void)
{
    m_pPointFeatureCursor = NULL;
    m_pTextFeatureCursor = NULL;
    m_pLineFeatureCursor = NULL;
    m_pAnnoFeatureCursor = NULL;
    m_pPolygonFeatureCursor = NULL;
    m_pExtentTableRowCursor = NULL;

    m_pPointFeatureBuffer = NULL;
    m_pTextFeatureBuffer = NULL;
    m_pLineFeatureBuffer = NULL;
    m_pAnnoFeatureBuffer = NULL;
    m_pPolygonFeatureBuffer = NULL;
    m_pExtentTableRowBuffer = NULL;

    m_pFeatClassPoint = NULL;
    m_pFeatClassLine = NULL;
    m_pFeatClassPolygon = NULL;
    m_pAnnoFtCls = NULL;
    m_pExtendTable = NULL;
    m_pFeatClassText = NULL;
}

