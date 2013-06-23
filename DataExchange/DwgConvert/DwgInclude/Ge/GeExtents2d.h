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
// with their copyright notices
//      DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef _ODGEEXTENTS2D_INCLUDED_
#define _ODGEEXTENTS2D_INCLUDED_ /* {Secret} */


#include "GePoint2d.h"
#include "GeVector2d.h"
#include "GeMatrix2d.h"



#define INVALIDEXTENTS 1.0e20 /* {Secret} */

/**
    Description:
    This class represents 2D bounding boxes as minimum and maximum 2d points.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeExtents2d
{
public:

  /**
    Arguments:
    min (I) Minimum *point*.
    max (I) Maximum *point*.
  */
  OdGeExtents2d ()
    : m_min (INVALIDEXTENTS,  INVALIDEXTENTS)
    , m_max (-INVALIDEXTENTS, -INVALIDEXTENTS)
  {}

  OdGeExtents2d (
    const OdGePoint2d& min, 
    const OdGePoint2d& max) 
  { set (min, max); }

  /**
    Description:
    Returns the minimum *point* of this *extents* box.
  */
  const OdGePoint2d& minPoint () const 
  { return m_min; }

  /**
    Description:
    Returns the maximum *point* of this *extents* box.
  */
  const OdGePoint2d& maxPoint () const 
  { return m_max; }

  /**
    Description:
    Sets the minimum and maximum points for this *extents* box.
    
    Arguments:
    min (I) Minimum *point*.
    max (I) Maximum *point*.
  */
  void set (
    const OdGePoint2d& min, 
    const OdGePoint2d& max) 
  { m_min = min; m_max = max; }

  /**
    Description:
    Updates the *extents* of this *extents* box with the specified *point*.

    Arguments:
    point (I) Any 2D *point*.
  */
  void addPoint (
    const OdGePoint2d& point)
  {
    if ( !isValidExtents() )
    {
      m_max = m_min = point;
    }
    else
    {
      m_max.x = odmax (point.x, m_max.x);
      m_max.y = odmax (point.y, m_max.y);
      m_min.x = odmin (point.x, m_min.x);
      m_min.y = odmin (point.y, m_min.y);
    }
  }
    

  /**
    Description:
    Updates the *extents* of this *extents* box with the specified *extents* box.
    
    Arguments:
    extents (I) Any 2D *extents* box.
  */
  void addExt (
    const OdGeExtents2d& extents)
  {
    if (extents.isValidExtents ())
    {
      addPoint (extents.minPoint ());
      addPoint (extents.maxPoint ());
    }
  }

  /**
    Description:
    Returns true if and only if this *extents* box *contains* valid *extents*.
    
    Remarks:
    Extents are valid if and only if each member of the minimum *extents* 
    is less than or equal to the corresponding member of maximum *extents*.
  */
  bool isValidExtents () const
  {
    return ( (m_max.x >= m_min.x) && (m_max.y >= m_min.y) );
  }

  /* Description:
    Updates the *extents* of this *extents* box by the specified vector.
    Arguments:
    vect (I) Any 2D vector.
  */  
  void expandBy (
    const OdGeVector2d& vect)
  {
    if (isValidExtents ())
    {
      OdGePoint2d p1 = m_min, p2 = m_max;
      p1 += vect;
      p2 += vect;
      addPoint (p1);
      addPoint (p2);
    }
  }

  /**
    Description:
    Applies the 2D transformation matrix to the *extents*.

    Arguments:
    xfm (I) 2D transformation matrix.
  */
  void transformBy (
    const OdGeMatrix2d& xfm)
  {
    OdGeVector2d vecX (OdGeVector2d::kXAxis * (m_max.x - m_min.x)),
      vecY (OdGeVector2d::kYAxis * (m_max.y - m_min.y));

    if (isValidExtents ())
    {
      m_max = m_min = (xfm * m_min);
      expandBy (xfm * vecX);
      expandBy (xfm * vecY);
    }
  }
   
    
  /**
    Description:
    Returns true if and only if this *extents* box *contains* the specified object.
    
    Arguments:
    point (I) Any 2D *point*.
    extents (I) Any 2D *extents* box.
  */
  bool contains (
    const OdGePoint2d& point) const
  {
    return ( point.x >= m_min.x  &&  point.y >= m_min.y  &&
              point.x <= m_max.x  &&  point.y <= m_max.y );
  }

  bool contains (
    const OdGeExtents2d& extents) const
  {
    return (extents.m_min.x >=         m_min.x && extents.m_min.y >=     m_min.y      &&
                    m_max.x >= extents.m_max.x &&          m_max.y >= extents.m_max.y );
  }


  /**
    Description:
    Returns true if and only if specified *extents* box
    does not intersect this one.

    Arguments:
    extents (I) Any 2D *extents* box.
  */
  bool isDisjoint (
    const OdGeExtents2d& extents) const
  {
    return (extents.m_min.x >         m_max.x || extents.m_min.y >         m_max.y ||
                    m_min.x > extents.m_max.x ||         m_min.y > extents.m_max.y );
  }    

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
    extents (I) Any 2D *extents* box.
    pResult (O) Receives a pointer to the *extents* box of the intersection.
    
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
  IntersectionStatus intersectWith (
    const OdGeExtents2d& extents, 
    OdGeExtents2d* pResult = 0) const;

protected:
  OdGePoint2d m_min;
  OdGePoint2d m_max;
};


#undef INVALIDEXTENTS

#endif //_ODGEEXTENTS2D_INCLUDED_



