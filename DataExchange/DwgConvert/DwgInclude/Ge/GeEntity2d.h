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



#ifndef OD_GE_ENTITY_2D_H
#define OD_GE_ENTITY_2D_H /* {Secret} */


class OdGeMatrix2d;
class OdGeLine2d;

#include "GePoint2d.h"
#include "OdHeap.h"

#include "DD_PackPush.h"

/**
    Description:
    This class is the base class for all OdGe 2D geometric operations.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeEntity2d
{
public:
  ODRX_HEAP_OPERATORS ();

  virtual ~OdGeEntity2d () {}

  /**
    Description:
    Returns true if and only if this entity is of  *type* (or is derived from) entType.

    Arguments:
    entType (I) Entity *type* to test. 
  */
  virtual bool isKindOf (
    OdGe::EntityId entType) const;

  /**
    Description:
    Returns the entity *type* of this entity.
  */
  virtual OdGe::EntityId type () const = 0;

  /**
    Description:
    Returns a pointer to a *copy* of this entity.

    Remarks:
    The returned object is created with the new operator, and it is the responsibility of the caller to delete it.
  */
  virtual OdGeEntity2d* copy () const;

  bool operator == (
    const OdGeEntity2d& entity) const;

  bool operator != (
    const OdGeEntity2d& entity) const;

  /**
    Description:
    Returns true if the specified entity is equal to this one.

    Remarks:
    Returns true if and only if both entities are of the same *type*, have the same point *set* within the 
    specified tolerance, and have the same parameterization.

    Arguments:
    other (I) Entity to be compared
    tol (I) Geometric tolerance.
  */
  bool isEqualTo (
    const OdGeEntity2d& other, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Applies the 2D transformation matrix to this entity, and returns
    a reference to this entity.

    Arguments:
    xfm (I) 2D transformation matrix.
  */
  virtual OdGeEntity2d& transformBy (
    const OdGeMatrix2d& xfm);

  /**
    Description:
    Applies the 2D translation vector to this entity, and returns
    a reference to this entity.

    Arguments:
    translateVec (I) Translation Vector.
  */
  virtual OdGeEntity2d& translateBy (
    const OdGeVector2d& translateVec);

  /**
    Description:
    Rotates this entity about the specified *point* by the specified *angle*, and returns
    a reference to this entity.

    Arguments:
    angle (I) Rotation *angle*.
    basePoint (I) Basepoint.
  */
  virtual OdGeEntity2d& rotateBy (
    double angle, 
    const OdGePoint2d& basePoint = OdGePoint2d::kOrigin);

  /**
    Description:
    Mirrors this entity about the specified 2D *line*, and returns
    a reference to this entity.

    Arguments:
    line (I) Mirror *Line*.
  */
//  virtual OdGeEntity2d& mirror (
//    const OdGeLine2d& line);

  /**
    Description:
    Scales this entity by the scale factor about the basepoint, and returns
    a reference to this entity.

    Arguments:
    scaleFactor (I) Scale Factor. The scale factor must be greater than zero.
    basePoint (I) Basepoint.
  */
  virtual OdGeEntity2d& scaleBy (
    double scaleFactor, 
    const OdGePoint2d& basePoint = OdGePoint2d::kOrigin);

  /**
    Description:
    Returns true if and only if the specified *point* is on this entity, as determined by the tolerance.

    Arguments:
    point (I) Point to be queried.
    tol (I) Geometric tolerance.
  */
  virtual bool isOn (
    const OdGePoint2d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

protected:
  OdGeEntity2d () {}

  /*:>
  OdGeImpEntity3d* mpImpEnt;
  int mDelEnt;
  OdGeEntity2d (const OdGeEntity2d&);
  OdGeEntity2d (OdGeImpEntity3d&, int);
  OdGeEntity2d (OdGeImpEntity3d*);
  OdGeEntity2d* newEntity2d (OdGeImpEntity3d*) const;
  */
};

#include "DD_PackPop.h"

#endif // OD_GE_ENTITY_2D_H


