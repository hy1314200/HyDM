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
// programs incorporating this software must include the following statment 
// with their copyright notices:
//
//      DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef OD_GENURB3D_H
#define OD_GENURB3D_H /* {Secret} */

class OdGeEllipArc3d;
class OdGeLineSeg3d;
class OdGeKnotVector;

#include "GeSplineEnt3d.h"
#include "OdPlatformSettings.h"

class OdGeNurbCurve3dImpl;

#include "DD_PackPush.h"

/**
    Description:
    This class represents non-uniform rational B-splines in 3D space.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeNurbCurve3d : public OdGeSplineEnt3d
{
public:

  virtual ~OdGeNurbCurve3d ();


  /**
    Arguments:
    degree (I) Degree of spline.
    knots (I) Knot vector.
    controlPoints (I) Array of 3D control points.
    weights (I) Array of *weights*
    isPeriodic (I) True if and only if this spline is to be periodic.
    fitPoints (I) Interpolation points.
    fitTol (I) Geometric tolerance.
    ellipse (I) Any elliptical arc.
    numSpans (I) Number of knot spans in the *curve*.
    lineSeg (I) Any 3D line segment.
    startTangent (I) Starting tangent.
    endTangent (I) Ending tangent.
    startTangentDefined (I) If true, startTangent is used.
    endTangentDefined (I) If true, endTangent is used.
    source (I) Object to be cloned.
    cntrlPntsSize (I) Number of control points.
    weightsSize (I) Number of weights.
    
    Remarks:
    o If weights is not specified, a non-rational spline is constructed. 
    o If weights is specified, then a rational spline is constructed, and controlPoints.length() must equal weights.length()
    o If isPeriodic is false, then knots.length() must equal controlPoints.length() + degree + 1
    o If isPeriodic is true, then knots.length() must equal controlPoints.length(), the first and last controlPoints must be equal
      and the first and last weights (if provided) must be equal.
    o If ellipse is specified, a curve identical to the ellipse is created.
    o If lineSeg is specified, a curve identical to the line segment is created.
  */
  OdGeNurbCurve3d ();

  OdGeNurbCurve3d (
    const OdGeNurbCurve3d& source);

  OdGeNurbCurve3d (
    int degree,
    const OdGeKnotVector& knots,
    const OdGePoint3dArray& controlPoints,
    bool isPeriodic = false);

  OdGeNurbCurve3d (
    int degree,
    const OdGeKnotVector& knots,
    const OdGePoint3dArray& controlPoints,
    const OdGeDoubleArray& weights,
    bool isPeriodic = false);

  OdGeNurbCurve3d (
    int degree,
    const OdGeKnotVector& knots,
    const OdGePoint3d* controlPoints,
    OdUInt32 cntrlPntsSize,
    const double* weights,
    OdUInt32 weightsSize,
    bool isPeriodic = false);

  /* Not implemented yet
  OdGeNurbCurve3d (int degree, const OdGePolyline3d& fitPolyline,
  bool isPeriodic = false); */

  OdGeNurbCurve3d (
    const OdGePoint3dArray& fitPoints,
    const OdGeVector3d& startTangent,
    const OdGeVector3d& endTangent,
    bool startTangentDefined = true,
    bool endTangentDefined = true,
    const OdGeTol& fitTol = OdGeContext::gTol);

  OdGeNurbCurve3d (
    const OdGePoint3dArray& fitPoints,
    const OdGeTol& fitTolerance = OdGeContext::gTol);

  /* Not implemented yet - low priority
  OdGeNurbCurve3d (const OdGePoint3dArray& fitPoints,
  const OdGeVector3dArray& fitTangents,
  const OdGeTol& fitTolerance = OdGeContext::gTol,
  bool isPeriodic = false);*/

  // numSpans - the number of knot spans in nurbs curve
  // if numSpans == 0 (default) it is automatically calculated from 
  // ellipse domain

  OdGeNurbCurve3d (
    const OdGeEllipArc3d& ellipse, 
    int numSpans = 0);

  OdGeNurbCurve3d (
    const OdGeLineSeg3d& lineSeg);

  OdGe::EntityId type () const;

  OdGeEntity3d* copy () const;

  
  /**
    Description:
    Returns the number of fit points.
  */
  int numFitPoints () const;

  /**
    Description:
    Returns true if and only if  0 <= idx < numFitPoints(),
    and returns the fit *point* at the specified index.

    Arguments:
    idx (I) Index of fit *point*.
    point (O) Receives the fit *point*. 
  */
  bool getFitPointAt (
    int idx, 
    OdGePoint3d& point) const;

  /** 
    Description:
    Returns true if and only if this spline was constructed
    with fit points, and returns the fit Tolerance that was
    used to construct this spline.
    
    Arguments:
    fitTolerance (I) Geometric tolerance.
 */
  bool getFitTolerance (
    OdGeTol& fitTolerance) const;


  /**
    Descripton:
    Returns true if and only if this spline was constructed with fit data, and
    returns the starting and ending tangents.
    
    Arguments:
    startTangent (O) Receives the starting tangent
    endTangent (O) Receives the ending tangent.
    startTangentDefined (O) Receives the if true, startTangent was used.
    endTangentDefined (O) Receives the if true, endTangent was used.
  */
  bool getFitTangents (
    OdGeVector3d& startTangent,
    OdGeVector3d& endTangent) const;

  bool getFitTangents (
    OdGeVector3d& startTangent,
    OdGeVector3d& endTangent,
    bool& startTangentDefined,
    bool& endTangentDefined) const;

  /**
    Description:
    Returns true if and only if this spline was constructed with fit data,
    and returns all the fit data used to construct this spline.
    
    Arguments:
    fitPoints (O) Receives the onterpolation points.
    fitTolerance (O) Receives the geometric tolerance.
    tangentsExist (O) Receives true if and only if tangents were used in constructing this spline.
    startTangent (O) Receives the starting tangent
    endTangent (O) Receives the ending tangent.
 
    Remarks:
    startTangent and endTangent are meaningful if and only if tangentsExist == true.
  */
  bool getFitData (
    OdGePoint3dArray& fitPoints,
    OdGeTol& fitTolerance,
    bool& tangentsExist,
    OdGeVector3d& startTangent,
    OdGeVector3d& endTangent) const;


  // NURBS data query functions

  /**
    Description:
    Returns the data used to define this spline.

    Arguments:
    degree (O) Receives the degree of spline.
    periodic (O) Receives true if and only if this spline is *periodic*.
    rational (I) True if and only if this spline is *rational*.
    knots (O) Receives the knot vector.
    controlPoints (O) Receives an array of 2D control points.
    weights (O) Receives an array of *weights*
    
    Remarks:
    The weights array will be empty if the spline is not *rational*.
    
  */
  void getDefinitionData (
    int& degree,
    bool& rational,
    bool& periodic,
    OdGeKnotVector& knots,
    OdGePoint3dArray& controlPoints,
    OdGeDoubleArray& weights) const;

  /**
    Description:
    Returns the number of weights in the spline.
    
    Remarks:
    Feturns numControlPoints() if this spline is rational, 0 if it is not.
  */
  int numWeights () const;

  /**
    Description:
    Returns the weight at the specified index.

    Arguments:
    idx (I) Index of  weight.
  */
  double weightAt (
    int idx) const;

  /* bool evalMode () const;

  bool getParamsOfC1Discontinuity (OdGeDoubleArray& params,
  const OdGeTol& tol
  = OdGeContext::gTol) const;

  bool getParamsOfG1Discontinuity (OdGeDoubleArray& params,
  const OdGeTol& tol
  = OdGeContext::gTol) const;*/

  // Fit data edit functions

  /**
    Description:
    Sets the fit *point* at the specified index.

    Arguments:
    idx (I) Index of fit *point*.
    point (I) Fit *point*. 
  */
  void setFitPointAt (
    int idx, 
    const OdGePoint3d& point);

  /**
    Description:
    Returns true if and only if this spline was created with
    fit data, and inserts the fit *point* at the specified index.

    Arguments:
    idx (I) Index of fit *point*.
    point (I) Fit *point*. 
  */
  void addFitPointAt (
    int idx, 
    const OdGePoint3d& point);

  /**
    Description:
    Deletes the fit *point* at the specified index.

    Arguments:
    idx (I) Index of fit *point*.
  */
  void deleteFitPointAt (
    int idx);

  /**
    Description:
    Sets the fit tolerance for this spline.
    
    Arguments:
    fitTol (I) Geometric tolerance.
  */
  void setFitTolerance (
    const OdGeTol& fitTol = OdGeContext::gTol);


  /**
    Description:
    Sets the fit tangents for this spline.
    
    Arguments:
     startTangent (I) Starting tangent.
     endTangent (I) Ending tangent.
  */
  void setFitTangents (
    const OdGeVector3d& startTangent,
    const OdGeVector3d& endTangent);

  /* bool setFitTangents(const OdGeVector3d& startTangent,
  const OdGeVector3d& endTangent,
  bool& startTangentDefined,
  bool& endTangentDefined) const; Strange function */

  /**
    Sets the fit data for this spline.
    
    Arguments:
    fitPoints (I) Interpolation points.
    fitTol (I) Geometric tolerance.
    startTangent (I) Starting tangent.
    endTangent (I) Ending tangent.
  */
  OdGeNurbCurve3d& setFitData (
    const OdGePoint3dArray& fitPoints,
    const OdGeVector3d& startTangent,
    const OdGeVector3d& endTangent,
    const OdGeTol& fitTol = OdGeContext::gTol);

  /* OdGeNurbCurve3d& setFitData(const OdGeKnotVector& fitKnots,
  const OdGePoint3dArray& fitPoints,
  const OdGeVector3d& startTangent,
  const OdGeVector3d& endTangent,
  const OdGeTol& fitTol = OdGeContext::gTol,
  bool isPeriodic = false);*/

  /*OdGeNurbCurve3d& setFitData(int degree,
  const OdGePoint3dArray& fitPoints,
  const OdGeTol& fitTol = OdGeContext::gTol);*/

  /**
    Description:
    Purges the fit data defining this spline.
    
    Remarks:
    The fit data consists of the fit points, fit tolerance,
    *start* tangent, and *end* tangent.
  */
  bool purgeFitData ();

  // NURBS data edit functions
  
  /**
    Description:
    Sets the parameters for this spline according to the arguments, 
    and returns a reference to this spline.

    Arguments:
    degree (I) Degree of spline.
    knots (I) Knot vector.
    controlPoints (I) Array of 2D control points.
    weights (I) Array of *weights*
    isPeriodic (I) True if and only if this spline is to be periodic.
    
    Remarks:
    o A rational spline is constructed, and controlPoints.length() must equal weights.length()
    o If isPeriodic is false, then knots.length() must equal controlPoints.length() + degree + 1
    o If isPeriodic is true, then knots.length() must equal controlPoints.length(), the first and last controlPoints must be equal
      and the first and last weights (if provided) must be equal.
  */
  void set (
    int degree,
    const OdGeKnotVector& knots,
    const OdGePoint3dArray& controlPoints,
    const OdGeDoubleArray& weights,
    bool isPeriodic = false);

  /*
  OdGeNurbCurve3d& addKnot (double newKnot);

  OdGeNurbCurve3d& insertKnot (double newKnot);*/

  /**
    Description:
    Sets the *weight* at the specified index.

    Arguments:
    idx (I) Index of fit *point*.
    weight (I) Weight at the specified fit *point*. 
  */
  OdGeSplineEnt3d& setWeightAt (
    int idx, 
    double weight);

  //  Not high priority
  // OdGeNurbCurve3d& setEvalMode (bool evalMode = false);

  // OdGeNurbCurve3d& joinWith (const OdGeNurbCurve3d& curve);

  //OdGeNurbCurve3d& hardTrimByParams(double newStartParam,
  //double newEndParam);

  /**
    Arguments:
    Makes this spline rational (if it is not), and returns a reference to this spline.
    
    Arguments:
    weight (I) Weight to be applied to each control *point*. 
    
    Remarks:
    If this spline was already rational, the *weight* at each control point is multiplied by the
    specified weight.
  */
  OdGeNurbCurve3d& makeRational(
    double weight = 1.0);

  /* OdGeNurbCurve3d& makeClosed(); - deferred

  OdGeNurbCurve3d& makePeriodic();

  OdGeNurbCurve3d& makeNonPeriodic();

  OdGeNurbCurve3d& makeOpen();

  OdGeNurbCurve3d& elevateDegree (int plusDegree);*/

  // Virtual overrides(NURBS query functions)
  
  int degree () const;
  int order () const;

  bool hasFitData () const;

  bool isRational () const;
  virtual bool isClosed (
    const OdGeTol& tol = OdGeContext::gTol) const;

  bool isPlanar (
    OdGePlane& plane, 
    const OdGeTol& tol = OdGeContext::gTol) const;


  int numKnots () const;
  const OdGeKnotVector& knots () const;

  double startParam () const;
  double endParam () const;
  
  /**
    Arguments:
    start (O) Receives the start point of interval.
    end (O) Receives the start point of interval. 
  */
  virtual void getInterval (
    OdGeInterval& interval) const;
  virtual void getInterval (
    OdGeInterval& interval, 
    OdGePoint3d& start, 
    OdGePoint3d& end) const;
  virtual bool setInterval (
    const OdGeInterval& interval);
  DD_USING (OdGeSplineEnt3d::setInterval);

  // Evaluate methods.
  //

  OdGePoint3d evalPoint (
    double param) const;
  OdGePoint3d evalPoint (
    double param, 
    int numDeriv, 
    OdGeVector3dArray& derivatives) const;

  // This is only for internal use

  /** { Secret } */
  OdGePoint3d evalPoint (
    double param, 
    int hint) const;  

  // This is need in surface (for rational case only)

  /** { Secret } */
  void evalPointDivider (
    double param, 
    OdGePoint3d& point, 
    double& divider, 
    int hint) const;

  OdGePoint3d startPoint () const;
  OdGePoint3d endPoint () const;

  double knotAt (
    int idx) const;
  OdGeSplineEnt3d& setKnotAt (
    int idx, 
    double val);

  int numControlPoints () const;
  OdGePoint3d controlPointAt (
    int idx) const;
  OdGeSplineEnt3d& setControlPointAt (
    int idx, 
    const OdGePoint3d& point);

  virtual void appendSamplePoints (
    double fromParam, 
    double toParam,
    double approxEps, 
    OdGePoint3dArray& pointArray,
    OdGeDoubleArray* pParamArray = 0) const;
  DD_USING (OdGeSplineEnt3d::appendSamplePoints);

  // virtual void appendSamplePoints (int numSample, OdGePoint3dArray& pointArray) const;

  // Parameter of the *point* on *curve*.  Contract: point IS on curve
  //

  /**
    Note:
    Valid if and only if *point* is on curve.
  */
  double paramOf (
    const OdGePoint3d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  virtual OdGeCurve3d& reverseParam ();

  // Assignment operator.
  
  OdGeNurbCurve3d& operator = (
    const OdGeNurbCurve3d& spline);

  virtual OdGeEntity3d& transformBy (
    const OdGeMatrix3d& xfm);

  // Attention!!
  // resulting curves will be created by new operator!!
  // it is user responsibility to delete them!!

  virtual void getSplitCurves (
    double param, 
    OdGeCurve3d*& piece1,
    OdGeCurve3d*& piece2) const;

  bool hasStartPoint (
    OdGePoint3d& startPoint) const;
  bool hasEndPoint (
    OdGePoint3d& endPoint) const;

  virtual bool isLinear (
    OdGeLine3d& line, 
    const OdGeTol& tol = OdGeContext::gTol) const;
private:
  friend class OdGeSystemInternals;
  OdGeNurbCurve3dImpl* m_pImpl;
  void compute_Aders_wders (
    double param, 
    int numDeriv, 
    OdGeVector3dArray& Aders, 
    OdGeDoubleArray& wders) const;
};

#include "DD_PackPop.h"

#endif // OD_GENURB3D_H


