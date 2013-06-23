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



#ifndef _OD_DB_2LINEANGULAR_DIMENSION_
#define _OD_DB_2LINEANGULAR_DIMENSION_

#include "DD_PackPush.h"

#include "DbDimension.h"

/** Description:
    This class represents 2-Line Angular Dimension entities in an OdDbDatabase instance.
    
    Remarks:
    A 2-Line Angular Dimension entity dimensions the angle defined by two lines.
    
    See also:
    OdDb3PointAngularDimension
    
    Library:
    Db

    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDb2LineAngularDimension : public OdDbDimension
{
public:

  ODDB_DECLARE_MEMBERS(OdDb2LineAngularDimension);
  OdDb2LineAngularDimension();
  
  /** Description:
    Returns the point defining the location of dimension arc for this Dimension entity (DXF 16 as WCS).
  */
  OdGePoint3d arcPoint() const;

  /** Description:
    Sets the point defining the location of dimension arc for this Dimension entity (DXF 16 as WCS).
    Arguments:
    arcPoint (I) Arc point.
  */
  void setArcPoint(
    const OdGePoint3d& arcPoint);
  
  /** Description:
    Returns the WCS start point of the first extension line of this Dimension entity (DXF 13).
  */
  OdGePoint3d xLine1Start() const;

  /** Description:
    Sets the WCS start point of the first extension line of this Dimension entity (DXF 13).
    Arguments:
    xLine1Start (I) Start point.
  */
  void setXLine1Start(
    const OdGePoint3d& xLine1Start);
  
  /** Description:
    Returns the WCS end point of the first extension line of this Dimension entity (DXF 14).
  */
  OdGePoint3d xLine1End() const;

  /** Description:
    Sets the WCS end point of the first extension line of this Dimension entity (DXF 14).
    Arguments:
    xLine1End (I) End point.
  */
  void setXLine1End(
    const OdGePoint3d& xLine1End);
  
  /** Description:
    Returns the WCS start point of the second extension line of this Dimension entity (DXF 15).
  */
  OdGePoint3d xLine2Start() const;

  /** Description:
    Sets the WCS start point of the second extension line of this Dimension entity (DXF 15).
    Arguments:
    xLine2Start (I) Start point.
  */
  void setXLine2Start(
    const OdGePoint3d& xLine2Start);
  
  /** Description:
    Returns the WCS end point of the second extension line of this Dimension entity (DXF 10).
  */
  OdGePoint3d xLine2End() const;

  /** Description:
    Sets the WCS end point of the second extension line of this Dimension entity (DXF 10).
    Arguments:
    xLine2End (I) End point.
  */
  void setXLine2End(
    const OdGePoint3d& xLine2End);

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
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDb2LineAngularDimension object pointers.
*/
typedef OdSmartPtr<OdDb2LineAngularDimension> OdDb2LineAngularDimensionPtr;

#include "DD_PackPop.h"

#endif

