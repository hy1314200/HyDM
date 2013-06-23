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



#ifndef _ODDBDICTIONARY_INCLUDED_
#define _ODDBDICTIONARY_INCLUDED_

#include "DD_PackPush.h"

#include "RxIterator.h"
#include "RxDefs.h"
#include "DbObject.h"

class OdDbDictionaryImpl;
class OdDbObjectId;
class OdString;

/** Description:
    This class implements Iterator objects that traverse entries in OdDbDictionary objects in an OdDbDatabase.
    
    Remarks:
    An OdDbDictionaryIterator maintains a "current position" within the entries
    of the associated dictionary, and can provide access to the key value and *object* at the
    current position.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbDictionaryIterator : public OdRxIterator
{
public:
  ODRX_DECLARE_MEMBERS(OdDbDictionaryIterator);

  virtual ~OdDbDictionaryIterator() {}

  /** Description:
      Returns the *name* (key) of the dictionary entry at the current position specified
      by this Iterator object.
  */
  virtual OdString name() const = 0;

  /** Description:
      Opens the dictionary entry at the current position specified by this Iterator object.    

      Arguments:
      mode (I) Mode in which to open the *object*.

      Remarks:
      Returns a SmartPointer to the opened *object*.
  */
  virtual OdDbObjectPtr getObject(
    OdDb::OpenMode mode = OdDb::kForRead) = 0;

  /** Description:
      Returns the Object ID of the dictionary entry at the current position specified by
      this Iterator object.
  */
  virtual OdDbObjectId objectId() const = 0;

  /** Description:
      Sets the current position of this iterator to the dictionary entry containing the
      specified ObjectId.

      Arguments:
      objectId (I) Object ID of item to which the current position will be set.

      Remarks:
      Returns true if and only if the current position was set to the specified item.
  */
  virtual bool setPosition(
    OdDbObjectId objectId) = 0;

  /** Description:
      Opens the dictionary entry at the current position specified by this iterator in OdDb::kForRead mode.

      Remarks:
      Returns a SmartPointer to the opened *object*.
  */
  virtual OdRxObjectPtr object() const;

protected:
  
  OdDbDictionaryIterator() {}
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbDictionaryIterator object pointers..
*/
typedef OdSmartPtr<OdDbDictionaryIterator> OdDbDictionaryIteratorPtr;

/** Description:
    This class implements *database* -resident object dictionaries.
    
    Remarks:
    
    o  Each instance of OdDbDictionary is a single container object, in which
       entries are searched, added, modified, and deleted.  
  
    o  Each OdDbDictionary entry associates a unique *name* (key) string with a unique OdDbObject.
    
    o  Anonymous names are signified by specifying a name starting with an asterisk; e.g., *U. 
       A unique *name* (also starting with an asterisk) will be constructed for the entry.

    o  Entry names honor the rules of Symbol names
    
        o  Names may be any length.
        
        o  Names are case-insensitve
        
        o  Names may not contain any of the following characters
    
                      | * \ : ; < > ? " , equals

    See Also:
    OdDbDictionaryIterator
    
    Library: Db
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbDictionary : public OdDbObject
{
  ODDB_DECLARE_MEMBERS(OdDbDictionary);

protected:
  OdDbDictionary();

public:
  /** Description:
    Returns a SmartPointer to, or Object ID of, the OdDbObject associated with the specified *name* (key) in this Dictionary object.

    Arguments:
    name (I) Entry *name*.
    mode (I) Mode in which to open the object.
    pStatus (O) Returns eOk if successful, or an appropriate error code if not.

    Remarks:
    Opens the associated OdDbObject in the specified mode.
    
    Returns a null SmartPointer or Object ID if the specified entry could not be opened.
  */
  OdDbObjectPtr getAt(
    const OdString& name, 
    OdDb::OpenMode mode) const;

  OdDbObjectId getAt(
    const OdString& name, 
    OdResult* pStatus = NULL) const;

  /** Description:
    Returns the entry *name* (key) of the specified object in this Dictionary object.

    Arguments:
    objectId (I) Object ID.
    
    Remarks:
    Returns an empty string if objectId is not in this Dictionary object.
  */
  OdString nameAt(
    const OdDbObjectId& objectId) const;

  /** Description:
    Returns true if and only if this Dictionary object *has* an entry with the specified *name* (key) or Object ID.

    Arguments:
    name (I) Entry *name*.
    objectId (I) Object ID.
  */
  bool has(
    const OdString& name) const;
  bool has(
    const OdDbObjectId& objectId) const;

  /** Description:
    Returns the number of entries in this Dictionary object.
  */
  OdUInt32 numEntries() const;

  /** Description:
    Removes the entry with the specified *name* (key) or Object ID from this Dictionary object, and returns
    the Object ID of the removed entry.

    Arguments:
    name (I) Entry *name*.
    objectId (I) Object ID.
    
    Note:
    Only the dictionary entry is removed; the associated object remains in the *database* without an owner.

    The dictionary is removed as a persistent reactor on the associated object. 
  */
  OdDbObjectId remove(
    const OdString& name);
  void remove(
    const OdDbObjectId& objectId);

  /** Description:
    Changes the *name* (key) of the specified entry in this Dictionary object.

    Arguments:
    oldName (I) Name of entry to change.
    newName (I) New *name* for the entry.

    Remarks:
    Returns true and only if the entry *name* was successfully changed.
  */
  bool setName(
    const OdString& oldName, 
    const OdString& newName);

  /** Description:
    Sets the value for the specified entry in this Dictionary object.

    Remarks:
    Returns the Object ID of the newly added entry.
    
    If an entry the specified *name* exists in the dictionary, it is
    erased, and a new entry created.

    If an entry for this *name* does not exist in this dictionary, 
    a new entry is created. 

    Arguments:
    name (I) Name of entry to be changed.
    newValue (I) Database object to be added.
  */
  OdDbObjectId setAt(
    const OdString& name, 
    OdDbObject* newValue);

  /** Description:
    Returns true if and only if this Dictionary object is the hard owner of its elements.
     
    Remarks:
    Hard ownership protects the elements from purge. Soft ownership does not. 
  */
  bool isTreatElementsAsHard() const;

  /** Description:
    Sets the hard ownership property for this Dictionary object.

    Arguments:
    hard (I) Controls ownership.

    Remarks:
    Hard ownership protects the elements from purge. Soft ownership does not. 
  */
  void setTreatElementsAsHard(
    bool doIt);

  /** Description:
    Returns an new interator that can be used to traverse the entries in this Dictionary object.

    Arguments:
    iterType (I) Type of iterator.

    Remarks:
    The iterator type can be one of the following:

    @table
    Name                Description
    kDictCollated       Traverses the entries in the order they were added to the dictionary.
    kDictSorted         Traverses the entries in alphabetical order by key value.
  */
  OdDbDictionaryIteratorPtr newIterator(
    OdRx::DictIterType iterType = OdRx::kDictCollated) const;

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  /** Description:
      Returns the merge *style* for this dictionary object.
  */
  virtual OdDb::DuplicateRecordCloning mergeStyle() const;

  /** Description:
      Sets the merge *style* for this dictionary object.
      
      Arguments:
      style (I) Merge *style*.
  */
  virtual void setMergeStyle(
    OdDb::DuplicateRecordCloning style);

  virtual void goodbye(
    const OdDbObject* pObject);

  virtual void erased(
    const OdDbObject* pObject, 
    bool erasing = true);

  virtual void getClassID(
    void** pClsid) const;

  /*
     virtual void decomposeForSave(OdDb::DwgVersion ver,
                                   OdDbObject** replaceObj, 
                                   OdDbObjectId& replaceId, 
                                   bool& exchangeXData);
     virtual OdResult subErase(bool pErasing = true);
  */

};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbDictionary pointer objects.
*/
typedef OdSmartPtr<OdDbDictionary> OdDbDictionaryPtr;

#include "DD_PackPop.h"

#endif


