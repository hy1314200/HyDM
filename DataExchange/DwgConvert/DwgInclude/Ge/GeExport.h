/* 
   Copyright © 2002, Open Design Alliance Inc. ("Open Design") 
 
   This software is owned by Open Design, and may only be incorporated into 
   application programs owned by members of Open Design subject to a signed 
   Membership Agreement and Supplemental Software License Agreement with 
   Open Design. The structure and organization of this Software are the valuable 
   trade secrets of Open Design and its suppliers. The Software is also protected 
   by copyright law and international treaty provisions. You agree not to 
   modify, adapt, translate, reverse engineer, decompile, disassemble or 
   otherwise attempt to discover the source code of the Software. Application 
   programs incorporating this software must include the following statment 
   with their copyright notices:
  
        DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
  
   By use of this software, you acknowledge and accept the terms of this 
   agreement.
*/


/*  GeExport.h

*/

#ifndef _GE_EXPORT_DEFINED
#define _GE_EXPORT_DEFINED /* {Secret} */

/**
  The following four defines and undefs are strictly for consistant documentation.
*/

#define GE_TOOLKIT_EXPORT
#undef GE_TOOLKIT_EXPORT
#define GE_TEMPLATE_EXPORT
#undef GE_TEMPLATE_EXPORT

#if defined(_MSC_VER)

#ifdef  _TOOLKIT_IN_DLL_

  #ifdef  ODA_GE_EXPORTS

    #define GE_TOOLKIT_EXPORT   __declspec(dllexport) /* {Secret} */
    #define GE_TEMPLATE_EXPORT                        /* {Secret} */   

  #else   /* ODW_GE_EXPORTS */

    #define GE_TOOLKIT_EXPORT  __declspec(dllimport)  /* {Secret} */
    #define GE_TEMPLATE_EXPORT extern                 /* {Secret} */   

  #endif  /* ODW_GE_EXPORTS */

#else   /* _TOOLKIT_IN_DLL_ */

  #define GE_TOOLKIT_EXPORT                           /* {Secret} */

#endif  /* _TOOLKIT_IN_DLL_ */

#else /* _MSC_VER */

#define GE_TOOLKIT_EXPORT                             /* {Secret} */    

#endif /* _MSC_VER */

#endif  /* _GE_EXPORT_DEFINED */



