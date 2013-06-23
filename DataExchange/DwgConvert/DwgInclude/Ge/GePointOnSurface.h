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



#ifndef OD_GEPONSRF_H
#define OD_GEPONSRF_H /* {Secret} */


#include "GePointEnt3d.h"

class OdGeSurface;

#include "DD_PackPush.h"

/**
    Description:
    This class represents points on surfaces.

    Library: Ge

    {group:OdGe_Classes} 

    Remarks:
    The *point* representation includes its *parameter* value, its coordinates, and
    the *derivatives* and *normal* of the *surface* at that *point*.
*/
class OdGePointOnSurface : public OdGePointEnt3d
{
public:
  /**
    Arguments:
    surface (I) Any *surface*.
    param (I) Parameter to specify a *point* on *surface*.
    source (I) Object to be cloned.

    Remarks:
    The default constructor constructs a *point* on the *surface* OdAcGePlane::kXYPlane with a *parameter* value of (0,0).
  */
  OdGePointOnSurface ();
  OdGePointOnSurface (
    const OdGeSurface& surface);
  OdGePointOnSurface (
    const OdGeSurface& surface, 
    const OdGePoint2d& param);
  OdGePointOnSurface (
    const OdGePointOnSurface& source);

  // Assignment operator.
  
  OdGePointOnSurface& operator = (
    const OdGePointOnSurface& pntOnSurface);

  /**
    Description:
    Returns a pointer to the *surface*.
  */
  const OdGeSurface* surface () const;
  
  /**
    Description:
    Returns the *parameter* value on the *surface* corresponding to the *point*.
  */
  OdGePoint2d parameter () const;

  /**
    Description:
    Returns the *point* on the *surface* corresponding to the *parameter*.
    
    Arguments:
    surface (I) Any *surface*.
    param (I) Parameter to specify a *point* on *surface*.
    
    Remarks:
    Specifying surface or param updates the underlying value.
  */
  OdGePoint3d point () const;
  OdGePoint3d point (
    const OdGePoint2d& param);
  OdGePoint3d point (
    const OdGeSurface& surface,
    const OdGePoint2d& param);

  
  /**
    Description:
    Returns the *normal* to the *surface* at the *point* corresponding to the *parameter*.
    
    Arguments:
    surface (I) Any *surface*.
    param (I) Parameter to specify a *point* on *surface*.
    
    Remarks:
    Specifying surface or param updates the underlying value.
  */
  OdGeVector3d normal () const;
  OdGeVector3d normal (
    const OdGePoint2d& param);
  OdGeVector3d normal (
    const OdGeSurface& surface,
    const OdGePoint2d& param);

  /**
    Description:
    Returns the specified derivative of the *surface* in the U direction  
    at the *point* corresponding to the *parameter*.
    
    Arguments:
    order (I) Specifies the *order* of the derivitive [1-2].
    surface (I) Any *surface*.
    param (I) Parameter to specify a *point* on *surface*.
    
    Remarks:
    Specifying surface or param updates the underlying value.
  */
  OdGeVector3d uDeriv (
    int order) const;
  OdGeVector3d uDeriv (
    int order, 
    const OdGePoint2d& param);
  OdGeVector3d uDeriv (
    int order, const OdGeSurface& surface,
    const OdGePoint2d& param);

  /**
    Description:
    Returns the specified derivative of the *surface* in the V direction 
    at the *point* corresponding to the *parameter*.
    
    Arguments:
    order (I) Specifies the *order* of the derivitive [1-2].
    surface (I) Any *surface*.
    param (I) Parameter to specify a *point* on *surface*.
    
    Remarks:
    Specifying surface or param updates the underlying value.
  */
  OdGeVector3d vDeriv (
    int order) const;
  OdGeVector3d vDeriv (
    int order, 
    const OdGePoint2d& param);
  OdGeVector3d vDeriv (
    int order, 
    const OdGeSurface& surface,
    const OdGePoint2d& param);


  /**
    Description:
    Returns the mixed partial derivative of the *surface* 
    at the *point* corresponding to the *parameter*.
    
    Arguments:
    surface (I) Any *surface*.
    param (I) Parameter to specify a *point* on *surface*.
    
    Remarks:
    Specifying surface or param updates the underlying value.
  */
  OdGeVector3d mixedPartial () const;
  OdGeVector3d mixedPartial (
    const OdGePoint2d& param);
  OdGeVector3d mixedPartial (
    const OdGeSurface& surface,
    const OdGePoint2d& param);

  /**
    Description:
    Maps the specified vector to the tangent *plane* of the *surface* 
    at the *point* corresponding to the *parameter*
    
    Arguments:
    surface (I) Any *surface*.
    param (I) Parameter to specify a *point* on *surface*.
    vect (I) Any 2D vector.
    
    Remarks:
    Specifying surface or param updates the underlying value.
    
    The tangent vector is mapped as follows:
    
            tangentVector = uDeriv(1) * vect.x + vDeriv(1) * vect.y
    
  */
  OdGeVector3d tangentVector (
    const OdGeVector2d& vect) const;
  OdGeVector3d tangentVector (
    const OdGeVector2d& vect,
    const OdGePoint2d& param);
  OdGeVector3d tangentVector (
    const OdGeVector2d& vect,
    const OdGeSurface& surface,
    const OdGePoint2d& param);

  /**
    Description:
    Projects the specified vector to the tangent *plane* of the *surface* 
    at the *point* corresponding to the *parameter*
    
    Arguments:
    surface (I) Any *surface*.
    param (I) Parameter to specify a *point* on *surface*.
    vect (I) Any 3D vector.
  */
  OdGeVector2d inverseTangentVector (
    const OdGeVector3d& vect) const;
  OdGeVector2d inverseTangentVector (
    const OdGeVector3d& vect,
    const OdGePoint2d& param);
  OdGeVector2d inverseTangentVector (
    const OdGeVector3d& vect,
    const OdGeSurface& surface,
    const OdGePoint2d& param);


  /**
    Description:
    Sets the current *surface*.

    Arguments:
    OdGeSurface (I) Any *surface*.
  */
   OdGePointOnSurface& setSurface (
    const OdGeSurface& surface);
    
  /**
    Description:
    Sets the current *parameter*.

    Arguments:
    param (I) Sets the current *parameter*.
  */
  OdGePointOnSurface& setParameter (
    const OdGePoint2d& param);
};

#include "DD_PackPop.h"

#endif // OD_GEPONSRF_H

