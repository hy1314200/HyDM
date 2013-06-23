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



#ifndef _OD_GECURVE3D_H_
#define _OD_GECURVE3D_H_ /* {Secret} */

#include "GeEntity3d.h"
#include "GeDoubleArray.h"
#include "GeVector3dArray.h"
#include "GeInterval.h"

class OdGeCurve2d;
class OdGeSurface;
class OdGePoint3d;
class OdGePlane;
class OdGeVector3d;
class OdGeLinearEnt3d;
class OdGeLine3d;
class OdGePointOnCurve3d;
class OdGePointOnSurface;
class OdGeMatrix3d;
class OdGePointOnCurve3dData;
class OdGeBoundBlock3d;


#include "DD_PackPush.h"

/**
    Description: 
    This class is the base class for all OdGe 3D curves.

    Library: Ge

   {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeCurve3d : public OdGeEntity3d
{
public:
  /**
    Description:
    Returns the parametric *interval* of this *curve*.

    Arguments:
    interval (O) Receives the parametric *interval* of this *curve*.
  */ 
  virtual void getInterval (
    OdGeInterval& interval) const = 0;

  //      virtual void getInterval (OdGeInterval& interval, OdGePoint3d& start, OdGePoint3d& end) const = 0;

  /**
    Description:
    Sets the parametric *interval* of this *curve*.

    Arguments:
    interval (I) Parametric *interval* of this *curve*
    
    
    Note:
    As implemented, this function does nothing but
    assert in debug mode, and return false.
    It will be fully implemented in a future *release*.
    
  */      
  virtual bool setInterval (
    const OdGeInterval& interval);

  /**
    Description:
    Reverses the parameter *direction* this *curve*.   

    Remarks:
    The *point* *set* of this *curve* is unchanged.

 */
  /**
    Note:
    As implemented, this function does nothing but
    assert in debug mode, and return this object.
    It will be fully implemented in a future *release*.
  */
  virtual OdGeCurve3d& reverseParam () { ODA_FAIL (); return *this; }


  //      virtual OdGeCurve3d& setInterval () { ODA_FAIL (); return *this; }

  /**
    Description:
    Returns the distance to the *point* on this *curve* closest to the specified *point* or *curve*.

    Arguments:
    point (I) Any 3D *point*.
    curve (I) Any 3D *curve*.
    tol (I) Geometric tolerance.
  */
  double distanceTo (
    const OdGePoint3d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  double distanceTo (
    const OdGeCurve3d& curve, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the *point* on this *curve* closest to the specified *point* or *curve*, and the *point*
    on the other *curve* closest to this *curve*.

    Arguments:
    point (I) Any 3D *point*.
    curve (I) Any 3D *curve*.
    pntOnOtherCrv (O) Receives the closest *point* on other *curve*.
    tol (I) Geometric tolerance.
  */
  OdGePoint3d closestPointTo (
    const OdGePoint3d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  OdGePoint3d closestPointTo (
    const OdGeCurve3d& curve, 
    OdGePoint3d& pntOnOtherCrv, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
  Description:
    Returns the *point* on this *curve* closest to the specified *point* or *curve*, 
    and the *point* on the other *curve* closest to this *curve*.

    Arguments:
    point (I) Any 3D *point*.
    curve (I) Any 3D *curve*.
    pntOnThisCrv (O) Receives the closest *point* on this *curve*.
    pntOnOtherCrv (O) Receives the closest *point* on other *curve*.
    tol (I) Geometric tolerance.
  */
  void getClosestPointTo (
    const OdGePoint3d& point, 
    OdGePointOnCurve3d& pntOnCrv, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  void getClosestPointTo (
    const OdGeCurve3d& curve, 
    OdGePointOnCurve3d& pntOnThisCrv, 
    OdGePointOnCurve3d& pntOnOtherCrv, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the *point* on this *curve* closest to the specified *point* or *curve*, 
    and the *point* on the other *curve* closest to this *curve*, when this *curve*
    is projected in the specified direction.

    Arguments:
    point (I) Any 3D *point*.
    curve (I) Any 3D *curve*.
    projectDirection (I) Projection Direction.
    pntOnOtherCrv (O) Receives the closest *point* on other *curve*.
    tol (I) Geometric tolerance.
   
    Note:
    As implemented, this function does nothing but
    throw an eNotImplemented error. 
    It will be fully implemented in a future *release*.

    Throws:
    @table
    Exception    
    eNotImplemented
   
  */
  virtual OdGePoint3d projClosestPointTo (
    const OdGePoint3d& point, 
    const OdGeVector3d& projectDirection, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  virtual OdGePoint3d projClosestPointTo (
    const OdGeCurve3d& curve, 
    const OdGeVector3d& projectDirection, 
    OdGePoint3d& pntOnOtherCrv, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the *point* on this *curve* closest to the specified *point* or *curve*, 
    and the *point* on the other *curve* closest to this *curve*, when this *curve*
    is projected in the specified direction.

    Arguments:
    point (I) Any 3D *point*.
    curve (I) Any 3D *curve*.
    projectDirection (I) Projection Direction.
    pntOnCrv (O) Receives the closest *point* on this *curve*.
    pntOnOtherCrv (O) Receives the closest *point* on other *curve*.
    tol (I) Geometric tolerance.
   
    Note:
    As implemented, this function does nothing but
    throw an eNotImplemented error. 
    It will be fully implemented in a future *release*.

    Throws:
    @table
    Exception    
    eNotImplemented
    
  */
  virtual void getProjClosestPointTo (
    const OdGePoint3d& point, 
    const OdGeVector3d& projectDirection, 
    OdGePointOnCurve3d& pntOnCrv, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  virtual void getProjClosestPointTo (
    const OdGeCurve3d& curve,
    const OdGeVector3d& projectDirection,
    OdGePointOnCurve3d& pntOnThisCrv,
    OdGePointOnCurve3d& pntOnOtherCrv,
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns true, and the *point* on this *curve* whose *normal* vector passes through the specified *point*,
    if and only if a normal *point* was found.

    Arguments:
    point (I) Any 3D *point*.
    pntOnCrv (O) Receives the normal *point*.
    tol (I) Geometric tolerance.
  */
  bool getNormalPoint (const OdGePoint3d& point, 
    OdGePointOnCurve3d& pntOnCrv, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**     Description
    Returns the oriented bounding box of *curve*.
  */
  OdGeBoundBlock3d boundBlock () const;

  /**     Arguments:
    range (I) Sub-interval of this *curve* to be bounded.
  */
  OdGeBoundBlock3d boundBlock (
    const OdGeInterval& range) const;

  /**
    Description:
    Returns the bounding box whose edges are aligned with the coordinate axes.

    Arguments:
    range (I) Interval of this *curve* to be bounded.
  */
  OdGeBoundBlock3d orthoBoundBlock () const;

  OdGeBoundBlock3d orthoBoundBlock (
  const OdGeInterval& range) const;

  /*
  OdGeEntity3d* project (const OdGePlane& projectionPlane,
  const OdGeVector3d& projectDirection,
  const OdGeTol& tol = OdGeContext::gTol) const;
  OdGeEntity3d* orthoProject (const OdGePlane& projectionPlane,
  const OdGeTol& tol = OdGeContext::gTol) const;
  */

  // Tests if point is on *curve*.
  //
  /*bool isOn (const OdGePoint3d& point, const OdGeTol& tol = OdGeContext::gTol) const;
  bool isOn (const OdGePoint3d& point, double& param,
  const OdGeTol& tol = OdGeContext::gTol) const;
  bool isOn (double param, const OdGeTol& tol = OdGeContext::gTol) const;*/

  /**
    Description:
    Returns the parameter *value* of a *point*.

    Arguments:
    point (I) Point to be evaluated.
    tol (I) Geometric tolerance.

    Remarks:
    The returned parameters specify a *point* within tol of point.
    If point is not on this *curve*, the results are unpredictable.
    If you are not sure the *point* is on this *curve*, use 
    isOn () instead of this function.
    
    Note:
    As implemented, this function does nothing but
    return 0.
    It will be fully implemented in a future *release*.
    
  */
  virtual double paramOf (
    const OdGePoint3d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
  Description:
    Returns one or more trimmed offset curves.

    Arguments:
    distance (I) Offset *distance*.
    planeNormal (I) A normal to the plane of this *curve*.
    offsetCurveList (O) Receives an array of trimmed offset curves.
    extensionType (I) How curves will be extended at discontinuities of *type* C1.
    tol (I) Geometric tolerance.

    Remarks:
    The offsets are trimmed to eliminate self-intersecting loops.

    The *curve* is assumed to be planar, and planeNomal is assumed to be 
    normal to the *curve* plane.

    The direction of positive offset at any *point* on this *curve*
    is the cross product of planeNormal and the tangent to the
    *curve* at that *point*.

    The new operator is used to create the curves returned by 
    offsetCurveList. It is up to the caller to delete these curves. 
  */
  void getTrimmedOffset (
    double distance, 
    const OdGeVector3d& planeNormal, 
    OdArray<void*>& offsetCurveList, 
    OdGe::OffsetCrvExtType extensionType = OdGe::kFillet, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns true if and only if this *curve* is closed within the specified tolerance.

    Arguments:
    tol (I) Geometric tolerance.
  */
  virtual bool isClosed (
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns true if and only if this *curve* is planar, and returns
    the *plane* of this *curve*.

    Arguments:
    plane (O) Receives the *plane* of this *curve*.
    tol (I) Geometric tolerance.

    Remarks:
    Lines are considered planar; the returned *plane* is an
    arbitrary *plane* containing the *line*.
  */
  bool isPlanar (
    OdGePlane& plane, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns true, and a linear entity coincident with this *curve*,
    if and only if this *curve* is linear.

    Arguments:
    line (O) Receives the *line* coincident with this *curve*.
    tol (I) Geometric tolerance.

    Remarks:
    An infinite *line* is returned, even if this *curve* is bounded.
    
    Note:
    As implemented, this function does nothing but
    return false.
    It will be fully implemented in a future *release*.
    
  */
  bool isLinear (
    OdGeLine3d& line,
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns true if and only if the specified *curve*
    is coplanar with this *curve*, and returns the common *plane*.

    Arguments:
    curve (I) Any 3D *curve*.
    plane (O) Receives the *plane* of the curves.
    tol (I) Geometric tolerance.
  */
  bool isCoplanarWith (
    const OdGeCurve3d& curve, 
    OdGePlane& plane, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns true if and only if this *curve* is *periodic* for the specified *period*.

    Arguments:
    period (O) Receives the period of this *curve*. 
  */
  virtual bool isPeriodic (
    double& period) const;

  /**
    Description:
    Returns the *length* of this *curve* over the specified parameter range.

    Arguments:
    fromParam (I) Starting parameter *value*.
    toParam (I) Ending parameter *value*.
    tol (I) Geometric tolerance.
    
  */      
  virtual double length (
    double fromParam, 
    double toParam, 
    double tol = OdGeContext::gTol.equalPoint ()) const;

  /**
    Description:
    Returns the parameter of the *point* a specified distance 
    from the starting point corresponding to datumParam.

    Arguments:
    datumParam (I) Parameter corresponding to the start point.
    length (I) Distance along *curve* from the start point.
    posParamDir (I) True if and only if returned parameter is to be greater than dataParam.
    tol (I) Geometric tolerance.
  */      
  virtual double paramAtLength (
    double datumParam, 
    double length, 
    bool posParamDir = true, 
    double tol = OdGeContext::gTol.equalPoint ()) const;

  /**
    Description:
    Determines the *area* of this *curve* between the specified parameters. 
    Returns true (and a value) if and only if this *curve* is planar. 

    Arguments:
    startParam (I) Starting parameter *value*.
    endParam (I) Ending parameter *value*.
    value (O) Receives the *area*.
    tol (I) Geometric tolerance.
  */      
  bool area (
    double startParam, 
    double endParam, 
    double& value, 
    const OdGeTol& tol = OdGeContext::gTol) const;
  /**
  Description:
    Returns true if and only if this *curve* degenerates, and returns
    the entity or *type* of entity to which this *curve* degenerates.

    Arguments:
    degenerateType (O) Receives the *type* of *curve* to which this *curve* degenerates.
    pConvertedEntity (O) Receives a pointer to the object to which this *curve* degenerates.
    tol (I) Geometric tolerance.

    Remarks:
    If isDegenerate returns true, the returned object was created with the new operator, and it is the responsibility of the caller to delete it.
  */      
  bool isDegenerate (
    OdGe::EntityId& degenerateType, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  bool isDegenerate (
    OdGeEntity3d*& pConvertedEntity, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns pointers to the two curves that result from splitting this *curve* at the *point* corresponding 
    to param.

    Arguments:
    param (I) The parameter *value* corresponding to the split *point*.
    piece1 (O) Receives a pointer to the first *curve*.
    piece2 (O) Receives a pointer to the second *curve*.

    Remarks:
    If param corresponds to a *point* not on this *curve*,
    or to the start point or end point of this *curve*,
    piece1 and piece2 are set to NULL.

    The curves pointed to by piece1 and piece2 
    are created with the new command, and must be deleted by the caller. 

  */      
  void getSplitCurves (
    double param, 
    OdGeCurve3d* & piece1,
    OdGeCurve3d* & piece2) const;

  /**
    Description:
    Explodes this *curve* over the specified interval.

    Arguments:
    explodedCurves (O) Receives an array of pointers to the subcurves from the explosion.
    newExplodedCurves (O) Receives an array of flags which, if true, correspond to those explodedCurves the caller must delete.
    interval (I) Interval to be exploded. Defaults to entire *curve*.

    Remarks:
    The original *curve* is not changed.
  */      
  bool explode (
    OdArray<void*>& explodedCurves, 
    OdArray<int>& newExplodedCurves, 
    const OdGeInterval* interval = NULL) const;

  /**
    Description:
    Returns the *point* on this *curve* locally closest to the specified *point*
    or *curve*, and the *point* on the other *curve* locally closest to this *curve*.

    Arguments:
    point (I) Any 3D *point*.
    curve (I) Any 3D *curve*.
    approxPntOnThisCrv (I/O) Approximate *point* on this *curve*.
    approxPntOnOtherCrv (I/O) Approximate *point* on other *curve*.
    nbhd1 (I) The *point* on this *curve* must lie in this interval.
    nbhd2 (I) The *point* on the other *curve* must lie in this interval.
    tol (I) Geometric tolerance.
  */      
  void getLocalClosestPoints (
    const OdGePoint3d& point,
    OdGePointOnCurve3d& approxPntOnThisCrv,
    const OdGeInterval* nbhd1 = 0,
    const OdGeTol& tol = OdGeContext::gTol) const;

  void getLocalClosestPoints (
    const OdGeCurve3d& curve,
    OdGePointOnCurve3d& approxPntOnThisCrv,
    OdGePointOnCurve3d& approxPntOnOtherCrv,
    const OdGeInterval* nbhd1 = 0,
    const OdGeInterval* nbhd2 = 0,
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns true, and the start point, if and only if the parametric *interval* of this *curve* has a lower bound.

    Arguments:
    startPoint (O) Receives the start point.
  */
  virtual bool hasStartPoint (
    OdGePoint3d& startPoint) const = 0;

  /**
    Description:
    Returns true, and the end point,  if and only if the parametric *interval* of this *curve* has an upper bound.

    Arguments:
    endPoint (O) Receives the end point.
  */
  virtual bool hasEndPoint (
    OdGePoint3d& endPoint) const = 0;

  /**
  Description:
    Returns the *point* on this *curve* corresponding to the specified parameter *value*,
    and the *derivatives* at that *point*.

    Arguments:
    param (I) Parameter to be evaluated.
    numDeriv (I) Number of *derivatives* to be computed.
    derivatives (O) Receives an array of *derivatives* at the *point* corresponding to param.

  */
  virtual OdGePoint3d evalPoint (
    double param) const = 0;
  virtual OdGePoint3d evalPoint (
    double param, 
    int numDeriv, 
    OdGeVector3dArray& derivatives) const = 0;

  /**
    Description:
    Appends sample points along this *curve* and their parameter values to the specified arrays.

    Arguments:
    fromParam (I) Starting parameter *value*.
    toParam (I) Ending parameter *value*.
    paramInterval (I) Pointer to the parametric interval.
    approxEps (I) Approximate spacing along a *curve*.
    numSample (I) Number of samples.
    pointArray (O) Returns an array of sample points.
    pParamArray (O) Receives an array of parameters at each *point*.

    Remarks:
    If paramInterval is NULL, the current *curve* interval will be used.

  */
  virtual void appendSamplePoints (
    double fromParam, 
    double toParam, 
    double approxEps, 
    OdGePoint3dArray& pointArray, 
    OdGeDoubleArray* pParamArray = 0) const = 0;

  virtual void appendSamplePoints (
    int numSample, 
    OdGePoint3dArray& pointArray) const;

  void appendSamplePoints (
    const OdGeInterval *paramInterval, 
    double approxEps, 
    OdGePoint3dArray& pointArray, 
    OdGeDoubleArray* pParamArray = 0) const
  {
    OdGeInterval i;

    if (!paramInterval)
    {
      getInterval (i);
      paramInterval = &i;
    }

    if (paramInterval->isBounded ())
    {
      appendSamplePoints (paramInterval->lowerBound (), paramInterval->upperBound (), approxEps, pointArray, pParamArray);
    }
  }

  /**
    Description:
    Returns sample points along this *curve* and their parameter values in the specified arrays.

    Arguments:
    fromParam (I) Starting parameter *value*.
    toParam (I) Ending parameter *value*.
    paramInterval (I) Pointer to the parametric interval.
    approxEps (I) Approximate spacing along a *curve*.
    numSample (I) Number of samples.
    pointArray (O) Returns an array of sample points.
    pParamArray (O) Receives an array of parameters at each *point*.

    Remarks:
    If paramInterval is NULL, the current *curve* interval will be used.
  */
  void getSamplePoints (
    double fromParam, 
    double toParam, 
    double approxEps, 
    OdGePoint3dArray& pointArray, 
    OdGeDoubleArray* pParamArray = 0) const
  {
    pointArray.clear ();
    if (pParamArray)
      pParamArray->clear ();
    appendSamplePoints (fromParam, toParam, approxEps, pointArray, pParamArray);
  }

  void getSamplePoints (
    const OdGeInterval *paramInterval,
    double approxEps, 
    OdGePoint3dArray& pointArray,
    OdGeDoubleArray* pParamArray = 0) const
  {
    OdGeInterval i;

    if (!paramInterval)
    {
      getInterval (i);
      paramInterval = &i;
    }

    if (paramInterval->isBounded ())
    {
      getSamplePoints (paramInterval->lowerBound (), paramInterval->upperBound (), approxEps, pointArray, pParamArray);
    }
  }

  void getSamplePoints (
    int numSample, 
    OdGePoint3dArray& pointArray) const
  {
    pointArray.clear ();
    appendSamplePoints (numSample, pointArray);
  }

  // Assignment operator.
  //
  // OdGeCurve3d&   operator= (const OdGeCurve3d& curve);

protected:
  /**
    Arguments:
    source (I) Object to be cloned.
  */
  OdGeCurve3d () {}
  OdGeCurve3d (
    const OdGeCurve3d& source);
};


#include "DD_PackPop.h"

#endif


