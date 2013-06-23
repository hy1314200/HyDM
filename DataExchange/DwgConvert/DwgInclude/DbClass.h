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


#ifndef _ODDBCLASS_INCLUDED_
#define _ODDBCLASS_INCLUDED_

#include "DD_PackPush.h"

#include "RxObject.h"

////1) Override
//OdRxClass* ClassName::saveAsClass(OdRxClass*) const
//{
//  return PseudoBaseClass::saveAsClass(PseudoBaseClass::desc());
//}
//
////2) rxInit()
//// - verify pseudo base class (Save_As_Class)
//// - remember old constructor 
//// - set new constructor
//// 


/** Description:
    Declares the functions required by pseudo database objects.
*/
#define ODDB_PSEUDO_DECLARE_MEMBERS(ClassName)    \
public:                                           \
  ODRX_DECLARE_MEMBERS(ClassName);                \
  static OdPseudoConstructorType g_pMainConstr;   \
  OdRxClass* saveAsClass(OdRxClass* pClass) const

/*
  Description:
  Defines initialization functions for pseudo database objects.

*/
#define ODDB_PSEUDO_DEFINE_INIT_MEMBERS(ClassName,ParentClass,pseudoConsFn,PseudoBaseClass) \
                                                                            \
/* Registers this class with DWGdirect. */                                  \
void ClassName::rxInit()                                                    \
{                                                                           \
  if (!ClassName::g_pDesc)                                                  \
  {                                                                         \
    OdRxClass* pParent = ParentClass::desc();                               \
    OdRxClass* pPseudoBase = PseudoBaseClass::desc();                       \
    if (!pParent->isDerivedFrom(pPseudoBase))                               \
    {                                                                       \
      throw OdError(eNotThatKindOfClass);                                   \
    }                                                                       \
    ClassName::g_pDesc = ::newOdRxClass(#ClassName, ParentClass::desc(),    \
      ClassName::pseudoConstructor);                                        \
    g_pMainConstr = pPseudoBase->constructor();                             \
    pPseudoBase->setConstructor(ClassName::pseudoConstructor);              \
  }                                                                         \
  else                                                                      \
  {                                                                         \
    ODA_ASSERT(("Class ["#ClassName"] is already initialized.",0));         \
    throw OdError(eExtendedError);                                          \
  }                                                                         \
}                                                                           \
                                                                            \
/* Unregisters this class with DWGdirect. */                                \
void ClassName::rxUninit()                                                  \
{                                                                           \
  if (ClassName::g_pDesc)                                                   \
  {                                                                         \
    OdRxClass* pPseudoBase = PseudoBaseClass::desc();                       \
    pPseudoBase->setConstructor(g_pMainConstr);                       \
    ClassName::g_pMainConstr = 0;                                           \
    ::deleteOdRxClass(ClassName::g_pDesc);                                  \
    ClassName::g_pDesc = 0;                                                 \
  }                                                                         \
  else                                                                      \
  {                                                                         \
    ODA_ASSERT(("Class ["#ClassName"] is not initialized yet.",0));         \
    throw OdError(eNotInitializedYet);                                      \
  }                                                                         \
}

/** Description:
    Defines RTTI, initialization functions and constructor for pseudo database objects.
*/
#define ODDB_PSEUDO_DEFINE_MEMBERS(ClassName,ParentClass,PseudoBaseClass,DOCREATE)  \
                                                                                    \
ODRX_DEFINE_RTTI_MEMBERS(ClassName,ParentClass)                                     \
                                                                                    \
ODRX_DEFINE_PSEUDOCONSTRUCTOR(ClassName,DOCREATE)                                   \
                                                                                    \
ODDB_PSEUDO_DEFINE_INIT_MEMBERS(ClassName,ParentClass,pseudoConsFn,PseudoBaseClass) \
                                                                                    \
OdPseudoConstructorType ClassName::g_pMainConstr = 0;                               \
                                                                                    \
OdRxClass* ClassName::saveAsClass(OdRxClass*) const                                 \
{                                                                                   \
  return PseudoBaseClass::saveAsClass(PseudoBaseClass::desc());                     \
}

#include "DD_PackPop.h"

#endif //_ODDBCLASS_INCLUDED_

