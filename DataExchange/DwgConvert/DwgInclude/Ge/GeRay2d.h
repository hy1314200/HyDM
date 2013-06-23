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



#ifndef OD_GE_RAY_2D_H
#define OD_GE_RAY_2D_H /* {Secret} */

#include "GeLinearEnt2d.h"

#include "DD_PackPush.h"

/**
    Description:
    This class represents semi-infinite lines in 2D space.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeRay2d : public OdGeLinearEnt2d
{
public:
  OdGe::EntityId type() const;

  /**Arguments:
    line (I) Any 2D *line*.
    point (I) Any 2D *point*.
    vect (I) Any 2D vector
    point1 (I) Any 2D *point*.
    point2 (I) Any 2D *point*.

    Remarks:
    point and vect construct a semi-infinite *line* starting point with 
    a *direction* of vect. vect cannot have a zero *length*.

    point1 and point2 construct a semi-infinite *line* starting at point1, and passing through point2. The 
    points cannnot be coincident.

    If called with no arguments, constructs a semi-infinite *line* starting at (0,0) and passing through (1,0).
  */
  OdGeRay2d() 
  { set(OdGePoint2d::kOrigin, OdGeVector2d::kXAxis); }

  OdGeRay2d(
    const OdGePoint2d& point, 
    const OdGeVector2d& vect)
  { set(point, vect); }

  OdGeRay2d(
    const OdGePoint2d& point1, 
    const OdGePoint2d& point2)
  { set(point1, point2); }
  
  /**
    Description:
    Sets the parameters for this *line* according to the arguments, and returns a reference to this *line*.

    Arguments:
    point (I) Any 2D *point*.
    vect (I) Any 2D vector
    point1 (I) Any 2D *point*.
    point2 (I) Any 2D *point*.

    Remarks:
    point and vect construct a semi-infinite *line* starting point with 
    a *direction* of vect. vect cannot have a zero *length*.

    point1 and point2 construct a semi-infinite *line* starting at point1, and passing through point2. The 
    points cannnot be coincident.
  */
  OdGeRay2d& set(
    const OdGePoint2d& point, 
    const OdGeVector2d& vect)
  {
    m_point1 = m_point2 = point;
    m_point2 += vect;
    return *this;
  }
    
  OdGeRay2d& set(
    const OdGePoint2d& point1, 
    const OdGePoint2d& point2)
  {
    m_point1 = point1;
    m_point2 = point2;
    return *this;
  }

  void getInterval(
    OdGeInterval& interval) const;

  bool intersectWith(
    const OdGeLinearEnt2d& line, 
    OdGePoint2d& intPnt, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  bool isClosed(
    const OdGeTol& tol = OdGeContext::gTol) const;
};

#include "DD_PackPop.h"

#endif // OD_GE_RAY_2D_H

