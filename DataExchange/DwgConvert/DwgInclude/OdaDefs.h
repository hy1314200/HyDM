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



#ifndef _ODA_DEFS_
#define _ODA_DEFS_

#ifndef _ODA_COMMON_INCLUDED_
#error  "Each cpp module MUST include OdaCommon.h as first header included"
#endif

#include <stdio.h>
#include <limits.h>

typedef char OdChar;

typedef char OdInt8;
typedef short OdInt16;
typedef int OdInt;

typedef unsigned char OdUInt8;
typedef unsigned short OdUInt16;

#if   UINT_MAX == 0xFFFFFFFFUL
#define DD_SIZEOF_INT  4
#elif UINT_MAX > 0xFFFFFFFFU && UINT_MAX == 0xFFFFFFFFFFFFFFFFU
#error "8 byte size of `int' type unsupported!"
#else
#error "Unsupported number of bytes in `int' type!"
#endif

#if   ULONG_MAX == 0xFFFFFFFFUL
#define DD_SIZEOF_LONG  4
#elif (ULONG_MAX > 0xFFFFFFFFU && ULONG_MAX == 0xFFFFFFFFFFFFFFFFU) || (defined(sparc) && defined(_LP64))
#define DD_SIZEOF_LONG  8
#else
#error "Unsupported number of bytes in `long' type!"
#endif

#if DD_SIZEOF_LONG == 4
typedef long OdInt32;
typedef unsigned long OdUInt32;
#else // assumes 4-byte int type
typedef int OdInt32;
typedef unsigned int OdUInt32;
#endif

#if defined(_IA64_) || (defined(_MSC_VER) && (_MSC_VER >= 1020))
#define BUILTIN_INT64 __int64
#elif DD_SIZEOF_LONG == 8
#define BUILTIN_INT64 long
#elif defined(__MWERKS__)
#define BUILTIN_INT64 long long
#endif

#ifdef BUILTIN_INT64
typedef BUILTIN_INT64 OdInt64;
typedef unsigned BUILTIN_INT64 OdUInt64;
#else
#include "Int64.h"
#endif

typedef void * VoidPtr;

#include <string.h>

#include "DbHandle.h"

//DD:EXPORT_ON

#define OD_ERROR_DEF(code, string)  code,
enum OdResult
{
#include "ErrorDefs.h"
        eDummyLastError
};
#undef OD_ERROR_DEF

#define OD_MESSAGE_DEF(code, string)  code,
enum OdMessage
{
  sidDummyFirstMassage = eDummyLastError -1,
#include "MessageDefs.h"
        sidDummyLastMassage
};
#undef OD_MESSAGE_DEF

class OdErrorContext;

/** Description:

    {group:Error_Classes}
*/
class FIRSTDLL_EXPORT OdError
{
protected:
  OdErrorContext* m_pContext;
public:
  explicit OdError(OdResult errCode);
  OdError(const OdError&);
  explicit OdError(OdErrorContext*);
  explicit OdError(const OdChar* szErrorMessage);
  OdError(const OdChar* szErrorMessage, const OdError& previousError);
  virtual ~OdError();
  OdError& operator = (const OdError&);
  OdResult code() const;
  OdString description() const;
  void attachPreviousError(const OdError&);
};


typedef OdResult OdWarning;

/** Description:

    {group:DD_Namespaces}
*/
namespace OdDb
{
  enum DwgVersion
  {
    kDHL_MC0_0   = 0,
    kDHL_AC1_2   = 1,
    kDHL_AC1_40  = 2,
    kDHL_AC1_50  = 3,
    kDHL_AC2_20  = 4,
    kDHL_AC2_10  = 5,
    kDHL_AC2_21  = 6,
    kDHL_AC2_22  = 7,
    kDHL_1001    = 8,
    kDHL_1002    = 9,  vAC025= kDHL_1002,     // AutoCAD 2.5
    kDHL_1003    = 10, vAC026= kDHL_1003,     // AutoCAD 2.6
    kDHL_1004    = 11, vAC09 = kDHL_1004,     // Release 9
    kDHL_1005    = 12,
    kDHL_1006    = 13, vAC10 = kDHL_1006,     // Release 10
    kDHL_1007    = 14,
    kDHL_1008    = 15,
    kDHL_1009    = 16, vAC12 = kDHL_1009,     // R11 and R12
    kDHL_1010    = 17,
    kDHL_1011    = 18,
    kDHL_1012    = 19, vAC13 = kDHL_1012,     // R13
    kDHL_1013    = 20, vAC14beta = kDHL_1013, // R14 beta
    kDHL_1014    = 21, vAC14 = kDHL_1014,     // R14 release
    kDHL_1500    = 22,                        // R15 beta
    kDHL_1015    = 23, vAC15 = kDHL_1015,     // R15 (2000) release
    kDHL_1800a   = 24,                        // R18 beta
    kDHL_1800    = 25, vAC18 = kDHL_1800,     // R18 release 

    kDHL_PRECURR = vAC15,
    kDHL_CURRENT = vAC18,
    kDHL_Unknown = 32766,
    kDHL_Max     = 32767
  };
  // kDHL_1012, kMRelease0 = R13c0-3
  // kDHL_1012, kMRelease1 = R13c0-3
  // kDHL_1012, kMRelease4 = R13c4
  // kDHL_1012, kMRelease5 = R13c4_m
  // kDHL_1012, kMRelease6 = R13c4a

  // kDHL_1013, kMRelease0 = Sedona s000..s045
  // kDHL_1013, kMRelease1 = Sedona s046..s050
  // kDHL_1013, kMRelease2 = Sedona s051..s052
  // kDHL_1013, kMRelease3 = Sedona s053..s054
  // kDHL_1013, kMRelease4 = Sedona s055..s059
  // kDHL_1013, kMRelease5 = Sedona s060..s063
  // kDHL_1013, kMRelease6 = Sedona s064..
  // kDHL_1014, kMRelease0 = R14.0

  // kDHL_1500, kMRelease0  = Tahoe t010..t016
  // kDHL_1500, kMRelease1  = Tahoe t017
  // kDHL_1500, kMRelease2  = Tahoe t018, t019
  // kDHL_1500, kMRelease3  = Tahoe t020
  // kDHL_1500, kMRelease4  = Tahoe t021..t023
  // kDHL_1500, kMRelease5  = Tahoe t024
  // kDHL_1500, kMRelease6  = Tahoe t025..t027
  // kDHL_1500, kMRelease7  = Tahoe t028
  // kDHL_1500, kMRelease8  = Tahoe t029
  // kDHL_1500, kMRelease9  = Tahoe t030
  // kDHL_1500, kMRelease10 = Tahoe t031..t033
  // kDHL_1500, kMRelease11 = Tahoe t034
  // kDHL_1500, kMRelease12 = Tahoe t035..t036
  // kDHL_1500, kMRelease13 = Tahoe t037..t038
  // kDHL_1500, kMRelease14 = Tahoe t039
  // kDHL_1500, kMRelease15 = Tahoe t040..t041
  // kDHL_1500, kMRelease17 = Tahoe t042
  // kDHL_1500, kMRelease20 = Tahoe t047
  // kDHL_1500, kMRelease21 = Tahoe t048
  // kDHL_1500, kMRelease22 = Tahoe t049..t050
  // kDHL_1500, kMRelease23 = Tahoe t051
  // kDHL_1500, kMRelease24 = Tahoe t052..t053
  // kDHL_1500, kMRelease25 = Tahoe t054
  // kDHL_1500, kMRelease26 = Tahoe t055
  // kDHL_1500, kMRelease27 = Tahoe t056
  // kDHL_1500, kMRelease28 = not used
  // kDHL_1500, kMRelease29 = Tahoe t057
  // kDHL_1500, kMRelease30 = Tahoe t058
  // kDHL_1500, kMRelease31 = Tahoe t059
  // kDHL_1500, kMRelease32 = Tahoe t060
  // kDHL_1500, kMRelease33 = Tahoe t061
  // kDHL_1500, kMRelease34 = Tahoe t062..t063
  // kDHL_1500, kMRelease35 = Tahoe t064
  // kDHL_1500, kMRelease36 = Tahoe t065
  // kDHL_1500, kMRelease37 = Tahoe t066
  // kDHL_1500, kMRelease38 = Tahoe t067
  // kDHL_1500, kMRelease39 = Tahoe t068
  // kDHL_1500, kMRelease40 = Tahoe t069..t070 (fmt changed, tho this didn't)
  // kDHL_1500, kMRelease41 = Tahoe t071
  // kDHL_1500, kMRelease42 = Tahoe t072
  // kDHL_1500, kMRelease43 = Tahoe t073
  // kDHL_1500, kMRelease44 = Tahoe t074
  // kDHL_1500, kMRelease45 = Tahoe t075..t077
  // kDHL_1500, kMRelease46 = Tahoe t078
  // kDHL_1015, kMRelease0  = Tahoe t079..t080
  //
  enum MaintReleaseVer
  {
    kMRelease0         = 0,
    kMRelease1         = 1,
    kMRelease2         = 2,
    kMRelease3         = 3,
    kMRelease4         = 4,
    kMRelease5         = 5,
    kMRelease6         = 6,
    kMRelease7         = 7,
    kMRelease8         = 8,
    kMRelease9         = 9,
    kMRelease10        = 10,
    kMRelease11        = 11,
    kMRelease12        = 12,
    kMRelease13        = 13,
    kMRelease14        = 14,
    kMRelease15        = 15,
    kMRelease16        = 16,
    kMRelease17        = 17,
    kMRelease18        = 18,
    kMRelease19        = 19,
    kMRelease20        = 20,
    kMRelease21        = 21,
    kMRelease22        = 22,
    kMRelease23        = 23,
    kMRelease24        = 24,
    kMRelease25        = 25,
    kMRelease26        = 26,
    kMRelease27        = 27,
    kMRelease28        = 28,
    kMRelease29        = 29,
    kMRelease30        = 30,
    kMRelease31        = 31,
    kMRelease32        = 32,
    kMRelease33        = 33,
    kMRelease34        = 34,
    kMRelease35        = 35,
    kMRelease36        = 36,
    kMRelease37        = 37,
    kMRelease38        = 38,
    kMRelease39        = 39,
    kMRelease40        = 40,
    kMRelease41        = 41,
    kMRelease42        = 42,
    kMRelease43        = 43,
    kMRelease44        = 44,
    kMRelease45        = 45,
    kMRelease46        = 46,
    kMRelease47        = 47,
    kMRelease48        = 48,
    kMRelease49        = 49,
    kMReleaseFirstValid1500 = kMRelease41,
    kMReleaseCurrent   = kMRelease0,
    kMReleaseUnknown   = 126,
    kMReleaseMax       = 127
  };

  TOOLKIT_EXPORT const OdChar* DwgVersionToStr(DwgVersion ver);

  DwgVersion DwgVersionFromStr(const OdChar* str);

  enum MeasurementValue
  {
    kEnglish = 0,
    kMetric  = 1
  };

  enum ProxyImage
  {
    kProxyNotShow	    = 0,
	  kProxyShow	      = 1,
	  kProxyBoundingBox	= 2
  };

  // lineweights are in 100ths of a millimeter
  enum LineWeight
  {
    kLnWt000          =   0,
    kLnWt005          =   5,
    kLnWt009          =   9,
    kLnWt013          =  13,
    kLnWt015          =  15,
    kLnWt018          =  18,
    kLnWt020          =  20,
    kLnWt025          =  25,
    kLnWt030          =  30,
    kLnWt035          =  35,
    kLnWt040          =  40,
    kLnWt050          =  50,
    kLnWt053          =  53,
    kLnWt060          =  60,
    kLnWt070          =  70,
    kLnWt080          =  80,
    kLnWt090          =  90,
    kLnWt100          = 100,
    kLnWt106          = 106,
    kLnWt120          = 120,
    kLnWt140          = 140,
    kLnWt158          = 158,
    kLnWt200          = 200,
    kLnWt211          = 211,
    kLnWtByLayer      = -1,
    kLnWtByBlock      = -2,
    kLnWtByLwDefault  = -3
  };

  enum PlotStyleNameType
  {
    kPlotStyleNameByLayer       = 0,
    kPlotStyleNameByBlock       = 1,
    kPlotStyleNameIsDictDefault = 2,
    kPlotStyleNameById          = 3
  };

  // Do not append any enums here !
}

#define SETBIT(flags, bit, value) ((value) ? (flags |= (bit)) : (flags &= ~(bit)))
#define GETBIT(flags, bit) (((flags) & (bit)) ? true : false)

inline bool OdPositive(double x, double tol = 1.e-10)
{
  return (x > tol);
}

inline bool OdNegative(double x, double tol = 1.e-10)
{
  return (x < -tol);
}

inline bool OdZero(double x, double tol = 1.e-10)
{
  return !OdPositive(x, tol) && !OdNegative(x, tol);
}

inline bool OdNonZero(double x, double tol = 1.e-10)
{
  return OdPositive(x, tol) || OdNegative(x, tol);
}

inline bool OdEqual(double x, double y, double tol = 1.e-10)
{
  return OdZero(x - y, tol);
}

inline int OdCmpDouble(double x, double y, double tol = 1.e-10)
{
  if (OdEqual(x,y,tol))
    return 0;
  if (OdPositive(x - y, tol))
    return 1;
  else
    return -1;
}

//DD:EXPORT_OFF

// These definitions cause problems on Linux.
//#define max(a,b)            (((a) > (b)) ? (a) : (b))
//#define min(a,b)            (((a) < (b)) ? (a) : (b))

#endif // _ODA_DEFS_


