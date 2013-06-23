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

#if !defined(_ODAAPP_LOADREASONS_H_INCLUDED_)
#define _ODAAPP_LOADREASONS_H_INCLUDED_

/** Description:

    {group:DD_Namespaces}
*/
namespace OdaApp
{
  enum LoadReasons
  {
    kOnProxyDetection       = 0x01,
    kOnDWGdirectStartup     = 0x02,
    kOnCommandInvocation    = 0x04,
    kOnLoadRequest          = 0x08,
    kLoadDisabled           = 0x10,
    kTransparentlyLoadable  = 0x20
  };
};

#endif // !defined(_ODAAPP_LOADREASONS_H_INCLUDED_)


