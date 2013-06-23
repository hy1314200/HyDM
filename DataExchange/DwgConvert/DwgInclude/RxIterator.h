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



#ifndef _ODRXITERATOR_H_
#define _ODRXITERATOR_H_

#include "RxObject.h"

/** Description
  This class is the abstract base class for OdDbDictionaryIterator and OdRxDictionaryIterator objects.

  Library:
  Db
  
  {group:OdRx_Classes} 
*/
class FIRSTDLL_EXPORT OdRxIterator : public OdRxObject
{
public:

  OdRxIterator() {}

  ODRX_DECLARE_MEMBERS(OdRxIterator);
  
  /** Description:
    Returns true if and only if the traversal by this Iterator *object* is complete.
  */  
  virtual bool done() const = 0;

  /** Description:
    Sets this Iterator *object* to reference the entry following the current object.

    Remarks:
    Returns !done().
  */
  virtual bool next() = 0;

  /** Description:
    Returns a SmartPointer to the entry pointed to by this Iterator *object*. 
  */
  virtual OdRxObjectPtr object() const = 0;
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdRxIterator pointer objects.
*/
typedef OdSmartPtr<OdRxIterator> OdRxIteratorPtr;

#endif // _ODRXITERATOR_H_


