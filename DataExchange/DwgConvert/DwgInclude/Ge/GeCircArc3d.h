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



#ifndef OD_GECIRCARC3D_H
#define OD_GECIRCARC3D_H /* {Secret} */

#include "OdPlatform.h"

#include "GeCurve3d.h"
#include "GePlane.h"

#include "DD_PackPush.h"

class OdGeLine3d;
class OdGeCircArc2d;
class OdGePlanarEnt;
class OdGeExtents3d;

/**
    Description: 
    A mathematical entity used to represent a circular arc in 3D space.

    Library: Ge

   {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeCircArc3d : public OdGeCurve3d
{
public:
  /**
    Arguments:
    center (I) Center of *arc*.
    normal (I) A vector *normal* to the *plane* of the *arc*
    radius (I) Radius of *arc*.
    startAng (I) Starting angle of *arc*.
    endAng (I) Ending angle of *arc*.
    refVec (I) The reference *vector* defining *arc* angle 0.
    isClockWise (I) If true, the *arc* is drawn clockwise, otherwise, counterclockwise.
    startPoint (I) Startpoint of *arc*.
    secondPoint (I) Second *point* on a 3-point *arc*.
    endPoint (I) Endpoint of *arc*.
    source (I) Object to be cloned.

    Remarks:
    To construct a circle, *set* endAng = startAng + Oda2PI
    
    Note:
    All angles are expressed in radians.
    
    startAng must be less than endAng. 
    
   */
  OdGeCircArc3d ()
    : m_vNormal (OdGeVector3d::kZAxis)
    , m_vRefVec (OdGeVector3d::kXAxis)
    , m_dRadius (1.)
    , m_dStartAngle (0.)
    , m_dInclAngle (Oda2PI)
  {}
  OdGeCircArc3d (
    const OdGeCircArc3d& source)
    : m_pCenter (source.m_pCenter)
    , m_vNormal (source.m_vNormal)
    , m_vRefVec (source.m_vRefVec)
    , m_dRadius (source.m_dRadius)
    , m_dStartAngle (source.m_dStartAngle)
    , m_dInclAngle (source.m_dInclAngle)
  {}

  OdGeCircArc3d (
    const OdGePoint3d& center, 
    const OdGeVector3d& normal, 
    double radius)
    : m_dStartAngle (0.)
    , m_dInclAngle (Oda2PI)
  {set (center, normal, radius);}

  OdGeCircArc3d (
    const OdGePoint3d& center, 
    const OdGeVector3d& normal,
    const OdGeVector3d& refVec, 
    double radius, double startAng = 0, 
    double endAng = Oda2PI)
  {set (center, normal, refVec, radius, startAng, endAng);}

  OdGeCircArc3d (
    const OdGePoint3d& startPoint, 
    const OdGePoint3d& secondPoint, 
    const OdGePoint3d& endPoint)
  {set (startPoint, secondPoint, endPoint);}

  OdGe::EntityId type () const 
  { return OdGe::kCircArc3d;}

  /**
  */
  bool isKindOf (
    OdGe::EntityId entType) const;

  /**
    Description:
    Returns the *point* on this *circle* closest
    to the specified *plane*, and the point
    on the *plane* closest to this *circle*.

    Arguments:
    plane (I) Any *plane*.
    pointOnPlane (O) Receives the closest *point* on *plane*.
    tol (I) Geometric tolerance.
  */  
  /**
    Note:
    As currently implemented, this function does nothing but
    assert in debug mode, and return the point (0,0,0).
    It will be fully implemented in a future *release*.
  */
  OdGePoint3d closestPointToPlane (
    const OdGePlanarEnt& plane,
    OdGePoint3d& pointOnPlane, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the intersections with other objects.

    Arguments:
    arc (I) Any 3D *arc*.
    line (I) Any 3D linear entity.
    plane (I) Any *plane*.
    numInt (O) Receives the number of intersections.
    p1 (O) Receives the first intersection *point*.
    p2 (O) Receives the second intersection *point*.
    tol (I) Geometric tolerance.
    
  */
   /**
    Note:
    As implemented, this function does nothing but
    assert in debug mode, and return false.
    It will be fully implemented in a future *release*.
  */
  bool intersectWith (
    const OdGeLinearEnt3d& line, 
    int& numInt, 
    OdGePoint3d& p1, 
    OdGePoint3d& p2,        
    const OdGeTol& tol = OdGeContext::gTol) const;

  bool intersectWith (
    const OdGeCircArc3d& arc, 
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
    pntOnArc1 (O) Receives the first intersection *point* on the arc. 
    pntOnArc2 (O) Receives the second intersection *point* on the arc.
    pntOnLine1 (O) Receives the first intersection *point* on the *line*.       
    pntOnLine2 (O) Receives the second intersection *point* on the *line*.
    tol (I) Geometric tolerance.
    
  */
  /**
    Note:
    As implemented, this function does nothing but
    assert in debug mode, and return false.
    It will be fully implemented in a future *release*.
  */
  bool projIntersectWith (
    const OdGeLinearEnt3d& line, 
    const OdGeVector3d& projDir, 
    int& numInt,
    OdGePoint3d& pntOnArc1, 
    OdGePoint3d& pntOnArc2,
    OdGePoint3d& pntOnLine1,        
    OdGePoint3d& pntOnLine2,
    const OdGeTol& tol = OdGeContext::gTol) const;

  /* Description:
    Returns true if and only if the specified *point* is
    on the full circle of this *arc*, the *tangent*
    at that *point*, and the *status* of the query.

    Arguments:
    point (I) The *point* on the full circle.
    line (O) Receives the *tangent* *line* at that *point*.
    tol (I) Geometric tolerance.
    status (O) Receives the *status* of the query.

    Remarks:
    Possible values for status are as follows:

    @untitled table
    kArg1TooBig
    kArg1InsideThis
    kArg1OnThis
    
    Note:
    As implemented, this function does nothing but
    assert in debug mode, and return false.
    It will be fully implemented in a future *release*.
  */
  bool tangent (
    const OdGePoint3d& point, 
    OdGeLine3d& line, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  bool tangent (
    const OdGePoint3d& point, 
    OdGeLine3d& line, 
    const OdGeTol& tol, 
    OdGeError& status) const;
  /**
    Description:
    Returns the *plane* of the *arc*.

    Arguments:
    plane (O) Receives the *plane* of the *arc*.
  */
  void getPlane (
    OdGePlane& plane) const;

  /**
    Description:
    Returns true if and only if the specified *point* lies inside the full *circle* of this *arc*, and is
    on the same plane as this *arc*.

    Arguments:
    point (I) Any 3D *point*.
    tol (I) Geometric tolerance.
  */
  bool isInside (
    const OdGePoint3d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the *center* of this *arc*.
  */
  OdGePoint3d center () const 
  { return m_pCenter;}

  /**
    Description:
    Returns the *vector* *normal* to the plane of this *arc*.
  */
  OdGeVector3d normal () const 
  { return m_vNormal;}

  /**
    Description:
    Returns the reference *vector* as a unit vector.
  */
  OdGeVector3d refVec () const 
  { return m_vRefVec;}

  /**
    Description:
    Returns the *radius* of this *arc*.
  */
  double radius () const 
  { return m_dRadius;}

  /**
    Description:
    Returns the starting angle measured from the reference *vector* in the *arc* direction.
    Note:
    All angles are expressed in radians.
  */
  double startAng () const 
  { return m_dStartAngle;}

  /**
    Description:
    Returns the ending angle measured from the reference *vector* in the *arc* direction.
    Note:
    All angles are expressed in radians.
  */
  double endAng () const 
  { return m_dStartAngle + m_dInclAngle;}

  /**
    Description:
    Returns the start point of this *arc*.
  */
  OdGePoint3d startPoint () const;

  /**
    Description:
    Returns the end point of this *arc*.
  */
  OdGePoint3d endPoint () const;

  /**
    Description:
    Sets the *center* of this *arc*, and returns a reference to this *arc*. 
    
    Arguments:
    center (I) Center of *arc*.
  */
  OdGeCircArc3d& setCenter (
    const OdGePoint3d& center) 
  { m_pCenter = center; return *this;}

  /** 
    Description:
    Sets the *normal* and reference vectors for this *arc*. Returns a reference
    to this arc.

    Arguments:
    normal (I) A vector *normal* to the *plane* of the *arc*.
    refVec (I) The reference *vector* defining *arc* angle 0.
  */
  OdGeCircArc3d& setAxes (
    const OdGeVector3d& normal, 
    const OdGeVector3d& refVec)
  {m_vNormal = normal; m_vRefVec = refVec; return *this;}


  /**
    Description:
    Sets the *radius* of this *arc*, and returns a reference to this *arc*.

    Arguments:
    radius (I) Radius of *arc*.
  */
  OdGeCircArc3d& setRadius (
    double radius) 
  { m_dRadius = radius; return *this;}

  /**
    Description:
    Sets the starting and ending angles of this *arc*, and returns a reference to this *arc*.

    Arguments:
    startAng (I) Starting angle of *arc*.
    endAng (I) Ending angle of *arc*.
    Note:
    All angles are expressed in radians.
  */
  OdGeCircArc3d& setAngles (
    double startAng, 
    double endAng);

  /**
    Description:
    Sets the parameters for this *arc* according to the arguments, and returns a reference to this *arc*.

    Arguments:
    center (I) Center of *arc*.
    normal (I) A vector *normal* to the *plane* of the *arc*
    radius (I) Radius of *arc*.
    startAng (I) Starting angle of *arc*.
    endAng (I) Ending angle of *arc*.
    refVec (I) The reference *vector* defining *arc* angle 0.
    startPoint (I) Startpoint of *arc*.
    secondPoint (I) Second *point* on a 3-point *arc*.
    endPoint (I) Endpoint of *arc*.
    curve1 (I) First curve to define a *tangent* *arc*.
    curve2 (I) Second curve to define a *tangent* *arc*.
    curve3 (I) Third curve to define a *tangent* *arc*.
    status (O) Receives status of set().
    param1 (I) Parameter corresponding tangency *point* on curve1.
    param2 (I) Parameter corresponding tangency *point* on curve2.
    param3 (I) Parameter corresponding tangency *point* on curve3.
    success (O) Receives true if and only if the tan-tan-radius or
    tan-tan-tan curve could be constructed. If false,
    this *arc* is unmodified.

    Remarks:
    To construct a circle, set endAng = startAng + Oda2PI

    If bulgeFlag == true, then *bulge* is the maximum distance from the *arc* to a
    chord between the start and endpoints.

    If bulgeFlag == false, the *bulge* is the *tangent* of 1/4 the included angle of the *arc*.
   
    Note:
    All angles are expressed in radians.

    startAng must be less than endAng. 
  */
  OdGeCircArc3d& set (
    const OdGePoint3d& center,   
    const OdGeVector3d& normal, 
    double radius);

  OdGeCircArc3d& set (
    const OdGePoint3d& center, 
    const OdGeVector3d& normal,
    const OdGeVector3d& refVec, 
    double radius,  
    double startAng, 
    double endAng);

  OdGeCircArc3d& set (
    const OdGePoint3d& startPoint, 
    const OdGePoint3d& secondPoint,
    const OdGePoint3d& endPoint);

  OdGeCircArc3d& set (
    const OdGePoint3d& startPoint, 
    const OdGePoint3d& secondPoint,
    const OdGePoint3d& endPoint, 
    OdGeError& status);

  OdGeCircArc3d& set (
    const OdGeCurve3d& curve1,
    const OdGeCurve3d& curve2,
    double radius, 
    double& param1, 
    double& param2, 
    bool& success);

  OdGeCircArc3d& set (
    const OdGeCurve3d& curve1,   
    const OdGeCurve3d& curve2,
    const OdGeCurve3d& curve3, 
    double& param1, 
    double& param2, 
    double& param3, 
    bool& success);

  OdGeCircArc3d& operator = (
    const OdGeCircArc3d& arc);

  OdGeEntity3d& transformBy (
    const OdGeMatrix3d& xfm);

  /**
    Description:
    Returns the *length* of the *curve* over the specified parameter range.

    Arguments:
    fromParam (I) Starting parameter value.
    toParam (I) Ending parameter value.
    tol (I) Geometric tolerance.
  */      
  double length (
    double fromParam, 
    double toParam, 
    double tol = OdGeContext::gTol.equalPoint ()) const;

  /**
    Description:
    Returns the parameter of the point a specified distance 
    from the starting point corresponding to dataParam.

    Arguments:
    datumParam (I) Parameter corresponding to the start point.
    length (I) Distance along *curve* from the start point.
    posParamDir (I) True if and only if returned parameter is to be greater than dataParam.
    tol (I) Geometric tolerance.
  */      
  double paramAtLength (
    double datumParam, 
    double length, 
    bool posParamDir = true, 
    double tol = OdGeContext::gTol.equalPoint ()) const;

  void appendSamplePoints (
    double fromParam, 
    double toParam, 
    double approxEps, 
    OdGePoint3dArray& pointArray, 
    OdGeDoubleArray* pParamArray = 0) const;

  DD_USING (OdGeCurve3d::appendSamplePoints);
  DD_USING (OdGeCurve3d::setInterval); 


  OdGePoint3d evalPoint (
    double param) const;
  OdGePoint3d evalPoint (
    double param, 
    int numDeriv, 
    OdGeVector3dArray& derivatives) const;

  double paramOf (
    const OdGePoint3d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  void getInterval (
    OdGeInterval& interval) const;

  bool setInterval (
    const OdGeInterval& interval);

  OdGeCurve3d& reverseParam ();

  bool isOn (
    const OdGePoint3d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  virtual bool isClosed (
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the geometric *extents* of this *arc*.

    Arguments:
    extents (O) Receives the geometric *extents*.
  */
  void getGeomExtents (
    OdGeExtents3d& extents) const;

  bool hasStartPoint (
    OdGePoint3d& startPoint) const;

  bool hasEndPoint (
    OdGePoint3d& endPoint) const;

private:
  OdGePoint3d m_pCenter;
  OdGeVector3d m_vNormal;
  OdGeVector3d m_vRefVec;
  double m_dRadius;
  double m_dStartAngle;
  double m_dInclAngle;
};

#include "DD_PackPop.h"

#endif


