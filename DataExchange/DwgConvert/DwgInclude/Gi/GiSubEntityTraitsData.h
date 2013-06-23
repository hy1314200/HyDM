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



#ifndef __ODGISUBENTITYTRAITSDATA_H__
#define __ODGISUBENTITYTRAITSDATA_H__

#include "DD_PackPush.h"

#include "CmColor.h"

/** Description:

    {group:OdGi_Classes} 
*/
class OdGiSubEntityTraitsData
{
  // Members
  //
  int                     m_flags;
  OdCmEntityColor         m_cmColor;
  OdDbStub*               m_layerId;
  OdDbStub*               m_lineTypeId;
  OdGiFillType            m_fillType;
  OdDb::LineWeight        m_lineWeight;
  double                  m_lineTypeScale;
  double                  m_thickness;
  OdDb::PlotStyleNameType m_plotStyleNameType;
  OdDbStub*               m_plotStyleNameId;
  OdDbStub*               m_materialId;

public:
  enum
  {
    kLayerOff         = 1,
    kLayerFrozen      = 2
  };

  // Constructors/destructors
  //
  OdGiSubEntityTraitsData()
    : m_cmColor(OdCmEntityColor::kForeground)
    , m_flags(0)
  {
    m_plotStyleNameType = OdDb::kPlotStyleNameIsDictDefault;
    m_materialId =
    m_plotStyleNameId =
    m_layerId =
    m_lineTypeId = 0;
    m_fillType = kOdGiFillNever;
    m_lineWeight = OdDb::kLnWt000;
    m_lineTypeScale = 1.0;
    m_thickness = 0.0;
  }

  // Accessories / transformers
  //
  inline int flags() const;
  inline bool isLayerFrozen() const;
  inline bool isLayerOff() const;
  inline bool isLayerVisible() const;
  bool visibility() const { return isLayerVisible(); }
  const OdCmEntityColor& trueColor() const;
  OdUInt16 color() const;
  OdDbStub* layer() const;
  OdDbStub*  lineType() const;
  OdGiFillType fillType() const;
  OdDb::LineWeight lineWeight() const;
  double lineTypeScale() const;
  double thickness() const;
  OdDb::PlotStyleNameType plotStyleNameType() const;
  OdDbStub* plotStyleNameId() const;
  OdDbStub* material() const;

  void setFlags(int flags);
  void setTrueColor(const OdCmEntityColor& trueColor);
  void setColor(OdUInt16 colorIndex);
  void setLayer(OdDbStub* layerId);
  void setLineType(OdDbStub* lineTypeId);
  void setFillType(OdGiFillType fillType);
  void setLineWeight(OdDb::LineWeight lineWeight);
  void setLineTypeScale(double lineTypeScale);
  void setThickness(double thickness);
  void setPlotStyleName(OdDb::PlotStyleNameType plotStyleNameType, OdDbStub* plotStyleNameId);
  void setMaterial(OdDbStub* materialId);
};


inline int OdGiSubEntityTraitsData::flags() const
{
  return m_flags;
}
inline bool OdGiSubEntityTraitsData::isLayerVisible() const
{
  return (m_flags & (kLayerOff|kLayerFrozen))==0;
}
inline bool OdGiSubEntityTraitsData::isLayerFrozen() const
{
  return GETBIT(m_flags, kLayerFrozen);
}
inline bool OdGiSubEntityTraitsData::isLayerOff() const
{
  return GETBIT(m_flags, kLayerOff);
}
inline OdUInt16 OdGiSubEntityTraitsData::color() const
{
  return m_cmColor.colorIndex();
}
inline const OdCmEntityColor& OdGiSubEntityTraitsData::trueColor() const
{
  return m_cmColor;
}
inline OdDbStub* OdGiSubEntityTraitsData::layer() const
{
  return m_layerId;
}
inline OdDbStub*  OdGiSubEntityTraitsData::lineType() const
{
  return m_lineTypeId;
}
inline OdGiFillType OdGiSubEntityTraitsData::fillType() const
{
  return m_fillType;
}
inline OdDb::LineWeight OdGiSubEntityTraitsData::lineWeight() const
{
  return m_lineWeight;
}
inline double OdGiSubEntityTraitsData::lineTypeScale() const
{
  return m_lineTypeScale;
}
inline double OdGiSubEntityTraitsData::thickness() const
{
  return m_thickness;
}
inline OdDb::PlotStyleNameType OdGiSubEntityTraitsData::plotStyleNameType() const
{
  return m_plotStyleNameType;
}
inline OdDbStub* OdGiSubEntityTraitsData::plotStyleNameId() const
{
  return m_plotStyleNameId;
}
inline OdDbStub* OdGiSubEntityTraitsData::material() const
{
  return m_materialId;
}


inline void OdGiSubEntityTraitsData::setFlags(int flags)
{
  m_flags = flags;
}
inline void OdGiSubEntityTraitsData::setTrueColor(const OdCmEntityColor& rgb)
{
  m_cmColor = rgb;
}
inline void OdGiSubEntityTraitsData::setColor(OdUInt16 colorIndex)
{
  m_cmColor.setColorIndex(colorIndex);
}
inline void OdGiSubEntityTraitsData::setLayer(OdDbStub* layerId)
{
  m_layerId = layerId;
}
inline void OdGiSubEntityTraitsData::setLineType(OdDbStub* lineTypeId)
{
  m_lineTypeId = lineTypeId;
}
inline void OdGiSubEntityTraitsData::setFillType(OdGiFillType fillType)
{
  m_fillType = fillType;
}
inline void OdGiSubEntityTraitsData::setLineWeight(OdDb::LineWeight lineWeight)
{
  m_lineWeight = lineWeight;
}
inline void OdGiSubEntityTraitsData::setLineTypeScale(double lineTypeScale)
{
  m_lineTypeScale = lineTypeScale;
}
inline void OdGiSubEntityTraitsData::setThickness(double thickness)
{
  m_thickness = thickness;
}
inline void OdGiSubEntityTraitsData::setPlotStyleName(OdDb::PlotStyleNameType plotStyleNameType, OdDbStub* plotStyleNameId)
{
  m_plotStyleNameType = plotStyleNameType;
  m_plotStyleNameId = plotStyleNameId;
}
inline void OdGiSubEntityTraitsData::setMaterial(OdDbStub* materialId)
{
  m_materialId = materialId;
}


#include "DD_PackPop.h"

#endif // __ODGISUBENTITYTRAITSDATA_H__


