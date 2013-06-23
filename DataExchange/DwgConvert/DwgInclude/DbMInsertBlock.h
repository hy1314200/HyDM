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



#ifndef _OD_DB_MINSERT_BLOCK_
#define _OD_DB_MINSERT_BLOCK_

#include "DD_PackPush.h"

#include "DbBlockReference.h"

class OdDbMInsertBlockImpl;

/** Description:
    This class represents arrayed instances of block references (MInserts) in an OdDbDatabase instance.

    Library:
    Db

    Remarks:
    Creating an OdDbMInsertBlock instance with exactly one row and column creates an OdDbBlockReference instance. 
    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDbMInsertBlock : public OdDbBlockReference
{
public:
  ODDB_DECLARE_MEMBERS(OdDbMInsertBlock);

  OdDbMInsertBlock();
  
  /** Description:
    Returns the number of *columns* for this MInsert (DXF 70).
  */
  OdUInt16 columns() const;

  /** Description:
    Sets the number of *columns* for this MInsert (DXF 70).
    Arguments:
    numColumns (I) Number of *columns*.
  */
  void setColumns(OdUInt16 numColumns);
  
  /** Description:
    Returns the number of *rows* for this MInsert (DXF 71).
  */
  OdUInt16 rows() const;

  /** Description:
    Sets the number of *rows* for this MInsert (DXF 71).
    Arguments:
    numRows (I) Number of *rows*.
  */
  void  setRows(OdUInt16 numRows);
  
  /** Description:
    Returns the column spacing for this MInsert (DXF 44).
  */
  double columnSpacing() const;

  /** Description:
    Sets the column spacing for this MInsert (DXF 44).
    Arguments:
    colSpacing (I) Column spacing.
  */
  void  setColumnSpacing(double colSpacing);
  
  /** Description:
    Returns the row spacing for this MInsert (DXF 45).
  */
  double rowSpacing() const;

  /** Description:
    Sets the row spacing for this MInsert (DXF 45).
    Arguments:
    rowSpacing (I) Row spacing.
  */
  void  setRowSpacing(
    double rowSpacing);
  
  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual void getClassID(
    void** pClsid) const;

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);


  OdResult getGeomExtents(
    OdGeExtents3d& extents) const;

  virtual bool worldDraw(
    OdGiWorldDraw* pWd) const;
};
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbMInsertBlock object pointers.
*/
typedef OdSmartPtr<OdDbMInsertBlock> OdDbMInsertBlockPtr;

#include "DD_PackPop.h"

#endif

