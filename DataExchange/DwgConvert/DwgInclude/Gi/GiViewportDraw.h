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



#ifndef __ODGIVIEWPORTDRAW_H__
#define __ODGIVIEWPORTDRAW_H__

#include "GiCommonDraw.h"
#include "GiViewport.h"
#include "GiViewportGeometry.h"

#include "DD_PackPush.h"

/** Description:
    This class defines the functionality for *viewport* -dependent entity-level vectorization.

    Remarks:
    Consider a circular cone. A circular cone can be drawn as a circle and two silhouette lines. The circle could be drawn with
    the worldDraw() function, but the the silhouette lines are dependent on the view direction,
    and must be computed with by viewportDraw().
    
    Library:
    Gi
    
    {group:OdGi_Classes} 
*/
class FIRSTDLL_EXPORT OdGiViewportDraw : public OdGiCommonDraw 
{ 
public:
  ODRX_DECLARE_MEMBERS(OdGiViewportDraw);

  /** Description:
    Returns a reference to the OdGiViewport object associated with this object.
    
    Remarks:
    The OdGiViewport contains information specific to the *viewport* being drawn.
  */
  virtual OdGiViewport& viewport() const = 0;

  /** Description:
    Returns the OdGiViewportGeometry object associated with this object.  
    
    Remarks:
    This instance contains the functions that can be used by an entity to
    vectorize itself.
  */
  virtual OdGiViewportGeometry& geometry() const = 0;

  /** Description:
    Returns the number of viewports that have been deleted from the current *database* 
    during this editing session..
  */
  virtual OdUInt32 sequenceNumber() const = 0;

  /** Description:
    Returns true if and only if specified *viewport* ID is a valid *viewport* ID.
    
    Arguments:
    viewportId (I) Viewport ID.
    
    Remarks:
    viewportId is typically obtained from viewport().viewportId().
  */
  virtual bool isValidId(
    const OdUInt32 viewportId) const = 0;

  /** Description:
    Returns the object ID of the OdDbViewport object associated with this object.
    
    Remarks:
    Returns NULL if TileMode == 1.
  */
  virtual OdDbStub* viewportObjectId() const = 0;
};

#include "DD_PackPop.h"

#endif //#ifndef __ODGIVIEWPORTDRAW_H__


