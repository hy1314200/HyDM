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



#ifndef ODDBHANDLE
#define ODDBHANDLE /* {Secret} */

#include "DD_PackPush.h"

#include "OdArrayPreDef.h"
class OdString;

/** Returns the numeric value of a hexadecimal digit. For example, getHexValue('A') returns 10.
*/
unsigned char getHexValue(unsigned char c);

/** Returns the 64 bit integer value corresponding to the passed in string.
@param pStr (I) String representation of an integer.
*/
//TOOLKIT_EXPORT OdInt64 atoi64(const OdChar* pStr);
//TOOLKIT_EXPORT void    i64toA(const OdInt64 &val, OdChar* pStr);

class OdString;

/** Description:

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbHandle
{
public:
  //  Constructors
  OdDbHandle();
  OdDbHandle(const OdDbHandle& handle);
  OdDbHandle(OdUInt64 val);
  OdDbHandle(const OdChar* string);
  // This constructor prevents using (OdChar *)  for handle(0);
  OdDbHandle(int val);

  OdDbHandle& operator=(OdUInt64 val);
  OdDbHandle& operator=(const OdChar* string);
  
  OdDbHandle& operator=(const OdDbHandle& val)
  {
    m_val = val.m_val;
    return *this;
  }

  // This operator prevents using =(OdChar *)  for handle = 0;
  OdDbHandle& operator=(int val);

  operator OdUInt64() const;
  void getIntoAsciiBuffer(OdChar* pBuf) const;
  OdString ascii() const;
  bool isNull() const;
  bool operator == (OdUInt64 val) const;
  bool operator != (OdUInt64 val) const;
  bool operator > (OdUInt64 val) const;
  bool operator < (OdUInt64 val) const;
  bool operator <= (OdUInt64 val) const;
  bool operator >= (OdUInt64 val) const;
  OdDbHandle& operator+=(const OdInt64& n);
  OdDbHandle operator+(const OdInt64& n);

  void bytes(OdUInt8 * pBuf) const;

private:
    OdUInt64  m_val;
};

typedef OdArray<OdDbHandle> OdHandleArray;

inline OdDbHandle::OdDbHandle()
    : m_val(0)
{}

inline OdDbHandle::OdDbHandle(const OdDbHandle& handle)
    : m_val(handle.m_val)
{}

inline OdDbHandle::OdDbHandle(OdUInt64 val)
    : m_val(val)
{}

    // This constructor prevents using (OdChar *)  for handle(0);
inline OdDbHandle::OdDbHandle(int val)
    : m_val(val)
{}

inline OdDbHandle::OdDbHandle(const OdChar* string)
{
  *this = string;
}

inline OdDbHandle& OdDbHandle::operator=(OdUInt64 val)
{
  m_val = val;
  return *this;
}

// This operator prevents using =(OdChar *)  for handle = 0;
inline OdDbHandle& OdDbHandle::operator=(int val)
{
  m_val = val;
  return *this;
}

inline OdDbHandle::operator OdUInt64() const
{
  return m_val;
}

inline bool OdDbHandle::isNull() const
{
  return m_val == 0;
}

inline bool OdDbHandle::operator == (OdUInt64 val) const
{
  return m_val == val;
}

inline bool OdDbHandle::operator != (OdUInt64 val) const
{
  return m_val != val;
}

inline bool OdDbHandle::operator > (OdUInt64 val) const
{
  return m_val > val;
}

inline bool OdDbHandle::operator < (OdUInt64 val) const
{
  return m_val < val;
}

inline bool OdDbHandle::operator >= (OdUInt64 val) const
{
  return m_val > val || m_val == val;
}

inline bool OdDbHandle::operator <= (OdUInt64 val) const
{
  return m_val < val || m_val == val;
}

inline OdDbHandle& OdDbHandle::operator+=(const OdInt64& val)
{
  m_val += val;
  return *this;
}

inline OdDbHandle OdDbHandle::operator+(const OdInt64& val)
{
  OdDbHandle res;
  res.m_val = m_val + val;
  return res;
}

inline void OdDbHandle::bytes(OdUInt8 * pBuf) const
{
  OdUInt64 val = m_val;
  *pBuf++ = (OdUInt8)(val & 0xFF);
  val >>= 8;
  *pBuf++ = (OdUInt8)(val & 0xFF);
  val >>= 8;
  *pBuf++ = (OdUInt8)(val & 0xFF);
  val >>= 8;
  *pBuf++ = (OdUInt8)(val & 0xFF);
  val >>= 8;
  *pBuf++ = (OdUInt8)(val & 0xFF);
  val >>= 8;
  *pBuf++ = (OdUInt8)(val & 0xFF);
  val >>= 8;
  *pBuf++ = (OdUInt8)(val & 0xFF);
  val >>= 8;
  *pBuf = (OdUInt8)(val & 0xFF);
}

#include "DD_PackPop.h"

#endif


