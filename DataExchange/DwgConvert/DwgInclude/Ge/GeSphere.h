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



#ifndef OD_GESPHERE_H
#define OD_GESPHERE_H /* {Secret} */

#include "GeSurface.h"

class OdGeCircArc3d;

#include "DD_PackPush.h"

/**
    Description:  
    This class represents spheres.  

    Remarks:
    A sphere is defined by its

    o radius
    o center
    o northAxis
    o refAxis

    northAxis defines the direction from the center to the north pole. 

    refAxis, a vector orthogonal to northAxis, the prime meridian.

    Parameter U is the latitude, which for a closed sphere defaults
    to the range [-OdaPI/2, OdaPI/2].  The lower bound maps to the south
    pole, zero is on the equator, and the upperbound maps to the north pole.

    Parameter V is the longitude, which for a closed sphere defaults
    to the range [-OdaPI, OdaPI).  Zero corresponds to the meridian defined by
    the refAxis of this sphere.

    The sphere is *periodic* in V with a period of Oda2PI.

    [umin, umax] x [vmin, vmax] defines a four sided spherical patch
    bounded by two arcs that are meridians of longitude, and two arcs
    that are parallels of latitude.  

    The following constraints apply when defining a spherical patch.

    o umin < umax and |umin - umax| <= Oda2PI.
    o vmin < vmax and |vmin - vmax| <= OdaPI.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeSphere : public OdGeSurface
{
public:
  /* Arguments:
    radius (I) The *radius* of this sphere.
    center (I) The origin of the this sphere.
    northAxis (I) The direction to the north pole.
    refAxis (I) The direction to the prime meridian.
    startAngleU (I) Starting longitude.
    endAngleU (I) Ending longitude.
    startAngleV (I) Starting latitude.
    endAngleV (I) Ending latitude.
  */
  OdGeSphere()
  { set(1.0, OdGePoint3d::kOrigin); }
  
  OdGeSphere(
    double radius, 
    const OdGePoint3d& center)
  { set(radius, center); }
    
  OdGeSphere(
    double radius, 
    const OdGePoint3d& center,
    const OdGeVector3d& northAxis, 
    const OdGeVector3d& refAxis,
    double startAngleU, 
    double endAngleU, 
    double startAngleV, 
    double endAngleV);

  //OdGeSphere(const OdGeSphere& sphere);

  bool isKindOf (OdGe::EntityId entType) const;
  OdGe::EntityId type () const;
  OdGeEntity3d* copy () const;

  void getEnvelope (OdGeInterval& intrvlU, OdGeInterval& intrvlV) const;

  /**
    Description:
    Returns the *radius* of this sphere.
  */
  double radius () const;

  /**
    Description:
    Returns the *center* of this sphere.
  */
  OdGePoint3d center () const { return m_center; }

  /**
    Description:
    Returns the start and end longitude.

    Arguments:
    startAngleU (O) Receives the start longitude.
    endAngleU (O) Receives the end longitude.
  */
  void getAnglesInU (
    double& startAngleU, 
    double& endAngleU) const
  { startAngleU = m_startAngleU; endAngleU = m_endAngleU; }

  /**
    Description:
    Returns the start and end latitude.

    Arguments:
    startAngleV (O) Receives the start latitude.
    endAngleV (O) Receives the end latitude.
  */
  void getAnglesInV (
    double& startAngleV, 
    double& endAngleV) const
  { startAngleV = m_startAngleV; endAngleV = m_endAngleV; }

  /**
    Description:
    Returns the direction to the north pole.

    Arguments:
    northAxis (O) Receives the direction to the north pole.
  */
  OdGeVector3d northAxis () const { return m_northAxis; }

  /**
    Description:
    Returns the direction to the north pole.

    Arguments:
    refAxis (O) Receives the direction to the prime meridian.
  */
  OdGeVector3d refAxis () const { return m_refAxis; }

  /**
    Description:
    Returns the location of the north pole.
  */
  OdGePoint3d northPole () const
  { return m_center + m_northAxis * (m_radius > 0.0 ? m_radius : -m_radius); }

  /**
    Description:
    Returns the location of the south pole.
  */
  OdGePoint3d southPole () const
  { return m_center - m_northAxis * (m_radius > 0.0 ? m_radius : -m_radius); }

  /**
    Description:
    Returns true if and only if the *normal* to this surface
    is pointing outward.
  */
  bool isOuterNormal () const;

  /**
    Description:
    Returns true if and only if the equator is full circle.

    Arguments:
    tol (I) Geometric tolerance.
  */  
  bool isClosed (
    const OdGeTol& tol = OdGeContext::gTol) const { return isClosedInV(tol); }

  bool isClosedInU (
    const OdGeTol& tol = OdGeContext::gTol) const;
  bool isClosedInV (
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Sets the *radius* of this sphere.

    Arguments:
    radius (I) The *radius* of this sphere.
  */
  OdGeSphere& setRadius (
    double radius)
  { m_radius = radius; return *this; }

  /**
    Description:
    Sets the startint and ending longitudes.

    Arguments:
    startAngleU (I) Starting longitude.
    endAngleU (I) Ending longitude.
  */
  OdGeSphere& setAnglesInU (
    double startAngleU, 
    double endAngleU)
  { m_startAngleU = startAngleU; m_endAngleU = endAngleU; return *this; }

  /**
    Description:
    Sets the starting and ending latitudes.

    Arguments:
    startAngleV (I) Starting latitude.
    endAngleV (I) Ending latitude.
  */
  OdGeSphere& setAnglesInV (
    double startAngleV, 
    double endAngleV)
  { m_startAngleV = startAngleV; m_endAngleV = endAngleV; return *this; }

  /**
    Description:
    Sets the parameters for this sphere according to the arguments, 
    and returns a reference to this sphere.

    Arguments:
    radius (I) The *radius* of this sphere.
    center (I) The origin of the this sphere.
    northAxis (I) The direction to the north pole.
    refAxis (I) The direction to the prime meridian.
    startAngleU (I) Starting longitude.
    endAngleU (I) Ending longitude.
    startAngleV (I) Starting latitude.
    endAngleV (I) Ending latitude.
  */
  OdGeSphere& set (
    double radius, 
    const OdGePoint3d& center);
  OdGeSphere& set (
    double radius, 
    const OdGePoint3d& center,
    const OdGeVector3d& northAxis,
    const OdGeVector3d& refAxis,
    double startAngleU,
    double endAngleU,
    double startAngleV,
    double endAngleV);

  virtual OdGePoint2d paramOf (
    const OdGePoint3d& point,
    const OdGeTol& tol = OdGeContext::gTol) const;

  virtual OdGePoint3d evalPoint (
    const OdGePoint2d& param) const;
  virtual OdGePoint3d evalPoint (
    const OdGePoint2d& param, 
    int numDeriv,
    OdGeVector3dArray& derivatives) const;
  virtual OdGePoint3d evalPoint (
    const OdGePoint2d& param, 
    int numDeriv,
    OdGeVector3dArray& derivatives, 
    OdGeVector3d& normal) const;

  // Assignment operator.
  //
  //OdGeSphere&    operator = (const OdGeSphere& sphere);

  /**
    Description:
    Returns true if and only if this cylinder intersects with
    a line entity, and returns the number of intersections and the
    points of intersection.

    Arguments:
    lineEnt (I) Any 3D *line* entity.
    numInt (O) Receives the number of intersections.
    p1 (O) Receives the first intersection *point*.
    p2 (O) Receives the second intersection *point*.
    tol (I) Geometric tolerance.

    Remarks:
    o p1 is valid if and only if numInt >= 1.
    o p2 is valid if and only if numInt = 2.
  */
  bool intersectWith (
    const OdGeLinearEnt3d& lineEnt, 
    int& numInt,
    OdGePoint3d& p1, 
    OdGePoint3d& p2,
    const OdGeTol& tol = OdGeContext::gTol) const;

protected:
  double m_radius,
    m_startAngleU,
    m_endAngleU,
    m_startAngleV,
    m_endAngleV;
  OdGePoint3d   m_center;
  OdGeVector3d  m_northAxis;
  OdGeVector3d  m_refAxis;
};

#include "DD_PackPop.h"

#endif // OD_GESPHERE_H


