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
// programs incorporating this software must include the following statement
// with their copyright notices:
//
//      DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved.
//
// By use of this software, you acknowledge and accept the terms of this
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef OD_DBPL_H
#define OD_DBPL_H

#include "DD_PackPush.h"

#include "DbCurve.h"
#include "Db2dPolyline.h"

class OdDb2dPolyline;
class OdGeLineSeg2d;
class OdGeLineSeg3d;
class OdGeCircArc2d;
class OdGeCircArc3d;

/** Description:
    This class represents Lightweight Polyline entities in an OdDbDatabase instance.

    Library:
    Db

    OdDbPolyline entities differ from OdDb2dPolyline entities as follows:
    
    1. OdDbPolyline entities are stored as single objects, thereby improving
       performance and reducing overhead compared to OdDb2D Polyline objects
       
    2. Curve fitting and Spline fitting are not supported.  
    
    Note:
    The number of vertices in an OdDbPolyline must be at least two. 
    Polylines with less than two vertices should not left in or added to
    the *database*.
    
    Since the nth Segment of a Polyline is the segment following the nth Vertex, 
    the segment index and vertex index may be used interchangeably. 
    
    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDbPolyline : public OdDbCurve
{
public:
  ODDB_DECLARE_MEMBERS(OdDbPolyline);

  OdDbPolyline();

  /** Description:
    Fills this OdDbPolyline entity with data from the specified OdDb2dPolyline entity.  
    
    Arguments:
    pSource (I) Pointer to the source OdDb2dPolyline entity.
    transferId (I) True to do a handOverTo() between the source OdDb2dPolyline 
                   entity and this OdDbPolyline entity.  

    Remarks:
    If and only if transferId is true, 
    
      o  This OdDbPolyline entity will be made *database* resident.
      o  It will assume the objectId, handle, extended entity data, extension
         dictionary, and reactors of the source OdDb2dPolyline entity
      o  The source OdDb2dPolyline entity will be deleted.
    
        
    Note:
    The source OdDb2dPolyline entity
    
      o  Must be non- *database* resident.
      o  Must by of type k2dSimplePoly or k2dFitCurvePoly.
   
    Remarks:
    eOk will be one of the following:
    
    @table
    Name                   Description
    eOk                    Success
    eAlreadyInDb           This entity is *database* resident
    eIllegalEntityType     *pSource is not an OdDb2dPolyline
    eNotApplicable         *pSource is not k2dSimplePoly or there is extended
                           entity data attached to a vertex.
  */
  OdResult convertFrom(
    OdDbEntity* pSource, 
    bool transferId = true);

  /** Description:
    Fills the specified OdDb2dPolyline with data from this OdDbPolyline entity.

    Arguments:
    pDest (I) Pointer to the destination OdDb2dPolyline entity.
    transferId (I) True to do a handOverTo() between this OdDbPolyline entity 
    and the destination OdDb2dPolyline entity.  

    Remarks:
    If and only if transferId is true,
    
      o  The destination OdDb2dPolyline entity will be made *database* resident.
      o  It will assume the objectId, handle, extended entity data, extension
         dictionary, and reactors of this OdDbPolyline entity. 
      o  This OdDbPolyline entity will made non- *database* resident, and may be deleted with the C++ delete operator.

    Remarks:
    eOk will be one of the following:
    
    @table
    Name                   Description
    eOk                    Success
    eIllegalReplacement    This entity is non- *database* resident.
    eObjectToBeDeleted     This entity is now non- *database* resident, and should be deleted.
  */
  OdResult convertTo(
    OdDb2dPolyline* pDest, 
    bool transferId = true);

  /** Description:
    Returns the OCS or WCS *point* of the specified vertex of this Polyline entity (DXF 10).

    Arguments:
    index (I) Vertex *index*.
    point2d (O) Receives the OCS *point*.
    point3d (O) Receives the WCS *point*.
 */
  void getPointAt(
    unsigned int index, 
    OdGePoint2d& point2d) const;
  void getPointAt(
    unsigned int index, 
    OdGePoint3d& point3d) const;


  enum SegType
  {
    kLine,        // Straight segment with length > 0.
    kArc,         // Arc segment with length > 0.
    kCoincident,  // Segment with length == 0.
    kPoint,       // Polyline with 1 vertex.
    kEmpty        // Polyline with 0 vertices.
  };

  /** Description:
    Returns the type of the the specified segment of this Polyline entity.

    Arguments:
    index (I) Segment *index*.
    Remarks:
    segType will return one of the following:
    
    @table
    Name           Description
    kLine          Straight segment with length > 0.0
    kArc           Arc segment with length > 0.0
    kCoincident    Segment with length == 0.0
    kPoint         Polyline with 1 vertex.
    kEmpty         Polyline with 0 vertices.
  */
  SegType segType(
    unsigned int index) const;

  /** Description:
    Returns the specified OCS or WCS line segment of this Polyline entity.
    Arguments:
    index (I) Segment *index*.
    line2d (O) Receives the OCS *line* segment.
    line3d (O) Receives the WCS *line* segment.
  */
  void getLineSegAt(
    unsigned int index, 
    OdGeLineSeg2d& line2d) const;

  void getLineSegAt(
    unsigned int index, 
    OdGeLineSeg3d& line3d) const;

  /** Description:
    Returns the specified OCS or WCS arc segment of this Polyline entity.
    Arguments:
    index (I) Segment *index*.
    arc2d (O) Receives the OCS *arc* segment.
    arc3d (O) Receives the WCS *arc* segment.
  */
  void getArcSegAt(
    unsigned int index, 
    OdGeCircArc2d& arc2d) const;

  void getArcSegAt(
    unsigned int index, 
    OdGeCircArc3d& arc3d) const;

  /** Description:
    Returns true if and only if the specified OCS point is on the specified segment
    of this Polyline entity,
    and returns the parameter of that point on the segment.
    
    Arguments:
    index (I) Segment *index*.
    point2d (I) The OCS point to query.
    param (O) The parameter at that point.
    
    Remarks:
    The returned parameter will be in the parametric form of the segment (linear or arc).
  */
  virtual bool onSegAt(
    unsigned int index, 
    const OdGePoint2d& point2d, 
    double& param) const;

  /** Description:
    Controls the *closed* state for this Polyline entity (DXF 70, bit 0x01=1).
    
    Arguments:
    closed (I) Controls *closed*.
  */
  void setClosed(
    bool closed);

  /** Description:
    Controls the *linetype* generation for this Polyline entity (DXF 70, bit 0x80).
      
    Remarks:
    Linetype generation on indicates that the *linetype* pattern of this Polyline entity
    is continuously generated around all vertices.
    
    Linetype generation off indicates that the *linetype* pattern of this Polyline entity is
    restarted at each vertex.
    
    Arguments:
    plinegen (I) Controls *linetype* generation.
  */
  void setPlinegen(
    bool plinegen);

  /** Description:
    Sets the *elevation* of this entity in the OCS (DXF 38).

    Arguments:
    elevation (I) Elevation.    

    Remarks:
    The *elevation* is the *distance* from the WCS origin to the plane of this entity.
  */
  void setElevation(
    double elevation);

  /** Description:
    Sets the *thickness* of this entity (DXF 39).
    Arguments:
    thickness (I) Thickness.
    Remarks:
    Thickness is the extrusion length along the *normal*.
  */
  void setThickness(
    double thickness);

  /** Description:
    Sets this Polyline entity to a constant width (DXF 43).
    Arguments:
    constantWidth (I) Constant width.
  */
  void setConstantWidth(
    double constantWidth);

  /** Description:
    Sets the WCS *normal* to the plane of this entity (DXF 210).
    Arguments:
    normal (I) Normal.
  */
  void setNormal(
    const OdGeVector3d& normal);

  /** Description:
    Returns true if and only if this Polyline entity consists solely of line segments.
  */
  bool isOnlyLines() const;

  /** Description:
    Returns true if and only if *linetype* generation is on for this Polyline entity (DXF 70, bit 0x80).
      
    Remarks:
    Linetype generation on indicates that the *linetype* pattern of this Polyline entity
    is continuously generated around all vertices.
    
    Linetype generation off indicates that the *linetype* pattern of this Polyline entity is
    restarted at each vertex.
  */
  bool hasPlinegen() const;

  /** Description:
    Returns the *elevation* of this entity in the OCS (DXF 30).
    
    Remarks:
    The *elevation* is the *distance* from the WCS origin to the plane of this entity.
  */
  double elevation() const;

  /** Description:
    Returns the *thickness* of this entity (DXF 39).
    
    Remarks:
    Thickness is the extrusion length along the *normal*.
  */
  double thickness() const;

  /** Description:
    Returns the constant width for this Polyline entity (DXF 43).
  */
  double getConstantWidth() const;

  /** Description:
    Returns the WCS *normal* to the plane of this entity (DXF 210).
  */
  OdGeVector3d normal() const;

  /** Description:
    Inserts a vertex into this Polyline entity at the specified *index*.

    Arguments:
    index (I) Vertex *index*.
    point2d (I) OCS *point* of the vertex.
    bulge (I) Bulge value for the segment following the vertex.
    startWidth (I) Start width for the segment following the vertex.
    endWidth (I) End width for the segment following the vertex.
    
    Remarks:
    The vertex is inserted before the specified vertex.
    If index == numVerts(), the vertex is appended to the Polyline.
    
    Bulge is the *tangent* of 1/4 the included angle of the arc segment, measured counterclockwise.
  */
  void addVertexAt(
    unsigned int index,
    const OdGePoint2d& point2d,
    double bulge = 0.,
    double startWidth = -1.,
    double endWidth = -1.);

  /** Description:
    Removes the specified vertex from this Polyline entity.
    Arguments:
    index (I) Vertex *index*.
  */
  void removeVertexAt(
    unsigned int index);

  /** Description:
     Returns the number of vertices in this Polyline entity (DXF 90).
  */
  unsigned int numVerts() const;


  /** Description:
    Returns the bulge of the specified segment of this Polyline entity.

    Arguments:
    index (I) Segment *index*.
    
    Remarks:
    Bulge is the *tangent* of 1/4 the included angle of the arc segment, measured counterclockwise.
 */
  double getBulgeAt(
    unsigned int index) const;

  /** Description:
    Returns the start and end widths for the specified segment of this Polyline entity.

    Arguments:
    index (I) Segment *index*.
    startWidth (O) Receives the start width for the vertex.
    endWidth (O) Receives the end width for the vertex.
  */
  void getWidthsAt(
    unsigned int index, 
    double& startWidth,  
    double& endWidth) const;

  /** Description:
    Sets the OCS *point* for the specified vertex of this Polyline entity.
    Arguments:
    index (I) Vertex *index*.
    point2d (I) OCS *point* of vertex.
  */
  void setPointAt(
    unsigned int index, 
    const OdGePoint2d& point2d);

  /** Description:
    Sets the *bulge* of the specified segment of this Polyline entity.

    Arguments:
    index (I) Segment *index*.
    bulge (I) Bulge.
    
    Remarks:
    Bulge is the *tangent* of 1/4 the included angle of the arc segment, measured counterclockwise.

  */
  void setBulgeAt(
    unsigned int index, 
    double bulge);

  /** Description:
    Sets the start and end widths for the specified segment of this Polyline entity.

    Arguments:
    index (I) Segment *index*.
    startWidth (I) Start width for the vertex.
    endWidth (I) End width for the vertex.
  */
  void setWidthsAt(
    unsigned int index, 
    double startWidth, 
    double endWidth);

  /** Description:
    Compresses this Polyline entity.
    
    Remarks:
    Takes processing time, and should not be used until all edits are complete.
  */
  void minimizeMemory();

  /** Description:
    Decompresses this Polyline entity to expedite modifications.
  */
  void maximizeMemory();

  /** Description:
    Resets the vertex data for this Polyline entity.

    Arguments:
    reuse (I) True to retain vertices.
    numVerts (I) Number of vertices to retain.
      
    Remarks:
    If reuse is true, the vertex list will be expanded or truncated 
    such that exactly numVerts vertices exist.
    
    If reuse is false, all vertices will be deleted.
  */
  void reset(
    bool reuse, 
    unsigned int numVerts);

  /** Description:
    Returns true if and only if any of the segments in this Polyline entity have non-zero bulges.
  */
  bool hasBulges() const;

  /** Description:
    Returns true if and only if any of the segments in this Polyline entity have start and end widths.
  */
  bool hasWidth() const;

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);

  virtual bool worldDraw(
    OdGiWorldDraw* pWd) const;

  virtual void viewportDraw(
    OdGiViewportDraw* pVd) const;

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual OdResult dxfInFields_R12(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual void getClassID(
    void** pClsid) const;

  virtual bool isPlanar() const;

  virtual OdResult getPlane(
    OdGePlane& plane, 
    OdDb::Planarity& planarity) const;



  /** Description:
      OdDbCurveMethods
  */

  virtual bool isClosed() const;

  virtual bool isPeriodic() const;

  virtual OdResult getStartParam(
    double& startParam) const;

  virtual OdResult getEndParam (
    double& endParam) const;

  virtual OdResult getStartPoint(
    OdGePoint3d& startPoint) const;

  virtual OdResult getEndPoint(
    OdGePoint3d& endPoint) const;

  virtual OdResult getPointAtParam(
    double param, 
    OdGePoint3d& pointOnCurve) const;

  virtual OdResult getParamAtPoint(
    const OdGePoint3d& pointOnCurve, 
    double& param) const;


  virtual OdResult explode(
    OdRxObjectPtrArray& entitySet) const;

};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbPolyline object pointers.
*/
typedef OdSmartPtr<OdDbPolyline> OdDbPolylinePtr;

#include "DD_PackPop.h"

#endif //OD_DBPL_H


