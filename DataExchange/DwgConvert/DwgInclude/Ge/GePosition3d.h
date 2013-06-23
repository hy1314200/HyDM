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



#ifndef OD_GEPOS3D_H
#define OD_GEPOS3D_H /* {Secret} */

#include "GePointEnt3d.h"

#include "DD_PackPush.h"

/**
    Description:
    This class represents points (positions) in 3D space.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGePosition3d : public OdGePointEnt3d
{
public:
  /**
    Arguments:
    point (I) Any 3D *point*.
    x (I) The X coordinate of the *point*.
    y (I) The Y coordinate of the *point*.
    z (I) The Z coordinate of the *point*.
    source (I) Object to be cloned.
 */
  OdGePosition3d ();
  OdGePosition3d (
    const OdGePoint3d& point);
  OdGePosition3d (
    double x, 
    double y, 
    double z);
  OdGePosition3d (
    const OdGePosition3d& source);

  /**
    Description:
    Sets the coordinates of, and returns a reference
    to, this *point*.

    Arguments:
    point (I) Any 3D *point*.
    x (I) The X coordinate of the position.
    y (I) The Y coordinate of the position.
    z (I) The Z coordinate of the *point*.
  */
  OdGePosition3d& set (
    const OdGePoint3d& point);
  OdGePosition3d& set (
    double x, 
    double y, 
    double z);

  OdGePosition3d& operator = (
    const OdGePosition3d& pos);
};

#include "DD_PackPop.h"

#endif // OD_GEPOS3D_H


