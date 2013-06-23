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

#ifndef OD_DBHATCH_PATTERN_MANAGER
#define OD_DBHATCH_PATTERN_MANAGER

#include "OdString.h"
#include "Ge/GePoint2d.h"
#include "DbHatch.h"
#include "Ge/GeDoubleArray.h"

#include "DD_PackPush.h"


/** Description:

    {group:Other_Classes}
*/
class ODRX_ABSTRACT TOOLKIT_EXPORT OdHatchPatternManager : public OdRxObject
{
protected:
  /** Description:
      Constructor (no arguments).
  */
  OdHatchPatternManager() {}
public:
  ODRX_DECLARE_MEMBERS(OdHatchPatternManager);

  virtual void setApplicationService(OdDbHostAppServices *pService) = 0;

  /** Description:
      gets the pattern with specified name
  */
  virtual OdResult retrievePattern(OdDbHatch::HatchPatternType hpType, const OdString& patternName, 
                                   OdDb::MeasurementValue mvValue, OdHatchPattern& pattern) = 0;

  /** Description:
      appends the pattern with specified name
  */
  virtual void appendPattern(OdDbHatch::HatchPatternType hpType, const OdString& patternName, 
                             const OdHatchPattern& pattern, OdDb::MeasurementValue mvValue = OdDb::kEnglish) = 0;
};

typedef OdSmartPtr<OdHatchPatternManager> OdHatchPatternManagerPtr;

#include "DD_PackPop.h"

#endif

