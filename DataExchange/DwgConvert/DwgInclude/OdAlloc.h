/***********************************************************************************
   Copyright 2002, Open Design Alliance Inc. ("Open Design") 
   
   This software is owned by Open Design, and may only be incorporated into 
   application programs owned by members of Open Design subject to a signed 
   Membership Agreement and Supplemental Software License Agreement with 
   Open Design. The structure and organization of this Software are the valuable 
   trade secrets of Open Design and its suppliers. The Software is also protected 
   by copyright law and international treaty provisions. You agree not to 
   modify, adapt, translate, reverse engineer, decompile, disassemble or 
   otherwise attempt to discover the source code of the Software. Application 
   programs incorporating this software must include the following statement 
   with their copyright notices:
  
        DWGdirect copyright 2002 by Open Design Alliance Inc. All rights reserved. 
  
   By use of this software, you acknowledge and accept the terms of this 
   agreement.
***********************************************************************************/

#ifndef _OD_ALLOC_INCLUDED_
#define _OD_ALLOC_INCLUDED_

#include "OdAllocExport.h"

/** Description:
    Provides DWGdirect with heap to use.
*/

//DD:EXPORT_ON

#ifdef __cplusplus
  extern "C" {
#endif

  extern ALLOCDLL_EXPORT void* odrxAlloc  (size_t s);
  extern ALLOCDLL_EXPORT void* odrxRealloc(void* p, size_t new_size, size_t old_size);
  extern ALLOCDLL_EXPORT void  odrxFree   (void* p);

#ifdef __cplusplus
  }   // extern "C"
#endif

//DD:EXPORT_OFF

#endif 

