#ifndef _DDTYPELIBREG_INCLUDED_
#define _DDTYPELIBREG_INCLUDED_

#include <atlbase.h>
#include <comdef.h>
#include "OdString.h"


long  FIRSTDLL_EXPORT ddTypeLibReg(REFGUID libGuid, LPCTSTR szTLibPathName, LPCTSTR szModuleName);
long FIRSTDLL_EXPORT ddTypeLibUnreg(REFGUID libGuid);

#endif //#ifndef _DDTYPELIBREG_INCLUDED_

