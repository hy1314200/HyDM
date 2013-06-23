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



#ifndef _ODDBBLOCKBEGIN_INCLUDED_
#define _ODDBBLOCKBEGIN_INCLUDED_

#include "DD_PackPush.h"

#include "DbEntity.h"
class OdDbBlockBeginImpl;

/** Description:
    This class represents BlockBegin entities in an OdDbDatabase.  
    
    Remarks:
    OdDbBlockBegin entities are automatically created and handled by DWGdirect for
    each AcDbBlockTableRecord object.
     
    Client applications may add extended data or extension dictionaries to these 
    entities, but should not create or delete them.
    
    These entities have no associated geometries.
    
    Note:
    Do not derive from this class.
    
    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbBlockBegin : public OdDbEntity
{
public:
  ODDB_DECLARE_MEMBERS(OdDbBlockBegin);

  OdDbBlockBegin();

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult dxfInFields_R12(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields_R12(
    OdDbDxfFiler* pFiler) const;

  /**
    Note:
    Always returns eInvalidExtents.
  */
  virtual OdResult getGeomExtents(
    OdGeExtents3d& extents) const;
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbBlockBegin object pointers.
*/
typedef OdSmartPtr<OdDbBlockBegin> OdDbBlockBeginPtr;

#include "DD_PackPop.h"

#endif 

