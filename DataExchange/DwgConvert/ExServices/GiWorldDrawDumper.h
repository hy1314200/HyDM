#ifndef _GI_WORLDDRAWDUMPER_H
#define _GI_WORLDDRAWDUMPER_H

#include "Gi/Gi.h"
#include "Gi/GiWorldDraw.h"
#include "Gi/GiWorldGeometry.h"
#include "Ge/GeMatrix3d.h"
#include "StaticRxObject.h"

class OdGiWorldGeometryDumper : public OdStaticRxObject<OdGiWorldGeometry>
{
  STD(ostream) & m_os;
public:
  OdGiWorldGeometryDumper(STD(ostream) & os) : m_os(os) {}

  void setExtents(const OdGePoint3d *pNewExtents);

  virtual OdGeMatrix3d getModelToWorldTransform() const { return OdGeMatrix3d(); }
  virtual OdGeMatrix3d getWorldToModelTransform() const { return OdGeMatrix3d(); }
  virtual void pushModelTransform(const OdGeVector3d& vNormal);
  virtual void pushModelTransform(const OdGeMatrix3d& xMat);
  virtual void popModelTransform();


  // Primitives
  virtual void circle(const OdGePoint3d& center, double radius, const OdGeVector3d& normal);
  
  virtual void circle(const OdGePoint3d&, const OdGePoint3d&, const OdGePoint3d&);
  
  virtual void circularArc(const OdGePoint3d& center,
    double radius,
    const OdGeVector3d& normal,
    const OdGeVector3d& startVector,
    double sweepAngle,
    OdGiArcType arcType = kOdGiArcSimple);
  
  virtual void circularArc(const OdGePoint3d& start,
    const OdGePoint3d& point,
    const OdGePoint3d& end,
    OdGiArcType arcType = kOdGiArcSimple);
  
  virtual void polyline(OdInt32 nbPoints,
    const OdGePoint3d* pVertexList,
    const OdGeVector3d* pNormal = NULL,
    OdInt32 lBaseSubEntMarker = -1);
  
  virtual void polygon(OdInt32 nbPoints, const OdGePoint3d* pVertexList);
  
  virtual void pline(const OdGiPolyline& lwBuf, OdUInt32 fromIndex = 0, OdUInt32 numSegs = 0);

  virtual void mesh(OdInt32 rows,
    OdInt32 columns,
    const OdGePoint3d* pVertexList,
    const OdGiEdgeData* pEdgeData = NULL,
    const OdGiFaceData* pFaceData = NULL,
    const OdGiVertexData* pVertexData = NULL);
  
  virtual void shell(OdInt32 nbVertex,
    const OdGePoint3d* pVertexList,
    OdInt32 faceListSize,
    const OdInt32* pFaceList,
    const OdGiEdgeData* pEdgeData = NULL,
    const OdGiFaceData* pFaceData = NULL,
    const OdGiVertexData* pVertexData = NULL);
  
  virtual void text(const OdGePoint3d& position,
    const OdGeVector3d& normal, const OdGeVector3d& direction,
    double height, double width, double oblique, const OdChar* msg);
  
  virtual void text(const OdGePoint3d& position,
    const OdGeVector3d& normal, const OdGeVector3d& direction,
    const OdChar* msg, OdInt32 length, bool raw, const OdGiTextStyle* pTextStyle);
  
  virtual void xline(const OdGePoint3d&, const OdGePoint3d&);
  
  virtual void ray(const OdGePoint3d&, const OdGePoint3d&);
  
  virtual void pline(const OdDbPolyline& lwBuf, OdUInt32 fromIndex = 0, OdUInt32 numSegs = 0);

  virtual void nurbs(const OdGeNurbCurve3d& nurbs);
  
  virtual void pushClipBoundary(OdGiClipBoundary* pBoundary);
  virtual void popClipBoundary();
  virtual void draw(const OdGiDrawable*) {}
  virtual void worldLine(const OdGePoint3d pnts[2]) {}
  virtual void body(const char *body, void **pCacheData = NULL) {}

  virtual void ellipArc(const OdGeEllipArc3d& center,
    const OdGePoint3d* pEndPointsOverrides = 0,
    OdGiArcType arcType = kOdGiArcSimple) {}

  virtual void ellipArc(const OdGePoint3d& center,
    const OdGeVector3d& majorRadius,
    const OdGeVector3d& minorRadius,
    const OdGePoint3d& startPoint,
    const OdGePoint3d& endPoint,
    OdGiArcType arcType = kOdGiArcSimple) {}
};

class OdGiSubEntityTraitsDumper : public OdStaticRxObject<OdGiSubEntityTraits>
{
  STD(ostream) & m_os;
public:
  OdGiSubEntityTraitsDumper(STD(ostream) & os) : m_os(os) {}

  void setColor(OdUInt16 color);
  void setTrueColor(const OdCmEntityColor& cmColor);
  void setLayer(OdDbStub* layerId);
  void setLineType(OdDbStub* linetypeId);
  void setSelectionMarker(OdInt32 markerId);
  void setFillType(OdGiFillType fillType);
  void setLineWeight(OdDb::LineWeight lw);
  void setLineTypeScale(double dScale = 1.0);
  void setThickness(double dThickness);
  void setPlotStyleName(OdDb::PlotStyleNameType, OdDbStub* = 0);
  void setMaterial(OdDbStub* materialId);
  
  OdUInt16 color() const;
  OdCmEntityColor trueColor() const { return OdCmEntityColor(); }
  OdDbStub* layer() const;
  OdDbStub* lineType() const;
  OdGiFillType fillType() const;
  OdDb::LineWeight lineWeight() const;
  double lineTypeScale() const;
  double thickness() const;
  OdDb::PlotStyleNameType plotStyleNameType() const;
  OdDbStub* plotStyleNameId() const;
  OdDbStub* material() const;
};

class OdGiContextDumper : public OdStaticRxObject<OdGiContext>
{
  OdDbDatabase* m_pDb;
public:
  OdGiContextDumper(OdDbDatabase* pDb) : m_pDb(pDb) {}
  OdDbDatabase* database() const { return m_pDb; }
  OdGiDrawablePtr openDrawable(OdDbStub*) { return (OdGiDrawable*)0; }
};

class OdGiWorldDrawDumper : public OdStaticRxObject<OdGiWorldDraw>
{
  OdGiWorldGeometryDumper m_WdGeom;
  OdGiSubEntityTraitsDumper m_Traits;
  OdGiContext* pCtx;
  STD(ostream) & m_os;
public:
	OdGiWorldDrawDumper(STD(ostream) & os);

	OdGiWorldGeometry& geometry() const;
  OdGiRegenType regenType() const;
  bool regenAbort() const;
  OdGiSubEntityTraits& subEntityTraits() const;
  OdGiGeometry& rawGeometry() const;
  bool isDragging() const;
  double deviation(const OdGiDeviationType, const OdGePoint3d&) const;
  double modelDeviation(const OdGiDeviationType type, const OdGePoint3d& modelPoint) const;
  OdUInt32 numberOfIsolines() const;
  void setContext(OdGiContext* pUserContext);
  OdGiContext* context() const;
};

#endif

