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



#ifndef _ODGEEXTENTS3D_INCLUDED_
#define _ODGEEXTENTS3D_INCLUDED_ /* {Secret} */


#include "GePoint3d.h"
#include "GeVector3d.h"
#include "GeMatrix3d.h"



#define INVALIDEXTENTS 1.0e20  /* {Secret} */

/**
    Description:
    This class represents 3D bounding boxes as minimum and maximum 3d points.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeExtents3d
{
public:

  /**
    Arguments:
    min (I) Minimum *point*.
    max (I) Maximum *point*.
  */
  OdGeExtents3d();

  OdGeExtents3d(
    const OdGePoint3d& min, 
    const OdGePoint3d& max);

  /**
    Description:
    Returns the minimum *point* of this *extents* box.
  */
  const OdGePoint3d& minPoint() const;

  /**
    Description:
    Returns the maximum *point* of this *extents* box.
  */
  const OdGePoint3d& maxPoint() const;

  /**
    Description:
    Sets the minimum and maximum points for this *extents* box.
    
    Arguments:
    min (I) Minimum *point*.
    max (I) Maximum *point*.
  */
  void set(
    const OdGePoint3d& min, 
    const OdGePoint3d& max);

  /**
    Description:
    Sets the minimum and maximum points for this *extents* box to
    that of the box defined by pt1 and pt2.
    
    Arguments:
    pt1 (I) First *point*.
    pt2 (I) Second *point*.

    Remarks:
    pt1 and pt2 need only define a box. They need not be the minimum
    and maximum points of the box.
  */
  void comparingSet(
    const OdGePoint3d& pt1, 
    const OdGePoint3d& pt2);

  /**
    Description:
    Updates the *extents* of this *extents* box with the specified *point*.

    Arguments:
    point (I) Any 3D *point*.
  */
  void addPoint(
    const OdGePoint3d& point);

  /**
    Description:
    Updates the *extents* of this *extents* box with the specified *extents* box.
    
    Arguments:
    extents (I) Any 3D *extents* box.
  */
  void addExt(
    const OdGeExtents3d& extents);

  /**
    Description:
    Returns true if and only if this *extents* box *contains* valid *extents*.
    
    Remarks:
    Extents are valid if and only if each member of the minimum *extents* 
    is less than or equal to the corresponding member of maximum *extents*.
  */
  inline bool isValidExtents() const
  {
    return ( (m_max.x >= m_min.x) && (m_max.y >= m_min.y) && (m_max.z >= m_min.z));
  }

  /* Description:
    Updates the *extents* of this *extents* box by the specified vector.
    Arguments:
    vect (I) Any 3D vector.
  */  
  void expandBy(
    const OdGeVector3d& vect);

  /**
    Description:
    Applies the 3D transformation matrix to the *extents*.

    Arguments:
    xfm (I) 3D transformation matrix.
  */
  void transformBy(
    const OdGeMatrix3d& xfm);
    
  /**
    Description:
    Returns true if and only if this *extents* box *contains* the specified object.
    
    Arguments:
    point (I) Any 3D *point*.
    extents (I) Any 3D *extents* box.
  */
  bool contains(
    const OdGePoint3d& point) const;

  bool contains(
    const OdGeExtents3d& extents) const;

  /**
    Description:
    Returns true if and only if specified *extents* box
    does not intersect this one.

    Arguments:
    extents (I) Any 3D *extents* box.
  */
  bool isDisjoint(
    const OdGeExtents3d& extents) const;

  enum IntersectionStatus
  {
    kIntersectUnknown,// Either or both extents are invalid
    kIntersectNot,    // Extents are NOT intersecting
    kIntersectOpIn,   // Operand is completely within this extents
    kIntersectOpOut,  // This extents is completely within operand
    kIntersectOk      // Extents are intersecting, result is returned
  };

  /**
    Description:
    Determines the intersection of the specified *extents* box with this one,
    and returns the resulting intersection box.
    
    Arguments:
    extents (I) Any 3D *extents* box.
    pResult (O) Receives a pointer to the *extents* box of intersection.
    
    Remarks:
    Possible return values are as follows.
    
    @untitled table
    kIntersectUnknown   Either or both *extents* boxes are invalid
    kIntersectNot       The *extents* boxes are NOT intersecting
    kIntersectOpIn      The specified *extents* box is completely within this one
    kIntersectOpOut     This *extents* box is completely within the specified one
    kIntersectOk        The *extents* boxes are intersecting, and a result is returned
    
    The returned object is created with the new operator, and it is the responsibility of the caller to delete it.
  */
  IntersectionStatus intersectWith(const OdGeExtents3d& extents, OdGeExtents3d* pResult = 0) const;

protected:
  OdGePoint3d m_min;
  OdGePoint3d m_max;
};



inline OdGeExtents3d::OdGeExtents3d()
  : m_min(INVALIDEXTENTS,  INVALIDEXTENTS,  INVALIDEXTENTS)
  , m_max(-INVALIDEXTENTS, -INVALIDEXTENTS, -INVALIDEXTENTS)
{
}

inline OdGeExtents3d::OdGeExtents3d(const OdGePoint3d& min, const OdGePoint3d& max)
  : m_min(min)
  , m_max(max)
{
}

inline const OdGePoint3d& OdGeExtents3d::minPoint() const
{
  return m_min;
}

inline const OdGePoint3d& OdGeExtents3d::maxPoint() const
{
  return m_max;
}

inline void OdGeExtents3d::set(const OdGePoint3d& min, const OdGePoint3d& max)
{
  m_min = min;
  m_max = max;
}

inline void OdGeExtents3d::comparingSet(const OdGePoint3d& pt1, const OdGePoint3d& pt2)
{
  if(pt1.x > pt2.x)
  {
    m_max.x = pt1.x;
    m_min.x = pt2.x;
  }
  else
  {
    m_min.x = pt1.x;
    m_max.x = pt2.x;
  }
  if(pt1.y > pt2.y)
  {
    m_max.y = pt1.y;
    m_min.y = pt2.y;
  }
  else
  {
    m_min.y = pt1.y;
    m_max.y = pt2.y;
  }
  if(pt1.z > pt2.z)
  {
    m_max.z = pt1.z;
    m_min.z = pt2.z;
  }
  else
  {
    m_min.z = pt1.z;
    m_max.z = pt2.z;
  }
}

inline void OdGeExtents3d::addPoint(const OdGePoint3d& point)
{
  if ( !isValidExtents() )
  {
    m_max = m_min = point;
  }
  else
  {
    m_max.x = odmax(point.x, m_max.x);
    m_max.y = odmax(point.y, m_max.y);
    m_max.z = odmax(point.z, m_max.z);
    m_min.x = odmin(point.x, m_min.x);
    m_min.y = odmin(point.y, m_min.y);
    m_min.z = odmin(point.z, m_min.z);
  }
}

inline void OdGeExtents3d::addExt(const OdGeExtents3d& extents)
{
  ODA_ASSERT(extents.isValidExtents());

  addPoint(extents.minPoint());
  addPoint(extents.maxPoint());
}

inline void OdGeExtents3d::expandBy(const OdGeVector3d& vect)
{
  ODA_ASSERT(isValidExtents());

  OdGePoint3d p1 = m_min, p2 = m_max;
  addPoint(p1 + vect);
  addPoint(p2 + vect);
}    

inline void OdGeExtents3d::transformBy(const OdGeMatrix3d& xfm)
{
  ODA_ASSERT(isValidExtents());

  OdGeVector3d d = m_max - m_min;
  
  m_max = m_min = (xfm * m_min);
  if(OdNonZero(d.x, 1.e-200))
    expandBy(xfm * (OdGeVector3d::kXAxis * d.x));
  if(OdNonZero(d.y, 1.e-200))
    expandBy(xfm * (OdGeVector3d::kYAxis * d.y));
  if(OdNonZero(d.z, 1.e-200))
    expandBy(xfm * (OdGeVector3d::kZAxis * d.z));
}    

inline bool OdGeExtents3d::contains(const OdGePoint3d& point) const
{
  ODA_ASSERT(isValidExtents());

  return ( point.x >= m_min.x  &&  point.y >= m_min.y  &&  point.z >= m_min.z  &&
           point.x <= m_max.x  &&  point.y <= m_max.y  &&  point.z <= m_max.z );
}

inline bool OdGeExtents3d::contains(const OdGeExtents3d& extents) const
{
  ODA_ASSERT(isValidExtents());

  return (extents.m_min.x >=         m_min.x && extents.m_min.y >=     m_min.y      && extents.m_min.z >=         m_min.z &&
                  m_max.x >= extents.m_max.x &&          m_max.y >= extents.m_max.y &&         m_max.z >= extents.m_max.z);
}    

inline bool OdGeExtents3d::isDisjoint(const OdGeExtents3d& extents) const
{
  ODA_ASSERT(isValidExtents());

  return (extents.m_min.x >         m_max.x || extents.m_min.y >         m_max.y || extents.m_min.z >         m_max.z||
                  m_min.x > extents.m_max.x ||         m_min.y > extents.m_max.y ||         m_min.z > extents.m_max.z);
}    

#undef INVALIDEXTENTS

#endif //_ODGEEXTENTS3D_INCLUDED_



