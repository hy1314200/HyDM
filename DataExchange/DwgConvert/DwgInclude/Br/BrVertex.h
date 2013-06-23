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


#ifndef _INC_DDBRVERTEX_3F82DAA9031C_INCLUDED
#define _INC_DDBRVERTEX_3F82DAA9031C_INCLUDED

#include "BrEntity.h"
#include "BrEnums.h"
#include "Ge/GePoint3d.h"

#include "DD_PackPush.h"


/** Description:
    Interface class for B-Rep vertices.  A B-Rep vertex has an associated 3d point.

    {group:OdBr_Classes}
*/
class ODBR_TOOLKIT_EXPORT OdBrVertex : public OdBrEntity
{
public:
  OdGePoint3d getPoint() const;

  OdBrVertex();

  ~OdBrVertex();

};

#include "DD_PackPop.h"

#endif /* _INC_DDBRVERTEX_3F82DAA9031C_INCLUDED */


