
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

#ifndef _ODOLEITEMHANDLER_INCLUDED_
#define _ODOLEITEMHANDLER_INCLUDED_

#include "RxObject.h"
#include "DbSystemServices.h"
#include "OdErrorContext.h"
#include "Gi/GiViewportGeometry.h"
#include "RxModule.h"
#include "OdStreamBuf.h"

#include "DD_PackPush.h"

/** Description:

    {group:Other_Classes}
*/
class OdOleError : public OdError
{
public:
  OdOleError(OdErrorContext* pCtx) : OdError(pCtx) { }
  const OdErrorContext* context() const { return m_pContext; }
};

class OdDbObjectId;

/** Description:

    {group:Other_Classes}
*/
class TOOLKIT_EXPORT OdOleItemInitStream : public OdStreamBuf
{
public:
  ODRX_DECLARE_MEMBERS(OdOleItemInitStream);

  virtual OdDbObjectId frameId() const = 0;
};

typedef OdSmartPtr<OdOleItemInitStream> OdOleItemInitStreamPtr;


class OdOleItemHandler;
class OdDbObjectId;
typedef OdSmartPtr<OdOleItemHandler> OdOleItemHandlerPtr;

/** Description:

    {group:Other_Classes}
*/
class TOOLKIT_EXPORT OdOleItemHandler : public OdGiSelfGdiDrawable
{
public:
  ODRX_DECLARE_MEMBERS(OdOleItemHandler);

  /** Description:
      Reads in OdDbOle2Frame object's binary data.
      See MFC COleClientItem::Serialize(CArchive& ar) for detailes
      ( assuming that ar.m_bForceFlat is TRUE ).
  */
  virtual void load(OdStreamBuf& stream) = 0;

  /** Description:
      Writes out OdDbOle2Frame object's binary data.
      See MFC COleClientItem::Serialize(CArchive& ar) for detailes
      ( assuming that ar.m_bForceFlat is TRUE ).
  */
  virtual void save(OdStreamBuf& stream) const = 0;

  /** Description:
      Returns compound document data size.
  */
  virtual OdUInt32 getCompoundDocumentDataSize() const = 0;

  /** Description:
      Writes compound document data into specified stream.
  */
  virtual void getCompoundDocument(OdStreamBuf& stream) const = 0;

  /** Description:
      Reads compound document data from specified stream.
  */
  virtual void setCompoundDocument(OdUInt32 nSize, OdStreamBuf& stream) = 0;

  enum Type
  {
    kUnknown    = 0,
    kLink       = 1,
    kEmbedded   = 2,
    kStatic     = 3
  };
  virtual Type type() const = 0;

  enum DvAspect
  {
    kContent    = 1,
    kThumbnail  = 2,
    kIcon       = 4,
    kDocPrint   = 8
  };

  /** Description:
      Returns draw aspect.
  */
  virtual DvAspect drawAspect() const = 0;

  virtual OdString linkName() const = 0;

  virtual OdString linkPath() const = 0;

  virtual OdString userType() const = 0;

  virtual void setDrawAspect(DvAspect drawAspect) = 0;
};

#include "DD_PackPop.h"

#endif // _ODOLEITEMHANDLER_INCLUDED_

