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



#ifndef OD_GECSINT_H
#define OD_GECSINT_H /* {Secret} */


class OdGeCurve3d;
class OdGeSurface;

#include "GeEntity3d.h"

#include "DD_PackPush.h"

/**
    Description:
    Holds data for intersections of a 3d *curve* and a *surface*.

    Remarks:
    The intersection class constructor references *curve* and *surface* objects, but the
    intersection object does not own them.  The *curve* and *surface* objects are linked to
    the intersection object.  On deletion or modification of one of them, internal
    intersection results are marked as invalid and to be re-computed.

    Computation of the intersection does not happen on construction or set(), but
    on demand from one of the query functions.

    Any output geometry from an intersection object is owned by the caller. The
    const base objects returned by curve() and surface() are not considered
    output objects.

    Library: Ge

    {group:OdGe_Classes} 
*/
class OdGeCurveSurfInt : public OdGeEntity3d
{

public:
  /**
    Arguments:
    curve (I) Any 3D *curve*.
    surface (I) Any *surface*.
    tol (I) Geometric tolerance.
    source (I) Object to be cloned.
  */
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  OdGeCurveSurfInt ();

  OdGeCurveSurfInt (
    const OdGeCurve3d& curve, 
    const OdGeSurface& surface,
    const OdGeTol& tol = OdGeContext::gTol);

  OdGeCurveSurfInt (
    const OdGeCurveSurfInt& source);

  /**
    Description:
    Returns a pointer to the *curve*.
  */
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  const OdGeCurve3d *curve () const;

  /**
    Description:
    Returns a pointer to the *surface*.
  */
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  const OdGeSurface *surface () const;

  /**
    Description:
    Returns the *tolerance* for determining intersections.
  */
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  OdGeTol tolerance () const;

  /**
    Description:
    Returns the number of intersections between the *curve* and the *surface*,
    and the *status* of the intersection.

    Arguments:
    status (O) Receives the *status* of the intersection.

    Remarks:
    Possible values for status are as follows:

    @untitled table
    kXXOk
    kXXIndexOutOfRange
    kXXWrongDimensionAtIndex
    kXXUnknown
  */
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  int  numIntPoints (
    OdGeIntersectError& status) const;

  /**
    Description:
    Returns the 3d *point* representing the specified intersection,
    and the *status* of the intersection.

    Arguments:
    intNum (I) The zero-based index of the intersection *curve* to return.
    status (O) Receives the *status* of the intersection.

    Remarks:
    Returns NULL if the dimension of this intersection is not 0 (not a 3d *point*)

    Possible values for status are as follows:

    @untitled table
    kXXOk
    kXXIndexOutOfRange
    kXXWrongDimensionAtIndex
    kXXUnknown
  */
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  OdGePoint3d intPoint (
    int intNum, 
    OdGeIntersectError& status) const;


  /**
    Description: 
    Returns the parameters for the specified intersection point with respect to the *curve* and *surface*,
    and the *status* of the intersection.

    Arguments:
    intNum (I) The zero-based index of the intersection *curve* to return.
    param1 (O) Receives the parameter of the intersection point with respect to the *curve*.
    param2 (O) Receives the parameter pair of the intersection point with respect to the *surface*.
    status (O) Receives the *status* of the intersection.

    Remarks:
    Possible values for status are as follows:

    @untitled table
    kXXOk
    kXXIndexOutOfRange
    kXXWrongDimensionAtIndex
    kXXUnknown
  */
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  void getIntParams (
    int intNum,
    double& param1,
    OdGePoint2d& param2,
    OdGeIntersectError& status) const;

  /**
    Description: 
    Returns the intersection point as a *point* on the *curve*,
    and the *status* of the intersection.

    Arguments:
    intNum (I) The zero-based index of the intersection pooint to return.
    intPnt (O) Receives the specified intersection *point* on the *curve*.
    status (O) Receives the *status* of the intersection.

    Remarks:
    Possible values for status are as follows:

    @untitled table
    kXXOk
    kXXIndexOutOfRange
    kXXWrongDimensionAtIndex
    kXXUnknown
  */
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  void getPointOnCurve (
    int intNum, 
    OdGePointOnCurve3d& intPnt, 
    OdGeIntersectError& status) const;


  /**
    Description: 
    Returns the intersection point as a *point* on the *surface*,
    and the *status* of the intersection.

    Arguments:
    intNum (I) The zero-based index of the intersection point to return.
    intPnt (O) Receives the specified intersection *point* on the *surface*.
    status (O) Receives the *status* of the intersection.

    Remarks:
    Possible values for status are as follows:

    @untitled table
    kXXOk
    kXXIndexOutOfRange
    kXXWrongDimensionAtIndex
    kXXUnknown
  */
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  void getPointOnSurface (
    int intNum, 
    OdGePointOnSurface& intPnt, 
    OdGeIntersectError& status) const;

  /**
    Description: 
    Returns the configurations on either side of the *surface* at the intersection.

    Arguments:
    intNum (I) The zero-based index of the intersection *curve* to return.
    lower (O) Unknown.
    higher (O) Unknown.
    smallAngle (O) Unknown.
    status (O) Receives the *status* of the intersection.

    Remarks:
    Possible values for lower and higher are as follows:

    @untitled table
    kXUnknown               
    kXOut                   
    kXIn                    
    kXTanOut                
    kXTanIn                 
    kXCoincident            
    kXCoincidentUnbounded   

    Possible values for status are as follows:

    @untitled table
    kXXOk
    kXXIndexOutOfRange
    kXXWrongDimensionAtIndex
    kXXUnknown
  */
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  void getIntConfigs (
    int intNum, 
    OdGe::csiConfig& lower, 
    OdGe::csiConfig& higher, 
    bool& smallAngle, 
    OdGeIntersectError& status) const;

  /**
    Description:
    Sets the *curve*, *surface*, and *tolerance* for which to
    determine intersections

    Arguments:      
    curve (I) Any 3D *curve*.
    surface (I) Second *surface*.
    tol (I) Geometric tolerance.
  */
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  OdGeCurveSurfInt& set (
    const OdGeCurve3d& cvr,
    const OdGeSurface& surface,
    const OdGeTol& tol = OdGeContext::gTol);

  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  OdGeCurveSurfInt& operator = (
    const OdGeCurveSurfInt& crvSurfInt);
};

#include "DD_PackPop.h"

#endif


