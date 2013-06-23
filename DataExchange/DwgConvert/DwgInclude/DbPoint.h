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



#ifndef _OD_DB_POINT_
#define _OD_DB_POINT_

#include "DD_PackPush.h"

#include "DbEntity.h"

/** Description:
    This class represents point (Point) entities in an OdDbDatabase instance.
  
    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbPoint : public OdDbEntity
{
public:
  ODDB_DECLARE_MEMBERS(OdDbPoint);

  OdDbPoint();

  /** Arguments:
    position (I) WCS *position* for this Point entity (DXF 10).
  */
  OdDbPoint(
    const OdGePoint3d& position);

  /** Description:
    Returns the WCS *position* of this entity (DXF 10).
  */
  OdGePoint3d position() const;

  /** Description:
    Sets the WCS *position* of this entity (DXF 10).
    Arguments:
    position (I) WCS *position*.
  */
  void setPosition(
    const OdGePoint3d& position);

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
    normal (I) Normal vector.
  */
  void setNormal(
    const OdGeVector3d& normal);

  /** Description:
    Returns the between the OCS X-axis and the X-axis used for displaying this Point entity (DXF 50).
    
    Remarks:
    Used when PDMODE != 0
  */
  double ecsRotation() const;

  /** Description:
    Sets the between the OCS X-axis and the X-axis used for displaying this Point entity (DXF 50).
    
    Arguments:
    rotation (I) Rotation angle.
    
    Remarks:
    Used when PDMODE != 0

    Note:
    All angles are expressed in radians.    
  */
  void setEcsRotation(
    double rotation);

  virtual bool isPlanar() const { return true; }

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

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);

  virtual bool worldDraw(
    OdGiWorldDraw* pWd) const;

  virtual void viewportDraw(
    OdGiViewportDraw*) const;

  virtual void getClassID(
    void** pClsid) const;
};
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbPoint object pointers.
*/
typedef OdSmartPtr<OdDbPoint> OdDbPointPtr;

#include "DD_PackPop.h"

#endif


