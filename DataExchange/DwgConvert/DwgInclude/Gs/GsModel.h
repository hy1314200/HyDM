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



#ifndef __ODGSMODEL_H_INCLUDED_
#define __ODGSMODEL_H_INCLUDED_

#include "Gs/Gs.h"

class OdGsNode;

typedef OdGiDrawablePtr (*OdGiOpenDrawableFn)(OdDbStub* id);

#include "DD_PackPush.h"

/** Description:
    Interface that provides notifications for model events. A model represents
    a collection of drawable objects.

    {group:OdGs_Classes} 
*/
class FIRSTDLL_EXPORT ODRX_ABSTRACT OdGsModel : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdGsModel);

  /** Description:
  */
  virtual void setOpenDrawableFn(OdGiOpenDrawableFn openDrawableFn) = 0;

  /** Description:
      Called when a drawable object is added to this model.

      Arguments:
        pAdded (I) Drawable object that was added.
        pParent (I) Parent of pAdded.
  */
  virtual void onAdded(OdGiDrawable* pAdded, OdGiDrawable* pParent) = 0;

  /** 
      Arguments:
        parentID (I) Parent of pAdded.
  */
  virtual void onAdded(OdGiDrawable* pAdded, OdDbStub* parentID) = 0;
  
  /** Description:
      Called when the specified drawable object (belonging to this model) has been modified.

      Arguments:
        pModified (I) Drawable object that was modified.
        pParent (I) Parent of pModified.
  */
  virtual void onModified(OdGiDrawable* pModified, OdGiDrawable* pParent) = 0;

  /** 
      Arguments:
        parentID (I) Parent of pModified.
  */
  virtual void onModified(OdGiDrawable* pModified, OdDbStub* parentID) = 0;
  
  /** Description:
      Called when a drawable object (belonging to this model) has been erased.

      Arguments:
        pErased (I) Drawable object that was erased.
        pParent (I) Parent of pErased.
  */
  virtual void onErased(OdGiDrawable* pErased, OdGiDrawable* pParent) = 0;

  /** 
      Arguments:
        parentID (I) Parent of pErased.
  */
  virtual void onErased(OdGiDrawable* pErased, OdDbStub* parentID) = 0;

  // Invalidation Hint
  enum InvalidationHint
  {
    kInvalidateIsolines,
    kInvalidateViewportCache,
    kInvalidateAll
  };

  /** Description:
      Invalidates the cached data contained in this model, so that it will 
      be regenerated.
  */
  virtual void invalidate(InvalidationHint hint) = 0;

  /** Description:
      Invalidates the viewport dependent cached data contained in this model,
      so that it will be regenerated.
  */
  virtual void invalidate(OdGsView* pView) = 0;

  /*
  enum RenderType
  { 
    kMain,    // Use main Z-buffer
    kSprite,  // Use alternate Z-buffer, for sprites
    kDirect,  // Render on device directly
    kCount    // Count of RenderTypes
  };
  */

  // Highlighting
  //
  //virtual bool highlight(const OdGsPath*) = 0;
  //virtual bool unhighlight(const OdGsPath*) = 0;

  // Scene Graph Roots
  //
  //virtual bool addSceneGraphRoot(OdGiDrawable* pRoot) = 0;
  //virtual bool eraseSceneGraphRoot(OdGiDrawable* pRoot) = 0;

  //virtual void onPaletteModified() = 0;

  // Transformations 
  // 
  //virtual void setTransform(const OdGeMatrix3d&) = 0;
  //virtual OdGeMatrix3d transform() const = 0;

  //virtual bool getTransformAt(const OdGsPath*, OdGeMatrix3d &) = 0;

  //virtual void setExtents(const OdGePoint3d&, const OdGePoint3d&) = 0;
  // invalidation notification 

  // OdGsView property overrides 
  //
  //virtual void setViewClippingOverride(bool bOverride) = 0;
  //virtual void setMaterialsOverride(bool bOverride) = 0;
  //virtual void setRenderModeOverride(OdGsView::RenderMode mode = OdGsView::kNone) = 0;
};


#include "DD_PackPop.h"

#endif // __ODGSMODEL_H_INCLUDED_


