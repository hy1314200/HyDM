///////////////////////////////////////////////////////////////////////////////
// Copyright © 2002, Open Design Alliance Inc. ("Open Design") 
// 
// This software is owned by Open Design, and may only be incorporated into 
// application programs owned by members of Open Design subject to a signed 
// Membership Agreement and Supplemental Software License Agreement with 
// Open Design. The structure and organization of this Software are the valuable 
// trade secrets of Open Design aand its suppliers. The Software is also protected 
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



#ifndef _OD_DB_LINE_
#define _OD_DB_LINE_

#include "DD_PackPush.h"

#include "DbCurve.h"

/** Description:
    This class represents Line entities in an OdDbDatabase instance.

    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbLine : public OdDbCurve
{
public:
  ODDB_DECLARE_MEMBERS(OdDbLine);

  OdDbLine();

  /** Description:
    Returns the WCS start point of this curve (DXF 10).
  */
  OdGePoint3d startPoint() const;

  /** Description:
    Sets the WCS start point of this curve (DXF 10).
    
    Arguments:
    startPoint (I) Start point.
  */
  void  setStartPoint(
    const OdGePoint3d& startPoint);

  /** Description:
    Returns the WCS end point of this curve (DXF 11).
  */
  OdGePoint3d endPoint() const;

  /** Description:
    Sets the WCS end point of this curve (DXF 11).
    
    Arguments:
    endPoint (I) End point.
  */
  void setEndPoint(
    const OdGePoint3d& endPoint);

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

  virtual bool worldDraw(
    OdGiWorldDraw* pWd) const;

  virtual OdResult getGeomExtents(
    OdGeExtents3d& extents) const;

  virtual void getClassID(
    void** pClsid) const;
  
  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);

  virtual bool isPlanar() const;

  virtual OdResult getPlane(
    OdGePlane& plane, 
    OdDb::Planarity& planarity) const;

  virtual bool isClosed() const;

  virtual bool isPeriodic() const;

  virtual OdResult getStartParam(
    double& startParam) const;

  virtual OdResult getEndParam (
    double& endParam) const;

  virtual OdResult getStartPoint(
    OdGePoint3d& startPoint) const;

  virtual OdResult getEndPoint(
    OdGePoint3d& endPoint) const;

  virtual OdResult getPointAtParam(
    double param, 
    OdGePoint3d& pointOnCurve) const;

  virtual OdResult getParamAtPoint(
    const OdGePoint3d& pointOnCurve, 
    double& param) const;

  /*
     void getOffsetCurvesGivenPlaneNormal(
       const OdGeVector3d& normal, double offsetDist,
       OdRxObjectPtrArray& offsetCurves) const;
  */
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbLine object pointers.
*/
typedef OdSmartPtr<OdDbLine> OdDbLinePtr;

#include "DD_PackPop.h"

#endif


