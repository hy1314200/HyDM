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

#ifndef __OD_GI_EXTACCUM__
#define __OD_GI_EXTACCUM__

#include "Ge/GeExtents3d.h"
#include "Gi/GiConveyorNode.h"

#include "DD_PackPush.h"

/** Description:

    {group:OdGi_Classes} 
*/
class ODGI_EXPORT OdGiExtAccum : public OdGiConveyorNode
{
public:
  ODRX_DECLARE_MEMBERS(OdGiExtAccum);

  virtual void setDrawContext(OdGiConveyorContext* pDrawCtx) = 0;

  /**
    Sets the draw context object (to access to traits, etc).
  */
  virtual OdGiConveyorGeometry& geometry() = 0;

  /**
    Gets the calculated extents.
  */
  virtual bool getExtents(OdGeExtents3d& exts) const = 0;

  /**
    Resets the calculating extents.
  */
  virtual void resetExtents(const OdGeExtents3d& exts = OdGeExtents3d()) = 0;

  virtual void addExtents(const OdGeExtents3d& exts) = 0;
};

typedef OdSmartPtr<OdGiExtAccum> OdGiExtAccumPtr;

#include "DD_PackPop.h"

#endif // __OD_GI_EXTACCUM__
