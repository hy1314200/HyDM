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



#ifndef __ODDBIDMAPPING_INCLUDED__
#define __ODDBIDMAPPING_INCLUDED__

#include "DD_PackPush.h"

#include "OdaDefs.h"
#include "DbObjectId.h"
#include "DbObject.h"
#include "RxObject.h"

class OdDbDatabase;

/** Description:
    This class is the element class for OdDbMapping, 
    which is used in deepclone operations to map
    object IDs from the original objects to their clones.
  
    Library:
    Db.
    
    Remarks:
    Key is the object ID of the original object.  
    Value is the object ID of the cloned object.  
    
    {group:OdDb_Classes}
*/
class OdDbIdPair
{
public:
  /**
    Arguments:
    source (I) OdDbIdPair to be copied.
    key (I) Object ID to use as *key*.
    value (I) Object ID to use as Value.
    cloned (I) Key object has been *cloned*.
    ownerXlated (I) Owner of *key* object has been translated.    
  */
  OdDbIdPair()
    : m_bCloned(false), m_bOwnerXlated(false) { }
    
  OdDbIdPair(
    const OdDbIdPair& source)
    : m_Key(source.key())
    , m_Value(source.value())
    , m_bCloned(source.isCloned())
    , m_bOwnerXlated(source.isOwnerXlated()) { }

  OdDbIdPair(
    const OdDbObjectId& key)
    : m_Key(key)
    , m_bCloned(false)
    , m_bOwnerXlated(false) { }
    
  OdDbIdPair(
    const OdDbObjectId& key, 
    const OdDbObjectId& value, 
    bool cloned = false, 
    bool ownerXlated = true)
    : m_Key(key)
    , m_Value(value)
    , m_bCloned(cloned)
    , m_bOwnerXlated(ownerXlated) { }

  
  /** Description:
    Returns the key for this IdPair object.
  */
  OdDbObjectId key() const { return m_Key; } 

  /** Description:
    Returns the value for this IdPair object.
  */
  OdDbObjectId value() const { return m_Value; }

  /** Description:
    Returns true if and only if the key object for this IdPair object has been cloned.
  */

  inline bool isCloned() const { return m_bCloned; }
  
  /** Description:
    Returns true if and only if the owner of the key object for this IdPair object has been translated.
  */
  inline bool isOwnerXlated() const { return m_bOwnerXlated; }
  
  /** Description:
    Sets the parameters for this IdPair object according to the arguments.

    Arguments:
    key (I) Object ID to use as *key*.
    value (I) Object ID to use as Value.
    cloned (I) Key object has been *cloned*.
    ownerXlated (I) Owner of *key* object has been translated.    
  */
  OdDbIdPair& set(
    const OdDbObjectId& key, 
    const OdDbObjectId& value,
    bool cloned = false, 
    bool ownerXlated = true)
  {
    setKey(key);
    setValue(value);
    setCloned(cloned);
    setOwnerXlated(ownerXlated);
    return *this;
  }

    
  /** Description:
    Sets the *key* for this IdPair object.

    Arguments:
    key (I) Object ID to use as *key*.
  */
  void setKey(
    const OdDbObjectId& key) { m_Key = key; }
    
  /** Description:
    Sets the *value* for this IdPair object.

    Arguments:
    value (I) Object ID to use as Value.
  */
  void setValue(
    const OdDbObjectId& value) { m_Value = value; }
    
  /** Description:
    Controls the *cloned* setting for this IdPair object.

    Arguments:
    cloned (I) Key object has been *cloned*.
  */
  void setCloned(
    bool cloned) { m_bCloned = cloned; }

  /** Description:
    Controls the owner translated setting for this IdPair object.

    Arguments:
    ownerXlated (I) Owner of *key* object has been translated.    
  */
  void setOwnerXlated(
    bool ownerXlated) { m_bOwnerXlated = ownerXlated; }
  
private:
  OdDbObjectId m_Key;
  OdDbObjectId m_Value;
  bool m_bCloned;
  bool m_bOwnerXlated;
};

class OdDbIdMappingIter;

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbIdMappingIter object pointers.
*/
typedef OdSmartPtr<OdDbIdMappingIter> OdDbIdMappingIterPtr;

/** Description: 
  
    {group:DD_Namespaces}
*/
namespace OdDb
{
  enum DeepCloneType
  {
    kDcCopy           = 0,    // Copy, Array, Mirror
    kDcExplode        = 1,    // Explode
    kDcBlock          = 2,    // Block definition
    kDcXrefBind       = 3,    // Xref Bind
    kDcSymTableMerge  = 4,    // Xref Attach, DxfIn, IgesIn
    kDcInsert         = 6,    // Insert of a DWG file
    kDcWblock         = 7,    // Wblock
    kDcObjects        = 8,    // OdDbDatabase::deepCloneObjects()
    kDcXrefInsert     = 9,    // Xref Insert, Xref BInd
    kDcInsertCopy     = 10,   // Insert() 
    kDcWblkObjects    = 11    // Wblock objects
  };
}

/** Description:
    This class is used in deepclone operations to map, using OdDbIdPair objects,
    object IDs from the original objects to their clones.
    
    Library:
    Db
    
    Remarks:
    There is no mechanism to clear all the mappings in an OdDbIdMapping instance; a new
    instance must be created for each deepclone operation.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbIdMapping : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdDbIdMapping);

  OdDbIdMapping() {}
  
  /** Description:
    Adds the specified IdPair object to this IdMapping object.
    
    Arguments:
    idPair (I) IdPair to add.
  */
  virtual void assign(
    const OdDbIdPair& idPair) = 0;

  /** Description:
    Returns the *value* of the IdPair in this IdMapping object
    that matches the *key* in the specified IdPair object.
    
    Arguments:
    idPair (I/O) Supplies the *key* and receives the *value*.
    
    Remarks:
    Returns true if and only if the *key* is found. 
  */
  virtual bool compute(
    OdDbIdPair& idPair) const = 0;
  /** Description:
    Deletes the IdPair with the specified *key* from this IdMapping object.
    
    Arguments:
    key (I) Object ID *key* to delete.
  */
  virtual bool del(
    const OdDbObjectId& key) = 0;

  /** Description:
    Creates an Iterator object that provides access to the IdPair objects in this IdMapping object.
  */ 
  virtual OdDbIdMappingIterPtr newIterator() = 0;
  
  /** Description:
    Returns a pointer to the destination *database* for the deepclone operation using this IdMapping object. 
  */
  virtual OdDbDatabase* destDb() const = 0;
  
  /** Description:
    Sets the destination *database* for the deepclone operation using this IdMapping object.
    
    Arguments:
    pDb (I) Pointer to the destination *database*. 
  */
  virtual void setDestDb(
    OdDbDatabase* pDb) = 0;

  /** Description:
    Returns a pointer to the source *database* for the deepclone operation using this IdMapping object. 
  */
  virtual OdDbDatabase* origDb() const = 0;
  
  virtual OdDbObjectId insertingXrefBlockId() const = 0;
  
  /** Description:
    Returns the type of deepclone operation using this IdMapping object.
    
    Remarks:
    deepCloneContext will return one of the of the following:
    
    @table
    Name                Value   Description
    kDcCopy             0       Copy Array Mirror
    kDcExplode          1       Explode
    kDcBlock            2       Block definition
    kDcXrefBind         3       Xref Bind
    kDcSymTableMerge    4       Xref Attach DxfIn IgesIn
    kDcInsert           6       Insert of a DWG file
    kDcWblock           7       Wblock
    kDcObjects          8       OdDbDatabase::deepCloneObjects()
    kDcXrefInsert       9       Xref Insert Xref BInd
    kDcInsertCopy       10      Insert() 
    kDcWblkObjects      11      Wblock objects
    
  */
  virtual OdDb::DeepCloneType deepCloneContext() const = 0;
  
  /** Description:
    Returns the type of duplicate record cloning for IdMapping object.
    
    Remarks:
    duplicateRecordCloning will return one of the of the following:
    
    @table
    Name                  Value   Description
    kDrcNotApplicable     0       Not applicable to the object.
    kDrcIgnore            1       If a duplicate record exists, use the existing record in the *database*, and ignore the clone.
    kDrcReplace           2       If a duplicate record exists, replace it with the cloned record.
    kDrcXrefMangleName    3       Incoming record names are mangled with <xref>$0$<name>
    kDrcMangleName        4       Incoming record names are mangled with $0$<name>
    kDrcUnmangleName      5       Unmangle the names mangled by kDrcMangleName, then default to kDrcIgnore. Typically used by RefEdit when checking records into the original *database*.  

  */
  virtual OdDb::DuplicateRecordCloning duplicateRecordCloning() const = 0;
};

typedef OdSmartPtr<OdDbIdMapping> OdDbIdMappingPtr;

/** Description:
    This class implements Iterator objects that traverse OdDbIdPair records in OdDbIdMapping objects.

    Library:
    Db.
    
  
    Remarks:
    Returned by OdDbIdMapping::newIterator().

    {group:OdDb_Classes}
*/
class OdDbIdMappingIter : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdDbIdMappingIter);

  OdDbIdMappingIter() {}
  
  /** Description:
    Sets this Iterator object to reference the OdIdPair that it would normally return first.
     
    Remarks:
    Allows multiple traversals of the Iterator list.
  */  
  virtual void start() = 0;
  
  /** Description:
    Returns the IdPair pointed to by this Iterator Object.
    
    Arguments:
    idPair (O) IdPair.
  */ 
  virtual void getMap(
    OdDbIdPair& idPair) = 0;

  /** Description:
    Increments this Iterator object.
  */
  virtual void next() = 0;
  
  /** Description:
    Returns true if and only if the traversal of the OdDbMapping instance is complete.
  */  
  virtual bool done() = 0;
};


#include "DD_PackPop.h"

#endif // __ODDBIDMAPPING_INCLUDED__



