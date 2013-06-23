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



#ifndef OD_GECONE_H
#define OD_GECONE_H /* {Secret} */

//
// This file implements the class OdGeCone, a representation for
// a single right circular cone.  It is defined by its axis of
// symmetry, height, half angle (the angle between the generating
// line and the axis of symmetry), its origin which is a *point* on
// the axis of symmetry, and the radius at the origin.  The cross
// section containing the origin is refered to as the base.  The
// radius at the base should not be zero.  this cone may extend on
// either side of the base.
// The fabs (halfAngle) is constrained to the interval (0, OdaPI/2),
// and is measured from the symmetric axis of this cone.  A positive
// angle results in an *apex* on the opposite direction of the
// symmetric axis, and vice versa for a negative angle.
// Parameter v is the angle of revolution measured from the refAxis
// (an axis perpendicular to the axis of symmetry).  For a closed cone,
// it defaults to [-OdaPI, OdaPI).  Right hand rule applied along the
// direction of the axis of symmetry defines the positive direction
// of v.  The surface is periodic in v with a period of Oda2PI.
// Parameter U varies along the generating line.  U = 0 correspond
// to the base of this cone.  U is dimensionless and increases in the
// direction the axis of symmetry.  It is scaled against the initial
// base radius of this cone.
// [umin, umax] x [vmin, vmax] defines a four sided conical patch
// bounded by two straight lines (at angles umin and umax), and two
// circular arcs (at vmin and vmax).  Following must be observed
// when defining a cone.
// umin < umax and |umin - umax| <= Oda2PI.
// Base radius > 0.0
// The hight of this cone is specified relative to its origin
// (with the height increasing in the direction of the symmetric
// axis).
//

#include "OdPlatformSettings.h"
#include "GeSurface.h"
#include "GeInterval.h"

class OdGePoint3d;
class OdGeVector3d;
class OdGeCircArc3d;
class OdGeLinearEnt3d;

#include "DD_PackPush.h"

/**
    Description:
    This class represents right circular cones.

    Remarks:
    A right circular cone is defined by its 

    o axis of symmetry
    o height
    o half angle (the angle between the generating line and the axis of symmetry)
    o origin (a *point* on the axis of symmetry)
    o radius at the origin.  

    The cross section containing the origin is refered to as the base. The radius at the base cannot be zero.  
    This cone may extend on either side of the base.

    The half angle is constrained to the interval (0, OdaPI/2),
    and is measured from the symmetric axis of this cone.
    A negative angle results in an *apex* on the opposite direction of the
    symmetric axis.

    Parameter V is the angle of revolution measured from the refAxis
    (an axis perpendicular to the axis of symmetry).  For a closed cone,
    it defaults to [-OdaPI, OdaPI).  The right hand rule applied along the
    direction of the axis of symmetry defines the positive direction
    of V.  The surface is periodic in V with a period of Oda2PI.

    Parameter U varies along the axis of symmetry.  U is dimensionless,
    and increases in the direction of the axis of symmetry. 
    U = 0 corresponds to center of the base of this cone, 
    and U = 1 corresponds to the apex of this cone. 

    [umin, umax] x [vmin, vmax] defines a four sided conical patch
    bounded by two straight lines (at angles vmin and vmax), and two
    circular arcs (at umin and umax).  The following must be observed
    when defining a cone:

    o umin < umax
    o |vmax - vmin| <= Oda2PI.
    o baseRadius > 0.0

    The *height* interval of this cone is specified relative to its origin
    (with the height increasing in the direction of the symmetric
    axis).
    
    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeCone : public OdGeSurface
{
protected:
  /* { Secret } */
  double ConvertHeight2U (
    double h) const;
  /* { Secret } */
  OdGeInterval ConvertHeight2U (
    const OdGeInterval& height) const;
public:
  /**
    Arguments:
    cosineAngle (I) The cosine of the angle between the generating line and the axis of symmetry.
    sineAngle (I) The sine of the angle between the generating line and the axis of symmetry.
    baseOrigin (I) Center of the base.
    baseRadius (I) Radius of the base. 
    axisOfSymmetry (I) Axis of symmetry (rotation).
    refAxis (I) Defines angle 0 of circular base.
    height (I) Height *interval* of this cone.
    startAng (I) Start angle.
    endAng (I) End angle.

    Remarks:
    The default constructor uses a half angle of 45°,
    a reference axis of (1,0,0), a baseOrigin of (0,0,0), and a base radius of 2.0.
    Note:
    All angles are expressed in radians.

  */
  OdGeCone ();
  OdGeCone (
    double cosineAngle, 
    double sineAngle,
    const  OdGePoint3d& baseOrigin, 
    double baseRadius,
    const  OdGeVector3d& axisOfSymmetry);
  OdGeCone (
    double cosineAngle, 
    double sineAngle,
    const  OdGePoint3d& baseOrigin, 
    double baseRadius,
    const  OdGeVector3d& axisOfSymmetry,
    const  OdGeVector3d& refAxis, 
    const  OdGeInterval& height,
    double startAng, 
    double endAng);

  //OdGeCone (const OdGeCone& cone);

  // Run time type information.

  bool isKindOf (
    OdGe::EntityId entType) const;
  OdGe::EntityId type () const;

  // Make a copy of the entity.

  OdGeEntity3d* copy () const;

  /**
  Description:
  Returns the *base* radius of this cone.
  */
  double baseRadius () const { return m_baseRadius; }

  /**
  Description:
  Returns the center of the base.
  */
  OdGePoint3d baseCenter () const { return m_baseOrigin; }

  /**
  Description:
  Returns the starting and ending angles of this cone.

  Arguments:
  startAng (O) Receives the start angle.
  endAng (O) Receives the end angle.
  Note:
  All angles are expressed in radians.

  */
  void getAngles (
    double& startAng, 
    double& endAng) const
    { startAng = m_startAngle; endAng = m_endAngle; }

  /**
    Description:
    Returns the angle between the generating line and the axis of symmetry.
    */
  double halfAngle () const;

  /**
    Description:
    Returns the cosine and the sine of the angle between the generating line and the axis of symmetry.

    Arguments:
    cosineAngle (O) Receives the cosine of the angle.
    sineAngle (O) Receives the sine of the angle.
    */
  void getHalfAngle (
    double& cosineAngle, 
    double& sineAngle) const
    { cosineAngle = m_cosineAngle; sineAngle = m_sineAngle; }

  /**
    Description:
    Returns the interval of the axis of symmetry.

    Arguments:
    height (O) Receives the interval.
  */
  void getHeight (
    OdGeInterval& height) const;

  /**
    Description:
    Returns the *height* of this cone corresponding to
    the specified position on the U-axis.

    Remarks:
    Parameter U varies along the axis of symmetry.  U is dimensionless,
    and increases in the direction of the axis of symmetry. 

    Arguments:
    u (I) Position on the U-axis of this cone.

  */
  double heightAt (
    double u) const;

  /**
    Description:
    Returns the axis of symmetry of this cone.
  */
  OdGeVector3d axisOfSymmetry () const { return m_axisOfSymmetry; }

  /**
    Description:
    Returns the reference axis of this cone.
  */
  OdGeVector3d refAxis () const { return m_refAxis; }


  /**
    Description:
    Returns the *apex* of this cone.
  */
  OdGePoint3d apex () const
  { return m_baseOrigin - m_axisOfSymmetry * (m_baseRadius * m_cosineAngle / m_sineAngle); }

  /**
    Description
    Returns true if and only if the base of this cone
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


  /**
    Description:
    Returns true if and only if the *normal* to this surface
    is pointing outward.
  */
  bool isOuterNormal () const;

  /**
    Description:
    Sets the base radius of this cone.

    Arguments:      
    baseRadius (I) Radius of the base. 
  */
  OdGeCone& setBaseRadius (
    double baseRadius)
  { m_baseRadius = baseRadius > 0. ? baseRadius : -baseRadius; return *this; }   

  /**
    Description:
    Sets the starting and ending angles of this cone.

    Arguments:      
    startAng (I) Start angle.
    endAng (I) End angle.
    Note:
    All angles are expressed in radians.

  */
  OdGeCone& setAngles (
    double startAng, 
    double endAng);

  /**
    Description:
    Sets the *height* interval of this cone.

    Arguments:      
    height (I) Height *interval* of this cone.
  */
  OdGeCone& setHeight (
    const OdGeInterval& height);

  /**
    Description:
    Sets the parameters for this cone according to the arguments, 
    and returns a reference to this cone.

    Arguments:
    cosineAngle (I) The cosine of the angle between the generating line and the axis of symmetry.
    sineAngle (I) The sine of the angle between the generating line and the axis of symmetry.
    baseOrigin (I) Center of the base.
    baseRadius (I) Radius of the base. 
    axisOfSymmetry (I) Axis of symmetry (rotation).
    refAxis (I) Defines angle 0 of circular base.
    height (I) Height *interval* of this cone.
    startAng (I) Start angle.
    endAng (I) End angle.
    
    Note:
    All angles are expressed in radians.

  */
  OdGeCone& set (double cosineAngle, 
    double sineAngle,
    const OdGePoint3d& baseCenter,
    double baseRadius,
    const OdGeVector3d& axisOfSymmetry);
  OdGeCone& set (double cosineAngle, 
    double sineAngle,
    const OdGePoint3d& baseCenter,
    double baseRadius,
    const OdGeVector3d& axisOfSymmetry,
    const OdGeVector3d& refAxis,
    const OdGeInterval& height,
    double startAng, 
    double endAng);
  //
  //OdGeCone&     operator = (const OdGeCone& cone);

  // Intersection with a linear entity
  //
  /*
  bool          intersectWith (const OdGeLinearEnt3d& linEnt, int& numInt,
  OdGePoint3d& p1, OdGePoint3d& p2,
  const OdGeTol& tol = OdGeContext::gTol) const;*/

  virtual void getEnvelope (
    OdGeInterval& intrvlU, 
    OdGeInterval& intrvlV) const;

  virtual OdGePoint3d evalPoint (
    const OdGePoint2d& param) const;
  virtual OdGePoint3d evalPoint (
    const OdGePoint2d& param, 
    int numDeriv,
    OdGeVector3dArray& derivatives, 
    OdGeVector3d& normal) const;
  DD_USING (OdGeSurface::evalPoint);

  virtual OdGePoint2d paramOf (
    const OdGePoint3d& ,
    const OdGeTol& tol = OdGeContext::gTol) const;

  virtual bool project ( 
    const OdGePoint3d& p, OdGePoint3d& projP ) const;
private:
  double        m_sineAngle,
    m_cosineAngle,
    m_baseRadius,
    m_startAngle,
    m_endAngle;
  OdGeInterval  m_height;
  OdGeVector3d  m_axisOfSymmetry;
  OdGeVector3d  m_refAxis;
  OdGePoint3d   m_baseOrigin;
};


#include "DD_PackPop.h"

#endif // OD_GECONE_H


