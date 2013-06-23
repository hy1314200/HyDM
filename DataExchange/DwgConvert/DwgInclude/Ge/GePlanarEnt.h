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


#ifndef OD_GEPLANAR_H
#define OD_GEPLANAR_H /* {Secret} */

#include "GeSurface.h"
#include "GeInterval.h"
#include "OdPlatformSettings.h"

class OdGeLinearEnt3d;

#include "DD_PackPush.h"

/**
    Description:
    This class is the base class for all OdGe planes in 3D space.

    Remarks:
    A parametric *point* on the *plane* with parameters u and v maps to the *point* S(u,v) as follows

              S(u,v) = originOfPlanarEntity + (u * uAxis) + (v * vAxis)

    uAxis and vAxis need not be either normalized or perpendicular, but they must
    not be colinear.

    @table
    Parameter       Description                                   Computed as
    *origin*        Origin of *plane*.                            *origin*              
    axis1           A unit vector in the *plane*.                 uAxis.normal()                        
    axis2           A unit vector perpendicular to the *plane*.   uAxis.crossProduct(vAxis).normal()   

    The *plane* equation for *a* *plane* is as follows

              a * X + b * Y + c * Z + d = 0

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGePlanarEnt : public OdGeSurface
{
public:
  // OdGeSurface functions

  bool isNormalReversed () const;
  OdGeSurface& reverseNormal ();
  bool isClosedInU (
    const OdGeTol& tol = OdGeContext::gTol) const;
  bool isClosedInV (
    const OdGeTol& tol = OdGeContext::gTol) const;

  virtual OdGePoint3d evalPoint (
    const OdGePoint2d& param) const;
  virtual OdGePoint3d evalPoint (
    const OdGePoint2d& param, 
    int numDeriv,
    OdGeVector3dArray& derivatives, 
    OdGeVector3d& normal) const;
  DD_USING(OdGeSurface::evalPoint);

  void getEnvelope (
    OdGeInterval& intrvlU, 
    OdGeInterval& intrvlV) const;

  /**
    Description:
    Set the rectangle in parameter space that defines the parameter
    domain of this surface.

    Arguments:
    intrvlU (O) u interval
    intrvlV (O) v interval
  */
  virtual void setEnvelope (
    const OdGeInterval& intrvlU, 
    const OdGeInterval& intrvlV);

  bool isKindOf (
    OdGe::EntityId entType) const;
  OdGe::EntityId type () const;

  /**
    Description:
    Returns true and the intersection with the specified linear entity,
    if and only if the specified linear entity intersects with this *plane*.

    Arguments:
    line (I) Any 3D linear entity.
    point (O) Receives the *point* of intersection. 
    tol (I) Geometric tolerance.
  */
  bool intersectWith (
    const OdGeLinearEnt3d& line, 
    OdGePoint3d& point,
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the *point* on this *plane* closest to 
    the specified linear entity, and the point
    on that linear entity closest to this *plane*.

    Arguments:
    line (I) Any 3D linear entity.
    pointOnLine (O) Receives the closest *point* on the linear entity.
    tol (I) Geometric tolerance.
  */
  OdGePoint3d closestPointToLinearEnt (
    const OdGeLinearEnt3d& line,
    OdGePoint3d& pointOnLine, 
    const OdGeTol& tol = OdGeContext::gTol) const;


  /**
    Description:
    Returns the *point* on this *plane* closest to 
    the specified *plane*, and the point
    on that *plane* closest to this *plane*.

    Arguments:
    plane (I) Any *plane*.
    pointOnOtherPlane (O) Receives the closest *point* on that *plane*.
    tol (I) Geometric tolerance.
  */
  OdGePoint3d closestPointToPlanarEnt (
    const OdGePlanarEnt& plane,
    OdGePoint3d& pointOnOtherPlane, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns true if and only if the specified entity is parallel to this one.

    Arguments:
    line (I) Any 3D linear entity.
    plane (I) Any *plane*.
    tol (I) Geometric tolerance.
  */
  bool isParallelTo (
    const OdGeLinearEnt3d& line, 
    const OdGeTol& tol = OdGeContext::gTol) const;
  bool isParallelTo (
    const OdGePlanarEnt& plane,
    const OdGeTol& tol = OdGeContext::gTol) const
  { return m_normal.isParallelTo(plane.normal(), tol); }


  /**
    Description:
    Returns true if and only if the specified entity is perpendicular to this one.

    Arguments:
    line (I) Any 3D linear entity.
    plane (I) Any *plane*.
    tol (I) Geometric tolerance.
  */
  bool isPerpendicularTo (
    const OdGeLinearEnt3d& line,
    const OdGeTol& tol = OdGeContext::gTol) const;
  bool isPerpendicularTo (
    const OdGePlanarEnt& plane,
    const OdGeTol& tol = OdGeContext::gTol) const
  { return m_normal.isPerpendicularTo(plane.normal(), tol); }

  /**
    Description:
    Returns true if and only 
    the specified *plane* is colinear with this one.

    Arguments:
    plane (I) Any *plane*.
    tol (I) Geometric tolerance.
  */
  bool isCoplanarTo (
    const OdGePlanarEnt& plane,
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the parameters of this *plane*.

    Arguments:
    origin (I) The *origin* of this *plane*.
    uAxis (I) The U-axis.
    vAxis (I) The V-axis.
    uPnt (I) A *point* at the end of the U-axis.
    vPnt (I) A *point* at the end of the V-axis.

    Remarks:
    The U-axis and V-axis cannot be colinear, and are defined as follows

            uAxis=uPnt-origin
            vAxis=vPnt-origin
  */
  void get (
    OdGePoint3d& origin, 
    OdGeVector3d& uAxis, 
    OdGeVector3d& vAxis) const
  {
    origin = m_origin;
    uAxis = m_uVec;
    vAxis = m_vVec;
  }

  void get (
    OdGePoint3d& uPnt, 
    OdGePoint3d& origin, 
    OdGePoint3d& vPnt) const;

  /**
    Description:
    Returns an arbitrary *point* on the *plane*.
  */
  OdGePoint3d pointOnPlane() const { return m_origin; }

  /**
    Description:
    Returns the *normal* to the plane as a unit vector. 
  */
  OdGeVector3d normal() const { return m_normal; }

  /**
    Description
    Returns the coefficients of the *plane* equation for this *plane*.

    Arguments:
    a (O) Receives the coefficient *a*.
    b (O) Receives the coefficient *b*.
    c (O) Receives the coefficient *c*.
    d (O) Receives the coefficient *d*.

    Remarks:
    The *plane* equation for this *plane* is as follows

            a * x + b * y + c * z + d = 0
  */
  void getCoefficients (
    double& a, 
    double& b, 
    double& c, 
    double& d) const
  { a = m_normal.x; b = m_normal.y; c = m_normal.z; d = -m_normal.dotProduct(m_origin.asVector()); }

  /**
    Description:
    Returns the orthonormal canonical coordinate system of this *plane*.

    Arguments:
    origin (O) Receives the origin of this *plane*
    axis1 (O) Receives a unit vector in the *plane*.
    axis2 (O) Receives a unit vector perpendicular to the *plane*.

    Remarks
    The orthonormal canonical coordinate system associated with *a* *plane* defined follows

    @table
    Parameter       Description                                   Computed as
    *origin*        Origin of *plane*.                            *origin*              
    axis1           A unit vector in the *plane*.                 uAxis.normal()                        
    axis2           A unit vector perpendicular to the *plane*.   uAxis.crossProduct(vAxis).normal()   
  */
  void getCoordSystem (
    OdGePoint3d& origin, 
    OdGeVector3d& axis1, 
    OdGeVector3d& axis2) const;

  bool isOn (
    const OdGePoint3d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the signed distance to (elevation of) the specified *point*.

    Arguments:
    point (I) Any 3D *point*.
  */
  double signedDistanceTo (
    const OdGePoint3d& point) const
  { return m_normal.dotProduct(point - m_origin); }


  virtual OdGeEntity3d& transformBy (
    const OdGeMatrix3d& xfm);

  virtual OdGePoint2d paramOf (
    const OdGePoint3d& point,
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns projP and true,
    if and only if there is *a* *point* on this surface, projP, where
    the this surface *normal* or unitDir (if specified) passes through the point p.

    Arguments:
    p (I) Any 3D *point*.
    projP (O) Receives the *point* on the *plane*.
    unitDir (I) Unit vector specifying the projection direction.
    tol (I) Geometric tolerance.
  */
  virtual bool project (
    const OdGePoint3d& p, 
    OdGePoint3d& projP) const;

  bool project (
    const OdGePoint3d& p, 
    const OdGeVector3d& unitDir, 
    OdGePoint3d& projP, 
    const OdGeTol& tol = OdGeContext::gTol) const;

protected:
  OdGePlanarEnt() {}


  /**
    Description:
    Sets the parameters for this *plane* according to the arguments, 
    and returns *a* reference to this *plane*.

    Arguments:
    origin (I)  Origin of *plane*.
    normal (I)  The *normal* to the *plane*.
    uPnt (I) A *point* at the end of the u axis.
    vPnt (I) A *point* at the end of the v axis.
    uAxis (I) The U-axis.
    vAxis (I) The V-axis.
    a (I) Coefficient *a*.
    b (I) Coefficient *b*.
    c (I) Coefficient *c*.
    d (I) Coefficient *d*.

    Remarks:
    A parametric *point* on the *plane* with parameters u and v maps to the *point* S(u,v) as follows

            S(u,v) = originOfPlanarEntity + (u * uAxis) + (v * vAxis)

    uAxis and vAxis need not be either normalized or perpendicular, but they must
    not be colinear.

    The orthonormal canonical coordinate system associated with *a* *plane* defined as follows

    @table
    Parameter       Description                                   Computed as
    *origin*        Origin of *plane*.                            *origin*              
    axis1           A unit vector in the *plane*.                 uAxis.normal()                        
    axis2           A unit vector perpendicular to the *plane*.   uAxis.crossProduct(vAxis).normal()   

    The *plane* equation for this *plane* is as follows

            a * X + b * Y + c * Z + d = 0
  */
  void set (
    const OdGePoint3d& point, 
    const OdGeVector3d& normal)
  {
    m_origin = point; m_normal = normal; m_normal.normalize();
    m_uVec = normal.perpVector(); m_vVec = normal.crossProduct(m_uVec);
  }
    
  void set (
    const OdGePoint3d& uPnt, 
    const OdGePoint3d& origin, 
    const OdGePoint3d& vPnt)
  { set(origin, uPnt - origin, vPnt - origin); }
  
  void set (
    double a, 
    double b, 
    double c, 
    double d)
  { set(OdGePoint3d(a * d, b * d, c * d), OdGeVector3d(a, b, c)); }
    
  void set (
    const OdGePoint3d& origin, 
    const OdGeVector3d& uAxis, 
    const OdGeVector3d& vAxis)
  {
    m_origin = origin; m_uVec = uAxis; m_vVec = vAxis;
    
    //  m_normal = uAxis.crossProduct(vAxis); m_normal.normalize();
    // It's more safe (if vectors' length is close to tolerance)
    
    m_normal = uAxis.normal().crossProduct(vAxis.normal());
  }
    

  OdGePoint3d   m_origin; // Origin of *plane*
  OdGeVector3d  m_uVec;   // u Axis of the *plane*.
  OdGeVector3d  m_vVec;   // v Axis of the *plane*.
  OdGeVector3d  m_normal; // The *normal* to the *plane*.
  OdGeInterval  m_URange; // u interval of the *plane*.
  OdGeInterval  m_VRange; // v interval of the *plane*.
};




#include "DD_PackPop.h"

#endif // OD_GEPLANAR_H


