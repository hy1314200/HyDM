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



#ifndef __ODGIGEOMETRY_H__
#define __ODGIGEOMETRY_H__

#include "DD_PackPush.h"
#include "RxObject.h"

#include "Ge/GePoint3d.h"

class OdDbStub;
class OdCmEntityColor;
class OdGeMatrix3d;
class OdGeVector3d;
//class OdGePoint3d;
class OdGePoint2d;

/** Description:
    This class contains arrays of edge attributes for meshes and shells.
    
    Remarks:
    Objects of this class are intended to be passed as arguments
    to the mesh and shell methods of OdGiWorldGeometry and OdGiViewportGeometry. 
    Note: setting an 'improper' size of any array will cause
    unpredictable or fatal results.

    Library:
    Gi

    {group:OdGi_Classes} 
*/
class OdGiEdgeData
{
  const OdUInt16*         m_pColors;
  const OdCmEntityColor*  m_pTrueColors;
        OdDbStub**        m_pLayerIds;
        OdDbStub**        m_pLinetypeIds;
  const OdInt32*          m_pSelectionMarkers;
  const OdUInt8*          m_pVisibilities;
public:
  OdGiEdgeData()
    : m_pColors(NULL)
    , m_pTrueColors(NULL)
    , m_pLayerIds(NULL)
    , m_pLinetypeIds(NULL)
    , m_pSelectionMarkers(NULL)
    , m_pVisibilities(NULL)
  {}

  /** Description:
    Sets the edge *colors* to be used by this object.

    Arguments:
    colors (I) Array of color indices.
        
    Note:
    You cannot call both setColors() and setTrueColors().
    
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. An incorrect number of elements will have unpredictable or fatal consequences.
  */
  void setColors(
    const OdUInt16 *colors)                    { m_pColors = colors; }

  /** Description:
    Sets the edge *colors* to be used by this object.

    Arguments:
    colors (I) Array of OdCmEntityColor objects.
        
    Note:
    You cannot call both setColors() and setTrueColors().

    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. An incorrect number of elements will have unpredictable or fatal consequences.
  */
  void setTrueColors(
    const OdCmEntityColor *colors)         { m_pTrueColors = colors; }

  /** Description:
    Sets the edge LayerTable records to be used by this object.

    Arguments:
    layerIds (I) Array of LayerTableRecord object IDs.
        
    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. An incorrect number of elements will have unpredictable or fatal consequences.
  */
  void setLayers(
    OdDbStub* *layerIds)                       { m_pLayerIds = layerIds; }

  /** Description:
    Sets the edge LinetypeTable records to be used by this object.

    Arguments:
    linetypeIds (I) Array of LinetypeTableRecord object IDs.
        
    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. An incorrect number of elements will have unpredictable or fatal consequences.
  */
  void setLinetypes(
    OdDbStub* *linetypeIds)                 { m_pLinetypeIds = linetypeIds; }

  /** Description:
    Sets the edge graphics system selection markers to be used by this object.

    Arguments:
    selectionMarkers (I) Array of graphics system selection markers.
        
    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. An incorrect number of elements will have unpredictable or fatal consequences.
  */
  void setSelectionMarkers(
    const OdInt32* selectionMarkers) { m_pSelectionMarkers = selectionMarkers; }

  /** Description:
    Sets the edge *visibilities* to be used by this object.

    Arguments:
    visibilities (I) Array of *visibility* values.

    Remarks:
    Each *visibility* will be one of the following:
     
    @table
    Name              Value   Description
    kAcGiInvisible    0       Invisible 
    kAcGiVisible      1       Visible 
    kAcGiSilhouette   2       Silhouette edge 
        
    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. An incorrect number of elements will have unpredictable or fatal consequences.
  */
  void setVisibility(
    const OdUInt8* visibilities)           { m_pVisibilities = visibilities; }
  
  /** Description:
    Returns a pointer to the array of edge *colors* used by this object.

    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. Writing beyond the array bounds will have unpredictable or fatal consequences.
  */
  const OdUInt16* colors() const           { return m_pColors; }

  /** Description:
    Returns a pointer to the array of edge *colors* used by this object.

    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. Writing beyond the array bounds will have unpredictable or fatal consequences.
  */
  const OdCmEntityColor* trueColors() const       { return m_pTrueColors; }

  /** Description:
    Returns a pointer to the array of edge LayerTableRecord object IDs used by this object.

    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. Writing beyond the array bounds will have unpredictable or fatal consequences.
  */
  OdDbStub** layerIds() const         { return m_pLayerIds; }

  /** Description:
    Returns a pointer to the array of edge LinetypeTableRecord object IDs used by this object.

    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. Writing beyond the array bounds will have unpredictable or fatal consequences.
  */
  OdDbStub** linetypeIds() const      { return m_pLinetypeIds; }

  /** Description:
    Returns a pointer to the array of edge graphics system selection markers used by this object.

    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. Writing beyond the array bounds will have unpredictable or fatal consequences.
  */
  const OdInt32* selectionMarkers() const { return m_pSelectionMarkers; }

  /** Description:
    Returns a pointer to the array of edge *visibilities* used by this object.

    Remarks:
    Each *visibility* will be one of the following:
     
    @table
    Name              Value   Description
    kAcGiInvisible    0       Invisible 
    kAcGiVisible      1       Visible 
    kAcGiSilhouette   2       Silhouette edge 

    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. Writing beyond the array bounds will have unpredictable or fatal consequences.
  */
  const OdUInt8* visibility() const       { return m_pVisibilities; }
};


/** Description:
    This class contains arrays of face attributes for meshes and shells.

    Library:
    Gi

    {group:OdGi_Classes} 
*/
class OdGiFaceData
{
  const OdUInt16*         m_pColors;
  const OdCmEntityColor*  m_pTrueColors;
        OdDbStub**        m_pLayerIds;
  const OdInt32*          m_pSelectionMarkers;
  const OdUInt8*          m_pVisibilities;
  const OdGeVector3d*     m_pNormals;
public:
  OdGiFaceData()
    : m_pColors(NULL)
    , m_pTrueColors(NULL)
    , m_pLayerIds(NULL)
    , m_pSelectionMarkers(NULL)
    , m_pNormals(NULL)
    , m_pVisibilities(NULL)
  {}

  /** Description:
    Sets the face *colors* to be used by this object.

    Arguments:
    colors (I) Array of color indices.
        
    Note:
    You cannot call both setColors() and setTrueColors().

    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. An incorrect number of elements will have unpredictable or fatal consequences.
  */
  void setColors(
    const OdUInt16 *colors)                    { m_pColors = colors; }

  /** Description:
    Sets the face *colors* to be used by this object.

    Arguments:
    colors (I) Array of OdCmEntityColor objects.
        
    Note:
    You cannot call both setColors() and setTrueColors().

    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. An incorrect number of elements will have unpredictable or fatal consequences.
  */
  void setTrueColors(
    const OdCmEntityColor *colors)         { m_pTrueColors = colors; }

  /** Description:
    Sets the face LayerTable records to be used by this object.

    Arguments:
    layerIds (I) Array of LayerTableRecord object IDs.
        
    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. An incorrect number of elements will have unpredictable or fatal consequences.
  */
  void setLayers(
    OdDbStub* *layerIds)                       { m_pLayerIds = layerIds; }

  /** Description:
    Sets the face graphics system selection markers to be used by this object.

    Arguments:
    selectionMarkers (I) Array of graphics system selection markers.
        
    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. An incorrect number of elements will have unpredictable or fatal consequences.
  */
  void setSelectionMarkers(
    const OdInt32* selectionMarkers) { m_pSelectionMarkers = selectionMarkers; }

  /** Description:
    Sets the face normal vectors to be used by this object.

    Arguments:
    normals (I) Array of normal vectors.
        
    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. An incorrect number of elements will have unpredictable or fatal consequences.
  */
  void setNormals(
    const OdGeVector3d* normals)              { m_pNormals = normals; }

  /** Description:
    Sets the face *visibilities* to be used by this object.

    Arguments:
    visibilities (I) Array of *visibility* values.

    Remarks:
    Each *visibility* will be one of the following:
     
    @table
    Name              Value   Description
    kAcGiInvisible    0       Invisible 
    kAcGiVisible      1       Visible 
        
    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. An incorrect number of elements will have unpredictable or fatal consequences.
  */
  void setVisibility(
    const OdUInt8* visibilities)           { m_pVisibilities = visibilities; }
  
  /** Description:
    Returns a pointer to the array of face *colors* used by this object.

    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. Writing beyond the array bounds will have unpredictable or fatal consequences.
  */
  const OdUInt16* colors() const           { return m_pColors; }

  /** Description:
    Returns a pointer to the array of face *colors* used by this object.

    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. Writing beyond the array bounds will have unpredictable or fatal consequences.
  */
  const OdCmEntityColor* trueColors() const       { return m_pTrueColors; }

  /** Description:
    Returns a pointer to the array of face LayerTableRecord object IDs used by this object.

    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. Writing beyond the array bounds will have unpredictable or fatal consequences.
  */
  OdDbStub** layerIds() const         { return m_pLayerIds; }

  /** Description:
    Returns a pointer to the array of face graphics system selection markers used by this object.

    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. Writing beyond the array bounds will have unpredictable or fatal consequences.
  */
  const OdInt32* selectionMarkers() const { return m_pSelectionMarkers; }

  /** Description:
    Returns a pointer to the array of face normal vectors used by this object.

    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. Writing beyond the array bounds will have unpredictable or fatal consequences.
  */
  const OdGeVector3d* normals() const          { return m_pNormals; }

  /** Description:
    Returns a pointer to the array of edge *visibilities* used by this object.

    Remarks:
    Each *visibility* will be one of the following:
     
    @table
    Name              Value   Description
    kAcGiInvisible    0       Invisible 
    kAcGiVisible      1       Visible 

    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. Writing beyond the array bounds will have unpredictable or fatal consequences.
  */
  const OdUInt8* visibility() const       { return m_pVisibilities; }
};

typedef enum
{
  kOdGiCounterClockwise = -1,
  kOdGiNoOrientation    = 0,
  kOdGiClockwise        = 1
} OdGiOrientationType;


/** Description:
    This class contains arrays of vertex attributes for meshes and shells.
    
    Remarks:
    Objects of this class are intended to be passed as arguments
    to the mesh and shell methods of OdGiWorldGeometry and OdGiViewportGeometry. 
    Note: setting an 'improper' size of any array will cause
    unpredictable or fatal results.

    Library:
    Gi

    {group:OdGi_Classes} 
*/
class OdGiVertexData
{
  const OdGeVector3d* m_pNormals;
  OdGiOrientationType m_orientationFlag;
  const OdCmEntityColor*  m_pTrueColors;
public:
  OdGiVertexData()
    : m_pNormals(NULL)
    , m_orientationFlag(kOdGiNoOrientation)
    , m_pTrueColors(NULL)
  {}

  /** Description:
    Sets the vertex normal vectors to be used by this object.

    Arguments:
    normals (I) Array of normal vectors.
        
    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. An incorrect number of elements will have unpredictable or fatal consequences.
  */
  void setNormals(
    const OdGeVector3d* normals)              { m_pNormals = normals; }

  /** Description:
    Sets the orientation type to be used by this object.
    
    Arguments:
    orientationFlag (I) Orientation type.
    
    Remarks:
    The orientation type defines the positive direction of the normal at the vertices.
    
    orientationFlag will be one of the following:
    
    @table
    Name                      Value 
    kOdGiCounterClockwise     -1
    kOdGiNoOrientation        0
    kOdGiClockwise            1

  */
  void setOrientationFlag(
    const OdGiOrientationType orientationType) { m_orientationFlag = orientationType; }

  /** Description:
    Sets the vertex *colors* to be used by this object.

    Arguments:
    colors (I) Array of OdCmEntityColor objects.
        
    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. An incorrect number of elements will have unpredictable or fatal consequences.
  */
  void setTrueColors(
    const OdCmEntityColor *colors)         { m_pTrueColors = colors; }

  /** Description:
    Returns a pointer to the array of vertex normal vectors used by this object.

    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. Writing beyond the array bounds will have unpredictable or fatal consequences.
  */
  const OdGeVector3d* normals() const         { return m_pNormals; }

  /** Description:
    Returns the orientation type used by this object.
    
    Remarks:
    The orientation type defines the positive direction of the normal at the vertices.
    
    orientationFlag will be one of the following:
    
    @table
    Name                      Value 
    kOdGiCounterClockwise     -1
    kOdGiNoOrientation        0
    kOdGiClockwise            1

  */
  OdGiOrientationType orientationFlag() const { return m_orientationFlag; }

  /** Description:
    Returns a pointer to the array of vertex *colors* used by this object.

    Note:
    This function does not make a copy of the array, which should have the same number of elements as the 
    mesh or shell with which it is used. Writing beyond the array bounds will have unpredictable or fatal consequences.
  */
  const OdCmEntityColor* trueColors() const   { return m_pTrueColors; }
};

class OdGiDrawable;
struct OdGiClipBoundary;

class OdDbPolyline;
class OdPolyPolygon3d;
class OdGeNurbCurve3d;
class OdGiTextStyle;
class OdDbBody;
class OdGeEllipArc3d;
class OdGiPolyline;


/** Description:
    Arc types
*/
typedef enum
{
  kOdGiArcSimple = 0,   // Unfilled.
  kOdGiArcSector = 1,   // Filled area bounded by the arc and its center.
  kOdGiArcChord  = 2    // Filled area bounded by the arc and its end points
} OdGiArcType;


/** Description:
    This class defines functions that allow entities to vectorize themselves.

    Library:
    Gi

    {group:OdGi_Classes} 
*/
class FIRSTDLL_EXPORT OdGiGeometry : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdGiGeometry);

  /** Description:
    Returns the model-to-world coordinate transform for the entity being vectorized.
    
    Remarks:
    This is the inverse of the matrix returned by getWorldToModelTransform(). 

    If an entity is in one or more blocks, this matrix can be used to
    determine the WCS coordinates of the entity.  
  */
  virtual OdGeMatrix3d getModelToWorldTransform() const = 0;

  /** Description:
    Returns the world-to-model coordinate transform for the entity being vectorized.
    
    Remarks:
    This is the inverse of the matrix returned by getModelToWorldTransform(). 
  */
  virtual OdGeMatrix3d getWorldToModelTransform() const = 0;
  
  /** Description:
    Pushes a model transformation onto the current transformation stack.  
    
    Remarks:
    When a vector is specified, the transformation is created by using the arbitrary axis algorithm 
    on the vector. 
 
    The specified transformation
    is concatenated to the current model transformation (which is initially the identity
    transform).  The resulting model transformation subsequently applied to all geometry 
    introduced into this vectorization context, until popModelTransform() is called.
    
    Arguments:
    normal (I) Normal vector.
    xfm (I) Transformation matrix.
    
  */
  virtual void pushModelTransform(
    const OdGeVector3d& normal) = 0;

  virtual void pushModelTransform(
    const OdGeMatrix3d& xfm) = 0;

  /** Description:
    Pops the top transformation off the current transformation stack.
    
    See also:
    pushModelTransform
    
  */
  virtual void popModelTransform() = 0;
  
  /** Description:
    Introduces a *circle* into this vectorization context.  
    
    Remarks:
    The *circle* is not filled, and takes on the current drawing color.
    
    If startPoint, secondPoint, and endPoint are specified, they
    cannot be colinear and no two can be coincident.

    Arguments:
    center (I) Center point.
    radius (I) Radius.
    normal (I) Normal.
  */
  virtual void circle(
    const OdGePoint3d& center, 
    double radius, 
    const OdGeVector3d& normal) = 0;
  
  /** Arguments:
    startPoint (I) Start point.
    secondPoint (I) Second point.
    endPoint (I) End point.
  */
  virtual void circle(
    const OdGePoint3d& startPoint, 
    const OdGePoint3d& secondPoint, 
    const OdGePoint3d& endPoint) = 0;
  
/** Description:
    Introduces a circular arc into this vectorization context.  
    
    Arguments:
    center (I) Center point.
    radius (I) Radius.
    normal (I) Normal vector.
    startVector (I) Defines the start of this arc.
    sweepAngle (I) Angle that defines the arc.
    arcType (I) Arc type.
    
    Remarks:
    The arc takes on the current drawing color.      

    If startPoint, secondPoint, and endPoint are specified, they
    cannot be colinear and no two can be coincident.
    
    arcType will be one of the following:
    
    @table
    Name              Value     Description
    kOdGiArcSimple    0         Unfilled.
    kOdGiArcSector    1         Filled area bounded by the arc and its center.
    kOdGiArcChord     2         Filled area bounded by the arc and its end points

    
    Note:
    All angles are expressed in radians.
  */
  virtual void circularArc(
    const OdGePoint3d& center,
    double radius,
    const OdGeVector3d& normal,
    const OdGeVector3d& startVector,
    double sweepAngle,
    OdGiArcType arcType = kOdGiArcSimple) = 0;
  
  /** Arguments:
      startPoint (I) Start point.
      secondPoint (I) Second point.
      endPoint (I) End point.
  */
  virtual void circularArc(
    const OdGePoint3d& startPoint,
    const OdGePoint3d& secondPoint,
    const OdGePoint3d& endPoint,
    OdGiArcType arcType = kOdGiArcSimple) = 0;

  /** Description:
    Introduces a *polyline* into this vectorization context.  
    
    Remarks:
    The *polyline* is unfilled, takes on the current drawing color and thickness. Use polygon() to render filled areas.
    
    The *polyline* is rendered as a series of lines connecting the first point
    in vertexList to the second, the second to the third, etc.
    All points must be coplanar.

    Use polygon() to render closed areas.

    Arguments:
    nVertices (I) Number of vertices.
    vertexList (I) Array of vertices.
    normal (I) Normal vector of the *polyline*.
    lBaseSubEntMarker (I) Not used.
  */
  virtual void polyline(
    OdInt32 nVertices,
    const OdGePoint3d* vertexList,
    const OdGeVector3d* normal = NULL,
    OdInt32 lBaseSubEntMarker = -1) = 0;
  
  /** Description:
    Introduces a *polygon* into this vectorization context.  
    
    Remarks:
    The *polygon* is filled, takes on the current drawing color. Use polyline() to render unfilled areas.

    
    The *polygon* is rendered as a series of lines connecting the first point
    in vertexList to the second, the second to the third, etc.
    All points must be coplanar.


    Arguments:
    nVertices (I) Number of vertices.
    vertexList (I) Array of vertices.
  */
  virtual void polygon(
    OdInt32 nVertices, 
    const OdGePoint3d* vertexList) = 0;

  /** Description:
    Introduces a lightweight *polyline* into this vectorization context.
    
    Remarks:
    The *polyline* may contain varying segment widths, straight segments
    and arc segments.    

    The *polyline* takes on the current drawing color.
    
    All points must be coplanar.

    Arguments:
    lwPolyline (I) Polyline.
    fromIndex (I) Index of the first segment to be processed.
    numSegs (I) Number of segments to be processed (0 indicates all segments).
  */
  virtual void pline(
    const OdGiPolyline& lwPolyline, 
    OdUInt32 fromIndex = 0, 
    OdUInt32 numSegs = 0) = 0;

  /** Description:
    Introduces a *mesh* into this vectorization context.  
    
    Remarks:
    A *mesh* is a surface defined by a grid of vertices, and corresponds to a Polygon Mesh.
    
    By default, a *mesh* takes on the current drawing color.  Color, linetype, and
    and other properties can be controlled supplying the appropriate
    data for the edgeData, faceData, and vertexData arguments.

    Arguments:
    rows (I) Number of *rows*.
    columns (I) Number of *columns*.
    vertexList (I) Array of vertices.
    edgeData (I) Array of additional edge data.
    faceData (I) Array of additional face data.
    vertexData (I) Array of additional vertex data. 
  */
  virtual void mesh(
    OdInt32 rows,
    OdInt32 columns,
    const OdGePoint3d* vertexList,
    const OdGiEdgeData* edgeData = NULL,
    const OdGiFaceData* faceData = NULL,
    const OdGiVertexData* vertexData = NULL) = 0;
  
  /** Description:
    Introduces a *shell* into this vectorization context.  
    
    Remarks:
    A shell is a set of faces that can contain holes, and corresponds to a Polyface *mesh*.

    By default, a *mesh* takes on the current drawing color.  Color, linetype, and
    and other properties can be controlled supplying the appropriate
    data for the edgeData, faceData, and vertexData arguments.
    
    See Also:
    Faces
      
    Arguments:
    nVertices (I) Number of vertices.
    vertexList (I) Array of vertices.
    faceListSize (I) Number of entries in facesList.
    faceList (I) Array of integers defining faces.
    edgeData (I) Array of additional edge data.
    faceData (I) Array of additional face data.
    vertexData (I) Array of additional vertex data. 

  */
  virtual void shell(
    OdInt32 nVertices,
    const OdGePoint3d* vertexList,
    OdInt32 faceListSize,
    const OdInt32* faceList,
    const OdGiEdgeData* edgeData = NULL,
    const OdGiFaceData* faceData = NULL,
    const OdGiVertexData* vertexData = NULL) = 0;
  
  /** Description:
    Introduces *text* into this vectorization context.
    
    Arguments:
    position (I) Position of the text string.
    normal (I) Normal vector of the *text*.
    direction (I) Baseline direction of the *text*.
    height (I) Height of the text.
    width (I) Width of text.
    oblique (I) Oblique angle.
    msg (I) Text string.

    Remarks:
    The *text* takes on the current drawing color.
 
    If length is not specified, msg must be null terminated.
    
    Note:
    All angles are expressed in radians.

    As currently implemented, this function ignores width and oblique.
    They will be fully implemented in a future *release*.
  */
  virtual void text(
    const OdGePoint3d& position,
    const OdGeVector3d& normal, 
    const OdGeVector3d& direction,
    double height, 
    double width, 
    double oblique, 
    const OdChar* msg) = 0;
  
  /** Arguments:
    length (I) Number of bytes in msg (not including the optional null byte).
    raw (I) If and only if true, escape sequences, such as %%P, will not be converted to special characters.
    pTextStyle (I) Pointer to the TextStyle for msg.
  */
  virtual void text(
    const OdGePoint3d& position,
    const OdGeVector3d& normal, 
    const OdGeVector3d& direction,
    const OdChar* msg, 
    OdInt32 length, 
    bool raw, 
    const OdGiTextStyle* pTextStyle) = 0;
  
  /** Description:
    Introduces an Xline into this vectorization context.  
      
    Remarks:
    Xlines are infinite lines passing through two points.
 
    The *xline* takes on the current drawing color.
    
    Arguments:
    firstPoint (I) First point.
    secondPoint (I) Second point.
  */
  virtual void xline(
    const OdGePoint3d& firstPoint, 
    const OdGePoint3d& secondPoint) = 0;

  /** Description:
    Introduces a Ray into this vectorization context.  
    
    Remarks:
    A Ray is a semi-infinite line that starts one point and passes through a second point.

    The *ray* takes on the current drawing color.
 
    Arguments:
    firstPoint (I) First point.
    secondPoint (I) Second point.
  */
  virtual void ray(
    const OdGePoint3d& firstPoint, 
    const OdGePoint3d& secondPoint) = 0;
  
  /** Description:
    Introduces a NURBS curve into this vectorization context.
      
    Remarks:
    The curve takes on the current drawing color.
    
    Arguments:
    nurbsCurve (I) NURBS curve data.
  */
  virtual void nurbs(
    const OdGeNurbCurve3d& nurbsCurve) = 0;

  /** Description:
    Introduces an elliptical arc into this vectorization context.
        
    Arguments:
    ellipArc (I) Elliptical arc data.
    endPointsOverrides (I) Array of points to be used as the the first and last points of the vectorized arc.
    arcType (I) Arc type.
        
    Remarks:
    arcType will be one of the following:
    
    @table
    Name              Value     Description
    kOdGiArcSimple    0         Unfilled.
    kOdGiArcSector    1         Filled area bounded by the arc and its center.
    kOdGiArcChord     3         Filled area bounded by the arc and its end points
  */
  virtual void ellipArc(
    const OdGeEllipArc3d& ellipArc,
    const OdGePoint3d* endPointsOverrides = 0,
    OdGiArcType arcType = kOdGiArcSimple) = 0;

  /** Description:
    Introduces the specified object into this vectorization context.
    
    Arguments:
    pDrawable (I) Pointer to a drawable object.

    Remarks:
    Implementations of this method are expected to do the following:

    o  Call OdGiDrawable::setAttributes to set attribute information for the object.
    o  Call worldDraw on the drawable object  to vectorize it into this context.
    o  If worldDraw returns false, call viewportDraw each viewport.
  */
  virtual void draw(
    const OdGiDrawable* pDrawable) = 0;

  /** Description:
    Pushes a clip boundary onto the current clip stack.
    
    Remarks:
    Subsequent objects are clipped until popClipBoundary() is called.
    
    Arguments:
    pBoundary (I) Pointer to the boundary.
  */
  virtual void pushClipBoundary(
    OdGiClipBoundary* pBoundary) = 0;

  /** Description:
    Pops the topmost clip boundary from the clip stack.
    
    See also:
    pushClipBoundary
  */
  virtual void popClipBoundary() = 0;

  /** Description:
    Introduces a line into this vectorization context.  
    Arguments:
    pnts (I) Array of WCS start and end points.
    Remarks:
    The current model transformation is not applied to the line.
  */
  virtual void worldLine(
    const OdGePoint3d pnts[2]) = 0;
};

#include "DD_PackPop.h"

#endif


