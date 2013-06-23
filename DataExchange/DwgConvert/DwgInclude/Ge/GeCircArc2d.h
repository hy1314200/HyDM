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



#ifndef OD_GE_ARC2D_H
#define OD_GE_ARC2D_H /* {Secret} */

class OdGeLine2d;
class OdGeLinearEnt2d;
class OdGeExtents2d;

#include "OdPlatform.h"

#include "GeCurve2d.h"

#include "DD_PackPush.h"

/**
    Description:
    This class represents arcs and full circles in 2D space.

    Library: Ge

   {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeCircArc2d : public OdGeCurve2d
{
public:
  /**
    Arguments:
    center (I) Center of arc.
    radius (I) Radius of arc.
    startAng (I) Starting angle of arc.
    endAng (I) Ending angle of arc.
    refVec (I) The reference vector defining arc angle 0.
    startPoint (I) Startpoint of arc.
    secondPoint (I) Second *point* on a 3-point arc.
    endPoint (I) Endpoint of arc.
    bulge (I) Specifies the *bulge* of the arc.
    bulgeFlag (I) Specifies how *bulge* is to be interpreted.
    source (I) Object to be cloned.

    Remarks:
    The default constructor creates a circle with a *center* of (0,0) and a *radius* of 1.
    
    To construct a circle, *set* endAng = startAng + Oda2PI

    If bulgeFlag == true, then *bulge* is the maximum distance from the arc perpendicular to a
    chord between the start and endpoints.

    If bulgeFlag == false, the *bulge* is the *tangent* of 1/4 the included 
    angle of the arc, measured counterclockwise.

    Note:
    All angles are expressed in radians.
        
    startAng must be less than endAng. 

  */
  OdGeCircArc2d ()
    : m_vRefVector (OdGeVector2d::kXAxis),
    m_dStartAngle (0.0),
    m_dInclAngle (Oda2PI)
  {}

  OdGeCircArc2d (
    const OdGeCircArc2d& source)
    : m_pCenter (source.m_pCenter),
    m_vRefVector (source.m_vRefVector),
    m_dStartAngle (source.m_dStartAngle),
    m_dInclAngle (source.m_dInclAngle)
  {}

  OdGeCircArc2d (
    const OdGePoint2d& center, 
    double radius)
    : m_vRefVector (OdGeVector2d::kXAxis),
    m_dStartAngle (0.0),
    m_dInclAngle (Oda2PI)
  {set (center, radius);}

  OdGeCircArc2d (
    const OdGePoint2d& center, 
    double radius, 
    double startAng,
    double endAng, 
    const OdGeVector2d& refVec = OdGeVector2d::kXAxis,
    bool isClockWise = false)
  {set (center, radius, startAng, endAng, refVec, isClockWise);}

  OdGeCircArc2d (
    const OdGePoint2d& startPoint,
    const OdGePoint2d& secondPoint,
    const OdGePoint2d& endPoint)
    : m_vRefVector (OdGeVector2d::kXAxis)
  {set (startPoint, secondPoint, endPoint);}

  OdGeCircArc2d (
    const OdGePoint2d& startPoint, 
    const OdGePoint2d& endPoint, 
    double bulge, 
    bool bulgeFlag = true)
    : m_vRefVector (OdGeVector2d::kXAxis)
  {set (startPoint, endPoint, bulge, bulgeFlag);}

  OdGe::EntityId type () const  
  { return OdGe::kCircArc2d;}

  OdGeEntity2d* copy () const
  {
    return new OdGeCircArc2d (*this);
  }

  /**
    Description:
    Returns true if and only if this arc intersects
    with the specified arc or linear entity, the number of intersections, and
    the intersection points.

    Arguments:
    line (I) Any 2D linear entity.
    circarc (I) Any 2D arc.
    numInt (O) Receives the number of intersections with this *curve*.
    p1 (O) Receives the first intersection *point*.
    p2 (O) Receives the second intersection *point*.
    tol (I) Geometric tolerance.

    Remarks:
    o p1 has meaning if and only if numInt > 0. 
    o p2 has meaning if and only if numInt > 1.   
  */
  bool intersectWith (
    const OdGeLinearEnt2d& line, 
    int& numInt,
    OdGePoint2d& p1, 
    OdGePoint2d& p2, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  bool intersectWith (
    const OdGeCircArc2d& circarc, 
    int& numInt,
    OdGePoint2d& p1, 
    OdGePoint2d& p2, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /* Description:
    Returns true if and only if the specified *point* is
    on the full circle of this arc, the *tangent*
    at that *point*, and the *status* of the query.

    Arguments:
    point (I) The *point* on the full circle.
    line (O) Receives the *tangent* at that *point*.
    tol (I) Geometric tolerance.
    status (O) Receives the *status* of the query.

    Remarks:
    Possible values for status are as follows:

    @untitled table
    kArg1TooBig
    kArg1InsideThis
    kArg1OnThis
  */
  bool tangent (
    const OdGePoint2d& point, 
    OdGeLine2d& line,
    const OdGeTol& tol = OdGeContext::gTol) const;

  bool tangent (
    const OdGePoint2d& point, 
    OdGeLine2d& line,
    const OdGeTol& tol, 
    OdGeError& status) const;

  /**
    Description:  
    Returns true if and only if the input *point* lies within
    the full circle of this arc.

    Arguments:
    point (I) Any 2D *point*.
    tol (I) Geometric tolerance.
  */
  bool isInside (
    const OdGePoint2d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the *center* of this arc.
  */
  OdGePoint2d center () const
  { return m_pCenter;}

  /**
    Description:
    Returns the *radius* of this arc.
  */
  double radius () const 
  { return m_vRefVector.length ();}

  /**
    Description:
    Returns the starting angle measured from tje reference vector in the arc direction.
    Note:
    All angles are expressed in radians.

  */
  double startAng () const;

  /**
    Description:
    Returns the ending angle measured from the reference vector in the arc direction.
    Note:
    All angles are expressed in radians.

  */
  double endAng () const;

  /**
    Description:
    Returns the starting angle measured from the X-axis in the arc direction.
    
    Note:
    All angles are expressed in radians.
  */
  double startAngFromXAxis () const;

  /**
    Description:
    Returns the ending angle measured from the X-axis in the arc direction.
    Note:
    All angles are expressed in radians.
  */
  double endAngFromXAxis () const;

  /**
    Description:
    Returns true if and only if this arc is drawn clockwise from start point to end point.
  */
  bool isClockWise () const  
  { return m_dInclAngle < 0.;}

  /**
    Description:
    Returns the reference vector as a unit vector.
  */
  OdGeVector2d refVec () const;

  /**
    Description:
    Returns the start point of this arc.
  */
  OdGePoint2d startPoint () const;

  /**
    Description:
    Returns the end point of this arc.
  */
  OdGePoint2d endPoint () const;

  /**
    Description:
    Sets the *center* of this arc, and returns a reference to this arc.

    Arguments:
    center (I) Center of arc.
  */
  OdGeCircArc2d& setCenter (
    const OdGePoint2d& center) 
  { m_pCenter = center; return *this; }

  /**
    Description:
    Sets the *radius* of this arc, and returns a reference to this arc.

    Arguments:
    radius (I) Radius of arc.
  */
  OdGeCircArc2d& setRadius
 (double radius)           
  { m_vRefVector.normalize (); m_vRefVector *= radius; return *this;}


  /**
    Description:
    Sets the starting and ending angles of this arc, and returns a reference to this arc.

    Arguments:
    startAng (I) Starting angle of arc.
    endAng (I) Ending angle of arc.
    Note:
    All angles are expressed in radians.

  */
  OdGeCircArc2d& setAngles (
    double startAng, 
    double endAng) 
  { m_dStartAngle = startAng; m_dInclAngle = endAng - startAng; return *this; }

  /**
    Description:
    Reverses the direction of this arc while maintaining its endpoints, and returns a reference to this arc.
  */
  OdGeCircArc2d& setToComplement ();

  /**
    Description:
    Sets the reference vector of this arc, and returns a reference to this arc.

    Arguments:
    refVec (I) The reference vector defining arc angle 0.
  */
  OdGeCircArc2d& setRefVec (
    const OdGeVector2d& vect) 
  { double rad = m_vRefVector.length (); m_vRefVector = vect; m_vRefVector *= rad; return *this; }

  /**
    Description:
    Sets the parameters for this arc according to the arguments, and returns a reference to this arc.

    Arguments:
    center (I) Center of arc.
    radius (I) Radius of arc.
    startAng (I) Starting angle of arc.
    endAng (I) Ending angle of arc.
    status (O) Receives the *status* for this method.
    refVec (I) The reference vector defining arc angle 0.
    isClockWise (I) If true, the arc is drawn clockwise, otherwise, counterclockwise.
    startPoint (I) Startpoint of arc.
    secondPoint (I) Second *point* on a 3-point arc.
    endPoint (I) Endpoint of arc.
    bulge (I) Specifies the *bulge* of the arc.
    bulgeFlag (I) Specifies how *bulge* is to be interpreted.
    curve1 (I) First curve to define a *tangent* arc.
    curve2 (I) Second curve to define a *tangent* arc.
    curve3 (I) Third curve to define a *tangent* arc.
    param1 (O) Receives the parameter corresponding tangency *point* on curve1.
    param2 (O) Receives the parameter corresponding tangency *point* on curve2.
    param3 (O) Receives the parameter corresponding tangency *point* on curve2.
    success (O) Receives true if and only if the tan-tan-radius or
    tan-tan-tan curve could be constructed. If false,
    this arc is unmodified.

    Remarks:
    To construct a circle, set endAng = startAng + Oda2PI

    If bulgeFlag == true, then *bulge* is the maximum distance from the arc to a
    chord between the start and endpoints.

    If bulgeFlag == false, the *bulge* is the *tangent* of 1/4 the included angle of the arc.

    Possible values for status are as follows

    @untitled table
    kEqualArg1Arg2
    kEqualArg1Arg3
    kEqualArg2Arg3
    kLinearlyDependentArg1Arg2Arg3.
    
    Note:
    startAng must be less than endAng. 
    All angles are expressed in radians.
  */
  OdGeCircArc2d& set (
    const OdGePoint2d& center, 
    double radius);

  OdGeCircArc2d& set (
    const OdGePoint2d& center, 
    double radius, 
    double startAng, 
    double endAng,
    const OdGeVector2d& refVec = OdGeVector2d::kXAxis, 
    bool isClockWise = false);

  OdGeCircArc2d& set (
    const OdGePoint2d& startPoint, 
    const OdGePoint2d& secondPoint,
    const OdGePoint2d& endPoint);

  OdGeCircArc2d& set (
    const OdGePoint2d& startPoint,
    const OdGePoint2d& secondPoint,
    const OdGePoint2d& endPoint, 
    OdGeError& status);

  OdGeCircArc2d& set (
    const OdGePoint2d& startPoint, 
    const OdGePoint2d& endPoint, 
    double bulge, 
    bool bulgeFlag = true);

  OdGeCircArc2d& set (
    const OdGeCurve2d& curve1, 
    const OdGeCurve2d& curve2,
    double radius, 
    double& param1, 
    double& param2, 
    bool& success);
  OdGeCircArc2d& set (
    const OdGeCurve2d& curve1,
    const OdGeCurve2d& curve2, 
    const OdGeCurve2d& curve3,
    double& param1, 
    double& param2, 
    double& param3, 
    bool& success);

  //:>OdGeCircArc2d& operator = (const OdGeCircArc2d& arc);

  void getInterval (
    OdGeInterval& interval) const;

  virtual double paramOf (
    const OdGePoint2d& point,
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the *point* on the *curve* corresponding to the specified parameter value.
    
    Arguments:
    param (I) Parameter to be evaluated.
  */
  virtual OdGePoint2d evalPoint (
    double param) const;

  DD_USING(OdGeCurve2d::evalPoint);

  void appendSamplePoints (
    double fromParam, 
    double toParam,
    double approxEps, 
    OdGePoint2dArray& pointArray,
    OdGeDoubleArray* pParamArray = 0) const;

  virtual OdGeEntity2d& transformBy (
    const OdGeMatrix2d& xfm);

  /**
    Description:
    Returns the geometric *extents* of this arc.

    Arguments:
    extents (O) Receives the geometric *extents*.
  */
  void getGeomExtents (
    OdGeExtents2d& extents);

  bool hasStartPoint (
    OdGePoint2d& startPoint) const;

  bool hasEndPoint (
    OdGePoint2d& endPoint) const;

  bool isClosed (
    const OdGeTol& tol = OdGeContext::gTol) const;

  DD_USING (OdGeCurve2d::appendSamplePoints);

private:
  OdGePoint2d   m_pCenter;
  OdGeVector2d  m_vRefVector;

  //  double        m_dRadius;

  double        m_dStartAngle;
  double        m_dInclAngle;

  //  double        m_dRefAngle;
};

#include "DD_PackPop.h"

#endif // OD_GE_ARC2D_H


