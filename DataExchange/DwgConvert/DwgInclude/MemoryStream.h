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



// MemoryStream.h: interface for the OdMemoryStreamImpl class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(_MEMORYSTREAM_H_INCLUDED_)
#define _MEMORYSTREAM_H_INCLUDED_


//DD:EXPORT_ON

#include "OdStreamBuf.h"

class OdMemoryStream;
typedef OdSmartPtr<OdMemoryStream> OdMemoryStreamPtr;

/** Description:

    {group:Other_Classes}
*/
class FIRSTDLL_EXPORT OdMemoryStream : public OdStreamBuf
{
protected:
  OdMemoryStream();
public:
  ODRX_DECLARE_MEMBERS(OdMemoryStream);

  static OdMemoryStreamPtr createNew(OdUInt32 nPageDataSize = 0x800);

  virtual OdUInt32 pageDataSize() const = 0;

  virtual void setPageDataSize(OdUInt32 nPageSize) = 0;

  virtual void reserve(OdUInt32 nSize) = 0;

  OdString fileName();
};

//DD:EXPORT_OFF

#endif // !defined(_MEMORYSTREAM_H_INCLUDED_)


