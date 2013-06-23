///////////////////////////////////////////////////////////////////////////////
// Copyright © 2002, Open Design Alliance Inc. ("Open Design") 
// 
// This software is owned by Open Design, and may only be incorporated into 
// application programs owned by members of Open Design subject to a signed 
// Membership Agreement and Supplemental Software License Agreement with 
// Open Design. The structure and organization of this Software are the valuable 
// trade secrets of Open Design and its suppliers. The Software is also protected 
// by copyright law and international treaty provisions. You agree not to 
// modify, adapt, translate, reverse engineer, decompile, disassemble or 
// otherwise attempt to discover the source code of the Software. Application 
// programs incorporating this software must include the following statement 
// with their copyright notices:
//
//      DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef _ODDBSAFETRANSACTION_INCLUDED_
#define _ODDBSAFETRANSACTION_INCLUDED_

#include "RxObject.h"
#include "StaticRxObject.h"
#include "DbTransact.h"


/** Description:

    {group:OdDb_Classes}
*/
class OdDbSafeTransaction : private OdStaticRxObject<OdDbTransactionReactor>
{
  int m_nCounter;
  OdDbDatabase* m_pTrackedDB;

  OdDbSafeTransaction& operator = (const OdDbSafeTransaction& ptr);

  void transactionStarted(OdDbDatabase* )  { ++m_nCounter; }
  void transactionEnded(OdDbDatabase* pDb)
  {
    if(!--m_nCounter)
      pDb->removeTransactionReactor(this);
  }
  inline void transactionAborted(OdDbDatabase* pDb)
  {
    if(!--m_nCounter)
      pDb->removeTransactionReactor(this);
  }
public:
  inline OdDbSafeTransaction(OdDbDatabase* pTrackedDB);

	inline ~OdDbSafeTransaction();
};


inline OdDbSafeTransaction::OdDbSafeTransaction(OdDbDatabase* pTrackedDB)
  : m_pTrackedDB(pTrackedDB), m_nCounter(0)
{
  if (m_pTrackedDB)
  {
    m_pTrackedDB->addTransactionReactor(this);
  }
}

inline OdDbSafeTransaction::~OdDbSafeTransaction() 
{ 
  m_pTrackedDB->removeTransactionReactor(this);
  while(m_nCounter--)
    m_pTrackedDB->abortTransaction();
}

#endif // _ODDBSAFETRANSACTION_INCLUDED_


