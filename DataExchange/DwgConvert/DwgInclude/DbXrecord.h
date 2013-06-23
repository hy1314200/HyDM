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



#ifndef ODDB_XRECORD_H
#define ODDB_XRECORD_H /* {Secret} */

#include "DD_PackPush.h"

#define ODDB_XRECORD_CLASS          "OdDbXrecord"

#include "DbObject.h"
#include "DbFiler.h"

class OdDbXrecordIteratorImpl;

/** Description:
    This class implements Iterators for the data lists in OdDbXrecord instances.  

    Library:
    Db:
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbXrecordIterator : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdDbXrecordIterator);

  /** Description:
    Sets this Iterator object to reference the first resbuf structure in the Xrecord data list.
     
    Remarks:
    Allows multiple traversals of the data list.
  */  
  void start();

  /** Description:
    Returns true if and only if the traversal is complete.
  */  
  bool done() const;
 
  /** Description:
    Sets this Iterator object to reference the next resbuf structure in the Xrecord data list.
  */ 
  bool next();
  
  /** Description:
    Returns the restype field of the current resbuf structure in the Xrecord data list.
  */
  int curRestype() const;
  
  /** Description:
    Returns a SmartPointer to a copy of the current resbuf structure in the Xrecord data list.
    Arguments:
    pDb (I) Pointer to the OdDbDatabase used for object ID resolution when this XRecord object is not *database* resident.
  */
  OdResBufPtr getCurResbuf(OdDbDatabase* pDb = NULL) const;
protected:
  OdDbXrecordIterator();
  OdDbXrecordIterator(
    OdDbXrecordIteratorImpl* pIterImpl);
  OdDbXrecordIteratorImpl* m_pImpl;
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbXrecordIterator object pointers.
*/
typedef OdSmartPtr<OdDbXrecordIterator> OdDbXrecordIteratorPtr;


/** Description:
    This class implements XRecord objects in an OdDbDatabase, container objects
    used attach arbitrary data to other OdDb objects.

    Library:
    Db
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbXrecord : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbXrecord);

  OdDbXrecord();

  /** Description:
    Returns a SmartPointer to a copy of the resbuf list owned by the XRecord object.

    Arguments:
    pDb (I) Pointer to the OdDbDatabase used for object ID resolution when this XRecord object is not *database* resident.
    pStatus (I) Pointer to receive the status. 
    
    Remarks:
    If pStatus != NULL, returns eOk if successful, or an appropriate error code if not.
*/
  OdResBufPtr rbChain(
    OdDbDatabase* pDb = NULL, 
    OdResult* pStatus = NULL) const;

  /** Description:
    Returns an iterator that can be used to traverse this Xrecord object.


    Remarks:
    This method can be faster than using rbChain()

    Arguments:
    pDb (I) Pointer to the OdDbDatabase used for object ID resolution when this XRecord object is not *database* resident.
  */
  OdDbXrecordIteratorPtr newIterator(
    OdDbDatabase* pDb = NULL) const;

  /** Description:
    Sets the data in this Xrecord object to the data in the specified resbuf chain. 
    Arguments:
    pDb (I) Pointer to the OdDbDatabase used for object ID resolution when this XRecord object is not *database* resident.
    pRb (I) Pointer to the first resbuf in the resbuf chain.
  */
  void setFromRbChain(const OdResBuf* pRb, OdDbDatabase* pDb = NULL);
  
  /** Description:
    Appends the data in the specified resbuf chain to the data in this Xrecord.

    Arguments:
    pRb (I) Pointer to the first resbuf in the resbuf chain.
    pDb (I) Pointer to the OdDbDatabase used for object ID resolution when this XRecord object is not *database* resident.
  */
  void appendRbChain(
    const OdResBuf* pRb, 
    OdDbDatabase* pDb = NULL);

  /** Description:
    Returns true if and only if this Xrecord object is set to translate 
    data chain object IDs during deepClone() and wblockClone() operations.
  */
  bool isXlateReferences() const;

  /** Description:
    Controls if this Xrecord object is to translate 
    data chain object IDs during deepClone() and wblockClone() operations.
    
    xlateReferences (I) Translate references, if and only if true. 
  */
  void setXlateReferences(
    bool isXlateReferences);
  
  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;
  
  /** Description:
      Returns the merge *style* for this Xrecord object (DXF 280).
  */
  virtual OdDb::DuplicateRecordCloning mergeStyle() const;

   /** Description:
      Sets the merge *style* for this Xrecord object (DXF 280).
      
      Arguments:
      style (I) Merge *style*.
  */
  virtual void setMergeStyle(
    OdDb::DuplicateRecordCloning style);
  
  virtual void getClassID(
    void** pClsid) const;
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbXrecord object pointers.
*/
typedef OdSmartPtr<OdDbXrecord> OdDbXrecordPtr;

#include "DD_PackPop.h"

#endif //ODDB_XRECORD_H


