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



#ifndef _OD_DB_ABSTRACTVIEWPORTDATAFORDBVIEWPORT_
#define _OD_DB_ABSTRACTVIEWPORTDATAFORDBVIEWPORT_

#include "DD_PackPush.h"

#include "DbAbstractViewportData.h"
#include "ViewportDefs.h"
#include "DbStubArray.h"

/** Description:

    {group:OdDb_Classes}
*/
class OdDbAbstractViewportDataForDbViewport : public OdDbAbstractViewportData
{
public:
  OdGePoint2d lowerLeftCorner(const OdRxObject* pVpFrom) const;
  OdGePoint2d upperRightCorner(const OdRxObject* pVpFrom) const;
  void setViewport(OdRxObject* pVpTo, const OdGePoint2d& lowerLeft, const OdGePoint2d& upperRight) const;

  void setView(
      OdRxObject* pVpTo
    , const OdGePoint3d& target
    , const OdGeVector3d& direction
    , const OdGeVector3d& upVector
    , double fieldWidth
    , double fieldHeight
    , bool bPerspective
    , const OdGeVector2d& viewOffset = OdGeVector2d::kIdentity
    ) const;

  OdGePoint3d  target   (const OdRxObject* pVpFrom) const;
  OdGeVector3d direction(const OdRxObject* pVpFrom) const;
  OdGeVector3d upVector (const OdRxObject* pVpFrom) const;

  double fieldWidth (const OdRxObject* pVpFrom) const;
  double fieldHeight(const OdRxObject* pVpFrom) const;

  OdGeVector2d viewOffset(const OdRxObject* pVpFrom) const;

  bool isPerspective(const OdRxObject* pVpFrom) const;

  void setLensLength(OdRxObject* pVpTo, double dLensLength) const;
  double lensLength(const OdRxObject* pVpFrom) const;

  bool isFrontClipOn(const OdRxObject* pVpFrom) const;
  void setFrontClipOn(OdRxObject* pVpTo, bool bOn) const;

  bool isBackClipOn(const OdRxObject* pVpFrom) const;
  void setBackClipOn(OdRxObject* pVpTo, bool bOn) const;

  bool isFrontClipAtEyeOn(const OdRxObject* pVpFrom) const;
  void setFrontClipAtEyeOn(OdRxObject* pVpTo, bool bOn) const;

  double frontClipDistance(const OdRxObject* pVpFrom) const;
  void setFrontClipDistance(OdRxObject* pVpTo, double newVal) const;

  double backClipDistance(const OdRxObject* pVpFrom) const;
  void setBackClipDistance(OdRxObject* pVpTo, double newVal) const;

  void setRenderMode(OdRxObject* pVpTo, OdDb::RenderMode mode) const;
  OdDb::RenderMode renderMode(const OdRxObject* pVpFrom) const;

  void frozenLayers(const OdRxObject* pVp, OdDbObjectIdArray& ids) const;
  void setFrozenLayers(OdRxObject* pVp, const OdDbObjectIdArray& ids) const;

  bool hasUcs(const OdRxObject* ) const;
  void getUcs(const OdRxObject* pVpFrom, OdGePoint3d& origin, OdGeVector3d& xAxis, OdGeVector3d& yAxis) const;
  OdDb::OrthographicView orthoUcs(const OdRxObject* pVpFrom) const;
  OdDbObjectId ucsName(const OdRxObject* pVpFrom) const;
  double elevation(const OdRxObject* pVpFrom) const;
  void setUcs(OdRxObject* pVpTo, const OdGePoint3d& origin, const OdGeVector3d& xAxis, const OdGeVector3d& yAxis) const;
  bool setUcs(OdRxObject* pVpTo, OdDb::OrthographicView view) const;
  bool setUcs(OdRxObject* pVpTo, const OdDbObjectId& ucsId) const;
  void setUcsToWorld(OdRxObject* pVpTo) const;
  void setElevation(OdRxObject* pVpTo, double elev ) const;

  bool viewExtents(const OdRxObject* pVp, OdGeBoundBlock3d& extents) const;

  // OdDbAbstractViewportData

  bool isUcsSavedWithViewport(const OdRxObject* pVpFrom) const;
  void setUcsPerViewport( OdRxObject* pVpTo, bool ucsvp ) const;

  bool isUcsFollowModeOn(const OdRxObject* pVpFrom) const;
  void setUcsFollowModeOn(OdRxObject* pVpTo, bool bOn) const;

  OdUInt16 circleSides(const OdRxObject* pVpFrom) const;
  void setCircleSides(OdRxObject* pVpTo, OdUInt16) const;

  bool isGridOn(const OdRxObject* pVpFrom) const;
  void setGridOn(OdRxObject* pVpTo, bool bOn) const;

  OdGeVector2d gridIncrement(const OdRxObject* pVpFrom) const;
  void setGridIncrement(OdRxObject* pVpTo, const OdGeVector2d&) const;

  bool isUcsIconVisible(const OdRxObject* pVpFrom) const;
  void setUcsIconVisible(OdRxObject* pVpTo, bool bVisible) const;

  bool isUcsIconAtOrigin(const OdRxObject* pVpFrom) const;
  void setUcsIconAtOrigin(OdRxObject* pVpTo, bool bAtOrigin) const;

  bool isSnapOn(const OdRxObject* pVpFrom) const;
  void setSnapOn(OdRxObject* pVpTo, bool bOn) const;

  bool isSnapIsometric(const OdRxObject* pVpFrom) const;
  void setSnapIsometric(OdRxObject* pVpTo, bool bIsometric) const;

  double snapAngle(const OdRxObject* pVpFrom) const;
  void setSnapAngle(OdRxObject* pVpTo, double) const;

  OdGePoint2d snapBase(const OdRxObject* pVpFrom) const;
  void setSnapBase(OdRxObject* pVpTo, const OdGePoint2d&) const; 

  OdGeVector2d snapIncrement(const OdRxObject* pVpFrom) const;
  void setSnapIncrement(OdRxObject* pVpTo, const OdGeVector2d&) const;

  OdUInt16 snapIsoPair(const OdRxObject* pVpFrom) const;
  void setSnapIsoPair(OdRxObject* pVpTo, OdUInt16) const;

  DD_USING(OdDbAbstractViewportData::setView);
  DD_USING(OdDbAbstractViewportData::setUcs);
};

#include "DD_PackPop.h"

#endif

