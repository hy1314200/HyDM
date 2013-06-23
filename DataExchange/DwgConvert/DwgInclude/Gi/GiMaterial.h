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

#ifndef __ODGIMATERIAL_H__
#define __ODGIMATERIAL_H__

#include "DD_PackPush.h"

#include "Gi/GiMaterialData.h"

/** Description:

    {group:OdGi_Classes} 
*/
class OdGiMaterial
{
public:
  OdGiMaterial()
    : m_SpecularGloss(0.5)
    , m_OpacityPercent(1.0)
    , m_RefractionIndex(1.0)
  {}
  
  OdGiMaterialColor m_AmbientColor;

  OdGiMaterialColor m_DiffuseColor;
  OdGiMaterialMap   m_DiffuseMap;

  OdGiMaterialColor m_SpecularColor;
  OdGiMaterialMap   m_SpecularMap;
  double            m_SpecularGloss;

  OdGiMaterialMap   m_ReflectionMap;

  OdGiMaterialMap   m_OpacityMap;
  double            m_OpacityPercent;

  OdGiMaterialMap   m_BumpMap;

  OdGiMaterialMap   m_RefractionMap;
  double            m_RefractionIndex;
};

#include "DD_PackPop.h"

#endif

