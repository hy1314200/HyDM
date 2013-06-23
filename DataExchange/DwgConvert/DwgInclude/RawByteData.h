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

#ifndef _ODRAWBYTEDATA_INCLUDED_
#define _ODRAWBYTEDATA_INCLUDED_

#include "OleStorage.h"

/** Description:

    {group:Other_Classes} 
*/
class OdRawByteData : public OdByteData
{
  const OdUInt8* m_pData;
  OdUInt32       m_nDataLeft;
protected:
  OdRawByteData()
    : m_pData(0)
    , m_nDataLeft(0)
  {
  }
public:
  void init(const OdUInt8* pData, OdUInt32 length)
  {
    m_pData = pData;
    m_nDataLeft = length;
  }

  OdUInt32 bytesLeft() const
  {
    return m_nDataLeft;
  }

  void read(OdUInt32 nBytes, void* buffer)
  {
    if(m_nDataLeft >= nBytes)
    {
      m_nDataLeft -= nBytes;
      ::memcpy(buffer, m_pData, nBytes);
      m_pData += nBytes;
    }
    else
      throw OdError(eEndOfFile);
  }
};


#endif // _ODRAWBYTEDATA_INCLUDED_
