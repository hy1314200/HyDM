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

#ifndef __ODGIMATERIALDATA_H__
#define __ODGIMATERIALDATA_H__

#include "DD_PackPush.h"

#include "CmColor.h"
#include "Ge/GeMatrix3d.h"

/** Description:
    This class implements material colors by *color* *method* and value.
    
    Library:
    Gi
    
    {group:OdGi_Classes} 
*/
class OdGiMaterialColor
{
public:
  enum Method
  {
      kInherit  = 0, // Uses the current drawing *color*.
      kOverride = 1  // Uses the *color* set with setColor.
  };

  static const OdGiMaterialColor kNull; // Inherited *color* with blend *factor* of 1.0.

  OdGiMaterialColor()
    : m_method(kInherit)
    , m_factor(1.0)
  {
  }

  /** Description
    Sets the *color* *method* for this MaterialColor object.

    Arguments:
    method (I) *Color* *method*.

    Remarks:
    method will be one of the following: 
    
    @table
    Name          Value    Description
    kInherit      0        Uses the current drawing *color*.
    kOverride     1        Uses the *color* set with setColor.
  */
  void setMethod(
    Method method) 
  { m_method = method; }
  
  /** Description:
    Sets the *color* *factor* for this MaterialColor object.
    Arguments:
    factor (I) *Color* *factor*.
    Remarks:
    A *color* *factor* of 0.0 makes all colors black; a *color* *factor* of 1.0 leaves them unchanged.
  */
  void setFactor(
    double factor) 
  { m_factor = factor; }
  
  /** Description:
    Returns a reference to, or a copy of, the *color* of this MaterialColor object as 
    an OdCmEntityColor instance.
  
    Returns the *color* of this MaterialColor object as a reference to, or a copy of, an OdCmEntityColor instance.
  */
  OdCmEntityColor& color() 
  { return m_color; }


  /** Description
    Returns the *color* *method* for this MaterialColor object.

    Remarks:
    method will return one of the following: 
    
    @table
    Name          Value    Description
    kInherit      0        Uses the current drawing *color*.
    kOverride     1        Uses the *color* set with setColor.
  */
  Method method() const 
  { return m_method; }
  
  /** Description:
    Returns the *color* *factor* for this MaterialColor object.
    Arguments:
 
    Remarks:
    A *color* *factor* of 0.0 makes all colors black; a *color* *factor* of 1.0 leaves them unchanged.
  */
  double factor() const 
  { return m_factor; }
  
  const OdCmEntityColor& color() const 
  { return m_color; }

private:
  Method m_method;
  double m_factor;
  OdCmEntityColor m_color;
};

/** Description:
    This class implements mappers.
    
    Mappers determine how an OdGiMaterialMap is mapped to an object surface. 

    Library:
    Gi
    
    {group:OdGi_Classes} 
*/
class OdGiMapper
{
public:
  enum Projection
  {
    kInheritProjection  = 0, // Inherits *projection* from the current material's mapper.
    kPlanar             = 1, // Maps directly to XY coordinates.
    kBox                = 2, // Maps to planes perpendicular to major axes.
    kCylinder           = 3, // Maps to cylinder aligned with Z-axis.
    kSphere             = 4  // Maps to sphere aligned with Z-axis
  };

  enum Tiling
  {
    kInheritTiling    = 0, // Inherits *tiling* from the current material's mapper.
    kTile             = 1, // Repeats map along image axes.
    kCrop             = 2, // Crops map < 0.0 or > 1.0 on image axes.
    kClamp            = 3  // Clamps (stretches) map between 0.0 and 1.0 on image axes.
  };

  enum AutoTransform
  {
    kInheritAutoTransform = 0x0, // Inherits automatic *transform* from the current material/s mapper.
    kNone                 = 0x1, // No automatic *transform*.
    kObject               = 0x2, // Adjusts the mapper *transform* to align with and fit the current object.
    kModel                = 0x4  // Multiples the mapper *transform* by the current block *transform*.
  };

  static const OdGiMapper kIdentity;

  OdGiMapper()
    : m_projection(kPlanar)
    , m_tiling(kTile)
    , m_autoTransform(kNone)
  {}

  /** Description:
    Sets the type of *projection* for this Mapper object.
    
    Arguments:
    projection (I) *Projection* type.
    
    Remarks:
    projection will be one of the following:
    
    @table
    Name                  Value   Description
    kInheritProjection    0       Inherits *projection* from the current material's mapper.
    kPlanar               1       Maps directly to XY coordinates.
    kBox                  2       Maps to planes perpendicular to major axes.
    kCylinder             3       Maps to cylinder aligned with Z-axis.
    kSphere               4       Maps to sphere aligned with Z-axis
  */
  void setProjection(
    Projection projection) 
  { m_projection = projection; }
  
  /** Description:
    Sets the type of *tiling* for this Mapper object.
    
    Arguments:
    tiling (I) *Tiling* type.
    
    Remarks:
    tiling will be one of the following:
    
    @table
    Name                  Value   Description
    kInheritTiling        0       Inherits *tiling* from the current material's mapper.
    kTile                 1       Repeats map along image axes.
    kCrop                 2       Crops map < 0.0 or > 1.0 on image axes.
    kClamp                3       Clamps (stretches) map between 0.0 and 1.0 on image axes.
  */
  void setTiling(
    Tiling tiling) 
  { m_tiling = tiling; }
  
  /** Description:
    Sets the type of automatic *transform* for this Mapper object.
    
    Arguments:
    autoTransform (I) Automatic *transform* type.
    
    Remarks:
    autoTransform will be a combination of the following:
    
    @table
    Name                      Value   Description
    kInheritAutoTransform     0x0     Inherits automatic *transform* from the current material/s mapper.
    kNone                     0x1     No automatic *transform*.
    kObject                   0x2     Adjusts the mapper *transform* to align with and fit the current object.
    kModel                    0x4     Multiples the mapper *transform* by the current block *transform*.
  */
  void setAutoTransform(
    AutoTransform autoTransform) 
  { m_autoTransform = autoTransform; }
  /** Description:
    Returns a reference to, or a copy of, the transformatiom matrix for this Mapper object.
    
    Remarks:
    The transform matrix defines mapping of an OdGiMaterialMap image when applied to the surface of an object. 
  */
  OdGeMatrix3d& transform() 
  { return m_transform; }

  /** Description:
    Returns the type of *projection* for this Mapper object.
    
    Remarks:
    projection will return one of the following:
    
    @table
    Name                  Value   Description
    kInheritProjection    0       Inherits *projection* from the current material's mapper.
    kPlanar               1       Maps directly to XY coordinates.
    kBox                  2       Maps to planes perpendicular to major axes.
    kCylinder             3       Maps to cylinder aligned with Z-axis.
    kSphere               4       Maps to sphere aligned with Z-axis
  */
  Projection projection() const 
  { return m_projection; }
  
  /** Description:
    Returns the type of *tiling* for this Mapper object.
    
    Remarks:
    tiling will return one of the following:
    
    @table
    Name                  Value   Description
    kInheritTiling        0       Inherits *tiling* from the current material's mapper.
    kTile                 1       Repeats map along image axes.
    kCrop                 2       Crops map < 0.0 or > 1.0 on image axes.
    kClamp                3       Clamps (stretches) map between 0.0 and 1.0 on image axes.
  */
  Tiling tiling() const 
  { return m_tiling; }
  
  
  /** Description:
    Returns the type of automatic *transform* for this Mapper object.
    
    Remarks:
    autoTransform will return a combination of the following:
    
    @table
    Name                      Value   Description
    kInheritAutoTransform     0x0     Inherits automatic *transform* from the current material's mapper.
    kNone                     0x1     No automatic *transform*.
    kObject                   0x2     Adjusts the mapper *transform* to align with and fit the current object.
    kModel                    0x4     Multiples the mapper *transform* by the current block *transform*.
  */
  AutoTransform autoTransform() const 
  { return m_autoTransform; }
  
  const OdGeMatrix3d& transform() const 
  { return m_transform; };

private:
  Projection    m_projection;
  Tiling        m_tiling;
  AutoTransform m_autoTransform;
  OdGeMatrix3d  m_transform;
};

/** Description:
  This class implements material maps.
  
  Remarks:
  Material maps define bitmapped images which are applied to object surfaces.
  
  {group:OdGi_Classes} 
*/
class OdGiMaterialMap
{
public:
  enum Source
  {
    kScene = 0, // Image is created from the current scene.
    kFile  = 1  // Image is from a file.
  };

  static const OdGiMaterialMap kNull;

  OdGiMaterialMap()
    : m_source(kFile)
    , m_blendFactor(1.0)
  {}

  /** Description:
    Sets the image *source* for this MaterialMap object.
    
    Arguments:
    source (I) Image *source*.
    
    Remarks:
    source will be one of the following:
    
    @table
    Name      Value   Description
    kScene    0,      Image is created from the current scene.
    kFile     1       Image is from a file.
  */
  void setSource(
    Source source) 
  { m_source = source; }
  
  /** Descripion:
    Sets the *source* file name for this MaterialMap object.
    Arguments:
    fileName (I) Source file name.
  */
  void setSourceFileName(
    const OdString& fileName) 
  { m_fileName = fileName; }
  
  /** Descripion:
    Sets the blend factor for this MaterialMap object.
    Arguments:
    blendFactor (I) Blend factor.
    Remarks:
    A blend factor of 0.0 makes the MaterialMap invisible. A blend factor of 1.0 makes it opaque.
    In between, the MaterialMap is transparent.
  */
  void setBlendFactor(
    double blendFactor) 
  { m_blendFactor = blendFactor; }
  
  /** Description:
    Returns a reference to, or a copy of, the OdGiMapper for this MaterialMap object.
    
    Remarks:
    The *transform* matrix defines mapping of an OdGiMaterialMap image when applied to the surface of an object. 
  */
  OdGiMapper& mapper() 
  { return m_mapper; }

  /** Description:
    Returns the image *source* for this MaterialMap object.

    Remarks:
    source will be one of the following:
    
    @table
    Name      Value   Description
    kScene    0,      Image is created from the current scene.
    kFile     1       Image is from a file.
  */
  Source source() const 
  { return m_source; }
  
  /** Descripion:
    Returns the *source* file name for this MaterialMap object.
  */
  OdString sourceFileName() const 
  { return m_fileName; }
  
  /** Descripion:
    Returns the blend factor for this MaterialMap object.

    Remarks:
    A blend factor of 0.0 makes the MaterialMap invisible. A blend factor of 1.0 makes it opaque.
    In between, the MaterialMap is transparent.
  */
  double blendFactor() const 
  { return m_blendFactor; }
  
  const OdGiMapper& mapper() const 
  { return m_mapper; }

private:
  Source     m_source;
  OdString   m_fileName;
  double     m_blendFactor;
  OdGiMapper m_mapper;
};

#include "DD_PackPop.h"

#endif

