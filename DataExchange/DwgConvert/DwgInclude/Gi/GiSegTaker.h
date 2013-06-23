#ifndef _ODGISEGTAKER_INCLUDED_
#define _ODGISEGTAKER_INCLUDED_

#include "Int32Array.h"
#include "Ge/GePoint3d.h"
#include "Ge/GeVector3d.h"
#include "Ge/GeMatrix3d.h"
#include "Gi/GiLinetype.h"
#include "Gi/GiTextStyle.h"
#include "Gi/GiLinetypeGenerator.h"
#include "Gi/GiDrawable.h"
#include "Gi/GiNonEntityTraits.h"

/////////////////////////////////////////////////////////////////////////////////
static inline void buildTape(OdUInt32 nPoints, OdInt32Array& faceList)
{
  OdUInt32 n = nPoints / 2;
  ODA_ASSERT(n*2==nPoints);
  faceList.resize((n-1) * 5);
  OdInt32 *faces = faceList.asArrayPtr();
  for (OdUInt32 i=0; i < n-1; ++i)
  {
    *faces++ = 4;
    *faces++ = i+1;
    *faces++ = i;
    *faces++ = n+i;
    *faces++ = n+i+1;
  }
}

static inline void buildFrontFace(OdUInt32 nPoints, OdInt32Array& faceList)
{
  faceList.resize(nPoints + 1);
  OdInt32 *face = faceList.asArrayPtr();
  *face++ = nPoints;
  for (OdUInt32 i=0; i < nPoints; ++i)
  {
    *face++ = i;
  }
}

static inline void buildSolid(OdUInt32 nPoints, OdInt32Array& faceList)
{
  OdUInt32 n = nPoints / 2;
  ODA_ASSERT(n*2==nPoints);
  faceList.resize(n*5 + (n+1)*2);
  OdInt32 *frontFace = faceList.asArrayPtr();
  OdInt32 *backFace = frontFace + (n+1);
  *frontFace = *backFace = n;
  OdInt32 *sideFaces = backFace + (n+1);
  OdUInt32 i;
  for (i=0; i < n-1; ++i)
  {
    *sideFaces++ = 4;
    *sideFaces++ = i+1;
    *sideFaces++ = i;
    *sideFaces++ = n+i;
    *sideFaces++ = n+i+1;

    *++frontFace = i;
    *++backFace = nPoints-1-i;
  }
  *++frontFace = i;
  *++backFace = nPoints-1-i;

  *sideFaces++ = 4;
  *sideFaces++ = 0;
  *sideFaces++ = n-1;
  *sideFaces++ = nPoints-1;
  *sideFaces++ = n;
}

static inline void dd_extend(OdGePoint3dArray& points, const OdGeVector3d& widthDir, const double* widths)
{
  ODA_ASSERT(points.size()<3);
  OdGeVector3d halfWidthDir = widthDir;
  halfWidthDir /= 2.0;
  points[0] += (halfWidthDir * widths[0]);
  if(points.size()==2)
  {
    points[1] += (halfWidthDir * widths[1]);
    points.append(points[1] - widthDir * widths[1]);
  }
  points.append(points[0] - widthDir * widths[0]);
}

static inline void dd_extend(OdGePoint3dArray& points, const OdGeVector3d& widthDir)
{
  ODA_ASSERT(points.size()<3);
  OdGeVector3d halfWidthDir = widthDir;
  halfWidthDir /= 2.0;
  points[0] += halfWidthDir;
  if(points.size()==2)
  {
    points[1] += halfWidthDir;
    points.append(points[1] - widthDir);
  }
  points.append(points[0] - widthDir);
}

// halfWidthToRadiusAspect = WIDTH / RADIUS / 2.0
static inline void dd_extend(OdGePoint3dArray& points, const OdGePoint3d& center, double halfWidthToRadiusAspect)
{
  OdUInt32 n = points.size(), N = n * 2 - 1;
  points.resize(n * 2);
  OdGePoint3d* pts = points.asArrayPtr();
  OdGeVector3d halfWidthDir;
  for (OdUInt32 i=0; i < n; ++i)
  {
    halfWidthDir = pts[i] - center;
    halfWidthDir *= halfWidthToRadiusAspect;

    pts[i] += halfWidthDir;
    pts[N-i] = pts[i] - halfWidthDir * 2.0;
  }
}

// halfWidthToRadiusAspect = WIDTH / RADIUS / 2.0
static inline void dd_extend(OdGePoint3dArray& points, const OdGePoint3d& center, double half1ToRadiusAspect, const double* widths)
{
  OdUInt32 n = points.size(), N = n * 2 - 1;
  points.resize(n * 2);
  OdGePoint3d* pts = points.asArrayPtr();
  OdGeVector3d halfWidthDir;
  for (OdUInt32 i=0; i < n; ++i)
  {
    halfWidthDir = pts[i] - center;
    halfWidthDir *= half1ToRadiusAspect;
    halfWidthDir *= widths[i];

    pts[i] += halfWidthDir;
    pts[N-i] = pts[i] - halfWidthDir * 2.0;
  }
}

inline void extrude(OdGePoint3dArray& points, const OdGeVector3d& vExtrusion)
{
  OdUInt32 n = points.size();
  points.resize(n * 2);
  OdGePoint3d* pts = points.asArrayPtr();
  for (OdUInt32 i = 0; i < n; ++i)
  {
    pts[n] = *pts;
    *pts += vExtrusion;
    ++pts;
  }
}

//inline bool giStyle(OdGiContext* pCtx, double height, OdGiTextStyleTraits* pStyle, OdDbStub* styleId)
//{
//  OdGiDrawablePtr pDrw = pCtx->openDrawable(styleId);
//  if (!pDrw.isNull())
//  {
//    pDrw->setAttributes(pStyle);
//    double h = pStyle->textSize();
//    if(OdNonZero(h))
//    {
//      height *= h;
//    }
//    pStyle->setTextSize(height);
//    return true;
//  }
//  return false;
//}

////////////////////////////////////////////////////////////////////////////

/** Description:

    {group:Other_Classes}
*/
//class SegTaker
//{
//  SegTaker& operator = (const SegTaker&);
//  typedef OdGiLinetypeGenCtx<OdGePoint3d, OdGeVector3d, SegTaker> CTX;
//
//  OdInt32Array                      m_faceList;
//  OdGiDashConcatenator<OdGePoint3d> m_points;
//  OdGiCommonDraw*                   m_pDraw;
//  OdGiContext*                      m_pCtx;
//  OdGiGeometry&                     m_geom;
//  const OdGeVector3d*               m_pNormal;
//  double                            m_thickness;
//
//public:
//  SegTaker(OdGiCommonDraw* pDraw, OdGiGeometry* pGeom, OdGiContext* pCtx, const OdGeVector3d* pNormal, double thickness)
//    : m_pDraw(pDraw)
//    , m_pCtx(pCtx)
//    , m_geom(*pGeom)
//    , m_pNormal(pNormal)
//    , m_thickness(thickness)
//  { }
//  
//  inline void tape(OdInt32 nbPoints, const OdGePoint3d* pVertexList)
//  {
//    m_points.reserve(nbPoints * 2);
//    m_points.insert(m_points.end(), pVertexList, pVertexList+nbPoints);
//    endDash();
//  }
//  inline void endDash()
//  {
//    if(m_points.size() > 1)
//    {
//      ::extrude(m_points, *m_pNormal * m_thickness);
//      ::buildTape(m_points.size(), m_faceList);
//      m_geom.shell(m_points.size(), m_points.getPtr(), m_faceList.size(), m_faceList.getPtr());
//      m_points.resize(0);
//    }
//  }
//  void dashPt(const OdGePoint3d& point) { m_points.append(point); }
//  void dot(const OdGePoint3d& point)
//  {
//    m_points.clear();
//    m_points.append(point);
//    ::extrude(m_points, *m_pNormal * m_thickness);
//    m_geom.polyline(2, m_points.getPtr());
//  }
//  void text(double height, const OdGePoint3d& point, const OdGeVector3d& dir, const OdString& str, OdDbStub* styleId)
//  {
//    OdGiTextStyle style;
//    if(::giStyle(m_pCtx, height, style, styleId))
//    {
//      m_geom.text(point, *m_pNormal, dir, str, str.getLength(), true, &style);
//    }
//  }
//  void shape(const OdGePoint3d& position,
//            const OdGeVector3d& normal, const OdGeVector3d& direction,
//            int shapeNo, const OdGiTextStyle* pStyle)
//  {
//    OdGeMatrix3d xForm;
//    xForm.setCoordSystem(position, direction, normal.crossProduct(direction), normal);
//    m_geom.pushModelTransform(xForm); // Text entity's xForm
//    OdGePoint3d pos;
//    m_pCtx->drawShape(m_pDraw, pos, shapeNo, pStyle);
//    m_geom.popModelTransform();
//  }
//
//  void shape(double height, const OdGePoint3d& point, const OdGeVector3d& dir,
//            int shapeNo, OdDbStub* styleId)
//  {
//    OdGiTextStyle style;
//    if(::giStyle(m_pCtx, height, style, styleId))
//    {
//      shape(point, *m_pNormal, dir, shapeNo, &style);
//    }
//  }
//};


#endif //_ODGISEGTAKER_INCLUDED_

