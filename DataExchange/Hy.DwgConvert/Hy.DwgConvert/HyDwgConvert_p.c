

/* this ALWAYS GENERATED file contains the proxy stub code */


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

#if !defined(_M_IA64) && !defined(_M_AMD64)


#pragma warning( disable: 4049 )  /* more than 64k source lines */
#if _MSC_VER >= 1200
#pragma warning(push)
#endif

#pragma warning( disable: 4211 )  /* redefine extern to static */
#pragma warning( disable: 4232 )  /* dllimport identity*/
#pragma warning( disable: 4024 )  /* array to pointer mapping*/
#pragma warning( disable: 4152 )  /* function/data pointer conversion in expression */
#pragma warning( disable: 4100 ) /* unreferenced arguments in x86 call */

#pragma optimize("", off ) 

#define USE_STUBLESS_PROXY


/* verify that the <rpcproxy.h> version is high enough to compile this file*/
#ifndef __REDQ_RPCPROXY_H_VERSION__
#define __REQUIRED_RPCPROXY_H_VERSION__ 475
#endif


#include "rpcproxy.h"
#ifndef __RPCPROXY_H_VERSION__
#error this stub requires an updated version of <rpcproxy.h>
#endif /* __RPCPROXY_H_VERSION__ */


#include "HyDwgConvert_i.h"

#define TYPE_FORMAT_STRING_SIZE   69                                
#define PROC_FORMAT_STRING_SIZE   355                               
#define EXPR_FORMAT_STRING_SIZE   1                                 
#define TRANSMIT_AS_TABLE_SIZE    0            
#define WIRE_MARSHAL_TABLE_SIZE   1            

typedef struct _HyDwgConvert_MIDL_TYPE_FORMAT_STRING
    {
    short          Pad;
    unsigned char  Format[ TYPE_FORMAT_STRING_SIZE ];
    } HyDwgConvert_MIDL_TYPE_FORMAT_STRING;

typedef struct _HyDwgConvert_MIDL_PROC_FORMAT_STRING
    {
    short          Pad;
    unsigned char  Format[ PROC_FORMAT_STRING_SIZE ];
    } HyDwgConvert_MIDL_PROC_FORMAT_STRING;

typedef struct _HyDwgConvert_MIDL_EXPR_FORMAT_STRING
    {
    long          Pad;
    unsigned char  Format[ EXPR_FORMAT_STRING_SIZE ];
    } HyDwgConvert_MIDL_EXPR_FORMAT_STRING;


static const RPC_SYNTAX_IDENTIFIER  _RpcTransferSyntax = 
{{0x8A885D04,0x1CEB,0x11C9,{0x9F,0xE8,0x08,0x00,0x2B,0x10,0x48,0x60}},{2,0}};


extern const HyDwgConvert_MIDL_TYPE_FORMAT_STRING HyDwgConvert__MIDL_TypeFormatString;
extern const HyDwgConvert_MIDL_PROC_FORMAT_STRING HyDwgConvert__MIDL_ProcFormatString;
extern const HyDwgConvert_MIDL_EXPR_FORMAT_STRING HyDwgConvert__MIDL_ExprFormatString;


extern const MIDL_STUB_DESC Object_StubDesc;


extern const MIDL_SERVER_INFO IDwgReader_ServerInfo;
extern const MIDL_STUBLESS_PROXY_INFO IDwgReader_ProxyInfo;


extern const MIDL_STUB_DESC Object_StubDesc;


extern const MIDL_SERVER_INFO IDwgEntity_ServerInfo;
extern const MIDL_STUBLESS_PROXY_INFO IDwgEntity_ProxyInfo;


extern const USER_MARSHAL_ROUTINE_QUADRUPLE UserMarshalRoutines[ WIRE_MARSHAL_TABLE_SIZE ];

#if !defined(__RPC_WIN32__)
#error  Invalid build platform for this stub.
#endif

#if !(TARGET_IS_NT50_OR_LATER)
#error You need Windows 2000 or later to run this stub because it uses these features:
#error   /robust command line switch.
#error However, your C/C++ compilation flags indicate you intend to run this app on earlier systems.
#error This app will fail with the RPC_X_WRONG_STUB_VERSION error.
#endif


static const HyDwgConvert_MIDL_PROC_FORMAT_STRING HyDwgConvert__MIDL_ProcFormatString =
    {
        0,
        {

	/* Procedure GetEntityCount */

			0x33,		/* FC_AUTO_HANDLE */
			0x6c,		/* Old Flags:  object, Oi2 */
/*  2 */	NdrFcLong( 0x0 ),	/* 0 */
/*  6 */	NdrFcShort( 0x7 ),	/* 7 */
/*  8 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 10 */	NdrFcShort( 0x0 ),	/* 0 */
/* 12 */	NdrFcShort( 0x24 ),	/* 36 */
/* 14 */	0x44,		/* Oi2 Flags:  has return, has ext, */
			0x2,		/* 2 */
/* 16 */	0x8,		/* 8 */
			0x1,		/* Ext Flags:  new corr desc, */
/* 18 */	NdrFcShort( 0x0 ),	/* 0 */
/* 20 */	NdrFcShort( 0x0 ),	/* 0 */
/* 22 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter count */

/* 24 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 26 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 28 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Return value */

/* 30 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 32 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 34 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure put_FileName */

/* 36 */	0x33,		/* FC_AUTO_HANDLE */
			0x6c,		/* Old Flags:  object, Oi2 */
/* 38 */	NdrFcLong( 0x0 ),	/* 0 */
/* 42 */	NdrFcShort( 0x8 ),	/* 8 */
/* 44 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 46 */	NdrFcShort( 0x0 ),	/* 0 */
/* 48 */	NdrFcShort( 0x8 ),	/* 8 */
/* 50 */	0x46,		/* Oi2 Flags:  clt must size, has return, has ext, */
			0x2,		/* 2 */
/* 52 */	0x8,		/* 8 */
			0x5,		/* Ext Flags:  new corr desc, srv corr check, */
/* 54 */	NdrFcShort( 0x0 ),	/* 0 */
/* 56 */	NdrFcShort( 0x1 ),	/* 1 */
/* 58 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter DwgFile */

/* 60 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 62 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 64 */	NdrFcShort( 0x24 ),	/* Type Offset=36 */

	/* Return value */

/* 66 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 68 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 70 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure Init */

/* 72 */	0x33,		/* FC_AUTO_HANDLE */
			0x6c,		/* Old Flags:  object, Oi2 */
/* 74 */	NdrFcLong( 0x0 ),	/* 0 */
/* 78 */	NdrFcShort( 0x9 ),	/* 9 */
/* 80 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 82 */	NdrFcShort( 0x0 ),	/* 0 */
/* 84 */	NdrFcShort( 0x22 ),	/* 34 */
/* 86 */	0x44,		/* Oi2 Flags:  has return, has ext, */
			0x2,		/* 2 */
/* 88 */	0x8,		/* 8 */
			0x1,		/* Ext Flags:  new corr desc, */
/* 90 */	NdrFcShort( 0x0 ),	/* 0 */
/* 92 */	NdrFcShort( 0x0 ),	/* 0 */
/* 94 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter succeed */

/* 96 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 98 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 100 */	0x6,		/* FC_SHORT */
			0x0,		/* 0 */

	/* Return value */

/* 102 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 104 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 106 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure Close */

/* 108 */	0x33,		/* FC_AUTO_HANDLE */
			0x6c,		/* Old Flags:  object, Oi2 */
/* 110 */	NdrFcLong( 0x0 ),	/* 0 */
/* 114 */	NdrFcShort( 0xa ),	/* 10 */
/* 116 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 118 */	NdrFcShort( 0x0 ),	/* 0 */
/* 120 */	NdrFcShort( 0x8 ),	/* 8 */
/* 122 */	0x44,		/* Oi2 Flags:  has return, has ext, */
			0x1,		/* 1 */
/* 124 */	0x8,		/* 8 */
			0x1,		/* Ext Flags:  new corr desc, */
/* 126 */	NdrFcShort( 0x0 ),	/* 0 */
/* 128 */	NdrFcShort( 0x0 ),	/* 0 */
/* 130 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Return value */

/* 132 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 134 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 136 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure get_GeometryType */

/* 138 */	0x33,		/* FC_AUTO_HANDLE */
			0x6c,		/* Old Flags:  object, Oi2 */
/* 140 */	NdrFcLong( 0x0 ),	/* 0 */
/* 144 */	NdrFcShort( 0x3 ),	/* 3 */
/* 146 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 148 */	NdrFcShort( 0x0 ),	/* 0 */
/* 150 */	NdrFcShort( 0x8 ),	/* 8 */
/* 152 */	0x45,		/* Oi2 Flags:  srv must size, has return, has ext, */
			0x2,		/* 2 */
/* 154 */	0x8,		/* 8 */
			0x3,		/* Ext Flags:  new corr desc, clt corr check, */
/* 156 */	NdrFcShort( 0x1 ),	/* 1 */
/* 158 */	NdrFcShort( 0x0 ),	/* 0 */
/* 160 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter pVal */

/* 162 */	NdrFcShort( 0x2113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=8 */
/* 164 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 166 */	NdrFcShort( 0x3a ),	/* Type Offset=58 */

	/* Return value */

/* 168 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 170 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 172 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure put_GeometryType */

/* 174 */	0x33,		/* FC_AUTO_HANDLE */
			0x6c,		/* Old Flags:  object, Oi2 */
/* 176 */	NdrFcLong( 0x0 ),	/* 0 */
/* 180 */	NdrFcShort( 0x4 ),	/* 4 */
/* 182 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 184 */	NdrFcShort( 0x0 ),	/* 0 */
/* 186 */	NdrFcShort( 0x8 ),	/* 8 */
/* 188 */	0x46,		/* Oi2 Flags:  clt must size, has return, has ext, */
			0x2,		/* 2 */
/* 190 */	0x8,		/* 8 */
			0x5,		/* Ext Flags:  new corr desc, srv corr check, */
/* 192 */	NdrFcShort( 0x0 ),	/* 0 */
/* 194 */	NdrFcShort( 0x1 ),	/* 1 */
/* 196 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter newVal */

/* 198 */	NdrFcShort( 0x8b ),	/* Flags:  must size, must free, in, by val, */
/* 200 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 202 */	NdrFcShort( 0x24 ),	/* Type Offset=36 */

	/* Return value */

/* 204 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 206 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 208 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure get_Color */

/* 210 */	0x33,		/* FC_AUTO_HANDLE */
			0x6c,		/* Old Flags:  object, Oi2 */
/* 212 */	NdrFcLong( 0x0 ),	/* 0 */
/* 216 */	NdrFcShort( 0x5 ),	/* 5 */
/* 218 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 220 */	NdrFcShort( 0x0 ),	/* 0 */
/* 222 */	NdrFcShort( 0x22 ),	/* 34 */
/* 224 */	0x44,		/* Oi2 Flags:  has return, has ext, */
			0x2,		/* 2 */
/* 226 */	0x8,		/* 8 */
			0x1,		/* Ext Flags:  new corr desc, */
/* 228 */	NdrFcShort( 0x0 ),	/* 0 */
/* 230 */	NdrFcShort( 0x0 ),	/* 0 */
/* 232 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter pVal */

/* 234 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 236 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 238 */	0x6,		/* FC_SHORT */
			0x0,		/* 0 */

	/* Return value */

/* 240 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 242 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 244 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure put_Color */

/* 246 */	0x33,		/* FC_AUTO_HANDLE */
			0x6c,		/* Old Flags:  object, Oi2 */
/* 248 */	NdrFcLong( 0x0 ),	/* 0 */
/* 252 */	NdrFcShort( 0x6 ),	/* 6 */
/* 254 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 256 */	NdrFcShort( 0x6 ),	/* 6 */
/* 258 */	NdrFcShort( 0x8 ),	/* 8 */
/* 260 */	0x44,		/* Oi2 Flags:  has return, has ext, */
			0x2,		/* 2 */
/* 262 */	0x8,		/* 8 */
			0x1,		/* Ext Flags:  new corr desc, */
/* 264 */	NdrFcShort( 0x0 ),	/* 0 */
/* 266 */	NdrFcShort( 0x0 ),	/* 0 */
/* 268 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter newVal */

/* 270 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 272 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 274 */	0x6,		/* FC_SHORT */
			0x0,		/* 0 */

	/* Return value */

/* 276 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 278 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 280 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure get_Handle */

/* 282 */	0x33,		/* FC_AUTO_HANDLE */
			0x6c,		/* Old Flags:  object, Oi2 */
/* 284 */	NdrFcLong( 0x0 ),	/* 0 */
/* 288 */	NdrFcShort( 0x7 ),	/* 7 */
/* 290 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 292 */	NdrFcShort( 0x0 ),	/* 0 */
/* 294 */	NdrFcShort( 0x8 ),	/* 8 */
/* 296 */	0x45,		/* Oi2 Flags:  srv must size, has return, has ext, */
			0x2,		/* 2 */
/* 298 */	0x8,		/* 8 */
			0x3,		/* Ext Flags:  new corr desc, clt corr check, */
/* 300 */	NdrFcShort( 0x1 ),	/* 1 */
/* 302 */	NdrFcShort( 0x0 ),	/* 0 */
/* 304 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter pVal */

/* 306 */	NdrFcShort( 0x2113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=8 */
/* 308 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 310 */	NdrFcShort( 0x3a ),	/* Type Offset=58 */

	/* Return value */

/* 312 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 314 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 316 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure put_Handle */

/* 318 */	0x33,		/* FC_AUTO_HANDLE */
			0x6c,		/* Old Flags:  object, Oi2 */
/* 320 */	NdrFcLong( 0x0 ),	/* 0 */
/* 324 */	NdrFcShort( 0x8 ),	/* 8 */
/* 326 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 328 */	NdrFcShort( 0x0 ),	/* 0 */
/* 330 */	NdrFcShort( 0x8 ),	/* 8 */
/* 332 */	0x46,		/* Oi2 Flags:  clt must size, has return, has ext, */
			0x2,		/* 2 */
/* 334 */	0x8,		/* 8 */
			0x5,		/* Ext Flags:  new corr desc, srv corr check, */
/* 336 */	NdrFcShort( 0x0 ),	/* 0 */
/* 338 */	NdrFcShort( 0x1 ),	/* 1 */
/* 340 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter newVal */

/* 342 */	NdrFcShort( 0x8b ),	/* Flags:  must size, must free, in, by val, */
/* 344 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 346 */	NdrFcShort( 0x24 ),	/* Type Offset=36 */

	/* Return value */

/* 348 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 350 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 352 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

			0x0
        }
    };

static const HyDwgConvert_MIDL_TYPE_FORMAT_STRING HyDwgConvert__MIDL_TypeFormatString =
    {
        0,
        {
			NdrFcShort( 0x0 ),	/* 0 */
/*  2 */	
			0x11, 0xc,	/* FC_RP [alloced_on_stack] [simple_pointer] */
/*  4 */	0x8,		/* FC_LONG */
			0x5c,		/* FC_PAD */
/*  6 */	
			0x11, 0x0,	/* FC_RP */
/*  8 */	NdrFcShort( 0x1c ),	/* Offset= 28 (36) */
/* 10 */	
			0x12, 0x0,	/* FC_UP */
/* 12 */	NdrFcShort( 0xe ),	/* Offset= 14 (26) */
/* 14 */	
			0x1b,		/* FC_CARRAY */
			0x1,		/* 1 */
/* 16 */	NdrFcShort( 0x2 ),	/* 2 */
/* 18 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 20 */	NdrFcShort( 0xfffc ),	/* -4 */
/* 22 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 24 */	0x6,		/* FC_SHORT */
			0x5b,		/* FC_END */
/* 26 */	
			0x17,		/* FC_CSTRUCT */
			0x3,		/* 3 */
/* 28 */	NdrFcShort( 0x8 ),	/* 8 */
/* 30 */	NdrFcShort( 0xfff0 ),	/* Offset= -16 (14) */
/* 32 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 34 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 36 */	0xb4,		/* FC_USER_MARSHAL */
			0x83,		/* 131 */
/* 38 */	NdrFcShort( 0x0 ),	/* 0 */
/* 40 */	NdrFcShort( 0x4 ),	/* 4 */
/* 42 */	NdrFcShort( 0x0 ),	/* 0 */
/* 44 */	NdrFcShort( 0xffde ),	/* Offset= -34 (10) */
/* 46 */	
			0x11, 0xc,	/* FC_RP [alloced_on_stack] [simple_pointer] */
/* 48 */	0x6,		/* FC_SHORT */
			0x5c,		/* FC_PAD */
/* 50 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 52 */	NdrFcShort( 0x6 ),	/* Offset= 6 (58) */
/* 54 */	
			0x13, 0x0,	/* FC_OP */
/* 56 */	NdrFcShort( 0xffe2 ),	/* Offset= -30 (26) */
/* 58 */	0xb4,		/* FC_USER_MARSHAL */
			0x83,		/* 131 */
/* 60 */	NdrFcShort( 0x0 ),	/* 0 */
/* 62 */	NdrFcShort( 0x4 ),	/* 4 */
/* 64 */	NdrFcShort( 0x0 ),	/* 0 */
/* 66 */	NdrFcShort( 0xfff4 ),	/* Offset= -12 (54) */

			0x0
        }
    };

static const USER_MARSHAL_ROUTINE_QUADRUPLE UserMarshalRoutines[ WIRE_MARSHAL_TABLE_SIZE ] = 
        {
            
            {
            BSTR_UserSize
            ,BSTR_UserMarshal
            ,BSTR_UserUnmarshal
            ,BSTR_UserFree
            }

        };



/* Object interface: IUnknown, ver. 0.0,
   GUID={0x00000000,0x0000,0x0000,{0xC0,0x00,0x00,0x00,0x00,0x00,0x00,0x46}} */


/* Object interface: IDispatch, ver. 0.0,
   GUID={0x00020400,0x0000,0x0000,{0xC0,0x00,0x00,0x00,0x00,0x00,0x00,0x46}} */


/* Object interface: IDwgReader, ver. 0.0,
   GUID={0x11212A91,0xCAD7,0x40B4,{0xA3,0x22,0x9F,0x89,0x47,0x28,0xC1,0x61}} */

#pragma code_seg(".orpc")
static const unsigned short IDwgReader_FormatStringOffsetTable[] =
    {
    (unsigned short) -1,
    (unsigned short) -1,
    (unsigned short) -1,
    (unsigned short) -1,
    0,
    36,
    72,
    108
    };

static const MIDL_STUBLESS_PROXY_INFO IDwgReader_ProxyInfo =
    {
    &Object_StubDesc,
    HyDwgConvert__MIDL_ProcFormatString.Format,
    &IDwgReader_FormatStringOffsetTable[-3],
    0,
    0,
    0
    };


static const MIDL_SERVER_INFO IDwgReader_ServerInfo = 
    {
    &Object_StubDesc,
    0,
    HyDwgConvert__MIDL_ProcFormatString.Format,
    &IDwgReader_FormatStringOffsetTable[-3],
    0,
    0,
    0,
    0};
CINTERFACE_PROXY_VTABLE(11) _IDwgReaderProxyVtbl = 
{
    &IDwgReader_ProxyInfo,
    &IID_IDwgReader,
    IUnknown_QueryInterface_Proxy,
    IUnknown_AddRef_Proxy,
    IUnknown_Release_Proxy ,
    0 /* IDispatch::GetTypeInfoCount */ ,
    0 /* IDispatch::GetTypeInfo */ ,
    0 /* IDispatch::GetIDsOfNames */ ,
    0 /* IDispatch_Invoke_Proxy */ ,
    (void *) (INT_PTR) -1 /* IDwgReader::GetEntityCount */ ,
    (void *) (INT_PTR) -1 /* IDwgReader::put_FileName */ ,
    (void *) (INT_PTR) -1 /* IDwgReader::Init */ ,
    (void *) (INT_PTR) -1 /* IDwgReader::Close */
};


static const PRPC_STUB_FUNCTION IDwgReader_table[] =
{
    STUB_FORWARDING_FUNCTION,
    STUB_FORWARDING_FUNCTION,
    STUB_FORWARDING_FUNCTION,
    STUB_FORWARDING_FUNCTION,
    NdrStubCall2,
    NdrStubCall2,
    NdrStubCall2,
    NdrStubCall2
};

CInterfaceStubVtbl _IDwgReaderStubVtbl =
{
    &IID_IDwgReader,
    &IDwgReader_ServerInfo,
    11,
    &IDwgReader_table[-3],
    CStdStubBuffer_DELEGATING_METHODS
};


/* Object interface: IDwgEntity, ver. 0.0,
   GUID={0x82B362A4,0x7827,0x4439,{0xB4,0x3C,0xFC,0x94,0x71,0x92,0x6C,0xF2}} */

#pragma code_seg(".orpc")
static const unsigned short IDwgEntity_FormatStringOffsetTable[] =
    {
    138,
    174,
    210,
    246,
    282,
    318
    };

static const MIDL_STUBLESS_PROXY_INFO IDwgEntity_ProxyInfo =
    {
    &Object_StubDesc,
    HyDwgConvert__MIDL_ProcFormatString.Format,
    &IDwgEntity_FormatStringOffsetTable[-3],
    0,
    0,
    0
    };


static const MIDL_SERVER_INFO IDwgEntity_ServerInfo = 
    {
    &Object_StubDesc,
    0,
    HyDwgConvert__MIDL_ProcFormatString.Format,
    &IDwgEntity_FormatStringOffsetTable[-3],
    0,
    0,
    0,
    0};
CINTERFACE_PROXY_VTABLE(9) _IDwgEntityProxyVtbl = 
{
    &IDwgEntity_ProxyInfo,
    &IID_IDwgEntity,
    IUnknown_QueryInterface_Proxy,
    IUnknown_AddRef_Proxy,
    IUnknown_Release_Proxy ,
    (void *) (INT_PTR) -1 /* IDwgEntity::get_GeometryType */ ,
    (void *) (INT_PTR) -1 /* IDwgEntity::put_GeometryType */ ,
    (void *) (INT_PTR) -1 /* IDwgEntity::get_Color */ ,
    (void *) (INT_PTR) -1 /* IDwgEntity::put_Color */ ,
    (void *) (INT_PTR) -1 /* IDwgEntity::get_Handle */ ,
    (void *) (INT_PTR) -1 /* IDwgEntity::put_Handle */
};

const CInterfaceStubVtbl _IDwgEntityStubVtbl =
{
    &IID_IDwgEntity,
    &IDwgEntity_ServerInfo,
    9,
    0, /* pure interpreted */
    CStdStubBuffer_METHODS
};

static const MIDL_STUB_DESC Object_StubDesc = 
    {
    0,
    NdrOleAllocate,
    NdrOleFree,
    0,
    0,
    0,
    0,
    0,
    HyDwgConvert__MIDL_TypeFormatString.Format,
    1, /* -error bounds_check flag */
    0x50002, /* Ndr library version */
    0,
    0x700022b, /* MIDL Version 7.0.555 */
    0,
    UserMarshalRoutines,
    0,  /* notify & notify_flag routine table */
    0x1, /* MIDL flag */
    0, /* cs routines */
    0,   /* proxy/server info */
    0
    };

const CInterfaceProxyVtbl * const _HyDwgConvert_ProxyVtblList[] = 
{
    ( CInterfaceProxyVtbl *) &_IDwgReaderProxyVtbl,
    ( CInterfaceProxyVtbl *) &_IDwgEntityProxyVtbl,
    0
};

const CInterfaceStubVtbl * const _HyDwgConvert_StubVtblList[] = 
{
    ( CInterfaceStubVtbl *) &_IDwgReaderStubVtbl,
    ( CInterfaceStubVtbl *) &_IDwgEntityStubVtbl,
    0
};

PCInterfaceName const _HyDwgConvert_InterfaceNamesList[] = 
{
    "IDwgReader",
    "IDwgEntity",
    0
};

const IID *  const _HyDwgConvert_BaseIIDList[] = 
{
    &IID_IDispatch,
    0,
    0
};


#define _HyDwgConvert_CHECK_IID(n)	IID_GENERIC_CHECK_IID( _HyDwgConvert, pIID, n)

int __stdcall _HyDwgConvert_IID_Lookup( const IID * pIID, int * pIndex )
{
    IID_BS_LOOKUP_SETUP

    IID_BS_LOOKUP_INITIAL_TEST( _HyDwgConvert, 2, 1 )
    IID_BS_LOOKUP_RETURN_RESULT( _HyDwgConvert, 2, *pIndex )
    
}

const ExtendedProxyFileInfo HyDwgConvert_ProxyFileInfo = 
{
    (PCInterfaceProxyVtblList *) & _HyDwgConvert_ProxyVtblList,
    (PCInterfaceStubVtblList *) & _HyDwgConvert_StubVtblList,
    (const PCInterfaceName * ) & _HyDwgConvert_InterfaceNamesList,
    (const IID ** ) & _HyDwgConvert_BaseIIDList,
    & _HyDwgConvert_IID_Lookup, 
    2,
    2,
    0, /* table of [async_uuid] interfaces */
    0, /* Filler1 */
    0, /* Filler2 */
    0  /* Filler3 */
};
#pragma optimize("", on )
#if _MSC_VER >= 1200
#pragma warning(pop)
#endif


#endif /* !defined(_M_IA64) && !defined(_M_AMD64)*/

