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



#ifndef OD_DBABSTRACTVIEWPORTDATA_H
#define OD_DBABSTRACTVIEWPORTDATA_H


#include "AbstractViewPE.h"


/** Description:
    Protocol extention class that represents either an OdDbViewport, 
    an OdDbViewportTableRecord.

    See Also:
    OdDbAbstractViewTableRecord

    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDbAbstractViewportData : public OdAbstractViewPE
{
public:
  ODRX_DECLARE_MEMBERS(OdDbAbstractViewportData);

  virtual void setProps(OdRxObject* pTo, const OdRxObject* pFrom) const;

  virtual bool isUcsSavedWithViewport(const OdRxObject* pVpFrom) const = 0;
  virtual void setUcsPerViewport( OdRxObject* pVpTo, bool ucsvp ) const = 0;

  virtual bool isUcsFollowModeOn(const OdRxObject* pVpFrom) const = 0;
  virtual void setUcsFollowModeOn(OdRxObject* pVpTo, bool bOn) const = 0;

  virtual OdUInt16 circleSides(const OdRxObject* pVpFrom) const = 0;
  virtual void setCircleSides(OdRxObject* pVpTo, OdUInt16) const = 0;

  virtual bool isGridOn(const OdRxObject* pVpFrom) const = 0;
  virtual void setGridOn(OdRxObject* pVpTo, bool bOn) const = 0;

  virtual OdGeVector2d gridIncrement(const OdRxObject* pVpFrom) const = 0;
  virtual void setGridIncrement(OdRxObject* pVpTo, const OdGeVector2d&) const = 0;

  virtual bool isUcsIconVisible(const OdRxObject* pVpFrom) const = 0;
  virtual void setUcsIconVisible(OdRxObject* pVpTo, bool bVisible) const = 0;

  virtual bool isUcsIconAtOrigin(const OdRxObject* pVpFrom) const = 0;
  virtual void setUcsIconAtOrigin(OdRxObject* pVpTo, bool bAtOrigin) const = 0;

  virtual bool isSnapOn(const OdRxObject* pVpFrom) const = 0;
  virtual void setSnapOn(OdRxObject* pVpTo, bool bOn) const = 0;

  virtual bool isSnapIsometric(const OdRxObject* pVpFrom) const = 0;
  virtual void setSnapIsometric(OdRxObject* pVpTo, bool bIsometric) const = 0;

  virtual double snapAngle(const OdRxObject* pVpFrom) const = 0;
  virtual void setSnapAngle(OdRxObject* pVpTo, double) const = 0;

  virtual OdGePoint2d snapBase(const OdRxObject* pVpFrom) const = 0;
  virtual void setSnapBase(OdRxObject* pVpTo, const OdGePoint2d&) const = 0; 

  virtual OdGeVector2d snapIncrement(const OdRxObject* pVpFrom) const = 0;
  virtual void setSnapIncrement(OdRxObject* pVpTo, const OdGeVector2d&) const = 0;

  virtual OdUInt16 snapIsoPair(const OdRxObject* pVpFrom) const = 0;
  virtual void setSnapIsoPair(OdRxObject* pVpTo, OdUInt16) const = 0;
};

typedef OdSmartPtr<OdDbAbstractViewportData> OdDbAbstractViewportDataPtr;


#endif //#ifndef OD_DBABSTRACTVIEWPORTDATA_H


