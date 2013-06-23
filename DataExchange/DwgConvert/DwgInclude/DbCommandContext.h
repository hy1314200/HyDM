#ifndef _ODDBCOMMANDCONTEXT_H_INCLUDED_
#define _ODDBCOMMANDCONTEXT_H_INCLUDED_

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



#include "DbExport.h"
#include "Ed/EdCommandContext.h"


class OdDbDatabase;
class OdDbSelectionSet;
class OdDbCommandContext;
typedef OdSmartPtr<OdDbCommandContext> OdDbCommandContextPtr;

#include "DD_PackPush.h"

/** Description:
    Provides I/O and database access to custom objects during their execution.

    Remarks:
    Client applications that invoke custom commands, should use an instance of this
    class (or a subclass) to pass to the OdEdCommandStack::executeCommand function.
    Using this class instead of a true instance of OdEdCommandContext provides
    custom commands with database access.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbCommandContext : public OdEdCommandContext
{ 
public:
  ODRX_DECLARE_MEMBERS(OdDbCommandContext);

  /** Description:
      Returns a pointer to a database, for use in a custom command.
  */
  virtual OdDbDatabase* database() = 0;

  /** Description:
      Returns a pointer to a selection set object, for use in a custom command.
  */
  virtual OdDbSelectionSet& selectionSet() = 0;
  
  /** Description:
      Launches interactive object selection to fill selection set.
  */
  virtual void select(const OdChar* prompt, bool clearSelection = false) = 0;
};

#include "DD_PackPop.h"

#endif //#ifndef _ODDBCOMMANDCONTEXT_H_INCLUDED_

