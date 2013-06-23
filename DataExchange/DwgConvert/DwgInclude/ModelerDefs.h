#ifndef TYPEVER_H
#define TYPEVER_H /* { Secret } */

typedef int AfTypeVer;

enum AfTypeVerEnum
{
  kAfVerMask           = 0x00FFFFFF,
  kAfVerInvalid        = kAfVerMask,
  kAfVerAny            = 0x00000000,
  kAfVerUnknown        = kAfVerAny,

  kAfVer105            = 105,
  kAfVer106            = 106,
  kAfVer107            = 107,
  kAfVer200            = 200,
  kAfVer201            = 201,
  kAfVer400            = 400,
  kAfVer500            = 500,
  kAfVer700            = 700,
  kAfVer1000           = 1000,
  kAfVer1100           = 1100,
  kAfVer20800          = 20800,

  
  kAfTypeMask          = (OdInt32)0xFF000000,
  kAfTypeInvalid       = kAfTypeMask,
  kAfTypeAny           = 0x00000000,
  kAfTypeUnknown       = kAfTypeAny,

  kAfTypeASCII         = 0x01000000,
  kAfTypeBinary        = 0x02000000,
  kAfTypeIndexed       = (OdInt32)0x80000000, // used only with kAfTypeASCII

  
  kAfTypeVerInvalid = kAfTypeInvalid | kAfVerInvalid,
  kAfTypeVerAny     = kAfTypeAny     | kAfVerAny,
  kAfTypeVerUnknown = kAfTypeVerAny,

  //////// these constants are for convenience //////////

  kAf_ASCII_Any        = kAfTypeASCII|kAfVerAny,
  kAf_ASCII_106        = kAfTypeASCII|kAfVer106,
  kAf_ASCII_400        = kAfTypeASCII|kAfVer400,
  kAf_ASCII_500        = kAfTypeASCII|kAfVer500,
  kAf_ASCII_700        = kAfTypeASCII|kAfVer700,
  kAf_ASCII_20800      = kAfTypeASCII|kAfVer20800,

  kAf_Binary_Any       = kAfTypeBinary|kAfVerAny,
  kAf_Binary_106       = kAfTypeBinary|kAfVer106,
  kAf_Binary_400       = kAfTypeBinary|kAfVer400,
  kAf_Binary_500       = kAfTypeBinary|kAfVer500,
  kAf_Binary_700       = kAfTypeBinary|kAfVer700,
  kAf_Binary_20800     = kAfTypeBinary|kAfVer20800
};

/** Description: 
  
    {group:DD_Namespaces}
*/
namespace OdDb
{
  enum BoolOperType
  {
    kBoolUnite     = 0,
    kBoolIntersect = 1,
    kBoolSubtract  = 2
  };
}

#endif //TYPEVER_H

