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



#ifndef OD_DBMLINE_H
#define OD_DBMLINE_H

#include "DD_PackPush.h"

#include "DbEntity.h"
#include "Ge/GeVoidPointerArray.h"

/** Description:
  This structure contains enumerated types and data used by OdDbMline.
  
  {group:Structs}
*/
struct Mline
{
  typedef OdInt8 MlineJustification;
  enum
  {
    kTop = 0,
    kZero = 1,
    kBottom = 2
  };
  enum
  {
    kOpen = 0,
    kClosed = 1,
    kMerged = 2
  };
};

class OdGePlane;

/** Description:
  {group:Structs}
*/      
struct OdMLSegment
{
  OdGeDoubleArray m_AreaFillParams;
  OdGeDoubleArray m_SegParams;
};

typedef OdArray<OdMLSegment> OdMLSegmentArray;

/** Description:
    This class represents multi-line (MLine) entities in an OdDbDatabase instance.
    
    Library:
    Db
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbMline : public OdDbEntity
{
public:
  ODDB_DECLARE_MEMBERS(OdDbMline);

  OdDbMline();

  /** Description:
    Sets the Object ID of the MLine Style of this MLine entity (DXF 340).
    Arguments:
    styleId (I) Object ID of the MLine Style
  */
  void setStyle(
    const OdDbObjectId &styleId);

  /** Description:
    Returnss the Object ID of the MLine Style of this MLine entity (DXF 340).
  */
  OdDbObjectId style() const;

  /** Description:
    Sets the *justification* of this MLine entity (DXF 70).
    Arguments:
    justification (I) Justification.
  */
  void setJustification(Mline::MlineJustification justification);

  /** Description:
    Returns the *justification* of this MLine entity (DXF 70).
  */
  Mline::MlineJustification justification() const;

  /** Description:
    Sets the *scale* of this entity (DXF 40).
  */
  void setScale(
    double scale);

  /** Description:
    Returns the *scale* of this entity (DXF 40).
  */
  double scale() const;

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
    Appends a vertex onto the end of this MLine entity.

    Arguments:
    newVertex (I) Vertex to append.
    
    Remarks:
    newVertex is projected onto the plane of this MLine entity, and this projected vertex
    appended to it.     
  */
  void appendSeg(
    const OdGePoint3d& newVertex);

  /** Description:
    Returns and removes the last vertex from this MLine entity.

    Arguments:
    lastVertex (O) Receives the last vertex.
  */
  void removeLastSeg
    (OdGePoint3d& lastVertex);

  /** Description:
    Moves the specified vertex of this MLine entity.

    Arguments:
    vertexIndex (I) Vertex index.
    newPosition (I) New WCS position for specified vertex.

    Remarks:
    newPosition is projected onto the plane of this MLine entity, and this projected vertex
    replaces the specified vertex.     
  */
  void moveVertexAt(
    int vertexIndex, 
    const OdGePoint3d& newPosition);

  /** Description:
    Controls the closed status of this MLine entity (DXF 71, bit 0x02).
    
    Arguments:
    closedMline (I) Sets this MLine closed if true, open if false.
  */
  void setClosedMline(
    bool closedMline);

  /** Description:
    Returns the closed status of this MLine entity(DXF 71, bit 0x02).
    Remarks:
    Returns true if and only if this MLine entity is closed.
  */
  bool closedMline() const;

  /** Description:
    Sets the "Suppress Start Caps" status of this MLine entity (DXF 71, bit 0x04).

    Arguments:
    suppressIt (I) Suppresses startcaps if true, enables startcaps if false.
  */
  void setSupressStartCaps(
    bool supressIt);

  /** Description:
    Returns the "Suppress Start Caps" status of this MLine entity (DXF 71, bit 0x04).

    Remarks:
    Returns true if and only if startcaps are suppressed.
  */
  bool supressStartCaps() const;

  /** Description:
    Sets the "Suppress End Caps" status of this MLine entity (DXF 71, bit 0x04).

    Arguments:
    suppressIt (I) Suppresses endcaps if true, enables endcaps if false.
  */
  void setSupressEndCaps(
    bool supressIt);

  /** Description:
    Returns the "Suppress End Caps" status of this MLine entity (DXF 71, bit 0x04).

    Remarks:
    Returns true if and only if endcaps are suppressed.
  */
  bool supressEndCaps() const;

  /** Description:
    Returns the number of vertices in this MLine entity (DXF 72).
  */
  int numVertices() const;

  /** Description:
    Returns the specified vertex of this MLine entity (DXF 10 or 11).
    Arguments:
    vertexIndex (I) Vertex index.
  */
  OdGePoint3d vertexAt(
    int vertexIndex) const;

  /** Description:
    Returns the *direction* vector of the segment starting at the specified vertex (DXF 12).
    Arguments:
    vertexIndex (I) Vertex index.
  */
  OdGeVector3d axisAt(
    int vertexIndex) const;

  /** Description:
    Returns the *direction* vector of the miter starting at the specified vertex (DXF 13).
    Arguments:
    vertexIndex (I) Vertex index.
  */
  OdGeVector3d miterAt(
    int vertexIndex) const;

  /** Description:
    Returns the element and area fill parameters at the specified vertex (DXF 41 and 42).
    Arguments:
    vertexIndex (I) Vertex index.
    params (O) Receives the element and area fill parameters.
  */
  void getParametersAt(int index, OdMLSegmentArray& params) const;

  /** Description:
    Sets the element and area fill parameters at the specified vertex (DXF 41 and 42).
    Arguments:
    vertexIndex (I) Vertex index.
    params (I) Element and area fill parameters.
  */
  void setParametersAt(
    int vertexIndex, 
    const OdMLSegmentArray& params);

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);

  virtual bool worldDraw(
    OdGiWorldDraw* pWd) const;

  virtual OdResult explode(
    OdRxObjectPtrArray& entitySet) const; /* Replace OdRxObjectPtrArray */

  virtual void subClose();

  virtual void getClassID(
    void** pClsid) const;

  /*
  
    int element(const OdGePoint3d & pt) const;

    void getClosestPointTo(const OdGePoint3d& givenPoint,
      OdGePoint3d& pointOnCurve,
      bool extend, bool excludeCaps = false) const;

    void getClosestPointTo(const OdGePoint3d& givenPoint,
      const OdGeVector3d& normal, OdGePoint3d& pointOnCurve,
      bool extend, bool excludeCaps = false) const;
  
    OdResult getPlane(OdGePlane&) const;
    bool getGeomExtents(OdGeExtents3d&) const;

    void getSubentPathsAtGsMarker(OdDb::SubentType type, int gsMark,
      const OdGePoint3d& pickPoint, const OdGeMatrix3d& viewXform,
      int& numPaths, OdDbFullSubentPath*& subentPaths,
      int numInserts = 0, OdDbObjectId* entAndInsertStack = NULL) const;

    void getGsMarkersAtSubentPath(const OdDbFullSubentPath& subPath, OdDbIntArray& gsMarkers) const;

    OdDbEntity* subentPtr(const OdDbFullSubentPath& id) const;
  
  */

};
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbMline object pointers.
*/
typedef OdSmartPtr<OdDbMline> OdDbMlinePtr;

#include "DD_PackPop.h"

#endif


