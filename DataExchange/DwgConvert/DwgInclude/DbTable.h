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

#ifndef OD_DBTABLE_H
#define OD_DBTABLE_H

#include "DD_PackPush.h"
#include "DbBlockReference.h"
#include "DbTableStyle.h"
// typedef for OdSubentPathArray
//
class OdDbTableImpl;

typedef OdArray<OdDbFullSubentPath,
         OdMemoryAllocator<OdDbFullSubentPath> > OdSubentPathArray;


/** Description:
    This class represents Table entities in an OdDbDatabase instance.

    Library:
    Db

    {group:OdDb_Classes} 

*/
class TOOLKIT_EXPORT OdDbTable: public OdDbBlockReference
{
public:
  ODDB_DECLARE_MEMBERS(OdDbTable);

  enum TableStyleOverrides {

    kTitleSuppressed             = 1,
    kHeaderSuppressed            = 2,
    kFlowDirection               = 3,         
    kHorzCellMargin              = 4,
    kVertCellMargin              = 5,
    kTitleRowColor               = 6,
    kHeaderRowColor              = 7,
    kDataRowColor                = 8,
    kTitleRowFillNone            = 9,
    kHeaderRowFillNone           = 10,
    kDataRowFillNone             = 11,
    kTitleRowFillColor           = 12,
    kHeaderRowFillColor          = 13,
    kDataRowFillColor            = 14,
    kTitleRowAlignment           = 15,
    kHeaderRowAlignment          = 16,
    kDataRowAlignment            = 17,
    kTitleRowTextStyle           = 18,
    kHeaderRowTextStyle          = 19,
    kDataRowTextStyle            = 20,
    kTitleRowTextHeight          = 21,
    kHeaderRowTextHeight         = 22,
    kDataRowTextHeight           = 23,

    kTitleHorzTopColor           = 40,
    kTitleHorzInsideColor        = 41,
    kTitleHorzBottomColor        = 42,
    kTitleVertLeftColor          = 43,
    kTitleVertInsideColor        = 44,
    kTitleVertRightColor         = 45,

    kHeaderHorzTopColor          = 46,
    kHeaderHorzInsideColor       = 47,
    kHeaderHorzBottomColor       = 48,
    kHeaderVertLeftColor         = 49,
    kHeaderVertInsideColor       = 50,
    kHeaderVertRightColor        = 51,

    kDataHorzTopColor            = 52,
    kDataHorzInsideColor         = 53,
    kDataHorzBottomColor         = 54,
    kDataVertLeftColor           = 55,
    kDataVertInsideColor         = 56,
    kDataVertRightColor          = 57,

    kTitleHorzTopLineWeight      = 70,
    kTitleHorzInsideLineWeight   = 71,
    kTitleHorzBottomLineWeight   = 72,
    kTitleVertLeftLineWeight     = 73,
    kTitleVertInsideLineWeight   = 74,
    kTitleVertRightLineWeight    = 75,

    kHeaderHorzTopLineWeight     = 76,
    kHeaderHorzInsideLineWeight  = 77,
    kHeaderHorzBottomLineWeight  = 78,
    kHeaderVertLeftLineWeight    = 79,
    kHeaderVertInsideLineWeight  = 80,
    kHeaderVertRightLineWeight   = 81,

    kDataHorzTopLineWeight       = 82,
    kDataHorzInsideLineWeight    = 83,
    kDataHorzBottomLineWeight    = 84,
    kDataVertLeftLineWeight      = 85,
    kDataVertInsideLineWeight    = 86,
    kDataVertRightLineWeight     = 87,

    kTitleHorzTopVisibility      = 100,
    kTitleHorzInsideVisibility   = 101,
    kTitleHorzBottomVisibility   = 102,
    kTitleVertLeftVisibility     = 103,
    kTitleVertInsideVisibility   = 104,
    kTitleVertRightVisibility    = 105,

    kHeaderHorzTopVisibility     = 106,
    kHeaderHorzInsideVisibility  = 107,
    kHeaderHorzBottomVisibility  = 108,
    kHeaderVertLeftVisibility    = 109,
    kHeaderVertInsideVisibility  = 110,
    kHeaderVertRightVisibility   = 111,

    kDataHorzTopVisibility       = 112,
    kDataHorzInsideVisibility    = 113,
    kDataHorzBottomVisibility    = 114,
    kDataVertLeftVisibility      = 115,
    kDataVertInsideVisibility    = 116,
    kDataVertRightVisibility     = 117,

    kCellAlignment               = 130,
    kCellBackgroundFillNone      = 131,
    kCellBackgroundColor         = 132,
    kCellContentColor            = 133,
    kCellTextStyle               = 134,
    kCellTextHeight              = 135,
    kCellTopGridColor            = 136,
    kCellRightGridColor          = 137,
    kCellBottomGridColor         = 138,
    kCellLeftGridColor           = 139,
    kCellTopGridLineWeight       = 140,
    kCellRightGridLineWeight     = 141,
    kCellBottomGridLineWeight    = 142,
    kCellLeftGridLineWeight      = 143,
    kCellTopVisibility           = 144,
    kCellRightVisibility         = 145,
    kCellBottomVisibility        = 146,
    kCellLeftVisibility          = 147    
  };

  OdDbTable();
  // virtual ~OdDbTable();


  /** Description:
    Returns the Object ID of the OdDbTableStyle used by this Table entity (DXF 342).
  */
  virtual OdDbObjectId tableStyle() const;

  /** Description:
    Sets the Object ID of the OdDbTableStyle for use by this Table entity (DXF 342).

    Arguments:
    tableStyleId (I) Object ID of the table style.

    Remarks:
    Returns eOk if successful, eInvalidInput otherwise.
  */
  virtual void setTableStyle(
    const OdDbObjectId& tableStyleId);

  /** Description:
    Returns the unit X-axis for this Table entity in WCS coordinates (DXF 11,21,31).
  */
  virtual OdGeVector3d direction() const;
  
  /** Description:
    Sets the X-axis for this Table entity in WCS coordinates (DXF 11,21,31).
    
    Remarks:
    horizVector (I) Horizontal vector.
  */
  virtual void setDirection(
    const OdGeVector3d& horizVector);

  /** Description:
    Returns the number of rows in this Table entity (DXF 91).
    
    Note:
    This includes title and header rows, if any. 
  */
  virtual OdUInt32 numRows() const;

  /** Description:
    Sets the number of rows for this Table entity (DXF 91).
    
    Arguments:
    numRows (I) Number of rows.
    
    Note:
    This includes title and header rows, if any.
    
    The number of rows must be greater than zero.
  */
  virtual void setNumRows(
    OdUInt32 numRows);

  /** Description:
    Returns the number of columns in this Table entity (DXF 92). 
  */
  virtual OdUInt32 numColumns() const;

  /** Description:
    Sets the number of columns for this Table entity (DXF 92). 
    
    Arguments:
    numColumns (I) Number of columns.
    
    Remarks:
    The number of columns must be greater than zero.    
  */
  virtual void setNumColumns(OdUInt32 numColumns);

  /** Description:
    Returns the overall *width* of this Table entity.
  */
  virtual double width() const;

  /** Description:
    Sets the overall *width* for this Table entity. 
    
    Arguments:
    width (I) Overall *width*.
    
    Remarks:
    Column widths may be adjusted proportionally.
    
    See also:
    columWidth
  */
  virtual void setWidth(double width);

  /** Description:
    Returns the *width* of the specified *column* in this Table entity (DXF 142).
    
    Arguments:
    column (I) Column index. 
  */
  virtual double columnWidth(OdUInt32 column) const;
  
  /** Description:
    Sets the *width* of all columns or the specified *column* in this Table entity (DXF 142).

    Arguments:
    column (I) Column index. 
    width (I) Column *width*.
  */
  virtual void setColumnWidth(
    OdUInt32 column, 
    double width);
  virtual void setColumnWidth(
    double width);

  /** Description:
    Returns the overall *height* of this Table entity.
  */
  virtual double height() const;

  /** Description:
    Sets the overall *height* of this Table entity.
    
    Arguments:
    height (I) Overall *height*.
      
    Remarks:
    Row heights may be adjusted proportionally.
  */
  virtual void setHeight(
    double height);

  /** Description:
    Returns the *height* of the specified *row* in this Table entity (DXF 141).
    
    Arguments:
    row (I) Row index.
  */
  virtual double rowHeight(
    OdUInt32 row) const;
  
  /** Description:
    Sets the *height* of all rows or the specified *row* in this Table entity (DXF 141).
    Arguments:
    row (I) Row index.
    height (I) Row *height*.
  */
  virtual void setRowHeight(
    OdUInt32 row, 
    double height);

  virtual void setRowHeight(
    double height);
  
  /** Description:
    Returns the minimum *column* *width* for the specified *column* in this Table entity.
    Arguments:
    column (I) Column index. 
  */
  virtual double minimumColumnWidth(
    OdUInt32 column) const;

  /** Description:
    Returns the minimum *row* *height* for the specified *row* in this Table entity.
    Arguments:
    row (I) Row index.
  */
  virtual double minimumRowHeight(
    OdUInt32 row) const;    

  /** Description:
    Returns the minimum overall *width* for this Table entity.
  */
  virtual double minimumTableWidth() const;
  
  /** Description:
    Returns the minimum overall *height* for this Table entity.
  */
  virtual double minimumTableHeight() const;        

  //********************************************************************
  // Get and set methods for table style overrides
  //********************************************************************
  //
  
  /** Description:
    Returns the horizontal cell margin for this Table entity (DXF 40). 
    Remarks:
    The horizontal cell margin is the horizontal space between the cell text and the cell border.
  */
  virtual double horzCellMargin() const;

  /** Description:
    Sets the horizontal cell margin for this Table entity (DXF 40).
    
    Arguments:
    cellMargin (I) Cell margin.
    
    Remarks:
    The horizontal cell margin is the horizontal space between the cell text and the cell border.
  */
  virtual void setHorzCellMargin(
    double cellMargin);

  /** Description:
    Returns the vertical cell margin for this Table entity (DXF 41). 

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
  virtual void setVertCellMargin(double cellMargin);

  /** Description:
    Returns the *direction* this Table entity flows from its first *row* to its last (DXF 70).

    Remarks:
    flowDirection will return one of the following:
    
    @table
    Name    Value   Description
    kTtoB   0       Top to Bottom
    kBtoT   1       Bottom to Top
  */
  virtual OdDb::FlowDirection flowDirection() const; 

  /** Description:
    Sets the *direction* this Table entity flows from its first *row* to its last. (DXF 70).

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
      Returns true if and only if the title *row* is suppressed for this Table entity (DXF 280).
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
    Returns true if and only if the header *row* is suppressed for this Table entity (DXF 281). 
  */
  virtual bool isHeaderSuppressed() const;

  /** Description:
      Controls the suppression of the header *row* for this Table entity (DXF 280).
      Arguments:
      enable (I) Controls suppression.
  */
  virtual void suppressHeaderRow(
    bool suppress);

  /** Description:
    Returns the cell *alignment* for the specified *row* type or cell in this Table entity (DXF 170).

    Arguments:
    rowType (I) Row type
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    
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
  virtual OdDb::CellAlignment alignment(
    OdUInt32 row, 
    OdUInt32 column) const;

  /** Description:
    Sets the cell *alignment* for the specified *row* types or cell in this Table entity (DXF 170).
    
    Arguments:
    rowTypes (I) Row types.
    alignment (I) Alignment.
    row (I) Row index of the cell.
    column (I) Column index of the cell.

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
    OdUInt32 rowTypes = OdDb::kAllRows);
  virtual void setAlignment(
    OdUInt32 row, 
    OdUInt32 column, 
    OdDb::CellAlignment alignment);

  /** Description:
    Returns true if and only if the background *color* for the specified *row* 
    type or cell is disabled for this Table entity (DXF 283).

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
  virtual bool isBackgroundColorNone(
    OdUInt32 row, 
    OdUInt32 column) const;


  /** Description:
    Controls the background *color* setting for the specified *row* types or cell in this Table entity (DXF 283). 

    Arguments:
    disable (I) Disables the background *color* if true, enables if false.
    rowTypes (I) Row types.
    row (I) Row index of the cell.
    column (I) Column index of the cell.

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
    OdUInt32 rowTypes = OdDb::kAllRows);
  virtual void setBackgroundColorNone(
    OdUInt32 row, 
    OdUInt32 column, 
    bool disable);
    
  /** Description:
    Returns the background *color* for the specified *row* type or cell in this Table entity (DXF 63).

    Arguments:
    rowType (I) Row type.
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
  virtual OdCmColor backgroundColor(
    OdDb::RowType rowType = OdDb::kDataRow) const;
  virtual OdCmColor backgroundColor(
    OdUInt32 row, 
    OdUInt32 column) const;

  /** Description:
    Sets the background *color* for the specified *row* types or cell in this Table entity (DXF 63). 

    Arguments:
    rowTypes (I) Row types.
    color (I) Background *color*.
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    
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
    OdUInt32 rowTypes = OdDb::kAllRows);
  virtual void setBackgroundColor(
    OdUInt32 row, 
    OdUInt32 column,
    const OdCmColor& color);

  /** Description:
    Returns the content *color* for the specified *row* type or cell in this Table entity (DXF 64).

    Arguments:
    rowType (I) Row type.
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
  virtual OdCmColor contentColor(
    OdDb::RowType rowType = OdDb::kDataRow) const;
  virtual OdCmColor contentColor(
    OdUInt32 row, 
    OdUInt32 column) const;

  /** Description:
    Sets the content *color* for the specified *row* types or cell in this Table entity (DXF 64). 

    Arguments:
    rowTypes (I) Row types.
    color (I) Content *color*.
    row (I) Row index of the cell.
    column (I) Column index of the cell.
   
    Remarks: 
    rowTypes will be a combination of the following:
    
    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
  */
  virtual void setContentColor(
    const OdCmColor& color, 
    OdUInt32 nRowType = OdDb::kAllRows);
  virtual void setContentColor(
    OdUInt32 row, 
    OdUInt32 column,
    const OdCmColor& color);

  /** Description:
    Returns the Object ID of the text style for the specified *row* type or cell in this Table entity (DXF 7).

    Arguments:
    rowType (I) Row type.
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
  virtual OdDbObjectId textStyle(
    OdDb::RowType rowType = OdDb::kDataRow) const;
  virtual OdDbObjectId textStyle(
    OdUInt32 row, 
    OdUInt32 column) const;

  /** Description:
    Sets the Object ID of the text style for the specified *row* types or cell in this Table entity (DXF 7).
    
    Arguments:
    rowTypes (I) Row types.
    row (I) Row index of the cell.
    column (I) Column index of the cell.
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
    const OdDbObjectId& textStyleId, 
    OdUInt32 rowTypes = OdDb::kAllRows);
  virtual void setTextStyle(
    OdUInt32 row, 
    OdUInt32 column, 
    const OdDbObjectId& textStyleId);
    
  /** Description:
    Returns the text *height* for the specified *row* type or cell in this Table entity (DXF 140).

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
  virtual double textHeight(
    OdDb::RowType rowType = OdDb::kDataRow) const;
  virtual double textHeight(
    OdUInt32 row, 
    OdUInt32 column) const;


  /** Description:
    Sets the text *height* for the specified *row* types or cell in this Table entity (DXF 140).

    Arguments:
    rowTypes (I) Row types.
    row (I) Row index of the cell.
    column (I) Column index of the cell.
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
    OdUInt32 rowTypes = OdDb::kAllRows);
  virtual void setTextHeight(
    OdUInt32 row, 
    OdUInt32 column, 
    double height);


  /** Description:
    Returns the grid lineweight for the specified gridline type and *row* type,
    or the specified cell and edge in this Table entity (DXF 274-279).
      
    Arguments:      
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    edgeType (I) Edge type.
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
    
    edgeType will be one of the following:

    @table
    Name              Value
    kTopMask          1
    kRightMask        2
    kBottomMask       4
    kLeftMask         8
  */
  virtual OdDb::LineWeight gridLineWeight(
    OdDb::GridLineType gridlineType,
    OdDb::RowType rowType = OdDb::kDataRow) const; 
  virtual OdDb::LineWeight gridLineWeight(
    OdUInt32 row, 
    OdUInt32 column,
    OdDb::CellEdgeMask edgeType) const;

  /** Description:
    Sets the grid lineweight for the specified gridline types and *row* types,
    or the specified cell and edges in this Table entity (DXF 274-279).
      
    Arguments:
    lineWeight (I) Lineweight.      
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    edgeTypes (I) Edge types.
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
    
    edgeTypes will be a combination of the following:

    @table
    Name              Value
    kTopMask          1
    kRightMask        2
    kBottomMask       4
    kLeftMask         8
  */
  virtual void setGridLineWeight(
    OdDb::LineWeight lineWeight, 
    OdUInt32 gridlineTypes, 
    OdUInt32 rowTypes);
  virtual void setGridLineWeight(
    OdUInt32 row, 
    OdUInt32 column, 
    OdInt16 edgeTypes,
    OdDb::LineWeight lineWeight);

  /** Description:
    Returns the grid *color* for the specified gridline type and *row* type,
    or the specified cell and edge in this Table entity (DXF 63,64,65,66,68,69).
      
    Arguments:      
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    edgeType (I) Edge type.
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
    
    edgeType will be one of the following:

    @table
    Name              Value
    kTopMask          1
    kRightMask        2
    kBottomMask       4
    kLeftMask         8
  */
  virtual OdCmColor gridColor(
    OdDb::GridLineType gridlineType,
    OdDb::RowType rowType = OdDb::kDataRow) const; 
  virtual OdCmColor gridColor(
    OdUInt32 row, 
    OdUInt32 column,
    OdDb::CellEdgeMask edgeType) const;


  /** Description:
    Returns the grid visibility for the specified gridline type and *row* type,
    or the specified cell and edge in this Table entity (DXF 284-289).
      
    Arguments:      
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    edgeType (I) Edge type.
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
    
    edgeType will be one of the following:

    @table
    Name              Value
    kTopMask          1
    kRightMask        2
    kBottomMask       4
    kLeftMask         8
  */
  virtual OdDb::Visibility gridVisibility(
    OdDb::GridLineType gridlineType,
    OdDb::RowType rowType = OdDb::kDataRow) const; 
  virtual OdDb::Visibility gridVisibility(
    OdUInt32 row, 
    OdUInt32 column,
    OdDb::CellEdgeMask edgeType) const;

/** Description:
    Sets the grid visibility for the specified gridline types and *row* types,
    or the specified cell and edges in this Table entity (DXF 284-289).
      
    Arguments:
    gridVisibility (I) Grid visibility.      
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    edgeTypes (I) Edge types.
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
    
    edgeTypes will be a combination of the following:

    @table
    Name              Value
    kTopMask          1
    kRightMask        2
    kBottomMask       4
    kLeftMask         8
  */
  virtual void setGridVisibility(
    OdDb::Visibility gridVisiblity, 
    OdUInt32 gridlineTypes, 
    OdUInt32 rowTypes);
  virtual void setGridVisibility(
    OdUInt32 row, 
    OdUInt32 column, 
    OdInt16 edgeTypes,
    OdDb::Visibility gridVisibility);

  /** Description:
    Returns the table style *overrides* for this Table entity. 

    Arguments:
    overrides (O) Receives an array of table style *overrides* for this Table entity.         
    
    Remarks:
    Returns true and only if successful.
  */
  virtual bool tableStyleOverrides( 
    OdArray<OdUInt32>& overrides) const;

  /** Description:
    Clears the table style *overrides* for this Table entity and/or its cells. 

    Arguments:
    option (I) Option.
        
    Remarks:
    option can be one of the following:
    
    @table
    Action                        Value
    Clears all overrides.         0
    Clears all table overrides.   1
    Clears all cell overrides.    2
  */
  virtual void clearTableStyleOverrides(
    int option = 0);
  
  /** Description:
    Returns the cell type of the specified cell in this Table entity.

    Arguments:      
    row (I) Row index of the cell.
    column (I) Column index of the cell.

    Remarks:
    cellType will return one of the following:
    
    @table
    Name                    Value
    kTextCell    1
    kBlockCell   2
  */
  virtual OdDb::CellType cellType(
    OdUInt32 row, 
    OdUInt32 column) const;

  /** Description:
    Sets the cell type for the specified cell in this Table entity.

    Arguments:      
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    cellType (I) Cell type.
    
    Remarks:
    cellType will be one of the following:
    
    @table
    Name                    Value
    AcDbTable::kTextCell    1
    AcDbTable::kBlockCell   2
  */
  virtual void setCellType(
    OdUInt32 row, 
    OdUInt32 column, 
    OdDb::CellType cellType);

  /** Description:
    Returns the cell extents for the specified cell in this Table entity.

    Arguments:
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    isOuterCell (I) If and only if true, ignores margins.
    pts (O) Receives the cell extents information.
    
    Remarks:
    If isOuterCell is true, this function returns the *extents* of the cell without regard to margins.
    
    If isOuterCell is false, this function returns the *extents( of cell reduced
    by the horizontal and vertical cell margins.
  */
  virtual void getCellExtents(
    OdUInt32 row, 
    OdUInt32 column,
    bool isOuterCell, 
    OdGePoint3dArray& pts) const;

  /** Description:
    Returns the attachment point of the specified cell in this Table entity.
    
    Arguments:      
    row (I) Row index of the cell.
    column (I) Column index of the cell.
  */
  virtual OdGePoint3d attachmentPoint(
    OdUInt32 row, 
    OdUInt32 column) const; 

 
  /** Description:
    Returns the cell style *overrides* for the specified cell in this Table entity.
      
    Arguments:      
    row (I) Row index of the 
    column (I) Column index of the cell.
    overrides (O) Receives the *overrides*.   
  */
  virtual bool cellStyleOverrides(
    OdUInt32 row, 
    OdUInt32 column,
    OdArray<OdUInt32>& overrides) const;
  
  /** Description:
    Deletes the content of the specified cell in this Table entity.

    Arguments:      
    row (I) Row index of the cell.
    column (I) Column index of the cell.
  */
  virtual void deleteCellContent(
    OdUInt32 row, 
    OdUInt32 column);

  /** Description:
    Returns the type of the specified *row* in this Table entity.
    
    Arguments:
    row (I) Row index.
  
    Remarks:
    rowType will return one of the following:

    @table
    Name              Value
    kTitleRow         1
    kHeaderRow        2
    kDataRow          4
  */
  virtual OdDb::RowType rowType(
    OdUInt32 row) const;

  /** Description:
    Returns the text string in the specified cell in this Table entity.

    Arguments:      
    row (I) Row index of the cell.
    column (I) Column index of the cell.
  */
  virtual OdString textString(
    OdUInt32 row, 
    OdUInt32 column) const;

  /** Description:
    Sets the text string in the for the specified cell in this Table entity.

    Arguments:      
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    textString (I) Text string.
  */
  virtual void setTextString(
    OdUInt32 row, 
    OdUInt32 column, 
    const OdString& textString);
    
  /** Description:
    Returns the Object ID of the OdDbField in the specified cell in this Table entity. 
      
    Arguments:
    row (I) Row index of the cell.
    column (I) Column index of the cell.
  */
  virtual OdDbObjectId fieldId(
    OdUInt32 row, 
    OdUInt32 column) const;

  /** Description:
    Sets the Object ID for OdDbField in the specified cell in this Table entity.

    Arguments:
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    fieldId (I) Object ID of the AdDbField.
  */
  virtual void setFieldId(
    OdUInt32 row, 
    OdUInt32 column, 
    const OdDbObjectId& fieldId);


  /** Description:
    Returns the text *rotation* angle for the specified cell in this Table entity. 

    Arguments:
    row (I) Row index of the cell.
    column (I) Column index of the cell.

    Remarks:
    textRotation will return one of the following:
    
    @table
    Name              Value     Description
    kDegrees000       0         0°
    kDegrees090       1         90°
    kDegrees180       2         180°
    kDegrees270       3         270°
  */
  virtual OdDb::RotationAngle textRotation(
    OdUInt32 row, 
    OdUInt32 column) const;

  /** Description:
    Sets the text *rotation* angle of the text in the specified cell in this Table entity. 

    Arguments:
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    textRotation (I) Text *rotation* angle.
    
    textRotation will be one of the following:
    
    @table
    Name              Value     Description
    kDegrees000       0         0°
    kDegrees090       1         90°
    kDegrees180       2         180°
    kDegrees270       3         270°
  */
  virtual void setTextRotation(
    OdUInt32 row, 
    OdUInt32 column, 
    OdDb::RotationAngle textRotation);
  

  /** Description:
    Returns true if and only if the block in the specified cell in this Table entity is
    automatically scaled and positioned to fit into the cell.
     
    Arguments:
    row (I) Row index of the cell.
    column (I) Column index of the cell.
  */
  virtual bool isAutoScale(
    OdUInt32 row, 
    OdUInt32 column) const;

  /** Description:
    Determines if the  block in the specified cell in this Table entity is to be
    automatically scaled and positioned to fit into the cell.

    Arguments:
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    autoScale (I) True to autoscale the block to the cell.
  */
  virtual void setAutoScale(
    OdUInt32 row, 
    OdUInt32 column, 
    bool autoScale);
 
  /** Description:
    Returns the Object ID of the block table record in the specified cell in this Table entity. 

    Arguments:
    row (I) Row index of the cell.
    column (I) Column index of the cell.
  */
  virtual OdDbObjectId blockTableRecordId(
    OdUInt32 row, 
    OdUInt32 column) const;

  /** Description:
    Sets the Object ID of the block table record in the specified cell in this Table entity 

    Arguments:
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    blockId (I) Object ID of the block.
    autoScale (I) If true, autoscales the block to the cell.
  */
  virtual void setBlockTableRecordId(
    OdUInt32 row, 
    OdUInt32 column,
    const OdDbObjectId& blockId, 
    bool autoScale = false);

  /** Description:
    Returns the scale factor of the block reference in the specified cell in this Table entity 

    Arguments:
    row (I) Row index of the cell.
    column (I) Column index of the cell.
  */
  virtual double blockScale(
    OdUInt32 row, 
    OdUInt32 column) const;

  /** Description:
    Sets the *scale* factor of the block reference in the specified cell in this Table entity 

    Arguments:
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    blockScale (I) Uniform *scale* factor.
    
    Note:
    blockScale cannot be zero.
  */
  virtual void setBlockScale(
    OdUInt32 row, 
    OdUInt32 column, 
    double blockScale);

  /** Description:
    Returns the *rotation* angle of the block reference in the specified cell in this Table entity

    Arguments:
    row (I) Row index of the cell.
    column (I) Column index of the cell.
  */
  virtual double blockRotation(
    OdUInt32 row, 
    OdUInt32 column) const;

  /** Description:
    Sets the *rotation* angle of the block reference in the specified cell in this Table entity

    Arguments:
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    blockRotation (I) Rotation angle.
  */
  virtual void setBlockRotation(
    OdUInt32 row, 
    OdUInt32 column, 
    double blockRotation);

  /** Description:
    Gets the attribute *value* for the specified Object ID key for the specified cell in this Table entity

    Arguments:
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    attdefId (I) Object ID of the OdDbAttributeDefinition.
    attValue (O) Receives the attribute *value*.
  */
  virtual void getBlockAttributeValue(
    OdUInt32 row, 
    OdUInt32 column, 
    const OdDbObjectId& attdefId, 
    OdString& attValue) const;

  /** Description:
    Sets the attribute *value* for the specified Object ID key for the specified cell in this Table entity

    Arguments:
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    attdefId (I) Object ID of the OdDbAttributeDefinition.
    attValue (O) Sets the attribute *value*.
  */
  virtual void setBlockAttributeValue(
    OdUInt32 row, 
    OdUInt32 column,
    const OdDbObjectId& attdefId, 
    const OdString& value);

  /** Description:
    Returns the grid *color* for the specified gridline types and *row* type,
    or the specified cell and edges in this Table entity (DXF 63,64,65,66,68,69).
      
    Arguments:      
    row (I) Row index of the cell.
    column (I) Column index of the cell.
    edgeTypes (I) Edge types.
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
    
    edgeTypes will be a combination of the following:

    @table
    Name              Value
    kTopMask          1
    kRightMask        2
    kBottomMask       4
    kLeftMask         8
  */
  virtual void setGridColor(
    const OdCmColor& color, 
    OdUInt32 gridlineTypes, 
    OdUInt32 rowTypes);
  virtual void setGridColor(
    OdUInt32 row, 
    OdUInt32 column, 
    OdInt16 edgeTypes,
    const OdCmColor& color);

  
  /** Description:
    Inserts the specified number of columns into this Table entity at the specified *column* index.
    Arguments:
    column (I) Column index.
    width (I) Width of the inserted columns.
    nCols (I) Number of columns to insert.
  */
  virtual void insertColumns(
    OdUInt32 column, 
    double width, 
    OdUInt32 nCols = 1);

  /** Description:
    Deletes the specified number of columns from this Table entity.

    Arguments:      
    column (I) Index of first *column* to delete.
    nCols (I) Number of columns to delete. 
  */
  virtual void deleteColumns(
    OdUInt32 column, 
    OdUInt32 nCols = 1);

  /** Description:
    Inserts the specified number of rows into this Table entity at the specified *row* index.
    Arguments:
    row (I) Row index.
    height (I) Height of the inserted rows.
    nRows (I) Number of rows to insert.
  */  
  virtual void insertRows(
    OdUInt32 row, 
    double height, 
    OdUInt32 nRows = 1);
  
  /** Description:
    Deletes the specified number of rows from this Table entity.

    Arguments:      
    row (I) Index of first *row* to delete.
    nRows (I) Number of rows to delete. 
      
  */
  virtual void deleteRows(
    OdUInt32 row, 
    OdUInt32 nRows = 1);

 
  /** Description:
    Merges a rectangular region of cells in this Table entity.
      
    Arguments:
    minRow (I) Minimum *row* index of the merged cells.
    maxRow (I) Maximum *row* index of the merged cells. 
    minColumn (I) Minimum *column* index of the merged cells.
    maxColumn (I) Maximum *column* index of the merged cells. 
  */  
  virtual void mergeCells(
    OdUInt32 minRow, 
    OdUInt32 maxRow,
    OdUInt32 minColumn, 
    OdUInt32 maxColumn);

  /** Description:
    Unmerges a rectangular region of cells in this Table entity.
      
    Arguments:
    minRow (I) Minimum *row* index of the merged cells.
    maxRow (I) Maximum *row* index of the merged cells. 
    minColumn (I) Minimum *column* index of the merged cells.
    maxColumn (I) Maximum *column* index of the merged cells. 
  */  
  virtual void unmergeCells(
    OdUInt32 minRow, 
    OdUInt32 maxRow,
    OdUInt32 minColumn, 
    OdUInt32 maxColumn);
    
  /** Description:
    Returns true if and only if the specified cell has been merged, and 
    the range of the merged cells in this Table entity.
    
    Arguments:
    row (I) Row index.
    column (I) Column index.
    minRow (O) Receives the minimum *row* index of the merged cells.
    maxRow (O) Receives the maximum *row* index of the merged cells. 
    minColumn (O) Receives the minimum *column* index of the merged cells.
    maxColumn (O) Receives the maximum *column* index of the merged cells. 
        
  */
  virtual bool isMergedCell(
    OdUInt32 row, 
    OdUInt32 column, 
    OdUInt32* minRow = NULL, 
    OdUInt32* maxRow = NULL,
    OdUInt32* minColumn = NULL, 
    OdUInt32* maxColumn = NULL) const;
  
  /**
    Description:
    Updates this Table entity according to its current table style.
    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  virtual OdResult generateLayout();

  /** Description:
    Updates the block table record referenced by this Table entity.
    
    Arguments:
    forceUpdate (I) Force an update of block table record.
    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
    
    If forceUpdate is false, the block table record is updated
    if and only if this table entity has been changed since the
    block table record was last updated.
    
    If forceUpdate is true, the block table will be unconditionally
    updated.
  */
  virtual OdResult recomputeTableBlock(
    bool forceUpdate = true);
  
  //********************************************************************
  // Methods for sub-selection  
  //********************************************************************
  //
  /*
  virtual bool              hitTest(const OdGePoint3d& wpt, 
                                    const OdGeVector3d& wviewVec,
                                    double wxaper,
                                    double wyaper,
                                    int& resultRowIndex, 
                                    int& resultColumnIndex);

  virtual OdResult select(const OdGePoint3d& wpt, 
                                   const OdGeVector3d& wvwVec, 
                                   const OdGeVector3d& wvwxVec, 
                                   double wxaper,
                                   double wyaper,
                                   bool allowOutside,
                                   bool bInPickFirst, 
                                   int& resultRowIndex, 
                                   int& resultColumnIndex,
                                   OdSubentPathArray* pPaths = NULL) const;

  virtual OdResult selectSubRegion(const OdGePoint3d& wpt1, 
                                   const OdGePoint3d& wpt2,
                                   const OdGeVector3d& wvwVec, 
                                   const OdGeVector3d& wvwxVec,
                                   double wxaper,
                                   double wyaper,                             
                                   OdDb::SelectType seltype,
                                   bool bIncludeCurrentSelection,
                                   bool bInPickFirst,                             
                                   int& rowMin,
                                   int& rowMax,
                                   int& colMin,
                                   int& colMax,
                                   OdSubentPathArray* pPaths = NULL) const;

  virtual bool reselectSubRegion(OdSubentPathArray& paths) const;
          
  virtual OdResult getSubSelection(int& rowMin,
                                    int& rowMax,
                                    int& colMin,
                                    int& colMax) const;

  virtual OdResult setSubSelection(int rowMin,
                                    int rowMax,
                                    int colMin,
                                    int colMax);

  virtual void              clearSubSelection();

  virtual bool              hasSubSelection() const;
  */
  //********************************************************************
  // Overridden methods from OdDbObject
  //********************************************************************
  //

  virtual OdResult dwgInFields (
    OdDbDwgFiler* pFiler);
  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields (
    OdDbDxfFiler* pFiler);
  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  // virtual OdResult audit(OdDbAuditInfo* pAuditInfo);

  void subClose();
  /** Note:
     This function is an override for OdDbEntity::subSetDatabaseDefaults() to set 
     the dimension style of this entity to the current style for the specified *database*.
  */
  void subSetDatabaseDefaults(
    OdDbDatabase *pDb);

  // virtual void              objectClosed(const OdDbObjectId objId);
  // virtual void              erased(const OdDbObject* dbObj,
  //                                  bool pErasing = true);

  virtual void getClassID(
    void** pClsid) const;

  //********************************************************************
  // Overridden methods from OdDbEntity
  //********************************************************************
  //
  /*
  virtual void              list() const;
  */

  virtual bool worldDraw(
    OdGiWorldDraw* pWd) const;

  /*
  virtual OdResult getGripPoints(OdGePoint3dArray&, 
                                          OdDbIntArray&,
                                          OdDbIntArray&) const;

  virtual OdResult moveGripPointsAt(const OdDbIntArray&,
                                             const OdGeVector3d&);

  virtual OdResult getStretchPoints(OdGePoint3dArray& stretchPoints)
                                             const;

  virtual OdResult moveStretchPointsAt(const OdDbIntArray& 
                                               indices,
                                               const OdGeVector3d& offset);

  virtual OdResult getOsnapPoints(OdDb::OsnapMode osnapMode,
                                           int   gsSelectionMark,
                                           const OdGePoint3d&  pickPoint,
                                           const OdGePoint3d&  lastPoint,
                                           const OdGeMatrix3d& viewXform,
                                           OdGePoint3dArray&      snapPoints,
                                           OdDbIntArray&   geomIds)
                                           const;
  */

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);

  virtual OdResult getTransformedCopy(
    const OdGeMatrix3d& xfm,
    OdDbEntityPtr& pCopy) const;

  /*
  virtual OdResult getGeomExtents(OdGeExtents& extents) const;

  virtual OdResult explode(OdDbVoidPtrArray& entitySet) const;
  */

  //********************************************************************
  // Overridden methods from OdDbBlockReference required for OdDbTable
  //********************************************************************
  //
  //arx OdGePoint3d            position() const;
  // virtual OdResult setPosition(const OdGePoint3d&);

  //arx OdGeVector3d           normal() const;
  // virtual OdResult setNormal(const OdGeVector3d& newVal);

  //********************************************************************
  // Methods for internal use only
  //********************************************************************
  //
  // TODO: Temporary method for navigating after editing cells
  /*
  virtual OdResult select_next_cell(int dir,
                                   int& resultRowIndex, 
                                   int& resultColumnIndex,
                                   OdSubentPathArray* pPaths = NULL,
                                   bool bSupportTextCellOnly = true) const;

  virtual void              setRegen();
  virtual void              suppressInvisibleGrid(bool value);
  virtual OdResult getCellExtents(int row, 
                                       int column,
                                       double& cellWidth,
                                       double& cellHeight,
                                       bool bAdjustForMargins) const;
  */
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbTable object pointers.
*/
typedef OdSmartPtr<OdDbTable> OdDbTablePtr;

#include "DD_PackPop.h"

#endif

