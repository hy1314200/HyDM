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



#ifndef _ODDBUCSTABLE_INCLUDED
#define _ODDBUCSTABLE_INCLUDED

#include "DD_PackPush.h"

#include "DbSymbolTable.h"

class OdDbUCSTableRecord;
class OdDbUCSTableIterator;

/** Description:
    This class implements the UCSTable, which represents User Coordinate Systems in an OdDbDatabase instance.

    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbUCSTable: public OdDbSymbolTable
{
public:
  ODDB_DECLARE_MEMBERS(OdDbUCSTable);

  /** Note:
    DWGdirect applications typically will not use this constructor, insofar as 
    the OdDbDatabase class creates its own instance.
  */
  OdDbUCSTable();

  virtual void getClassID(
    void** pClsid) const;
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbUCSTable object pointers.
*/
typedef OdSmartPtr<OdDbUCSTable>  OdDbUCSTablePtr;

#include "DD_PackPop.h"

#endif // _ODDBUCSTABLE_INCLUDED


