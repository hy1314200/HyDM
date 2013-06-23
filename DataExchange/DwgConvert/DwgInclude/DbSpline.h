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



#ifndef ODDB_DBSPLINE_H
#define ODDB_DBSPLINE_H

#include "DD_PackPush.h"

#include "DbCurve.h"

class OdGeKnotVector;

/** Description:
    This class represents Spline entities in an OdDbDatabase instance.

    Library:
    Db
    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDbSpline : public OdDbCurve
{
public:
  ODDB_DECLARE_MEMBERS(OdDbSpline);

  OdDbSpline();
  
  /** Description:
     Returns true if and only if this Spline entity is *rational* (DXF 70, bit 0x04).
  */
  bool isRational() const;
  
  /** Description:
    Returns the *degree* of this Spline entity (DXF 71).
    Remarks:
    degree is in the range [1, 25].  
  */
  int degree() const;
  
  /** Description:
      Increased the degree of this spline to the specified value.
      void elevateDegree(int newDegree);
  */
  
  /** Description:
    Returns the number of control *points* in this Spline entity (DXF 73).
  */
  int numControlPoints() const;
  
  /** Description:
    Returns the specified control *point* of this Spline entity (DXF 10).

    Arguments:
    index (I) Control *point* *index*.
    point (O) Receives the control *point*.
  */
  void getControlPointAt(
    int index, 
    OdGePoint3d& point) const;
  
  /** Description:
      Sets the specified control *point* of this Spline entity (DXF 10).

      Arguments:
      index (I) Control *point* *index*.
      point (I) Control *point*.
  */
  void setControlPointAt(
    int index, 
    const OdGePoint3d& point);
  
  /** Description:
    Returns the number of fit points in this Spline entity (DXF 74).
  */
  int numFitPoints() const;
  
  /** Description:
    Returns the specified fit *point* of this Spline entity (DXF 11).

    Arguments:
    index (I) Fit *point* *index*.
    point (O) Receives the fit *point*.
  */
  OdResult getFitPointAt(
    int index, 
    OdGePoint3d& point) const;
  
  /** Description:
    Sets the specfied fit *point* of this Spline entity (DXF 11).

    Arguments:
    index (I) Fit *point* *index*.
    point (I) Fit *point*.
  */
  void setFitPointAt(
    int index, 
    const OdGePoint3d& point);
  
  /** Description:
    Inserts a fit *point* into this Spline entity after the specified *index*.
      
    Arguments:
    index (I) Fit *point* *index*.
    point (O) Receives the fit *point*.
    
    Remarks:
    If index < 0, point is inserted at before the first fit *point*. 
    
    If index >= numFitPoints(), point is appended to the spline. 
  */
  void insertFitPointAt(
    int index, 
    const OdGePoint3d& point);
  
  /** Description:
    Removes the specified fit *point* from this Spline entity.

    Arguments:
    index (I) Fit *point* *index*.
  */
  void removeFitPointAt(
    int index);
  
  /** Description:
    Returns the curve fitting tolerance for this Spline entity (DXF 44).
    
    Remarks:
    This is the maximum drawing unit distance by which the Spline curve can deviate
    from a fit *points*. 
  */
  double fitTolerance() const;
  
  /** Description:
    Sets the curve fitting tolerance for this Spline entity (DXF 44).
    
    Remarks:
    This is the maximum drawing unit distance by which the Spline curve can deviate
    from a fit *points*. 
    
    Arguments:
    fitTolerance (I) Geometric tolerance.
  */
  void setFitTol(
    double fitTolerance);
  
  /** Description:
    Returns the start point and end point WCS fit tangents for this Spline entity (DXF 12, 13).
    Arguments:
    startTangent (O) Receives the start point tangent.
    endTangent (O) Receives the end point tangent.
  */
  OdResult getFitTangents(
    OdGeVector3d& startTangent, 
    OdGeVector3d& endTangent) const;
  
  /** Description:
    Sets the start point and end point WCS fit tangents for this Spline entity (DXF 12, 13).
    Arguments:
    startTangent (O) Start point tangent.
    endTangent (O) End point tangent.
  */
  void setFitTangents(
    const OdGeVector3d& startTangent, 
    const OdGeVector3d& endTangent);
  
  /** Description:
    Returns true if and only if this Spline entity is constructed using fit points.
  */
  bool hasFitData() const;
  
  /** Description:
    Returns the fit data for this Spline entity.

    Arguments:
    fitPoints (O) Receives the fit points.
    degree (O) Receives the *degree*.
    fitTolerance (O) Receives the fit tolerance.
    tangentsExist (O) Receives true if and only if start and end tangents are used.
    startTangent (O) Receives the start point tangent.
    endTangent (O) Receives the end point tangent.
  */
  OdResult getFitData(
    OdGePoint3dArray& fitPoints, 
    int& degree, 
    double& fitTolerance, 
    bool& tangentsExist, 
    OdGeVector3d& startTangent, 
    OdGeVector3d& endTangent ) const;
  
  /** Description:
    Sets the fit data for this Spline entity.
    Arguments:
    fitPoints (I) Fit points.
    degree (I) Degree.
    fitTolerance (I) Fit tolerance.
    startTangent (I) Start point tangent.
    endTangent (I) End point tangent.
  */
  void setFitData(
    const OdGePoint3dArray& fitPoints, 
    int degree, 
    double fitTolerance, 
    const OdGeVector3d& startTangent, 
    const OdGeVector3d& endTangent );
  
  /** Description:
    Purges the fit data for this Spline entity.
  */
  void purgeFitData();
  
  /** Description:
      TBC.
      void updateFitData();
  */
  
  /** Description:
    Returns the NURBS data for this Spline entity.
    Arguments:
    degree (O) Receives the *degree*.
    rational (O) Receives true if and only if this Spline entity is *rational*.
    closed (O) Receives true if and only if this Spline entity is *closed*.
    periodic (O) Receives true if and only if this Spline entity is *periodic*.
    controlPoints (O) Receives an array of WCS control points.
    knots (O) Receives the knot vector.
    weights (O) Receives an array of *weights*.
    controlPtTol (O) Receives the control point tolerance.
    knotTol (O) Receives the knot tolerance.
  */
  void getNurbsData(
    int& degree, 
    bool& rational, 
    bool& closed, 
    bool& periodic,
    OdGePoint3dArray& controlPoints, 
    OdGeDoubleArray& knots, 
    OdGeDoubleArray& weights, 
    double& controlPtTol,
    double& knotTol) const;

  void getNurbsData(
    int& degree, 
    bool& rational, 
    bool& closed, 
    bool& periodic,
    OdGePoint3dArray& controlPoints, 
    OdGeKnotVector& knots, 
    OdGeDoubleArray& weights, 
    double& controlPtTol) const;
  
  /** Description:
    Sets the NURBS data for this Spline entity.
    Arguments:
    degree (I) Degree.
    rational (I) Controls if this Spline entity is *rational*.
    closed (I) Controls if this Spline entity *closed*.
    periodic (I) Controls if this Spline entity is *periodic*.
    controlPoints (I) Array of WCS control points.
    knots (I) Knot vector.
    weights (I) Array of *weights*.
    controlPtTol (I) Control point tolerance.
    knotTol (I) Knot tolerance.
    
    Remarks:
    o degree is in the range of [1..25]
    o If rational is true, controlPoints.length() must equal weights.length()
    o If periodic is false, then knots.length() must equal controlPoints.length() + degree + 1
    o If periodic is true, then knots.length() must equal controlPoints.length(), 
      the first and last controlPoints must be equal, and the first and last *weights* (if provided) must be equal.
    o If two control points are within controlPtTol, they are treated as the same control point.
    o If two *knots* are within knotTol, they are treated as the same knot.
  */
  void setNurbsData(int degree, 
    bool rational, 
    bool closed, 
    bool periodic,
    const OdGePoint3dArray& controlPoints, 
    const OdGeDoubleArray& knots, 
    const OdGeDoubleArray& weights,
    double controlPtTol, 
    double knotTol );
  
  void setNurbsData(
    int degree, 
    bool rational, 
    bool closed, 
    bool periodic,
    const OdGePoint3dArray& controlPoints, 
    const OdGeKnotVector& knots, 
    const OdGeDoubleArray& weights,
    double controlPtTol);
  
  /** Description:
    Returns the specified *weight* (DXF 41).
    Arguments:
    index (I) Weight *index*.
  */
  double weightAt(
    int index) const;
  
  /** Description:
    Sets the specified *weight* (DXF 41).
    Arguments:
    index (I) Weight *index*.
    weight (I) Weight.
  */
  void setWeightAt(
    int index, 
    double weight);
  
  /** Description:
      Inserts a knot value into this spline.
  */
  // --not implemented yet-- void insertKnot(double param);
  
  /** Description:
     Reverses this Spline entity.
     
     Remarks:
     The start point becomes the end point, and vice versa.
  */
  void reverseCurve();
  
  
  virtual void getClassID(
    void** pClsid) const;
  
  /*
     void getOffsetCurvesGivenPlaneNormal(const OdGeVector3d& normal,  //Replace OdRxObjectPtrArray
                                          double offsetDist, 
                                          OdRxObjectPtrArray& offsetCurves ) const;
  */
    
  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);
  
  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;
  
  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);
  
  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;
  
  virtual bool worldDraw(
    OdGiWorldDraw* pWd) const;

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);

  virtual bool isPlanar() const;

  virtual OdResult getPlane(
    OdGePlane& plane, 
    OdDb::Planarity& planarity) const;


  /** Description:
      OdDbCurve methods
  */

  virtual OdResult getPointAtParam(
    double param, 
    OdGePoint3d& pointOnCurve) const;

  virtual OdResult getParamAtPoint(
    const OdGePoint3d& pointOnCurve, 
    double& param) const;

  virtual OdResult getStartParam(
    double& startParam) const;

  virtual OdResult getEndParam (
    double& endParam) const;

  virtual OdResult getStartPoint(
    OdGePoint3d& startPoint) const;

  virtual OdResult getEndPoint(
    OdGePoint3d& endPoint) const;

  /**
    Description:
    Returns pointers to the curves that result from splitting this *curve* at the points corresponding 
    to params.

    Arguments:
    params (I) The parameter values corresponding to the split points.
    entitySet (O) Receives an array of pointers to the split curves.
    
    Remarks:
    o The first split curve will be from the start of this *curve* to params[0].
    o The second split curve will be from params[0] to params[1].
    o The last split curve will be from params[n-1] to the end of his *curve*.
  */      
  virtual OdResult getSplitCurves(
    const OdGeDoubleArray& params, 
    OdRxObjectPtrArray& entitySet) const;

  OdDbObjectPtr decomposeForSave(
    OdDb::DwgVersion ver,
    OdDbObjectId& replaceId,
    bool& exchangeXData);
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbSpline object pointers.
*/
typedef OdSmartPtr<OdDbSpline> OdDbSplinePtr;

#include "DD_PackPop.h"

#endif


