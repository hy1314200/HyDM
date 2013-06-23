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



#ifndef   _ODRX_EVENT_H__
#define   _ODRX_EVENT_H__

#include "DD_PackPush.h"

#include "RxObject.h"

#define ODRX_EVENT_OBJ "OdRxEvent"

class OdDbDatabase;
class OdGeMatrix3d;
class OdDbIdMapping;
class OdGePoint3d;
class OdDbObjectId;

/** Description:
    This class is the base class for custom classes that receive notification
    of OdRxEvent (application level) events.
    
    Note:
    The default implementations of all methods in this class do nothing.

    Library:
    Db
  
    {group:OdRx_Classes} 
*/
class TOOLKIT_EXPORT OdRxEventReactor : public OdRxObject 
{ 
public:
  ODRX_DECLARE_MEMBERS(OdRxEventReactor);
  
  /** Description:
    Notification function called whenever a DWG file is being read.
    
    Arguments:
    pDb (I) Pointer to the *database* being created.
    fileName (I) Name of the DWG file.
    
    Remarks:
    This function is called during the operation.
    
    See Also:
    o  databaseConstructed
    o  initialDwgFileOpenComplete
  */
  virtual void dwgFileOpened(
    OdDbDatabase* pDb, 
    const OdChar* fileName);
    
  /** Description:
    Notification function called whenever a DWG file has been read.
    
    Arguments:
    pDb (I) Pointer to the *database* being created.
    
    Remarks:
    This function is called after the read operation, but before the database is constructed.
    
    See Also:
    o  databaseConstructed
    o  dwgFileOpened
  */
  virtual void initialDwgFileOpenComplete(
    OdDbDatabase* pDb);
    
  /** Description:
    Notification function called whenever a *database* has been constructed from a file.
    
    Arguments:
    pDb (I) Pointer to the *database* constructed.
    
    Remarks:
    This function is called after the operation.
    
    See Also:
    o  dwgFileOpened
    o  initialDwgFileOpenComplete
  */
  virtual void databaseConstructed(
    OdDbDatabase* pDb);

  /**  Description
    Notification function called whenever a *database* is about to be deleted from memory.  

    Arguments:
    pDb (I) Pointer to the *database* to be destroyed.
    
    Remarks:
    This function is called before the operation.
  */
  virtual void databaseToBeDestroyed(
    OdDbDatabase* pDb);
  
  /** Description:
    Notification function called whenever a *database* is about to be saved to a DWG file.
    
    Arguments:
    pDb (I) Pointer to the *database* to be saved.
    intendedName (I) Intended *name* of the DWG file.
    
    Remarks:
    This function is called before the operation, and before the user 
    has had a chance to modify the file name. The file may not have the intendedName.
    
    See Also:
    o  abortSave
    o  saveComplete
  */
  virtual void beginSave(
    OdDbDatabase* pDb, 
    const OdChar* intendedName);

  /** Description:
    Notification function called whenever a *database* has been save
    d to a DWG file.
    
    Arguments:
    pDb (I) Pointer to the *database* saved.
    actualName (I) Actual *name* of the DWG file.
    
    Remarks:
    This function is called after the operation, but before the database is constructed.
    
    See Also:
    o  abortSave
    o  beginSave
  */
  virtual void saveComplete(
    OdDbDatabase* pDb, 
    const OdChar* actualName);
    
  /** Description:
    Notification function called whenever the save of a *database* has failed.
    
    Arguments:
    pDb (I) Pointer to the *database* being saved.
    
    Remarks:
    This function is called after the operation.

    See Also:
    o  beginSave
    o  saveComplete
  */
  virtual void abortSave(
    OdDbDatabase* pDb);
  
  // DXF In/Out Events.

  /** Description:
    Notification function called whenever a *database* is about to be *modified* by a DXF input operation.
    
    Arguments:
    pDb (I) Pointer to the *database* to be *modified*.
    
    Remarks:
    This function is called before the operation.
    
    See Also:
    o  abortDxfIn
    o  dxfInComplete
  */
  virtual void beginDxfIn(
    OdDbDatabase* pDb);
    
  /** Description:
    Notification function called whenever the DXF input to a *database* has failed.
    
    Arguments:
    pDb (I) Pointer to the *database* being *modified*.
    
    Remarks:
    This function is called after the operation.
        
    See Also:
    o  beginDxfIn
    o  dxfInComplete
  */
  virtual void abortDxfIn(
    OdDbDatabase* pDb);
 
  /** Description:
    Notification function called whenever a *database* has been *modified* by a DXF input operation.
    
    Arguments:
    pDb (I) Pointer to the *database* *modified*.
    
    Remarks:
    This function is called after the operation.
    
    See Also:
    o  abortDxfIn
    o  beginDxfIn
  */
   virtual void dxfInComplete(
    OdDbDatabase* pDb);


  /** Description:
    Notification function called whenever a *database* is about to be saved to a DXF file.
    
    Arguments:
    pDb (I) Pointer to the *database* to be saved.
    
    Remarks:
    This function is called before the operation
    
    See Also:
    o  abortDxfOut
    o  dxfOutComplete
  */
  virtual void beginDxfOut(
    OdDbDatabase* pDb);
    
    
  /** Description:
    Notification function called whenever the DXF output from a *database* has failed.
    
    Arguments:
    pDb (I) Pointer to the *database* being saved.
    
    Remarks:
    This function is called after the operation.
        
    See Also:
    beginDxfOut
    dxfOutComplete
  */
  virtual void abortDxfOut(
    OdDbDatabase* pDb);
    
    
  /** Description:
    Notification function called whenever a *database* has be saved to a DXF file.
       
    Arguments:
    pDb (I) Pointer to the *database* saved.
    
    Remarks:
    This function is called after the operation.
    
    See Also:
    o  abortDxfOut
    o  beginDxfOut
  */
  virtual void dxfOutComplete(
    OdDbDatabase* pDb);
  
  // Insert Events.
  
  /** Description:
    Notification function called whenever one database is about to be inserted into another.
    
    Arguments:
    pToDb (I) Destination *database*.
    pFromDb (I) Source *database*.
    blockName (I) Name of the block.
    xfm (I) Transformation matrix.
    
    Remarks:
    This function is called before the operation.
    
    Remarks:
    If blockName is specified, pFromDb was inserted into pToDb as a OdDbBlockTableRecord.
    
    If xfm is specified, pFromDb was inserted into pToDb as entities.
    
    See Also:
    o  abortInsert
    o  endInsert
    o  otherInsert
  */
  virtual void beginInsert(
    OdDbDatabase* pToDb, 
    const OdChar* blockName, 
    OdDbDatabase* pFromDb);
  virtual void beginInsert(
    OdDbDatabase* pToDb, 
    const OdGeMatrix3d& xfm, 
    OdDbDatabase* pFromDb);
    
  /** Description:
    Notification function called whenever one database has been inserted into another.
    
    Arguments:
    pToDb (I) Destination *database*.
    pFromDb (I) Source *database*.
    idMap (I) ID Map source -> destination.
    
    Remarks:
    This function is called after the operation, and is sent just before 
    beginDeepCloneXlation. It is called before any Object IDs are translated.
    
    Note:
    Object IDs of cloned objects do not point at the cloned objects, and
    must therefore not not be used at this point for any operations on
    those objects.
    
    See Also:
    o  abortInsert
    o  beginInsert
    o  endInsert
  */
  virtual void otherInsert(
    OdDbDatabase* pToDb, 
    OdDbIdMapping& idMap, 
    OdDbDatabase* pFromDb);
    
  /** Description:
    Notification function called whenever the insertion of a *database* has failed.
    
    Arguments:
    pToDb (I) Destination *database*.
    
    Remarks:
    This function is called after the operation.
        
    See Also:
    o  beginInsert
    o  endInsert
    o  otherInsert
  */
  virtual void abortInsert(
    OdDbDatabase* pToDb);
    
  /** Description:
    Notification function called whenever the insertion of a *database* has succeeded.
    
    Arguments:
    pToDb (I) Destination *database*.
    
    Remarks:
    This function is called after the operation.
        
    See Also:
    o  abortInsert
    o  beginInsert
    o  otherInsert
  */
  virtual void endInsert(
    OdDbDatabase* pToDb);
  
  // Wblock Events.

  /** Description:
    Notification function called while one database is about to be wblocked to another.
    
    Arguments:
    pFromDb (I) Source *database*.
  */
  virtual void wblockNotice(
    OdDbDatabase* pFromDb);
  
  /** Description:
    Notification function called while one database is being wblocked to another.
    
    Arguments:
    pToDb (I) Destination *database*.
    pFromDb (I) Source *database*.
    insertionPoint (I) INSBASE of pToDb.
    blockId (I) Object ID of OdDbBlockTableRecord being wblocked.
    
    Remarks:
    This function is called during the operation.
    
    If insertionPoint is specified, the wblock operation is being performed on a set
    of entities in pFromDb.
    
    If blockId is specified, the wblock operation is being performed on a 
    BlockTableRecord in pFromDb.
    
    If neither insertionPoint nor blockId is specified, the entire pFromDb database is
    wblocked to pToDb.  
    
    See Also:
    o  abortWblock
    o  beginWblockObjects
    o  endWblock
    o  otherWblock
    o  wblockNotice
  */
  virtual void beginWblock(
    OdDbDatabase* pToDb, 
    OdDbDatabase* pFromDb, 
    const OdGePoint3d& insertionPoint);
  virtual void beginWblock(
    OdDbDatabase* pToDb, 
    OdDbDatabase* pFromDb, 
    OdDbObjectId blockId);
  virtual void beginWblock(
    OdDbDatabase* pToDb, 
    OdDbDatabase* pFromDb);

     
  /** Description:
    Notification function called whenever one database is being wblocked to another.
    
    Arguments:
    pToDb (I) Destination *database*.
    pFromDb (I) Source *database*.
    idMap (I) ID Map source -> destination.
    
    Remarks:
    idMap contains mapping of the original objects in pFromDb to the objects created in pToDb.

    This function is called after the operation, and is sent just before 
    beginDeepCloneXlation.
    
    See Also:
    o  abortWblock
    o  beginWblock
    o  beginWblockObjects
    o  endWblock
    o  wblockNotice
  */
  virtual void otherWblock(
    OdDbDatabase* pToDb, 
    OdDbIdMapping& idMap, 
    OdDbDatabase* pFromDb);
    
  /** Description:
    Notification function called whenever the wblock of a *database* has failed.
    
    Arguments:
    pToDb (I) Destination *database*.
    
    Remarks:
    This function is called after the operation.
        
    See Also:
    beginWblock
    beginWblockObjects
    endWblock
    otherWblock
    wblockNotice
  */
  virtual void abortWblock(
    OdDbDatabase* pToDb);
    
  /** Description:
    Notification function called whenever the wblock of a *database* has succeded.
    
    Arguments:
    pToDb (I) Destination *database*.
    
    Remarks:
    This function is called after the operation.
        
    See Also:
    o  abortWblock
    o  beginWblock
    o  beginWblockObjects
    o  otherWblock
    o  wblockNotice
  */
  virtual void endWblock(
    OdDbDatabase* pToDb);
    
  /** Description:
    Notification function called whenever one database is being wblocked to another.
    
    Arguments:
    pFromDb (I) Source *database*.
    idMap (I) ID Map source -> destination.
    
    Remarks:
    idMap contains mapping of the original objects in pFromDb to the objects created in pToDb.

    This notification function gives wblockCloneObjects a method
    of being notified before each pFrom database before the actual cloning begins.
    
    See Also:
    o  abortWblock
    o  beginWblock
    o  endWblock
    o  otherWblock
    o  wblockNotice
  */
  virtual void beginWblockObjects(
    OdDbDatabase* pFromDb, 
    OdDbIdMapping& idMap);
  
  // Deep Clone Events.
  
  /** Description:
    Notification function called whenever a deepClone operation is about to be started on a *database*.
    
    Arguments:
    pToDb (I) Destination *database*.
    idMap (I) ID Map source -> destination.
    
    Remarks:
    idMap will always be empty.

    See Also:
    o  abortDeepClone
    o  beginDeepCloneXlation
    o  endDeepClone
  */
  virtual void beginDeepClone(
    OdDbDatabase* pToDb, 
    OdDbIdMapping& idMap);

  /** Description:
    Notification function called whenever a the translation stage of a deepClone 
    operation is about to be started on a *database*.
    
    Arguments:
    idMap (I) ID Map source -> destination.
    
    Remarks:
    This function is called after all objects, and their owned objects, have been cloned.
    It is called before any Object IDs are translated 
    
    Note:
    Object IDs of cloned objects do not point at the cloned objects, and
    must therefore not not be used at this point for any operations on
    those objects.
    
    See Also:
    o  abortDeepClone
    o  beginDeepClone
    o  endDeepClone
  */
  virtual void beginDeepCloneXlation(
    OdDbIdMapping& idMap);

  /** Description:
    Notification function called whenever a deepClone operation has failed.
    
    Arguments:
    idMap (I) ID Map source -> destination.
    
    Remarks:
    This function is called after the operation.
   
    Note:
    The objects involved in the deepClone operation are in an indeterminate state 
    and must be cleaned up.
   
    See Also:
    o  beginDeepClone
    o  beginDeepCloneXlation
    o  endDeepClone
  */
  virtual void abortDeepClone(
    OdDbIdMapping& idMap);

  /** Description:
    Notification function called whenever a deepClone operation has succeeded.
    
    Arguments:
    idMap (I) ID Map source -> destination.
    
    Remarks:
    This function is called after the operation.
   
    See Also:
    o  abortDeepClone
    o  beginDeepClone
    o  beginDeepCloneXlation
  */
  virtual void endDeepClone(
    OdDbIdMapping& idMap);
  
  // Partial Open Events.
  
  /** Description:
    Notification function called before an attempt is made to partially open an OdDbDatabase.

    Arguments:
    pDb (I) Pointer to the *database*.
    
    Remarks:
    To 
    veto the partial open, an application should override partialOpenNotice, and from it call
    
                pDb->disablePartialOpen()
    
  */
  virtual void partialOpenNotice(
    OdDbDatabase* pDb);
  
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdRxEventReactor object pointers.
*/
typedef OdSmartPtr<OdRxEventReactor> OdRxEventReactorPtr;

/** Description:
    This class manages application level OdRxEventReactor instances.
    
    Library:
    Db
    {group:OdRx_Classes} 
*/
class TOOLKIT_EXPORT OdRxEvent : public OdRxObject 
{ 
public:
  ODRX_DECLARE_MEMBERS(OdRxEvent);
  
  /** Description:
    Adds the specified event reactor to the host application's event reactor list.
    Arguments:
    pReactor (I) Pointer to the event reactor.
  */
  virtual void addReactor(
    OdRxEventReactor* pReactor) = 0;

  /** Description:
    Removes the specified event reactor from the host application's event reactor list.
    Arguments:
    pReactor (I) Pointer to the event reactor.
  */
  virtual void removeReactor(
    OdRxEventReactor* pReactor) = 0;
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdRxEvent object pointers.
*/
typedef OdSmartPtr<OdRxEvent> OdRxEventPtr;


TOOLKIT_EXPORT OdRxEventPtr odrxEvent();

#include "DD_PackPop.h"

#endif //_ODRX_EVENT_H__


