#ifndef _RxInit_h_Included_
#define _RxInit_h_Included_

class OdRxSystemServices;

//DD:EXPORT_ON

FIRSTDLL_EXPORT bool odrxInitialize(OdRxSystemServices* pSysSvcs);

FIRSTDLL_EXPORT void odrxUninitialize();

//DD:EXPORT_OFF

#endif
