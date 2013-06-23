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

#ifndef _OD_RXMODULE_H_
#define _OD_RXMODULE_H_

#include "RxObject.h"

#include "DD_PackPush.h"

/** Description:
    Class that is overridden to create a custom DWGdirect application (DRX module).

    {group:OdRx_Classes} 
*/
class ODRX_ABSTRACT FIRSTDLL_EXPORT OdRxModule : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdRxModule);

  /** Description:
  */
  virtual void* sysData() = 0;

  virtual void deleteModule() = 0;

  /** Description:
      The user override of this function should register any custom objects defined in the 
      custom application, using the OdRxObject::rxInit function.  It should also register
      custom commands defined in the module.
  */
  virtual void initApp() = 0;

  /** Description:
      The user override of this function should unregister any custom objects defined in the
      custom application, using the OdRxObject::rxUninit function.  It should also
      remove any custom commands that were registered in the initApp function.
  */
  virtual void uninitApp() = 0;

  /** Description:
  */
  virtual OdString moduleName() const = 0;
};

typedef OdSmartPtr<OdRxModule> OdRxModulePtr;


#include "DD_PackPop.h"

#endif // _OD_RXMODULE_H_


