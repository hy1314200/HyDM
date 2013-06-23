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



#ifndef ODDB_DBSUBENT_H
#define ODDB_DBSUBENT_H /* {Secret} */

#include "DbObjectId.h"

/** Description:
    This class implements SubentityId objects for OdDbEntity objects in an OdDbDatabase.  

    Remarks:
    A given OdDbEntity object may consist of a number of graphical subentities.
    
    Subentity type will be one of the following:

    @table
    Name                  Value
    kNullSubentType       0
    kFaceSubentType       1
    kEdgeSubentType       2
    kVertexSubentType     3   
    MlineSubentCache      4  

    Each SubentityId object is created from a Subentity *type* and an *index*.
    
    Library: Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbSubentId
{
public:
  OdDbSubentId() : m_Type(OdDb::kNullSubentType), m_Index(0) 
  {}

  OdDbSubentId(
    OdDb::SubentType type, 
    OdInt32 index)  : m_Type(type), m_Index(index) 
  {}

  bool operator ==(
    const OdDbSubentId& subentId) const
  {
    return ((m_Index == subentId.m_Index) && (m_Type == subentId.m_Type));
  }
      
  bool operator !=(
    const OdDbSubentId& subentId) const
  {
    return ((m_Index != subentId.m_Index) || (m_Type != subentId.m_Type));
  }
  
  /** Description:
    Returns the *type* of this SubentId object.

    Remarks:
    
    *type* returns one of the following:

    @table
    Name                  Value
    kNullSubentType       0
    kFaceSubentType       1
    kEdgeSubentType       2
    kVertexSubentType     3   
    MlineSubentCache      4  
  */
  OdDb::SubentType type () const 
  { 
    return m_Type; 
  }
  
  /** Description:
    Sets the *type* of this SubentId object.

    Arguments:
    type (I) Type.
    
    Remarks:
    *type* will be one of the following:

    @table
    Name                  Value
    kNullSubentType       0
    kFaceSubentType       1
    kEdgeSubentType       2
    kVertexSubentType     3   
    MlineSubentCache      4  
  */
  void setType(
    OdDb::SubentType type) 
  { 
    m_Type = type; 
  }
  /** Description:
    Returns the *index* of this SubentId object.
  */
  OdInt32 index() const 
  { 
    return m_Index; 
  }
  
  /** Description:
    Sets the *index* of this SubentId object.
    Arguments:
    index (I) Index.
  */
  void setIndex(
    OdInt32 index) 
  { 
    m_Index = index; 
  }
  
private:
  OdDb::SubentType m_Type;
  OdInt32          m_Index;
};

/** Description:
    This class uniquely defines subentities within entities in an OdDbDatabase.  

    Remarks:
    Each OdDbFullSubentPath object consists of a OdDbSubentId object and
    an ordered array of object Ids. The SubentId contains the index and subentType
    of the object. The array of object Ids defines the path to the 
    subentity from the outermost entity (in PaperSpace or ModelSpace) 
    to the entity containing the subentity.
    
    Library: Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbFullSubentPath
{
public:
  OdDbFullSubentPath() 
  {}
  OdDbFullSubentPath(
    OdDb::SubentType type, 
    OdInt32 index) : m_SubentId(type, index) 
  {}
    
  //OdDbFullSubentPath(OdDbObjectId entId, OdDb::SubentType type, OdInt32 index);
  //OdDbFullSubentPath(OdDbObjectId entId, OdDbSubentId subId);
  //OdDbFullSubentPath(OdDbObjectIdArray objectIds, OdDbSubentId subId);
  //OdDbFullSubentPath(const OdDbFullSubentPath&);
  //~OdDbFullSubentPath();

  //OdDbFullSubentPath& operator =(const OdDbFullSubentPath&);
  //bool operator ==(const OdDbFullSubentPath& id) const;
  //bool operator !=(const OdDbFullSubentPath& id) const;

  /** Description:
    Returns a reference to the embedded OdDbObjectIdArray object in this FullSubentPath object.
    
    Arguments:
    objectIds (O) Receives the reference.
  */
  void objectIds(
    OdDbObjectIdArray& objectIds) const
  {
    objectIds = m_ObjectIds;
  }
  
  OdDbObjectIdArray& objectIds()
  {
    return m_ObjectIds;
  }
  
  /** Description:
    Returns a reference to, or a copy of, the embedded OdDbSubentId object in this FullSubentPath object.
  */
  const OdDbSubentId subentId() const
  {
    return m_SubentId;
  }

  OdDbSubentId& subentId()
  {
    return m_SubentId;
  }
  
private:              
  OdDbObjectIdArray m_ObjectIds;
  OdDbSubentId      m_SubentId;
};

#endif



