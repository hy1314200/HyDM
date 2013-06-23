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


#ifndef _ACISRENDERER_H_
#define _ACISRENDERER_H_

#include "Wr/wrExport.h"
#include "Wr/wrTriangulationParams.h"

class OdGiWorldDraw;
class OdGiViewportDraw;
class OdBrBrep;
class OdGeMatrix3d;

/** Description:

    {group:AcisRenderer_Classes}
*/
class RENDER_EXPORT OdAcisRenderer
{
protected:
  OdAcisRenderer() {}

public:  
  
  virtual ~OdAcisRenderer() {}

  enum
  {
    kNothing      = 0,
    kIsolines     = 1,
    kEdges        = 2,
    kShells       = 4,
    kOrderedEdges = 8
  };

  virtual void setBrep(const OdBrBrep& brep) = 0;

  virtual void enableCaching() = 0;
  virtual void disableCaching(bool clearCache = true) = 0;
  virtual bool isCachingEnabled() const = 0;

  virtual void setTriangulationParams(const wrTriangulationParams *TriangulationParams) = 0;
  virtual const wrTriangulationParams * getTriangulationParams() const = 0;

  virtual bool Draw(OdGiWorldDraw* pWd, OdUInt32 geomType) = 0;
  virtual bool DrawSilhouettes(OdGiViewportDraw* pVd) = 0;
  virtual void transformBy(const OdGeMatrix3d &xMat) = 0;
};

#endif //_ACISRENDERER_H_

