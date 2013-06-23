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



#ifndef _ODDBABSTRACTVIEWPORTDATAFORDBVIEWTABLERECORD_INCLUDED
#define _ODDBABSTRACTVIEWPORTDATAFORDBVIEWTABLERECORD_INCLUDED

#include "DD_PackPush.h"
#include "DbAbstractViewportDataForAbstractViewTabRec.h"

/** Description:

    {group:OdDb_Classes}
*/
class OdDbAbstractViewportDataForDbViewTabRec : public OdDbAbstractViewportDataForAbstractViewTabRec
{
public:

  // OdAbstractViewPE

  OdGePoint2d lowerLeftCorner(const OdRxObject* pVpFrom) const;
  OdGePoint2d upperRightCorner(const OdRxObject* pVpFrom) const;
  void setViewport(OdRxObject* pVpTo, const OdGePoint2d& lowerLeft, const OdGePoint2d& upperRight) const;
  bool hasUcs(const OdRxObject* pVpFrom) const;

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
};

#include "DD_PackPop.h"

#endif // _ODDBABSTRACTVIEWPORTDATAFORDBVIEWTABLERECORD_INCLUDED


