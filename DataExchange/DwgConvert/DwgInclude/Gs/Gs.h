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



#ifndef __GS_H_INCLUDED_
#define __GS_H_INCLUDED_


#include "RxObject.h"
#include "RxIterator.h"
#include "GsExport.h"

class OdGiDrawable;
typedef OdSmartPtr<OdGiDrawable> OdGiDrawablePtr;

class OdGeMatrix3d;
class OdGePoint3d;
class OdGeVector3d;
class OdGePoint2d;
class OdDbHostAppServices;
class OdGiEdgeData;
class OdGiFaceData;
class OdGiVertexData;
class OdGiVertexData;
class OdDbDatabase;
class OdDbObjectId;
class OdDbStub;
class OdDbViewport;
class OdGeExtents3d;

#include "DD_PackPush.h"

#include "OdPlatform.h"
#include "RxModule.h"

class OdGsSelectionReactor;

class OdGsView;
typedef OdSmartPtr<OdGsView> OdGsViewPtr;

class OdGsModel;
typedef OdSmartPtr<OdGsModel> OdGsModelPtr;

class OdGsDevice;
typedef OdSmartPtr<OdGsDevice> OdGsDevicePtr;

class OdRxDictionary;
typedef OdSmartPtr<OdRxDictionary> OdRxDictionaryPtr;

class OdGsReactor;
typedef OdSmartPtr<OdGsReactor> OdGsReactorPtr;


//****************************************************************************
// Helper classes
//****************************************************************************

// copied(with modification) from limits.h to avoid extra #includes
#define SCALAR_MIN    (-2147483647 - 1) // minimum(signed) int value
#define SCALAR_MAX       2147483647      // maximum(signed) int value

/** Description:

    {group:OdGs_Classes} 
*/
class OdGsDCPoint 
{
public:
  enum MaxFlag { Maximum };
  enum MinFlag { Minimum };
  
  OdGsDCPoint() { }
  OdGsDCPoint(long xin, long yin) : x(xin), y(yin) { }
  
  OdGsDCPoint(MaxFlag) { x = SCALAR_MAX; y = SCALAR_MAX; }
  OdGsDCPoint(MinFlag) { x = SCALAR_MIN; y = SCALAR_MIN; }
  
  void operator=(MaxFlag) { x = SCALAR_MAX; y = SCALAR_MAX; }
  void operator=(MinFlag) { x = SCALAR_MIN; y = SCALAR_MIN; }
  
  void operator=(const OdGsDCPoint& r) { x = r.x; y = r.y; }
  bool operator==(const OdGsDCPoint& r) const { return x == r.x && y == r.y; }
  bool operator!=(const OdGsDCPoint& r) const { return x != r.x || y != r.y; }
  
  long x;
  long y;
};

/** Description:

    {group:OdGs_Classes} 
*/
class OdGsDCRect 
{
public:
  enum NullFlag { Null };
  
  OdGsDCRect() { }
  OdGsDCRect(const OdGsDCPoint& min, const OdGsDCPoint& max) : m_min(min), m_max(max) { }
  OdGsDCRect(long l, long r, long b, long t) : m_min(l,b), m_max(r,t) { }
  OdGsDCRect(NullFlag) { set_null(); }
  
  OdGsDCRect & operator=(const OdGsDCRect& other)
  {
    m_min = other.m_min;
    m_max = other.m_max;
    return*this;
  }
  void operator|=(const OdGsDCRect& op)
  {
    if(m_min.x > op.m_min.x) 
      m_min.x = op.m_min.x; 
    if(m_max.x < op.m_max.x) 
      m_max.x = op.m_max.x;
    
    if(m_min.y > op.m_min.y)
      m_min.y = op.m_min.y;
    if(m_max.y < op.m_max.y)
      m_max.y = op.m_max.y;
  }
  void operator&=(const OdGsDCRect& op)
  {
    if(m_min.x < op.m_min.x)
      m_min.x = op.m_min.x; 
    if(m_max.x > op.m_max.x) 
      m_max.x = op.m_max.x;
    
    if(m_min.y < op.m_min.y)
      m_min.y = op.m_min.y;
    if(m_max.y > op.m_max.y)
      m_max.y = op.m_max.y;
    
    if(m_min.x > m_max.x || m_min.y > m_max.y)
     *this = Null;
  }
  bool operator==(const OdGsDCRect& op) const
  {
    return m_min == op.m_min && m_max == op.m_max; 
  }
  bool operator!=(const OdGsDCRect& op) const
  {
    return !(*this == op);
  }
  
  void set_null()
  { 
    m_min = OdGsDCPoint::Maximum; 
    m_max = OdGsDCPoint::Minimum; 
  } 
  
  bool is_null() const
  {// should be either valid or properly null
    ODA_ASSERT((m_min.x <= m_max.x && m_min.y <= m_max.y) ||
     (m_min == OdGsDCPoint::Maximum && m_max == OdGsDCPoint::Minimum));
    return m_min.x > m_max.x;
  }
  
  bool within(const OdGsDCRect& op) const
  {
    ODA_ASSERT(!is_null()); //(if*this is Null and op is not, does not work.
    return m_min.x >= op.m_min.x && // if*this is non-Null and right is Null, works.
      m_min.y >= op.m_min.y && // if both are Null, does not work.)
      m_max.x <= op.m_max.x &&
      m_max.y <= op.m_max.y;
  }
  
  OdGsDCPoint m_min;
  OdGsDCPoint m_max;
};


typedef void* OdGsWindowingSystemID; // i.e. -- HWND

class OdGiContext;

#include "Gi/GiExport.h"

/** Description:

    {group:OdGs_Classes} 
*/
class FIRSTDLL_EXPORT OdGsView : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdGsView);
  
  enum RenderMode
  {
    kBoundingBox = -1,          // Bounding box (when simpler than the geometry).
    k2DOptimized,               // Standard 2D display.
    kWireframe,                 // Same display as k2Doptimized (but using the 3D pipeline).
    kHiddenLine,                // Wireframe display with hidden lines removed.
    kFlatShaded,                // Faceted - constant color per face.
    kGouraudShaded,             // Smooth shaded - colors interpolated between vertices.
    kFlatShadedWithWireframe,   // Faceted with wireframe overlayed.
    kGouraudShadedWithWireframe // Smooth shaded with wireframe overlayed.
  };
  
  enum Projection
  {
    kParallel,
    kPerspective
  };

  virtual OdGiContext* userGiContext() const = 0;
  virtual void setUserGiContext(OdGiContext* pUserContext) = 0;

  virtual double lineweightToDcScale() const = 0;
  virtual void setLineweightToDcScale(double dMultiplier) = 0;
  virtual void setLineweightEnum(int nCount, const OdUInt8* lweights) = 0;

  // Viewport size & position in normalized device coords and screen coords
  //
  virtual void setViewport(const OdGePoint2d& lowerLeft, const OdGePoint2d& upperRight) = 0;
  virtual void setViewport(const OdGsDCRect& screen_rect) = 0;
  virtual void getViewport(OdGePoint2d& lowerLeft, OdGePoint2d& upperRight) const = 0;
  virtual void getViewport(OdGsDCRect& screen_rect) const = 0;
  
  
  // Non-Rectangular Viewports
  virtual void setViewportClipRegion(int contours, int const* counts, OdGsDCPoint const* points) = 0;
  virtual void removeViewportClipRegion() = 0;
  
  // Viewport Borders
  virtual void setViewportBorderProperties(ODCOLORREF color, int weight) = 0;
  virtual void getViewportBorderProperties(ODCOLORREF& color, int& weight) const = 0;
  
  virtual void setViewportBorderVisibility(bool bVisible) = 0;
  virtual bool isViewportBorderVisible() const = 0;
  
  // View transformation
  //
  virtual void setView(const OdGePoint3d & position,
    const OdGePoint3d& target,
    const OdGeVector3d& upVector,
    double fieldWidth,
    double fieldHeight,
    Projection projection = kParallel) = 0;
  
  virtual OdGePoint3d position() const = 0;
  virtual OdGePoint3d target() const = 0;
  virtual OdGeVector3d upVector() const = 0;

  /** Description:
      Returns the perspective lens length of this view.
  */
  virtual double lensLength() const = 0;

  /** Description:
      Sets the perspective lens length of this view.
  */
  virtual void setLensLength(double) = 0;

  virtual bool isPerspective() const = 0;
  virtual double fieldWidth() const = 0;
  virtual double fieldHeight() const = 0;
  
  // Clip Planes
  //
  virtual void setEnableFrontClip(bool enable) = 0;
  virtual bool isFrontClipped() const = 0;
  virtual void setFrontClip(double distance) = 0;
  virtual double frontClip() const = 0;
  
  virtual void setEnableBackClip(bool enable) = 0;
  virtual bool isBackClipped() const = 0;
  virtual void setBackClip(double distance) = 0;
  virtual double backClip() const = 0;

  /** Description:
      Returns a matrix that will transform world space to view space.

      See Also:
      Coordinate Systems.
  */
  virtual OdGeMatrix3d viewingMatrix() const = 0;

  /** Description: 
      Returns a matrix that will transform view space to normalized device space.

      See Also:
      Coordinate Systems.
  */
  virtual OdGeMatrix3d projectionMatrix() const = 0;

  /** Description: 
      Returns a matrix that will transform normalized device space to screen space.

      See Also:
      Coordinate Systems.
  */
  virtual OdGeMatrix3d screenMatrix() const = 0;

  /** Description: 
      Returns a matrix that will transform coordinates from world space to screen space. 
      This is equivalent to the concatenation of the viewingMatrix, 
      projectionMatrix, and screenMatrix.

      See Also:
      Coordinate Systems.
  */
  virtual OdGeMatrix3d worldToDeviceMatrix() const = 0;

  /** Description: 
      Returns a matrix that will transform coordinates from model space to screen space.

      See Also:
      Coordinate Systems.
  */
  virtual OdGeMatrix3d objectToDeviceMatrix() const = 0;
  
  // Render mode
  //
  virtual void setMode(RenderMode mode) = 0;
  virtual RenderMode mode() const = 0;
  
  // Drawables
  //
  virtual bool add(OdGiDrawable* drawable, OdGsModel* model) = 0;
  virtual bool erase(OdGiDrawable* drawable) = 0;
  virtual void eraseAll() = 0;
  
  // Validation
  //
  virtual void invalidate() = 0;
  virtual void invalidate(const OdGsDCRect &rect)= 0;
  virtual bool isValid() const = 0;
  
  // Updates
  //
  virtual void update() = 0;
  virtual void beginInteractivity(double fFrameRateInHz) = 0;
  virtual void endInteractivity() = 0;
  virtual void flush() = 0;
  
  // Visibility
  // 
  virtual void hide() = 0;
  virtual void show() = 0;
  virtual bool isVisible() = 0;
  
  // Viewport visibility of layers
  //
  virtual void freezeLayer(OdDbStub* layerID) = 0;
  virtual void thawLayer(OdDbStub* layerID) = 0;
  virtual void clearFrozenLayers() = 0;
  
  // Logical View Control
  //
  virtual void invalidateCachedViewportGeometry() = 0;
  
  // Selection(in device coords)
  //
  virtual void select(const OdGsDCPoint* pts, int nPoints, OdGsSelectionReactor* pReactor) = 0;

  // View-specific highlighting
  //
  //virtual bool highlight(const OdGsPath*) = 0;
  //virtual bool unhighlight(const OdGsPath*) = 0;
  
  // For client-friendly view manipulation

  //
  virtual void dolly(const OdGeVector3d& vector) = 0;
  virtual void dolly(double x, double y, double z) = 0;
  virtual void roll(double angle) = 0;
  virtual void orbit(double x, double y) = 0;
  virtual void zoom(double factor) = 0;
  virtual void pan(double x, double y) = 0;
  
  virtual OdGsViewPtr cloneView(bool bCloneViewParameters = true, bool bCloneGeometry = false) = 0;
  virtual void viewParameters(OdGsView* pRes) const = 0;
  
  // Viewing limits 
  //
  virtual bool exceededBounds() = 0;
  
  // Stereo
  virtual void enableStereo(bool bEnable) = 0;
  virtual bool isStereoEnabled() const = 0;
  virtual void setStereoParameters(double magnitude, double parallax) = 0;
  virtual void getStereoParameters(double& magnitude, double& parallax) const = 0;

  // Lights
  virtual void initLights(OdRxIterator* pLightsIter) = 0;

  virtual void setLinetypeScaleMultiplier(double dLinetypeScaleMultiplier) = 0;
};

//****************************************************************************
// OdGsDevice
//****************************************************************************

/** Description:

    {group:OdGs_Classes} 
*/
struct OdGsClientViewInfo 
{
  long      acadWindowId;      // returned via OdGiViewport::acadWindowId()
  OdDbStub* viewportObjectId;  // returned via OdGiViewportDraw::viewportObjectId()

  OdGsClientViewInfo()
    : acadWindowId(0)
    , viewportObjectId(NULL)
  {
  }
};

class OdGiContext;

/** Description:

    {group:OdGs_Classes} 
*/
class FIRSTDLL_EXPORT OdGsDevice : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdGsDevice);

  // Configuration 
  //
  virtual OdRxDictionaryPtr properties() = 0;

  virtual OdGiContext* userGiContext() const = 0;
  virtual void setUserGiContext(OdGiContext* pUserContext) = 0;

  // Validation 
  // 
  virtual void invalidate() = 0;
  virtual void invalidate(const OdGsDCRect &rect)= 0;
  virtual bool isValid() const = 0;
  
  // Updates 
  //
  // Pass in a rectangle to know which region on the device was updated by the GS
  virtual void update(OdGsDCRect* pUpdatedRect = NULL) = 0;
  
  // Change notification
  //
  virtual void onSize(const OdGsDCRect& outputRect) = 0;
  virtual void onRealizeForegroundPalette() = 0;
  virtual void onRealizeBackgroundPalette() = 0;
  virtual void onDisplayChange(int nBitsPerPixel, int nXRes, int nYRes) = 0;

  // View connections 
  //
  /** Description:
      Creates appropriate aggregated view. Returns pointer to newly created object.
  */
  virtual OdGsViewPtr createView(const OdGsClientViewInfo* pInfo = 0, bool bEnableLayerVisibilityPerView = false) = 0;
  virtual void addView(OdGsView* pView) = 0;
  virtual OdGsModelPtr createModel() = 0;
  virtual bool eraseView(OdGsView* view) = 0;
  virtual bool eraseView(int n) = 0;
  virtual void eraseAllViews() = 0;
  virtual int numViews() const = 0;
  virtual OdGsView* viewAt(int n) = 0;
  
  virtual bool setBackgroundColor(ODCOLORREF color) = 0;
  virtual ODCOLORREF getBackgroundColor() = 0;
  
  virtual void setLogicalPalette(const ODCOLORREF* palette, int nCount) = 0;
};

class OdGsModule;

/** Description:

    {group:OdGs_Classes} 
*/
class OdGsReactor : public OdRxObject
{
public:
  virtual void viewWasCreated(OdGsView* ) { }
  virtual void viewToBeDestroyed(OdGsView* ) { }
  virtual void gsToBeUnloaded(OdGsModule* ) { }
};


/** Description:

    {group:OdGs_Classes} 
*/
class FIRSTDLL_EXPORT OdGsModule : public OdRxModule
{
public:
  ODRX_DECLARE_MEMBERS(OdGsModule);

  virtual OdGsDevicePtr createDevice() = 0;

  virtual OdGsDevicePtr createBitmapDevice();

  virtual void addReactor(OdGsReactor* pReactor) = 0;
  virtual void removeReactor(OdGsReactor* pReactor) = 0;
};

typedef OdSmartPtr<OdGsModule> OdGsModulePtr;

#include "DD_PackPop.h"

#endif // __GS_H_INCLUDED_


