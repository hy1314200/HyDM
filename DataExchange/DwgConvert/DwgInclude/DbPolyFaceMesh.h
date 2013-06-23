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



#ifndef _OD_DB_POLYFACE_MESH_
#define _OD_DB_POLYFACE_MESH_

#include "DD_PackPush.h"

#include "DbEntity.h"
#include "DbObjectIterator.h"

class OdDbPolyFaceMeshVertex;
class OdDbSequenceEnd;
class OdDbFaceRecord;
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbPolyFaceMeshVertex object pointers.
*/
typedef OdSmartPtr<OdDbPolyFaceMeshVertex> OdDbPolyFaceMeshVertexPtr;
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbSequenceEnd object pointers.
*/
typedef OdSmartPtr<OdDbSequenceEnd> OdDbSequenceEndPtr;

/** Description:
    This class represents PolyFaceMesh entities in an OdDbDatabase instance.
  
    Library:
    Db

    Remarks:
    A PolyFaceMesh entity consists of a list of PolyFaceMeshVertex (coordinate) vertices 
    and a list of OdDbFaceRecord face records.
    Together they define a set of 3D faces.
    
    Note:
    Never derive from ths class.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbPolyFaceMesh : public OdDbEntity
{
public:
  ODDB_DECLARE_MEMBERS(OdDbPolyFaceMesh);

  OdDbPolyFaceMesh();
  
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
    Returns the number of vertices in this PolyFaceMesh entity (DXF 71).
  */
  OdInt16 numVertices() const;

  /** Description:
    Returns the number of faces in this PolyFaceMesh entity (DXF 72).
  */
  OdInt16 numFaces() const;
  
  /** Description:
    Appends the specified face vertex onto this PolyFaceMesh entity.

    Arguments:
    pVertex (I) Pointer to the vertex to append.

    Remarks:
    Returns the Object ID of the appended face vertex.
  */
  OdDbObjectId appendVertex(
    OdDbPolyFaceMeshVertex* pVertex);
  
  /** Description:
    Appends the specified face record onto this PolyFaceMesh entity.

    Arguments:
    pFaceRecord (I) Pointer to the face record to append.

    Remarks:
    Returns the Object ID of the newly appended face record.
  */
  OdDbObjectId appendFaceRecord(
    OdDbFaceRecord* pFaceRecord);
  
  /** Description:
    Opens the specified vertex or face record owned by this PolyFaceMesh entity.

    Arguments:
    subObjId (I) Object ID of vertex or face record to be opened.
    mode (I) Mode in which the object is being opened.
    openErasedOne (I) If and only if true, *erased* objects will be opened.
    Remarks:
    Returns a SmartPointer to the opened object if successful, otherwise a null SmartPointer.
  */
  OdDbPolyFaceMeshVertexPtr openVertex(
    OdDbObjectId subObjId, 
    OdDb::OpenMode mode, 
    bool openErasedOne = false);
  
  /** Description:
    Opens the OdDbSequenceEnd entity for this PolyfaceMesh entity.

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
    Returns a SmartPointer to an iterator that can be used to traverse the vertices and face records owned 
    by this PolyFaceMesh entity.
  */
  OdDbObjectIteratorPtr vertexIterator() const;

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

  virtual void dxfOut(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult dxfIn(
    OdDbDxfFiler* pFiler);

  virtual void getClassID(
    void** pClsid) const;

  virtual OdResult dxfInFields_R12(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields_R12(
    OdDbDxfFiler* pFiler) const;

  void subClose();

  virtual bool isPlanar() const;
  
  virtual OdResult getPlane(
    OdGePlane& plane, 
    OdDb::Planarity& planarity) const;

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);

  OdResult getGeomExtents(
    OdGeExtents3d& extents) const;

  /** Remarks:
    Creates and returns a set of OdDbFace entities.
  */
  virtual OdResult explode(
    OdRxObjectPtrArray& entitySet) const;
};
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbPolyFaceMesh object pointers.
*/
typedef OdSmartPtr<OdDbPolyFaceMesh> OdDbPolyFaceMeshPtr;

#include "DD_PackPop.h"

#endif

