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



#ifndef _OD_BLOCK_REFERENCE_
#define _OD_BLOCK_REFERENCE_

#include "DD_PackPush.h"

#include "DbEntity.h"
#include "DbDatabase.h"
#include "Ge/GeMatrix3d.h"
#include "DbObjectIterator.h"

class OdGeScale3d;
class OdDbAttribute;
class OdDbSequenceEnd;
class OdDbBlockReferenceImpl;
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbAttribute object pointers.
*/
typedef OdSmartPtr<OdDbAttribute> OdDbAttributePtr;

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbSequenceEnd object pointers.
*/
typedef OdSmartPtr<OdDbSequenceEnd> OdDbSequenceEndPtr;

/** Description:
    This class represents block references (Inserts) in an OdDbDatabase instance.

    Library:
    Db

    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDbBlockReference : public OdDbEntity
{
public:
  ODDB_DECLARE_MEMBERS(OdDbBlockReference);

  OdDbObjectPtr wblockClone(
    OdDbIdMapping& ownerIddMap) const;

  OdDbBlockReference();
  
  /** Description:
    Returns the Object ID of the block table record associated with this OdDbBlockReference (DXF 2).
  */
  OdDbObjectId blockTableRecord() const;

  /** Description:
    Sets the Object ID of the block table record associated with this OdDbBlockReference (DXF 2).
    
    Arguments:
    objectId (I) Object ID of the block.
  */
  virtual void setBlockTableRecord(
    OdDbObjectId objectId);

  /** Description:
    Returns the insertion point of this block reference (WCS equivalent of DXF 10).
  */
  OdGePoint3d position() const;

  /** Description:
    Sets the insertion point of this block reference. (WCS equivalent of DXF 10.)

    Arguments:
    position (I) Any 3D point.
  */
  virtual void setPosition(
    const OdGePoint3d& position);

  /** Description:
    Returns the *scale* factors applied to this block reference (DXF 41, 42, 43).
  */
  OdGeScale3d scaleFactors() const;

  /** Description:
    Sets the scale factors to be applied to this block reference (DXF 41, 42, 43).

    Arguments:
    scale (I) Any 3D *scale* factor.
    
    Throws:
    @table
    Exception             Cause
    eInvalidInput         One or more *scale* factors is 0
  */
  virtual void setScaleFactors(
    const OdGeScale3d& scale);

  /** Description:
    Returns the *rotation* *angle* applied to this block reference (DXF 50).

    Remarks:
    Rotation is about the Z axis, relative the X-axis, in the coordinate system parallel to
    this object's OCS, but with its origin at this object's insertion point.

    Note:
    All angles are expressed in radians.
  */
  double rotation() const;

  /** Description:
    Sets the *rotation* *angle* to be applied to this block reference, in radians (DXF 50).

    Remarks:
    Rotation is about the Z axis, relative the X-axis, in the coordinate system parallel to
    this object's OCS, but with its origin at this object's insertion point.

    Arguments:
    angle (I) Rotation *angle*.
    
    Note:
    All angles are expressed in radians.
  */
    virtual void setRotation(
    double angle);

  /** Description:
    Returns the WCS *normal* to the plane of this entity (DXF 210).
  */
  OdGeVector3d normal() const;
  
  /** Description:
    Sets the WCS *normal* to the plane of this entity (DXF 210).
    Arguments:
    normal (I) Normal.
  */
  virtual void setNormal(
    const OdGeVector3d& normal);

  virtual bool isPlanar() const { return true; }

  virtual OdResult getPlane(
    OdGePlane& plane, 
    OdDb::Planarity& planarity) const;
  
  /** Description:
    Returns the transformation matrix mapping a point in the MCS
    to the WCS.
    
    Remarks:
    The Microspace Coordinate System (MCS) is the WCS within the block definition.
    Applying this matrix to the center of an arc within the block definition
    will return the center of the arc as it appears in the block reference.
  */
  OdGeMatrix3d blockTransform() const;

  /** Description:
    Sets the transformation matrix mapping a point in the MCS
    to the WCS.
    
    Arguments:
    xfm (I) Any 3D transformation matrix
    
    Remarks:
    The Microspace Coordinate System (MCS) is the WCS within the block definition.
    Applying this matrix to the center of an arc within the block definition
    will return the center of the arc as it appears in the block reference.
  */
  virtual OdResult setBlockTransform(
    const OdGeMatrix3d& xfm);

  /** Description:
    Appends the specified OdDbAttribute to the attribute list of this block reference.

    Arguments:
    pAttr (I) Pointer to the attribute to be added.

    Remarks:
    Returns the Object ID of the newly appended attribute.

    Note:
    This block reference becomes the owner of the passed in attribute, and the attribute
    is added to the *database* to which this block reference belongs.  
    
    This block reference must be added to a *database* before calling this function.
    
    The object's attribute list should not be added by the client application.
  */
  OdDbObjectId appendAttribute(
    OdDbAttribute* pAttr);

  /** Description:
    Opens an attribute owned by this block reference.

    Arguments:
    objId (I) Object ID of the attribute to be opened.
    mode (I) Mode in which the attribute is to be opened.
    openErasedOne (I) If and only if true, *erased* objects will be opened.

    Remarks:
    Returns a SmartPointer to the opened object if successful, otherwise a null SmartPointer.
  */
  OdDbAttributePtr openAttribute(
    OdDbObjectId ObjId,
    OdDb::OpenMode mode, 
    bool openErasedOne = false);

  /** Description:
    Opens the OdDbSequenceEnd entity for this block reference.

    Arguments:
    mode (I) Mode in which to open the OdDbSequenceEnd entity.

    Remarks:
    Returns a SmartPointer to the newly opened OdDbSequenceEnd, or a null SmartPointer.

    Note:
    This method is provided solely for applications that store XData on
    OdDbSequenceEnd entities; this is not recommended. 
  */
  OdDbSequenceEndPtr openSequenceEnd(
    OdDb::OpenMode mode);

  /** Description:
    Returns a SmartPointer to an attribute iterator for this block reference.
  */
  OdDbObjectIteratorPtr attributeIterator() const;

  virtual OdResult explodeToBlock(
    OdDbBlockTableRecord *BlockRecord, 
    OdDbObjectIdArray *ids = NULL);

  OdUInt32 setAttributes(
    OdGiDrawableTraits* pTraits) const;

  virtual bool worldDraw(
    OdGiWorldDraw* pWd) const;

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult dxfInFields_R12(OdDbDxfFiler* pFiler);

  virtual void dxfOutFields_R12(
  OdDbDxfFiler* pFiler) const;

  virtual OdResult dxfIn(
    OdDbDxfFiler* pFiler);

  virtual void dxfOut (
    OdDbDxfFiler* pFiler) const;

  void subClose();

  virtual void getClassID(
    void** pClsid) const;
  
  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);

  virtual OdResult getTransformedCopy(
    const OdGeMatrix3d& xfm, 
    OdDbEntityPtr& pCopy) const;

 /* Replace OdRxObjectPtrArray */
 
  virtual OdResult explode(
    OdRxObjectPtrArray& entitySet) const;

  void subHandOverTo(
    OdDbObject* newObject);

  OdResult getGeomExtents(
    OdGeExtents3d& extents) const;

  /*
  OdDbBlockReference(const OdGePoint3d& position, OdDbObjectId blockTableRec);
  virtual bool treatAsOdDbBlockRefForExplode() const;
  void geomExtentsBestFit(OdGeExtents& extents,
                          const OdGeMatrix3d& parentXform = OdGeMatrix3d::kIdentity) const;
  */
};
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbBlockReference object pointers.
*/
typedef OdSmartPtr<OdDbBlockReference> OdDbBlockReferencePtr;

#include "DD_PackPop.h"

#endif


