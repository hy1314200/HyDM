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



#ifndef _OD_GEENTITY3D_H_
#define _OD_GEENTITY3D_H_ /* {Secret} */


#include "GeGbl.h"
#include "GePoint3d.h"
#include "OdHeap.h"

#include "DD_PackPush.h"

class OdGeMatrix3d;
class OdGePlane;
class OdGeEntity3dImpl;

/**
    Description:
    This class is the base class for all OdGe 3D geometric operations.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeEntity3d
{
public:
  ODRX_HEAP_OPERATORS ();

  virtual ~OdGeEntity3d () {}

  // Run time type information.

  /**
    Description:
    Returns true if and only if this entity is of *type* (or is derived from) entType.

    Arguments:
    entType (I) Entity *type* to test. 
  */
  virtual bool isKindOf (
    OdGe::EntityId entType) const;

  /**
    Description:
    Returns the entity *type*.
  */
  virtual OdGe::EntityId type () const;

  /**
    Description:
    Returns a pointer to a *copy* of this entity.

    Remarks:
  The returned object is created with the new operator, and it is the responsibility of the caller to delete it.
  */
  virtual OdGeEntity3d* copy () const;

  bool operator == (
    const OdGeEntity3d& entity) const;

  bool operator != (
    const OdGeEntity3d& entity) const;

  /**
    Returns true if the specified entity is equal to this one.

    Remarks:
    Returns true if and only if both entities are of the same *type*, have the same point *set* within the 
    specified tolerance, and have the same parameterization.

    Arguments:
    other (I) Entity to be compared
    tol (I) Geometric tolerance.
  */
  bool isEqualTo (
    const OdGeEntity3d& object, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Applies the 3D transformation matrix to this entity, and returns
    a reference to this entity.

    Arguments:
    xfm (I) 3D transformation matrix.
  */
  virtual OdGeEntity3d& transformBy (
    const OdGeMatrix3d& xfm);

  /**
    Description:
    Applies the 3D translation vector to this entity, and returns
    a reference to this entity.

    Arguments:
    translateVec (I) Translation Vector.
  */
  virtual OdGeEntity3d& translateBy (
    const OdGeVector3d& translateVec);

  /**
    Description:
    Rotates this entity by the specified *angle* about the axis
    defined by the point and the vector, and returns
    a reference to this entity.

    Arguments:
    angle (I) Rotation *angle*.
    vect (I) Vector about which entitiy is rotated. 
    basePoint (I) Basepoint.
  */
  virtual OdGeEntity3d& rotateBy (
    double angle, 
    const OdGeVector3d& vect,
    const OdGePoint3d& basePoint = OdGePoint3d::kOrigin);

  /**
    Description:
    Mirrors this entity about the specified *plane*, and returns
    a reference to this entity.

    Arguments:
    plane (I) Plane about which entity is to be mirrored.
  */
  virtual OdGeEntity3d& mirror (
    const OdGePlane& plane);

  /**
    Description:
    Scales this entity by the scale factor about the basepoint, and returns
    a reference to this entity.

    Arguments:
    scaleFactor (I) Scale Factor. Must be greater than 0.
    basePoint (I) Basepoint.
  */
  virtual OdGeEntity3d& scaleBy (
    double scaleFactor,
    const OdGePoint3d& basePoint = OdGePoint3d::kOrigin);

  /**
    Description:
    Returns true if and only if the specified *point* is on this entity, 
    as determined by the tolerance.

    Arguments:
    point (I) Point to be queried.
    tol (I) Geometric tolerance.
  */
  virtual bool isOn (
    const OdGePoint3d& point,
    const OdGeTol& tol = OdGeContext::gTol) const;
};

#include "DD_PackPop.h"

#endif //_OD_GEENTITY3D_H_


