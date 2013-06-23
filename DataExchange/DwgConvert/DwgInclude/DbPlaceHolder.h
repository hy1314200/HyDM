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



#ifndef OD_DBPLACEHOLDER_H
#define OD_DBPLACEHOLDER_H

#include "DD_PackPush.h"

#include "DbObject.h"

/** Description:
  This class implements Placeholder objects in an OdDbDatabase.
  
  Library:
  Db
  
  Remarks:
  Placeholder objects are designed to be added to dictionaries,
  providing object IDs that can be linked to dictionary keys. They are used in the
  Plot Style Name dictionary.

  {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbPlaceHolder : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbPlaceHolder);

  OdDbPlaceHolder();
  
  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  /*
  virtual void wblockClone(OdDbIdMapping& idMap) const;
  */
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbPlaceHolder object pointers.
*/
typedef OdSmartPtr<OdDbPlaceHolder> OdDbPlaceHolderPtr;

#include "DD_PackPop.h"

#endif


