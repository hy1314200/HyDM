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



#ifndef _ODSTACK_H_
#define _ODSTACK_H_

template <class T> class OdStackItem;
template <class T> class OdStack;

/** Description:

    {group:OdGi_Classes} 
*/
template <class T>
class OdStackItem : public T
{
  friend class OdStack<T>;
protected:
  OdStackItem* m_pUnder;
  inline OdStackItem(OdStackItem* pUnder, const T& val)
    : T(val), m_pUnder(pUnder) { }
  inline OdStackItem(OdStackItem* pUnder) : m_pUnder(pUnder) { }
};

/** Description:

    {group:OdGi_Classes} 
*/
template <class T>
class OdStack
{
  typedef OdStackItem<T> TItem;
public:
  TItem* m_pTop;
  inline OdStack() : m_pTop(0) { }
  inline void push(const T& inVal)
  {
    m_pTop = new TItem(m_pTop, inVal);
  }
  inline T* push()
  {
    m_pTop = new TItem(m_pTop);
    return top();
  }
  inline void pop(T& outVal)
  {
    ODA_ASSERT(m_pTop); // pop from empty stack
    outVal = *m_pTop;
    pop();
  }

  inline const T* top() const { return m_pTop; }
  inline T* top() { return m_pTop; }

  inline void pop()
  {
    TItem* pTop = m_pTop;
    ODA_ASSERT(pTop); // pop from empty stack
    m_pTop = pTop->m_pUnder;
    delete pTop;
  }
  inline ~OdStack()
  {
    while(m_pTop)
    {
      pop();
    }
  }

  inline T* beforeTop() const 
  { 
    ODA_ASSERT(m_pTop);
    return m_pTop->m_pUnder;
  }
};

#endif //#ifndef _ODSTACK_H_
