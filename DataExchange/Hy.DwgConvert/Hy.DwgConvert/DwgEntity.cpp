// DwgEntity.cpp : CDwgEntity 的实现

#include "stdafx.h"
#include "DwgEntity.h"
#include "DwgEntityDumper.h"
#include <vector>

// CDwgEntity


void CDwgEntity::setInnerEntity(OdDbEntityPtr oddbEntity)
{
	this->m_InnerEntity=oddbEntity;
}

OdDbEntityPtr CDwgEntity::getInnerEntity()
{
	return this->m_InnerEntity;
}

STDMETHODIMP CDwgEntity::get_GeometryType(BSTR* pVal)
{
	CString sEntType = OdDbEntityPtr(this->m_InnerEntity)->isA()->name();		
	*pVal=sEntType.AllocSysString();
	
	return S_OK;
}


STDMETHODIMP CDwgEntity::put_GeometryType(BSTR newVal)
{	
	m_GeometryType=newVal;

	return S_OK;
}


STDMETHODIMP CDwgEntity::get_Color(long* pVal)
{
	
	*pVal=this->m_InnerEntity->color().color();

	return S_OK;
}


STDMETHODIMP CDwgEntity::put_Color(long newVal)
{
	this->m_Color=newVal;

	return S_OK;
}


STDMETHODIMP CDwgEntity::get_Handle(BSTR* pVal)
{
	OdDbHandle hTmp = this->m_InnerEntity->getDbHandle();
    char szEntityHandle[50] = {0};
    hTmp.getIntoAsciiBuffer(szEntityHandle);
	CString strHandle=szEntityHandle;
	*pVal=strHandle.AllocSysString();

	return S_OK;
}


STDMETHODIMP CDwgEntity::put_Handle(BSTR newVal)
{
	this->m_Handle=newVal;

	return S_OK;	
}


STDMETHODIMP CDwgEntity::get_Shape(IGeometry** pVal)
{
	OdSmartPtr<OdDbEntity_Dumper> pEntDumper = this->m_InnerEntity;
		
	*pVal=pEntDumper->dump(this->m_InnerEntity);

	return S_OK;
}


STDMETHODIMP CDwgEntity::put_Shape(IGeometry* newVal)
{
	this->m_Shape=newVal;

	return S_OK;
}


STDMETHODIMP CDwgEntity::get_Layer(BSTR* pVal)
{
	 CString strLayer=this->m_InnerEntity->layer().c_str();
	*pVal= strLayer.AllocSysString();

	return S_OK;
}


STDMETHODIMP CDwgEntity::put_Layer(BSTR newVal)
{	
	this->m_Layer=newVal;

	return S_OK;
}


STDMETHODIMP CDwgEntity::get_XData(IXData** pVal)
{
	*pVal=this->m_XData;

	return S_OK;
}


STDMETHODIMP CDwgEntity::put_XData(IXData* newVal)
{
	
	this->m_XData= newVal;

	return S_OK;
}


STDMETHODIMP CDwgEntity::GetXData(BSTR appName,VARIANT* xType,VARIANT* xValue) /* SAFEARRAY** xType, SAFEARRAY** xValue)*/
{	 
	OdResBufPtr xIter;
	if(appName)
		xIter=this->m_InnerEntity->xData(OdString(appName));
	else
		xIter=this->m_InnerEntity->xData();
	
	// 循环读取值，记录下来
	OdResBufPtr xIterLoop = xIter;
	CSimpleMap<int,CComVariant> mapExtraRes;
    for (;! xIterLoop.isNull() ; xIterLoop = xIterLoop->next())
    {
        int typeCode = xIterLoop->restype();
		switch(OdDxfCode::_getType(typeCode))
		{
			case OdResBuf::kDxfRegAppName:				
			case OdResBuf::kDxfXdAsciiString:
			case OdResBuf::kDxfXdReal:
			case OdDxfCode::Handle:
			case OdDxfCode::LayerName:
			case OdDxfCode::Name:
			case OdDxfCode::String:
				mapExtraRes.Add(typeCode,CComVariant(ATL::CString(xIterLoop->getString().c_str())));
				break;

			case OdDxfCode::Bool:
				mapExtraRes.Add(typeCode,(CComVariant)(xIterLoop->getBool()));
				break;

			case OdDxfCode::Integer8:
				 mapExtraRes.Add(typeCode,(CComVariant) xIterLoop->getInt8());
				break;

			case OdDxfCode::Integer16:
				mapExtraRes.Add(typeCode,(CComVariant) xIterLoop->getInt16());
				break;

			case OdDxfCode::Integer32:
				mapExtraRes.Add(typeCode,(CComVariant) xIterLoop->getInt32());
				break;

			case OdDxfCode::Double:
			case OdDxfCode::Angle:
				{
					double pDouble=xIterLoop->getDouble();
					mapExtraRes.Add(typeCode,(CComVariant)pDouble);
				}
				break;


			case OdDxfCode::BinaryChunk:
				{
					OdBinaryData bData=xIterLoop->getBinaryChunk();
					mapExtraRes.Add(typeCode,(CComVariant)&bData);
				}
				break;

			case OdDxfCode::ObjectId:
			case OdDxfCode::SoftPointerId:
			case OdDxfCode::HardPointerId:
			case OdDxfCode::SoftOwnershipId:
			case OdDxfCode::HardOwnershipId:
				{
					OdString strID = xIterLoop->getHandle().ascii();
					mapExtraRes.Add(typeCode,(CComVariant) strID);
				}
				break;
				
			case OdDxfCode::Point:
				{
					OdGePoint3d pPoint=xIterLoop->getPoint3d();
					mapExtraRes.Add(typeCode,(CComVariant) &pPoint);
				}
				break;
			//case OdDxfCode::Unknown:
			default:
				mapExtraRes.Add(typeCode,NULL);
				break;
		}
	}
	
	//// 返回Com数据
	int xCount=mapExtraRes.GetSize();
	VARIANT* vType=new VARIANT[xCount];
	VARIANT* vValue=new  VARIANT[xCount];


	//std::vector<VARIANT> vResult;//=new std::vector<VARIANT>(xCount);
	
	for(int i=0;i<xCount;i++)
	{
		vType[i]=(CComVariant)mapExtraRes.GetKeyAt(i);
		vValue[i]=(CComVariant)mapExtraRes.GetValueAt(i);
		//vResult.push_back((CComVariant)mapExtraRes.GetValueAt(i));
	}
	int xx[10];
	xx[0]=123;
	xx[1]=10;
	xx[2]=100;
	int *yy=xx;
	*xType=*(VARIANT*)(void*)vType;
	*xValue=*(VARIANT*)(void*)xx;

	/*void* pResult=&vResult;
	*xValue=*(VARIANT*)pResult;*/
	
	//SAFEARRAYBOUND rgsabound[1];
	//rgsabound[0].cElements = xCount;
	//rgsabound[0].lLbound = 0;

	//*xType=::SafeArrayCreate(VT_INT,1,rgsabound);
	//SAFEARRAY* xValue= ::SafeArrayCreate(VT_VARIANT,1,rgsabound);
	for(long i=0;i<xCount;i++)
	{
		//::SafeArrayPutElement(*xType,&i,(void*)&mapExtraRes.GetKeyAt(i));
		//::SafeArrayPutElement(xValue,&i,(void*)&(mapExtraRes.GetValueAt(i)));
	}




	return S_OK;
}
