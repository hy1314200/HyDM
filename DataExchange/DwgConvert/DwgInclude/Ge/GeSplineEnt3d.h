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



#ifndef OD_GESPNT3D_H
#define OD_GESPNT3D_H /* {Secret} */


class OdGeKnotVector;

#include "GeCurve3d.h"

/**
    Description:
    This class represents various spline objects in 3D space.
    
    Library: Ge
   
    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeSplineEnt3d : public OdGeCurve3d
{
public:
  /**
    Description:
    Returns true if and only if the spline is *rational* or a polynomial.

    Remarks:
    This function is meaningful only for AcGeNurbCurve3d objects derived from OdGeSplineEnt3d.
  */
  virtual bool isRational () const = 0;

  /**
    Description:
    Returns the *degree* of the spline.
  */
  virtual int degree () const = 0;

  /**
    Description:
    Returns the *order* of the spline.
  */
  virtual int order () const = 0;

  /**
    Description:
    Returns the number of *knots* in the knot vector.
  */
  virtual int numKnots () const = 0;

  /**
    Description:
    Returns the knot vector.
  */
  virtual const OdGeKnotVector& knots () const = 0;

  /**
    Description:
    Returns the number of *points* in the the control *point* array.
  */
  virtual int numControlPoints () const = 0;

  /*int continuityAtKnot (int idx, const OdGeTol& tol =
  OdGeContext::gTol) const;*/

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
  OdGePoint3d startPoint () const;

  /**
    Description:
    Returns the end point of this spline.
  */
  OdGePoint3d endPoint () const;

  /**
    Description:
    Returns true if and only if the spline is constructed using fit points.
  */
  virtual bool hasFitData () const = 0;

  /**
    Description:
    Returns a knot value for the specified knot.

    Arguments:
    idx (I) The knot to be queried.
  */
  virtual double knotAt (
    int idx) const = 0;

  /**
    Description:
    Sets the knot value for the specified knot.

    Arguments:
    idx (I) The knot to be *set*.
    val (I) The new value for the knot.
  */
  virtual OdGeSplineEnt3d& setKnotAt (
    int idx, 
    double val) = 0;

  /**
    Description:
    Returns the specified control *point* in the control *point* array.

    Arguments:
    idx (I) The control *point* to be queried.
  */
  virtual OdGePoint3d controlPointAt (
    int idx) const = 0;

  /**
    Description:
    Sets the specified control *point* in the control *point* array.

    Arguments:
    idx (I) The control *point* to be *set*.
  */
  virtual OdGeSplineEnt3d& setControlPointAt (
    int idx, 
    const OdGePoint3d& point) = 0;

protected:
  // Because of malfunction our NURBS-retrieving algorithm

  friend class OdDbSpline; 

  friend class OdDbSplineImpl;

  OdGeSplineEnt3d () {}
};

#endif


