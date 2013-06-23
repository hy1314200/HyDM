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


#ifndef _INC_DDBREDGE_3F82D9C2030D_INCLUDED
#define _INC_DDBREDGE_3F82D9C2030D_INCLUDED

#include "BrEntity.h"
#include "BrVertex.h"

#include "BrEnums.h"

class OdGeNurbCurve3d;
class OdGeCurve3d;
class OdCmEntityColor;

#include "DD_PackPush.h"

/** Description:
    Interface class for B-Rep edges.

    {group:OdBr_Classes}
*/
class ODBR_TOOLKIT_EXPORT OdBrEdge : public OdBrEntity
{
public:
  bool getVertex2(OdBrVertex &v) const;
  bool getVertex1(OdBrVertex &v) const;

  OdGe::EntityId getCurveType() const;

  OdGeCurve3d* getCurve() const;
  bool getCurveAsNurb(OdGeNurbCurve3d& nurb) const;

  /** Description:
      Returns true if the orientation of the edge from vertex 1 to vertex 2 is the 
      same as the orientation of the curve parameterization, false otherwise.
  */
  bool getOrientToCurve() const;
  bool getColor(OdCmEntityColor &color) const;

  OdBrEdge();

  ~OdBrEdge();

};

#include "DD_PackPop.h"

#endif /* _INC_DDBREDGE_3F82D9C2030D_INCLUDED */


