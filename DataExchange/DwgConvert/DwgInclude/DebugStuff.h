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



#ifndef __DEBUG_STUFF__INCLUDED
#define __DEBUG_STUFF__INCLUDED

#ifdef _DEBUG
#define ODA_DIAGNOSTICS
#endif // _DEBUG

#ifdef ODA_DIAGNOSTICS

void FIRSTDLL_EXPORT OdAssert(const OdChar* expresssion, const OdChar* fileName, int nLineNo);
void FIRSTDLL_EXPORT OdTrace(const OdChar* szFormat, ...);

#define ODA_ASSERT(exp) (void)( (exp) || (OdAssert(#exp, __FILE__, __LINE__), 0) )
#define ODA_VERIFY(exp) ODA_ASSERT(exp)
#define ODA_ASSERT_ONCE(exp)\
{ static bool was_here = false;\
  if (!was_here && !(exp))\
  { was_here = true;\
    OdAssert(#exp, __FILE__, __LINE__);\
  }\
}
#define ODA_FAIL() OdAssert("Invalid Execution.", __FILE__, __LINE__)
#define ODA_FAIL_ONCE()\
{ static bool was_here = false;\
  if (!was_here)\
  { was_here = true;\
    OdAssert("Invalid Execution.", __FILE__, __LINE__);\
  }\
}

#else // ODA_DIAGNOSTICS

#define ODA_ASSERT(condition) 
#define ODA_VERIFY(condition)         (void)(condition)
#define ODA_ASSERT_ONCE(condition)
#define ODA_FAIL() 
#define ODA_FAIL_ONCE() 

#define ODA_NON_TRACING

#endif // ODA_DIAGNOSTICS

#ifndef ODA_NON_TRACING
  #define ODA_TRACE OdTrace
  #define ODA_TRACE0(szFormat) OdTrace(szFormat)
  #define ODA_TRACE1(szFormat, param1) OdTrace(szFormat, param1)
  #define ODA_TRACE2(szFormat, param1, param2) OdTrace(szFormat, param1, param2)
  #define ODA_TRACE3(szFormat, param1, param2, param3) OdTrace(szFormat, param1, param2, param3)
#else // ODA_NON_TRACING
  #define ODA_TRACE
  #define ODA_TRACE0(szFormat) 
  #define ODA_TRACE1(szFormat, param1) 
  #define ODA_TRACE2(szFormat, param1, param2) 
  #define ODA_TRACE3(szFormat, param1, param2, param3) 
#endif // ODA_NON_TRACING

// Use this macro to perform compilation time check.
// For example:   ODA_ASSUME(sizeof(double) == 8)
#ifdef _DEBUG
#define ODA_ASSUME(expr)  /*FIRSTDLL_EXPORT*/ extern char OdaAssumeArray[expr];
#else
#define ODA_ASSUME(expr)
#endif

#if defined(_MSC_VER)

#pragma warning (push)
#pragma warning ( disable : 4100 )  // Unreferenced formal parameter
#pragma warning ( disable : 4512 )  //assignment operator could not be generated

#include <memory>

// Memory allocation
#if defined(_DEBUG) && defined(_CRTDBG_MAP_ALLOC) && (_MSC_VER >= 1200)

#include <crtdbg.h>

inline void* operator new(size_t nSize, const char* /*LPCSTR*/ lpszFileName, int nLine)
{
  void* pRes = _malloc_dbg(nSize, _NORMAL_BLOCK, lpszFileName, nLine);
  if(!pRes) throw OdError(eOutOfMemory);
  return pRes;
}

inline void  operator delete(void * pMem, const char* /*LPCSTR lpszFileName*/, int /*nLine*/)
{
  _free_dbg(pMem, _NORMAL_BLOCK);
}

#ifndef DEBUG_NEW
#define DEBUG_NEW new(__FILE__, __LINE__)
#endif

#else //#ifdef _CRTDBG_MAP_ALLOC

//inline void* operator new(size_t nSize) { return Oda::mem_alloc(nSize); }
//inline void  operator delete(void* pMem) { Oda::mem_free(pMem); }

#endif  //_CRTDBG_MAP_ALLOC
#pragma warning (pop)

#include <new.h>

#endif  // _MSC_VER

// Cause Compiler to print a message to output console with File and Line# for 
// Double-Click response
// Ex:
// #pragma MARKMESSAGE("Warning! Implementation is incorrect!")
#ifndef MARKMESSAGE
  #if defined(_MSC_VER)
    #pragma warning (disable:4081)
  #endif
  #if defined(_MSC_VER) && defined(_DEBUG) && !defined(DD_CLIENT_BUILD)
    #define _schSTR(x)  #x
    #define _schSTR2(x) _schSTR(x)
    #define MARKMESSAGE(desc) message(__FILE__ "(" _schSTR2(__LINE__) "): " #desc)
  #else
    #define MARKMESSAGE(desc) 
  #endif
#endif  // MARKMESSAGE

#if defined(_MSC_VER) && defined(_DEBUG)
#pragma function(memcpy)
inline void * memcpy(void * dest, const void * src, size_t size)
{ // Memory blocks must not overlap
  ODA_ASSERT(((char*)dest > (char*)src && (char*)dest >= ((char*)src + size)) ||
             ((char*)dest < (char*)src && ((char*)dest + size) <= (char*)src) ||
                     dest == src );
  return memmove(dest, src, size);
}
#endif // _MSC_VER

// _MSC_VER default behaviour is 'If both parameters of atan2 are 0, the function returns 0'
// Other compilers (e.g. BB6) can throw exception.
#if defined(_MSC_VER) && defined(_DEBUG)
  #define OD_ATAN2(y,x) (ODA_ASSERT((y)!=0 || (x)!=0), atan2(y,x))
#else
  #define OD_ATAN2(y,x) atan2(y,x)
#endif


#endif // __DEBUG_STUFF__INCLUDED



