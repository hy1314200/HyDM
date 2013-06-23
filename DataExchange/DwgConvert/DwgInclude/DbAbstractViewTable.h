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



#ifndef _ODDBABSTRACTVIEWTABLE_INCLUDED
#define _ODDBABSTRACTVIEWTABLE_INCLUDED

#include "DbSymbolTable.h"

class OdDbAbstractViewTableRecord;
class OdDbAbstractViewTableIterator;

/** Description:
    This class is the base class for OdDbViewTable and OdDbViewportTable.

    Library:
    Db

    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDbAbstractViewTable : public OdDbSymbolTable
{
public:

  ODDB_DECLARE_MEMBERS(OdDbAbstractViewTable);

  /** Note:
    DWGdirect applications typically will not use this constructor, insofar as 
    this class is a base class.
  */
  OdDbAbstractViewTable();

//  void getAt(const char* entryName, OdDbAbstractViewTableRecord** pRec,
//    OdDb::OpenMode openMode, bool openErasedRec = false) const;

//  OdDbObjectId getAt(const char* entryName, bool getErasedRecord = false) const;

//  bool has(const char* name) const;
//  bool has(const OdDbObjectId& id) const;

//  void newIterator(OdDbAbstractViewTableIterator** pIterator,
//    bool atBeginning = true, bool skipDeleted = true) const;

//  virtual const OdDbObjectId& add(OdDbSymbolTableRecord* pRecord);

};

#endif // _ODDBABSTRACTVIEWTABLE_INCLUDED


