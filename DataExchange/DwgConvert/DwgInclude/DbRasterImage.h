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



#ifndef __IMGENT_H
#define __IMGENT_H /* {Secret} */

#include "DD_PackPush.h"

#include "DbProxyEntity.h"
#include "Ge/GePoint2dArray.h"

//const double kEpsilon = 1.0e-7;

const int kAllEntityProxyFlags =
OdDbProxyEntity::kEraseAllowed |
OdDbProxyEntity::kTransformAllowed |
OdDbProxyEntity::kColorChangeAllowed |
OdDbProxyEntity::kLayerChangeAllowed |
OdDbProxyEntity::kLinetypeChangeAllowed |
OdDbProxyEntity::kLinetypeScaleChangeAllowed |
OdDbProxyEntity::kVisibilityChangeAllowed;

class OdRasterImageImpl;

#include "DbImage.h"
#include "DbRasterImageDef.h"

/** Description:
    This class represents raster image entities in an OdDbDatabase instance.

    Library:
    Db
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbRasterImage : public OdDbImage
{
public:
  ODDB_DECLARE_MEMBERS(OdDbRasterImage);

  OdDbRasterImage();

  enum ClipBoundaryType
  {
    kInvalid, // Invalid
    kRect,    // Rectangular
    kPoly     // Polygonal
  };

  enum ImageDisplayOpt
  {
    kShow          = 1,   // Show
    kShowUnAligned = 2,   // Show unaligned
    kClip          = 4,   // Clip
    kTransparent   = 8    // Transparent
  };

  /** Description:
    Sets the Object ID of the OdDbRasterImageDef object associated with this raster image entity (DXF 340).
    Arguments:
    imageDefId (I) Object ID of image to be displayed.
  */
  void setImageDefId(
    OdDbObjectId imageDefId);

  /** Description:
    Returns the Object ID of the OdDbRasterImageDef object associated with this raster image entity (DXF 340).
  */
  OdDbObjectId imageDefId() const;

  /** Description:
    Sets the OdDbRasterImageDefReactor object associated with this raster image entity (DXF 360).
    Arguments:
    reactorId (I) Object ID of reactor for this image.
    
    Remarks:
    Each OdDbRasterImage object must have an OdDbRasterImageDefReactor associated with the OdDbRasterImageDef
    object it is using. This call makes this OdDbRasterImage object the owner of the reactor object.
  */
  void setReactorId(
    OdDbObjectId reactorId);

  /** Description:
    Returns the Object ID of the OdDbRasterImageDefReactor object associated with this raster image entity (DXF 360).

    Remarks:
    Each OdDbRasterImage object must have an OdDbRasterImageDefReactor associated with the OdDbRasterImageDef
    object it is using.
  */
  OdDbObjectId reactorId() const;

  /** Description:
    Sets the clip boundary for this raster image entity (DXF 71, 14, 24).

    Arguments:
    clipPoints (I) Array of 2D points in pixel coordinates.
  */
  void setClipBoundary(
    const OdGePoint2dArray& clipPoints);

  /** Description:
    Returns the clip boundary for this raster image entity (DXF 14, 24).
    Remarks:
    The clip boundary is specified in pixel coordinates.
  */
  const OdGePoint2dArray& clipBoundary() const;

  /** Description:
    Returns the clipping state for this raster image entity (DXF 280).
  */
  bool isClipped() const;

  /** Description:
    Sets the clipping state for this raster image entity (DXF 280).
    
    Arguments:
    enable (I) True to *enable* clipping, false to *disable*.
.
  */
  void setClipped( 
    bool enable);

  /** Description:
    Adds the frame *vertices* of this raster image entity to the specified array.
    
    Arguments:
    vertices (I/O) Receives the vertices of the image frame.

    Remarks:
    If isClipped(), these are the *vertices* of the clip boundary. 
    
    If !isClipped(), these are the corners of the image.
  */
  void getVertices(
    OdGePoint3dArray& vertices) const;

  /** Description:
    Sets the clip boundary to coincide with the image corners of this raster image entity. 
    
    Remarks:
    Any existing boundary is deleted.
  */
  virtual void setClipBoundaryToWholeImage();

  /** Description:
    Returns the clip boundary type of this this raster image entity (DXF 71).
  */
  ClipBoundaryType clipBoundaryType() const;

  /** Description
    Returns the pixel-to-model coordinate transformation matrix for this raster image entity.
  */
  virtual OdGeMatrix3d getPixelToModelTransform() const;

  /** Description:
    Sets the *brightness* value for this raster image entity (DXF 281).
    Arguments:
    brightness (I) Image *brightness*.
    
    Remarks:
    brightness ranges from [0..100]
  */
  void setBrightness( 
    OdInt8 brightness );

  /** Description:
    Returns the *brightness* value for this raster image entity (DXF 281).
    Remarks:
    brightness ranges from [0..100]
  */
  OdInt8 brightness() const;

  /** Description:
    Sets the *contrast* value for this raster image entity (DXF 282).
    Arguments:
    contrast (I) Image *contrast*.
    Remarks:
    contrast ranges from [0..100]
  */
  void setContrast( 
    OdInt8 contrast );

  /** Description:
    Returns the *contrast* value for this raster image entity (DXF 282).
    Remarks:
    contrast ranges from [0..100]
  */
  OdInt8 contrast() const;

  /** Description:
    Sets the *fade* value for this raster image entity (DXF 283).

    Arguments:
    fade (I) Image *fade*.
    Remarks:
    fade ranges from [0..100]
  */
  void setFade( 
    OdInt8 fade );

  /** Description:
    Returns the *fade* value for this raster image entity (DXF 283).
    Remarks:
    fade ranges from [0..100]
  */
  OdInt8 fade() const;

  /** Description:
    Sets the specified image display option for this raster image entity (DXF 70).

    Arguments:
    option (I) Option to set.
    value (I) Value for option.
    Remarks:
    option is one of the following:
    
    @table
    Name              Value   Description
    kShow             1       Show
    kShowUnAligned    2       Show unaligned
    kClip             4       Clip
    kTransparent      8       Transparent
  */
  void setDisplayOpt(
    ImageDisplayOpt option, 
    bool value);

  /** Description:
    Returns the specified image display option for this raster image entity (DXF 70).

    Arguments:
    option (I) Option to return.

  */
  bool isSetDisplayOpt(
    ImageDisplayOpt option) const;

  /** Description:
    Returns the size in pixels of this raster image entity (DXF 13, 23).
      
    Arguments:
    bGetCachedValue (I) True to always return cached value. False to return the value from the OdDbRasterImageDef
                        object of available.  
  */
  OdGeVector2d imageSize(
    bool bGetCachedValue = false) const;

  /** Description:
    Returns the effective *scale* factor of this raster image entity.
    
    Remarks:
    The effective *scale* factor is the image size in drawing units divided by the original image size.
    
    If the original image size is unavalable, or the user has not defined a drawing unit, the
    image width is assumed to be one drawing unit. 
  */
  OdGeVector2d scale() const;

  /** Description:
    Sets the orientation and *origin* of this raster image entity.

    Arguments:
    origin (I) Lower-left corner. 
    uCorner (I) Vector defining the image direction and width. 
    vOnPlane (I) Vector defining the direction of the height of the image.
    
    Remarks:
    Returns true if and only if successful.
    
    Note:
    The actual height of the image is determined by the width and aspect ratio fo the original image. 
    
    These vectors define the outer edges of the raster image entity. 
  */
  bool setOrientation(
    const OdGePoint3d& origin, 
    const OdGeVector3d& uCorner,
    const OdGeVector3d& vOnPlane);

  /** Description:
    Returns the orientation and *origin* of this raster image entity.

    Arguments:
    origin (O) Receives the lower-left corner. 
    uCorner (O) Receives the vector defining the image direction and width. 
    vOnPlane (O) Receives the vector defining the direction of the height of the image.
    
    Note:
    These vectors define the outer edges of the raster image entity.
  */
  void getOrientation(
    OdGePoint3d& origin, 
    OdGeVector3d& uCorner, 
    OdGeVector3d& vOnPlane) const;

  /** Description:
      TBC.
  OdGiSentScanLines* getScanLines(const OdGiRequestScanLines& req) const;
  bool freeScanLines(OdGiSentScanLines* pSSL) const;
  */

  /** Description:
       coment it for a while, because we do not know what it do
     static Oda::ClassVersion classVersion();
  */

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

	void subClose();

  /*
     virtual void subSwapIdWith(OdDbObjectId otherId, bool swapXdata = false, bool swapExtDict = false);
  */

  bool worldDraw(
    OdGiWorldDraw* pWd) const;

  void viewportDraw(
    OdGiViewportDraw* pVd) const;

  OdResult getGeomExtents(
    OdGeExtents3d& extents) const;

  void getClassID(
    void** pClsid) const;

  OdResult transformBy(
    const OdGeMatrix3d& xfm);

  OdDbObjectPtr deepClone(
    OdDbIdMapping& ownerIdMap) const;

  /*
  virtual void getOsnapPoints( OdDb::OsnapMode osnapMode,
    int gsSelectionMark, const OdGePoint3d& pickPoint,
    const OdGePoint3d& lastPoint, const OdGeMatrix3d& viewXform,
    OdGePoint3dArray& snapPoints, OdDbIntArray& geomIds) const;

  virtual void getGripPoints(OdGePoint3dArray& gripPoints,
    OdDbIntArray& osnapModes, OdDbIntArray& geomIds) const;

  virtual void moveGripPointsAt(const OdDbIntArray& indices, const OdGeVector3d& offset);

  virtual void getStretchPoints(OdGePoint3dArray& stretchPoints) const;

  virtual void moveStretchPointsAt(const OdDbIntArray& indices, const OdGeVector3d& offset);

  virtual void transformBy(const OdGeMatrix3d& xform);
  virtual void getTransformedCopy(const OdGeMatrix3d& xform, OdDbEntity** ent) const;
  virtual OdResult explode(OdRxObjectPtrArray& entitySet) const; //Replace OdRxObjectPtrArray
  virtual bool getGeomExtents(OdGeExtents3d& extents) const;

  virtual void getSubentPathsAtGsMarker(OdDb::SubentType type,
    int gsMark, const OdGePoint3d& pickPoint,
    const OdGeMatrix3d& viewXform, int& numPaths,
    OdDbFullSubentPath*& subentPaths, int numInserts = 0,
    OdDbObjectId* entAndInsertStack = 0) const;

  virtual void getGsMarkersAtSubentPath(
    const OdDbFullSubentPath& subPath,
    OdDbIntArray& gsMarkers) const;

  virtual OdDbEntity* subentPtr(const OdDbFullSubentPath& id) const;
  virtual void saveAs(OdGiWorldDraw* mode, OdDb::EntSaveAsType st);
  virtual void intersectWith(const OdDbEntity* ent,
    OdDb::Intersect intType, OdGePoint3dArray& points,
    int thisGsMarker = 0, int otherGsMarker = 0) const;

  virtual void intersectWith(const OdDbEntity* ent,
    OdDb::Intersect intType, const OdGePlane& projPlane,
    OdGePoint3dArray& points, int thisGsMarker = 0,
    int otherGsMarker = 0) const;
  */
};


inline void 
pixelToModel(const OdGeMatrix3d& pixToMod,
             const OdGePoint2d& pixPt,
             OdGePoint3d& modPt)
{
  modPt.set(pixPt.x, pixPt.y, 0);
  modPt.transformBy(pixToMod);
}

inline void
modelToPixel(const OdGeMatrix3d& modToPix,
             const OdGePoint3d& modPt,
             OdGePoint2d& pixPt)
{
  OdGePoint3d modelPt = modPt;
  modelPt.transformBy(modToPix);
  pixPt.set(modelPt.x, modelPt.y);
}

inline void
modelToPixel(const OdGeVector3d& viewDir,
             const OdGeMatrix3d& modToPix,
             const OdGePlane& plane,
             const OdGePoint3d& modPt,
             OdGePoint2d& pixPt)
{
  OdGePoint3d ptOnPlane = modPt.project(plane, viewDir);
  ptOnPlane.transformBy(modToPix);
  pixPt.set(ptOnPlane.x, ptOnPlane.y);
}

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbRasterImage object pointers.
*/
typedef OdSmartPtr<OdDbRasterImage> OdDbRasterImagePtr;

#include "DD_PackPop.h"

#endif // __IMGENT_H


