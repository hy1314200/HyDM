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



#ifndef ODDB_CASEDLPNTABLERECORD_H
#define ODDB_CASEDLPNTABLERECORD_H

#include "DD_PackPush.h"

#include "DbObject.h"

/** Description:
    Represents a CAseDLPNTableRecord in an OdDbDatabase.

    {group:Other_Classes}
*/
class TOOLKIT_EXPORT CAseDLPNTableRecord : public OdDbObject
{
public:
  ODRX_DECLARE_MEMBERS(CAseDLPNTableRecord);
  
  CAseDLPNTableRecord();
  CAseDLPNTableRecord(OdDbObjectImpl* pImpl);
};

typedef OdSmartPtr<CAseDLPNTableRecord> CAseDLPNTableRecordPtr;

#include "DD_PackPop.h"

#endif //ODDB_CASEDLPNTABLERECORD_H


