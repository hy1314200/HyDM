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



#ifndef   _ODEDITORIMPL_H_INCLUDED_
#define   _ODEDITORIMPL_H_INCLUDED_

#include "DD_PackPush.h"

#include "Editor.h"

/** Description:

    {group:OdRx_Classes} 
*/

#define RXEVENT_FIRE(method, inparams, params) \
  inline void fire_##method inparams\
{\
  for(unsigned i = 0; i < m_reactors.size(); ++i)\
{\
  m_reactors[i]->method params;\
}\
}

/** Description:

    {group:OdRx_Classes} 
*/
class OdRxEventImpl : public OdRxEvent
{
  OdArray<OdRxEventReactorPtr> m_reactors;
public:
  OdRxEventImpl() {}
  ODRX_DECLARE_MEMBERS(OdRxEventImpl);
  
  void addReactor(OdRxEventReactor* pReactor);
  void removeReactor(OdRxEventReactor* pReactor);

  // notifiers
  RXEVENT_FIRE(dwgFileOpened, (OdDbDatabase* db, const OdChar* fileName),(db, fileName))
  RXEVENT_FIRE(initialDwgFileOpenComplete, (OdDbDatabase* db), (db))
  RXEVENT_FIRE(databaseConstructed, (OdDbDatabase* db), (db))
  RXEVENT_FIRE(databaseToBeDestroyed, (OdDbDatabase* db), (db))
  RXEVENT_FIRE(beginSave, (OdDbDatabase* db, const OdChar* pIntendedName), (db, pIntendedName))
  RXEVENT_FIRE(saveComplete, (OdDbDatabase* db, const OdChar* pActualName), (db, pActualName))
  RXEVENT_FIRE(abortSave, (OdDbDatabase* db), (db))
  // DXF In/Out Events.
  RXEVENT_FIRE(beginDxfIn, (OdDbDatabase* db), (db))
  RXEVENT_FIRE(abortDxfIn, (OdDbDatabase* db), (db))
  RXEVENT_FIRE(dxfInComplete, (OdDbDatabase* db), (db))
  //
  RXEVENT_FIRE(beginDxfOut, (OdDbDatabase* db), (db))
  RXEVENT_FIRE(abortDxfOut, (OdDbDatabase* db), (db))
  RXEVENT_FIRE(dxfOutComplete, (OdDbDatabase* db), (db))
  // Insert Events.
  RXEVENT_FIRE(beginInsert, (OdDbDatabase* pTo, const OdChar* pBlockName, OdDbDatabase* pFrom), (pTo, pBlockName, pFrom))
  RXEVENT_FIRE(beginInsert, (OdDbDatabase* pTo, const OdGeMatrix3d& xform, OdDbDatabase* pFrom), (pTo, xform, pFrom))
  RXEVENT_FIRE(otherInsert, (OdDbDatabase* pTo, OdDbIdMapping& idMap, OdDbDatabase* pFrom), (pTo, idMap, pFrom))
  RXEVENT_FIRE(abortInsert, (OdDbDatabase* pTo), (pTo))
  RXEVENT_FIRE(endInsert, (OdDbDatabase* pTo), (pTo))
  
  // Wblock Events.
  RXEVENT_FIRE(wblockNotice, (OdDbDatabase* pDb), (pDb))
  RXEVENT_FIRE(beginWblock, (OdDbDatabase* pTo, OdDbDatabase* pFrom, const OdGePoint3d& insertionPoint), (pTo, pFrom, insertionPoint))
  RXEVENT_FIRE(beginWblock, (OdDbDatabase* pTo, OdDbDatabase* pFrom, OdDbObjectId blockId), (pTo, pFrom, blockId))
  RXEVENT_FIRE(beginWblock, (OdDbDatabase* pTo, OdDbDatabase* pFrom), (pTo, pFrom))
  RXEVENT_FIRE(otherWblock, (OdDbDatabase* pTo, OdDbIdMapping& m, OdDbDatabase* pFrom), (pTo, m, pFrom))
  RXEVENT_FIRE(abortWblock, (OdDbDatabase* pTo), (pTo))
  RXEVENT_FIRE(endWblock, (OdDbDatabase* pTo), (pTo))
  RXEVENT_FIRE(beginWblockObjects, (OdDbDatabase* pDb, OdDbIdMapping& m), (pDb, m))
  
  // Deep Clone Events.
  RXEVENT_FIRE(beginDeepClone, (OdDbDatabase* pTo, OdDbIdMapping& m), (pTo, m))
  RXEVENT_FIRE(beginDeepCloneXlation, (OdDbIdMapping& m), (m))
  RXEVENT_FIRE(abortDeepClone, (OdDbIdMapping& m), (m))
  RXEVENT_FIRE(endDeepClone, (OdDbIdMapping& m), (m))
  
  // Partial Open Events.
  RXEVENT_FIRE(partialOpenNotice, (OdDbDatabase* pDb), (pDb))
};

typedef OdSmartPtr<OdRxEventImpl> OdRxEventImplPtr;

/** Description:

    {group:Other_Classes}
*/
class OdEditorImpl : public OdRxEventImpl
{
public:
  OdEditorImpl() {}
  ODRX_DECLARE_MEMBERS(OdEditorImpl);
};

#include "DD_PackPop.h"

#endif // _ODEDITORIMPL_H_INCLUDED_


