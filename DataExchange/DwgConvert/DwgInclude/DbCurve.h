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



#ifndef OD_DBCURVE_H
#define OD_DBCURVE_H

#include "DD_PackPush.h"

#include "DbEntity.h"

class OdDbSpline;
class OdDbCurveImpl;

/** 
    Description:
    The class is the base class for all OdDb curves.

    Library: Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbCurve : public OdDbEntity
{
public:
  ODDB_DECLARE_MEMBERS(OdDbCurve);

  OdDbCurve();

  /** 
    Description:
    Returns true if and only if this curve is closed.
  */
  virtual bool isClosed() const;

  /** 
    Description:
    Returns true if and only if this curve is periodic.
  */
  virtual bool isPeriodic() const;

  /** 
    Description:
    Returns the parameter corresponding to the start point of this curve.

    Arguments:
    startParam (O) Receives the start parameter.

    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  virtual OdResult getStartParam(
    double& startParam) const = 0;

  /** 
    Description:
    Returns the parameter corresponding to the end point of this curve.

    Arguments:
    endParam (O) Receives the end parameter.

    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  virtual OdResult getEndParam (
    double& endParam) const = 0;

  /** 
    Description:
    Returns the WCS start point of this curve.

    Arguments:
    startPoint (O) Receives the start point.

    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  virtual OdResult getStartPoint(
    OdGePoint3d& startPoint) const = 0;

  /** 
    Description:
    Returns the WCS end point of this curve.

    Arguments:
    endPoint (O) Receives the end point.

    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  virtual OdResult getEndPoint(
    OdGePoint3d& endPoint) const = 0;

  /**
    Description:
    Returns the WCS point on this curve corresponding to the specified parameter.
    
    Arguments:
    param (I) Parameter specifying point.
    pointOnCurve (O) Receives the point on this curve.
    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */    
  virtual OdResult getPointAtParam(
    double param, 
    OdGePoint3d& pointOnCurve) const = 0;

  /**
    Description:
    Returns the parameter corresponding to the specified WCS point on this curve.

    Arguments:
    param (O) Receives the parameter corresponding to pointOnCurve.
    pointOnCurve (I) Point to be evaluated.
    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */    
  virtual OdResult getParamAtPoint(
    const OdGePoint3d& pointOnCurve, 
    double& param) const = 0;



//#ifdef OD_USE_STUB_FNS
//  /**
//    Description:
//    Returns the distance along this curve from its start point to the point specified by param.
//    
//    Arguments:
//    param (I) Parameter specifying point.
//    dist (O) Receives the distance. 
//
//    Remarks:
//    Returns eOk if successful, or an appropriate error code if not.
//  */
//  virtual OdResult getDistAtParam (
//    double param, 
//    double& dist) const = 0;
//
//  /**
//    Description:
//    Returns the parameter corresponding to the point a specified distance 
//    along this curve from its start point.
//    
//    Arguments:
//    param (O) Receives the parameter.
//    dist (I) Distance along the curve. 
//
//    Remarks:
//    Returns eOk if successful, or an appropriate error code if not.
//  */
//  virtual OdResult getParamAtDist (
//    double dist, 
//    double& param) const;
//
//
//  /**
//    Description:
//    Returns the distance along this curve from its start point to the specified WCS point.
//    
//    Arguments:
//    pointOnCurve (I) Point on the curve.
//    dist (O) Receives the distance. 
//
//    Remarks:
//    Returns eOk if successful, or an appropriate error code if not.
//  */
//  virtual OdResult getDistAtPoint (
//    const OdGePoint3d& pointOnCurve, 
//    double& dist)const;
//
//  /**
//    Description:
//    Returns the WCS point a specified distance along this curve from its start point.
//    
//    Arguments:
//    pointOnCurve (O) Receives the point.
//    dist (I) Distance along the curve. 
//
//    Remarks:
//    Returns eOk if successful, or an appropriate error code if not.
//  */
//  virtual OdResult getPointAtDist (
//    double dist, 
//    OdGePoint3d& pointOnCurve) const;
//  
//  /**
//    Description:
//    Returns the first derivitive of this curve at the specified WCS point.
//    
//    Arguments:
//    param (I) Parameter specifying point.
//    pointOnCurve (I) Point on the curve.
//    firstDeriv (O) Receives the first derivative. 
//
//    Remarks:
//    Returns eOk if successful, or an appropriate error code if not.
//  */
//  virtual OdResult getFirstDeriv (
//    double param,
//    OdGeVector3d& firstDeriv) const;
//
//  virtual OdResult getFirstDeriv (
//    const OdGePoint3d& pointOnCurve,
//    OdGeVector3d& firstDeriv) const;
//
//  /**
//    Description:
//    Returns the second derivitive of this curve at the specified point.
//    
//    Arguments:
//    param (I) Parameter specifying point.
//    pointOnCurve (I) Point on the curve.
//    secondDeriv (O) Receives the first derivative. 
//
//    Remarks:
//    Returns eOk if successful, or an appropriate error code if not.
//  */
//  virtual OdResult getSecondDeriv (
//    double param,
//    OdGeVector3d& secondDeriv) const;
//
//  virtual OdResult getSecondDeriv (
//    const OdGePoint3d& pointOnCurve,
//    OdGeVector3d& secondDeriv) const;
//
//
//
//  /**
//    Description:
//    Returns the point on this curve closest to the given point.
//    
//    Arguments:
//    givenPoint (I) Given point.
//    pointOnCurve (O) Receives the closed point on this curve.
//    normal (I) Extends this curve if and only if true.
// 
//    Remarks:
//    Returns eOk if successful, or an appropriate error code if not.
//    
//    If normal is specified, this curve is projected onto the plane
//    defined by givenPoint and normal, finds the closest point on
//    the projected curve to givenPoint, and projects said closest
//    point back onto the plane of this curve. It is this point
//    that is returned as pointOnCurve. 
//  */
//  virtual OdResult getClosestPointTo(
//    const OdGePoint3d& givenPoint,
//    OdGePoint3d& pointOnCurve, 
//    bool extend = false) const;
//
//  virtual OdResult getClosestPointTo(
//    const OdGePoint3d& givenPoint,
//    const OdGeVector3d& normal,
//    OdGePoint3d& pointOnCurve, 
//    bool extend = false) const;
//  
//
//  /**
//    Description:
//    Returns a pointer to an OdDbSpline approximation of this curve.
//    
//    Arguments:
//    spline (O) Receives a pointer to the OdDbSpline.
//
//    Remarks:
//    Returns eOk if successful, or an appropriate error code if not.
// */
//  virtual OdResult getSpline (
//    OdDbSpline** spline) const;
//
//  /**
//    Description:
//    Extends this curve to the specified WCS point.
//    
//    Arguments:
//    param (I) Parameter specifying point.
//    toPoint (I) Point to which to extend.
//    extendStart (I) True to extend start of curve, false to extend end of curve.
//
//    Remarks:
//    Returns eOk if successful, or an appropriate error code if not.
//  */
//  virtual OdResult extend(
//    double param);
//  
//  virtual OdResult 
//    extend(bool extendStart,
//    const OdGePoint3d& toPoint);
//
//  /**
//    Description:
//    Returns the *area* of this curve.
//
//    Arguments:
//    area (O) Receives the *area*.
//    Remarks:
//    Returns eOk if successful, or an appropriate error code if not.
//  */
//  virtual OdResult getArea(
//    double& area) const;
//  
//#endif

  /*
  virtual OdResult getOrthoProjectedCurve(const OdGePlane&,
    OdDbCurve** projCrv) const;

  virtual OdResult getProjectedCurve(const OdGePlane&,
    const OdGeVector3d& projDir, OdDbCurve** projCrv) const;

  virtual OdResult getOffsetCurves(double offsetDist,  //Replace OdRxObjectPtrArray
    OdRxObjectPtrArray& offsetCurves) const;

  virtual OdResult getOffsetCurvesGivenPlaneNormal(
    const OdGeVector3d& normal, double offsetDist,
    OdRxObjectPtrArray& offsetCurves) const;          //Replace OdRxObjectPtrArray

  virtual OdResult getSplitCurves (const OdGeDoubleArray& params, Replace OdRxObjectPtrArray
    OdRxObjectPtrArray& curveSegments) const;

  virtual OdResult getSplitCurves (const OdGePoint3dArray& points, Replace OdRxObjectPtrArray
    OdRxObjectPtrArray& curveSegments) const;

  */
};
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbCurve object pointers.
*/
typedef OdSmartPtr<OdDbCurve> OdDbCurvePtr;

#include "DD_PackPop.h"

#endif


