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



#ifndef OD_DBLAYOUT_H
#define OD_DBLAYOUT_H

#include "DD_PackPush.h"

#include "DbPlotSettings.h"


class OdString;

/** Description:
    This class represents Layout objects in an OdDbDatabase.
    
    Library:
    Db
   
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbLayout : public OdDbPlotSettings
{
public:
  ODDB_DECLARE_MEMBERS(OdDbLayout);

  /** Description:
      Constructor (no arguments).
  */
  OdDbLayout();

  ///////// BEGIN ODA SPECIFIC /////////////

  /** Description:
    Returns the minimum limits for this Layout object (DXF 10).
  */
  const OdGePoint2d& getLIMMIN() const;

  /** Description:
    Sets the minimum limits for this Layout object (DXF 10).
    
    Arguments:
    limMin (I) Minimum limits.
  */
  void setLIMMIN(
    const OdGePoint2d& limMin);

  /** Description:
    Returns the maximum limits for this Layout object (DXF 11).
  */
  const OdGePoint2d& getLIMMAX() const;

  /** Description:
    Sets the maximum limits for this Layout object (DXF 11).

    Arguments:
    limMax (I) Maximum limits.
  */
  void setLIMMAX(
    const OdGePoint2d& limMax);

  /** Description:
    Returns the minimum extents for this Layout object (DXF 14).
  */
  const OdGePoint3d& getEXTMIN() const;

  /** Description:
    Sets the minimum extents for this Layout object (DXF 14).
    
    Arguments:
    extMin (I) Minimum extents.
  */
  void setEXTMIN(
    const OdGePoint3d& extMin);

  /** Description:
    Returns the maximum extents for this Layout object (DXF 15).
  */
  const OdGePoint3d& getEXTMAX() const;

  /** Description:
    Sets the maximum extents for this Layout object (DXF 15).
    
    Arguments:
    extMax (I) Maximum extents.
  */
  void setEXTMAX(
    const OdGePoint3d& extMax);

  /** Description:
    Returns the insertion base for this Layout object (DXF 12).
  */
  const OdGePoint3d& getINSBASE() const;

  /** Description:
    Sets the insertion base for this Layout object (DXF 12).

    Arguments:
    insBase (I) Insertion base.
  */
  void setINSBASE(
    const OdGePoint3d& insBase);

  /** Description:
    Returns the LIMCHECK variable for this Layout object (DXF 70, bit 0x02).
  */
  bool getLIMCHECK() const;

  /** Description:
    Sets the LIMCHECK variable for this Layout object (DXF 70, bit 0x02).
    
    Arguments:
    limCheck (I) LIMCHECK variable.  
  */
  void setLIMCHECK(
    bool limCheck);

  /** Description:
    Returns the PSLTSCALE variable for this Layout object (DXF 70, bit 0x01).
  */
  bool getPSLTSCALE() const;

  /** Description:
    Sets the PSLTSCALE variable for this Layout object (DXF 70, bit 0x01).
      
    Arguments:
    psLtScale (I) PSLTSCALE variable.
  */
  void setPSLTSCALE(
    bool psLtScale);

  ///////// END ODA SPECIFIC /////////////

  
  /** Description:
    Returns the Object ID of the PaperSpace OdDbBlockTableRecord associated with this Layout object (DXF 330).
  */
  OdDbObjectId getBlockTableRecordId() const;

  /** Description:
    Associates this Layout object with the specified PaperSpace OdDbBlockTableRecord (DXF 330).
    
    Arguments:
    blockTableRecordId (I) Block table record Object Id.
  */
  virtual void setBlockTableRecordId(
    const OdDbObjectId& blockTableRecordId);

  /** Description:
    Adds this Layout object to the layout dictionary in the specified *database*, and associates 
    this Layout object with the specified PaperSpace OdDbBlockTableRecord. 

    Arguments:
    pDb (I) Pointer to the *database*
    blockTableRecordId (I) Block table record Object Id.
  */
  virtual void addToLayoutDict(
    OdDbDatabase* pDb, 
    OdDbObjectId blockTableRecordId);
  
  /** Description:
    Returns the *name* of this Layout object (DXF 1).
  */
  OdString getLayoutName() const;

  /** Description:
    Sets the *name* of this Layout object (DXF 1).
      
    Arguments:
    layoutName (I) Layout name.  
  */
  virtual void setLayoutName(const OdString& layoutName);
  
  /** Description:
    Returns the tab order for this Layout object (DXF 71).
    
    Remarks:
    The tab order determines the order in which layout tabs are to be displayed.
  */
  int getTabOrder() const;

  /** Description:
    Sets the tab order for this Layout Ooject (DXF 71).
    
    Arguments:
    tabOrder (I) Tab order.

    Remarks:
    The tab order determines the order in which layout tabs are to be displayed.
    
    Tab order should be sequential for all Layout objects in the *database*.
  */
  virtual void setTabOrder(
    int tabOrder);
  
  /** Description:
    Returns true if and only if this layout tab is selected.
  */
  bool getTabSelected() const;

  /** Description:
    Controls the selected status for this Layout object.
    
    Arguments:
    tabSelected (I) Controls selected status. 
    
    Remarks:
    Selected Layout objects are included in selection sets for operations effecting multiple layouts.
  */
  virtual void setTabSelected(
    bool tabSelected);
  
  virtual void getClassID(
    void** pClsid) const;

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual void appendToOwner (
    OdDbIdPair& idPair, 
    OdDbObject* pOwnerObject, 
    OdDbIdMapping& ownerIdMap);

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  /** Description:
    Returns the Object ID of the viewport that was last active
    in this Layout object (DXF 331).
  */
  OdDbObjectId activeViewportId() const;

  /** Description:
    Makes the specified viewport the active viewport of this Layout object (DXF 331).

    Arguments:  
    viewportID (I) Object ID of the active viewport.      
  */
  void setActiveViewportId(
    OdDbObjectId viewportId);

  /** Description:
      Returns the Object ID of the overall viewport of this Layout object.
  */
  OdDbObjectId overallVportId() const;

  OdUInt32 setAttributes(
    OdGiDrawableTraits* pTraits) const;

  bool worldDraw(
    OdGiWorldDraw* pWd) const;

  void viewportDraw(
    OdGiViewportDraw* vPd) const;

  OdResult getGeomExtents(OdGeExtents3d& ext) const;
};

// !!! SYMBOL RENAMED !!! USE activeViewportId()

#define lastActiveVportId() activeViewportId()

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbLayout object pointers.
*/
typedef OdSmartPtr<OdDbLayout> OdDbLayoutPtr;

#include "DD_PackPop.h"

#endif


