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



#ifndef _ODDBVIEWTABLE_INCLUDED
#define _ODDBVIEWTABLE_INCLUDED

#include "DD_PackPush.h"

#include "DbAbstractViewTable.h"

class OdDbViewTableRecord;
class OdDbViewTableIterator;

/** Description:
    This class implements the ViewTable, which represents stored views in an OdDbDatabase instance.

    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbViewTable: public OdDbAbstractViewTable
{
public:
  ODDB_DECLARE_MEMBERS(OdDbViewTable);

  /** Note:
    DWGdirect applications typically will not use this constructor, insofar as 
    this class is a base class.
  */
  OdDbViewTable();

  virtual void getClassID(
    void** pClsid) const;
};
/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbViewTable object pointers.
*/
typedef OdSmartPtr<OdDbViewTable> OdDbViewTablePtr;

#include "DD_PackPop.h"

#endif // _ODDBVIEWTABLE_INCLUDED


