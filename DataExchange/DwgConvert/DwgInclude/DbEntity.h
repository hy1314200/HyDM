///////////////////////////////////////////////////////////////////////////////
// Copyright ?2002, Open Design Alliance Inc. ("Open Design")
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
//      DWGdirect ?2002 by Open Design Alliance Inc. All rights reserved.
//
// By use of this software, you acknowledge and accept the terms of this
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef _ODDBENTITY_INCLUDED_
#define _ODDBENTITY_INCLUDED_ /** {Secret} */

#include "DD_PackPush.h"

#include "DbObject.h"
#include "Ge/GePoint3d.h"
#include "Ge/GeLine3d.h"
#include "Ge/GePlane.h"
#include "CmColor.h"
#include "IntArray.h"

class OdGePlane;
class OdGeMatrix3d;
class OdDbFullSubentPath;
class OdGePoint3d;
class OdGeVector3d;
class OdDbBlockTableRecord;
class OdGsView;

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbEntity pointers.
*/
typedef OdSmartPtr<OdDbEntity> OdDbEntityPtr;

/** Description:
  This template class is a specialization of the OdArray class for OdDbEntity object SmartPointers.
*/
typedef OdArray<OdDbEntityPtr> OdDbEntityPtrArray;

/** Description: 
  
    {group:DD_Namespaces}
*/
namespace OdDb
{
  enum SubentType
  {
    kNullSubentType   = 0,
    kFaceSubentType   = 1,
    kEdgeSubentType   = 2,
    kVertexSubentType = 3,
    kMlineSubentCache = 4
  };

  enum Intersect
  {
    kOnBothOperands    = 0,
    kExtendThis        = 1,
    kExtendArg         = 2,
    kExtendBoth        = 3
  };

  enum EntSaveAsType
  {
    kNoSave = 0,
    kSaveAsR12,
    kSaveAsR13,
    kSaveAsR14
  };

  enum OsnapMode
  {
    kOsModeEnd      = 1,  // Endpoint
    kOsModeMid      = 2,  // Midpoint
    kOsModeCen      = 3,  // Center
    kOsModeNode     = 4,  // Node
    kOsModeQuad     = 5,  // Quadrant
    kOsModeIntersec = 6,  // Intersection
    kOsModeIns      = 7,  // Insertion point
    kOsModePerp     = 8,  // Perpendicular
    kOsModeTan      = 9,  // Tangent
    kOsModeNear     = 10, // Nearest
    kOsModeApint    = 11, // Apparent intersection
    kOsModePar      = 12, // Parallel
    kOsModeStart    = 13  // Unknown 
  };

  enum Planarity
  {
    kNonPlanar = 0,
    kPlanar    = 1,
    kLinear    = 2
  };
}

/** Description:
    This class is the base class for all graphical objects contained in an OdDbDatabase instance.
    
    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbEntity : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbEntity);

  OdDbEntity();

  /** Description:
    Returns the Object ID of the OdOdDbBlockTableRecord that owns this entity.
  */
  OdDbObjectId blockId() const;

  /** Description:
    Returns the *color* information of this entity as an OdCmColor instance.
  */
  OdCmColor color() const;

  /** Description:
    Sets the *color* information of this entity from an OdCmColor instance.

    Arguments:
    color (I) OdCmColor object.
    doSubents (I) If and only if true, applies the change to all sub-entities owned by
                  this entity.
                    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  virtual OdResult setColor(
    const OdCmColor &color, 
    bool doSubents = true);

  /** Description:
    Returns the *color* index of this entity (DXF 62).
    
    Remarks:
    
    o The *color* index will be in the range [0..256].
    o 0 indicates a *color* of BYBLOCK.
    o 256 indicates a *color* of BYLAYER.
  */
  OdUInt16 colorIndex() const;

  /** Description:
    Returns the OdCmEntityColor settings of this object.
  */
  virtual OdCmEntityColor entityColor() const;

  /** Description:
    Sets the *color* index of this entity (DXF 62).

    Arguments:
    colorIndex (I) Color index.
    doSubents (I) If and only if true, applies the change to all sub-entities owned by
                  this entity.

    Remarks:
    Returns eOk if successful, or an appropriate error code if not.

    o The *color* index will be in the range [0..256].
    o 0 indicates a *color* of BYBLOCK.
    o 256 indicates a *color* of BYLAYER.
  */
  virtual OdResult setColorIndex(
    OdUInt16 colorIndex, 
    bool doSubents = true);

  /** Description:
      Returns the Object ID of the OdDbColor object referenced by this entity.
  */
  OdDbObjectId colorId() const;

  /** Description:
    Assigns the specified OdDbColor object to this entity.

    Arguments:
    colorId (I) Object ID of the OdDbColor object.
    doSubents (I) If and only if true, applies the change to all sub-entities owned by
                  this entity.
                    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  virtual OdResult setColorId(
    OdDbObjectId colorId, 
    bool doSubents = true);

  /** Description:
    Returns the *transparency* setting of this entity.
  */
  OdCmTransparency transparency() const;
  
  /** Description:
    Sets the *transparency* setting of this entity, and returns eOk if successful.

    Arguments:
    transparency (I) OdCmTransparency object be assigned.
    doSubents (I) If and only if true, applies the change to all sub-entities owned by
                  this entity.
                
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.

    If you override this function, you must call OdDbEntity::setTransparency(), passing to it the calling arguments.
  */
  virtual OdResult setTransparency(
    const OdCmTransparency& transparency, 
    bool doSubents = true);

  /** Description:
    Returns the name of the *plotStyleName* string associated with this entity (DXF 390).
  */
  OdString plotStyleName() const;

  /** Description:
    Returns the PlotStyleNameType and *plotStyleName* of this entity. 
    
    Arguments:
    id (O) Returns the Object ID OdDbPlaceHolder representing the *plotStyleName* of this entity.
  */
  OdDb::PlotStyleNameType getPlotStyleNameId(OdDbObjectId& id) const;

  /** Description:
    Sets the plot style of this entity.

    Arguments:
    plotStyleName (I) Name of the plot style.
    doSubents (I) If and only if true, applies the change to all sub-entities owned by
                  this entity.
                    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  virtual OdResult setPlotStyleName(
    const OdString& plotStyleName,
    bool doSubents = true);

  /** 
    Arguments:
    plotStyleNameType (I) Plot Style Name Type.
    plotStyleId (I) Object ID of the plot style.
    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.

    plotStyleId is used only when plotStyleNameType == kPlotStyleNameById.
    
    plotStyleNameType will be one of the following:
    
    @table
    Name                           Value                         
    kPlotStyleNameByLayer          0
    kPlotStyleNameByBlock          1
    kPlotStyleNameIsDiceDefault    2
    kPlotStyleNameById             3
  */
  virtual OdResult setPlotStyleName(
    OdDb::PlotStyleNameType plotStyleNameType,
    OdDbObjectId plotStyleId = OdDbObjectId::kNull, 
    bool doSubents = true);

  /** Description:
    Returns the name of the *layer* referenced by this entity (DXF 8).
  */
  OdString layer() const;

  /** Description:
    Returns the Object ID of the OdDbLayerTableRecord referenced by this entity.
  */
  OdDbObjectId layerId() const;

  /** Description:
    Sets the *layer* to be referenced by this entity (DXF 8).

    Arguments:
    layerName (I) Name of the *layer*.
    layerId (I) Object ID of the *layer*.
    doSubents (I) If and only if true, applies the change to all sub-entities owned by
                this entity.
                    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  virtual OdResult setLayer(
    const OdString& layerName, 
    bool doSubents = true);

  virtual OdResult setLayer(
    OdDbObjectId layerId,
     bool doSubents = true);


  /** Description:
    Returns the name of the *linetype* referenced by this entity (DXF 6).
  */
  OdString linetype() const;

  /** Description:
    Returns the Object ID of the *linetype* referenced by this entity (DXF 6).
  */
  OdDbObjectId linetypeId() const;

  /** Description:
    Sets the *linetype* to be referenced by this entity (DXF 6).

    Arguments:
    linetypeName (I) Name of the *linetype*.
    linetypeID (I) Object ID of the *linetype*.    
    doSubents (I) If and only if true, applies the change to all sub-entities owned by
                this entity.
                    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  virtual OdResult setLinetype(
    const OdString& linetypeName, 
    bool doSubents = true);
  virtual OdResult setLinetype(
    OdDbObjectId linetypeID, 
    bool doSubents = true);

  /** Description:
    Returns the *linetype* scale of this entity (DXF 48).
  */
  double linetypeScale() const;

  /** Description:
    Sets the Linetype scale of this entity (DXF 48).

    Arguments:
    linetypeScale (I) Linetype scale factor.
    doSubents (I) If and only if true, applies the change to all sub-entities owned by
                this entity.
                    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  virtual OdResult setLinetypeScale(
    double linetypeScale, 
    bool doSubents = true);

  /** Description:
    Returns the *visibility* status of this entity (DXF 60).
    
    Remarks:
    visibility will return one of the following:
    
    @table
    Name           Value
    kInvisible     1
    kVisible       0
  */
  OdDb::Visibility visibility() const;

  /** Description:
    Sets the *visibility* status of this entity (DXF 60).

    Arguments:
    visibility (I) Visibility status.
    doSubents (I) If and only if true, applies the change to all sub-entities owned by
                this entity.
                
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.

    visibility will be one of the following:
    
    @table
    Name           Value
    kInvisible     1
    kVisible       0
  */
  virtual OdResult setVisibility(
    OdDb::Visibility visibility, 
    bool doSubents = true);

  /** Description:
    Returns the LineWeight property of this entity (DXF 370).
  */
  OdDb::LineWeight lineWeight() const;

  /** Description:
    Sets the LineWeight property of this entity (DXF 370).

    Arguments:
    lineWeight (I) LineWeight.
    doSubents (I) If and only if true, applies the change to all sub-entities owned by
                this entity.
                    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  virtual OdResult setLineWeight(
    OdDb::LineWeight lineWeight, 
    bool doSubents = true);

  /** Description:
    Copies the properties from the specified entity to this one.
    Arguments:
    pSource (I) Pointer to the source entity.
    doSubents (I) If and only if true, applies the change to all sub-entities owned by
                this entity.
  */
  void setPropertiesFrom(
    const OdDbEntity* pSource, 
    bool doSubents = true);

  /** Description:
    Returns true if and only if this entity is planar.
  */
  virtual bool isPlanar() const;

  /** Description:
    Returns the *plane* that contains this entity.

    Arguments:
    plane (O)  Returns the *plane* that contains this entity.
    planarity (O) Returns the *planarity* of this entity.

    Remarks:
    Returns eOk if successful, or an appropriate error code if not.

    planarity and plane return values as follows::
    
    @table
    planarity     Value    Description    plane      
    kNonPlanar    0        Non-planar     Not set
    kPlanar       1        Planar         Entity *plane*
    kLinear       2        Linear         Arrbitrary *plane* containing this entity
  */
  virtual OdResult getPlane(
    OdGePlane& plane, 
    OdDb::Planarity& planarity) const;

  /** Description:
    Returns the WCS geometric *extents* of this entity.

    Arguments:
    extents (O) Receives the *extents*.

    Remarks:
    Returns eOk if successful, or eInvalidExtents if not.

    The *extents* are the WCS corner points of a box, aligned with the 
    WCS axes, that encloses the 3D *extents* of this entity.  
  */
  virtual OdResult getGeomExtents(
    OdGeExtents3d& extents) const;

  void subHandOverTo(
    OdDbObject* newObject);
    
  /**
    Description:
    Applies the 3D transformation matrix to this entity.

    Arguments:
    xfm (I) 3D transformation matrix.

    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);

  /** Description:
    Creates a copy of this entity, and applies the supplied transformation
    to the newly created copy.

    Arguments:
    xfm (I) 3D transformation matrix.
    pCopy (O) Returns a SmartPointer to the newly created copy.

    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  virtual OdResult getTransformedCopy(
    const OdGeMatrix3d& xfm, 
    OdDbEntityPtr& pCopy) const;

  /** Description:
    Explodes this entity into a set of simpler entities.  

    Arguments:
    entitySet (I/O) Returns an array of pointers to the new entities.
    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.

    Note:
    Entities resulting from the explosion are appended to the specified array.
    
    The newly created entities are not *database* resident.
    
    The default implementation of this function returns eNotApplicable. This function can be
    overridden in custom classes.
  */
  virtual OdResult explode(
    OdRxObjectPtrArray& entitySet) const; /* Replace OdRxObjectPtrArray */

  /** Description:
    Explodes this entity into a set of simpler entities, and adds them to the specified block table record.

    Arguments:
    pBlockRecord (I) Pointer to the BlockTable record.
    ids (I/O) Returns array of Object IDs of the new entities.
    
    Note:
    Entities resulting from the explosion are appended to the specified array.
    
    The newly created entities are *database* resident or not depending on the block table record
    they are appended to. If block table record is *database* resident the entities are *database*
    resident as well. If block table record is not *database* resident newly created entities are not
    *database* resident.
    
    The default implementation of this function returns eNotApplicable. This function can be
    overridden in custom classes.
  */
  virtual OdResult explodeToBlock(
    OdDbBlockTableRecord *pBlockRecord,
    OdDbObjectIdArray *ids = NULL);

  /** Description:
    Explodes this entity into a set of simpler entities.  

    Arguments:
    entitySet (I/O) Returns an array of pointers to the new entities.
    
    Remarks:
    The newly created entities will be not *database* resident.

    Returns eOk if successful, or an appropriate error code if not.

    Note:
    Entities resulting from the explosion are appended to the specified array.
    
    
    The default implementation of this function calls worldDraw() and makes
    entities from geometry generated by worldDraw(). This function can be
    overridden in custom classes.
  */
  virtual OdResult explodeGeometry(
    OdRxObjectPtrArray& entitySet) const;

  /** Description:
    Explodes this entity into a set of simpler entities, and adds them to the specified block.

    Arguments:
    pBlockRecord (I) Pointer to the BlockTable record.
    ids (I/O) Returns array of Object IDs of the new entities.
    
    Remarks:
    The newly created entities are *database* resident or not depending on the block table record
    they are appended to. If block table record is *database* resident the entities are *database*
    resident too. If block table record is not *database* resident newly created entities are not
    *database* resident.

    Note:
    Entities resulting from the explosion are appended to the specified array.
    
    
    The default implementation of this function calls worldDraw() and makes
    entities from geometry generated by worldDraw(). This function can be
    overridden in custom classes.
  */
  virtual OdResult explodeGeometryToBlock(
    OdDbBlockTableRecord *pBlockRecord,
    OdDbObjectIdArray *ids = NULL);

  virtual bool worldDraw(
    OdGiWorldDraw* pWd) const;

  virtual void viewportDraw(
    OdGiViewportDraw* pVd) const;

  /** Description:
    Applies the default properties of the specified *database* to this entity.
    
    Arguments:
    pDb (I) Pointer to the *database* whose default values are to be used.
     
    Remarks:
    If pDb is NULL, the *database* containing this entity is used.
    
    The following properties are set
    
    o *color*
    o *layer*
    o *linetype*
    o *linetype* scale
    o *lineweight*
    o *plotstyle*
    o *visibility*
  */
  void setDatabaseDefaults(
  OdDbDatabase* pDb = NULL);

  /** Description:
    Called by setDatabaseDefaults() after the values are set.
    
    Arguments:
    pDb (I) Pointer to the *database* whose default values are to be used.

    Remarks:
    If pDb is NULL, the *database* containing this entity is used.
   
    This function allows custom classes to inspect and modify the values set by setDatabaseDefaults.
      
    The default implementation of this function returns eNotApplicable. This function can be
    overridden in custom classes.
    
    Note:
    This function is not intended to be called by the user.
  */
  virtual void subSetDatabaseDefaults(
    OdDbDatabase* pDb = NULL);

  virtual void applyPartialUndo(
    OdDbDwgFiler* pUndoFiler, 
    OdRxClass* pClassObj);

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  void appendToOwner(
    OdDbIdPair& idPair, 
    OdDbObject* pOwnerObject, 
    OdDbIdMapping& ownerIdMap);

  virtual void dxfOut (
    OdDbDxfFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult dxfInFields_R12(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields_R12(
    OdDbDxfFiler* pFiler) const;

  virtual OdUInt32 setAttributes(
    OdGiDrawableTraits* pTraits) const;

  virtual void getClassID(void** pClsid) const;

  /** Description:
    This function is called as the first operation of the swapIdWith() function.  
    
    Arguments:
    otherId (I) Object ID of object with which to swap.
    swapXdata (I) XData will be swaped if and only if swapXData is true.
    swapExtDict (I) Extension dictionaries will be swapped if and only if swapExtDict is true.

    Remarks:
    This function allows derived classes
    to implement custom behavior during the swapIdWith operation.

    The default implementation of this function does nothing. This function can be
    overridden in custom classes.


  
    See Also:
    OdDbObject::swapIdWith
  */
  void subSwapIdWith(
    const OdDbObjectId& otherId, 
    bool swapXdata = false, 
    bool swapExtDict = false);

  virtual OdResult subErase(
    bool erasing);

  virtual void subOpen(
    OdDb::OpenMode mode);

  /** Description:
    Sets the bit flag indicating the entity's geometry is changed.
    
    Arguments:
    graphicsModified (I) New value.
    
    Remarks:
    If true, assures that modifiedGraphics() will be called as the entity is being closed, even 
    if the object was not opened for write.
  */
  void recordGraphicsModified(
    bool graphicsModified = true);

  virtual void copyFrom(
    const OdRxObject* pSource);

  virtual OdDbObjectPtr wblockClone(
    OdDbIdMapping& ownerIddMap) const;

  virtual OdDbObjectPtr deepClone(
    OdDbIdMapping& ownerIdMap) const;

/* No implementation
  Intersects this entity with pEnt if possible.  inType determines type of
      intersection to be calculated
    virtual void intersectWith(const OdDbEntity* pEnt,
    OdDb::Intersect intType, OdGePoint3dArray& points,
    int thisGsMarker = 0, int otherGsMarker = 0) const;

  virtual void intersectWith(const OdDbEntity* pEnt, OdDb::Intersect intType,
    const OdGePlane& projPlane, OdGePoint3dArray& points,
    int thisGsMarker = 0, int otherGsMarker = 0) const;

  This method is triggered by the standard LIST command and is to be display
      the dxf format contents of the entity to the display.
  virtual void list() const;

  Cause this entity, and any other entity who's draw bit is set, to be be drawn.
  void draw();

  Uses the bounding box of this object to determine an intersection
      array of points.
  void boundingBoxIntersectWith(const OdDbEntity* pEnt,
    OdDb::Intersect intType, OdGePoint3dArray& points,
    int thisGsMarker, int otherGsMarker) const;

  void boundingBoxIntersectWith(const OdDbEntity* pEnt, OdDb::Intersect intType,
    const OdGePlane& projPlane, OdGePoint3dArray& points,
    int thisGsMarker, int otherGsMarker) const;


  */

  /*
  virtual void getCompoundObjectTransform(OdGeMatrix3d & xMat) const;
  virtual void getSubentPathsAtGsMarker(OdDb::SubentType type,
  int gsMark, const OdGePoint3d& pickPoint,
  const OdGeMatrix3d& viewXform, int& numPaths,
  OdDbFullSubentPath** subentPaths, int numInserts = 0,
  OdDbObjectId* entAndInsertStack = NULL) const;

  virtual void getGsMarkersAtSubentPath(const OdDbFullSubentPath& subPath, OdDbIntArray& gsMarkers) const;

  virtual OdDbEntity* subentPtr(const OdDbFullSubentPath& id) const;


  virtual void highlight(const OdDbFullSubentPath& subId = OdDb::kNullSubent,
    const bool highlightAll = false) const;

  virtual void unhighlight(const OdDbFullSubentPath& subId = kNullSubent,
    const bool highlightAll = false) const;

  virtual void audit(OdDbAuditInfo* pAuditInfo);
  virtual OdGiDrawable* drawable();

  virtual void setGsNode(OdGsNode* pNode);
  virtual OdGsNode* gsNode() const;

  virtual bool cloneMeForDragging();

  virtual void saveAs(OdGiWorldDraw* mode, OdDb::EntSaveAsType st);

  */
  
  /** Description:
    Returns all appropriate object snap points of this entity.
    
    Arguments:
    osnapMode (I) The object snap mode being queried.
    gsSelectionMark (I) The GS marker of the subentity being queried.
    pickPoint (I) The WCS point being queried.
    lastPoint (I) The WCS point picked before pickPoint.
    viewXform (I) The WCS->DCS transformation matrix.
    ucs (I) The WCS->UCS transformation matrix.
    snapPoints (I/O) Returns an array of UCS object snap points.
    
    Remarks:
    Object snap points are appended to the specified array.
    
    osnapMode will be one of the following:
    
    @table
    Name                      Value   Description 
    OdDb::kOsModeEnd          1       Endpoint
    OdDb::kOsModeMid          2       Midpoint
    OdDb::kOsModeCen          3       Center
    OdDb::kOsModeNode         4       Node
    OdDb::kOsModeQuad         5       Quadrant
    OdDb::kOsModeIntersec     6       Intersection
    OdDb::kOsModeIns          7       Insertion point
    OdDb::kOsModePerp         8       Perpendicular
    OdDb::kOsModeTan          9       Tangent
    OdDb::kOsModeNear         10      Nearest
    OdDb::kOsModeApint        11      Apparent intersection
    OdDb::kOsModePar          12      Parallel
    OdDb::kOsModeStart        13      Unknown 
   
  */  
  virtual OdResult getOsnapPoints(
    OdDb::OsnapMode osnapMode, 
    int gsSelectionMark, 
    const OdGePoint3d& pickPoint,
    const OdGePoint3d& lastPoint, 
    const OdGeMatrix3d& viewXform, 
    const OdGeMatrix3d& ucs, 
    OdGePoint3dArray& snapPoints ) const;

  /** Description:
    Returns all grip points of this entity.

    Arguments:
    gripPoints (I/O) Returns an array of WCS grip points.

    Remarks:
    Grip points are appended to the specified array.
  */
  virtual OdResult getGripPoints(
    OdGePoint3dArray& gripPoints ) const;
    
  /**
    Description:
    Moves the specified grip points of this entity.
    
    Arguments:
    gripPoints (I) Array of moved grip points.
    indices (I) Array of indicies.
    
    Remarks:
    Each element in gripPoints has a corresponding entry in indices, which specifies the index of 
    the grip point as returned by getGripPoints.
  */  
  virtual OdResult moveGripPointsAt( 
    const OdGePoint3dArray& gripPoints, 
    const OdIntArray& indices );
    
  /** Description:
    Returns all stretch points of this entity.

    Arguments:
    stretchPoints (I/O) Returns an array of WCS stretch points.

    Remarks:
    Stretch points are appended to the specified array.
  */
  virtual OdResult getStretchPoints( 
    OdGePoint3dArray& stretchPoints ) const;
    
  /**
    Description:
    Moves the specified stretch points of this entity.
    
    Arguments:
    stretchPoints (I) Array of moved grip points.
    indices (I) Array of indicies.
    
    Remarks:
    Each element in stretchPoints has a corresponding entry in indices, which specifies the index of 
    the stretch point as returned by getStretchPoints.
  */  
  virtual OdResult moveStretchPointsAt(
    const OdGePoint3dArray& stretchPoints, 
    const OdIntArray& indices );
};

#include "DD_PackPop.h"

#endif /* _ODDBENTITY_INCLUDED_ */


