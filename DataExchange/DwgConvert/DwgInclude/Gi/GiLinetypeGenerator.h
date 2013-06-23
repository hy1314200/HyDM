#ifndef _ODGILINETYPEGEN_INCLUDED_
#define _ODGILINETYPEGEN_INCLUDED_

#include "GiLinetype.h"
#include "Ge/GeVector3d.h"
class OdDbStub;

class TPoint;

/** Description:
    
    {group:OdGi_Classes} 
*/
template <class TPoint, class TVector>
struct OdGiLtpdSeg
{
  TVector        m_dir;
  double         m_len;
  const TPoint*  m_point1;
  const TPoint*  m_point2;

  inline void normalize() { 
    m_len = m_dir.length(); 
    if( m_len ) 
      m_dir /= m_len; 
  }

  inline void attachToPoints(const TPoint* points)
  {
    m_dir = points[1] - points[0];
    m_point1 = points;
    m_point2 = ++points;
    normalize();
  }
  inline void attachToPoints(const TPoint& point1, const TPoint& point2)
  {
    m_dir = point2 - point1;
    m_point1 = &point1;
    m_point2 = &point2;
    normalize();
  }
};

class OdGiLinetype;

template <class TVector> class OdGiLinetypeShapeRotator;

template<> class OdGiLinetypeShapeRotator<OdGeVector2d>
// class OdGiLinetypeShapeRotator<OdGeVector2d> // old way -- to use on old compilers 
{
public:
  inline void rotate(OdGeVector2d& vec, double ang) { vec.rotateBy(ang); }
  inline void rotateFromXAxis(OdGeVector2d& vec, double ang)
  { vec = OdGeVector2d::kXAxis; vec.rotateBy(ang); }
  inline void setNormal(const OdGeVector2d& ) { }
  inline OdGeVector2d perpVector(const OdGeVector2d& vec) { return vec.perpVector(); }
};

template<> class OdGiLinetypeShapeRotator<OdGeVector3d>
// class OdGiLinetypeShapeRotator<OdGeVector3d> // old way -- to use on old compilers 
{
  OdGeVector3d m_normal;
  OdGeVector3d m_ref;
public:
  inline OdGiLinetypeShapeRotator() {}
  inline void rotate(OdGeVector3d& vec, double ang) { vec.rotateBy(ang, m_normal); }
  inline void rotateFromXAxis(OdGeVector3d& vec, double ang)
  { vec = m_ref; vec.rotateBy(ang, m_normal); }
  inline void setNormal(const OdGeVector3d& normal) { m_normal = normal; m_ref = normal.perpVector(); }
  inline OdGeVector3d perpVector(const OdGeVector3d& vec) { return vec.perpVector(); }
};


/** Description:
    
    {group:OdGi_Classes} 
*/
template <class TSegTaker, class TPoint, class TVector, class TRotator = OdGiLinetypeShapeRotator<TVector> >
class OdGiLinetypeGenCtx
{
  typedef OdGiLtpdSeg<TPoint, TVector> TSegment;

  OdArray<TSegment, OdMemoryAllocator<TSegment> > m_segs;
  TRotator                                        m_rotator;
  double                                          m_totalLen;
  TSegTaker*                                      m_pSegTaker;
  const OdGiLinetype*                             m_pLinetype;

  inline double chooseMin()
  {
    double res = odmin(m_segLength, m_dashLength);
    m_segLength -= res;
    m_dashLength -= res;
    m_curPos += res;
    return res;
  }

  OdGiLinetypeDash  m_dash;
  double            m_diff;
  bool              m_bScaleToFit;
public:

  bool              m_bDashStarted;
  inline double ltypedPolyLength() const { return m_totalLen; }

  //////////////////////////////////////////////////////////////////////
  // context parameters varied during OdGiLinetype::generate() calls.
  double            m_scale;
  int               m_numPatts;
  int               m_numSegs;

  TPoint            m_curPt;
  int               m_dashIndex;
  int               m_segIndex;
  double            m_dashLength;
  double            m_segLength;
  double            m_curPos;

  //
  //////////////////////////////////////////////////////////////////////

  OdGiLinetypeGenCtx()
    : m_pSegTaker(0)
    , m_totalLen(0.0)
    , m_scale(1.0)
    , m_numPatts(0)
    , m_dashIndex(0)
    , m_segIndex(0)
    , m_dashLength(0.)
    , m_segLength(0.)
    , m_curPos(0.)
    , m_pLinetype(0)
    , m_bDashStarted(false)
  {}
  void setLinetype(const OdGiLinetype& ltp) { m_pLinetype = &ltp; }

  void setSegTaker(TSegTaker* pSegTaker) { m_pSegTaker = pSegTaker; }

  void setScale(double scale) { m_scale = scale; }

  // for 3d instances only
  void setNormal(const TVector& normal) { m_rotator.setNormal(normal); }

  bool isClosed() const
  {
    return (m_segs.last().m_point2 == m_segs.first().m_point1 || 
      m_segs.first().m_point1->isEqualTo(*m_segs.last().m_point2, 
      OdGeTol(OdGeContext::gZeroTol)));
  }

  void attachToPoints(OdUInt32 nPoints, const TPoint* points)
  {
    m_bDashStarted = false;
    m_totalLen = 0.0;
    m_segs.reserve(nPoints);
    m_segs.resize(nPoints-1);
    OdGiLtpdSeg<TPoint, TVector>* pSeg = m_segs.asArrayPtr();
    m_curPt = *points;
    m_segIndex = 0;
    while(--nPoints)
    {
      pSeg->attachToPoints(points);
      m_totalLen += pSeg->m_len;
      ++pSeg;
      ++points;
    }
  }

  inline void close()
  {
    if(!isClosed())
    {
      m_segs.append()->attachToPoints(*m_segs.last().m_point2, *m_segs.first().m_point1);
    }
  }

  void align(bool bScaleToFit)
  {
    m_bDashStarted = false;
    m_bScaleToFit = bScaleToFit;
    m_numPatts = 0;
    double pattLen = m_pLinetype->patternLength() * m_scale;
    if(OdNonZero(pattLen))
    {
      if(pattLen < m_totalLen)
      {
        double numPatts;
        m_segLength = m_segs.first().m_len;
        m_dashIndex = 0;
        m_pLinetype->dashAt(0, m_dash);
        if(bScaleToFit)
        {
          numPatts = ::floor(m_totalLen / pattLen + 0.5);
          m_scale *= (m_totalLen / (pattLen * numPatts));
          m_dashLength = m_dash.length * m_scale;
          m_diff = 0.0;
        }
        else
        {
          numPatts = ::floor(m_totalLen / pattLen);
          m_diff = m_totalLen - pattLen * numPatts;
          m_dashLength = (m_diff + m_dash.length * m_scale) / 2.0;
          if(m_dash.length==0.0) // the first is a dot.
          {
            m_dash.length = 0.000001; // to be positive (sign of dash)
          }
        }
        m_numPatts = int(numPatts);
      }
      else // generate continuous
      {
        m_numPatts = 1;
        m_dash.length = m_dashLength = pattLen;
      }
      if(m_numPatts > 10000)
      {
        m_numPatts = 1;
        m_dash.length = m_dashLength = m_totalLen * 2.;
      }
    }
  }

  const OdGiLtpdSeg<TPoint, TVector>* currSeg() const { return &m_segs[m_segIndex]; }

  void generateSegs(int nSegsToGen)
  {
    m_numSegs = nSegsToGen;
    ODA_ASSERT(m_numSegs);
    if(m_numPatts==0)
    {
      return ;
    }
    if(!m_bDashStarted)
    {
      m_bDashStarted = (m_dash.length > 0.0);
      if(m_bDashStarted)
      {
        m_pSegTaker->dashPt(m_curPt); // start dash
      }
    }
    bool bExit = false;
    const OdGiLtpdSeg<TPoint, TVector>* pSeg = currSeg();
    if(m_segLength <= 0.0)
    {
      m_segLength = currSeg()->m_len;
    }
    do
    {
      double minStep = chooseMin();
      if(m_dash.length > 0.0) // dash
      {
        if(m_segLength > 0.0)
        {
          m_curPt += (pSeg->m_dir * minStep);
        }
        else
        {
          m_curPt = *pSeg->m_point2;
        }
      }
      else if(m_dash.length < 0.0) // space
      {
        m_curPt += (pSeg->m_dir * minStep);
      }
      else // if(m_dash.length==0.0) // dot
      {
        m_pSegTaker->dot(m_curPt);
      }
      if(m_segLength == 0.0)
      {
        if(m_bDashStarted)
        {
          m_pSegTaker->dashPt(m_curPt);
        }
        ++pSeg;
        ++m_segIndex;
        if(--m_numSegs > 0)
        {
          m_segLength = pSeg->m_len;
        }
        else
        {
          bExit = true;
        }
      }
      if(m_dashLength == 0.0)
      {
        if(m_dash.isEmbeddedTextString() || m_dash.isEmbeddedShape())
        {
          TVector xdir = pSeg->m_dir;
          TVector ydir = m_rotator.perpVector(xdir);
          TVector offset = xdir * (m_dash.shapeOffset.x * m_scale)
                         + ydir * (m_dash.shapeOffset.y * m_scale);
          if(m_dash.isRotationAbsolute())
          {
            m_rotator.rotateFromXAxis(xdir, m_dash.shapeRotation);
          }
          else
          {
            m_rotator.rotate(xdir, m_dash.shapeRotation);
          }
          if(m_dash.isEmbeddedTextString())
          {
            m_pSegTaker->text(m_scale * m_dash.shapeScale, m_curPt + offset,
              xdir, m_dash.textString, m_dash.styleId);
          }
          else
          {
            m_pSegTaker->shape(m_scale * m_dash.shapeScale, m_curPt + offset,
              xdir, m_dash.shapeNumber, m_dash.styleId);
          }
        }
        ++m_dashIndex;
        if(m_dashIndex == m_pLinetype->numDashes())
        {
          m_dashIndex = 0;
          --m_numPatts;
          bExit = (m_numPatts==0);
        }
        m_pLinetype->dashAt(m_dashIndex, m_dash);
        m_dashLength = fabs(m_dash.length * m_scale);
        if(m_dash.length <= 0.0)
        {
          if(m_bDashStarted)
          {
            m_pSegTaker->dashPt(m_curPt);
            m_pSegTaker->endDash();
            m_bDashStarted = false;
          }
        }
        else
        {
          if(!m_bDashStarted && !bExit)
          {
            m_pSegTaker->dashPt(m_curPt);
            m_bDashStarted = true;
          }
        }
      }
    }
    while(!bExit);
  }

  void finishGeneration()
  {
    if(!m_bScaleToFit)
    {
      const OdGiLtpdSeg<TPoint, TVector>* pSeg = m_segs.getPtr() + m_segIndex;
      const OdGiLtpdSeg<TPoint, TVector>* pEndSeg = m_segs.getPtr() + m_segs.size();
      if(pSeg!=pEndSeg)
      {
        m_curPos += m_segLength;
        m_segLength = 0.0;
        m_curPt = *pSeg->m_point2;
        m_pSegTaker->dashPt(m_curPt);
        ++pSeg;
        while(pSeg!=pEndSeg)
        {
          m_curPos += pSeg->m_len;
          m_curPt = *pSeg->m_point2;
          m_pSegTaker->dashPt(m_curPt);
          ++pSeg;
        }
      }
    }
    else if(m_bDashStarted && m_segLength > 0.0)
    {
      m_segLength = 0.0;
      m_curPt = *currSeg()->m_point2;
      m_pSegTaker->dashPt(m_curPt);
    }
    if(m_bDashStarted)
    {
      m_pSegTaker->endDash();
      m_bDashStarted = false;
    }
  }

  // Complette linetype generation. Calls align(), generateSegs(), finish();
  void generate(bool bScaleToFit)
  {
    //ODA_ASSERT(m_pLinetype && !m_pLinetype->isContinuous()); // must be non-continuous
    align(bScaleToFit);
    generateSegs(m_segs.size());
    finishGeneration();
  }
};

/** Description:
    
    {group:OdGi_Classes} 
*/
template <class TPoint>
class OdGiDashConcatenator : public OdArray<TPoint, OdMemoryAllocator<TPoint> >
{
public:
  inline bool append(const TPoint& point)
  {
    if(this->empty() || !(this->last()-point).isZeroLength())
    {
      push_back(point);
      return true;
    }
    return false;
  }
};


#endif //_ODGILINETYPEGEN_INCLUDED_

