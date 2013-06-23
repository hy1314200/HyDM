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

#ifndef ODA_CM_COLOR
#define ODA_CM_COLOR

#include "OdString.h"
#include "Gi/Gi.h"

class OdCmEntityColor;
class OdDbDwgFiler;
class OdDbDxfFiler;
class OdDbAuditInfo;

#include "DD_PackPush.h"

/** Description:
    This class implements EntityColor objects, which represent the colors of OdDbEntity objects.

    Remarks:
    EntityColor objects may specify any of the following *color* methods:

    @table
    Name         Value    Description
    kByLayer     0xC0     *Color* of the layer on which it resides.
    kByBlock     0xC1     *Color* of the block reference in which it is contained.
    kByColor     0xC2     *Color* specified by RGB value.
    kByACI       0xC3     *Color* specified by an index (ACI) into a *color* table.
    kForeground  0xC5     Editor foreground *color*.
    kNone        0xC7     No *color*.

    An entity has no *color* from the time it is first instantiated until it is assigned one
    or added to a *database*.
    
    Colors are stored in the m_RGBM member of the EntityColor object, defined as follows:

              typedef OdUInt32 RGBM;
              RGBM   m_RGBM
    
    Colors are stored in m_RGBM as follows:
    
    @table
    Byte      Description
    0         Blue component.
    1         Green component.
    2         Red component.
    high      *Color* Method.
    
    @table
    Byte      Description
    0-1       ACI Signed.
    2         Not used.
    high      *Color* Method.
    
    {group:OdCm_Classes}
*/
class FIRSTDLL_EXPORT OdCmEntityColor
{
public:
  ODRX_HEAP_OPERATORS();
  enum Color
  {
    kRed,
    kGreen,
    kBlue
  };
  
  enum ColorMethod
  {
    kByLayer    = 0xC0,   // *Color* of the layer on which it resides.
    kByBlock    = 0xC1,   // *Color* of the block reference in which it is contained.
    kByColor    = 0xC2,   // *Color* specified by RGB value.
    kByACI      = 0xC3,   // *Color* specified by an index (ACI) into a *color* table
    kForeground = 0xC5,   // Editor foreground *color*.
    kNone       = 0xC7    // No *color*.
  };
  
  enum ACIcolorMethod
  {
    kACIbyBlock     = 0,    // ByBlock.
    kACIforeground  = 7,    // Editor foreground *color*.
    kACIbyLayer     = 256,  // ByLayer.

    kACIclear       = 0,    // Clear.

    kACIRed         = 1,    // Red.
    kACIYellow      = 2,    // Yellow.
    kACIGreen       = 3,    // Green.
    kACICyan        = 4,    // Cyan.
    kACIBlue        = 5,    // Blue.
    kACIMagenta     = 6,    // Magenta.

    kACIstandard    = 7,    
    kACImaximum     = 255,
    kACInone        = 257,  // None
    kACIminimum     = -255
  };
  
  typedef OdUInt32 RGBM;
  
  /** Arguments:
    colorMethod (I) *Color* method.
    red (I) Red component.
    green (I) Green component.
    blue (I) Blue component.
    
    Remarks:
    Default ColorMethod is kByColor. 
  */
  OdCmEntityColor() : m_RGBM(0) { setColorMethod(kByColor); }
  OdCmEntityColor(
    const OdCmEntityColor & color) : m_RGBM(color.m_RGBM) { }
  OdCmEntityColor(
    ColorMethod colorMethod) : m_RGBM(0) { setColorMethod(colorMethod); }
  OdCmEntityColor(
    OdUInt8 red, 
    OdUInt8 green, 
    OdUInt8 blue)
  {
    setColorMethod(kByColor);
    setRGB(red, green, blue);
  }

  OdCmEntityColor& operator =(
    const OdCmEntityColor& color)
  {
    m_RGBM = color.m_RGBM;
    return *this;
  }

  bool operator ==(
    const OdCmEntityColor& color) const
  {
    return m_RGBM == color.m_RGBM;
  }
  
  bool operator !=(
    const OdCmEntityColor& color) const
  {
    return m_RGBM != color.m_RGBM;
  }

  /** Description
    Sets the *color* method for this EntityColor object.

    Arguments:
    colorMethod (I) *Color* method.

    Remarks:
    colorMethod will be one of the following: 
    
    @table
    Name         Value    Description
    kByLayer     0xC0     *Color* of the layer on which it resides.
    kByBlock     0xC1     *Color* of the block reference in which it is contained.
    kByColor     0xC2     *Color* specified by RGB value.
    kByACI       0xC3     *Color* specified by an index (ACI) into a *color* table.
    kForeground  0xC5     *Color* of editor foreground.
    kNone        0xC7     No *color*.

    An entity has no *color* from the time it is first instantiated until it is assigned one
    or added to a *database*.
  */
  void setColorMethod(
    ColorMethod colorMethod) { OdCmEntityColor::setColorMethod(&m_RGBM, colorMethod); }

  /** Description
    Returns the *color* method for this EntityColor object.

    Remarks:
    colorMethod returns one of the following:
     
    @table
    Name         Value    Description
    kByLayer     0xC0     *Color* of the layer on which it resides.
    kByBlock     0xC1     *Color* of the block reference in which it is contained.
    kByColor     0xC2     *Color* specified by RGB value.
    kByACI       0xC3     *Color* specified by an index (ACI) into a *color* table.
    kForeground  0xC5     *Color* of editor foreground.
    kNone        0xC7     No *color*.

    An entity has no *color* from the time it is first instantiated until it is assigned one
    or added to a *database*.
  */
  ColorMethod colorMethod() const
  {
    return colorMethod(&m_RGBM);
  } 
  
  /** Description:
    Sets the *color* value and *color* method for this EntityColor object.

    Arguments:
    color (I) *Color* value and *color* method.
    
    Remarks:
    *Color* value and *color* method are packed as follows:
    
    @table
    Byte      Description
    0         Blue component.
    1         Green component.
    2         Red component.
    high      *Color* Method.
    
    See Also:
    OdCmEntityColor::ColorMethod
  */
  void setColor(
    OdUInt32 color);

  /** Description:
    Returns the *color* value and *color* method for this EntityColor object.

    Remarks:
    *Color* value and *color* method are packed as follows:
    
    @table
    Byte      Description
    0         Blue component.
    1         Green component.
    2         Red component.
    high      *Color* Method.
    
    See Also:
    OdCmEntityColor::ColorMethod
  */
  OdUInt32 color() const { return m_RGBM; } 
  
  /** Description:
    Sets the *color* index (ACI) for this EntityColor object.

    Arguments:
    colorIndex (I) *Color* index.
    
    Remarks:
    colorIndex will be one of the following:
    
    @table
    Name              Value   Description
    kACIbyBlock       0       ByBlock.
    kACIforeground    7       Foreground *color*.
    kACIbyLayer       256     ByLayer. 
    kACIRed           1       Red. 
    kACIYellow        2       Yellow. 
    kACIGreen         3       Green. 
    kACICyan          4       Cyan. 
    kACIBlue          5       Blue. 
    kACIMagenta       6       Magenta. 
    ..                8-255   Defined by display device.
    kACInone          257     No *color*.

    An entity has no *color* from the time it is first instantiated until it is assigned one
    or added to a *database*.
  */
  void setColorIndex(
    OdInt16 colorIndex) { setColorIndex(&m_RGBM, colorIndex); }

  /** Description:
    Returns the *color* index (ACI) for this EntityColor object.

    Remarks:
    colorIndex returns one of the following:
    
    @table
    Name              Value   Description
    kACIbyBlock       0       ByBlock.
    kACIforeground    7       Foreground *color*.
    kACIbyLayer       256     ByLayer. 
    kACIRed           1       Red. 
    kACIYellow        2       Yellow. 
    kACIGreen         3       Green. 
    kACICyan          4       Cyan. 
    kACIBlue          5       Blue. 
    kACIMagenta       6       Magenta. 
    ..                8-255   Defined by display device.
    kACInone          257     No *color*.

    An entity has no *color* from the time it is first instantiated until it is assigned one
    or added to a *database*.
  */
  OdInt16 colorIndex() const { return OdCmEntityColor::colorIndex(&m_RGBM); }
  
  
  /** Description:
    Sets the *red*, *green*, and *blue* components for this EntityColor object.
    
    Arguments: 
    red (I) Red component.
    green (I) Green component.
    blue (I( Blue component.
  */
  void setRGB(
    OdUInt8 red, 
    OdUInt8 green, 
    OdUInt8 blue)
  {
    setRGB(&m_RGBM, red, green, blue);
  }    

  /** Description:
    Sets the *red* component for this EntityColor object.
    
    Arguments: 
    red (I) Red component.
  */
  void setRed(
    OdUInt8 red) { setRed(&m_RGBM, red); }

  /** Description:
    Sets the *green* component for this EntityColor object.
    
    Arguments: 
    green (I) Green component.
  */
  void setGreen(
    OdUInt8 green) { setGreen(&m_RGBM, green); }

  /** Description:
    Sets the *blue* component for this EntityColor object.
    
    Arguments: 
    blue (I) Blue component.
  */
  void setBlue(
    OdUInt8 blue) { setBlue(&m_RGBM, blue); }

  /** Description:
    Returns the *red* component for this EntityColor object.
  */
  OdUInt8 red() const { return red(&m_RGBM); }
  
  /** Description:
    Returns the *green* component for this EntityColor object.
  */
  OdUInt8 green() const { return green(&m_RGBM); }

  /** Description:
    Returns the *blue* component for this EntityColor object.
  */
  OdUInt8 blue() const { return blue(&m_RGBM); }
  
  // Method check
  
  /** Description:
    Returns true if and only if the *color* method for this EntityColor object is kByColor.
  */
  bool isByColor() const { return isByColor(&m_RGBM); }

  /** Description:
    Returns true if and only if the *color* method for this EntityColor object is kByLayer or the
    *color* method is kByACI and the ACI is kACIbyLayer.
  */
  bool isByLayer() const { return isByLayer(&m_RGBM); }

  
  /** Description:
    Returns true if and only if the *color* method for this EntityColor object is kByBlock or the
    *color* method is kByACI and the ACI is kACIbyBlock.
  */
  bool isByBlock() const { return isByBlock(&m_RGBM); }
   

  /** Description:
    Returns true if and only if the *color* method for this EntityColor object is kByACI.
  */
  bool isByACI() const { return isByACI(&m_RGBM); }

  /** Description:
    Returns true if and only if the *color* method for this EntityColor object is kForeground or the
    *color* method is kByACI and the ACI is kACIForeground.
  */
  bool isForeground() const { return isForeground(&m_RGBM); }

  /** Description:
    Returns true if and only if the *color* method for this EntityColor object is kNone or the
    *color* method is kByACI and the ACI is kACInone.
  */
  bool isNone() const  { return isNone(&m_RGBM); }
  
  /** Description:
    Returns the TrueColor value of this EntityColor object.
  */
  OdUInt32 trueColor() const;
  
  /** Description:
    Converts the *color* method from ACIcolorMethod to ColorMethod. 

    Remarks:
    Returns the ColorMethod value.
    
    Note:
    Assumes the current *color* method is ACIcolorMethod.
  */
  OdUInt8 trueColorMethod() const;

  /** Description:
    Sets the *color* of the calling Entity with this EntityColor object.
  */
  void setTrueColor();
  
  /** Description:
    Converts the *color* method from ACIcolorMethod to ColorMethod. 

    Note:
    Assumes the current *color* method is ACIcolorMethod.
  */
  void setTrueColorMethod();
  
  // static methods for reuse in other classes.

  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static void setColorMethod(
    RGBM* rgbm, 
    ColorMethod colorMethod);

  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static ColorMethod colorMethod(
    const RGBM* rgbm)
  {
    return OdCmEntityColor::ColorMethod((*rgbm >> 24) & 0xFF);
  }
  
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static void setColor(
    RGBM* rgbm, 
    OdUInt32 color);
    
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static OdUInt32 color(
    const RGBM* rgbm); 
  
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static void setColorIndex(
    RGBM* rgbm, 
    OdInt16 colorIndex);
    
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static OdInt16 colorIndex(
    const RGBM* rgbm);
  
    
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static void setRGB(
    RGBM* rgbm, 
    OdUInt8 red, 
    OdUInt8 green, 
    OdUInt8 blue)
  {
    setColorMethod(rgbm, kByColor);
    setRed(rgbm, red);
    setGreen(rgbm, green);
    setBlue(rgbm, blue);
  }
    
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static void setRed(
    RGBM* rgbm, 
    OdUInt8 red)
  {
    ODA_ASSERT(colorMethod(rgbm) == kByColor); 
    *rgbm &= 0xFF00FFFF;
    *rgbm |= RGBM(((OdUInt32)red << 16) & 0x00FF0000);
  }
      
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static void setGreen(
    RGBM* rgbm, 
    OdUInt8 green)
  {
    ODA_ASSERT(colorMethod(rgbm) == kByColor); 
    *rgbm &= 0xFFFF00FF;
    *rgbm |= RGBM(((OdUInt32)green << 8) & 0x0000FF00);
  }
    
    
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static void setBlue(
    RGBM* rgbm, 
    OdUInt8 blue)
  {
    ODA_ASSERT(colorMethod(rgbm) == kByColor); 
    *rgbm &= 0xFFFFFF00;
    *rgbm |= RGBM((blue) & 0x000000FF);
  }    
    
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static OdUInt8 red(
    const RGBM* rgbm)
  {
    ODA_ASSERT(colorMethod(rgbm) == kByColor); 
    return OdUInt8((*rgbm >> 16) & 0xFF);
  }
    
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static OdUInt8 green(
    const RGBM* rgbm)
  {
    ODA_ASSERT(colorMethod(rgbm) == kByColor); 
    return OdUInt8((*rgbm >> 8) & 0xFF);
  }
    
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static OdUInt8 blue(
    const RGBM* rgbm)
  {
    ODA_ASSERT(colorMethod(rgbm) == kByColor); 
    return OdUInt8(*rgbm & 0xFF);
  }
    
    
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static bool isByColor(
    const RGBM* rgbm) { return colorMethod(rgbm) == kByColor; }  
    
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static bool isByLayer(
    const RGBM* rgbm)
  {
    return (colorMethod(rgbm) == kByLayer || (colorMethod(rgbm) == kByACI && indirect(rgbm) == kACIbyLayer)); 
  }
    
    
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static bool isByBlock(
    const RGBM* rgbm)
  {
    return (colorMethod(rgbm) == kByBlock || (colorMethod(rgbm) == kByACI && indirect(rgbm) == kACIbyBlock)); 
  }    
    
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static bool isByACI(
    const RGBM* rgbm)
  {
    return colorMethod(rgbm) == kByACI;
  }
    
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static bool isForeground(
    const RGBM* rgbm)
  {
    return (colorMethod(rgbm) == kForeground || (colorMethod(rgbm) == kByACI && indirect(rgbm) == kACIforeground)); 
  }
    
    
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static bool isNone(
    const RGBM* rgbm)
  {
    return (colorMethod(rgbm) == kNone || (colorMethod(rgbm) == kByACI && indirect(rgbm) == kACInone)); 
  }
    
    
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static OdUInt32 trueColor(
    const RGBM* rgbm);
    
  /**
    Arguments:
    aciColorMethod (I) ACIcolorMethod value.
  */
  static OdUInt8 trueColorMethod(
    OdInt16 aciColorMethod);
    
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static void setTrueColor(
    RGBM* rgbm);
    
  /**
    Arguments:
    rgbm (I) Pointer to the m_RGBM member of the EntityColor object.
  */
  static void setTrueColorMethod(
    RGBM* rgbm);
  
  /** Description:
    Returns the *color* value and *color* method corresponding to the specified *color* index (ACI).

    Remarks:
    *Color* value and *color* method are packed as follows:
    
    @table
    Byte      Description
    0         Blue component.
    1         Green component.
    2         Red component.
    high      *Color* Method.

    See Also:
    OdCmEntityColor::ColorMethod
  */
  static OdUInt32 lookUpRGB(
    OdUInt8 colorIndex);
  
  /** Description:
    Returns the *color* index (ACI) corresponding to the specified *red*, *green*, and *blue* components.

    Arguments:
    red (I) Red component.
    green (I) Green component.
    blue (I( Blue component.
  */
  static OdUInt8 lookUpACI(
    OdUInt8 red, 
    OdUInt8 green, 
    OdUInt8 blue); 
  
protected:
  /* {Secret} */
  static OdInt16 indirect(
    const RGBM* rgbm) 
  {
    ODA_ASSERT(colorMethod(rgbm) != kByColor); 
    return OdInt16((*rgbm) & 0x0000FFFF);
  }

  /* {Secret} */
  static void setIndirect(
    RGBM* rgbm, 
    OdInt16 indirect)
  {
    ODA_ASSERT(colorMethod(rgbm) != kByColor); 
    *rgbm = ((*rgbm & 0xFF000000) | (OdUInt32(indirect) & 0x0000FFFF));
  }
      
  /* {Secret} */
  OdInt16 indirect() const { return indirect(&m_RGBM); }

  /* {Secret} */
  void setIndirect(OdInt16 indirect) { setIndirect(&m_RGBM, indirect); }

  RGBM m_RGBM;
  
public:
  // The Look Up Table (LUT) provides a mapping between ACI colors and their RGB representations.
  static const OdUInt8 mLUT[256][3]; 
};


/** Description:
    This class is a virtual interface for classes that represent colors in DWGdirect as RGB, ACI, or named colors.
    
    Remarks:
    
    See Also:
    OdCmEntityColor
    
    {group:OdCm_Classes}
*/
class FIRSTDLL_EXPORT OdCmColorBase
{
public:
  ODRX_HEAP_OPERATORS();
  virtual ~OdCmColorBase() {}

  /** Description
    Returns the *color* method for this object.

    Remarks:
    colorMethod returns one of the following:
     
    @table
    Name         Value    Description
    kByLayer     0xC0     *Color* of the layer on which it resides.
    kByBlock     0xC1     *Color* of the block reference in which it is contained.
    kByColor     0xC2     *Color* specified by RGB value.
    kByACI       0xC3     *Color* specified by an index (ACI) into a *color* table.
    kForeground  0xC5     *Color* of editor foreground.
    kNone        0xC7     No *color*.

    An entity has no *color* from the time it is first instantiated until it is assigned one
    or added to a *database*.
    
    Note:
    Use of this method by third-party applications is neither supported nor recommended.
  */
  virtual OdCmEntityColor::ColorMethod colorMethod() const = 0;
  
  /** Description
    Sets the *color* method for this object.

    Arguments:
    colorMethod (I) *Color* method.

    Remarks:
    colorMethod will be one of the following: 
    
    @table
    Name         Value    Description
    kByLayer     0xC0     *Color* of the layer on which it resides.
    kByBlock     0xC1     *Color* of the block reference in which it is contained.
    kByColor     0xC2     *Color* specified by RGB value.
    kByACI       0xC3     *Color* specified by an index (ACI) into a *color* table.
    kForeground  0xC5     *Color* of editor foreground.
    kNone        0xC7     No *color*.

    An entity has no *color* from the time it is first instantiated until it is assigned one
    or added to a *database*.

    Note:
    Use of this method by third-party applications is neither supported nor recommended.
  */
  virtual void  setColorMethod(
    OdCmEntityColor::ColorMethod colorMethod) = 0;

  /** Description:
    Returns true if and only if the *color* method for this object is kByColor.
  */
  virtual bool isByColor() const = 0;
    
  /** Description:
    Returns true if and only if the *color* method for this object is kByLayer or the
    *color* method is kByACI and the ACI is kACIbyLayer.
  */
  virtual bool isByLayer() const = 0;
  
  /** Description:
    Returns true if and only if the *color* method for this object is kByBlock or the
    *color* method is kByACI and the ACI is kACIbyBlock.
  */
  virtual bool isByBlock() const = 0;

  /** Description:
    Returns true if and only if the *color* method for this object is kByACI.
  */
  virtual bool isByACI() const = 0;

  /** Description:
    Returns true if and only if the *color* method for this object is kForeground or the
    *color* method is kByACI and the ACI is kACIForeground.
  */
  virtual bool isForeground()   const = 0;

  /** Description:
    Returns the *color* value and *color* method for this object.

    Remarks:
    *Color* value and *color* method are packed as follows:
    
    @table
    Byte      Description
    0         Blue component.
    1         Green component.
    2         Red component.
    high      *Color* Method.
    
    See Also:
    OdCmEntityColor::ColorMethod
    
  */
  virtual OdUInt32  color() const = 0;
  
  /** Description:
    Sets the *color* value and *color* method for this object.

    Arguments:
    color (I) *Color* value and *color* method.
    
    Remarks:
    *Color* value and *color* method are packed as follows:
    
    @table
    Byte      Description
    0         Blue component.
    1         Green component.
    2         Red component.
    high      *Color* Method.
    
    See Also:
    OdCmEntityColor::ColorMethod
  */
  virtual void setColor(
    OdUInt32 color) = 0;

  /** Description:
    Sets the *red*, *green*, and *blue* components for this object.

    Arguments: 
    red (I) Red component.
    green (I) Green component.
    blue (I( Blue component.
  */
  virtual void setRGB(
    OdUInt8 red, 
    OdUInt8 green, 
    OdUInt8 blue) = 0;
                           
  /** Description:
    Sets the *red* component for this object.
    
    Arguments: 
    red (I) Red component.
  */
  virtual void setRed(
    OdUInt8 red) = 0;
    
  /** Description:
    Sets the *green* component for this object.
    
    Arguments: 
    green (I) Green component.
  */
  virtual void setGreen(
    OdUInt8 green) = 0;
    
  /** Description:
    Sets the *blue* component for this object.
    
    Arguments: 
    blue (I) Blue component.
  */
  virtual void setBlue(
    OdUInt8 blue) = 0;
    
  /** Description:
    Returns the *red* component for this object.
  */
  virtual OdUInt8 red() const = 0;
   /** Description:
    Returns the *green* component for this object.
  */
  virtual OdUInt8 green() const = 0;

  /** Description:
    Returns the *blue* component for this object.
  */
  virtual OdUInt8 blue() const = 0;

  /** Description:
    Returns the *color* index (ACI) for this object.

    Remarks:
    colorIndex returns one of the following:
    
    @table
    Name              Value   Description
    kACIbyBlock       0       ByBlock.
    kACIforeground    7       Foreground *color*.
    kACIbyLayer       256     ByLayer. 
    kACIRed           1       Red. 
    kACIYellow        2       Yellow. 
    kACIGreen         3       Green. 
    kACICyan          4       Cyan. 
    kACIBlue          5       Blue. 
    kACIMagenta       6       Magenta. 
    ..                8-255   Defined by display device.
    kACInone          257     No *color*.

    An entity has no *color* from the time it is first instantiated until it is assigned one
    or added to a *database*.
  */
  virtual OdUInt16  colorIndex() const = 0;

  /** Description:
    Sets the *color* index (ACI) for this object.

    Arguments:
    colorIndex (I) *Color* index.
    
    Remarks:
    colorIndex will be one of the following:
    
    @table
    Name              Value   Description
    kACIbyBlock       0       ByBlock.
    kACIforeground    7       Foreground *color*.
    kACIbyLayer       256     ByLayer. 
    kACIRed           1       Red. 
    kACIYellow        2       Yellow. 
    kACIGreen         3       Green. 
    kACICyan          4       Cyan. 
    kACIBlue          5       Blue. 
    kACIMagenta       6       Magenta. 
    ..                8-255   Defined by display device.
    kACInone          257     No *color*.

    An entity has no *color* from the time it is first instantiated until it is assigned one
    or added to a *database*.
  */
  virtual void setColorIndex(
    OdUInt16 colorIndex) = 0;

  /** Description:
    Sets the *color* name and book name for this object.
    
    Arguments:
    colorName (I) *Color* name.
    bookName (I) Book name.
  */
  virtual void  setNames(
    const OdString& colorName,
    const OdString& bookName = OdString()) = 0;
    
  /** Description:
    Returns the *color* name for this object.
  */
  virtual OdString colorName() const = 0;

  /** Description:
    Returns the book name for this object.
  */
  virtual OdString bookName () const = 0;
  
  /** Description:
    Returns the display *color* name for this object.
    
    Remarks:
    For named colors, this is the same as colorName(). For unnamed colors, it is an 'appropriate' name.
  */
  virtual OdString colorNameForDisplay() = 0;
};

class OdDbObject;
class OdDbFiler;

/** Description:
    This class implements Color objects, which represent the colors of OdDbEntity objects, including RGB and ACI.

    {group:OdCm_Classes}
*/
class TOOLKIT_EXPORT OdCmColor : public OdCmColorBase
{
public:
  OdCmColor();
  OdCmColor(
    const OdCmColor&);
  OdCmColor(
    const OdCmColorBase&);
  OdCmColor(
    OdCmEntityColor::ColorMethod colorMethod);
  OdCmColor& operator=(
    const OdCmColor& inputColor);
  OdCmColor& operator=(
    const OdCmColorBase& inputColor);
  ~OdCmColor();

  bool operator ==(
    const OdCmColor& color) const;
  bool operator !=(
    const OdCmColor& color) const;
  bool operator ==(
    const OdCmColorBase& color) const;
  bool operator !=(
    const OdCmColorBase& color) const;
  
  /** Description:
    Returns the description string of this Color object.
  */
  OdString getDescription() const;

  /** Description:
    Returns the explanation string of this Color object.
  */
 OdString getExplanation() const;

  virtual OdCmEntityColor::ColorMethod colorMethod() const;
  virtual void setColorMethod(
    OdCmEntityColor::ColorMethod colorMethod);

  virtual bool isByColor() const;
  virtual bool isByLayer() const;
  virtual bool isByBlock() const;
  virtual bool isByACI() const;

  virtual bool isForeground() const;

  /** Description:
    Returns true if and only if the *color* method for this object is kNone or the
    *color* method is kByACI and the ACI is kACInone.
  */
  bool isNone() const;

  virtual OdUInt32 color() const;
  virtual void setColor(
    OdUInt32 color);

  virtual void setRGB(
    OdUInt8 red, 
    OdUInt8 green, 
    OdUInt8 blue);
  virtual void setRed(
    OdUInt8 red);
  virtual void setGreen(
    OdUInt8 green);
  virtual void setBlue(
    OdUInt8 blue);
  virtual OdUInt8 red() const;
  virtual OdUInt8 green() const;
  virtual OdUInt8 blue () const;

  virtual OdUInt16 colorIndex() const;
  virtual void setColorIndex(
    OdUInt16 colorIndex);

  virtual void setNames(
    const OdString& colorName,
    const OdString& bookName = OdString());
  virtual OdString colorName() const;
  virtual OdString bookName () const;
  virtual OdString colorNameForDisplay();

  /** Description:
    Returns the OdCmEntityColor settings of this object.
  */
  OdCmEntityColor entityColor() const;
  /** Description:
    Returns a dictionary key based on the *color* name and book name of this object.
   
    Remarks:
    OdCmColor objects with *color* names can be stored in the form of an OdDbColor in a dictionary.
    This function returns the key for that dictionary.
  */
  OdString getDictionaryKey() const;
  
  /** Description:
  
    Extracts the *color* name and book name from the specified dictionary key, and
    calls setNames() with them.
    Arguments:
    dictionaryKey (I) Dictionary key.   
  */
  
  /** { Secret } */
  void setNamesFromDictionaryKey(
    const OdString& sDictionaryKey);

  /** 
    Description:
    Reads the DWG format data of this object from the specified file.
       
    Arguments:   
    pFiler (I) Pointer to the filer from which the data are to be read.
    
  */
  void dwgIn(
    OdDbDwgFiler* pFiler);

  /** 
    Description:
    Writes the DWG format data of this object to the specified filer. 
    
    Arguments:
    pFiler (I) Pointer to the filer to which the data are to be written.
  */
  void dwgOut(
    OdDbDwgFiler* pFiler) const;

  /** 
    Description:
    Reads the DXF format data of this object from the specified filer. 
    
    Arguments:
    pFiler (I) Pointer to the filer from which the data are to be read.
    groupCodeOffset (I) Group code offset.
  */
  void dxfIn(
    OdDbDxfFiler* pFiler, 
    int groupCodeOffset = 0);

  /** 
    Description:
    Writes the DXF format data of this object to the specified filer. 
    
    Arguments:
    pFiler (I) Pointer to the filer to which the data are to be written.
    groupCodeOffset (I) Group code offset.
  */
  void dxfOut(
    OdDbDxfFiler* pFiler, 
    int groupCodeOffset = 0) const;

  /** 
    Description:
    Perform an *audit* operation on this object.

    Arguments:
    pAuditInfo (I) Pointer to an OdDbAuditInfo object.
    
    Remarks:
    When overriding this function for a custom class, first call OdCmColorBase::audit(pAuditInfo) 
    to validate the *audit* operation.
  */
  void audit(
    OdDbAuditInfo* pAuditInfo);

  // For internal use
  /** { Secret } */
  void     dwgInAsTrueColor (
    OdDbDwgFiler* pFiler);
  /** { Secret } */
  void     dwgOutAsTrueColor(
    OdDbDwgFiler* pFiler) const;

  static const OdUInt16 MaxColorIndex;

private:
  OdCmEntityColor::RGBM   m_RGBM;
  OdString                m_colorName;
  OdString                m_bookName;
};




/** Description:
    This class implements Transparency objects, which provide *transparency* information about OdGiDrawable objects.
    {group:OdCm_Classes}
*/
class OdCmTransparency 
{
public:
  ODRX_HEAP_OPERATORS();
  
  enum transparencyMethod 
  {
    kByLayer    = 0, // Use the setting for the layer.
    kByBlock    = 1, // Use the setting for the block.
    kByAlpha    = 2, // Use the alpha value in this object.     
    kErrorValue = 3  //  
  };
  
  /**
    Remarks:
    The default method is kByLayer.
  */
  OdCmTransparency() { setMethod(kByLayer); }
  OdCmTransparency(
    const OdCmTransparency& transparency) { m_AM = transparency.m_AM; }
  ~OdCmTransparency() {}
  
  OdCmTransparency& operator=(
    const OdCmTransparency& transparency) { m_AM = transparency.m_AM; return *this; }

  bool operator==(
    const OdCmTransparency& transparency) const { return (m_AM == transparency.m_AM); }
  bool operator!=(
    const OdCmTransparency& transparency) const { return (m_AM != transparency.m_AM); }
  
  /** Description:
    Sets the alpha, and sets the *transparency* method to kByALpha, for this Transparency object.
    
    Remarks:
    alpha == 0 means totally transparent. alpha == 255 meahs totally opaque.
    Arguments:
    alpha (I) Alpha.
  */
  void setAlpha(
    OdUInt8 alpha) 
  { 
    m_AM = ((OdUInt32)kByAlpha << 24) | alpha; 
  }

  /** Description:
    Returns the *transparency* method for this Transparency object.
    
    Remarks:
    transparencyMethod will return one of the following:
    
    @table
    Name        Value   Description
    kByLayer    0       Use the setting for the layer.
    kByBlock    1       Use the setting for the block.
    kByAlpha    2       Use the alpha value in this object.     
  */
  transparencyMethod method() const 
  { 
    return transparencyMethod(OdUInt8(m_AM >> 24)); 
  }
  
  /** Description:
    Sets the *transparency* method for this Transparency object.
    
    Remarks:
    method (I) Transparency method.
    
    method will be one of the following:
    
    @table
    Name        Value   Description
    kByLayer    0       Use the setting for the layer.
    kByBlock    1       Use the setting for the block.
    kByAlpha    2       Use the alpha value in this object.     
  */
  void setMethod(
    transparencyMethod method) 
  { 
    m_AM = (OdUInt32)method << 24; 
  }
  
  OdUInt8 alpha() const 
  { 
    ODA_ASSERT(method() == kByAlpha); 
    return OdUInt8(m_AM); 
  }

  /** Description:
    Returns true if and only if the *transparency* method for this Transparency object is kByAlpha.
  */
  bool isByAlpha() const { return (method() == kByAlpha); }

  /** Description:
    Returns true if and only if the *transparency* method for this Transparency object is kByBlock.
  */
  bool isByBlock() const { return (method() == kByBlock); }
  
  /** Description:
    Returns true if and only if the *transparency* method for this Transparency object is kByLayer.
  */
  bool isByLayer() const { return (method() == kByLayer); }
  
  /** Description:
    Returns true if and only if the *transparency* method for this Transparency object is kByALpha and
    alpha == 0.
  */
  bool isClear() const   { return (method() == kByAlpha) && (alpha() == 0);}
  
  /** Description:
    Returns true if and only if the *transparency* method for this Transparency object is kByALpha and
    alpha == 255.
  */
  bool isSolid() const   { return (method() == kByAlpha) && (alpha() == 255);}
  
  /** Description:
    Returns the transparency converted to an integer.
    
    Remarks:
    This is useful when storing transparency values in a DWG file.
  */
  OdUInt32  serializeOut() const { return m_AM; }
  /** Description:
    Converts the specified integer to a *transparency* value, and sets the value of this Transparency object from it.

    Arguments:
    transparency (I) Transparency.
    
    Remarks:
    This is useful when reading transparency values from a DWG file.
  */
  void  serializeIn(
    OdUInt32 transparency) { m_AM = transparency; }
  
private: 
  OdUInt32 m_AM;
};

#include "DD_PackPop.h"

#endif // ODA_CM_COLOR
