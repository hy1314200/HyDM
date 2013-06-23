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



#ifndef _ODDBOBJECTITERATOR_INCLUDED_
#define _ODDBOBJECTITERATOR_INCLUDED_

#include "RxObject.h"
#include "DbObjectId.h"

class OdDbEntity;
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbEntity pointer objects.
*/
typedef OdSmartPtr<OdDbEntity> OdDbEntityPtr;

/** Description:
    This class implements bidirectional Iterator objects that traverse entities contained in complex entities.
    
    
    OdDbBlockTable records in an OdDbDatabase.

    Library:
    Db.
    
    Remarks:
    Complex entities include the following:
    
    @table
    Entity               Iterated entities
    OdDbBlockReference   All
    OdDb2dPolyline       Vertex
    OdDb3dPolyline       Vertex
    OdDbPolyFaceMesh     Vertex
    OdDbPolygonMesh      Vertex

    This class cannot directly instantiated, but must be instantiatd with the
    iterator creation methods of the class to be iterated through.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbObjectIterator : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdDbObjectIterator);

  OdDbObjectIterator() {}

  /** Description:
    Sets this Iterator object to reference the *entity* that it would normally return first.
     
    Remarks:
    Allows multiple traversals of the iterator list.

    Arguments:
    atBeginning (I) True to start at the beginning, false to start at the end. 
    skipErased (I) If and only if true, *erased* records are skipped.
  */  
  virtual void start(
    bool atBeginning = true, 
    bool skipErased = true) = 0;

  /** Description:
    Returns true if and only if the traversal by this Iterator object is complete.
  */  
  virtual bool done() const = 0;

  /** Description:
    Returns the Object ID of the current *entity* referenced by this Iterator object.
  */
  virtual OdDbObjectId objectId() const = 0;

  /** Description:
    Opens and returns the *entity* referenced by this Iterator object.

    Arguments:
    openMode (I) Mode in which to open the *entity*.
    openErasedEntity (I) If and only if true, *erased* records will be opened or retrived.

    Remarks:
    Returns a SmartPointer to the opened *entity* if successful, otherwise a null SmartPointer.
  */
  virtual OdDbEntityPtr entity(
    OdDb::OpenMode openMode = OdDb::kForRead, 
    bool openErasedEntity = false) = 0;

  /** Description:
    Steps this Iterator object.

    Arguments:
    forward (I) True to step *forward*, false to step backward.
    skipErased (I) If and only if true, *erased* records are skipped.
  */
  virtual void step(
    bool forward = true, 
    bool skipErased = true) = 0;

  /** Description:
    Positions this Iterator object at the specified record.
    Arguments:
    objectId (I) Object ID of the *entity*.
    pEntity(I) Pointer to the *entity*.
  */  
  virtual bool seek(
    OdDbObjectId objectId) = 0;
  virtual bool seek(
    const OdDbEntity* pEntity) = 0;
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbObjectIterator pointer objects.
*/
typedef OdSmartPtr<OdDbObjectIterator> OdDbObjectIteratorPtr;

#endif //_ODDBOBJECTITERATOR_INCLUDED_


