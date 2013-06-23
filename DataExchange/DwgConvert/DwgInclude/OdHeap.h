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

#ifndef _OD_HEAPOPERATORS_INCLUDED_
#define _OD_HEAPOPERATORS_INCLUDED_

#include "OdAlloc.h"

#ifdef __BCPLUSPLUS__ // borland compiler does not support "placement delete"
#define ODRX_HEAP_OPERATORS() \
void* operator new(size_t s) { return ::operator new(s); }\
void operator delete(void* p) { ::operator delete(p); }\
void* operator new[](size_t s) { return ::operator new(s); }\
void operator delete[](void* p) { ::operator delete(p); }\
void *operator new(size_t, void* p) { return p; }\
void *operator new[](size_t, void* p) { return p; }
#else
#define ODRX_HEAP_OPERATORS() \
void* operator new(size_t s) throw() { return ::operator new(s); }\
void operator delete(void* p) { ::operator delete(p); }\
void* operator new[](size_t s) throw() { return ::operator new(s); }\
void operator delete[](void* p) { ::operator delete(p); }\
void *operator new(size_t, void* p) throw() { return p; }\
void operator delete( void*, void* ) {}\
void *operator new[](size_t, void* p) throw() { return p; }\
void operator delete[]( void*, void* ) {}
#endif

#define ODRX_NO_HEAP_OPERATORS() \
void* operator new(size_t ) throw() { ODA_FAIL(); return 0; }\
void operator delete(void* ) { ODA_FAIL(); throw OdError(eNotApplicable); }\
void* operator new[](size_t ) throw() { ODA_FAIL(); return 0; }\
void operator delete[](void* ) { ODA_FAIL(); throw OdError(eNotApplicable); }

#if defined(sgi) || defined(__hpux)
#define ODRX_USING_HEAP_OPERATORS(T) \
void* operator new(size_t s) throw() { return T::operator new(s); }\
void operator delete(void* p) { T::operator delete(p); }\
void* operator new[](size_t s) throw() { return T::operator new(s); }\
void operator delete[](void* p) { T::operator delete(p); }
#else
#define ODRX_USING_HEAP_OPERATORS(T) \
using T::operator new;\
using T::operator delete;\
using T::operator new[];\
using T::operator delete[]
#endif


#endif // _OD_HEAPOPERATORS_INCLUDED_

