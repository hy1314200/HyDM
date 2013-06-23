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

#ifndef _OD_DB_COLOR_
#define _OD_DB_COLOR_


#include "DbObject.h"
#include "CmColor.h"


#include "DD_PackPush.h"

/** Description:
  For DWGdirect internal use only.
  Library:
  Db
  {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbColor : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbColor);
  // ODRX_DECLARE_MEMBERS(OdDbColor);

  OdDbColor();
  
  /** Description:
    For DWGdirect internal use only.
    
    Arguments:
    color (O) For DWGdirect internal use only.
  */
  void getColor(
    OdCmColor& color) const;
  /** Description:
    For DWGdirect internal use only.
    
    Arguments:
    color (I) For DWGdirect internal use only.
  */
  void setColor(
    const OdCmColor& color);

  /** Description:
    For DWGdirect internal use only.
  */
  const OdCmEntityColor& entityColor() const;

  // Saving as previous versions.
  //
  /* virtual OdResult            decomposeForSave(
                              OdDb::OdDbDwgVersion ver,
                              OdDbObject*& replaceObj,
                              OdDbObjectId& replaceId,
                              Adesk::Boolean& exchangeXData);*/


  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

private:

  OdCmColor           m_color;    // main storage
  OdCmEntityColor     m_ecol;     // quick reference

  friend class OdDbColorImpl;
};

typedef OdSmartPtr<OdDbColor> OdDbColorPtr;

#include "DD_PackPop.h"

#endif
