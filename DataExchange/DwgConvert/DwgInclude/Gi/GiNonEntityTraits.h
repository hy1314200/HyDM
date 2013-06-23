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

#ifndef __ODGI_NONENTITYTRAITS_H__
#define __ODGI_NONENTITYTRAITS_H__

#include "DD_PackPush.h"

#include "Gi/Gi.h"
#include "OdCodePage.h"
#include "Gi/GiDrawable.h"

class OdGiLinetypeDash;
class OdGiMaterialColor;
class OdGiMaterialMap;
class OdFont;
class OdTtfDescriptor;
class OdGiTextStyle;

/** Description:

    {group:OdGi_Classes} 
*/
class FIRSTDLL_EXPORT OdGiLayerTraits : public OdGiDrawableTraits
{
public:
  ODRX_DECLARE_MEMBERS(OdGiLayerTraits);

  // returned via setAttributes()
  enum
  {
    kByBlock    = (OdGiDrawable::kLastFlag << 1),
    kFrozen     = (OdGiDrawable::kLastFlag << 2),
    kOff        = (OdGiDrawable::kLastFlag << 3),
    kPlottable  = (OdGiDrawable::kLastFlag << 4)
  };

  virtual OdCmEntityColor color() const = 0;
  virtual OdDb::LineWeight lineweight() const = 0;
  virtual OdDbStub* linetype() const = 0;
  virtual OdDb::PlotStyleNameType plotStyleNameType() const = 0;
  virtual OdDbStub* plotStyleNameId() const = 0;

  virtual void setColor(const OdCmEntityColor& cl) = 0;
  virtual void setLineweight(OdDb::LineWeight lw) = 0;
  virtual void setLinetype(OdDbStub* id) = 0;
  virtual void setPlotStyleName(OdDb::PlotStyleNameType, OdDbStub* = 0) = 0;
};

typedef OdSmartPtr<OdGiLayerTraits> OdGiLayerTraitsPtr;


/** Description:

    {group:OdGi_Classes} 
*/
class FIRSTDLL_EXPORT OdGiLinetypeTraits : public OdGiDrawableTraits
{
public:
  ODRX_DECLARE_MEMBERS(OdGiLinetypeTraits);

  // returned via setAttributes()
  enum
  {
    kByBlock    = (OdGiDrawable::kLastFlag << 1),
    kByLayer    = (OdGiDrawable::kLastFlag << 2),
    kContinuous = (OdGiDrawable::kLastFlag << 3)
  };

  virtual double patternLength() const = 0;
  virtual void dashes(OdArray<OdGiLinetypeDash>& dashes) = 0;
  virtual double scale() const = 0;
  
  virtual void setDashes(const OdArray<OdGiLinetypeDash>& dashes) = 0;
  virtual void setScale(double patternLength) = 0;
  virtual void setPatternLength(double patternLength) = 0;
};

typedef OdSmartPtr<OdGiLinetypeTraits> OdGiLinetypeTraitsPtr;

/** Description:

    {group:OdGi_Classes} 
*/
class FIRSTDLL_EXPORT OdGiTextStyleTraits : public  OdGiDrawableTraits
{
public:
  ODRX_DECLARE_MEMBERS(OdGiTextStyleTraits);

  virtual void textStyle(OdGiTextStyle& giTextStyle) const = 0;

  virtual void setTextStyle(const OdGiTextStyle& giTextStyle) = 0;
};

typedef OdSmartPtr<OdGiTextStyleTraits> OdGiTextStyleTraitsPtr;

/** Description:

    {group:OdGi_Classes} 
*/
class FIRSTDLL_EXPORT OdGiMaterialTraits : public OdGiDrawableTraits
{
public:
  ODRX_DECLARE_MEMBERS(OdGiMaterialTraits);

  virtual void ambient(OdGiMaterialColor& color) const = 0;
  virtual void diffuse(OdGiMaterialColor& color, OdGiMaterialMap& map) const = 0;
  virtual void specular(OdGiMaterialColor& color, OdGiMaterialMap& map, double& dGloss) const = 0;
  virtual void reflection(OdGiMaterialMap& map) const = 0;
  virtual void opacity(double& dPercentage, OdGiMaterialMap& map) const = 0;
  virtual void bump(OdGiMaterialMap& map) const = 0;
  virtual void refraction(double& dIndex, OdGiMaterialMap& map) const = 0;

  virtual void setAmbient(const OdGiMaterialColor& color) = 0;
  virtual void setDiffuse(const OdGiMaterialColor& color, const OdGiMaterialMap& map) = 0;
  virtual void setSpecular(const OdGiMaterialColor& color, const OdGiMaterialMap& map, double dGloss) = 0;
  virtual void setReflection(const OdGiMaterialMap& map) = 0;
  virtual void setOpacity(double dPercentage, const OdGiMaterialMap& map) = 0;
  virtual void setBump(const OdGiMaterialMap& map) = 0;
  virtual void setRefraction(double dIndex, const OdGiMaterialMap& map) = 0;
};

typedef OdSmartPtr<OdGiMaterialTraits> OdGiMaterialTraitsPtr;

#include "DD_PackPop.h"

#endif

