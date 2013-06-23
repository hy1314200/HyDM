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



#ifndef OD_WIPEOUT_H
#define OD_WIPEOUT_H

#include "DD_PackPush.h"

#include "DbRasterImage.h"

/** Description:
    Represents a wipeout entity in an OdDbDatabase.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbWipeout : public OdDbRasterImage
{
public:
  ODDB_DECLARE_MEMBERS(OdDbWipeout);

  OdDbWipeout();

  /** Description:
      Sets orientation and boundary for this entity (DXF 10, 11, 12, 71, 14, 24).
      Arguments:
      points (I) 3d points array in WCS. Must be planar.
  */
  void setBoundary(const OdGePoint3dArray& points);


  virtual OdResult dwgInFields(OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(OdDbDxfFiler* pFiler) const;

  virtual bool worldDraw(OdGiWorldDraw* pWd) const;
};
 
typedef OdSmartPtr<OdDbWipeout> OdDbWipeoutPtr;

#include "DD_PackPop.h"

#endif  // OD_WIPEOUT_H


