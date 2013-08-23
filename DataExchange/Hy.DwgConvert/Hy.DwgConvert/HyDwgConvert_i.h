

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


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


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __HyDwgConvert_i_h__
#define __HyDwgConvert_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IDwgReader_FWD_DEFINED__
#define __IDwgReader_FWD_DEFINED__
typedef interface IDwgReader IDwgReader;
#endif 	/* __IDwgReader_FWD_DEFINED__ */


#ifndef __IDwgEntity_FWD_DEFINED__
#define __IDwgEntity_FWD_DEFINED__
typedef interface IDwgEntity IDwgEntity;
#endif 	/* __IDwgEntity_FWD_DEFINED__ */


#ifndef __DwgReader_FWD_DEFINED__
#define __DwgReader_FWD_DEFINED__

#ifdef __cplusplus
typedef class DwgReader DwgReader;
#else
typedef struct DwgReader DwgReader;
#endif /* __cplusplus */

#endif 	/* __DwgReader_FWD_DEFINED__ */


#ifndef __DwgEntity_FWD_DEFINED__
#define __DwgEntity_FWD_DEFINED__

#ifdef __cplusplus
typedef class DwgEntity DwgEntity;
#else
typedef struct DwgEntity DwgEntity;
#endif /* __cplusplus */

#endif 	/* __DwgEntity_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IDwgReader_INTERFACE_DEFINED__
#define __IDwgReader_INTERFACE_DEFINED__

/* interface IDwgReader */
/* [unique][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IDwgReader;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("11212A91-CAD7-40B4-A322-9F894728C161")
    IDwgReader : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetEntityCount( 
            /* [retval][out] */ LONG *count) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_FileName( 
            /* [in] */ BSTR *DwgFile) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Init( 
            /* [retval][out] */ VARIANT_BOOL *succeed) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Close( void) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IDwgReaderVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IDwgReader * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IDwgReader * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IDwgReader * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IDwgReader * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IDwgReader * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IDwgReader * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IDwgReader * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetEntityCount )( 
            IDwgReader * This,
            /* [retval][out] */ LONG *count);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_FileName )( 
            IDwgReader * This,
            /* [in] */ BSTR *DwgFile);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Init )( 
            IDwgReader * This,
            /* [retval][out] */ VARIANT_BOOL *succeed);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Close )( 
            IDwgReader * This);
        
        END_INTERFACE
    } IDwgReaderVtbl;

    interface IDwgReader
    {
        CONST_VTBL struct IDwgReaderVtbl *lpVtbl;
		[, helpstring("进行一次实体读取，读取器指向下一个实体位置，返回一个实体，结尾返回null")] HRESULT Read([out,retval] IDwgEntity* curEntity);
	};

    

#ifdef COBJMACROS


#define IDwgReader_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IDwgReader_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IDwgReader_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IDwgReader_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IDwgReader_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IDwgReader_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IDwgReader_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IDwgReader_GetEntityCount(This,count)	\
    ( (This)->lpVtbl -> GetEntityCount(This,count) ) 

#define IDwgReader_put_FileName(This,DwgFile)	\
    ( (This)->lpVtbl -> put_FileName(This,DwgFile) ) 

#define IDwgReader_Init(This,succeed)	\
    ( (This)->lpVtbl -> Init(This,succeed) ) 

#define IDwgReader_Close(This)	\
    ( (This)->lpVtbl -> Close(This) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IDwgReader_INTERFACE_DEFINED__ */


#ifndef __IDwgEntity_INTERFACE_DEFINED__
#define __IDwgEntity_INTERFACE_DEFINED__

/* interface IDwgEntity */
/* [unique][uuid][object] */ 


EXTERN_C const IID IID_IDwgEntity;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("82B362A4-7827-4439-B43C-FC9471926CF2")
    IDwgEntity : public IUnknown
    {
    public:
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_GeometryType( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_GeometryType( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_Color( 
            /* [retval][out] */ USHORT *pVal) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_Color( 
            /* [in] */ USHORT newVal) = 0;
        
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_Handle( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_Handle( 
            /* [in] */ BSTR newVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IDwgEntityVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IDwgEntity * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IDwgEntity * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IDwgEntity * This);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE *get_GeometryType )( 
            IDwgEntity * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE *put_GeometryType )( 
            IDwgEntity * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Color )( 
            IDwgEntity * This,
            /* [retval][out] */ USHORT *pVal);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Color )( 
            IDwgEntity * This,
            /* [in] */ USHORT newVal);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Handle )( 
            IDwgEntity * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Handle )( 
            IDwgEntity * This,
            /* [in] */ BSTR newVal);
        
        END_INTERFACE
    } IDwgEntityVtbl;

    interface IDwgEntity
    {
        CONST_VTBL struct IDwgEntityVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IDwgEntity_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IDwgEntity_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IDwgEntity_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IDwgEntity_get_GeometryType(This,pVal)	\
    ( (This)->lpVtbl -> get_GeometryType(This,pVal) ) 

#define IDwgEntity_put_GeometryType(This,newVal)	\
    ( (This)->lpVtbl -> put_GeometryType(This,newVal) ) 

#define IDwgEntity_get_Color(This,pVal)	\
    ( (This)->lpVtbl -> get_Color(This,pVal) ) 

#define IDwgEntity_put_Color(This,newVal)	\
    ( (This)->lpVtbl -> put_Color(This,newVal) ) 

#define IDwgEntity_get_Handle(This,pVal)	\
    ( (This)->lpVtbl -> get_Handle(This,pVal) ) 

#define IDwgEntity_put_Handle(This,newVal)	\
    ( (This)->lpVtbl -> put_Handle(This,newVal) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IDwgEntity_INTERFACE_DEFINED__ */



#ifndef __HyDwgConvert_LIBRARY_DEFINED__
#define __HyDwgConvert_LIBRARY_DEFINED__

/* library HyDwgConvert */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_HyDwgConvert;

EXTERN_C const CLSID CLSID_DwgReader;

#ifdef __cplusplus

class DECLSPEC_UUID("847B149E-3CC9-4F57-BB11-AA412E66C420")
DwgReader;
#endif

EXTERN_C const CLSID CLSID_DwgEntity;

#ifdef __cplusplus

class DECLSPEC_UUID("CEE471D4-E47D-489D-A699-6C2D807B1A74")
DwgEntity;
#endif
#endif /* __HyDwgConvert_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long *, BSTR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


