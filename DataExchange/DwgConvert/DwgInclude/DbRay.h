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



#ifndef OD_DBRAY_H
#define OD_DBRAY_H

#include "DD_PackPush.h"

#include "DbCurve.h"

class OdGePoint3d;
class OdGeVector3d;

/** Description:
    This class represents Ray entities in an OdDbDatabase instance.

    Library:
    Db

    Remarks:
    A Ray is a semi-infinite line.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbRay : public OdDbCurve
{
public:
  ODDB_DECLARE_MEMBERS(OdDbRay);

  OdDbRay();
  
/*
  void getOffsetCurvesGivenPlaneNormal(
  const OdGeVector3d& normal, double offsetDist,
  OdRxObjectPtrArray& offsetCurves) const; //Replace OdRxObjectPtrArray
*/
  
  /** Description:
    Returns the WCS base point of this curve (DXF 10).
  */
  OdGePoint3d basePoint() const;

  /** Description:
    Sets the WCS base point of this curve (DXF 10).
    Arguments:
    basePoint (I) Base point.
  */
  void setBasePoint(
    const OdGePoint3d& basePoint); 
  
  /** Description:
    Returns the WCS unit direction vector of this curve (DXF 11).
  */
  OdGeVector3d unitDir() const;

  /** Description:
    Sets the WCS unit direction vector of this curve (DXF 11).
    Arguments:
    unitDir (I) Unit direction vector.
  */
  void setUnitDir(
    const OdGeVector3d& unitDir);

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;
  
  virtual void getClassID(
    void** pClsid) const;

  bool worldDraw(
    OdGiWorldDraw* pWd) const;

  /**
    Note:
    Always returns eInvalidExtents.
  */
  OdResult getGeomExtents(
    OdGeExtents3d& extents) const;

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

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);
};
/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbRay object pointers.
*/
typedef OdSmartPtr<OdDbRay> OdDbRayPtr;

#include "DD_PackPop.h"

#endif


