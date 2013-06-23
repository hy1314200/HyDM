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



#ifndef OD_GEEXTC3D_H
#define OD_GEEXTC3D_H /* {Secret} */

class OdGeLine3d;
class OdGeLineSeg3d;
class OdGeRay3d;
class OdGeEllipArc3d;
class OdGeNurbCurve3d;
class OdGeExternalCurve2d;
class OdGeExternalCurve3d;

#include "GeCurve3d.h"

#include "DD_PackPush.h"

/**
    Description:
    This class represents, as OdGeCurve3d curves, 3D curves whose definitions are external to the OdGe library.
    
    Remarks:
    OdGeExternalCurve3d curve instances can be treated as any other OdCurve3d. 
    
    You can access the OdGeExternalCurve3d as a corresponding native OdGeCurve3d curve, if such a corresponding
    curve exists, or you may (more efficiently) access the external data in its native form.
    
    One example of using OdGeOdGeExternalCurve3d is to represent an ACIS curve. 
    
    Library: Ge

    {group:OdGe_Classes} 
*/
class OdGeExternalCurve3d : public OdGeCurve3d
{
public:
  /**
    Arguments:
    curveDef (I) Pointer to the curve definition.
    curveKind (I) Information about system that created the *curve*.
    makeCopy (I) Makes a *copy* of curveDef.
    source (I) Object to be cloned.

    Remarks:
    Without arguments, the constructor creates an empty instance.

    Possible values for curveKind:

    @untitled table
    kAcisEntity
    kExternalEntityUndefined
  */
  OdGeExternalCurve3d ();
  OdGeExternalCurve3d (
    const OdGeExternalCurve3d& source);
  OdGeExternalCurve3d (
    void* curveDef, 
    OdGe::ExternalEntityKind curveKind,
    bool makeCopy = true);

  /**
    Description:
    Return true if and only if the external curve can be represented as an OdGeLine3d. 
  */
  bool isLine () const;

  /**
    Description:
    Return true if and only if the external curve can be represented as an OdGeRay3d. 
  */
  bool isRay () const;

  /**
    Description:
    Return true if and only if the external curve can be represented as an OdGeLineSeg3d. 
  */
  bool isLineSeg () const;

  /**
    Description:
    Return true if and only if the external curve can be represented as an OdGeCircArc3d. 
  */
  bool isCircArc () const;

  /**
    Description:
    Return true if and only if the external curve can be represented as an OdGeEllipArc3d. 
  */
  bool isEllipArc () const;

  /**
    Description:
    Return true if and only if the external curve can be represented as an OdGeNurbCurve3d. 
  */
  bool isNurbCurve () const;
  
  /**
    Description:
    Returns true if and only if the curve is defined (not an empty instance).
  */
  bool isDefined () const;

  /**
    Description:
    Return true if and only if the external curve can be represented as a native OdGeCurve3d,
    and returns a pointer to an instance of that native *curve*.

    Arguments:
    nativeCurve (O) Receives the native *curve*.

    Remarks:
    The returned object is created with the new operator, and it is the responsibility of the caller to delete it.
  */
  bool isNativeCurve (
    OdGeCurve3d*& nativeCurve) const;

  /**
    Description:
    Returns a pointer to a *copy* of the raw curve definition.

    Arguments:
    curveDef (O) Receives a pointer to a *copy* of the raw curve definition.

    Remarks:
    It is up to the caller to delete the memory allocated.
  */
  void getExternalCurve (
    void*& curveDef) const;

  /**
    Description:
    Rerurns information about the system that created the *curve*.

    Remarks:
    Possible values for curveKind:

    @untitled table
    kAcisEntity
    kExternalEntityUndefined
  */
  OdGe::ExternalEntityKind externalCurveKind () const;

  /* Description:
    Sets the parameters for this external curve according to the arguments, 
    and returns a reference to this external *curve*.

    Arguments:
    curveDef (I) Pointer to the curve definition.
    curveKind (I) Information about system that created the *curve*.
    makeCopy (I) If true, makes a *copy* of curveDef.
  */
  OdGeExternalCurve3d& set (
    void* curveDef, 
    OdGe::ExternalEntityKind curveKind,
    bool makeCopy = true);

  OdGeExternalCurve3d& operator = (
    const OdGeExternalCurve3d& extCurve);

  /**
    Description:
    Returns true if and only if the external curve owns the data.

    Remarks:
    If the external curve owns the data, it will be destroyed when
    the curve is destroyed.
  */
  bool isOwnerOfCurve () const;

  /**
    Description:
    Forces this external curve to own the data, and returns a reference to this *curve*.

    Remarks:
    If the external curve owns the data, it will be destroyed when
    the curve is destroyed.
  */
  OdGeExternalCurve3d& setToOwnCurve ();
};

#include "DD_PackPop.h"

#endif // OD_GEEXTC3D_H


