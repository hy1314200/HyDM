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



#ifndef _ODDBLINETYPETABLE_INCLUDED
#define _ODDBLINETYPETABLE_INCLUDED

#include "DD_PackPush.h"

#include "DbSymbolTable.h"

class OdDbLinetypeTableRecord;

/** Description:
    This class implements bidirectional Iterators for OdDbLinetypeTable instances.

    Library: Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbLinetypeTableIterator : public OdDbSymbolTableIterator
{
public:
  ODRX_DECLARE_MEMBERS(OdDbLinetypeTableIterator);

protected:
  OdDbLinetypeTableIterator();

  OdDbLinetypeTableIterator(
    OdDbSymbolTableIteratorImpl* pImpl);
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbLinetypeTableIterator object pointers.
*/
typedef OdSmartPtr<OdDbLinetypeTableIterator> OdDbLinetypeTableIteratorPtr;


/** Description:
    This class implements the LinetypeTable, which represents linetypes in an OdDbDatabase instance.

    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbLinetypeTable : public OdDbSymbolTable
{
public:
  ODDB_DECLARE_MEMBERS(OdDbLinetypeTable);

  /** Note:
    DWGdirect applications typically will not use this constructor, insofar as 
    the OdDbDatabase class creates its own instance.
  */
  OdDbLinetypeTable();

virtual OdDbObjectId getAt(
    const OdString& recordName, 
    bool getErasedRecord = false) const;

OdDbSymbolTableRecordPtr getAt(
    const OdString& recordName,
    OdDb::OpenMode openMode, 
    bool getErasedRecord = false) const;

  bool has(
    const OdString& recordName) const;

  bool has(
    const OdDbObjectId& objectId) const;

  OdDbSymbolTableIteratorPtr newIterator( 
    bool atBeginning = true, 
    bool skipDeleted = true) const;

  virtual OdDbObjectId add(
    OdDbSymbolTableRecord* pRecord);

  /** Description:
    Returns the Object ID of the "ByLayer" record within this LinetypeTable object.
  */
  const OdDbObjectId& getLinetypeByLayerId() const;

  /** Description:
    Returns the Object ID of the "ByBlock" record within this LinetypeTable object.
  */
  const OdDbObjectId& getLinetypeByBlockId() const;

  virtual void getClassID(
    void** pClsid) const;

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual void dxfOut(
    OdDbDxfFiler* pFiler) const;

  /*
    virtual OdResult dxfInFields(OdDbDxfFiler* pFiler);
    virtual void dxfOutFields(OdDbDxfFiler* pFiler) const;
  */
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbLinetypeTable object pointers.
*/
typedef OdSmartPtr<OdDbLinetypeTable> OdDbLinetypeTablePtr;

#include "DD_PackPop.h"

#endif // _ODDBLINETYPETABLE_INCLUDED


