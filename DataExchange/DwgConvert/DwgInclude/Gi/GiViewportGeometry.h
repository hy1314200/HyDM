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



#ifndef _OD_GIVIEWPORT_GEOMETRY_H_
#define _OD_GIVIEWPORT_GEOMETRY_H_

#include "Gi.h"
#include "GiGeometry.h"

class OdGePoint3d;
class OdGeVector3d;
class OdGeMatrix2d;
class OdGiRasterImage;
class OdGiMetafile;
class OdGiCommonDraw;
class OdGsDCRect;

#include "DD_PackPush.h"


/** { Secret } */ 
class FIRSTDLL_EXPORT OdGiSelfGdiDrawable : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdGiSelfGdiDrawable);

  virtual void draw(const OdGiCommonDraw& drawObj,
                    void* hdc,
                    const OdGsDCRect& rect) const = 0;
};



/** Description:
    This class defines functions that allow entities to vectorize themselves.
    
    Remarks:
    Vectorization with the methods of this class are viewport dependent. 
    For viewport specific vectorization, use viewportDraw() instead of worldDraw().

    An OdGiViewportGeometry object passed to the viewportDraw()
    method of an entity.

    Model coordinates are applied to all geometry in this class except for setExtents(). 
   
    You can obtain objects of the following classes fromOdGiViewportDraw:

    @table
    Class                 Description
    OdGiSubEntityTraits   Control of drawing attributes and selection markers. 
    OdGiViewportGeometry  Drawing model coordinate geometry and transforms. 

    Library:
    Gi
    
    {group:OdGi_Classes} 
*/
class FIRSTDLL_EXPORT OdGiViewportGeometry : public OdGiGeometry
{
public:
  ODRX_DECLARE_MEMBERS(OdGiViewportGeometry);

  /** Description:
    Draws a *polyline*, with coordinates specified in Eye Coordinates.

    Arguments:
    nVertices (I) Number of vertices.
    vertexList (I) Array of vertices.

    Remarks:
    This *polyline* is drawn with a continuous linetype.

    See Also:
    Coordinate Systems
  */
  virtual void polylineEye(
    OdUInt32 nVertices, 
    const OdGePoint3d* vertexList) = 0;

  /** Description:
    Draws a filled *polygon*, with coordinates specified in Eye Coordinates.

    Arguments:
    nVertices (I) Number of points in the *polygon*.
    vertexList (I) Points of the *polygon*.

    See Also:
    Coordinate Systems
  */
  virtual void polygonEye(
    OdUInt32 nVertices, 
    const OdGePoint3d* vertexList) = 0;

  /** Description:
    Draws a *polyline*, with coordinates specified in Normalized Device Coordinates.

    Arguments:
    nVertices (I) Number of points in the *polyline*.
    vertexList (I) Points of the *polyline*.

    Remarks:
    This *polyline* is drawn with a continuous linetype.

    See Also:
    Coordinate Systems
  */
  virtual void polylineDc(
    OdUInt32 nVertices, 
    const OdGePoint3d* vertexList) = 0;

  /** Description:
    Draws a filled *polygon*, with coordinates specified in Normalized Device Coordinates.

    Arguments:
    nVertices (I) Number of points in the *polygon*.
    vertexList (I) Points of the *polygon*.

    See Also:
    Coordinate Systems
  */
  virtual void polygonDc(
    OdUInt32 nVertices, 
    const OdGePoint3d* vertexList) = 0;

  enum ImageSource
  {
    kFromDwg        = 0,
    kFromOleObject  = 1,
    kFromRender     = 2
  };

  /** Description:
    Draws the specified raster image.

    Arguments:
    origin (I) Image *origin*.
    u (I) Image width vector.
    v (I) Image height vector.
    pImage (I) Pointer to the image object.
    uvBoundary (I) Array of image boundary points (may not be null).
    numBoundPts (I) Number of boundary points.
    transparency (I) True if and only if image *transparency* is on.
    brightness (I) Image *brightness* ([0,100], default is 50).
    contrast (I) Image *contrast* ([0,100], default is 50).
    fade (I) Image *fade* value ([0,100], default is 0).
*/
  virtual void rasterImageDc(
    const OdGePoint3d& origin,
    const OdGeVector3d& u,
    const OdGeVector3d& v,
    const OdGiRasterImage* pImage, 
    const OdGePoint2d* uvBoundary, 
    OdUInt32 numBoundPts,
    bool transparency = false,
    double brightness = 50.0,
    double contrast = 50.0,
    double fade = 0.0) = 0;

  /** Description:
    Draws the specified Windows Metafile.

    Arguments:
    origin (I) Metafile *origin*.
    u (I) Metafile width vector.
    v (I) Metafile height vector.
    pMetafile (I) Pointer to the Metafile object.
    bDcAligned (I) reserved
    bAllowClipping (I) reserved
  */
  virtual void metafileDc(
    const OdGePoint3d& origin,
    const OdGeVector3d& u,
    const OdGeVector3d& v,
    const OdGiMetafile* pMetafile,
    bool bDcAligned = true,
    bool bAllowClipping = false) = 0;

  /** { Secret } */
  virtual void ownerDrawDc(
    const OdGePoint3d& origin,
    const OdGeVector3d& u,
    const OdGeVector3d& v,
    const OdGiSelfGdiDrawable* pDrawable,
    bool bDcAligned = true,
    bool bAllowClipping = false);
};

#include "DD_PackPop.h"

#endif


