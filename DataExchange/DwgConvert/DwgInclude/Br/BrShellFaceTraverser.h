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


#ifndef _INC_DDBRSHELLFACETRAVERSER_3F83F80B005D_INCLUDED
#define _INC_DDBRSHELLFACETRAVERSER_3F83F80B005D_INCLUDED

#include "BrShell.h"

#include "BrFace.h"

#include "BrTraverser.h"

#include "DD_PackPush.h"


/** Description:
    Interface class for B-Rep shell face traversers.

    {group:OdBr_Classes}
*/
class ODBR_TOOLKIT_EXPORT OdBrShellFaceTraverser : public OdBrTraverser
{
public:
  OdBrFace getFace() const;
  OdBrShell getShell() const;

  OdBrErrorStatus setShell(const OdBrShell& shell);
  void setFace(const OdBrFace& face);
  void setShellAndFace(const OdBrFace& face);

  OdBrShellFaceTraverser();
};

#include "DD_PackPop.h"

#endif /* _INC_DDBRSHELLFACETRAVERSER_3F83F80B005D_INCLUDED */


