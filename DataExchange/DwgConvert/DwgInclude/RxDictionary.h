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



#ifndef _RXDICTIONARY_INC_
#define _RXDICTIONARY_INC_


#include "RxObject.h"
#include "RxIterator.h"
#include "RxDefs.h"
#include "RxNames.h"

class OdString;

#include "DD_PackPush.h"

/** Description:

    {group:OdRx_Classes} 
*/
class FIRSTDLL_EXPORT OdRxDictionaryIterator : public OdRxIterator
{
public:
  ODRX_DECLARE_MEMBERS(OdRxDictionaryIterator);

  /** Description:
      Returns the entry key of the dictionary entry at the current position specified
      by this Iterator object.
  */
  virtual OdString getKey() const = 0;

  /** Description:
      Returns the entry ID of the dictionary entry at the current position specified
      by this Iterator object.
  */
  virtual OdUInt32 id() const = 0;
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdRxDictionaryIterator pointer objects.
*/
typedef OdSmartPtr<OdRxDictionaryIterator> OdRxDictionaryIteratorPtr;


/** Description:
    This class implements dictionaries of OdRxObject objects.
    
    Remarks:
    
    o  Each instance of OdRxbDictionary is a single container object, in which
       entries are searched, added, modified, and deleted.  
  
    o  Each OdRxDictionary entry associates a unique *name* (key) string or 32-Bit ID with a unique OdDbObject.
    
    o  Anonymous names are signified by specifying a name starting with an asterisk; e.g., *U. 
       A unique *name* (also starting with an asterisk) will be constructed for the entry.

    o  Entry names honor the rules of Symbol names
    
        o  Names may be any length.
        
        o  Names are case-insensitve
        
        o  Names may not contain any of the following characters
    
                      | * \ : ; < > ? " , equals

    See Also:
    OdRxDictionaryIterator
    
    Library: Db
    
    {group:OdRx_Classes} 
*/
class FIRSTDLL_EXPORT OdRxDictionary : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdRxDictionary);
  
  /** Description:
    Allocates storage for the specified number of entries in this Dictionary object.
    Arguments:
    minSize (I) Minimum number of entries. 
  */
  virtual void reserve(
    OdUInt32 minSize);

  /** Description:
    Returns a SmartPointer to the OdRxObject associated with the specified *name* (key) 
    or entry ID in this Dictionary object.

    Arguments:
    name (I) Entry *name*.
    id (I) Entry ID.

    Remarks:
    Returns a NULL SmartPointer if the specified entry could found.
  */
  virtual OdRxObjectPtr getAt(
    const OdChar* key) const = 0;
  virtual OdRxObjectPtr getAt(
    OdUInt32 id) const = 0;

  /** Description:
    Puts the specified object pointer into this Dictionary object.
    
    Arguments:
    key (I) Entry *name*.
    id (I) Entry ID.
    pObject (I) Pointer to the object.
    pRetId (I) Pointer to an OdUInt32 to receive the Entry ID of the entry.
    
    Remarks:
    Returns a SmartPointer to the object at this entry prior to the call.
    
    Note:
    key and pObject must not be NULL, and id > 0.
  */
  virtual OdRxObjectPtr putAt(
    const OdString& key, 
    OdRxObject* pObject, 
    OdUInt32* pRetId = NULL) = 0;
  virtual OdRxObjectPtr putAt(
    OdUInt32 id, 
    OdRxObject* pObject) = 0;
  
  /** Description:
    Sets the *key* associated with the specified entry ID for this Dictionary object.
    
    Arguments:
    id (I) Entry ID.
    newKey (I) New *key*.
    
    Remarks:
    Returns true if and only if successful.    
  */
  virtual bool resetKey(
    OdUInt32 id, 
    const OdString& newKey) = 0;

  /** Description:
    Removes the specified entry from this Dictionary object.
    
    Arguments:
    key (I) Entry *name*.
    id (I) Entry ID.
    
    Remarks:
    Returns a SmartPointer to the object referenced by the entry.
  */
  virtual OdRxObjectPtr remove(
    const OdChar* key) = 0;
  virtual OdRxObjectPtr remove(
    OdUInt32 id) = 0;


  /** Description:
    Returns true if and only if this Dictionary object *has* an entry with the specified *name* (key) or entry ID.

    Arguments:
    name (I) Entry *name*.
    id (I) Entry ID.
  */
  virtual bool has(
    const OdChar* name) const = 0;
  virtual bool has(
    OdUInt32 id) const = 0;


  /** Description:
    Returns the entry ID of the specified object in this Dictionary object.

    Arguments:
    key (I) Entry *name*.
    
    Remarks:
    Returns an empty string if id is not in this Dictionary object.
  */
  virtual OdUInt32 idAt(
    const OdChar* key) const = 0;

  /** Description:
    Returns the entry *name* (key) of the specified object in this Dictionary object.

    Arguments:
    id (I) Entry ID.
    
    Remarks:
    Returns an empty string if id is not in this Dictionary object.
  */
   virtual OdString keyAt(
    OdUInt32 id) const = 0;

  /** Description:
    Returns the number of entries in this Dictionary object.
  */
  virtual OdUInt32 numEntries() const = 0;

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
  virtual OdRxDictionaryIteratorPtr newIterator(
    OdRx::DictIterType iterType = OdRx::kDictCollated) = 0;

  /** Description: 
    Returns true if and only if keys for this Dictionary object are case-sensitive.
    
  */
  virtual bool isCaseSensitive() const = 0;
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdRxDictionary pointer objects.
*/
typedef OdSmartPtr<OdRxDictionary> OdRxDictionaryPtr;

FIRSTDLL_EXPORT OdRxDictionary* odrxSysRegistry();

FIRSTDLL_EXPORT OdRxDictionaryPtr odrxClassDictionary();

FIRSTDLL_EXPORT OdRxDictionaryPtr odrxServiceDictionary();

FIRSTDLL_EXPORT OdRxDictionaryPtr odrxCreateRxDictionary();


#include "DD_PackPop.h"

#endif // _RXDICTIONARY_INC_


