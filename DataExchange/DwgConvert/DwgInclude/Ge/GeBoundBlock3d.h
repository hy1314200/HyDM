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



#ifndef _OD_GEBOUNDBLOCK3D_H
#define _OD_GEBOUNDBLOCK3D_H /* {Secret} */

#include "GeEntity3d.h"

#include "DD_PackPush.h"

/**
Description:
    This class implements 3D bounding volumes.

    Library: Ge

    {group:OdGe_Classes}
*/
class GE_TOOLKIT_EXPORT OdGeBoundBlock3d : public OdGeEntity3d
{
public: 

  /**
  Arguments:
    p1 (I) First *point* of a coordinate-aligned *block*.
    p2 (I) Second *point* of a coordinate-aligned *block*.
    base (I) Base of parallelepiped bounding *block*.
    side1 (I) First side of parallelepiped bounding *block*.
    side2 (I) Second side of parallelepiped bounding *block*.
    side3 (I) Third side of parallelepiped bounding *block*.
    source (I) Object to be cloned.

    Remarks:
    The default constructor constructs a parallelepiped reduced to the coordinate origin.
    
    A parallelepiped is a parallelogram extruded in an arbitrary direction. 
  */
  OdGeBoundBlock3d ();

  OdGeBoundBlock3d (
    const OdGePoint3d& base, 
    const OdGeVector3d& side1,
    const OdGeVector3d& side2, 
    const OdGeVector3d& side3);

  OdGeBoundBlock3d (
    const OdGePoint3d& p1, 
    const OdGePoint3d& p2);

  OdGeBoundBlock3d (
    const OdGeBoundBlock3d& source);

  OdGe::EntityId type () const;

  OdGeEntity3d& transformBy (
    const OdGeMatrix3d& xfm);

  OdGeEntity3d& translateBy (
    const OdGeVector3d& translateVec);

  OdGeEntity3d& rotateBy (
    double angle, 
    const OdGeVector3d& vect, 
    const OdGePoint3d& basePoint);

  OdGeEntity3d& mirror (const OdGePlane& plane);

  OdGeEntity3d& scaleBy (double scaleFactor, const OdGePoint3d& basePoint);

  /**
    Description
    Returns the extents of the bounding *block*.

    Arguments:
    p1 (O) Receives the first corner of the extents.
    p2 (O) Receives the second corner of the extents.
    
    Note:
    The returned values are meaningful if and only if this object is a coordinate-aligned box.
    
  */
  void getMinMaxPoints (
    OdGePoint3d& p1, 
    OdGePoint3d& p2) const;

  OdGePoint3d minPoint () const;
  OdGePoint3d maxPoint () const;

  /**
    Description:
    Returns the *center* of the bounding *block*.
  */
  OdGePoint3d center () const
  {
    return m_IsBox ? OdGePoint3d ( (m_Min.x + m_Max.x) / 2, (m_Min.y + m_Max.y) / 2, (m_Min.z + m_Max.z) / 2) : m_Min;
  }

  /**
    Description:
    Returns base and sides of bounding *block*.

    Arguments:
    base (O) Receives the *base* of the bounding box.
    side1 (O) Receives the first side.
    side2 (O) Receives the second side.
    side3 (O) Receives the third side.
  */
  void get (
    OdGePoint3d& base,
    OdGeVector3d& side1,
    OdGeVector3d& side2,
    OdGeVector3d& side3) const;

  /**
    Description:
    Sets the bounding *block* to a coordinate-aligned box or to
    a parallelepiped bounding *block*.

    Arguments:
    p1 (I) First *point* of a coordinate-aligned box.
    p2 (I) Second *point* of a coordinate-aligned box.
    base (I) Base of parallelepiped bounding *block*.
    side1 (I) First side of parallelepiped bounding *block*.
    side2 (I) Second side of parallelepiped bounding *block*.
    side3 (I) Third side of parallelepiped bounding *block*.
  */
  OdGeBoundBlock3d& set (
    const OdGePoint3d& p1,
    const OdGePoint3d& p2);

  OdGeBoundBlock3d& set (
    const OdGePoint3d& base,
    const OdGeVector3d& side1,
    const OdGeVector3d& side2,
    const OdGeVector3d& side3);
  /**
  Description:
    Extends the bounding *block* to contain
    the specified *point*.

    Arguments:
    point (I) Any 3D *point*.
  */
  OdGeBoundBlock3d& extend (
    const OdGePoint3d& point);

  /**
    Description:
    Moves the walls of the bounding *block* the specified *distance*.

    Arguments:
    distance (I) Distance.
  */
  OdGeBoundBlock3d& swell (
    double distance);

  /**
    Description:
    Returns true if and only if this *point* is
    contained in the bounding *block*.

    Arguments:
    point (I) Any 3D *point*.
  */
  bool contains (
    const OdGePoint3d& point) const;

  /**
  Description:
  Returns true if and only if specified bounding *block*
  does not intersect this bounding *block*.

  Arguments:
  block (I) Any 3D bounding *block*.
  */
  bool isDisjoint (const OdGeBoundBlock3d& block) const;

  OdGeBoundBlock3d& operator = (const OdGeBoundBlock3d& block);

  /**
    Description:
    Returns true if and only if this bounding *block* is a
    coordinate-aligned box.
  */
  bool isBox () const;

  /**
    Description:
    Sets this bounding *block* to a coordinate-aligned box, or a
    parallelogram bounding *block*. Returns a reference to this 
    bounding *block*.

    Arguments:
    toBox (I) If true, sets this bounding *block* to a 
    coordinate-aligned box; otherwise, sets it to a 
    parallelepiped bounding *block*.
  */
  OdGeBoundBlock3d&  setToBox (
    bool toBox);

protected:
   /* { Secret } */
  void transformU (
    const OdGeMatrix3d& xfm);
  /* { Secret } */
  bool m_IsBox;
  /* { Secret } */
  OdGePoint3d  m_Min;
  /* { Secret } */
  OdGePoint3d  m_Max;
  /* { Secret } */
  OdGeVector3d m_V[3]; 
};


#include "DD_PackPop.h"

#endif


