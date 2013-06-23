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



#if !defined(_ODVARIANT_H_INCLUDED_)
#define _ODVARIANT_H_INCLUDED_

#include "DD_PackPush.h"
#include "OdPlatformSettings.h"
#include "RxObject.h"

#include "StringArray.h"
#include "BoolArray.h"
#include "Int8Array.h"
#include "Int16Array.h"
#include "Int32Array.h"
#include "Int64Array.h"
#include "UInt8Array.h"
#include "UInt16Array.h"
#include "UInt32Array.h"
#include "UInt64Array.h"
#include "DoubleArray.h"
#include "OdWString.h"

/** Description:

    {group:Error_Classes}
*/
class FIRSTDLL_EXPORT OdError_InvalidVariantType : public OdError
{
public:
  OdError_InvalidVariantType();
};

const int nOdVariantDataSize = 8;

/** Description:

    {group:Other_Classes}
*/
class FIRSTDLL_EXPORT OdVariant
{
protected:
  int m_type;

#ifdef __hpux
  double padding;  // this aligns m_data properly on HP Itanium
#endif

  OdUInt8 m_data[nOdVariantDataSize];
public:
  struct TypeFactory
  {
    virtual void construct(void* pData) const = 0;
    virtual void destroy(void* pData) const = 0;
  };
  static const TypeFactory* typeFactory(int type);
public:
  typedef enum
  {
    kVoid       = 0x0000,
    kString     = 0x0001,
    kBool       = 0x0002,
    kInt8       = 0x0003,
    kUInt8      = 0x0003,
    kInt16      = 0x0004,
    kUInt16     = 0x0004,
    kInt32      = 0x0005,
    kUInt32     = 0x0005,
    kInt64      = 0x0006,
    kUInt64     = 0x0006,
    kDouble     = 0x0007,
    kWString    = 0x0008,
    kRxObjectPtr= 0x0009,
    kNextType,

    kByRef      = 0x0040,
    kArray      = 0x0080
  } Type;

  OdVariant();
  OdVariant(const OdVariant& val);
  OdVariant& operator =(const OdVariant& val);
  ~OdVariant();

  int  varType() const;
  Type type()    const;
  bool isArray() const;
  bool isByRef() const;

  OdVariant(const OdString& val);
  OdVariant(const OdWString& val);
  OdVariant(const OdRxObjectPtr& val);
  OdVariant(bool val);
  OdVariant(OdInt8 val);
  OdVariant(OdUInt8 val);
  OdVariant(OdInt16 val);
  OdVariant(OdUInt16 val);
  OdVariant(OdInt32 val);
  OdVariant(OdUInt32 val);
  OdVariant(OdInt64 val);
  OdVariant(OdUInt64 val);
  OdVariant(double val);

  const OdString& getString() const;
  const OdWString& getWString() const;
  const OdRxObjectPtr& getRxObjectPtr() const;
  bool            getBool()   const;
  OdInt8          getInt8()   const;
  OdUInt8         getUInt8()  const;
  OdInt16         getInt16()  const;
  OdUInt16        getUInt16() const;
  OdInt32         getInt32()  const;
  OdUInt32        getUInt32() const;
  OdInt64         getInt64()  const;
  OdUInt64        getUInt64() const;
  double          getDouble() const;

  OdString* getStringPtr() const;
  OdWString* getWStringPtr() const;
  OdRxObjectPtr* getRxObjectPtrPtr() const;
  bool*     getBoolPtr()   const;
  OdInt8*   getInt8Ptr()   const;
  OdUInt8*  getUInt8Ptr()  const;
  OdInt16*  getInt16Ptr()  const;
  OdUInt16* getUInt16Ptr() const;
  OdInt32*  getInt32Ptr()  const;
  OdUInt32* getUInt32Ptr() const;
  OdInt64*  getInt64Ptr()  const;
  OdUInt64* getUInt64Ptr() const;
  double*   getDoublePtr() const;

  const OdStringArray& getStringArray() const;
  const OdWStringArray& getWStringArray() const;
  const OdRxObjectPtrArray& getRxObjectPtrArray() const;
  const OdBoolArray&   getBoolArray  () const;
  const OdInt8Array&   getInt8Array  () const;
  const OdUInt8Array&  getUInt8Array () const;
  const OdInt16Array&  getInt16Array () const;
  const OdUInt16Array& getUInt16Array() const;
  const OdInt32Array&  getInt32Array () const;
  const OdUInt32Array& getUInt32Array() const;
  const OdInt64Array&  getInt64Array () const;
  const OdUInt64Array& getUInt64Array() const;
  const OdDoubleArray& getDoubleArray() const;

  OdStringArray& asStringArray();
  OdWStringArray& asWStringArray();
  OdRxObjectPtrArray& asRxObjectPtrArray();
  OdBoolArray&   asBoolArray  ();
  OdInt8Array&   asInt8Array  ();
  OdUInt8Array&  asUInt8Array ();
  OdInt16Array&  asInt16Array ();
  OdUInt16Array& asUInt16Array();
  OdInt32Array&  asInt32Array ();
  OdUInt32Array& asUInt32Array();
  OdInt64Array&  asInt64Array ();
  OdUInt64Array& asUInt64Array();
  OdDoubleArray& asDoubleArray();

  OdStringArray* getStringArrayPtr() const;
  OdWStringArray* getWStringArrayPtr() const;
  OdRxObjectPtrArray* getRxObjectPtrArrayPtr() const;
  OdBoolArray*   getBoolArrayPtr  () const;
  OdInt8Array*   getInt8ArrayPtr  () const;
  OdUInt8Array*  getUInt8ArrayPtr () const;
  OdInt16Array*  getInt16ArrayPtr () const;
  OdUInt16Array* getUInt16ArrayPtr() const;
  OdInt32Array*  getInt32ArrayPtr () const;
  OdUInt32Array* getUInt32ArrayPtr() const;
  OdInt64Array*  getInt64ArrayPtr () const;
  OdUInt64Array* getUInt64ArrayPtr() const;
  OdDoubleArray* getDoubleArrayPtr() const;

  OdVariant& setString(const OdString& val);
  OdVariant& setWString(const OdWString& val);
  OdVariant& setRxObjectPtr( const OdRxObjectPtr& val );
  OdVariant& setBool  (bool val);
  OdVariant& setInt8  (OdInt8 val);
  OdVariant& setUInt8 (OdUInt8 val);
  OdVariant& setInt16 (OdInt16 val);
  OdVariant& setUInt16(OdUInt16 val);
  OdVariant& setInt32 (OdInt32 val);
  OdVariant& setUInt32(OdUInt32 val);
  OdVariant& setInt64 (OdInt64 val);
  OdVariant& setUInt64(OdUInt64 val);
  OdVariant& setDouble(double val);

  OdVariant& setStringPtr(OdString* pVal);
  OdVariant& setWStringPtr(OdWString* pVal);
  OdVariant& setRxObjectPtrPtr(OdRxObjectPtr* pVal);
  OdVariant& setBoolPtr  (bool*     pVal);
  OdVariant& setInt8Ptr  (OdInt8*   pVal);
  OdVariant& setUInt8Ptr (OdUInt8*  pVal);
  OdVariant& setInt16Ptr (OdInt16*  pVal);
  OdVariant& setUInt16Ptr(OdUInt16* pVal);
  OdVariant& setInt32Ptr (OdInt32*  pVal);
  OdVariant& setUInt32Ptr(OdUInt32* pVal);
  OdVariant& setInt64Ptr (OdInt64*  pVal);
  OdVariant& setUInt64Ptr(OdUInt64* pVal);
  OdVariant& setDoublePtr(double*   pVal);

  OdVariant& setStringArray(const OdStringArray& array);
  OdVariant& setWStringArray(const OdWStringArray& array);
  OdVariant& setRxObjectPtrArray(const OdRxObjectPtrArray& array);
  OdVariant& setBoolArray  (const OdBoolArray&   array);
  OdVariant& setInt8Array  (const OdInt8Array&   array);
  OdVariant& setUInt8Array (const OdUInt8Array&  array);
  OdVariant& setInt16Array (const OdInt16Array&  array);
  OdVariant& setUInt16Array(const OdUInt16Array& array);
  OdVariant& setInt32Array (const OdInt32Array&  array);
  OdVariant& setUInt32Array(const OdUInt32Array& array);
  OdVariant& setInt64Array (const OdInt64Array&  array);
  OdVariant& setUInt64Array(const OdUInt64Array& array);
  OdVariant& setDoubleArray(const OdDoubleArray& array);

  OdVariant& setStringArrayPtr(OdStringArray* pArray);
  OdVariant& setWStringArrayPtr(OdWStringArray* pArray);
  OdVariant& setRxObjectPtrArrayPtr(OdRxObjectPtrArray* array);
  OdVariant& setBoolArrayPtr  (OdBoolArray*   pArray);
  OdVariant& setInt8ArrayPtr  (OdInt8Array*   pArray);
  OdVariant& setUInt8ArrayPtr (OdUInt8Array*  pArray);
  OdVariant& setInt16ArrayPtr (OdInt16Array*  pArray);
  OdVariant& setUInt16ArrayPtr(OdUInt16Array* pArray);
  OdVariant& setInt32ArrayPtr (OdInt32Array*  pArray);
  OdVariant& setUInt32ArrayPtr(OdUInt32Array* pArray);
  OdVariant& setInt64ArrayPtr (OdInt64Array*  pArray);
  OdVariant& setUInt64ArrayPtr(OdUInt64Array* pArray);
  OdVariant& setDoubleArrayPtr(OdDoubleArray* pArray);
};

inline int
OdVariant::varType() const { return m_type; }

inline OdVariant::Type
OdVariant::type() const { return Type(m_type & (0x3F)); }

inline bool
OdVariant::isArray() const { return (m_type & kArray)!=0; }

inline bool
OdVariant::isByRef() const { return (m_type & kByRef)!=0; }


#include "DD_PackPop.h"

#endif //_ODVARIANT_H_INCLUDED_


