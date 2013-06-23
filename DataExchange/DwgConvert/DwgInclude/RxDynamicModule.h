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

#ifndef _OD_RXDYNAMICMODULE_H_
#define _OD_RXDYNAMICMODULE_H_

#include "RxModule.h"
#include "DDVersion.h"
#include "OdString.h"

#include "DD_PackPush.h"

/** Description:

    {group:OdRx_Classes} 
*/
template<class T, class TInterface = T>
class OdRxStaticModule : public T
{
  ODRX_HEAP_OPERATORS();

  long      m_nLockCount;
  OdString  m_sName;

  OdRxStaticModule(const OdString& sName)
    : m_nLockCount(0)
    , m_sName(sName)
  { }
  void addRef() 
  { 
    ++m_nLockCount; 
  }
  void release() 
  { 
    ODA_ASSERT(m_nLockCount); 
    --m_nLockCount; 
  }
  long numRefs() const { return m_nLockCount; }
public:
  void* sysData() { return 0; }
  
  static OdRxModule* createModule(const OdString& sName)
  {
    return new OdRxStaticModule(sName);
  }

  void deleteModule() { delete this; }

  OdString moduleName() const { return m_sName; }
};

typedef OdRxModule* (*StaticModuleEntryPoint)(const OdChar* szModuleName);

#define ODRX_STATIC_MODULE_ENTRY_POINT(_UserModuleClass) odrxCreateModuleObject_For_##_UserModuleClass

/** Description:
    This macro is used to create an entry point function for a custom application which 
    are linked in statically to a DWGdirect client application.
*/
#define ODRX_DECLARE_STATIC_MODULE_ENTRY_POINT(_UserModuleClass) \
OdRxModule* ODRX_STATIC_MODULE_ENTRY_POINT(_UserModuleClass)(const OdChar* szModuleName)

/** Description:
  {group:Structs}
*/      
struct STATIC_MODULE_DESC
{
  const OdChar*           szAppName;
  StaticModuleEntryPoint  entryPoint;
};

/** Description: 
    Defines the start of the static module map, which contains entries for all
    custom DWGdirect modules that are linked in statically to a DWGdirect client
    application.
*/
#define ODRX_BEGIN_STATIC_MODULE_MAP() \
STATIC_MODULE_DESC g_ODRX_STATIC_MODULE_MAP[] = {

/** Description: 
    Defines an entry in the static module map, which contains entries for all
    custom DWGdirect modules that are linked in statically to a DWGdirect client
    application.

    Arguments:
    AppName (I) Registered application name, that can be later passed to 
      OdRxDynamicLinker::loadModule to load the static application.
    ModuleClassName (I) Name of the C++ class derived from OdRxModule, that 
      implements support for this module.
*/
#define ODRX_DEFINE_STATIC_APPLICATION(AppName, ModuleClassName) \
{ AppName, ODRX_STATIC_MODULE_ENTRY_POINT(ModuleClassName) },

/** Description: 
*/
#define ODRX_DEFINE_STATIC_APPMODULE(moduleName, ModuleClassName) \
ODRX_DEFINE_STATIC_APPLICATION(moduleName, ModuleClassName)

/** Description: 
    Defines the end of the static module map, which contains entries for all
    custom DWGdirect modules that are linked in statically to a DWGdirect client
    application.
*/
#define ODRX_END_STATIC_MODULE_MAP() \
{ 0, 0 } };

FIRSTDLL_EXPORT void odrxInitStaticModuleMap(STATIC_MODULE_DESC* pMap);

#define ODRX_INIT_STATIC_MODULE_MAP() odrxInitStaticModuleMap(g_ODRX_STATIC_MODULE_MAP)



#define ODRX_DEFINE_STATIC_MODULE(_UserModuleClass)\
ODRX_DECLARE_STATIC_MODULE_ENTRY_POINT(_UserModuleClass)\
{\
  return OdRxStaticModule<_UserModuleClass >::createModule(szModuleName);\
}



#if defined(_TOOLKIT_IN_DLL_) && !defined(__MWERKS__) ////////////////////////////////////////////////////////////////////////////////

#define ODRX_STATIC_MODULE_PATH "{5CEAD1EF-4D33-48fe-99E4-E09176BCF088}/"

#define ODRX_STATIC_APP_MODULE_NAME(AppName) (OdString(ODRX_STATIC_MODULE_PATH) + AppName + ".drx")

/** Description:

    {group:OdRx_Classes} 
*/
template<class T, class TInterface = T>
class OdRxWin32Module : public T
{
  long    m_nLockCount;
  HMODULE m_hModule;

  OdRxWin32Module() : m_nLockCount(0) { }
  void addRef() 
  { 
    ++m_nLockCount; 
  }
  void release() 
  { 
    ODA_ASSERT(m_nLockCount); 
    --m_nLockCount; 
  }
  long numRefs() const { return m_nLockCount; }
  OdRxWin32Module(HMODULE hModule)
    : m_nLockCount(0)
    , m_hModule(hModule)
  {}
  ODRX_HEAP_OPERATORS();
public:
  void* sysData() { return reinterpret_cast<void*>(m_hModule); }
  
  static OdRxWin32Module* createModule(HMODULE hModule) { return new OdRxWin32Module(hModule); }

  void deleteModule()
  {
    g_pSingletonModule = 0;
    delete this;
  }

  OdString moduleName() const
  {
    OdString res;
    ::GetModuleFileNameA(m_hModule, res.getBuffer(_MAX_PATH), _MAX_PATH);
    res.releaseBuffer();
    return res;
  }
};

/** Description:
    Creates the entry point function for a DWGdirect custom application.
*/
#define ODRX_DEFINE_DYNAMIC_MODULE(_UserModuleClass)\
static OdRxModule* g_pSingletonModule = 0;\
__declspec(dllexport) OdRxModule* odrxCreateModuleObject(HMODULE& hModule)\
{\
  if(!g_pSingletonModule)\
  {\
    g_pSingletonModule = OdRxWin32Module<_UserModuleClass >::createModule(hModule);\
    hModule = 0;\
  }\
  return g_pSingletonModule;\
}\
__declspec(dllexport) void odrxGetAPIVersion(int& nMajorVersion, int& nMinorVersion,\
                                             int& nMajorBuildVersion, int& nMinorBuildVersion)\
{\
  nMajorVersion       = DD_MAJOR_VERSION;\
  nMinorVersion       = DD_MINOR_VERSION;\
  nMajorBuildVersion  = DD_MAJOR_BUILD_VERSION;\
  nMinorBuildVersion  = DD_MINOR_BUILD_VERSION;\
}\

#else //#ifdef _TOOLKIT_IN_DLL_ ////////////////////////////////////////////////////////////////////////////////

#define ODRX_STATIC_MODULE_PATH ""

#define ODRX_STATIC_APP_MODULE_NAME(AppName) AppName

#define ODRX_DEFINE_DYNAMIC_MODULE(_UserModuleClass) ODRX_DEFINE_STATIC_MODULE(_UserModuleClass)


#endif //#ifdef _TOOLKIT_IN_DLL_ ////////////////////////////////////////////////////////////////////////////////


#include "DD_PackPop.h"

#endif // _OD_RXDYNAMICMODULE_H_


