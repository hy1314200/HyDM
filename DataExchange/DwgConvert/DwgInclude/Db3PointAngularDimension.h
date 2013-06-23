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



#ifndef _OD_DB_3POINTANGULAR_DIMENSION_
#define _OD_DB_3POINTANGULAR_DIMENSION_

#include "DD_PackPush.h"

#include "DbDimension.h"

/** Description:
    This class represents 3-Point Angular Dimension entities in an OdDbDatabase instance.

    Remarks:
    A 3-Point Angular Dimension entity dimensions the angle defined by three points.
    
    See also:
    OdDb2LineAngularDimension
    
    Library:
    Db

    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDb3PointAngularDimension : public OdDbDimension
{
public:

  ODDB_DECLARE_MEMBERS(OdDb3PointAngularDimension);

  OdDb3PointAngularDimension();
  
  /** Description:
    Returns the point defining the the location of dimension arc for this Dimension entity (DXF 16 as WCS).
  */
  OdGePoint3d arcPoint() const;

  /** Description:
    Sets the point defining the the location of dimension arc for this Dimension entity (DXF 16 as WCS).
    Arguments:
    arcPoint (I) Arc point.
  */
  void setArcPoint(
    const OdGePoint3d& arcPoint);
  
  /** Description:
    Returns the WCS start point of the first extension line of this Dimension entity (DXF 13).
      
    Remarks:
    This point and the centerPoint define one side of 
    the angle being dimensioned.
  */
  OdGePoint3d xLine1Point() const;

  /** Description:
    Sets the WCS start point of the first extension line of this Dimension entity (DXF 13).
  
    Arguments:
    xLine1Point (I) Start point.
        
    Remarks:
    This point and the centerPoint define one side of 
    the angle being dimensioned.
  */
  void setXLine1Point(
    const OdGePoint3d& xLine1Point);

  /** Description:
    Returns the WCS start point of the second extension line of this Dimension entity (DXF 14).
      
    Remarks:
    This point and the centerPoint define one side of 
    the angle being dimensioned.
  */
  OdGePoint3d xLine2Point() const;

  /** Description:
    Sets the WCS start point of the second extension line of this Dimension entity (DXF 14).
  
    Arguments:
    xLine2Point (I) Start point.
        
    Remarks:
    This point and the centerPoint define one side of 
    the angle being dimensioned.
  */
  void setXLine2Point(
    const OdGePoint3d& xLine2Point);

  
  /** Description:
    Returns the WCS vertex of the angle being dimensioned by this Dimension entity (DXF 15).
    Remarks:
    The vertex of the angle being dimensioned is the center of the dimension arc.
  */
  OdGePoint3d centerPoint() const;

  /** Description:
    Sets the WCS vertex of the angle being dimensioned by this Dimension entity (DXF 15).
    Arguments:
    centerPoint (I) Center point.
    Remarks:
    The vertex of the angle being dimensioned is the center of the dimension arc.
 */
  void setCenterPoint(
    const OdGePoint3d& centerPoint);

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
  This template class is a specialization of the OdSmartPtr class for OdDb3PointAngularDimension object pointers.
*/
typedef OdSmartPtr<OdDb3PointAngularDimension> OdDb3PointAngularDimensionPtr;

#include "DD_PackPop.h"

#endif

