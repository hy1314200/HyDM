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



// RxDLinkerReactor.h: interface for the OdRxDLinkerReactor class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(_ODRXDLINKERREACTOR_H_INCLUDED_)
#define _ODRXDLINKERREACTOR_H_INCLUDED_

#include "DD_PackPush.h"

/** Description:

    {group:OdRx_Classes} 
*/
class FIRSTDLL_EXPORT OdRxDLinkerReactor : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdRxDLinkerReactor);

  virtual void rxAppWillBeLoaded(const OdChar* moduleName);
  virtual void rxAppLoaded(OdRxModule* pModule);
  virtual void rxAppLoadAborted(const OdChar* moduleName);

  virtual void rxAppWillBeUnloaded(OdRxModule* pModule);
  virtual void rxAppUnloaded(const OdChar* moduleName);
  virtual void rxAppUnloadAborted(OdRxModule* pModule);
};

#include "DD_PackPop.h"

#endif // !defined(_ODRXDLINKERREACTOR_H_INCLUDED_)


