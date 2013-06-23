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



// (C) Copyright 2001 by Open Dwg Alliance. All rights reserved.
//

#ifndef _ODDBUNDOCONTROLLER_INCLUDED_
#define _ODDBUNDOCONTROLLER_INCLUDED_

#include "RxObject.h"

class OdDbDatabase;

/** Description:

    {group:OdDb_Classes}
*/
class OdDbUndoController : public OdRxObject
{
public:
  typedef void* PAGE;
  typedef unsigned long SIZE;
  typedef unsigned long POS;

  virtual SIZE pageSize() const = 0;

  virtual PAGE allocPage() = 0;
  virtual void freePage(PAGE page) = 0;
  virtual void read(PAGE page, void* pDest, POS nStart, SIZE nCount) const = 0;
  virtual void write(PAGE page, const void* pSorce, POS nStart, SIZE nCount) const = 0;

  virtual void setDatabase(OdDbDatabase* pDb) = 0;
  virtual int maxUndoSteps() const = 0;
};

typedef OdSmartPtr<OdDbUndoController> OdDbUndoControllerPtr;


#endif // _ODDBUNDOCONTROLLER_INCLUDED_




