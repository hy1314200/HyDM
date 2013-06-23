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
// programs incorporating this software must include the following statment 
// with their copyright notices:
//
//      DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef __ODGIEMPTYGEOMETRY_H__
#define __ODGIEMPTYGEOMETRY_H__


#include "GiExport.h"
#include "GiConveyorGeometry.h"

#include "DD_PackPush.h"

/** Description:
    OdGiConveyorGeometry implementation that provides no-ops for all pure virtual functions.

    {group:OdGi_Classes} 
*/
class ODGI_EXPORT OdGiEmptyGeometry : public OdGiConveyorGeometry
{
public:
  static OdGiConveyorGeometry& kVoid;

  /** Remarks:
      No-op.
  */
  void plineProc(const OdGiPolyline& lwBuf,
    const OdGeMatrix3d* pXform = 0,
    OdUInt32 fromIndex = 0,
    OdUInt32 numSegs = 0);

  /** Remarks:
      No-op.
  */
  void circleProc(
    const OdGePoint3d& center, double radius,
    const OdGeVector3d& normal,
    const OdGeVector3d* pExtrusion = 0);
  
  /** Remarks:
      No-op.
  */
  void circleProc(
    const OdGePoint3d&, const OdGePoint3d&, const OdGePoint3d&,
    const OdGeVector3d* pExtrusion = 0);
  
  /** Remarks:
      No-op.
  */
  void circularArcProc(
    const OdGePoint3d& center,
    double radius,
    const OdGeVector3d& normal,
    const OdGeVector3d& startVector,
    double sweepAngle,
    OdGiArcType arcType = kOdGiArcSimple,
    const OdGeVector3d* pExtrusion = 0);
  
  /** Remarks:
      No-op.
  */
  void circularArcProc(
    const OdGePoint3d& start,
    const OdGePoint3d& point,
    const OdGePoint3d& end,
    OdGiArcType arcType = kOdGiArcSimple,
    const OdGeVector3d* pExtrusion = 0);
  
  /** Remarks:
      No-op.
  */
  void polylineProc(
    OdInt32 nbPoints, const OdGePoint3d* pVertexList,
    const OdGeVector3d* pNormal = 0,
    const OdGeVector3d* pExtrusion = 0, OdInt32 lBaseSubEntMarker = -1);
  
  /** Remarks:
      No-op.
  */
  void polygonProc(
    OdInt32 nbPoints, const OdGePoint3d* pVertexList,
    const OdGeVector3d* pNormal = 0, const OdGeVector3d* pExtrusion = 0);

  /** Remarks:
      No-op.
  */
  void meshProc(
    OdInt32 rows,
    OdInt32 columns,
    const OdGePoint3d* pVertexList,
    const OdGiEdgeData* pEdgeData = 0,
    const OdGiFaceData* pFaceData = 0,
    const OdGiVertexData* pVertexData = 0);
  
  /** Remarks:
      No-op.
  */
  void shellProc(
    OdInt32 nbVertex,
    const OdGePoint3d* pVertexList,
    OdInt32 faceListSize,
    const OdInt32* pFaceList,
    const OdGiEdgeData* pEdgeData = 0,
    const OdGiFaceData* pFaceData = 0,
    const OdGiVertexData* pVertexData = 0);
  
  /** Remarks:
      No-op.
  */
  void textProc(
    const OdGePoint3d& position,
    const OdGeVector3d& u, const OdGeVector3d& v,
    const OdChar* msg, OdInt32 length, bool raw, const OdGiTextStyle* pTextStyle,
    const OdGeVector3d* pExtrusion = 0);
  
  /** Remarks:
      No-op.
  */
  void shapeProc(
    const OdGePoint3d& position,
    const OdGeVector3d& u, const OdGeVector3d& v,
    int shapeNo, const OdGiTextStyle* pStyle,
    const OdGeVector3d* pExtrusion = 0);

  /** Remarks:
      No-op.
  */
  void xlineProc(
    const OdGePoint3d&, const OdGePoint3d&);
  
  /** Remarks:
      No-op.
  */
  void rayProc( const OdGePoint3d&, const OdGePoint3d&);
  
  /** Remarks:
      No-op.
  */
  void nurbsProc( const OdGeNurbCurve3d& nurbs);

  /** Remarks:
      No-op.
  */
  void ellipArcProc(
    const OdGeEllipArc3d& arc,
    const OdGePoint3d* pEndPointsOverrides = 0,
    OdGiArcType arcType = kOdGiArcSimple, const OdGeVector3d* pExtrusion = 0);

  /** Remarks:
      No-op.
  */
  void rasterImageProc(
    const OdGePoint3d& origin,
    const OdGeVector3d& u,
    const OdGeVector3d& v,
    const OdGiRasterImage* pImg, 
    const OdGePoint2d* uvBoundary,
    OdUInt32 numBoundPts,
    bool transparency = false,
    double brightness = 50.0,
    double contrast = 50.0,
    double fade = 0.0);

  /** Remarks:
      No-op.
  */
	void metafileProc(
    const OdGePoint3d& origin,
		const OdGeVector3d& u,
		const OdGeVector3d& v,
    const OdGiMetafile* pMetafile,
    bool bDcAligned = true,           
    bool bAllowClipping = false); 
};

#include "DD_PackPop.h"

#endif //#ifndef __ODGIEMPTYGEOMETRY_H__

