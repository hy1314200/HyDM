// GiWorldDrawDumper.cpp: implementation of the GiWorldDrawDumper class.
//
//////////////////////////////////////////////////////////////////////

#include "OdaCommon.h"
#include "ExProtocolExtension.h"
#include "GiWorldDrawDumper.h"

//----------------------------------------------------------
//
// OdGiWorldGeometryDumper
//
//----------------------------------------------------------
void OdGiWorldGeometryDumper::setExtents(const OdGePoint3d *pNewExtents)
{
  m_os << "      " << "setExtents()" << STD(endl);
}

void OdGiWorldGeometryDumper::pushModelTransform(const OdGeVector3d& vNormal)
{
  m_os << "      " << "pushModelTransform()" << STD(endl);
}

void OdGiWorldGeometryDumper::pushModelTransform(const OdGeMatrix3d& xMat)
{
  m_os << "      " << "pushModelTransform()" << STD(endl);
}

void OdGiWorldGeometryDumper::popModelTransform()
{
  m_os << "      " << "popModelTransform()" << STD(endl);
}

void OdGiWorldGeometryDumper::circle(const OdGePoint3d& center, double radius, const OdGeVector3d& normal)
{
  m_os << "      " << "circle()" << STD(endl);
}
  
void OdGiWorldGeometryDumper::circle(const OdGePoint3d&, const OdGePoint3d&, const OdGePoint3d&)
{
  m_os << "      " << "circle()" << STD(endl);
}

void OdGiWorldGeometryDumper::circularArc(const OdGePoint3d& center,
    double radius,
    const OdGeVector3d& normal,
    const OdGeVector3d& startVector,
    double sweepAngle,
    OdGiArcType arcType)
{
  m_os << "      " << "circularArc()" << STD(endl);
}

void OdGiWorldGeometryDumper::circularArc(const OdGePoint3d& start,
    const OdGePoint3d& point,
    const OdGePoint3d& end,
    OdGiArcType arcType)
{
  m_os << "      " << "circularArc()" << STD(endl);
}

void OdGiWorldGeometryDumper::polyline(OdInt32 nbPoints,
    const OdGePoint3d* pVertexList,
    const OdGeVector3d* pNormal,
    OdInt32 lBaseSubEntMarker)
{
  m_os << "      " << "polyline()" << STD(endl);
}

void OdGiWorldGeometryDumper::polygon(OdInt32 nbPoints, const OdGePoint3d* pVertexList)
{
  m_os << "      " << "polygon()" << STD(endl);
}

void OdGiWorldGeometryDumper::pline(const OdGiPolyline& lwBuf, OdUInt32 fromIndex, OdUInt32 numSegs)
{
  m_os << "      " << "pline()" << STD(endl);
}

void OdGiWorldGeometryDumper::mesh(OdInt32 rows,
    OdInt32 columns,
    const OdGePoint3d* pVertexList,
    const OdGiEdgeData* pEdgeData,
    const OdGiFaceData* pFaceData,
    const OdGiVertexData* pVertexData)
{
  m_os << "      " << "mesh()" << STD(endl);
}

void OdGiWorldGeometryDumper::shell(OdInt32 nbVertex,
    const OdGePoint3d* pVertexList,
    OdInt32 faceListSize,
    const OdInt32* pFaceList,
    const OdGiEdgeData* pEdgeData,
    const OdGiFaceData* pFaceData,
    const OdGiVertexData* pVertexData)
{
  m_os << "      " << "shell()" << STD(endl);
}

void OdGiWorldGeometryDumper::text(const OdGePoint3d& position,
    const OdGeVector3d& normal, const OdGeVector3d& direction,
    double height, double width, double oblique, const OdChar* msg)
{
  m_os << "      " << "text()" << STD(endl);
}

void OdGiWorldGeometryDumper::text(const OdGePoint3d& position,
    const OdGeVector3d& normal, const OdGeVector3d& direction,
    const OdChar* msg, OdInt32 length, bool raw, const OdGiTextStyle* pTextStyle)
{
  m_os << "      " << "text()" << STD(endl);
  m_os << "            " << msg << " len " << length << STD(endl);
}

void OdGiWorldGeometryDumper::xline(const OdGePoint3d&, const OdGePoint3d&)
{
  m_os << "      " << "xline()" << STD(endl);
}

void OdGiWorldGeometryDumper::ray(const OdGePoint3d&, const OdGePoint3d&)
{
  m_os << "      " << "ray()" << STD(endl);
}

void OdGiWorldGeometryDumper::pline(const OdDbPolyline& lwBuf, OdUInt32 fromIndex, OdUInt32 numSegs)
{
  m_os << "      " << "pline()" << STD(endl);
}

void OdGiWorldGeometryDumper::nurbs(const OdGeNurbCurve3d& nurbs)
{
  m_os << "      " << "nurbs()" << STD(endl);
}

void OdGiWorldGeometryDumper::pushClipBoundary(OdGiClipBoundary* pBoundary)
{
  m_os << "      " << "pushClipBoundary()" << STD(endl);
}

void OdGiWorldGeometryDumper::popClipBoundary()
{
  m_os << "      " << "popClipBoundary()" << STD(endl);
}

//----------------------------------------------------------
//
// OdGiSubEntityTraitsDumper
//
//----------------------------------------------------------
void OdGiSubEntityTraitsDumper::setColor(OdUInt16 color)
{
  m_os << "      " << "setColor()" << STD(endl);
}

void OdGiSubEntityTraitsDumper::setTrueColor(const OdCmEntityColor& cmColor)
{
  m_os << "      " << "setTrueColor()" << STD(endl);
}

void OdGiSubEntityTraitsDumper::setLayer(OdDbStub* layerId)
{
  m_os << "      " << "setLayer()" << STD(endl);
}

void OdGiSubEntityTraitsDumper::setLineType(OdDbStub* linetypeId)
{
  m_os << "      " << "setLineType()" << STD(endl);
}

void OdGiSubEntityTraitsDumper::setSelectionMarker(OdInt32 markerId)
{
  m_os << "      " << "setSelectionMarker()" << STD(endl);
}

void OdGiSubEntityTraitsDumper::setFillType(OdGiFillType fillType)
{
  m_os << "      " << "setFillType()" << STD(endl);
}

void OdGiSubEntityTraitsDumper::setLineWeight(OdDb::LineWeight lw)
{
  m_os << "      " << "setLineWeight()" << STD(endl);
}

void OdGiSubEntityTraitsDumper::setLineTypeScale(double dScale)
{
  m_os << "      " << "setLineTypeScale()" << STD(endl);
}

void OdGiSubEntityTraitsDumper::setThickness(double dThickness)
{
  m_os << "      " << "setThickness()" << STD(endl);
}

void OdGiSubEntityTraitsDumper::setPlotStyleName(OdDb::PlotStyleNameType, OdDbStub*)
{
  m_os << "      " << "setPlotStyleName()" << STD(endl);
}

void OdGiSubEntityTraitsDumper::setMaterial(OdDbStub* materialId)
{
  m_os << "      " << "setMaterial()" << STD(endl);
}

OdUInt16 OdGiSubEntityTraitsDumper::color() const
{
  return 0;
}

OdDbStub* OdGiSubEntityTraitsDumper::layer() const
{
  return NULL;
}

OdDbStub* OdGiSubEntityTraitsDumper::lineType() const
{
  return NULL;
}

OdGiFillType OdGiSubEntityTraitsDumper::fillType() const
{
  return OdGiFillType();
}

OdDb::LineWeight OdGiSubEntityTraitsDumper::lineWeight() const
{
  return OdDb::LineWeight(0);
}

double OdGiSubEntityTraitsDumper::lineTypeScale() const
{
  return 0;
}

double OdGiSubEntityTraitsDumper::thickness() const
{
  return 0;
}

OdDb::PlotStyleNameType OdGiSubEntityTraitsDumper::plotStyleNameType() const 
{
  return OdDb::kPlotStyleNameByLayer;
}

OdDbStub* OdGiSubEntityTraitsDumper::plotStyleNameId() const 
{
  return 0;
}

OdDbStub* OdGiSubEntityTraitsDumper::material() const
{
  return 0;
}

//----------------------------------------------------------
//
// OdGiWorldDrawDumper
//
//----------------------------------------------------------
OdGiWorldDrawDumper::OdGiWorldDrawDumper(STD(ostream) & os)
  : m_WdGeom(os)
  , m_Traits(os)
  , m_os(os)
{
}

OdGiWorldGeometry& OdGiWorldDrawDumper::geometry() const
{
	return (OdGiWorldGeometry&)m_WdGeom;
}

OdGiRegenType OdGiWorldDrawDumper::regenType() const
{
  return kOdGiForExplode;
}

bool OdGiWorldDrawDumper::regenAbort() const
{
  return false;
}

OdGiSubEntityTraits& OdGiWorldDrawDumper::subEntityTraits() const
{
  return (OdGiSubEntityTraits&)m_Traits;
}

OdGiGeometry& OdGiWorldDrawDumper::rawGeometry() const
{
  return (OdGiGeometry&)m_WdGeom;
}

bool OdGiWorldDrawDumper::isDragging() const
{
  return false;
}
  
double OdGiWorldDrawDumper::deviation(const OdGiDeviationType, const OdGePoint3d&) const
{
  return 0;
}

double OdGiWorldDrawDumper::modelDeviation(const OdGiDeviationType type, const OdGePoint3d& modelPoint) const
{
  return 0.0;
}

OdUInt32 OdGiWorldDrawDumper::numberOfIsolines() const
{
  return 1;
}
  
void OdGiWorldDrawDumper::setContext(OdGiContext* pUserContext)
{
  pCtx = pUserContext;
}

OdGiContext* OdGiWorldDrawDumper::context() const
{
  return pCtx;
}



