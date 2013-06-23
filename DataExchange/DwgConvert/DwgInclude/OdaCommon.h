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



#ifndef _ODA_COMMON_INCLUDED_
#define _ODA_COMMON_INCLUDED_

#if defined(_MSC_VER)
// warning C4290: C++ Exception Specification ignored
#pragma warning ( disable : 4290 )
// warning C4514: ... : unreferenced inline function has been removed
#pragma warning ( disable : 4514 )
// identifier was truncated to '255' characters in the debug information
#pragma warning ( disable : 4786 )
// class 'NAME' needs to have dll-interface to be used by clients of class NAME
#pragma warning ( disable : 4251 )
// copy constructor could not be generated
#pragma warning ( disable : 4511 ) 
// assignment operator could not be generated
#pragma warning ( disable : 4512 ) 

#ifndef _DEBUG
#pragma warning ( disable : 4100 ) // unreferenced formal parameter
#pragma warning ( disable : 4702 ) // unreachable code
#pragma warning ( disable : 4710 ) // not inlined
#endif

#define ODRX_ABSTRACT __declspec(novtable)

#else

#define ODRX_ABSTRACT

#endif  // _MSC_VER

// Prevent strange errors on SGI.
#include <stdlib.h>

#include "DbExport.h"

#include  "OdaDefs.h"
#include  "DebugStuff.h"

#define OdaPI 3.14159265358979323846
#define OdaPI2 (OdaPI / 2.0)
#define OdaPI4 (OdaPI / 4.0)
#define Oda2PI (OdaPI+OdaPI)

#define OdaToDegree(rad) ((rad)/OdaPI*180.0)
#define OdaToRadian(deg) ((deg)*OdaPI/180.0)

#define odmin(X,Y) ((X) < (Y) ? (X) : (Y))
#define odmax(X,Y) ((X) > (Y) ? (X) : (Y))

// SSL:
// from http://www.ex.ac.uk/cimt/dictunit/dictunit.htm
//
// Even as late as the middle of the 20th century there were some differences in UK
// and US measures which were nominally the same. The UK inch measured 2.53998 cm while
// the US inch was 2.540005 cm. Both were standardised at 2.54 cm in July 1959.
const double kMmPerInch = 25.4;

#endif



