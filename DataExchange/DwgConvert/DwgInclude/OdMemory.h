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



#ifndef _OD_MEMORY_H_
#define _OD_MEMORY_H_

#define STL_USING_MEMORY
#include "OdaSTL.h"

#include "OdPlatform.h"
#include "OdAlloc.h"

// TEMPLATE CLASS allocator
// Some compilers don't have support for construct & destroy functions.
#ifdef OD_STD_ALLOCATOR
/** Description:

    {group:Other_Classes}
*/
template<class T>
class OdAllocator : public std::allocator<T>
{};

#else


/** Description:

    {group:Other_Classes}
*/
template<class T>
class OdAllocator : public std::allocator<T>
{
public:
  typedef typename std::allocator<T>::size_type       size_type;
  typedef typename std::allocator<T>::difference_type difference_type;
  typedef typename std::allocator<T>::pointer         pointer;
  typedef typename std::allocator<T>::const_pointer   const_pointer;
  typedef typename std::allocator<T>::reference       reference;
  typedef typename std::allocator<T>::const_reference const_reference;
  typedef typename std::allocator<T>::value_type      value_type;

private:
  inline void constructn(size_type N, pointer ptr, const T& _V = T())
  {
    while(N--) construct(ptr + N, _V);
  }
  inline void destroyn(size_type N, pointer ptr)
  {
    while(N--) destroy(ptr + N);
  }
public:

  pointer allocate(size_type N, const void *)
  {
    pointer res = NULL;
    if (N)
    {
      res = (pointer)::odrxAlloc(N * sizeof(T));
      constructn(N, res);
    }
    return res;
  }
  
  // For CodeWarrior
  pointer allocate(size_type N, const_pointer = 0)
  {
    pointer res = NULL;
    if (N)
    {
      res = (pointer)::odrxAlloc(N * sizeof(T));
      constructn(N, res);
    }
    return res;
  }

#ifdef _MSC_VER
  // Needed to prevent "freeing mismatched memory" errors in Purify
  char _FARQ *_Charalloc(size_type n)
  {
    return (char _FARQ*)::odrxAlloc(n);
  }
#endif

  void deallocate(void* P, size_type ) { ::odrxFree(P); }
};

#endif


#endif //_OD_MEMORY_H_


