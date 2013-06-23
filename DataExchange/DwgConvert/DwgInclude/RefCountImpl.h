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



#if !defined(_ODREFCOUNTIMPL_INCLUDED_)
#define _ODREFCOUNTIMPL_INCLUDED_

#include "DD_PackPush.h"

#include "RxObject.h"

/** Description:
    Template class that provides the reference counting functionality needed
    to instantiate objects that are descended from OdRxObject.

    {group:OdRx_Classes} 
*/
template<class T, class TInterface = T>
class OdRefCountImpl : public T
{
  /** Description:
      Allows to create OdRefCountImpl<T> instances inside this class only.
  */
  OdUInt32 m_nRefCounter;

  /** Description:
      Assignment operator prohibited.
  */
  OdRefCountImpl& operator = (const OdRefCountImpl&) { throw OdError(eNotApplicable); return *this; }

  /** Description:
      Allows to create OdRefCountImpl<T> instances inside this class only.
  */
  ODRX_HEAP_OPERATORS();

  /** Description:
      Constructor.  Sets the reference count to one.
  */
  OdRefCountImpl() : m_nRefCounter(1) {}
public:

  /** Description:
      Increments the reference count.
  */
  void addRef()
  {
    ++m_nRefCounter;
  }

  /** Description:
      Decrements the reference count, and deletes this object if the reference count
      becomes zero.
  */
  void release()
  {
		ODA_ASSERT((m_nRefCounter > 0));
		if (!(--m_nRefCounter))
		{
      T::onFinalRelease();
      delete this;
		}
  }

  long numRefs() const { return m_nRefCounter; }

  /** Description:
      Creates an instance of OdRefCountImpl<T, TInterface>
      and returns smart pointer to it.
  */
  static OdSmartPtr<TInterface> createObject()
  {
    return OdSmartPtr<TInterface>(static_cast<TInterface*>(new OdRefCountImpl<T, TInterface>), kOdRxObjAttach);
  }
};

#define ODREFCOUNTIMPL_CONSTR(CLASS, pRes) pRes = OdRefCountImpl<CLASS>::createObject()

#include "DD_PackPop.h"

#endif // !defined(_ODREFCOUNTIMPL_INCLUDED_)


