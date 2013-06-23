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



// OdFont.h: interface for the OdFont class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_ODFONT_H__0B76813A_DCFA_450E_8591_B6C6F1ED76EC__INCLUDED_)
#define AFX_ODFONT_H__0B76813A_DCFA_450E_8591_B6C6F1ED76EC__INCLUDED_

#include "RxObject.h"
#include "OdArray.h"
//#include "OdCodePage.h"

// For memset below
#include <memory.h>

class OdGiCommonDraw;
class OdGiConveyorGeometry;
class OdGePoint2d;
class OdStreamBuf;

#include "DD_PackPush.h"

typedef enum {
	kFontTypeUnknown,
	kFontTypeShx,
	kFontTypeTrueType,
  kFontTypeShape,
  kFontTypeBig
} OdFontType;

/** Description:

    {group:Structs}
*/
struct FIRSTDLL_EXPORT OdCharacterProperties
{
  OdCharacterProperties()
  { ::memset(this, 0, sizeof(*this)); }

  bool  bUnderlined;
  bool  bOverlined;
  bool  bLastChar;
  bool  bInBigFont;
  bool  bAsian;
  bool  bValid;
};

/** Description:

    {group:Structs}
*/
struct FIRSTDLL_EXPORT OdTextProperties
{
  OdUInt8	m_flags;
  double  m_trackingPercent;
  enum
  {
    kNormalText   = 0x01,
    kVerticalText = 0x02,
    kUnderlined   = 0x04,
    kOverlined    = 0x08,
    kLastChar     = 0x10,
    kInBigFont    = 0x20,
    kInclPenups   = 0x40
  };
public:
  OdTextProperties() : m_flags(0), m_trackingPercent(0.0) {}
  bool isNormalText() const { return GETBIT(m_flags, kNormalText); }
  void setNormalText(bool val) { SETBIT(m_flags, kNormalText, val); }
  bool isVerticalText() const { return GETBIT(m_flags, kVerticalText); }
  void setVerticalText(bool val) { SETBIT(m_flags, kVerticalText, val); }
  bool isUnderlined() const { return GETBIT(m_flags, kUnderlined); }
  void setUnderlined(bool val) { SETBIT(m_flags, kUnderlined, val); }
  bool isOverlined() const { return GETBIT(m_flags, kOverlined); }
  void setOverlined(bool val) { SETBIT(m_flags, kOverlined, val); }
  bool isLastChar() const { return GETBIT(m_flags, kLastChar); }
  void setLastChar(bool val) { SETBIT(m_flags, kLastChar, val); }
  bool isInBigFont() const { return GETBIT(m_flags, kInBigFont); }
  void setInBigFont(bool val) { SETBIT(m_flags, kInBigFont, val); }
  bool isIncludePenups() const { return GETBIT(m_flags, kInclPenups); }
  void setIncludePenups(bool val) { SETBIT(m_flags, kInclPenups, val); }

  double trackingPercent() const {return m_trackingPercent; }
  void setTrackingPercent(double tracking) { m_trackingPercent = tracking; }
};

typedef OdUInt32 OdFontSubType;
#define kBigFont10    0x0001
#define kUniFont10    0x0002
#define kFont10       0x0004
#define kFont11       0x0008
#define kFont10A      0x0010
#define kTrueType     0x0020
#define kFontGdt      0x0040
#define kFontSimplex6 0x0080
#define kShapes11     0x0100

class OdGePoint2d;

/** Description:

    {group:Other_Classes}
*/
class OdFontGeometry2d
{
public:
  virtual void polyline(OdUInt32 nPoints, const OdGePoint2d* points) = 0;
  virtual void polyPolygon(OdUInt32 nTotalPoints, OdUInt32 nContours,
    OdUInt32* pnCounts, const OdGePoint2d* points) = 0;
  //virtual double deviation() = 0;
};

class OdGePoint3d;

/** Description:

    {group:Other_Classes}
*/
class OdFontGeometry3d
{
public:
  virtual void polyline(OdUInt32 nPoints, const OdGePoint3d* points) = 0;
  virtual void polyPolygon(OdUInt32 nTotalPoints, OdUInt32 nContours,
    OdUInt32* pnCounts, const OdGePoint3d* points) = 0;
  //virtual double deviation() = 0;
};

class OdGiConveyorGeometry;

/** Description:

    {group:Other_Classes}
*/
class FIRSTDLL_EXPORT OdFont : public OdRxObject
{
	OdUInt32 m_Flags;

public:
  ODRX_DECLARE_MEMBERS(OdFont);

  OdFont() : m_Flags(0) {}

	OdUInt32 getFlags() const { return m_Flags; }
	OdUInt32 flags() { return m_Flags; }

	void setFlags(OdUInt32 value) { m_Flags = value; }
	void addFlag(OdUInt32 flag) { m_Flags |= flag; }

	virtual OdResult     initialize(OdStreamBuf* io) = 0;
  virtual OdResult     drawCharacter(OdUInt16 character, OdGePoint2d& advance, OdGiCommonDraw* pWd,
                                     OdTextProperties& textFlags) = 0;
  virtual OdResult     drawCharacter(OdUInt16 character, OdGePoint2d& advance,
                                     OdGiConveyorGeometry* pGeom,
                                     OdTextProperties& textFlags) = 0;
	virtual double       getAbove() const = 0;
	virtual double       getBelow() const = 0;
  virtual OdUInt32     getAvailableChars(OdArray<OdUInt16>& retArray) = 0;
  virtual bool        hasCharacter(OdUInt16 character) = 0;
  
  virtual double      getHeight() const                                                   // MKU 20.02.2003
  {
    return getAbove() + getBelow();
  }
  virtual double getInternalLeading() const
  {
    return 0;
  }


  //  removed here from GiContextForDbDatabase.cpp              // MKU 04.03.2003

  double fontAbove() const  
  {
    double above = getAbove();
    if(above == 0.0)
    {
      above = 1.0;
    }
    return above;
  }
  virtual double getUnderlinePos(double textSize) const
  {
    return -0.2 * textSize;
  }
  virtual double getOverlinePos(double textSize) const
  {
    return 1.2 * textSize;
  }
  virtual bool isShxFont()
  {
    return true;
  }
  virtual double getAverageWidth()
  {
    return 0.0;
  }

  virtual void getScore( OdUInt16 /*character*/, 
                         OdGePoint2d& /*advance*/, 
                         OdGePoint3d* /*pointsOver*/,
                         OdGePoint3d* /*pointsUnder*/,
                         const OdTextProperties& /*flags*/ )
  {}
};

typedef OdSmartPtr<OdFont> OdFontPtr;

#include "DD_PackPop.h"

#endif // !defined(AFX_ODFONT_H__0B76813A_DCFA_450E_8591_B6C6F1ED76EC__INCLUDED_)


