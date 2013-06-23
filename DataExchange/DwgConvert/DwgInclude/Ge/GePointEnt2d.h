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



#ifndef OD_GE_POINT_ENT_2D_H
#define OD_GE_POINT_ENT_2D_H /* {Secret} */


#include "GeEntity2d.h"

#include "DD_PackPush.h"

/**
    Description:
    This class is the base class for all OdGe 2D *point* classes.

    Library: Ge

    {group:OdGe_Classes} 
*/
class OdGePointEnt2d : public OdGeEntity2d
{
public:

  /**
    Description:
    Returns this object as an OdGePoint2d.
  */
  OdGePoint2d point2d () const;

  operator OdGePoint2d () const;


  OdGePointEnt2d& operator = (
    const OdGePointEnt2d& point);

protected:
  /**
    Arguments:
    source (I) Object to be cloned.
  */
  OdGePointEnt2d ();
  OdGePointEnt2d (
    const OdGePointEnt2d& source);
};

#include "DD_PackPop.h"

#endif // OD_GE_POINT_ENT_2D_H


