/******************************************************************************
 * FILE NAME  DBSSET.H
 * PURPOSE    
 *
 * SPEC       20.11.2002 Serge Kuligin
 * NOTES      
 *******************************************************************************/
#ifndef _DBSSET_H_
#define _DBSSET_H_

#include "Ge/GePoint3d.h"
#include "DbDatabase.h"
#include "DbObjectId.h"
#include "DbObject.h"
#include "DbObjectIterator.h"
#include "ResBuf.h"

class OdDbSelectionSet;
class OdDbBlockTableRecord;
class OdDbFilter;

/** Description:
    Iterator class that provides sequential access to the entities in a selection Set.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbSelectionSetIterator : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdDbSelectionSetIterator);

  /** Description:
      Opens the current object referenced by this iterator, in the specified mode.

      Return Value:
      The opened object (if successful), or a NULL smart pointer.
  */
  virtual OdDbObjectPtr getObject(OdDb::OpenMode) = 0;

  /** Description:
      Returns the Object ID of the current object referenced by this iterator.
  */
  virtual OdDbObjectId objectId() const = 0;

  /** Description:
      Returns true if there are no more objects to iterate through, false otherwise.
  */
  virtual bool done() const = 0;

  /** Description:
      Increments the current object for this iterator to the next object in the group.
  */
  virtual bool next() = 0;

protected:
  /** Description:
      Constructor (no arguments).
  */
  OdDbSelectionSetIterator() {}
};

typedef OdSmartPtr<OdDbSelectionSetIterator>  OdDbSelectionSetIteratorPtr;
typedef OdSmartPtr<OdDbSelectionSet>  OdDbSelectionSetPtr;

/** Description:
    Represents a Selection Set

    Remarks:
    Only entities from the current drawing's model space and papers space can be placed into the selection set,
    (not non-graphical objects or entities in other block definitions)

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbSelectionSet : public OdRxObject
{
protected:
  OdDbSelectionSet() {}
public:
  ODRX_DECLARE_MEMBERS(OdDbSelectionSet);

  enum SelSetStatus 
  {
    kNone = 0,
    kSelected,
    kCanceled,
    kRejected
  };
  /** Description:
      constructors/destructor
  */
  static OdDbSelectionSetPtr createObject(OdDbDatabase *pDb);

  virtual void  select(const OdDbBlockTableRecord* pBlock, const OdArray<OdSmartPtr<OdDbFilter> >& filters) = 0;

  /** Description:
      Returns an iterator that provides access to the entities in this Selection Set
  */
  virtual OdDbSelectionSetIteratorPtr newIterator() = 0;

  // selection mechanisms
  virtual void  filterOnlySelect(const OdResBuf* filter) = 0;
  virtual void  pointSelect(const OdGePoint3d& pt1, const OdResBuf* filter) = 0;

  virtual void  crossingSelect(const OdGePoint3d& pt1, const OdGePoint3d& pt2, const OdResBuf* filter) = 0;
  virtual void  windowSelect(const OdGePoint3d& pt1, const OdGePoint3d& pt2, const OdResBuf* filter) = 0;
  virtual void  boxSelect(const OdGePoint3d& pt1, const OdGePoint3d& pt2, const OdResBuf* filter) = 0;

  // inquiry
  /** Description:
      Returns the status of last selection operation.
  */
  virtual SelSetStatus  lastStatus() const = 0;

  // operations on the selection set 

  /** Description:
      Returns the number of entities in this group.

      Remarks:
  */
  virtual OdUInt32  numEntities() const = 0;

  /** Description:
      Appends the specified object to Selection Set.  

      Arguments:
      id (I) Object ID of OdDbEntity to be added 
  */
  virtual void      append(const OdDbObjectId& objId) = 0;

  /** Description:
      Removes the specified object from this selection Set.
  */
  virtual void      remove(const OdDbObjectId& objId) = 0;

  /** Description:
      Returns true if this selection Set contains the specified entity, false otherwise.
  */
  virtual bool      isMember(const OdDbObjectId& objId) = 0;

  /** Description:
      Clears the contents of this SelectionSet, so that it contains no entities.
  */
  virtual void      clear() = 0;

};


#endif //_DBSSET_H_
/*---------------- end of dbsset.h -----------------------------------------*/


