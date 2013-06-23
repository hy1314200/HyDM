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



#ifndef _ODDBLINETYPETABLERECORD_INCLUDED
#define _ODDBLINETYPETABLERECORD_INCLUDED

#include "DD_PackPush.h"

#include "DbSymbolTableRecord.h"

class OdGeVector2d;

/** Description:
    This class represents Linetype records in the OdDbLinetypeTable in an OdDbDatabase instance.

    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbLinetypeTableRecord : public OdDbSymbolTableRecord
{
public:
  ODDB_DECLARE_MEMBERS(OdDbLinetypeTableRecord);

  OdDbLinetypeTableRecord();

  /** Description:
    Returns the simple ASCII representation of this Linetype (DXF 3).
  */
  const OdString comments() const;

  /** Description:
    Sets the simple ASCII representation for this Linetype (DXF 3).
    Arguments:
    comments (I) Comment string
  */
  void setComments(
    const OdString& comments);

  /** Description:
    Returns the length of this Linetype pattern (DXF 40).
    
    Remarks:
    patternLength is the total length in drawing units of all the dashes in this Linetype pattern.
  */
  double patternLength() const;

  /** Description:
    Sets the length of this Linetype pattern (DXF 40).
    
    Arguments:
    patternLength (I) Pattern length.
    
    Remarks:
    patternLength is the total length in drawing units of all the dashes in this Linetype pattern.
  */
  void setPatternLength(
    double patternLength);

  /** Description:
    Returns the number of dashes in this Linetype (DXF 73).
  */
  int numDashes() const;

  /** Description:
    Sets the number of dashes for this Linetype (DXF 73).
    Arguments:
    numDashes (I) Number of dashes [0,2..12]
    
    Note:
    Setting the number of dashes results in said number of zero-length dashes with no shape
    or text string associated with them.
  */
  void setNumDashes(
    int numDashes);

  /** Description:
    Returns the length of the dash at the specified index (DXF 49).  
    
    Arguments:
    dashIndex (I) Dash index.
    
    Remarks:
    0 <= dashIndex < numDashes().

  */
  double dashLengthAt(
    int dashIndex) const;

  /** Description:
    Sets the length of the dash at the specified index (DXF 49).  
    
    Remarks:
    0 <= dashIndex < numDashes().

    Arguments:
    dashIndex (I) Dash index.
    dashLength (I) Dash length.
  */
  void setDashLengthAt(
    int dashIndex, 
    double dashLength);

  /** Description:
    Returns the Object ID of the OdDbTextStyleTableRecord of
    the shape or TextStyle at the specified index (DXF 340).  
      
    Remarks:
    Returns NULL if there is no shape or text at the specified index.
    
    0 <= dashIndex < numDashes().

    Arguments:
    dashIndex (I) Dash index.

  */
  OdDbObjectId shapeStyleAt(
    int dashIndex) const;

  /** Description:
    Sets the Object ID of the OdDbTextStyleTableRecord of
    the shape or TextStyle for the specified index (DXF 340).  
      
    Remarks:
    0 <= dashIndex < numDashes().

    Arguments:
    dashIndex (I) Dash index.
    objectId (I) Object Id.
  */
  void setShapeStyleAt(
    int dashIndex, 
    OdDbObjectId objectId);

  /** Description:
    Returns the shape number of the shape at the specified
    index (DXF 75).  

    Remarks:
    0 <= dashIndex < numDashes().

    Arguments:
    dashIndex (I) Dash index.
  */
  OdUInt16 shapeNumberAt(
    int dashIndex) const;

  /** Description:
    Sets the shape number of the shape at the specified
    index (DXF 75).  

    Remarks:
    0 <= dashIndex < numDashes().

    Arguments:
    dashIndex (I) Dash index.
    shapeNumber (I) Shape number.
  */
  void setShapeNumberAt(
    int dashIndex, 
    OdUInt16 shapeNumber);

  /** Description:
    Returns the shape offset of the shape or text string at the specified
    index (DXF 44, 45).  
    
    Remarks:
    0 <= dashIndex < numDashes().

    Arguments:
    dashIndex (I) Dash index.
  */
  OdGeVector2d shapeOffsetAt(int dashIndex) const;

  /** Description:
    Sets the shape offset of the shape or text string at the specified
    index (DXF 44, 45).  
    
    Remarks:
    0 <= dashIndex < numDashes().

    Arguments:
    dashIndex (I) Dash index.
    shapeOffset (I) Shape offset.
  */
  void setShapeOffsetAt(
    int dashIndex, 
    const OdGeVector2d& shapeOffset);

  /** Description:
    Returns the scale of the shape or text string at the specified
    index (DXF 46).  
      
    Remarks:
    0 <= dashIndex < numDashes().

    Arguments:
    dashIndex (I) Dash index.
  */
  double shapeScaleAt(
    int dashIndex) const;

  /** Description:
    Returns the scale of the shape or text string at the specified
    index (DXF 46).  
      
    Remarks:
    0 <= dashIndex < numDashes().

    Arguments:
    dashIndex (I) Dash index.
    shapeScale (I) Shape scale.
  */
  void setShapeScaleAt(
    int dashIndex, 
    double shapeScale);

  /** Description:
    Returns the alignment type for this Linetype (DXF 72). 
    
    Remarks:
    A value of true indicates that DXF 72 code contains the letter 'S' (scaled to fit), 
    false indicates that it contains the letter 'A' (not scaled to fit).
  */
  bool isScaledToFit() const;

  /** Description:
    Sets the alignment type for this Linetype (DXF 72).  
    
    Arguments:
    scaledToFit (I) Controls the ScaledToFit flag. 

    Remarks:
    A value of true indicates that DXF 72 code contains the letter 'S' (scaled to fit), 
    false indicates that it contains the letter 'A' (not scaled to fit).
  */
  void setIsScaledToFit(
    bool scaledToFit);

  /** Description:
    Returns the UcsOriented flag for the shape or text string at the specified index (DXF 74, bit 0x01).

    Arguments:
    dashIndex (I) Dash index.

    Remarks:
    Returns true if and only if the shape or text string is UcsOriented.
    
    0 <= dashIndex < numDashes().
  */
  bool shapeIsUcsOrientedAt(
    int dashIndex) const;

  /** Description:
    Returns the UcsOriented flag for the shape or text string at the specified index (DXF 74, bit 0x01).

    Arguments:
    dashIndex (I) Dash index.
    isUcsOriented (I) True for UcsOriented, false for line-segment oriented.

    Remarks:
    0 <= dashIndex < numDashes().
  */
  void setShapeIsUcsOrientedAt(
    int dashIndex, 
    bool isUcsOriented);

  /** Description:
    Returns the rotation angle of the shape or text string at the specified
    index (DXF 50).  
      
    Remarks:
    0 <= dashIndex < numDashes().

    Arguments:
    dashIndex (I) Dash index.
    
    Note:
    All angles are expressed in radians.
  */
  double shapeRotationAt(
    int dashIndex) const;

  /** Description:
    Sets the rotation angle of the shape or text string at the specified
    index (DXF 50).  
      
    Remarks:
    0 <= dashIndex < numDashes().

    Arguments:
    dashIndex (I) Dash index.
    shapeRotation (I) Shape rotation angle.
    
    Note:
    All angles are expressed in radians.
  */
  void setShapeRotationAt(
    int dashIndex, 
    double shapeRotation);

  /** Description:
    Returns the text string at the specified
    index (DXF 9).  

    Remarks:
    0 <= dashIndex < numDashes().

    Arguments:
    dashIndex (I) Dash index.
  */
  OdString textAt(int dashIndex) const;

  /** Description:
    Sets the *text* string at the specified
    index (DXF 9).  

    Remarks:
    0 <= dashIndex < numDashes().

    Arguments:
    dashIndex (I) Dash index.
    text (I) Text string.
  */
  void setTextAt(
    int dashIndex, 
    const OdString& text);

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

  virtual void getClassID(void** pClsid) const;

  virtual OdUInt32 setAttributes(
    OdGiDrawableTraits* pTraits) const;
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbLinetypeTableRecord object pointers.
*/
typedef OdSmartPtr<OdDbLinetypeTableRecord> OdDbLinetypeTableRecordPtr;

#include "DD_PackPop.h"

#endif // _ODDBLINETYPETABLERECORD_INCLUDED


