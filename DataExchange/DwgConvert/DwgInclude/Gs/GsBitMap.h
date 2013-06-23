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



#ifndef _OD_GSBITMAP_H_
#define _OD_GSBITMAP_H_

#include "RxObject.h"
#include "Gs.h"
#include "OdStreamBuf.h"
#include "SmartPtr.h"


/** Description:

    {group:OdGs_Classes} 
*/
class OdGsPalette : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdGsPalette);
  OdGsPalette(){}

  virtual OdUInt32 numColors() const = 0;
  virtual void setNumColors(OdUInt32 nColors) = 0;
  virtual void setColorAt(OdUInt32 nIndex, OdUInt8 blue, OdUInt8 green, OdUInt8 red, OdUInt8 alpha = 0) = 0;
  virtual void colorAt(OdUInt32 nIndex, OdUInt8& blue, OdUInt8& green, OdUInt8& red, OdUInt8* pAlpha = 0) const = 0;
  virtual void setColors(OdUInt32 nColors, const ODCOLORREF* pColors);
  virtual void getColors(OdUInt32 nColors, ODCOLORREF* pColors) const;
};

typedef OdSmartPtr<OdGsPalette> OdGsPalettePtr;


/** Description:

    {group:OdGs_Classes} 
*/
class OdGsBitMap : public OdRxObject
{
protected:
  OdGsBitMap() {}
public:
  ODRX_DECLARE_MEMBERS(OdGsBitMap);

  virtual OdUInt32 width() const = 0;
  virtual OdUInt32 height() const = 0;
  virtual OdUInt8 bitPerPixel() const = 0;
  virtual void create(OdUInt32 width, OdUInt32 height, OdUInt8 bitCount) = 0;

  virtual OdUInt32 bitDataSize() const;
  virtual OdUInt32 bytePerLine() const;
  virtual const OdUInt8* bits() const = 0;
  virtual void setBits(const OdUInt8* pData, OdUInt32 nSize) = 0;

  virtual OdGsPalette* palette() = 0;
  virtual const OdGsPalette* palette() const = 0;
};

typedef OdSmartPtr<OdGsBitMap> OdGsBitMapPtr;

#endif //_OD_GSBITMAP_H_

