#ifndef __ODGSMODELIMPL_H__
#define __ODGSMODELIMPL_H__


class OdGsNode;
class OdGsBaseVectorizeView;

#include "GsExport.h"

#include "Gi/GiDrawable.h"
#include "Gs/GsModel.h"
#include "UInt8Array.h"
#include "UInt32Array.h"
#include "Gi/GiCommonDraw.h"
#include "StaticRxObject.h"
#include "Si/SiSpatialIndex.h"
#include "Gi/GiExtAccum.h"


class OdGsBaseModel;
class OdGsContainerNode;


/** Description:
    Serves as a placeholder for a drawable object, that can be used to 
    implement custom caching support for rendering applications.  

    Remarks:
    For example, a client application can create a custom OdGsNode descendent object
    for each drawable object in a database, and can store cached rendering information
    in the descendent.  This cached information can then be fed into the DWGdirect
    framework during a redraw (instead of making the normal calls to worldDraw and 
    viewportDraw), to greatly improve the speed of redraws.

    {group:OdGs_Classes} 
*/
class GS_TOOLKIT_EXPORT ODRX_ABSTRACT OdGsNode : public OdRxObject
{
  friend class OdGsBaseModel;

  mutable OdGsNode* m_pPrev;
  mutable OdGsNode* m_pNext;
protected:

  OdGsBaseModel*    m_pModel;
  void*             m_underlayingDrawable;
  
  enum
  {
    kPersistent = 0x00000001,
    kContainer  = 0x00000002,
    kLastFlag   = 0x00000002
  };

  mutable OdUInt32  m_flags;

  inline void clearDrawable();

public:
  ODRX_DECLARE_MEMBERS(OdGsNode);
  ODRX_HEAP_OPERATORS();

  /** Remarks:
      OdGsNode objects perform no reference counting.
  */
  void addRef();

  /** Remarks:
      OdGsNode objects perform no reference counting.
  */
  void release();

  OdGsNode(OdGsBaseModel* pModel, const OdGiDrawable* pUnderlayingDrawable);
  virtual ~OdGsNode();

  OdGsBaseModel* model() const;

  /** Description:
      Returns true if this node serves as a container for other nodes, false otherwise.
  */
  bool isContainer() const;

  /** Description:
      Invalidates the cached data within this node, so that it will be regenerated
      the next time it is accessed.
  */
  virtual void invalidate(OdGsContainerNode* pParent, OdGsBaseVectorizeView* pView, OdUInt32 nMask) = 0;

  /** Description:
      Returns the underlying OdGiDrawable object associated with this node.
  */
  OdGiDrawablePtr underlayingDrawable() const;

  OdDbStub* underlayingDrawableId() const;

  virtual void update(OdGsBaseVectorizeView& view, OdGsContainerNode* pParent) = 0;

  virtual void display(OdGsBaseVectorizeView& view) = 0;

  virtual bool extents(OdGeExtents3d& ext) const = 0;
};


class OdGsEntityNode;
class OdGsViewProps;



/** Description:
    OdGsNode descendent that can serve as a container for other OdGsNode objects.

    {group:OdGs_Classes} 
*/
class GS_TOOLKIT_EXPORT OdGsContainerNode : public OdGsNode
{
  OdGsEntityNode*         m_pFirstEntity;
  OdGsEntityNode*         m_pLastEntity;
  OdSiSpatialIndexPtr     m_pSpIndex;
  OdGsEntityNode*         m_pClearSpatialQueryStateFirst;
  OdUInt32Array           m_vpAwareFlags;

  enum
  {
    kEntityListValid  = OdGsNode::kLastFlag << 1,
    kChildrenUpToDate = OdGsNode::kLastFlag << 2,

    kLastFlag = kChildrenUpToDate
  };

  void setEntityListValid(bool bValid);

  bool childrenUpToDate() const;
  bool needRegen(OdUInt32 nVpID) const;

  OdGsEntityNode* updateList(OdGsBaseVectorizeView& view);
  void displayEntityList(OdGsBaseVectorizeView& view);
  OdUInt32 awareFlags(OdUInt32 nVpID) const;
  void setAwareFlags(OdUInt32 nVpID, OdUInt32 flags);
public:
  ODRX_DECLARE_MEMBERS(OdGsContainerNode);

  OdGsContainerNode(OdGsBaseModel* pModel, const OdGiDrawable* pUnderlayingDrawable);

  bool entityListValid() const;
  void setChildrenUpToDate(bool bValid);

  /** Description:
      Adds a child node to this container.
  */
  void addChild(const OdGiDrawable* pDrawable);

  /** Description:
      Removes the specified child node from this container.
  */
  void removeChild(OdGsNode* pNode);

  void update(OdGsBaseVectorizeView& view, OdGsContainerNode* pParent);

  void display(OdGsBaseVectorizeView& view);

  OdSiSpatialIndex& spatialIndex();
  void spatialQuery(const OdSiShape& query, OdSiVisitor& visitor);
  void spatialSequentalQuery(const OdSiShape& query, OdSiVisitor& visitor);

  void invalidate(OdGsContainerNode* pParent, OdGsBaseVectorizeView* pView, OdUInt32 nMask);

  OdUInt32 currViewChanges() const;

  bool extents(OdGeExtents3d& ext) const;
};


/** Description:
    OdGsNode descendent that can serve as a container for other OdGsNode objects.

    {group:OdGs_Classes} 
*/
class GS_TOOLKIT_EXPORT OdGsEntityNode : public OdGsNode, public OdSiEntity
{
  OdGsEntityNode*             m_pNextEntity;

  mutable OdGeExtents3d       m_extents;

  struct VpMetafileHolder : public OdRxObject
  {
    ODRX_HEAP_OPERATORS();
    VpMetafileHolder(const OdRxObject* pObj);

    void addRef();
    void release();
    long numRefs() const;

    OdRxObject*         pGsMetafile;
    OdUInt32            nAwareFlags;
    OdGeExtents3d       extents;
  };
  typedef OdSmartPtr<VpMetafileHolder> VpMetafileHolderPtr;

public:
  typedef OdArray<VpMetafileHolderPtr> VpMetafileHolderPtrArray;

private:
  struct MetafileStrg
  {
    OdUInt8 data[odmax(sizeof(VpMetafileHolderPtrArray),sizeof(OdRxObject*))];
  };

  mutable MetafileStrg m_metafileStrg;

  VpMetafileHolderPtrArray& metafileArray();

  OdRxObject* metafile();
  VpMetafileHolderPtr metafileAt(int n) const;
  void setMetafile(OdRxObject* pMetafile);
  void setMetafileAt(int n, VpMetafileHolder* pMetafile);
  void convertToViewportDependent(const OdGsBaseVectorizeView& refView);

  OdRxObject* metafile(OdGsBaseVectorizeView& view, bool bCheckVpChanges);
  void setMetafile(OdGsBaseVectorizeView& view, VpMetafileHolder* pMetafile);
  OdRxObject* findCompatibleCache(const OdGsBaseVectorizeView& keyView);
protected:
  enum
  {
    kHasExtents         = OdGsNode::kLastFlag << 1,
    kSpatiallyIndexed   = OdGsNode::kLastFlag << 2,
    kMarkedToSkip       = OdGsNode::kLastFlag << 3,
    kArrayAllocated     = OdGsNode::kLastFlag << 4,
    kViewportDependent  = OdGsNode::kLastFlag << 5,
    kOwned              = OdGsNode::kLastFlag << 6,

    kLastFlag           = kOwned
  };
public:
  ODRX_DECLARE_MEMBERS(OdGsEntityNode);

  OdGsEntityNode(OdGsBaseModel* pModel, const OdGiDrawable* pUnderlayingDrawable);
  ~OdGsEntityNode();

  void setNextEntity(OdGsEntityNode* pNextEntity);
  OdGsEntityNode* nextEntity();

  void update(OdGsBaseVectorizeView& view, OdGsContainerNode* pParent);
  OdUInt32 awareFlags(OdUInt32 nVpID) const;

  void display(OdGsBaseVectorizeView& view);

  void invalidate(OdGsContainerNode* pParent, OdGsBaseVectorizeView* pView, OdUInt32 nMask);

  bool extents(OdGeExtents3d& ext) const;
  bool spatiallyIndexed() const;
  void setSpatiallyIndexed(bool bIndexed);
  bool owned() const;
  void setOwned(bool bOwned);

  bool hasExtents() const;
  const OdGeExtents3d& extents() const;

  bool markedToSkip() const;

  void markToSkip(bool bSkip);
};

typedef OdSmartPtr<OdGsEntityNode> OdGsEntityNodePtr;

typedef OdArray<OdDbStub*, OdMemoryAllocator<OdDbStub*> > OdDbStubPtrArray;

/** Description:
    Model class that can be used to coordinate custom caching support for 
    DWGdirect vectorization applications.  Clients should derive their
    custom model classes from this class.

    {group:OdGs_Classes} 
*/
class GS_TOOLKIT_EXPORT OdGsBaseModel : public OdGsModel
{
  OdGiOpenDrawableFn      m_openDrawableFn;
  OdGsNode*               m_pNodes;
  OdGiExtAccumPtr         m_pExtAccum;

  friend class OdGsNode;

  /** Description:
      Adds the specified node to this model.
  */
  void addNode(OdGsNode* pNew);

public:
  struct ViewProps
  {
  public:
    ViewProps();
    OdUInt32              m_vpId;
    OdGiRegenType         m_regenType;
    OdGsView::RenderMode  m_renderMode;
    OdGeMatrix3d          m_worldToEye;
    OdGePoint3d           m_cameraLocation;
    OdGePoint3d           m_cameraTarget;
    OdGeVector3d          m_cameraUpVector;
    OdGeVector3d          m_viewDir;
    OdGePoint2d           m_vpLowerLeft;
    OdGePoint2d           m_vpUpperRight;
    double                m_deviation[5];
    double                m_frontClip;
    double                m_backClip;
    OdDbStubPtrArray      m_frozenLayers;
    OdUInt32              m_nViewChanges;
    OdUInt32              m_nViewDependencyMask;

    void set(const OdGsBaseVectorizeView& vpDraw);
    OdUInt32 difference(const ViewProps& props) const;
    bool isCompatibleWith(const ViewProps& props, OdUInt32 nFlags) const;
  };
private:

  OdArray<ViewProps>      m_viewProps;

protected:

  OdGsBaseModel();
  ~OdGsBaseModel();

public:
  virtual OdGsNode* gsNode(OdGiDrawable* pDrawable);

  /** Description:
      Removes the specified node from the model, and calls deleteWrapperObject for the node.
  */
  virtual void detach(OdGsNode* pCache);

  /** Description:
      Opens the passed in object.
  */
  OdGiDrawablePtr open(OdDbStub* objectId);

  void setOpenDrawableFn(OdGiOpenDrawableFn openDrawableFn);

  void onAdded(OdGiDrawable* pAdded, OdGiDrawable* pParent);
  void onAdded(OdGiDrawable* pAdded, OdDbStub* parentID);
  
  void onModified(OdGiDrawable* pModified, OdGiDrawable* pParent);
  void onModified(OdGiDrawable* pModified, OdDbStub* parentID);
  
  void onErased(OdGiDrawable* pErased, OdGiDrawable* pParent);
  void onErased(OdGiDrawable* pErased, OdDbStub* parentID);

  void invalidate(InvalidationHint hint);
  void invalidate(OdGsView* pView);
  void invalidate(OdGsBaseVectorizeView& view, OdUInt32 nMask);

  void update(OdGsBaseVectorizeView& view, OdGsNode* pRoot, bool bDisplay);

  OdGiExtAccum& extentsAccum();

  OdUInt32 viewChanges(OdUInt32 viewportId) const;
  OdUInt32 numViewProps() const;
  const ViewProps& viewProps(OdUInt32 viewportId) const;
  void updateViewProps(const OdGsBaseVectorizeView& viewport);
};

inline OdGsBaseModel* OdGsNode::model() const
{
  return const_cast<OdGsBaseModel*>(m_pModel);
}

inline bool OdGsNode::isContainer() const
{
  return GETBIT(m_flags, kContainer);
}

inline OdGiDrawablePtr OdGsBaseModel::open(OdDbStub* objectId) 
{
  ODA_ASSERT(m_openDrawableFn);
  return m_openDrawableFn(objectId);
}

inline OdUInt32 OdGsBaseModel::numViewProps() const
{
  return m_viewProps.size();
}

inline OdGiDrawablePtr OdGsNode::underlayingDrawable() const
{
  if(GETBIT(m_flags, kPersistent))
    return m_pModel->open(reinterpret_cast<OdDbStub*>(m_underlayingDrawable));
  return reinterpret_cast<OdGiDrawable*>(m_underlayingDrawable);
}

inline OdDbStub* OdGsNode::underlayingDrawableId() const
{
  if(GETBIT(m_flags, kPersistent))
    return reinterpret_cast<OdDbStub*>(m_underlayingDrawable);
  return 0;
}

inline void OdGsNode::clearDrawable()
{
  OdGiDrawablePtr pDrawable = underlayingDrawable();
  if(pDrawable.get())
    pDrawable->setGsNode(0);

  m_underlayingDrawable = 0;
}

inline bool OdGsContainerNode::entityListValid() const
{
  return GETBIT(m_flags, kEntityListValid);
}

inline void OdGsContainerNode::setEntityListValid(bool bValid)
{
  SETBIT(m_flags, kEntityListValid, bValid);
}

inline bool OdGsContainerNode::childrenUpToDate() const
{
  return GETBIT(m_flags, kChildrenUpToDate);
}

inline void OdGsContainerNode::setChildrenUpToDate(bool bValid)
{
  SETBIT(m_flags, kChildrenUpToDate, bValid);
}

inline OdSiSpatialIndex& OdGsContainerNode::spatialIndex()
{
  return *m_pSpIndex;
}


inline void OdGsEntityNode::setNextEntity(OdGsEntityNode* pNextEntity)
{
  m_pNextEntity = pNextEntity;
}

inline OdGsEntityNode* OdGsEntityNode::nextEntity()
{
  return m_pNextEntity;
}

inline bool OdGsEntityNode::hasExtents() const
{
  return GETBIT(m_flags, kHasExtents);
}

inline const OdGeExtents3d& OdGsEntityNode::extents() const
{
  return m_extents;
}

inline bool OdGsEntityNode::markedToSkip() const
{
  return GETBIT(m_flags, kMarkedToSkip);
}

inline void OdGsEntityNode::markToSkip(bool bSkip)
{
  SETBIT(m_flags, kMarkedToSkip, bSkip);
}

inline bool OdGsEntityNode::spatiallyIndexed() const
{
  return GETBIT(m_flags, kSpatiallyIndexed);
}

inline void OdGsEntityNode::setSpatiallyIndexed(bool bIndexed)
{
  SETBIT(m_flags, kSpatiallyIndexed, bIndexed);
}

inline bool OdGsEntityNode::owned() const
{
  return GETBIT(m_flags, kOwned);
}

inline void OdGsEntityNode::setOwned(bool bOwned)
{
  SETBIT(m_flags, kOwned, bOwned);
}

inline OdGiExtAccum& OdGsBaseModel::extentsAccum()
{
  return *m_pExtAccum.get();
}


#endif // __ODGSMODELIMPL_H__
