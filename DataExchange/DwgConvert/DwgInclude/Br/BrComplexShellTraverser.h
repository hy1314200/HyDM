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


#ifndef _INC_DDBRCOMPLEXSHELLTRAVERSER_3F83F6550148_INCLUDED
#define _INC_DDBRCOMPLEXSHELLTRAVERSER_3F83F6550148_INCLUDED

#include "BrComplex.h"
#include "BrShell.h"
#include "BrTraverser.h"

#include "DD_PackPush.h"

//The OdBrComplexShellTraverser class is the interface class 
//for OdComplex
//complex traversers. All the functionality supported by this 
//class
//is implemented by the class OdBrImpComplexShellTraverser.
//
//This class defines the functions that are pertinent to a 
//complex
//in the global context of a OdComplex. It is used to traverse all 
//of
//the unique complexes in a OdComplex.

/** Description:

    {group:OdBr_Classes}
*/
class ODBR_TOOLKIT_EXPORT OdBrComplexShellTraverser : public OdBrTraverser
{
public:
		OdBrComplexShellTraverser();

    OdBrErrorStatus setComplex( const OdBrComplex& OdComplex );

    void setShell(const OdBrShell& complex);

    void setComplexAndShell(const OdBrShell& complex);

    OdBrShell getShell() const;

    OdBrComplex getComplex() const;
};

#include "DD_PackPop.h"

#endif /* _INC_DDBRCOMPLEXSHELLTRAVERSER_3F83F6550148_INCLUDED */


