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



#ifndef OD_GE_LINEAR_ENT_2D_H
#define OD_GE_LINEAR_ENT_2D_H /* {Secret} */


class OdGeCircArc2d;

#include "GeCurve2d.h"
#include "OdPlatformSettings.h"
#include <memory.h> // for memcpy

#include "DD_PackPush.h"

/**
    Description:
    This class is the base class for OdGe 2D linear entities.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeLinearEnt2d : public OdGeCurve2d
{
  void getInterval (
    OdGeInterval& interval, 
    OdGePoint2d& start, 
    OdGePoint2d& end) const;

  virtual bool checkInterval (
    const OdGePoint2d& pntOn, 
    const OdGeTol& tol = OdGeContext::gTol) const;

public:

  virtual void getInterval (
    OdGeInterval& interval) const = 0;

  /**
    Description:
    Returns true and the intersection point, if and only 
    if the specified linear entity intersects with this one.

    Arguments:
    line (I) Any 2D linear entity.
    intPnt (O) Receives the intesection *point*.
    tol (I) Geometric tolerance.
  */
  virtual bool intersectWith (
    const OdGeLinearEnt2d& line, 
    OdGePoint2d& intPnt,
    const OdGeTol& tol = OdGeContext::gTol) const;

  /*
  virtual bool overlap (const OdGeLinearEnt2d& line, OdGeLinearEnt2d*& overlap,
  const OdGeTol& tol = OdGeContext::gTol) const = 0;
  */


  /**
    Description:
    Returns true if and only 
    if the specified linear entity is parallel to this one.

    Arguments:
    line (I) Any 2D linear entity.
    tol (I) Geometric tolerance.
  */
  bool isParallelTo (
    const OdGeLinearEnt2d& line, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns true if and only 
    if the specified linear entity is perpendicular to this one.

    Arguments:
    line (I) Any 2D linear entity.
    tol (I) Geometric tolerance.
  */
  bool isPerpendicularTo (
    const OdGeLinearEnt2d& line, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns true if and only 
    if the specified linear entity is colinear to this one.

    Arguments:
    line (I) Any 2D linear entity.
    tol (I) Geometric tolerance.
  */
  bool isColinearTo (
    const OdGeLinearEnt2d& line, 
    const OdGeTol& tol = OdGeContext::gTol) const;


  /**
    Description:
    Returns a reference to a 2D *line* perpendicular to this one,
    and passing through the specified *point*.

    Arguments:
    point (I) Any 2D *point*.
    perpLine (O) Receives a reference to the perpendicular *line*.

    Remarks:
    It is up to the caller to delete the returned *line*.
  */
  void getPerpLine (
    const OdGePoint2d& point, 
    OdGeLine2d& perpLine) const;

  /**
    Description:
    Returns an arbitrary *point* on this linear entity.
  */
  OdGePoint2d pointOnLine () const;

  /**
    Description:
    Returns a unit vector parallel to this linear entity, 
    and pointing in the *direction* of increasing parametric value.
  */
  OdGeVector2d direction () const;

  /**
    Description:
    Returns a reference to an infinite *line* colinear with this linear entity.

    Arguments:
    line (O) Receives the infinite *line*.

    Remarks:
    It is up to the caller to delete the returned *line*.
  */
  void getLine (
    OdGeLine2d& line) const;

  virtual OdGePoint2d evalPoint (
    double param) const;

  DD_USING(OdGeCurve2d::evalPoint);

  virtual void appendSamplePoints (
    double fromParam, 
    double toParam,
    double approxEps, 
    OdGePoint2dArray& pointArray,
    OdGeDoubleArray* pParamArray = 0) const;

  virtual void appendSamplePoints (
    int numSample, 
    OdGePoint2dArray& pointArray) const;

  double paramOf (
    const OdGePoint2d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  virtual bool isOn (const OdGePoint2d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;


protected:
  /**
    Arguments:
    source (I) Object to be cloned.
  */
  OdGeLinearEnt2d () {}
  OdGeLinearEnt2d (
    const OdGeLinearEnt2d& source) 
  { memcpy (this, &source, sizeof (source)); }
  
  OdGePoint2d m_point1, m_point2;
  friend class OdGeLine2d;
  friend class OdGeLineSeg2d;
  friend class OdGeRay2d;
};

#include "DD_PackPop.h"

#endif


