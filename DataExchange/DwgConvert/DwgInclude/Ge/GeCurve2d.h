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



#ifndef OD_GE_CURVE_2D_H
#define OD_GE_CURVE_2D_H /* {Secret} */


class OdGePoint2d;
class OdGeVector2d;
class OdGePointOnCurve2d;
class OdGeInterval;
class OdGeMatrix2d;
class OdGeLine2d;
class OdGePointOnCurve2dData;
class OdGeBoundBlock2d;

#include "GeEntity2d.h"
#include "GeIntArray.h"
#include "GePoint2dArray.h"
#include "GeVector2dArray.h"
#include "GeVoidPointerArray.h"
#include "GeDoubleArray.h"

#include "DD_PackPush.h"

/**
    Description:
    This class is the base class for all OdGe 2D curves.

    Library: Ge

   {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeCurve2d : public OdGeEntity2d
{
public:

  /**
    Description:
    Returns the parametric *interval* of the *curve*.

    Arguments:
    interval (O) Receives the parametric *interval* of the *curve*.
*/
  virtual void getInterval (
    OdGeInterval& interval) const = 0;
/*
    start (O) Receives the start point of interval.
    end (O) Receives the end point of interval. 
  virtual void getInterval (
    OdGeInterval& interval, 
    OdGePoint2d& start,
    OdGePoint2d& end) const = 0;
*/
  //  virtual OdGeCurve2d& reverseParam () = 0;

  /**
    Description:
    Reverses the parameter *direction* this *curve*.   

    Remarks:
    The *point* *set* of this *curve* is unchanged.
   */
   /**
    Note:
    As implemented, this function does nothing but
    assert in debug mode, and return a reference to this object.
    It will be fully implemented in a future *release*.
  */
  virtual OdGeCurve2d& reverseParam () { ODA_FAIL (); return *this; }
  
  
  //  virtual OdGeCurve2d& setInterval () = 0;

  //  virtual bool setInterval (const OdGeInterval& interval) = 0;

  /**
    Description:
    Sets the parametric *interval* of this *curve*.

    Arguments:
    interval (I) Parametric *interval* of this *curve*

  */      
 /**
    Note:
    As implemented, this function does nothing but
    assert in debug mode, and return the point (0,0).
    It will be fully implemented in a future *release*.
  */
  virtual bool setInterval (
    const OdGeInterval& interval);

  //  virtual double distanceTo (const OdGePoint2d& point,
  //    const OdGeTol& = OdGeContext::gTol) const = 0;
  //  virtual double distanceTo (const OdGeCurve2d&,
  //    const OdGeTol& tol = OdGeContext::gTol) const = 0;

  //  virtual OdGePoint2d closestPointTo (const OdGePoint2d& point,
  //    const OdGeTol& tol = OdGeContext::gTol) const = 0;
  //  virtual OdGePoint2d closestPointTo (const OdGeCurve2d& curve2d,
  //    OdGePoint2d& pntOnOtherCrv,
  //    const OdGeTol& tol= OdGeContext::gTol) const = 0;

  //  virtual void getClosestPointTo (const OdGePoint2d& point,
  //    OdGePointOnCurve2d& pntOnCrv,
  //    const OdGeTol& tol = OdGeContext::gTol) const = 0;
  //  virtual void getClosestPointTo (const OdGeCurve2d& curve2d,
  //    OdGePointOnCurve2d& pntOnThisCrv,
  //    OdGePointOnCurve2d& pntOnOtherCrv,
  //    const OdGeTol& tol = OdGeContext::gTol) const = 0;

  //  virtual bool getNormalPoint (const OdGePoint2d& point,
  //    OdGePointOnCurve2d& pntOnCrv,
  //    const OdGeTol& tol = OdGeContext::gTol) const = 0;

  //  virtual bool isOn (const OdGePoint2d& point, const OdGeTol& tol = OdGeContext::gTol) const = 0;

  //  virtual bool isOn (const OdGePoint2d& point, double& param,
  //    const OdGeTol& tol = OdGeContext::gTol) const = 0;

  //  virtual bool isOn (double param,
  //    const OdGeTol& tol = OdGeContext::gTol) const = 0;

  /**
    Description:
    Returns the parameter value of a *point*.

    Arguments:
    point (I) Point to be evaluated.
    tol (I) Geometric tolerance.

    Remarks:
    The returned parameters specify a *point* within tol of point.
    If point is not on the *curve*, the results are unpredictable.
    If you are not sure the *point* is on the *curve*, use 
    isOn () instead of this function.

  */
  virtual double paramOf (const OdGePoint2d& point,
    const OdGeTol& tol = OdGeContext::gTol) const = 0;

  //  virtual void getTrimmedOffset (double distance,
  //    OdGeVoidPointerArray& offsetCurveList,
  //    OdGe::OffsetCrvExtType extensionType = OdGe::kFillet,
  //    const OdGeTol& = OdGeContext::gTol) const = 0;

  /**
      Description:
      Returns true if and only if the *curve* is closed within the specified tolerance.

      Arguments:
      tol (I) Geometric tolerance.
  */
  virtual bool isClosed (
    const OdGeTol& tol = OdGeContext::gTol) const = 0;

  //  virtual bool isPeriodic (double& period) const = 0;
  //   virtual bool isLinear (OdGeLine2d& line, const OdGeTol& tol = OdGeContext::gTol) const = 0;

  /**
    Description:
    Returns true, and a linear entity coincident with this *curve*,
    if and only if this *curve* is linear.

    Arguments:
    line (O) Receives the *line* coincident with this *curve*.
    tol (I) Geometric tolerance.

    Remarks:
    An infinite *line* is returned, even if this *curve* is bounded.

  */
  /**
    Note:
    As implemented, this function does nothing but
    assert in debug mode, and return false.
    It will be fully implemented in a future *release*.
  */
  bool isLinear (
    OdGeLine2d& line,
    const OdGeTol& tol = OdGeContext::gTol) const;

  //  virtual double length (double fromParam, double toParam,
  //    double tol = OdGeContext::gTol.equalPoint ()) const = 0;

  //  virtual double paramAtLength (double datumParam, double length,
  //    bool posParamDir = true, double tol = OdGeContext::gTol.equalPoint ()) const = 0;

  //  virtual bool area (double startParam, double endParam,
  //    double& value, const OdGeTol& tol = OdGeContext::gTol) const = 0;

  //  virtual bool isDegenerate (OdGe::EntityId& degenerateType,
  //    const OdGeTol& tol = OdGeContext::gTol) const = 0;

  //  virtual bool isDegenerate (OdGeEntity2d*& pConvertedEntity,
  //    const OdGeTol& tol = OdGeContext::gTol) const = 0;

  //  virtual void getSplitCurves (double param, OdGeCurve2d* & piece1,
  //    OdGeCurve2d* & piece2) const = 0;

  //  virtual bool explode (OdGeVoidPointerArray& explodedCurves,
  //    OdGeIntArray& newExplodedCurve,
  //    const OdGeInterval* interval = NULL) const = 0;

  //  virtual void getLocalClosestPoints (const OdGePoint2d& point,
  //    OdGePointOnCurve2d& approxPnt,
  //    const OdGeInterval* nbhd = 0,
  //    const OdGeTol& = OdGeContext::gTol) const = 0;
  //  virtual void getLocalClosestPoints (const OdGeCurve2d& otherCurve,
  //    OdGePointOnCurve2d& approxPntOnThisCrv,
  //    OdGePointOnCurve2d& approxPntOnOtherCrv,
  //    const OdGeInterval* nbhd1 = 0,
  //    const OdGeInterval* nbhd2 = 0,
  //    const OdGeTol& tol = OdGeContext::gTol) const = 0;

  //  virtual OdGeBoundBlock2d boundBlock () const = 0;
  //  virtual OdGeBoundBlock2d boundBlock (const OdGeInterval& range) const = 0;

  //  virtual OdGeBoundBlock2d orthoBoundBlock () const = 0;
  //  virtual OdGeBoundBlock2d orthoBoundBlock (const OdGeInterval& range) const = 0;

  /**
    Description:
    Returns true, and the start point, if and only if the parametric *interval* of the *curve* has a lower bound.

    Arguments:
    startPoint (O) Receives the start point of the interval.
  */
  virtual bool hasStartPoint (
    OdGePoint2d& startPoint) const = 0;

  /**
    Description:
    Returns true, and the end point, if and only if the parametric *interval* of the *curve* has an upper bound.

    Arguments:
    endPoint (O) Receives the end point of the interval.
  */
  virtual bool hasEndPoint (
    OdGePoint2d& endPoint) const = 0;

  /**
    Description:
    Returns the *point* on the *curve* corresponding to the specified parameter value, and the derviatives at that *point*.

    Arguments:
    param (I) Parameter to be evaluated.
    numDeriv (I) The number of *derivatives* to be computed.
    derivatives (O) Receives an array of *derivatives* at the point corresponding to param.

    Note:
    OdGeCurve2d::evalpoint(double param, int numDeriv, OdGeVector2dArray& derivatives) is not implemented, 
    and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  virtual OdGePoint2d evalPoint (
    double param) const = 0;
  virtual OdGePoint2d evalPoint (
    double param, 
    int numDeriv,
    OdGeVector2dArray& derivatives) const;

  /**
  Description:
    Appends sample points along this *curve* and their parameter values to the specified arrays.

    Arguments:
    fromParam (I) Starting parameter value.
    toParam (I) Ending parameter value.
    approxEps (I) Approximate spacing along a *curve*.
    numSample (I) Number of samples.
    pointArray (O) Receives an array of sample points.
    pParamArray (O) Receives a pointer to an array of parameters at each *point*.
  */
  virtual void appendSamplePoints (
    double fromParam, 
    double toParam, 
    double approxEps, 
    OdGePoint2dArray& pointArray, 
    OdGeDoubleArray* pParamArray = 0) const = 0;

  virtual void appendSamplePoints (
    int numSample, 
    OdGePoint2dArray& pointArray) const;

  /**
    Description:
    Returns sample points along this *curve* and their parameter values in the specified arrays.

    Arguments:
    fromParam (I) Starting parameter value.
    toParam (I) Ending parameter value.
    approxEps (I) Approximate spacing along a *curve*.
    pointArray Returns an array of sample points.
    pParamArray (O) Receives an array of parameters at each *point*.
    
  */
  void getSamplePoints (
    double fromParam, 
    double toParam, 
    double approxEps, 
    OdGePoint2dArray& pointArray, 
    OdGeDoubleArray* pParamArray = 0) const
    {
      pointArray.clear ();
      if (pParamArray)
        pParamArray->clear ();
      appendSamplePoints (fromParam, toParam, approxEps, pointArray, pParamArray);
    }

  void getSamplePoints (
    int numSample, 
    OdGePoint2dArray& pointArray) const
  {
    pointArray.clear ();
    appendSamplePoints (numSample, pointArray);
  }
  //:>OdGeCurve2d& operator = (const OdGeCurve2d& curve);

protected:
  /**
    Arguments:
    source (I) Object to be cloned.
  */
  OdGeCurve2d () {}
  OdGeCurve2d (
    const OdGeCurve2d& source);
};


#include "DD_PackPop.h"

#endif // OD_GE_CURVE_2D_H


