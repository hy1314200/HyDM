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



// GiBaseVectorizer.h: interface for the OdGiBaseVectorizer class.
//
//////////////////////////////////////////////////////////////////////

#ifndef _ODGIFASTEXTCALC_INCLUDED_
#define _ODGIFASTEXTCALC_INCLUDED_


#include "Gi/GiBaseVectorizer.h"
#include "OdStack.h"

#include "DD_PackPush.h"

/** Description:

    {group:OdGi_Classes}
*/
class ODGI_EXPORT ODRX_ABSTRACT OdGiFastExtCalc : public OdGiWorldDrawImpl
{
  struct Extents
  {
    Extents() : bWorldToModelValid(false) {}
    OdGeExtents3d         ext;
    OdGeMatrix3d          xModelToWorld;

    mutable OdGeMatrix3d  xWorldToModel;
    mutable bool          bWorldToModelValid;
  };
  OdGeExtents3d         m_worldExt;
  OdStack<Extents>      m_extStack;
  OdGeExtents3d*        m_pCurrExt;

  bool                  m_bSetExtentsCalled;


  void addTextExtents(
    const OdGePoint3d& locExtMin,
    const OdGePoint3d& locExtMax,
    const OdGePoint3d& position,
    const OdGeVector3d& normal,
    const OdGeVector3d& direction);
protected:
  ODRX_USING_HEAP_OPERATORS(OdGiWorldDrawImpl);

  OdGiFastExtCalc();
public:

  void resetExtents();
  void getExtents(OdGeExtents3d& exts) const;

  /////////////////////////////////////////////////////////////////////////////
  // OdGiCommonDraw Overrides

  bool regenAbort() const;
  double deviation(const OdGiDeviationType devType, const OdGePoint3d& worldPoint) const;
  OdGiRegenType regenType() const;

   /////////////////////////////////////////////////////////////////////////////
  // OdGiGeometry Overrides

  void circle(const OdGePoint3d& center, double radius, const OdGeVector3d& normal);

  void circle(const OdGePoint3d& pt1, const OdGePoint3d& pt2, const OdGePoint3d& pt3);

  void circularArc(const OdGePoint3d& center,
    double radius,
    const OdGeVector3d& normal,
    const OdGeVector3d& startVector,
    double sweepAngle,
    OdGiArcType arcType = kOdGiArcSimple);

  void circularArc(const OdGePoint3d& start,
    const OdGePoint3d& point,
    const OdGePoint3d& end,
    OdGiArcType arcType = kOdGiArcSimple);

  void polyline(OdInt32 nbPoints,
    const OdGePoint3d* pVertexList,
    const OdGeVector3d* pNormal = NULL,
    OdInt32 lBaseSubEntMarker = -1);

  void polygon(OdInt32 nbPoints, const OdGePoint3d* pVertexList);

  void pline(const OdGiPolyline& lwBuf, OdUInt32 fromIndex = 0, OdUInt32 numSegs = 0);

  void shape(const OdGePoint3d& position,
    const OdGeVector3d& normal, const OdGeVector3d& direction,
    int shapeNo, const OdGiTextStyle* pStyle);

  void text(const OdGePoint3d& position,
    const OdGeVector3d& normal, const OdGeVector3d& direction,
    double height, double width, double oblique, const OdChar* msg);

  void text(const OdGePoint3d& position,
    const OdGeVector3d& normal, const OdGeVector3d& direction,
    const OdChar* msg, OdInt32 length, bool raw, const OdGiTextStyle* pTextStyle);

  void xline(const OdGePoint3d&, const OdGePoint3d&);

  void ray(const OdGePoint3d&, const OdGePoint3d&);

  void nurbs(const OdGeNurbCurve3d& nurbs);

  void ellipArc(const OdGeEllipArc3d& arc,
    const OdGePoint3d* pEndPointsOverrides = 0,
    OdGiArcType arcType = kOdGiArcSimple);

  void mesh(OdInt32 rows,
    OdInt32 columns,
    const OdGePoint3d* pVertexList,
    const OdGiEdgeData* pEdgeData = NULL,
    const OdGiFaceData* pFaceData = NULL,
    const OdGiVertexData* pVertexData = NULL);

  void shell(OdInt32 nbVertex,
    const OdGePoint3d* pVertexList,
    OdInt32 faceListSize,
    const OdInt32* pFaceList,
    const OdGiEdgeData* pEdgeData = NULL,
    const OdGiFaceData* pFaceData = NULL,
    const OdGiVertexData* pVertexData = NULL);

  void worldLine(const OdGePoint3d pnts[2]);

  void setExtents(const OdGePoint3d *pNewExtents);

  void pushClipBoundary(OdGiClipBoundary* pBoundary);
  void popClipBoundary();

  void pushModelTransform(const OdGeMatrix3d& xMat);
  void pushModelTransform(const OdGeVector3d& vNormal);
  void popModelTransform();

  OdGeMatrix3d getModelToWorldTransform() const;
  OdGeMatrix3d getWorldToModelTransform() const;

  void draw(const OdGiDrawable* pDrawable);
};


#include "DD_PackPop.h"

#endif // #ifndef _ODGIFASTEXTCALC_INCLUDED_

