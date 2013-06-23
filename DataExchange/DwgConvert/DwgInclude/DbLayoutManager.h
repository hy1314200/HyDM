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



#ifndef _ODDBLAYOUTMANAGER_INCLUDED_
#define _ODDBLAYOUTMANAGER_INCLUDED_

#include "DD_PackPush.h"

class OdDbLayout;
class OdDbObjectId;

#include "RxObject.h"

/** Description:

    {group:OdDb_Classes}
*/
class OdDbLayoutManagerReactor : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdDbLayoutManagerReactor);
  
  virtual void layoutCreated(const char* newLayoutName, const OdDbObjectId& layoutId);
  virtual void layoutToBeRemoved(const char* layoutName, const OdDbObjectId& layoutId);
  virtual void layoutRemoved(const char* layoutName, const OdDbObjectId& layoutId);
  virtual void abortLayoutRemoved(const char* layoutName, const OdDbObjectId& layoutId);
  virtual void layoutToBeCopied(const char* layoutName, const OdDbObjectId& oldLayoutId);
  virtual void layoutCopied(const char* oldLayoutName, const OdDbObjectId& oldLayoutId,
    const char* newLayoutname, const OdDbObjectId& newLayoutId);
  virtual void abortLayoutCopied(const char* layoutName, const OdDbObjectId& layoutId);
  virtual void layoutToBeRenamed(const char* oldName,
    const char* newName, const OdDbObjectId& layoutId);
  virtual void layoutRenamed(const char* oldName,
    const char* newName, const OdDbObjectId& layoutId);
  virtual void abortLayoutRename(const char* oldName,
    const char* newName, const OdDbObjectId& layoutId);
  virtual void layoutSwitched(const char* newLayoutname, const OdDbObjectId& newLayoutId);
  virtual void plotStyleTableChanged(const char* newTableName, const OdDbObjectId& layoutId);
};

typedef OdSmartPtr<OdDbLayoutManagerReactor> OdDbLayoutManagerReactorPtr;

#include "DD_PackPop.h"

#endif //_ODDBLAYOUTMANAGER_INCLUDED_

