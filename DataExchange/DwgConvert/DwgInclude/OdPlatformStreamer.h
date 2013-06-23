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

#ifndef _OD_PLATFORMSTREAMER_H_
#define _OD_PLATFORMSTREAMER_H_

#include "OdPlatform.h"
#include "OdStreamBuf.h"

/** Description:

    {group:Other_Classes}
*/
class OdPlatformStreamer
{
public:
  static OdInt16    rdInt16   (OdStreamBuf& io);
  static OdInt32    rdInt32   (OdStreamBuf& io);
  static double     rdDouble  (OdStreamBuf& io);
  static OdDbHandle rdDbHandle(OdStreamBuf& io);
  static void       rd2Doubles(OdStreamBuf& io, void* pRes2Doubles);
  static void       rd3Doubles(OdStreamBuf& io, void* pRes3Doubles);
  static void       rdDoubles (OdStreamBuf& io, int n, void* pResDoubles);


  static void       wrInt16   (OdStreamBuf& io, OdInt16 val);
  static void       wrInt32   (OdStreamBuf& io, OdInt32 val);
  static void       wrDouble  (OdStreamBuf& io, double val);
  static void       wrDbHandle(OdStreamBuf& io, OdDbHandle val);
  static void       wr2Doubles(OdStreamBuf& io, const void* p2Doubles);
  static void       wr3Doubles(OdStreamBuf& io, const void* p3Doubles);
  static void       wrDoubles (OdStreamBuf& io, int n, const void* pDoubles);
};

inline OdInt16 OdPlatformStreamer::rdInt16(OdStreamBuf& io)
{
  OdInt16 res;
  io.getBytes(&res, sizeof(res));
  odSwap2BytesNumber(res);
  return res;
}
inline OdInt32 OdPlatformStreamer::rdInt32(OdStreamBuf& io)
{
  OdInt32 res;
  io.getBytes(&res, sizeof(res));
  odSwap4BytesNumber(res);
  return res;
}
inline double OdPlatformStreamer::rdDouble(OdStreamBuf& io)
{
  double res;
  ODA_ASSUME(sizeof(res) == 8)
  io.getBytes(&res, 8);
  odSwap8Bytes(&res);
  // if unnormalized or NaN or infinity, set it to 0.0
  if (!isValidNonZeroIEEEDouble((OdUInt8 *)&res))
    return 0.0;
  return res;
}
inline OdDbHandle OdPlatformStreamer::rdDbHandle(OdStreamBuf& io)
{
  OdDbHandle res;
  ODA_ASSUME(sizeof(res) == 8)
  io.getBytes(&res, 8);
  odSwapInt64(&res);
  return res;
}
inline void OdPlatformStreamer::rd2Doubles(OdStreamBuf& io, void* pRes2Doubles)
{
  ODA_ASSUME(sizeof(double) == 8)
  io.getBytes(pRes2Doubles, sizeof(double)*2);
  fixDouble((double*)pRes2Doubles);
  fixDouble((double*)pRes2Doubles+1);
}
inline void OdPlatformStreamer::rd3Doubles(OdStreamBuf& io, void* pRes3Doubles)
{
  ODA_ASSUME(sizeof(double) == 8)
  io.getBytes(pRes3Doubles, sizeof(double)*3);
  fixDouble((double*)pRes3Doubles);
  fixDouble((double*)pRes3Doubles+1);
  fixDouble((double*)pRes3Doubles+2);
}
inline void OdPlatformStreamer::rdDoubles(OdStreamBuf& io, int n, void* pResDoubles)
{
  ODA_ASSUME(sizeof(double) == 8)
  io.getBytes(pResDoubles, sizeof(double)*n);
  double* pD = (double*)pResDoubles;
  while (n--) { fixDouble(pD++); }
}

inline void OdPlatformStreamer::wrInt16(OdStreamBuf& io, OdInt16 val)
{
  odSwap2BytesNumber(val);
  io.putBytes(&val, sizeof(val));
}
inline void OdPlatformStreamer::wrInt32(OdStreamBuf& io, OdInt32 val)
{
  odSwap4BytesNumber(val);
  io.putBytes(&val, sizeof(val));
}
inline void OdPlatformStreamer::wrDouble(OdStreamBuf& io, double val)
{
  ODA_ASSUME(sizeof(double) == 8)
  odSwap8Bytes(&val);
  io.putBytes(&val, sizeof(val));
}
inline void OdPlatformStreamer::wrDbHandle(OdStreamBuf& io, OdDbHandle val)
{
  odSwapInt64(&val);
  io.putBytes(&val, sizeof(val));
}

#ifdef ODA_BIGENDIAN

inline void OdPlatformStreamer::wr2Doubles(OdStreamBuf& io, const void* p2Doubles)
{
  wrDouble(io, *(((double*)p2Doubles)+0));
  wrDouble(io, *(((double*)p2Doubles)+1));
}
inline void OdPlatformStreamer::wr3Doubles(OdStreamBuf& io, const void* p3Doubles)
{
  wrDouble(io, *(((double*)p3Doubles)+0));
  wrDouble(io, *(((double*)p3Doubles)+1));
  wrDouble(io, *(((double*)p3Doubles)+2));
}
inline void OdPlatformStreamer::wrDoubles(OdStreamBuf& io, int n, const void* pDoubles)
{
  while(n--) wrDouble(io, ((double*)pDoubles)[n]);
}

#else // #ifdef ODA_BIGENDIAN

inline void OdPlatformStreamer::wr2Doubles(OdStreamBuf& io, const void* p2Doubles)
{
  ODA_ASSUME(sizeof(double) == 8)
  io.putBytes(p2Doubles, sizeof(double)*2);
}
inline void OdPlatformStreamer::wr3Doubles(OdStreamBuf& io, const void* p3Doubles)
{
  ODA_ASSUME(sizeof(double) == 8)
  io.putBytes(p3Doubles, sizeof(double)*3);
}
inline void OdPlatformStreamer::wrDoubles(OdStreamBuf& io, int n, const void* pDoubles)
{
  ODA_ASSUME(sizeof(double) == 8)
  io.putBytes(pDoubles, sizeof(double) * n);
}

#endif // ODA_BIGENDIAN

#endif // _OD_PLATFORMSTREAMER_H_


