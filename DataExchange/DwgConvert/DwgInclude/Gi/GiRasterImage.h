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



#ifndef _OD_ODGIRASTERIMAGE_H_
#define _OD_ODGIRASTERIMAGE_H_

#include "RxObject.h"
#include "OdPlatform.h"
#include "Gi/Gi.h"

class OdGeVector2d;
class OdStreamBuf;

#include "DD_PackPush.h"

/** Description:
    Represents a raster image within the DWGdirect vectorization framework.

    {group:OdGi_Classes} 
*/
class FIRSTDLL_EXPORT OdGiRasterImage : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdGiRasterImage);

  enum Units
  {
    kNone         = 0,
    kMillimeter   = 1,
    kCentimeter   = 2,
    kMeter        = 3,
    kKilometer    = 4,
    kInch         = 5,
    kFoot         = 6,
    kYard         = 7,
    kMile         = 8,
    kMicroinches  = 9,
    kMils         = 10,
    kAngstroms    = 11,
    kNanometers   = 12,
    kMicrons      = 13,
    kDecimeters   = 14,
    kDekameters   = 15,
    kHectometers  = 16,
    kGigameters   = 17,
    kAstronomical = 18,
    kLightYears   = 19,
    kParsecs      = 20
  };

  /** Description:
      Returns the image width in pixels.
  */
  virtual OdUInt32 pixelWidth() const = 0;

  /** Description:
      Returns the image height in pixels.
  */
  virtual OdUInt32 pixelHeight() const = 0;

  /** Description:
      Returns the default image resolution in pixels per unit.

      Arguments:
        xPelsPerUnit (O) Receives the pixels per unit value (x direction).
        yPelsPerUnit (O) Receives the pixels per unit value (y direction).

      Remarks:
      If the returned value is kNone, then xPelsPerUnit and yPelsPerUnit are not set.
  */ 
  virtual Units defaultResolution(double& xPelsPerUnit, double& yPelsPerUnit) const;

  /** Description:
      Retrieves the number of bits per pixel used for colors on the destination device or buffer.
  */
  virtual int colorDepth() const = 0;

  /** Description:
      Returns the number of colors in the palette of the image.
  */
  virtual OdUInt32 numColors() const = 0;

  /** Description:
      Returns the color for the specified index, from the palette of the image.
  */
  virtual ODCOLORREF color(OdUInt32 nIndex) const = 0;

  /** Description:
      Returns the size (in bytes) the the palette used by this image.
  */
  virtual OdUInt32 paletteDataSize() const = 0;

  /** Description:
      Returns the palette of the image in BMP format.
  */
  virtual void paletteData(OdUInt8* pBytes) const = 0;

  /** Description:
      Returns the number of bytes between the beginning of scan line N and
      the beginning of scan line N+1 (taking into account any padding that is added to the 
      end of the scan line).
  */
  virtual OdUInt32 scanLineSize() const = 0;

  /** Description:
      Retrieves a specified set of scanlines from this image object.

      Arguments:
        pBytes (O) Receives the specified scan line data (caller must allocate sufficient memory).
        index (I) Index of first scanline to retrieve.
        numLines (I) Number of scanlines to retrieve.

      Remarks:

      o The number of accessible scanlines is equal to value 'height' (see pixelHeight() call).
      o The number of accessible bytes in a scanline is equal to the value returned by scanLineSize.
      o The scanline returned by index 0 is the first scanline in the image.
      o The scanline returned by index (height - 1) is the last scanline in the image.
  */
  virtual void scanLines(OdUInt8* pBytes, OdUInt32 index, OdUInt32 numLines = 1) const = 0;

  /** Description:
      Returns pointer to the pixel data for this image, in BMP format.
      Note that implementation of this function is optional--NULL can be returned 
      if it is inconvenient to implement this function, and the caller must take 
      into account that the return value can be NULL.
  */
  virtual const OdUInt8* scanLines() const = 0;

  /** Description:
      Applies brightness, contrast, and fade values to the pixels in this image.
  */
  virtual void applyBCF(double brightness, double contrast, double fade) = 0;

  /** Description:
      Applies a palette transformation to bitonal images (does nothing for images
      that are not bitonal).
  */
  virtual void applyBitonalPaletteTransform(ODCOLORREF traitsColor, ODCOLORREF bgColor) = 0;

  /** Description:
      Returns the size of a single scan line for this image, in bytes.
  */
  static OdUInt32 calcScanLineSize(OdUInt32 pixelWidth, int colorDepth);
};

typedef OdSmartPtr<OdGiRasterImage> OdGiRasterImagePtr;

#include "DD_PackPop.h"

#endif //#ifndef _OD_ODGIRASTERIMAGE_H_

