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


#ifndef _INC_DDBRFACE_3F82D5A203C8_INCLUDED
#define _INC_DDBRFACE_3F82D5A203C8_INCLUDED

#include "Br/BrEntity.h"
#include "Br/BrEnums.h"
#include "Ge/GeSurface.h"
#include "Ge/GeNurbSurface.h"

#include "DD_PackPush.h"

class OdCmEntityColor;

/** Description:
    Inteface class for B-Rep faces.  Faces have associated surface geometry.

    {group:OdBr_Classes}
*/
class ODBR_TOOLKIT_EXPORT OdBrFace : public OdBrEntity
{
public:

  /** Description
      Returns this face as a NURBS surface.
  */
  OdBrErrorStatus getSurfaceAsNurb(OdGeNurbSurface& nurb) const;

  OdGeSurface* getSurface() const;

  OdBrErrorStatus getSurfaceType(OdGe::EntityId& type) const;

  /** Description:
      Returns true if the orientation of the outside of the face is in
      the direction of the surface normal, false otherwise.
  */
  bool getOrientToSurface() const;
  bool getColor(OdCmEntityColor &color) const;

  OdBrFace();

  ~OdBrFace();

};

#include "DD_PackPop.h"

#endif /* _INC_DDBRFACE_3F82D5A203C8_INCLUDED */


