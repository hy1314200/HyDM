

/* this ALWAYS GENERATED file contains the IIDs and CLSIDs */

/* link this file in with the server and any clients */


 /* File created by MIDL compiler version 7.00.0555 */
/* at Sun Jul 28 10:55:13 2013
 */
/* Compiler settings for DwgConvert.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 7.00.0555 
    protocol : dce , ms_ext, c_ext
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#pragma warning( disable: 4049 )  /* more than 64k source lines */


#ifdef __cplusplus
extern "C"{
#endif 


#include <rpc.h>
#include <rpcndr.h>

#ifdef _MIDL_USE_GUIDDEF_

#ifndef INITGUID
#define INITGUID
#include <guiddef.h>
#undef INITGUID
#else
#include <guiddef.h>
#endif

#define MIDL_DEFINE_GUID(type,name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8) \
        DEFINE_GUID(name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8)

#else // !_MIDL_USE_GUIDDEF_

#ifndef __IID_DEFINED__
#define __IID_DEFINED__

typedef struct _IID
{
    unsigned long x;
    unsigned short s1;
    unsigned short s2;
    unsigned char  c[8];
} IID;

#endif // __IID_DEFINED__

#ifndef CLSID_DEFINED
#define CLSID_DEFINED
typedef IID CLSID;
#endif // CLSID_DEFINED

#define MIDL_DEFINE_GUID(type,name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8) \
        const type name = {l,w1,w2,{b1,b2,b3,b4,b5,b6,b7,b8}}

#endif !_MIDL_USE_GUIDDEF_

MIDL_DEFINE_GUID(IID, LIBID_DwgConvertLib,0x3DEEF6BD,0xBEB8,0x468C,0x90,0xF7,0x33,0x0F,0x77,0x17,0xAF,0x18);


MIDL_DEFINE_GUID(IID, IID_IDwgReader,0x9F1A9E88,0x4B96,0x4F8C,0x8C,0xE2,0x1A,0x07,0x04,0x1D,0xA3,0xEA);


MIDL_DEFINE_GUID(IID, IID_IDwgWriter,0x8C1C207C,0xD53F,0x4965,0x8D,0xEF,0x22,0x3A,0xA1,0xEB,0xE0,0xEC);


MIDL_DEFINE_GUID(CLSID, CLSID_DwgWriter,0xBE2255C9,0xF951,0x416F,0x9E,0x71,0xBF,0x0E,0x42,0x99,0x5C,0x55);


MIDL_DEFINE_GUID(CLSID, CLSID_DwgReader,0xB3665367,0x4710,0x4C99,0xA3,0x75,0x80,0x2C,0xDC,0x11,0x6E,0xA1);

#undef MIDL_DEFINE_GUID

#ifdef __cplusplus
}
#endif



