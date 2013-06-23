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



#ifndef _ODDBABSTRACTVIEWLTABLERECORD_INCLUDED
#define _ODDBABSTRACTVIEWLTABLERECORD_INCLUDED

#include "DD_PackPush.h"

#include "DbSymbolTableRecord.h"
#include "DbAbstractViewportData.h"
#include "ViewportDefs.h"

/** Description:
    This class is the base class for OdDbViewTableRecord and OdDbViewportTableRecord.

    Library:
    Db
    
    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDbAbstractViewTableRecord : public OdDbSymbolTableRecord
{
public:

  ODDB_DECLARE_MEMBERS(OdDbAbstractViewTableRecord);

  OdDbAbstractViewTableRecord();

  /** Description:
    Returns the DSC center point of this View. 
    Remarks:
    o  DXF 10 for OdDbViewTableRecord.
    o  DXF 12 for OdDbViewportTableRecord.
  */
  OdGePoint2d centerPoint() const;

  /** Description:
    Sets the DCS center point of this View.
    Arguments:
    centerPoint (I) Center *point*.
    Remarks:
    o  DXF 10 for OdDbViewTableRecord.
    o  DXF 12 for OdDbViewportTableRecord.
  */
  void setCenterPoint(
    const OdGePoint2d& centerPoint);

  /** Description:
    Returns the DCS *height* of this View (DXF 40).
  */
  double height() const;

  /** Description:
    Sets the DCS *height* of this View (DXF 40).
    Arguments:
    height (I) Height.
  */
  void setHeight(double height);

  /** Description:
    Returns the DCS *width* of this View (DXF 41).
    
    Remarks:
    DXF 41 contains the *width* : *height* ratio for OdDbViewportTableRecord.
  */
  double width() const;

  /** Description:
    Sets the DCS *width* of this View (DXF 41).

    Arguments:
    width (I) Width. 

    Remarks:
    DXF 41 contains the *width* : *height* ratio for OdDbViewportTableRecord.
  */
  void setWidth(
    double width);

   /** Description:
    Returns the WCS view *target* of this View.
    
    Remarks:
    o  DXF 12 for OdDbViewTableRecord. 
    o  DXF 17 for OdDbViewportTableRecord.    
  */
  OdGePoint3d target() const;

  /** Description:
    Sets the WCS view *target* of this View.
    Arguments:
    target (I) Target.
    Remarks:
    o  DXF 12 for OdDbViewTableRecord.
    o  DXF 17 for OdDbViewportTableRecord.    
  */
  void setTarget(
    const OdGePoint3d& target);

  /** Description:
    Returns the WCS view direction of this View.
    Arguments:
    viewDirection (I) View direction.

    Remarks:
    o  DXF 11 for OdDbViewTableRecord. 
    o  DXF 16 for OdDbViewportTableRecord.    
  */
  OdGeVector3d viewDirection() const;

  /** Description:
    Sets the WCS view direction of this View.
    Arguments:
    viewDirection (I) View direction.

    Remarks:
    o  DXF 12 for OdDbViewTableRecord. 
    o  DXF 17 for OdDbViewportTableRecord.    
  */
 void setViewDirection(
  const OdGeVector3d& viewDirection);

  /** Description:
    Returns the DCS twist angle of this View.

    Remarks:
    o  DXF 50 for OdDbViewTableRecord 
    o  DXF 51 for OdDbViewportTableRecord.    
    
    Note:
    All angles are expressed in radians.
  */
  double viewTwist() const;

  /** Description:
    Sets the DCS twist angle of this View.

    Remarks:
    o  DXF 50 for OdDbViewTableRecord 
    o  DXF 51 for OdDbViewportTableRecord.    
    
    Arguments:
    viewTwist (I) View twist angle.
    Note:
    All angles are expressed in radians.
  */
  void setViewTwist(
    double viewTwist);

  /** Description:
    Returns the perspective mode lens length of this View (DXF 42).
  */
  double lensLength() const;

  /** Description:
    Sets the perspective mode lens length of this View (DXF 42).
    Arguments:
    lensLength (I) Lens length.
  */
  void setLensLength(
    double lensLength);

  /** Description:
      Returns the front clip distance of this View (DXF 43).
  */
  double frontClipDistance() const;

  /** Description:
    Sets the front clip distance of this View (DXF 43).
    Arguments:
    frontClipDistance (I) Front clip distance.
  */
  void setFrontClipDistance(
    double frontClipDistance);

  /** Description:
    Returns the back clip distance of this View (DXF 44).
  */
  double backClipDistance() const;

  /** Description:
    Sets the back distance of this View (DXF 44).
    Arguments:
    backClipDistance (I) Back clip distance.
  */
  void setBackClipDistance(
    double backClipDistance);

  /** Description:
     Returns true if and only if perspective is on for this View (DXF 70, bit 0x01).
  */
  bool perspectiveEnabled() const;

  /** Description:
    Controls perspective mode for this view (DXF 71, bit 0x01).

    Arguments:
    perspectiveEnabled (I) Perspective Enabled.
  */
  void setPerspectiveEnabled(
    bool perspectiveEnabled);

  /** Description:
    Returns true if and only if front clipping is enabled for this View (DXF 71, bit 0x02).
  */
  bool frontClipEnabled() const;

  /** Description:
    Controls front clipping for this view (DXF 71, bit 0x02).

    Arguments:
    frontClipEnabled (I) Front clipping enabled.
  */
  void setFrontClipEnabled(
    bool frontClipEnabled);

  /** Description:
    Returns true if and only if back clipping is enabled for this View (DXF 71, bit 0x04).
  */
  bool backClipEnabled() const;

  /** Description:
    Controls back clipping for this view (DXF 71, bit 0x04).

    Arguments:
    backClipEnabled (I) Back clipping enabled.     
  */
  void setBackClipEnabled(
    bool backClipEnabled);

  /** Description:
    Returns true if and only if the front clipping plane plane passes through the camera (DXF 71, bit 0x10).
    Remarks:
    If true, the front clipping plane ignores the front clip distance.
  */
  bool frontClipAtEye() const;

  /** Description:
    Controls the front clipping plane passing through the camera (DXF 71, bit 0x10).

    Arguments:
    atEye (I) True to pass through camera, false to use front clip distance.
 */
  void setFrontClipAtEye(
    bool atEye);

  /** Description:
    Sets the render mode of this View (DXF 281).
    Arguments:
    renderMode (I) Render mode.
    
    Remarks:
    renderMode determines how this Viewport entity is rendered on the screen.
  */
  void setRenderMode(OdDb::RenderMode renderMode);

  /** Description:
    Returns the render mode of this View (DXF 281).
    
    Remarks:
    renderMode determines how this Viewport entity is rendered on the screen.
  */
  OdDb::RenderMode renderMode() const;

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
    True if and only if the UCS associated with this View is
    orthographic with respect to UCSBASE (DXF 79).

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
    Returns the Object ID of the UCS associated with this View (DXF 345).
  */
  OdDbObjectId ucsName() const;

  /** Description:
    Returns the *elevation* of the UCS plane of this View (DXF 146).
    
    Remarks:
    The *elevation* is the *distance* from the WCS origin to the UCS plane of this View.
  */
  double elevation() const;

  /** Description:
    Sets the UCS associated with this Viewport entity 

    Arguments:
    origin (I) The UCS *origin* (DXF 110).
    xAxis (I) The UCS X-axis (DXF 111).
    yAxis (I) The UCS Y-axis (DXF 112).
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
    Sets the UCS associated with this View to the WCS. 
  */
  void setUcsToWorld();

  /** Description:
    Sets the *elevation* of the UCS plane of this View (DXF 146).
    Arguments:
    elevation (I) Elevation.
    Remarks:
    The *elevation* is the *distance* from the WCS origin to the plane of this View.
  */
  void setElevation(
    double elevation);
  
  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual void copyFrom (
    const OdRxObject* pSource);

  /*
      bool isViewOrthographic(OdDb::OrthographicView& view) const;
      void setViewDirection(OdDb::OrthographicView view);
  */

};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbViewportTableRecord object pointers.
*/
typedef OdSmartPtr<OdDbAbstractViewTableRecord> OdDbAbstractViewTableRecordPtr;

#include "DD_PackPop.h"

#endif // _ODDBABSTRACTVIEWLTABLERECORD_INCLUDED


