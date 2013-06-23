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



#ifndef OD_GESURF_H
#define OD_GESURF_H /* {Secret} */


#include "GeEntity3d.h"
#include "GeVector3dArray.h"
#include "GePoint2d.h"

class OdGePoint2d;
class OdGeCurve3d;
class OdGePointOnCurve3d;
class OdGePointOnSurface;
class OdGePointOnSurfaceData;
class OdGeInterval;

#include "DD_PackPush.h"

/**
    Description:
    This class is the base class for all OdGe pametric surfaces.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeSurface : public OdGeEntity3d
{
public:

  virtual bool isKindOf (
    OdGe::EntityId entType) const
  { return entType == OdGe::kSurface || OdGeEntity3d::isKindOf(entType); }

  virtual OdGe::EntityId type () const { return OdGe::kSurface; }

  /**
    Description:
    Returns the 2D pair of parameter values of a *point* on this surface.

    Arguments:
    point (I) Point to be evaluated.
    tol (I) Geometric tolerance.

    Remarks:
    The returned parameters specify a point within tol of *point*.
    If point is not on this surface, the results are unpredictable.
    If you are not sure the point is on this surface, use 
    isOn() instead of this function.
  */
  virtual OdGePoint2d paramOf (
    const OdGePoint3d& point,
    const OdGeTol& tol = OdGeContext::gTol) const;


  bool isOn (
    const OdGePoint3d& , 
    const OdGeTol&  = OdGeContext::gTol) const
  { 
    return false;
  }

  /**
    Arguments:
    ParamPoint (O) Receives the 2D pair of parameter values at the *point*. 
  */
  bool isOn ( 
    const OdGePoint3d& point, 
    OdGePoint2d& paramPoint,
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the *point* on this surface closest to the specified *point*.

    Arguments:
    point (I) Any 3D *point*.
    tol (I) Geometric tolerance.
  */
  OdGePoint3d closestPointTo (
    const OdGePoint3d& point,
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the *point* on this surface closest to the specified *point*.

    Arguments:
    point (I) Any 3D *point*.
    pntOnSurface (O) Receives the closest *point* on surface to specified *point*. 
    tol (I) Geometric tolerance.
  */
  void getClosestPointTo (
    const OdGePoint3d& point, 
    OdGePointOnSurface& pntOnSurface,
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the distance to the *point* on this curve closest to the specified *point*.

    Arguments:
    point (I) Any 3D *point*.
    tol (I) Geometric tolerance.
  */
  double distanceTo (
    const OdGePoint3d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns true if and only if the *normal* of this surface has been reversed an odd
    number of times.
  */
  virtual bool isNormalReversed () const { return m_isNormalReversed; }

  /**
    Description:
    Reverses the *normal* of this surface and reurns a reference to this surface.
  */
  virtual OdGeSurface& reverseNormal () { m_isNormalReversed = !m_isNormalReversed; return *this; }

  /**
    Description:
    Returns the minimum rectangle in parameter space that contains the parameter
    domain of this surface.

    Arguments:
    intrvlU (O) Receives the u interval.
    intrvlV (O) Receives the v interval.
  */
  virtual void getEnvelope (
    OdGeInterval& intrvlU, 
    OdGeInterval& intrvlV) const = 0;

  /**
    Description:
    Returns true if and only if this surface is closed in the U direction.

    Arguments:
    tol (I) Geometric tolerance.
  */  
  virtual bool isClosedInU(
    const OdGeTol& tol = OdGeContext::gTol) const = 0;

  /**
    Description:
    Returns true if and only if this surface is closed in the V direction.

    Arguments:
    tol (I) Geometric tolerance.
  */  
  virtual bool isClosedInV (
    const OdGeTol& tol = OdGeContext::gTol) const = 0;

  /**
    Description:
    Returns projP and true,
    if and only if there is a *point* on this surface, projP, where
    a *normal* to this surface passes through the point p.

    Arguments:
    p (I) Any 3D *point*.
    projP (O) Receives the *point* on this surface. 
  */
  virtual bool project(
    const OdGePoint3d& p, 
    OdGePoint3d& projP) const;

  /**
    Description:
    Returns the point corresponding to the parameter pair, as well as the
    *derivatives* and the *normal* at that *point*.

    Arguments:
    param (I) The parameter pair to be evaluated.
    numDeriv (I) The number of *derivatives* to be computed.
    derivatives (O) Receives an array of *derivatives* at the point corresponding to param.
    normal (O) Receives the *normal* at the point corresponding to param.
    
    Remarks:
    Derivatives are ordered as follows: du, dv, dudu, dvdv, dudv
  */
  virtual OdGePoint3d evalPoint (
    const OdGePoint2d& param) const = 0;
  virtual OdGePoint3d evalPoint (
    const OdGePoint2d& param, 
    int numDeriv,
    OdGeVector3dArray& derivatives) const;
  virtual OdGePoint3d evalPoint (
    const OdGePoint2d& param,
    int numDeriv,
    OdGeVector3dArray& derivatives, 
    OdGeVector3d& normal) const;

protected:
  OdGeSurface() : m_isNormalReversed(false) {}
private:
  bool m_isNormalReversed;
};

#include "DD_PackPop.h"

#endif // OD_GESURF_H


