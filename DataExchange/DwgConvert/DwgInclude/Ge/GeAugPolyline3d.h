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



#ifndef OD_GEAPLN3D_H
#define OD_GEAPLN3D_H  /* {Secret} */

class OdGeKnotVector;
class OdGeVector3dArray;

#include "GePolyline3d.h"
#include "GePoint3d.h"

#include "DD_PackPush.h"

/**
    Description:
    This class represents mathematical entities used to support various types of spline curves in 3D space.

    Library: Ge

    {group:OdGe_Classes} 
*/
class OdGeAugPolyline3d : public OdGePolyline3d
{
public:
  /**
    Arguments:
    curve (I) Any 3D *curve*.
    fromParam (I) Starting parameter value.
    toParam (I) Ending parameter value.
    approxEps (I) Approximate spacing along a *curve*.
    knots (I) Knot vector.
    controlPoints (I) Control *point* array.
    vecBundle (I) Vector array. 
    source (I) Object to be cloned.
  */
  OdGeAugPolyline3d ();
  OdGeAugPolyline3d (
    const OdGeAugPolyline3d& source);

  OdGeAugPolyline3d (
    const OdGeKnotVector& knots,
    const OdGePoint3dArray& controlPoints,
    const OdGeVector3dArray& vecBundle);

  OdGeAugPolyline3d (
    const OdGePoint3dArray& controlPoints,
    const OdGeVector3dArray& vecBundle);

  OdGeAugPolyline3d (
    const OdGeCurve3d& curve,
    double fromParam, 
    double toParam, 
    double approxEps);

  OdGeAugPolyline3d& operator = (
    const OdGeAugPolyline3d& apline);

  /**
    Description:
    Returns the control *point* at the specified index.

    Arguments:
    idx (I) Index of control *point*.
  */
  OdGePoint3d getPoint (
    int idx) const;

  /**
    Description:
    Sets the control *point* at the specified index, and returns a reference to this *curve*.

    Arguments:
    idx (I) Index of control *point*.
    point (I) Any 3D *point*.
  */
  OdGeAugPolyline3d& setPoint (
    int idx, 
    OdGePoint3d point);
  /**
    Description:
    Returns the array of control *points*.

    Arguments:
    controlPoints (O) Receives an array of control *points*.
  */
  void getPoints (
    OdGePoint3dArray& controlPoints) const;


  /**
    Description:
    Returns the vector at the specified index.

    Arguments:
    idx (I) Index of vector.
  */
  OdGeVector3d getVector (
    int idx) const;

  /**
    Description:
    Sets the vector at the specified index, and returns a reference to this *curve*.

    Arguments:
    idx (I) Index of vector.
    vect (I) Any 3D vector.
  */
  OdGeAugPolyline3d& setVector (
    int idx, 
    OdGeVector3d vect);

  /**
    Description:
    Returns an array of the *tangents* (first derivative vectors) to the curve at each control *point*.

    Arguments:
    tangents (O) Receives an array of *tangents*.
  */
  void getD1Vectors (
    OdGeVector3dArray& tangents) const;

  /**
    Description:
    Returns the second derivative vector at the specified index.

    Arguments:
    idx (I) Index of second derivative vector.
  */
  OdGeVector3d getD2Vector (
    int idx) const;

  /**
    Description:
    Sets the second derivative vector at the specified index, and returns a reference to this *curve*.

    Arguments:
    idx (I) Index of knot vector.
    vect (I) Second derivative vector.
  */
  OdGeAugPolyline3d& setD2Vector (
    int idx, 
    OdGeVector3d vect);

  /**
    Description:
    Returns an array of the second derivative vectors to the curve at each control *point*.

    Arguments:
    d2Vectors Returns an array of second derivative cectors.
  */
  void getD2Vectors (
    OdGeVector3dArray& d2Vectors) const;

  /** 
    Description:
    Returns the approximate tolerance that was used to construct the polyline.
  */
  double approxTol () const;

  /** 
    Description:
    Sets the approximate tolerance to be used to construct the polyline, and returns
    a reference to this polyline.

    Remarks:
    This method recomputes the polyline.
  */
  OdGeAugPolyline3d& setApproxTol (
    double approxTol);
  
};

#include "DD_PackPop.h"

#endif // OD_GEAPLN3D_H


