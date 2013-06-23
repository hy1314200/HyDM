///////////////////////////////////////////////////////////////////////////////
// Copyright c 2002, Open Design Alliance Inc. ("Open Design") 
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
//      DWGdirect c 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////

#ifndef OD_DBFIELDVALUE_H
#define OD_DBFIELDVALUE_H

#include "DD_PackPush.h"

class OdFieldValueImpl;

/** Description:

    {group:Other_Classes}
*/
class TOOLKIT_EXPORT OdFieldValue : public OdRxObject
{
public:
  enum DataType
  {
      kUnknown        = 0,
      kLong           = 0x01,
      kDouble         = 0x02,
      kString         = 0x04,
      kDate           = 0x08,
      kPoint          = 0x10,
      k3dPoint        = 0x20,
      kObjectId       = 0x40,
      kBuffer         = 0x80,
      kResbuf         = 0x100
  };
// public:
    // static bool     isValidDataType         (const VARIANT& var);

public:
  ODRX_DECLARE_MEMBERS(OdFieldValue);
  
  OdFieldValue(void);

  OdFieldValue(const OdFieldValue& value);
  OdFieldValue(OdString& pszValue);
  OdFieldValue(long lValue);
  OdFieldValue(double fValue);
  OdFieldValue(const OdInt64& date);
  OdFieldValue(double x, double y);
  OdFieldValue(double x, double y, double z);
  OdFieldValue(const OdDbObjectId& id);
  OdFieldValue(const OdResBuf& rb);
  // OdFieldValue(const OdRxVariantValue& var);
  OdFieldValue(const void* pBuf, OdInt32 dwBufSize);

  bool  reset                   (void);
  OdFieldValue::DataType dataType (void) const;
  bool  isValid                 (void) const;

  operator OdString       (void) const;
  operator OdInt32        (void) const;
  operator double         (void) const;
  operator OdInt64        (void) const;
  operator OdDbObjectId   (void) const;

  OdFieldValue& operator=     (const OdFieldValue& value);
  OdFieldValue& operator=     (OdString& pszValue);
  OdFieldValue& operator=     (OdInt32 lValue);
  OdFieldValue& operator=     (double fValue);
  OdFieldValue& operator=     (OdInt64 date);
  OdFieldValue& operator=     (const OdDbObjectId& objId);
  OdFieldValue& operator=     (const OdResBuf& rb);
  
  // OdFieldValue& operator=     (const OdRxVariantValue& var);

  // bool  get                     (const char*& pszValue) const;

  bool  get                     (OdString& pszValue) const;
  bool  get                     (OdInt32& lValue) const;
  bool  get                     (double& fValue) const;
  bool  get                     (OdInt64& date) const;
  bool  get                     (double& x, 
                                           double& y) const;
  bool  get                     (double& x, 
                                           double& y, 
                                           double& z) const;
  bool  get                     (OdDbObjectId& objId) const;
  bool  get                     (OdResBuf& pRb) const;
  
  // bool  get                     (OdRxVariantValue& var) const;

  bool  get                     (void*& pBuf, 
                                           OdInt32& dwBufSize) const;

  bool  set                     (const OdFieldValue& value);
  bool  set                     (const OdString& pszValue);
  bool  set                     (OdInt32 lValue);
  bool  set                     (double fValue);
  bool  set                     (const OdInt64& date);
  bool  set                     (double x, 
                                           double y);
  bool  set                     (double x, 
                                           double y, 
                                           double z);
  bool  set                     (const OdDbObjectId& objId);
  bool  set                     (const OdResBuf& rb);
  
  // bool  set                     (const OdRxVariantValue& var);

  bool  set                     (const void* pBuf, 
                                           OdInt32 dwBufSize);

  // bool  format                  (LPCTSTR pszFormat, 
  //                                             char*& pszValue) const;

  OdResult dwgInFields           (OdDbDwgFiler* pFiler);
  void dwgOutFields          (OdDbDwgFiler* pFiler) const;
  OdResult dxfInFields           (OdDbDxfFiler* pFiler);
  void dxfOutFields          (OdDbDxfFiler* pFiler) const;

protected:

   OdFieldValue(OdFieldValueImpl* pFldValImpl);
   
   friend class OdDbSystemInternals;
   friend class OdDbField;

   OdFieldValueImpl* m_pImpl;
};
typedef OdSmartPtr<OdFieldValue> OdFieldValuePtr;

#include "DD_PackPop.h"

#endif // OD_DBFIELDVALUE_H
