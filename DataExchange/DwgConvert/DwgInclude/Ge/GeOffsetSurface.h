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
// DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef OD_GEOFFSF_H
#define OD_GEOFFSF_H /* {Secret} */


//
// Description:
//
// This file contains the class OdGeOffsetSurface, a
// representation for an offset surface
//

#include "GeSurface.h"

#include "DD_PackPush.h"

class OdGePlane;
class OdGeBoundedPlane;
class OdGeCylinder;
class OdGeCone;
class OdGeSphere;
class OdGeTorus;
class OdGeEllipCone; // AE 03.09.2003 
class OdGeEllipCylinder; // AE 09.09.2003 

/**
    Description:
    This class represents surfaces that are exact offsets of other surfaces.

    Library: Ge

   {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeOffsetSurface : public OdGeSurface
{
public:
  /**
    Arguments:
    source (I) Object to be cloned.
    baseSurface (I) Any *surface*.
    offsetDistance (I) Offset *distance*.
    makeCopy (I) Makes a *copy* of baseSurface.

    Remarks:
    Without arguments, the constructor sets the base surface pointer to NULL, and the offset distance to 0.0.
  */
  OdGeOffsetSurface();
  OdGeOffsetSurface(
    OdGeSurface* baseSurface,
    double offsetDistance,
    bool makeCopy = true);
  OdGeOffsetSurface(
    const OdGeOffsetSurface& source);

  // Test whether this offset surface can be converted to a simple surface
  //
  
  /**
    Description:
    Return true if and only if the offset *surface* can be represented as an OdGePlanarEnt. 
  */
  bool isPlane () const;
  
  /**
    Description:
    Return true if and only if the offset *surface* can be represented as an OdGeBoundedPlane. 
  */
  bool isBoundedPlane () const;
  
  /**
    Description:
    Return true if and only if the offset *surface* can be represented as an OdGeSphere. 
  */
  bool isSphere () const;
  
  /**
    Description:
    Return true if and only if the offset *surface* can be represented as an OdGeCylinder. 
  */
  bool isCylinder () const;
  
  /**
    Description:
    Return true if and only if the offset *surface* can be represented as an OdGeCone. 
  */
  bool isCone () const;
  
  /**
    Description:
    Return true if and only if the offset *surface* can be represented as an OdGeTorus. 
  */
  bool isTorus () const;
  
  /**
    Description:
    Return true if and only if the offset *surface* can be represented as an OdGeEllipCone. 
  */
  bool isEllipCone () const; // AE 03.09.2003 

  // Convert this offset surface to a simple surface
  //
  
  /**
    Description:
    Creates a *copy* of this *surface* as an OdGeSurface, and returns a pointer to the new *surface*.
    
    Arguments:
    simpleSurface (O) Receives the OdGeSurface. 
  */
  bool getSurface (
    OdGeSurface*& simpleSurface) const;

  // Get a copy of the construction surface.
  //
  
  /**
    Description:
    Returns a pointer to the base *surface*.
    
    Arguments:
    baseSurface (O) Receives base *surface*.
  */
  void getConstructionSurface (
    OdGeSurface*& baseSurface) const;

  /**
    Description:
    Returns the offset *distance* of this *surface*. 
  */
  double offsetDist () const;

  // Reset surface
  //

  /**
    Description:
    Sets the parameters for this *surface* according to the arguments, 
    and returns a reference to this *surface*.

    Arguments:
    baseSurface (I) Any *surface*.
    offsetDistance (I) Offset *distance*.
    makeCopy (I) Makes a *copy* of baseSurface.
  */
  OdGeOffsetSurface& set (
    OdGeSurface* baseSurface, 
    double offsetDistance,
    bool makeCopy = true);

  // Assignment operator.
  //

  OdGeOffsetSurface& operator = (
    const OdGeOffsetSurface& surface);
};

#include "DD_PackPop.h"

#endif // OD_GEOFFSF_H


