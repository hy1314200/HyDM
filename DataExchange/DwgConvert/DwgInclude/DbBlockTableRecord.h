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



#ifndef _ODDBBLOCKTABLERECORD_INCLUDED
#define _ODDBBLOCKTABLERECORD_INCLUDED

#include "DD_PackPush.h"

#include "DbSymbolTableRecord.h"
#include "DbObjectIterator.h"
#include "DbXrefGraph.h"

class OdDbBlockBegin;
class OdDbBlockEnd;
class OdDbBlockTableRecord;
class OdDbBlockTable;
class OdBinaryData;
class OdDbSortentsTable;
typedef OdSmartPtr<OdDbBlockBegin> OdDbBlockBeginPtr;
typedef OdSmartPtr<OdDbBlockEnd> OdDbBlockEndPtr;
typedef OdSmartPtr<OdDbSortentsTable> OdDbSortentsTablePtr;

/** Description:
    This class represents Block records in the OdDbBlockTable in an OdDbDatabase instance.

    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbBlockTableRecord : public OdDbSymbolTableRecord
{
public:

  ODDB_DECLARE_MEMBERS(OdDbBlockTableRecord);

  OdDbBlockTableRecord();

  typedef OdDbBlockTable TableType;
  typedef OdBinaryData PreviewIcon;

  /** Description:
    Appends the specified entity to this Block.

    Arguments:
    pEntity (I) Pointer to the entity.

    Remarks:
    Returns the Object ID of the newly appended entity.
  */
  OdDbObjectId appendOdDbEntity(
    OdDbEntity* pEntity);

  /** Description:
    Returns an iterator that can be used to traverse this Block.

    Arguments:
    atBeginning (I) True to start at the beginning, false to start at the end. 
    skipDeleted (I) If and only if true, deleted records are skipped.
    sorted (I) If and only if true, the iterator will traverse the Block as *sorted* by this Block's SortentsTable.
  */
  OdDbObjectIteratorPtr newIterator(
    bool atBeginning = true, 
    bool skipDeleted = true, 
    bool sorted = false) const;

  /** Description:
    Returns the description text associated with this Block (DXF 4).
  */
  OdString comments() const;

  /** Description:
    Sets the description text associated with this Block (DXF 4).
    Arguments:
    comments (I) Description text.
  */
  void setComments(
    const OdString& comments);

  /** Description:
    Returns the path and file name for the Xref drawing (DXF 1).
    
    Remarks:
    Returns an empty string for non-Xref blocks.
  */
  OdString pathName() const;

  /** Description:
    Sets the path and file name for the Xref drawing (DXF 1).
    Arguments:
    pathName (I) Path and file name.
  */
  void setPathName(
    const OdString& pathName);

  /** Description:
    Returns the WCS *origin* of this Block (DXF 10).
  */
  OdGePoint3d origin() const;

  /** Description:
    Sets the WCS *origin* of this Block (DXF 10).
    Arguments:
    origin (I) Origin.
  */
  void setOrigin(
    const OdGePoint3d& origin);

  /** Description:
    Opens and returns the OdDbBlockBegin object associated with this Block.

    Arguments:
    openMode (I) Mode in which to open the object.

    Remarks:
    This function allows DWGdirect applications to access the OdDbBlockBegin object 
    to store and retrieve Xdata in a manner compatible with AutoLISP applications.
  */
  OdDbBlockBeginPtr openBlockBegin(
    OdDb::OpenMode openMode = OdDb::kForRead);

  /** Description:
    Opens and returns the OdDbBlockEnd object associated with this Block.

    Arguments:
    openMode (I) Mode in which to open the object.

    Remarks:
    This function allows DWGdirect applications to access the OdDbBlockEnd object 
    to store and retrieve Xdata in a manner compatible with AutoLISP applications.
  */
  OdDbBlockEndPtr openBlockEnd(OdDb::OpenMode openMode = OdDb::kForRead);

  /** Description:
    Returns true if and only if this Block contains Attribute definitions.

    See Also:
    OdDbAttributeDefinition
  */
  bool hasAttributeDefinitions() const;

  /** Description:
    Returns true if and only if this Block is anonymous (DXF 70, bit 0x01).
  */
  bool isAnonymous() const;

  /** Description:
    Returns true if and only if this Block is an Xref (DXF 70, bit 0x04).
  */
  bool isFromExternalReference() const;

  /** Description:
    Returns true if and only if this Block is an overlaid Xref (DXF 70, bit 0x08).
  */
  bool isFromOverlayReference() const;

  /** Description:
    Returns true if and only if this Block represents a layout.
  */
  bool isLayout() const;

  /** Description:
    Returns the Object ID of the OdDbLayout associated with this Block.
  */
  OdDbObjectId getLayoutId() const;

  /** Description:
    Sets the Object ID of the OdDbLayout associated with this Block.
    
    Arguments:
    layoutId (I) Layout ID.
  */
  void setLayoutId(
    const OdDbObjectId& layoutId);

  /** Description:
    Returns the Object ID's of all OdDbBlockReference entities that reference this Block.

    Arguments:
    referenceIds (O) Receives the BlockReference Object ID's.
    directOnly (I) If true, returns only direct references.
    forceValidity (I) If true, forces the loading of partially loaded drawings.

    Remarks:
    If this Block is nested, references to the parent block(s) will be included if and only if directOnly is false.
    
    Older drawings do not explcitly store this information, and hence must be completely loaded.

    See also:
    getErasedBlockReferenceIds
  */
  void getBlockReferenceIds(
    OdDbObjectIdArray& referenceIds,
    bool directOnly = true, 
    bool forceValidity = false);

  /** Description:
    Returns the Object ID's of all *erased* OdDbBlockReference entities that directly reference this Block.

    Arguments:
    referenceIds (O) Receives the BlockReference Object ID's.

    See also:
    getBlockReferenceIds
  */
  void getErasedBlockReferenceIds(
    OdDbObjectIdArray& referenceIds);

  /** Description:
    Returns true if and only if this Xref is unloaded (DXF 71).
  */
  bool isUnloaded() const;

  /** Description:
    Sets the unloaded status of this xref (DXF 71).

    Arguments:
    isUnloaded (I) Unloaded status.
  */
  void setIsUnloaded(
    bool isUnloaded);

  bool worldDraw(
    OdGiWorldDraw* pWd) const;

  OdUInt32 setAttributes(
    OdGiDrawableTraits* pTraits) const;

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual void subClose();

  virtual void getClassID(
    void** pClsid) const;

  OdResult subErase(
    bool erasing);

  void subHandOverTo(
    OdDbObject* newObject);
  
  /** Description:
    Returns the *database* that defines this Xref.
    Arguments:
    includeUnresolved (I) Include unresolved Xrefs.
  */
  OdDbDatabase* xrefDatabase(
    bool includeUnresolved = false) const;

  /** Description:
    Returns the Xref status of this Block.
    Remarks:
    xrefStatus will return one of the following:
    
    @table
    Name                Value   Description
    kXrfNotAnXref       0       Not an Xref
    kXrfResolved        1       Resolved
    kXrfUnloaded        2       Unloaded
    kXrfUnreferenced    3       Unreferenced
    kXrfFileNotFound    4       File Not Found
    kXrfUnresolved      5       Unresolved
    
  */
  OdDb::XrefStatus xrefStatus() const;

  /** Description:
    Returns true if and only if this Block has a preview icon.
  */
  bool hasPreviewIcon() const;

  /** Description:
    Returns the preview icon associated with this Block.
    Arguments:
    previewIcon (O) Receives the Preview icon.
  */
  void getPreviewIcon(
    PreviewIcon &previewIcon) const;

  /** Description:
    Sets the preview icon associated with this Block.
    Arguments:
    previewIcon (I) Preview icon.
  */
  void setPreviewIcon(
    const PreviewIcon &previewIcon);

  OdDbObjectPtr wblockClone(
    OdDbIdMapping& ownerIdMap) const;

  /** Description:
    Returns the SortentsTable associated with this block.

    Arguments:
    createIfNotFound (I) Create the SortentsTable if not found. 

    Note:
    The SortentsTable is opened for write.
    
    See also:
    OdDbSortentsTable
  */
  OdDbSortentsTablePtr getSortentsTable(
    bool createIfNotFound = true);

  /*
      virtual void assumeOwnershipOf(OdArray<OdDbObjectId> entitiesToMove);  
      OdGiDrawable* drawable();
      void viewportDraw(OdGiViewportDraw* pVd);
      void setGsNode(OdGsNode* pNode);
      OdGsNode* gsNode() const;
  */
  
  /** Description:
    Returns the WCS geometric *extents* of this object.

    Arguments:
    extents (O) Receives the *extents*.

    Remarks:
    Returns eOk if successful, or eInvalidExtents if not.

    The *extents* are the WCS corner points of a box, aligned with the 
    WCS axes, that encloses the 3D *extents* of this Block. 
  */
  virtual OdResult getGeomExtents(
    OdGeExtents3d& extents) const ;

};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbBlockTableRecord object pointers.
*/
typedef OdSmartPtr<OdDbBlockTableRecord> OdDbBlockTableRecordPtr;

#include "DD_PackPop.h"

#endif 


