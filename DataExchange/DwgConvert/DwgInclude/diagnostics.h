#ifndef __DIAGNOSTICS__INCLUDED
#define __DIAGNOSTICS__INCLUDED


//DD:EXPORT_ON

typedef void (*OdAssertFunc)(const OdChar* expresssion, const OdChar* fileName, int nLineNo);
typedef void (*OdTraceFunc)(const OdChar* debugString);

OdTraceFunc FIRSTDLL_EXPORT odSetTraceFunc(OdTraceFunc traceFunc);
OdAssertFunc FIRSTDLL_EXPORT odSetAssertFunc(OdAssertFunc assertFunc);

void FIRSTDLL_EXPORT OdTrace(const char* lpszFormat, ...);
void FIRSTDLL_EXPORT OdAssert(const OdChar* expresssion, const OdChar* fileName, int nLineNo);

//DD:EXPORT_OFF

#endif //__DIAGNOSTICS__INCLUDED

