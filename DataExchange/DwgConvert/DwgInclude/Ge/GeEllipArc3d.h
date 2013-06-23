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



#ifndef OD_GEELL3D_H
#define OD_GEELL3D_H /* {Secret} */

class OdGeEllipArc2d;
class OdGeCircArc3d;
class OdGeLineEnt3d;
class OdGePlanarEnt;
class OdGeExtents3d;

#include "GeCurve3d.h"
#include "OdPlatformSettings.h"

#include "DD_PackPush.h"


/**
    Description:
    Converts an elliptical parameter to an *angle*.

    Arguments:
    param (I) Elliptical parameter.
    radiusRatio (I) The minorRadius:majorRadius ratio of the ellipse.
    
    Remarks:
    The *angle* of a *point* on an ellipse corresponding to a parameter
    of the ellipse is determined by projecting a vector perpendicular to the
    major axis from the parameter *point* on the parameter circle. The *angle* from the major axis to the intersection
    of the vector with the ellipse is the desired *angle*.

    library: Ge
*/
inline double angleFromParam (
    double param, 
    double radiusRatio)
{
  if (OdZero (param - Oda2PI))
    return Oda2PI;
  else
    return OD_ATAN2(radiusRatio*sin (param), cos (param));
}

/**
    Description:
    Converts an elliptical *angle* to a parameter.
    
    Arguments:
    angle (I) Elliptical *angle*.
    radiusRatio (I) The minorRadius:majorRadius ratio of the ellipse.
    
    Remarks:
    The parameter of a *point* on an ellipse corresponding to the
    angle of the *point* is determined by projecting a vector perpendicular to the
    major axis from the *point* on the ellipse to the parameter circle. 
    The *angle* from the major axis to the intersection
    of the vector with the circle is the desired parameter.

    library: Ge
*/
inline double paramFromAngle (
    double angle, 
    double radiusRatio)
{
  if ( OdZero(angle) ) return 0;
  if ( OdZero(angle - Oda2PI) ) return Oda2PI;
  double correction = 0;
  if ( fabs(angle) > OdaPI )
  {
    if ( angle > 0 )
      correction = floor((angle + OdaPI)/Oda2PI)*Oda2PI;
    else 
      correction = -floor((-angle + OdaPI)/Oda2PI)*Oda2PI;
  }
  return OD_ATAN2(sin (angle), radiusRatio*cos (angle)) + correction;
}


/**
    Description:
    This class represents 3D elliptical arcs and full ellipses in 3D space.

    Remarks:
    The angle of a *point* on an ellipse is measured by projecting
    the *point* along a vector perpendicular to the major axis onto a
    circle whose *center* is the *center* of this *ellipse* and whose
    radius is the major radius of this *ellipse*.

    The *angle* between the major axis of the *ellipse*, and a vector from
    the *center* of the *ellipse* to the intersection point with the circle, 
    measured counterclockwise about the crossproduct of the major and minor axes,
    is the *angle* of the *point* on the ellipse.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeEllipArc3d : public OdGeCurve3d
{
public:
  ~OdGeEllipArc3d ();

  /**
    Arguments:
    center (I) The *center* of the elliptical arc.
    majorAxis (I) The major axis of the elliptical arc.
    minorAxis (I) The minor axis of the elliptical arc.
    majorRadius (I) The major radius of the elliptical arc.
    minorRadius (I) The minor radius of the elliptical arc.
    startAng (I) Starting angle of the elliptical arc.
    endAng (I) Ending angle of the elliptical arc.
    source (I) Object to be cloned.
    Note:
    All angles are expressed in radians.

  */
  OdGeEllipArc3d ();

  OdGeEllipArc3d (
    const OdGeEllipArc3d& ell);

  OdGeEllipArc3d (
    const OdGeCircArc3d& source);

  OdGeEllipArc3d (
    const OdGePoint3d& center, 
    const OdGeVector3d& majorAxis,
    const OdGeVector3d& minorAxis, 
    double majorRadius,
    double minorRadius);

  OdGeEllipArc3d (
    const OdGePoint3d& center, 
    const OdGeVector3d& majorAxis,
    const OdGeVector3d& minorAxis, 
    double majorRadius,
    double minorRadius, 
    double startAng, 
    double endAng);

  /** Description:
    Returns the *point* on this *ellipse* closest
    to the specified *plane*, and the point
    on the *plane* closest to this *ellipse*.

    Arguments:
    plane (I) Any *plane*.
    pointOnPlane (O) Closes *point* on plane.
    tol (I) Geometric tolerance.
  */
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  OdGePoint3d closestPointToPlane (
    const OdGePlanarEnt& plane,
    OdGePoint3d& pointOnPlane,
    const OdGeTol& tol = OdGeContext::gTol) const;

  OdGeEntity3d* copy () const;

  OdGe::EntityId type () const;

  bool isKindOf (
    OdGe::EntityId entType) const;

  /**
    Description:
    Returns the intersections with other objects.

    Arguments:
    line (I) Any 3D linear entity.
    plane (I) Any *plane*.
    numInt (O) Receives the number of intersections.
    p1 (O) Receives the first intersection *point*.
    p2 (O) Receives the second intersection *point*.
    tol (I) Geometric tolerance.

    Remarks:
    o p1 is valid if and only if numInt >= 1.
    o p2 is valid if and only if numInt = 2.
  */
  /**
    Note:
    This function is not implemented for intersections with a *plane*, 
    and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  bool intersectWith (
    const OdGeLinearEnt3d& line, 
    int& numInt,
    OdGePoint3d& p1, 
    OdGePoint3d& p2,
    const OdGeTol& tol = OdGeContext::gTol) const;

  bool intersectWith (
    const OdGePlanarEnt& plane, 
    int& numInt,
    OdGePoint3d& p1, 
    OdGePoint3d& p2,
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the projected intersections with the specified linear entity.

    Arguments:
    line (I) Any 3D linear entity.
    projDir (I) Projection *direction*.
    numInt (O) Receives the number of intersections.
    pntOnEllipse1 (O) Receives the first intersection *point* on the ellipse. 
    pntOnEllipse2 (O) Receives the second intersection *point* on the ellipse.
    pntOnLine1 (O) Receives the first intersection *point* on the line.   
    pntOnLine2 (O) Receives the second intersection *point* on the line.
    tol (I) Geometric tolerance.
  */
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  bool projIntersectWith (
    const OdGeLinearEnt3d& line,
    const OdGeVector3d& projDir, 
    int& numInt,
    OdGePoint3d& pntOnEllipse1,
    OdGePoint3d& pntOnEllipse2,
    OdGePoint3d& pntOnLine1,
    OdGePoint3d& pntOnLine2,
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the *plane* of the *ellipse*.
  */
  void getPlane (
    OdGePlane& plane) const;

  /**
    Description:
    Returns true if and only if the major and minor radii of the *ellipse*
    are the same within the specified tolerance.

    Arguments:
    tol (I) Geometric tolerance.
  */
  bool isCircular (
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns true if and only if the specified *point* lies within
    the full ellipse, and on the *plane* of the *ellipse*.

    Arguments:
    point (I) Any 3D *point*.
    tol (I) Geometric tolerance.
  */
  bool isInside (
    const OdGePoint3d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the *center* of this elliptical arc.
  */
  OdGePoint3d center () const; 

  /**
    Description:
    Returns the minor *radius* of this elliptical arc.
  */
  double minorRadius () const;
  
  /**
    Description:
    Returns the major *radius* of this elliptical arc.
  */
  double majorRadius () const;

  /**
    Description:
    Returns the minor axis of this elliptical arc as a unit vector.
  */
  OdGeVector3d minorAxis () const;

  /**
    Description:
    Returns the major axis of this elliptical arc as a unit vector.
  */
  OdGeVector3d majorAxis () const;

  /**
    Description:
    Returns the *normal* to this elliptical arc as a unit vector. 

    Remarks:
    Positive angles are always drawn counterclockwise about this vector.
  */
  OdGeVector3d normal () const;

  /**
    Description:
    Returns the start angle measured from the major axis of this elliptical arc in the 
    *plane* of the *arc*.
    Note:
    All angles are expressed in radians.

  */
  double startAng () const;

  /**
    Description:
    Returns the end angle measured from the major axis of this elliptical arc in the 
    *plane* of the *arc*.
    Note:
    All angles are expressed in radians.

  */
  double endAng () const;

  /**
    Description:
    Returns the start point of this elliptical arc.
  */
  OdGePoint3d startPoint () const;

  /**
    Description:
    Returns the end point of this elliptical arc.
  */
  OdGePoint3d endPoint () const;

  /**
    Description:
    Sets the *center* of the elliptical arc.

    Arguments:
    center (I) The *center* of the elliptical arc.
  */
  OdGeEllipArc3d& setCenter (
    const OdGePoint3d& center);

  /**
    Description:
    Sets the minor radius of the elliptical arc.

    Arguments:
    minorRadius (I) The minor radius of the elliptical arc.
  */
  OdGeEllipArc3d& setMinorRadius (
    double rad);

  /**
    Description:
    Sets the major radius of the elliptical arc.

    Arguments:
    majorRadius (I) The major radius of the elliptical arc.
  */
  OdGeEllipArc3d& setMajorRadius (
    double rad);

  /**
    Description:
    Sets the major and minor axes of the elliptical arc.

    Arguments:
    majorAxis (I) The major axis of the elliptical arc.
    minorAxis (I) The minor axis of the elliptical arc.
  */
  OdGeEllipArc3d& setAxes (
    const OdGeVector3d& majorAxis,
    const OdGeVector3d& minorAxis);

  /**
    Description:
    Sets the starting and ending angles of the elliptical arc.

    Arguments:
    startAng (I) Starting angle of the elliptical arc.
    endAng (I) Ending angle of the elliptical arc.
    Note:
    All angles are expressed in radians.
  */
  OdGeEllipArc3d& setAngles (
    double startAng, 
    double endAng);

  /**
    Description:
    Sets the parameters for this elliptical arc according to the arguments, 
    and returns a reference to this elliptical arc.

    Arguments:
    arc (I) Any 3D circular *arc*.
    center (I) The *center* of the elliptical arc.
    majorAxis (I) The major axis of the elliptical arc.
    minorAxis (I) The minor axis of the elliptical arc.
    majorRadius (I) The major radius of the elliptical arc.
    minorRadius (I) The minor radius of the elliptical arc.
    startAng (I) Starting angle of the elliptical arc.
    endAng (I) Ending angle of the elliptical arc.
    Note:
    All angles are expressed in radians.

  */
  OdGeEllipArc3d& set (
    const OdGePoint3d& center,
    const OdGeVector3d& majorAxis,
    const OdGeVector3d& minorAxis,
    double majorRadius, 
    double minorRadius);

  OdGeEllipArc3d& set (
    const OdGePoint3d& center,
    const OdGeVector3d& majorAxis,
    const OdGeVector3d& minorAxis,
    double majorRadius, 
    double minorRadius,
    double startAng, 
    double endAng);

  OdGeEllipArc3d& set (
    const OdGeCircArc3d& arc);

  void appendSamplePoints (
    double fromParam, 
    double toParam, 
    double approxEps,
    OdGePoint3dArray& pointArray, 
    OdGeDoubleArray* pParamArray = 0) const;
  DD_USING (OdGeCurve3d::appendSamplePoints);

  OdGeEntity3d& transformBy (
    const OdGeMatrix3d& xfm);

  // functions making ellipse's axis orthogonal
  // this one requires error flag

  /**
    Description:
    Makes the minor axis orthogonal to the major axis.

    Arguments:
    tol (I) Geometric tolerance.
    flag (O) Receives the *status* of computation.

    Remarks:
    Possible values for flag are as follows:

    @untitled table
    kOk
    kDegenerateGeometry
    
    If flag is not specified, the following exception may be thrown:

    Throws:
    @table
    Exception           
    eDegenerateGeometry 
  */
  void correctAxis (
    const OdGeTol& tol, 
    OdGe::ErrorCondition& flag);
  void correctAxis (
    const OdGeTol& tol = OdGeContext::gTol);

  OdGePoint3d evalPoint (
    double param) const;

  OdGePoint3d evalPoint (
    double param, 
    int numDeriv,
    OdGeVector3dArray& derivatives) const;

  /**
    Description:
    Returns the *tangent* vector at the point specified by param.

    Arguments:
    param (I) Parameter corresponding to the *point* at which to compute the *tangent*.
  */
  OdGeVector3d tangentAt (
    double param) const;

  double paramOf (
    const OdGePoint3d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  void getInterval (
    OdGeInterval&) const;

  bool setInterval (
    const OdGeInterval& interval);
  DD_USING (OdGeCurve3d::setInterval);

  OdGeCurve3d& reverseParam ();

  /**
    Description:
    Projects this elliptical arc onto the specified plane along the specified vector.

    Arguments:
    plane (I) Plane on which this elliptical arc is to be projected.
    vect (I) Vector defining the projection direction.
    tol (I) Geometric tolerance.

    Remarks:
    The returned entity might not be the same *type* as the original one.

    The returned object is created with the new operator, and it is the responsibility of the caller to delete it.
  */
  virtual OdGeEntity3d* project (
    const OdGePlane& plane, 
    const OdGeVector3d& vect,
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Projects this elliptical arc onto the specified plane along a vector perpendicular to the specified plane.

    Arguments:
    plane (I) Plane on which this elliptical arc is to be projected.
    tol (I) Geometric tolerance.

    Remarks:
    The returned entity might not be the same *type* as the original one.

    The returned object is created with the new operator, and it is the responsibility of the caller to delete it. 
  */
  virtual OdGeEntity3d* orthoProject (
    const OdGePlane& plane,
    const OdGeTol& tol = OdGeContext::gTol) const;


  virtual bool isClosed (
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the geometric *extents* of this elliptical arc.

    Arguments:
    extents (O) Receives the geometric *extents*.
    */
  void getGeomExtents (
    OdGeExtents3d& extents) const;

  /** Description:
    Returns the parameters of the points (if any) on the elliptical arc,
    whose *tangents* are parallel to the specified tangent vector.

    Parameters:
    tan (I) A vector defining the tangent direction.
    params (O) Receives the array of tangent point parameters.

    Note:
    The parameters are appended to the specified array. You may wish to 
    initialize the params array with clear().
  */
  OdResult inverseTangent (
    OdGeVector3d tan, 
    OdGeDoubleArray& params) const;

  /** Description:
    Returns the parameters of the points (if any) on the elliptical arc,
    whose *tangents* are parallel to the specified reference plane.

    Parameters:
    refPlane (I) A reference plane.
    params (O) Receives the array of tangent point parameters.

    Note:
    The parameters are appended to the specified array. You may wish to 
    initialize the params array with clear().
  */
  OdResult inverseTangentPlane (
    const OdGePlane& refPlane, 
    OdGeDoubleArray& params) const;

  bool hasStartPoint (
    OdGePoint3d& startPoint) const;

  bool hasEndPoint (
    OdGePoint3d& endPoint) const;

  virtual bool isOn (
    const OdGePoint3d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;
protected:
  OdGePoint3d m_center;
  OdGeVector3d m_majorAxis, m_minorAxis;
  double m_startParam, m_includedParam;
};

#include "DD_PackPop.h"

#endif // OD_GEELL3D_H


