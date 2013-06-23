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

// AE 02.09.2003 - Begin 

#ifndef OD_GEELLIPCONE_H
#define OD_GEELLIPCONE_H /* {Secret} */

//
// Description:
//



#include "GeSurface.h"
#include "GeInterval.h"
#include "OdPlatformSettings.h"

class OdGePoint3d;
class OdGeVector3d;
class OdGeCircArc3d;
class OdGeLinearEnt3d;

#include "DD_PackPush.h"

/**
    Description:
    This class represents right elliptical cones.

    Remarks:
    A elliptical cone is defined by its 

    o  major and minor radii
    o  *origin* (a *point* on the axis of symmetry)
    o  axis of symmetry
    o  major axis
    o  height

    The cross section containing the origin is refered to as the base.

    The major and minor radii must be greater than 0.  

    The cone may extend on either side of the base.

    The half angle is constrained to the interval (0, OdaPI/2),
    and is measured from the symmetric axis of this elliptical cone to a *point* on the major axis.

    A negative angle results in an *apex* on the opposite direction of the
    symmetric axis.

    Parameter V is the angle of revolution measured from the refAxis
    (an axis perpendicular to the axis of symmetry).  For a closed cone,
    it defaults to [-OdaPI, OdaPI).  The right hand rule applied along the
    direction of the axis of symmetry defines the positive direction
    of v.  The surface is *periodic* in v with a period of Oda2PI.

    The angle of *point* on an ellipse is measured by projecting
    the *point* on the ellipse perpendicular to major axis onto a
    circle whose *center* is the *center* of the *ellipse* and whose
    radius is the major radius of the *ellipse*.

    The angle between the major axis of the ellipse, and a vector from
    the *center* of the *ellipse* to the intersection point with the circle, 
    measured counterclockwise, is the angle of the *point* on the ellipse.

    Parameter U varies along the axis of symmetry.  U is dimensionless,
    and increases in the direction of the axis of symmetry. 
    U = 0 corresponds to center of the base of this cone, 
    and U = 1 corresponds to the apex of this cone. 

    [umin, umax] x [vmin, vmax] defines a four sided conical patch
    bounded by two straight lines (at angles vmin and vmax), and two
    circular arcs (at umin and umax).  The following must be observed
    when defining a cone:

    o umin < umax
    o |vmax - vmin| <= Oda2PI
    o majorRadius > 0.0
    o minorRadius > 0.0

    The *height* interval of this elliptical cone is specified relative to its origin
    (with the height increasing in the direction of the symmetric
    axis).

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeEllipCone : public OdGeSurface
{
public:
  /**
    Arguments:
    cosineAngle (I) The cosine of the angle between the generating line passing
    through the major axis, and the axis of symmetry.
    sineAngle (I) The sine of the angle between the generating line passing
    through the major axis, and the axis of symmetry.
    majorRadius (I) The major radius of this elliptical cone.
    minorRadius (I) The minor radius of this elliptical cone.
    baseOrigin (I) The *origin* of this elliptical cone.
    axisOfSymmetry (I) Axis of symmetry (rotation).
    majorAxis (I) The major axis of this elliptical cone.
    height (I) Height *interval* of this elliptical cone.
    startAng (I) Starting angle of this elliptical cone.
    endAng (I) Ending angle of this elliptical cone.
    Note:
    All angles are expressed in radians.
  */
  OdGeEllipCone ();
  OdGeEllipCone (
    double cosineAngle, 
    double sineAngle,
    const  OdGePoint3d& origin, 
    double minorRadius,
    double majorRadius, 
    const OdGeVector3d& axisOfSymmetry);
  OdGeEllipCone (
    double cosineAngle, 
    double sineAngle,
    const OdGePoint3d& baseOrigin, 
    double minorRadius,
    double majorRadius,
    const OdGeVector3d& axisOfSymmetry,
    const OdGeVector3d& majorAxis, 
    const OdGeInterval& height,
    double startAng, 
    double endAng);

  bool isKindOf (
    OdGe::EntityId entType) const;
  OdGe::EntityId type () const;

  OdGeEntity3d* copy () const;

  // Geometric properties.
  //

  /**
    Description:
    Returns the ratio of the minor to the major radius of this elliptical cone.
  */
  double radiusRatio () const { return m_minorRadius / m_majorRadius; }
  
  /**
    Description:
    Returns the minor radius of this elliptical cone.
  */
  double minorRadius () const {return m_minorRadius; }

  /**
    Description:
    Returns the major radius of this elliptical cone.
  */
  double majorRadius () const { return m_majorRadius; }

  /**
    Description:
    Returns the center of the base.
  */
  OdGePoint3d baseCenter () const { return m_baseOrigin; }

  /**
    Description:
    Returns the starting and ending angles of this elliptical cone.

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
    Returns the angle between the generating line passing through the major axis, and the axis of symmetry.
  */
  double halfAngle () const;

  /**
    Description:
    Returns the angle between the generating line passing through the major axis, and the axis of symmetry.
  */
  void getHalfAngle (
    double& cosineAngle, 
    double& sineAngle) const
    { cosineAngle = m_cosineAngle; sineAngle = m_sineAngle; }


  /**
    Description:
    Returns the interval of the axis of symmetry.

    Arguments:
    height (O) Receives the interval of the axis of symmetry.
  */
  void getHeight (
    OdGeInterval& height) const
    { height = m_height; }

  /**
    Description:
    Returns the *height* of this elliptical cone corresponding to
    the specified position on the U-axis.

    Remarks:
    Parameter U varies along the axis of symmetry.  U is dimensionless,
    and increases in the direction of the axis of symmetry. 

    Arguments:
    u (I) Position on the U-axis of this elliptical cone.
  */
  double heightAt (
    double u) const { return u; }

  /**
    Description:
    Returns the axis of symmetry of this elliptical cone.
  */
  OdGeVector3d axisOfSymmetry () const { return m_axisOfSymmetry; }


  /**
    Description:
    Returns the major axis of this elliptical cone.
  */
  OdGeVector3d majorAxis () const { return m_majorAxis;} 

  /**
    Description:
    Returns the minor axis of this elliptical cone.
  */
  OdGeVector3d minorAxis () const;

  /**
    Description:
    Returns the *apex* of this elliptical cone.
  */
  OdGePoint3d apex () const
  { 
      return m_baseOrigin - m_axisOfSymmetry * (majorRadius () * fabs (m_cosineAngle) / m_sineAngle); 
  }  

  /**
    Description
    Returns true if and only if the base of this elliptical cone
    is a full ellipse within the specified tolerance.

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

  /** Description:
    Sets minor radius of this elliptical cone.

    Arguments:
    minorRadius (I) The minor radius of this elliptical cone.
  */
  OdGeEllipCone& setMinorRadius (
    double minorRadius);

  /** Description:
    Sets major radius of this elliptical cone.

    Arguments:
    majorRadius (I) The major radius of this elliptical cone.
  */
  OdGeEllipCone& setMajorRadius (
    double majorRadius);


  /** Description:
    Sets the starting and ending angles of this elliptical cone.

    Arguments:      
    startAng (I) Start angle in radians.
    endAng (I) End angle in radians.
  */
  OdGeEllipCone& setAngles (
    double startAng, 
    double endAng);

  /**
    Description:
    Sets the *height* interval of this elliptical cone.

    Arguments:      
    height (I) Height *interval* of this elliptical cone.
  */
  OdGeEllipCone& setHeight (
    const OdGeInterval& height);

  /**
    Description:
    Sets the parameters for this elliptical cone according to the arguments, 
    and returns a reference to this elliptical cone.

    Arguments:
    cosineAngle (I) The cosine of the angle between the generating line passing
    through the major axis, and the axis of symmetry.
    sineAngle (I) The sine of the angle between the generating line passing
    through the major axis, and the axis of symmetry.
    majorRadius (I) The major radius of this elliptical cone.
    minorRadius (I) The minor radius of this elliptical cone.
    origin (I) The *origin* of this elliptical cone.
    axisOfSymmetry (I) Axis of symmetry (rotation).
    majorAxis (I) The major axis of this elliptical cone.
    height (I) Height *interval* of this elliptical cone.
    startAng (I) Starting angle of this elliptical cone.
    endAng (I) Ending angle of this elliptical cone in.
    Note:
    All angles are expressed in radians.

  */
  OdGeEllipCone& set (
    double cosineAngle, 
    double sineAngle,
    const  OdGePoint3d& center,
    double minorRadius, 
    double majorRadius,
    const  OdGeVector3d& axisOfSymmetry);
  OdGeEllipCone& set (
    double cosineAngle, 
    double sineAngle,
    const  OdGePoint3d& center,
    double minorRadius, 
    double majorRadius,
    const  OdGeVector3d& axisOfSymmetry,
    const  OdGeVector3d& majorAxis,
    const  OdGeInterval& height,
    double startAng, 
    double endAng);

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

  /**
    Note:
    As implemented, this function does nothing but
    return false.
    It will be fully implemented in a future *release*.
  */
  virtual bool project (
    const OdGePoint3d& p, 
    OdGePoint3d& projP) const;

private:
  double m_sineAngle;
  double m_cosineAngle;
  double m_minorRadius;
  double m_majorRadius;
  double m_startAngle;
  double m_endAngle;

  OdGeInterval m_height;
  OdGeVector3d m_axisOfSymmetry;
  OdGeVector3d m_majorAxis;
  OdGePoint3d m_baseOrigin;
};

#include "DD_PackPop.h"

#endif // OD_GEELLIPCONE_H

// AE - End 

