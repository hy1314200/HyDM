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



#ifndef _OD_DB_2DPOLYLINE_
#define _OD_DB_2DPOLYLINE_

#include "DD_PackPush.h"

#include "DbCurve.h"
#include "DbObjectIterator.h"
#include "Db2dVertex.h"

class OdGePoint3d;
class OdDbSequenceEnd;
template <class T> class OdSmartPtr;

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbSequenceEnd object pointers.
*/
typedef OdSmartPtr<OdDbSequenceEnd> OdDbSequenceEndPtr;

/** Description: 
  
    {group:DD_Namespaces}
*/
namespace OdDb
{
  enum Poly2dType
  {
    k2dSimplePoly      = 0, // Simple polyline.
    k2dFitCurvePoly    = 1, // Curve fit (DXF 70, bit 0x04).
    k2dQuadSplinePoly  = 2, // Quadratic B-spline fit (DXF 80, bit 0x08; DXF 75 == 5).
    k2dCubicSplinePoly = 3  // Cubic B-spline-fit (DXF 80, bit 0x08; DXF 75 == 6).
  };
}
/** Description:
    This class represents 2D Polyline entities in an OdDbDatabase instance.

    Library:
    Db

    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDb2dPolyline: public OdDbCurve
{
public:

  ODDB_DECLARE_MEMBERS(OdDb2dPolyline);

  OdDb2dPolyline();

  OdResult setColor(
    const OdCmColor &color, 
    bool doSubents = false);

  OdResult setColorIndex(
    OdUInt16 colorIndex, 
    bool doSubents = false);

  OdResult setTransparency(
    const OdCmTransparency& transparency, 
    bool doSubents = true);
  
  /** Description:
    Returns the type of this Polyline entity. 
    
    Remarks:
    polyType will return one of the following:
    
    @table
    Name                  Value   Description
    k2dSimplePoly         0       Simple polyline.
    k2dFitCurvePoly       1       Curve fit (DXF 70, bit 0x04).
    k2dQuadSplinePoly     2       Quadratic B-spline fit (DXF 80, bit 0x08; DXF 75 == 5).
    k2dCubicSplinePoly    3       Cubic B-spline-fit (DXF 80, bit 0x08; DXF 75 == 6).
  */
  OdDb::Poly2dType polyType() const;

  /** Description:
    Sets the type of this Polyline entity. 
      
    Arguments:
    polyType (I) Polyline type.
    
    Remarks:
    polyType will be one of the following:
    
    @table
    Name                  Value   Description
    k2dSimplePoly         0       Simple polyline.
    k2dFitCurvePoly       1       Curve fit (DXF 70, bit 0x04).
    k2dQuadSplinePoly     2       Quadratic B-spline fit (DXF 80, bit 0x08; DXF 75 == 5).
    k2dCubicSplinePoly    3       Cubic B-spline-fit (DXF 80, bit 0x08; DXF 75 == 6).
  */
  void setPolyType(
    OdDb::Poly2dType polyType);

  /** Description:
    Sets this Polyline entity closed (DXF 70, bit 0x01=1).
  */
  void makeClosed();

  /** Description:
    Sets this Polyline entity open (DXF 70, bit 0x01=0).
  */
  void makeOpen();

  /** Description:
    Returns the default segment start width for this Polyline entity (DXF 40).
  */
  double defaultStartWidth() const;

  /** Description:
    Sets the default segment start width for this Polyline entity (DXF 40).
    Arguments:
    defaultStartWidth (I) Default start width.
  */
  void setDefaultStartWidth(
    double defaultStartWidth);

  /** Description:
    Returns the default segment end width for this Polyline entity (DXF 41).
  */
  double defaultEndWidth() const;

  /** Description:
    Sets the default segment end width for this Polyline entity (DXF 41).
    Arguments:
    defaultEndWidth (I) Default end width.
  */
  void setDefaultEndWidth(
    double defaultEndWidth);

  /** Description:
    Returns the *thickness* of this entity (DXF 39).
    
    Remarks:
    Thickness is the extrusion length along the *normal*.
  */
  double thickness() const;
  
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
    Returns the WCS *normal* to the plane of this entity (DXF 210).
  */
  OdGeVector3d normal() const;
  
  /** Description:
    Sets the WCS *normal* to the plane of this entity (DXF 210).
    Arguments:
    normal (I) Normal.
  */
  void setNormal(
    const OdGeVector3d& normal);

  /** Description:
    Returns the *elevation* of this entity in the OCS (DXF 30).
    
    Remarks:
    The *elevation* is the *distance* from the WCS origin to the plane of this entity.
  */
  double elevation() const;

  /** Description:
    Sets the *elevation* of this entity in the OCS (DXF 30).

    Arguments:
    elevation (I) Elevation.    

    Remarks:
    The *elevation* is the *distance* from the WCS origin to the plane of this entity.
  */
  void setElevation(
    double elevation);

  /** Description:
    Returns true if and only if *linetype* generation is on for this Polyline entity (DXF 70, bit 0x80).
      
    Remarks:
    Linetype generation on indicates that the *linetype* pattern of this Polyline entity
    is continuously generated around all vertices, rather than being restarted at each vertex.
  */
  bool isLinetypeGenerationOn() const;

  /** Description:
    Sets the *linetype* generation on for this Polyline entity (DXF 70, bit 0x80).
      
    Remarks:
    Linetype generation on indicates that the *linetype* pattern of this Polyline entity
    is continuously generated around all vertices, rather than being restarted at each vertex.
  */
  void setLinetypeGenerationOn();

  /** Description:
    Sets the *linetype* generation off for this Polyline entity (DXF 70, bit 0x80).
      
    Remarks:
    Linetype generation off indicates that the *linetype* pattern of this Polyline entity is
    restarted at each vertex, rather than being continuously generated around all vertices.
  */
  void setLinetypeGenerationOff();

  /** Description:
    Removes all curve and spline fitting from this Polyline entity.
  
    Remarks:
    Removes all but the simple vertices.
  */
  void straighten();

  /** Description:
    Appends the specified Vertex entity to this Polyline entity, and makes this Polyline entity its owner.
    
    Remarks:
    Returns the Object ID of the appended vertex.
    
    If this Polyline entity is *database* resident, the Vertex entity will be made *database* resident. 
    
    If this Polyline entity is not *database* resident, the Vertex entity will be made *database* resident
    when this Polyline entity is made *database* resident.
    
    Note:
    If this Polyline is *database* resident, the Vertex entity must explicitly be closed when
    appendVertex() returns.
    
    Arguments:
    pVertex (I) Pointer to the Vertex entity to be appended.
  */
  OdDbObjectId appendVertex(
    OdDb2dVertex* pVertex);

  /** Description:
    Inserts the specified Vertex entity into this Polyline entity
    after the specified vertex, and makes this Polyline its owner.
    
    Remarks:
    Returns the Object ID of the newly inserted vertex.
    
    The Vertex will be made *database* resident. 
    
    If this Polyline entity is not *database* resident, the vertex will be made *database* resident
    when the polyline is made *database* resident.
    
    To insert the specified Vertex at the start of this Polyline, use a null indexVertexId.
    
    Note:
    The Vertex entity must explicitly be closed when insertertex() returns.

    Arguments:
    indexVertId (I) Object ID of vertex after which to insert the specified vertex.
    pVertex (I) Pointer to the Vertex entity to be inserted.

  */
  OdDbObjectId insertVertexAt(
    const OdDbObjectId& indexVertId, 
    OdDb2dVertex* pVertex);

  /**
    Arguments:
    pIndexVert (I) Pointer to vertex after which to insert the specified vertex.
  */
  OdDbObjectId insertVertexAt(
    const OdDb2dVertex* pIndexVert, 
    OdDb2dVertex* pVertex);

  /** Description:
    Opens a vertex owned by this Polyline entity.

    Arguments:
    vertId (I) Object ID of vertex to be opened.
    mode (I) Mode in which to open the vertex.
    openErasedOne (I) If and only if true, *erased* objects will be opened.

    Remarks:
    Returns a SmartPointer to the opened object if successful, otherwise a null SmartPointer.
  */
  OdDb2dVertexPtr openVertex(
    OdDbObjectId vertId, 
    OdDb::OpenMode mode, 
    bool openErasedOne = false);

  /** Description:
    Opens the OdDbSequenceEnd entity for this Polyline entity.

    Arguments:
    mode (I) Mode in which to open the OdDbSequenceEnd entity.

    Remarks:
    Returns a SmartPointer to the newly opened OdDbSequenceEnd, or a null SmartPointer.

    Note:
    This method is provided solely for applications that store XData on
    OdDbSequenceEnd entities; this is not recommended. 
  */
  OdDbSequenceEndPtr openSequenceEnd(
    OdDb::OpenMode mode);

  /** Description:
    Returns a SmartPointer to an iterator that can be used to traverse the vertices owned 
    by this Polyline entity.
  */
  OdDbObjectIteratorPtr vertexIterator() const;

  /** Description:
    Returns the WCS position of the specified Vertex entity.
    
    Arguments:
    vertex (I) Vertex.
  */
  OdGePoint3d vertexPosition(
    const OdDb2dVertex& vertex) const;

  virtual bool worldDraw(
    OdGiWorldDraw* pWd) const;

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult dxfInFields_R12(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields_R12(
    OdDbDxfFiler* pFiler) const;

  virtual void dxfOut(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult dxfIn(
    OdDbDxfFiler* pFiler);

  virtual void subClose();

  virtual OdResult explode(
    OdRxObjectPtrArray& entitySet) const;

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);

  virtual void getClassID(
    void** pClsid) const;

  virtual bool isPlanar() const;

  virtual OdResult getPlane(
    OdGePlane& plane, 
    OdDb::Planarity& planarity) const;


  // OdDbCurveMethods

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

  /*
      void convertToPolyType(OdDb::Poly2dType newVal);
      void splineFit();
      void splineFit(OdDb::Poly2dType splineType, OdInt16 splineSegs);
      void curveFit();
  */
};
/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDb2dPolyline object pointers.
*/
typedef OdSmartPtr<OdDb2dPolyline> OdDb2dPolylinePtr;


/*

inline void OdDb2dPolyline::extend(double)
{
  return OdeNotApplicable;
}
*/
#include "DD_PackPop.h"

#endif


