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



#ifndef OD_DBSORTENTSTABLE_H
#define OD_DBSORTENTSTABLE_H

#include "DD_PackPush.h"

#include "DbObject.h"

#define STL_USING_UTILITY
#include "OdaSTL.h"

/** Description:
    This template class is a specialization of the std::pair class for OdDbHandle-OdDbSoftPointerId pairs.
*/
typedef std::pair<OdDbHandle, OdDbSoftPointerId> HandlePair;

/** Description:
    This template class is a specialization of the OdArray class for OdDbHandle-OdDbSoftPointerId pairs.
*/
typedef OdArray<HandlePair> HandlePairsArray;


/** Description:
    This class implements the SortentsTable, which specifies the DrawOrder 
    of entities in an OdDbDatabase instance.

    Remarks:
    Each instance of this class contains the DrawOrder for a single OdDbBlockRecord. 
    
    When drawing entities in a BlockTable record, an iterator traverses the BlockTable record 
    in the order of ascending handles. If there is a HandlePair in the SortentsTable corresponding
    to the *handle* of the object about to be drawn, the entity specified by the entity ID in the *HandlePair* is
    drawn in its stead. If there is no such *HandlePair*, the entity referenced by the iterator is drawn.
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbSortentsTable : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbSortentsTable);

  OdDbSortentsTable();

  /** Description:
    Moves the entities with specified entity IDs to the bottom of this SortentsTable.

    Arguments:
    entityIds (I) Entity IDs.
  */
  void moveToBottom(
    OdDbObjectIdArray& entityIds);

  /** Description:
    Moves the entities with specified entity IDs to the top of this SortentsTable.

    Arguments:
    entityIds (I) Entity IDs.
  */
  void moveToTop(
    OdDbObjectIdArray& entityIds); 

  /** Description:
    Moves the entities with specified entity IDs below the target entity in this SortentsTable.

    Arguments:
    entityIds (I) Entity IDs.
    targetId (I) Target entity ID.
  */
  void moveBelow(
    OdDbObjectIdArray& entityIds, 
    OdDbObjectId targetId);

  /** Description:
    Moves the entities with specified entity IDs above  the target entity in this SortentsTable.

    Arguments:
    entityIds (I) Entity IDs.
    targetId (I) Target entity ID.
  */
  void moveAbove(
    OdDbObjectIdArray& entityIds, 
    OdDbObjectId targetId);

  /** Description:
    Swaps the DrawOrder of the specified entities in this SortentsTable.
    
    Arguments:
    firstId (I) First entity ID.
    secondId (I) Second entity ID.
  */
  void swapOrder(
    OdDbObjectId firstId, 
    OdDbObjectId secondId);

  /** Description:
    Returns the object ID of the BlockTable record to which this SortentsTable belongs.
  */
  OdDbObjectId  blockId() const; 
        
  ///  2005 functions
  ///

  /** Description:
    Returns true if and only if the first entity is drawn before the second with this SortentsTable.

    Arguments:
    firstID (I) First entity ID.
    secondID (I) Second entity ID.
    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  bool firstEntityIsDrawnBeforeSecond(
    OdDbObjectId firstID, 
    OdDbObjectId secondID) const;

  /** Description:
    Returns an array of the entity IDs of the entities in the associated BlockTable record,
    in the DrawOrder for this SortentsTable.

    Arguments:
    entityIds (O) Entity IDs.
    honorSortentsMask (I) SORTENTS mask. 

    Remarks:
    If (honorSortentsMask ^ SORTENTS) != honorSortentsMask, the entities are returned unsorted.
    
    Returns eOk if successful, or an appropriate error code if not.
    
    Note:
    As implemented, honorSortentsMask is ignored.
    It will be fully implemented in a future *release*.
  */
  void getFullDrawOrder(
    OdDbObjectIdArray& entityIds, 
    OdUInt8 honorSortentsMask = 0) const;

  /** Description:
    Rearranges the specified entity IDs into their current relative DrawOrder for this SortentsTable.
    
    Arguments:
    entityIds (I/O) Entity IDs.
    honorSortentsMask (I) SORTENTS mask. 

    Remarks:
    If (honorSortentsMask ^ SORTENTS) != honorSortentsMask, the the entities are returned unsorted.

    Returns eOk if successful, or an appropriate error code if not.

    Note:
    As implemented, honorSortentsMask is ignored.
    It will be fully implemented in a future *release*.
  */
  void getRelativeDrawOrder(
    OdDbObjectIdArray& entityIds, 
    OdUInt8 honorSortentsMask = 0) const;


  /** Description:
    Sets the relative DrawOrder for the specified entities in this SortentsTable.

    Arguments:
    entityIds (I) Entity IDs in DrawOrder.

    Remarks:
    The DrawOrder of other entities are unaffected.
    
    Returns eOk if successful, or an appropriate error code if not.
  */
  void setRelativeDrawOrder(
    const OdDbObjectIdArray& entityIds);

  /** Description:
    Sets this SortentsTable from an array of HandlePair pairs.
    
    Arguments:
    handlePairs (I) HandlePairs.
    
    Note:
    Use of this function is not recommended.
  */
  void setAbsoluteDrawOrder(
    const HandlePairsArray& handlePairs);

  /** Description:
    Returns this SortentsTable as an array of HandlePair pairs.
    
    Arguments:
    handlePairs (O) Receives an array of HandlePairs.
    
    Note:
    Use of this function is not recommended.
  */
  void getAbsoluteDrawOrder(
    HandlePairsArray& handlePairs);

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual void getClassID(
    void** pClsid) const;
};


/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbSortentsTable object pointers.
*/
typedef OdSmartPtr<OdDbSortentsTable> OdDbSortentsTablePtr;

#include "DD_PackPop.h"

#endif //OD_DBSORTENTSTABLE_H


