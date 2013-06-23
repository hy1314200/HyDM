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



#ifndef OD_GESSINT_H
#define OD_GESSINT_H /* {Secret} */


#include "GeEntity3d.h"

class OdGeCurve3d;
class OdGeCurve2d;
class OdGeSurface;

#include "DD_PackPush.h"

/**
    Description:
    This class holds the intersection data of two surfaces.

    Remarks:
    The intersection class constructor references surface objects, but the
    intersection object does not own them.  The surface objects are linked to the
    intersection object.  On deletion or modification of one of them, internal
    intersection results are marked as invalid and to be re-computed.

    Computation of the intersection does not happen on construction or set(), but
    on demand from one of the query functions.

    Any output geometry from an intersection object is owned by the caller.  The
    const base objects returned by surface1() and surface2() are not considered
    output objects.

    Library: Ge

    {group:OdGe_Classes} 
*/
class OdGeSurfSurfInt  : public OdGeEntity3d
{

public:

  /**
    Arguments:
    srf1 (I) First surface.
    srf2 (I) Second surface.
    tol (I) Geometric tolerance.
    source (I) Object to be cloned.
  */
  OdGeSurfSurfInt ();

  OdGeSurfSurfInt (
    const OdGeSurface& srf1,
    const OdGeSurface& srf2,
    const OdGeTol& tol = OdGeContext::gTol) ;

  OdGeSurfSurfInt (
    const OdGeSurfSurfInt& source);

  /**
    Description:
    Returns a pointer to the first surface.
  */
  const OdGeSurface *surface1 () const;

  /**
    Description:
    Returns a pointer to the second surface.
  */
  const OdGeSurface *surface2 () const;

  /**
    Description:
    Returns the *tolerance* for determining intersections.
  */
  OdGeTol tolerance () const;

  /**
    Description:
    Returns the number of intersections between the two surfaces,
    and the *status* of the intersections.

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
  int numResults (
    OdGeIntersectError& status) const;

  /**
    Description:
    Returns a pointer to the the 3D curve representing the specified intersection
    and the *status* of the intersection.

    Arguments:
    intNum (I) The zero-based index of the intersection curve to return.
    isExternal (I) Unknown.
    status (O) Receives the *status* of the intersection.

    Remarks:
    Possible values for status are as follows:

    @untitled table
    kXXOk
    kXXIndexOutOfRange
    kXXWrongDimensionAtIndex
    kXXUnknown
  */
  OdGeCurve3d* intCurve (
    int intNum, 
    bool isExternal, 
    OdGeIntersectError& status) const; 

  /**
    Description: 
    Returns a pointer to the the 2D curve representing the specified intersection
    and the *status* of the intersection.

    Arguments:
    intNum (I) The zero-based index of the intersection curve to return.
    isExternal (I) Unknown.
    isFirst (I) If true, returns the curve on the first surface, otherwise it the curve on the second surface.
    status (O) Receives the *status* of the intersection.

    Remarks:
    Returns NULL if the dimension of this intersection is not 1 (not a 2d curve)

    Possible values for status are as follows:

    @untitled table
    kXXOk
    kXXIndexOutOfRange
    kXXWrongDimensionAtIndex
    kXXUnknown
  */
  OdGeCurve2d* intParamCurve (
    int intNum, 
    bool isExternal, 
    bool isFirst, 
    OdGeIntersectError& status) const;

  /**
    Description: 
    Returns the 3d *point* representing the specified intersection,
    and the *status* of the intersection.

    Arguments:
    intNum (I) The zero-based index of the intersection curve to return.
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
  OdGePoint3d intPoint (
    int intNum, 
    OdGeIntersectError& status) const;


  /**
    Description: 
    Returns the parameter pairs for the specified intersection point with respect to each surface,
    and the *status* of the intersection.

    Arguments:
    intNum (I) The zero-based index of the intersection curve to return.
    param1 (O) Receives the parameter pair of the intersection point with respect to the first *curve*.
    param2 (O) Receives the parameter pair of the intersection point with respect to the second *curve*.
    status (O) Receives the *status* of the intersection.

    Possible values for status are as follows:

    @untitled table
    kXXOk
    kXXIndexOutOfRange
    kXXWrongDimensionAtIndex
    kXXUnknown
  */
  void getIntPointParams (
    int intNum,
    OdGePoint2d& param1, 
    OdGePoint2d& param2, 
    OdGeIntersectError& status) const;

  /**
    Description: 
    Returns the configurations on either side of the intersection each surface.

    Arguments:
    intNum (I) The zero-based index of the intersection curve to return.
    surf1Left (O) Receives the configuration for surface 1 on the left side of the intersection.
    surf1Right (O) Receives the configuration for surface 1 on the right side of the intersection.
    surf2Left (O) Receives the configuration for surface 2 on the left side of the intersection.
    surf2Right (O) Receives the configuration for surface 2 on the right side of the intersection.
    status (O) Receives the *status* of the intersection.

    Possible values for surf1Left, surf1Right, surf2Left, and surf2Right are as follows:

    @untitled table
    kSSIUnknown
    kSSIOut            Neighborhood is outside this surface.
    kSSIIn             Neighborhood is inside this surface.
    kSSICoincident     Non-zero area intersection.

    Possible values for status are as follows:

    @untitled table
    kXXOk
    kXXIndexOutOfRange
    kXXWrongDimensionAtIndex
    kXXUnknown 
  */
  void getIntConfigs (
    int intNum, 
    OdGe::ssiConfig& surf1Left,  
    OdGe::ssiConfig& surf1Right,
    OdGe::ssiConfig& surf2Left,  
    OdGe::ssiConfig& surf2Right,  
    OdGe::ssiType& intType, 
    int& dim, 
    OdGeIntersectError& status) const; 


  /**
    Description:
    Description Pending.

    Arguments:
    intNum (I) The zero-based index of the intersection curve to return.
    status (O) Receives the *status* of the intersection.

    Possible values for status are as follows:

    @untitled table
    kXXOk
    kXXIndexOutOfRange
    kXXWrongDimensionAtIndex
    kXXUnknown
  */
  int getDimension (
    int intNum, 
    OdGeIntersectError& status) const;

  /**
    Description:
    Returns the *type* of the specified intersection.

    Arguments:
    intNum (I) The zero-based index of the intersection curve to return.
    status (O) Receives the *status* of the intersection.

    Possible retrurns values for the configuratons are as follows:

    @untitled table
    kXXOk
    kXXIndexOutOfRange
    kXXWrongDimensionAtIndex
    kXXUnknown
  */
  OdGe::ssiType getType (
    int intNum, 
    OdGeIntersectError& status) const;

  /**
    Description:
    Sets the surfaces and tolerances whose intersection data is to be determines.
    Returns a reference to this SurfSurfInt.

    Arguments:
    srf1 (I) First surface.
    srf2 (I) Second surface.
    tol (I) Geometric tolerance.
  */
  OdGeSurfSurfInt& set (
    const OdGeSurface& srf1,
    const OdGeSurface& srf2,
    const OdGeTol& tol = OdGeContext::gTol);

  OdGeSurfSurfInt& operator = (
    const OdGeSurfSurfInt& surfSurfInt);
};

#include "DD_PackPop.h"

#endif // OD_GESSINT_H

