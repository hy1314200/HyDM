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



#ifndef _ODDBVIEWTABLERECORD_INCLUDED
#define _ODDBVIEWTABLERECORD_INCLUDED

#include "DD_PackPush.h"

#include "DbAbstractViewTableRecord.h"

/** Description:
    This class represents View records in the OdDbViewTable in an OdDbDatabase instance.

    Library:
    Db
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbViewTableRecord : public OdDbAbstractViewTableRecord
{
public:
  ODDB_DECLARE_MEMBERS(OdDbViewTableRecord);

  OdDbViewTableRecord();

  /** Description:
    Returns true if and only if this View is a Paper Space view (DXF 70, bit 0x01).
  */
  bool isPaperspaceView() const;
  
  /** Description:
    Controls if this View is a Paper Space view (DXF 70, bit 0x01).
    
    pSpace (I) True for Paper Space, false for Model space.
  */
  void setIsPaperspaceView(
    bool pSpace);
  
  /** Description:
    Returns true if and only if there is a UCS associated with this View.
  */
  bool isUcsAssociatedToView() const;
  /** Description:
    Disassociates any UCS associated with this View.
  */
  void disassociateUcsFromView();

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

  virtual void getClassID(
    void** pClsid) const;

  // new in ARX 2005
  
  /** Description:
    Returns the name of the category of this View.
  */
  OdString getCategoryName() const;

  /** Description:
    Sets the name of the category of this View.
    Arguments:
    categoryName (I) Category name.
  */
  void setCategoryName(
    const OdString& categoryName);

  /** Description:
    Returns the name of the layer state of this View.
  */
  OdString getLayerState() const;
  /** Description:
    Sets the name of the layer state of this View.
    Arguments:
    layerStateName (I) Layer State name.
  */
  void setLayerState(
    const OdString& layerStateName);

  /** Description:
    Returns the Object ID of the Layout of this View.
  */
  OdDbObjectId getLayout() const;

  /** Description:
    Sets the Object ID of the Layout of this View.
    Arguments:
    layoutId (I) Layout ID.
  */
  void setLayout(
    OdDbObjectId layoutId);

  /** Description:
    Returns true if and only if this View is associated with a Paper Space Viewport.
  */
  bool isViewAssociatedToViewport() const;
  
  /** Description:
    Controls if this View is associated with a Viewport.
    Arguments:
    viewAssociated (I) Controls association.
  */
  void setViewAssociatedToViewport (
    bool viewAssociated);

  /** Description:
    Returns the *thumbnail* information for this view.
    
    Remarks:
    Thumbnail information consists of a packed BITMAPINFO structure 
    immediately followed in memory by the *thumbnail* data.
  */
  void getThumbnail (
    OdBinaryData* thumbnail) const;
  /** Description:
    Sets the *thumbnail* information for this view.
    
    Arguments:
    thumbnail (I) Pointer to the *thumbnail*.

    Remarks:
    Thumbnail information consists of a packed BITMAPINFO structure 
    immediately followed in memory by the *thumbnail* data.
  */
  void setThumbnail(
    const OdBinaryData* thumbnail);

  //  void setParametersFromViewport(OdDbObjectId objId);
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbViewTableRecord object pointers.
*/
typedef OdSmartPtr<OdDbViewTableRecord> OdDbViewTableRecordPtr;

#include "DD_PackPop.h"

#endif // _ODDBVIEWTABLERECORD_INCLUDED


