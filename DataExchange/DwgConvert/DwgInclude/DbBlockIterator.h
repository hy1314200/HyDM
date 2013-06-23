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



#ifndef ODDBBLOCKITERATOR_H
#define ODDBBLOCKITERATOR_H

#include "RxObject.h"
#include "OdArrayPreDef.h"

class OdDbObjectId;
class OdDbFilter;
class OdDbBlockTableRecord;
class OdDbBlockIterator;
/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbBlockIterator object pointers.
*/
typedef OdSmartPtr<OdDbBlockIterator> OdDbBlockIteratorPtr;

/** Description:
    This class implements Iterator objects that traverse entries in OdDbBlockTableRecord objects in an OdDbDatabase.
  
    Remarks:
    Returned by OdDbBlockTableRecord::newIterator().

    {group:OdDb_Classes}
*/
class  TOOLKIT_EXPORT OdDbBlockIterator : public OdRxObject
{
protected: 
  OdDbBlockIterator() {}
public:
  ODRX_DECLARE_MEMBERS(OdDbBlockIterator);
  
  /** Description:
    Sets this Iterator object to reference the OdDbBlock that it would normally return first.
     
    Remarks:
    Allows multiple traversals of the Iterator list.
  */  
  virtual void start() = 0;
  
  /** Description:
    Returns the object Id of the next object, and increments this Iterator object.
  */
  virtual OdDbObjectId next() = 0;
  
  /** Description:
    Returns the Object ID of the record currently referenced by this Iterator object.
  */
  virtual OdDbObjectId id() const = 0;
  
  /** Description:
    Positions this Iterator object at the specified record.
    Arguments:
    objectId (I) Object ID of the record.
    Remarks:
    Returns true if and only if successful.
  */  
  virtual bool seek(
    OdDbObjectId objectId) = 0;

 /** Description:
    Returns an Iterator object that can be used to traverse the specified BlockTable record.

    Arguments:
    pBtr (I) Pointer the the BlockTable record to traverse.
  */
  static OdDbBlockIteratorPtr newBlockIterator(
    const OdDbBlockTableRecord* pBtr);

 /** Description:
    Returns an iterator that can be used to traverse queries defined by OdDbFilter objects 
    on the specified BlockTable record.

    Arguments:
    pBtr (I) Pointer the the BlockTable record to traverse.
    pFilter (I) Pointer to the filter.
  */
  static OdDbBlockIteratorPtr newFilteredIterator(
      const OdDbBlockTableRecord* pBtr, 
      const OdDbFilter* pFilter);

 /** Description:
    Returns an iterator that can be used to traverse queries defined by an array of OdDbFilter objects 
    on the specified BlockTable record.

    Arguments:
    pBtr (I) Pointer the the BlockTable record to traverse.
    filters (I) Array of pointers to filter objects.
  */
  static OdDbBlockIteratorPtr newCompositeIterator(
      const OdDbBlockTableRecord* pBtr, 
      const OdArray<OdSmartPtr<OdDbFilter> >& filters);

};
/** Description:
  This class implements Iterator objects that traverse queries defined by OdDbFilter objects 
  on BlockTable records.

  Library:
  Db

  Remarks:
  This class is used by OdDbCompositeFilteredBlockIterator.
  {group:OdDb_Classes}
*/
class  TOOLKIT_EXPORT OdDbFilteredBlockIterator : public OdDbBlockIterator
{
protected: 
  OdDbFilteredBlockIterator() {}
public:
  ODRX_DECLARE_MEMBERS(OdDbFilteredBlockIterator);

  /** Description:
    Returns the estimated fraction [0.0, 1.0] of the contents that will
    be selected by the OdDbFilter query.  
    
    Remarks:
    Used to order the OdDbFilteredBlockIterator objects during a block traversal.
    
    o  0.0 forces the iterator to be used first.
    o  1.0 forces the iterator to be used last. 

    The filters with the fewest hits will be applied first.     
  */
  virtual double estimatedHitFraction() const = 0;
  
  /** Description:
    Returns true if and only if specified object passes the OdDbFilter query.
    
    Arguments:
    objectId (I) Object ID of the entity to be tested.
  */
  virtual bool accepts(
    OdDbObjectId objectId) const = 0;
    
  /** Description:
    Returns true if and only if the Index iterator, when not the primary iterator,
    is to buffer its output.
    
    Remarks:
    When false, only the first index/filter pair is traversed, with subsequent pairs
    queried via accepts().

    If true, after all the the IDs from the previous  
    iterator have been added to the buffer, the start(), next() and id() of this iterator are be used
    iterate through the data.
  */  
  virtual bool buffersForComposition() const;
  
  /** Description:
    Adds the specified object ID to the buffer of an Index iterator.
    
    Arguments:
    objectId (I) Object ID of the entity to be added.
    
    See Also:
    buffersForComposition    
  */
  virtual void addToBuffer(
    OdDbObjectId objectId);
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbFilteredBlockIterator object pointers.
*/
typedef OdSmartPtr<OdDbFilteredBlockIterator> OdDbFilteredBlockIteratorPtr;


#endif // ODDBBLOCKITERATOR_H


