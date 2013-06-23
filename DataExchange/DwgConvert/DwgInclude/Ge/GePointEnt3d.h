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



#ifndef OD_GEPENT3D_H
#define OD_GEPENT3D_H /* {Secret} */


#include "GeEntity3d.h"

/**
    Description:
    This class is the base class for all OdGe 3D *point* classes.

    Library: Ge

    {group:OdGe_Classes} 
*/
class OdGePointEnt3d : public OdGeEntity3d
{
public:
  OdGe::EntityId type() const;

  /**
    Description: 
    Returns this object as an OdGePoint3d.
  */
  virtual OdGePoint3d point3d() const = 0;
protected:
  /**
    Arguments:
    source (I) Object to be cloned.
  */
  OdGePointEnt3d () {}
  OdGePointEnt3d (
    const OdGePointEnt3d& source) {}

  virtual operator OdGePoint3d () const = 0;
};

#endif // OD_GEPENT3D_H


