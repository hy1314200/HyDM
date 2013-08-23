// DwgReader.cpp : CDwgReader 的实现

#include "stdafx.h"
#include "DwgReader.h"

#include "ExSystemServices.h"
#include "ExHostAppServices.h"
#include "RxDynamicModule.h"
class DwgReaderServices : public ExSystemServices, public ExHostAppServices
{
protected:
    ODRX_USING_HEAP_OPERATORS(ExSystemServices);
};

OdRxObjectImpl<DwgReaderServices> svcs;

   
HRESULT  CDwgReader::GetEntityCount(LONG* count)
{
	 // Open ModelSpace
	OdDbBlockTableRecordPtr pBlock = this->m_DwgDatabase->getModelSpaceId().safeOpenObject();

    LONG eCount=0;
    for (OdDbObjectIteratorPtr pEntIter = pBlock->newIterator(); !pEntIter->done(); pEntIter->step())
    {
        eCount++;
    }
	*count=eCount;

	return S_OK;
}

STDMETHODIMP CDwgReader::put_FileName(BSTR* DwgFile)
{
	this->m_FileName=*DwgFile;

	return S_OK;
}

STDMETHODIMP CDwgReader::Init(VARIANT_BOOL* succeed)
{
	try
	{
		odInitialize(&svcs);

		CString strFile=this->m_FileName;
		this->m_DwgDatabase = svcs.readFile(strFile , false, false, Oda::kShareDenyReadWrite);
        *succeed=this->m_DwgDatabase.isNull();
	}
	catch(...)
	{
		return S_FALSE;
	}


	return S_OK;
}

STDMETHODIMP CDwgReader::Close(void)
{
	
	this->m_DwgDatabase.release();

	return S_OK;
}



STDMETHODIMP CDwgReader::Read(IDwgEntity* curEntity)
{
	// 获取读取器
	if(this->m_EntityIterator.isNull())
	{ 
		OdDbBlockTableRecordPtr pBlock = this->m_DwgDatabase->getModelSpaceId().safeOpenObject();
		this->m_EntityIterator=pBlock->newIterator();
	}

	// 循环读取
	if(!this->m_EntityIterator->done())
	{
		this->m_EntityIterator->step();

		// 构造对象
		OdDbObjectId pID= this->m_EntityIterator->objectId();
		OdDbEntityPtr pEnt = pID.safeOpenObject();

		IDwgEntity* newEntity=new CDwgEntity();
        
		OdDbHandle hTmp;
        char szEntityHandle[50] = {0};
        hTmp = pEnt->getDbHandle();
        hTmp.getIntoAsciiBuffer(szEntityHandle);
		CString strHandle=szEntityHandle;
		newEntity->put_Handle();
	}

	return S_OK;
}
