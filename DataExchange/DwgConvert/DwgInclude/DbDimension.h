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



#ifndef OD_DBDIM_H
#define OD_DBDIM_H

#include "DD_PackPush.h"

#include "DbEntity.h"
#include "DbMText.h"
#include "Ge/GeScale3d.h"

class OdDbDimStyleTableRecord;
typedef OdSmartPtr<OdDbDimStyleTableRecord> OdDbDimStyleTableRecordPtr;

/** Description:
    This class is the base class for all Dimension classes in an OdDbDatabase instance.
    
    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbDimension : public OdDbEntity
{
public:
  ODDB_DECLARE_MEMBERS(OdDbDimension);

  OdDbDimension();

  /** Description:
    Returns the position of the dimension text for this Dimension entity (DXF 11 as WCS).

    Remarks:
    This position is the middle center point of the dimension text, which is itself a 
    middle-center justified MText entity.  
  */
  OdGePoint3d textPosition() const;

  /** Description:
    Sets the position of the dimension text for this Dimension entity (DXF 11 as WCS).

    Arguments:
    textPosition (I) Text position.  
    
    Remarks:
    This position is the middle center point of the dimension text, which is itself a 
    middle-center justified MText entity.  
  */
  void setTextPosition(
    const OdGePoint3d& textPosition);

  /** Description:
      Returns true if and only if the dimension text for this Dimension entity
      is in the default position (DXF 70, bit 0x80 == 0).
  */
  bool isUsingDefaultTextPosition() const;

  /** Description:
    Sets the dimension text for this Dimension entity to not use the default position (DXF 70, sets bit 0x80).
  */
  void useSetTextPosition();

  /** Description:
    Sets the dimension text for this Dimension entity to use the default position (DXF 70, clears bit 0x80).
  */
  void useDefaultTextPosition();

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

  virtual bool isPlanar() const { return true; }

  virtual OdResult getPlane(
    OdGePlane& plane, 
    OdDb::Planarity& planarity) const;

  /** Description:
    Returns the *elevation* of this entity in the OCS (DXF 30).
    
    Remarks:
    The *elevation* is the *distance* from the WCS origin to the plane of this entity.
  */
  double elevation() const;

  /** Description:
    Sets the *elevation* of this entity in the OCS (DXF 30).

    Arguments:
    elevation (I) Elevation.    

    Remarks:
    The *elevation* is the *distance* from the WCS origin to the plane of this entity.
  */
  void setElevation(
    double elevation);

  /** Description:
    Returns the user-supplied dimension text for this Dimension Entity (DXF 1).

    Remarks:
    o If no text is to appear, "." will be returned.
    o If only the default text is to appear, "" will be returned.
    o The default text is represented as "<>" within the returned text.
  */
  const OdString dimensionText() const;

  /** Description:
    Sets the user-supplied dimension text for this Dimension Entity (DXF 1).
    
    Arguments:
    dimensionText (I) Dimension text.

    Remarks:
    o If no text is to appear, supply ".".
    o If only the default text is to appear, supply an empty string "".
    o The default text is represented as "<>" within the supplied text.
     
  */
  void setDimensionText(
    const OdChar* dimensionText);
    
  /** Description:
    Returns the rotation angle for the dimension text for this Dimension entity (DXF 53).

    Note:
    All angles are expressed in radians.
 */
  double textRotation() const;

  /** Description:
    Sets the rotation angle for the dimension text for this Dimension entity (DXF 53).
    
    Arguments:
    textRotation (I) Text rotation.
    Note:
    All angles are expressed in radians.
 */
  void setTextRotation(
    double textRotation);

  /** Description:
    Returns the Object ID of the dimension style (OdDbDimStyleTableRecord) for this Dimension entity (DXF 3).
  */
  OdDbObjectId dimensionStyle() const;

  /** Description:
    Sets the Object ID of the dimension style (OdDbDimStyleTableRecord) for this Dimension entity (DXF 3).
    Arguments:
    objectID (I) Object ID.
  */
  void setDimensionStyle(
    OdDbObjectId objectID);

  /** Description:
    Returns the dimension text attachment point for this Dimension entity (DXF 71).
  */
  OdDbMText::AttachmentPoint textAttachment() const;

  /** Description:
    Sets the dimension text attachment point for this Dimension entity (DXF 71).
    Arguments:
    attachmentPoint (I) Attachment Point.
  */
  void setTextAttachment(
    OdDbMText::AttachmentPoint attachmentPoint);

  /** Description:
    Returns the dimension text line spacing style for this Dimension entity (DXF 72).
  */
  OdDb::LineSpacingStyle textLineSpacingStyle() const;

  /** Description:
    Sets the dimension text line spacing style for this Dimension entity (DXF 72).
    Arguments:
    lineSpacingStyle (I) Line spacing style.
  */
  void setTextLineSpacingStyle(
    OdDb::LineSpacingStyle lineSpacingStyle);

  /** Description:
    Returns the dimension text line spacing factor for this Dimension entity (DXF 41).
    
    See also:
    OdDb::LineSpacingStyle
  */
  double textLineSpacingFactor() const;

  /** Description:
    Sets the dimension text line spacing factor for this Dimension entity (DXF 41).
    
    Arguments:
    lineSpacingFactor (I) Line spacing factor [0.25, 4.00]
    
    See also:
    OdDb::LineSpacingStyle
  */
  void setTextLineSpacingFactor(
    double lineSpacingFactor);

  /** Description:
    Copies the dimension style settings, including overrides, of this Dimension entity into the specified
    dimension style table record.
    
    Arguments:
    pRecord (O) Receives the effective dimension style data associated with entity.
    
    Remarks:
    The *copied* data includes the dimension style data with all applicable overrides. 
  */
  void getDimstyleData(
    OdDbDimStyleTableRecord *pRecord) const;

  /** Description:
    Copies the dimension style settings, including overrides, from the specified
    dimension style table record to this Dimension entity.
    
    Arguments:
    pDimstyle (I) Pointer to non- *database* -resident dimension style record.
    dimstyleID (I) Database-resident dimension style record.

    Remarks:
    The *copied* data includes the dimension style with all applicable overrides. 
  */
  void setDimstyleData(
    const OdDbDimStyleTableRecord* pDimstyle);
  void setDimstyleData(
    OdDbObjectId dimstyleID);

  /** Description:
    Returns the horizontal rotation angle for this Dimension entity (DXF 51).
    Note:
    All angles are expressed in radians.
  */
  double horizontalRotation() const;

  /** Description:
    Sets the horizontal rotation angle for this Dimension entity (DXF 51).
    Arguments:
    horizontalRotation (I) Horizontal rotation angle.
    Note:
    All angles are expressed in radians.
  */
  void setHorizontalRotation(
    double horizontalRotation);


  /** Description:
      Returns the Object ID of the dimension block (OdDbBlockTableRecord) associated with this Dimension entity (DXF 2).
  */
  OdDbObjectId dimBlockId() const;

  /** Description:
      Sets the Object ID of the dimension block (OdDbBlockTableRecord) associated with this Dimension entity.
   
  Arguments:
    dimBlockId (I) Object ID of the OdDbBlockTableRecord.
    singleReferenced (I) True if and only if dimBlockId is referenced only by this Dimension entity (DXF 70 bit 0x20).
  */
  void setDimBlockId(
    const OdDbObjectId& dimBlockId, 
    bool singleReferenced = true);

  /** Description:
    Returns true and only if this Dimension entity has the only reference to its 
    associated OdDbBlockTableRecord (DXF 70 bit 0x20).
  */
  bool isSingleDimBlockReference() const;

  /** Description:
    Returns the WCS relative position of the block associated with this Dimension entity (DXF 12).
      
    Remarks:
    dimBlockPosition is the insertion point of the block with respect to the primary
    definition point (DXF 10) of the dimension block.      
  */
  OdGePoint3d dimBlockPosition() const;

  /** Description:
    Sets the relative position of the dimension block referenced by this Dimension entity,
    in WCS (DXF 12). 
    
    Note:
    For DWGdirect internal use only. Dimension update sets it to 0,0,0
    
    Argument
    dimBlockPosition (I) Dimension block position.
    {Secret}
  */
  void setDimBlockPosition(
    const OdGePoint3d& dimBlockPosition);


  /** Description:
    Returns the rotation angle of the dimension block referenced by this Dimension entity (DXF 54).
    Note:
    All angles are expressed in radians.    
  */
  double dimBlockRotation() const;

  /** Description:
    Returns the scale of the dimension block referenced by this Dimension entity.
    
    Note:
    This is not saved to a DXF file.
  */
  OdGeScale3d dimBlockScale() const;

  /** Description:
    Returns the transformation matrix applied to dimension block referenced by this Dimension entity.
    
    Remarks
    The transformation consists of position, scale, rotation angle and *normal*.
    It does not include block's base point.
  */
  OdGeMatrix3d dimBlockTransform() const;
  
  /** Description:
    (Re)computes the dimension block referenced by this Dimension entity.
    
    Remarks:
    The OdDbBlockTableRecord of the dimension block is updated to reflect any changes made to this Dimension
    entity since the last time the block table recordit was updated.

    Arguments:
    forceUpdate (I) If true, the OdDbBlockTableRecord is updated even if the dimension has not been changed.
  */
  void recomputeDimBlock(
    bool forceUpdate = true);

  /** Description:
      Returns the measurement for this dimension (DXF 42).
  */
  double measurement() const;


#define VAR_DEF(a, b, dxf, c, d, r1, r2) \
/** Returns the dim##b value of this dimension entity. */ \
virtual a dim##b() const; \
/** Sets the dim##b value of this dimension entity. */ \
virtual void setDim##b(a);
#include "DimVarDefs.h"
#undef  VAR_DEF

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

  void getClassID(void** pClsid) const;

  OdUInt32 setAttributes(
    OdGiDrawableTraits* pTraits) const;

  bool worldDraw(
    OdGiWorldDraw* pWd) const;
    
  /* Replace OdRxObjectPtrArray */
  
  virtual OdResult explode(
    OdRxObjectPtrArray& entitySet) const; 

  void subClose();

  virtual void modified (
    const OdDbObject* pObject);

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);
  
  virtual OdResult getTransformedCopy(
    const OdGeMatrix3d& xfm, 
    OdDbEntityPtr& pCopy) const;

  /** Note:
     This function is an override for OdDbEntity::subSetDatabaseDefaults() to set 
     the dimension style of this entity to the current style for the specified *database*.
  */
  void subSetDatabaseDefaults(
    OdDbDatabase *pDb);

  void appendToOwner(
    OdDbIdPair& idPair, 
    OdDbObject* pOwnerObject, 
    OdDbIdMapping& ownerIdMap);
  /*
      void formatMeasurement(OdString MTextContentBuffer, double measurement,
                             OdString dimensionText );
      void generateLayout();
      void getOsnapPoints(OdDb::OsnapMode osnapMode, const OdDbFullSubentPath& subentId,
                          const OdGePoint3d& pickPoint, const OdGePoint3d& lastPoint,
                          const OdGeMatrix3d& viewXform, OdGePoint3dArray& snapPoints,
                          OdDbIntArray& geomIds ) const;
  */
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbDimension object pointers.
*/
typedef OdSmartPtr<OdDbDimension> OdDbDimensionPtr;

#include "DD_PackPop.h"

#endif


