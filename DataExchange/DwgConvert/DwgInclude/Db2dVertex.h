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



#ifndef _OD_DB_2DVERTEX_
#define _OD_DB_2DVERTEX_

#include "DD_PackPush.h"

#include "DbVertex.h"

/** Description: 
  
    {group:DD_Namespaces}
*/
namespace OdDb
{
  enum Vertex2dType
  {
    k2dVertex          = 0,  // Standard vertex
    k2dSplineCtlVertex = 1,  // Spline-fit or curve-fit control point
    k2dSplineFitVertex = 2,  // Spline-fit generated vertex
    k2dCurveFitVertex  = 3   // Curve-fit generated vertex
  };
}

/** Description:
    This class represents OdDb2Polyline vertices in an OdDbDatabase instance.
  
    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDb2dVertex: public OdDbVertex
{
public:

  ODDB_DECLARE_MEMBERS(OdDb2dVertex);

  /**
    Remarks:
    Newly created vertices are of type OdDb::k2dVertex.
  */
  OdDb2dVertex();

  /** Description:
    Returns the type of this Vertex entity.  
    
    Remarks:
    vertexType type will return one of the following:

    @table
    Name                  Value   DXF 70  Description    
    k2dVertex             0       0x00    Standard vertex.
    k2dSplineCtlVertex    1       0x10    Spline-fit or curve-fit control point.
    k2dSplineFitVertex    2       0x08    Spline-fit generated vertex.
    k2dCurveFitVertex     3       0x01    Curve-fit generated vertex.
  */
  OdDb::Vertex2dType vertexType() const;

  /** Description:
    Sets the type of this Vertex entity.

    Arguments:
    vertexType (I) Vertex type.

    Remarks:
    vertexType type will be one of the following:

    @table
    Name                  Value   DXF 70  Description    
    k2dVertex             0       0x00    Standard vertex.
    k2dSplineCtlVertex    1       0x10    Spline-fit or curve-fit control point.
    k2dSplineFitVertex    2       0x08    Spline-fit generated vertex.
    k2dCurveFitVertex     3       0x01    Curve-fit generated vertex.
  */
  void setVertexType (
    OdDb::Vertex2dType vertexType);

  /** Description:
    Returns the OdDb2dPolyline OCS *position* of this entity (DXF 10).

    Remarks:
    Each OdDb2dPolyline is described a series of OCS AcDb2dVertices and an elevation.
    
    While Z coordinates are set and retrieved by setPosition() and position() respectively,
    they are ignored by the Polyline.
    
    The elevation of the Polyline can be changed only with OdDb2dPolyline::setElevation(). 
      
  */
  OdGePoint3d position() const;

  /** Description:
    Sets the OdDb2dPolyline OCS *position* of this entity (DXF 10).
    Arguments:
    position (I) Position.
    
    Remarks:
    Each OdDb2dPolyline is described a series of OCS AcDb2dVertices and an elevation.
    
    While Z coordinates are set and retrieved by setPosition() and position() respectively,
    they are ignored by the Polyline.
    
    The elevation of the Polyline can be changed only with OdDb2dPolyline::setElevation(). 
  */
  void setPosition(
    const OdGePoint3d& position);

  /** Description:
    Returns the starting width of the segment following this Vertex entity (DXF 40).
  */
  double startWidth() const;

  /** Description:
    Sets the starting width of the segment following this Vertex entity (DXF 40).

    Arguments:
    startWidth (I) Starting width.
  */
  void setStartWidth(
    double startWidth);

  /** Description:
    Returns the ending width of the segment following this Vertex entity (DXF 41).
  */
  double endWidth() const;

  /** Description:
    Sets the ending width of the segment following this Vertex entity (DXF 41).
   
    Arguments:
    endWidth (I) Ending width.
  */
  void setEndWidth(
    double endWidth);

  /** Description:
    Returns the *bulge* of the segment following this Vertex entity (DXF 42).
    
    Remarks:
    Bulge is the *tangent* of 1/4 the included angle of the arc segment, measured counterclockwise.
  */
  double bulge() const;

  /** Description:
    Sets the *bulge* of the arc segment following this Vertex entity (DXF 42).

    Arguments:
    bulge (I) Bulge.
    
    Remarks:
    Bulge is the *tangent* of 1/4 the included angle of the arc segment, measured counterclockwise.
  */
  void setBulge(
    double bulge);

  /** Description:
    Returns true if and only if the curve fit *tangent* direction is used by this Vertex entity.(DXF 70, bit 0x02).
  */
  bool isTangentUsed() const;

  /** Description:
    Specifies the curve fit *tangent* direction is to be used by this Vertex entity.(DXF 70, bit 0x02).
  */
  void useTangent();

  /** Description:
    Specifies the curve fit *tangent* direction is not to be used by this Vertex entity.(DXF 70, bit 0x02).
  */
  void ignoreTangent();

  /** Description:
    Returns the curve fit *tangent* direction for this Vertex (DXF 50).

    Remarks:
    This is the angle between the the OdDb2dPolyline OCS X-axis and the *tangent* direction.

    Note:
    All angles are expressed in radians.
  */
  double tangent() const;

  /** Description:
    Sets the curve fit *tangent* direction for this Vertex (DXF 50).

    Arguments:
    tangent (I) Tangent angle.
    
    Remarks:
    This is the angle between the the OdDb2dPolyline OCS X-axis and the *tangent* direction.

    Note:
    All angles are expressed in radians.
  */
  void setTangent(
    double tangent);

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

  /*
      OdDb2dVertex(const OdGePoint3d&  position, double bulge = 0,
                   double startWidth = 0, double endWidth = 0, double tangent = 0);
  */


  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);
};
/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDb2dVertex object pointers.
*/
typedef OdSmartPtr<OdDb2dVertex> OdDb2dVertexPtr;

#include "DD_PackPop.h"

#endif


