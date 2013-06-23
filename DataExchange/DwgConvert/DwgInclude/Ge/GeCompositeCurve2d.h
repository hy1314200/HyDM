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



#ifndef OD_GE_COMPOSITE_CURVE_2D_H
#define OD_GE_COMPOSITE_CURVE_2D_H /* {Secret} */

#include "GeCurve2d.h"

#include "DD_PackPush.h"

/**
    Description:
    This class represents composite curves in 2D space.

    Remarks:
    Composite curves consists of pointers to any number of subcurves that
    are joined end to end. Each subcurve must be bounded.

    The parameter at the start of the composite curve is 0.0. The parameter at any
    point along the the composite curve is the approximate length of the
    composite curve up to that *point*.

    Library: Ge

    {group:OdGe_Classes} 
*/
class OdGeCompositeCurve2d : public OdGeCurve2d
{
public:
  /**
    Arguments:
    curvelist (I) Array of pointers to subcurves comprising the composite *curve*.
    isOwnerOfCurves (I) Non-zero if and only if the corresponding subcurve
        is to be deleted when the composite curve is deleted.
    source (I) Object to be cloned.

    Remarks:
    If isOwnerOfCurves is not specified, the subcurves in the curvelist
    are copied, and the composite curve contains pointers to the copies.

    The default constructor creates a composite curve that consists 
    of a single subcurve: a line segment from (0,0) to (1,0). 
  */
  OdGeCompositeCurve2d ();
  OdGeCompositeCurve2d (
    const OdGeVoidPointerArray& curveList);
  OdGeCompositeCurve2d (
    const OdGeVoidPointerArray& curveList,
    const OdGeIntArray& isOwnerOfCurves);
  OdGeCompositeCurve2d (
    const OdGeCompositeCurve2d& source);


  /**
    Description:
    Returns an array of pointers to subcurves comprising the composite *curve*.

    Arguments:
    curvelist (O) Receives an array of pointers to subcurves comprising the composite *curve*.

    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  void getCurveList (
    OdGeVoidPointerArray& curveList) const;


  /**
    Description:
    Sets the curve list of the composite *curve*.

    Arguments:
    curvelist (I) Array of pointers to subcurves comprising the composite *curve*.
    isOwnerOfCurves (I) True if and only if the specified curve
    is to be deleted when the composite curve is deleted.

    Remarks:
    If OwnerOfCurves is not specified, the subcurves in the curvelist
    are copied, and the composite curve contains pointers to the copies.

    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  OdGeCompositeCurve2d& setCurveList (
    const OdGeVoidPointerArray& curveList);
  OdGeCompositeCurve2d& setCurveList (
    const OdGeVoidPointerArray& curveList,
    const OdGeIntArray& isOwnerOfCurves);

  /**
    Description:
    Returns the parameter on a subcurve, and the index of that subcurve,
    corresponding to the specified parameter on the composite *curve*.

    Arguments:
    param (I) Parameter value on composite *curve*.
    crvNum (O) Receives the *curve* number of the subcurve.

    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  double globalToLocalParam 
 (double param, 
    int& crvNum) const; 

  /**
  Description:
    Returns the parameter on the composite curve, corresponding
    corresponding to the specified parameter on the specifed subcurve *curve*.

    Arguments:
    param (I) Parameter value on the subcurve.
    crvNum (I) Curve number of the subcurve.

    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  double localToGlobalParam (
    double param, 
    int crvNum) const; 

  /**
    Remarks:
    All of the subcurves of the input curve are copied.         
  */
  OdGeCompositeCurve2d& operator = (
    const OdGeCompositeCurve2d& compCurve);
};

#include "DD_PackPop.h"

#endif // OD_GE_COMPOSITE_CURVE_2D_H


