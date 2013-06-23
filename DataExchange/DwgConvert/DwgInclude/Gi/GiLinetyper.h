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



#ifndef __ODGILINETYPER_H__
#define __ODGILINETYPER_H__


#include "Gi/GiConveyorNode.h"
#include "Gi/GiNonEntityTraits.h"
#include "Ge/GeDoubleArray.h"

class OdGiDeviation;

#include "DD_PackPush.h"


/** Description:

    {group:OdGi_Classes} 
*/
class ODGI_EXPORT OdGiLinetyper : public OdGiConveyorNode
{
public:
  ODRX_DECLARE_MEMBERS(OdGiLinetyper);

  virtual void setDrawContext( OdGiConveyorContext* pDrawCtx ) = 0;

  virtual void setDeviation(const OdGeDoubleArray& deviations) = 0;

  virtual void setDeviation(const OdGiDeviation* pDeviation) = 0;

  virtual OdUInt32 setLinetype(OdDbStub* id, double scale, double generationCriteria = 0.) = 0;

  virtual OdGiLinetypeTraits& linetypeTraits() const = 0;

  virtual void enable() = 0;

  virtual bool enabled() const = 0;

  virtual void disable() = 0;
  
  // makes linetyper to linetype circle curves (circles, circular arcs) 
  // analytically or using breaking to polyline.
  // bAnalytic = true turns on analytic linetyper
  // bAnalytic = false turns off analytic linetyper
  virtual void setAnalyticLinetypingCircles(bool bAnalytic) = 0;  
  virtual bool isAnalyticLinetypingCircles() const = 0;
  
  // makes linetyper to linetype complex curves (ellipses, elliptic arcs, nurbs curves) 
  virtual void setAnalyticLinetypingComplexCurves(bool bAnalytic) = 0;  
  virtual bool isAnalyticLinetypingComplexCurves() const = 0;
};

typedef OdSmartPtr<OdGiLinetyper> OdGiLinetyperPtr;

#include "DD_PackPop.h"

#endif //#ifndef __ODGILINETYPER_H__

