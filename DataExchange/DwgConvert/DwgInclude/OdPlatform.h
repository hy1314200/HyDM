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



#ifndef _OD_PLATFORM_H_
#define _OD_PLATFORM_H_


#include "OdPlatformSettings.h"

#define OD_MAKEWORD(a, b)      ((OdUInt16)(((OdUInt8)(a)) | ((OdUInt16)((OdUInt8)(b))) << 8))
#define OD_MAKELONG(a, b)      ((OdInt32)(((OdUInt16)(a)) | ((OdUInt32)((OdUInt16)(b))) << 16))
#define OD_LOWORD(l)           ((OdUInt16)(l))
#define OD_HIWORD(l)           ((OdUInt16)(((OdUInt32)(l) >> 16) & 0xFFFF))
#define OD_LOBYTE(w)           ((OdUInt8)(w))
#define OD_HIBYTE(w)           ((OdUInt8)(((OdUInt16)(w) >> 8) & 0xFF))

inline void odSwapBytes(OdUInt8& X, OdUInt8& Y) { X ^= Y; Y ^= X; X ^= Y; }
inline void odSwapWords(OdUInt16& X, OdUInt16& Y) { X ^= Y; Y ^= X; X ^= Y; }

#ifdef ODA_BIGENDIAN

inline void odSwap2BytesNumber(OdInt16& W) 
{ 
  odSwapBytes(((OdUInt8*)&(W))[0], ((OdUInt8*)&(W))[1]); 
}

inline void odSwap4BytesNumber(OdUInt32& DW)
{
  odSwapWords(((OdUInt16*)&(DW))[0], ((OdUInt16*)&(DW))[1]);
  odSwapBytes(((OdUInt8*)&(DW))[0], ((OdUInt8*)&(DW))[1]);
  odSwapBytes(((OdUInt8*)&(DW))[2], ((OdUInt8*)&(DW))[3]);
}

inline void odSwap4BytesNumber(OdInt32& DW)
{
  // NOTE: Using above unsigned version on AIX results in runtime errors.
  // Macro version also causes errors on AIX.
  OdInt8* p = (OdInt8*)&DW;
  OdInt8 tmp;
  tmp = p[0]; p[0] = p[3]; p[3] = tmp;
  tmp = p[1]; p[1] = p[2]; p[2] = tmp;
}

inline void odSwap8Bytes(void* pBytes)
{
  odSwapBytes(((OdUInt8*)(pBytes))[0], ((OdUInt8*)(pBytes))[7]);
  odSwapBytes(((OdUInt8*)(pBytes))[1], ((OdUInt8*)(pBytes))[6]);
  odSwapBytes(((OdUInt8*)(pBytes))[2], ((OdUInt8*)(pBytes))[5]);
  odSwapBytes(((OdUInt8*)(pBytes))[3], ((OdUInt8*)(pBytes))[4]);
}

inline void odSwapInt64(void* pBytes)
{
  odSwapBytes(((OdUInt8*)(pBytes))[0], ((OdUInt8*)(pBytes))[4]);
  odSwapBytes(((OdUInt8*)(pBytes))[1], ((OdUInt8*)(pBytes))[5]);
  odSwapBytes(((OdUInt8*)(pBytes))[2], ((OdUInt8*)(pBytes))[6]);
  odSwapBytes(((OdUInt8*)(pBytes))[3], ((OdUInt8*)(pBytes))[7]);
}

#else

#define odSwap2BytesNumber(n)
#define odSwap4BytesNumber(n)
#define odSwap8Bytes(bytes)

inline void odSwapInt64(void* pBytes)
{
  odSwapBytes(((OdUInt8*)(pBytes))[0], ((OdUInt8*)(pBytes))[7]);
  odSwapBytes(((OdUInt8*)(pBytes))[1], ((OdUInt8*)(pBytes))[6]);
  odSwapBytes(((OdUInt8*)(pBytes))[2], ((OdUInt8*)(pBytes))[5]);
  odSwapBytes(((OdUInt8*)(pBytes))[3], ((OdUInt8*)(pBytes))[4]);
}

#endif // ODA_BIGENDIAN

/** Description
  Checks if 8 bytes buffer represents a valid non-zero IEEE double value.
  (In this format doubles are stored in AutoCAD's binary files.)
*/
inline bool isValidNonZeroIEEEDouble(const OdUInt8 * buf)
{
#ifdef ODA_BIGENDIAN
  int nExponent = (buf[0] & 0x7F) << 4 | (buf[1] & 0xF0) >> 4;
#else
  int nExponent = (buf[7] & 0x7F) << 4 | (buf[6] & 0xF0) >> 4;
#endif
  switch (nExponent)
  {
  case 0:     // The value is zero or possibly denormalized
  case 2047:  // -INF, +INF or Nan
    return false;
  }
  return true;
}

inline void fixDouble(double * pD)
{
  ODA_ASSUME(sizeof(double) == 8)
  odSwap8Bytes(pD);
  if (!isValidNonZeroIEEEDouble((OdUInt8 *)pD))
  { // if unnormalized or NaN or infinity, set it to 0.0
    *pD = 0.;
  }
}


#define OD_INT8_FROM_BUFFPTR(pBuffPtr) *(pBuffPtr++)

inline OdUInt64 OD_INT64_FROM_BUFFPTR(const OdUInt8 *pBuffPtr)
{
  OdUInt32 low(*pBuffPtr++);
  low |= ((OdUInt32)*pBuffPtr++) << 8;
  low |= ((OdUInt32)*pBuffPtr++) << 16;
  low |= ((OdUInt32)*pBuffPtr++) << 24;

  OdUInt32 high(*pBuffPtr++);
  high |= ((OdUInt32)*pBuffPtr++) << 8;
  high |= ((OdUInt32)*pBuffPtr++) << 16;
  high |= ((OdUInt32)*pBuffPtr++) << 24;

  OdUInt64 res(high);
  res <<= 32;
  res |= low;
  return res;
}

#define OD_BYTES_FROM_BUFFPTR(pBuffPtr, ResBuff, nCount) (pBuffPtr+=nCount, ::memcpy(ResBuff, pBuffPtr-nCount, nCount))

#define OD_INT8_TO_BUFFPTR(pBuffPtr, val) (++pBuffPtr, pBuffPtr[-1] = OdUInt8(val))
#define OD_BYTES_TO_BUFFPTR(pBuffPtr, FromBuff, nCount) (pBuffPtr+=nCount, ::memcpy(pBuffPtr-nCount, FromBuff, nCount))



#ifndef ODA_BIGENDIAN

#ifdef DD_STRICT_ALIGNMENT

extern double getStrictDouble(OdUInt8** ppBuff);
extern void setStrictDouble(OdUInt8** ppBuff, double d);
extern void setStrictInt16(OdUInt8** ppBuff, OdInt16 val);
extern void setStrictInt32(OdUInt8** ppBuff, OdInt32 val);

#define OD_INT16_FROM_BUFFPTR(pBuffPtr) (pBuffPtr += 2, (OdInt16)(*(pBuffPtr - 2) | (*(pBuffPtr - 1) << 8)))
#define OD_INT32_FROM_BUFFPTR(pBuffPtr) (pBuffPtr += 4, (OdInt32)(*(pBuffPtr - 4) | (*(pBuffPtr - 3) << 8) | (*(pBuffPtr - 2) << 16) | (*(pBuffPtr - 1) << 24)))

#define OD_DOUBLE_FROM_BUFFPTR(pBuffPtr) getStrictDouble(&pBuffPtr)

#define OD_INT16_TO_BUFFPTR(pBuffPtr, val) setStrictInt16(&pBuffPtr, val)
#define OD_INT32_TO_BUFFPTR(pBuffPtr, val) setStrictInt32(&pBuffPtr, val)

#define OD_DOUBLE_TO_BUFFPTR(pBuffPtr, val) setStrictDouble(&pBuffPtr, val)


#else

inline double getValidDouble(OdUInt8** ppBuff)
{
  double d = isValidNonZeroIEEEDouble(*ppBuff) ? *((double*)(*ppBuff)) : 0.0;
  *ppBuff+=8;
  return d;
}

#define OD_INT16_FROM_BUFFPTR(pBuffPtr) (pBuffPtr+=2, *((OdInt16*)(pBuffPtr-2)))
#define OD_INT32_FROM_BUFFPTR(pBuffPtr) (pBuffPtr+=4, *((OdInt32*)(pBuffPtr-4)))

#define OD_DOUBLE_FROM_BUFFPTR(pBuffPtr) getValidDouble(&pBuffPtr)
//#define OD_POINT3D_FROM_BUFFPTR(pBuffPtr) (pBuffPtr+=24, *((OdGePoint3d*)(pBuffPtr-24)))

#define OD_INT16_TO_BUFFPTR(pBuffPtr, val) (pBuffPtr+=2, *((OdInt16*)(pBuffPtr-2)) = OdInt16(val))
#define OD_INT32_TO_BUFFPTR(pBuffPtr, val) (pBuffPtr+=4, *((OdInt32*)(pBuffPtr-4)) = OdInt32(val))

#define OD_DOUBLE_TO_BUFFPTR(pBuffPtr, val) (pBuffPtr+=8, *((double*)(pBuffPtr-8)) = double(val))
//#define OD_POINT3D_TO_BUFFPTR(pBuffPtr, val) (pBuffPtr+=24, *((OdGePoint3d*)(pBuffPtr-24)) = val)

#endif // DD_STRICT_ALIGNMENT


#else

//DD:EXPORT_ON
extern double getBeDouble(OdUInt8** ppBuff);
//extern OdGePoint3d getBePnt3d(OdUInt8** ppBuff);
extern void setStrictInt16(OdUInt8** ppBuff, OdInt16 val);
extern void setStrictInt32(OdUInt8** ppBuff, OdInt32 val);
extern void setBeDouble(OdUInt8** ppBuff, double d);
//extern void setBePnt3(OdUInt8** ppBuff, const OdGePoint3d& p);
//DD:EXPORT_OFF

// SGI doesn't like these versions.
//#define OD_INT16_FROM_BUFFPTR(pBuffPtr) ((OdInt16)(*pBuffPtr++ | (*pBuffPtr++ << 8)))
//#define OD_INT32_FROM_BUFFPTR(pBuffPtr) ((OdInt32)(*pBuffPtr++ | (*pBuffPtr++ << 8) | (*pBuffPtr++ << 16) | (*pBuffPtr++ << 24)))
// So use these instead
#define OD_INT16_FROM_BUFFPTR(pBuffPtr) (pBuffPtr += 2, (OdInt16)(*(pBuffPtr - 2) | (*(pBuffPtr - 1) << 8)))
#define OD_INT32_FROM_BUFFPTR(pBuffPtr) (pBuffPtr += 4, (OdInt32)(*(pBuffPtr - 4) | (*(pBuffPtr - 3) << 8) | (*(pBuffPtr - 2) << 16) | (*(pBuffPtr - 1) << 24)))

#define OD_DOUBLE_FROM_BUFFPTR(pBuffPtr) getBeDouble(&pBuffPtr)
//#define OD_POINT3D_FROM_BUFFPTR(pBuffPtr) getBePnt3d(&pBuffPtr)

#define OD_INT16_TO_BUFFPTR(pBuffPtr, val) setStrictInt16(&pBuffPtr, val)
#define OD_INT32_TO_BUFFPTR(pBuffPtr, val) setStrictInt32(&pBuffPtr, val)

#define OD_DOUBLE_TO_BUFFPTR(pBuffPtr, val) setBeDouble(&pBuffPtr, val)
//#define OD_POINT3D_TO_BUFFPTR(pBuffPtr, val) setBePnt3(&pBuffPtr, val)

#endif // ODA_BIGENDIAN


#if defined(_WIN32) || defined(WIN64)
#include "WINDOWS.H"
#include "WINGDI.H"
#endif

#if defined(_WIN32) && !defined(WIN64)
// COLORREF on WIN64 is 8 bytes
#define ODCOLORREF COLORREF
#define ODRGB(r,g,b) RGB(r,g,b)
#define ODRGBA(r,g,b,a) (((ODCOLORREF)ODRGB(r,g,b))|(((DWORD)(BYTE)(a))<<24))
#define ODGETRED(rgb) GetRValue(rgb)
#define ODGETGREEN(rgb) GetGValue(rgb)
#define ODGETBLUE(rgb) GetBValue(rgb)
#define ODGETALPHA(rgba) ((BYTE)((rgba)>>24))

#else //#ifdef _WIN32

#define ODCOLORREF OdUInt32
#define ODRGB(r,g,b) ((ODCOLORREF)(((OdUInt8)(r)|((OdUInt16)((OdUInt8)(g))<<8))|(((OdUInt32)(OdUInt8)(b))<<16)))
#define ODRGBA(r,g,b,a) (((ODCOLORREF)ODRGB(r,g,b))|(((OdUInt32)(OdUInt8)(a))<<24))

#define ODGETRED(rgb)     ((OdUInt8)(rgb))
#define ODGETGREEN(rgb)   ((OdUInt8)(((OdUInt16)(rgb)) >> 8))
#define ODGETBLUE(rgb)    ((OdUInt8)((rgb)>>16))
#define ODGETALPHA(rgba)  ((OdUInt8)((rgba)>>24))

#ifndef WIN64
/** Description:
  {group:Structs}
*/      
typedef struct tagRGBQUAD 
{
  OdUInt8  rgbBlue;
  OdUInt8  rgbGreen;
  OdUInt8  rgbRed;
  OdUInt8  rgbReserved;
} RGBQUAD, *LPRGBQUAD;

/** Description:
  {group:Structs}
*/      
typedef struct tagBITMAPINFOHEADER {
  OdUInt32  biSize;
  OdInt32   biWidth;
  OdInt32   biHeight;
  OdUInt16  biPlanes;
  OdUInt16  biBitCount;
  OdUInt32  biCompression;
  OdUInt32  biSizeImage;
  OdInt32   biXPelsPerMeter;
  OdInt32   biYPelsPerMeter;
  OdUInt32  biClrUsed;
  OdUInt32  biClrImportant;
} BITMAPINFOHEADER, *PBITMAPINFOHEADER;

/** Description:
  {group:Structs}
*/      
typedef struct tagBITMAPFILEHEADER 
{
  OdUInt16 bfType;
  OdUInt32 bfSize;
  OdUInt16 bfReserved1;
  OdUInt16 bfReserved2;
  OdUInt32 bfOffBits;
} BITMAPFILEHEADER, *PBITMAPFILEHEADER;
#endif

#endif //#ifdef _WIN32

#define ODTOCMCOLOR(colorref) OdCmEntityColor( ODGETRED(colorref), ODGETGREEN(colorref), ODGETBLUE(colorref) )
#define ODTOCOLORREF(cmColor) ODRGB( cmColor.red(), cmColor.green(), cmColor.blue() )


#endif // _OD_PLATFORM_H_
