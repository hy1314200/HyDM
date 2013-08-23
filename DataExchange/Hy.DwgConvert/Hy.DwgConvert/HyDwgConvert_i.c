

/* this ALWAYS GENERATED file contains the IIDs and CLSIDs */

/* link this file in with the server and any clients */


 /* File created by MIDL compiler version 7.00.0555 */
/* at Fri Aug 23 17:27:19 2013
 */
/* Compiler settings for HyDwgConvert.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 7.00.0555 
    protocol : dce , ms_ext, c_ext, robust
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

MIDL_DEFINE_GUID(IID, IID_IDwgReader,0x11212A91,0xCAD7,0x40B4,0xA3,0x22,0x9F,0x89,0x47,0x28,0xC1,0x61);


MIDL_DEFINE_GUID(IID, IID_IDwgEntity,0x82B362A4,0x7827,0x4439,0xB4,0x3C,0xFC,0x94,0x71,0x92,0x6C,0xF2);


MIDL_DEFINE_GUID(IID, LIBID_HyDwgConvert,0xAAA94798,0x1C41,0x4359,0x97,0x70,0x5E,0xF0,0xA3,0x0B,0xAB,0x81);


MIDL_DEFINE_GUID(CLSID, CLSID_DwgReader,0x847B149E,0x3CC9,0x4F57,0xBB,0x11,0xAA,0x41,0x2E,0x66,0xC4,0x20);


MIDL_DEFINE_GUID(CLSID, CLSID_DwgEntity,0xCEE471D4,0xE47D,0x489D,0xA6,0x99,0x6C,0x2D,0x80,0x7B,0x1A,0x74);

#undef MIDL_DEFINE_GUID

#ifdef __cplusplus
}
#endif



