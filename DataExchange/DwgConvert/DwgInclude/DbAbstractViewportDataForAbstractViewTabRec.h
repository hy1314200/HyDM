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



#ifndef _ODDBABSTRACTVIEWPORTDATAFORABSTRACTVIEWTABLEREC_INCLUDED
#define _ODDBABSTRACTVIEWPORTDATAFORABSTRACTVIEWTABLEREC_INCLUDED

#include "DD_PackPush.h"
#include "DbAbstractViewportData.h"

/** Description:

    Library:
    Db

    {group:OdDb_Classes} 
*/
class OdDbAbstractViewportDataForAbstractViewTabRec : public OdDbAbstractViewportData
{
public:
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

  bool hasUcs(const OdRxObject* pVpFrom) const;
  void getUcs(const OdRxObject* pVpFrom, OdGePoint3d& origin, OdGeVector3d& xAxis, OdGeVector3d& yAxis) const;
  OdDb::OrthographicView orthoUcs(const OdRxObject* pVpFrom) const;
  double elevation(const OdRxObject* pVpFrom) const;
  void setUcs(OdRxObject* pVpTo, const OdGePoint3d& origin, const OdGeVector3d& xAxis, const OdGeVector3d& yAxis) const;
  bool setUcs(OdRxObject* pVpTo, OdDb::OrthographicView view) const;
  OdDbObjectId ucsName(const OdRxObject* pVpFrom) const;
  bool setUcs(OdRxObject* pVpTo, const OdDbObjectId& ucsId) const;
  void setElevation(OdRxObject* pVpTo, double elev ) const;

  DD_USING(OdDbAbstractViewportData::setView);
  DD_USING(OdDbAbstractViewportData::setUcs);
};

#include "DD_PackPop.h"

#endif // _ODDBABSTRACTVIEWPORTDATAFORABSTRACTVIEWTABLEREC_INCLUDED


