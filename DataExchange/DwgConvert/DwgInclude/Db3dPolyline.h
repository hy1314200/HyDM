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



#ifndef _OD_DB_3D_POLYLINE_
#define _OD_DB_3D_POLYLINE_

#include "DD_PackPush.h"

#include "DbCurve.h"
#include "DbObjectIterator.h"

class OdDb3dPolylineVertex;
class OdDbSequenceEnd;

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDb3dPolylineVertex object pointers.
*/
typedef OdSmartPtr<OdDb3dPolylineVertex> OdDb3dPolylineVertexPtr;
/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbSequenceEnd object pointers.
*/
typedef OdSmartPtr<OdDbSequenceEnd> OdDbSequenceEndPtr;

/** Description: 
  
    {group:DD_Namespaces}
*/
namespace OdDb
{
  enum Poly3dType
  {
    k3dSimplePoly      = 0, // Simple polyline.
    k3dQuadSplinePoly  = 1, // Quadratic B-spline fit (DXF 80, bit 0x08; DXF 75 == 5).
    k3dCubicSplinePoly = 2  // Cubic B-spline-fit (DXF 80, bit 0x08; DXF 75 == 6).
  };
}

/** Description:
    This class represents 3D Polyline entities in an OdDbDatabase instance.

    Library:
    Db
    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDb3dPolyline: public OdDbCurve
{
public:

  ODDB_DECLARE_MEMBERS(OdDb3dPolyline);

  OdDb3dPolyline();

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
    Sets this Polyline entity closed (DXF 70, bit 0x01=1).
  */
  void makeClosed();

  /** Description:
    Sets this Polyline entity open (DXF 70, bit 0x01=0).
  */
  void makeOpen();

  /** Description:
    Returns the type of this Polyline entity. 
    
    Remarks:
    polyType will return one of the following:
    
    @table
    Name                  Value   Description
    k3dSimplePoly         0       Simple polyline.
    k3dQuadSplinePoly     1       Quadratic B-spline fit (DXF 80, bit 0x08; DXF 75 == 5).
    k3dCubicSplinePoly    2       Cubic B-spline-fit (DXF 80, bit 0x08; DXF 75 == 6).
  */
  OdDb::Poly3dType polyType() const;

  /** Description:
    Returns the type of this Polyline entity. 

    Arguments:
    polyType (I) Polyline type.
   
    Remarks:
    polyType will return one of the following:
    
    @table
    Name                  Value   Description
    k3dSimplePoly         0       Simple polyline.
    k3dQuadSplinePoly     1       Quadratic B-spline fit (DXF 80, bit 0x08; DXF 75 == 5).
    k3dCubicSplinePoly    2       Cubic B-spline-fit (DXF 80, bit 0x08; DXF 75 == 6).
  */
  void setPolyType(OdDb::Poly3dType);

  /** Description:
    Removes all spline fitting from this Polyline entity.
  
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
    OdDb3dPolylineVertex* pVertex);

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
    OdDb3dPolylineVertex* pVertex);

  /**
    Arguments:
    pIndexVert (I) Pointer to vertex after which to insert the specified vertex.
  */
  OdDbObjectId insertVertexAt(
    const OdDb3dPolylineVertex* pIndexVert, 
    OdDb3dPolylineVertex* pVertex);

  /** Description:
    Opens a vertex owned by this Polyline entity.

    Arguments:
    vertId (I) Object ID of vertex to be opened.
    mode (I) Mode in which to open the vertex.
    openErasedOne (I) If and only if true, *erased* objects will be opened.

    Remarks:
    Returns a SmartPointer to the opened object if successful, otherwise a null SmartPointer.
  */
  OdDb3dPolylineVertexPtr openVertex(
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

  virtual void dxfOut(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult dxfIn(
    OdDbDxfFiler* pFiler);

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

  virtual void getClassID(
    void** pClsid) const;

  void subClose();

  virtual OdResult explode(
    OdRxObjectPtrArray& entitySet) const;

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);

  virtual bool worldDraw(
    OdGiWorldDraw* pWd) const;

  virtual bool isPlanar() const;

  virtual OdResult getPlane(
    OdGePlane& plane, 
      OdDb::Planarity& planarity) const;


  /* OdDbCurveMethods */

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
  OdDb3dPolyline(OdDb::Poly3dType, OdGePoint3dArray& vertices, bool closed = false);
  void convertToPolyType(OdDb::Poly3dType newVal);
  void splineFit();
  void splineFit(OdDb::Poly3dType splineType, OdInt16 splineSegs);
  */
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDb3dPolyline object pointers.
*/
typedef OdSmartPtr<OdDb3dPolyline> OdDb3dPolylinePtr;

#include "DD_PackPop.h"

#endif


