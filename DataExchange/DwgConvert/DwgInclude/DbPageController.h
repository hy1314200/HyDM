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


#ifndef _ODDBPAGECONTROLLER_INCLUDED_
#define _ODDBPAGECONTROLLER_INCLUDED_

#include "RxObject.h"

class OdDbDatabase;
class OdDbObjectId;
class OdStreamBuf;
typedef OdSmartPtr<OdStreamBuf> OdStreamBufPtr;

/** Description:

    {group:DD_Namespaces}
*/
namespace OdDb
{
  /** Description:
      Flags that can be combined together (bit-wise) to describe a desired paging
      behavior.

      See Also:

      o OdDbPageController
      o odDbPageObjects
      o Paging Support
  */
  enum PagingType
  {
    /** Description:
        Apply unloading for objects in a partially opened database.  
        
        Remarks:
        If this flag is set, objects will be unloaded from memory after they are closed, 
        during the next call to odDbPageObjects.  This behavior can be useful in environments
        with limited heap space (such as Windows CE).
    */
    kUnload           = 0x00000001,  

    /** Description:
        Apply paging for database objects.

        Remarks:
        If this flag is set, objects will be paged out after they are closed, during the 
        next call to OdDbPageObjects.  Paging will be done through a client-supplied
        OdDbPageController instance.
    */
    kPage             = 0x00000002   
  };
}

/** Description:
    Controls the paging support applied to a database.

    See Also:

    o Paging Support
    o odDbPageObjects

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbPageController : public OdRxObject
{
public:
  typedef unsigned long Key;

  /** Description:
      Returns the type of paging to be performed.
      
      Remarks:
      The values of PagingType enum can be combined together to get a necessary paging type.

      See Also:

      o Paging Support
      o PagingType
  */
  virtual int pagingType() const = 0;

  /** Description:
      Retrieves paged information from a client-supplied data source, and returns
      this paged data to the caller.
      
      Arguments:
        key (I) The key of the paged data, which will be a value previously returned by OdDbPageController::write.
        
      Return Value:
      A stream containing the paged data read from a client-supplied data source.
  */
  virtual OdStreamBufPtr read(Key key) = 0;

  /** Description:
      Called to page out data as necessary, when paging support has been enabled.
      
      Arguments:
        key (O) The key of paged data (this will be passed to subsequent calls to OdDbPageController::read).
        pStreamSrc (I) The stream containing data to be paged out by the client application.

      Remarks:
      Implementations of this function are expected to write the entire contents of pStreamSrc
      to an external data source, and to set assign a value to the "key" argument that uniquely 
      identifies this chunk of paged data.  This key value will be used in subsequent calls
      to OdDbPageController::read, to retrieve this specific paged data.
     
      Return Value:
      true if the PageController implementation has stored the data successfully, false otherwise.
  */
  virtual bool write(Key& key, OdStreamBuf* pStreamSrc) = 0;

  /** Description:
      Sets a pointer to the database served by this controller.

      Remarks:
      The method is called by DWGdirect during initialization of database paging.
  */
  virtual void setDatabase(OdDbDatabase* pDb) = 0;

  /** Description:
      Returns a pointer to the database served by this controller.
  */
  virtual OdDbDatabase* database() = 0;

  /** Description:
      The method is called during unloading/paging of an object,
      before anything else is done. The default implementation returns eOk.

      Return Value:
      
      o eOk - to do nothing.
      o eSkipObjPaging - to skip the object during paging.
      o eStopPaging - to stop paging.
  */
  virtual OdResult subPage(const OdDbObjectId& id);
};

typedef OdSmartPtr<OdDbPageController> OdDbPageControllerPtr;

/** Description:
    Forces the paging of the database objects that have been marked for 
    paging since the last call to odDbPageObjects.  See Paging Support for details.
*/
TOOLKIT_EXPORT void odDbPageObjects(OdDbDatabase*);

#endif // _ODDBPAGECONTROLLER_INCLUDED_




