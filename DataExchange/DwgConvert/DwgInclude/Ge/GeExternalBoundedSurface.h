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



#ifndef OD_GEXBNDSF_H
#define OD_GEXBNDSF_H /* {Secret} */


#include "GeSurface.h"

#include "DD_PackPush.h"

class OdGeExternalSurface;
class OdGeCurveBoundary;

/**
    Description:
    This class represents bounded surfaces, whose definitions are external to the OdGe library.
    
    Remarks:
    Each instance of ns OdGeExternalBoundedSurface is comprosed of an instance
    of an OdExternalSurface and a collection of instances of AcGeCurveBoundary.
    
    OdGeExternalBoundedSurface instances can be treated as any other OdGeSurface. 
    
    You can access the OdGeExternalBoundedSurface as a corresponding native OdGeSurface, if such a corresponding
    surface exists, or you may (more efficiently) access the external data in its native form.
    
    One example of using OdGeExternalBoundedSurface is to access an ACIS surface. 
   
    Library: Ge

    {group:OdGe_Classes} 
*/
class OdGeExternalBoundedSurface : public OdGeSurface
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
  OdGeExternalBoundedSurface ();
  OdGeExternalBoundedSurface (
    void* surfaceDef, 
    OdGe::ExternalEntityKind surfaceKind, 
    bool makeCopy = true);
  OdGeExternalBoundedSurface (
    const OdGeExternalBoundedSurface& source);

  // Surface data.
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
    Returns true if and only if the surface is defined (not an empty instance).
  */
  bool isDefined () const;

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

  // Odcess to unbounded surface.
  //

  /**
    Description:
    Returns a pointer to the unbounded surface.
    
    Arguments:
    surfaceDef (O) Receives the unbounded surface definition.
    unboundedSurfaceDef (O) Receives the unbounded surface definition.
    
    Remarks:
    If called with a pointer to OdGeSurface, the OdExternalSurface
    will be converted to a native OdGeSurface, if possible.
  */   
  void getBaseSurface (
    OdGeSurface*& surfaceDef) const;
  void getBaseSurface (
    OdGeExternalSurface& unboundedSurfaceDef) const;

  // Type queries on the unbounded base surface.

  /**
    Description:
    Return true if and only if the unbounded base surface can be represented as an OdGePlanarEnt. 
  */
  bool isPlane () const;

  /**
    Description:
    Return true if and only if the unbounded base surface can be represented as an OdGeSphere. 
  */
  bool isSphere () const;

  /**
    Description:
    Return true if and only if the unbounded base surface can be represented as an OdGeCylinder. 
  */
  bool isCylinder () const;

  /**
    Description:
    Return true if and only if the unbounded base surface can be represented as an OdGeCone. 
  */
  bool isCone () const;
  
  /**
    Description:
    Return true if and only if the unbounded base surface can be represented as an OdGeEllipCone. 
  */
  bool isEllipCone () const;   // AE 03.09.2003 
  /**
    Description:
    Return true if and only if the unbounded base surface can be represented as an OdGeTorus. 
  */
  bool isTorus () const;
  
  /**
    Description:
    Return true if and only if the unbounded base surface can be represented as an OdGeNurbSurface. 
  */
  bool isNurbSurface () const;

  /**
    Description:
    Return true if and only if the unbounded base surface cannot be represented as an native OdGeSurface. 
  */
  bool isExternalSurface () const;

  // Odcess to the boundary data.
  //
  
  /**
    Description:
    Returns the number of *contours* on this surface.  
  */
  int numContours () const;
  /**
    Description:
    Returns an array of the *contours* on this surface.

    Arguments
    numContours (O) Receives the number of *contours* on this surface.
    contours (O) Receives the array of *contours*. 
  */
  void getContours (
    int& numContours, 
    OdGeCurveBoundary*& contours) const;

  // Set methods
  //
  
  /* Description:
    Sets the parameters for this external bound surface according to the arguments, 
    and returns a reference to this external surface.

    Arguments:
    surfaceDef (I) Pointer to the surface definition.
    surfaceKind (I) Information about system that created the surface.
    makeCopy (I) If true, makes a *copy* of surfaceDef.
  */
  OdGeExternalBoundedSurface& set (
    void* surfaceDef,
    OdGe::ExternalEntityKind surfaceKind, 
    bool makeCopy = true);

  // Assignment operator.
  //
  
  OdGeExternalBoundedSurface& operator = (
    const OdGeExternalBoundedSurface& extBoundSurf);

  // Surface ownership.
  //
  
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
  OdGeExternalBoundedSurface& setToOwnSurface ();
};

#include "DD_PackPop.h"

#endif // OD_GEXBNDSF_H

