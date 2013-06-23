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



#ifndef __ODGICONVEYORGEOMETRY_H__
#define __ODGICONVEYORGEOMETRY_H__


#include "Gi/GiGeometry.h"
#include "Gi/GiCommonDraw.h"
#include "Gs/GsSelectionReactor.h"

class OdGiRasterImage;
class OdGiMetafile;
class OdGiViewport;
class OdGsView;
class OdGiCommonDraw;
class OdGiSubEntityTraitsData;


/** Description:
    Interface that allows conveyor nodes to interact with the parent object
    (e.g. OdGiBaseVectorizer derived).  This interface gives
    abstract access to OdGiBaseVectorizer level data and services.
    Many conveyor nodes require a pointer to an OdGiConveyorContext interface,
    in order to be initialized.

    {group:OdGi_Classes} 
*/
class OdGiConveyorContext
{
public:
  /** Description:
      Returns the OdGiContext object associated with this object.
  */
  virtual OdGiContext& giContext() const = 0;

  /** Description:
      Returns the OdGiSubEntityTraits instance associated with this object.
      The OdGiSubEntityTraits object is used to register attribute changes with
      the vectorization framework.
  */
  virtual OdGiSubEntityTraits& subEntityTraits() const = 0;

  /** Description:
      Computes and returns the "effective traits", or the set of attributes
      currently in effect, for this context.

  */
  virtual const OdGiSubEntityTraitsData& effectiveTraits() const = 0;

  /** Description:
      Returns an OdGiDrawableDesc instance, which contains information about the 
      current drawable object (the object itself, rendering flags, etc.).
  */
  virtual const OdGiDrawableDesc* currentDrawableDesc() const = 0;

  /** Description:
      Returns a pointer to the current drawable object (the object that is currently
      being vectorized by the framework).
  */
  virtual const OdGiDrawable* currentDrawable() const = 0;

  /** Description:
      Returns the OdGiViewport associated with this object.
  */
  virtual const OdGiViewport* giViewport() const = 0;

  /** Description:
      Returns the OdGsView associated with this object.
  */
  virtual const OdGsView* gsView() const = 0;

  /** Description:
      Called by the vectorization framework before sending each portion of 
      geometry with constant rendering attributes, through the geometry conveyor.

      Remarks:
      The main purpose of this callback is to provide a hook
      for setting rendering attributes, to the final geometry handler (e.g. Win GDI device).
  */
  virtual void onTraitsModified() = 0;
  
  /** Description:
      Returns if regen was aborted (need, e.g. in simplifier, to decide to stop pass along
      set of result primitives)
  */
  virtual bool regenAbort() const = 0;
};

/** Description:
    Defines the interface through which nodes in the DWGdirect vectorization
    pipeline transfer geometric data.

    {group:OdGi_Classes} 
*/
class OdGiConveyorGeometry
{
public:
  /** Description:
      This function is called to process the data contained in an OdDbPolyline, 
      or lightweight polyline.

      Arguments:
        lwBuf (I) Polyline data to be processed.
        pXform (I) Optional transformation that should be applied to this polyline.
        fromIndex (I) Index of the first segment in this polyline that should be processed.
        numSegs (I) Number of segments, starting with fromIndex, that should be processed (0 indicates that all segments should be processed).
  */
  virtual void plineProc(const OdGiPolyline& lwBuf,
    const OdGeMatrix3d* pXform = 0,
    OdUInt32 fromIndex = 0,
    OdUInt32 numSegs = 0) = 0;

  /** Description:
      This function is called to process the data contained in a simple polyline.

      Arguments:
        nbPoints (I) Number of points in this polyline.
        pVertexList (I) Pointer to an array of vertices that make up the polyline.
        pNormal (I) Normal vector for this polyline.
        pExtrusion (I) Extrusion vector for this polyline (specifies the direction and 
          distance of the extrusion).
        lBaseSubEntMarker (I) Currently unused.
  */
  virtual void polylineProc(
    OdInt32 nbPoints, const OdGePoint3d* pVertexList,
    const OdGeVector3d* pNormal = 0,
    const OdGeVector3d* pExtrusion = 0,
    OdInt32 lBaseSubEntMarker = -1) = 0;

  /** Description:
      This function is called to process the data for a polygon.

      Arguments:
        nbPoints (I) Number of points in this polygon.
        pVertexList (I) Pointer to an array of vertices that make up the polygon.
        pNormal (I) Normal vector for this polygon.
        pExtrusion (I) Extrusion vector for this polyline (specifies the direction and 
          distance of the extrusion).
  */
  virtual void polygonProc(
    OdInt32 nbPoints, const OdGePoint3d* pVertexList,
    const OdGeVector3d* pNormal = 0,
    const OdGeVector3d* pExtrusion = 0) = 0;

  /** Description:
      This function is called to process the data for an XLINE.
      An XLINE is a line unbounded in both directions, that passes through the 
      two supplied points.
  */
  virtual void xlineProc(
    const OdGePoint3d&, const OdGePoint3d&) = 0;

  /** Description:
      This function is called to process the data for a RAY.
      A RAY is a line that starts at the supplied point, and extends unbounded 
      in the direction of the supplied direction vector.
  */
  virtual void rayProc(
    const OdGePoint3d&, const OdGePoint3d&) = 0;

  /** Description:
      This function is called to process the data for a mesh.

      Arguments:
        rows (I) Number of rows in the mesh.
        columns (I) Number of columns in the mesh.
        pVertexList (I) Pointer to an array of vertices that make up the mesh.
        pEdgeData (I) Pointer to additional edge data (color, layer, etc.).
        pFaceData (I) Pointer to additional face data (color, layer, etc.).
        pVertexData (I) Additional vertex attributes (normals, colors, etc.). 

      See Also:
        OdGiEdgeData
        OdGiFaceData
        OdGiVertexData
        Vectorization Primitives
  */
  virtual void meshProc(
    OdInt32 rows, OdInt32 columns,
    const OdGePoint3d* pVertexList,
    const OdGiEdgeData* pEdgeData = 0,
    const OdGiFaceData* pFaceData = 0,
    const OdGiVertexData* pVertexData = 0) = 0;

  /** Description:
      This function is called to process the data for a shell.

      Arguments:
        nbVertex (I) Number of vertices in the shell.
        pVertexList (I) Pointer to an array of vertices that make up the shell.
        faceListSize (I) Number of entries in pFaceList.
        pFaceList (I) List of numbers that define the faces in the shell.  See
          Vectorization Primitives for more details.
        pEdgeData (I) Pointer to additional edge data (color, layer, etc.).
        pFaceData (I) Pointer to additional face data (color, layer, etc.).
        pVertexData (I) Additional vertex attributes (normals, colors, etc.). 

      See Also:
        OdGiEdgeData
        OdGiFaceData
        OdGiVertexData
        Vectorization Primitives      
  */
  virtual void shellProc(
    OdInt32 nbVertex, const OdGePoint3d* pVertexList,
    OdInt32 faceListSize, const OdInt32* pFaceList,
    const OdGiEdgeData* pEdgeData = 0,
    const OdGiFaceData* pFaceData = 0,
    const OdGiVertexData* pVertexData = 0) = 0;

  /** Description:
      This function is called to process the data for a circle. 

      Arguments:
        center (I) Center point of the circle.
        radius (I) Radius of the circle.
        normal (I) Normal vector for this entity.
        pExtrusion (I) Extrusion vector for this entity (specifies the direction and 
          distance of the extrusion).
  */
  virtual void circleProc(
    const OdGePoint3d& center, 
    double radius, 
    const OdGeVector3d& normal, 
    const OdGeVector3d* pExtrusion = 0) = 0;

  /** Arguments:
        start (I) Start point on circle.
        point (I) Point on circle.
        end (I) End point on circle.

      Remarks:
        This circle is defined by 3 points, which may not be colinear or coincident.
  */
  virtual void circleProc(
    const OdGePoint3d& start, 
    const OdGePoint3d& point, 
    const OdGePoint3d& end, 
    const OdGeVector3d* pExtrusion = 0) = 0;
  
  /** Description:
      This function is called to process the data for a circular arc.

      Arguments:
        center (I) Center point of the arc.
        radius (I) Radius of the arc.
        normal (I) Normal vector for this entity.
        startVector (I) Defines the start of this arc.
        sweepAngle (I) Angle (in radians) that defines the arc.
        arcType (I) Type of arc, either kOdGiArcSimple, kOdGiArcSector or kOdGiArcChord.
        pExtrusion (I) Extrusion vector for this entity (specifies the direction and 
          distance of the extrusion).
  */
  virtual void circularArcProc(
    const OdGePoint3d& center,
    double radius,
    const OdGeVector3d& normal,
    const OdGeVector3d& startVector,
    double sweepAngle,
    OdGiArcType arcType = kOdGiArcSimple, const OdGeVector3d* pExtrusion = 0) = 0;
  
  /** Arguments:
        start (I) Start point of circular arc.
        point (I) Point on circular arc.
        end (I) End point on circular arc.

      Remarks:
        This circular arc is defined by 3 points, which may not be colinear or coincident.
  */
  virtual void circularArcProc(
    const OdGePoint3d& start,
    const OdGePoint3d& point,
    const OdGePoint3d& end,
    OdGiArcType arcType = kOdGiArcSimple, const OdGeVector3d* pExtrusion = 0) = 0;

  /** Description:
      This function is called to process the data for an elliptical arc.

      Arguments:
        ellipArc (I) Elliptical arc parameters.
        pEndPointsOverrides (I) Override values for the first and last points of the arc.
        arcType (I) Type of arc, either kOdGiArcSimple, kOdGiArcSector or kOdGiArcChord.
        pExtrusion (I) Extrusion vector for this entity (specifies the direction and 
          distance of the extrusion).
  */
  virtual void ellipArcProc(
    const OdGeEllipArc3d& arc,
    const OdGePoint3d* pEndPointsOverrides = 0,
    OdGiArcType arcType = kOdGiArcSimple, const OdGeVector3d* pExtrusion = 0) = 0;

  /** Description:
      This function is called to process the data for a NURBS curve.

      Arguments:
        nurbs (I) NURBS curve to tessellate.
  */
  virtual void nurbsProc(
    const OdGeNurbCurve3d& nurbs) = 0;

  /** Description:
      This function is called to process text data.

      Arguments:
        position (I) Position of the text string.
        u (I) Defines the baseline direction for the text string.
        v (I) The up vector for the text string.
        msg (I) The text string.
        nLength (I) Number of bytes in msg (not including the optional null byte).
        raw (I) If true, then treat the string as raw text, in which case special characters
          such as %%D (degree symbol), etc. are not converted to the appropriate symbol, 
          but are instead treated as literal strings.
        pTextStyle (I) Text style properties for this text string.
        pExtrusion (I) Extrusion vector for this entity (specifies the direction and 
          distance of the extrusion).
  */
  virtual void textProc(
    const OdGePoint3d& position,
    const OdGeVector3d& u, 
    const OdGeVector3d& v,
    const OdChar* msg, 
    OdInt32 length, 
    bool raw, 
    const OdGiTextStyle* pTextStyle,
    const OdGeVector3d* pExtrusion = 0) = 0;
  
  /** Description:
      This function is called to process the data for a shape.

      Arguments:
        position (I) Position of the text string.
        u (I) Defines the baseline direction for the text string.
        v (I) The up vector for the text string.
        shapeNo (I) Index of the shape.
        pStyle (I) Text style properties for this shape.
        pExtrusion (I) Extrusion vector for this entity (specifies the direction and 
          distance of the extrusion).
  */
  virtual void shapeProc(
    const OdGePoint3d& position,
    const OdGeVector3d& u, 
    const OdGeVector3d& v,
    int shapeNo, 
    const OdGiTextStyle* pStyle,
    const OdGeVector3d* pExtrusion = 0) = 0;

  /** Description:
      This function is called to process the data for a raster image.

      Arguments:
        origin (I) Image origin point.
        u (I) Image width vector.
        v (I) Image height vector.
        pImg (I) Image object.
        uvBoundary (I) Image boundary points (may not be null).
        numBoundPts (I) Number of boundary points.
        transparency (I) Image transparency (true if transparency is on, false otherwise).
        brightness (I) Image brightness (0-100, default is 50).
        contrast (I) Image contrast (0-100, default is 50).
        fade (I) Image fade value (0-100, default is 0).
  */
  virtual void rasterImageProc(
    const OdGePoint3d& origin,
    const OdGeVector3d& u,
    const OdGeVector3d& v,
    const OdGiRasterImage* pImg, 
    const OdGePoint2d* uvBoundary, 
    OdUInt32 numBoundPts,
    bool transparency = false,
    double brightness = 50.0,
    double contrast = 50.0,
    double fade = 0.0) = 0;

  /** Description:
      This function is called to process the data for a metafile.

      Arguments:
        origin (I) Metafile origin.
        u (I) Metafile width vector.
        v (I) Metafile height vector.
        pMetafile (I) Metafile object.
        bDcAligned (I) reserved
        bAllowClipping (I) reserved
  */
  virtual void metafileProc(
    const OdGePoint3d& origin,
    const OdGeVector3d& u,
    const OdGeVector3d& v,
    const OdGiMetafile* pMetafile,
    bool bDcAligned = true,           
    bool bAllowClipping = false) = 0; 
};

#endif //#ifndef __ODGICONVEYORGEOMETRY_H__

