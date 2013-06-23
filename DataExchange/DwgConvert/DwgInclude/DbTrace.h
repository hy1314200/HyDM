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



#ifndef _OD_DB_TRACE_
#define _OD_DB_TRACE_

#include "DD_PackPush.h"

#include "DbEntity.h"

/** Description:
    This class represents 2D solid-filled (Trace) entities in an OdDbDatabase instance.
  
    Library:
    Db
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbTrace : public OdDbEntity
{
public:
  ODDB_DECLARE_MEMBERS(OdDbTrace);

  OdDbTrace();

  /* OdDbTrace(const OdGePoint3d& pt0,
       const OdGePoint3d& pt1,
       const OdGePoint3d& pt2,
       const OdGePoint3d& pt3);
  */

  /** Description:
    Returns the specified WCS *point* of this Trace entity (DXF 10, 11, 12, or 13).

    Arguments:
    pointIndex (I) Point index.
    point (O) Receives the specified *point*.    

    Remarks:
    pointIndex has a range of [0..3]    
  */
  void getPointAt(
    int pointIndex, 
    OdGePoint3d& point) const;

  
  /** Description:
    Sets the specified WCS *point* of this Trace entity (DXF 10, 11, 12, or 13).

    Arguments:
    pointIndex (I) Point index.
    point (I) Point.
    
    Remarks:
    pointIndex has a range of [0..3]    
  */
  void setPointAt(
    int pointIndex, 
    const OdGePoint3d& point);

  /** Description:
    Returns the *thickness* of this entity (DXF 39).
    
    Remarks:
    Thickness is the extrusion length along the *normal*.
  */
  double thickness() const;
  
  /** Description:
    Sets the *thickness* of this entity (DXF 39).
    Arguments:
    thickness (I) Thickness.
    Remarks:
    Thickness is the extrusion length along the *normal*.
  */
  void setThickness(
    double thickness);
  
  /** Description:
    Returns the WCS *normal* to the plane of this entity (DXF 210).
  */
  OdGeVector3d normal() const;
  
  /** Description:
    Sets the WCS *normal* to the plane of this entity (DXF 210).
    Arguments:
    normal (I) Normal.
  */
  void setNormal(
    const OdGeVector3d& normal);

  virtual bool isPlanar() const;

  virtual OdResult getPlane(
    OdGePlane& plane, 
    OdDb::Planarity& planarity) const;

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  bool worldDraw(
    OdGiWorldDraw* pWd) const;

  void viewportDraw(
    OdGiViewportDraw* pVd) const;

  virtual OdResult dxfInFields_R12(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields_R12(
    OdDbDxfFiler* pFiler) const;

  void getClassID(
    void** pClsid) const;

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbTrace object pointers.
*/
typedef OdSmartPtr<OdDbTrace> OdDbTracePtr;

#include "DD_PackPop.h"

#endif


