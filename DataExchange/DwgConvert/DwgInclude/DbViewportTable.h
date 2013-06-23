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



#ifndef _ODDBVIEWPORTTABLE_INCLUDED
#define _ODDBVIEWPORTTABLE_INCLUDED

#include "DD_PackPush.h"

#include "DbAbstractViewTable.h"

class OdDbViewportTableRecord;

/** Description:
    This class implements the ViewportTable, which represents Viewport entities in an OdDbDatabase instance.

    Library:
    Db

    Note:
    Do not derive from this class.
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbViewportTable: public OdDbAbstractViewTable
{
public:
  ODDB_DECLARE_MEMBERS(OdDbViewportTable);

  /** Note:
    DWGdirect applications typically will not use this constructor, insofar as 
    this class is a base class.
  */
  OdDbViewportTable();

  /** Description:
    Makes the specified viewport the active viewport of this ViewportTable object.

    Arguments:  
    viewportID (I) Object ID of the active viewport.      
  */
  void SetActiveViewport(
    OdDbObjectId viewportId);

  /** Description:
    Returns the active viewport of this ViewportTable object.
  */
  OdDbObjectId getActiveViewportId() const;

  /** Description:
    Deletes the specified configuration from this ViewportTable object.
    
    Arguments:
    configName (I) Configuration *name* to delete.
  */
  void DeleteConfiguration(
    const OdString& configName);

  OdDbObjectId add(
    OdDbSymbolTableRecord* pRecord);

  OdResult dxfIn(
    OdDbDxfFiler* pFiler);

  void getClassID(
    void** pClsid) const;

  void subClose();
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbTextStyleTable object pointers.
*/
typedef OdSmartPtr<OdDbViewportTable> OdDbViewportTablePtr;

#include "DD_PackPop.h"

#endif // _ODDBVIEWPORTTABLE_INCLUDED


