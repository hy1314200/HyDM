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



#ifndef _OD_DB_POLYGON_MESH_
#define _OD_DB_POLYGON_MESH_

#include "DD_PackPush.h"

#include "DbEntity.h"
#include "DbObjectIterator.h"
#include "DbPolygonMeshVertex.h"

class OdDbSequenceEnd;
typedef OdSmartPtr<OdDbPolygonMeshVertex> OdDbPolygonMeshVertexPtr;
typedef OdSmartPtr<OdDbSequenceEnd> OdDbSequenceEndPtr;

/** Description: 
  
    {group:DD_Namespaces}
*/
namespace OdDb
{
  enum PolyMeshType
  {
    kSimpleMesh        = 0, // Simple mesh
    kQuadSurfaceMesh   = 5, // Quadratic B-spline fit
    kCubicSurfaceMesh  = 6, // Cubic B-spline fit
    kBezierSurfaceMesh = 8  // Bezier Surface fit
  };
}

/** Description:
    This class represents PolygonMesh entities in an OdDbDatabase instance.

    Library:
    Db

    Remarks:
    A PolygonMesh entity consists of a list of PolygonMeshVertex (coordinate) vertices
    describing a M × N array of vertices, defining a set of 3D faces.   
    
    M is the number vertices in a row, N is the number of vertices in a column.
    The first N vertices define the first column, the second N, the second column, etc.

    The mesh may be closed in the M and/or N directions. A closed mesh is connected from the last row
    or column to the first.

    Note:
    Never derive from ths class.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbPolygonMesh : public OdDbEntity
{
public:
  ODDB_DECLARE_MEMBERS(OdDbPolygonMesh);

  OdDbPolygonMesh();
  
  /* OdDbPolygonMesh(OdDb::PolyMeshType pType,
       OdInt16 mSize,
       OdInt16 nSize,
       const OdGePoint3dArray& vertices,
       bool mClosed = true,
       bool nClosed = true);
  */

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
    Returns the mesh type of this PolygonMesh entity (DXF 75).
    
    Remarks:
    polyMeshType will return one of the following:
      
    @table
    Name                 Value    Description
    kSimpleMesh          0        Simple Mesh
    kQuadSurfaceMesh     5        Quadratic B-spline fitting
    kCubicSurfaceMesh    6        Cubic B-spline fitting
    kBezierSurfaceMesh   8        Bezier Surface fitting
  */
  OdDb::PolyMeshType polyMeshType() const;

  /** Description:
    Sets the mesh type of this PolygonMesh entity (DXF 75).

    Arguments:
    polyMeshType (I) PolygonMesh type.
    
    Remarks:
    polyMeshType will be one of the following:
      
    @table
    Name                 Value    Description
    kSimpleMesh          0        Simple Mesh
    kQuadSurfaceMesh     5        Quadratic B-spline fitting
    kCubicSurfaceMesh    6        Cubic B-spline fitting
    kBezierSurfaceMesh   8        Bezier Surface fitting
  */
  void setPolyMeshType(
    OdDb::PolyMeshType polyMeshType);

  /** Description:
    Uses surfaceFit() to convert mesh type of this PolygonMesh entity (DXF 75).

    Arguments:
    polyMeshType (I) PolygonMesh type.
    
    Remarks:
    polyMeshType will be one of the following:
      
    @table
    Name                 Value    Description
    kSimpleMesh          0        Simple Mesh
    kQuadSurfaceMesh     5        Quadratic B-spline fitting
    kCubicSurfaceMesh    6        Cubic B-spline fitting
    kBezierSurfaceMesh   8        Bezier Surface fitting
  */
  void convertToPolyMeshType(
    OdDb::PolyMeshType polyMeshType);
  
  /** Description:
    Returns the number of vertices in the M direction for this PolygonMesh entity (DXF 71).
  */
  OdInt16 mSize() const;

  /** Description:
    Sets the number of vertices in the M direction for this PolygonMesh entity (DXF 71).
    Arguments:
    mSize (I) Number of vertices in M direction.  
  */
  void setMSize(
    OdInt16 mSize);
  
  /** Description:
    Returns the number of vertices in the N direction for this PolygonMesh entity (DXF 72).
  */
  OdInt16 nSize() const;

  /** Description:
    Sets the number of vertices in the N direction for this PolygonMesh entity (DXF 72).
    Arguments:
    nSize (I) Number of vertices in N direction.  
  */
  void setNSize(OdInt16 nSize);
  
  /** Description:
    Returns true if and only if this PolygonMesh entity is closed in the M direction (DXF 70, bit 0x01).
  */
  bool isMClosed() const;

  /** Description:
    Sets this PolygonMesh entity closed in the M direction (DXF 70, bit 0x01).
  */
  void makeMClosed();

  /** Description:
    Sets this PolygonMesh entity opened in the M direction (DXF 70, bit 0x01).
  */
  void makeMOpen();
  
  /** Description:
    Returns true if and only if this PolygonMesh entity is closed in the N direction (DXF 70, bit 0x20).
  */
  bool isNClosed() const;

  /** Description:
    Sets this PolygonMesh entity closed in the N direction (DXF 70, bit 0x20).
  */
  void makeNClosed();

  /** Description:
    Sets this PolygonMesh entity opened in the N direction (DXF 70, bit 0x20).
  */
  void makeNOpen();
  
  /** Description:
    Returns the M surface density for this PolygonMesh entity (DXF 73).
    
    Remarks:
    This is the number of vertices in the M direction after a surfaceFit.
     
    Used instead of M if polyMeshType() != OdDb::kSimpleMesh.
  */
  OdInt16 mSurfaceDensity() const;

  /** Description:
    Sets the M surface density for this PolygonMesh entity (DXF 73).
    Arguments:
    mSurfaceDensity (I) M surface density. 
      
    Remarks:
    This is the number of vertices in the M direction after a surfaceFit.
    
    Used instead of M if polyMeshType() != OdDb::kSimpleMesh.
  */
  void setMSurfaceDensity(
    OdInt16 mSurfaceDensity);
  
  /** Description:
    Returns the N surface density for this PolygonMesh entity (DXF 74).
    
    Remarks:
    This is the number of vertices in the N direction after a surfaceFit. 
    
    Used instead of N if polyMeshType() != OdDb::kSimpleMesh.
  */
  OdInt16 nSurfaceDensity() const;

  /** Description:
    Sets the M surface density for this PolygonMesh entity (DXF 74).
    Arguments:
    nSurfaceDensity (I) N surface density. 
      
    Remarks:
    This is the number of vertices in the N direction after a surfaceFit.
    
    Used instead of N if polyMeshType() != OdDb::kSimpleMesh.
  */
  void setNSurfaceDensity(
    OdInt16 nSurfaceDensity);
  
  /** Description:
    Removes all the surface fit vertices for this PolygonMesh entity.
  */
  void straighten();

  /** Description:
    Surface fits a smooth surface to this PolygonMesh entity.
  */
  void surfaceFit();

  /** Arguments:
    surfType (I) Surface type (overrides SURFTYPE system variable).
    surfU (I) M Surface density (overrides SURFU system variable).
    surfV (I) N Surface density (overrides SURFV system variable.
    Remarks:
    surfU and surfV will be in the range [2..200].

    Remarks:
    surfType will be one of the following:
      
    @table
    Name                 Value    Description
    kQuadSurfaceMesh     5        Quadratic B-spline fitting
    kCubicSurfaceMesh    6        Cubic B-spline fitting
    kBezierSurfaceMesh   8        Bezier Surface fitting
          
     
  */
  void surfaceFit(
    OdDb::PolyMeshType surfType, 
    OdInt16 surfU, 
    OdInt16 surfV);
  
   /** Description:
    Appends the specified vertex onto this PolygonMesh entity.

    Arguments:
    pVertex (I) Pointer to the vertex to append.
    vType (I) Vertex type.
    
    Remarks:
    Returns the Object ID of the appended vertex.
    
    vType will be one of the following:
    
    @table
    Name               Value   DXF 70   Description
    k3dSimpleVertex    0       0x40     Standard vertex.
    k3dControlVertex   1       0x10     Spline-fit or curve-fit control point.
    k3dFitVertex       2       0x08     Spline-fit or curve-fit generated vertex.
  */
  OdDbObjectId appendVertex(
    OdDbPolygonMeshVertex* pVertex, 
    OdDb::Vertex3dType vType = OdDb::k3dSimpleVertex);
  
  /** Description:
    Opens a vertex owned by this PolygonMesh entity.

    Arguments:
    vertId (I) Object ID of vertex to be opened.
    mode (I) Mode in which to open the vertex.
    openErasedOne (I) If and only if true, *erased* objects will be opened.

    Remarks:
    Returns a SmartPointer to the opened object if successful, otherwise a null SmartPointer.
  */
  OdDbPolygonMeshVertexPtr openVertex(
    OdDbObjectId vertId, 
    OdDb::OpenMode mode, 
    bool openErasedOne = false);
  
  /** Description:
    Opens the OdDbSequenceEnd entity for this PolygonMesh entity.

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
    by this PolygonMesh entity.
  */
  OdDbObjectIteratorPtr vertexIterator() const;
  
  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual void dxfOut(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult dxfIn(
    OdDbDxfFiler* pFiler);

  virtual OdResult dxfInFields_R12(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields_R12(
    OdDbDxfFiler* pFiler) const;

  bool worldDraw(
    OdGiWorldDraw* pWd) const;

  void subClose();

  void getClassID(
    void** pClsid) const;

  virtual bool isPlanar() const;
  
  virtual OdResult getPlane(
    OdGePlane& plane, 
    OdDb::Planarity& planarity) const;

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);

  /** Remarks:
    Creates and returns a set of OdDbFace entities.
  */
  virtual OdResult explode(
    OdRxObjectPtrArray& entitySet) const;
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbPolygonMesh object pointers.
*/
typedef OdSmartPtr<OdDbPolygonMesh> OdDbPolygonMeshPtr;

#include "DD_PackPop.h"

#endif

