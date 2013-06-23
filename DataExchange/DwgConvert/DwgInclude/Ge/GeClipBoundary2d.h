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



#ifndef OD_GE_CLIP_BOUNDARY_2D_H
#define OD_GE_CLIP_BOUNDARY_2D_H  /* {Secret} */

#include "GeEntity2d.h"
#include "GeIntArray.h"
#include "GePoint2dArray.h"

#include "DD_PackPush.h"

/**
    Description:
    This class implements a 2D clipping object for clipping 2D polygons and polylines
    into convex open or closed 2D polygons and polylines.

    Remarks:
    Similar to Sutherland-Hodgman pipeline clipping.
    
    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeClipBoundary2d : public OdGeEntity2d
{
  OdGePoint2dArray m_clipPoly;
public:
  /**
    Arguments:
    cornerA (I) First corner of rectangular clip boundary.
    cornerB (I) Opposite corner of rectangular clip boundary.
    clipBoundary (I) An array of *points* defining a counterclockwise,
        convex polygon.
    source (I) Object to be cloned.
  */
  OdGeClipBoundary2d ();

  OdGeClipBoundary2d (
    const OdGePoint2d& cornerA, 
    const OdGePoint2d& cornerB);

  OdGeClipBoundary2d (
    const OdGePoint2dArray& clipBoundary);

  OdGeClipBoundary2d (
    const OdGeClipBoundary2d& source);

  /**
    Description:
    Converts this clip boundary to a rectangular or poylgonal clip boundary.
    Returns the *status* of the conversion.

    Arguments:
    cornerA (I) First corner of rectangular clip boundary.
    cornerB (I) Opposite corner of rectangular clip boundary.
    clipBoundary (I) An array of *points* defining a counterclockwise,
    convex polygon.
  */
  OdGe::ClipError set (
    const OdGePoint2d& cornerA, 
    const OdGePoint2d& cornerB);

  OdGe::ClipError set (
    const OdGePoint2dArray& clipBoundary);

  /**
    Description: 
    Clips the specified closed polygon with this clip boundary. Returns the *status* of the clipping.

    Arguments:
    rawVertices (I) An array of 2d points defining the polygon to be clipped.
    The polygon can be concave and/or self-intersecting.
    clippedVertices (O) Receives an array of 2d points defining the clipped polygon.
    clipCondition (O) Receives spatial information about the clip boundary
                      and the polygon.
    pClippedSegmentSourceLabel (I) An array of integers identifying
                                   the source segment of each segment in the clipped polygon.

    Remarks:
    Possible values for clipCondition are as follows:

    @untitled table
    kInvalid                        
    kAllSegmentsInside              
    kSegmentsIntersect              
    kAllSegmentsOutsideZeroWinds    
    kAllSegmentsOutsideOddWinds     
    kAllSegmentsOutsideEvenWinds

    Note:
    As implemented, this object does nothing but
    return OdGe::eOk.
    It will be fully implemented in a future *release*.

  */
  OdGe::ClipError clipPolygon (
    const OdGePoint2dArray& rawVertices,
    OdGePoint2dArray& clippedVertices,
    OdGe::ClipCondition& clipCondition,
    OdGeIntArray* pClippedSegmentSourceLabel = 0) const;

  /**
    Description: 
    Clips the specified closed polyline with this clip boundary. Returns the *status* of the clipping.

    Arguments:
    rawVertices (I) An array of 2d points defining the polyline to be clipped.
    The polyline can be concave and/or self-intersecting.
    clippedVertices (O) Receives an array of 2d points defining the clipped polyline.
    clipCondition (O) Receives spatial information about the clip boundary
                      and the polyline.
    pClippedSegmentSourceLabel (I) An array of integers identifying
                                   the source segment of each segment in the clipped polygon.
    Remarks:
    Possible values for clipCondition are as follows:

    @untitled table
    kInvalid
    kAllSegmentsInside
    kSegmentsIntersect
    kAllSegmentsOutsideZeroWinds    
    kAllSegmentsOutsideOddWinds     
    kAllSegmentsOutsideEvenWinds    
  */
  OdGe::ClipError clipPolyline (
    const OdGePoint2dArray& rawVertices, 
    OdGePoint2dArray& clippedVertices,
    OdGe::ClipCondition& clipCondition,
    OdGeIntArray* pClippedSegmentSourceLabel = 0) const;

  OdGeClipBoundary2d& operator = (const OdGeClipBoundary2d& clipBoundary);

  OdGe::EntityId type () const;
};

#include "DD_PackPop.h"

#endif

