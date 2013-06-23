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



#ifndef OD_GEOFFC3D_H
#define OD_GEOFFC3D_H /* {Secret} */

#include "GeCurve3d.h"

#include "DD_PackPush.h"

/**
    Description:
    This class represents 3D curves that are exact offsets of other curves.

    Remarks:
    Every GeOffsetCurve2d contains a pointer to the *curve* from which it is offest.
    Modifying the base *curve* modifies the offset *curve*. Modifying the offset *curve*
    does not modify the base *curve*. setInterval() for an offset *curve* result create a *curve* that
    is an offset of the specified interval in the base *curve*.

    This *curve* may be self-intersecting, even if the base *curve* is not. To create offset
    curves that are not self-intersecting, use OdGeCurve3d::getTrimmedOffset().

    The base *curve* must be planar, and a *normal* to the plane must be specified.
    Positive offset *distance*s at any *point* on the base *curve* are defined as 90° counterclockwise
    from the tangent of the base *curve* at that *point*.

    An offset *curve* with a 0.0 offset *distance* is exact replica of the base *curve*. An offseet *curve* with
    a non-zero 0.0 offset *distance* has a continuity of one less than that of the base *curve*. 
    To insure that the offset *curve* is a valid *curve*, the base *curve* must have a continuity
    of at least 1.

    Library: Ge

   {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeOffsetCurve3d : public OdGeCurve3d
{
public:
  /**
    Arguments:
    planeNormal (I) A *normal* to the plane of the base *curve*.
    baseCurve (I) Any 3D *curve*.
    offsetDistance (I) Offset *distance*.
    source (I) Object to be cloned.
  */
  OdGeOffsetCurve3d (
    const OdGeCurve3d& baseCurve, 
    const OdGeVector3d& planeNormal,
    double offsetDistance);
  OdGeOffsetCurve3d (
    const OdGeOffsetCurve3d& source);

  /**
    Description:
    Returns a pointer to the base *curve*. 
  */
  const OdGeCurve3d* curve () const;

  /**
    Description:
    Returns the *normal* to the base *curve* as a unit vector. 
  */
  OdGeVector3d normal () const; 

  /**
    Description:
    Returns the offset *distance* of this *curve*. 
  */
  double offsetDistance () const;

  /**
    Description:
    Returns true if and only if this *curve* has the same parameter *direction* as the base *curve*.
  */
  bool paramDirection () const;

  /**
    Description:
    Returns the concatination of the *transformation* matrices applied to this *curve* with transformBy(). 

    Remarks:
    If no *transformation* matrices have been applied to this *curve*, returns the indentity matrix.
  */
  OdGeMatrix3d transformation () const;

  /**
    Description: 
    Sets the base *curve* for this *curve*.

    Arguments:
    baseCurve (I) Any 3D *curve*.
  */
  OdGeOffsetCurve3d& setCurve (
    const OdGeCurve3d& baseCurve);

  /**
    Description: 
    Sets the *normal* to the plane of the base *curve*.

    Arguments:
    planeNormal (I) A *normal* to the plane of the base *curve*.
  */
  OdGeOffsetCurve3d& setNormal (
    const OdGeVector3d& planeNormal);

  /**
    Description: 
    Sets the offset *distance* for this *curve*.

    Arguments:
    offsetDistance (I) Offset *distance*.
  */
  OdGeOffsetCurve3d& setOffsetDistance (
    double offsetDistance);

  OdGeOffsetCurve3d&  operator = (
    const OdGeOffsetCurve3d& offsetCurve);
};

#include "DD_PackPop.h"

#endif // OD_GEOFFC3D_H

