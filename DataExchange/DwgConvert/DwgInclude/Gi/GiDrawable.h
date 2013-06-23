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
// programs incorporating this software must include the following statment 
// with their copyright notices:
//
//      DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef __ODDRAWABLE_H__
#define __ODDRAWABLE_H__  /** {Secret} **/

#include "RxObject.h"

class OdGiDrawableTraits;
class OdGiWorldDraw;
class OdGiViewportDraw;
class OdGsNode;
class OdDbStub;
class OdGeExtents3d;

#include "DD_PackPush.h"

/** Description:
    This class is the base class for all graphical objects, both transient and persistent.
    
    Remarks:
    This interface must be implemented by all graphical objects.  Such objects
    are capable of vectorizing themselves within the DWGdirect framework.

    {group:OdGi_Classes} 
*/
class FIRSTDLL_EXPORT OdGiDrawable : public OdRxObject
{
protected:
  OdGiDrawable();
public:

  ODRX_DECLARE_MEMBERS(OdGiDrawable);

  enum SetAttributesFlags
  {
    kDrawableNone                         = 0, // Default flags; the drawable object uses only OdGi primitives, 
                                               // with no nested calls to draw().
    kDrawableIsAnEntity                   = 1, // Classes derived from OdDbEntity must set this flag,
                                               // which is set by the default implementation of 
                                               // OdDbEntity::setAttributes().
    kDrawableUsesNesting                  = 2, // The drawable uses nested calls to draw(), but makes no
                                               // calls to other OdGi primatives. 
    kDrawableIsCompoundObject             = 4, // The drawable is to be treated as a block. Valid only when combined 
                                               // with kDrawableIsAnEntity. If set, you must override 
                                               // OdDbEntity::getCompoundObjectTransform().
    kDrawableViewIndependentViewportDraw  = 8,  // Currently not supported.
    kDrawableIsInvisible                  = 16, // Object is invisible, and should not be rendered.
    kDrawableHasAttributes                = 32, // Currently not supported.
    kDrawableRegenTypeDependantGeometry   = 64, // Currently not supported.
    kDrawableIsDimension                  = (kDrawableIsAnEntity + kDrawableIsCompoundObject + 128), // Dimension objects must set these flags
                                                // which are set by the default implementation of 
                                                // OdDbDimension.
    kDrawableRegenDraw                    = 256,// Unknown.
    kLastFlag                             = kDrawableRegenDraw // Marker
  };

  /** Description:
    Sets the vectorization attributes of this object, and returns its attribute flags.  

    Arguments:
    pTraits (I) Pointer to OdGiDrawableTraits object from and to which the attributes are to be set.

    Remarks:
    This function is called by the vectorization framework, prior to calling worldDraw() or
    viewportDraw(), to set the attributes for an object.

    See Also:
    SetAttributesFlags 
  */
  virtual OdUInt32 setAttributes(
    OdGiDrawableTraits* pTraits) const = 0;

  /** Description:
    Creates a viewport-independent geometric representation of this object.
    
    Remarks:
    Returns true if and only if the geometric representation can be generated in a
    viewport-independent manner. 

    Note:
    A return value of false indicates that viewportDraw() must be called of this object.

    The 3D GS will call this function at least once, but may cache subsequent display updates.

    Use OdGsModel::onModified() to assure that the 3D GS will call worldDraw() for next display update.
    
    The default implementation does nothing but return true. This function can be
    overridden in custom classes.

    Arguments:
    pWd (I) Pointer to the OdGiWorldDraw interface.
  */
  virtual bool worldDraw(
  OdGiWorldDraw* pWd) const = 0;

  /** Description:
    Creates a viewport-dependent geometric representation of this object.

    Remarks:
    Causes an OdGiDrawable to describe its geometry to the specified OdGeWorldDraw object. 

    This function is called once per viewport.

    Remarks:
    By overriding this function, it is possible for have totally different images in each viewport; a schematic
    in one viewport, a layout in another.
    
    The default implementation of this function does nothing.  This function can be
    overridden in custom classes.

    Arguments:
    pVd (I) Pointer to the OdGiViewportDraw interface.
  */
  virtual void viewportDraw(
    OdGiViewportDraw* pVd) const = 0;

  /** Description:
    Returns true and only if this object is persistent (stored in a database).
  */
  virtual bool isPersistent() const = 0;

  /** Description:
    Returns the *database* ID of this object.

    Remarks:
    Returns a null pointer if this object is not persistent.
  */
  virtual OdDbStub* id() const = 0;

  /** Description:
    Assigns the specified OdGsNode to this object.
    
    Arguments:
    pGsNode (I) Pointer to the OdGsNode to be assigned.
  */
  virtual void setGsNode(
    OdGsNode* pGsNode) = 0;

  /** Description:
    Returns a pointer to the OdGsNode associated with this object.
  */
  virtual OdGsNode* gsNode() const = 0;

  /** Description:
    Returns the setAttributes flags for the current viewportDraw. 
    
    Arguments:
    pVd (I) Pointer to OdGiViewportDraw interface.
    
    Note:
    The default implementation of this function always returns 0.
  */
  virtual OdUInt32 viewportDrawLogicalFlags(
    OdGiViewportDraw* pVd) const;

 /** Description:
    Returns a WCS bounding box that represents the *extents* of this entity.

    Arguments:
    extents (O) Returns the WCS *extents*.

    Remarks:
    Returns eOk if successful, or an appropriate error code if not.

    The default implementation of this function always returns eInvalidExtents. 
  */
  virtual OdResult getGeomExtents(OdGeExtents3d& extents) const;
};

typedef OdSmartPtr<OdGiDrawable> OdGiDrawablePtr;

#include "DD_PackPop.h"

#endif // __ODDRAWABLE_H__


