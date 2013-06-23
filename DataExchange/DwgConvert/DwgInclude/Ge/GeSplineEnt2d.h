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



#ifndef OD_GE_SPLINE_ENT_2D_H
#define OD_GE_SPLINE_ENT_2D_H /* {Secret} */

class OdGeKnotVector;

#include "GeCurve2d.h"

#include "DD_PackPush.h"

/**
    Description:
    This class represents various spline objects in 2D space.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeSplineEnt2d : public OdGeCurve2d
{
public:

  /**
    Description:
    Returns true if and only if the spline is *rational* or a polynomial.

    Remarks:
    This function is meaningful only for AcGeNurbCurve2d objects derived from OdGeSplineEnt2d.
  */
  bool isRational () const;

  /**
    Description:
    Returns the *degree* of the spline.
  */
  int degree () const;

  /**
    Description:
    Returns the *order* of the spline.
  */
  int order () const;


  /**
    Description:
    Returns the number of *knots* in the knot vector.
  */
  int numKnots () const;


  /**
    Description:
    Returns the knot vector.
  */
  const OdGeKnotVector& knots () const;

  /**
    Description:
    Returns the number of *points* in the the control *point* array.
  */
  int numControlPoints () const;

  /**
    Description:
    Returns the *degree* of the highest derivative that is defined at a specified knot.

    Arguments:
    idx (I) The knot to be queried.
    tol (I) Geometric tolerance.
  */
  int continuityAtKnot (
    int idx, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the parameter corresponding to the start point of this spline.
  */
  double startParam () const;

  /**
    Description:
    Returns the parameter corresponding to the end point of this spline.
  */
  double endParam () const;

  /**
    Description:
    Returns the start point of this spline.
  */
  OdGePoint2d startPoint () const;

  /**
    Description:
    Returns the end point of this spline.
  */
  OdGePoint2d endPoint () const;

  /**
    Description:
    Returns true if and only if the spline is constructed using fit points.
  */
  bool hasFitData () const;


  /**
    Description:
    Returns a knot value for the specified knot.

    Arguments:
    idx (I) The knot to be queried.
  */
  double knotAt (
    int idx) const;

  /**
    Description:
    Sets the knot value for the specified knot.

    Arguments:
    idx (I) The knot to be *set*.
    val (I) The new value for the knot.
  */
  OdGeSplineEnt2d& setKnotAt (
    int idx, 
    double val);

  /**
    Description:
    Returns the specified control *point* in the control *point* array.

    Arguments:
    idx (I) The control *point* to be queried.
  */
  OdGePoint2d controlPointAt (
    int idx) const;

  /**
    Description:
    Sets the specified control *point* in the control *point* array.

    Arguments:
    idx (I) The control *point* to be *set*.
  */
  OdGeSplineEnt2d& setControlPointAt (int idx, 
    const OdGePoint2d& point);

  //:> OdGeSplineEnt2d&  operator = (const OdGeSplineEnt2d& spline);

protected:
  OdGeSplineEnt2d () {}
  OdGeSplineEnt2d (
    const OdGeSplineEnt2d& splineEnt);
};

#include "DD_PackPop.h"

#endif // OD_GE_SPLINE_ENT_2D_H

