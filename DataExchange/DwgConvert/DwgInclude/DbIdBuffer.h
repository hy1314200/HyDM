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



#ifndef OD_DBIDBUFFER_H
#define OD_DBIDBUFFER_H

#include "DD_PackPush.h"

#include "DbObject.h"

/** Description:
    Iterator class used to access the Object ID's in an OdDbIdBuffer object.

    {group:OdDb_Classes}
*/
class  OdDbIdBufferIterator : public OdRxObject
{
public:
  /** Description:
      Sets this iterator to point to the first element in the associated container. 
  */
  virtual void start() = 0;

  /** Description:
      Returns true if there are no more objects to iterate through, false otherwise.
  */
  virtual bool done() const = 0;

  /** Description:
      Increments the current object for this iterator to the next object in the associated container.
  */
  virtual void next() = 0;

  /** Description:
      Returns the Object ID of the current object referenced by this iterator.
  */
  virtual OdDbObjectId id() const = 0;

  /** Description:
      If the specified Object ID is present in the associated container, makes this object the 
      current object referenced by this iterator. If the specified Object ID is not found, 
      the current object referenced by this iterator will be invalid, and done() will return true.
  */
  virtual bool seek(OdDbObjectId id) = 0;

  /** Description:
      Removes the Object ID referenced by this iterator as the current element.  The element 
      immediately following the removed element becomes the current element for this iterator. 
  */
  virtual void removeCurrentId() = 0;
};

typedef OdSmartPtr<OdDbIdBufferIterator> OdDbIdBufferIteratorPtr;

/** Description:
    Represents an ID buffer object in an OdDbDatabase.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbIdBuffer : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbIdBuffer);

  /** Description:
      Constructor (no arguments).
  */
  OdDbIdBuffer();

  /** Description:
      Returns an iterator that can be used to traverse the Object ID's in the OdDbIdBuffer object.

      Return Value:
      Smart pointer to the new iterator.
  */
  OdDbIdBufferIteratorPtr newIterator();

  /** Description:
      Appends an Object ID onto this object's ID list.
  */
  void addId(const OdDbObjectId &id);

  /** Description:
      Returns the number of Object ID's referenced by this OdDbIdBuffer object.
  */
  int numIds() const;

  /** Description:
      Clears this object's Object ID list.
  */
  void removeAll();

  virtual OdResult dwgInFields(OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(OdDbDxfFiler* pFiler) const;
};
 
typedef OdSmartPtr<OdDbIdBuffer> OdDbIdBufferPtr;

#include "DD_PackPop.h"

#endif  // OD_DBIDBUFFER_H


