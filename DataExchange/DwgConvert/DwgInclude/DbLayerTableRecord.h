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



#ifndef _ODDBLAYERTABLETABLERECORD_INCLUDED
#define _ODDBLAYERTABLETABLERECORD_INCLUDED

#include "DD_PackPush.h"

#include "DbSymbolTableRecord.h"
#include "CmColor.h"

class OdDbLayerTable;

/** Description:
    This class represents Layer records in the OdDbLayerTable in an OdDbDatabase instance.

    Library:
    Db
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbLayerTableRecord : public OdDbSymbolTableRecord
{
public:
  ODDB_DECLARE_MEMBERS(OdDbLayerTableRecord);

  OdDbLayerTableRecord();

  typedef OdDbLayerTable TableType;

  /** Description:
    Returns true if and only if this Layer is *frozen* (DXF 70, bit 0x01).
  */
  bool isFrozen() const;

  /** Description:
    Sets the *frozen* status of this Layer (DXF 70, bit 0x01).

    Arguments:
    frozen (I) True to freeze, false to thaw.
  */
  void setIsFrozen(
    bool frozen);

  /** Description:
    Returns true if and only if this Layer is off (DXF 62).
    
    Remarks:
    DXF 62 is negative if and only if this Layer is *off*.
  */
  bool isOff() const;

  /** Description:
    Sets the on / *off* status of this Layer (DXF 62)

    Arguments:    
    off (I) True for off, false for on.

    Remarks:
    DXF 62 is negative if and only if this Layer is *off*.
  */
  void setIsOff(
    bool off);

  /** Description:
    Returns true if and only if this Layer is *frozen* in new viewports (DXF 70, bit 0x02).
  */
  bool VPDFLT() const;

  /** Description:
    Sets the *frozen* in new viewports status of this Layer (DXF 70, bit 0x02).

    Arguments:
    frozen (I) True for *frozen*, false for thawed.
  */
  void setVPDFLT(
    bool frozen);

  /** Description:
      Returns true if and only if this Layer is locked (DXF 70, bit 0x04.  
  */
  bool isLocked() const;

  /** Description:
    Sets the *locked* status of this Layer (DXF 70, bit 0x04).

    Arguments:
    locked (I) True to lock, false to unlock.
  */
  void setIsLocked(
    bool locked);

  /** Description:
    Returns the *color* of this Layer (DXF 62).
  */
  OdCmColor color() const;

  /** Description:
    Sets the *color* of this Layer (DXF 62).

    Arguments:
    color (I) Layer *color*.
  */
  void setColor(
    const OdCmColor &color);
  
  /** Description:
    Returns the *color* index for this Layer (DXF 62).
  */
  OdInt16 colorIndex() const;

  /** Description:
    Sets the *color* index this Layer (DXF 62).

    Arguments:
    colorIndex (I) Color index.
  */
  void setColorIndex(
    OdInt16 colorIndex);

  /** Description:
    Returns the Object ID of the linetype associated with this Layer (DXF 6).
    
    Remarks:
    Returns NULL if there is no linetype associated with this Layer.
  */
  OdDbObjectId linetypeObjectId() const;

  /** Description:
    Sets the Object ID for the linetype associated with this Layer (DXF 6).

    Arguments:
    linetypeId (I) Object ID of the linetype.
  */
  void setLinetypeObjectId(
    const OdDbObjectId& linetypeId);

  /** Description:
    Returns true if and only if this Layer is plottable.
  */
  bool isPlottable() const;

  /** Description:
    Sets the plottable status of this Layer (DXF 290).

    Arguments:
    plot (I) True for plottable, false for not plottable.

    Remarks:
    Some layers can't be set to plottable; e.g., the "Defpoints" Layer.
  */
  void setIsPlottable(bool plot);

  /** Description:
    Returns the lineweight associated with this Layer (DXF 370).
  */
  OdDb::LineWeight lineWeight() const;

  /** Description:
    Sets the lineweight associated with this Layer (DXF 370).

    Arguments:
    lineWeight (I) Lineweight.
  */
  void setLineWeight(
    OdDb::LineWeight lineWeight);

  /** Description:
    Retuns the name of the plot style associated with this Layer (DXF 390).
  */
  OdString plotStyleName() const;

  /** Description:
    Returns the Object ID of the plot style associated with this Layer (DXF 390).
  */
  OdDbObjectId plotStyleNameId() const;

  /** Description:
    Sets the name of the plot style associated with this Layer (DXF 390).

    Arguments:
    plotstyleName (I) Plot style *name*.
    plotstyleID (I) Plot style ID.
    
    Remarks:
    plotstyleID is the Object ID of an OdDbPlaceHolder object owned by the plot style name dictionary. 
    The plot style name dictionary key for this object is the plot style name.
    
    Returns eOk if successful, or an appropriate error code if not.  
  */
  OdResult setPlotStyleName(
    const OdChar* plotstyleName);
  void setPlotStyleName(
    const OdDbObjectId& plotstyleId);
  
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

  virtual OdUInt32 setAttributes(
    OdGiDrawableTraits* pTraits) const;

  /* 
     OdGiDrawable* drawable();
     bool worldDraw(OdGiWorldDraw* pWd) const;
     void viewportDraw(OdGiViewportDraw* pVd);
     void setGsNode(OdGsNode* pNode);
     OdGsNode* gsNode() const;
  */


  /** Description:
    Returns true if and only if this Layer is in use.
    
    Remarks:
    Usage is determined by a call to OdDbLayerTable::generateUsageData().

    Note:
    Returns true if OdDbLayerTable::generateUsageData() has not been called
    or this Layer table record is not *database* resident.
  */
  bool isInUse() const;


  /** Description:
    Returns the *description* of this Layer.
  */
  OdString description() const;

  /** Description:
    Returns the *description* of this Layer.

    Arguments:
    description (I) Description.
    
    Remarks:
    The *description* may be up to 255 characters length.
  */
  void setDescription(
    const OdString& description);
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbLayerTableRecord object pointers.
*/
typedef OdSmartPtr<OdDbLayerTableRecord> OdDbLayerTableRecordPtr;

#include "DD_PackPop.h"

#endif // _ODDBLAYERTABLETABLERECORD_INCLUDED


