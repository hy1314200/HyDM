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
// DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef OD_GENURB2d_H
#define OD_GENURB2d_H /* {Secret} */

#include "GeSplineEnt2d.h"
#include "OdPlatformSettings.h"

class OdGeNurbCurve2dImpl;
class OdGePolyline2d;
class OdGeEllipArc2d;
class OdGeLineSeg2d;
class OdGeKnotVector;

#include "DD_PackPush.h"

/**
    Description:
    This class represents non-uniform rational B-splines (NURBs) in 2D space.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeNurbCurve2d : public OdGeSplineEnt2d
{
public:
  /**
    Arguments:
    degree (I) Degree of spline.
    knots (I) Knot vector.
    controlPoints (I) Array of 2D control points.
    weights (I) Array of *weights*
    isPeriodic (I) True if and only if the spline is to be periodic.
    source (I) Object to be cloned.
    
    Remarks:
    o If weights is not specified, a non-rational spline is constructed. 
    o If weights is specified, then a rational spline is constructed, and controlPoints.length() must equal weights.length()
    o If isPeriodic is false, then knots.length() must equal controlPoints.length() + degree + 1
    o If isPeriodic is true, then knots.length() must equal controlPoints.length(), the first and last controlPoints must be equal
      and the first and last weights (if provided) must be equal.
  */
  OdGeNurbCurve2d ();
  OdGeNurbCurve2d (
    const OdGeNurbCurve2d& source);
  OdGeNurbCurve2d (
    int degree, 
    const OdGeKnotVector& knots,
    const OdGePoint2dArray& controlPoints,
    bool isPeriodic = false);
  OdGeNurbCurve2d (
    int degree, 
    const OdGeKnotVector& knots,
    const OdGePoint2dArray& controlPoints,
    const OdGeDoubleArray& weights,
    bool isPeriodic = false);
  /*
  // Construct spline from interpolation data.
  //
  OdGeNurbCurve2d (int degree, const OdGePolyline2d& fitPolyline,
  bool isPeriodic = false);
  OdGeNurbCurve2d (const OdGePoint2dArray& fitPoints,
  const OdGeVector2d& startTangent,
  const OdGeVector2d& endTangent,
  bool startTangentDefined = true,
  bool endTangentDefined = true,
  const OdGeTol& fitTolerance = OdGeContext::gTol);
  OdGeNurbCurve2d (const OdGePoint2dArray& fitPoints,
  const OdGeTol& fitTolerance = OdGeContext::gTol);
  OdGeNurbCurve2d (const OdGePoint2dArray& fitPoints,
  const OdGeVector2dArray& fitTangents,
  const OdGeTol& fitTolerance = OdGeContext::gTol,
  bool isPeriodic = false);
  */

  virtual ~OdGeNurbCurve2d ();

  /*
  // Spline representation of ellipse
  //
  OdGeNurbCurve2d (const OdGeEllipArc2d& ellipse);

  // Spline representation of line segment
  //
  OdGeNurbCurve2d (const OdGeLineSeg2d& linSeg);

  // Query methods.
  //
  int numFitPoints () const;
  bool getFitPointAt (int index, OdGePoint2d& point) const;
  bool getFitTolerance (OdGeTol& fitTolerance) const;
  bool getFitTangents (OdGeVector2d& startTangent,
  OdGeVector2d& endTangent) const;
  bool getFitData (OdGePoint2dArray& fitPoints,
  OdGeTol& fitTolerance,
  bool& tangentsExist,
  OdGeVector2d& startTangent,
  OdGeVector2d& endTangent) const;
  */
  
  /** 
    Description:
    Returns all the data that define the spline.
    
    Arguments:
    degree (O) Receives the degree of spline.
    knots (I) Knot vector.
    controlPoints (I) Array of 2D control points.
    weights (I) Array of *weights*.
    rational (I) True if and only if the spline is *rational*.
    periodic (I) True if and only if the spline is *periodic*.
  */
  void getDefinitionData (
    int& degree, 
    bool& rational,
    bool& periodic,
    OdGeKnotVector& knots,
    OdGePoint2dArray& controlPoints,
    OdGeDoubleArray& weights) const;

  int degree () const;
  bool isRational () const;
  virtual bool isClosed (
    const OdGeTol& tol = OdGeContext::gTol) const;

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

  int numKnots () const;
  const OdGeKnotVector& knots () const;
  double knotAt (
    int idx) const;

  int numControlPoints () const;
  OdGePoint2d controlPointAt (
    int idx) const;

  OdGeCurve2d& reverseParam ();

  /*
  bool evalMode () const;
  bool getParamsOfC1Discontinuity (OdGeDoubleArray& params,
  const OdGeTol& tol
  = OdGeContext::gTol) const;
  bool getParamsOfG1Discontinuity (OdGeDoubleArray& params,
  const OdGeTol& tol
  = OdGeContext::gTol) const;

  // Modification methods.
  //
  void setFitPointAt (int index, const OdGePoint2d& point);
  void addFitPointAt (int index, const OdGePoint2d& point);
  void deleteFitPointAt (int index);
  void setFitTolerance (const OdGeTol& fitTol=OdGeContext::gTol);
  void setFitTangents (const OdGeVector2d& startTangent,
  const OdGeVector2d& endTangent);
  OdGeNurbCurve2d& setFitData (const OdGePoint2dArray& fitPoints,
  const OdGeVector2d& startTangent,
  const OdGeVector2d& endTangent,
  const OdGeTol& fitTol=OdGeContext::gTol);
  OdGeNurbCurve2d& setFitData (const OdGeKnotVector& fitKnots,
  const OdGePoint2dArray& fitPoints,
  const OdGeVector2d& startTangent,
  const OdGeVector2d& endTangent,
  const OdGeTol& fitTol=OdGeContext::gTol,
  bool isPeriodic=false);
  OdGeNurbCurve2d& setFitData (int degree,
  const OdGePoint2dArray& fitPoints,
  const OdGeTol& fitTol=OdGeContext::gTol);
  */
  
  /**
    Description:
    Deletes all the fit data used to construct this *spline*. 
    Returns true if and only if the *spline* was constructed with fit data.

    Remarks:
    The definition of the *spline* is unchanged. 
  */
  bool purgeFitData ();
  
  OdGeSplineEnt2d& setKnotAt (
    int idx, 
    double val);
  /* OdGeNurbCurve2d& addKnot (double newKnot);
  OdGeNurbCurve2d& insertKnot (double newKnot);*/

  /**
    Description:
    Sets the *weight* at the specified control *point*.
    
    Arguments:
    idx (I) The control *point* to be *set*.
    weight (I) The *weight* to be assigned to that control *point*.
  */
  OdGeSplineEnt2d& setWeightAt (
    int idx, 
    double weight);
    
  /**
    Description:
    Makes this spline rational.
    
    Arguments:
    weight (I) The *weight* to be assigned to each control *point*.
    
    Remarks:
    When called for a non-rational spline, this spline is made rational, and the specified weight is assigned to each control *point*.
    
    When called for a rational spline, the *weight* at each control *point* is multiplied by the specified weight. 
  */
  OdGeNurbCurve2d& makeRational (
    double weight = 1.0);

  OdGeSplineEnt2d& setControlPointAt (
    int idx, 
    const OdGePoint2d& point);

  /*
  OdGeNurbCurve2d& setEvalMode (bool evalMode=false);
  OdGeNurbCurve2d& joinWith (const OdGeNurbCurve2d& curve);
  OdGeNurbCurve2d& hardTrimByParams (double newStartParam,
  double newEndParam);
  OdGeNurbCurve2d& makeClosed ();
  OdGeNurbCurve2d& makePeriodic ();
  OdGeNurbCurve2d& makeNonPeriodic ();
  OdGeNurbCurve2d& makeOpen ();
  OdGeNurbCurve2d& elevateDegree (int plusDegree);
  */
  // Virtual overrides


  OdGe::EntityId type () const;
  void appendSamplePoints (
    double fromParam,
    double toParam,
    double approxEps,
    OdGePoint2dArray& pointArray,
    OdGeDoubleArray* pParamArray = 0) const;
  DD_USING (OdGeSplineEnt2d::appendSamplePoints);

  OdGeEntity2d* copy () const
  {
    return new OdGeNurbCurve2d (*this);
  }

  virtual OdGeEntity2d& transformBy (
    const OdGeMatrix2d& xfm);

  OdGePoint2d evalPoint (
    double param) const;

  DD_USING(OdGeSplineEnt2d::evalPoint);

  double paramOf (
    const OdGePoint2d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Arguments:
    start (O) Receives the start point of *interval*.
    end (O) Receives the start point of *interval*. 
  */
  void getInterval (
    OdGeInterval& interval) const;
  void getInterval (
    OdGeInterval& interval, 
    OdGePoint2d& start, 
    OdGePoint2d& end) const;

  double startParam () const;
  double endParam () const;
  bool setInterval (
    const OdGeInterval& interval);

  // Assignment operator.
  //
  
  OdGeNurbCurve2d& operator = (
    const OdGeNurbCurve2d& spline);

  bool hasStartPoint (
    OdGePoint2d& startPoint) const;
  bool hasEndPoint (
    OdGePoint2d& endPoint) const;
  OdGePoint2d startPoint () const;
  OdGePoint2d endPoint () const;
  /**
    Description:
    Sets the parameters for this spline according to the arguments, 
    and returns a reference to this spline.

    Arguments:
    degree (I) Degree of spline.
    knots (I) Knot vector.
    controlPoints (I) Array of 2D control points.
    weights (I) Array of *weights*
    isPeriodic (I) True if and only if the spline is to be periodic (closed).
    
    Remarks:
    o If weights is not specified, a non-rational spline is constructed. 
    o If weights is specified, then a rational spline is constructed, and controlPoints.length() must equal weights.length()
    o If isPeriodic is false, then knots.length() must equal controlPoints.length() + degree + 1
    o If isPeriodic is true, then knots.length() must equal controlPoints.length(), the first and last controlPoints must 
      be equal, and the first and last weights (if provided) must be equal.
  */
  void set (
    int degree, 
    const OdGeKnotVector& knots,
    const OdGePoint2dArray& controlPoints, 
    const OdGeDoubleArray& weights, 
    bool isPeriodic = false);

  /**
    Description:
    Returns true if and only if this *spline* intersects with the specified linear entity.
    Returns the intersection points well as the parameters of this *spline* said points.  

    Arguments:
    line (I) Any 2D linear entity.
    pnts2D (O) Receives an array of 2D points.
    tol (I) Geometric tolerance.
    pParams (O) Receives a pointer to an array of parameters.
  */
  bool intersectWith (
    const OdGeLine2d &line2d, 
    OdGePoint2dArray &pnts2d, 
    const OdGeTol& tol = OdGeContext::gTol, 
    OdGeDoubleArray *pParams = NULL) const;

  bool isLinear (
    OdGeLine2d& line, 
    const OdGeTol& tol = OdGeContext::gTol) const;

private:
  friend class OdGeSystemInternals;
  OdGeNurbCurve2dImpl* m_pImpl;
};

#include "DD_PackPop.h"

#endif


