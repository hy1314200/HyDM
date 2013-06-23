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



#ifndef _OD_DB_FACERECORD_
#define _OD_DB_FACERECORD_

#include "DD_PackPush.h"

#include "DbVertex.h"

/** Description:
  This class represents OdDbPolyFaceMesh faces in an OdDbDatabase instance. 

  {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbFaceRecord : public OdDbEntity
{
public:
  ODDB_DECLARE_MEMBERS(OdDbFaceRecord);

  OdDbFaceRecord();

  /** Description:
    Returns the index of the parent PolyFaceMesh vertex that defines the specified corner (DXF 71-74) of
    this FaceRecord entity.
    
    Arguments:
    cornerIndex (I) Corner index [0..3].

    Remarks:
    Vertex indices start at 1.  Negative indices indicate the following edge is invisible.
  */
  OdInt16 getVertexAt(
    int cornerIndex) const;

  /** Description:
    Sets the specified corner (DXF 71-74) of this FaceRecord entity to the specified index of 
    the parent PolyFaceMesh mesh vertex. 
    
    Arguments:
    cornerIndex (I) Corner index [0..3].
    vertexIndex (I) Vertex index.
    
    Remarks:
    Vertex indices start at 1.  Negative indices indicate the following edge is invisible.
  */
  void setVertexAt(
    int cornerIndex, 
    OdInt16 vertexIndex);
  
  /** Description:
    Returns true if and only if the specified edge of this FaceRecord entity is visible (sign of DXF 71-74)

    Arguments:
    edgeIndex (I) Edge index [0..3]
  */
  bool isEdgeVisibleAt(
    int edgeIndex) const;
  
  /** Description:
    Sets visible the specified edge of this FaceRecord entity (sign of DXF 71-74)

    Arguments:
    edgeIndex (I) Edge index [0..3]
  */
  void makeEdgeVisibleAt(
    int edgeIndex);

  /** Description:
    Sets invisible the specified edge of this FaceRecord entity (sign of DXF 71-74)

    Arguments:
    edgeIndex (I) Edge index [0..3]
  */
  void makeEdgeInvisibleAt(
    int edgeIndex);

  virtual OdResult dxfInFields_R12(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields_R12(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult subErase(
    bool erasing);
};
/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbFaceRecord object pointers.
*/
typedef OdSmartPtr<OdDbFaceRecord> OdDbFaceRecordPtr;

#include "DD_PackPop.h"

#endif

