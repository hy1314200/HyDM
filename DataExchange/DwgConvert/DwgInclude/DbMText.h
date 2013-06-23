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



#ifndef OD_DBMTEXT_H
#define OD_DBMTEXT_H

#include "DD_PackPush.h"

#include "DbEntity.h"
#include "TextDefs.h"
#include "Ge/GePoint2d.h"
#include "Ge/GePoint3d.h"

class OdGeVector3d;
class OdGePoint2d;

/** Description: 
  
    {group:DD_Namespaces}
*/
namespace OdDb
{
  enum LineSpacingStyle
  {
    kAtLeast = 1, // Larger characters on lines will increase line spacing.
    kExactly = 2  // Line spacing is constant.
  };
}

/** Description:
    This structure is used to describe a fragment of text created by explodeFragments().
    
    Remarks:
    Each fragment contains a text string and its attributes.   

    {group:Structs}
*/
struct OdDbMTextFragment
{
  OdGePoint3d location;           // Insertion point.

  /* OdGeVector3d normal;
     OdGeVector3d direction;
  */

  OdString text;                  // Text string.

  OdString font;                  // SHX Font.
  OdString bigfont;               // SHX Bigfont.

  OdGePoint2d extents;            // Extents in OCS.
  double capsHeight;              // Height.
  double widthFactor;             // Width factor.
  double obliqueAngle;            // Obliquing angle.
  double trackingFactor;          // Tracking factor.
  
  // OdUInt16 colorIndex;
  
  OdCmEntityColor color;          // Color.
  bool vertical;                  // Text is vertical.
  
  bool stackTop;                  // Text is top of stacked text.
  bool stackBottom;               // Text is bottom of stacked text.

  bool underlined;                // Text is underlined.
  bool overlined;                 // Text is overlined.
  OdGePoint3d underPoints[2];     // Underline endpoints.
  OdGePoint3d overPoints[2];      // Overline endpoints.

  //  true type font data
    
  OdString  fontname;             // TrueType font name, or empty string.
  int       charset;              // Truetype character set.
  bool      bold;                 // Text is bold.
  bool      italic;               // Text is italic.

  // 0 - no change 1 - change to original 2 - change to other 

  int      changeStyle;           // 0 == No change; 1 == Change to original; 2 == Change to other
  bool     lineBreak;             // Text is followed by a line break.
  bool     newParagraph;          // Text is followed by a paragraph break.
};

/** Description:
    This structure is used by OdDbMText::getParagraphsIndent() to to return indentation and tab data.

    {group:Structs}
*/
struct OdDbMTextIndent
{
  double  paragraphInd;  // Subsequent line indent.

  double  firstLineInd;  // First line indent.

  OdArray<double> tabs; // Tab settings.
};

typedef int(*OdDbMTextEnum)(OdDbMTextFragment *, void *);

typedef OdArray<OdDbMTextIndent> OdDbMTextIndents;

class OdDbText;

/** Description:
    This class represents paragraph (multi-line) text (MText) entities in an OdDbDatabase instance.
  
    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbMText : public OdDbEntity
{
public:
  ODDB_DECLARE_MEMBERS(OdDbMText);

  OdDbMText();

  /** Description:
    Returns the insertion point of this MText entity (WCS equivalent of DXF 10).
  */
  OdGePoint3d location() const;

  /** Description:
    Sets the insertion point of this MText entity (WCS equivalent of DXF 10).

    Arguments:
    location (I) Insertion point.
  */
  void setLocation(
    const OdGePoint3d& location);

  /** Description:
    Returns the WCS *normal* to the plane of this entity (DXF 210).
  */
  OdGeVector3d normal() const;
  
  /** Description:
    Sets the WCS *normal* to the plane of this entity (DXF 210).
    Arguments:
    normal (I) Normal.
  */
  void setNormal(
    const OdGeVector3d& normal);

  /** Description:
    Returns the WCS X-axis *direction* vector of this MText entity (DXF 11).
      
  */
  OdGeVector3d direction() const;

  /** Description:
    Sets the WCS X-axis *direction* vector of this MText entity (DXF 11).
    Arguments:
    direction (I) Direction vector.
    Remarks:
    Direction and *rotation* are equivalent, and only one need be set when creating (or modifying) an MText entity.\\\\\
  */
  void setDirection(
    const OdGeVector3d& direction);

  /** Description:
    Returns the *rotation* angle of this MText entity (DXF 50).
    Note:
    All angles are expressed in radians.
 */
  double rotation() const;

  /** Description:
    Sets the *rotation* angle of this MText entity (DXF 50).
    Arguments:
    rotation (I) Rotation angle.
    Note:
    All angles are expressed in radians.
  */
  void setRotation(
    double rotation);

  /** Description:
    Returns the word-wrap *width* of this MText entity (DXF 41).

    Remarks:
    Words exceeding this value will extend beyond this *width*.
  */
  double width() const;

  /** Description:
    Sets the word-wrap *width* of this MText entity (DXF 41).
    Arguments:
    width (I) Word-wrap *width*
    Remarks:
    Words exceeding this value will extend beyond the Specified *width*.
  */
  void setWidth(
    double width);

  /** Description:
    Returns the Object ID of the text style of this MText entity (DXF 7).
  */
  OdDbObjectId textStyle() const;

  /** Description:
    Sets the Object ID of the text style of this MText entity (DXF 7).
    Arguments:
    textStyleId (I) Text style Object ID.
  */
  void setTextStyle(
    OdDbObjectId textStyleId);

  /** Description:
    Returns the text *height* of this MText entity (DXF 40).
  */
  double textHeight() const;

  /** Description:
    Sets the text *height* of this MText entity (DXF 40).
    Arguments:
    height (I) Text *height*.
  */
  void setTextHeight(
    double height);

  enum AttachmentPoint
  {
    kTopLeft        = 1,    // Top Left
    kTopCenter      = 2,    // Top Center
    kTopRight       = 3,    // Top Right
    kMiddleLeft     = 4,    // Middle Left
    kMiddleCenter   = 5,    // Middle Center
    kMiddleRight    = 6,    // Middle Right
    kBottomLeft     = 7,    // Bottom Left
    kBottomCenter   = 8,    // Bottom Center
    kBottomRight    = 9,    // Bottom Right
    kBaseLeft       = 10,   // Baseline Left
    kBaseCenter     = 11,   // Baseline Center
    kBaseRight      = 12,   // Baseline Right
    kBaseAlign      = 13,   // Baseline Aligned
    kBottomAlign    = 14,   // Bottom Aligned
    kMiddleAlign    = 15,   // Middle Aligned
    kTopAlign       = 16,   // Top Aligned
    kBaseFit        = 17,   // Baseline Fit
    kBottomFit      = 18,   // Bottom Fit
    kMiddleFit      = 19,   // Middle Fit
    kTopFit         = 20,   // Top Fit
    kBaseMid        = 21,   // Baseline Middled
    kBottomMid      = 22,   // Bottom Middled
    kMiddleMid      = 23,   // Middle Middled
    kTopMid         = 24    // Top Middled
  };

  /** Description:
    Returns the *type* of *attachment* point of this MText entity (DXF 71).
    
    Remarks:
    attachment will return one of the following:
    
    @table
    Name            Value
    kTopLeft        1
    kTopCenter      2
    kTopRight       3
    kMiddleLeft     4
    kMiddleCenter   5
    kMiddleRight    6
    kBottomLeft     7
    kBottomCenter   8
    kBottomRight    9
  */
  AttachmentPoint attachment() const;

  /** Description:
    Sets the *type* of *attachment* point of this MText entity (DXF 71).
    Arguments:
    type (I) Type of *attachment* point.

    Remarks:
    attachment will be one of the following:
    
    @table
    Name            Value
    kTopLeft        1
    kTopCenter      2
    kTopRight       3
    kMiddleLeft     4
    kMiddleCenter   5
    kMiddleRight    6
    kBottomLeft     7
    kBottomCenter   8
    kBottomRight    9

    Note:
    setAttachment() keeps the location of this MText entity constant 
    while changing the *attachment* type, thereby changing the extents of (moving) this MText entity.
    
    setAttachmentMovingLocation() moves the location of the MText entity
    while changing the *attachment* type, thereby maintaining the extents (not moving) this MText entity.
    
    See also:
    setAttachmentMovingLocation    
  */
  void setAttachment(
    AttachmentPoint type);

  enum FlowDirection
  {
    kLtoR     = 1,  // Left to Right
    kRtoL     = 2,  // Right to Left  
    kTtoB     = 3,  // Top to Bottom
    kBtoT     = 4,  // Bottom to Top
    kByStyle  = 5   // By Style
  };

  /** Description:
    Returns the flow *direction* of this MText entity (DXF 72).
    
    Remarks:
    flowDirection will return one of the following:
    
    @table
    Name        Value
    kLtoR       1
    kRtoL       2  
    kTtoB       3
    kBtoT       4
    kByStyle    5
  */
  FlowDirection flowDirection() const;

  /** Description:
    Sets the flow *direction* of this MText entity (DXF 72).
    Arguments:
    flowDirection (I) Flow *direction*.
    Remarks:
    flowDirection will return one of the following:
    
    @table
    Name        Value
    kLtoR       1
    kRtoL       2  
    kTtoB       3
    kBtoT       4
    kByStyle    5
  */
  void setFlowDirection(
    FlowDirection flowDirection);

  /** Description:
    Returns a copy of the character *contents* of this MText entity (DXF 1 & 3).
  */
  OdString contents() const;

  /** Description:
    Sets the character *contents* of this MText entity (DXF 1 & 3).
    Arguments:
    text (I) Character *contents*.  
  */
  int setContents(const char* text);

  /** Description:
    Returns the *width* of the bounding box of this MText entity.
    Note:
    This value will probably be different than that returned by OdDbMText::width().
  */
  double actualWidth() const;

  /** Description:
    Returns the non-break space string "\~".
    
    Remarks:
    May be used in place of "\~" to make code more understandable.
  */
  static const char* nonBreakSpace();   

  /** Description:
    Returns the overline on string "\O".

    Remarks:
    May be used in place of "\O" to make code more understandable.
  */
  static const char* overlineOn();      

  /** Description:
    Returns the overline off string "\o".

    Remarks:
    May be used in place of "\o" to make code more understandable.
  */
  static const char* overlineOff();     

  /** Description:
    Returns the underline on string "\L".

    Remarks:
    May be used in place of "\L" to make code more understandable.
  */
  static const char* underlineOn();

  /** Description:
    Returns the underline off string "\l".

    Remarks:
    May be used in place of "\l" to make code more understandable.
  */
  static const char* underlineOff();

  /** Description:
    Returns the *color* change string "\C".

    Remarks:
    May be used in place of "\C" to make code more understandable.
  */
  static const char* colorChange(); 

  /** Description:
    Returns the font change string "\F".
      
    Remarks:
    May be used in place of "\F" to make code more understandable.
  */
  static const char* fontChange();      

  /** Description:
    Returns the height change string "\H".

    Remarks:
    May be used in place of "\H" to make code more understandable.
  */
  static const char* heightChange(); 

  /** Description:
    Returns the *width* change string "\W".

    Remarks:
    May be used in place of "\W" to make code more understandable.
  */
  static const char* widthChange();

  /** Description:
    Returns the oblique angle change string "\Q".

    Remarks:
    May be used in place of "\Q" to make code more understandable.
  */
  static const char* obliqueChange();

  /** Description:
    Returns the track change string "\T".

    Remarks:
    May be used in place of "\T" to make code more understandable.
  */
  static const char* trackChange();

  /** Description:
    Returns the line break string "\p".

    Remarks:
    May be used in place of "\p" to make code more understandable.
  */
  static const char* lineBreak(); 

  /** Description:
    Returns the paragraph break string "\P".
    Remarks:
    May be used in place of "\P" to make code more understandable.
  */
  static const char* paragraphBreak(); 

  /** Description:
    Returns the stacked text start string "\S".
    Remarks:
    May be used in place of "\S" to make code more understandable.
  */
  static const char* stackStart();

  /** Description:
    Returns the alignment change string "\A".

    Remarks:
    May be used in place of "\A" to make code more understandable.
  */
  static const char* alignChange();     

  /** Description:
    Returns the begin block string "{".

    Remarks:
    May be used in place of "{" to make code more understandable.
  */
  static const char* blockBegin();

  /** Description:
    Returns the end block string "}".
    
    Remarks:
    May be used in place of "}" to make code more understandable.
  */
  static const char* blockEnd();  

  /** Description:
    Sets the linespacing style of this MText entity (DXF 73).
      
    Arguments:
    lineSpacingStyle (I) Linespacing style.
  */
  void setLineSpacingStyle(
    OdDb::LineSpacingStyle lineSpacingStyle);

  /** Description:
    Returns the linespacing style of this MText entity (DXF 73).
  */
  OdDb::LineSpacingStyle lineSpacingStyle() const;

  /** Description:
    Sets the linespacing factor of this MText entity (DXF 44).
    Arguments:
    lineSpacingFactor (I) Linespacing Factor.  
  */
  void setLineSpacingFactor(
    double lineSpacingFactor);

  /** Description:
      Returns the linespacing factor of this MText entity (DXF 44).
  */
  double lineSpacingFactor() const;

  /** Description:
    Returns the horizontal mode of this MText entity (DXF 72).

    Remarks:
    horizontalMode returns one of the following:
    
    @table
    Name           Value
    kTextLeft      0
    kTextCenter    1 
    kTextRight     2
    kTextAlign     3
    kTextMid       4    
    kTextFit       5

  */
  OdDb::TextHorzMode horizontalMode() const;

  /** Description:
    Sets the horizontal mode of this MText entity (DXF 72).
    Arguments:
    horizontalMode (I) Horizontal mode.

    Remarks:
    horizontalMode will be one of the following:
    
    @table
    Name           Value
    kTextLeft      0
    kTextCenter    1 
    kTextRight     2
    kTextAlign     3
    kTextMid       4    
    kTextFit       5
  */
  void setHorizontalMode(
    OdDb::TextHorzMode horizontalMode);

  /** Description:
    Returns the vertical mode of this MText entity (DXF 73).

    Remarks:
    verticalMode will return one of the following:
    
    @table
    Name           Value
    kTextBase      0
    kTextBottom    1 
    kTextVertMid   2
    kTextTop       3
  */
  OdDb::TextVertMode verticalMode() const;

  /** Description:
    Sets the vertical mode of this MText entity (DXF 73).

    Arguments:
    verticalMode (I) Vertical mode.

    Remarks:
    verticalMode will be one of the following:
    
    @table
    Name           Value
    kTextBase      0
    kTextBottom    1 
    kTextVertMid   2
    kTextTop       3
  */
  void setVerticalMode(
    OdDb::TextVertMode verticalMode);

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  bool worldDraw(
    OdGiWorldDraw* pWd) const;

  virtual OdResult transformBy(const OdGeMatrix3d& xfm);

  virtual OdResult explode(
    OdRxObjectPtrArray& entitySet) const; /* Replace OdRxObjectPtrArray */

  void getClassID(
    void** pClsid) const;

  void subClose();
  
  /** Description:
    Explodes this MText entity into a sequence of simple text fragments, passing each
    fragment to the specified function

    Arguments:
    fragmentFn (I) Function pointer to the fragment elaboration callback function.
    params (I) Void pointer to the fragmentFn callback function's second argument.
    ctxt (I) Drawing context.
      
    Remarks:
    The prototype of the fragment elaboration is as follows:
    
            int (*OdDbMtextEnum)(OdDbMTextFragment *fragment, void *param)
     
    If ctxt == NULL, the current OdGiWorldDraw object will be used.

    The elaboration function should return 1 to continue, or 0 to terminate the explosion.
    
    Each new line in this MText entity, and each change of text attributes, will start a new fragment. 
  */
  void explodeFragments(
    OdDbMTextEnum fragmentFn, 
    void *params, 
    OdGiWorldDraw *ctxt = NULL) const;

  /** Description:
    Returns the height of the bounding box of this MText entity.
   
    Arguments:
    ctxt (I) Drawing context.
  
    Remarks:
    If ctxt == NULL, the current OdGiWorldDraw object will be used.
  */
  double actualHeight(
    OdGiWorldDraw *ctxt = NULL) const;
  
  /** Description:
    Returns the WCS bounding points of this MText entity.
    
    Arguments:
    boundingPoints (O) Receives the bounding points.
    
    Remarks:
    The points are returned as follows:
    
    @table
    Point                Corner
    boundingPoints[0]    Top left
    boundingPoints[1]    Top right
    boundingPoints[2]    Bottom right
    boundingPoints[3]    Bottom left
  */
  void getBoundingPoints(
    OdGePoint3dArray& boundingPoints) const;

  /** Description:
    Sets the *type* of *attachment* point of this MText entity (DXF 71).
    Arguments:
    attachment (I) Type of *attachment* point.

    Remarks:
    attachment will be one of the following:

    @table
    Name            Value
    kTopLeft        1
    kTopCenter      2
    kTopRight       3
    kMiddleLeft     4
    kMiddleCenter   5
    kMiddleRight    6
    kBottomLeft     7
    kBottomCenter   8
    kBottomRight    9

    Note:
    setAttachment() keeps the location of this MText entity constant 
    while changing the *attachment* type, thereby changing the extents of (moving) this MText entity.
    
    setAttachmentMovingLocation() adjusts the location of the MText entity
    while changing the *attachment* type, so as to maintain the extents (not move) this MText entity.
    
    See also:
    setAttachment    
  */
  OdResult setAttachmentMovingLocation(
    AttachmentPoint type);

  virtual OdResult getGeomExtents(
    OdGeExtents3d& extents) const;

  /** Description:
    Returns true if and only if background fill is on of this MText entity (DXF 90, bit 0x01).
  */
  bool backgroundFillOn() const;

  /** Description:
    Controls the background fill of this MText entity (DXF 90, bit 0x01).
    Arguments:
    enable (I) True to *enable* background fill, false to *disable*.
  */
  void setBackgroundFill(
    bool enable);

  /** Description:
    Returns the background fill *color* of this MText entity (DXF 63 and optionally 421 & 431).
  */
  OdCmColor getBackgroundFillColor() const;

  /** Description:
    Sets the background fill *color* of this MText entity (DXF 63 and optionally 421 & 431).
    Arguments:
    color (I) Backgroud fill *color*.
  */
  void setBackgroundFillColor(
    const OdCmColor& color);

  /** Description:
    Returns the background scale factor of this MText entity (DXF 45).
    
    Remarks:
    The filled background area is extended by (ScaleFactor - 1) * TextHeight in all directions.
  */
  double getBackgroundScaleFactor() const;

  /** Description:
    Sets the background scale factor of this MText entity (DXF 45).

    Arguments:
    scaleFactor (I) Background scale factor.

    Remarks:
    The filled background area is extended by (ScaleFactor - 1) * TextHeight in all directions.
    
    Valid range is [1.0..5.0].
  */
  void setBackgroundScaleFactor(
    const double scaleFactor);

  /** Description:
    Returns the background *transparency* of this MText entity (DXF 441).
  */
  OdCmTransparency getBackgroundTransparency() const;

  /** Description:
    Sets the background *transparency* of this MText entity (DXF 441).
    Arguments:
    transparency (I) Background *transparency*.  
  */
  void setBackgroundTransparency(
    const OdCmTransparency& transparency);

  /** Description:
    Returns true if and only if the screen background *color* is 
    used as the background color of this MText entity (DXF 90, bit 0x02).
  */
  bool useBackgroundColorOn() const;

  /** Description:
    Controls the use of the screen background *color* as the background *color* 
    of this MText entity (DXF 90, bit 0x02).

    Arguments:
    enable (I) True to use the screen background *color*, false to use
               the setBackgroundFillColor() *color*.
  */
  void setUseBackgroundColor(
    bool enable);

  /** Description:
    Returns the paragraph indentation and tab data of this MText entity.
    Arguments:
    indents (O) Receives the indentation and tab data.
  */
  void getParagraphsIndent(
    OdDbMTextIndents& indents) const;

  OdDbObjectId setField(
    const OdString& fieldName, 
    OdDbField* pField);
  OdResult removeField(
    OdDbObjectId fieldId);
  OdDbObjectId removeField(
    const OdString& fieldName);

  /**Description:
    Converts the fields in this MText entity to text, and removes the fields.
    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
    Note:
    The fields are not evaluated before conversion.
  */
  void convertFieldToText();
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbMText object pointers.
*/
typedef OdSmartPtr<OdDbMText> OdDbMTextPtr;

#include "DD_PackPop.h"

#endif


