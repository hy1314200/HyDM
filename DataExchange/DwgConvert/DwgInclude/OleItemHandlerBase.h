
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

#ifndef _OLEITEMDATAHEADER_INCLUDED_
#define _OLEITEMDATAHEADER_INCLUDED_

#include "DD_PackPush.h"
#include "OleItemHandler.h"
#include "UInt8Array.h"


/** Description:
    This class is intended to be used for implementing custom OLE handlers.
    Represents header of OdDbOle2Frame binary data.

    Remarks:
    The header is an MFC COleClientItem object's fields serialized
    through MFC CArchive object. See MFC source code for detailes.

    {group:Other_Classes}
*/
class TOOLKIT_EXPORT OdOleItemHandlerBase : public OdOleItemHandler
{
protected:
  /* FROM MFC SOURCE:
  enum OLE_OBJTYPE
  {
    OT_UNKNOWN  = 0,

    // These are OLE 1.0 types and OLE 2.0 types as returned from GetType().
    OT_LINK     = 1,
    OT_EMBEDDED = 2,
    OT_STATIC   = 3,
    
    // All OLE2 objects are written with this tag when serialized.  This
    //  differentiates them from OLE 1.0 objects written with MFC 2.0.
    //  This value will never be returned from GetType().
    OT_OLE2     = 256,
  };
  */
  
  OdUInt32  m_nOleVer;    // (enum OLE_OBJTYPE) must be always OT_OLE2
  OdUInt32  m_nItemId;    // id in COleDocument
  DvAspect  m_adviseType; // view advise type (DVASPECT)
  OdUInt16  m_bMoniker;   // flag indicating whether to create moniker upon load
  DvAspect  m_drawAspect; // current default display aspect

  OdOleItemHandlerBase();
public:
  ODRX_DECLARE_MEMBERS(OdOleItemHandlerBase);

  /** Remarks:
      Reads in header of OdDbOle2Frame binary data.
  */
  void load(OdStreamBuf& stream);

  /** Remarks:
      Writes out header of OdDbOle2Frame binary data.
  */
  void save(OdStreamBuf& stream) const;

  /** Remarks:
      Does nothing.
  */
  void draw(const OdGiCommonDraw& drawObj, void* hdc, const OdGsDCRect& rect) const;

  /** Remarks:
      returns kUnknown.
  */
  Type type() const;

  /** Remarks:
      returns empty string.
  */
  OdString linkName() const;

  /** Remarks:
      returns empty string.
  */
  OdString linkPath() const;

  /** Remarks:
      returns empty string.
  */
  OdString userType() const;



  /** Remarks:
      returns m_drawAspect field.
  */
  DvAspect drawAspect() const;

  void setDrawAspect(DvAspect drawAspect);

  OdUInt32 itemId() const;

  void setItemId(OdUInt32 nId);

  DvAspect adviseType() const;

  void setAdviseType(DvAspect at);

  bool monikerAssigned() const;

  void setMonikerAssigned(bool bAssigned);
};


/*
    {group:Other_Classes}
*/
class TOOLKIT_EXPORT OdOleItemSimplestHandler : public OdOleItemHandlerBase
{
  OdUInt8Array  m_compDocData;
public:
  /** Remarks:
      returns m_compDocData.size().
  */
  OdUInt32 getCompoundDocumentDataSize() const;

  /** Remarks:
      writes out compound document data from m_compDocData.
  */
  void getCompoundDocument(OdStreamBuf& stream) const;

  /** Remarks:
      reads in compound document data into m_compDocData.
  */
  void setCompoundDocument(OdUInt32 nDataSize, OdStreamBuf& stream);
};


#include "DD_PackPop.h"

#endif // #ifndef _OLEITEMDATAHEADER_INCLUDED_

