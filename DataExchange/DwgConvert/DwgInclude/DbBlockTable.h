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



#ifndef _ODDBBLOCKTABLE_INCLUDED
#define _ODDBBLOCKTABLE_INCLUDED

#include "DD_PackPush.h"

#include "DbSymbolTable.h"

class OdDbBlockTableRecord;

/** Description:
    This class implements bidirectional Iterator objects that traverse entries in OdDbBlockTable objects in an OdDbDatabase.

    Library: Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbBlockTableIterator : public OdDbSymbolTableIterator
{
public:
  
  ODRX_DECLARE_MEMBERS(OdDbBlockTableIterator);
  
protected:

  OdDbBlockTableIterator();
    
  OdDbBlockTableIterator(
    OdDbSymbolTableIteratorImpl* pImpl);
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbBlockTableIterator object pointers.
*/
typedef OdSmartPtr<OdDbBlockTableIterator> OdDbBlockTableIteratorPtr;


/** Description:
    This class implements the BlockTable, which represents block definitions in an OdDbDatabase instance.

    Library:
    Db
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbBlockTable : public OdDbSymbolTable
{
public:
  ODDB_DECLARE_MEMBERS(OdDbBlockTable);

  /** Note:
    DWGdirect applications typically will not use this constructor, insofar as 
    the OdDbDatabase class creates its own instance.
  */
  OdDbBlockTable();

  virtual OdDbSymbolTableRecordPtr getAt(
    const OdString& recordName,
    OdDb::OpenMode openMode, 
    bool getErasedRecord = false) const;

  virtual OdDbObjectId getAt(
    const OdString& recordName, 
    bool getErasedRecord = false) const;

  virtual bool has(
    const OdString& recordName) const;
  virtual bool has(
    const OdDbObjectId& objectId) const;

  OdDbSymbolTableIteratorPtr newIterator( 
    bool atBeginning = true, 
    bool skipDeleted = true) const;

  virtual OdDbObjectId add(
    OdDbSymbolTableRecord* pRecord);

  /** Description:
    Returns the Object ID of the Model Space record within this BlockTable object.
  */
  const OdDbObjectId& getModelSpaceId() const;

  /** Description:
    Returns the Object ID of the PaperSpace record within this BlockTable object.
  */
  const OdDbObjectId& getPaperSpaceId() const;

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfIn(
    OdDbDxfFiler* pFiler);

  virtual void getClassID(
    void** pClsid) const;
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbBlockTable object pointers.
*/
typedef OdSmartPtr<OdDbBlockTable> OdDbBlockTablePtr;

#include "DD_PackPop.h"

#endif 


