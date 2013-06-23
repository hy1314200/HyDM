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


#ifndef _INC_DDBRLOOPEDGETRAVERSER_3F82E1FB0177_INCLUDED
#define _INC_DDBRLOOPEDGETRAVERSER_3F82E1FB0177_INCLUDED

#include "BrTraverser.h"

class OdBrEdgeLoopTraverser;
class OdGeNurbCurve3d;
class OdGeNurbCurve2d;
class OdGeCurve3d;
class OdGeCurve2d;
class OdBrLoop;
class OdBrEdge;


#include "DD_PackPush.h"

/** Description:
    Interface class for B-Rep loop edge traversers.

    {group:OdBr_Classes}
*/
class ODBR_TOOLKIT_EXPORT OdBrLoopEdgeTraverser : public OdBrTraverser
{
public:
  OdGeCurve3d *getOrientedCurve() const;
  bool getOrientedCurveAsNurb(OdGeNurbCurve3d& nurb) const;

  OdGeCurve2d* getParamCurve() const;
  OdBrErrorStatus getParamCurveAsNurb( OdGeNurbCurve2d& nurb ) const;

  bool getEdgeOrientToLoop() const;

  OdBrEdge getEdge() const;
  OdBrLoop getLoop() const;

  void setEdge( const OdBrEdge& edge );
  OdBrErrorStatus setLoop( const OdBrLoop& loop );
  void setLoopAndEdge( const OdBrEdgeLoopTraverser &edgeLoop );

  OdBrLoopEdgeTraverser();
};

#include "DD_PackPop.h"

#endif /* _INC_DDBRLOOPEDGETRAVERSER_3F82E1FB0177_INCLUDED */


