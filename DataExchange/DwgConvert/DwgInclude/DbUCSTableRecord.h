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



#ifndef _ODDBUCSTABLERECORD_INCLUDED
#define _ODDBUCSTABLERECORD_INCLUDED

#include "DD_PackPush.h"

#include "DbSymbolTableRecord.h"
#include "ViewportDefs.h"

/** Description:
    This class represents UCS records in the OdDbUCSTable in an OdDbDatabase instance.

    Library:
    Db
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbUCSTableRecord: public OdDbSymbolTableRecord
{
public:
  ODDB_DECLARE_MEMBERS(OdDbUCSTableRecord);

  OdDbUCSTableRecord();

  /** Description:
    Returns the WCS *origin* of this UCS (DXF 10).
  */
  OdGePoint3d origin() const;

  /** Description:
    Sets the WCS *origin* of this UCS (DXF 10).
    
    Arguments:
    origin (I) Origin.
  */
  void setOrigin(
    const OdGePoint3d& origin);

  /** Description:
    Returns the WCS X-axis of this UCS (DXF 11).
  */
  OdGeVector3d xAxis() const;

  /** Description:
    Sets the WCS X-axis of this UCS (DXF 11).
    
    Arguments:
    xAxis (I) X-axis.
  */
  void setXAxis(
    const OdGeVector3d& xAxis);

  /** Description:
    Returns the WCS Y-axis of this UCS (DXF 12).
  */
  OdGeVector3d yAxis() const;

  /** Description:
    Sets the WCS Y-axis of this UCS (DXF 12).
    
    Arguments:
    yAxis (I) Y-axis.
  */
  void setYAxis(
    const OdGeVector3d& yAxis);

  /** Description:
    Returns the WCS *origin* (DXF 13) of this UCS for the specified orthographic *view*, 
    when UCSBASE is set to this UCS.
    
    Arguments:
    view (I) Orthographic *view*.
  */
  OdGePoint3d ucsBaseOrigin(OdDb::OrthographicView view) const;

  /** Description:
    Sets the WCS *origin* (DXF 13) of this UCS for the specified orthographic *view* (DXF 71), 
    when UCSBASE is set to this UCS.

    Arguments:
    origin (I) Origin for this *view*.
    view (I) Orthographic *view*.
  */
  void setUcsBaseOrigin(
    const OdGePoint3d& origin, 
    OdDb::OrthographicView view);

  virtual void getClassID(
    void** pClsid) const;

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
};
/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbUCSTableRecord object pointers.
*/
typedef OdSmartPtr<OdDbUCSTableRecord> OdDbUCSTableRecordPtr;

#include "DD_PackPop.h"

#endif // _ODDBUCSTABLERECORD_INCLUDED


