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

#ifndef _ODGIBASEVECTORIZER_INCLUDED_
#define _ODGIBASEVECTORIZER_INCLUDED_


#include "Gi/GiViewportDraw.h"
#include "Gi/GiWorldDraw.h"
#include "Gs/Gs.h"
#include "Ge/GeExtents3d.h"
#include "Gi/GiViewport.h"
#include "Gi/GiXform.h"
#include "Gi/GiModelToViewProc.h"
#include "Gi/GiConveyorEntryPoint.h"
#include "Gi/GiConveyorConnector.h"
#include "Gi/GiDeviation.h"
#include "Ge/GePlane.h"
#include "Gi/GiLinetyper.h"
#include "Gi/GiOrthoClipper.h"
#include "Gi/GiExtAccum.h"
#include "Gi/GiSubEntityTraitsData.h"
#include "Gi/GiTextStyle.h"

class OdGiBaseVectorizer;


class OdDbStub;

#include "DD_PackPush.h"

/** Description:
    This class provides an implementation of the OdGiWorldDraw::geometry() function.

    Library:
    Gi
    
    {group:OdGi_Classes}
*/
class ODGI_EXPORT ODRX_ABSTRACT OdGiWorldDraw_ : public OdGiWorldDraw
                                               , public OdGiWorldGeometry
{
protected:
  ODRX_USING_HEAP_OPERATORS(OdGiWorldDraw);
public:
  OdGiWorldGeometry& geometry() const;
};


/** Description:
    This class provides an implementation of the OdGiWorldDraw::geometry() function.

    Library:
    Gi

    {group:OdGi_Classes}
*/
class ODGI_EXPORT ODRX_ABSTRACT OdGiWorldDrawImpl : public OdGiWorldDraw_
                                                  , public OdGiSubEntityTraits
{
protected:
  ODRX_USING_HEAP_OPERATORS(OdGiWorldDraw_);

  OdGiSubEntityTraitsData m_entityTraitsData;
  OdGiContext*            m_pContext;

  OdGiWorldDrawImpl();
public:
  /** Description:
    Sets the OdGiContext instance associated with this object.

    Arguments:
    pUserContext (I) Pointer to the user *context*.
  */
  virtual void setContext(
  OdGiContext* pUserContext);

  OdUInt16 color() const;
  OdCmEntityColor trueColor() const;
  OdDbStub* layer() const;
  OdDbStub* lineType() const;
  OdGiFillType fillType() const;
  OdDb::LineWeight lineWeight() const;
  double lineTypeScale() const;
  double thickness() const;
  OdDb::PlotStyleNameType plotStyleNameType() const;
  OdDbStub* plotStyleNameId() const;
  OdDbStub* material() const;

  void setTrueColor(
    const OdCmEntityColor& color);
  void setPlotStyleName(
    OdDb::PlotStyleNameType plotStyleNameType, OdDbStub* plotStyleId = 0);
  void setColor(
    OdUInt16 color);
  void setLayer(
    OdDbStub* layerId);
  void setLineType(
    OdDbStub* lineTypeId);
  void setFillType(
    OdGiFillType fillType);
  void setLineWeight(
    OdDb::LineWeight lineWeight);
  void setLineTypeScale(
    double lineTypeScale);
  void setThickness(
    double thickness);
  void setSelectionMarker(
    OdInt32 marker);
  void setMaterial(
    OdDbStub* materialId);


  /////////////////////////////////////////////////////////////////////////////
  // OdGiCommonDraw Overrides

  OdGiContext* context() const;

  bool isDragging() const;

  OdGiSubEntityTraits& subEntityTraits() const;

  OdGiGeometry& rawGeometry() const;

  OdUInt32 numberOfIsolines() const;
};


/** Description:
    Provides an implementation of the OdGiViewportDraw::geometry() function.

    {group:OdGi_Classes}
*/
class ODGI_EXPORT ODRX_ABSTRACT OdGiViewportDraw_ : public OdGiViewportDraw
                                                  , public OdGiViewportGeometry
{
protected:
  ODRX_USING_HEAP_OPERATORS(OdGiViewportDraw);
public:
  OdGiViewportGeometry& geometry() const;
};


/** Description:

    Remarks:
    Can be used for worldDraw() only.

    {group:OdGi_Classes}
*/
class ODGI_EXPORT OdGiBaseVectorizer : public OdGiWorldDrawImpl
                                     , public OdGiViewportDraw_
                                     , public OdGiConveyorContext
                                     , public OdGiDeviation
{
public:
  class LayerTraits : public OdGiLayerTraits
  {
    OdUInt32                m_flags;
    OdDb::LineWeight        m_lineweight;
    OdCmEntityColor         m_color;
    OdDbStub*               m_linetypeId;
    OdDb::PlotStyleNameType m_plotStyleNameType;
    OdDbStub*               m_plotStyleNameId;
  public:
    LayerTraits();
    LayerTraits(const LayerTraits&);
    void operator =(const LayerTraits&);
    void addRef();
    void release();

    OdUInt32 flags() const { return m_flags; };
    void setFlags(OdUInt32 flags) { m_flags = flags; };
    OdCmEntityColor color() const;
    OdDb::LineWeight lineweight() const;
    OdDbStub* linetype() const;
    OdDb::PlotStyleNameType plotStyleNameType() const;
    OdDbStub* plotStyleNameId() const;

    void setColor(const OdCmEntityColor& cl);
    void setLineweight(OdDb::LineWeight lw);
    void setLinetype(OdDbStub* id);
    void setPlotStyleName(OdDb::PlotStyleNameType, OdDbStub* = 0);
    void set(OdGiDrawable* pLayer);
  };
protected:
  ODRX_HEAP_OPERATORS();

  mutable OdGiSubEntityTraitsData m_effectiveEntityTraitsData;
  mutable OdDbStub*               m_layerId;
  mutable LayerTraits             m_layerTraitsData;
  mutable OdDbStub*               m_effectiveLayerId;
  OdInt32                         m_nSelectionMarker;

  void updateLayerTraits(OdGiSubEntityTraitsData& subEntityTraits) const;
  bool isEntityTraitsDataChanged() const { return m_bEntityTraitsDataChanged; }
  virtual void setEntityTraitsDataChanged() const { m_bEntityTraitsDataChanged = true; }
  void clearEntityTraitsDataChanged() const { m_bEntityTraitsDataChanged = false; }
  bool effectivelyVisible() const;

  /////////////////////////////////////////////////////////////////////////////
  // OdGiConveyorContext Overrides

  OdGiContext& giContext() const;
  const OdGiDrawableDesc* currentDrawableDesc() const;
  const OdGiDrawable* currentDrawable() const;
  const OdGiViewport* giViewport() const;
  const OdGsView* gsView() const;

  /////////////////////////////////////////////////////////////////////////////

private:
  // Data Members

  mutable bool                    m_bEntityTraitsDataChanged;
  OdGiConveyorEntryPoint          m_dcEntryPoint;
  OdGiTextStyle                   m_textStyle;
  OdGePlane                       m_primitivePlane;
  OdGeVector3d                    m_extrusion;
  OdGiXformPtr                    m_pDcInputToOutput;
protected:
  OdGiConveyorConnector           m_eyeEntryPoint;

  const OdGeVector3d* extrusion(const OdGePoint3d& start, const OdGePoint3d& point, const OdGePoint3d& end);
  const OdGeVector3d* extrusion(const OdGeVector3d& normal);
  const OdGeVector3d* extrusion(const OdGeVector3d* pNormal)
  {
    if(pNormal)
      return extrusion(*pNormal);
    return 0;
  }
  const OdGeVector3d* extrusion(const OdGePoint3d& orig, const OdGeVector3d& u, const OdGeVector3d& v);

  OdGiConveyorConnector           m_modelEntryPoint;
  OdGiConveyorConnector*          m_pActiveEntryPoint;

  OdGiModelToViewProcPtr          m_pModelToEyeProc;
  OdGiConveyorConnector           m_output;

  OdGiDrawableDesc*               m_pDrawableDesc;

  enum Flags
  {
    kFirstFlag            = 1,

    kSuppressViewportDraw = kFirstFlag << 0,
    kDrawInvisibleEnts    = kFirstFlag << 1,
    kDrawLayerOff         = kFirstFlag << 2,
    kDrawLayerFrozen      = kFirstFlag << 3,
    kIgnoreFillPlane      = kFirstFlag << 4,

    kLastFlag             = kIgnoreFillPlane
  };
  mutable OdUInt32                m_flags;

  OdGiSubEntityTraitsData*        m_pByBlock;

  /** Description:
  */
  virtual void affectTraits(const OdGiSubEntityTraitsData* pFrom, OdGiSubEntityTraitsData& to) const;

  /** Description:
  */
  virtual double linetypeGenerationCriteria() const;

  /////////////////////////////////////////////////////////////////////////////
  // OdGiConveyorContext Override

  virtual const OdGiSubEntityTraitsData& effectiveTraits() const;

  /////////////////////////////////////////////////////////////////////////////

  /** Remarks:
      This class is not an OdRx-interface so it can't be queried.
  */
  static OdRxClass* desc() { return ::OdRxObject::desc(); }

public:
  OdGiBaseVectorizer();
  virtual ~OdGiBaseVectorizer();

  /** Remarks:
      Returns OdRxObject::desc().
  */
  OdRxClass* isA() const;

  /** Remarks:
      Any of base OdRxObject-based classes can be queried.
  */
  OdRxObject* queryX(const OdRxClass* pClass) const;

  virtual OdGiConveyorOutput& output();
  void setEyeToOutputTransform(const OdGeMatrix3d& x);
  void eyeToOutputTransform(OdGeMatrix3d& x) const;
  const OdGeMatrix3d& eyeToOutputTransform() const;

  /** Description:
      Returns the OdGiConveyorContext associated with this object.

      Remarks:
      Since OdGiConveyorContext is a parent class, returns "this".
  */
  OdGiConveyorContext* drawContext() { return this; }

  const OdGiConveyorContext* drawContext() const { return this; }

  /** Description:
  */
  virtual void beginViewVectorization();

  /** Description:
  */
  virtual void endViewVectorization();

  /////////////////////////////////////////////////////////////////////////////
  // OdGiSubEntityTraits Overrides

  void setTrueColor(const OdCmEntityColor& trueColor);
  void setPlotStyleNameType(OdDb::PlotStyleNameType plotStyleNameType);
  void setPlotStyleNameId(OdDbStub* plotStyleNameId);
  void setColor(OdUInt16 color);
  void setLayer(OdDbStub* layerId);
  void setLineType(OdDbStub* lineTypeId);
  void setFillType(OdGiFillType fillType);
  void setLineWeight(OdDb::LineWeight lineWeight);
  void setLineTypeScale(double lineTypeScale);
  void setThickness(double thickness);

  void setPlotStyleName(OdDb::PlotStyleNameType, OdDbStub* = 0);
  void setSelectionMarker(OdInt32 markerId);
  void setMaterial(OdDbStub* materialId);

  /////////////////////////////////////////////////////////////////////////////


  /////////////////////////////////////////////////////////////////////////////
  // OdGiCommonDraw Overrides

  OdGiContext* context() const;
  bool regenAbort() const;
  OdGiSubEntityTraits& subEntityTraits() const;
  double deviation(const OdGiDeviationType , const OdGePoint3d& ) const;
  OdGiRegenType regenType() const;
  OdUInt32 numberOfIsolines() const;
  OdGiGeometry& rawGeometry() const;
  bool isDragging() const;

  /////////////////////////////////////////////////////////////////////////////


  /////////////////////////////////////////////////////////////////////////////
  // OdGiViewportDraw Override
  //

  /** Remarks:
      This implemenation always returns 0.
  */
  OdUInt32  sequenceNumber() const;

  /** Remarks:
      This implemenation always returns false.
  */
  bool      isValidId(const OdUInt32 acgiId) const;

  /** Remarks:
      This implemenation always returns 0.
  */
  OdDbStub* viewportObjectId() const;

  OdGiViewport& viewport() const;

  /////////////////////////////////////////////////////////////////////////////


  /////////////////////////////////////////////////////////////////////////////
  // OdGiGeometry Overrides

  void circle(const OdGePoint3d& center, double radius, const OdGeVector3d& normal);

  void circle(const OdGePoint3d&, const OdGePoint3d&, const OdGePoint3d&);

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

  void pushClipBoundary(OdGiClipBoundary* pBoundary);
  void popClipBoundary();

  void pushModelTransform(const OdGeMatrix3d& xMat);
  void pushModelTransform(const OdGeVector3d& vNormal);
  void popModelTransform();

  OdGeMatrix3d getModelToWorldTransform() const;
  OdGeMatrix3d getWorldToModelTransform() const;

  void draw(const OdGiDrawable* pDrawable);

  /////////////////////////////////////////////////////////////////////////////

  /** Description:
      Called by draw() to setup drawable traits and get drawable's flags
      (via call pDrawable->setAttributes(this)).
  */
  virtual OdUInt32 setAttributes(const OdGiDrawable* pDrawable);

  /** Description:
      Called by draw() to test if drawable is visible and so needs to be drawn.
  */
  bool needDraw(OdUInt32 nDrawableFlags);

  /** Description:
      Called by draw() after setAttributes() either to perform drawable vectorization
      (via call pDrawable->worldDraw(this) or/and pDrawable->viewportDraw(this)) or
      to draw drawable's cache (if one exists and is valid).
  */
  virtual bool doDraw(OdUInt32 nDrawableFlags, const OdGiDrawable* pDrawable);

  /////////////////////////////////////////////////////////////////////////////
  // OdGiWorldGeometry Override

  void setExtents(const OdGePoint3d *pNewExtents);

  /////////////////////////////////////////////////////////////////////////////


  /////////////////////////////////////////////////////////////////////////////
  // OdGiViewportGeometry

  void rasterImageDc(
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

  void metafileDc(
    const OdGePoint3d& origin,
    const OdGeVector3d& u,
    const OdGeVector3d& v,
    const OdGiMetafile* pMetafile,
    bool bDcAligned = true,
    bool bAllowClipping = false);

  void polylineEye(OdUInt32 nbPoints, const OdGePoint3d* pPoints);
  void polygonEye(OdUInt32 nbPoints, const OdGePoint3d* pPoints);

  void polylineDc(OdUInt32 nbPoints, const OdGePoint3d* pPoints);
  void polygonDc(OdUInt32 nbPoints, const OdGePoint3d* pPoints);


  /////////////////////////////////////////////////////////////////////////////
  // OdGiConveyorContext Overrides

  virtual void onTraitsModified();

  /////////////////////////////////////////////////////////////////////////////

};

inline bool OdGiBaseVectorizer::effectivelyVisible() const
{
  const OdGiSubEntityTraitsData& traits = effectiveTraits();
  return (
    (GETBIT(m_flags, kDrawLayerOff   ) || !traits.isLayerOff()) &&
    (GETBIT(m_flags, kDrawLayerFrozen) || !traits.isLayerFrozen())
  );
}

/** Description:

    {group:OdGi_Classes}
*/
class ODGI_EXPORT OdGiExtCalc : public OdGiBaseVectorizer
{
protected:
  bool            m_BBoxSet;
  OdGiExtAccumPtr m_pExtAccum;

public:

  OdGiExtCalc();
  virtual ~OdGiExtCalc();

  OdGiRegenType regenType() const;
  bool regenAbort() const;
  void draw(const OdGiDrawable*);
  void setExtents(const OdGePoint3d *pNewExtents);
  void resetExtents();

  void getExtents(OdGeExtents3d& exts) const;

  void setContext(OdGiContext* pUserContext);

  // optimization
  const OdGiSubEntityTraitsData& effectiveTraits() const;
  void affectTraits(const OdGiSubEntityTraitsData* pFrom, OdGiSubEntityTraitsData& to) const;
};

#include "DD_PackPop.h"

#endif // #ifndef _ODGIBASEVECTORIZER_INCLUDED_

