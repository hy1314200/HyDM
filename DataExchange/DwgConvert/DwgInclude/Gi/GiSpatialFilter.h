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



#ifndef __ODGISPATIALFILTER__
#define __ODGISPATIALFILTER__

#include "Gi/GiConveyorNode.h"

#include "Ge/GeDoubleArray.h"
#include "Ge/GeExtents2d.h"

#include "DD_PackPush.h"

class OdGiDeviation;
class OdGiConveyorContext;

/** Description:

    Conveyor member performing spatial filtering by 3D rectangular prism.

    o exts        - base of the prism.
    o bClipLowerZ - if lower boundary exists.
    o dLowerZ     - lower boundary.
    o bClipUpperZ - if upper boundary exists.
    o dUpperZ     - upper boundary.

    {group:OdGi_Classes} 
*/
class ODGI_EXPORT OdGiSpatialFilter : public OdRxObject
{
protected:
  OdGiSpatialFilter();

public:
  ODRX_DECLARE_MEMBERS(OdGiSpatialFilter);

  virtual OdGiConveyorInput& input() = 0;
  virtual OdGiConveyorOutput& insideOutput() = 0;
  virtual OdGiConveyorOutput& intersectsOutput() = 0;
  virtual OdGiConveyorOutput& disjointOutput() = 0;

  // set output conveyor geometry to this member, if it is needn't
  static OdGiConveyorGeometry& kNullGeometry;

  virtual void set(const OdGeExtents2d& exts,
                   bool bClipLowerZ = false,
                   double dLowerZ = 0.0,
                   bool   bClipUpperZ = false,
                   double dUpperZ = 0.0) = 0;

  virtual void get(OdGeExtents2d& exts,
                   bool&   bClipLowerZ,
                   double& dLowerZ,
                   bool&   bClipUpperZ,
                   double& dUpperZ) const = 0;
  
  /**
    Sets the draw context object (to access to traits, etc).
  */
  virtual void setDrawContext(OdGiConveyorContext* pDrawCtx) = 0;
};

typedef OdSmartPtr<OdGiSpatialFilter> OdGiSpatialFilterPtr;

#include "DD_PackPop.h"

#endif //#ifndef __ODGISPATIALFILTER__
