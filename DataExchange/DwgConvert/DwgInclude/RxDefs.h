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



// ODA typedefs
#ifndef _RX_DEFS_
#define _RX_DEFS_

/** Description:

    {group:DD_Namespaces}
*/
namespace OdRx
{
  
  
  //typedef void (*FcnPtr) ();
  
  enum DictIterType
  {
    kDictSorted   = 0,
    kDictCollated = 1,
    kDictReversed = 2
  };
  
  /*
  enum AppMsgCode
  {
    kNullMsg         = 0,
    kInitAppMsg      = 1,
    kUnloadAppMsg    = 2,
    kLoadDwgMsg      = 3,
    kUnloadDwgMsg    = 4,
    kInvkSubrMsg     = 5,
    kCfgMsg          = 6,
    kEndMsg          = 7,
    kQuitMsg         = 8,
    kSaveMsg         = 9,
    kDependencyMsg   = 10,
    kNoDependencyMsg = 11,
    kOleUnloadAppMsg = 12,
    kPreQuitMsg      = 13,
    kInitDialogMsg   = 14,
    kEndDialogMsg    = 15
  }; 
  
  enum AppRetCode
  {
    kRetOK = 0,
    kRetError = 3
  };
  */
}

#endif // _RX_DEFS_


