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



#ifndef _OD_DB_VIEWPORT_
#define _OD_DB_VIEWPORT_

#include "DD_PackPush.h"

#include "DbEntity.h"
#include "DbSymbolTable.h"
#include "ViewportDefs.h"

class OdGeExtents3d;
class OdGeMatrix3d;

/** Description:
    This class represents viewport (Viewport) entities in an OdDbDatabase instance.
  
    Library:
    Db
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbViewport : public OdDbEntity
{
protected:
  /* void dxfOutXData(OdDbDxfFiler* pFiler) const;
  */
public:
  ODDB_DECLARE_MEMBERS(OdDbViewport);

  OdDbViewport();

  /** Description:
    Returns the *height* of this Viewport entity (DXF 41).
  */
  double height() const;

  /** Description:
    Sets the *height* of this Viewport entity (DXF 41).
    Arguments:
    height (I) Height.
  */
  void setHeight(
    double height);

  /** Description:
    Returns the *width* of this Viewport entity (DXF 40).
  */
  double width() const;

  /** Description:
    Sets the *width* of this Viewport entity (DXF 40).
    Arguments:
    width (I) Width.
  */
  void setWidth(
    double width);

  /** Description:
    Returns the WCS center point of this Viewport entity (DXF 10).
  */
  OdGePoint3d centerPoint() const;

  /** Description:
    Sets the WCS center point of this Viewport entity (DXF 10).
    Arguments:
    centerPoint (I) Center point.
  */
  void setCenterPoint(
    const OdGePoint3d& centerPoint);

  /** Description:
    Returns the ID *number* of this Viewport entity.
    Remarks:
    Returns -1 if this Viewport entity is inactive.
  */
  OdInt16 number() const;

  /** Description:
    Returns true if and only if this Viewport entity is on (DXF 90, bit 0x20000).
  */
  bool isOn() const;

  /** Description:
    Turns on this Viewport entity (DXF 90, bit 0x20000).
  */
  void setOn();

  /** Description:
    Turns off this Viewport entity (DXF 90, bit 0x20000).
  */
  void setOff();

  /** Description:
    Returns the WCS view target of this Viewport entity (DXF 17).
  */
  OdGePoint3d viewTarget() const;

  /** Description:
    Sets the WCS view target of this Viewport entity (DXF 17).
    Arguments:
    viewTarget (I) View target.
  */
  void setViewTarget(
    const OdGePoint3d& viewTarget);

  /** Description:
    Returns the WCS view direction of this Viewport entity (DXF 16).
  */
  OdGeVector3d viewDirection() const;

  /** Description:
    Sets the WCS view direction of this Viewport entity (DXF 16).
    Arguments:
    viewDirection (I) View direction.
  */
  void setViewDirection(
    const OdGeVector3d& viewDirection);

  /** Description:
    Returns the DCS view *height* of this Viewport entity (DXF 45).
  */
  double viewHeight() const;

  /** Description:
    Sets the DCS view *height* of this Viewport entity (DXF 45).
    Arguments:
    viewHeight (I) View *height*.
  */
  void setViewHeight(
    double viewHeight);

  /** Description:
    Returns the DCS view center of this Viewport entity (DXF 12).
  */
  OdGePoint2d viewCenter() const;

  /** Description:
    Sets the DCS view center of this Viewport entity (DXF 12).
    Arguments:
    viewCenter (I) View center.
  */
  void setViewCenter(
    const OdGePoint2d& viewCenter);

  /** Description:
    Returns the DCS twist angle of this Viewport entity (DXF 51).
    
    Note:
    All angles are expressed in radians.
  */
  double twistAngle() const;

  /** Description:
    Sets the DCS twist angle of this Viewport entity (DXF 51).
    Arguments:
    twistAngle (I) Twist angle.
    Note:
    All angles are expressed in radians.
  */
  void setTwistAngle(
    double twistAngle);

  /** Description:
    Returns the perspective mode lens length of this Viewport entity (DXF 42).
  */
  double lensLength() const;

  /** Description:
    Sets the perspective mode lens length of this Viewport entity (DXF 42).
    Arguments:
    lensLength (I) Lens length.
  */
  void setLensLength(
    double lensLength);

  /** Description:
    Returns true if and only if front clipping is on for this Viewport entity (DXF 90, bit 0x02).
  */
  bool isFrontClipOn() const;

  /** Description:
    Turns on front clipping for this Viewport entity (DXF 90, bit 0x02).
  */
  void setFrontClipOn();

  /** Description:
    Turns off front clipping for this Viewport entity (DXF 90, bit 0x02).
  */
  void setFrontClipOff();

  /** Description:
    Returns true if and only if back clipping is on for this Viewport entity (DXF 90, bit 0x04).
  */
  bool isBackClipOn() const;

  /** Description:
    Turns on back clipping for this Viewport entity (DXF 90, bit 0x04).
  */
  void setBackClipOn();

  /** Description:
    Turns off back clipping for this Viewport entity (DXF 90, bit 0x04).
  */
  void setBackClipOff();

  /** Description:
    Returns true if and only if the front clipping plane passes trough the camera (DXF 90, bit 0x10).
    Remarks:
    If true, the front clipping plane ignores the front clip distance.
  */
  bool isFrontClipAtEyeOn() const;

  /** Description:
    Sets the front clipping plane to pass through the camera (DXF 90, bit 0x10).
    Remarks:
    The front clipping plane ignores the front clip distance.
  */
  void setFrontClipAtEyeOn();

  /** Description:
    Sets the front clipping plane to utilize the front clip distance (DXF 90, bit 0x10).
  */
  void setFrontClipAtEyeOff();

  /** Description:
      Returns the front clip distance of this Viewport entity (DXF 43).
  */
  double frontClipDistance() const;

  /** Description:
    Sets the front clip distance of this Viewport entity (DXF 43).
    Arguments:
    frontClipDistance (I) Front clip distance.
  */
  void setFrontClipDistance(
    double frontClipDistance);

  /** Description:
    Returns the back clip distance of this Viewport entity (DXF 44).
  */
  double backClipDistance() const;

  /** Description:
    Sets the back distance of this Viewport entity (DXF 44).
    Arguments:
    backClipDistance (I) Back clip distance.
  */
  void setBackClipDistance(
    double backClipDistance);

  /** Description:
     Returns true if and only if perspective is on for this Viewport entity (DXF 90, bit 0x01).
  */
  bool isPerspectiveOn() const;

  /** Description:
    Sets perspective on for this Viewport entity  (DXF 90, bit 0x01).
  */
  void setPerspectiveOn();

  /** Description:
    Sets perspective off for this Viewport entity (DXF 90, bit 0x01).
  */
  void setPerspectiveOff();

  /** Description:
    Returns true if and only if UCS follow is on for this Viewport entity  (DXF 90, bit 0x08).
  */
  bool isUcsFollowModeOn() const;

  /** Description:
    Sets UCS follow on for this Viewport entity (DXF 90, bit 0x08).
  */
  void setUcsFollowModeOn();

  /** Description:
    Sets UCS follow off for this Viewport entity (DXF 90, bit 0x08).
  */
	void setUcsFollowModeOff();

  /** Description:
    Returns true if and only if the UCS icon is visible for this Viewport entity (DXF 90, bit 0x20).
  */
  bool isUcsIconVisible() const;

  /** Description:
    Sets UCS icon visible on for this Viewport entity (DXF 90, bit 0x20).
  */
  void setUcsIconVisible();

  /** Description:
    Sets UCS icon visible false forthis Viewport entity (DXF 90, bit 0x20).
  */
  void setUcsIconInvisible();

  /** Description:
    Returns true if and only if the UCS icon is at the UCS orgin for this Viewport entity (DXF 90, bit 0x40).
  */
  bool isUcsIconAtOrigin() const;

  /** Description:
    Sets the UCS icon to the UCS orgin for this Viewport entity (DXF 90, bit 0x40).
  */
  void setUcsIconAtOrigin();

  /** Description:
    Sets the UCS icon to the cornder of this Viewport entity (DXF 90, bit 0x40).
  */
  void setUcsIconAtCorner();

  /** Description:
    Returns true if and only if fast zooms are on for this Viewport entity (DXF 90, bit 0x80).
  */
  bool isFastZoomOn() const;

  /** Description:
    Sets fast zooms on for this Viewport entity (DXF 90, bit 0x80).
  */
  void setFastZoomOn();

  /** Description:
    Sets fast zooms off for this Viewport entity (DXF 90, bit 0x80).
  */
  void setFastZoomOff();

  /** Description:
    Returns the circle zoom percent of this Viewport entity (DXF 72).
    Remarks:
    circleSides has a range of [1..20000]
  */
  OdUInt16 circleSides() const;

  /** Description:
    Sets the circle zoom percent of this Viewport entity (DXF 72).
    Arguments:
    circleSides (I) Circle zoom percent [1,20000].
  */
  void setCircleSides(
    OdUInt16 circleSides);

  /** Description:
    Returns true if and only if the snap mode is on for this Viewport entity (DXF 90, bit 0x100).
  */
  bool isSnapOn() const;

  /** Description:
    Sets the snap mode on for this Viewport entity (DXF 90, bit 0x100).
  */
  void setSnapOn();

  /** Description:
    Sets the snap mode off for this Viewport entity (DXF 90, bit 0x100).
  */
  void setSnapOff();

  /** Description:
    Returns true if and only if isometric snap style is on for this Viewport entity (DXF 90, bit 0x400).
  */
  bool isSnapIsometric() const;

  /** Description:
    Sets the isometric snap style on for this Viewport entity (DXF 90, bit 0x400).
  */
  void setSnapIsometric();

  /** Description:
    Sets the isometric snap style off for this Viewport entity (DXF 90, bit 0x400).
  */
  void setSnapStandard();

  /** Description:
    Returns the UCS snap angle of this Viewport entity (DXF 50).
  */
  double snapAngle() const;

  /** Description:
    Sets the UCS snap angle of this Viewport entity (DXF 50).
    Arguments:
    snapAngle (I) Snap angle.
  */
  void setSnapAngle(
    double snapAngle);

  /** Description:
    Returns the UCS snap base point of this Viewport entity (DXF 13).
  */
  OdGePoint2d snapBasePoint() const;

  /** Description:
    Sets the UCS snap base point of this Viewport entity (DXF 13).
    Arguments:
    snapBasePoint (I) Snap base point.
  */
  void setSnapBasePoint(
    const OdGePoint2d& snapBasePoint); 
  
  /** Description:
    Returns the snap increment of this Viewport entity (DXF 14).
  */
  OdGeVector2d snapIncrement() const;

  /** Description:
    Sets the snap increment of this Viewport entity (DXF 14).
    Arguments:
    snapIncrement (I) Snap increment.
  */
  void setSnapIncrement(
    const OdGeVector2d& snapIncrement);
  
  /** Description:
    Returns the snap IsoPair of this Viewport entity (DXF 14).
    Remarks:
    snapIsoPair will return one of the following:
    
    @table
    Value    Description
    0        Left isoplane
    1        Top isoplane
    2        Right isoplane
  */
  OdUInt16 snapIsoPair() const;

  /** Description:
    Sets the snap IsoPair of this Viewport entity (DXF 14).
    Arguments:
    snapIsoPair (I) Snap IsoPair.
    Remarks:
    snapIsoPair will be one of the following:
    
    @table
    Value    Description
    0        Left isoplane
    1        Top isoplane
    2        Right isoplane
  */
  void setSnapIsoPair(
    OdUInt16 snapIsoPair);
  
  /** Description:
    Returns true if and only if the grid is on for this Viewport entity (DXF 90, bit 0x200).
  */
  bool isGridOn() const;

  /** Description:
    Sets the the grid on for this Viewport entity (DXF 90, bit 0x200).
  */
  void setGridOn();

  /** Description:
    Sets the the grid off for this Viewport entity (DXF 90, bit 0x200).
  */
  void setGridOff();
  
  /** Description:
    Returns the grid increment of this Viewport entity (DXF 15).
  */
  OdGeVector2d gridIncrement() const;

  /** Description:
    Sets the grid increment of this Viewport entity (DXF 15).
    Arguments:
    gridIncrement (I) Grid increment.
  */
  void setGridIncrement(
    const OdGeVector2d& gridIncrement);
  
  /** Description:
    Returns true if and only if this Viewport entity will have hidden lines removed during plotting. (DXF 90, bit 0x800).
  */
  bool hiddenLinesRemoved() const;

  /** Description:
    Sets this Viewport entity to have hidden shown lines during plotting. (DXF 90, bit 0x800).  
  */
  void showHiddenLines();

  /** Description:
    Sets this Viewport entity to have hidden lines removed during plotting. (DXF 90, bit 0x800).  
  */
  void removeHiddenLines();
  
  /** Description:
    Freezes the sApecified layers in this Viewport entity (DXF 341)
    
    Arguments:
    layerIds (I) Object IDs of the layers to be frozen.

    Remarks:
    Other viewports are unaffected by this function.
  */
  void freezeLayersInViewport(
    const OdDbObjectIdArray& layerIds);

  /** Description:
    Thaws the specified layers in this Viewport entity (DXF 341)
    
    Arguments:
    layerIds (I) Object IDs of the layers to be thawed.

    Remarks:
    Other viewports are unaffected by this function.
  */
  void thawLayersInViewport(
    const OdDbObjectIdArray& layerIds);

  /** Description:
      Thaws all layers in this Viewport entity.
  */
  void thawAllLayersInViewport();

  /** Description:
    Returns true and only if the specified *layer* is frozen in this Viewport entity.
    Arguments:
    layerId (I) Layer ID of the *layer* to be queried.
  */
  bool isLayerFrozenInViewport(
    const OdDbObjectId& layerId) const;

  /** Description:
    Returns all layers that are frozen in this Viewport entity.
    Arguments:
    layerIds (O) Receives the object IDs of the frozen layers.
  */
  void getFrozenLayerList(
    OdDbObjectIdArray& layerIds) const;
  
  /** Description:
     Updates the display to reflect changes in this Viewport entity.
     
     Remarks:
     Closing this Viewport entity automatically calls this function. 
     
     Throws:
     eNotImplemented
  */
  void updateDisplay() const;
  
  
  virtual bool worldDraw(
    OdGiWorldDraw* pWd) const;
  
  /** Description:
    Returns true if and only if scale factor of this Viewport entity is locked (DXF 90, bit 0x4000).
  */
  bool isLocked() const;

  /** Description:
    Locks the scale factor of this Viewport entity (DXF 90, bit 0x4000).
  */
  void setLocked();

  /** Description:
    Unlocks the scale factor of this Viewport entity (DXF 90, bit 0x4000).
  */
  void setUnlocked();
  
  /** Description:
    Returns true if and only if this Viewport entity is transparent (DXF 90, bit 0x4000).
  */
  bool isTransparent() const;

  /** Description:
    Sets this Viewport entity transparent (DXF 90, bit 0x4000).
  */
  void setTransparent();

  /** Description:
    Sets this Viewport entity opaque (DXF 90, bit 0x4000).
  */
  void setOpaque();
  
  enum StandardScaleType
  {
    kScaleToFit,  // Scaled to Fit
    kCustomScale, // Scale is not a standard scale
    k1_1,         // 1:1
    k1_2,         // 1:2
    k1_4,         // 1:4
    k1_8,         // 1:8
    k1_10,        // 1:10
    k1_16,        // 1:16
    k1_20,        // 1:20
    k1_30,        // 1:30
    k1_40,        // 1:40
    k1_50,        // 1:50
    k1_100,       // 1:100
    k2_1,         // 2:1
    k4_1,         // 4:1
    k8_1,         // 8:1
    k10_1,        // 10:1
    k100_1,       // 100:1
    k1_128in_1ft, // 1/128"= 1'
    k1_64in_1ft,  // 1/64"= 1'
    k1_32in_1ft,  // 1/32"= 1'
    k1_16in_1ft,  // 1/16"= 1'
    k3_32in_1ft,  // 3/32"= 1'
    k1_8in_1ft,   // 1/8" = 1'
    k3_16in_1ft,  // 3/16"= 1'
    k1_4in_1ft,   // 1/4" = 1'
    k3_8in_1ft,   // 3/8" = 1'
    k1_2in_1ft,   // 1/2" = 1'
    k3_4in_1ft,   // 3/4" = 1'
    k1in_1ft,     // 1" = 1'
    k3in_1ft,     // 3" = 1'
    k6in_1ft,     // 6" = 1'
    k1ft_1ft      // 1' = 1'
  };
  
  enum ShadePlotType
  {
      kAsDisplayed,  // As displayed
      kWireframe,    // Wireframe
      kHidden,       // Hidden
      kRendered      // Rendered
  };

  /** Description:
    Returns the custom scale of this Viewport entity.
  */
  double customScale() const;

  /** Description:
    Sets the custom scale of this Viewport entity.
    Arguments:
    customScale (I) Custom scale.
  */
  void setCustomScale(
    double customScale);
  
  /** Description:
    Returns the standard scale type of this Viewport entity/
  */
  StandardScaleType standardScale() const;

  /** Description:
    Sets the standard scale type of this Viewport entity.
    Arguments:
    standardScale (I) Standard scale type.
  */
  void setStandardScale(
    const StandardScaleType standardScale);
  
  /** Description:
    Returns the name of the plot style sheet applied to objects in this Viewport entity (DXF 1).
  */
  OdString plotStyleSheet() const;

  /** Description:
    Returns the plot style sheet name associated with this Viewport entity.
  */
  OdString effectivePlotStyleSheet() const;

  /** Description:
   Sets the plot style sheet name associated with this Viewport entity.
  */
  void setPlotStyleSheet(
    const char* plotStyleSheetName);
  
  /** Description:
    Returns true if and only if non-rectangular clipping is enabled for this Viewport entity (DXF 90, bit 0x10000).
  */
  bool isNonRectClipOn() const;

  /** Description:
    Sets non-rectangular clipping on for this Viewport entity (DXF 90, bit 0x10000).
  */
  void setNonRectClipOn();

  /** Description:
    Sets non-rectangular clipping off for this Viewport entity (DXF 90, bit 0x10000).
  */
  void setNonRectClipOff();
  
  /** Description:
    Returns the Object ID of the clipping entity for this Viewport entity (DXF 340).
  */
  OdDbObjectId nonRectClipEntityId() const;

  /** Description:
    Sets the Object ID of the clipping entity for this Viewport entity (DXF 340).
    
    Arguments:
    clipEntityId (I) Object ID of the clipping entity.
    
    Remarks:
    The following entity types are acceptable clipping entities:

    @untitled table
    OdDb2dPolyline
    OdDb3dPolyline
    OdDbCircle
    OdDbEllipse
    OdDbFace
    OdDbPolyline
    OdDbRegion
    OdDbSpline
    
    Note:
    A clipping entity must be in the same PaperSpace as this viewport.
  */
  void setNonRectClipEntityId(
    OdDbObjectId clipEntityId);
  
  /*
     virtual void erased(const OdDbObject* , bool);
     virtual void modified(const OdDbObject *);
     virtual void copied(const OdDbObject* pDbObj,const OdDbObject* pNewObj);
     virtual void subObjModified(const OdDbObject* pDbObj, const OdDbObject* pSubObj);
  */
  
  /** Description:
    Returns the *origin*, X-axis, and Y-Axis of the UCS associated with this Viewport entity.

    Arguments:
    origin (O) Receives the UCS *origin* (DXF 110).
    xAxis (O) Receives the UCS X-axis (DXF 111).
    yAxis (O) Receives the UCS Y-axis (DXF 112).
  */
  void getUcs(OdGePoint3d& origin, 
    OdGeVector3d& xAxis, 
    OdGeVector3d& yAxis) const;

  /** Description:
    True if and only if the UCS associated with this Viewport entity
    is orthographic with respect to UCSBASE (DXF 79).

    Arguments:
    viewType (O) Receives the orthographic view type.

    Remarks:
    Returns the type of orthographic view.
    
    viewType will be one of the following:
    
    @table
    Name            Value   View type
    kNonOrthoView   0       Non-orthographic with respect to the UCS 
    kTopView        1       Top view with respect to the UCS 
    kBottomView     2       Bottom view with respect to the UCS 
    kFrontView      3       Front view with respect to the UCS 
    kBackView       4       Back view with respect to the UCS 
    kLeftView       5       Left view with respect to the UCS 
    kRightView      6       Right view with respect to the UCS 
  */
  bool isUcsOrthographic(
    OdDb::OrthographicView& viewType) const;

  /** Description:
    Returns the Object ID of the UCS associated with this Viewport entity (DXF 345 or 346).
  */
  OdDbObjectId ucsName() const;

  /** Description:
    Returns the *elevation* of the UCS plane of this entity (DXF 146).
    
    Remarks:
    The *elevation* is the *distance* from the WCS origin to the UCS plane of this entity.
  */
  double elevation() const;
  
  /** Description:
    Sets the UCS associated with this Viewport entity 

    Arguments:
    origin (I) The WCS *origin* of the UCS (DXF 110).
    xAxis (I) The WCS X-axis of the UCS (DXF 111).
    yAxis (I) The WCS Y-axis of the UCS(DXF 112).
    viewType (I) Orthographic view type (DXF 79).
    ucsId (I) Object ID of UCS (DXF 345 or 346).

    viewType will be one of the following:
    
    @table
    Name            Value   View type
    kNonOrthoView   0       Non-orthographic with respect to the UCS 
    kTopView        1       Top view with respect to the UCS 
    kBottomView     2       Bottom view with respect to the UCS 
    kFrontView      3       Front view with respect to the UCS 
    kBackView       4       Back view with respect to the UCS 
    kLeftView       5       Left view with respect to the UCS 
    kRightView      6       Right view with respect to the UCS 
  */
  void setUcs(
    const OdGePoint3d& origin, 
    const OdGeVector3d& xAxis, 
    const OdGeVector3d& yAxis);

  void setUcs(
    OdDb::OrthographicView viewType);

  void setUcs(
    const OdDbObjectId& ucsId);

  /** Description:
    Sets the UCS associated with this Viewport entity to the WCS. 
  */
  void setUcsToWorld();

  /** Description:
    Sets the *elevation* of the UCS plane of this Viewport entity (DXF 146).
    Arguments:
    elevation (I) Elevation.
    Remarks:
    The *elevation* is the *distance* from the WCS origin to the plane of this Viewport entity.
  */
  void setElevation(
    double elevation);
  
  /*
  bool isViewOrthographic(OdDb::OrthographicView& view ) const;

  void setViewDirection(OdDb::OrthographicView view );
  */
  
  /** Description:
    Returns true if and only if the UCS that is associated with this viewport will become active
    with the activation of this viewport.
  */
  bool isUcsSavedWithViewport() const;

  /** Description:
    Controls the activation of the UCS that is associated with this viewport 
    with the activation of this viewport.
    
    Arguments:
    ucsvp (I) Enables activation of UCS.
  */
  void setUcsPerViewport(
    bool ucsvp );
  
  /** Description:
    Set the render mode of this Viewport entity (DXF 281).
    Arguments:
    renderMode (I) Render mode.
    
    Remarks:
    renderMode determines how this Viewport entity is rendered on the screen.
  */
  void setRenderMode(
    OdDb::RenderMode renderMode);

  /** Description:
    Returns the render mode of this Viewport entity (DXF 281).

    Remarks:
    renderMode determines how this Viewport entity is rendered on the screen.
  */
  OdDb::RenderMode renderMode() const;

  /** Description:
    Returns the shade plot type of the current viewport. 

    Remarks:
    shadePlot determines how this Viewport entity will plot.
  */
  ShadePlotType shadePlot() const;

  /** Description:
    Set the shade plot type of this Viewport entity (DXF 281).
    Arguments:
    shadePlot (I) Shade plot type.
    Remarks:
    shadePlot determines how this Viewport entity will plot.
  */
  void setShadePlot(
    const ShadePlotType shadePlot);

  void setDatabaseDefaults(
    OdDbDatabase* pDb = NULL);

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult dxfInFields_R12(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields_R12(
    OdDbDxfFiler* pFiler) const;

  virtual void getClassID(
    void** pClsid) const;

  void subClose();

  OdResult subErase(
    bool erasing);

  /** Description:
    Adjusts the parameters of this Viewport entity such that the view is zoomed 
    to the extents of the drawing.
  */
  void zoomExtents();

  virtual OdResult getGeomExtents(
    OdGeExtents3d& extents) const;

  virtual OdResult transformBy(
  const OdGeMatrix3d& xfm);
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbViewport object pointers.
*/
typedef OdSmartPtr<OdDbViewport> OdDbViewportPtr;

#include "DD_PackPop.h"

#endif

