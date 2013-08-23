// DwgEntity.cpp : CDwgEntity µÄÊµÏÖ

#include "stdafx.h"
#include "DwgEntity.h"


// CDwgEntity



STDMETHODIMP CDwgEntity::get_GeometryType(BSTR* pVal)
{
	*pVal= this->m_GeometryType;

	return S_OK;
}


STDMETHODIMP CDwgEntity::put_GeometryType(BSTR newVal)
{	
	m_GeometryType=newVal;

	return S_OK;
}


STDMETHODIMP CDwgEntity::get_Color(USHORT* pVal)
{
	
	*pVal=this->m_Color;

	return S_OK;
}


STDMETHODIMP CDwgEntity::put_Color(USHORT newVal)
{
	this->m_Color=newVal;

	return S_OK;
}


STDMETHODIMP CDwgEntity::get_Handle(BSTR* pVal)
{
	 
	*pVal=this->m_Handle;

	return S_OK;
}


STDMETHODIMP CDwgEntity::put_Handle(BSTR newVal)
{
	this->m_Handle=newVal;

	return S_OK;	
}


STDMETHODIMP CDwgEntity::get_Shape(IGeometry** pVal)
{
	*pVal=this->m_Shape;

	return S_OK;
}


STDMETHODIMP CDwgEntity::put_Shape(IGeometry* newVal)
{
	this->m_Shape=newVal;

	return S_OK;
}
