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



// DynamicLinker.h: interface for the OdaDynamicLinkerI class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(_ODADYNAMICLINKERI_H_INCLUDED_)
#define _ODADYNAMICLINKERI_H_INCLUDED_

class OdRxDictionary;
class OdDbSystemServices;
class OdRxDLinkerReactor;

#include "RxDictionary.h"

class OdRxModule;
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdRxModule pointers.
*/
typedef OdSmartPtr<OdRxModule> OdRxModulePtr;

class OdRxSystemServices;
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdRxSystemServices pointers.
*/
typedef OdSmartPtr<OdRxSystemServices> OdRxSystemServicesPtr;

#include "DD_PackPush.h"

/** Description:
    This class implements Dynamic Linker services for DWGdirect (DRX) applications:
    
    The following services are implemented:
    
    o  Loading applications by specified by application name.
    
    o  Loading and unloading modules specified by file name.
    
    o  Addition and removal of reactors from the dynamic linker reactor chain.
    
    
    Note:
    There is exactly one OdRxDynamicLinker class object. It is gloval to DWGdirect,
    and thus is non-instantiable for DWGdirect applications.
    
    A pointer to the OdRxDynamicLinker object will be returned by the
    global odrxDynamicLinker() function.

    {group:OdRx_Classes} 
*/
class FIRSTDLL_EXPORT OdRxDynamicLinker : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdRxDynamicLinker);

  /** Description:
    Adds the specified reactor to the dynamic linker reactor chain.
    Arguments:
    pReactor (I) Pointer to the reactor.
  */
  virtual void addReactor (
    OdRxDLinkerReactor* pReactor) = 0;
  /** Description:
    Removes the specified reactor to the dynamic linker reactor chain.
    Arguments:
    pReactor (I) Pointer to the reactor.
  */
  virtual void removeReactor (
    OdRxDLinkerReactor* pReactor) = 0;

  /** Description:
    Loads the specified module.
    
    Remarks:
    Returns a pointer to the abstract module object.
    
    Arguments:
    moduleFileName (I) Module file name to load.
    silent (I) If and only if true, no load status message will be printed.
  */
  virtual OdRxModulePtr loadModule(
    const OdChar* moduleFileName, bool silent = true) = 0;

  /** Description:
    Unoads the specified module.
    
    Arguments:
    moduleFileName (I) Module file name to unload.
  */
  virtual bool unloadModule(
    const OdChar* moduleFileName) = 0;


  /** Description:
    Unoads all unreferenced modules.
  */
  virtual bool unloadUnreferenced() = 0;

  /** Description:
    Maps the specfied application name to the a module file name,
    and loads that module.

    Arguments:
    applicationName (I) DRX application name.
    silent (I) If and only if true, no load status message will be printed.
  */
  virtual OdRxModulePtr loadApp(
    const OdChar* applicationName, 
    bool silent = true) = 0;

  /** Description:
    Returns a pointer to the OdDbSystemServices instance that is used for file creation and access.
    
    See also:
    odInitialize
  */
  virtual OdRxSystemServicesPtr sysServices() const = 0;

  /** Description:
    Returns a pointer to the OdRxDictionary instance that created by OdInitialize.
  */
  virtual OdRxDictionaryPtr sysRegistry() const = 0;
};

/** Description:
    Performs DWGdirect system initialization.  
    
    Remarks:
    This function registers all classes supported by DWGdirect. Instances of these classes may
    Thus be created by client applications. 
    
    It also allows the user to specify the OdDbSystemServices instance that to be used for file creation and access.  
    
    Note:
    This function should be called once per process prior to any other DWGdirect calls.
    
    Arguments:
    pSystemServices (I) Pointer to SystemServices object.
*/
TOOLKIT_EXPORT void odInitialize(
  OdDbSystemServices* pSystemServices);

/** Description:
    Performs DWGdirect system uninitialization.  

    Remarks:
    This function releases the dynamic class registration data and the OdDbSystemServices pointer 
    associated with the earlier call to odInitialize.  
    
    Note:
    This function should be called once per process as the last operation performed by DWGdirect.
*/
TOOLKIT_EXPORT void odUninitialize();

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdRxDynamicLinker pointers.
*/
typedef OdSmartPtr<OdRxDynamicLinker> OdRxDynamicLinkerPtr;


/** Description:
    Returns the DWGdirect global dynamic linker.
*/
FIRSTDLL_EXPORT OdRxDynamicLinker* odrxDynamicLinker();


#include "DD_PackPop.h"

#endif // !defined(_ODADYNAMICLINKERI_H_INCLUDED_)


