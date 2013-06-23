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



#ifndef _ODDBSYMBOLTABLE_INCLUDED
#define _ODDBSYMBOLTABLE_INCLUDED

#include "DD_PackPush.h"

#include "DbObject.h"

class OdDbSymbolTableRecord;
class OdDbSymbolTableIterator;

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbSymbolTableIterator object pointers.
*/
typedef OdSmartPtr<OdDbSymbolTableIterator> OdDbSymbolTableIteratorPtr;
/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbSymbolTableRecord object pointers.
*/
typedef OdSmartPtr<OdDbSymbolTableRecord> OdDbSymbolTableRecordPtr;

/** Description:

    {group:Error_Classes}
*/
class TOOLKIT_EXPORT OdError_DuplicateRecordName : public OdError
{
public:
  OdError_DuplicateRecordName(
    OdDbObjectId existingRecId);
  OdDbObjectId existingRecordId() const;
};

/** Description:
    This class is the base class for all SymbolTable objects in an OdDbDatabase.

    Library: Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbSymbolTable : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbSymbolTable);
  /** Note:
    DWGdirect applications typically will not use this constructor, insofar as 
    this class is a base class.
  */
  OdDbSymbolTable();

  /** Description:
    Searches the SymbolTable object for the specified SymbolTableRecord.

    Arguments:
    recordName (I) Record name.
    openMode (I) Mode in which to open the record.
    getErasedRecord (I) If and only if true, *erased* records will be opened or retrieved.

    Remarks:
    Returns a SmartPointer to the opened object if successful, otherwise a null SmartPointer.
  */
  virtual OdDbSymbolTableRecordPtr getAt(
    const OdString& recordName,
    OdDb::OpenMode openMode, 
    bool getErasedRecord = false) const;

  virtual OdDbObjectId getAt(
    const OdString& recordName, 
    bool getErasedRecord = false) const;

  /** Description:
    Returns true if and only if this SymbolTable object *has* an record with the specified *name* or Object ID.

    Arguments:
    recordName (I)Record *name*.
    objectId (I) Object ID.
  */
  virtual bool has(
    const OdString& recordName) const;
  virtual bool has(
    const OdDbObjectId& objectId) const;

  /** Description:
    Returns an iterator that can be used to traverse this SymbolTable object.

    Arguments:
    atBeginning (I) True to start at the beginning, false to start at the end. 
    skipDeleted (I) If and only if true, deleted records are skipped.
  */
  virtual OdDbSymbolTableIteratorPtr newIterator( 
    bool atBeginning = true, 
    bool skipDeleted = true) const;

  /** Description:
    Adds the specified record to this SymbolTable object.

    Arguments:
    pRecord (I) Pointer to the record to add.

    Remarks:
    Returns the Object ID of the newly added record.
  */
  virtual OdDbObjectId add(
    OdDbSymbolTableRecord* pRecord);

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult dxfIn(
    OdDbDxfFiler* pFiler);

  virtual void dxfOut(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult dxfInFields_R12(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields_R12(
    OdDbDxfFiler* pFiler) const;
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbSymbolTable object pointers.
*/
typedef OdSmartPtr<OdDbSymbolTable> OdDbSymbolTablePtr;

class OdDbSymbolTableIteratorImpl;

/** Description:
    This class implements bidirectional Iterators for OdDbSymbolTable objects in an OdDbDatabase.

    Library: Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbSymbolTableIterator : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdDbSymbolTableIterator);

  /** Description:
    Sets this Iterator object to reference the SymbolTableRecord that it would normally return first.
     
    Remarks:
    Allows multiple traversals of the iterator list.

    Arguments:
    atBeginning (I) True to start at the beginning, false to start at the end. 
    skipErased (I) If and only if true, *erased* records are skipped.
  */  
  virtual void start(
    bool atBeginning = true, 
    bool skipErased = true);

  /** Description:
    Returns true if and only if the traversal by this Iterator object is complete.
  */  
  virtual bool done() const;

  /** Description:
    Returns the Object ID of the record currently referenced by this Iterator object.
  */
  virtual OdDbObjectId getRecordId() const;

  /** Description:
    Opens the record currently referenced by this Iterator object.

    Arguments:
    openMode (I) Mode in which to open the record.
    openErasedRecord (I) If and only if true, *erased* records will be opened.

    Remarks:
    Returns a SmartPointer to the opened record if successful, otherwise a null SmartPointer.
  */
  virtual OdDbSymbolTableRecordPtr getRecord(
    OdDb::OpenMode openMode = OdDb::kForRead,
    bool openErasedRecord = false) const;

  /** Description:
    Steps this Iterator object.

    Arguments:
    forward (I) True to step *forward*, false to step backward.
    skipErased (I) If and only if true, *erased* records are skipped.
  */
  virtual void step(
    bool forward = true, 
    bool skipErased = true);

  /** Description:
    Positions this Iterator object at the specified record.
    Arguments:
    objectId (I) Object ID of the record.
    pRecord (I) Pointer to the record.
  */  
  virtual void seek(
    const OdDbObjectId& ObjectId);
  virtual void seek(
    const OdDbSymbolTableRecord* pRecord);

  virtual ~OdDbSymbolTableIterator();
protected:
  friend class OdDbSymbolTable;

  OdDbSymbolTableIterator();
  OdDbSymbolTableIterator(
    OdDbSymbolTableIteratorImpl* pImpl);

  OdDbSymbolTableIteratorImpl* m_pImpl;
};

#include "DD_PackPop.h"

#endif // _ODDBSYMBOLTABLE_INCLUDED


