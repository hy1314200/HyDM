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



#ifndef OD_DBPROXY_H
#define OD_DBPROXY_H

#include "DD_PackPush.h"

#include "DbObject.h"
#include "IntArray.h"

/** Description:
    This class is the abstract base class for Proxy objects in an OdDbDatabase instance.

    Library:
    Db
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbProxyObject : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbProxyObject);

  OdDbProxyObject();

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult dxfIn(OdDbDxfFiler* filer);

  /** Description:
    Returns the edit flags for the class contained in this Proxy object.
    Remarks:
    proxyFlags will return a combination of the following bits:
    
    @table
    Name                     Value    Description
    kNoOperation             0        None
    kEraseAllowed            0x1      erase()
    kCloningAllowed          0x80     deepClone(), wblockClone()
    kAllButCloningAllowed    0x1      erase()
    kAllAllowedBits          0x81     erase(), deepClone(), wblockClone()
    kMergeIgnore             0        Keep orignal names.
    kMergeReplace            0x100    Use clone.
    kMergeMangleName         0x200    Create anonymous name.
  */
  int proxyFlags() const;

  /** Description:
    Returns the class name of the object represented by this Proxy object.
  */
  OdString originalClassName() const;

  /** Description:
    Returns the DXF name of the object represented by this Proxy object.
  */
  OdString originalDxfName() const;

  /** Description:
    Returns the application description of the object represented by this Proxy object.
  */
  OdString applicationDescription() const;

  /** Description:
    Returns an array of the object IDs referenced by this Proxy object.

    Arguments:
    objectIds (O) Receives an array of the reference object IDs.
  */
  void getReferences(
    OdTypedIdsArray& objectIds) const;

  OdDb::DuplicateRecordCloning mergeStyle() const;

  OdDbObjectPtr deepClone(
    OdDbIdMapping& ownerIdMap) const;

  OdDbObjectPtr wblockClone(
    OdDbIdMapping& ownerIdMap) const;

  enum
  {
    kNoOperation          = 0, 
    kEraseAllowed         = 0x1, 
    kCloningAllowed       = 0x80,
    kAllButCloningAllowed = 0x1, 
    kAllAllowedBits       = 0x81, 
    kMergeIgnore          = 0,      
    kMergeReplace         = 0x100,  
    kMergeMangleName      = 0x200  
  };

  /** Description:
    Returns true if and only if the erase() method is allowed for this Proxy entity. 
  */
  bool eraseAllowed() const               { return GETBIT(proxyFlags(), kEraseAllowed); }
  /** Description:
    Returns true if and only if all but the deepClone() and wblockClone() methods are allowed for this Proxy object.
  */
  bool allButCloningAllowed() const       { return (proxyFlags() & kAllAllowedBits) == (kAllAllowedBits ^ kAllButCloningAllowed); }

  /** Description:
    Returns true if and only if the deepClone() and wblockClone() methods are allowed for this Proxy object.
  */
  bool cloningAllowed() const             { return GETBIT(proxyFlags(), kCloningAllowed); }

  /** Description:
    Returns true if and only if all methods are allowed for this Proxy entity. 

    Remarks:
    The allowed methods are as follows
    
    @untitled table
    deepClone()
    erase()
    wblockClone()
  */
  bool allOperationsAllowed() const       { return (proxyFlags() & kAllAllowedBits) == kAllAllowedBits; }

  /** Description:
    Returns true if and only this Proxy entity is a R13 format Proxy object. 
  */
  bool isR13FormatProxy() const           { return GETBIT(proxyFlags(), 32768); }

  virtual OdResult subErase(
    bool erasing);
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbProxyObject object pointers.
*/
typedef OdSmartPtr<OdDbProxyObject> OdDbProxyObjectPtr;

#include "DD_PackPop.h"

#endif // OD_DBPROXY_H


