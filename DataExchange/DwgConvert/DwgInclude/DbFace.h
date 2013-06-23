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



#ifndef _OD_DB_FACE_
#define _OD_DB_FACE_

#include "DD_PackPush.h"

#include "DbEntity.h"

/** Description:
    This class represents 3D face entities in an OdDbDatabase instance.

    Library:
    Db
    
    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDbFace : public OdDbEntity
{
public:                
  ODDB_DECLARE_MEMBERS(OdDbFace);
  OdDbFace();

  /** Description:
    Returns the WCS value of the specified *vertex* (DXF 10-13).

    Arguments:
    index (I) Vertex *index* [0,3].
    vertex (O) Receives the WCS value of the specified *vertex*.
  */
  void getVertexAt(
    OdUInt16 index, 
    OdGePoint3d& vertex) const;

  /** Description:
  Sets the WCS value of the specified *vertex* (DXF 10-13).

  Arguments:
    index (I) Vertex *index* [0,3].
    vertex (I) The WCS value for the specified *vertex*.
  */
  void setVertexAt(
    OdUInt16 index, 
    const OdGePoint3d& vertex);

  /** Description:
    Returns true if and only if the specified edge is visible (DXF 70).

    Arguments:
    index (I) Edge *index* [0,3].
  */
  bool isEdgeVisibleAt(
    OdUInt16 index) const;

  /** Description:
    Turns on the *visibility* for the specified edge (DXF 70).

    Arguments:
    index (I) Edge *index* [0,3].
  */
  void makeEdgeVisibleAt(
  OdUInt16 index);

  /** Description:
    Turns off the *visibility* for the specified edge (DXF 70).

    Arguments:
    index (I) Edge *index* [0,3].
  */
  void makeEdgeInvisibleAt(
    OdUInt16 index);

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

  bool worldDraw(
    OdGiWorldDraw* pWd) const;

  void viewportDraw(
    OdGiViewportDraw* pVd) const;

  virtual void getClassID(
    void** pClsid) const;

  virtual OdResult transformBy(const OdGeMatrix3d& xfm);

  /*
  OdDbFace(const OdGePoint3d& pt0, const OdGePoint3d& pt1, const OdGePoint3d& pt2,
  const OdGePoint3d& pt3, bool e0vis = true, bool e1vis = true,
  bool e2vis = true, bool e3vis = true);
  OdDbFace(const OdGePoint3d& pt0, const OdGePoint3d& pt1, const OdGePoint3d& pt2,
  bool e0vis = true, bool e1vis = true, bool e2vis = true, bool e3vis = true);
  */

};
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbFace object pointers.
*/
typedef OdSmartPtr<OdDbFace> OdDbFacePtr;

#include "DD_PackPop.h"

#endif

