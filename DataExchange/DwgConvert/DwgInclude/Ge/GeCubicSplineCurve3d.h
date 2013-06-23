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



#ifndef OD_GECSPL3D_H
#define OD_GECSPL3D_H /* {Secret} */


class OdGePointOnCurve3d;
class OdGeCurve3dIntersection;
class OdGeInterval;
class OdGePlane;

#include "GeSplineEnt3d.h"

#include "DD_PackPush.h"

/**
    Description:
    This class represents 3D interpolation cubic spline curves.

    Library: Ge

   {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeCubicSplineCurve3d : public OdGeSplineEnt3d
{
public:
  /**
  Arguments:
    fitPnts (I) An array of 3D fit points.
    tol (I) Geometric tolerance.
    startDeriv (I) Starting derivative of the cubic *spline* *curve*.
    endDeriv (I) Ending derivative of this cubic *spline* *curve*.
    curve (I) A *curve* to be approximated by this cubic *spline* *curve*.
    knots (I) Knot vector.
    isPeriodic (I) True if and only if the cubic *spline* *curve* is to be periodic (closed).
    firstDerivs (I) Array of first derivatives at the fit points.
    source (I) Object to be cloned.

    Remarks:
    OdGeCubicCplineCurve3d(fitPnts, tol) constructs a periodic (closed)
    cubic *spline* *curve*. The last fit *point* must equal the first.

    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  OdGeCubicSplineCurve3d();
  OdGeCubicSplineCurve3d(
    const OdGeCubicSplineCurve3d& source);
  OdGeCubicSplineCurve3d(
    const OdGePoint3dArray& fitPnts,
    const OdGeTol& tol = OdGeContext::gTol);
  OdGeCubicSplineCurve3d(
    const OdGePoint3dArray& fitPnts,
    const OdGeVector3d& startDeriv,
    const OdGeVector3d& endDeriv,
    const OdGeTol& tol = OdGeContext::gTol);
  OdGeCubicSplineCurve3d(
    const OdGeCurve3d& curve,
    double epsilon = OdGeContext::gTol.equalPoint());
  OdGeCubicSplineCurve3d(
    const OdGeKnotVector& knots,
    const OdGePoint3dArray& fitPnts,
    const OdGeVector3dArray& firstDerivs,
    bool isPeriodic = false);

  /**
    Description:
    Returns the number of fit points.

    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  int numFitPoints () const;

  /**
  Description:
    Returns the fit *point* at the specified index.

    Arguments:
    idx (I) Index of fit *point*.

    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  OdGePoint3d fitPointAt (
    int idx) const;

  /**
    Description:
    Sets the fit *point* at the specified index, and returns a reference to this *curve*.

    Arguments:
    idx (I) Index of fit *point*.
    point (I) Any 3D *point*.

    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  OdGeCubicSplineCurve3d& setFitPointAt (
    int idx, 
    const OdGePoint3d& point);

  /**
    Description:
    Returns the first derivative at the specified index.

    Arguments:
    idx (I) Index of fit *point*.

    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  OdGeVector3d firstDerivAt (
    int idx) const;

  /**
    Description:
    Sets the first derivative at the specified index, and returns a reference to this *curve*.

    Arguments:
    idx (I) Index of fit *point*.
    deriv (I) The first derivative at the fit *point*.

    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  OdGeCubicSplineCurve3d& setFirstDerivAt(
    int idx, 
    const OdGeVector3d& deriv);

  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  OdGeCubicSplineCurve3d&  operator = (
    const OdGeCubicSplineCurve3d& spline);
};

#include "DD_PackPop.h"

#endif


