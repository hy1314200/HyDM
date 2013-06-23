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



#ifndef OD_GELENT3D_H
#define OD_GELENT3D_H /* {Secret} */

class OdGeLine3d;
class OdGeCircArc3d;
class OdGePlanarEnt;

#include "OdPlatform.h"

#include "GeCurve3d.h"

#include "DD_PackPush.h"

/**
    Description:
    This class is the base class for OdGe 3D linear entities.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeLinearEnt3d : public OdGeCurve3d
{
public:
  /**
    Description:
    Returns true and the intersection point, if and only 
    if the specified *line* or *plane* intersects with this *line*.

    Arguments:
    line (I) Any 3D linear entity.
    plane (I) Any planar entity.
    intPnt (O) Receives the intesection *point*.
    tol (I) Geometric tolerance.
  */
  bool intersectWith (
    const OdGeLinearEnt3d& line,
    OdGePoint3d& intPt,
    const OdGeTol& tol = OdGeContext::gTol) const;

  bool intersectWith (
    const OdGePlanarEnt& plane, 
    OdGePoint3d& intPnt,
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the projected intersection of the specified *line* with this *line*.

    Arguments:
    line (I) Any 3D *line*.
    projDir (I) Projection *direction*.
    pntOnThisLine (O) Receives the intersection *point* on this *line*.   
    pntOnOtherLine (O) Receives the intersection *point* on the other *line*. 
    tol (I) Geometric tolerance.
    */
  bool projIntersectWith (
    const OdGeLinearEnt3d& line,
    const OdGeVector3d& projDir,
    OdGePoint3d& pntOnThisLine,
    OdGePoint3d& pntOnOtherLine,
    const OdGeTol& tol = OdGeContext::gTol) const;

  /* bool overlap (const OdGeLinearEnt3d& line,
  OdGeLinearEnt3d*& overlap,
  const OdGeTol& tol = OdGeContext::gTol) const;*/

  bool isOn (
    const OdGePoint3d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /*:>bool isOn (const OdGePoint3d& point, double& param,
  const OdGeTol& tol = OdGeContext::gTol) const;
  bool isOn (double param,
  const OdGeTol& tol = OdGeContext::gTol) const;
  bool isOn (const OdGePlane& plane,
  const OdGeTol& tol = OdGeContext::gTol) const;*/

  /**
    Description:
    Returns true if and only 
    if the specified entity is parallel to this *line*.

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
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns true if and only 
    if the specified entity is perpendicular to this *line*.

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
    const OdGeTol& tol = OdGeContext::gTol) const;

#ifdef OD_USE_STUB_FNS
  /** Description:
    Returns true if and only if the specified *line* is colinear with this one. 

    Arguments:
    line (I) Any 3D linear entity.
    tol (I) Geometric tolerance.
  */
  bool isColinearTo (
    const OdGeLinearEnt3d& line,
    const OdGeTol& tol = OdGeContext::gTol) const { return false; }
#endif

    /**
    Description:
      Returns a *plane*, containing the specified *point*, perpendicular to this *line*.

      Arguments:
      point (I) Any 3D *point*.
      plane (O) Receives the perpendicular *plane*.

      Remarks:
      The returned plane is created with the new method. It is up to the caller to delete it.
    */
    void getPerpPlane (
      const OdGePoint3d& point, 
      OdGePlane& plane) const;

    /**
    Description:
      Returns an arbitrary *point* on this *line*.
    */
    OdGePoint3d pointOnLine () const 
    { return m_start;}

    /**
    Description:
      Returns a unit vector parallel to this *line*, 
      and pointing in the *direction* of increasing parametric value.
    */
    virtual OdGeVector3d direction () const;

    /**
    Description:
      Returns a reference to an infinite line colinear with this one.

      Arguments:
      line (O) Receives the infinite *line*.

      Remarks:
      It is up to the caller to delete the returned line.
    */
    void getLine (OdGeLine3d& line) const;

    OdGeLinearEnt3d& operator = (
      const OdGeLinearEnt3d& line);

    void appendSamplePoints (
      double fromParam, 
      double toParam, 
      double approxEps, 
      OdGePoint3dArray& pointArray, 
      OdGeDoubleArray* pParamArray = 0) const;

    DD_USING(OdGeCurve3d::appendSamplePoints);

    OdGePoint3d evalPoint (
      double param) const;

    DD_USING(OdGeCurve3d::evalPoint);

    void getInterval (
      OdGeInterval& Interval) const;

    virtual OdGeEntity3d& transformBy (
      const OdGeMatrix3d& xfm);

protected:
  /**
    Arguments:
    source (I) Object to be cloned.
  */
  OdGeLinearEnt3d () 
  {}
  OdGeLinearEnt3d (
    const OdGeLinearEnt3d& source);
  OdGePoint3d m_start;
  OdGeVector3d m_vToEnd;
};

#include "DD_PackPop.h"

#endif // OD_GELENT3D_H



