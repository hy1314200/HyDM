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



#ifndef _ODDBDATABASEREACTOR_INCLUDED_
#define _ODDBDATABASEREACTOR_INCLUDED_

#include "DD_PackPush.h"

#include "RxObject.h"
#include "IdArrays.h"

/** Description:
    This class is the base class for custom classes that receive notification
    of OdDbDatabase events.
    
    Remarks:
    Events consist of the addition, modification, or deletion of objects 
    from an OdDbDatabase instance.
    
    Note:
    The default implementations of all methods in this class do nothing.

    Library:
    Db
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbDatabaseReactor : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdDbDatabaseReactor);
  
  OdDbDatabaseReactor() {}
	
	/** Description:
	  Notification function called whenever an OdDbObject has been appended to an OdDbDatabase.
	  
	  Arguments:
	  pDb (I) Pointer to the *database* being *modified*.
	  pObject (I) Pointer to the object being appended.
	  
	  Remarks:
	  This function is called after the operation.
	*/
  virtual void objectAppended(
    const OdDbDatabase* pDb, 
    const OdDbObject* pObject);

	
	/** Description:
	  Notification function called whenever an OdDbObject has been unappended from an OdDbDatabase by an Undo operation.
	  
	  Arguments:
	  pDb (I) Pointer to the *database* being *modified*.
	  pObject (I) Pointer to the object being unappended.

	  Remarks:
	  This function is called after the operation.
	*/
  virtual void objectUnAppended(
    const OdDbDatabase* pDb, 
    const OdDbObject* pObject);

	
	/** Description:
	  Notification function called whenever an OdDbObject has been reappended to an OdDbDatabase by a Redo operation.
	  
	  Arguments:
	  pDb (I) Pointer to the *database* being *modified*.
	  pObject (I) Pointer to the object being reappended.

	  Remarks:
	  This function is called after the operation.
	*/
  virtual void objectReAppended(
    const OdDbDatabase* pDb, 
    const OdDbObject* pObject);

	
	/** Description:
	  Notification function called whenever an OdDbObject is about to be *modified*.
	  
	  Arguments:
	  pDb (I) Pointer to the *database* being *modified*.
	  pObject (I) Pointer to the object being *modified*.
	  
	  Remarks:
	  This function is called before the operations.
	  
	  See Also:
	  objectModified
	*/
  virtual void objectOpenedForModify(
    const OdDbDatabase* pDb, 
    const OdDbObject* pObject);

	
	/** Description:
	  Notification function called whenever an OdDbObject has been *modified*.
	  
	  Arguments:
	  pDb (I) Pointer to the *database* being *modified*.
	  pObject (I) Pointer to the object being *modified*.
	  
	  Remarks:
	  This function is called after the operations.

	  See Also:
	  objectOpenedForModify
	*/
  virtual void objectModified(
    const OdDbDatabase* pDb, 
    const OdDbObject* pObject);
    
	
	/** Description:
	  Notification function called whenever an OdDbObject has been *erased* or unerased.
	  
	  Arguments:
	  pDb (I) Pointer to the *database* being *modified*.
	  pObject (I) Pointer to the object being *modified*.
	  erased (I) True if and only if the object is being *erased*.
	  
	  Remarks:
	  This function is called after the operation.
	*/
  virtual void objectErased(
    const OdDbDatabase* pDb, 
    const OdDbObject* pObject, 
    bool erased = true);

	/** Description:
	  Notification function called whenever a *database* -resident system variable is about to change.
	  
	  Arguments:
	  pDb (I) Pointer to the *database* being *modified*.
	  name (I) Name of the system varable being *modified*.
	  
	  Remarks:
	  This function is called before the operation.
	  
	  See Also:
	  headerSysVarChanged
	*/
  virtual void headerSysVarWillChange(
    const OdDbDatabase* pDb, 
    const char* name);

#define VAR_DEF(type, name, d1, d2, r1, r2)                                \
  virtual void headerSysVar_##name##_WillChange(const OdDbDatabase* pDb);

#include "SysVarDefs.h"
#undef VAR_DEF

	
	/** Description:
	  Notification function called whenever a *database* -resident system variable has changed.
	  
	  Arguments:
	  pDb (I) Pointer to the *database* being *modified*.
	  name (I) Name of the system varable being *modified*.
	  
	  Remarks:
	  This function is called after the operation.
	  
	  See Also:
	  headerSysVarWillChange
	*/
  virtual void headerSysVarChanged(
    const OdDbDatabase* pDb, 
    const char* name);

#define VAR_DEF(type, name, d1, d2, r1, r2)                                 \
  virtual void headerSysVar_##name##_Changed(const OdDbDatabase* pDb);

#include "SysVarDefs.h"
#undef VAR_DEF
  /** Description:
    Notification function called after the specified application is loaded and all its proxy
    objects are resurrected.
    
	  Arguments:
	  pDb (I) Pointer to the *database* being *modified*.
	  appname (I) Name of the resurrecting application.
    objectIds (I) Object IDs of the resurrected objects.
  */
  virtual void proxyResurrectionCompleted(
    const OdDbDatabase* pDb,
    const char* appname, 
    OdDbObjectIdArray& objectIds);

  /** Description:
    Notification function called whenever a *database* is about to be deleted from memory.
    Arguments:
    pdb (I) Pointer to the *database* being *deleted*.

	  Remarks:
	  This function is called before the operation.
  */
  virtual void goodbye(
    const OdDbDatabase* pDb);
};
/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbDatabaseReactor object pointers.
*/
typedef OdSmartPtr<OdDbDatabaseReactor> OdDbDatabaseReactorPtr;

#include "DD_PackPop.h"

#endif // _ODDBDATABASEREACTOR_INCLUDED_

