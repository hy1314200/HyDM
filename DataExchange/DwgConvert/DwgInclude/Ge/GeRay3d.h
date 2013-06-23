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



#ifndef OD_GERAY3D_H
#define OD_GERAY3D_H /* {Secret} */

#include "GeLinearEnt3d.h"

class OdGeRay2d;

#include "DD_PackPush.h"

/**
    Description:
    This class represents semi-infinite lines in 3D space.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeRay3d : public OdGeLinearEnt3d
{
public:
  /**Arguments:
    line (I) Any 3D *line*.
    point (I) Any 3D *point*.
    vect (I) Any 3D vector
    point1 (I) Any 3D *point*.
    point2 (I) Any 3D *point*.

    Remarks:
    point and vect construct a semi-infinite *line* starting point with 
    a *direction* of vect. vect cannot have a zero *length*.

    point1 and point2 construct a semi-infinite *line* starting at point1, and passing through point2. The 
    points cannnot be coincident.

    If called with no arguments, constructs a semi-infinite *line* starting at (0,0m0) and passing through (1,0,0).
  */
  OdGeRay3d();
  OdGeRay3d(
    const OdGeRay3d& line);
  OdGeRay3d(
    const OdGePoint3d& point, 
    const OdGeVector3d& vect);
  OdGeRay3d(const OdGePoint3d& point1, 
    const OdGePoint3d& point2);

  /**
    Description:
    Sets the parameters for this *line* according to the arguments, and returns a reference to this *line*.

    Arguments:
    point (I) Any 3D *point*.
    vect (I) Any 3D vector.
    point1 (I) Any 3D *point*.
    point2 (I) Any 3D *point*.

    Remarks:
    point and vect construct a semi-infinite *line* starting point with 
    a *direction* of vect. vect cannot have a zero *length*.

    point1 and point2 construct a semi-infinite *line* starting at point1, and passing through point2. The 
    points cannnot be coincident.
  */
  OdGeRay3d& set(
    const OdGePoint3d& point, 
    const OdGeVector3d& vect);
  OdGeRay3d& set(
    const OdGePoint3d& point1, 
    const OdGePoint3d& point2);

  // Assignment operator.
  
  OdGeRay3d& operator = (
    const OdGeRay3d& line);
};

#include "DD_PackPop.h"

#endif


