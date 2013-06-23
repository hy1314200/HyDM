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



#ifndef _OD_DB_PROXY_ENTITY_
#define _OD_DB_PROXY_ENTITY_

#include "DD_PackPush.h"

#include "DbEntity.h"
#include "IdArrays.h"

/** Description:
    This class is the abstract base class for Proxy entities derived 
    from OdDbEntity in an OdDbDatabase instance.
    
    Library:
    dB
    
    {group:OdDb_Classes}
    
    Remarks:
    Proxy entities hold surrogate data for custom DWGdirect objects
    when the parent application is not loaded, and allow read-only access
    to data contained therein. 
    
    Whenever the parent application is loaded, the Proxy entities revert
    to their custom objects.
*/
class TOOLKIT_EXPORT OdDbProxyEntity : public OdDbEntity
{
public:
  ODDB_DECLARE_MEMBERS(OdDbProxyEntity);

  OdDbProxyEntity();

  enum GraphicsMetafileType
  {
    kNoMetafile   = 0, // No bounding box or graphics (R13 drawing files only)
    kBoundingBox  = 1, // Bounding Box
    kFullGraphics = 2  // Full Graphics
  };

  /** Description:
    Returns the GraphicsMetafileType for this Proxy entity;
    
    Remarks:
    graphicsMetafileType will return one of the following
    
    @table    
    Name            Value     Description
    kNoMetafile     0         No bounding box or graphics (R13 drawing files only)
    kBoundingBox    1         Bounding Box
    kFullGraphics   2         Full Graphics
  */
  virtual OdDbProxyEntity::GraphicsMetafileType graphicsMetafileType() const;

  bool worldDraw(
    OdGiWorldDraw* pWd) const;

  OdDbObjectPtr deepClone(
    OdDbIdMapping& ownerIdMap) const;

  OdDbObjectPtr wblockClone(
    OdDbIdMapping& ownerIdMap) const;

 /* Replace OdRxObjectPtrArray */
 
  virtual OdResult explode(
    OdRxObjectPtrArray& entitySet) const;

  /*
     virtual bool getGeomExtents(OdGeExtents3d& extents) const;
  */

  enum
  {
    kNoOperation                = 0,
    kEraseAllowed               = 0x1,
    kTransformAllowed           = 0x2,
    kColorChangeAllowed         = 0x4,
    kLayerChangeAllowed         = 0x8,
    kLinetypeChangeAllowed      = 0x10,
    kLinetypeScaleChangeAllowed = 0x20,
    kVisibilityChangeAllowed    = 0x40,
    kCloningAllowed             = 0x80,
    kLineWeightChangeAllowed    = 0x100,
    kPlotStyleNameChangeAllowed = 0x200,
    kAllButCloningAllowed       = 0x37F,
    kAllAllowedBits             = 0x3FF
  };

  /** Description:
    Returns the edit flags settings for the class associated with this Proxy entity.
    
    Remarks:
    These bits determine the allowable operations on this Proxy entity.
    
    proxyFlags will return a combination of the following:
    
    @table
    Name                          Value       Methods enabled       
    kNoOperation                  0           None
    kEraseAllowed                 0x1         erase()
    kTransformAllowed             0x2         transformBy()
    kColorChangeAllowed           0x4         setColor(), setColorIndex()
    kLayerChangeAllowed           0x8         setLayer() 
    kLinetypeChangeAllowed        0x10        setLinetype()
    kLinetypeScaleChangeAllowed   0x20        setLinetypeScale()
    kVisibilityChangeAllowed      0x40        setVisibility()
    kCloningAllowed               0x80        deepClone(), wblockClone()
    kLineWeightChangeAllowed      0x100       setLineWeight()
    kPlotStyleNameChangeAllowed   0x200       setPlotStyleName()
    kAllButCloningAllowed         0x37F       All of the above but cloning
    kAllAllowedBits               0x3FF       All of the above
  */
  virtual int proxyFlags() const;

  /** Description:
    Returns true if and only if the erase() method is allowed for this Proxy entity. 
  */
  bool eraseAllowed() const               { return GETBIT(proxyFlags(), kEraseAllowed); }
  /** Description:
    Returns true if and only if the transformBy() method is allowed for this Proxy entity. 
  */
  bool transformAllowed() const           { return GETBIT(proxyFlags(), kTransformAllowed); }
  /** Description:
    Returns true if and only if the setColor() and setColorIndex() methods are allowed for this Proxy entity. 
  */
  bool colorChangeAllowed() const         { return GETBIT(proxyFlags(), kColorChangeAllowed); }
  /** Description:
    Returns true if and only if the setLayer() method is allowed for this Proxy entity. 
  */
  bool layerChangeAllowed() const         { return GETBIT(proxyFlags(), kLayerChangeAllowed); }
  /** Description:
    Returns true if and only if the setLinetype() method is allowed for this Proxy entity. 
  */
  bool linetypeChangeAllowed() const      { return GETBIT(proxyFlags(), kLinetypeChangeAllowed); }
  /** Description:
    Returns true if and only if the setLinetypeScale() method is allowed for this Proxy entity. 
  */
  bool linetypeScaleChangeAllowed() const { return GETBIT(proxyFlags(), kLinetypeScaleChangeAllowed); }
  /** Description:
    Returns true if and only if the setVisibility() method is allowed for this Proxy entity. 
  */
  bool visibilityChangeAllowed() const    { return GETBIT(proxyFlags(), kVisibilityChangeAllowed); }
  /** Description:
    Returns true if and only if the setLineWeight() method is allowed for this Proxy entity. 
  */
  bool lineWeightChangeAllowed() const    { return GETBIT(proxyFlags(), kLineWeightChangeAllowed); }
  /** Description:
    Returns true if and only if the setPlotStyleName() method is allowed for this Proxy entity. 
  */
  bool plotStyleNameChangeAllowed() const { return GETBIT(proxyFlags(), kPlotStyleNameChangeAllowed); }
  /** Description:
    Returns true if and only the deepClone() and wblockClone() methods are allowed for this Proxy entity. 
  */
  bool cloningAllowed() const             { return GETBIT(proxyFlags(), kCloningAllowed); }
  /** Description:
    Returns true if and only if all but the deepClone() and wblockClone() methods are allowed for this Proxy entity.
    
    Remarks:
    The allowed methods are as follows
    
    @untitled table
    erase()
    setColor()
    setColorIndex()
    setLayer() 
    setLinetype()
    setLinetypeScale()
    setLineWeight()
    setPlotStyleName()
    setVisibility()
    transformBy()
  */
  bool allButCloningAllowed() const       { return (proxyFlags() & kAllAllowedBits) == (kAllAllowedBits ^ kAllButCloningAllowed); }
  /** Description:
    Returns true if and only if all methods are allowed for this Proxy entity. 

    Remarks:
    The allowed methods are as follows
    
    @untitled table
    deepClone()
    erase()
    setColor()
    setColorIndex()
    setLayer() 
    setLinetype()
    setLinetypeScale()
    setLineWeight()
    setPlotStyleName()
    setVisibility()
    transformBy()
    wblockClone()
  */
  bool allOperationsAllowed() const       { return (proxyFlags() & kAllAllowedBits) == kAllAllowedBits; }

  /** Description:
    Returns true if and only this Proxy entity is a R13 format Proxy entity. 
  */
  bool isR13FormatProxy() const           { return GETBIT(proxyFlags(), 0x8000); }

  /** Description:
    Returns the class name of the entity represented by this Proxy entity.
  */
  virtual OdString originalClassName() const;

  /** Description:
    Returns the DXF name of the entity represented by this Proxy entity.
  */
  virtual OdString originalDxfName() const;

  /** Description:
    Returns the application description of the entity represented by this Proxy entity.
  */
  virtual OdString applicationDescription() const;

  /** Description:
    Returns an array of the object IDs referenced by this Proxy entity.

    Arguments:
    objectIds (O) Receives an array of the reference object IDs.
  */
  virtual void getReferences(
    OdTypedIdsArray& objectIds) const;

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  OdResult dxfIn(
    OdDbDxfFiler* pFiler);

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);

  /** Description:
      Sets the properties for this entity taking into account the proxy flags.
  */
  
  /**
    Note:
    This function honors proxyFlags().
  */
  virtual OdResult setColor(
    const OdCmColor &color, 
    bool doSubents = true);
  /**
    Note:
    This function honors proxyFlags().
  */
  virtual OdResult setColorIndex(
    OdUInt16 colorIndex, 
    bool doSubents = true);
  /**
    Note:
    This function honors proxyFlags().
  */
  virtual OdResult setColorId(
    OdDbObjectId colorId, 
    bool doSubents = true);
  /**
    Note:
    This function honors proxyFlags().
  */
  virtual OdResult setPlotStyleName(
    const OdString& newName, 
    bool doSubents = true);
  /**
    Note:
    This function honors proxyFlags().
  */
  virtual OdResult setPlotStyleName(
    OdDb::PlotStyleNameType plotStyleNameType, 
    OdDbObjectId newId = OdDbObjectId::kNull, 
    bool doSubents = true);
  /**
    Note:
    This function honors proxyFlags().
  */
  virtual OdResult setLayer(
    const OdString& layerName, 
    bool doSubents = true);
  /**
    Note:
    This function honors proxyFlags().
  */
  virtual OdResult setLayer(
    OdDbObjectId layerId, 
    bool doSubents = true);
  /**
    Note:
    This function honors proxyFlags().
  */
  virtual OdResult setLinetype(
    const OdString& linetypeName, 
    bool doSubents = true);
  /**
    Note:
    This function honors proxyFlags().
  */
  virtual OdResult setLinetype(
    OdDbObjectId linetypeID, 
    bool doSubents = true);
  /**
    Note:
    This function honors proxyFlags().
  */
  virtual OdResult setLinetypeScale(
    double linetypeScale, 
    bool doSubents = true);
  /**
    Note:
    This function honors proxyFlags().
  */
  virtual OdResult setVisibility(
    OdDb::Visibility visibility, 
    bool doSubents = true);
  /**
    Note:
    This function honors proxyFlags().
  */
  virtual OdResult setLineWeight(
    OdDb::LineWeight lineWeight, 
    bool doSubents = true);
  virtual OdResult subErase(
    bool erasing);
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbProxyEntity object pointers.
*/
typedef OdSmartPtr<OdDbProxyEntity> OdDbProxyEntityPtr;

#include "DD_PackPop.h"

#endif

