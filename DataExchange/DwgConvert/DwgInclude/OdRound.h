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



#ifndef _ODROUND_INCLUDED_
#define _ODROUND_INCLUDED_

#include <math.h>
#include <limits.h>

inline double OdRound(double a)
{
  double aFloor = ::floor(a);
  if(a-aFloor >= 0.5)
    return aFloor+1.0;
  return aFloor;
}


inline long OdRoundToLong(double a)
{
  if(a > 0.)
  {
    a = ::floor(a + .5);
    if (a > double(LONG_MAX))
      throw OdError(eArithmeticOverflow);
  }
  else
  {
    a = ::ceil(a - .5);
    if (a < double(LONG_MIN))
      throw OdError(eArithmeticOverflow);
  }
  return long(a);
}

inline long OdTruncateToLong(double a)
{
  if(a > 0.)
  {
    a = ::floor(a + .5);
    if (a > double(LONG_MAX))
        return LONG_MAX;
  }
  else
  {
    a = ::ceil(a - .5);
    if (a < double(LONG_MIN))
      return LONG_MIN;
  }
  return long(a);
}

#endif //#ifndef _ODROUND_INCLUDED_

