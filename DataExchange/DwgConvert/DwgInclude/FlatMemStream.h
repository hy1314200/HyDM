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



// FlatMemStream.h: interface for the OdMemoryStream class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(_ODFLATMEMSTREAM_H_INCLUDED_)
#define _ODFLATMEMSTREAM_H_INCLUDED_

#include "DD_PackPush.h"

#include "OdStreamBuf.h"

class OdFlatMemStream;
typedef OdSmartPtr<OdFlatMemStream> OdFlatMemStreamPtr;

/** Description:

    {group:Other_Classes}
*/
class FIRSTDLL_EXPORT OdFlatMemStream : public OdStreamBuf
{
protected:
  void*     m_pMemData;
  OdUInt32  m_nEndPos;
  OdUInt32  m_nCurPos;
  inline OdUInt8* data();
  inline OdUInt32 left();
  virtual void append(OdUInt32 nSize);
  inline OdFlatMemStream(void* pMemData, OdUInt32 nSize, OdUInt32 nCurPos);
  inline OdFlatMemStream() { init(0, 0, 0); }
public:
  static OdFlatMemStreamPtr createNew(void* pMemData, OdUInt32 nSize, OdUInt32 nCurPos = 0);

  inline void init(void* pMemData, OdUInt32 nSize, OdUInt32 nCurPos = 0);

  OdUInt32 length();
  OdUInt32 tell();
  OdUInt32 seek(OdInt32 offset, OdDb::FilerSeekType whence);

  bool isEof();
  OdUInt8 getByte();
  void getBytes(void* buffer, OdUInt32 nLen);
  void copyDataTo(OdStreamBuf* pDest, OdUInt32 nSrcStart, OdUInt32 nSrcEnd);
  
  // overrides existing byte(s)
  void putByte(OdUInt8 val);
  void putBytes(const void* buffer, OdUInt32 nLen);
};

inline OdFlatMemStream::OdFlatMemStream(void* pMemData, OdUInt32 nSize, OdUInt32 nCurPos)
{ init(pMemData, nSize, nCurPos); }

inline OdUInt8* OdFlatMemStream::data() { return (OdUInt8*)m_pMemData; }

inline OdUInt32 OdFlatMemStream::left() 
{
  return m_nEndPos - m_nCurPos; 
}

inline void OdFlatMemStream::init(void* pMemData, OdUInt32 nSize, OdUInt32 nCurPos)
{
  m_pMemData = pMemData;
  m_nEndPos = nCurPos + nSize;
  m_nCurPos = nCurPos;
}

#include "DD_PackPop.h"

#endif // !defined(_ODFLATMEMSTREAM_H_INCLUDED_)


