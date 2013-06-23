#ifndef _ODDBUSERIO_H_INCLUDED_
#define _ODDBUSERIO_H_INCLUDED_

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



#include "DbExport.h"
#include "OdPlatform.h"
#include "Ed/EdUserIO.h"

class OdDbUnitsFormatter;

/** Description:

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbUserIO : public OdEdUserIO
{
public:
  ODRX_DECLARE_MEMBERS(OdDbUserIO);

  /** Description:
  */
  virtual OdDbUnitsFormatter& formatter();

  /** Description:
      Outputs the specified prompt string, and then reads in 3 double values separated by commas,
      returning these 3 values in an OdGePoint3d object.

      Arguments:
      prompt (I) String that is output prior to requesting the point values.  If this string
        is empty, a default prompt string is sent to the output.
      bNoLimCheck (I) 
      pBasePt (I)
      pDefVal (I) Default return value to be used if an empty string is received as input
        during this operation.

      Remarks:
      This function uses the underlying OdEdBaseIO functions putString and getString for low
      level output and input.  
  */
  virtual OdGePoint3d getPoint (const OdChar* prompt,
                                bool bNoLimCheck = false,
                                const OdGePoint3d* pBasePt = 0,
                                const OdGePoint3d* pDefVal = 0,
                                OdEdUserIOTracker<OdGePoint3d>* = 0) = 0;

  virtual double      getAngle (const OdChar* prompt,
                                const OdGePoint3d* pBasePt = 0,
                                OdEdUserIOTracker<double>* = 0) = 0;
  virtual double      getAngle (const OdChar* prompt,
                                double defVal,
                                const OdGePoint3d* pBasePt = 0,
                                OdEdUserIOTracker<double>* = 0) = 0;

  virtual double      getOrient(const OdChar* prompt,
                                const OdGePoint3d* pBasePt = 0,
                                OdEdUserIOTracker<double>* = 0) = 0;
  virtual double      getOrient(const OdChar* prompt,
                                double defVal,
                                const OdGePoint3d* pBasePt = 0,
                                OdEdUserIOTracker<double>* = 0) = 0;

  virtual OdGePoint3d getCorner(const OdChar* prompt,
                                bool bNoLimCheck = false,
                                const OdGePoint3d* pBasePt = 0,
                                const OdGePoint3d* pDefVal = 0,
                                OdEdUserIOTracker<OdGePoint3d>* = 0) = 0;

  virtual double      getDist  (const OdChar* prompt,
                                const OdGePoint3d* pBasePt = 0,
                                OdEdUserIOTracker<double>* = 0) = 0;
  virtual double      getDist  (const OdChar* prompt,
                                double defVal,
                                const OdGePoint3d* pBasePt = 0,
                                OdEdUserIOTracker<double>* = 0) = 0;
};


#endif //#ifndef _ODDBUSERIO_H_INCLUDED_

