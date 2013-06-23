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



#ifndef OD_GE_POLYLINE_2D_H
#define OD_GE_POLYLINE_2D_H /* {Secret} */

#include "OdPlatform.h"

#include "GeSplineEnt2d.h"

#include "DD_PackPush.h"

/**
    Description:
    This class represents piecewise linear splines in 2D space.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGePolyline2d : public OdGeSplineEnt2d
{
public:
  /**
    Arguments:
    source (I) Object to be cloned.
  */
  OdGePolyline2d () : m_isClosed (false) {}
  OdGePolyline2d (
    const OdGePolyline2d& source)
  {
    *this = source;
  }

  /**
    Arguments:
    points (I) Array of fit *points*.
    knots (I) Knot vector.
    crv (I) A 2D curve to be approximated as a polyline.
    approxEps (I) Approximate geometric tolerance. 
  */
  OdGePolyline2d (
    const OdGePoint2dArray& fitpoints);
  OdGePolyline2d (
    const OdGeKnotVector& knots,
    const OdGePoint2dArray& points);
  OdGePolyline2d (
    const OdGeCurve2d& crv, 
    double approxEps);

  OdGe::EntityId type () const 
  { return OdGe::kPolyline2d;}

  /**
    Description:
    Returns the number of fit *points*.
  */
  int numFitPoints () const;

  /**
    Description:
    Returns the fit *point* at the specified index.

    Arguments:
    idx (I) Index of fit *point*.
  */
  OdGePoint2d fitPointAt (
    int idx) const;

  /**
    Description:
    Sets the fit *point* at the specified index, and returns a reference to this *curve*.

    Arguments:
    idx (I) Index of fit *point*.
    point (I) Any 2D *point*.
  */
  OdGeSplineEnt2d& setFitPointAt (
    int idx, 
    const OdGePoint2d& point);

  /**
    Description:
    Closes or opens this polyline.

    Arguments:
    flag (I) Boolean indicating if this polyline is to be closed.
  */
  OdGePolyline2d& setClosed (
    bool flag) 
  {m_isClosed = flag; return *this;}

  bool isClosed (
    const OdGeTol& = OdGeContext::gTol) const 
  {return m_isClosed;}

  /* Description:
    Returns a reference to the bulge *vector* for this polyline.

    Remarks:
    The *bulge* is the *tangent* of 1/4 the included angle of the *arc*,
    measured counterclockwise.
  */
  OdGeDoubleArray& bulges () 
  {return m_bulges;}

  /* Description:
    Returns the bulge *vector* for this polyline.

    Remarks:
    The *bulge* is the *tangent* of 1/4 the included angle of the *arc*,
    measured counterclockwise.
  */
  const OdGeDoubleArray& getBulges () const 
  {return m_bulges;}

  /* Description:
    Returns a reference to the *vertices* for this polyline.
  */
  OdGePoint2dArray& vertices () 
  {return m_vertices;}

  /* Description:
    Returns the *vertices* for this polyline.
  */
  const OdGePoint2dArray& getVertices () const 
  {return m_vertices;}

  /**
    Description:
    Returns true in and only if this polyline has at least one non-zero bulge.
  */
  bool hasBulges () const;

  void getInterval (
    OdGeInterval& interval) const;

  virtual double paramOf (
    const OdGePoint2d& point,
    const OdGeTol& tol = OdGeContext::gTol) const;

  virtual OdGePoint2d evalPoint (
    double param) const;

  DD_USING(OdGeSplineEnt2d::evalPoint);

  virtual void appendSamplePoints (
    double fromParam, 
    double toParam,
    double approxEps, 
    OdGePoint2dArray& pointArray,
    OdGeDoubleArray* pParamArray = 0) const;

  virtual OdGeEntity2d& transformBy (
    const OdGeMatrix2d& xfm);

  bool hasStartPoint (
    OdGePoint2d& startPoint) const;

  bool hasEndPoint (
    OdGePoint2d& endPoint) const;

  DD_USING (OdGeSplineEnt2d::appendSamplePoints);

private:
  OdGeDoubleArray m_bulges;
  OdGePoint2dArray m_vertices;
  bool m_isClosed;
};

#include "DD_PackPop.h"

#endif // OD_GE_POLYLINE_2D_H


