///////////////////////////////////////////////////////////////////////////////
// Copyright � 2002, Open Design Alliance Inc. ("Open Design") 
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
//      DWGdirect � 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef OD_GECYLNDR_H
#define OD_GECYLNDR_H /* {Secret} */



#include "OdPlatformSettings.h"
#include "GeSurface.h"
#include "GeInterval.h"

class OdGeCircArc3d;

#include "DD_PackPush.h"

/**
    Description: 
    This class represents right circular cylinders.

    Remarks:
    A right circular cylinder is defined by its

    o   *radius*
    o   axis of symmetry
    o   *origin* (a *point* on the axis)

    It is generated by revolving a line parallel to the axis of symmetry,
    at a distance of *radius*. 

    The cylinder is parameterized as follows:

    Parameter V is the angle of revolution measured from the refAxis
    (an axis perpendicular to the axis of symmetry).  For a closed cone,
    it defaults to [-OdaPI, OdaPI).  The right hand rule applied along the
    direction of the axis of symmetry defines the positive direction
    of V.  The surface is periodic in V with a period of Oda2PI.

    Parameter U varies along the axis of symmetry.  U is dimensionless,
    and increases in the direction of the axis of symmetry. 
    U = 0 corresponds to center of the base of this cylinder, 
    and U = 1 corresponds to the center of the top of this cylinder.  
    
    [umin, umax] x [vmin, vmax] defines a four sided cylindrical
    patch bounded by two straight lines (at vmin and vmax), and
    two circular arcs (at umin and umax).  The following constraints
    apply to the definition of a cylindrical patch:

    o umin < umax
    o |vmax - vmin| <= Oda2PI.
    o radius > 0.0

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeCylinder : public OdGeSurface
{
public:
  /**
    Arguments:
    radius (I) Radius of this cylinder. 
    origin (I) A *point* on the axis of symmetry.
    axisOfSymmetry (I) Axis of symmetry (rotation).
    refAxis (I) Defines angle 0 of circular end.
    height (I) Height interval of this cylinder.
    startAng (I) Start angle.
    endAng (I) End angle.
    Note:
    All angles are expressed in radians.

  */
  OdGeCylinder();
  OdGeCylinder(
    double radius, 
    const OdGePoint3d& origin,
    const OdGeVector3d& axisOfSymmetry);
  OdGeCylinder(
    double radius, 
    const OdGePoint3d& origin,
    const OdGeVector3d& axisOfSymmetry,
    const OdGeVector3d& refAxis,
    const OdGeInterval& height,
    double startAng, 
    double endAng);

  // Run time type information.

  bool isKindOf(
    OdGe::EntityId entType) const;
  OdGe::EntityId type() const;

  virtual OdGePoint2d paramOf(
    const OdGePoint3d& point,
    const OdGeTol& tol = OdGeContext::gTol) const;

  virtual OdGePoint3d evalPoint(
    const OdGePoint2d& param) const;
  virtual OdGePoint3d evalPoint(
    const OdGePoint2d& param, 
    int numDeriv,
    OdGeVector3dArray& derivatives, 
    OdGeVector3d& normal) const;

  DD_USING(OdGeSurface::evalPoint);

  OdGeEntity3d* copy() const;

  /**
    Description:
    Returns the *radius* of this cylinder.
  */
  double radius () const;

  /**
    Description:
    Returns the *origin* of this cylinder.
  */
  OdGePoint3d origin () const;

  /**
    Description:
    Returns the starting and ending angles of this cylinder.

    Arguments:
    startAng (O) Receives the start angle.
    endAng (O) Receives the end angle.
    Note:
    All angles are expressed in radians.

  */
  void getAngles (
    double& startAng, 
    double& endAng) const;

  /**
    Description:
    Returns the interval of the axis of symmetry.

    Arguments:
    height (O) Receives the interval of the axis of symmetry.
  */
  void getHeight (
    OdGeInterval& height) const;

  /**
    Description:
    Returns the *height* of this cylinder corresponding to
    the specified position on the U-axis.

    Remarks:
    Parameter U varies along the axis of symmetry.  U is dimensionless,
    and increases in the direction of the axis of symmetry. 

    Arguments:
    u (I) Position on the U-axis of this cylinder.
  */
  double heightAt (
    double u) const;

  /**
    Description:
    Returns the axis of symmetry of this cylinder.
  */
  OdGeVector3d axisOfSymmetry() const;

  /**
    Description:
    Returns the reference axis of this cylinder.
  */
  OdGeVector3d refAxis () const;

  /**
    Description:
    Returns true if and only if the *normal* to this surface
    is pointing outward.
  */
  bool isOuterNormal () const;

  /**
    Description
    Returns true if and only if this cylinder
    is a full circle within the specified tolerance.

    Arguments:
    tol (I) Geometric tolerance.
  */
  bool isClosed (
    const OdGeTol& tol = OdGeContext::gTol) const;

  bool isClosedInU (
    const OdGeTol& tol = OdGeContext::gTol) const;
  bool isClosedInV (
    const OdGeTol& tol = OdGeContext::gTol) const;

  /** Description:
    Sets OuterNormal.

    Arguments:
    isOuterNormal (I) Sets OuterNormal.
  */
  void setIsOuterNormal(
    bool isOuterNormal);

  /** Description:
    Sets the *radius* of this cylinder.

    Arguments:
    radius (I) Radius of this cylinder.
  */
  OdGeCylinder& setRadius (
    double radius);

  /** Description:
    Sets the starting and ending angles of this cylinder.

    Arguments:      
    startAng (I) Start angle.
    endAng (I) End angle.
    
    Note:
    All angles are expressed in radians.

  */
  OdGeCylinder& setAngles (
    double startAng, 
    double endAng);

  /**
    Description:
    Sets the *height* interval of this cylinder.

    Arguments:      
    height (I) Height *interval* of this cylinder.
  */
  OdGeCylinder& setHeight (
    const OdGeInterval& height);

  /**
    Description:
    Sets the parameters for this cylinder according to the arguments, 
    and returns a reference to this cylinder.

    Arguments:
    radius (I) Radius of this cylinder. 
    axisOfSymmetry (I) Axis of symmetry (rotation).
    refAxis (I) Defines angle 0 of circular end.
    height (I) Height *interval* of this cylinder.
    startAng (I) Start angle.
    endAng (I) End angle.
    Note:
    All angles are expressed in radians.

  */
  OdGeCylinder& set (
    double radius, 
    const OdGePoint3d& origin, 
    const OdGeVector3d& axisOfSym);
  OdGeCylinder& set (
    double radius, 
    const OdGePoint3d& origin,
    const OdGeVector3d& axisOfSymmetry,
    const OdGeVector3d& refAxis,
    const OdGeInterval& height,
    double startAng, 
    double endAng);

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
    
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  bool intersectWith (
    const OdGeLinearEnt3d& linEnt, 
    int& numInt,
    OdGePoint3d& p1, 
    OdGePoint3d& p2,
    const OdGeTol& tol = OdGeContext::gTol) const;

  void getEnvelope(
    OdGeInterval& intrvlU, 
    OdGeInterval& intrvlV) const;

  virtual bool project(
    const OdGePoint3d& p, 
    OdGePoint3d& projP) const;

private:
  double        m_radius,
                m_startAngle,
                m_endAngle;
  OdGeInterval  m_height;
  OdGeVector3d  m_axisOfSymmetry;
  OdGeVector3d  m_refAxis;
  OdGePoint3d   m_origin;
};

inline OdGePoint3d OdGeCylinder::origin() const { return m_origin; }

inline void OdGeCylinder::getAngles(
    double& startAng, 
    double& endAng) const
{ startAng = m_startAngle; endAng = m_endAngle; }

inline void OdGeCylinder::getHeight(
    OdGeInterval& height) const 
{ 
  height = m_height;
}

inline double OdGeCylinder::heightAt(
    double u) const
{
  return fabs(m_radius) * u;
}

inline OdGeVector3d OdGeCylinder::axisOfSymmetry() const { return m_axisOfSymmetry; }

inline OdGeVector3d OdGeCylinder::refAxis() const { return m_refAxis; }

#include "DD_PackPop.h"

#endif // OD_GECYLNDR_H

