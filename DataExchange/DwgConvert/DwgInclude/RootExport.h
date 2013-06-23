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
// programs incorporating this software must include the following statment 
// with their copyright notices:
//
//      DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



/*  RootExport.h

*/
#ifndef _ROOTEXPORT_DEFINED
#define _ROOTEXPORT_DEFINED

#if defined(_MSC_VER)

#ifdef  _TOOLKIT_IN_DLL_

  #ifdef    ODAFIRST_DLL_EXPORTS
    #define FIRSTDLL_EXPORT     __declspec(dllexport)

  #else  /* ODAFIRST_DLL_EXPORTS */
    #define FIRSTDLL_EXPORT     __declspec(dllimport)

  #endif /* ODAFIRST_DLL_EXPORTS */

#else   /* _TOOLKIT_IN_DLL_ */

  #define FIRSTDLL_EXPORT

#endif  /* _TOOLKIT_IN_DLL_ */

#endif /* _MSC_VER */

#endif  // _ROOTEXPORT_DEFINED




