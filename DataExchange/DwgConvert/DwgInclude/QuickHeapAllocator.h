#ifndef _OdQuickHeap_h_Included_
#define _OdQuickHeap_h_Included_

#include "RootExport.h"
#include <stdlib.h> // size_t

/** Description:
  {group:Structs}
*/      
struct FIRSTDLL_EXPORT OdQuickHeap
{
  static void* Alloc( size_t );
  static void* Realloc( void*,size_t );
  static void Free( void* );
};

#endif // _OdQuickHeap_h_Included_
