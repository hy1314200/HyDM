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

#ifndef __OD_GI_PERSPECT_PREP__
#define __OD_GI_PERSPECT_PREP__

#include "Gi/GiConveyorNode.h"
#include "Gi/GiDeviation.h"

#include "Ge/GeDoubleArray.h"

#include "DD_PackPush.h"

/** Description:

    {group:OdGi_Classes}
    This class preprocesses geometry before xforming it by 
    perspective matrix.
    Circles, arcs, ellipses, texts are tesselated.
    (Although for export tasks it may be more useful to switch arcs to nurbs curves)
    Polylines, polygons, meshes, shells, xlines, rays, NURBSes are passed through.
    Images and metafiles are not passed.
*/
class ODGI_EXPORT OdGiPerspectivePreprocessor : public OdGiConveyorNode
{
public:
  ODRX_DECLARE_MEMBERS(OdGiPerspectivePreprocessor);

  /**
    Sets max deviation for curve tesselation.
  */
  virtual void setDeviation(const OdGeDoubleArray& deviations) = 0;

  /**
    Sets deviation object to obtain max deviation for curve tesselation.
  */
  virtual void setDeviation(const OdGiDeviation* pDeviation) = 0;

  /**
    Sets the draw context object (to access to traits, etc).
  */
  virtual void setDrawContext(OdGiConveyorContext* pDrawCtx) = 0;
};

typedef OdSmartPtr<OdGiPerspectivePreprocessor> OdGiPerspectivePreprocessorPtr;

#include "DD_PackPop.h"


#endif // __OD_GI_PERSPECT_PREP__
