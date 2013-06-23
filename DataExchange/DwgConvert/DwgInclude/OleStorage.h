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

#ifndef _ODSTORAGE_INCLUDED_
#define _ODSTORAGE_INCLUDED_

#include "RxObject.h"
#include "OdStreamBuf.h"
#include "DbSystemServices.h"
#include "OdErrorContext.h"

#include "DD_PackPush.h"

/** Description:

    {group:Other_Classes} 
*/
class FIRSTDLL_EXPORT OdByteData : public OdRxObject
{
public:
  typedef OdUInt32 SizeType;

  ODRX_DECLARE_MEMBERS(OdByteData);

  virtual SizeType readAt(SizeType nStart, OdUInt8 *buffer, SizeType nBytes) const = 0;
  virtual SizeType writeAt(SizeType nStart, const OdUInt8 *buffer, SizeType nBytes) = 0;
  virtual SizeType size() const = 0;
  virtual void resize(SizeType nBytes) = 0;
  virtual void flush() = 0;
};

typedef OdSmartPtr<OdByteData> OdByteDataPtr;


/** Description:

    {group:Other_Classes}
*/
class OdOleStorageError : public OdError
{
public:
  OdOleStorageError(OdErrorContext* pCtx) : OdError(pCtx) { }
};

class OdOleStorage;
typedef OdSmartPtr<OdOleStorage> OdOleStoragePtr;

/** Description:

    {group:Other_Classes}
*/
class FIRSTDLL_EXPORT OdOleStorage : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdOleStorage);

  virtual void init(OdByteData* pData, int mode = Oda::kFileRead) = 0;
  virtual OdByteDataPtr rawData() const = 0;

  virtual OdStreamBufPtr openStream(const OdString &streamName,
                                    bool bCreateIfNotFound = false, // reserved
                                    int mode = Oda::kFileRead) = 0; // reserved

  static OdOleStoragePtr createServiceObject(OdByteData* pInitData);
};

#define ODRX_OLESTORAGE_SERVICE_NAME OdOleStorage::desc()->name()

#include "DD_PackPop.h"

#endif // _ODSTORAGE_INCLUDED_
