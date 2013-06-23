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

#ifndef OD_DBTABLESTYLE_H
#define OD_DBTABLESTYLE_H

#include "DD_PackPush.h"
#include "DbObject.h"
#include "DbColor.h"

/** Description: 
  
    {group:DD_Namespaces}
*/
namespace OdDb
{
  // OdDbTable and OdTbTableStyle specific enum
  //
  
  enum CellType          
  { 
    kUnknownCell    = 0,
    kTextCell       = 1,
    kBlockCell      = 2 
  };

  enum CellEdgeMask      
  { 
    kTopMask        = 1,
    kRightMask      = 2,
    kBottomMask     = 4,
    kLeftMask       = 8 
  };

  enum SelectType
  { 
    kWindow         = 1,
    kCrossing       = 2 
  };

  enum FlowDirection
  { 
    kTtoB           = 0,
    kBtoT           = 1 
  };

  enum RotationAngle     
  { 
    kDegreesUnknown = -1,
    kDegrees000     = 0,
    kDegrees090     = 1,
    kDegrees180     = 2,
    kDegrees270     = 3 
  };

  enum CellAlignment
  { 
    kTopLeft        = 1,
    kTopCenter      = 2,
    kTopRight       = 3,
    kMiddleLeft     = 4,
    kMiddleCenter   = 5,
    kMiddleRight    = 6,
    kBottomLeft     = 7,
    kBottomCenter   = 8,
    kBottomRight    = 9 
  };

  enum GridLineType
  { 
    kInvalidGridLine= 0,
    kHorzTop        = 1,
    kHorzInside     = 2,
    kHorzBottom     = 4,
    kVertLeft       = 8,
    kVertInside     = 0x10,
    kVertRight      = 0x20 
  };


  enum RowType
  { 
    kUnknownRow     = 0,
    kDataRow        = 1,
    kTitleRow       = 2,
    kHeaderRow      = 4
  };

  enum TableStyleFlags
  { 
    kHorzInsideLineFirst  = 1,
    kHorzInsideLineSecond = 2,
    kHorzInsideLineThird  = 4,
    kTableStyleModified   = 8 
  };


  enum RowTypes
  { 
    kAllRows = kDataRow | kTitleRow | kHeaderRow 
  };
    
  enum GridLineTypes 
  { 
    kAllGridLines = kHorzTop | kHorzInside | kHorzBottom | kVertLeft | kVertInside | kVertRight 
  };
}

/** Description:
    This class represents TableStyles for OdDbTable entities in an OdDbDatabase.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbTableStyle : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbTableStyle);

  OdDbTableStyle();
  // virtual ~OdDbTableStyle();

  // General Properties
  //
  
  /** Description:
    Returns the *name* of this TableStyle object.

    Arguments
    name (O) Receives the *name*.
    
    Note:
    Returns eOk if successful, or an appropriate error code if not.
  */
  
  virtual OdResult getName(
    OdString& name) const;

  /*
  virtual OdResult   setName(const OdString& name);
  */

  /** Description:
    Returns the *description* of this TableStyle object.
  */
  virtual OdString description() const;

  /** Description:
    Sets the *description* of this TableStyle object.
    
    Arguments:
    description (I) Description.
  */
  virtual void setDescription(
    const OdString& description);

  /** Description:
    Returns the bit flags for this TableStyle object (DXF 71). 
      
    Remarks:  
    bitFlags will return a combination of the following:
    
    @table
    Name                      Value
    kHorzInsideLineFirst      1
    kHorzInsideLineSecond     2
    kHorzInsideLineThird      4
    kTableStyleModified       8 
  */
  virtual OdUInt32 bitFlags() const;

  /** Description:
    Sets the bit flags for this TableStyle object (DXF 71). 
    
    Arguments:
    bitFlags (I) Bit flags.
    
    Remarks:
    bitFlags will be a combination of the following:
    
    @table
    Name    Value
    kHorzInsideLineFirst      1
    kHorzInsideLineSecond     2
    kHorzInsideLineThird      4
    kTableStyleModified       8 
  */
  virtual void setBitFlags(
    OdUInt32 bitFlags);

  /** Description:
    Returns the *direction* this TableStyle object flows from its first *row* to its last (DXF 70).

    Remarks:
    flowDirection will return one of the following:
    
    @table
    Name    Value   Description
    kTtoB   0       Top to Bottom
    kBtoT   1       Bottom to Top
  */
  virtual OdDb::FlowDirection flowDirection() const; 

  /** Description:
    Sets the *direction* this TableStyle objectflows from its first *row* to its last. (DXF 70).

    Arguments:
    flowDirection (I) Flow *direction*.
    
    Remarks:
    flowDirection will be one of the following: 
         
    @table
    Name    Value   Description
    kTtoB   0       Top to Bottom
    kBtoT   1       Bottom to Top
  */
  virtual void setFlowDirection(
    OdDb::FlowDirection flowDirection);

  /** Description:
    Returns the horizontal cell margin for this TableStyle object (DXF 40). 
    Remarks:
    The horizontal cell margin is the horizontal space between the cell text and the cell border.
  */
  virtual double horzCellMargin() const;

  /** Description:
    Sets the horizontal cell margin for this TableStyle object (DXF 40).
    
    Arguments:
    cellMargin (I) Cell margin.
    
    Remarks:
    The horizontal cell margin is the horizontal space between the cell text and the cell border.
  */
  virtual void   setHorzCellMargin(
    double cellMargin);


  /** Description:
    Returns the vertical cell margin for this TableStyle object (DXF 41). 

    Remarks:
    The vertical cell margin is the vertical space between the cell text and the cell border.
  */
  virtual double vertCellMargin() const;

  /** Description:
    Sets the vertical cell margin for this Table entity (DXF 41).
    
    Arguments:
    cellMargin (I) Cell margin.
    
    Remarks:
    The vertical cell margin is the vertical space between the cell text and the cell border.
  */
  virtual void setVertCellMargin(
    double cellMargin);

  /** Description:
      Returns true if and only if the title *row* is suppressed for this TableStyle object (DXF 280).
  */
  virtual bool isTitleSuppressed() const;

  /** Description:
    Controls the suppression of the title *row* (DXF 280).
    Arguments:
    suppress (I) Controls suppression.
  */
  virtual void suppressTitleRow(
    bool suppress);

  /** Description:
    Returns true if and only if the header *row* is suppressed for this TableStyle object (DXF 281). 
  */
  virtual bool isHeaderSuppressed() const;

  /** Description:
      Controls the suppression of the header *row* for this TableStyle object (DXF 280).
      Arguments:
      enable (I) Controls suppression.
  */
  virtual void suppressHeaderRow(
    bool suppress);


  /** Description:
    Returns the Object ID of the text style for the specified *row* type in this TableStyle object (DXF 7).

    Arguments:
    rowType (I) Row type.
    
    Remarks: 
    rowType will be one of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
  */
  virtual OdDbObjectId textStyle(
    OdDb::RowType rowType = OdDb::kDataRow) const;

  /** Description:
    Sets the Object ID of the text style for the specified *row* types for this TableStyle object (DXF 7).
    
    Arguments:
    rowTypes (I) Row types.
    textStyleId (I) Text style Object ID.

    Remarks: 
    rowTypes will be a combination of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
  */
  virtual void setTextStyle(
    const OdDbObjectId textStyleId, 
    int rowTypes = OdDb::kAllRows);

  /** Description:
    Returns the text *height* for the specified *row* type in this TableStyle object (DXF 140).

    Arguments:
    rowType (I) Row type
    
    Remarks:
    rowType will be one of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
  */
  virtual double textHeight(
    OdDb::RowType rowType = OdDb::kDataRow) const;

  /** Description:
    Sets the text *height* for the specified *row* types in this TableStyle object (DXF 140).

    Arguments:
    rowTypes (I) Row types.
    height (I) Text *height*.

    Remarks:
    rowTypes will be a combination of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
  */
  virtual void setTextHeight(
    double height, 
    int rowTypes = OdDb::kAllRows);

  /** Description:
    Returns the cell *alignment* for the specified *row* type in this TableStyle object (DXF 170).

    Arguments:
    rowType (I) Row type
    
    Remarks:
    rowType be return one of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
    
    alignment will be one of the following:
    
    @table                  
    Name              Value
    kTopLeft          1
    kTopCenter        2 
    kTopRight         3
    kMiddleLeft       4
    kMiddleCenter     5
    kMiddleRight      6
    kBottomLeft       7 
    kBottomCenter     8
    kBottomRight      9
  */
  virtual OdDb::CellAlignment alignment(
    OdDb::RowType rowType = OdDb::kDataRow) const;


  /** Description:
    Sets the cell *alignment* for the specified *row* types in this TableStyle object (DXF 170).
    
    Arguments:
    rowTypes (I) Row types.
    alignment (I) Alignment.

    Remarks: 
    rowTypes will be a combination of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
    
    alignment will be one of the following:
    
    @table
    Name              Value
    kTopLeft          1
    kTopCenter        2 
    kTopRight         3
    kMiddleLeft       4
    kMiddleCenter     5
    kMiddleRight      6
    kBottomLeft       7 
    kBottomCenter     8
    kBottomRight      9

  */
  virtual void setAlignment(
    OdDb::CellAlignment alignment, 
    int rowTypes = OdDb::kAllRows);

  /** Description:
    Returns the text *color* for the specified *row* type in this TableStyle object (DXF 62).

    Arguments:
    rowTypes (I) Row types.

    Remarks:
    rowTypes will be a combination of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
  */
  virtual OdCmColor color(
    OdDb::RowType rowType = OdDb::kDataRow) const;
    

  /** Description:
    Sets the text *color* for the specified *row* types in this TableStyle object (DXF 62).

    Arguments:
    rowTypes (I) Row types.
    color (I) Color.

    Remarks:
    rowTypes will be a combination of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
  */
  virtual void setColor(
    const OdCmColor& color,
    int rowTypes = OdDb::kAllRows);

  /** Description:
    Returns the background *color* for the specified *row* type in this TableStyle object (DXF 63).

    Arguments:
    rowType (I) Row type.

    Remarks: 
    rowType will be one of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
  */
  virtual OdCmColor backgroundColor(
    OdDb::RowType rowType = OdDb::kDataRow) const;

  /** Description:
    Sets the background *color* for the specified *row* type in this TableStyle object (DXF 63). 

    Arguments:
    rowTypes (I) Row types.
    color (I) Background *color*.
    
    Remarks: 
    rowTypes will be a combination of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
  */
  virtual void setBackgroundColor(
    const OdCmColor& color,
    int rowTypes = OdDb::kAllRows);

  /** Description:
    Returns true if and only if the background *color* for the specified *row* 
    type is disabled for this TableStyle object (DXF 283).

    Arguments:
    rowType (I) Row type
    row (I) Row index of the cell.
    column (I) Column index of the cell.

    Remarks:
    rowType will be one of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
  */
  virtual bool isBackgroundColorNone(
    OdDb::RowType rowType = OdDb::kDataRow) const;

  /** Description:
    Controls the background *color* setting for the specified *row* types or cell in this TableStyle object (DXF 283). 

    Arguments:
    disable (I) Disables the background *color* if true, enables if false.
    rowTypes (I) Row types.

    Remarks: 
    rowTypes will be a combination of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
  */
  virtual void setBackgroundColorNone(
    bool disable,
    int rowTypes = OdDb::kAllRows);

  //Gridline properties
  //
  
  /** Description:
    Returns the grid lineweight for the specified gridline type and *row* type in this TableStyle object (DXF 274-279).
      
    Arguments:      
    gridlineType (I) Gridline type.
    rowType (I) Row type.

    Remarks:    
    gridlineType will be one of the following:
    
    @table
    Name              Value
    kHorzTop          1
    kHorzInside       2
    kHorzBottom       4
    kVertLeft         8
    kVertInside       0x10
    kVertRight        0x20
    
    rowType will be one of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
    
  */
  virtual OdDb::LineWeight gridLineWeight(
    OdDb::GridLineType gridlineType,
    OdDb::RowType rowType = OdDb::kDataRow) const; 
  
  /** Description:
    Sets the grid lineweight for the specified gridline types and *row* types,
    or the specified cell and edges in this Table entity (DXF 274-279).
      
    Arguments:
    lineWeight (I) Lineweight.      
    gridlineTypes (I) Gridline types.
    rowTypes (I) Row types.

    Remarks:    
    gridlineTypes will be a combination of the following:
    
    @table
    Name              Value
    kHorzTop          1
    kHorzInside       2
    kHorzBottom       4
    kVertLeft         8
    kVertInside       0x10
    kVertRight        0x20
    
    rowTypes will be a combination of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
  */
  virtual void setGridLineWeight(
    OdDb::LineWeight lineWeight, 
    int gridlineTypes = OdDb::kAllGridLines, 
    int rowTypes = OdDb::kAllRows);


  /** Description:
    Returns the grid *color* for the specified gridline type and *row* type
    in this TableStyle object (DXF 63,64,65,66,68,69).
      
    Arguments:      
    gridlineType (I) Gridline type.
    rowType (I) Row type.

    Remarks:    
    gridlineType will be one of the following:
    
    @table
    Name              Value
    kHorzTop          1
    kHorzInside       2
    kHorzBottom       4
    
    rowType will be one of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
  */
  virtual OdCmColor gridColor(
    OdDb::GridLineType gridlineType,
    OdDb::RowType rowType = OdDb::kDataRow) const; 

  /** Description:
    Returns the grid *color* for the specified gridline types and *row* type
    in this TableStyle object (DXF 63,64,65,66,68,69).
      
    Arguments:      
    gridlineTypes (I) Gridline types.
    rowTypes (I) Row types.

    Remarks:    
    gridlineTypes will be a combination of the following:
    
    @table
    Name              Value
    kHorzTop          1
    kHorzInside       2
    kHorzBottom       4
    
    rowTypes will be a combination of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
  */
  virtual void setGridColor(
    const OdCmColor color, 
    int gridlineTypes = OdDb::kAllGridLines, 
    int rowTypes = OdDb::kAllRows);


  /** Description:
    Returns the grid visibility for the specified gridline type and *row* type,
    in this TableStyle object (DXF 284-289).
      
    Arguments:      
    gridlineType (I) Gridline type.
    rowType (I) Row type.

    Remarks:    
    gridVisibility will return one of the following:
    
    @table
    Name              Value
    kInvisible        1
    kVisible          0 
        
    gridlineType will be one of the following:
    
    @table
    Name              Value
    kHorzTop          1
    kHorzInside       2
    kHorzBottom       4
    kVertLeft         8
    kVertInside       0x10
    kVertRight        0x20
    
    rowType will be one of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
    
  */
  virtual OdDb::Visibility gridVisibility(
    OdDb::GridLineType gridlineType,
    OdDb::RowType rowType = OdDb::kDataRow) const; 


/** Description:
    Sets the grid visibility for the specified gridline types and *row* types,
    in this TableStyle object (DXF 284-289).
      
    Arguments:
    gridVisibility (I) Grid visibility.      
    gridlineTypes (I) Gridline types.
    rowTypes (I) Row types.

    Remarks:
    gridVisibility will be one of the following:
    
    @table
    Name              Value
    kInvisible        1
    kVisible          0 
        
    gridlineTypes will be a combination of the following:
    
    @table
    Name              Value
    kHorzTop          1
    kHorzInside       2
    kHorzBottom       4
    kVertLeft         8
    kVertInside       0x10
    kVertRight        0x20
    
    rowTypes will be a combination of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
    
  */
  virtual void setGridVisibility(
    OdDb::Visibility gridVisiblity, 
    int gridlineTypes = OdDb::kAllGridLines, 
    int rowTypes = OdDb::kAllRows);

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  // virtual OdResult audit(OdDbAuditInfo* pAuditInfo);

  // Utility functions
  //

  /** Description:
    Applies the default properties of the specified *database* to this TableStyle object.
    
    Arguments:
    pDb (I) Pointer to the *database* whose default values are to be used.
     
    Remarks:
    If pDb is NULL, the *database* containing this object is used
  */
  void setDatabaseDefaults(
    OdDbDatabase* pDb = NULL);

  /** Description:
    Adds this specified TableStyle object to the specified *database*.
  
    Arguments:
    pDb (I) Pointer to the *database* in which to post.
    styleName (I) Name for the table style. 
    tableStyleId (O) Receives the object ID of the posted table style.

    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  virtual OdResult postTableStyleToDb(
    OdDbDatabase* pDb, 
    const OdString& styleName, 
    OdDbObjectId& tableStyleId);

  virtual void getClassID(
    void** pClsid) const;
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbTableStyle object pointers.
*/
typedef OdSmartPtr<OdDbTableStyle> OdDbTableStylePtr;

#include "DD_PackPop.h"

#endif // OD_DBTABLESTYLE_H

