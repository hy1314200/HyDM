#ifndef _GI_EXPORT_DEFINED_
#define _GI_EXPORT_DEFINED_

#if defined(_MSC_VER)

#ifdef  _TOOLKIT_IN_DLL_

  #ifdef  ODA_GI_EXPORTS

    #define ODGI_EXPORT __declspec(dllexport)

  #else   /* ODA_GI_EXPORTS */
  
    #define ODGI_EXPORT __declspec(dllimport)
  #endif  /* ODA_GI_EXPORTS */

#else   /* _TOOLKIT_IN_DLL_ */

  #define ODGI_EXPORT

#endif  /* _TOOLKIT_IN_DLL_ */

#else /* _MSC_VER */

#define ODGI_EXPORT

#endif /* _MSC_VER */

#endif  /* _GI_EXPORT_DEFINED_ */

