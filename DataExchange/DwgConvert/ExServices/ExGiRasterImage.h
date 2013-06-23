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



#ifndef _OD_ODEXGIRASTERIMAGE_H_
#define _OD_ODEXGIRASTERIMAGE_H_

#include "OdBinaryData.h"
#include "Gi/GiRasterImage.h"
#include "FlatMemStream.h"
#include "OdPlatform.h"
#include "RxRasterServices.h"

class OdExGiRasterImage;
typedef OdSmartPtr<OdExGiRasterImage> OdExGiRasterImagePtr;

class OdExGiRasterImage: public OdGiRasterImage
{
public:
  OdRxObjectPtr clone() const
  {
    OdSmartPtr<OdExGiRasterImage> pRes = OdRxObjectImpl<OdExGiRasterImage>::createObject();
    pRes->m_bits = m_bits;
    pRes->m_palette = m_palette;
    pRes->m_header = m_header;
    return (OdRxObject*)pRes;
  }

  struct Header
  {
    Header(OdUInt32 width, OdUInt32 height, OdUInt16 bitPerPixel)
      : m_width(width), m_height(height)
      , m_bitPerPixel(bitPerPixel)
      , m_resUnits(OdGiRasterImage::kNone)
    {}
    Header()
      : m_width(0), m_height(0)
      , m_bitPerPixel(0)
      , m_resUnits(OdGiRasterImage::kNone)
    {}
    
    OdUInt32  m_width;
    OdUInt32  m_height;
    OdUInt16   m_bitPerPixel;
    double    m_xPelsPerUnit,
              m_yPelsPerUnit;
    OdGiRasterImage::Units 
              m_resUnits;
  };
  
  class Palette
  {
    OdBinaryData m_colorsData;
  public:
    const OdUInt8* getData() const { return m_colorsData.getPtr(); }
    OdBinaryData& data() { return m_colorsData; }
    const OdBinaryData& getBinData() const { return m_colorsData; }
    OdUInt32 numColors() const { return m_colorsData.size() / 4; }
    void setNumColors(OdUInt32 nColors) { m_colorsData.resize(nColors * 4); }
    void setColorAt(OdUInt32 nIndex, OdUInt8 blue, OdUInt8 green, OdUInt8 red, OdUInt8 alpha = 0)
    {
      ODA_ASSERT(nIndex < m_colorsData.size());
      nIndex *= 4;
      m_colorsData[nIndex]     = blue;
      m_colorsData[nIndex + 1] = green;
      m_colorsData[nIndex + 2] = red;
      m_colorsData[nIndex + 3] = alpha;
    }
    
    void colorAt(OdUInt32 nIndex, OdUInt8& blue, OdUInt8& green, OdUInt8& red, OdUInt8* pAlpha = 0) const
    {
      ODA_ASSERT(nIndex < m_colorsData.size());
      nIndex *= 4;
      blue    = m_colorsData[nIndex];
      green   = m_colorsData[nIndex + 1];
      red     = m_colorsData[nIndex + 2];
      if(pAlpha)
      {
        *pAlpha = m_colorsData[nIndex + 3];
      }
    }
  };
  
  private:
    Header        m_header;
    OdBinaryData  m_bits;
    Palette       m_palette;
  public:
    OdExGiRasterImage(OdUInt32 width, OdUInt32 height, OdUInt8 bitCount);
    OdExGiRasterImage();
    
    static OdUInt32 bitDataSize(OdUInt32 width, OdUInt32 height, OdUInt16 bitCount)
    {
      return ((width * bitCount +31) & ~31) /8 * height;
    }
    
    OdUInt32 bitDataSize()
    {
      return OdExGiRasterImage::bitDataSize(m_header.m_width, m_header.m_height, 
        m_header.m_bitPerPixel);
    }
    
    OdBinaryData& bits() { return m_bits; }
    const OdBinaryData& getBits() const { return m_bits; }
    const OdUInt8* getScanLines(OdUInt32& numBytes) const
    {
      numBytes = m_bits.size();
      return m_bits.asArrayPtr();
    }
    
    void setBits(const OdBinaryData& data)
    {
      ODA_ASSERT(data.size() <= bitDataSize());
      m_bits = data;
    }
    
    void setBits(const OdUInt8* pData, OdUInt32 nSize)
    {
      ODA_ASSERT(nSize <= bitDataSize());
      m_bits.resize(nSize);
      ::memcpy(m_bits.asArrayPtr(), pData, nSize);
    }
    
    void setMetrics(OdUInt32 width, OdUInt32 height, OdUInt16 bitCount)
    {
      m_header.m_width = width;
      m_header.m_height = height;
      m_header.m_bitPerPixel = bitCount;
    }
    
    void setDefaultResolution(OdGiRasterImage::Units units, double xPelsPerUnit, double yPelsPerUnit)
    {
      m_header.m_resUnits = units;
      m_header.m_xPelsPerUnit = xPelsPerUnit;
      m_header.m_yPelsPerUnit = yPelsPerUnit;
    }
    
    void setPalNumColors(OdUInt32 nColors)
    {
      m_palette.setNumColors(nColors);
    }
    
    OdUInt32 getPalNumColors() const
    {
      return m_palette.numColors();
    }
    
    const Palette& getPalette() const
    {
      return m_palette;
    }

    OdUInt32 paletteDataSize() const
    {
      return m_palette.getBinData().size();
    }

    /** Description:
        Returns the palette of the image in BMP format.
    */
    void paletteData(OdUInt8* pBytes) const
    {
      const OdBinaryData& palData = m_palette.getBinData();
      memcpy(pBytes, palData.asArrayPtr(), palData.size());
    }

    Palette& palette()
    {
      return m_palette;
    }
    
    void setPalColorAt(OdUInt32 nIndex, OdUInt8 blue, OdUInt8 green, OdUInt8 red, OdUInt8 alpha = 0)
    {
      m_palette.setColorAt(nIndex, blue, green, red, alpha);
    }
    
    void getPalColorAt(OdUInt32 nIndex, OdUInt8& blue, OdUInt8& green, OdUInt8& red, OdUInt8* pAlpha = 0) const
    {
      m_palette.colorAt(nIndex, blue, green, red, pAlpha);
    }
    
    // Gets the color(RGB) in specified point of raster image
    void getColorAt(OdUInt32 x, OdUInt32 y, OdUInt8& blue, OdUInt8& green, OdUInt8& red) const;
    
    // virtual overrides
    void copyFrom(const OdRxObject* pOtherObj);
    OdUInt32 pixelWidth() const;
    OdUInt32 pixelHeight() const;
    int colorDepth() const;
    OdGiRasterImage::Units defaultResolution(double& xPelsPerUnit, double& yPelsPerUnit) const;
    
    // applying Brightness, Contrast, Fade to the pixel
    // Yes, I know that it is not very good to have such member functions
    // it will be moved out from here in near future.
    
    /*
    void applyBrightness(OdUInt8 oldB, OdUInt8 oldG, OdUInt8 oldR, 
      OdUInt8& newB, OdUInt8& newG, OdUInt8& newR,
      double brightness);
    
    void applyContrast(OdUInt8 oldB, OdUInt8 oldG, OdUInt8 oldR, 
      OdUInt8& newB, OdUInt8& newG, OdUInt8& newR,
      double contrast);
    
    void applyFade(OdUInt8 oldB, OdUInt8 oldG, OdUInt8 oldR, 
      OdUInt8& newB, OdUInt8& newG, OdUInt8& newR,
      double fade);*/

    void applyBCF(double brightness, double contrast, double fade);

    /** Returns bitmap info header
    */
    // void getBitmapInfoHeader(BITMAPINFOHEADER& bmih) const;

    // Returns the number of colors in the palette of the image
    OdUInt32 numColors() const;

    // Returns the color by index from the palette of the image
    virtual ODCOLORREF color(OdUInt32 nIndex) const;

    /** Description:
        The number of bytes between the beginning of scan line N and
        the beginning of scan line N+1
    */
    OdUInt32 scanLineSize() const;

    /** Description:
        Returns pointer on the scanline of the image by index without any stuff.
    
        Remarks:
        The number of accessible scanlines is equal to value 'height' (see pixelHeight() call).
        The number of accessible bytes  in the scanline is equal to scanLineSize() returning value.
        The scanline gotten by an index 0 is the first.
        The scanline gotten by an index (height - 1) is the last.
    */
    void scanLines(OdUInt8* pBytes, OdUInt32 index, OdUInt32 numLines = 1) const;

    /** Description:
        Returns pointer to image's pixels in bmp format.
        Note that it is optional to have implementation of this function
        (to prevent dummy copying of pixels data). You can return NULL in your
        implementation if it is inconvenient in some case - any functionality
        uses this interface must take it into account.
    */
    virtual const OdUInt8* scanLines() const;

    /** Description:
        applying palette transformation to bitanal images.
        If image isn't bitonal - does nothing
    */
    void applyBitonalPaletteTransform(ODCOLORREF traitsColor, ODCOLORREF bgColor);
};

class ExRasterModule : public OdRxRasterServices
{
public:
  /** Description:
      Loads raster image. Returned pointer will be passed to OdGiViewportGeometry::rasterImageDc()
  */
  OdGiRasterImagePtr loadRasterImage(const OdString &fileName);
  OdGiRasterImagePtr loadRasterImage(OdStreamBuf *pBuf);

  /** Description:
      Save raster image to file
      file type is recognized by extension 
      transparent_color is index in palette of input image ( if output format 
      supports transparency )
  */
  bool saveRasterImage(const OdGiRasterImage*, const OdChar* path, int transparent_color = -1);

  /** Description:
      The user override of this function should register any custom objects defined in the 
      custom application, using the OdRxObject::rxInit function.  It should also register
      custom commands defined in the module.
  */
  void initApp();

  /** Description:
      The user override of this function should unregister any custom objects defined in the
      custom application, using the OdRxObject::rxUninit function.  It should also
      remove any custom commands that were registered in the initApp function.
  */
  void uninitApp();
};

#endif //#ifndef _OD_ODEXGIRASTERIMAGE_H_
