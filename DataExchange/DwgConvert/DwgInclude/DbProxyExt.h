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



#ifndef _OD_DB_PROXY_EXT_
#define _OD_DB_PROXY_EXT_

#include "DD_PackPush.h"

#include "RxObject.h"
#include "IdArrays.h"

class OdDbObject;

/** Description:
    Represents either a proxy entity or a proxy object in an OdDbDatabase.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbProxyExt : public OdRxObject
{
protected:
  /** Description:
      Constructor (no arguments).
  */
  OdDbProxyExt();

public:
  ODRX_DECLARE_MEMBERS(OdDbProxyExt);

  /** Description:
      Returns the edit flags for the class associated with this proxy.
  */
  virtual int proxyFlags(const OdDbObject* pProxy) const = 0;

  /** Description:
      Returns the class name of the entity stored in this proxy.
  */
  virtual OdString originalClassName(const OdDbObject* pProxy) const = 0;

  /** Description:
      Returns the DXF name of the entity stored in this proxy.
  */
  virtual OdString originalDxfName(const OdDbObject* pProxy) const = 0;

  /** Description:
      Returns the application description for the class associated with this proxy.
  */
  virtual OdString applicationDescription(const OdDbObject* pProxy) const = 0;

  /** Description:
      Returns the references maintained by this proxy.

      Arguments:
      ids (O) Object ID array of references contained in this proxy.
      types (O) Reference type array, where each entry is associated with the 
      corresponding entry in the ids array.
  */
  virtual void getReferences(const OdDbObject* pProxy, OdTypedIdsArray& ids) const = 0;
};

typedef OdSmartPtr<OdDbProxyExt> OdDbProxyExtPtr;

#include "DD_PackPop.h"

#endif


