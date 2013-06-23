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



#ifndef DBFCF_H
#define DBFCF_H

#include "DD_PackPush.h"

#include "DbEntity.h"
#include "DbDimStyleTableRecord.h"

/** Description:
    This class represents feature control frames in an OdDbDatabase instance.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbFcf : public OdDbEntity
{
public:
  ODDB_DECLARE_MEMBERS(OdDbFcf);

  OdDbFcf();

  enum whichLine 
  {
    kAll = -1
  };
  
  /** Description:
    Sets the *text* string of this feature control frame (DXF 1).
    
    Arguments:
    text (I) Text of this feature control frame.
    
    Remarks:
    The following escape sequences are used to specify the feature control frame:

    @table
    Sequence        Meaning
    \n	            newline 
    {\\Fgdt;a}	    angularity
    {\\Fgdt;b}	    perpendicularity
    {\\Fgdt;c}	    flatness
    {\\Fgdt;d}	    profile of a surface
    {\\Fgdt;e}	    circularity
    {\\Fgdt;f}	    parallelism
    {\\Fgdt;g}	    cylindricity
    {\\Fgdt;h}	    circular runout
    {\\Fgdt;i}	    symmetry
    {\\Fgdt;j}	    positional (+)
    {\\Fgdt;k}	    profile of a line
    {\\Fgdt;l}	    least material condition
    {\\Fgdt;m}	    maximum material condition
    {\\Fgdt;n}	    diameter
    {\\Fgdt;p}	    positional(P)
    {\\Fgdt;r}	    concentricity
    {\\Fgdt;s}	    regardless of feature size
    {\\Fgdt;t}	    total runout
    {\\Fgdt;u}	    straightness
    %%v	            vertical side of frame box
  */
  virtual void setText(
    const char* text);
  
  /** Description:
    Returns the *text* string of this feature control frame (DXF 1).

    Arguments:
    lineNo (I) Index of the line desired.

    Remarks:
    The following escape sequences are used to specify the feature control frame:
    
    @table
    Sequence        Meaning
    \n	            newline 
    {\\Fgdt;a}	    angularity
    {\\Fgdt;b}	    perpendicularity
    {\\Fgdt;c}	    flatness
    {\\Fgdt;d}	    profile of a surface
    {\\Fgdt;e}	    circularity
    {\\Fgdt;f}	    parallelism
    {\\Fgdt;g}	    cylindricity
    {\\Fgdt;h}	    circular runout
    {\\Fgdt;i}	    symmetry
    {\\Fgdt;j}	    positional (+)
    {\\Fgdt;k}	    profile of a line
    {\\Fgdt;l}	    least material condition
    {\\Fgdt;m}	    maximum material condition
    {\\Fgdt;n}	    diameter
    {\\Fgdt;p}	    positional (P)
    {\\Fgdt;r}	    concentricity
    {\\Fgdt;s}	    regardless of feature size
    {\\Fgdt;t}	    total runout
    {\\Fgdt;u}	    straightness
    %%v	            vertical side of frame box
      
    Note:
    lineNo is not currently implemented. All lines of text will be returned, separated by \n.
  */
  virtual const OdString text(
    int lineNo = kAll) const;

  /** Description:
    Sets the insertion point of this feature control frame (WCS equivalent of DXF 10).

    Arguments:
    insPoint (I) Insertion point.

    Remarks:
    The insertion point is the middle of the left edge of the feature control frame.
  */
  virtual void setLocation(
    const OdGePoint3d& insPoint);

  /** Description:
    Returns the insertion point of this feature control frame (WCS equivalent of DXF 10).
    
    Remarks:
    The insertion point is the middle of the left edge of the feature control frame.
  */
  virtual OdGePoint3d location() const;

  /** Description:
    Sets the orientation vectors of this feature control frame (DXF 210 and 11).
    Arguments:
    normal (I) WCS *normal* to plane of feature control frame.
    direction (I) WCS X-axis *direction* vector of feature control frame. 
  */
  virtual void setOrientation(
    const OdGeVector3d& normal, 
    const OdGeVector3d& direction);

  /** Description:
    Returns the WCS *normal* to the plane of this feature control frame (DXF 210).
  */
  virtual OdGeVector3d normal() const;

  /** Description:
    Returns the WCS X-axis *direction* vector of this feature control frame (DXF 11).
  */
  virtual OdGeVector3d direction() const;

  /** Description:
    Sets the Object ID of the dimension style (OdDbDimStyleTableRecord) to used by this feature control frame (DXF 3).
    
    Arguments:
    dimensionStyle (I) Object ID of the dimension style.
  */
  virtual void setDimensionStyle(
    OdDbHardPointerId dimStyleID);

  /** Description:
    Returns the Object ID of the dimension style (OdDbDimStyleTableRecord) used by this feature control frame (DXF 3).
  */
  virtual OdDbHardPointerId dimensionStyle() const;

  /** Description:
    Returns the DIMCLRD ( frame *color* ) value of this feature control frame.  
    
    Remarks:
    The value from the feature control frame's dimension style will be returned unless that value is overridden, 
    in which case the override value will be returned.
  */
  OdCmColor dimclrd() const;

  /**
    Description:
    Returns the DIMCLRT ( *text* *color* ) value of this feature control frame.  
    
    Remarks:
    The value from the feature control frame's dimension style will be returned unless that value is overridden, 
    in which case the override value will be returned.
  */
  OdCmColor dimclrt() const;

  /** Description:
    Returns the DIMGAP ( dimension gap ) value of this feature control frame.  
      
    Remarks:
    The value from the feature control frame's dimension style will be returned unless that value is overridden, 
    in which case the override value will be returned.
  */
  double dimgap() const;

  /** Description:
    Returns the DIMSCALE ( dimension scale ) value of this feature control frame.  
      
    Remarks:
    The value from the feature control frame's dimension style will be returned unless that value is overridden, 
    in which case the override value will be returned.
  */
  double dimscale() const;

  /** Description:
    Returns the Object ID of the DIMTXSTY ( *text* style ) of this feature control frame.
    
    Remarks:
    The value from the feature control frame's dimension style will be returned unless that value is overridden, 
    in which case the override value will be returned.
  */
  OdDbObjectId dimtxsty() const;

  /** Description:
    Returns the DIMTXT value ( *text* height ) of this feature control frame.  
    
    Remarks:
    The value from the feature control frame's dimension style will be returned unless that value is overridden, 
    in which case the override value will be returned.
  */
  double dimtxt() const;

  /** Description:
    Sets the DIMCLRD override ( frame *color* ) of this feature control frame.
  */
  void setDimclrd(
    const OdCmColor& color);

  /** Description:
      Sets the DIMCLRT override ( *text* *color* ) of this feature control frame.
  */
  void setDimclrt(
    const OdCmColor& color);

  /** Description:
      Sets the DIMGAP override ( dimension gap ) of this feature control frame.
  */
  void setDimgap(
    double dimgap);

  /** Description:
      Sets the DIMSCALE override ( dimension scale ) of this feature control frame.
  */
  void setDimscale(
    double dimscale);

  /** Description:
      Sets the Object ID override of the DIMTXSTY ( *text* style ) of this feature control frame.
      
      Arguments (I)
      dimtxsty (I) Object ID override for DIMTXSTY.
  */
  void setDimtxsty(
    OdDbObjectId dimtxsty);

  /** Description:
    Sets the DIMTXT override ( *text* height) of this feature control frame.  
  */
  void setDimtxt(
    double dimtxt);

  virtual void getClassID(
    void** pClsid) const;

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  bool worldDraw(
    OdGiWorldDraw* pWd) const;

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);

  /** Note:
     This function is an override for OdDbEntity::subSetDatabaseDefaults() to set 
     the dimension style of this entity to the current style for the specified *database*.
  */
  void subSetDatabaseDefaults(
    OdDbDatabase *pDb);

  /** Description:
    Returns the WCS bounding points of this feature control frame.
    
    Arguments:
    boundingPoints (O) Receives the bounding points.
    
    Remarks:
    The points are returned as follows:
    
    @table
    Point                Corner
    boundingPoints[0]    Top left
    boundingPoints[1]    Top right
    boundingPoints[2]    Bottom right
    boundingPoints[3]    Bottom left
  */
  virtual void getBoundingPoints(
    OdGePoint3dArray& boundingPoints) const ;

  void subClose();

  /** Description:
    Copies the dimension style settings, including overrides, of this entity into the specified
    dimension style table record.
    
    Arguments:
    pRecord (O) Receives the effective dimension style data.
    
    Remarks:
    The *copied* data includes the dimension style data with all applicable overrides. 
  */
  void getDimstyleData(
    OdDbDimStyleTableRecord *pRecord) const;

  /** Description:
    Copies the dimension style settings, including overrides, from the specified
    dimension style table record to this entity.
    
    Arguments:
    pDimstyle (I) Pointer to non- *database* -resident dimension style record.
    dimstyleID (I) Database-resident dimension style record.

    Remarks:
    The *copied* data includes the dimension style with all applicable overrides. 
  */
  void setDimstyleData(
    const OdDbDimStyleTableRecord* pDimstyle);
  void setDimstyleData(
    OdDbObjectId dimstyleID);

  /** Description:
    Appends the consecutive distinct corner points of this feature control frame to the specified array.
    
    Arguments:
    ptArray (I/O) Receives corner points.
  */
  virtual void getBoundingPline(
    OdGePoint3dArray& 
    ptArray) const;
  /*
      virtual void getGripPoints(OdGePoint3dArray&, OdDbIntArray&, OdDbIntArray&) const;
      virtual void moveGripPointsAt(const OdDbIntArray&, const OdGeVector3d&);
  */
  
  virtual OdDbObjectPtr decomposeForSave(
    OdDb::DwgVersion ver, 
    OdDbObjectId& replaceId, 
    bool& exchangeXData);

};
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbFcf object pointers.
*/
typedef OdSmartPtr<OdDbFcf> OdDbFcfPtr;

#include "DD_PackPop.h"

#endif


