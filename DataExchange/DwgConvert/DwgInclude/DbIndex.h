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



#ifndef OD_DBINDEX_H
#define OD_DBINDEX_H

#include "DD_PackPush.h"

class OdDbDate;
class OdDbIndexIterator; 
class OdDbFilter;
class OdDbIndexUpdateData;
class OdDbBlockChangeIterator;
class OdDbFilter;
class OdDbBlockTableRecord;
class OdDbBlockChangeIterator;
class OdDbIndexUpdateData;
class OdDbBlockChangeIteratorImpl;
class OdDbIndexUpdateDataImpl;
class OdDbIndexUpdateDataIteratorImpl;
class OdDbFilteredBlockIterator;

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbFilteredBlockIterator object pointers.
*/
typedef OdSmartPtr<OdDbFilteredBlockIterator> OdDbFilteredBlockIteratorPtr;

#include "DbFilter.h"

extern void processBTRIndexObjects(
  OdDbBlockTableRecord* pBlock, 
  int indexCtlVal,
  OdDbBlockChangeIterator* pBlkChgIter,
  OdDbIndexUpdateData* pIdxUpdData);


/** Description:
    This class iterates through changed entities in an OdDbBlockTableRecord instance.
    
    Library:
    Db

    {group:OdDb_Classes}
*/
class OdDbBlockChangeIterator
{
private:
  friend class OdDbBlockChangeIteratorImpl;
  OdDbBlockChangeIteratorImpl* m_pImpl;
  OdDbBlockChangeIterator() : m_pImpl(0) {}
  OdDbBlockChangeIterator(
    const OdDbBlockChangeIterator&);
public:

  /** Description:
    Sets this Iterator object to reference the entity that it would normally return first.
     
    Remarks:
    Allows multiple traversals of the iterator list.
  */  
  void start();

  /** Description:
    Returns the Object ID of the entity currently referenced by this Iterator object.
  */  
  OdDbObjectId id() const;
  
  /** Description:
    Sets this Iterator object to reference the entity following the current entity.
  */  
  void next();

  /** Description:
    Returns true if and only if the traversal by this Iterator object is complete.
  */  
  bool done(); 
  
  /** Description:
    Returns the Object ID of the entity currently referenced by this Iterator object, 
    and the *flags* and *data* associated with it.
    
    Arguments:
    currentId (O) Receives the current Object ID.
    flags (O) Receives the 8-bit *flags*.
    data (O) Receives the 32-bit *data*
  */  
  void curIdInfo(
    OdDbObjectId& currentId, 
    OdUInt8& flags, 
    OdUInt32& data) const;
    
  /** Description:
    Sets the the *flags* and *data* associated with the object currently referenced by this Iterator object.
    
    Arguments:
    flags (I) Current object 8-bit *flags*.
    data (I) Current object 32-bit *data*
  */  
  void setCurIdInfo(
    OdUInt8 flags, 
    OdUInt32 data);
  
  /** Description:
    Returns a pointer to the OdDbIndexUpdateData object associating Object IDs to *data* and *flags*. 
  */
  OdDbIndexUpdateData* updateData() const;
  
  /** Description:
    Clears the processed bit (0x04) of the flags of entities being traversed.
  */
  void clearProcessedFlags();
};

/** Description:
    This class associates Index specific flags and data with an OdDbObjectId. 
    
    Library:
    Db
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbIndexUpdateData 
{
  friend class OdDbIndexUpdateDataImpl;
  OdDbIndexUpdateDataImpl* m_pImpl;
  OdDbIndexUpdateData() : m_pImpl(0) {}
  OdDbIndexUpdateData(
    const OdDbIndexUpdateData&);
public:
  enum UpdateFlags 
  { 
    kModified   = 1, // Modified RO
    kDeleted    = 2, // Deleted RO
    kProcessed  = 4, // Processed RW
    kUnknownKey = 8  // Unknown key
  };
  /** Description:
    Returns the Object ID of the OdDbBlockTableRecord instance that owns this OdDbIndexUpdateData object.
  */
  OdDbObjectId objectBeingIndexedId() const;

  
  /** Description:
    Adds the specified Object ID to this OdDbIndexUpdateData object.

    Arguments:
    object ID (I) Object ID to be added.
  */
  void addId(
    OdDbObjectId objectId);
  
  /** Description:
    Sets the *flags* associated with the specified Object ID in this OdDbIndexUpdateData object.
    
    Arguments:
    objectID (I) Object ID.
    flags (I) 8-bit *flags*.
    
    Remarks:
    Returns true if and only if successful.
  */
  bool setIdFlags(
    OdDbObjectId objectId, 
    OdUInt8 flags);

  /** Description:
    Sets the *data* associated with the specified Object ID in this OdDbIndexUpdateData object.
    
    Arguments:
    objectID (I) Object ID.
    data (I) 32-bit *data*.
    
    Remarks:
    Returns true if and only if successful.
  */
  bool setIdData(
    OdDbObjectId objectId, 
    OdUInt32 data);

  /** Description:
    Returns the *data* associated with the specified Object ID in this OdDbIndexUpdateData object.
    
    Arguments:
    objectID (I) Object ID.
    data (O) 32-bit *data*.
    
    Remarks:
    Returns true if and only if successful.
  */
  bool getIdData(
    OdDbObjectId objectId, 
    OdUInt32& data) const;

  /** Description:
    Returns the *flags* associated with the specified Object ID in this OdDbIndexUpdateData object.
    
    Arguments:
    objectID (I) Object ID.
    flags (O) Receives the 8-bit *flags*.
    
    Remarks:
    Returns true if and only if successful.
  */
  bool getIdFlags(
    OdDbObjectId objectId, 
    OdUInt8& flags) const; 

  /** Description:
    Returns the *flags* and *data* associated with the specified Object ID in this OdDbIndexUpdateData object.
    
    Arguments:
    objectID (I) Object ID.
    flags (O) Receives the 8-bit *flags*.
    data (O) Receives the 32-bit *data*.
    
    Remarks:
    Returns true if and only if successful.
  */
  bool getFlagsAndData(
    OdDbObjectId objectId, 
    OdUInt8& flags, 
    OdUInt32& data) const;
};

/** Description:
    This class iterates through OdDbIndexUpdateData instances.    

    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbIndexUpdateDataIterator
{
  friend class OdDbIndexUpdateDataIteratorImpl;
  OdDbIndexUpdateDataIteratorImpl* m_pImpl;
public:
  OdDbIndexUpdateDataIterator(
    const OdDbIndexUpdateData* pIndexUpdateData);
  
  /** Description:
    Sets this Iterator object to reference the entity that it would normally return first.
     
    Remarks:
    Allows multiple traversals of the iterator list.
  */  
  void start();

  /** Description:
    Returns the Object ID of the entity currently referenced by this Iterator object.
  */  
  OdDbObjectId id() const;
  
 /** Description:
    Sets this Iterator object to reference the entity following the current entity.
  */  
  void next();

  /** Description:
    Returns true if and only if the traversal by this Iterator object is complete.
  */  
  bool done(); 
  
  /** Description:
    Returns the Object ID of the entity currently referenced by this Iterator object, 
    and the *flags* and *data* associated with it.
    
    Arguments:
    currentId (O) Receives the current Object ID.
    flags (O) Receives the 8-bit *flags*.
    data (O) Receives the 32-bit *data*
  */  
  void currentData(
    OdDbObjectId& currentId, 
    OdUInt8& flags, 
    OdUInt32& data) const;
};


/** Description:
    The class is the base class for all OdDb Index objects.

    Library:
    Db
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbIndex : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbIndex);

  OdDbIndex();
  
  
  /** Description:
    Creates an Iterator object that provides access to the entities in this Index object.
    
    Arguments:
    pFilter (I) Pointer to the filter to be applied to this Index object.
  */ 
  virtual OdDbFilteredBlockIteratorPtr newIterator(
    const OdDbFilter* pFilter) const;
 
  /** Description:
    Fully rebuilds this Index object from the entities in the block table record object.
    Arguments:
    pIdxData (I) Pointer to the OdDbIndexUpdateData object to be used in the rebuild. 
  */ 
  virtual void rebuildFull(
    OdDbIndexUpdateData* pIdxData);
  
  /** Description:
    Sets the Julian *lastUpdatedAt* timestamp for this Index object.
    
    Arguments:
    time (I) Julian date.
  */
  void setLastUpdatedAt(
    const OdDbDate& time);
    
  /** Description:
    Returns the Julian *lastUpdatedAt* timestamp for this Index object.
  */
  OdDbDate lastUpdatedAt() const;
  
  /** Description:
    Sets the UT *lastUpdatedAt* timestamp for this Index object.
    
    Arguments:
    time (I) UT date.
  */
  void setLastUpdatedAtU(
    const OdDbDate& time);
    
  /** Description:
    Returns the UT *lastUpdatedAt* timestamp for this Index object.
  */
  OdDbDate lastUpdatedAtU() const;
  
  /** Description:
    Returns true if and only if this Index object is up to date.
  */
  bool isUptoDate() const; 
  
  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

protected:
 
  /** Description:
    Called by OdDbIndexFilterManager::updateIndexes() when only modifications are being registered. 

    Arguments:
    iterator (I) Iterator of *modified* entities.
    
    Remarks:
    Modified entities includes added, deleted, and changed entities.
    
    Note:
    This class must be implemented in custom classes derived from OdDbIndex. A full rebuild may be performed if desired.
 */
 virtual void rebuildModified(
    OdDbBlockChangeIterator* iterator);
  
  friend class OdDbIndexImpl;
  friend void processBTRIndexObjects(
    OdDbBlockTableRecord* pBTR, 
    int indexCtlVal, 
    OdDbBlockChangeIterator* pBlkChgIter, 
    OdDbIndexUpdateData* pIdxUpdData );
};
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbIndex object pointers.
*/
typedef OdSmartPtr<OdDbIndex> OdDbIndexPtr;

class OdDbBlockTableRecord;
class OdDbBlockReference;

/** Description:
    This namespace provides functions pertaining to indices and filters.
    
    Library:
    Db
    
    {group:DD_Namespaces}
*/
namespace OdDbIndexFilterManager
{
  /** Description:
    Updates all Index objects associated with all block table record objects in the specified
    OdDbDatabase object.
    
    Arguments:
    pDb (I) Pointer to the OdDbDatabase object.
  */
  TOOLKIT_EXPORT void updateIndexes(
    OdDbDatabase* pDb);
  
  /** Description:
    Adds the specified Index object to the specified block table record object.
    
    Arguments:
    pBTR (I) Pointer to the block table record object.
    pIndex (I) Pointer to the Index object.
    
    Remarks:
    An *index* of the same OdRxClass as the specified *index* will be deleted.
  */
  TOOLKIT_EXPORT void addIndex(
    OdDbBlockTableRecord* pBTR, 
    OdDbIndex* pIndex);
  
  /** Description:
    Removes the specified Index object from the specified block table record object.
    
    Arguments:
    pBTR (I) Pointer to the BlockTable record.
    key (I) Class descriptor to specify the *index*.
  */
  TOOLKIT_EXPORT void removeIndex(
    OdDbBlockTableRecord* pBTR, 
    const OdRxClass* key);

  
  /** Description:
    Returns a SmartPointer to the specified Index object.
    
    Arguments:
    pBTR (I) Pointer to the block table record object.
    key (I) Class descriptor to specify the *index* object.
    readOrWrite (I) Mode in which to open the *index* object.
  */
  TOOLKIT_EXPORT OdDbIndexPtr getIndex(
    const OdDbBlockTableRecord* pBTR, 
    const OdRxClass* key, 
    OdDb::OpenMode readOrWrite = OdDb::kForRead);

  /** 
    Arguments:
    index (I) Position of the Index object within the block table record object.
  */  
  TOOLKIT_EXPORT OdDbIndexPtr getIndex(
    const OdDbBlockTableRecord* pBTR, 
    int index, 
    OdDb::OpenMode readOrWrite = OdDb::kForRead);
  
  /** Description:
    Returns the number of indices associated with the block table record object.
    
    Arguments:
    pBTR (I) Pointer to the block table record object.
  */
  TOOLKIT_EXPORT int numIndexes(
    const OdDbBlockTableRecord* pBTR);
  
  /** Description:
    Adds the specified Filter object to the specified block reference entity.
    
    Arguments:
    pBlkRef (I) Pointer to the block reference entity.
    pFilter (I) Pointer to the Filter object.
    
    Remarks:
    An filter of the same OdRxClass as the specified filter will be deleted.
  */
  TOOLKIT_EXPORT void addFilter(
    OdDbBlockReference* pBlkRef, 
    OdDbFilter* pFilter);
  
  /** Description:
    Removes the specified Filter object from the specified block reference entity.
    
    Arguments:
    pBlkRef (I) Pointer to the block reference entity.
    key (I) Class descriptor to specify the filter.
  */
  TOOLKIT_EXPORT void removeFilter(
    OdDbBlockReference* pBlkRef, 
    const OdRxClass* key);
  
  /** Description:
    Returns a SmartPointer to the specified Filter object.
    
    Arguments:
    pBlkRef (I) Pointer to the block reference entity.
    key (I) Class descriptor to specify the filter object.
    readOrWrite (I) Mode in which to open the filter object.
  */
  TOOLKIT_EXPORT OdDbFilterPtr getFilter(
    const OdDbBlockReference* pBlkRef, 
    const OdRxClass* key, 
    OdDb::OpenMode readOrWrite);
  
  /** 
    Arguments:
    index (I) Position of the Index object within the block table record object.
  */  
  TOOLKIT_EXPORT OdDbFilterPtr getFilter(
    const OdDbBlockReference* pBlkRef, 
    int index, 
    OdDb::OpenMode readOrWrite);
  
  /** Description:
    Returns the number of indices associated with the block reference entity.
    
    Arguments:
    pBlkRef (I) Pointer to the block reference entity.
  */
  TOOLKIT_EXPORT int numFilters(
    const OdDbBlockReference* pBlkRef);
}

#include "DD_PackPop.h"

#endif // OD_DBINDEX_H


