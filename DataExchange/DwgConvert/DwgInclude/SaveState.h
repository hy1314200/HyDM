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



#ifndef _ODSAVESTATE_INCLUDED_
#define _ODSAVESTATE_INCLUDED_

/** Description:
    Class to save variable state locally, and restore automaticaly on scope exit
  
    {group:Other_Classes}
*/
template <class T>
class OdSaveState
{
  T&  m_val;
  T   m_oldValue;
public:
  OdSaveState( T& val )
    : m_val( val )
  {
    m_oldValue = m_val;
  }
  OdSaveState( T& val, const T& newVal )
    : m_val( val )
  {
    m_oldValue = m_val;
    m_val = newVal;
  }
  ~OdSaveState()
  {
    m_val = m_oldValue;
  }
  operator const T&() const
  {
    return m_oldValue;
  }
};


#endif //#ifndef _ODSAVESTATE_INCLUDED_

