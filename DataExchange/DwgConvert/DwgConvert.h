

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0555 */
/* at Thu Jun 20 23:18:40 2013
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


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 440
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __DwgConvert_h__
#define __DwgConvert_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IDwgReader_FWD_DEFINED__
#define __IDwgReader_FWD_DEFINED__
typedef interface IDwgReader IDwgReader;
#endif 	/* __IDwgReader_FWD_DEFINED__ */


#ifndef __IDwgWriter_FWD_DEFINED__
#define __IDwgWriter_FWD_DEFINED__
typedef interface IDwgWriter IDwgWriter;
#endif 	/* __IDwgWriter_FWD_DEFINED__ */


#ifndef __DwgWriter_FWD_DEFINED__
#define __DwgWriter_FWD_DEFINED__

#ifdef __cplusplus
typedef class DwgWriter DwgWriter;
#else
typedef struct DwgWriter DwgWriter;
#endif /* __cplusplus */

#endif 	/* __DwgWriter_FWD_DEFINED__ */


#ifndef __DwgReader_FWD_DEFINED__
#define __DwgReader_FWD_DEFINED__

#ifdef __cplusplus
typedef class DwgReader DwgReader;
#else
typedef struct DwgReader DwgReader;
#endif /* __cplusplus */

#endif 	/* __DwgReader_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __DwgConvertLib_LIBRARY_DEFINED__
#define __DwgConvertLib_LIBRARY_DEFINED__

/* library DwgConvertLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_DwgConvertLib;

#ifndef __IDwgReader_INTERFACE_DEFINED__
#define __IDwgReader_INTERFACE_DEFINED__

/* interface IDwgReader */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IDwgReader;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("9F1A9E88-4B96-4F8C-8CE2-1A07041DA3EA")
    IDwgReader : public IDispatch
    {
    public:
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_BreakBlock( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_BreakBlock( 
            /* [in] */ VARIANT_BOOL newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ReadInvisible( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_ReadInvisible( 
            /* [in] */ VARIANT_BOOL newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ReadPolygon( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_ReadPolygon( 
            /* [in] */ VARIANT_BOOL newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Line2Polygon( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Line2Polygon( 
            /* [in] */ VARIANT_BOOL newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ReadBlockPoint( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_ReadBlockPoint( 
            /* [in] */ VARIANT_BOOL newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_JoinXDataAttrib( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_JoinXDataAttrib( 
            /* [in] */ VARIANT_BOOL newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_XDataRegAppNames( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_XDataRegAppNames( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_AnnoScale( 
            /* [retval][out] */ SHORT *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_AnnoScale( 
            /* [in] */ SHORT newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_UnBreakBlocks( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_UnBreakBlocks( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE InitReadDwg( 
            /* [in] */ /* external definition not present */ IWorkspace *targetGDB,
            /* [in] */ /* external definition not present */ ISpatialReference *spRef) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ReadDwgFile( 
            /* [in] */ BSTR sDwgFile) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Close( void) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_LogFilePath( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_LogFilePath( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ParentHandle( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_ParentHandle( 
            /* [in] */ LONG newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_CreateAnnotation( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_CreateAnnotation( 
            /* [in] */ VARIANT_BOOL newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_UnbreakblockMode( 
            /* [retval][out] */ SHORT *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_UnbreakblockMode( 
            /* [in] */ SHORT newVal) = 0;
        
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
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_BreakBlock )( 
            IDwgReader * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_BreakBlock )( 
            IDwgReader * This,
            /* [in] */ VARIANT_BOOL newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ReadInvisible )( 
            IDwgReader * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ReadInvisible )( 
            IDwgReader * This,
            /* [in] */ VARIANT_BOOL newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ReadPolygon )( 
            IDwgReader * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ReadPolygon )( 
            IDwgReader * This,
            /* [in] */ VARIANT_BOOL newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Line2Polygon )( 
            IDwgReader * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Line2Polygon )( 
            IDwgReader * This,
            /* [in] */ VARIANT_BOOL newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ReadBlockPoint )( 
            IDwgReader * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ReadBlockPoint )( 
            IDwgReader * This,
            /* [in] */ VARIANT_BOOL newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_JoinXDataAttrib )( 
            IDwgReader * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_JoinXDataAttrib )( 
            IDwgReader * This,
            /* [in] */ VARIANT_BOOL newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_XDataRegAppNames )( 
            IDwgReader * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_XDataRegAppNames )( 
            IDwgReader * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_AnnoScale )( 
            IDwgReader * This,
            /* [retval][out] */ SHORT *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_AnnoScale )( 
            IDwgReader * This,
            /* [in] */ SHORT newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_UnBreakBlocks )( 
            IDwgReader * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_UnBreakBlocks )( 
            IDwgReader * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *InitReadDwg )( 
            IDwgReader * This,
            /* [in] */ /* external definition not present */ IWorkspace *targetGDB,
            /* [in] */ /* external definition not present */ ISpatialReference *spRef);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *ReadDwgFile )( 
            IDwgReader * This,
            /* [in] */ BSTR sDwgFile);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Close )( 
            IDwgReader * This);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_LogFilePath )( 
            IDwgReader * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_LogFilePath )( 
            IDwgReader * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ParentHandle )( 
            IDwgReader * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ParentHandle )( 
            IDwgReader * This,
            /* [in] */ LONG newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_CreateAnnotation )( 
            IDwgReader * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_CreateAnnotation )( 
            IDwgReader * This,
            /* [in] */ VARIANT_BOOL newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_UnbreakblockMode )( 
            IDwgReader * This,
            /* [retval][out] */ SHORT *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_UnbreakblockMode )( 
            IDwgReader * This,
            /* [in] */ SHORT newVal);
        
        END_INTERFACE
    } IDwgReaderVtbl;

    interface IDwgReader
    {
        CONST_VTBL struct IDwgReaderVtbl *lpVtbl;
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


#define IDwgReader_get_BreakBlock(This,pVal)	\
    ( (This)->lpVtbl -> get_BreakBlock(This,pVal) ) 

#define IDwgReader_put_BreakBlock(This,newVal)	\
    ( (This)->lpVtbl -> put_BreakBlock(This,newVal) ) 

#define IDwgReader_get_ReadInvisible(This,pVal)	\
    ( (This)->lpVtbl -> get_ReadInvisible(This,pVal) ) 

#define IDwgReader_put_ReadInvisible(This,newVal)	\
    ( (This)->lpVtbl -> put_ReadInvisible(This,newVal) ) 

#define IDwgReader_get_ReadPolygon(This,pVal)	\
    ( (This)->lpVtbl -> get_ReadPolygon(This,pVal) ) 

#define IDwgReader_put_ReadPolygon(This,newVal)	\
    ( (This)->lpVtbl -> put_ReadPolygon(This,newVal) ) 

#define IDwgReader_get_Line2Polygon(This,pVal)	\
    ( (This)->lpVtbl -> get_Line2Polygon(This,pVal) ) 

#define IDwgReader_put_Line2Polygon(This,newVal)	\
    ( (This)->lpVtbl -> put_Line2Polygon(This,newVal) ) 

#define IDwgReader_get_ReadBlockPoint(This,pVal)	\
    ( (This)->lpVtbl -> get_ReadBlockPoint(This,pVal) ) 

#define IDwgReader_put_ReadBlockPoint(This,newVal)	\
    ( (This)->lpVtbl -> put_ReadBlockPoint(This,newVal) ) 

#define IDwgReader_get_JoinXDataAttrib(This,pVal)	\
    ( (This)->lpVtbl -> get_JoinXDataAttrib(This,pVal) ) 

#define IDwgReader_put_JoinXDataAttrib(This,newVal)	\
    ( (This)->lpVtbl -> put_JoinXDataAttrib(This,newVal) ) 

#define IDwgReader_get_XDataRegAppNames(This,pVal)	\
    ( (This)->lpVtbl -> get_XDataRegAppNames(This,pVal) ) 

#define IDwgReader_put_XDataRegAppNames(This,newVal)	\
    ( (This)->lpVtbl -> put_XDataRegAppNames(This,newVal) ) 

#define IDwgReader_get_AnnoScale(This,pVal)	\
    ( (This)->lpVtbl -> get_AnnoScale(This,pVal) ) 

#define IDwgReader_put_AnnoScale(This,newVal)	\
    ( (This)->lpVtbl -> put_AnnoScale(This,newVal) ) 

#define IDwgReader_get_UnBreakBlocks(This,pVal)	\
    ( (This)->lpVtbl -> get_UnBreakBlocks(This,pVal) ) 

#define IDwgReader_put_UnBreakBlocks(This,newVal)	\
    ( (This)->lpVtbl -> put_UnBreakBlocks(This,newVal) ) 

#define IDwgReader_InitReadDwg(This,targetGDB,spRef)	\
    ( (This)->lpVtbl -> InitReadDwg(This,targetGDB,spRef) ) 

#define IDwgReader_ReadDwgFile(This,sDwgFile)	\
    ( (This)->lpVtbl -> ReadDwgFile(This,sDwgFile) ) 

#define IDwgReader_Close(This)	\
    ( (This)->lpVtbl -> Close(This) ) 

#define IDwgReader_get_LogFilePath(This,pVal)	\
    ( (This)->lpVtbl -> get_LogFilePath(This,pVal) ) 

#define IDwgReader_put_LogFilePath(This,newVal)	\
    ( (This)->lpVtbl -> put_LogFilePath(This,newVal) ) 

#define IDwgReader_get_ParentHandle(This,pVal)	\
    ( (This)->lpVtbl -> get_ParentHandle(This,pVal) ) 

#define IDwgReader_put_ParentHandle(This,newVal)	\
    ( (This)->lpVtbl -> put_ParentHandle(This,newVal) ) 

#define IDwgReader_get_CreateAnnotation(This,pVal)	\
    ( (This)->lpVtbl -> get_CreateAnnotation(This,pVal) ) 

#define IDwgReader_put_CreateAnnotation(This,newVal)	\
    ( (This)->lpVtbl -> put_CreateAnnotation(This,newVal) ) 

#define IDwgReader_get_UnbreakblockMode(This,pVal)	\
    ( (This)->lpVtbl -> get_UnbreakblockMode(This,pVal) ) 

#define IDwgReader_put_UnbreakblockMode(This,newVal)	\
    ( (This)->lpVtbl -> put_UnbreakblockMode(This,newVal) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IDwgReader_get_CreateAnnotation_Proxy( 
    IDwgReader * This,
    /* [retval][out] */ VARIANT_BOOL *pVal);


void __RPC_STUB IDwgReader_get_CreateAnnotation_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IDwgReader_put_CreateAnnotation_Proxy( 
    IDwgReader * This,
    /* [in] */ VARIANT_BOOL newVal);


void __RPC_STUB IDwgReader_put_CreateAnnotation_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IDwgReader_get_UnbreakblockMode_Proxy( 
    IDwgReader * This,
    /* [retval][out] */ SHORT *pVal);


void __RPC_STUB IDwgReader_get_UnbreakblockMode_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IDwgReader_put_UnbreakblockMode_Proxy( 
    IDwgReader * This,
    /* [in] */ SHORT newVal);


void __RPC_STUB IDwgReader_put_UnbreakblockMode_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IDwgReader_INTERFACE_DEFINED__ */


#ifndef __IDwgWriter_INTERFACE_DEFINED__
#define __IDwgWriter_INTERFACE_DEFINED__

/* interface IDwgWriter */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IDwgWriter;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("8C1C207C-D53F-4965-8DEF-223AA1EBE0EC")
    IDwgWriter : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE InitWriteDwg( 
            /* [in] */ BSTR sDwgFile,
            /* [in] */ BSTR sTemplateFile) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FeatureClass2Dwgfile( 
            /* [in] */ /* external definition not present */ IFeatureClass *pFtCls) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SetCompareTable( 
            /* [in] */ BSTR sCompareField,
            /* [in] */ /* external definition not present */ ITable *pCompareTable) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_XDataXMLConfigFile( 
            /* [in] */ BSTR sConfigFile) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_LogFilePath( 
            /* [in] */ BSTR sLogFile) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Close( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Cursor2Dwgfile( 
            /* [in] */ BSTR sFeatureClass,
            /* [in] */ /* external definition not present */ IFeatureCursor *pFtCur,
            /* [in] */ LONG numFeatures) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Cursor2DwgLayer( 
            /* [in] */ BSTR sFeatureClass,
            /* [in] */ /* external definition not present */ IFeatureCursor *pFtCur,
            LONG numFeatures,
            /* [in] */ BSTR sDwgLayer) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SetCompareField2( 
            /* [in] */ BSTR sConfigField,
            /* [in] */ BSTR sGdbField) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IDwgWriterVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IDwgWriter * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IDwgWriter * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IDwgWriter * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IDwgWriter * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IDwgWriter * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IDwgWriter * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IDwgWriter * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *InitWriteDwg )( 
            IDwgWriter * This,
            /* [in] */ BSTR sDwgFile,
            /* [in] */ BSTR sTemplateFile);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *FeatureClass2Dwgfile )( 
            IDwgWriter * This,
            /* [in] */ /* external definition not present */ IFeatureClass *pFtCls);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SetCompareTable )( 
            IDwgWriter * This,
            /* [in] */ BSTR sCompareField,
            /* [in] */ /* external definition not present */ ITable *pCompareTable);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_XDataXMLConfigFile )( 
            IDwgWriter * This,
            /* [in] */ BSTR sConfigFile);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_LogFilePath )( 
            IDwgWriter * This,
            /* [in] */ BSTR sLogFile);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Close )( 
            IDwgWriter * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Cursor2Dwgfile )( 
            IDwgWriter * This,
            /* [in] */ BSTR sFeatureClass,
            /* [in] */ /* external definition not present */ IFeatureCursor *pFtCur,
            /* [in] */ LONG numFeatures);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Cursor2DwgLayer )( 
            IDwgWriter * This,
            /* [in] */ BSTR sFeatureClass,
            /* [in] */ /* external definition not present */ IFeatureCursor *pFtCur,
            LONG numFeatures,
            /* [in] */ BSTR sDwgLayer);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SetCompareField2 )( 
            IDwgWriter * This,
            /* [in] */ BSTR sConfigField,
            /* [in] */ BSTR sGdbField);
        
        END_INTERFACE
    } IDwgWriterVtbl;

    interface IDwgWriter
    {
        CONST_VTBL struct IDwgWriterVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IDwgWriter_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IDwgWriter_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IDwgWriter_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IDwgWriter_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IDwgWriter_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IDwgWriter_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IDwgWriter_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IDwgWriter_InitWriteDwg(This,sDwgFile,sTemplateFile)	\
    ( (This)->lpVtbl -> InitWriteDwg(This,sDwgFile,sTemplateFile) ) 

#define IDwgWriter_FeatureClass2Dwgfile(This,pFtCls)	\
    ( (This)->lpVtbl -> FeatureClass2Dwgfile(This,pFtCls) ) 

#define IDwgWriter_SetCompareTable(This,sCompareField,pCompareTable)	\
    ( (This)->lpVtbl -> SetCompareTable(This,sCompareField,pCompareTable) ) 

#define IDwgWriter_put_XDataXMLConfigFile(This,sConfigFile)	\
    ( (This)->lpVtbl -> put_XDataXMLConfigFile(This,sConfigFile) ) 

#define IDwgWriter_put_LogFilePath(This,sLogFile)	\
    ( (This)->lpVtbl -> put_LogFilePath(This,sLogFile) ) 

#define IDwgWriter_Close(This)	\
    ( (This)->lpVtbl -> Close(This) ) 

#define IDwgWriter_Cursor2Dwgfile(This,sFeatureClass,pFtCur,numFeatures)	\
    ( (This)->lpVtbl -> Cursor2Dwgfile(This,sFeatureClass,pFtCur,numFeatures) ) 

#define IDwgWriter_Cursor2DwgLayer(This,sFeatureClass,pFtCur,numFeatures,sDwgLayer)	\
    ( (This)->lpVtbl -> Cursor2DwgLayer(This,sFeatureClass,pFtCur,numFeatures,sDwgLayer) ) 

#define IDwgWriter_SetCompareField2(This,sConfigField,sGdbField)	\
    ( (This)->lpVtbl -> SetCompareField2(This,sConfigField,sGdbField) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IDwgWriter_INTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_DwgWriter;

#ifdef __cplusplus

class DECLSPEC_UUID("BE2255C9-F951-416F-9E71-BF0E42995C55")
DwgWriter;
#endif

EXTERN_C const CLSID CLSID_DwgReader;

#ifdef __cplusplus

class DECLSPEC_UUID("B3665367-4710-4C99-A375-802CDC116EA1")
DwgReader;
#endif
#endif /* __DwgConvertLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


