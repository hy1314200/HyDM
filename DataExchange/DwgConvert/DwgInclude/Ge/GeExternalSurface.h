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



#ifndef OD_GEEXTSF_H
#define OD_GEEXTSF_H /* {Secret} */

#include "GeSurface.h"

#include "DD_PackPush.h"

class OdGePlane;
class OdGeCylinder;
class OdGeCone;
class OdGeSphere;
class OdGeTorus;
class OdGeNurbSurface;
class OdGeEllipCone;            // AE 03.09.2003 
class OdGeEllipCylinder;    // AE 09.09.2003    

/**
    Description:
    This class represents unbounded surfaces, whose definitions are external to the OdGe library, as OdGeSurface.
    
    Remarks:
    OdGeExternalSurface instances can be treated as any other OdGeSurface. 
    
    You can access the OdGeExternalSurface as a corresponding native OdGeSurface, if such a corresponding
    surface exists, or you may (more efficiently) access the external data in its native form.
    
    One example of using OdGeExternalSurface is to access an ACIS surface. 
    
    Library: Ge

    {group:OdGe_Classes} 
*/
class OdGeExternalSurface : public OdGeSurface
{
public:
  /**
    Arguments:
    surfaceDef (I) Pointer to the surface definition.
    surfaceKind (I) Information about system that created the surface.
    makeCopy (I) Makes a *copy* of curveDef.
    source (I) Object to be cloned.
    
    Remarks:
    Without arguments, the constructor creates an empty instance.

    Possible values for surfaceKind:
    
    @untitled table
    kAcisEntity
    kExternalEntityUndefined
  */
  OdGeExternalSurface ();
  OdGeExternalSurface (
    void* surfaceDef, 
    OdGe::ExternalEntityKind surfaceKind,
    bool makeCopy = true);
  OdGeExternalSurface (
    const OdGeExternalSurface& source);

  /**
    Description:
    Returns a pointer to a *copy* of the raw surface definition.

    Arguments:
    curveDef (O) Receives a pointer to a *copy* of the raw surface definition.

    Remarks:
    It is up to the caller to delete the memory allocated.
  */
  void getExternalSurface (
    void*& surfaceDef) const;

  // Type of the external surface.
  //

  /**
    Description:
    Rerurns information about the system that created the surface.

    Remarks:
    Possible values for externalSurfaceKind:

    @untitled table
    kAcisEntity
    kExternalEntityUndefined
  */
  OdGe::ExternalEntityKind externalSurfaceKind () const;

  /**
    Description:
    Return true if and only if the surface can be represented as an OdGePlanarEnt. 
  */
  bool isPlane () const;

  /**
    Description:
    Return true if and only if the external surface can be represented as an OdGeSphere. 
  */
  bool isSphere () const;

  /**
    Description:
    Return true if and only if the external surface can be represented as an OdGeCylinder. 
  */
  bool isCylinder () const;

  /**
    Description:
    Return true if and only if the external surface can be represented as an OdGeCone. 
  */
  bool isCone () const;

  /**
    Description:
    Return true if and only if the external surface can be represented as an OdGeTorus. 
  */
  bool isTorus () const;
  
  /**
    Description:
    Return true if and only if the external surface can be represented as an OdGeNurbSurface. 
  */
  bool isNurbSurface () const;

  /**
    Description:
    Returns true if and only if the surface is defined (not an empty instance).
  */
  bool isDefined () const;

  /**
    Description:
    Return true if and only if the external surface can be represented as an OdGeEllipCone. 
  */
  bool isEllipCone () const; 
  // AE 03.09.2003 


  /**
    Description:
    Return true if and only if the external surface can be represented as a native OdGeSurface,
    and returns a pointer to an instance of that native surface.

    Arguments:
    nativeSurface (O) Receives the native surface.

    Remarks:
    The returned object is created with the new operator, and it is the responsibility of the caller to delete it.
  */
  bool isNativeSurface (
    OdGeSurface*& nativeSurface) const;

  // Assignment operator.
  //

  OdGeExternalSurface& operator = (
    const OdGeExternalSurface& extSurf);

  // Reset surface
  //

  /* Description:
    Sets the parameters for this external surface according to the arguments, 
    and returns a reference to this external surface.

    Arguments:
    surfaceDef (I) Pointer to the surface definition.
    surfaceKind (I) Information about system that created the surface.
    makeCopy (I) If true, makes a *copy* of surfaceDef.
  */
  OdGeExternalSurface& set (
    void* surfaceDef,
    OdGe::ExternalEntityKind surfaceKind,
    bool makeCopy = true);

  /**
    Description:
    Returns true if and only if the external surface owns the data.

    Remarks:
    If the external surface owns the data, it will be destroyed when
    the curve is destroyed.
  */
  bool isOwnerOfSurface () const;
  
  /**
    Description:
    Forces this external surface to own the data, and returns a reference to this external surface.

    Remarks:
    If the external surface owns the data, it will be destroyed when
    the external surface is destroyed.
  */
  OdGeExternalSurface& setToOwnSurface ();
};

#include "DD_PackPop.h"

#endif // OD_GEEXTSF_H


