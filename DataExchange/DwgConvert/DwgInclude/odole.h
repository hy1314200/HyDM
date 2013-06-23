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



//   OdOle.h
//

#ifndef _ODOLEAUTO_H_
#define _ODOLEAUTO_H_

#include <wtypes.h>
#include "DbDatabase.h"

/** Description:
    OdOxOleLinkManager is used to maintain the link between an
    object and it's respective COM wrapper.

    {group:Other_Classes}
*/
class OdOxOleLinkManager
{
public:
    
    // Given a pointer to a database resident object, return
    // the IUnknown of the COM wrapper. NULL is returned if
    // no wrapper is found.
    virtual IUnknown* GetIUnknown(OdDbObject* pObject) = 0;

    // Set the link between a database resident object and a 
    // COM wrapper. If the IUnknown is NULL, then the link is removed.
    virtual bool SetIUnknown(OdDbObject* pObject, IUnknown* pUnknown) = 0;

    // Given a pointer to a database object, return
    // the IUnknown of the COM wrapper. NULL is returned if
    // no wrapper is found.
    virtual IUnknown* GetIUnknown(OdDbDatabase * pDatabase) = 0;

    // Set the link between a database object and a COM wrapper. 
    // If the IUnknown is NULL, then the link is removed.
    virtual bool SetIUnknown(OdDbDatabase * pDatabase, IUnknown* pUnknown) = 0;

    // Given a pointer to a database object, return the
    // IDispatch of then document object. NULL is returned if
    // the database does not belong to a particular document.
    virtual IDispatch* GetDocIDispatch(OdDbDatabase * pDatabase) = 0;

    // Set the link between a database object and the IDispatch
    // of the document it belongs to. If the IDispatch is NULL, then 
    // the link is removed.
    virtual bool SetDocIDispatch(OdDbDatabase * pDatabase, IDispatch* pDispatch) = 0;
};

TOOLKIT_EXPORT OdOxOleLinkManager* OdOxGetOleLinkManager(void);

#endif // _ODOLEAUTO_H_



