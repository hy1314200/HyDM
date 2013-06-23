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



#ifndef OD_DBTEXT_H
#define OD_DBTEXT_H

#include "DD_PackPush.h"

#include "DbEntity.h"
#include "TextDefs.h"

/** Description:
    This class represents simple text (Text) entities in an OdDbDatabase instance.
  
    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbText: public OdDbEntity
{
public:
  
  ODDB_DECLARE_MEMBERS(OdDbText);
  
  OdDbText();
  
  /** Description:
    Returns the *position* of this entity (WCS equivalent of DXF 10).
  */
  OdGePoint3d position() const;
  
  /** Description:
    Sets the *position* of this entity (WCS equivalent of DXF 10).

    Arguments:
    position (I) Position.
  */
  void setPosition(
    const OdGePoint3d& position);
  
  /** Description:
    Returns the *alignment* point of this Text entity (WCS equivalent of DXF 11).
  */
  OdGePoint3d alignmentPoint() const;
  
  /** Description:
    Sets the *alignment* point of this Text entity (WCS equivalent of DXF 11).
    Arguments:
    alignment (I) Alignment point.
  */
  void setAlignmentPoint(
    const OdGePoint3d& alignment);
  
  /** Description:
    Return true if and only if this Text entity is in the default alignment.
    Remarks:
    The default alignment is horizontalMode() == OdDb::kTextLeft and verticalMode() == OdDbKTextBase.
  */
  bool isDefaultAlignment() const;
  
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
 
  /** Remarks:
      Always returns true.
  */
  virtual bool isPlanar() const { return true; }
  
  virtual OdResult getPlane(
    OdGePlane& plane, 
    OdDb::Planarity& planarity) const;
  
  /** Description:
    Returns the *thickness* of this entity (DXF 39).
    
    Remarks:
    Thickness is the extrusion length along the *normal*.
  */
  double thickness() const;
  
  /** Description:
    Sets the *thickness* of this entity (DXF 39).
    Arguments:
    thickness (I) Thickness.
    Remarks:
    Thickness is the extrusion length along the *normal*.
  */
  void setThickness(
    double thickness);
  
  /** Description:
    Returns the *oblique* angle of this Text entity (DXF 51).

    Remarks:
    The range of oblique is ±1.48335 radians (±85°).
    
    Oblique angles are measured clockwise from the vertical.

    Note:
    All angles are expressed in radians.  
  */
  double oblique() const;
  
  /** Description:
    Sets the *oblique* angle of this Text entity (DXF 51).
    Arguments:
    oblique (I) Oblique angle.
    
    Remarks:
    The range of oblique is ±1.48335 radians (±85°).
    
    Oblique angles are measured clockwise from the vertical.
     
    Note:
    All angles are expressed in radians.  
  */
  void setOblique(
    double oblique);
  
  /** Description:
    Returns the *rotation* angle of this Text entity (DXF 50).
    Note:
    All angles are expressed in radians.
 */
  double rotation() const;
  
  /** Description:
    Sets the *rotation* angle of this Text entity (DXF 50).
    Arguments:
    rotation (I) Rotation angle.
    Note:
    All angles are expressed in radians.
  */
  void setRotation(
    double rotation);
  
  /** Description:
    Returns the *height* of this Text entity (DXF 40).
  */
  double height() const;

  /** Description:
    Sets the *height* of this Text entity (DXF 40).
    Arguments:
    height (I) Text *height*.
  */
  void setHeight(
    double height);
  
  /** Description:
    Returns the width factor of this Text entity (DXF 41).
  */
  double widthFactor() const;
  
  /** Description:
    Sets the width factor of this Text entity (DXF 40).
    Arguments:
    widthFactor (I) Width factor.  
  */
  void setWidthFactor(
    double widthFactor);
  
  /**
    Returns true if and only if this Text entity is mirrored in the X (horizontal) direction (DXF 71, bit 0x02).
  */
  bool isMirroredInX() const;
  
  /**
    Controls the mirroring of this Text entity in the X (horizontal) direction (DXF 71, bit 0x02).
    Arguments:
    mirror (I) Controls mirroring.
  */
  void mirrorInX(
    bool mirror);
  
  /**
    Returns true if and only if this Text entity is mirrored in the Y (vertical) direction (DXF 71, bit 0x04).
  */
  bool isMirroredInY() const;
  
  /**
    Controls the mirroring of this Text entity in the Y (vertical) direction (DXF 71, bit 0x04).
    Arguments:
    mirror (I) Controls mirroring.
  */
  void mirrorInY(
    bool mirror);
  
  /** Description:
    Returns the text string of this Text entity (DXF 1).
  */
  OdString textString() const;
  
  /** Description:
    Sets the text string of this Text entity (DXF 1).
    Arguments:
    textString (I) Text string.
    
    Note:
    textString cannot exceed 256 characters excluding the null terminator.
  */
  void setTextString(
    const OdChar* textString);
  
  /** Description:
    Returns the Object ID of the text style of this Text entity (DXF 7).
  */
  OdDbObjectId textStyle() const;
  
  /** Description:
    Sets the Object ID of the text style of this Text entity (DXF 7).
    Arguments:
    textStyleId (I) Text style Object ID.
  */
  void setTextStyle(
    OdDbObjectId textStyleId);
  
  /** Description:
    Returns the horizontal mode of this Text entity (DXF 72).

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
    Returns the horizontal mode of this Text entity (DXF 72).

    Arguments:
    horizontalMode (I) Horizontal mode.

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
  void setHorizontalMode(OdDb::TextHorzMode horizontalMode);
  
  /** Description:
    Returns the vertical mode of this Text entity (DXF 73).

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
    Sets the vertical mode of this Text entity (DXF 73).

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
  
  /** Description:
    Evokes the spell checker on this Text entity.
    
    Remarks:
    Returns 0 is successful, or 1 if not.
  */
  int correctSpelling();
  
  virtual void getClassID(
    void** pClsid) const;
  
  virtual OdUInt32 setAttributes(
    OdGiDrawableTraits* pTraits) const;
  
  /** Description:
    Adjusts the *position* of this Text entity if its alignent is not left baseline.
    
    Arguments:
    pDb (I) Pointer to *database* used to resolve the text style of this
            Text entity.

    Remarks:
    Returns eOk if successful, or an appropriate error code if not.

    Note:
    This function is called by DWGdirect when a Text entity is closed.

    If this Text entity is *database* resident, pDb is ignored.
    
    If this Text entity is not *database* resident, pDb cannot be NULL.
  */
  virtual void adjustAlignment(
    OdDbDatabase* pDb = NULL);
  
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
  
  void subClose();
  
  virtual bool worldDraw(OdGiWorldDraw* pWd) const;
  
  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);

  virtual OdResult getTransformedCopy(
    const OdGeMatrix3d& xfm, 
    OdDbEntityPtr& pCopy) const;

  /** Description:
    Returns the WCS bounding points of this Text entity.
    
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

  /* Constructor. */
  /*OdDbText(const OdGePoint3d& position,
      const char* text,
      OdDbObjectId style = OdDbObjectId::kNull,
      double height = 0,
      double rotation = 0);
  */

  OdDbObjectId setField(
    const OdString& fieldName, 
    OdDbField* pField);
  OdResult removeField(
    OdDbObjectId fieldId);
  OdDbObjectId removeField(
    const OdString& fieldName);

  /**Description:
    Converts the fields in this Text entity to text, and removes the fields.
    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
    Note:
    The fields are not evaluated before conversion.
  */
  void convertFieldToText();
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbText object pointers.
*/
typedef OdSmartPtr<OdDbText> OdDbTextPtr;

#include "DD_PackPop.h"

#endif // ODDBTEXT_H


