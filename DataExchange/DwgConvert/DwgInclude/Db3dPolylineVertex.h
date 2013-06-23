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



#ifndef _OD_DB_3DPOLYLINE_VERTEX_
#define _OD_DB_3DPOLYLINE_VERTEX_

#include "DD_PackPush.h"

#include "DbVertex.h"

/** Description:
    This class represents OdDb3dPolyline vertices in an OdDbDatabase instance.
  
    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDb3dPolylineVertex : public OdDbVertex
{
public:

  ODDB_DECLARE_MEMBERS(OdDb3dPolylineVertex);

 /**
    Remarks:
    Newly created vertices are of type OdDb::k3dSimpleVertex.
  */
  OdDb3dPolylineVertex();

  /** Description:
    Returns the type of this Vertex entity.  
    
    Remarks:
    vertexType type will return one of the following:

    @table
    Name               Value   DXF 70   Description
    k3dSimpleVertex    0       0x40     Standard vertex.
    k3dControlVertex   1       0x10     Spline-fit or curve-fit control point.
    k3dFitVertex       2       0x08     Spline-fit or curve-fit generated vertex.
  */
  OdDb::Vertex3dType vertexType() const;

  /** Description:
    Sets the type of this Vertex entity.

    Arguments:
    vertexType (I) Vertex type.

    Remarks:
    vertexType type will be one of the following:

    @table
    Name                     Value   DXF 70   Description
    k3dSimpleVertex    0       0x40     Standard vertex.
    k3dControlVertex   1       0x10     Spline-fit or curve-fit control point.
    k3dFitVertex       2       0x08     Spline-fit or curve-fit generated vertex.
  */
  void setVertexType (OdDb::Vertex3dType 
    vertexType);

  /** Description:
    Returns the WCS *position* of this entity (DXF 10).
  */
  OdGePoint3d position() const;

  /** Description:
    Sets the WCS *position* of this entity (DXF 10).
    Arguments:
    position (I) Position.
  */
  void setPosition(
    const OdGePoint3d& position);

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields_R12(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields_R12(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult getGeomExtents(
    OdGeExtents3d& extents) const;

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);


  /*
     OdDb3dPolylineVertex(const OdGePoint3d&);
  */
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDb3dPolylineVertex object pointers.
*/
typedef OdSmartPtr<OdDb3dPolylineVertex> OdDb3dPolylineVertexPtr;

#include "DD_PackPop.h"

#endif


