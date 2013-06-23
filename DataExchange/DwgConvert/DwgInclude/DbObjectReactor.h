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



#ifndef _DB_OBJECT_REACTOR_INCLUDED_
#define _DB_OBJECT_REACTOR_INCLUDED_

#include "DD_PackPush.h"

#include "RxObject.h"
#include "OdArrayPreDef.h"

/** Description:
    This class is the base class for custom classes that receive notification
    when OdDbObject objects in an OdDbDatabase instance are accessed.
  
    Note:
    The default implementations of all methods in this class do nothing.

    Library:
    Db
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbObjectReactor : public OdRxObject
{
protected:
  OdDbObjectReactor() {}
public:
  ODRX_DECLARE_MEMBERS(OdDbObjectReactor);
  
  /**
    Description:
    Notification function called whenever the notifying object has had its cancel() member function called. 
    
    Arguments:
    pObject (I) Pointer to the object sending the notification.

    Remarks:
    The default implementation of this function does nothing.  This function can be
    overridden in custom classes.

    When this function is called, pObject points to the object sending the notification.
     
    The sending object is open kOpenForRead.
  */  
  virtual void cancelled(
    const OdDbObject* pObject);

  /**
    Description:
    Notification function called whenever the notifying object *has* had its clone() member function called. 
    
    Arguments:
    pObject (I) Pointer to the notifying object.
    pNewObject (I) Pointer to the object resulting from the copy.

    Remarks:
    The default implementation of this function does nothing.  This function can be
    overridden in custom classes.
  */
  virtual void copied(
    const OdDbObject* pObject, 
    const OdDbObject* pNewObject);

  /** 
    Description:
    Notification function called whenever an object *has* been *erased* or unerased.

    Arguments:
    pObject (I) Pointer to the object that was erased/unerased.
    erasing (I) True if and only if this object is being *erased*.
    
    Remarks:
    The default implementation of this function does nothing.  This function can be
    overridden in custom classes.
  */
  virtual void erased(
    const OdDbObject* pObject, 
    bool erasing = true);

  /** 
    Description:
    Notification function called just before an object is deleted from memory. 
    
    Arguments:
    pObject (I) Pointer to the object that is being deleted.

    Remarks:
    The default implementation of this function does nothing.  This function can be
    overridden in custom classes.
  */
  virtual void goodbye(
    const OdDbObject* pObject);

  /** 
    Description:
    Notification function called whenever an object is opened for modify OdDb::kForWrite.

    Arguments:
    pObject (I) Pointer to the object that is being opened.

    Remarks:
    The default implementation of this function does nothing.  This function can be
    overridden in custom classes.
  */
  virtual void openedForModify(
    const OdDbObject* pObject);

  /** Description:
      Notification function called whenever an object is opened OdDb::kForWrite, a function
      *has* been called that could modify the contents of this object, and this object is now being
      closed.

      Arguments:
      pObject (I) Pointer to the object that is being closed after being *modified*.

      Remarks:
      The default implementation of this function does nothing.  This function can be
      overridden in custom classes.
  */
  virtual void modified(
    const OdDbObject* pObject);

  /**
    Description:
    Notification function called whenever an object derived from OdDbEntity is *modified*.
    
    Arguments:
    pObject (I) Pointer to the *modified* object.
    
    Remarks:
    The default implementation of this function does nothing.  This function can be
    overridden in custom classes.
    
    This function is called when the following has occurred.
    
    1)  The calling object is opened OdDb::kForWrite.
    
    2)  One of its member functions either
    
        a Calls its assertWriteEnabled with recordModified == true.
        
        b Calls its recordGraphicsModified(true).
    
    3) The calling object is being closed.
    
    This function is called whenever the object as been *modified*. It therefore
    indicates only that the graphics for it may have changed.
            
  */
  virtual void modifiedGraphics(
    const OdDbObject* pObject);

  /**
    Description:
    Notification function called whenever a method of the
    specified subobject has called assertWriteEnabled()
    
    Arguments:
    pObject (I) Pointer to the notifying object.
    pSubObj (I) A pointer to the subobject owned by pObject.
    
    Remarks:
    Notification is made upon pObject->close() or pObject->cancel(). 
 
    Note:
    This function is called only for the modification of the following:
       
    Vertices of OdDb2dPolylines, OdDb3dPolylines, OdDbPolygonMeshs, and OdDbPolyFaceMeshes
    OdDbFaceRecords of OdDbPolyFaceMeshs 
    OdDbAttributes owned by OdDbBlockReferences, classes derived from OdDbBlockReference, and OdDbMInsertBlocks
  */
  virtual void subObjModified(
    const OdDbObject* pObject, 
    const OdDbObject* pSubObj);
  /**
    Description:
    Notification function called whenever the notifying
    object is in the midst an Undo operation that
    is undoing modifications.
    
    Arguments:
    pObject (I) Pointer to the notifying object.
    
    Remarks:
    The notifying object is open OdDb::kForRead.
    
    The default implementation of this function does nothing.  This function can be
    overridden in custom classes.
  */
  virtual void modifyUndone(
    const OdDbObject* pObject);

  /**
    Description:
    Notification function called whenever the XData *has* been written
    to the notifying object.
    
    Arguments:
    pObject (I) Pointer to the notifying object.
    
    Remarks:
    Notification is made upon pObject->close() or pObject->cancel(). 

    The notifying object is open OdDb::kForRead.
    
    The default implementation of this function does nothing.  This function can be
    overridden in custom classes.
  */
  virtual void modifiedXData(
    const OdDbObject* pObject);

  /**
    Description:
    Notification function called whenever the Undo process
    processes the appending of the notifying object to the *database*.
    
    Arguments:
    pObject (I) Pointer to the notifying object.
    
    Remarks:
    The notifying object is marked as *erased*. It is not removed the *database*, and can be unerased,
    even 'before' it was created. 
    
    The default implementation of this function does nothing.  This function can be
    overridden in custom classes.
  */
  virtual void unappended(
    const OdDbObject* pObject);

  /**
    Description:
    Notification function called whenever a Redo process
    processes the reappending of the notifying object to the *database*.
    
    Arguments:
    pObject (I) Pointer to the notifying object.
    
    Remarks:
    
    The default implementation of this function does nothing.  This function can be
    overridden in custom classes.
  */
  virtual void reappended(
    const OdDbObject* pObject);

  /** Description:
    Notification function called immediately before an object is closed.

    Arguments:
    objectId (I) Object ID of the object that is being closed.

    Remarks:
    The default implementation of this function does nothing.  This function can be
    overridden in custom classes.
  */
  virtual void objectClosed(
    const OdDbObjectId& objectId);

  /** Description:
    Returns a pointer to the first transient reactor of the specified class
    that is attached to the specified object.

    Arguments:
    pObject (I) Pointer to the notifying object.
    pKeyClass (I) Pointer to the class desciption.
  */
  static OdDbObjectReactor* findReactor(
    const OdDbObject* pObject, 
    const OdRxClass* pKeyClass);
};
/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbObjectReactor object pointers.
*/
typedef OdSmartPtr<OdDbObjectReactor> OdDbObjectReactorPtr;
/** Description:
    This template class is a specialization of the OdArray class for OdDbObjectReactor object SmartPointers.
*/
typedef OdArray<OdDbObjectReactorPtr> OdDbObjectReactorArray;

#include "DD_PackPop.h"

#endif // _DB_OBJECT_REACTOR_INCLUDED_

