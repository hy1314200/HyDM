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



#ifndef _VARIANTVALUE_H_INCLUDED_
#define _VARIANTVALUE_H_INCLUDED_

#include "RxObject.h"
#include "RxVariant.h"

class OdRxVariant;
typedef OdSmartPtr<OdRxVariant> OdRxVariantPtr;

/** Description:

    {group:OdRx_Classes} 
*/
class OdRxVariant : public OdRxObject, public OdVariant { };

/** Description:

    {group:OdRx_Classes} 
*/
class OdRxVariantValue : public OdSmartPtr<OdRxVariant>
{
public:
  inline void assign(const OdRxVariant* pVar) { OdSmartPtr<OdRxVariant>::operator=(pVar); }

  inline OdRxVariantValue(const OdRxObject* pVar) : OdSmartPtr<OdRxVariant>(pVar) {}

  inline OdRxVariantValue(const OdRxVariant* pVar) : OdSmartPtr<OdRxVariant>(pVar) {}

  inline OdRxVariantValue(bool val)
  {
    assign(OdRxObjectImpl<OdRxVariant>::createObject());
    get()->setBool(val);
  }
  inline OdRxVariantValue(OdUInt8 val)
  {
    assign(OdRxObjectImpl<OdRxVariant>::createObject());
    get()->setUInt8(val);
  }
  inline OdRxVariantValue(OdUInt16 val)
  {
    assign(OdRxObjectImpl<OdRxVariant>::createObject());
    get()->setUInt16(val);
  }
  inline OdRxVariantValue(OdInt16 val)
  {
    assign(OdRxObjectImpl<OdRxVariant>::createObject());
    get()->setInt16(val);
  }
  inline OdRxVariantValue(OdUInt32 val)
  {
    assign(OdRxObjectImpl<OdRxVariant>::createObject());
    get()->setUInt32(val);
  }
  inline OdRxVariantValue(OdInt32 val)
  {
    assign(OdRxObjectImpl<OdRxVariant>::createObject());
    get()->setInt32(val);
  }
  inline OdRxVariantValue(double val)
  {
      assign(OdRxObjectImpl<OdRxVariant>::createObject());
      get()->setDouble(val);
  }
  inline OdRxVariantValue(const OdString& val)
  {
    assign(OdRxObjectImpl<OdRxVariant>::createObject());
    get()->setString(val);
  }

  inline operator bool() const
  {
    return get()->getBool();
  }
  inline operator OdUInt8() const
  {
    return get()->getInt8();
  }
  inline operator OdUInt16() const
  {
    return get()->getInt16();
  }
  inline operator OdUInt32() const
  {
    return get()->getInt32();
  }
  inline operator OdInt8() const
  {
    return get()->getInt8();
  }
  inline operator OdInt16() const
  {
    return get()->getInt16();
  }
  inline operator OdInt32() const
  {
    return get()->getInt32();
  }
  inline operator double() const
  {
    return get()->getDouble();
  }
  inline operator OdString() const
  {
    return get()->getString();
  }
};

#endif //_VARIANTVALUE_H_INCLUDED_


