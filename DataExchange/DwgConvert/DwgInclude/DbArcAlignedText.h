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



#ifndef _OD_DBARCALIGNEDTEXT_INC_
#define _OD_DBARCALIGNEDTEXT_INC_

#include "DD_PackPush.h"

#include "DbEntity.h"

enum OdArcTextAlignment
{
  kFit    = 1,
  kLeft   = 2,
  kRight  = 3,
  kCenter = 4
};

enum OdArcTextPosition
{
  kOnConvexSide = 1,
  kOnConcaveSide = 2
};

enum OdArcTextDirection
{
  kOutwardFromCenter = 1,
  kInwardToTheCenter = 2
};

/** Description:
    Represents an "Arc Aligned Text" entity within an OdDbDatabase.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbArcAlignedText : public OdDbEntity
{
public:
  ODDB_DECLARE_MEMBERS(OdDbArcAlignedText);

  /** Description:
      Constructor (no arguments).
  */
  OdDbArcAlignedText();
  
  virtual OdResult dwgInFields(OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(OdDbDxfFiler* pFiler) const;
  
  /** Description:
      Returns the text string for this entity (DXF 1).
  */
  OdString textString() const;
  
  /** Description:
      Sets the text string for this entity (DXF 1).
  */
  void setTextString(const OdString& str);
  
  /** Description:
      Sets the text string for this entity (DXF 1).
  */
  void setTextString(const OdChar *str);

  /** Description:
      Returns the Object ID of the arc associated with entity (DXF 330).
  */
  OdDbObjectId arcId() const;

  /** Description:
      Sets the Object ID of the arc associated with entity (DXF 330).
  */
  void setArcId(OdDbObjectId id);

  /** Description:
      Returns the center point for this entity (DXF 10).
  */
  OdGePoint3d center() const;

  /** Description:
      Sets the center point for this entity (DXF 10).
  */
  void setCenter(const OdGePoint3d& point);
  
  /** Description:
      Returns the radius value for this entity (DXF 40).
  */
  double radius() const;

  /** Description:
      Sets the radius value for this entity (DXF 40).
  */
  void setRadius(double radius);

  /** Description:
      Sets the start angle value for this entity (DXF 50).  
  */
  void setStartAngle(double  angle);

  /** Description:
      Gets the start angle value for this entity (DXF 50).  
  */
  double startAngle() const;

  /** Description:
      Sets the end angle value for this entity (DXF 51).    
  */
  void   setEndAngle(double  angle);

  /** Description:
      Gets the end angle value for this entity (DXF 51).    
  */
  double endAngle() const;

  /** Description:
      Gets the normal value for this entity (DXF 210).  
  */
  OdGeVector3d normal() const;
  
  /** Description:
      Sets the normal value for this entity (DXF 210).  
  */
  void setNormal(const OdGeVector3d& normal);

  /** Description:
      Gets the offset from arc value for this entity (DXF 44).  
  */
  double offsetFromArc() const;
  
  /** Description:
      Sets the offset from arc value for this entity (DXF 44).  
  */
  void setOffsetFromArc(double offset);
  
  /** Description:
      Gets the right offset value for this entity (DXF 45).  
  */
  double rightOffset() const;
  
  /** Description:
      Sets the right offset value for this entity (DXF 45).  
  */
  void setRightOffset(double offset);
  
  /** Description:
      Gets the left offset value for this entity (DXF 46).  
  */
  double leftOffset() const;
  
  /** Description:
      Sets the left offset value for this entity (DXF 46).  
  */
  void setLeftOffset(double offset);
  
  /** Description:
      Gets the text size value for this entity (DXF 42).    
  */
  double textSize() const;
  
  /** Description:
      Sets the text size value for this entity (DXF 42).  
  */
  void setTextSize(double height);
  
  /** Description:
      Gets the X scale value for this entity (DXF 41).  
  */
  double xScale() const;
  
  /** Description:
      Sets the X scale value for this entity (DXF 41).  
  */
  void setXScale(double WidthFactor);
  
  /** Description:
      Gets the character spacing value for this entity (DXF 43).  
  */
  double charSpacing() const;
  
  /** Description:
      Sets the character spacing value for this entity (DXF 43).  
  */
  void setCharSpacing(double CharSpacing);

  /** Description:
      Returns true is char order is reversed, false otherwise (DXF 70).
  */
  bool isReversedCharOrder() const;
  
  /** Description:
      Sets the reverse char order value for this entity (DXF 70).
  */
  void reverseCharOrder(bool doIt);

  /** Description:
      Gets the wizard flag value for this entity (DXF 280).  
  */
  bool wizardFlag() const;
  
  /** Description:
      Sets the wizard flag value for this entity (DXF 280).  
  */
  void setWizardFlag(bool val);

  /** Description:
      Gets the alignment value for this entity (DXF 72).  
  */
  OdArcTextAlignment alignment() const;
  
  /** Description:
      Sets the alignment value for this entity (DXF 72).  
  */
  void setAlignment(OdArcTextAlignment Alignment);

  /** Description:
      Sets the text position value for this entity (DXF 73).  
  */
  OdArcTextPosition textPosition() const;
  
  /** Description:
      Sets the text position value for this entity (DXF 73).  
  */
  void setTextPosition(OdArcTextPosition n);

  /** Description:
      Sets the text direction value for this entity (DXF 71).  
  */
  OdArcTextDirection textDirection() const;
  
  /** Description:
      Sets the text direction value for this entity (DXF 71).  
  */
  void setTextDirection(OdArcTextDirection textDir);

  /** Description:
      Gets the underlined value for this entity (DXF 76).  
  */
  bool isUnderlined() const;
  
  /** Description:
      Sets the underlined value for this entity (DXF 76).  
  */
  void setUnderlined(bool Underline);

  /** Description:
      Gets the text style value for this entity (DXF 7).    
  */
  OdDbObjectId textStyle() const;
  
  /** Description:
      Sets the text style value for this entity (DXF 7).  
  */
  void setTextStyle(const OdDbObjectId &id);
  
  /** Description:
      Sets the text style value for this entity (DXF 7).  
  */
  void setTextStyle(const char *stylename);

  /** Description:
      Gets the SHX font file value for this entity.  
  */
  OdString fileName() const;
  
  /** Description:
      Sets the SHX font file value for this entity.  
  */
  void setFileName(const OdChar* fontFile);
  
  /** Description:
      Gets the big font value for this entity (DXF 3).  
  */
  OdString bigFontFileName() const;
  
  /** Description:
      Sets the big font value for this entity (DXF 3).  
  */
  void setBigFontFileName(const OdChar* bigFontFile);

  /** Description:
      Sets the Truetype font values for this entity.  
  */
  void setFont(const OdChar* pTypeface, 
               bool bold, 
               bool italic,
               int charset, 
               int pitchAndFamily);

  /** Description:
      Gets the Truetype font values for this entity.  
  */
  void font(OdString& Typeface,
            bool& bold,
            bool& italic,
            int& charset,
            int& pitchAndFamily) const;

  /** Description:
      Returns true if this entity uses a SHX font, false otherwise (DXF 79).
  */
  bool isShxFont() const;

  virtual bool worldDraw(OdGiWorldDraw* pWd) const;

  virtual OdResult transformBy(const OdGeMatrix3d& xform);

  virtual OdResult explode(OdRxObjectPtrArray& entitySet) const; /* Replace OdRxObjectPtrArray */

  void subClose();

  /** Description:
      Support for persistent reactor to arc.
  */
  void modified(const OdDbObject* obj);
  void erased(const OdDbObject* obj, bool bErase);

private:

  /** Description:
      Returns the extents of the entity in WCS.
  
  virtual OdGePoint2d extents(const char* pStr, 
                              bool penups,
                              int len,
                              bool raw,
                              OdGiWorldDraw *ctxt = NULL) const;



  / * 
      virtual void getClassID(CLSID* pClsid) const;
   
      OdDbArcAlignedText(const OdGePoint3d& center, double radius,
                         double startAngle, double endAngle);
      OdDbArcAlignedText(const OdGePoint3d& center, const OdGeVector3d& normal,
                         double radius, double startAngle, double endAngle);
  */

};

typedef OdSmartPtr<OdDbArcAlignedText> OdDbArcAlignedTextPtr;

#include "DD_PackPop.h"

#endif 



