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



#ifndef OD_GEPLANE_H
#define OD_GEPLANE_H /* {Secret} */


#include "GePlanarEnt.h"

    class OdGeBoundedPlane;
    class OdGeLine3d;
    class OdGeLineSeg3d;

#include "DD_PackPush.h"


/**
    Description:
    This class represents infinite planes in 3D space.

    Library: Ge

   {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGePlane : public OdGePlanarEnt
{
public:
  static const OdGePlane kXYPlane; // XY *plane*.
  static const OdGePlane kYZPlane; // YZ *plane*.
  static const OdGePlane kZXPlane; // ZY *plane*.

  bool isKindOf (
    OdGe::EntityId entType) const;
  OdGe::EntityId type () const;

  /**
    Arguments:
    origin (I)  Origin of *plane*.
    normal (I)  The *normal* to the *plane*.
    uPnt (I) A point at the end of the U-axis.
    vPnt (I) A point at the end of the V-axis.
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

    The orthonormal canonical coordinate system associated with *a* *plane* defined follows

    @untitled table
    *origin*        Origin of *plane*.                            originOfPlanarEntiity                 
    axis1           A unit vector in the *plane*.                 uAxis.normal()                        
    axis2           A unit vector perpendicular to the *plane*.   uAxis.crossProduct(vAxis).normal()   

    The *plane* equation for this *plane* is as follows

            a * X + b * Y + c * Z + d = 0
  */
  
  OdGePlane ()
  { *this = OdGePlane::kXYPlane;}

  OdGePlane (
    const OdGePoint3d& origin, 
    const OdGeVector3d& normal)
  { OdGePlanarEnt::set(origin, normal); }

  OdGePlane (
    const OdGePoint3d& uPnt, 
    const OdGePoint3d& origin, 
    const OdGePoint3d& vPnt)
  { OdGePlanarEnt::set(uPnt, origin, vPnt); }

  OdGePlane (
    const OdGePoint3d& origin, 
    const OdGeVector3d& uAxis, 
    const OdGeVector3d& vAxis)
  { OdGePlanarEnt::set(origin, uAxis, vAxis); }

  OdGePlane (
    double a, 
    double b, 
    double c, 
    double d)
  { OdGePlanarEnt::set(a, b, c, d); }

  virtual OdGeEntity3d* copy () const;

  /**
    Description:
    Returns true and the intersection *point* or *line*, if and only 
    if the specified *line* or *plane* intersects with this *plane*.

    Arguments:
    line (I) Any 3D linear entity.
    plane (I) Any *plane*.
    intPnt (O) Receives the intesection *point*.
    intLine (O) Receives the intersection *line*
    tol (I) Geometric tolerance.
  */
  bool intersectWith (
    const OdGeLinearEnt3d& line, 
    OdGePoint3d& intPnt,
    const OdGeTol& tol = OdGeContext::gTol) const;
  bool intersectWith (
    const OdGePlane& plane, 
    OdGeLine3d& intLine,
    const OdGeTol& tol = OdGeContext::gTol) const;
  bool intersectWith (
    const OdGeBoundedPlane& plane, 
    OdGeLineSeg3d& intLine,
    const OdGeTol& tol = OdGeContext::gTol) const;


  /**
    Description:
    Sets the parameters for this *plane* according to the arguments, 
    and returns *a* reference to this *plane*.

    Arguments:
    origin (I)  Origin of *plane*.
    normal (I)  The *normal* to the *plane*.
    uPnt (I) A point at the end of the U-axis.
    vPnt (I) A point at the end of the V-axis.
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

    The orthonormal canonical coordinate system associated with *a* *plane* defined follows

    @untitled table
    *origin*        Origin of *plane*.                            originOfPlanarEntiity                 
    axis1           A unit vector in the *plane*.                 uAxis.normal()                        
    axis2           A unit vector perpendicular to the *plane*.   uAxis.crossProduct(vAxis).normal()   

    The *plane* equation for this *plane* is as follows

            a * X + b * Y + c * Z + d = 0
  */
  OdGePlane& set (
    const OdGePoint3d& point, 
    const OdGeVector3d& normal)
  { OdGePlanarEnt::set(point, normal); return *this; }

  OdGePlane& set (
    const OdGePoint3d& uPnt, 
    const OdGePoint3d& origin, 
    const OdGePoint3d& vPnt)
  { OdGePlanarEnt::set(uPnt, origin, vPnt); return *this; }

  OdGePlane& set (
    double a, 
    double b, 
    double c, 
    double d)
  { OdGePlanarEnt::set(a, b, c, d); return *this; }

  OdGePlane& set (
    const OdGePoint3d& origin, 
    const OdGeVector3d& uAxis, 
    const OdGeVector3d& vAxis)
  { OdGePlanarEnt::set(origin, uAxis, vAxis); return *this; }
};


#include "DD_PackPop.h"

#endif // OD_GEPLANE_H


