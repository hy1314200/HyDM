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
// programs incorporating this software must include the following statment 
// with their copyright notices:
//
//      DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef __ODGIDEVIATION_H__
#define __ODGIDEVIATION_H__


#include "Gi/GiCommonDraw.h"
#include "Ge/GeDoubleArray.h"
#include "Ge/GePoint3d.h"

/** Description:
    Defines an interface that returns the deviation values used for anisotropic space 
    (perspective view).

    {group:OdGi_Classes} 
*/
class OdGiDeviation
{
public:

  /** Description:
      Returns the maximum deviation allowed during the 
      tessellation process, for the specified deviation type.  

      See Also:
        OdGiGeometrySimplifier::setDeviation
  */
  virtual double deviation(const OdGiDeviationType deviationType, const OdGePoint3d& point) const = 0;
};

inline OdGeDoubleArray odgiGetAllDeviations(const OdGiDeviation& devObj, const OdGePoint3d& at = OdGePoint3d::kOrigin)
{
  OdGeDoubleArray values(5);
  values.append(devObj.deviation(kOdGiMaxDevForCircle,   at));
  values.append(devObj.deviation(kOdGiMaxDevForCurve,    at));
  values.append(devObj.deviation(kOdGiMaxDevForBoundary, at));
  values.append(devObj.deviation(kOdGiMaxDevForIsoline,  at));
  values.append(devObj.deviation(kOdGiMaxDevForFacet,    at));
  return values;
}

#endif //#ifndef __ODGIDEVIATION_H__

