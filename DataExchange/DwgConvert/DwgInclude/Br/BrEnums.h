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


// OdBrEnums.h -- declaration of enumerated types used in the
//                OdBr B-Rep and traverser classes.


#ifndef ODBRENUMS_H
#define ODBRENUMS_H
#include "OdaDefs.h"

enum OdBrErrorStatus
{ 
	 odbrOK
 , odbrWrongObjectType
 , odbrUnrecoverableErrors
 , odbrInvalidObject
 , odbrMissingTopology
 , odbrUnsuitableTopology
 , odbrMissingGeometry
 , odbrInvalidInput
 , odbrDegenerateTopology
 , odbrWrongDatabase
 , odbrUninitialisedObject
 , odbrNotInDatabase
 , odbrDegenerateGeometry
 , odbrOutOfMemory
 , odbrBrepChanged
};

/** Description:

    {group:OdBr_Classes}
*/
class OdBrException : public OdError
{
public:
  OdBrException( OdBrErrorStatus );
  OdBrErrorStatus getErrorStatus() const;
};

#define BR_THROW(n) throw OdBrException(n)

#endif // ODBRENUMS_H

