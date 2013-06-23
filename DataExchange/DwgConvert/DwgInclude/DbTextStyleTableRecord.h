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



#ifndef _ODDBTEXTSTYLETABLERECORD_INCLUDED
#define _ODDBTEXTSTYLETABLERECORD_INCLUDED

#include "DD_PackPush.h"

#include "DbSymbolTableRecord.h"

/** Description:
    This class represents TextStyle records in the OdDbTextStyleTable in an OdDbDatabase instance.

    Library:
    Db
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbTextStyleTableRecord : public OdDbSymbolTableRecord
{
public:
  ODDB_DECLARE_MEMBERS(OdDbTextStyleTableRecord);

  OdDbTextStyleTableRecord();

  /** Description:
    Returns true if and only if the text *font* file for this TextStyle is interpreted as a shape file (DXF 70, bit 0x01).
  */
  bool isShapeFile() const;

  /** Description:
    Controls if the text *font* file for this TextStyle is interpreted as a shape file (DXF 70, bit 0x01).

    Arguments:
    isShapeFile (I) As a shape file, if and only if true.
  */
  void setIsShapeFile(
    bool isShapeFile);

  /** Description:
    Returns true if and only if text is drawn vertically with this TextStyle (DXF 70, bit 0x04).
  */
  bool isVertical() const;

  /** Description:
    Controls the vertical drawing of text with this TextStyle (DXF 70, bit 0x04).
    
    Arguments:
    isVertical (I) Controls vertical drawing.
  */
  void setIsVertical(
    bool isVertical);

  /** Description:
      Returns the fixed text size for this TextStyle (DXF 40).
  */
  double textSize() const;

  /** Description:
    Sets the fixed text size for this TextStyle (DXF 40).

    Arguments:
    textSize (I) Fixed text size.
  */
  void setTextSize(
    double textSize);

  /** Description:
    Returns the X-scale (width) factor for this TextStyle (DXF 41).
  */
  double xScale() const;

  /** Description:
    Sets the X-scale (width) factor for this TextStyle (DXF 41).
    
    Arguments:
    xScale (I) X-scale factor.
  */
  void setXScale(
    double xScale);

  /** Description:
    Returns the obliquing angle for this TextStyle (DXF 50).
    
    Remarks:
    The range of obliquingAngle is ±1.48335 radians (±85°). Negative angles will have Oda2PI added to them.

    Note:
    All angles are expressed in radians.
  */
  double obliquingAngle() const;

  /** Description:
    Sets the obliquing angle for this TextStyle (DXF 50).
    
    Arguments:
    obliquingAngle (I) Obliquing angle.

    Remarks:
    The range of obliquingAngle is ±1.48335 radians (±85°). Negative angles will have Oda2PI added to them.

    Note:
    All angles are expressed in radians.
  */
  void setObliquingAngle(
    double obliquingAngle);

  /** Description:
    Returns true if and only if text is drawn backwards with this TextStyle (DXF 71, bit 0x02).
  */
  bool isBackwards() const;

  /** Description:
    Controls the backwards drawing of text with this TextStyle (DXF 71, bit 0x02).
    
    Arguments:
    isBackwards (I) Controls backwards drawing.
  */
  void setIsBackwards(
    bool isBackwards);

  /** Description:
    Returns true if and only if text is drawn upside down with this TextStyle (DXF 71, bit 0x04).
  */
  bool isUpsideDown() const;

  /** Description:
    Controls the upside down drawing of text with this TextStyle (DXF 71, bit 0x04).
    
    Arguments:
    isUpsideDown (I) Controls upside down drawing.
  */
  void setIsUpsideDown(
    bool isUpsideDown);

  /** Description:
    Returns the size of the text most recently created with this TextStyle (DXF 42).
  */
  double priorSize() const;

  /** Description:
    Sets the size of the text most recently created with this TextStyle (DXF 42).
    
    Arguments:
    priorSize (I) Prior size.
  */
  void setPriorSize(
    double priorSize);

  /** Description:
      Returns the name of the *font* file associated with this TextStyle (DXF 3).
  */
  OdString fileName() const;

  /** Description:
      Sets the name of the *font* file associated with this TextStyle (DXF 3).

      Arguments:
      fontFileName (I) Font file name.
  */
  void setFileName(
    const OdString& fontFileName);

  /** Description:
    Returns the name of the BigFont file associated with this TextStyle (DXF 4).
  */
  OdString bigFontFileName() const;

  /** Description:
    Sets the name of the BigFont file associated with this TextStyle (DXF 4).
    
    Arguments:
    bigFontFileName (I) BigFont file name.
  */
  void setBigFontFileName(
    const OdString& bigFontFileName);

  /** Description:
    Sets this TextStyle to use the specified Windows font characteristics.

    Arguments:
    typeface (I) Typeface name of the font.
    bold (I) Bold if and only if true.
    italic (I) Italic if and only if true.
    charset (I) Windows character set identitier.
    pitchAndFamily (I) Windows pitch and character family identifier.
    
    Remarks:
    If typeface == NULL, the Windows font information is removed from this text style.
  */
  void setFont(
    const OdChar* typeface,
		bool bold,
		bool italic,
		int charset,
		int pitchAndFamily);

  /** Description:
    Returns the Windows *font* characteristics for this TextStyle.

    Arguments:
    typeface (O) Typeface name of the *font*.
    bold (O) True if and only if bold.
    italic (O) True if and only if italic.
    charset (O) Windows character set identitier.
    pitchAndFamily (O) Windows pitch and character family identifier.
  */
  void font(
    OdString& typeface,
		bool& bold,
		bool& italic,
		int& charset,
		int& pitchAndFamily) const;

  virtual void getClassID(
    void** pClsid) const;

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult dxfInFields_R12(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields_R12(
    OdDbDxfFiler* pFiler) const;
  
  OdResult dxfIn(
    OdDbDxfFiler* pFiler);

  virtual void subClose();
  virtual OdResult subErase(
    bool erasing);
  virtual void subHandOverTo(
    OdDbObject* newObject);

  virtual OdUInt32 setAttributes(
    OdGiDrawableTraits* pTraits) const;
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbTextStyleTableRecord object pointers.
*/
typedef OdSmartPtr<OdDbTextStyleTableRecord> OdDbTextStyleTableRecordPtr;

class OdGiTextStyle;
TOOLKIT_EXPORT void giFromDbTextStyle(const OdDbTextStyleTableRecord* pTStyle, OdGiTextStyle& giStyle);
TOOLKIT_EXPORT void giFromDbTextStyle(OdDbObjectId styleId, OdGiTextStyle& giStyle);

#include "DD_PackPop.h"

#endif // _ODDBTEXTSTYLETABLERECORD_INCLUDED


