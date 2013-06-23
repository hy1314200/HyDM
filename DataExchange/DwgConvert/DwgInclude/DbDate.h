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



#ifndef _ODDBDATE_INCLUDED_
#define _ODDBDATE_INCLUDED_

#include "DD_PackPush.h"

#include "OdTimeStamp.h"

class OdDbDwgFiler;

/** Description:
    This class represents Date objects in an OdDbDatabase instance.

    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbDate : public OdTimeStamp
{
public:
  /**
    Arguments:
    tStamp (I) TimeStamp.
    init (I) Initial value.
    
    Remarks:
    The default constructor initializes the Julian date and time to zero.
    
    init will be one of the following:
    
    @table
    Name                 Value    Description
    kInitZero            1        Zero.
    kInitLocalTime       2        Workstation date in local time.
    kInitUniversalTime   3        Workstation date in Universal (Grenwich Mean) Time..
  */ 
  OdDbDate();
  OdDbDate(
    const OdTimeStamp& tStamp);
  OdDbDate(
    InitialValue init);

  OdDbDate operator=(
    const OdTimeStamp& tStamp);

  void dwgIn(
    OdDbDwgFiler* inFiler);

  void dwgOut(
    OdDbDwgFiler* outFiler) const;
};

#include "DD_PackPop.h"

#endif // _ODDBDATE_INCLUDED_


