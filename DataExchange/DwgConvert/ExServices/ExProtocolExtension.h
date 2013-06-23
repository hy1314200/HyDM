// ExProtocolExtension.h: interface for the ExProtocolExtension class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_EXPROTOCOLEXTENSION_H__A6B44366_0CE0_408A_BFDF_BAE98BD49250__INCLUDED_)
#define AFX_EXPROTOCOLEXTENSION_H__A6B44366_0CE0_408A_BFDF_BAE98BD49250__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "RxObject.h"
#include "DbEntity.h"

#define STL_USING_IOSTREAM
#include "OdaSTL.h"
#define  STD(a)  std:: a

class OdDbEntity_Dumper : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdDbEntity_Dumper);

  virtual void dump(OdDbEntity* pEnt, STD(ostream) &os) const;
}; // end class OdDbEntity_Dumper

class Dumpers;
  
class ExProtocolExtension  
{
  Dumpers* m_pDumpers;
public:
	ExProtocolExtension();
	virtual ~ExProtocolExtension();
  void initialize();
  void uninitialize();
};

#endif // !defined(AFX_EXPROTOCOLEXTENSION_H__A6B44366_0CE0_408A_BFDF_BAE98BD49250__INCLUDED_)

