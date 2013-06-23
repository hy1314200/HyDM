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



#ifndef OD_GE_LINE_2D_H
#define OD_GE_LINE_2D_H /* {Secret} */

#include "GeLinearEnt2d.h"

#include "DD_PackPush.h"

/**
    Description:
    This class represents infinite lines in 2D space.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeLine2d : public OdGeLinearEnt2d
{
  bool checkInterval (
    const OdGePoint2d& pntOn,
    const OdGeTol& tol = OdGeContext::gTol) const;
public:

  /**
    Arguments:
    line (I) Any 2D *line*.
    point (I) Any 2D *point*.
    point1 (I) Any 2D *point*.
    point2 (I) Any 2D *point*.
    source (I) Object to be cloned.
    vect (I) Any 2D vector.

    Remarks:
    point and vect construct an infinite *line* passing through point with 
    a *direction* of vect. vect cannot have a zero *length*.

    point1 and point2 construct an infinite *line* passing through both points. The 
    points cannnot be coincident.

    If called with no arguments, constructs in infinite *line* coincident with the x-axis.
  */
  OdGeLine2d () {}
  OdGeLine2d (
    const OdGeLine2d& source);
  OdGeLine2d (
    const OdGePoint2d& point, 
    const OdGeVector2d& vect);
  OdGeLine2d (
    const OdGePoint2d& point1, 
    const OdGePoint2d& point2);

  static const OdGeLine2d kXAxis; // X-axis *line*.
  static const OdGeLine2d kYAxis; // Y-axis *line*.
  OdGe::EntityId type () const;


  /**
    Description:
    Sets the parameters for this *line* according to the arguments, and returns a reference to this *line*.

    Arguments:
    point (I) Any 2D *point*.
    vect (I) Any 2D vector
    point1 (I) Any 2D *point*.
    point2 (I) Any 2D *point*.

    Remarks:
    point and vect construct an infinite *line* passing through point with 
    a *direction* of vect. vect cannot have a zero *length*.

    point1 and point2 construct an infinite *line* passing through both points. The 
    points should no be coincident.
  */
  OdGeLine2d& set (
    const OdGePoint2d& point, 
    const OdGeVector2d& vect);
  OdGeLine2d& set (
    const OdGePoint2d& point1, 
    const OdGePoint2d& point2);

  OdGeLine2d& operator = (const OdGeLine2d& line);

  /**
    Description:
    Returns true, and the *overlap* region, if and only if the specified
    line overlaps with this one.

    Arguments:
    line (I) Any 2D line.
    overlap (O) Receives a pointer to the *overlap* region.
    tol (I) Geometric tolerance.

    Remarks:
    The *overlap* region is created with the new operator. It is up to the caller to 
  delete the *overlap* region.
  */
  virtual bool overlap (
    const OdGeLinearEnt2d& line, 
    OdGeLinearEnt2d*& overlap,
    const OdGeTol& tol = OdGeContext::gTol) const;

  virtual void getInterval (
    OdGeInterval& interval) const;

  bool hasStartPoint (
    OdGePoint2d& startPoint) const;

  bool hasEndPoint (
    OdGePoint2d& endPoint) const;

  bool isClosed (
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns the distance to the *point* on this *line* closest to the specified *point*.

    Arguments:
    point (I) Any 2D *point*.
    tol (I) Geometric tolerance.
  */
  double distanceTo (
    const OdGePoint2d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;
};

#include "DD_PackPop.h"

#endif // OD_GE_LINE_2D_H



